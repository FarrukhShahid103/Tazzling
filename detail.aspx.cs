using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;

public partial class MyShopDetail : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        BLLProducts objProduct = new BLLProducts();
        BLLProductProperties objProductProperties = new BLLProductProperties();
        if (ViewState["title"] != null)
        {
            Page.Title = ViewState["title"].ToString();
        }
        if (!IsPostBack)
        {
            try
            {
                int productID = 0;
                if (Request.QueryString["pid"] != null
                    && Request.QueryString["pid"].ToString().Trim() != ""
                    && int.TryParse(Request.QueryString["pid"].ToString().Trim(), out productID))
                {
                    DataTable dtUser = null;
                    long userID = 0;
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
                    if (dtUser != null && dtUser.Rows.Count > 0)
                    {
                        long.TryParse(dtUser.Rows[0]["userId"].ToString().Trim(), out userID);
                    }
                    objProduct.productID = productID;
                    objProduct.createdBy = userID;
                    DataTable dtProduct = objProduct.getProductsByProductIDForClient();
                    
                    if (dtProduct != null && dtProduct.Rows.Count > 0)
                    {
                        //lblTotalSold.Text = "Sold: <span style='color:#FF42E7;'>" + dtProduct.Rows[0]["Orders"].ToString().Trim() + "</span>";
                        lblEstimatedArrivalTime.Text = dtProduct.Rows[0]["estimatedArivalTime"].ToString().Trim();
                        lblReturnPolicy.Text = dtProduct.Rows[0]["returnPolicy"].ToString().Trim();
                        Page.Title = "Tazzling.com | " + dtProduct.Rows[0]["title"].ToString().Trim();
                        lblTitle.Text = dtProduct.Rows[0]["title"].ToString().Trim();
                        lblSubTitle.Text = dtProduct.Rows[0]["subTitle"].ToString().Trim();
                        hlReturnURL.Text = "Return to " + dtProduct.Rows[0]["campaignTitle"].ToString().Trim();
                        hlReturnURL.NavigateUrl = "shop.aspx?cid=" + dtProduct.Rows[0]["campaignID"].ToString().Trim();
                        int dealStatus = 0;
                        if (dtProduct.Rows[0]["MyFavorites"].ToString().Trim() == "0" && userID > 0)
                        {
                            ltFavroutite.Text = "<div style='float: right; font-weight: bold; padding-left: 5px;'><div id='FavouriteDeal-1' class='FavouriteDeal' style='margin-top:3px !important;' onclick='javascript:AddToMyFavourite(\"1|" + dtProduct.Rows[0]["productID"].ToString() + "\");'></div><div id='fav-1' class='FavouriteText' style='margin-left:0px !important; margin-top:0px !important;margin-right:0px !important;margin-left:3px !important;'>" + Convert.ToString(dtProduct.Rows[0]["TotalFavorites"].ToString()).ToString() + "</div></div>";
                        }
                        else
                        {
                            ltFavroutite.Text = "<div style='float: right; font-weight: bold; padding-left: 5px;'><div class='FavouriteDealNonClickable' style='margin-top:3px !important;' onclick='javascript:void(0);'></div> <div id='fav-1' class='FavouriteText' style='margin-left:0px !important; margin-top:0px !important;margin-right:0px !important;margin-left:3px !important;'>" + Convert.ToString(dtProduct.Rows[0]["TotalFavorites"].ToString()).ToString() + "</div></div>";
                        }

                        if (dtProduct.Rows[0]["maxOrdersPerUser"].ToString().Trim() != null
                                   && dtProduct.Rows[0]["maxOrdersPerUser"].ToString().Trim() != "0"
                                   && Convert.ToInt32(dtProduct.Rows[0]["SuccessfulOrders"].ToString()) >= Convert.ToInt32(dtProduct.Rows[0]["maxOrdersPerUser"].ToString().Trim()))
                         {
                            dealStatus = 1;
                            ltAddToCart.Text = "<div style='display:none;padding-top:13px;' id='Loader-1'><img src='images/Loader.gif' /></div><img src='Images/add-to-cart-gray.png'/>";
                            //ltAddToCart2.Text = "<div style='display:none;padding-top:13px;' id='Loader-2'><img src='images/Loader.gif' /></div><img src='Images/add-to-cart-gray.png'/>";
                            string[] strDealImages = dtProduct.Rows[0]["images"].ToString().Split(',');
                            if (strDealImages.Length > 0)
                            {
                                if ((strDealImages.Length == 1) || (strDealImages[1].Trim() == "" && strDealImages[2].Trim() == ""))
                                {
                                    ltSlideShow.Text = "<div style='clear: both;'>";
                                    Literal lit = (Literal)Master.FindControl("linkID");
                                    if (lit != null)
                                    {
                                        lit.Text = "<link rel='image_src' href='" + ConfigurationManager.AppSettings["YourSite"].ToString() + "/Images/dealfood/" + dtProduct.Rows[0]["restaurantId"].ToString().Trim() + "/" + strDealImages[0] + "' >";
                                    }
                                    // ltSlideShow.Text += "<div><img height='308px' width='360px' src='" + ConfigurationManager.AppSettings["YourSite"] + "/Images/dealfood/" + dtProduct.Rows[0]["restaurantId"].ToString().Trim() + "/" + strDealImages[0] + "'/></div>";
                                    ltSlideShow.Text += "<div><div class='clearfix'><a href='" + ConfigurationManager.AppSettings["YourSite"] + "/Images/dealfood/" + dtProduct.Rows[0]["restaurantId"].ToString().Trim() + "/Large/" + strDealImages[0] + "' class='jqzoom' rel='gal1'><img height='276px' width='380px' src='" + ConfigurationManager.AppSettings["YourSite"] + "/Images/dealfood/" + dtProduct.Rows[0]["restaurantId"].ToString().Trim() + "/" + strDealImages[0] + "'></a>";

                                    ltSlideShow.Text += "<div style='float:left; position:absolute; margin-left:20px; margin-top:267px; z-index:100;'><img src='Images/upArrowWhite.png' alt='' /></div>";
                                    ltSlideShow.Text += "<div style='float:left; clear:both; background-color:White; width:100%; padding-bottom:10px; padding-top:5px; box-shadow:0 1px 2px rgba(0, 0, 0, 0.2); border-radius:0 0 5px 5px;'>";
                                    ltSlideShow.Text += "<div style='width: 100%; clear: both; padding-left:10px; padding-top:5px;'>";
                                    ltSlideShow.Text += "<div style='float: left;'>";
                                    ltSlideShow.Text += "<img src='Images/clock-icon.png' />";
                                    ltSlideShow.Text += "</div>";
                                    ltSlideShow.Text += "<div style='float: left; color: #102343; font-size: 18px; padding-top: 4px; padding-left: 10px;'>Time Left:</div>";
                                    ltSlideShow.Text += "<div style='float: left; color: #102343; font-size: 18px; padding-top: 4px; padding-left: 10px;'>";
                                    ltSlideShow.Text += "<div id='defaultCountdown' align='center'></div></div></div></div>";
                                    ltSlideShow.Text += "</div></div>";

                                    ltSlideShow.Text += "<div style='float: right; position: relative;'><div style='position: absolute; right: -6px; top: -105px; z-index:500;'><img src='Images/sold-out.png' /></div></div>";
                                    ltSlideShow.Text += "</div>";
                                }
                                else
                                {
                                    // ltSlideShow.Text += "<ul id=\"myGallery\">";
                                    for (int i = 0; i < strDealImages.Length; i++)
                                    {
                                        if (i == 0)
                                        {
                                            Literal lit = (Literal)Master.FindControl("linkID");
                                            if (lit != null)
                                            {
                                                lit.Text = "<link rel='image_src' href='" + ConfigurationManager.AppSettings["YourSite"].ToString() + "/Images/dealfood/" + dtProduct.Rows[0]["restaurantId"].ToString().Trim() + "/" + strDealImages[i] + "' >";
                                            }
                                            ltSlideShow.Text += "<div style='float:left; border-radius:5px; width:100%;'><div class='clearfix' style='margin-bottom:15px; width:100%;'><a href='" + ConfigurationManager.AppSettings["YourSite"] + "/Images/dealfood/" + dtProduct.Rows[0]["restaurantId"].ToString().Trim() + "/Large/" + strDealImages[i] + "' class='jqzoom' rel='gal1'><img height='276px' width='380px' src='" + ConfigurationManager.AppSettings["YourSite"] + "/Images/dealfood/" + dtProduct.Rows[0]["restaurantId"].ToString().Trim() + "/" + strDealImages[i] + "'></a>";
                                            ltSlideShow.Text += "<div style='float:left; position:absolute; margin-left:20px; margin-top:267px; z-index:100;'><img src='Images/upArrowWhite.png' alt='' /></div>";
                                            ltSlideShow.Text += "<div style='float:left; clear:both; background-color:White; width:100%; padding-bottom:10px; padding-top:5px; box-shadow:0 1px 2px rgba(0, 0, 0, 0.2); border-radius:0 0 5px 5px;'>";
                                            ltSlideShow.Text += "<div style='width: 100%; clear: both; padding-left:10px; padding-top:5px;'>";
                                            ltSlideShow.Text += "<div style='float: left;'>";
                                            ltSlideShow.Text += "<img src='Images/clock-icon.png' />";
                                            ltSlideShow.Text += "</div>";
                                            ltSlideShow.Text += "<div style='float: left; color: #102343; font-size: 18px; padding-top: 4px; padding-left: 10px;'>Time Left:</div>";
                                            ltSlideShow.Text += "<div style='float: left; color: #102343; font-size: 18px; padding-top: 4px; padding-left: 10px;'>";
                                            ltSlideShow.Text += "<div id='defaultCountdown' align='center'></div></div></div></div>";
                                            ltSlideShow.Text += "</div></div>";

                                            ltSlideShow.Text += "<div style='clear:both; float;left; padding:bottom:5px;'>More Images</div>";

                                            ltSlideShow.Text += "<div class='clearfix' ><ul id='thumblist' class='clearfix' ><li><a class='zoomThumbActive' href='javascript:void(0);' rel=\"{gallery: 'gal1', smallimage:'" + ConfigurationManager.AppSettings["YourSite"] + "/Images/dealfood/" + dtProduct.Rows[0]["restaurantId"].ToString().Trim() + "/" + strDealImages[i] + "',largeimage: '" + ConfigurationManager.AppSettings["YourSite"] + "/Images/dealfood/" + dtProduct.Rows[0]["restaurantId"].ToString().Trim() + "/Large/" + strDealImages[i] + "'}\"><img height='64px' width='88px' src='" + ConfigurationManager.AppSettings["YourSite"] + "/Images/dealfood/" + dtProduct.Rows[0]["restaurantId"].ToString().Trim() + "/" + strDealImages[i] + "'></a></li>";
                                            //ltSlideShow.Text += "<li><img data-frame='" + ConfigurationManager.AppSettings["YourSite"] + "/Images/dealfood/" + dtProduct.Rows[0]["restaurantId"].ToString().Trim() + "/thumb/" + strDealImages[i] + "' src='" + ConfigurationManager.AppSettings["YourSite"] + "/Images/dealfood/" + dtProduct.Rows[0]["restaurantId"].ToString().Trim() + "/" + strDealImages[i] + "'/></li>";
                                        }
                                        else if (strDealImages[i].ToString().Trim() != "")
                                        {
                                            // ltSlideShow.Text += "<li><img data-frame='" + ConfigurationManager.AppSettings["YourSite"] + "/Images/dealfood/" + dtProduct.Rows[0]["restaurantId"].ToString().Trim() + "/thumb/" + strDealImages[i] + "' src='" + ConfigurationManager.AppSettings["YourSite"] + "/Images/dealfood/" + dtProduct.Rows[0]["restaurantId"].ToString().Trim() + "/" + strDealImages[i] + "'/></li>";
                                            ltSlideShow.Text += "<li><a href='javascript:void(0);' rel=\"{gallery: 'gal1', smallimage:'" + ConfigurationManager.AppSettings["YourSite"] + "/Images/dealfood/" + dtProduct.Rows[0]["restaurantId"].ToString().Trim() + "/" + strDealImages[i] + "',largeimage:'" + ConfigurationManager.AppSettings["YourSite"] + "/Images/dealfood/" + dtProduct.Rows[0]["restaurantId"].ToString().Trim() + "/Large/" + strDealImages[i] + "'}\"><img height='64px' width='88px' src='" + ConfigurationManager.AppSettings["YourSite"] + "/Images/dealfood/" + dtProduct.Rows[0]["restaurantId"].ToString().Trim() + "/thumb/" + strDealImages[i] + "'></a></li>";
                                        }
                                    }
                                    ltSlideShow.Text += "</ul></div>";
                                    ltSlideShow.Text += "<div style='float: right; position: relative;'><div style='position: absolute; right: -6px; top: -200px; z-index:1000;'><img src='Images/sold-out.png' /></div></div>";
                                }
                            }
                        }
                        else if (Convert.ToDateTime(dtProduct.Rows[0]["campaignEndTime"].ToString().Trim()) <= DateTime.Now)
                        {
                            dealStatus = 2;
                            ltAddToCart.Text = "<div style='display:none;padding-top:13px;' id='Loader-1'><img src='images/Loader.gif' /></div><img src='Images/add-to-cart-gray.png'/>";
                            //ltAddToCart2.Text = "<div style='display:none;padding-top:13px;' id='Loader-2'><img src='images/Loader.gif' /></div><img src='Images/add-to-cart-gray.png'/>";
                            string[] strDealImages = dtProduct.Rows[0]["images"].ToString().Split(',');
                            if (strDealImages.Length > 0)
                            {
                                if ((strDealImages.Length == 1) || (strDealImages[1].Trim() == "" && strDealImages[2].Trim() == ""))
                                {
                                    ltSlideShow.Text = "<div style='clear: both;'>";
                                    Literal lit = (Literal)Master.FindControl("linkID");
                                    if (lit != null)
                                    {
                                        lit.Text = "<link rel='image_src' href='" + ConfigurationManager.AppSettings["YourSite"].ToString() + "/Images/dealfood/" + dtProduct.Rows[0]["restaurantId"].ToString().Trim() + "/" + strDealImages[0] + "' >";
                                    }
                                    //ltSlideShow.Text += "<div><img height='308px' width='360px' src='" + ConfigurationManager.AppSettings["YourSite"] + "/Images/dealfood/" + dtProduct.Rows[0]["restaurantId"].ToString().Trim() + "/" + strDealImages[0] + "'/></div>";
                                    ltSlideShow.Text += "<div><div style='float:left; border-radius:5px; width:100%;'><div class='clearfix'><a href='" + ConfigurationManager.AppSettings["YourSite"] + "/Images/dealfood/" + dtProduct.Rows[0]["restaurantId"].ToString().Trim() + "/Large/" + strDealImages[0] + "' class='jqzoom' rel='gal1'><img height='276px' width='380px' src='" + ConfigurationManager.AppSettings["YourSite"] + "/Images/dealfood/" + dtProduct.Rows[0]["restaurantId"].ToString().Trim() + "/" + strDealImages[0] + "'></a>";

                                    ltSlideShow.Text += "<div style='float:left; position:absolute; margin-left:20px; margin-top:267px; z-index:100;'><img src='Images/upArrowWhite.png' alt='' /></div>";
                                    ltSlideShow.Text += "<div style='float:left; clear:both; background-color:White; width:100%; padding-bottom:10px; padding-top:5px; box-shadow:0 1px 2px rgba(0, 0, 0, 0.2);border-radius:0 0 5px 5px;'>";
                                    ltSlideShow.Text += "<div style='width: 100%; clear: both; padding-left:10px; padding-top:5px;'>";
                                    ltSlideShow.Text += "<div style='float: left;'>";
                                    ltSlideShow.Text += "<img src='Images/clock-icon.png' />";
                                    ltSlideShow.Text += "</div>";
                                    ltSlideShow.Text += "<div style='float: left; color: #102343; font-size: 18px; padding-top: 4px; padding-left: 10px;'>Time Left:</div>";
                                    ltSlideShow.Text += "<div style='float: left; color: #102343; font-size: 18px; padding-top: 4px; padding-left: 10px;'>";
                                    ltSlideShow.Text += "<div id='defaultCountdown' align='center'></div></div></div></div>";
                                    ltSlideShow.Text += "</div></div>";

                                    ltSlideShow.Text += "<div style='float: right; position: relative;'><div style='position: absolute; right: -6px; top: -105px;z-index:500;'><img src='Images/expired.png' /></div></div>";
                                    ltSlideShow.Text += "</div>";
                                }
                                else
                                {
                                    // ltSlideShow.Text += "<ul id=\"myGallery\">";
                                    for (int i = 0; i < strDealImages.Length; i++)
                                    {
                                        if (i == 0)
                                        {
                                            Literal lit = (Literal)Master.FindControl("linkID");
                                            if (lit != null)
                                            {
                                                lit.Text = "<link rel='image_src' href='" + ConfigurationManager.AppSettings["YourSite"].ToString() + "/Images/dealfood/" + dtProduct.Rows[0]["restaurantId"].ToString().Trim() + "/" + strDealImages[i] + "' >";
                                            }
                                            //ltSlideShow.Text += "<div class='clearfix' style='margin-bottom:15px;'><a href='" + ConfigurationManager.AppSettings["YourSite"] + "/Images/dealfood/" + dtProduct.Rows[0]["restaurantId"].ToString().Trim() + "/Large/" + strDealImages[i] + "' class='jqzoom' rel='gal1'><img height='276px' width='360px' src='" + ConfigurationManager.AppSettings["YourSite"] + "/Images/dealfood/" + dtProduct.Rows[0]["restaurantId"].ToString().Trim() + "/" + strDealImages[i] + "'></a></div>";
                                            ltSlideShow.Text += "<div style='float:left; border-radius:5px; width:100%;'><div class='clearfix' style='margin-bottom:15px; width:100%;'><a href='" + ConfigurationManager.AppSettings["YourSite"] + "/Images/dealfood/" + dtProduct.Rows[0]["restaurantId"].ToString().Trim() + "/Large/" + strDealImages[i] + "' class='jqzoom' rel='gal1'><img height='276px' width='380px' src='" + ConfigurationManager.AppSettings["YourSite"] + "/Images/dealfood/" + dtProduct.Rows[0]["restaurantId"].ToString().Trim() + "/" + strDealImages[i] + "'></a>";
                                            ltSlideShow.Text += "<div style='float:left; position:absolute; margin-left:20px; margin-top:267px; z-index:100;'><img src='Images/upArrowWhite.png' alt='' /></div>";
                                            ltSlideShow.Text += "<div style='float:left; clear:both; background-color:White; width:100%; padding-bottom:10px; padding-top:5px; box-shadow:0 1px 2px rgba(0, 0, 0, 0.2);border-radius:0 0 5px 5px;'>";
                                            ltSlideShow.Text += "<div style='width: 100%; clear: both; padding-left:10px; padding-top:5px;'>";
                                            ltSlideShow.Text += "<div style='float: left;'>";
                                            ltSlideShow.Text += "<img src='Images/clock-icon.png' />";
                                            ltSlideShow.Text += "</div>";
                                            ltSlideShow.Text += "<div style='float: left; color: #102343; font-size: 18px; padding-top: 4px; padding-left: 10px;'>Time Left:</div>";
                                            ltSlideShow.Text += "<div style='float: left; color: #102343; font-size: 18px; padding-top: 4px; padding-left: 10px;'>";
                                            ltSlideShow.Text += "<div id='defaultCountdown' align='center'></div></div></div></div>";
                                            ltSlideShow.Text += "</div></div>";

                                            ltSlideShow.Text += "<div style='clear:both; float;left; padding:bottom:5px;'>More Images</div>";

                                            ltSlideShow.Text += "<div class='clearfix' ><ul id='thumblist' class='clearfix' ><li><a class='zoomThumbActive' href='javascript:void(0);' rel=\"{gallery: 'gal1', smallimage:'" + ConfigurationManager.AppSettings["YourSite"] + "/Images/dealfood/" + dtProduct.Rows[0]["restaurantId"].ToString().Trim() + "/" + strDealImages[i] + "',largeimage: '" + ConfigurationManager.AppSettings["YourSite"] + "/Images/dealfood/" + dtProduct.Rows[0]["restaurantId"].ToString().Trim() + "/Large/" + strDealImages[i] + "'}\"><img height='64px' width='88px' src='" + ConfigurationManager.AppSettings["YourSite"] + "/Images/dealfood/" + dtProduct.Rows[0]["restaurantId"].ToString().Trim() + "/thumb/" + strDealImages[i] + "'></a></li>";
                                            //ltSlideShow.Text += "<li><img data-frame='" + ConfigurationManager.AppSettings["YourSite"] + "/Images/dealfood/" + dtProduct.Rows[0]["restaurantId"].ToString().Trim() + "/thumb/" + strDealImages[i] + "' src='" + ConfigurationManager.AppSettings["YourSite"] + "/Images/dealfood/" + dtProduct.Rows[0]["restaurantId"].ToString().Trim() + "/" + strDealImages[i] + "'/></li>";
                                        }
                                        else if (strDealImages[i].ToString().Trim() != "")
                                        {
                                            // ltSlideShow.Text += "<li><img data-frame='" + ConfigurationManager.AppSettings["YourSite"] + "/Images/dealfood/" + dtProduct.Rows[0]["restaurantId"].ToString().Trim() + "/thumb/" + strDealImages[i] + "' src='" + ConfigurationManager.AppSettings["YourSite"] + "/Images/dealfood/" + dtProduct.Rows[0]["restaurantId"].ToString().Trim() + "/" + strDealImages[i] + "'/></li>";
                                            ltSlideShow.Text += "<li><a href='javascript:void(0);' rel=\"{gallery: 'gal1', smallimage:'" + ConfigurationManager.AppSettings["YourSite"] + "/Images/dealfood/" + dtProduct.Rows[0]["restaurantId"].ToString().Trim() + "/" + strDealImages[i] + "',largeimage:'" + ConfigurationManager.AppSettings["YourSite"] + "/Images/dealfood/" + dtProduct.Rows[0]["restaurantId"].ToString().Trim() + "/Large/" + strDealImages[i] + "'}\"><img height='64px' width='88px' src='" + ConfigurationManager.AppSettings["YourSite"] + "/Images/dealfood/" + dtProduct.Rows[0]["restaurantId"].ToString().Trim() + "/thumb/" + strDealImages[i] + "'></a></li>";
                                        }
                                    }
                                    ltSlideShow.Text += "</ul></div>";
                                    ltSlideShow.Text += "<div style='float: right; position: relative;'><div style='position: absolute; right: -6px; top: -200px; z-index:1000;'><img src='Images/expired.png' /></div></div>";
                                }
                            }
                        }
                        else
                        {
                            if (Convert.ToBoolean(dtProduct.Rows[0]["enableSize"].ToString().Trim()))
                            {
                                ltAddToCart.Text = "<div style='display:none;padding-top:13px;' id='Loader-1'><img src='images/Loader.gif' /></div><a id='Buy-1' onclick='javascript:AddToCartwithSize(" + dtProduct.Rows[0]["productID"].ToString() + ");' href='javascript:void(0);'><img src='Images/add-to-cart-red.png'/></a>";
                                //ltAddToCart2.Text = "<div style='display:none;padding-top:13px;' id='Loader-2'><img src='images/Loader.gif' /></div><a id='Buy-2' onclick='javascript:AddToCartwithSize(" + dtProduct.Rows[0]["productID"].ToString() + ");' href='javascript:void(0);'><img src='Images/add-to-cart-red.png'/></a>";
                            }
                            else
                            {
                                ltAddToCart.Text = "<div style='display:none;padding-top:13px;' id='Loader-1'><img src='images/Loader.gif' /></div><a id='Buy-1' onclick='javascript:AddToCart(" + dtProduct.Rows[0]["productID"].ToString() + ");' href='javascript:void(0);'><img src='Images/add-to-cart-red.png'/></a>";
                                //ltAddToCart2.Text = "<div style='display:none;padding-top:13px;' id='Loader-2'><img src='images/Loader.gif' /></div><a id='Buy-2' onclick='javascript:AddToCart(" + dtProduct.Rows[0]["productID"].ToString() + ");' href='javascript:void(0);'><img src='Images/add-to-cart-red.png'/></a>";
                            }
                            string[] strDealImages = dtProduct.Rows[0]["images"].ToString().Split(',');
                            if (strDealImages.Length > 0)
                            {
                                if ((strDealImages.Length == 1) || (strDealImages[1].Trim() == "" && strDealImages[2].Trim() == ""))
                                {
                                    ltSlideShow.Text = "<div style='clear: both;'>";
                                    Literal lit = (Literal)Master.FindControl("linkID");
                                    if (lit != null)
                                    {
                                        lit.Text = "<link rel='image_src' href='" + ConfigurationManager.AppSettings["YourSite"].ToString() + "/Images/dealfood/" + dtProduct.Rows[0]["restaurantId"].ToString().Trim() + "/" + strDealImages[0] + "' >";
                                    }
                                    ltSlideShow.Text += "<div style='float:left; border-radius:5px;'><div class='clearfix'><a href='" + ConfigurationManager.AppSettings["YourSite"] + "/Images/dealfood/" + dtProduct.Rows[0]["restaurantId"].ToString().Trim() + "/Large/" + strDealImages[0] + "' class='jqzoom' rel='gal1'><img height='276px' width='380px' src='" + ConfigurationManager.AppSettings["YourSite"] + "/Images/dealfood/" + dtProduct.Rows[0]["restaurantId"].ToString().Trim() + "/" + strDealImages[0] + "'></a>";//</div>";

                                    ltSlideShow.Text += "<div style='float:left; position:absolute; margin-left:20px; margin-top:267px; z-index:100;'><img src='Images/upArrowWhite.png' alt='' /></div>";
                                    ltSlideShow.Text += "<div style='float:left; clear:both; background-color:White; width:100%; padding-bottom:10px; padding-top:5px; box-shadow:0 1px 2px rgba(0, 0, 0, 0.2);border-radius:0 0 5px 5px;'>";
                                    ltSlideShow.Text += "<div style='width: 100%; clear: both; padding-left:10px; padding-top:5px;'>";
                                    ltSlideShow.Text += "<div style='float: left;'>";
                                    ltSlideShow.Text += "<img src='Images/clock-icon.png' />";
                                    ltSlideShow.Text += "</div>";
                                    ltSlideShow.Text += "<div style='float: left; color: #102343; font-size: 18px; padding-top: 4px; padding-left: 10px;'>Time Left:</div>";
                                    ltSlideShow.Text += "<div style='float: left; color: #102343; font-size: 18px; padding-top: 4px; padding-left: 10px;'>";
                                    ltSlideShow.Text += "<div id='defaultCountdown' align='center'></div></div></div></div>";

                                    ltSlideShow.Text += "</div>";
                                }
                                else
                                {
                                    for (int i = 0; i < strDealImages.Length; i++)
                                    {
                                        if (i == 0)
                                        {
                                            Literal lit = (Literal)Master.FindControl("linkID");
                                            if (lit != null)
                                            {
                                                lit.Text = "<link rel='image_src' href='" + ConfigurationManager.AppSettings["YourSite"].ToString() + "/Images/dealfood/" + dtProduct.Rows[0]["restaurantId"].ToString().Trim() + "/" + strDealImages[i] + "' >";
                                            }
                                            //ltSlideShow.Text += "<div class='clearfix' style='margin-bottom:15px;'><a href='" + ConfigurationManager.AppSettings["YourSite"] + "/Images/dealfood/" + dtProduct.Rows[0]["restaurantId"].ToString().Trim() + "/Large/" + strDealImages[i] + "' class='jqzoom' rel='gal1'><img height='276px' width='360px' src='" + ConfigurationManager.AppSettings["YourSite"] + "/Images/dealfood/" + dtProduct.Rows[0]["restaurantId"].ToString().Trim() + "/" + strDealImages[i] + "'></a></div>";
                                            ltSlideShow.Text += "<div style='float:left; border-radius:5px; width:100%;'><div class='clearfix' style='margin-bottom:15px; width:100%;'><a href='" + ConfigurationManager.AppSettings["YourSite"] + "/Images/dealfood/" + dtProduct.Rows[0]["restaurantId"].ToString().Trim() + "/Large/" + strDealImages[i] + "' class='jqzoom' rel='gal1'><img height='276px' width='380px' src='" + ConfigurationManager.AppSettings["YourSite"] + "/Images/dealfood/" + dtProduct.Rows[0]["restaurantId"].ToString().Trim() + "/" + strDealImages[i] + "'></a>";
                                            ltSlideShow.Text += "<div style='float:left; position:absolute; margin-left:20px; margin-top:267px; z-index:100;'><img src='Images/upArrowWhite.png' alt='' /></div>";
                                            ltSlideShow.Text += "<div style='float:left; clear:both; background-color:White; width:100%; padding-bottom:10px; padding-top:5px; box-shadow:0 1px 2px rgba(0, 0, 0, 0.2);border-radius:0 0 5px 5px;'>";
                                            ltSlideShow.Text += "<div style='width: 100%; clear: both; padding-left:10px; padding-top:5px;'>";
                                            ltSlideShow.Text += "<div style='float: left;'>";
                                            ltSlideShow.Text += "<img src='Images/clock-icon.png' />";
                                            ltSlideShow.Text += "</div>";
                                            ltSlideShow.Text += "<div style='float: left; color: #102343; font-size: 18px; padding-top: 4px; padding-left: 10px;'>Time Left:</div>";
                                            ltSlideShow.Text += "<div style='float: left; color: #102343; font-size: 18px; padding-top: 4px; padding-left: 10px;'>";
                                            ltSlideShow.Text += "<div id='defaultCountdown' align='center'></div></div></div></div>";
                                            ltSlideShow.Text += "</div></div>";

                                            ltSlideShow.Text += "<div style='clear:both; float;left; padding:bottom:5px;'>More Images</div>";

                                            ltSlideShow.Text += "<div class='clearfix' ><ul id='thumblist' class='clearfix' ><li><a class='zoomThumbActive' href='javascript:void(0);' rel=\"{gallery: 'gal1', smallimage:'" + ConfigurationManager.AppSettings["YourSite"] + "/Images/dealfood/" + dtProduct.Rows[0]["restaurantId"].ToString().Trim() + "/" + strDealImages[i] + "',largeimage: '" + ConfigurationManager.AppSettings["YourSite"] + "/Images/dealfood/" + dtProduct.Rows[0]["restaurantId"].ToString().Trim() + "/Large/" + strDealImages[i] + "'}\"><img height='64px' width='88px' src='" + ConfigurationManager.AppSettings["YourSite"] + "/Images/dealfood/" + dtProduct.Rows[0]["restaurantId"].ToString().Trim() + "/thumb/" + strDealImages[i] + "'></a></li>";
                                        }
                                        else if (strDealImages[i].ToString().Trim() != "")
                                        {
                                            ltSlideShow.Text += "<li><a href='javascript:void(0);' rel=\"{gallery: 'gal1', smallimage:'" + ConfigurationManager.AppSettings["YourSite"] + "/Images/dealfood/" + dtProduct.Rows[0]["restaurantId"].ToString().Trim() + "/" + strDealImages[i] + "',largeimage:'" + ConfigurationManager.AppSettings["YourSite"] + "/Images/dealfood/" + dtProduct.Rows[0]["restaurantId"].ToString().Trim() + "/Large/" + strDealImages[i] + "'}\"><img height='64px' width='88px' src='" + ConfigurationManager.AppSettings["YourSite"] + "/Images/dealfood/" + dtProduct.Rows[0]["restaurantId"].ToString().Trim() + "/thumb/" + strDealImages[i] + "'></a></li>";
                                        }
                                    }
                                    ltSlideShow.Text += "</ul></div>";
                                }
                            }
                        }
                        DateTime dtEndTime = Convert.ToDateTime(dtProduct.Rows[0]["campaignEndTime"].ToString().Trim());
                        if (dealStatus != 0)
                        {
                            dtEndTime = Convert.ToDateTime(dtProduct.Rows[0]["campaignStartTime"].ToString().Trim());
                        }

                        ltCountDown.Text += "<script type='text/javascript'>";
                        ltCountDown.Text += "function serverTime() { ";
                        ltCountDown.Text += "var time = null; ";
                        ltCountDown.Text += "$.ajax({url: '" + System.Configuration.ConfigurationManager.AppSettings["YourSite"].ToString() + "/getStateLocalTime.aspx?sid=0', ";
                        ltCountDown.Text += "async: false, dataType: 'text', ";
                        ltCountDown.Text += "success: function(text) { ";
                        ltCountDown.Text += "time = new Date(text); ";
                        ltCountDown.Text += "}, error: function(http, message, exc) { ";
                        ltCountDown.Text += "time = new Date(); ";
                        ltCountDown.Text += "}}); ";
                        ltCountDown.Text += "return time; ";
                        ltCountDown.Text += "}";


                        ltCountDown.Text += "$(function () {";
                        ltCountDown.Text += "var austDay = new Date();";
                        ltCountDown.Text += "austDay = new Date(" + dtEndTime.Year + "," + (dtEndTime.Month - 1) + "," + dtEndTime.Day + "," + dtEndTime.Hour + "," + dtEndTime.Minute + ",0);";
                        ltCountDown.Text += "$('#defaultCountdown').countdown({until: austDay,compact: true, serverSync: serverTime});";
                        ltCountDown.Text += "$('#year').text(austDay.getFullYear());";
                        ltCountDown.Text += "});</script>";

                        lblProductPrice.Text = "$" + Convert.ToDecimal(dtProduct.Rows[0]["sellingPrice"].ToString()).ToString("0.###").Trim();
                        lblProdcutActualPrice.Text = "$" + Convert.ToDecimal(dtProduct.Rows[0]["valuePrice"].ToString().Trim()).ToString("0.###") + " retail price";
                        //lblProductPrice2.Text = "$" + Convert.ToDecimal(dtProduct.Rows[0]["sellingPrice"].ToString()).ToString("0.###").Trim();
                        //lblProdcutActualPrice2.Text = "$" + Convert.ToDecimal(dtProduct.Rows[0]["valuePrice"].ToString()).ToString("0.###").Trim() + " retail price";
                        lblDescription.Text = dtProduct.Rows[0]["description"].ToString().Trim();
                        lblShortDescription.Text = dtProduct.Rows[0]["shortDescription"].ToString().Trim();
                        objProductProperties.productID = Convert.ToInt64(dtProduct.Rows[0]["productID"].ToString().Trim());
                        DataTable dtProductProperties = objProductProperties.getProductPropertiesByProductID();
                        if (dtProductProperties != null && dtProductProperties.Rows.Count > 0)
                        {
                            for (int i = 0; i < dtProductProperties.Rows.Count; i++)
                            {
                                if (i % 2 == 0)
                                {
                                    ltProductProperties.Text += @"<div style='clear: both; width: 100%; height: 30px;'>
                                                        <div style='width: 120px; padding-left: 10px; float: left; text-align: left; color: #919aa8;
                                                        padding-top: 7px;'>";
                                    ltProductProperties.Text += dtProductProperties.Rows[i]["propertiesLabel"].ToString().Trim();
                                    ltProductProperties.Text += "</div><div style='padding-left: 20px; float: left; text-align: left; color: #5e636c; padding-top: 7px;'>";
                                    ltProductProperties.Text += dtProductProperties.Rows[i]["propertiesDescription"].ToString().Trim() + "</div></div>";
                                }
                                else
                                {
                                    ltProductProperties.Text += @"<div style='clear: both; width: 100%; height: 30px; background-color: white;'>
                                                        <div style='width: 120px; padding-left: 10px; float: left; text-align: left; color: #919aa8;
                                                        padding-top: 7px;'>";
                                    ltProductProperties.Text += dtProductProperties.Rows[i]["propertiesLabel"].ToString().Trim();
                                    ltProductProperties.Text += "</div><div style='padding-left: 20px; float: left; text-align: left; color: #5e636c; padding-top: 7px;'>";
                                    ltProductProperties.Text += dtProductProperties.Rows[i]["propertiesDescription"].ToString().Trim() + "</div></div>";
                                }
                            }
                        }

                        if (Convert.ToBoolean(dtProduct.Rows[0]["enableSize"].ToString().Trim()))
                        {
                            pnlEnableSize.Visible = true;
                            //pnlEnableSize2.Visible = true;
                            BindSize(objProductProperties.productID.ToString());
                            //BindSizeDropDown(objProductProperties.productID);
                        }
                        else
                        {
                            pnlEnableSize.Visible = false;
                            //pnlEnableSize2.Visible = false;
                            BindQuantityDropDown(Convert.ToInt32(dtProduct.Rows[0]["minQty"].ToString()), Convert.ToInt32(dtProduct.Rows[0]["maxQty"].ToString()));
                        }
                        if (Convert.ToBoolean(dtProduct.Rows[0]["shipCanada"].ToString().Trim())
                            && Convert.ToBoolean(dtProduct.Rows[0]["shipUSA"].ToString().Trim()))
                        {
                            lblShipUSToCanada.Text = "Ship To US and Canada Only";
                        }
                        else if (Convert.ToBoolean(dtProduct.Rows[0]["shipCanada"].ToString().Trim()))
                        {
                            lblShipUSToCanada.Text = "Ship To Canada Only";
                        }
                        else if (Convert.ToBoolean(dtProduct.Rows[0]["shipUSA"].ToString().Trim()))
                        {
                            lblShipUSToCanada.Text = "Ship To US Only";
                        }

                    }
                }

            }
            catch (Exception ex)
            {
            }
        }
    }

    protected void BindSize(string strProductID)
    {
        BLLProductSize objSize = new BLLProductSize();
        objSize.productID = Convert.ToInt64(strProductID);
        DataTable dtSize = objSize.getProductSizeByProductID();
        ltQty.Text = "<select id='series' class='detailDropDown' onchange='javascript:changeQty();'>";
        //ltQty2.Text = "<select id='series2' class='detailDropDown' onchange='javascript:changeQty2();'>";
        if (dtSize != null && dtSize.Rows.Count > 0)
        {
            ltSize.Text = "<select id='mark' class='detailDropDown' onchange='javascript:changeSize();'>";
            //ltSize2.Text = "<select id='mark2' class='detailDropDown' onchange='javascript:changeSize2();'>";
            for (int i = 0; i < dtSize.Rows.Count; i++)
            {
                ltSize.Text += "<option value='" + dtSize.Rows[i]["sizeText"].ToString().Trim() + "'>" + dtSize.Rows[i]["sizeText"].ToString().Trim() + "</option>";
                //ltSize2.Text += "<option value='" + dtSize.Rows[i]["sizeText"].ToString().Trim() + "'>" + dtSize.Rows[i]["sizeText"].ToString().Trim() + "</option>";
                for (int a = 1; a <= Convert.ToInt32(dtSize.Rows[i]["quantity"].ToString().Trim()); a++)
                {
                    ltQty.Text += "<option value='" + a.ToString() + "' class='" + dtSize.Rows[i]["sizeText"].ToString().Trim() + "'>" + a.ToString() + "</option>";
                    //ltQty2.Text += "<option value='" + a.ToString() + "' class='" + dtSize.Rows[i]["sizeText"].ToString().Trim() + "'>" + a.ToString() + "</option>";
                }
            }
            ltQty.Text += "</select>";
            ltSize.Text += "</select>";

            //ltQty2.Text += "</select>";
            //ltSize2.Text += "</select>";
            //strHTML += strOptionHTML;
            /* ltQty2.Text += "<script type='text/javascript' charset='utf-8'>";
             ltQty2.Text += "$(function () {$(\"#series" + "\").chained(\"#mark" + "\"); });</script>";

             ltQty2.Text += "<script type='text/javascript' charset='utf-8'>";
             ltQty2.Text += "$(function () {$(\"#series2" + "\").chained(\"#mark2" + "\");});</script>";*/
        }
    }

    protected void BindQuantityDropDown(int min, int Qty)
    {
        try
        {
            //Clears the Drop Down List
            ltQty.Text = "<select id='series' class='detailDropDown' onchange='javascript:changeQty();'>";
            //ltQty2.Text = "<select id='series2' class='detailDropDown' onchange='javascript:changeQty2();'>";
            for (int i = min; i <= Qty; i++)
            {
                ltQty.Text += "<option value='" + i.ToString() + "'>" + i.ToString() + "</option>";
                //ltQty2.Text += "<option value='" + i.ToString() + "'>" + i.ToString() + "</option>";
            }
            ltQty.Text += "</select>";
            //ltQty2.Text += "</select>";
        }
        catch (Exception ex)
        {

        }
    }
}