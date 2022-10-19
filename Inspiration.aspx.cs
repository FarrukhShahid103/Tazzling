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
using GecLibrary;
using System.Diagnostics;
using System.IO;


public partial class Inspiration : System.Web.UI.Page
{
    public string dealId = "";
    public string imagePath = "";
   // public string totalcomments="";
    public string title = "";
    public string Discription = "";
    public string valuePrice = "";
    public string sellingPrice = "";
    public string Favourite = "";
    public string Purchase = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            BLLLiveFeed dealdeatail = new BLLLiveFeed();

            if (Request.QueryString["sidedeal"] != null && Request.QueryString["sidedeal"].ToString().Trim() != "")
            {

                dealId = Request.QueryString["sidedeal"].ToString().Trim();
                dealdeatail.dealId = Convert.ToInt16(Request.QueryString["sidedeal"].ToString().Trim());
                DataTable dtdealdeatail = dealdeatail.getDealDetailByDealDealID();
                if (dtdealdeatail != null && dtdealdeatail.Rows.Count > 0)
                {
                    string[] images = dtdealdeatail.Rows[0]["images"].ToString().Split(',');
                    string restaurantId = dtdealdeatail.Rows[0]["restaurantId"].ToString();
                    string image = images[0];
                    string completePath = "images/dealfood/" + restaurantId + "/" + image;
                    imagePath = completePath;

                    title = dtdealdeatail.Rows[0]["topTitle"].ToString().Trim();
                    if (dtdealdeatail.Rows[0]["description"].ToString().Trim().Length > 1000)
                    {
                        Discription = dtdealdeatail.Rows[0]["description"].ToString().Trim().Substring(0, 995) + "...";
                    }


                    valuePrice = dtdealdeatail.Rows[0]["valuePrice"].ToString().Trim();
                    sellingPrice = dtdealdeatail.Rows[0]["sellingPrice"].ToString().Trim();
                    Favourite = dtdealdeatail.Rows[0]["TotalFavorites"].ToString().Trim();
                    Purchase = dtdealdeatail.Rows[0]["TotalPurchases"].ToString().Trim();
                }

            }



            if (Request.QueryString["DealID"] != null && Request.QueryString["DealID"].ToString().Trim() != "")
            {
                BLLLiveFeed updatefvrt = new BLLLiveFeed();
                updatefvrt.dealId = Convert.ToInt16(Request.QueryString["DealID"].ToString().Trim());


                int Result = updatefvrt.UpdateTotalFav();
                if (Result != 0)
                {

                    Response.Write("True");
                    Response.End();
                }
                else
                {
                    Response.Write("False");
                    Response.End();
                }
            }
        }
    }
    
}