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
    public partial class new_usertype : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["userType"] == null || (int)Session["userType"] != 2)
            {
                Response.Redirect("login.aspx");
            }
        }

        protected void btn_createType_Click(object sender, EventArgs e)
        {
            if (tb_type.Text == "")
            {
                lbl_infos.Text = "You must write the type of the User Type!";
            }
            else
            {
                // Criar a conexão - Abrir a connectionString
                SqlConnection myCon = new SqlConnection(ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString);

                SqlCommand myCommand = new SqlCommand();

                myCommand.Connection = myCon;
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.CommandText = "create_usertype";

                // O que queremos inserir/enviar
                myCommand.Parameters.AddWithValue("@type", tb_type.Text);
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
                    lbl_infos.Text = "User Type Created!";
                }
                else
                {
                    lbl_infos.Text = "There is already a User Type with that Name!";
                }
            }
        }

        protected void btn_gotoBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("manage_usertypes.aspx");
        }
    }
}