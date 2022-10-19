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

public partial class Takeout_UserControls_Templates_All_Total : System.Web.UI.UserControl
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
                    return;
                }
                if (dtUser != null && dtUser.Rows.Count > 0)
                {
                   // calculateTastyPointsByUserId(dtUser);
                    calculateTastyCreditUserId(dtUser);
                    calculateComissionMoneyByUserId(dtUser);
                }               
            }
        }
        catch (Exception ex)
        {

        }
    }

    private void calculateTastyCreditUserId(DataTable dtUser)
    {
        double dGainedCommission = 0;
        BLLMemberUsedGiftCards objBLLMemberUsedGiftCards = new BLLMemberUsedGiftCards();
        objBLLMemberUsedGiftCards.createdBy = Convert.ToInt64(dtUser.Rows[0]["userId"].ToString());

        DataTable dtUseAbleFoodCredit = null;
        dtUseAbleFoodCredit = objBLLMemberUsedGiftCards.getUseableFoodCreditRefferalByUserID();

        if (dtUseAbleFoodCredit != null && dtUseAbleFoodCredit.Rows.Count > 0)
        {
            dGainedCommission = Convert.ToDouble((dtUseAbleFoodCredit.Rows[0]["remainAmount"] != DBNull.Value) ? (dtUseAbleFoodCredit.Rows[0]["remainAmount"].ToString() != "" ? dtUseAbleFoodCredit.Rows[0]["remainAmount"].ToString() : "0") : "0");

            lblTastyCredit.Text = "$" + (dGainedCommission).ToString("###.00") + " CAD";
        }
        else
        {
            lblTastyCredit.Text = "$" + float.Parse("00").ToString("###.00") + " CAD";
        }
    }

    private void calculateComissionMoneyByUserId(DataTable dtUser)
    {
        double dGainedCommission = 0;
        BLLAffiliatePartnerGained objBLLAffiliatePartnerGained = new BLLAffiliatePartnerGained();
        objBLLAffiliatePartnerGained.UserId = Convert.ToInt32(dtUser.Rows[0]["userId"].ToString());
        DataTable dtComissionMaoney = objBLLAffiliatePartnerGained.getGetAffiliatePartnerGainedCreditsByUserID();
        if (dtComissionMaoney != null && dtComissionMaoney.Rows.Count > 0 && dtComissionMaoney.Rows[0][0].ToString().Trim() != "")
        {

            dGainedCommission = Convert.ToDouble((dtComissionMaoney.Rows[0][0] != DBNull.Value) ? (dtComissionMaoney.Rows[0][0].ToString() != "" ? dtComissionMaoney.Rows[0][0].ToString() : "0") : "0");
            lblTastyComission.Text = "$" + (dGainedCommission).ToString("###.00") + " CAD";
        }
        else
        {
            lblTastyComission.Text = "$" + float.Parse("00").ToString("###.00") + " CAD";
        }

    }

    private void calculateTastyPointsByUserId(DataTable dtUser)
    {
        //int dGainedCommission = 0;
        //BLLKarmaPoints objKarma = new BLLKarmaPoints();
        //objKarma.userId = Convert.ToInt32(dtUser.Rows[0]["userId"].ToString());
        //DataTable dtKarma = objKarma.getKarmaPointsTotalByUserId();
        //if (dtKarma != null && dtKarma.Rows.Count > 0 && dtKarma.Rows[0][0].ToString().Trim() != "")
        //{

        //    dGainedCommission = Convert.ToInt32((dtKarma.Rows[0][0] != DBNull.Value) ? (dtKarma.Rows[0][0].ToString() != "" ? dtKarma.Rows[0][0].ToString() : "0") : "0");
        //    lblTastyPoints.Text = dGainedCommission.ToString();
        //}
        //else
        //{
        //    lblTastyPoints.Text = "0";
        //}

    }
 
}
