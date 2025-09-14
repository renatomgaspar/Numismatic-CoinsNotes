using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Numismatic_CoinsNotes.Pages
{
    public partial class manage_users : System.Web.UI.Page
    {
        public class User
        {
            public int Id { get; set; }

            public string Name { get; set; }

            public string Email { get; set; }

            public bool Verified { get; set; }

            public bool Active { get; set; }

            public string Type { get; set; }

            public string Image { get; set; }
        }

        List<User> users_list_class = new List<User>();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["userType"] == null || (int)Session["userType"] != 2)
            {
                Response.Redirect("login.aspx");
            }

            if (!IsPostBack)
            {
                ShowUsers();
                Repeater1.DataSource = users_list_class.OrderByDescending(n => n.Active);
                Repeater1.DataBind();
            }
        }

        protected void ShowUsers()
        {
            users_list_class.Clear();

            SqlConnection myCon = new SqlConnection(ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString);

            SqlCommand myCommand = new SqlCommand();
            myCommand.Connection = myCon;
            myCommand.CommandType = CommandType.StoredProcedure;
            myCommand.CommandText = "get_users";

            myCon.Open();
            SqlDataReader dr = myCommand.ExecuteReader();

            while (dr.Read())
            {
                User u = new User();
                u.Id = (int)dr["id"];
                u.Name = dr["name"].ToString();
                u.Email = dr["email"].ToString();
                u.Verified = (bool)dr["verified"];
                u.Active = (bool)dr["active"];
                u.Type = dr["type"].ToString();

                if (dr["ctType"].ToString() != "" && (byte[])dr["image"] != null)
                {
                    u.Image = "data:" + dr["ctType"].ToString() + ";base64," + Convert.ToBase64String((byte[])dr["image"]);
                }
                else
                {
                    u.Image = "../Assets/images/noimage.png";
                }

                users_list_class.Add(u);
            }

            myCon.Close();
        }

        protected void btn_new_user_Click(object sender, EventArgs e)
        {
            Response.Redirect("new_user.aspx");
        }

        protected void Repeater1_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            int id = Convert.ToInt32(e.CommandArgument);

            if (e.CommandName == "DeleteItem")
            {
                string query = "UPDATE Users SET [active] = 0 WHERE id = @id";

                // Criar conexão
                SqlConnection myCon = new SqlConnection(ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString);
                SqlCommand myCommand = new SqlCommand(query, myCon);

                // Adicionar parâmetros
                myCommand.Parameters.AddWithValue("@id", id);

                myCon.Open();

                myCommand.ExecuteNonQuery();

                myCon.Close();

                string message = "User Deactivated Successfully!";
                string script = $"<script type='text/javascript'>alert('{message}');</script>";

                ClientScript.RegisterStartupScript(this.GetType(), "alert", script);

                ShowUsers();

                Repeater1.DataSource = users_list_class.OrderByDescending(n => n.Active);
                Repeater1.DataBind();
            }

            if (e.CommandName == "UpdateItem")
            {
                Session["userToUpdate"] = id;

                Response.Redirect("update_user.aspx");
            }

            if (e.CommandName == "ActiveItem")
            {
                string query = "UPDATE Users SET [active] = 1 WHERE id = @id";

                // Criar conexão
                SqlConnection myCon = new SqlConnection(ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString);
                SqlCommand myCommand = new SqlCommand(query, myCon);

                // Adicionar parâmetros
                myCommand.Parameters.AddWithValue("@id", id);

                myCon.Open();

                myCommand.ExecuteNonQuery();

                myCon.Close();

                string message = "User Actived Successfully!";
                string script = $"<script type='text/javascript'>alert('{message}');</script>";

                ClientScript.RegisterStartupScript(this.GetType(), "alert", script);

                ShowUsers();

                Repeater1.DataSource = users_list_class.OrderByDescending(n => n.Active);
                Repeater1.DataBind();
            }
        }
    }
}