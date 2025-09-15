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
    public partial class home : System.Web.UI.Page
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

                if (!numismatics_list_class.Any(x => x.Title == c.Title))
                {
                    numismatics_list_class.Add(c);
                }

                if (numismatics_list_class.Count >= 4)
                {
                    break; 
                }
            }

            myCon.Close();

            if (!IsPostBack)
            {
                Repeater1.DataSource = numismatics_list_class;
                Repeater1.DataBind();
            }
        }
    }
}