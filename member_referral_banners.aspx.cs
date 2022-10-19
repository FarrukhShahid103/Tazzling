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
using System.Text;
using System.Net.Mail;

public partial class member_referral_banners : System.Web.UI.Page
{ 
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                GetAndSetRefferalBanners();                
            }
        }
        catch (Exception ex)
        {
        
        }
    }

    #region "Get and Set the List of Refferal Banner"

    private void GetAndSetRefferalBanners()
    {
        try
        {
            BLLReferralBanner objBLLReferralBanner = new BLLReferralBanner();

            DataTable dtBanners = objBLLReferralBanner.getAllReferralBanner();

            if ((dtBanners != null) && (dtBanners.Rows.Count > 0))
            {
                //Reintialize the DataTable
                dtBanners = BuildBannerDataTable(dtBanners);

                //Set the Data Source here
                gridView_Banners.DataSource = dtBanners;
                gridView_Banners.DataBind();
            }
        }
        catch (Exception ex)
        {
            string strException = "There is an error occur, please email us at support@tazzling.com or call 1855-295-1771.";
        }
    }

    private DataTable BuildBannerDataTable(DataTable dtBannersData)
    {
        DataTable dtBanner = new DataTable();

        try
        {
            DataColumn colBannerID = new DataColumn("bannerId");
            DataColumn colBannerTitle = new DataColumn("bannerTitle");
            DataColumn colBannerWidth = new DataColumn("bannerWidth");
            DataColumn colBannerHeight = new DataColumn("bannerHeight");
            DataColumn colImageFile = new DataColumn("imageFile");
            DataColumn colCreatedBy = new DataColumn("createdBy");
            DataColumn colHtmlCodeTemplate = new DataColumn("HtmlCodeTemplate");

            dtBanner.Columns.Add(colBannerID);
            dtBanner.Columns.Add(colBannerTitle);
            dtBanner.Columns.Add(colBannerWidth);
            dtBanner.Columns.Add(colBannerHeight);
            dtBanner.Columns.Add(colImageFile);
            dtBanner.Columns.Add(colCreatedBy);
            dtBanner.Columns.Add(colHtmlCodeTemplate);

            //Initialize the DataRow here
            DataRow dRow;

            //Initialize the Bannder's Width & Height variables here
            double width=0;
            double height=0;
            string imageFile="";

            for (int i = 0; i < dtBannersData.Rows.Count; i++)
            {
                dRow = dtBanner.NewRow();
                dRow["bannerId"] = dtBannersData.Rows[i]["bannerId"];
                dRow["bannerTitle"] = dtBannersData.Rows[i]["bannerTitle"];

                //Set the Banner's Width here
                width = dtBannersData.Rows[i]["bannerWidth"] == null ? 0 : double.Parse(dtBannersData.Rows[i]["bannerWidth"].ToString().Trim());
                dRow["bannerWidth"] = dtBannersData.Rows[i]["bannerWidth"];
                
                //Set the Banner's Height here
                height = dtBannersData.Rows[i]["bannerHeight"] == null ? 0 : double.Parse(dtBannersData.Rows[i]["bannerHeight"].ToString().Trim());
                dRow["bannerHeight"] = dtBannersData.Rows[i]["bannerHeight"];

                //Set the Banner's Height here
                imageFile = dtBannersData.Rows[i]["imageFile"] == null ? "" : dtBannersData.Rows[i]["imageFile"].ToString().Trim();
                dRow["imageFile"] = dtBannersData.Rows[i]["imageFile"];

                dRow["createdBy"] = dtBannersData.Rows[i]["createdBy"];
                dRow["HtmlCodeTemplate"] = HtmlCodeTemplate(width, height, imageFile);
                dtBanner.Rows.Add(dRow);
            }
        }
        catch (Exception ex)
        {
            string strException = "There is an error occur, please email us at support@tazzling.com or call 1855-295-1771.";
        }

        return dtBanner;
    }

    protected string GetBannerHtmlCode(double width, double height, string imageFile, string htmlCode)
    {
        DataTable dtUserMemeber =  null;
        
        if (Session["member"] != null)
        {
            dtUserMemeber = (DataTable)Session["member"];
        }
        else if (Session["restaurant"] != null)
        {
            dtUserMemeber = (DataTable)Session["restaurant"];
        }
        else if (Session["sale"] != null)
        {
            dtUserMemeber = (DataTable)Session["sale"];
        }

        string strUserName = "";

        if (dtUserMemeber != null)
        {
            if (dtUserMemeber.Rows.Count > 0)
                strUserName = dtUserMemeber.Rows[0]["referralId"].ToString().Trim();
        }
        ViewState["userName"] = strUserName;

        string htmlText = "<table cellpadding=\"0\" cellspacing=\"0\" width=\"100%\"><tr>";
        htmlText += "<td width='110px' valign='top'>" + width + " * " + height + " px" + " " + "Image" + ":</td>";        
        htmlText += "<td>" + string.Format(htmlCode, strUserName) + "</td></tr>";
        htmlText += "<tr><td valign=\"middle\">" + "HTML Code" + ":</td>";
        htmlText += "<td><textarea onclick='this.select();'>" + string.Format(htmlCode, strUserName) + "</textarea></td></tr></table>";
        return htmlText;
    }

    public string HtmlCodeTemplate(double width, double height, string imageFile)
    {
        string file = ConfigurationSettings.AppSettings["RefferalImagePath"].ToString() + imageFile;
        string website = Misc.GetWebSite();
        if (file.Substring(0, 1) != "/")
            file = "/" + file;

        return "<a href='" + website + "/TastyGo/takeout/register_member.aspx?refid={0}'><img src='" + website + file + "' border='0' width='" + width + "' height='" + height + "' alt='" + "tazzling.com" + "' /></a>";
    }

    #endregion
}