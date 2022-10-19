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

public partial class UserDashBoard : System.Web.UI.Page
{
    public string TotalAmmount="";
    public string TotalSoled = "";
    public string UsedQuantity = "";
    public string UsedQuantityInPers = "";
    public string TotalQuantaty = "";
    public string TotalComment = "";
    public string TotalUnliked = "";
    public string Redeeme = "";
    public string ToGoOrder = "";
    public string FullName = "";
    public string TotalLiked = "";
    public string TotalCommentInPers = "";
    public string TotalOrders = "";
    public string UserBussiness = "";
    public string TotalLikedInPer = "";

  


    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["restaurant"] != null)
            {
                DataTable dtUser = null;
                if (Session["restaurant"] != null)
                {
                    string firstname = "";
                    string lastname = "";
                    dtUser = (DataTable)Session["restaurant"];
                    if (dtUser != null && dtUser.Rows.Count > 0)
                    {
                        firstname = (dtUser.Rows[0]["firstname"].ToString().Trim());
                        lastname = (dtUser.Rows[0]["lastname"].ToString().Trim());
                        FullName = firstname + " " + lastname;
                    }
                }
                if (dtUser != null && dtUser.Rows.Count > 0)
                {
                    BLLRestaurant ObjRestaurants = new BLLRestaurant();
                    BLLRestaurantComents ObjREsComments = new BLLRestaurantComents();
                    ObjREsComments.userID = Convert.ToInt32(dtUser.Rows[0]["userid"].ToString());
                    ObjRestaurants.userID = Convert.ToInt32(dtUser.Rows[0]["userid"].ToString());
                    DataTable dt = ObjREsComments.GetUserValue();

                    if (dt != null && dt.Rows.Count > 0)
                    {

                        long Total = 0;
                        long SoldTotal = 0;
                        long UsedOrder = 0;
                        long totalQty = 0;
                        long redeeme = 0;
                        long ToGo = 0;
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {

                            Total += Convert.ToInt64(dt.Rows[i]["sellingPrice"]) * Convert.ToInt64(dt.Rows[i]["AllOrder"]);
                            SoldTotal += Convert.ToInt64(dt.Rows[i]["AllOrder"]);
                            redeeme += Convert.ToInt64(dt.Rows[i]["UsedCount"]);
                            UsedOrder += Convert.ToInt64(dt.Rows[i]["PendingOrder"]) + Convert.ToInt64(dt.Rows[i]["UsedCount"]);
                            totalQty += Convert.ToInt64(dt.Rows[i]["AllOrder"]);
                            ToGo += Convert.ToInt64(dt.Rows[i]["AllOrder"]) - Convert.ToInt64(dt.Rows[i]["UsedCount"]);


                        }
                        TotalOrders = totalQty.ToString();
                        TotalAmmount = Total.ToString();
                        TotalSoled = SoldTotal.ToString();
                        UsedQuantity = UsedOrder.ToString();
                        if (UsedOrder.ToString().Trim() == "0" && SoldTotal.ToString().Trim() == "0" )
                        { 

                            UsedQuantityInPers = "0";
                            
                        }
                        else
                        {
                            float d = float.Parse(UsedOrder.ToString()) / float.Parse(SoldTotal.ToString());
                            UsedQuantityInPers = Convert.ToInt32(d * 100).ToString();
                           
                        }
                        TotalQuantaty = "100";
                        Redeeme = redeeme.ToString();
                        ToGoOrder = ToGo.ToString();
                    }

                    else
                    {
                        //no data.......

                        TotalOrders = "0";
                        TotalAmmount = "0";
                        TotalSoled = "0";
                        UsedQuantity = "0";
                        UsedQuantityInPers = "0";
                        Redeeme = "0";
                        ToGoOrder = "0";




                    }
                    DataTable dtusercomments = ObjREsComments.GetRestaurantComments();
                    if (dtusercomments != null && dtusercomments.Rows.Count > 0)
                    {
                        long totalcmnt = 0;
                        long unliked = 0;
                        bool check = true;
                        string comments = "";
                        string date = "";
                        string Description = "";
                        string HTML = "";
                        string javascript = "";
                        long liked = 0;
                        long nocomments = 0;
                       
                        for (int i = 0; i < dtusercomments.Rows.Count; i++)
                        {
                            string check2 = Convert.ToString(dtusercomments.Rows[i]["feedback"]);
                            if (Convert.ToString(dtusercomments.Rows[i]["feedback"]) != null && Convert.ToString(dtusercomments.Rows[i]["feedback"]) != "" && Convert.ToString(dtusercomments.Rows[i]["userComment"]) != null && Convert.ToString(dtusercomments.Rows[i]["userComment"]) != "")
                            {
                                totalcmnt ++;
                               

                                if (check2.ToString().Trim() == "Yes")
                                {
                                    liked++;
                                }
                                else
                                {
                                    unliked++;

                                }
                            }
                            else if (Convert.ToString(dtusercomments.Rows[i]["feedback"]) == null || Convert.ToString(dtusercomments.Rows[i]["feedback"]) == "" || Convert.ToString(dtusercomments.Rows[i]["userComment"]) == null || Convert.ToString(dtusercomments.Rows[i]["userComment"]) == "")
                            {
                                nocomments++;

                            }



                            if (Convert.ToString(dtusercomments.Rows[i]["feedback"]) != null && Convert.ToString(dtusercomments.Rows[i]["feedback"]) != "" && Convert.ToString(dtusercomments.Rows[i]["userComment"]) != null && Convert.ToString(dtusercomments.Rows[i]["userComment"]) != "")
                            {
                                comments += Convert.ToString(dtusercomments.Rows[i]["userComment"]).Trim();
                                date += Convert.ToString(dtusercomments.Rows[i]["commentDate"]);

                                Description += "<div style='width:290px; clear:both; height:auto; margin-top:10px;  margin-bottom:10px; bottom:10px; overflow:hidden;'>";
                                Description += "<div style='width:290px; min-height:130px;'>";
                                Description += "<div style='float: left;'>";
                                if (check2.ToString().Trim() == "Yes")
                                {
                                    Description += " <img src='Images/Smile.png' />";
                                }
                                else
                                {
                                    Description += " <img src='Images/Sad.png' />";
                                }

                                Description += "</div>";
                                Description += " <div style='  clear: both;height: 10px; margin-left: 38px;margin-top: -22px;text-decoration: none;width: 10px;float:left; '>";
                                Description += " <img src='Images/Pointing.png' />";
                                Description += "</div>";
                                Description += "<div style='float: left;padding: 5px 10px;margin-left: 55px;margin-top: -30px; width:215px; text-align: left; color:Black; font-family:Helvetica; font-size:14px; background-color:#ebebeb'>";
                                Description += "<div style='text-align:left; width:215px; min-height:75px; height:auto; overflow:hidden;'>";
                                Description += comments;
                                Description += "</div>";
                                Description += "<div style='text-align:right;width:215px; font-size:10px;  font-family: sans-serif; bottom:0; margin-bottom:0;  padding-right:3px;'>";
                                Description += date;
                                Description += "</div>";
                                Description += "</div>";
                                Description += "</div>";
                                Description += "</div>";

                                HTML += Description;
                                comments = "";
                                Description = "";
                                date = "";
                            }

                        }

                        TotalComment = totalcmnt.ToString();
                        TotalUnliked = unliked.ToString();
                        TotalLiked = liked.ToString().Trim();
                        //string tempcoments = Convert.ToString(Convert.ToInt32(TotalUnliked) + Convert.ToInt32(TotalLiked));
                        //float cmnt = float.Parse(liked.ToString()) / float.Parse(tempcoments.ToString());
                        //TotalCommentInPers = Convert.ToInt32(cmnt * 100).ToString();
                        string noComments = nocomments.ToString();


                        //float LikedComments = float.Parse(liked.ToString().Trim()) / float.Parse(totalcmnt.ToString().Trim());
                        //TotalLikedInPer = Convert.ToInt32(LikedComments * 100).ToString();

                        javascript = "<script>function pageLoad(){";
                        javascript += "jQuery.jqplot('chart3',";
                        javascript += "[[['Total Unliked', " + TotalUnliked + "],['Total Liked', " + TotalLiked + "], ['No Comment', " + noComments + "]]], ";
                        javascript += "{";
                        javascript += " title: ' ',";
                        javascript += " seriesDefaults: {";
                        javascript += "shadow: false,";
                        javascript += " renderer: jQuery.jqplot.PieRenderer,";
                        javascript += "rendererOptions: {";
                        javascript += " sliceMargin: 4,";
                        javascript += " showDataLabels: true";
                        javascript += "}},";
                        javascript += " legend:{ ";
                        javascript += "  show: true, location: 'e' ";
                        javascript += "}} );";
                        javascript += "}</script>";


                        ltlcomments.Text = HTML.ToString() + javascript;
                    }


                    else
                    {

                        string coment = "";
                        string date = "";
                        string Description = "";
                        string HTML = "";
                        string javascript = "";



                        date = (Convert.ToDateTime(DateTime.Now)).ToString();

                        HTML = Description;
                        javascript = "<script>function pageLoad(){";

                        javascript += "jQuery.jqplot('chart3',";
                        javascript += "  [[['No Comment', " + 100 + "]]],";
                        javascript += "{";
                        javascript += " title: ' ',";
                        javascript += " seriesDefaults: {";
                        javascript += "shadow: false,";
                        javascript += " renderer: jQuery.jqplot.PieRenderer,";
                        javascript += "rendererOptions: {";
                        javascript += " sliceMargin: 4,";
                        javascript += " showDataLabels: true";
                        javascript += "}},";
                        javascript += " legend:{ ";
                        javascript += "  show: true, location: 'e' ";
                        javascript += "}} );";
                        javascript += "}</script>";


                        ltlcomments.Text = HTML.ToString() + javascript;

                        //else part if no data.....
                    }

                    lblCommentBelow.Text = TotalLikedInPer + " % customers liked your business.";



                    DataTable dtUserBusseness = ObjRestaurants.getBussinessByUserId();
                    if (dtUserBusseness != null && dtUserBusseness.Rows.Count > 0)
                    {
                        for (int i = 0; i < dtUserBusseness.Rows.Count; i++)
                        {
                            ltlBussinessNames.Text += " <style type='text/css'>";
                            ltlBussinessNames.Text += " .hyperlinkstyle{";
                            ltlBussinessNames.Text += " color:Black; font-family:Helvetica; font-size:14px; font-weight:bold; text-decoration:none;}";
                            ltlBussinessNames.Text += "  .hyperlinkstyle:hover{";
                            ltlBussinessNames.Text += " color:Red; font-family:Helvetica; font-size:15px; font-weight:bold;text-decoration:none;}";
                            ltlBussinessNames.Text += "   </style>";
                            ltlBussinessNames.Text += " <div style='margin-top:10px; margin-bottom:10px; margin-left:20px; width:280px;'>";
                            ltlBussinessNames.Text += " <a class ='hyperlinkstyle'' href='frmBusDealOrderInfo.aspx?rid=" + Convert.ToString(dtUserBusseness.Rows[i]["restaurantId"]) + "'>";
                            ltlBussinessNames.Text += Convert.ToString(dtUserBusseness.Rows[i]["restaurantBusinessName"]);
                            ltlBussinessNames.Text += "</a>";
                            ltlBussinessNames.Text += "</div>";
                        }
                    }


                }
                else
                {
                    Response.Redirect("Default.aspx", false);
                }
            }
            else
            {
                Response.Redirect("Default.aspx", false);
            }
        }
        catch (Exception ex)
        {
            string jScript;
            jScript = "<script>";
            jScript += "MessegeArea('There is an error occur, please email us at support@tazzling.com or call 1855-295-1771.' , 'error');";
            jScript += "</script>";
            ScriptManager.RegisterClientScriptBlock(this, typeof(Button), "Javascript", jScript, false);
        }

    }

  
}
