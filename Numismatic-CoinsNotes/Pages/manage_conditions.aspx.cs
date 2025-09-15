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
    public partial class manage_conditions : System.Web.UI.Page
    {
        public class Condition
        {
            public int Id { get; set; }

            public string ConditionName { get; set; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["userType"] == null || (int)Session["userType"] != 2)
            {
                Response.Redirect("login.aspx");
            }
            else
            {
                ShowConditions();
            }
        }

        protected void Repeater1_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "UpdateItem")
            {
                int id = Convert.ToInt32(e.CommandArgument);

                Session["conditionToUpdate"] = id;

                // Redireciona
                Response.Redirect("update_condition.aspx");
            }
        }

        private void ShowConditions()
        {
            // Criar a conexão - Abrir a connectionString
            SqlConnection myCon = new SqlConnection(ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString);

            // Qual a ação - Neste caso insert
            SqlCommand myCommand = new SqlCommand();

            myCommand.Connection = myCon;
            myCommand.CommandType = CommandType.StoredProcedure; // Utilizar uma stored procedure
            myCommand.CommandText = "list_conditions"; // Noma da stored procedure

            // Abrir a conexão
            myCon.Open();

            // Usar repeater e codeblocks
            SqlDataReader dr = myCommand.ExecuteReader();
            List<Condition> list_cashtype = new List<Condition>();

            while (dr.Read())
            {
                Condition c = new Condition();

                c.Id = (int)dr["id"];
                c.ConditionName = dr["condition"].ToString();

                list_cashtype.Add(c);
            }

            myCon.Close();

            // Colocar os dados no repeater
            Repeater1.DataSource = list_cashtype;
            Repeater1.DataBind();
        }
    }
}