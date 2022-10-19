using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;

public partial class HomeTest : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindDeals();
        }
    }

    private void BindDeals()
    {

        BLLCampaign objCampaigns = new BLLCampaign();
        objCampaigns.creationDate = DateTime.Now;
        string _html = "";

        #region Campaigns
        DataTable dtCampaigs = objCampaigns.getCurrentCampaignByGivenDate();

        if (dtCampaigs != null && dtCampaigs.Rows.Count > 0)
        {
            for (int i = 0; i < dtCampaigs.Rows.Count; i++)
            {
                int check = i % 2;
                string DealAnchor = "shop.aspx?cid=" + dtCampaigs.Rows[i]["campaignID"].ToString().Trim();

                TimeSpan dtDifference = Convert.ToDateTime(dtCampaigs.Rows[i]["campaignEndTime"].ToString().Trim()).Subtract(objCampaigns.creationDate);

                string SealsEndIn = "Sales end in " + dtDifference.Days + " days and " + dtDifference.Hours + " hours";
                string DealTopTag = "";
                objCampaigns.campaignID = Convert.ToInt32(dtCampaigs.Rows[i]["campaignID"].ToString().Trim());
                bool IsMaxOrderLimited = objCampaigns.GetCampaginSoldOutStatus();

                if ((IsMaxOrderLimited) && Convert.ToInt32(dtCampaigs.Rows[i]["MaxOrders"].ToString().Trim()) <= Convert.ToInt32(dtCampaigs.Rows[i]["campaignOrders"].ToString().Trim()))
                {
                    DealTopTag = "Sold Out";
                }
                else
                {
                    TimeSpan campaignDateDifference;

                    campaignDateDifference = (Convert.ToDateTime(dtCampaigs.Rows[i]["campaignEndTime"].ToString().Trim()) - DateTime.Now);
                    if (campaignDateDifference.TotalHours < 24)
                    {
                        DealTopTag = "Ending Soon";
                    }
                    else
                    {
                        campaignDateDifference = (DateTime.Now - Convert.ToDateTime(dtCampaigs.Rows[i]["campaignStartTime"].ToString().Trim()));
                        if (campaignDateDifference.TotalHours < 24)
                        {
                            DealTopTag = "New Deal";
                        }
                        else
                        {
                            if (Convert.ToInt32(dtCampaigs.Rows[i]["campaignOrders"].ToString().Trim()) > 99)
                            {
                                DealTopTag = "Hot Deal";
                            }
                            else
                            {
                                DealTopTag = "none";
                            }
                        }
                    }
                    
                }
                string DealTitle = dtCampaigs.Rows[i]["campaignTitle"].ToString().Trim();
                string DealSubTitle = dtCampaigs.Rows[i]["campaignShortDescription"].ToString().Trim();
                DealSubTitle = DealSubTitle.ToString().Trim().Length > 25 ? DealSubTitle.ToString().Substring(0, 23) + "..." : DealSubTitle.ToString().Trim();
                string imagePath = ConfigurationManager.AppSettings["YourSite"].ToString() + "/Images/dealfood/" + dtCampaigs.Rows[i]["restaurantId"].ToString().Trim() + "/" + dtCampaigs.Rows[i]["campaignpicture"].ToString().Trim();
                string totalOrders = dtCampaigs.Rows[i]["campaignOrders"].ToString().Trim();




//                _html += @"<li class='DealBox'><a href=" + DealAnchor + @">
//                <img class='DealImage' src='" + imagePath + @"' />
//                <p>
//                    <img class='DealClock' src='images/Deal_bg_Clock.png' />
//                    <span>" + SealsEndIn + @"</span>
//                    <img class='DealRightArrow' src='images/Deal_Right_Arrow.png' />
//                </p>
//                </a>";
//                if (DealTopTag != "none")
//                {
//                    _html += @"<div style='position: absolute;'>
//                    <div style='position: absolute; top: -292px; left: 15px;'>
//                        <div class='DealTopArrow'>
//                            <div class='DealTopTag'>
//                               " + DealTopTag + @"
//                            </div>
//                        </div>
//                    </div>
//                </div>";
//                }
//                _html += @"<div>
//                    <div style='clear: both; margin-top: 10px; overflow: hidden;'>
//                        <span style='color: #103054; font-size: 16px; font-weight: bold;'>" + DealTitle + @"
//                        </span>
//                    </div>
//                    <div style='clear: both;'>
//                        <div style='float: left;'>
//                            <span style='color: #afb1b6; clear: both; font-size: 12px;'>" + DealSubTitle + @" </span>
//                        </div>
//                        <div style='float: right;'>
//                            <span style='color: #29b1e6; font-size: 16px; font-weight: bold;'>" + totalOrders + @" </span><span
//                                style='color: #4c4c4c; font-size: 14px; font-weight: bold;'>Bought </span>
//                        </div>
//                    </div>
//                </div>
//            </li>";

                /////////////////////
                string _float = "right";
                if (check == 0)
                {
                    _float = "left";
                }
                _html += @"<li style='float: " + _float + @";' class='picture '>";
                if (DealTopTag != "none")
                {
                    _html += @" <span class='new'>
                    <div class='TagText'>
                        " + DealTopTag + @"
                    </div>
                </span>";
                }
                _html += @"<a href=" + DealAnchor + @">
                <div class='top'>
                    <span class='arrow'></span>
                    <div class='cover'>
                        <img width='480' height='240' src='" + imagePath + @"'alt='' /></div>
                </div>
                <p>
                    
                    <img style='float:left' class='DealClock' src='images/Deal_bg_Clock.png' />
                    <span style='float:left'>" + SealsEndIn + @"</span>
                    <img class='DealRightArrow' src='images/Deal_Right_Arrow.png' />
                </p>
            </a>
                <div class='bottom'>
                    <div class='ProductTitle'>
                       " + DealTitle + @"
                    </div>
                    <div class='ProductSubTitle'>
                        " + DealSubTitle + @"
                    </div>
                    <div class='right'>
                        <div class='TotalBuyCount'>
                            " + totalOrders + @" 
                        </div>
                        <div class='ProductBought'>
                            Bought
                        </div>
                    </div>
                </div>
            </li>";


            }

           
        }
        ltDeals.Text = _html;
        #endregion
    }

    
        
}