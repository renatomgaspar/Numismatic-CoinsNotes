using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
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
    public partial class collection_list : System.Web.UI.Page
    {
        public class Favourite
        {
            public int Id { get; set; }

            public int CashId { get; set; }


            public string Title { get; set; }

            public string Description { get; set; }

            public string Type { get; set; }

            public string Condition { get; set; }

            public float Imprintvalue { get; set; }

            public float Currentvalue { get; set; }

            public bool Active { get; set; }

            public string Image { get; set; }
        }

        List<Favourite> your_collection_list = new List<Favourite>();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["userEmail"] == null)
            {
                Response.Redirect("login.aspx");
            }
            else
            {
                ShowFavourites();

                if (!IsPostBack)
                {
                    Repeater1.DataSource = your_collection_list;
                    Repeater1.DataBind();
                }
            }
        }

        protected void Repeater1_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "DeleteItem")
            {
                int id = Convert.ToInt32(e.CommandArgument.ToString());

                // Criar a conexão - Abrir a connectionString
                SqlConnection myCon = new SqlConnection(ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString);

                SqlCommand myCommand = new SqlCommand();

                myCommand.Connection = myCon;
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.CommandText = "delete_your_favourite";

                // O que queremos inserir/enviar
                myCommand.Parameters.AddWithValue("@id", id);
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

                if (response == 0)
                {
                    string message = "Someting went wrong!";
                    string script = $"<script type='text/javascript'>alert('{message}');</script>";

                    ClientScript.RegisterStartupScript(this.GetType(), "alert", script);
                }

                ShowFavourites();
                ApplyFilters();
            }
        }

        protected void ddl_price_SelectedIndexChanged(object sender, EventArgs e)
        {
            ApplyFilters();
        }

        protected void ddl_cashtype_SelectedIndexChanged(object sender, EventArgs e)
        {
            ApplyFilters();
        }

        protected void tb_search_TextChanged(object sender, EventArgs e)
        {
            ApplyFilters();
        }

        private void ShowFavourites()
        {
            your_collection_list.Clear();

            SqlConnection myCon = new SqlConnection(ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString);

            SqlCommand myCommand = new SqlCommand();
            myCommand.Connection = myCon;
            myCommand.CommandType = CommandType.StoredProcedure;
            myCommand.CommandText = "get_your_collection";

            // O que queremos inserir/enviar
            myCommand.Parameters.AddWithValue("@email", Session["userEmail"]);

            // Abrir a conexão
            myCon.Open();

            SqlDataReader dr = myCommand.ExecuteReader();

            while (dr.Read())
            {
                Favourite c = new Favourite();
                c.Id = (int)dr["favouriteId"];
                c.CashId = (int)dr["id"];
                c.Title = dr["title"].ToString();
                c.Description = dr["description"].ToString();
                c.Type = dr["type"].ToString();
                c.Condition = dr["condition"].ToString();
                c.Imprintvalue = Convert.ToSingle(dr["imprintValue"]);
                c.Currentvalue = Convert.ToSingle(dr["currentValue"]);
                c.Active = (bool)dr["active"];

                your_collection_list.Add(c);
            }

            myCon.Close();
        }

        protected void ApplyFilters()
        {
            var your_collection_list_filter = your_collection_list;

            if (!string.IsNullOrEmpty(ddl_cashtype.SelectedValue) && ddl_cashtype.SelectedValue != "All")
            {
                your_collection_list_filter = your_collection_list_filter.Where(c => c.Type == ddl_cashtype.SelectedValue).ToList();
            }

            if (ddl_price.SelectedValue == "ASC")
            {
                your_collection_list_filter = your_collection_list_filter.OrderBy(c => c.Currentvalue).ToList();
            }
            else if (ddl_price.SelectedValue == "DESC")
            {
                your_collection_list_filter = your_collection_list_filter.OrderByDescending(c => c.Currentvalue).ToList();
            }

            if (!string.IsNullOrEmpty(tb_search.Text.Trim()))
            {
                your_collection_list_filter = your_collection_list_filter.Where(c => c.Title.ToLower().Contains(tb_search.Text.Trim().ToLower())).ToList();
            }


            Repeater1.DataSource = your_collection_list_filter;
            Repeater1.DataBind();
        }

        protected void btn_clearfilters_Click(object sender, EventArgs e)
        {
            ddl_cashtype.SelectedIndex = 0;
            ddl_price.SelectedIndex = 0;
            tb_search.Text = "";

            Repeater1.DataSource = your_collection_list;
            Repeater1.DataBind();
        }

        protected void btn_export_collection_Click(object sender, EventArgs e)
        {
            string templatePath = Server.MapPath("~/Assets/PDFS/Numismatics_PDF_TemplateForm.pdf");
            string fileName = $"my_collection_{DateTime.Now:yyyyMMddHHmmss}.pdf";

            Response.Clear();
            Response.ContentType = "application/pdf";
            Response.AddHeader("Content-Disposition", $"attachment; filename={fileName}");

            Document doc = null;
            PdfCopy copy = null;

            try
            {
                doc = new Document();
                copy = new PdfCopy(doc, Response.OutputStream);
                doc.Open();

                foreach (var item in your_collection_list)
                {
                    MemoryStream ms = null;
                    PdfReader reader = null;
                    PdfStamper stamper = null;
                    PdfReader filledPage = null;

                    try
                    {
                        ms = new MemoryStream();
                        reader = new PdfReader(templatePath);
                        stamper = new PdfStamper(reader, ms);
                        AcroFields fields = stamper.AcroFields;

                        // Preencher campos
                        fields.SetField("tb_title", item.Title);
                        fields.SetField("tb_description", item.Description);
                        fields.SetField("tb_type", item.Type);
                        fields.SetField("tb_condition", item.Condition);
                        fields.SetField("tb_imprint", item.Imprintvalue.ToString("N2"));
                        fields.SetField("tb_current", item.Currentvalue.ToString("N2"));

                        // Inserir imagem
                        if (!string.IsNullOrEmpty(item.Image))
                        {
                            try
                            {
                                string base64 = item.Image.Split(',')[1];
                                byte[] bytes = Convert.FromBase64String(base64);
                                if (bytes.Length > 0)
                                {
                                    iTextSharp.text.Image img = iTextSharp.text.Image.GetInstance(bytes);
                                    var posList = fields.GetFieldPositions("ImageUpload_af_image");
                                    if (posList != null && posList.Count > 0)
                                    {
                                        var pos = posList[0];
                                        img.ScaleToFit(pos.position.Width, pos.position.Height);
                                        img.SetAbsolutePosition(pos.position.Left, pos.position.Bottom);
                                        stamper.GetOverContent(pos.page).AddImage(img);
                                    }
                                }
                            }
                            catch
                            {
                                // Log opcional de erro na imagem
                            }
                        }

                        stamper.FormFlattening = true;
                        stamper.Close();

                        filledPage = new PdfReader(ms.ToArray());
                        copy.AddPage(copy.GetImportedPage(filledPage, 1));
                    }
                    finally
                    {
                        // Fechar recursos em ordem inversa de criação
                        filledPage.Close();
                        stamper.Close();
                        reader.Close();
                        ms.Close();
                    }
                }
            }
            finally
            {
                // Fechar recursos principais
                doc.Close();
                copy.Close();
            }

            Response.End();
        }
    }
}