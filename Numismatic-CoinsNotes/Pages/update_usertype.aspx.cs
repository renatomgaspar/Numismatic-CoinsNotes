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
    public partial class update_usertype : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["userType"] == null || (int)Session["userType"] != 2)
            {
                Response.Redirect("login.aspx");
            }

            if (Session["usertypeToUpdate"] != null)
            {
                if (!IsPostBack)
                {
                    // Criar a conexão - Abrir a connectionString
                    SqlConnection myCon = new SqlConnection(ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString);

                    // Qual a ação - Neste caso insert
                    SqlCommand myCommand = new SqlCommand();

                    myCommand.Connection = myCon;
                    myCommand.CommandType = CommandType.StoredProcedure; // Utilizar uma stored procedure
                    myCommand.CommandText = "get_userype_byid"; // Noma da stored procedure

                    myCommand.Parameters.AddWithValue("@id", Session["usertypeToUpdate"]);

                    // Abrir a conexão
                    myCon.Open();

                    // Usar repeater e codeblocks
                    SqlDataReader dr = myCommand.ExecuteReader();

                    if (dr.Read())
                    {
                        tb_type.Text = dr["type"].ToString();
                    }

                    myCon.Close();
                }
            }
            else
            {
                Response.Redirect("manage_usertypes.aspx");
            }
        }

        protected void btn_gotoCreate_Click(object sender, EventArgs e)
        {
            Response.Redirect("manage_usertypes.aspx");
        }

        protected void btn_update_cashtype_Click(object sender, EventArgs e)
        {
            string query = @"
                DECLARE @return INT;

                IF NOT EXISTS (SELECT 1 FROM Usertypes WHERE [type] = @type)
                BEGIN
                    UPDATE Usertypes SET [type] = @type WHERE id = @id;
                    SET @return = 1;
                END
                ELSE
                BEGIN
                    SET @return = 0;
                END

                SELECT @return;
                ";

            // Criar conexão
            SqlConnection myCon = new SqlConnection(ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString);
            SqlCommand myCommand = new SqlCommand(query, myCon);

            // Adicionar parâmetros
            myCommand.Parameters.AddWithValue("@id", Convert.ToInt32(Session["usertypeToUpdate"]));
            myCommand.Parameters.AddWithValue("@type", tb_type.Text);

            myCon.Open();

            int response = Convert.ToInt32(myCommand.ExecuteScalar());

            myCon.Close();

            if (response == 1)
            {
                lbl_infos.Text = "User type updated successfully!";
            }
            else
            {
                lbl_infos.Text = "This type already exists!";
            }
        }
    }
}