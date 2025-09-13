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
    public partial class manage_numismatics : System.Web.UI.Page
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

            public bool Active { get; set; }
        }

        List<Cash> numismatics_list_class = new List<Cash>();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["userType"] == null || (int)Session["userType"] != 2)
            {
                Response.Redirect("login.aspx");
            }
            else
            {
                ShowNumismatics();

                if (!IsPostBack)
                {
                    Repeater1.DataSource = numismatics_list_class;
                    Repeater1.DataBind();
                }
            }
        }

        protected void Repeater1_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "DeleteItem")
            {
                int id = Convert.ToInt32(e.CommandArgument);

                string query = "UPDATE Cash SET [active] = 0 WHERE id = @id";

                // Criar conexão
                SqlConnection myCon = new SqlConnection(ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString);
                SqlCommand myCommand = new SqlCommand(query, myCon);

                // Adicionar parâmetros
                myCommand.Parameters.AddWithValue("@id", id);

                myCon.Open();

                myCommand.ExecuteNonQuery();

                myCon.Close();

                string message = "Numismatic Deleted Successfully!";
                string script = $"<script type='text/javascript'>alert('{message}');</script>";

                ClientScript.RegisterStartupScript(this.GetType(), "alert", script);

                ShowNumismatics();
                ApplyFilters();
            }

            if (e.CommandName == "UpdateItem")
            {
                int id = Convert.ToInt32(e.CommandArgument);

                Session["cashtypeToUpdate"] = id;

                // Redireciona
                Response.Redirect("update_cashtype.aspx");
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

            Repeater1.DataSource = numismatics_list_class_filter;
            Repeater1.DataBind();
        }

        protected void btn_clearfilters_Click(object sender, EventArgs e)
        {
            ddl_cashtype.SelectedIndex = 0;
            ddl_price.SelectedIndex = 0;

            Repeater1.DataSource = numismatics_list_class;
            Repeater1.DataBind();
        }

        protected void ShowNumismatics()
        {
            numismatics_list_class.Clear();

            SqlConnection myCon = new SqlConnection(ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString);

            SqlCommand myCommand = new SqlCommand();
            myCommand.Connection = myCon;
            myCommand.CommandType = CommandType.StoredProcedure;
            myCommand.CommandText = "get_numismatics";

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
                c.Active = (bool)dr["active"];

                numismatics_list_class.Add(c);
            }

            myCon.Close();
        }

        protected void btn_new_numismatic_Click(object sender, EventArgs e)
        {
            Response.Redirect("new_numismatic.aspx");
        }
        
    }
}