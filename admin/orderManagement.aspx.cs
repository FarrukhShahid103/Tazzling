using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class admin_orderManagement : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Request.QueryString["CtrlID"] != null)
            {
                Control Ctrl = LoadControl("UserControls/Order/" + Request.QueryString["CtrlID"]);
                ctrlHolder.Controls.Add(Ctrl);
            }
            else
            {
                Control Ctrl = LoadControl("UserControls/Order/OrdersList.ascx");
                ctrlHolder.Controls.Add(Ctrl);
            }
        }
        catch (Exception ex)
        { }
    }
}