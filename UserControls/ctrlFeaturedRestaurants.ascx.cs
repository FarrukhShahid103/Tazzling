using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using AjaxControlToolkit;
public partial class Takeout_UserControls_ctrlFeaturedRestaurants : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            //Get & Set the Featured Restaurants here
            BindFavouriteRestaurants();
        }
    }

    private void BindFavouriteRestaurants()
    {
        try
        {
            BLLRestaurant objBLLRestaurant = new BLLRestaurant();

            DataTable dtFeaturedRes = new DataTable();

            dtFeaturedRes = objBLLRestaurant.getFeaturedRestaurant();

            if ((dtFeaturedRes != null) && (dtFeaturedRes.Rows.Count > 0))
            {
                this.rptrFeaturedRes.DataSource = dtFeaturedRes;
                this.rptrFeaturedRes.DataBind();
            }
        }
        catch (Exception ex)
        {
            string strException = "There is an error occur, please email us at support@tazzling.com or call 1855-295-1771.";
        }
    }
    
    
    public static string GetDynamicContent(string contextKey)
    {
        StringBuilder b = new StringBuilder();

        b.Append("<div id='balloon' style='width: 450px;'>");
        b.Append("<div style='background: url(../images/Ballon/BallonTop.png) 0 0 no-repeat; width: 450px;");
        b.Append("    height: 44px;'>");
        b.Append("    <div style='text-align: right; padding-right: 41px; padding-top: 29px;'>");
        b.Append("        <asp:ImageButton ID='imgBtnClose' ImageUrl='~/Images/no.gif' runat='server' ToolTip='Close' /></div>");
        b.Append("</div>");
        b.Append("<div style='background: url(../images/Ballon/BallonMiddle.png) 0 0 repeat-y; background-repeat: repeat-y;");
        b.Append("    width: 450px; height: 235px;'>");
        b.Append("    <div style='padding-left: 30px; padding-right: 30px; width: 390px; padding-top: 12px;height:163px;'>");
        b.Append("        <div style='width: 172px; float: left; padding-left: 14px;'>");
        b.Append("            <div style='background: url(../images/Ballon/BallonImageBG.gif) 0 0 no-repeat; height: 152px;");
        b.Append("                width: 172px;'>");
        b.Append("                <div style='width: 142px; padding-left: 15px; padding-right: 15px; padding-top: 15px;");
        b.Append("                    padding-bottom: 15px;'>");
        b.Append("                    <img src='MenuImages/FeaturedFood/18/images/f4bd52e3-5fa5-42b2-94a2-13b287eace62.jpg'");
        b.Append("                        alt='' style='width: 142px; height: 122px' /></div>");
        b.Append("            </div>");
        b.Append("        </div>");
        b.Append("        <div style='float: right; padding-left: 12px; width: 192px;'>");
        b.Append("            <div style='float: left; color: #D35B16; font-family: Arial; font-size: 16px; font-weight: bold;");
        b.Append("                width: 192px; padding-bottom: 3px;'>");
        b.Append("                <asp:Label ID='Labels3' runat='server'>Dhaba1</asp:Label></div>");
        b.Append("            <div style='float: left; width: 182px; border-top: solid 1px #CECDCC; height: 4px;");
        b.Append("                padding-bottom: 3px;'>");
        b.Append("            </div>");
        b.Append("            <div style='float: left; color: #8C8C8C; font-family: Arial; font-size: 12px; width: 178px;");
        b.Append("                padding-right: 14px;'>");
        b.Append("                <asp:Label ID='Label3s' runat='server'>2106 West 41 Ave, Vancouver,British Columbia, v6b-3l4 032-411-9899</asp:Label></div>");
        b.Append("        </div>");
        b.Append("    </div>");
        b.Append("    <div style='padding-left: 45px; padding-right: 30px; width: 375px; padding-bottom: 5px;'>");
        b.Append("        <div style='width: 375px; color: #757474; font-family: Arial; font-size: 15px; font-weight: bold;");
        b.Append("            padding-bottom: 2px;'>");
        b.Append("            Description:</div>");
        b.Append("        <div style='float: left; width: 363px; border-top: solid 1px #CECDCC; height: 3px;");
        b.Append("            padding-bottom: 2px;'>");
        b.Append("        </div>");
        b.Append("        <div style='width: 363px; padding-right: 12px; color: #8C8C8C; font-family: Arial;");
        b.Append("            font-size: 12px; font-weight: bold;'>");
        b.Append("            Description it sk d dkd  it sk d dkd  it sk d dkd  it sk d dkd  it sk d dkd  it sk d dkd  it sk d dkd  it sk d dkd </div>");
        b.Append("    </div>");
        b.Append("</div>");
        b.Append("<div style='background: url(../images/Ballon/BallonBottom.png) 0 0 no-repeat; width: 450px;");
        b.Append("    height: 38px;'>");
        b.Append("</div>");
        b.Append("</div>");

        return b.ToString();
    }
}