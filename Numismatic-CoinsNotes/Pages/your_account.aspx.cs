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
    public partial class your_account : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["userType"] == null)
            {
                Response.Redirect("login.aspx");
            }
            else if ((int)Session["userType"] == 2)
            {
                btn_adminPage.Visible = true;
            }

            SqlConnection myCon = new SqlConnection(ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString);

            // Qual a ação - Neste caso insert
            SqlCommand myCommand = new SqlCommand();

            myCommand.Connection = myCon;
            myCommand.CommandType = CommandType.StoredProcedure; // Utilizar uma stored procedure
            myCommand.CommandText = "get_user_infos"; // Noma da stored procedure

            // O que queremos inserir/enviar
            // myCommand.Parameters.AddWithValue("@campoTabela", valorAPassar);
            myCommand.Parameters.AddWithValue("@email", Session["userEmail"]); // Request.QueryString["num"] - apanhar o valor num passado por link

            // Abrir a conexão
            myCon.Open();
            SqlDataReader dr = myCommand.ExecuteReader(); // PARA LEITURA DE INFORMAÇÃO - Vai ser guardado lá

            if (dr.Read())
            {
                lbl_name.Text = dr["name"].ToString();
                lbl_email.Text = dr["email"].ToString();
                if (dr["image"] != DBNull.Value)
                {
                    userImage.ImageUrl = "data:" + dr["ctType"].ToString() + ";base64," + Convert.ToBase64String((byte[])dr["image"]);
                }
                else
                {
                    userImage.ImageUrl = "../Assets/images/userDefaultImage.png";
                }
            }

            myCon.Close();
        }

        protected void btn_changePassword_Click(object sender, EventArgs e)
        {

        }

        protected void btn_logout_Click(object sender, EventArgs e)
        {
            Session.Remove("userType");
            Session.Remove("userEmail");

            Response.Redirect("home.aspx");
        }
    }
}