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
    public partial class update_user : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["userType"] == null || (int)Session["userType"] != 2)
            {
                Response.Redirect("login.aspx");
            }

            if (Session["userToUpdate"] != null)
            {
                if (!IsPostBack)
                {
                    SqlConnection myCon = new SqlConnection(ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString);

                    // Qual a ação - Neste caso insert
                    SqlCommand myCommand = new SqlCommand();

                    myCommand.Connection = myCon;
                    myCommand.CommandType = CommandType.StoredProcedure; // Utilizar uma stored procedure
                    myCommand.CommandText = "get_user_byid"; // Noma da stored procedure

                    myCommand.Parameters.AddWithValue("@id", Session["userToUpdate"]);

                    // Abrir a conexão
                    myCon.Open();

                    // Usar repeater e codeblocks
                    SqlDataReader dr = myCommand.ExecuteReader();
                    if (dr.Read())
                    {
                        tb_email.Text = dr["email"].ToString();
                        ddl_verified.SelectedIndex = (bool)dr["verified"] ? 1 : 0;
                        ddl_active.SelectedIndex = (bool)dr["active"] ? 1 : 0;
                        ddl_type.SelectedIndex = (int)dr["typeId"] - 1;

                        if (dr["ctType"].ToString() != "" && (byte[])dr["image"] != null)
                        {
                            Image1.ImageUrl = "data:" + dr["ctType"].ToString() + ";base64," + Convert.ToBase64String((byte[])dr["image"]);
                        }
                        else
                        {
                            Image1.ImageUrl = "../Assets/images/noimage.png";
                        }
                    }

                    myCon.Close();
                }
            }
            else
            {
                Response.Redirect("manage_numismatics.aspx");
            }
        }

        protected void btn_gotoBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("manage_users.aspx");
        }

        protected void btn_updateUser_Click(object sender, EventArgs e)
        {
            byte[] imgBinaryData = Array.Empty<byte>();
            string query;

            if (FileUpload1.HasFile)
            {
                if (!FileUpload1.PostedFile.ContentType.ToLower().StartsWith("image/"))
                {
                    lbl_infos.Text = "User updated but couldnt update the image! You must upload an Image!";
                }
                else
                {
                    try
                    {
                        // Aponta para o ficheiro selecionado
                        Stream imgStream = FileUpload1.PostedFile.InputStream;

                        // Cria array de bytes com o tamanho do stream
                        imgBinaryData = new byte[imgStream.Length];

                        // Lê o conteúdo do stream para o array
                        imgStream.Read(imgBinaryData, 0, Convert.ToInt32(imgStream.Length));
                    }
                    catch (Exception)
                    {
                        lbl_infos.Text += "\nSomething went wrong! Change Image";
                        throw;
                    }

                    lbl_infos.Text = "User and Image updated successfully!";
                }

                query = @"
                    DECLARE @return INT;

                    IF NOT EXISTS (SELECT 1 FROM Users WHERE email = @email)
                    BEGIN
                        UPDATE Users SET email = @email, verified = @verified, active = @active, typeId = @typeId, ctType = @ctType, image = @image WHERE id = @id
                        SET @return = 1;
                    END
                    ELSE
                    BEGIN
                        UPDATE Users SET verified = @verified, active = @active, typeId = @typeId, ctType = @ctType, image = @image WHERE id = @id
                        SET @return = 0;
                    END

                    SELECT @return;";
            }
            else
            {
                query = @"
                    DECLARE @return INT;

                    IF NOT EXISTS (SELECT 1 FROM Users WHERE email = @email)
                    BEGIN
                        UPDATE Users SET email = @email, verified = @verified, active = @active, typeId = @typeId WHERE id = @id
                        SET @return = 1;
                    END
                    ELSE
                    BEGIN
                        UPDATE Users SET verified = @verified, active = @active, typeId = @typeId WHERE id = @id
                        SET @return = 0;
                    END

                    SELECT @return;";
            }

            // Criar conexão
            SqlConnection myCon = new SqlConnection(ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString);
            SqlCommand myCommand = new SqlCommand(query, myCon);

            // Adicionar parâmetros
            myCommand.Parameters.AddWithValue("@id", Convert.ToInt32(Session["userToUpdate"]));
            myCommand.Parameters.AddWithValue("@email", tb_email.Text);
            myCommand.Parameters.AddWithValue("@verified", ddl_verified.SelectedValue);
            myCommand.Parameters.AddWithValue("@active", ddl_active.SelectedValue);
            myCommand.Parameters.AddWithValue("@typeId", ddl_type.SelectedValue);
            if (FileUpload1.HasFile)
            {
                myCommand.Parameters.AddWithValue("@ctType", FileUpload1.PostedFile.ContentType);
                myCommand.Parameters.AddWithValue("@image", imgBinaryData);
            }

            myCon.Open();

            int response = Convert.ToInt32(myCommand.ExecuteScalar());

            myCon.Close();

            if (response == 1)
            {
                lbl_infos.Text = "User Updated Successfully";
            }
            else
            {
                lbl_infos.Text = "User Update but couldnt update email because already exists!";
            }
        }
    }
}