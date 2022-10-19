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

public partial class Takeout_UserControls_Templates_Total_Referral : System.Web.UI.UserControl
{
   
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
                    Response.Redirect(ResolveUrl("~/default.aspx"), false);
                    return;
                }                
                if (dtUser != null && dtUser.Rows.Count > 0)
                {
                    getRemainedComissionMoneyByUserId(dtUser);
                }               
            }
        }
        catch (Exception ex)
        {

        }
    }

    private void getRemainedComissionMoneyByUserId(DataTable dtUser)
    {
        double dGainedCommission = 0;
        BLLAffiliatePartnerGained objBLLAffiliatePartnerGained = new BLLAffiliatePartnerGained();
        objBLLAffiliatePartnerGained.UserId = Convert.ToInt32(dtUser.Rows[0]["userId"].ToString());
        DataTable dtComissionMaoney = objBLLAffiliatePartnerGained.getGetAffiliatePartnerGainedCreditsByUserID();
        if (dtComissionMaoney != null && dtComissionMaoney.Rows.Count > 0 && dtComissionMaoney.Rows[0][0].ToString().Trim() != "")
        {
            
            dGainedCommission = Convert.ToDouble((dtComissionMaoney.Rows[0][0] != DBNull.Value) ? (dtComissionMaoney.Rows[0][0].ToString() != "" ? dtComissionMaoney.Rows[0][0].ToString() : "0") : "0");
            ltReferralGained.Text = "$" + (dGainedCommission).ToString("###.00") + " CAD";
        }
        else
        {
            ltReferralGained.Text = "$" + float.Parse("00").ToString("###.00") + " CAD";
        }

    }
 
}
