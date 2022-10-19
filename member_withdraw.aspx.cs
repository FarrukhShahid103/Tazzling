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

public partial class member_withdraw : System.Web.UI.Page
{
    BLLUser obj = new BLLUser();
    BLLMemberDeliveryInfo objDeliveryInfo = new BLLMemberDeliveryInfo();

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            Page.Title = ConfigurationManager.AppSettings["pageTitleStart"].ToString().Trim() + " | Member | Withdraw Request";
            if (!IsPostBack)
            {
                if (Session["member"] != null || Session["restaurant"] != null || Session["sale"] != null || Session["user"] != null)
                {
                    
                }
                else
                {
                    Response.Redirect("default.aspx", false);
                }
            }
        }
        catch (Exception ex)
        {

        }
    }

}
