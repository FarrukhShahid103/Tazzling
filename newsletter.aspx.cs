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
using System.IO;
using System.Text.RegularExpressions;

public partial class newsletter : System.Web.UI.Page
{


    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["cid"] != null && Request.QueryString["cid"].ToString().Trim() != ""
                && Request.QueryString["date"] != null && Request.QueryString["date"].ToString().Trim() != ""
                && Request.QueryString["slot"] != null && Request.QueryString["slot"].ToString().Trim() != "")
            {
                FillDealsDroDownList(Request.QueryString["cid"].ToString().Trim());
            }
        }
    }

    protected void FillDealsDroDownList(string strCityID)
    {
        try
        {
            BLLCities objCities = new BLLCities();
            BLLDeals objDeal = new BLLDeals();
            objCities.cityId = Convert.ToInt32(strCityID);
            DataTable dtCity = objCities.getCityByCityId();
            string strCityName = "";
            if (dtCity.Rows.Count > 0)
            {
               // objDeal.CreatedDate = Misc.getResturantLocalTime(Convert.ToInt32(dtCity.Rows[0]["provinceId"].ToString()));
                strCityName = dtCity.Rows[0]["cityName"].ToString().Trim();
                ViewState["CreatedDate"] = objDeal.CreatedDate.ToString();
            }
            objDeal.CreatedDate = DateTime.Parse(Request.QueryString["date"].ToString().Trim()); 
            objDeal.cityId = Convert.ToInt32(strCityID);
            DataTable dtDeal = objDeal.getCurrentDealByCityID();
            if (dtDeal.Rows.Count > 0)
            {                
                ltDealDetail.Text = "";
                btnBindDeals(dtDeal, strCityName);
            }
            else
            {
                ltDealDetail.Text = "There is no active deal for city";                                                
            }
        }
        catch (Exception ex)
        {

        }
    }

    static string StripHTML(string inputString)
    {
        return Regex.Replace
          (inputString, @"</?\w+((\s+\w+(\s*=\s*(?:"".*?""|'.*?'|[^'"">\s]+))?)+\s*|\s*)/?>", string.Empty);
    }

    protected void btnBindDeals(DataTable dtDeals, string strCityName)
    {
        try
        {
            string strCityName_New = "";
            if (strCityName.Trim() == "York Region")
            {
                strCityName_New = "York_Region";
            }
            else if (strCityName.Trim() == "Oakville - Burlington")
            {
                strCityName_New = "Oakville_Burlington";
            }
            else
            {
                strCityName_New = strCityName.Trim().Replace(" ","");
            }

            ltDealDetail.Text += "<!DOCTYPE html PUBLIC \" -//W3C//DTD XHTML 1.0 Transitional//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\">";
            ltDealDetail.Text += "<html><head><title>News Letter</title><style type=\"text/css\">body{padding: 0px;padding: 0px;color: Black;line-height: normal;background-color: #f3f3f3;font: 12px/150% Helvetica;}.Contant{padding: 0 auto;width: 680px;}.TopLink{font-size: 11px;color: #3fafef;cursor: pointer;text-decoration: none;}.ExternalClass{width:100% !important;}        .TopLink:hover{font-size: 11px;color: #3fafef;cursor: pointer;text-decoration: underline;}img{border: none;}.style1{width: 72px;}.style2{}.style3{height: 5px;}.style4{width: 100%;}.style6{height: 120px;}</style></head>";
            ltDealDetail.Text += "<body><center><table style=\"width: 680px; padding-top: 10px; padding-bottom: 10px;\"><tbody><tr><td style=\"text-align: center;\">";
            
            ltDealDetail.Text += "Unsubscribe";
            ltDealDetail.Text += "<span style=\"color: Black;cursor: default;\">|</span>";
            //ltDealDetail.Text += "<a class=\"TopLink\" target=\"_blanck\" href=\"" + ConfigurationManager.AppSettings["YourSite"].Trim() + "\">Go to Tazzling.com </a><span style=\"color: Black; cursor: default;\">|</span>";
            
            ltDealDetail.Text += "<a class=\"TopLink\" target=\"#\">View this email in browser </a>";
            ltDealDetail.Text += "</td></tr><tr><td style=\"text-align: center;\">add <a class=\"TopLink\" href=\"mailto:news@tazzling.com\">news@tazzling.com</a> to your address book or safe sender list so our emails get to your inbox. <a class=\"TopLink\" target=\"_blanck\" href=\"" + ConfigurationManager.AppSettings["YourSite"].Trim() + "/howItWorks.aspx\">Learn how</a></td></tr>";
            ltDealDetail.Text += "<tr><td style='width:100%;'><table style=\"clear: both; height: 95px; width: 100%; background-color: #12a4e0;border-bottom: 5px solid #ff72c7;\"><tr class=\"Contant\"><td style=\"padding-top: 25px; float: left; padding-left:15px;\"><a target=\"_blanck\" href=\"" + ConfigurationManager.AppSettings["YourSite"].Trim() + "/" + strCityName_New + "\"><img style='height:56px; width:209px;' height='56' width='209' src=\"" + ConfigurationManager.AppSettings["YourSite"].Trim() + "/images/NewLogo_2.png\" alt=\"Tastygo Logo\" /></a></td>";
            ltDealDetail.Text += "<td style=\"padding-top: 25px; text-align: center; float: left;padding-left:25px;\"><span style=\"padding-left: 15px; padding-top: 25px; text-align: center; width: 250px;\"><span style=\"clear: both; font-size: 13px; color: White; font-weight: bold;\">Your DailyDeal For</span><br /><span style=\"clear: both; font-size: 25px; color: White; padding-top: 10px; padding-left: 5px;\">";

            ltDealDetail.Text += strCityName + "</span> </span></td>";

            ltDealDetail.Text += "<td style=\"padding-top: 25px; text-align: right;\"><table><tr><td><span style=\"padding-top: 25px; width: 250px;\"><span style=\"clear: both; font-size: 13px;color: White; padding-top: 25px; font-weight: bold;\">";
            ltDealDetail.Text += Convert.ToDateTime(Request.QueryString["date"].ToString().Trim()).ToString("dddd MMMM dd, yyyy") + "</span><br /></span></td></tr>";

            ltDealDetail.Text += "<tr><td><a target=\"_blanck\" href=\"http://www.twitter.com/tastygo\"><img style='padding-right:5px;height:32px; width:32px;' height='32' width='32' src=\"" + ConfigurationManager.AppSettings["YourSite"].Trim() + "/images/twitter1.png\" alt=\"Tweet With Us on Twitter\" /></a><a target=\"_blanck\" href=\"https://www.facebook.com/tastygo\"><img style='height:32px; width:32px;' height='32' width='32' src=\"" + ConfigurationManager.AppSettings["YourSite"].Trim() + "/images/facebook2.png\" alt=\"Follow us on Facebook\" /></a></td></tr></table> </td></tr></table></td></tr>";
            ltDealDetail.Text += "<tr><td style=\"height: 40px; padding-top: 5px; background-color: #5f5f5f;\"><table style=\"width: 100%\"><tr><td style=\"text-align: left;\" align=\"left\"><table align=\"left\"><tr><td style=\"font-family: Helvetica; font-size: 18.6px; font-weight: bold; padding: 11px 0 0 10px;color: White; width: 470px;\">";

            string shortTitle = dtDeals.Rows[Convert.ToInt32(Request.QueryString["slot"].ToString().Trim())]["shortTitle"].ToString();
            string dealPagetitel = (dtDeals.Rows[Convert.ToInt32(Request.QueryString["slot"].ToString().Trim())]["dealPageTitle"].ToString());
            string titel = dtDeals.Rows[Convert.ToInt32(Request.QueryString["slot"].ToString().Trim())]["title"].ToString();
            string toptitel = dtDeals.Rows[Convert.ToInt32(Request.QueryString["slot"].ToString().Trim())]["topTitle"].ToString();


            ltDealDetail.Text += (shortTitle.Trim() != "" ? shortTitle.Trim().Length > 50 ? shortTitle.Substring(0, 47) + "..." : shortTitle : dealPagetitel.Trim() == "" ? titel.Trim().Length > 50 ? titel.Substring(0, 47) + "..." : titel : dealPagetitel.Trim().Length > 50 ? dealPagetitel.Substring(0, 47) + "..." : dealPagetitel);

            ltDealDetail.Text += "</td></tr><tr><td style=\"color: White; font-family: Helvetica; font-size: 12.44px; padding-left: 10px;padding-top: 5px; text-align: left;\">";


            ltDealDetail.Text += toptitel.Trim().Length > 85 ? toptitel.Substring(0, 82) + "..." : toptitel;

            ltDealDetail.Text += "</td></tr></table></td>";

            ltDealDetail.Text += "<td style=\"float: left; text-align: right;\"><table align=\"right\"><tr><td align=\"right\" style=\"width: 180px;\"><table><tr><td style=\"clear: both; font-family: Helvetica; font-size: 12.44px; font-weight: bold;padding-bottom: -5px; color: White\" class=\"style1\" align=\"center\"></td><td style=\"clear: both; font-family: Helvetica; font-size: 12.44px; font-weight: bold;padding-bottom: -5px;\" rowspan=\"2\">";

            ltDealDetail.Text += "<a target=\"_blank\" href=\"" + ConfigurationManager.AppSettings["YourSite"].Trim() + "/" + strCityName_New + "_" + dtDeals.Rows[Convert.ToInt32(Request.QueryString["slot"].ToString().Trim())]["dealId"].ToString().Trim() + "\"><img style='height:41px; width:59px;' height='41' width='59' src=\"" + ConfigurationManager.AppSettings["YourSite"].Trim() + "/images/Viewbtn.png\" alt=\"#\" /></a></td></tr><tr><td style=\"clear: both; color: White; font-family: Helvetica;text-align:center; font-size: 31.1px;font-weight: bold; line-height: normal; width:100px;\" width='100' class=\"style1\">";

            ltDealDetail.Text += "<sup><span style='font-size:11px; font-wight:normal; vertical-align:top'>Only</span> $</sup>" + dtDeals.Rows[Convert.ToInt32(Request.QueryString["slot"].ToString().Trim())]["sellingPrice"].ToString().Trim() + "</td></tr></table></td></tr></table></td></tr></table></td></tr> <tr class=\"Contant\" style=\"height: 20px; padding-top: 5px; font-family: Helvetica;font-size: 12.44px; font-weight: bold;\"><td style=\"width: 45%; float: left; padding-left: 110px; color: Black;\">";

            double dSellPrice = Convert.ToDouble(dtDeals.Rows[Convert.ToInt32(Request.QueryString["slot"].ToString().Trim())]["sellingPrice"].ToString().Trim());
            double dActualPrice = Convert.ToDouble(dtDeals.Rows[Convert.ToInt32(Request.QueryString["slot"].ToString().Trim())]["valuePrice"].ToString().Trim());
            double dDiscount = 0;
            if (dSellPrice == 0)
            {
                dDiscount = 100;
            }
            else
            {
                dDiscount = (100 / dActualPrice) * (dActualPrice - dSellPrice);
            }


            ltDealDetail.Text += "Reg: <del>$" + dtDeals.Rows[Convert.ToInt32(Request.QueryString["slot"].ToString().Trim())]["valuePrice"].ToString().Trim() + "</del>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;You Save :" + Convert.ToInt32(dDiscount).ToString() + "%";

            ltDealDetail.Text += "</td></tr><tr><td style=\"padding-top: 10px; text-align: center;\">";
            ltDealDetail.Text += " <a class=\"TopLink\" target=\"_blanck\" href=\"" + ConfigurationManager.AppSettings["YourSite"].Trim() + "/" + strCityName_New + "_" + dtDeals.Rows[Convert.ToInt32(Request.QueryString["slot"].ToString().Trim())]["dealId"].ToString().Trim() + "\">";
            ltDealDetail.Text += "<img  src=\"" + ConfigurationManager.AppSettings["YourSite"].Trim() + "/images/dealfood/" + dtDeals.Rows[Convert.ToInt32(Request.QueryString["slot"].ToString().Trim())]["restaurantId"].ToString().Trim() + "/" + dtDeals.Rows[Convert.ToInt32(Request.QueryString["slot"].ToString().Trim())]["image1"].ToString().Trim() + "\" style='width:464px; height:333px;' height='333' width='464' /></a></td></tr><tr><td style=\"padding-top: 8px; text-align: left; color: Black;font-weight: bold; width:680px;height:40px;\" height='40' width='680'>";

            ltDealDetail.Text += StripHTML(dtDeals.Rows[Convert.ToInt32(Request.QueryString["slot"].ToString().Trim())]["description"].ToString().Trim()).Trim().Length > 200 ? StripHTML(dtDeals.Rows[Convert.ToInt32(Request.QueryString["slot"].ToString().Trim())]["description"].ToString().Trim()).Substring(0, 199) + "..." : StripHTML(dtDeals.Rows[Convert.ToInt32(Request.QueryString["slot"].ToString().Trim())]["description"].ToString().Trim());
            ltDealDetail.Text += " <a class=\"TopLink\" target=\"_blanck\" href=\"" + ConfigurationManager.AppSettings["YourSite"].Trim() + "/" + strCityName_New + "_" + dtDeals.Rows[Convert.ToInt32(Request.QueryString["slot"].ToString().Trim())]["dealId"].ToString().Trim() + "\">Read More</a>";

            ltDealDetail.Text += " </td></tr><tr><td style=\" background-color: #ff72c7;\" class=\"style3\"></td></tr>";


            dtDeals.Rows.RemoveAt(Convert.ToInt32(Request.QueryString["slot"].ToString().Trim()));
            dtDeals.AcceptChanges();
            int dealsCount = dtDeals.Rows.Count;
            int loopCount = 0;
            if (dtDeals.Rows.Count > 0)
            {
                while (dealsCount > 0)
                {
                    if (loopCount >= 6)
                    {
                        break;
                    }

                    if (loopCount == 0)
                    {
                        ltDealDetail.Text += "<tr><td style=\"color: #019CFF; font-family: Helvetica; font-size: 16.77px; font-weight: bold;text-align: left;\" class=\"style2\"><table cellspacing=\"0\" class=\"style4\"><tr><td>more deals<font style=\"color: #019CFF; font-family: Helvetica; font-size: 16.77px;\"> for you</font></td><td align=\"right\"></td></tr></table></td></tr>";
                    }

                    string shortTitle1 = dtDeals.Rows[loopCount]["shortTitle"].ToString();
                    string dealPagetitel1 = (dtDeals.Rows[loopCount]["dealPageTitle"].ToString());
                    string titel1 = dtDeals.Rows[loopCount]["title"].ToString();
                    string toptitel1 = dtDeals.Rows[loopCount]["topTitle"].ToString();
                    ltDealDetail.Text += " <tr><td style=\"height: 40px; padding-top: 5px; background-color: #5f5f5f;\"><table style=\"width: 100%\"><tr><td style=\"text-align: left;\" align=\"left\"><table align=\"left\"><tr><td style=\"font-family: Helvetica; font-size: 18.6px; font-weight: bold; padding: 11px 0 0 10px;color: White; width:470px;\">";
                    ltDealDetail.Text += (shortTitle1.Trim() != "" ? shortTitle1.Trim().Length > 50 ? shortTitle1.Substring(0, 47) + "..." : shortTitle1 : dealPagetitel1.Trim() == "" ? titel1.Trim().Length > 50 ? titel1.Substring(0, 47) + "..." : titel1 : dealPagetitel1.Trim().Length > 50 ? dealPagetitel1.Substring(0, 47) + "..." : dealPagetitel1);
                    ltDealDetail.Text += "</td></tr><tr><td style=\"color: White; font-family: Helvetica; font-size: 12.44px; padding-left: 10px;padding-top: 5px; text-align: left;\">";
                    ltDealDetail.Text += toptitel1.Trim().Length > 85 ? toptitel1.Substring(0, 82) + "..." : toptitel1;
                    ltDealDetail.Text += "</td></tr></table></td>";
                    ltDealDetail.Text += "<td style=\"float: left; text-align: right;\"><table align=\"right\"><tr><td align=\"right\" style=\"width: 180px;\"><table><tr><td style=\"clear: both; font-family: Helvetica; font-size: 12.44px; font-weight: bold;padding-bottom: -5px; color: White\" class=\"style1\" align=\"center\"></td><td style=\"clear: both; font-family: Helvetica; font-size: 12.44px; font-weight: bold;padding-bottom: -5px;\" rowspan=\"2\">";
                    ltDealDetail.Text += "<a target=\"_blanck\" href=\"" + ConfigurationManager.AppSettings["YourSite"].Trim() + "/" + strCityName_New + "_" + dtDeals.Rows[loopCount]["dealId"].ToString().Trim() + "\"><img style='height:41px; width:59px;' height='41' width='59' src=\"" + ConfigurationManager.AppSettings["YourSite"].Trim() + "/images/Viewbtn.png\"/></a></td></tr>";
                    ltDealDetail.Text += " <tr><td style=\"clear: both; color: White; font-family: Helvetica; text-align:center;font-size: 31.1px;font-weight: bold; line-height: normal; width:100px;\" width='100' class=\"style1\">";
                    ltDealDetail.Text += "<sup><span style='font-size:11px; font-wight:normal; vertical-align:top'>Only</span> $</sup>" + dtDeals.Rows[loopCount]["sellingPrice"].ToString().Trim() + "</td></tr></table></td></tr></table></td></tr></table></td></tr>";
                    ltDealDetail.Text += "<tr><td class=\"style2\"><table><tr class=\"Contant\"><td align=\"left\" class=\"style6\">";
                    ltDealDetail.Text += "<a target=\"_blanck\" href=\"" + ConfigurationManager.AppSettings["YourSite"].Trim() + "/" + strCityName_New + "_" + dtDeals.Rows[loopCount]["dealId"].ToString().Trim() + "\">";
                    ltDealDetail.Text += "<img src=\"" + ConfigurationManager.AppSettings["YourSite"].Trim() + "/images/dealfood/" + dtDeals.Rows[loopCount]["restaurantId"].ToString().Trim() + "/" + dtDeals.Rows[loopCount]["image1"].ToString().Trim() + "\" style='width:160px; height:100px' height='100' width='160'/></a></td><td class=\"style6\"><table><tr class=\"Contant\" style=\"font-family: Helvetica;font-size: 12.44px; font-weight: bold;\"><td colspan=\"2\" align=\"left\">";

                    dSellPrice = Convert.ToDouble(dtDeals.Rows[loopCount]["sellingPrice"].ToString().Trim());
                    dActualPrice = Convert.ToDouble(dtDeals.Rows[loopCount]["valuePrice"].ToString().Trim());
                    dDiscount = 0;
                    if (dSellPrice == 0)
                    {
                        dDiscount = 100;
                    }
                    else
                    {
                        dDiscount = (100 / dActualPrice) * (dActualPrice - dSellPrice);
                    }
                    ltDealDetail.Text += "Reg: <del>$" + dtDeals.Rows[loopCount]["valuePrice"].ToString().Trim() + "</del>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;You Save " + Convert.ToInt32(dDiscount).ToString() + "%</td></tr><tr><td align=\"left\" style='width:480px;' width='480'>";
                    ltDealDetail.Text += StripHTML(dtDeals.Rows[loopCount]["description"].ToString().Trim()).Trim().Length > 65 ? StripHTML(dtDeals.Rows[loopCount]["description"].ToString().Trim()).Substring(0, 62) + "..." : StripHTML(dtDeals.Rows[loopCount]["description"].ToString().Trim());
                    ltDealDetail.Text += " <a class=\"TopLink\" target=\"_blanck\" href=\"" + ConfigurationManager.AppSettings["YourSite"].Trim() + "/" + strCityName_New + "_" + dtDeals.Rows[loopCount]["dealId"].ToString().Trim() + "\">Read More</a></td></tr> </table></td></tr></table></td></tr>";
                    ltDealDetail.Text += "<tr><td style=\" width:680px; background-color: #ff72c7;\" width='680' class=\"style3\" colspan=\"2\"></td></tr>";

                    dealsCount--;
                    loopCount++;

                }
            }
            ltDealDetail.Text += "<tr><td><table style='width:680px;' width='680'><tr><td style='width:340px; text-align:center;' width='340' align=\"center\"><a target=\"_blanck\" href=\"" + ConfigurationManager.AppSettings["YourSite"].Trim() + "/apps.aspx\"><img style='height:87px; width:263px;' height='87' width='263' src=\"" + ConfigurationManager.AppSettings["YourSite"].Trim() + "/images/MobileApps.png\"/></a></td>";
            ltDealDetail.Text += "<td style='width:340px; text-align:center;' width='340' align=\"center\"><a target=\"_blanck\" href=\"" + ConfigurationManager.AppSettings["YourSite"].Trim() + "/affiliate.aspx\"><img style='height:87px; width:263px;' height='87' width='263' src=\"" + ConfigurationManager.AppSettings["YourSite"].Trim() + "/images/Dollars.png\"/></a></td></tr></table>";
            ltDealDetail.Text += "</td></tr><tr><td align=\"left\" style=\"padding: 5px; color: White; font-size: medium; background-color: #000000; width:680px;\" width='680'>Question Or Coment? Contact Us</td></tr><tr><td align=\"left\" style=\"background-color: White; width: auto; height: 1px;\" width=\"2px\"></td></tr><tr><td style=\"background-color: #494949;\"><table><tr><td align=\"left\" valign=\"middle\" width=\"160px\"><table><tr><td width=\"165px\">";
            ltDealDetail.Text += "<img style='height:16px; width:157px;' height='16' width='157' src=\"" + ConfigurationManager.AppSettings["YourSite"].Trim() + "/images/FooterContactUs.png\"/>&nbsp;&nbsp;&nbsp;</td><td align=\"left\" width=\"50px\"><a target=\"_blanck\" href=\"http://www.twitter.com/tastygo\"><img style='height:49px; width:48px;' height='49' width='48' src=\"" + ConfigurationManager.AppSettings["YourSite"].Trim() + "/images/Twitter2.png\" /></a></td><td align=\"left\" width=\"50px\"><a target=\"_blanck\" href=\"https://www.facebook.com/tastygo\"><img style='height:49px; width:48px;' height='49' width='48' src=\"" + ConfigurationManager.AppSettings["YourSite"].Trim() + "/images/Fb.png\" alt=\"#\" /></a></td><td align=\"left\" width=\"50px\"><a target=\"_blanck\" href=\"" + ConfigurationManager.AppSettings["YourSite"].Trim() + "/RSS.aspx\"><img style='height:49px; width:48px;' height='49' width='48' src=\"" + ConfigurationManager.AppSettings["YourSite"].Trim() + "/images/RSS2.png\" alt=\"#\" /></a></td></tr></table></td></tr>";
            ltDealDetail.Text += "<tr><td style=\"padding: 5px; background-color: #494949; color: White; float: left; width: 663px; height: 60px;\" align=\"left\">";
            ltDealDetail.Text += "This email was sent by: TastyGo Online Inc. 20-206 E 6th Ave Vancouver, BC, V5T 1J7, Canada. You are receiving this email because you signed up for the<br /> ";
            ltDealDetail.Text += "Daily Tastygo Deal alerts. If you prefer not to receive Daily Tastygo emails, you can always Unsubscribe.";
            ltDealDetail.Text += "</td></tr></table></td></tr></tbody></table></center>  </body></html>";

        }
        catch (Exception ex)
        {

        }

    }

 
}

