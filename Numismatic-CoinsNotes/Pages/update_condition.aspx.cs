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
    public partial class update_condition : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["userType"] == null || (int)Session["userType"] != 2)
            {
                Response.Redirect("login.aspx");
            }

            if (Session["conditionToUpdate"] != null)
            {
                if (!IsPostBack)
                {
                    // Criar a conexão - Abrir a connectionString
                    SqlConnection myCon = new SqlConnection(ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString);

                    // Qual a ação - Neste caso insert
                    SqlCommand myCommand = new SqlCommand();

                    myCommand.Connection = myCon;
                    myCommand.CommandType = CommandType.StoredProcedure; // Utilizar uma stored procedure
                    myCommand.CommandText = "get_condition_byid"; // Noma da stored procedure

                    myCommand.Parameters.AddWithValue("@id", Session["conditionToUpdate"]);

                    // Abrir a conexão
                    myCon.Open();

                    // Usar repeater e codeblocks
                    SqlDataReader dr = myCommand.ExecuteReader();

                    if (dr.Read())
                    {
                        tb_condition.Text = dr["condition"].ToString();
                    }

                    myCon.Close();
                }
            }
            else
            {
                Response.Redirect("manage_conditions.aspx");
            }
        }

        protected void btn_gotoCreate_Click(object sender, EventArgs e)
        {
            Response.Redirect("manage_conditions.aspx");
        }

        protected void btn_update_condition_Click(object sender, EventArgs e)
        {
            string query = "UPDATE Conditions SET [condition] = @condition WHERE id = @id";

            // Criar conexão
            SqlConnection myCon = new SqlConnection(ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString);
            SqlCommand myCommand = new SqlCommand(query, myCon);

            // Adicionar parâmetros
            myCommand.Parameters.AddWithValue("@id", Convert.ToInt32(Session["conditionToUpdate"]));
            myCommand.Parameters.AddWithValue("@condition", tb_condition.Text);

            myCon.Open();

            myCommand.ExecuteNonQuery();

            myCon.Close();

            lbl_infos.Text = "Condition updated successfully!";
        }
    }
}