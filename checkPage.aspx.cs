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

public partial class Takeout_checkPage : System.Web.UI.Page
{
    protected string strStoreKey;//StoreKey --> merchantcardcapture200024
    protected string strCustomerRefNo; // --> 123456789
    protected string strSubTotal;//Total --> 8.00
    protected string strPaymentType;//PaymentType --> CC
    protected string strCardAction;//CardAction --> 0
    protected string strCardNo;//CardNumber --> 4111111111111111
    protected string strExpMonth;//CardExpMonth --> 08
    protected string strExpYear;//CardExpYear --> 08
    protected string strCSC;//CardIDNumber --> 3422
    protected string strThankyouUrl;//Thankyou URL -->  http://localhost:1675/TastyGo/takeout/giftcard_thankyou.aspx
    protected string strNoThanksUrl;//No Thanks URL -->  http://localhost:1675/TastyGo/takeout/giftcard_nothankyou.aspx
    protected string strResponseFormat;//Response Format --> HTML
    protected string strCompanyName;//Company Name
    protected string strBaddress1;//Address 
    protected string strBcity;//City
    protected string strBprovince;//Province
    protected string strBpostalcode;//Postalcode
    protected string strPhone;//Phone
    protected string strFax;//Fax
    protected string strEmail;//Email
    protected string strBname;//Business Name
    

    
    
    BLLMemberDeliveryInfo objDelInfo = new BLLMemberDeliveryInfo();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsPostBack)
            {
                              
                ////Get the Store ID from Web.config
                //strStoreKey = "merchantcardcapture200024";      //merchantcardcapture200024
                //strCustomerRefNo = "123456789";                 //123456789
                //strSubTotal = htSetting["payAble"].ToString().Replace("$", "").Trim();   //8.00
                //if (Session["PaymentType"] != null && Session["PaymentType"] != "")
                //{
                //    strPaymentType = Session["PaymentType"].ToString();                               //CC means Credit Card & DB means Intract Online (through Bank directly)
                //}
                //else
                //{
                //    strPaymentType = "CC";                               //CC means Credit Card & DB means Intract Online (through Bank directly)
                //}
                //strCardAction = "0";                                 //“0” for Sale, “1” for PreAuth, “2” for PostAuth 
                //strCardNo = arrCreditCardInfo[0].ToString();         //4111111111111111
                //strExpMonth = arrCreditCardInfo[1].ToString();       //08
                //strExpYear = arrCreditCardInfo[2].ToString();        //08
                //strCSC = arrCreditCardInfo[3].ToString();            //3422

                ////Get the Thankyou URL from Web.config
                //strThankyouUrl = ConfigurationSettings.AppSettings["ReturnUrl"].ToString();
                //strNoThanksUrl = ConfigurationManager.AppSettings["YourSecureSite"].ToString() + "Takeout/checkout_step3.aspx";
                //strResponseFormat = "HTML";                          //HTML or XML

                //if (Session["PaymentType"] != null && Session["PaymentType"].ToString() == "DB")
                //{
                //    pnlCC.Visible = false;
                //}
                //else
                //{
                //    pnlCC.Visible = true;
                //}
                ////Clear the Sessions here
                ////Session.Remove("htSetting");
                ////Session.Remove("dtMenuItems");

            }
        }
        catch (Exception ex)
        {
            string strException = "There is an error occur, please email us at support@tazzling.com or call 1855-295-1771.";
        }
    }    
}
