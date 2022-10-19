<%@ WebHandler Language="C#" Class="Search_CS" %>

using System;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;
using System.Text;

public class Search_CS : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
        string prefixText = "";
        if (context.Request.QueryString["q"] != null)
        {
            prefixText = context.Request.QueryString["q"];
        }
        using (SqlConnection conn = new SqlConnection())
        {
            conn.ConnectionString = ConfigurationManager
                    .ConnectionStrings["CRMConnectionString"].ConnectionString;
            using (SqlCommand cmd = new SqlCommand())
            {
                //cmd.CommandText = "select city,state,zip,country from zipcodes where city like '%" + prefixText + "%' or state like '%" + prefixText + "%' or zip like '%" + prefixText + "%'";
                cmd.CommandText = "select restaurantBusinessName from restaurant where restaurantBusinessName like '%" + prefixText + "%'";
                                              
                cmd.Connection = conn;
                StringBuilder sb = new StringBuilder(); 
                conn.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        sb.Append(sdr["restaurantBusinessName"])
                            .Append(Environment.NewLine);
                    }
                }
                conn.Close();
                context.Response.Write(sb.ToString()); 
            }
        }
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }
}