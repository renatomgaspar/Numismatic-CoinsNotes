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
    public partial class new_numismatic : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["userType"] == null || (int)Session["userType"] != 2)
            {
                Response.Redirect("login.aspx");
            }
        }

        protected void btn_createNumismatic_Click(object sender, EventArgs e)
        {
            if (tb_title.Text == "" || tb_description.Text == "" || tb_imprintValue.Text == "" || tb_currentValue.Text == "" || string.IsNullOrEmpty(ddl_type.SelectedValue) || string.IsNullOrEmpty(ddl_condition.SelectedValue))
            {
                lbl_infos.Text = "You must write and choose all the fields!";
            }
            else
            {
                byte[] imgBinaryData;

                if (FileUpload1.HasFile)
                {
                    // Aponta para o ficheiro selecionado
                    Stream imgStream = FileUpload1.PostedFile.InputStream;

                    // Cria array de bytes com o tamanho do stream
                    imgBinaryData = new byte[imgStream.Length];

                    // Lê o conteúdo do stream para o array
                    imgStream.Read(imgBinaryData, 0, Convert.ToInt32(imgStream.Length));
                }
                else
                {
                    // Caminho da imagem padrão
                    string defaultImagePath = Server.MapPath("../Assets/images/noimage.png");

                    // Lê o conteúdo da imagem default
                    imgBinaryData = File.ReadAllBytes(defaultImagePath);
                }


                // Criar a conexão - Abrir a connectionString
                SqlConnection myCon = new SqlConnection(ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString);

                SqlCommand myCommand = new SqlCommand();

                myCommand.Connection = myCon;
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.CommandText = "create_numismatic";

                // O que queremos inserir/enviar
                myCommand.Parameters.AddWithValue("@title", tb_title.Text);
                myCommand.Parameters.AddWithValue("@description", tb_description.Text);
                myCommand.Parameters.AddWithValue("@cashType", ddl_type.SelectedValue);
                myCommand.Parameters.AddWithValue("@conditionId", ddl_condition.SelectedValue);
                myCommand.Parameters.AddWithValue("@imprintValue", Convert.ToSingle(tb_imprintValue.Text));
                myCommand.Parameters.AddWithValue("@currentValue", Convert.ToSingle(tb_currentValue.Text));
                myCommand.Parameters.AddWithValue("@ct", FileUpload1.PostedFile.ContentType); // Buscar o content-type
                myCommand.Parameters.AddWithValue("@image", imgBinaryData);
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
                    lbl_infos.Text = "Numismatic Created!";
                }
                else
                {
                    lbl_infos.Text = "Something went wrong!"; ;
                }
            }
        }

        protected void btn_gotoBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("manage_numismatics.aspx");
        }
    }
}