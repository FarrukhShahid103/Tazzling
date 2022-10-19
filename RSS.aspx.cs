using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.SqlClient;
using System.Xml;
using System.Text;

public partial class RSS : System.Web.UI.Page
{
    BLLDeals objDeal = new BLLDeals();

    BLLDealOrders objOrders = new BLLDealOrders();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["cid"] == null)
        {
            Response.Redirect("Default.aspx");
        }
        else
        {
            string strCityID = Request.QueryString["cid"].ToString().Trim();

            Response.Clear();
            Response.ContentType = "text/xml";

            XmlTextWriter xtw = new XmlTextWriter(Response.OutputStream, Encoding.UTF8);
            xtw.WriteStartDocument();
            string processtext = "type=\"text/xsl\" href=\"rss.xsl\"";
            xtw.WriteProcessingInstruction("xml-stylesheet", processtext);
            xtw.WriteStartElement("rss");
            xtw.WriteAttributeString("version", "2.0");
            xtw.WriteStartElement("channel");
            xtw.WriteElementString("title", "tastygo");
            xtw.WriteElementString("link", "http://www.tazzling.com/");
            xtw.WriteElementString("description", "Tastygo picks the best deals in town. We actually buy our own deals too!");
            xtw.WriteElementString("copyright", "Copyright © 2011 Tazzling.Com. All Rights Reserved");
            xtw.WriteElementString("webMaster", "support@tazzling.com");

            objDeal.cityId = Convert.ToInt32(strCityID);

            DataTable dtDeal = objDeal.getCurrentDealByCityID();


            if (dtDeal != null && dtDeal.Rows.Count == 0)
            {
                objDeal.DealId = 20;
                dtDeal = objDeal.getCurrentDealByDealID();
            }

            if (dtDeal != null && dtDeal.Rows.Count > 0)
            {
                for (int i = 0; i < dtDeal.Rows.Count; i++)
                {
                    //Set the Deal Inage here

                    /*string[] strDealImages = dtDeal.Rows[0]["images"].ToString().Split(',');

                    for (int k = 0; k < strDealImages.Length; k++)
                    {
                        if (strDealImages[k].ToString().Trim().Length > 3)
                        {                            
                            xtw.WriteStartElement("image");
                            xtw.WriteElementString("title", "tazzling.com");
                            xtw.WriteElementString("url", System.Configuration.ConfigurationManager.AppSettings["YourSite"].ToString() + "/" + dtDeal.Rows[0]["cityName"].ToString().Trim());
                            xtw.WriteElementString("link", ConfigurationManager.AppSettings["YourSite"].ToString() + "/Images/dealfood/" + dtDeal.Rows[0]["restaurantId"].ToString().Trim() + "/" + strDealImages[k]);
                            xtw.WriteEndElement();
                            break;
                        }
                    }*/

                    xtw.WriteStartElement("item");
                    xtw.WriteElementString("title", dtDeal.Rows[i]["title"].ToString().Trim());
                    xtw.WriteElementString("description", dtDeal.Rows[i]["description"].ToString().Trim());
                    xtw.WriteElementString("link", System.Configuration.ConfigurationManager.AppSettings["YourSite"].ToString() + "/" + dtDeal.Rows[i]["cityName"].ToString().Trim().Replace(" ", ".") + "_" + dtDeal.Rows[i]["dealid"].ToString().Trim());
                    xtw.WriteElementString("dealprice", dtDeal.Rows[i]["sellingPrice"].ToString().Trim());
                    xtw.WriteElementString("dealvalue", dtDeal.Rows[i]["valuePrice"].ToString().Trim());
                    xtw.WriteElementString("dealstarttime", XmlConvert.ToString(Convert.ToDateTime(dtDeal.Rows[i]["dealStartTime"].ToString())));
                    xtw.WriteElementString("dealendtime", XmlConvert.ToString(Convert.ToDateTime(dtDeal.Rows[i]["dealEndTime"].ToString())));

                    //Business Address
                    xtw.WriteElementString("dealcompanyaddress", dtDeal.Rows[i]["restaurantAddress"].ToString().Trim());
                    xtw.WriteElementString("dealmerchantname", dtDeal.Rows[i]["restaurantBusinessName"].ToString().Trim());
                    xtw.WriteElementString("dealnoofpurchases", GetTotalOrderCountByDealId(dtDeal.Rows[i]["dealid"].ToString().Trim()).ToString());

                    string[] strDealImages = dtDeal.Rows[i]["images"].ToString().Split(',');

                    for (int k = 0; k < strDealImages.Length; k++)
                    {
                        if (strDealImages[k].ToString().Trim().Length > 3)
                        {
                            xtw.WriteElementString("image" + k + 1 + "Link", ConfigurationManager.AppSettings["YourSite"].ToString() + "/Images/dealfood/" + dtDeal.Rows[i]["restaurantId"].ToString().Trim() + "/" + strDealImages[k]);
                        }
                    }

                    xtw.WriteElementString("pubDate", XmlConvert.ToString(DateTime.Now));
                    xtw.WriteEndElement();
                }
            }

            xtw.WriteEndElement();
            xtw.WriteEndElement();
            xtw.WriteEndDocument();

            xtw.Flush();
            xtw.Close();
        }
    }

    private int GetTotalOrderCountByDealId(string DealId)
    {
        int intTotalOrders = 0;

        try
        {
            objOrders.dealId = Convert.ToInt64(DealId);
            
            DataTable dtOrders = objOrders.getTotalDealOrdersCountByDealId();

            if (dtOrders != null && dtOrders.Rows.Count > 0 && dtOrders.Rows[0]["totalQty"].ToString().Trim() != "")
            {
                intTotalOrders = Convert.ToInt32(dtOrders.Rows[0]["totalQty"].ToString());
            }
        }
        catch (Exception ex)
        {
            string strException = "There is an error occur, please email us at support@tazzling.com or call 1855-295-1771.";
        }
        return intTotalOrders;
    }
}