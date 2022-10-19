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

public partial class Takeout_UserControls_Templates_Total_Points : System.Web.UI.UserControl
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
        int dGainedCommission = 0;
        BLLKarmaPoints objKarma = new BLLKarmaPoints();
        objKarma.userId = Convert.ToInt32(dtUser.Rows[0]["userId"].ToString());
        DataTable dtKarma = objKarma.getKarmaPointsTotalByUserId();
        if (dtKarma != null && dtKarma.Rows.Count > 0 && dtKarma.Rows[0][0].ToString().Trim() != "")
        {

            dGainedCommission = Convert.ToInt32((dtKarma.Rows[0][0] != DBNull.Value) ? (dtKarma.Rows[0][0].ToString() != "" ? dtKarma.Rows[0][0].ToString() : "0") : "0");
            ltReferralGained.Text = dGainedCommission.ToString();
        }
        else
        {
            ltReferralGained.Text = "0";
        }

    }
 
}
