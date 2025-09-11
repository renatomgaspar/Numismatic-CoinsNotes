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
    public class CashType
    {
        public int Id { get; set; }

        public string Type { get; set; }
    }

    public partial class manage_cash_type : System.Web.UI.Page
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
            if (e.CommandName == "DeleteItem")
            {
                int id = Convert.ToInt32(e.CommandArgument);

                if (id != null)
                {
                    // Criar a conexão - Abrir a connectionString
                    SqlConnection myCon = new SqlConnection(ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString);

                    SqlCommand myCommand = new SqlCommand();

                    myCommand.Connection = myCon;
                    myCommand.CommandType = CommandType.StoredProcedure;
                    myCommand.CommandText = "delete_cashtype";

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

                    ShowCashTypes();
                }
            }

            if (e.CommandName == "UpdateItem")
            {
                int id = Convert.ToInt32(e.CommandArgument);

                Session["cashtypeToUpdate"] = id;

                // Redireciona
                Response.Redirect("update_cashtype.aspx");
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
            myCommand.CommandText = "list_cashtype"; // Noma da stored procedure

            // Abrir a conexão
            myCon.Open();

            // Usar repeater e codeblocks
            SqlDataReader dr = myCommand.ExecuteReader();
            List<CashType> list_cashtype = new List<CashType>();

            while (dr.Read())
            {
                CashType c = new CashType();

                c.Id = (int)dr["id"];
                c.Type = dr["type"].ToString();

                list_cashtype.Add(c);
            }

            myCon.Close();

            // Colocar os dados no repeater
            Repeater1.DataSource = list_cashtype;
            Repeater1.DataBind();
        }
    }
}