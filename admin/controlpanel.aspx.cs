using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class controlpanel : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
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
                if (struserid.Trim() == "2")
                {
                    Panel2.Visible = false;
                    Panel3.Visible = false;
                    Panel1.Visible = false;
                    Panel4.Visible = false;
                    Panel5.Visible = false;
                    pnlPromoter.Visible = false;
                    pnlCustomerService.Visible = false;
                    pnlForadmin.Visible = true;

                }
                else if (struserid.Trim() == "6")
                {
                    Panel2.Visible = false;
                    Panel3.Visible = false;
                    Panel1.Visible = false;
                    Panel4.Visible = false;
                    Panel5.Visible = false;
                    pnlForadmin.Visible = false;
                    pnlCustomerService.Visible = true;
                    pnlCustomerService2.Visible = true;
                    pnlPromoter.Visible = false;
                }
                else if (struserid.Trim() == "7")
                {
                    Panel2.Visible = false;
                    Panel3.Visible = false;
                    Panel1.Visible = false;
                    Panel4.Visible = false;
                    Panel5.Visible = false;
                    pnlForadmin.Visible = false;
                    pnlCustomerService.Visible = false;
                    pnlCustomerService2.Visible = false;
                    pnlPromoter.Visible = true;
                }
                else
                {
                    Panel2.Visible = true;
                    Panel3.Visible = true;
                    Panel1.Visible = true;
                    Panel4.Visible = true;
                    Panel5.Visible = true;
                    pnlForadmin.Visible = false;
                    pnlCustomerService.Visible = false;
                    pnlCustomerService2.Visible = false;
                    pnlPromoter.Visible = false;
                }
            }
        }
        catch (Exception ex)
        { }
    }
}
