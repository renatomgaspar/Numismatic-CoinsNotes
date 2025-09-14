using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Numismatic_CoinsNotes.Pages
{
    public partial class update_numismatic : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["userType"] == null || (int)Session["userType"] != 2)
            {
                Response.Redirect("login.aspx");
            }

            if (Session["cashToUpdate"] != null)
            {
                if (!IsPostBack)
                {
                    SqlConnection myCon = new SqlConnection(ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString);

                    // Qual a ação - Neste caso insert
                    SqlCommand myCommand = new SqlCommand();

                    myCommand.Connection = myCon;
                    myCommand.CommandType = CommandType.StoredProcedure; // Utilizar uma stored procedure
                    myCommand.CommandText = "get_numismatic_byid"; // Noma da stored procedure

                    myCommand.Parameters.AddWithValue("@id", Session["cashToUpdate"]);

                    // Abrir a conexão
                    myCon.Open();

                    // Usar repeater e codeblocks
                    SqlDataReader dr = myCommand.ExecuteReader();
                    if (dr.Read())
                    {
                        tb_title.Text = dr["title"].ToString();
                        tb_description.Text = dr["description"].ToString();
                        ddl_type.SelectedIndex = (int)dr["cashType"] - 1;
                        ddl_condition.SelectedIndex = (int)dr["conditionId"] - 1;
                        tb_imprintValue.Text = dr["imprintValue"].ToString();
                        tb_currentValue.Text = dr["currentValue"].ToString();
                        Session["imageId"] = (int)dr["imageId"];

                        if (dr["ctImage"].ToString() != "" && (byte[])dr["image"] != null)
                        {
                            Image1.ImageUrl = "data:" + dr["ctImage"].ToString() + ";base64," + Convert.ToBase64String((byte[])dr["image"]);
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
            Response.Redirect("manage_numismatics.aspx");
        }

        protected void btn_updateNumismatic_Click(object sender, EventArgs e)
        {
            string query = @"UPDATE Cash SET [title] = @title, [description] = @description, [cashType] = @cashType, [conditionId] = @conditionId,
                            [imprintValue] = @imprintValue, [currentValue] = @currentValue, [active] = @active WHERE id = @id";

            // Criar conexão
            SqlConnection myCon = new SqlConnection(ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString);
            SqlCommand myCommand = new SqlCommand(query, myCon);

            // Adicionar parâmetros
            myCommand.Parameters.AddWithValue("@id", Convert.ToInt32(Session["cashToUpdate"]));
            myCommand.Parameters.AddWithValue("@title", tb_title.Text);
            myCommand.Parameters.AddWithValue("@description", tb_description.Text);
            myCommand.Parameters.AddWithValue("@cashType", ddl_type.SelectedIndex + 1); 
            myCommand.Parameters.AddWithValue("@conditionId", ddl_condition.SelectedIndex + 1);
            myCommand.Parameters.AddWithValue("@imprintValue", tb_imprintValue.Text);
            myCommand.Parameters.AddWithValue("@currentValue", tb_currentValue.Text);
            myCommand.Parameters.AddWithValue("@active", ddl_active.SelectedValue);

            myCon.Open();

            myCommand.ExecuteNonQuery();

            myCon.Close();

            lbl_infos.Text = "Cash Updated Successfully";

            if (FileUpload1.HasFile)
            {
                if (!FileUpload1.PostedFile.ContentType.ToLower().StartsWith("image/"))
                {
                    lbl_infos.Text = "Cash updated but couldnt update the image! You must upload an Image!";
                }
                else
                {
                    try
                    {
                        // Aponta para o ficheiro selecionado
                        Stream imgStream = FileUpload1.PostedFile.InputStream;

                        // Cria array de bytes com o tamanho do stream
                        byte[] imgBinaryData = new byte[imgStream.Length];

                        // Lê o conteúdo do stream para o array
                        imgStream.Read(imgBinaryData, 0, Convert.ToInt32(imgStream.Length));

                        query = @"UPDATE [Images] SET [ctImage] = @ctImage, [image] = @image WHERE [id] = @imageId";

                        // Criar conexão
                        myCon = new SqlConnection(ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString);
                        myCommand = new SqlCommand(query, myCon);

                        // Adicionar parâmetros
                        myCommand.Parameters.AddWithValue("@imageId", Session["imageId"]);
                        myCommand.Parameters.AddWithValue("@ctImage", FileUpload1.PostedFile.ContentType);
                        myCommand.Parameters.AddWithValue("@image", imgBinaryData);

                        Image1.ImageUrl = "data:" + FileUpload1.PostedFile.ContentType + ";base64," + Convert.ToBase64String(imgBinaryData);

                        myCon.Open();

                        myCommand.ExecuteNonQuery();

                        myCon.Close();
                    }
                    catch (Exception)
                    {
                        lbl_infos.Text += "\nSomething went wrong! Change Image";
                        throw;
                    }

                    lbl_infos.Text = "Cash and Image updated successfully!";
                }
            }

            
        }
    }
}