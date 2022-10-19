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

public partial class Takeout_UserControls_Templates_Total_Funds : System.Web.UI.UserControl
{
    BLLMemberUsedGiftCards objUseableCard = new BLLMemberUsedGiftCards();
    BLLCommission objCommsission = new BLLCommission();
    BLLConsumptionRecord objConsumedRecord = new BLLConsumptionRecord();
    BLLMemberPoints obj = new BLLMemberPoints();
    BLLMemberPointGiftsRequests objUsedPoints = new BLLMemberPointGiftsRequests();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                DataTable dtUser = null;
              
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
                else
                {                   
                    return;
                }                
                if (dtUser != null && dtUser.Rows.Count > 0)
                {
                    getRemainedGainedBalByUserId(dtUser);               
                }               
            }
        }
        catch (Exception ex)
        {

        }
    }

    private void getRemainedGainedBalByUserId(DataTable dtUser)
    {
        double dGainedCommission = 0;
        BLLMemberUsedGiftCards objBLLMemberUsedGiftCards = new BLLMemberUsedGiftCards();
        objBLLMemberUsedGiftCards.createdBy = Convert.ToInt64(dtUser.Rows[0]["userId"].ToString());
        
        DataTable dtUseAbleFoodCredit = null;
        dtUseAbleFoodCredit = objBLLMemberUsedGiftCards.getUseableFoodCreditRefferalByUserID();

        if (dtUseAbleFoodCredit != null && dtUseAbleFoodCredit.Rows.Count > 0)
        {
            dGainedCommission = Convert.ToDouble((dtUseAbleFoodCredit.Rows[0]["remainAmount"] != DBNull.Value) ? (dtUseAbleFoodCredit.Rows[0]["remainAmount"].ToString() != "" ? dtUseAbleFoodCredit.Rows[0]["remainAmount"].ToString() : "0") : "0");

            ltReferralGained.Text = "$" + (dGainedCommission).ToString("###.00") ;
        }
        else
        {
            ltReferralGained.Text = "$" + float.Parse("00").ToString("###.00");
        }        
    }

 
}
