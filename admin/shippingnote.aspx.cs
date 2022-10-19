using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.IO;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using SQLHelper;
public partial class shippingnote : System.Web.UI.Page
{   
    #region Page Load Event
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["sid"] != null && Request.QueryString["sid"].Trim() != "")
            {                
                DataTable dtShipping = Misc.search("Select shippingNote from userShippingInfo where shippingInfoId=" + Request.QueryString["sid"].Trim());
                if (dtShipping != null && dtShipping.Rows.Count > 0)
                {
                    txtNote.Text = dtShipping.Rows[0]["shippingNote"].ToString().Trim();                    
                }
            }
        }
    }
    #endregion

    protected void btnUpdate_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            SqlHelper.ExecuteNonQuery(Misc.connStr, CommandType.Text, "Update userShippingInfo set shippingNote='" + txtNote.Text.Trim() + "' where shippingInfoId=" + Request.QueryString["sid"].Trim());
            Response.Redirect("CloseForm.aspx", true);
        }
        catch (Exception ex)
        {
          
        }
    }

    protected void CancelButton_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            Response.Redirect("CloseForm.aspx", true);
        }
        catch (Exception ex)
        {
          
        }
    }
    
}
