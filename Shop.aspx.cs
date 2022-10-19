using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;

public partial class MyShop : System.Web.UI.Page
{
    BLLCampaign objCampaign = new BLLCampaign();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (ViewState["title"] != null)
        {
            Page.Title = ViewState["title"].ToString();
        }
        if (!IsPostBack)
        {

            try
            {
                DataTable dtUser = null;
                if (Session["member"] != null || Session["restaurant"] != null || Session["sale"] != null || Session["user"] != null)
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
                }
                if (Request.QueryString["cid"] != null && Request.QueryString["cid"].Trim() != "")
                {
                    DataTable dtCampaign = null;

                    objCampaign.creationDate = DateTime.Now;
                    objCampaign.campaignID = Convert.ToInt64(Request.QueryString["cid"].ToString().Trim());
                    dtCampaign = objCampaign.getCurrentCampaignByGivenDateAndCampaignID();
                    if (dtCampaign != null && dtCampaign.Rows.Count > 0)
                    {
                        Page.Title = "Tazzling.com | " + dtCampaign.Rows[0]["campaignTitle"].ToString().Trim();
                        ViewState["title"] = "Tazzling.com | " + dtCampaign.Rows[0]["campaignTitle"].ToString().Trim();
                        TimeSpan ts = Convert.ToDateTime(dtCampaign.Rows[0]["campaignEndTime"].ToString().Trim()).Subtract(objCampaign.creationDate);
                        lblSalesTimeTop.Text = "Sale Ends in<span style='color:#DD0017;'> " + ts.Days + " days </span>and<span style='color:#DD0017;'> " + ts.Hours + " hours </span>";
                        imgSalesMainImage.ImageUrl = ConfigurationManager.AppSettings["YourSite"].ToString() + "/Images/dealfood/" + dtCampaign.Rows[0]["restaurantId"].ToString().Trim() + "/" + dtCampaign.Rows[0]["campaignpicture"].ToString().Trim();
                        lblTitle.Text = dtCampaign.Rows[0]["campaignTitle"].ToString().Trim();
                        lblShortDescription.Text = dtCampaign.Rows[0]["campaignShortDescription"].ToString().Trim();
                        lblDescription.Text = dtCampaign.Rows[0]["campaignDescription"].ToString().Trim();
                        lblMerchantName.Text = dtCampaign.Rows[0]["firstName"].ToString().Trim() + " " + dtCampaign.Rows[0]["lastName"].ToString().Trim();
                        imgMechantImage.ImageUrl = ConfigurationManager.AppSettings["YourSite"].ToString() + "/Images/RestaurantImages/" + dtCampaign.Rows[0]["restaurantlogo"].ToString().Trim();
                        lblMerchantCote.Text = dtCampaign.Rows[0]["campaignQuote"].ToString().Trim();
                        lblMerchantSign.Text = dtCampaign.Rows[0]["ownerSignature"].ToString().Trim();
                        BLLProducts objProducts = new BLLProducts();
                        objProducts.campaignID = objCampaign.campaignID;
                        if (dtUser != null && dtUser.Rows.Count > 0)
                        {
                            objProducts.createdBy = Convert.ToInt64(dtUser.Rows[0]["userId"].ToString().Trim());
                        }
                        else
                        {
                            objProducts.createdBy = 0;
                        }
                        DataTable dtProducts = objProducts.getProductsByCampaignIDForClientSide();
                        if (dtProducts != null && dtProducts.Rows.Count > 0)
                        {
                            ltCampaingsProducts.Text = "<div id='content'>";
                            for (int i = 0; i < dtProducts.Rows.Count; i++)
                            {
                                if (i < 3)
                                {
                                    if (i % 2 == 0)
                                    {
                                        ltCampaingsProducts.Text += "<div style='float: left;'>";
                                    }
                                    else
                                    {
                                        ltCampaingsProducts.Text += "<div style='float: left; margin-right: 15px; margin-left: 15px;'>";
                                    }
                                }
                                else
                                {
                                    if ((i / 3) % 2 == 0)
                                    {
                                        if (i % 2 == 0)
                                        {
                                            ltCampaingsProducts.Text += "<div style='float: left;'>";
                                        }
                                        else
                                        {
                                            ltCampaingsProducts.Text += "<div style='float: left; margin-right: 15px; margin-left: 15px;'>";
                                        }
                                    }
                                    else
                                    {
                                        if (i % 2 == 0)
                                        {
                                            ltCampaingsProducts.Text += "<div style='float: left; margin-right: 15px; margin-left: 15px;'>";
                                        }
                                        else
                                        {
                                            ltCampaingsProducts.Text += "<div style='float: left;'>";
                                        }

                                    }
                                }
                                int intDiscount = 0;
                                int.TryParse(Convert.ToInt32(Convert.ToDouble(Convert.ToDouble(100 / Convert.ToDouble(dtProducts.Rows[i]["valuePrice"].ToString())) * (Convert.ToDouble((dtProducts.Rows[i]["valuePrice"].ToString())) - Convert.ToDouble(dtProducts.Rows[i]["sellingPrice"].ToString())))).ToString(), out intDiscount);
                                ltCampaingsProducts.Text += "<div style='overflow:hidden; float:left; margin:0px; position:relative; margin-top:40px !important; border-radius:0px 0px 5px 5px; width:316px; box-shadow:0 1px 2px rgba(0, 0, 0, 0.2);'><div class='mosaic-block3 bar5'><a href='javascript:void(0);'  class='mosaic-overlay'><div class='details2'>";
                                ltCampaingsProducts.Text += "<div class='ShopDiscountBanner'>-" + intDiscount.ToString() + "%</div></div><div id='fav-" + i.ToString() + "' class='FavouriteText'>" + Convert.ToString(dtProducts.Rows[i]["TotalFavorites"].ToString()).ToString() + "</div>";
                                if (dtProducts.Rows[i]["MyFavorites"].ToString().Trim() == "0" && dtUser != null)
                                {
                                    ltCampaingsProducts.Text += "<div class='FavouriteDeal' onclick='javascript:AddToMyFavourite(\"" + i.ToString() + "|" + dtProducts.Rows[i]["productID"].ToString() + "\");'></div>";
                                }
                                else
                                {
                                    ltCampaingsProducts.Text += "<div class='FavouriteDealNonClickable' onclick='javascript:void(0);'></div>";
                                }

                                ltCampaingsProducts.Text += " <div style='float: right; padding-right: 15px; padding-left: 5px; padding-top: 4px; width: 70px;'><fb:like href='" + ConfigurationManager.AppSettings["YourSite"].ToString() + "detail.aspx?pid=" + dtProducts.Rows[i]["productID"].ToString() + "' data-send='false' data-layout='button_count' data-width='70'data-show-faces='false' data-font='arial'></fb:like></div></a>";

                                ltCampaingsProducts.Text += "<a href='detail.aspx?pid=" + dtProducts.Rows[i]["productID"].ToString() + "'  class='mosaic-backdrop'><img src='" + ConfigurationManager.AppSettings["YourSite"].ToString() + "/Images/dealfood/" + dtCampaign.Rows[0]["restaurantId"].ToString().Trim() + "/" + dtProducts.Rows[i]["image1"].ToString().Trim() + "' style='width:316px; height:230px; border-radius:5px 5px 0px 0px;' /></a></div>";
                                ltCampaingsProducts.Text += "<div style='float:left; position:absolute; top:226px; left:20px;'><img src='images/upArrow.png' alt='' /></div><div style='clear: both; width: 316px; height: 30px; background-color: #313131; color: White;font-size: 16px;'><div style='float: left; padding-top: 5px; padding-left: 10px;'>";
                                ltCampaingsProducts.Text += dtProducts.Rows[i]["title"].ToString().Trim() + "</div></div>";
                                ltCampaingsProducts.Text += "<div style='height:55px; background-color:white; box-shadow:0 1px 2px rgba(0, 0, 0, 0.2); border-radius:0px 0px 5px 5px;'><div id='divBuy-" + dtProducts.Rows[i]["productID"].ToString().Trim() + "' style='clear: both; width: 316px; height: 50px; border-bottom: 1px solid #FF42E7;'><div style='clear: both;'><div style='float: left; width: 200px;'><div style='clear: both; font-size: 18px; color: #102343; font-weight: bold; padding-top: 7px; padding-left:10px;'><div style='float: left; font-weight: bold; font-size: 22px; color:#434343; '>$";
                                ltCampaingsProducts.Text += Convert.ToDecimal(dtProducts.Rows[i]["sellingPrice"].ToString().Trim()).ToString("0.###") + "</div><div style='float: left; padding-top: 3px; padding-left: 2px; padding-right: 2px;'><img src='Images/fav-hear-hov.png' /></div><div style='float: left; color:#434343; padding-left:5px;'>tazzling</div></div><div style='clear: both; color: #686868; font-size: 16px; text-decoration: line-through;padding-top: 5px; padding-left:10px;'>$";
                                ltCampaingsProducts.Text += Convert.ToDecimal(dtProducts.Rows[i]["valuePrice"].ToString().Trim()).ToString("0.###") + " retail price</div></div>";
                                if (dtProducts.Rows[i]["maxOrdersPerUser"].ToString().Trim() != null
                                    && dtProducts.Rows[i]["maxOrdersPerUser"].ToString().Trim() != "0"
                                    && Convert.ToInt32(dtProducts.Rows[i]["SuccessfulOrders"].ToString()) >= Convert.ToInt32(dtProducts.Rows[i]["maxOrdersPerUser"].ToString().Trim()))
                                {
                                    ltCampaingsProducts.Text += "<div style='float: right; position: relative;'><div style='position: absolute; right: -13px; top: -90px;'><img src='Images/sold-out.png' /></div></div></div></div>";
                                }
                                else
                                {
                                    if (Convert.ToBoolean(dtProducts.Rows[i]["enableSize"].ToString().Trim()))
                                    {
                                        ltCampaingsProducts.Text += "<div style='overflow:hidden;'><div style='float:right; padding:15px 10px 0px 15px;''><img src='Images/BuyArrow.png' /></div><div style='float: right; padding-top: 10px;'><a onclick='javascript:changeDivs(" + dtProducts.Rows[i]["productID"].ToString() + ");' href='javascript:void(0);'><img src='Images/Buy_id.png' /></a></div></div></div></div>";
                                        ltCampaingsProducts.Text += BindSize(dtProducts.Rows[i]["productID"].ToString().Trim());
                                    }
                                    else
                                    {
                                        ltCampaingsProducts.Text += "<div style='overflow:hidden;'><div style='float:right; padding:15px 20px 0px 15px;'><img src='Images/BuyArrow.png' /></div><div style='float: left; margin-top: 10px;'><div style='display:none;padding-top:13px; position:absolute; padding-right:15px;' id='Loader-" + dtProducts.Rows[i]["productID"].ToString() + "'><img src='images/Loader.gif' /></div><a id='Buy-" + dtProducts.Rows[i]["productID"].ToString() + "' onclick='javascript:AddToCart(" + dtProducts.Rows[i]["productID"].ToString() + ");' href='javascript:void(0);'><img src='Images/Buy_id.png' /></a></div></div></div></div>";
                                    }
                                }

                                ltCampaingsProducts.Text += "</div></div></div>";
                            }

                            ltCampaingsProducts.Text += "</div'>";
                        }
                        else
                        {
                            Response.Redirect("default.aspx", true);
                        }
                    }
                    else
                    {
                        Response.Redirect("default.aspx", true);
                    }
                }
                else
                {
                    Response.Redirect("default.aspx", true);
                }
            }
            catch (Exception ex)
            { }

        }
    }

    protected string BindSize(string strProductID)
    {
        BLLProductSize objSize = new BLLProductSize();
        objSize.productID = Convert.ToInt64(strProductID);
        DataTable dtSize = objSize.getProductSizeByProductID();

        string strSizeHTML = "<div id='sizeDiv-" + strProductID + "' style='clear: both; width: 316px; height: 50px;display:none;'>";
        strSizeHTML += "<div style='clear:both;padding-top:11px; padding-left:15px;'>";
        string strHTML = "";
        string strOptionHTML = "<select id='series" + strProductID + "' class='detailDropDown'>";
        if (dtSize != null && dtSize.Rows.Count > 0)
        {
            strHTML += "<select id='mark" + strProductID + "' class='detailDropDown'>";
            for (int i = 0; i < dtSize.Rows.Count; i++)
            {
                strHTML += "<option value='" + dtSize.Rows[i]["sizeText"].ToString().Trim() + "'>" + dtSize.Rows[i]["sizeText"].ToString().Trim() + "</option>";
                for (int a = 1; a <= Convert.ToInt32(dtSize.Rows[i]["quantity"].ToString().Trim()); a++)
                {
                    strOptionHTML += "<option value='" + a.ToString() + "' class='" + dtSize.Rows[i]["sizeText"].ToString().Trim() + "'>" + a.ToString() + "</option>";
                }
            }
            strOptionHTML += "</select>";
            strHTML += "</select>";
            //strHTML += strOptionHTML;
            strHTML += "<script type='text/javascript' charset='utf-8'>";
            strHTML += "$(function () {$(\"#series" + strProductID + "\").chained(\"#mark" + strProductID + "\");});</script>";
        }
        strSizeHTML += "<div style='float:left;'>" + strHTML + "</div>";
        strSizeHTML += "<div style='float:left; padding-left:10px;'>" + strOptionHTML + "</div>";
        strSizeHTML += "<div style='float: right; padding-right:5px;'><div style='display:none;padding-top:13px;' id='Loader-" + strProductID + "'><img src='images/Loader.gif' /></div><a id='Buy-" + strProductID + "' onclick='javascript:AddToCartwithSize(" + strProductID + ");' href='javascript:void(0);'><img src='Images/add-to-cart-red.png' /></a></div>";
        strSizeHTML += "</div></div>";
        return strSizeHTML;
    }
}