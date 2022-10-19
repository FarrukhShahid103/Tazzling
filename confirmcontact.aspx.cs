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
using System.Threading;
public partial class confirmcontact : System.Web.UI.Page
{
    BLLUser obj = new BLLUser();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            Page.Title = getCityName() + "'s Tasty Daily Deal | Activate Account";
            if (!IsPostBack)
            {

               // string strEncode = HttpUtility.UrlEncode("0VJB+WetD0rGA/d+xiiSUQ==");
                //string strdecode = HttpUtility.UrlDecode(strEncode);
                

                GECEncryption oEnc = new GECEncryption();

              //string strData=   oEnc.DecryptData("123456", strdecode);

                if (Request.QueryString["c"] != null && Request.QueryString["c"] != "")
                {
                    int userID=0;
                    if (Int32.TryParse(Request.QueryString["c"].ToString().Trim(), out userID))
                    {
                        obj.userId = Convert.ToInt32(Request.QueryString["c"].ToString().Trim()) - 111111;
                    }
                    else
                    {
                        obj.userId = Convert.ToInt32(oEnc.DecryptData("123456", Server.UrlDecode(Request.QueryString["c"].ToString().Trim().Replace("_", "%")).Replace(" ", "+")));
                    }
                    obj.isActive = true;
                    obj.changeUserStatus();
                    DataTable dtUserInfo = obj.getUserByID();
                    if (dtUserInfo != null && dtUserInfo.Rows.Count > 0)
                    {
                        HttpCookie cookie3 = Request.Cookies["tastygoSignup"];
                        if (cookie3 == null)
                        {
                            cookie3 = new HttpCookie("tastygoSignup");
                        }
                        cookie3.Expires = DateTime.Now.AddMonths(1);
                        Response.Cookies.Add(cookie3);
                        cookie3["tastygoSignup"] = dtUserInfo.Rows[0]["userName"].ToString();
                    }
                    imgGridMessage.ImageUrl = "images/Checked.png";
                    lblMessage.Text = "Your account have been activated successfully.";
                    Response.AddHeader("REFRESH", "3;URL=Default.aspx");
                }
                //imgGridMessage.ImageUrl = "images/Checked.png";
                //lblMessage.Text = "Your account have been activated successfully.";
                //Response.AddHeader("REFRESH", "3;URL=Default.aspx");
            }
        }
        catch (Exception ex)
        {
            imgGridMessage.ImageUrl = "images/error.png";
            lblMessage.Text = "Your URL is not correct. Please try again with correct URL.<br>There is an error occur, please email us at support@tazzling.com or call 1855-295-1771.";
        }
    }

    protected string getCityName()
    {
        BLLCities objCity = new BLLCities();
        objCity.cityId = 337;
        HttpCookie yourCity = Request.Cookies["yourCity"];
        if (yourCity != null)
        {
            objCity.cityId = Convert.ToInt32(yourCity.Values[0].ToString().Trim());
        }
        DataTable dtCity = objCity.getCityByCityId();
        if (dtCity != null && dtCity.Rows.Count > 0)
        {
            return dtCity.Rows[0]["cityName"].ToString().Trim();
        }
        return "";
    }
}
