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
    public partial class statistics : System.Web.UI.Page
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

            public string Image { get; set; }
        }

        public class User
        {
            public int Id { get; set; }

            public string Email { get; set; }

            public string Average { get; set; }

            public string Image { get; set; }
        }

        List<Cash> numismatics_top10_class = new List<Cash>();
        List<User> users_average_class = new List<User>();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["userType"] == null || (int)Session["userType"] != 2)
            {
                Response.Redirect("login.aspx");
            }

            if (!IsPostBack)
            {
                SqlConnection myCon = new SqlConnection(ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString);

                // Qual a ação - Neste caso insert
                SqlCommand myCommand = new SqlCommand();

                myCommand.Connection = myCon;
                myCommand.CommandType = CommandType.StoredProcedure; // Utilizar uma stored procedure
                myCommand.CommandText = "get_statistics"; // Noma da stored procedure

                SqlParameter totalUsersParam = new SqlParameter("@totalUsers", SqlDbType.Int);
                totalUsersParam.Direction = ParameterDirection.Output;
                myCommand.Parameters.Add(totalUsersParam);

                SqlParameter usersWithCollectionsParam = new SqlParameter("@totalUsersWithCollections", SqlDbType.Int);
                usersWithCollectionsParam.Direction = ParameterDirection.Output;
                myCommand.Parameters.Add(usersWithCollectionsParam);

                SqlParameter topUserParam = new SqlParameter("@userWithMoreNumismatics", SqlDbType.VarChar, 50);
                topUserParam.Direction = ParameterDirection.Output;
                myCommand.Parameters.Add(topUserParam);

                SqlParameter totalNumismaticsParam = new SqlParameter("@totalNumismatics", SqlDbType.Int);
                totalNumismaticsParam.Direction = ParameterDirection.Output;
                myCommand.Parameters.Add(totalNumismaticsParam);

                // Abrir a conexão
                myCon.Open();

                // Usar repeater e codeblocks
                SqlDataReader dr = myCommand.ExecuteReader();

                while (dr.Read())
                {
                    Cash c = new Cash();
                    c.Id = (int)dr["cashIdCash"];
                    c.Title = dr["title"].ToString();
                    c.Description = dr["description"].ToString();
                    c.Type = dr["type"].ToString();
                    c.Condition = dr["condition"].ToString();
                    c.Imprintvalue = Convert.ToSingle(dr["imprintValue"]);
                    c.Currentvalue = Convert.ToSingle(dr["currentValue"]);
                    c.Active = (bool)dr["active"];

                    if (c.Active == false)
                    {
                        c.Image = "../Assets/images/eliminated.jpg";
                    }
                    else
                    {
                        if (dr["ctImage"].ToString() != "" && (byte[])dr["imageC"] != null)
                        {
                            c.Image = "data:" + dr["ctImage"].ToString() + ";base64," + Convert.ToBase64String((byte[])dr["imageC"]);
                        }
                        else
                        {
                            c.Image = "../Assets/images/noimage.png";
                        }
                    }

                    numismatics_top10_class.Add(c);
                }

                if (dr.NextResult())
                {
                    while (dr.Read())
                    {
                        User u = new User();
                        u.Email = dr["email"].ToString();
                        u.Average = dr["MediaValorMoedasFavoritas"].ToString();

                        if (dr["ctType"].ToString() != "" && (byte[])dr["image"] != null)
                        {
                            u.Image = "data:" + dr["ctType"].ToString() + ";base64," + Convert.ToBase64String((byte[])dr["image"]);
                        }
                        else
                        {
                            u.Image = "../Assets/images/noimage.png";
                        }

                        users_average_class.Add(u);
                    }
                }

                myCon.Close();

                lbl_totalUsers.Text = totalUsersParam.Value.ToString();
                lbl_totalUserWithCollections.Text = usersWithCollectionsParam.Value.ToString();
                lbl_user.Text = topUserParam.Value.ToString();
                lbl_quantity.Text = totalNumismaticsParam.Value.ToString();

                Repeater1.DataSource = numismatics_top10_class;
                Repeater1.DataBind();

                Repeater2.DataSource = users_average_class.OrderByDescending(u => float.Parse(u.Average));
                Repeater2.DataBind();
            }
        }
    }
}