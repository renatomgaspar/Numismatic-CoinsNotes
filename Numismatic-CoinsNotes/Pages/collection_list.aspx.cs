using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Numismatic_CoinsNotes.Pages
{
    public partial class collection_list : System.Web.UI.Page
    {
        public class Favourite
        {
            public int Id { get; set; }

            public string Title { get; set; }

            public string Description { get; set; }

            public string Type { get; set; }

            public string Condition { get; set; }

            public float Imprintvalue { get; set; }

            public float Currentvalue { get; set; }

            public bool Active { get; set; }
        }

        List<Favourite> your_collection_list = new List<Favourite>();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["userEmail"] == null)
            {
                Response.Redirect("login.aspx");
            }
            else
            {
                ShowFavourites();

                if (!IsPostBack)
                {
                    Repeater1.DataSource = your_collection_list;
                    Repeater1.DataBind();
                }
            }
        }

        protected void Repeater1_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "DeleteItem")
            {
                int id = Convert.ToInt32(e.CommandArgument.ToString());
                Response.Write(id);

                // Criar a conexão - Abrir a connectionString
                SqlConnection myCon = new SqlConnection(ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString);

                SqlCommand myCommand = new SqlCommand();

                myCommand.Connection = myCon;
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.CommandText = "delete_your_favourite";

                // O que queremos inserir/enviar
                myCommand.Parameters.AddWithValue("@id", id);
                // Parameter de retorno
                SqlParameter value = new SqlParameter();
                value.ParameterName = "@return";
                value.Direction = ParameterDirection.Output;
                value.SqlDbType = SqlDbType.Int;

                myCommand.Parameters.Add(value);

                // Abrir a conexão
                myCon.Open();
                myCommand.ExecuteNonQuery(); // Executar a nossa stored procedure
                int response = Convert.ToInt32(myCommand.Parameters["@return"].Value);
                myCon.Close();

                if (response == 0)
                {
                    string message = "Someting went wrong!";
                    string script = $"<script type='text/javascript'>alert('{message}');</script>";

                    ClientScript.RegisterStartupScript(this.GetType(), "alert", script);
                }

                ShowFavourites();
                ApplyFilters();
            }
        }

        protected void ddl_price_SelectedIndexChanged(object sender, EventArgs e)
        {
            ApplyFilters();
        }

        protected void ddl_cashtype_SelectedIndexChanged(object sender, EventArgs e)
        {
            ApplyFilters();
        }

        private void ShowFavourites()
        {
            your_collection_list.Clear();

            SqlConnection myCon = new SqlConnection(ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString);

            SqlCommand myCommand = new SqlCommand();
            myCommand.Connection = myCon;
            myCommand.CommandType = CommandType.StoredProcedure;
            myCommand.CommandText = "get_your_collection";

            // O que queremos inserir/enviar
            myCommand.Parameters.AddWithValue("@email", Session["userEmail"]);

            // Abrir a conexão
            myCon.Open();

            SqlDataReader dr = myCommand.ExecuteReader();

            while (dr.Read())
            {
                Favourite c = new Favourite();
                c.Id = (int)dr["favouriteId"];
                c.Title = dr["title"].ToString();
                c.Description = dr["description"].ToString();
                c.Type = dr["type"].ToString();
                c.Condition = dr["condition"].ToString();
                c.Imprintvalue = Convert.ToSingle(dr["imprintValue"]);
                c.Currentvalue = Convert.ToSingle(dr["currentValue"]);
                c.Active = (bool)dr["active"];

                your_collection_list.Add(c);
            }

            myCon.Close();
        }

        protected void ApplyFilters()
        {
            var your_collection_list_filter = your_collection_list;

            if (!string.IsNullOrEmpty(ddl_cashtype.SelectedValue) && ddl_cashtype.SelectedValue != "All")
            {
                your_collection_list_filter = your_collection_list_filter.Where(c => c.Type == ddl_cashtype.SelectedValue).ToList();
            }

            if (ddl_price.SelectedValue == "ASC")
            {
                your_collection_list_filter = your_collection_list_filter.OrderBy(c => c.Currentvalue).ToList();
            }
            else if (ddl_price.SelectedValue == "DESC")
            {
                your_collection_list_filter = your_collection_list_filter.OrderByDescending(c => c.Currentvalue).ToList();
            }

            Repeater1.DataSource = your_collection_list_filter;
            Repeater1.DataBind();
        }

        protected void btn_clearfilters_Click(object sender, EventArgs e)
        {
            ddl_cashtype.SelectedIndex = 0;
            ddl_price.SelectedIndex = 0;

            Repeater1.DataSource = your_collection_list;
            Repeater1.DataBind();
        }
    }
}