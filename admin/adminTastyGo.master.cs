using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;

public partial class admin_adminTastyGo : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["user"] == null)
        {
            Response.Redirect(ResolveUrl("~/admin/default.aspx"), false);
            return;
        }
        string struserid = "";
        DataTable dtUser = (DataTable)Session["user"];
        if ((dtUser != null) && (dtUser.Rows.Count > 0))
        {
            struserid = dtUser.Rows[0]["userTypeID"].ToString().Trim();
            string sPath = System.Web.HttpContext.Current.Request.Url.AbsolutePath;
            System.IO.FileInfo oInfo = new System.IO.FileInfo(sPath);
            string sRet = oInfo.Name;
            if (sRet.ToLower().Trim() != "restaurantmanagement.aspx"
                && sRet.ToLower().Trim() != "addeditrestaurantmanagement.aspx"
                && sRet.ToLower().Trim() != "trackingdealmanagement.aspx"
                && sRet.ToLower().Trim() != "supplierorder.aspx"
                && sRet.ToLower().Trim() != "resendordersmanagement.aspx"
                && sRet.ToLower().Trim() != "resendorders.aspx"
                && sRet.ToLower().Trim() != "shippingaddress.aspx"
                && sRet.ToLower().Trim() != "shippingnote.aspx"                 
                && sRet.ToLower().Trim() != "ordertracker.aspx"
                && sRet.ToLower().Trim() != "orderinvoice.aspx"  
                && sRet.ToLower().Trim() != "dealmanagement.aspx"
                && sRet.ToLower().Trim() != "addeditdealmanagement.aspx"
                && sRet.ToLower().Trim() != "controlpanel.aspx"
                 && sRet.ToLower().Trim() != "slotManagment.aspx"
                 && sRet.ToLower().Trim() != "contestwinnermanagement.aspx"
                 && sRet.ToLower().Trim() != "dealdiscuession.aspx"
                 && sRet.ToLower().Trim() != "dealverificationmanagement2.aspx"
                 && sRet.ToLower().Trim() != "dealverificationmanagement.aspx"
                 && sRet.ToLower().Trim() != "dealordersmgmtbyusers.aspx"
                && sRet.ToLower().Trim() != "dealordersdetailreport.aspx"
                && sRet.ToLower().Trim() != "dealordersdetailsbyusers.aspx"                 
                 && sRet.ToLower().Trim() != "dealsamplevouchersmanagement.aspx"
                && struserid.Trim() == "2")
            {
                Response.Redirect(ResolveUrl("~/admin/controlpanel.aspx"), false);
            }
            else if (sRet.ToLower().Trim() != "dealsamplevouchersmanagement.aspx"
                && sRet.ToLower().Trim() != "contestwinnermanagement.aspx"
                && sRet.ToLower().Trim() != "controlpanel.aspx"
                && sRet.ToLower().Trim() != "dealcausemanagement.aspx"                
                && struserid.Trim() == "7")
            {
                Response.Redirect(ResolveUrl("~/admin/controlpanel.aspx"), false);
            }
            else if (sRet.ToLower().Trim() != "usermanagement.aspx"
                && sRet.ToLower().Trim() != "dealverificationmanagement2.aspx"
                && sRet.ToLower().Trim() != "dealverificationmanagement.aspx"
                && sRet.ToLower().Trim() != "controlpanel.aspx"
                && sRet.ToLower().Trim() != "restaurantmanagement.aspx"
                && sRet.ToLower().Trim() != "addeditrestaurantmanagement.aspx"
                && sRet.ToLower().Trim() != "trackingdealmanagement.aspx"
                && sRet.ToLower().Trim() != "supplierorder.aspx"
                && sRet.ToLower().Trim() != "resendordersmanagement.aspx"
                && sRet.ToLower().Trim() != "resendorders.aspx"
                && sRet.ToLower().Trim() != "shippingaddress.aspx"
                && sRet.ToLower().Trim() != "shippingnote.aspx"                 
                && sRet.ToLower().Trim() != "ordertracker.aspx"
                && sRet.ToLower().Trim() != "orderinvoice.aspx"  
                && sRet.ToLower().Trim() != "dealmanagement.aspx"
                && sRet.ToLower().Trim() != "addeditdealmanagement.aspx"
                && sRet.ToLower().Trim() != "dealdiscuession.aspx"
                && sRet.ToLower().Trim() != "slotManagment.aspx"
                && sRet.ToLower().Trim() != "dealorderinfo.aspx"
                && sRet.ToLower().Trim() != "uploaddocuments.aspx"                
                 && sRet.ToLower().Trim() != "dealordersmgmtbyusers.aspx"
                && sRet.ToLower().Trim() != "dealordersdetailreport.aspx"
                 && sRet.ToLower().Trim() != "dealsamplevouchersmanagement.aspx"
                && sRet.ToLower().Trim() != "dealordersdetailsbyusers.aspx"
                 && sRet.ToLower().Trim() != "foodcreditmanagement.aspx"
                && sRet.ToLower().Trim() != "orderdetails.aspx"
                && sRet.ToLower().Trim() != "dealorderdetailinfo.aspx"
                && sRet.ToLower().Trim() != "createnewsletter.aspx"                  
                && struserid.Trim() == "6")
            {
                Response.Redirect(ResolveUrl("~/admin/controlpanel.aspx"), false);
            }
            if (struserid.Trim() == "2" || struserid.Trim() == "6" || struserid.Trim() == "7")
            {
                pnlforSuperAdmin.Visible = false;
            }
        }
        lblFooter.Text = "Copyrights © " + System.DateTime.Now.Year.ToString() + " TastyGo";
    }
    protected void lnkLogOut_Click(object sender, EventArgs e)
    {
        try
        {
            Session.Remove("user");
            Response.Redirect(ResolveUrl("~/admin/default.aspx"), false);
        }
        catch (Exception ex)
        {

        }
    }
}
