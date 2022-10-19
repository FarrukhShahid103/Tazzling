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

public partial class UpcomingDeals : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        BLLCampaign objcompain = new BLLCampaign();
        DataSet ds = objcompain.getCampaignCalanderByDate();

        if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
         {
             string html = "";
             html += "<span class='salesDateTime' style='padding-bottom: 16px;'>" + DateTime.Now.Date.AddDays(1).ToString("dddd") + "  " + DateTime.Now.AddDays(1).ToString("MM/yyyy") + " <span class='salesTagIcon '>";
             html += "  </span></span> ";
             html += "<ul style='font-family:Helvetica;'>";
             html += "<li class='noBorderBottom'><span class='salesDailyWeeklyShop' style='display: block;'>";
             html += "<span class='weeklyShopsUpCominSalesDollarIcon tastygoShopSprite floatLeft dIB'>";
             html += " </span><span style='color: #7c8087;font-family:Helvetica; font-size: 20px'>Daily Sales</span>";
             html += "</span><span class='salesDateBox '><span class='triIcon'></span><span class='salesWatchIcon tastygoShopSprite'>";
             html += " </span><span> " + "starts" + DateTime.Now.AddDays(1).ToString("MM/dd") + " 11am ET" + " </span> </span>";
             html += "<ul id='upcmgContent' class='upcmgContentShow'>";
             for (int i = 0; ds.Tables[0].Rows.Count > i; i++)
             {                 
                 string imagepath = ds.Tables[0].Rows[i]["restaurantId"].ToString() + "/" + ds.Tables[0].Rows[i]["campaignpicture"].ToString();
                 if (ds.Tables[0].Rows[i]["campaignTitle"].ToString().Trim() != "")
                 {
                     html += "<li><a href='' alt=''><span class='title'>" + (ds.Tables[0].Rows[i]["campaignTitle"].ToString().Trim().Length > 30 ? ds.Tables[0].Rows[i]["campaignTitle"].ToString().Trim().Substring(0, 27) + "..." : ds.Tables[0].Rows[i]["campaignTitle"].ToString()) + "</span>";
                 }
                 else
                 { 
                     html += "<li><a href='' alt=''><span class='title'>No Title for this deal.</span>"; 
                 }
                 if (ds.Tables[0].Rows[i]["campaignDescription"].ToString().Trim() != "")
                 {
                     html += " <span class='des'> " + (ds.Tables[0].Rows[i]["campaignTitle"].ToString().Trim().Length > 50 ? ds.Tables[0].Rows[i]["campaignTitle"].ToString().Trim().Substring(0, 35) + "..." : ds.Tables[0].Rows[i]["campaignTitle"].ToString()) + " </span> </a><span class='imgList transBrd transBrdHvr hide'>";
                 }
                 else
                 {
                     html += " <span class='des'>No Discription for this deal. </span> </a><span class='imgList transBrd transBrdHvr hide'>";
                 }
                 html += "<span class='hoverArrow'></span><a  href='' class='filler'  alt=''>";
                 html += "<img src='images/dealfood/" + imagepath + "' alt='' width='300' height='300' title='' /></a> <span class='imgInfoBottom'><span";
                 html += " class='floatLeft' style=' margin:5px 0 0 10px'>";
                 html += "<div  style=' color: white;font-family: century gothic;font-size: 20px;font-weight: bold;'>  " + (ds.Tables[0].Rows[i]["campaignTitle"].ToString().Trim().Length > 10 ? ds.Tables[0].Rows[i]["campaignTitle"].ToString().Trim().Substring(0, 10) + "..." : ds.Tables[0].Rows[i]["campaignTitle"].ToString()) + " </div> ";
                 html += "<span class='imgDescription'> " + (ds.Tables[0].Rows[i]["campaignTitle"].ToString().Trim().Length > 10 ? ds.Tables[0].Rows[i]["campaignTitle"].ToString().Trim().Substring(0, 10) + "..." : ds.Tables[0].Rows[i]["campaignTitle"].ToString()) + " </span></span><a class='viewDet floatRight round20 tastygoGrad'";
                 html += " href=''><span class='tastygoShopSprite gtIcon imgInfoArrow'>";
                 html += "  </span></a></span><span class='imgInfoTopHov hide'>";
                 html += "  <span class='tastygoShopSprite greyClockBig marginLeft5'></span>";
                 html += "</span></span></li>";


             }
             html += "</ul></li>    </ul>";
             ltltomorow.Text = html;
         }
         else
         {

             string html = "";
             html += "<span class='salesDateTime' style='padding-bottom: 16px;'>" + DateTime.Now.Date.AddDays(1).ToString("dddd") + "  " + DateTime.Now.AddDays(1).ToString("MM/yyyy") + " <span class='salesTagIcon '>";
             html += "  </span></span> ";
             html += "<ul style='font-family:Helvetica;'>";
             html += "<li class='noBorderBottom'><span class='salesDailyWeeklyShop' style='display: block;'>";
             html += "<span class='weeklyShopsUpCominSalesDollarIcon tastygoShopSprite floatLeft dIB'>";
             html += " </span><span style='color: #7c8087; font-size: 20px'>Daily Sales</span>";
             html += "</span><span class='salesDateBox borderR9'><span class='triIcon'></span><span class='salesWatchIcon tastygoShopSprite'>";
             html += " </span><span> " + "starts " + DateTime.Now.AddDays(1).ToString("MM/dd") + " 11am ET" +" </span> </span>";
             html += "<ul id='upcmgContent' class='upcmgContentShow'>";
             html += "<li><span class='title'>There is no feature deal for this day.</span>";

             html += "</ul></li>    </ul>";
             ltltomorow.Text = html;
         }

         if (ds.Tables[1] != null && ds.Tables[1].Rows.Count > 0)
         {
             string html = "";
             html += "<span class='salesDateTime' style='padding-bottom: 16px;'>" + DateTime.Now.Date.AddDays(2).ToString("dddd") + "  " + DateTime.Now.AddDays(2).ToString("MM/yyyy") + " <span class='salesTagIcon '>";
             html += "  </span></span> ";
             html += "<ul style='font-family:Helvetica;'>";
             html += "<li class='noBorderBottom'><span class='salesDailyWeeklyShop' style='display: block;'>";
             html += "<span class='weeklyShopsUpCominSalesDollarIcon tastygoShopSprite floatLeft dIB'>";
             html += " </span><span style='color: #7c8087; font-size: 20px'>Daily Sales</span>";
             html += "</span><span class='salesDateBox2 borderR9'><span class='triIcon2'></span><span class='salesWatchIcon tastygoShopSprite'>";
             html += " </span><span> " + "starts " + DateTime.Now.AddDays(2).ToString("MM/dd") + " 11am ET" + " </span> </span>";
             html += "<ul id='upcmgContent' class='upcmgContentShow'>";
             for (int i = 0; ds.Tables[1].Rows.Count > i; i++)
             {
                 string imagepath = ds.Tables[1].Rows[i]["restaurantId"].ToString() + "/" + ds.Tables[1].Rows[i]["campaignpicture"].ToString();
                 if (ds.Tables[1].Rows[i]["campaignTitle"].ToString().Trim() != "")
                 {
                     html += "<li><a href='' alt=''><span class='title'>" + (ds.Tables[1].Rows[i]["campaignTitle"].ToString().Trim().Length > 30 ? ds.Tables[1].Rows[i]["campaignTitle"].ToString().Trim().Substring(0, 27) + "..." : ds.Tables[1].Rows[i]["campaignTitle"].ToString()) + "</span>";
                 }
                 else { html += "<li><a href='' alt=''><span class='title'>No Title for this deal.</span>"; }
                 if (ds.Tables[1].Rows[i]["campaignDescription"].ToString().Trim() != "")
                 {
                     html += " <span class='des'> " + (ds.Tables[1].Rows[i]["campaignTitle"].ToString().Trim().Length > 50 ? ds.Tables[1].Rows[i]["campaignTitle"].ToString().Trim().Substring(0, 35) + "..." : ds.Tables[1].Rows[i]["campaignTitle"].ToString()) + " </span> </a><span class='imgList transBrd transBrdHvr hide'>";
                 }
                 else
                 {
                     html += " <span class='des'>No Discription for this deal. </span> </a><span class='imgList transBrd transBrdHvr hide'>";
                 }
                 html += "<span class='hoverArrow'></span><a  href='' class='filler'  alt=''>";
                 html += "<img src='images/dealfood/" + imagepath + "' alt='' width='300' height='300' title='' /></a> <span class='imgInfoBottom'><span";
                 html += " class='floatLeft' style=' margin:5px 0 0 10px'>";
                 html += "<div  style=' color: white;font-family: century gothic;font-size: 20px;font-weight: bold;'>  " + (ds.Tables[1].Rows[i]["campaignTitle"].ToString().Trim().Length > 10 ? ds.Tables[1].Rows[i]["campaignTitle"].ToString().Trim().Substring(0, 10) + "..." : ds.Tables[1].Rows[i]["campaignTitle"].ToString()) + " </div> ";
                 html += "<span class='imgDescription'> " + (ds.Tables[1].Rows[i]["campaignTitle"].ToString().Trim().Length > 10 ? ds.Tables[1].Rows[i]["campaignTitle"].ToString().Trim().Substring(0, 10) + "..." : ds.Tables[1].Rows[i]["campaignTitle"].ToString()) + " </span></span><a class='viewDet floatRight round20 tastygoGrad'";
                 html += " href=''><span class='tastygoShopSprite gtIcon imgInfoArrow'>";
                 html += "  </span></a></span><span class='imgInfoTopHov hide'>";
                 html += "  <span class='tastygoShopSprite greyClockBig marginLeft5'></span>";
                 html += "</span></span></li>";


             }
             html += "</ul></li>    </ul>";
            ltlsecDySale.Text = html;
         }
         else
         {
             string html = "";
             html += "<span class='salesDateTime' style='padding-bottom: 16px;'>" + DateTime.Now.Date.AddDays(2).ToString("dddd") + "  " + DateTime.Now.AddDays(2).ToString("MM/yyyy") + " <span class='salesTagIcon '>";
             html += "  </span></span> ";
             html += "<ul style='font-family:Helvetica;'>";
             html += "<li class='noBorderBottom'><span class='salesDailyWeeklyShop' style='display: block;'>";
             html += "<span class='weeklyShopsUpCominSalesDollarIcon tastygoShopSprite floatLeft dIB'>";
             html += " </span><span style='color: #7c8087; font-size: 20px'>Daily Sales</span>";
             html += "</span><span class='salesDateBox2 borderR9'><span class='triIcon2'></span><span class='salesWatchIcon tastygoShopSprite'>";
             html += " </span><span> " + "starts " + DateTime.Now.AddDays(2).ToString("MM/dd") + " 11am ET" + " </span> </span>";
             html += "<ul id='upcmgContent' class='upcmgContentShow'>";
             html += "<li><span class='title'>There is no feature deal for this day.</span>";

             html += "</ul></li>    </ul>";
             ltlsecDySale.Text = html;
         }

         if (ds.Tables[2] != null && ds.Tables[2].Rows.Count > 0)
         {
             string html = "";
             html += "<span class='salesDateTime' style='padding-bottom: 16px;'>" + DateTime.Now.Date.AddDays(3).ToString("dddd") + "  " + DateTime.Now.AddDays(3).ToString("MM/yyyy") + " <span class='salesTagIcon '>";
             html += "  </span></span> ";
             html += "<ul style='font-family:Helvetica;'>";
             html += "<li class='noBorderBottom'><span class='salesDailyWeeklyShop' style='display: block;'>";
             html += "<span class='weeklyShopsUpCominSalesDollarIcon tastygoShopSprite floatLeft dIB'>";
             html += " </span><span style='color: #7c8087; font-size: 20px'>Daily Sales</span>";
             html += "</span><span class='salesDateBox borderR9'><span class='triIcon'></span><span class='salesWatchIcon tastygoShopSprite'>";
             html += " </span><span> " + "starts " + DateTime.Now.AddDays(3).ToString("MM/dd") + " 11am ET" + " </span> </span>";
             html += "<ul id='upcmgContent' class='upcmgContentShow'>";
             for (int i = 0; ds.Tables[2].Rows.Count > i; i++)
             {
                 string imagepath = ds.Tables[2].Rows[i]["restaurantId"].ToString() + "/" + ds.Tables[2].Rows[i]["campaignpicture"].ToString();
                 if (ds.Tables[2].Rows[i]["campaignTitle"].ToString().Trim() != "")
                 {
                     html += "<li><a href='' alt=''><span class='title'>" + (ds.Tables[2].Rows[i]["campaignTitle"].ToString().Trim().Length > 30 ? ds.Tables[2].Rows[i]["campaignTitle"].ToString().Trim().Substring(0, 27) + "..." : ds.Tables[2].Rows[i]["campaignTitle"].ToString()) + "</span>";
                 }
                 else { html += "<li><a href='' alt=''><span class='title'>No Title for this deal.</span>"; }
                 if (ds.Tables[2].Rows[i]["campaignDescription"].ToString().Trim() != "")
                 {
                     html += " <span class='des'> " + (ds.Tables[2].Rows[i]["campaignTitle"].ToString().Trim().Length > 50 ? ds.Tables[2].Rows[i]["campaignTitle"].ToString().Trim().Substring(0, 35) + "..." : ds.Tables[2].Rows[i]["campaignTitle"].ToString()) + " </span> </a><span class='imgList transBrd transBrdHvr hide'>";
                 }
                 else
                 {
                     html += " <span class='des'>No Discription for this deal. </span> </a><span class='imgList transBrd transBrdHvr hide'>"; 
                 }
                 html += "<span class='hoverArrow'></span><a  href='' class='filler'  alt=''>";
                 html += "<img src='images/dealfood/" + imagepath + "' alt='' width='300' height='300' title='' /></a> <span class='imgInfoBottom'><span";
                 html += " class='floatLeft' style=' margin:5px 0 0 10px'>";
                 html += "<div  style=' color: white;font-family: century gothic;font-size: 20px;font-weight: bold;'>  " + (ds.Tables[2].Rows[i]["campaignTitle"].ToString().Trim().Length > 10 ? ds.Tables[2].Rows[i]["campaignTitle"].ToString().Trim().Substring(0, 10) + "..." : ds.Tables[2].Rows[i]["campaignTitle"].ToString()) + " </div> ";
                 html += "<span class='imgDescription'> " + (ds.Tables[2].Rows[i]["campaignTitle"].ToString().Trim().Length > 10 ? ds.Tables[2].Rows[i]["campaignTitle"].ToString().Trim().Substring(0, 10) + "..." : ds.Tables[2].Rows[i]["campaignTitle"].ToString()) + " </span></span><a class='viewDet floatRight round20 tastygoGrad'";
                 html += " href=''><span class='tastygoShopSprite gtIcon imgInfoArrow'>";
                 html += "  </span></a></span><span class='imgInfoTopHov hide'>";
                 html += "  <span class='tastygoShopSprite greyClockBig marginLeft5'></span>";
                 html += "</span></span></li>";


             }
             html += "</ul></li>    </ul>";
             ltltrdDySale.Text = html;
         }
         else
         {
             string html = "";
             html += "<span class='salesDateTime' style='padding-bottom: 16px;'>" + DateTime.Now.Date.AddDays(3).ToString("dddd") + "  " + DateTime.Now.AddDays(3).ToString("MM/yyyy") + " <span class='salesTagIcon '>";
             html += "  </span></span> ";
             html += "<ul style='font-family:Helvetica;'>";
             html += "<li class='noBorderBottom'><span class='salesDailyWeeklyShop' style='display: block;'>";
             html += "<span class='weeklyShopsUpCominSalesDollarIcon tastygoShopSprite floatLeft dIB'>";
             html += " </span><span style='color: #7c8087; font-size: 20px'>Daily Sales</span>";
             html += "</span><span class='salesDateBox borderR9'><span class='triIcon'></span><span class='salesWatchIcon tastygoShopSprite'>";
             html += " </span><span> " + "starts " + DateTime.Now.AddDays(3).ToString("MM/dd") + " 11am ET" + " </span> </span>";
             html += "<ul id='upcmgContent' class='upcmgContentShow'>";
             html += "<li><span class='title'>There is no feature deal for this day.</span>";

             html += "</ul></li>    </ul>";
             ltltrdDySale.Text = html;
         }

         if (ds.Tables[3] != null && ds.Tables[3].Rows.Count > 0)
         {
             string html = "";
             html += "<span class='salesDateTime' style='padding-bottom: 16px;'>" + DateTime.Now.Date.AddDays(4).ToString("dddd") + "  " + DateTime.Now.AddDays(4).ToString("MM/yyyy") + " <span class='salesTagIcon '>";
             html += "  </span></span> ";
             html += "<ul style='font-family:Helvetica;'>";
             html += "<li class='noBorderBottom'><span class='salesDailyWeeklyShop' style='display: block;'>";
             html += "<span class='weeklyShopsUpCominSalesDollarIcon tastygoShopSprite floatLeft dIB'>";
             html += " </span><span style='color: #7c8087; font-size: 20px'>Daily Sales</span>";
             html += "</span><span class='salesDateBox borderR9'><span class='triIcon'></span><span class='salesWatchIcon tastygoShopSprite'>";
             html += " </span><span> " + "starts " + DateTime.Now.AddDays(4).ToString("MM/dd") + " 11am ET" + " </span> </span>";
             html += "<ul id='upcmgContent' class='upcmgContentShow'>";
             for (int i = 0; ds.Tables[3].Rows.Count > i; i++)
             {
                 string imagepath = ds.Tables[3].Rows[i]["restaurantId"].ToString() + "/" + ds.Tables[3].Rows[i]["campaignpicture"].ToString();
                 if (ds.Tables[3].Rows[i]["campaignTitle"].ToString().Trim() != "")
                 {
                     html += "<li><a href='' alt=''><span class='title'>" + (ds.Tables[3].Rows[i]["campaignTitle"].ToString().Trim().Length > 30 ? ds.Tables[3].Rows[i]["campaignTitle"].ToString().Trim().Substring(0, 27) + "..." : ds.Tables[3].Rows[i]["campaignTitle"].ToString()) + "</span>";
                 }
                 else { html += "<li><a href='' alt=''><span class='title'>No Title for this deal.</span>"; }
                 if (ds.Tables[3].Rows[i]["campaignDescription"].ToString().Trim() != "")
                 {
                     html += " <span class='des'> " + (ds.Tables[3].Rows[i]["campaignTitle"].ToString().Trim().Length > 50 ? ds.Tables[3].Rows[i]["campaignTitle"].ToString().Trim().Substring(0, 35) + "..." : ds.Tables[3].Rows[i]["campaignTitle"].ToString()) + " </span> </a><span class='imgList transBrd transBrdHvr hide'>";
                 }
                 else
                 {
                     html += " <span class='des'>No Discription for this deal. </span> </a><span class='imgList transBrd transBrdHvr hide'>";
                 }
                 html += "<span class='hoverArrow'></span><a  href='' class='filler'  alt=''>";
                 html += "<img src='images/dealfood/" + imagepath + "' alt='' width='300' height='300' title='' /></a> <span class='imgInfoBottom'><span";
                 html += " class='floatLeft' style=' margin:5px 0 0 10px'>";
                 html += "<div  style=' color: white;font-family: century gothic;font-size: 20px;font-weight: bold;'>  " + (ds.Tables[3].Rows[i]["campaignTitle"].ToString().Trim().Length > 10 ? ds.Tables[3].Rows[i]["campaignTitle"].ToString().Trim().Substring(0, 10) + "..." : ds.Tables[3].Rows[i]["campaignTitle"].ToString()) + " </div> ";
                 html += "<span class='imgDescription'> " + (ds.Tables[3].Rows[i]["campaignTitle"].ToString().Trim().Length > 10 ? ds.Tables[3].Rows[i]["campaignTitle"].ToString().Trim().Substring(0, 10) + "..." : ds.Tables[3].Rows[i]["campaignTitle"].ToString()) + " </span></span><a class='viewDet floatRight round20 tastygoGrad'";
                 html += " href=''><span class='tastygoShopSprite gtIcon imgInfoArrow'>";
                 html += "  </span></a></span><span class='imgInfoTopHov hide'>";
                 html += "  <span class='tastygoShopSprite greyClockBig marginLeft5'></span>";
                 html += "</span></span></li>";


             }
             html += "</ul></li>    </ul>";
             ltlSecBlkfrst.Text = html;
         }
         else
         {
             string html = "";
             html += "<span class='salesDateTime' style='padding-bottom: 16px;'>" + DateTime.Now.Date.AddDays(4).ToString("dddd") + "  " + DateTime.Now.AddDays(4).ToString("MM/yyyy") + " <span class='salesTagIcon '>";
             html += "  </span></span> ";
             html += "<ul style='font-family:Helvetica;'>";
             html += "<li class='noBorderBottom'><span class='salesDailyWeeklyShop' style='display: block;'>";
             html += "<span class='weeklyShopsUpCominSalesDollarIcon tastygoShopSprite floatLeft dIB'>";
             html += " </span><span style='color: #7c8087; font-size: 20px'>Daily Sales</span>";
             html += "</span><span class='salesDateBox borderR9'><span class='triIcon'></span><span class='salesWatchIcon tastygoShopSprite'>";
             html += " </span><span> " + "starts " + DateTime.Now.AddDays(4).ToString("MM/dd") + " 11am ET" + " </span> </span>";
             html += "<ul id='upcmgContent' class='upcmgContentShow'>";
             html += "<li><span class='title'>There is no feature deal for this day.</span>";

             html += "</ul></li>    </ul>";
             ltlSecBlkfrst.Text = html;
         }

         if (ds.Tables[4] != null && ds.Tables[4].Rows.Count > 0)
         {
             string html = "";
             html += "<span class='salesDateTime' style='padding-bottom: 16px;'>" + DateTime.Now.Date.AddDays(5).ToString("dddd") + "  " + DateTime.Now.AddDays(5).ToString("MM/yyyy") + " <span class='salesTagIcon '>";
             html += "  </span></span> ";
             html += "<ul style='font-family:Helvetica;'>";
             html += "<li class='noBorderBottom'><span class='salesDailyWeeklyShop' style='display: block;'>";
             html += "<span class='weeklyShopsUpCominSalesDollarIcon tastygoShopSprite floatLeft dIB'>";
             html += " </span><span style='color: #7c8087; font-size: 20px'>Daily Sales</span>";
             html += "</span><span class='salesDateBox2 borderR9'><span class='triIcon2'></span><span class='salesWatchIcon tastygoShopSprite'>";
             html += " </span><span> " + "starts " + DateTime.Now.AddDays(5).ToString("MM/dd") + " 11am ET" + " </span> </span>";
             html += "<ul id='upcmgContent' class='upcmgContentShow'>";
             for (int i = 0; ds.Tables[4].Rows.Count > i; i++)
             {
                 string imagepath = ds.Tables[4].Rows[i]["restaurantId"].ToString() + "/" + ds.Tables[4].Rows[i]["campaignpicture"].ToString();
                 if (ds.Tables[4].Rows[i]["campaignTitle"].ToString().Trim() != "")
                 {
                     html += "<li><a href='' alt=''><span class='title'>" + (ds.Tables[4].Rows[i]["campaignTitle"].ToString().Trim().Length > 30 ? ds.Tables[4].Rows[i]["campaignTitle"].ToString().Trim().Substring(0, 27) + "..." : ds.Tables[4].Rows[i]["campaignTitle"].ToString()) + "</span>";
                 }
                 else { html += "<li><a href='' alt=''><span class='title'>No Title for this deal.</span>"; }
                 if (ds.Tables[4].Rows[i]["campaignDescription"].ToString().Trim() != "")
                 {
                     html += " <span class='des'> " + (ds.Tables[4].Rows[i]["campaignTitle"].ToString().Trim().Length > 50 ? ds.Tables[4].Rows[i]["campaignTitle"].ToString().Trim().Substring(0, 35) + "..." : ds.Tables[4].Rows[i]["campaignTitle"].ToString()) + " </span> </a><span class='imgList transBrd transBrdHvr hide'>";
                 }
                 else
                 {
                     html += " <span class='des'>No Discription for this deal. </span> </a><span class='imgList transBrd transBrdHvr hide'>";
                 }
                 html += "<span class='hoverArrow'></span><a  href='' class='filler'  alt=''>";
                 html += "<img src='images/dealfood/" + imagepath + "' alt='' width='300' height='300' title='' /></a> <span class='imgInfoBottom'><span";
                 html += " class='floatLeft' style=' margin:5px 0 0 10px'>";
                 html += "<div  style=' color: white;font-family: century gothic;font-size: 20px;font-weight: bold;'>  " + (ds.Tables[4].Rows[i]["campaignTitle"].ToString().Trim().Length > 10 ? ds.Tables[4].Rows[i]["campaignTitle"].ToString().Trim().Substring(0, 10) + "..." : ds.Tables[4].Rows[i]["campaignTitle"].ToString()) + " </div> ";
                 html += "<span class='imgDescription'> " + (ds.Tables[4].Rows[i]["campaignTitle"].ToString().Trim().Length > 10 ? ds.Tables[4].Rows[i]["campaignTitle"].ToString().Trim().Substring(0, 10) + "..." : ds.Tables[4].Rows[i]["campaignTitle"].ToString()) + " </span></span><a class='viewDet floatRight round20 tastygoGrad'";
                 html += " href=''><span class='tastygoShopSprite gtIcon imgInfoArrow'>";
                 html += "  </span></a></span><span class='imgInfoTopHov hide'>";
                 html += "  <span class='tastygoShopSprite greyClockBig marginLeft5'></span>";
                 html += "</span></span></li>";


             }
             html += "</ul></li>    </ul>";
             ltlSecBlkscnd.Text = html;
         }
         else
         {
             string html = "";
             html += "<span class='salesDateTime' style='padding-bottom: 16px;'>" + DateTime.Now.Date.AddDays(5).ToString("dddd") + "  " + DateTime.Now.AddDays(5).ToString("MM/yyyy") + " <span class='salesTagIcon'>";
             html += "  </span></span> ";
             html += "<ul style='font-family:Helvetica;'>";
             html += "<li class='noBorderBottom'><span class='salesDailyWeeklyShop' style='display: block;'>";
             html += "<span class='weeklyShopsUpCominSalesDollarIcon tastygoShopSprite floatLeft dIB'>";
             html += " </span><span style='color: #7c8087; font-size: 20px'>Daily Sales</span>";
             html += "</span><span class='salesDateBox4thDay borderR9'><span class='triIcon2'></span><span class='salesWatchIcon tastygoShopSprite'>";
             html += " </span><span> " + "starts " + DateTime.Now.AddDays(5).ToString("MM/dd") + " 11am ET" + " </span> </span>";
             html += "<ul id='upcmgContent' class='upcmgContentShow'>";
             html += "<li><span class='title'>There is no feature deal for this day.</span>";

             html += "</ul></li>    </ul>";
             ltlSecBlkscnd.Text = html;
         }
         if (ds.Tables[5] != null && ds.Tables[5].Rows.Count > 0)
         {
             string html = "";

             html += "<span class='salesDateTime' style='padding-bottom: 16px;'>" + DateTime.Now.Date.AddDays(6).ToString("dddd") + "  " + DateTime.Now.AddDays(6).ToString("MM/yyyy") + " <span class='salesTagIcon '>";
             html += "  </span></span> ";
             html += "<ul style='font-family:Helvetica;'>";
             html += "<li class='noBorderBottom'><span class='salesDailyWeeklyShop' style='display: block;'>";
             html += "<span class='weeklyShopsUpCominSalesDollarIcon tastygoShopSprite floatLeft dIB'>";
             html += " </span><span style='color: #7c8087; font-size: 20px'>Daily Sales</span>";
             html += "</span><span class='salesDateBox borderR9'><span class='triIcon'></span><span class='salesWatchIcon tastygoShopSprite'>";
             html += " </span><span> " + "starts " + DateTime.Now.AddDays(6).ToString("MM/dd") + " 11am ET" + " </span> </span>";
             html += "<ul id='upcmgContent' class='upcmgContentShow'>";
             for (int i = 0; ds.Tables[5].Rows.Count > i; i++)
             {
                 
                 string imagepath = ds.Tables[5].Rows[i]["restaurantId"].ToString() + "/" + ds.Tables[5].Rows[i]["campaignpicture"].ToString();
                 if (ds.Tables[5].Rows[i]["campaignTitle"].ToString().Trim() != "")
                 {
                     html += "<li><a href='' alt=''><span class='title'>" + (ds.Tables[5].Rows[i]["campaignTitle"].ToString().Trim().Length > 30 ? ds.Tables[5].Rows[i]["campaignTitle"].ToString().Trim().Substring(0, 27) + "..." : ds.Tables[5].Rows[i]["campaignTitle"].ToString()) + "</span>";
                 }
                 else { html += "<li><a href='' alt=''><span class='title'>No Title for this deal.</span>"; }
                 if ( ds.Tables[5].Rows[i]["campaignDescription"].ToString().Trim() != "")
                 {
                     html += " <span class='des'> " + (ds.Tables[5].Rows[i]["campaignTitle"].ToString().Trim().Length > 50 ? ds.Tables[5].Rows[i]["campaignTitle"].ToString().Trim().Substring(0, 35) + "..." : ds.Tables[5].Rows[i]["campaignTitle"].ToString()) + " </span> </a><span class='imgList transBrd transBrdHvr hide'>";
                 }
                 else
                 {
                     html += " <span class='des'>No Discription for this deal. </span> </a><span class='imgList transBrd transBrdHvr hide'>";
                 }
                 html += "<span class='hoverArrow'></span><a  href='' class='filler'  alt=''>";
                 html += "<img src='images/dealfood/" + imagepath + "' alt='' width='300' height='300' title='' /></a> <span class='imgInfoBottom'><span";
                 html += " class='floatLeft' style=' margin:5px 0 0 10px'>";
                 html += "<div  style=' color: white;font-family: century gothic;font-size: 20px;font-weight: bold;'>  " + (ds.Tables[5].Rows[i]["campaignTitle"].ToString().Trim().Length > 10 ? ds.Tables[5].Rows[i]["campaignTitle"].ToString().Trim().Substring(0, 10) + "..." : ds.Tables[5].Rows[i]["campaignTitle"].ToString()) + " </div> ";
                 html += "<span class='imgDescription'> " + (ds.Tables[5].Rows[i]["campaignTitle"].ToString().Trim().Length > 10 ? ds.Tables[5].Rows[i]["campaignTitle"].ToString().Trim().Substring(0, 10) + "..." : ds.Tables[5].Rows[i]["campaignTitle"].ToString()) + " </span></span><a class='viewDet floatRight round20 tastygoGrad'";
                 html += " href=''><span class='tastygoShopSprite gtIcon imgInfoArrow'>";
                 html += "  </span></a></span><span class='imgInfoTopHov hide'>";
                 html += "  <span class='tastygoShopSprite greyClockBig marginLeft5'></span>";
                 html += "</span></span></li>";


             }
             html += "</ul></li>    </ul>";
             ltlSecBlkthrd.Text = html;
         }
         else
         {
             string html = "";
             html += "<span class='salesDateTime' style='padding-bottom: 16px;'>" + DateTime.Now.Date.AddDays(6).ToString("dddd") + "  " + DateTime.Now.AddDays(6).ToString("MM/yyyy") + " <span class='salesTagIcon '>";
             html += "  </span></span> ";
             html += "<ul style='font-family:Helvetica;'>";
             html += "<li class='noBorderBottom'><span class='salesDailyWeeklyShop' style='display: block;'>";
             html += "<span class='weeklyShopsUpCominSalesDollarIcon tastygoShopSprite floatLeft dIB'>";
             html += " </span><span style='color: #7c8087; font-size: 20px'>Daily Sales</span>";
             html += "</span><span class='salesDateBox borderR9'><span class='triIcon'></span><span class='salesWatchIcon tastygoShopSprite'>";
             html += " </span><span> " + "starts " + DateTime.Now.AddDays(6).ToString("MM/dd") + " 11am ET" + " </span> </span>";
             html += "<ul id='upcmgContent' class='upcmgContentShow'>";
             html += "<li><span class='title'>There is no feature deal for this day.</span>";

             html += "</ul></li>    </ul>";
             ltlSecBlkthrd.Text = html;
         }
         if (ds.Tables[6] != null && ds.Tables[6].Rows.Count > 0)
         {
             string html = "";
             html += "<span class='salesDateTime' style='padding-bottom: 16px;'>" + DateTime.Now.Date.AddDays(7).ToString("dddd") + "  " + DateTime.Now.AddDays(7).ToString("MM/yyyy") + " <span class='salesTagIcon '>";
             html += "  </span></span> ";
             html += "<ul style='font-family:Helvetica;'>";
             html += "<li class='noBorderBottom'><span class='salesDailyWeeklyShop' style='display: block;'>";
             html += "<span class='weeklyShopsUpCominSalesDollarIcon tastygoShopSprite floatLeft dIB'>";
             html += " </span><span style='color: #7c8087; font-size: 20px'>Daily Sales</span>";
             html += "</span><span class='salesDateBox borderR9'><span class='triIcon'></span><span class='salesWatchIcon tastygoShopSprite'>";
             html += " </span><span> " + "starts " + DateTime.Now.AddDays(7).ToString("MM/dd") + " 11am ET" + " </span> </span>";
             html += "<ul id='upcmgContent' class='upcmgContentShow'>";
             for (int i = 0; ds.Tables[6].Rows.Count > i; i++)
             {
                 string imagepath = ds.Tables[6].Rows[i]["restaurantId"].ToString() + "/" + ds.Tables[6].Rows[i]["campaignpicture"].ToString();
                 if (ds.Tables[6].Rows[i]["campaignTitle"].ToString().Trim() != "")
                 {
                     html += "<li><a href='' alt=''><span class='title'>" + (ds.Tables[6].Rows[i]["campaignTitle"].ToString().Trim().Length > 30 ? ds.Tables[6].Rows[i]["campaignTitle"].ToString().Trim().Substring(0, 27) + "..." : ds.Tables[6].Rows[i]["campaignTitle"].ToString()) + "</span>";
                 }
                 else { html += "<li><a href='' alt=''><span class='title'>No Title for this deal.</span>"; }
                 if (ds.Tables[6].Rows[i]["campaignDescription"].ToString().Trim() != "")
                 {
                     html += " <span class='des'> " + (ds.Tables[6].Rows[i]["campaignTitle"].ToString().Trim().Length > 50 ? ds.Tables[6].Rows[i]["campaignTitle"].ToString().Trim().Substring(0, 35) + "..." : ds.Tables[6].Rows[i]["campaignTitle"].ToString()) + " </span> </a><span class='imgList transBrd transBrdHvr hide'>";
                 }
                 else
                 {
                     html += " <span class='des'>No Discription for this deal. </span> </a><span class='imgList transBrd transBrdHvr hide'>";
                 }
                 html += "<span class='hoverArrow'></span><a  href='' class='filler'  alt=''>";
                 html += "<img src='images/dealfood/" + imagepath + "' alt='' width='300' height='300' title='' /></a> <span class='imgInfoBottom'><span";
                 html += " class='floatLeft' style=' margin:5px 0 0 10px'>";
                 html += "<div  style=' color: white;font-family: century gothic;font-size: 20px;font-weight: bold;'>  " + (ds.Tables[6].Rows[i]["campaignTitle"].ToString().Trim().Length > 10 ? ds.Tables[6].Rows[i]["campaignTitle"].ToString().Trim().Substring(0, 10) + "..." : ds.Tables[6].Rows[i]["campaignTitle"].ToString()) + " </div> ";
                 html += "<span class='imgDescription'> " + (ds.Tables[6].Rows[i]["campaignTitle"].ToString().Trim().Length > 10 ? ds.Tables[6].Rows[i]["campaignTitle"].ToString().Trim().Substring(0, 10) + "..." : ds.Tables[6].Rows[i]["campaignTitle"].ToString()) + " </span></span><a class='viewDet floatRight round20 tastygoGrad'";
                 html += " href=''><span class='tastygoShopSprite gtIcon imgInfoArrow'>";
                 html += "  </span></a></span><span class='imgInfoTopHov hide'>";
                 html += "  <span class='tastygoShopSprite greyClockBig marginLeft5'></span>";
                 html += "</span></span></li>";


             }
             html += "</ul></li>    </ul>";
             ltltrdBlk.Text = html;
         }
         else
         {
             string html = "";
             html += "<span class='salesDateTime' style='padding-bottom: 16px;'>" + DateTime.Now.Date.AddDays(7).ToString("dddd") + "  " + DateTime.Now.AddDays(7).ToString("MM/yyyy") + " <span class='salesTagIcon '>";
             html += "  </span></span> ";
             html += "<ul style='font-family:Helvetica;'>";
             html += "<li class='noBorderBottom'><span class='salesDailyWeeklyShop' style='display: block;'>";
             html += "<span class='weeklyShopsUpCominSalesDollarIcon tastygoShopSprite floatLeft dIB'>";
             html += " </span><span style='color: #7c8087; font-size: 20px'>Daily Sales</span>";
             html += "</span><span class='salesDateBox borderR9'><span class='triIcon'></span><span class='salesWatchIcon tastygoShopSprite'>";
             html += " </span><span> " + "starts " + DateTime.Now.AddDays(7).ToString("MM/dd") + " 11am ET" + " </span> </span>";
             html += "<ul id='upcmgContent' class='upcmgContentShow'>";
             html += "<li><span class='title'>There is no feature deal for this day.</span>";

             html += "</ul></li>    </ul>";
             ltltrdBlk.Text = html;
         }


    }
}
