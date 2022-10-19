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
using GecLibrary;

public partial class referralBanner : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            Page.Title = ConfigurationManager.AppSettings["pageTitleStart"].ToString().Trim() + " | Tastygo | Referral Banner";
            if (!IsPostBack)
            {
                if (Session["member"] != null || Session["restaurant"] != null || Session["sale"] != null || Session["user"] != null)
                {
                    this.divLoginArea.Visible = false;
                }
                else
                {
                    this.divLoginArea.Visible = true;
                }

                GetAndSetRefferalBanners();
            }
        }
        catch (Exception ex)
        { }
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
                dlRefBanners.DataSource = dtBanners;
                dlRefBanners.DataBind();
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
            double width = 0;
            double height = 0;
            string imageFile = "";

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
        DataTable dtUserMemeber = null;

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
        else if (Session["user"] != null)
        {
            dtUserMemeber = (DataTable)Session["user"];
        }
        

        string strUserName = "";

        if (dtUserMemeber != null)
        {
            if (dtUserMemeber.Rows.Count > 0)
                strUserName = dtUserMemeber.Rows[0]["referralId"].ToString().Trim();
        }
        ViewState["userName"] = strUserName;

        if (width > 252)
            width = 252;

        if (height > 212)
            height = 212;

        string htmlText = "<table cellpadding='2' cellspacing='2' width=\"100%\" class='referralBanner_table'>";
        htmlText += "<tr>";
        htmlText += "<td style='text-align:center;height:235px;'>" + string.Format(htmlCode, strUserName) + "</td><Br />";
        htmlText += "</tr>";
        htmlText += "<tr><td style='text-align:center;'><font style='color: #F99D1C;font-size: 20px; font-weight: bold;'>" + width + " X " + height + "</font></td></tr>";
        htmlText += "<tr>";
        htmlText += "<tr><td style='color: #636363; font-size: 12px; font-weight: bold;text-align:center;'>Copy the code below and paste into your website!</td></tr>";
        htmlText += "<td style='text-align:center;'><textarea onclick='this.select();' style='width:272px;height:63px;'>" + string.Format(htmlCode, strUserName) + "</textarea></td>";
        htmlText += "</tr>";
        htmlText += "</table>";
        return htmlText;
    }

    public string HtmlCodeTemplate(double width, double height, string imageFile)
    {
        string file = ConfigurationSettings.AppSettings["RefferalImagePath"].ToString() + imageFile;
        string website = Misc.GetWebSite();
        if (file.Substring(0, 1) != "/")
            file = "/" + file;

        if (width > 252)
            width = 252;

        if (height > 212)
            height = 212;

        string strRetURL = "";


        if (Session["member"] != null || Session["restaurant"] != null || Session["sale"] != null || Session["user"] != null)
        {
            int iUserId = GetUserIdFromSession();
            string strUserIdEncrypted = (Convert.ToInt64(iUserId.ToString().Trim()) + 111111).ToString();
            strRetURL = "<a href='" + website + "/r/" + strUserIdEncrypted + "'><img src='" + website + file + "' border='0' width='" + width + "' height='" + height + "' alt='" + "tazzling.com" + "' /></a>";
        }
        else
        {
            strRetURL = "<a href='" + website + "/Default.aspx'><img src='" + website + file + "' border='0' width='" + width + "' height='" + height + "' alt='" + "tazzling.com" + "' /></a>";
        }
        return strRetURL;
    }

    private int GetUserIdFromSession()
    {
        int iUserid = 0;
        DataTable dtUser = null;

        try
        {
            if (Session["member"] != null)
            {
                dtUser = (DataTable)Session["member"];
            }
            else if (Session["restaurant"] != null)
            {
                dtUser = (DataTable)Session["restaurant"];
            }
            else if (Session["sale"] != null)
            {
                dtUser = (DataTable)Session["sale"];
            }
            else if (Session["user"] != null)
            {
                dtUser = (DataTable)Session["user"];
            }

            iUserid = int.Parse(dtUser.Rows[0]["userID"].ToString());
        }
        catch (Exception ex)
        {
            string strException = "There is an error occur, please email us at support@tazzling.com or call 1855-295-1771.";
        }

        return iUserid;
    }

    #endregion
}