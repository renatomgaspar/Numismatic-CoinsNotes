using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Numismatic_CoinsNotes.Pages
{
    public partial class change_password : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["userEmail"] == null)
            {
                Response.Redirect("login.aspx");
            }
        }

        protected void btn_changePassword_Click(object sender, EventArgs e)
        {
            if (tb_currentPassword.Text == "" || tb_newPassword.Text == "" || tb_newPasswordRepeat.Text == "")
            {
                lbl_infos.Text = "Fill all the fields!";
            }
            else if (tb_newPassword.Text != tb_newPasswordRepeat.Text)
            {
                lbl_infos.Text = "Passwords doesnt match!";
            }
            else
            {
                // Criar a conexão - Abrir a connectionString
                SqlConnection myCon = new SqlConnection(ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString);

                // Qual a ação - Neste caso insert
                SqlCommand myCommand = new SqlCommand();

                myCommand.Connection = myCon;
                myCommand.CommandType = CommandType.StoredProcedure; // Utilizar uma stored procedure
                myCommand.CommandText = "change_password"; // Noma da stored procedure

                // O que queremos inserir/enviar
                myCommand.Parameters.AddWithValue("@email", Session["userEmail"]);
                myCommand.Parameters.AddWithValue("@current_password", EncryptString(tb_currentPassword.Text));
                myCommand.Parameters.AddWithValue("@new_password", EncryptString(tb_newPassword.Text));

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
                    lbl_infos.Text = "Your password has been successfully changed!";
                }
                else
                {
                    lbl_infos.Text = "Your current password is incorrect! Try again!";
                }
            }
        }

        protected void btn_back_Click(object sender, EventArgs e)
        {
            Response.Redirect("your_account.aspx");
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