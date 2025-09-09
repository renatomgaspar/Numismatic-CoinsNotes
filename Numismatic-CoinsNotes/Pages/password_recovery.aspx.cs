using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Numismatic_CoinsNotes.Pages
{
    public partial class password_recovery : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btn_password_recovery_Click(object sender, EventArgs e)
        {
            if (tb_email.Text == "")
            {
                lbl_infos.Text = "Fill all the fields!";
            }
            else
            {
                SqlConnection myCon = new SqlConnection(ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString);

                // Qual a ação - Neste caso insert
                SqlCommand myCommand = new SqlCommand();

                myCommand.Connection = myCon;
                myCommand.CommandType = CommandType.StoredProcedure; // Utilizar uma stored procedure
                myCommand.CommandText = "get_user_infos"; // Noma da stored procedure

                // O que queremos inserir/enviar
                // myCommand.Parameters.AddWithValue("@campoTabela", valorAPassar);
                myCommand.Parameters.AddWithValue("@email", tb_email.Text); // Request.QueryString["num"] - apanhar o valor num passado por link

                // Abrir a conexão
                myCon.Open();
                SqlDataReader dr = myCommand.ExecuteReader(); // PARA LEITURA DE INFORMAÇÃO - Vai ser guardado lá

                if (dr.Read())
                {
                    MailMessage email = new MailMessage();
                    SmtpClient smtp = new SmtpClient();

                    try
                    {
                        // Base para o envio de email
                        email.From = new MailAddress("numismaticsteammanager@gmail.com");
                        email.To.Add(tb_email.Text);

                        email.Subject = "Password Recovery";
                        // 2 Maneira de Enviar em bloco de notas ou HTML
                        // Mete o body em html
                        email.IsBodyHtml = true;
                        email.Body = $"<b><h1>Password Recovery!</h1></b><br>Click <a href='https://localhost:44317/Pages/recovery_password_action.aspx?email={EncryptString(tb_email.Text)}'> > here < </a>to receive your new password!";

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

                    lbl_infos.Text = "Check your email to receive your new password";
                }
                else
                {
                    lbl_infos.Text = "There is no account with that email!";
                }  
            }
        }

        protected void btn_back_Click(object sender, EventArgs e)
        {
            Response.Redirect("login.aspx");
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
    }
}