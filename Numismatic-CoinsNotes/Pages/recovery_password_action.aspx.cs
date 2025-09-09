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
    public partial class chage_password_action : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Request.QueryString["email"]))
            {
                Response.Redirect("password_recovery.aspx");
            }
            else
            {
                // Criar a nova password random
                const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
                Random random = new Random();
                int length = 10;
                string result = "";

                for (int i = 0; i < length; i++)
                {
                    result += chars[random.Next(chars.Length)];
                }

                result = EncryptString(result);

                string email_url = Request.QueryString["email"];
                string email_decrypt = DecryptString(email_url);

                // Criar a conexão - Abrir a connectionString
                SqlConnection myCon = new SqlConnection(ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString);

                // Qual a ação - Neste caso insert
                SqlCommand myCommand = new SqlCommand();

                myCommand.Connection = myCon;
                myCommand.CommandType = CommandType.StoredProcedure; // Utilizar uma stored procedure
                myCommand.CommandText = "recovery_password"; // Noma da stored procedure

                // O que queremos inserir/enviar
                // myCommand.Parameters.AddWithValue("@campoTabela", valorAPassar);
                myCommand.Parameters.AddWithValue("@email", email_decrypt);
                myCommand.Parameters.AddWithValue("@new_password", result);
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
                        email.To.Add(email_decrypt);

                        email.Subject = "Your New Password";
                        // 2 Maneira de Enviar em bloco de notas ou HTML
                        // Mete o body em html
                        email.IsBodyHtml = true;
                        email.Body = $"<b><h1>Use this password in your next login!</h1></b><br>Password: {DecryptString(result)}";

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

                    lbl_response.Text = "Your Password was changed!";
                }
                else
                {
                    lbl_response.Text = "Something went wrong! Try again later!";
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

        public static string DecryptString(string Message)
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

            // Step 3. Setup the decoder
            TDESAlgorithm.Key = TDESKey;
            TDESAlgorithm.Mode = CipherMode.ECB;
            TDESAlgorithm.Padding = PaddingMode.PKCS7;

            // Step 4. Convert the input string to a byte[]

            Message = Message.Replace("KOKOKO", "+");
            Message = Message.Replace("JOJOJO", "/");
            Message = Message.Replace("IOIOIO", "\\");


            byte[] DataToDecrypt = Convert.FromBase64String(Message);

            // Step 5. Attempt to decrypt the string
            try
            {
                ICryptoTransform Decryptor = TDESAlgorithm.CreateDecryptor();
                Results = Decryptor.TransformFinalBlock(DataToDecrypt, 0, DataToDecrypt.Length);
            }
            finally
            {
                // Clear the TripleDes and Hashprovider services of any sensitive information
                TDESAlgorithm.Clear();
                HashProvider.Clear();
            }

            // Step 6. Return the decrypted string in UTF8 format
            return UTF8.GetString(Results);
        }
    }
}