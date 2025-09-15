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
    public class UserType
    {
        public int Id { get; set; }

        public string Type { get; set; }
    }


    public partial class manage_usertypes : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["userType"] == null || (int)Session["userType"] != 2)
            {
                Response.Redirect("login.aspx");
            }
            else
            {
                ShowCashTypes();
            }
        }

        protected void Repeater1_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "UpdateItem")
            {
                int id = Convert.ToInt32(e.CommandArgument);

                Session["usertypeToUpdate"] = id;

                // Redireciona
                Response.Redirect("update_usertype.aspx");
            }
        }

        private void ShowCashTypes()
        {
            // Criar a conexão - Abrir a connectionString
            SqlConnection myCon = new SqlConnection(ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString);

            // Qual a ação - Neste caso insert
            SqlCommand myCommand = new SqlCommand();

            myCommand.Connection = myCon;
            myCommand.CommandType = CommandType.StoredProcedure; // Utilizar uma stored procedure
            myCommand.CommandText = "list_usertype"; // Noma da stored procedure

            // Abrir a conexão
            myCon.Open();

            // Usar repeater e codeblocks
            SqlDataReader dr = myCommand.ExecuteReader();
            List<UserType> list_usertype = new List<UserType>();

            while (dr.Read())
            {
                UserType c = new UserType();

                c.Id = (int)dr["id"];
                c.Type = dr["type"].ToString();

                list_usertype.Add(c);
            }

            myCon.Close();

            // Colocar os dados no repeater
            Repeater1.DataSource = list_usertype;
            Repeater1.DataBind();
        }
    }
}