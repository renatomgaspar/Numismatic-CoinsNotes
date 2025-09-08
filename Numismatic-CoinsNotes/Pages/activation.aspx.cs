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
    public partial class activation : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string email_url = Request.QueryString["email"];
            string email_decrypt = DecryptString(email_url);

            // Criar a conexão - Abrir a connectionString
            SqlConnection myCon = new SqlConnection(ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString);

            // Qual a ação - Neste caso insert
            SqlCommand myCommand = new SqlCommand();

            myCommand.Connection = myCon;
            myCommand.CommandType = CommandType.StoredProcedure; // Utilizar uma stored procedure
            myCommand.CommandText = "account_activation"; // Noma da stored procedure

            // O que queremos inserir/enviar
            // myCommand.Parameters.AddWithValue("@campoTabela", valorAPassar);
            myCommand.Parameters.AddWithValue("@email", email_decrypt);
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
                lbl_response.Text = "Your Account is Active";
            }
            else
            {
                lbl_response.Text = "Something went wrong! Try again later!";
            }
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