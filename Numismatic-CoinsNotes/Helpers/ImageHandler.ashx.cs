using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Numismatic_CoinsNotes
{
    /// <summary>
    /// Summary description for ImageHandler
    /// </summary>
    public class ImageHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            int id = int.Parse(context.Request.QueryString["id"]);
            int page = int.Parse(context.Request.QueryString["page"]);

            SqlConnection myCon = new SqlConnection(ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString);

            if (page == 2)
            {
                SqlCommand cmd = new SqlCommand("get_user_image", myCon);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", id);

                myCon.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    string contentType = dr["ctType"].ToString();
                    byte[] bytes = (byte[])dr["image"];

                    context.Response.ContentType = contentType;
                    context.Response.BinaryWrite(bytes);
                }
            }
            else
            {
                SqlCommand cmd = new SqlCommand("get_numismatic_image", myCon);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", id);

                myCon.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    if ((bool)dr["active"])
                    {
                        string contentType = dr["ctImage"].ToString();
                        byte[] bytes = (byte[])dr["image"];

                        context.Response.ContentType = contentType;
                        context.Response.BinaryWrite(bytes);
                    }
                    else
                    {
                        string defaultImagePath = context.Server.MapPath("~/Assets/images/eliminated.jpg");
                        byte[] bytes = System.IO.File.ReadAllBytes(defaultImagePath);

                        context.Response.ContentType = "image/png";
                        context.Response.BinaryWrite(bytes);
                    }

                }
            }

            myCon.Close();
        }

        public bool IsReusable => false;
    }
}