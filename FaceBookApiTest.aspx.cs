using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class FaceBookApiTest : System.Web.UI.Page
{
    public string totalcomments = "";
    public static string dealid = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {

            if (Request.QueryString["GetFeeds"] != null && !string.IsNullOrEmpty(Request.QueryString["GetFeeds"].ToString().Trim()))
            {
                GetFeeds();
            }

            if (Request.QueryString["UserID"] != null && Request.QueryString["UserID"] != "" && Request.QueryString["DealID"] != null && Request.QueryString["DealID"] != "")
            {
                BLLLiveFeed updatefvrt = new BLLLiveFeed();
                updatefvrt.dealId = Convert.ToInt16(Request.QueryString["DealID"].ToString().Trim());
                updatefvrt.userId= Convert.ToInt16(Request.QueryString["UserID"].ToString().Trim());

                int Result = updatefvrt.UpdateTotalFvrtInFeeds();
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




            if (Request.QueryString["DealID"] != null && Request.QueryString["DealID"] != "")
            {
                string _Html = "";
                dealid = Request.QueryString["DealID"].ToString().Trim();
                oAuthFacebook oAuthComments = new oAuthFacebook();
                BLLUser objUsercomments = new BLLUser();
                string urlComments = "https://graph.facebook.com/?ids=http://www.demo.tazzling.com/dealid=" + dealid;
                string jsonComments = oAuthComments.WebRequest(oAuthFacebook.Method.GET, urlComments, String.Empty);
                _Html += "<div class='fb-comments' data-href='http://www.demo.tazzling.com/dealid=" + dealid + "'";
                _Html += " data-num-posts='2'>";
                _Html += "</div>";

                if (jsonComments != "")
                {
                    var serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                    var dict = (System.Collections.Generic.Dictionary<string, object>)serializer.DeserializeObject(jsonComments);
                    if (dict.Count > 0)
                    {
                        System.Collections.Generic.Dictionary<string, object> disct2 = (System.Collections.Generic.Dictionary<string, object>)dict["http://www.demo.tazzling.com/dealid=" + dealid];
                        if (disct2.Count > 1)
                        {
                            totalcomments = disct2["comments"].ToString();


                        }
                        else
                        {
                            totalcomments = "0";
                        }

                    }
                }

                BLLLiveFeed updateComments = new BLLLiveFeed();
                updateComments.totalComments = Convert.ToInt16(totalcomments);
                updateComments.dealId = Convert.ToInt16(Request.QueryString["DealID"].ToString().Trim());
                int Result = updateComments.UpdateTotalCommentsInFeeds();
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
    private void GetFeeds()
    {
        DataTable dt = null;
        BLLLiveFeed livefeed = new BLLLiveFeed();
        dt = livefeed.GetFeeds();
        if (dt != null && dt.Rows.Count > 0)
        {
            string Data = "";
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string title = "";
                string[] images = dt.Rows[i]["images"].ToString().Split(',');
                string restaurantId = dt.Rows[i]["restaurantId"].ToString();
                string image = images[0];
                string imagePath = "images/dealfood/" +restaurantId + "/" + image;

                if (dt.Rows[i]["title"].ToString().Trim() != "")
                {
                    if (dt.Rows[i]["title"].ToString().Trim().Length > 50)
                    {
                        title = dt.Rows[i]["title"].ToString().Trim().Substring(0, 45) + "...";

                    }
                    else
                    {
                        title = dt.Rows[i]["title"].ToString().Trim();
 
                    }
                }
                else if (dt.Rows[i]["topTitle"].ToString().Trim() != "")
                {
                    if (dt.Rows[i]["topTitle"].ToString().Trim().Length > 50)
                    {
                        title = dt.Rows[i]["topTitle"].ToString().Trim().Substring(0, 45) + "...";

                    }
                    else
                    {
                        title = dt.Rows[i]["topTitle"].ToString().Trim(); 
                    }

                }

                else if (dt.Rows[i]["description"].ToString().Trim() != "")
                {
                    if (dt.Rows[i]["description"].ToString().Trim().Length > 50)
                    {
                        title = dt.Rows[i]["description"].ToString().Trim().Substring(0, 45) + "...";

                    }
                    else
                    {
                        title = dt.Rows[i]["description"].ToString().Trim(); 
                    }

                }

                else
                {
                    title = "No Data According to this deal about its discription Or heading";
                }
                Data += dt.Rows[i]["DealID"].ToString().Trim() + "*" + title + "*" + imagePath + "*" + dt.Rows[i]["TotalFavorites"].ToString().Trim()  
                    + "*" + dt.Rows[i]["TotalPurchases"].ToString().Trim() + "*" + dt.Rows[i]["ActivityTime"].ToString().Trim() + "*" 
                    + dt.Rows[i]["restaurantId"].ToString().Trim() + "*" + dt.Rows[i]["firstName"].ToString().Trim() + "*" + dt.Rows[i]["lastName"].ToString().Trim()
                    + "*" + dt.Rows[i]["sellingPrice"].ToString().Trim() + "*" + dt.Rows[i]["TotalComments"].ToString().Trim() + "*" 
                    + dt.Rows[i]["userid"].ToString().Trim() + "|";
            }
            Response.Write(Data);
            Response.End();
        }

    }
}