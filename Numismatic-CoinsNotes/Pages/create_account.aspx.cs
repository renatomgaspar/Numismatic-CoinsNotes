using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Numismatic_CoinsNotes.Pages
{
    public partial class create_account : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btn_createAccount_Click(object sender, EventArgs e)
        {
            if (tb_email.Text == "" || tb_name.Text == "" || tb_password.Text == "")
            {
                lbl_infos.Text = "Fill all the fields!";
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
                    string defaultImagePath = Server.MapPath("../Assets/images/userDefaultImage.png");

                    // Lê o conteúdo da imagem default
                    imgBinaryData = File.ReadAllBytes(defaultImagePath);
                }
                

                // Criar a conexão - Abrir a connectionString
                SqlConnection myCon = new SqlConnection(ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString);

                SqlCommand myCommand = new SqlCommand();

                myCommand.Connection = myCon;
                myCommand.CommandType = CommandType.StoredProcedure; 
                myCommand.CommandText = "create_account"; 

                // O que queremos inserir/enviar
                myCommand.Parameters.AddWithValue("@name", tb_name.Text);
                myCommand.Parameters.AddWithValue("@email", tb_email.Text);
                myCommand.Parameters.AddWithValue("@password", EncryptString(tb_password.Text));
                myCommand.Parameters.AddWithValue("@verified", 0);
                myCommand.Parameters.AddWithValue("@active", 0);
                myCommand.Parameters.AddWithValue("@type", 1);
                myCommand.Parameters.AddWithValue("@ct", "image/png"); 
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

                if (response == 1)
                {
                    MailMessage email = new MailMessage();
                    SmtpClient smtp = new SmtpClient();

                    try
                    {
                        // Base para o envio de email
                        email.From = new MailAddress("numismaticsteammanager@gmail.com");
                        email.To.Add(tb_email.Text);

                        email.Subject = "Account Activation";
                        // 2 Maneira de Enviar em bloco de notas ou HTML
                        // Mete o body em html
                        email.IsBodyHtml = true;
                        email.Body = $"<b><h1>One More Step!</h1></b><br>Click <a href='https://localhost:44317/Pages/activation.aspx?email={EncryptString(tb_email.Text)}'> > here < </a>to active your account!";

                        // Servidor SMTP do gmail
                        smtp.Host = "smtp.gmail.com";
                        smtp.Port = 587;
                        smtp.Credentials = new NetworkCredential("numismaticsteammanager@gmail.com", "tufh faqe qclj hwei");
                        smtp.EnableSsl = true;
                        smtp.Send(email);
                    }
                    catch (Exception ex)
                    {
                        Response.Write(ex.Message);
                    }

                    lbl_infos.Text = "Your account was created! Go to your email to verify your account!";
                }
                else
                {
                    lbl_infos.Text = "Your email was already in use! Try again!";
                }
            }
        }

        public static string EncryptString(string Message)
        {
            string Passphrase = "numismaticappkey";
            byte[] Results;
            System.Text.UTF8Encoding UTF8 = new System.Text.UTF8Encoding();

            // Step 1. We hash the passphrase using MD5
            // We use the MD5 hash generator as the result is a 128 bit byte array
            // which is a valid length for the TripleDES encoder we use below

            MD5CryptoServiceProvider HashProvider = new MD5CryptoServiceProvider();
            byte[] TDESKey = HashProvider.ComputeHash(UTF8.GetBytes(Passphrase));

            // Step 2. Create a new TripleDESCryptoServiceProvider object
            TripleDESCryptoServiceProvider TDESAlgorithm = new TripleDESCryptoServiceProvider();

            // Step 3. Setup the encoder
            TDESAlgorithm.Key = TDESKey;
            TDESAlgorithm.Mode = CipherMode.ECB;
            TDESAlgorithm.Padding = PaddingMode.PKCS7;

            // Step 4. Convert the input string to a byte[]
            byte[] DataToEncrypt = UTF8.GetBytes(Message);

            // Step 5. Attempt to encrypt the string
            try
            {
                ICryptoTransform Encryptor = TDESAlgorithm.CreateEncryptor();
                Results = Encryptor.TransformFinalBlock(DataToEncrypt, 0, DataToEncrypt.Length);
            }
            finally
            {
                // Clear the TripleDes and Hashprovider services of any sensitive information
                TDESAlgorithm.Clear();
                HashProvider.Clear();
            }

            // Step 6. Return the encrypted string as a base64 encoded string

            string enc = Convert.ToBase64String(Results);
            enc = enc.Replace("+", "KOKOKO");
            enc = enc.Replace("/", "JOJOJO");
            enc = enc.Replace("\\", "IOIOIO");
            return enc;
        }

        protected void btn_gotoLogin_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Pages/login.aspx");
        }
    }
}