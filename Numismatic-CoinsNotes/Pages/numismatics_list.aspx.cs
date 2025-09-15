using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Numismatic_CoinsNotes.Pages
{
    public partial class numismatics_list : System.Web.UI.Page
    {
        public class Cash
        {
            public int Id { get; set; }

            public string Title { get; set; }

            public string Description { get; set; }

            public string Type { get; set; }

            public string Condition { get; set; }

            public float Imprintvalue { get; set; }

            public float Currentvalue { get; set; }

            public string Image { get; set; }
        }

        List<Cash> numismatics_list_class = new List<Cash>();

        protected void Page_Load(object sender, EventArgs e)
        {
            SqlConnection myCon = new SqlConnection(ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString);

            SqlCommand myCommand = new SqlCommand();
            myCommand.Connection = myCon;
            myCommand.CommandType = CommandType.StoredProcedure;
            myCommand.CommandText = "get_numismatics";

            myCommand.Parameters.AddWithValue("@userType", 0);

            myCon.Open();
            SqlDataReader dr = myCommand.ExecuteReader();

            while (dr.Read())
            {
                Cash c = new Cash();
                c.Id = (int)dr["id"];
                c.Title = dr["title"].ToString();
                c.Description = dr["description"].ToString();
                c.Type = dr["type"].ToString();
                c.Condition = dr["condition"].ToString();
                c.Imprintvalue = Convert.ToSingle(dr["imprintValue"]);
                c.Currentvalue = Convert.ToSingle(dr["currentValue"]);

                numismatics_list_class.Add(c);
            }

            myCon.Close();

            if (!IsPostBack)
            {
                Repeater1.DataSource = numismatics_list_class;
                Repeater1.DataBind();
            }
        }

        protected void Repeater1_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "SaveToFavourite")
            {
                if (Session["userType"] == null)
                {
                    Response.Redirect("login.aspx");
                }
                else
                {
                    int id = Convert.ToInt32(e.CommandArgument.ToString());

                    // Criar a conexão - Abrir a connectionString
                    SqlConnection myCon = new SqlConnection(ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString);

                    SqlCommand myCommand = new SqlCommand();

                    myCommand.Connection = myCon;
                    myCommand.CommandType = CommandType.StoredProcedure;
                    myCommand.CommandText = "add_cash_to_favourites";

                    // O que queremos inserir/enviar
                    myCommand.Parameters.AddWithValue("@cashId", id);
                    myCommand.Parameters.AddWithValue("@email", Session["userEmail"]);
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

                    // 1 -> User | 4 -> Admin
                    if (response == 1)
                    {
                        string message = "Added to Favorites!";
                        string script = $"<script type='text/javascript'>alert('{message}');</script>";
                        ApplyFilters();

                        ClientScript.RegisterStartupScript(this.GetType(), "alert", script);
                    }
                    else
                    {
                        string message = "Someting went wrong!";
                        string script = $"<script type='text/javascript'>alert('{message}');</script>";

                        ClientScript.RegisterStartupScript(this.GetType(), "alert", script);
                    }
                }
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

        protected void tb_search_TextChanged(object sender, EventArgs e)
        {
            ApplyFilters();
        }

        protected void ApplyFilters()
        {
            var numismatics_list_class_filter = numismatics_list_class;

            if (!string.IsNullOrEmpty(ddl_cashtype.SelectedValue) && ddl_cashtype.SelectedValue != "All")
            {
                numismatics_list_class_filter = numismatics_list_class_filter.Where(c => c.Type == ddl_cashtype.SelectedValue).ToList();
            }

            if (ddl_price.SelectedValue == "ASC")
            {
                numismatics_list_class_filter = numismatics_list_class_filter.OrderBy(c => c.Currentvalue).ToList();
            }
            else if (ddl_price.SelectedValue == "DESC")
            {
                numismatics_list_class_filter = numismatics_list_class_filter.OrderByDescending(c => c.Currentvalue).ToList();
            }

            if (!string.IsNullOrEmpty(tb_search.Text.Trim()))
            {
                numismatics_list_class_filter = numismatics_list_class_filter.Where(c => c.Title.ToLower().Contains(tb_search.Text.Trim().ToLower())).ToList();
            }

            Repeater1.DataSource = numismatics_list_class_filter;
            Repeater1.DataBind();
        }

        protected void btn_clearfilters_Click(object sender, EventArgs e)
        {
            ddl_cashtype.SelectedIndex = 0;
            ddl_price.SelectedIndex = 0;
            tb_search.Text = "";

            Repeater1.DataSource = numismatics_list_class;
            Repeater1.DataBind();
        }
    }
}