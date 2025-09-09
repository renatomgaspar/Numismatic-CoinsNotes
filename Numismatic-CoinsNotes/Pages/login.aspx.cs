using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Numismatic_CoinsNotes.Pages
{
    public partial class login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btn_login_Click(object sender, EventArgs e)
        {
            if (tb_email.Text == "" || tb_password.Text == "")
            {
                lbl_infos.Text = "Fill all the fields!";
            }
            else
            {
                // Criar a conexão - Abrir a connectionString
                SqlConnection myCon = new SqlConnection(ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString);

                SqlCommand myCommand = new SqlCommand();

                myCommand.Connection = myCon;
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.CommandText = "login";

                // O que queremos inserir/enviar
                myCommand.Parameters.AddWithValue("@email", tb_email.Text);
                myCommand.Parameters.AddWithValue("@password", EncryptString(tb_password.Text));
                // Parameter de retorno
                SqlParameter valor = new SqlParameter();
                valor.ParameterName = "@return";
                valor.Direction = ParameterDirection.Output;
                valor.SqlDbType = SqlDbType.Int;

                myCommand.Parameters.Add(valor);

                // Abrir a conexão
                myCon.Open();
                myCommand.ExecuteNonQuery(); // Executar a nossa stored procedure
                int response = Convert.ToInt32(myCommand.Parameters["@return"].Value);
                myCon.Close();

                if (response == 1)
                {
                    lbl_infos.Text = "Login Successful!";

                    Session["checkLogin"] = true;

                    ClientScript.RegisterStartupScript(
                        this.GetType(),
                        "Redirect",
                        "setTimeout(function() { window.location.href = '../Pages/home.aspx'; }, 3000);", 
                        true);
                }
                else if (response == 2)
                {
                    lbl_infos.Text = "Your account has been Desactivated! Contact Numismatic Support!";
                }
                else if (response == 3)
                {
                    lbl_infos.Text = "Your account is not verified! Check your email to verify it!";
                }
                else
                {
                    lbl_infos.Text = "Invalid Credentials! Try again!";
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

        protected void btn_gotoCreate_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Pages/create_account.aspx");
        }
    }
}