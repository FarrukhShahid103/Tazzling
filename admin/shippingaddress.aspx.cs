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
public partial class shippingaddress : System.Web.UI.Page
{   
    #region Page Load Event
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["sid"] != null && Request.QueryString["sid"].Trim() != "")
            {
                //BLLUserShippingInfo objShippingInfo = new BLLUserShippingInfo();
                //objShippingInfo.shippingInfoId = Convert.ToInt64(Request.QueryString["sid"].Trim());
                DataTable dtShipping = Misc.search("Select * from userShippingInfo where shippingInfoId=" + Request.QueryString["sid"].Trim());
                if (dtShipping != null && dtShipping.Rows.Count > 0)
                {
                    txtAddress.Text = dtShipping.Rows[0]["Address"].ToString().Trim();
                    txtCity.Text = dtShipping.Rows[0]["City"].ToString().Trim();
                    txtProvince.Text = dtShipping.Rows[0]["State"].ToString().Trim();
                    txtZipCode.Text = dtShipping.Rows[0]["ZipCode"].ToString().Trim();
                    ddlShippingCountry.SelectedValue = dtShipping.Rows[0]["shippingCountry"].ToString().Trim();
                }
            }
        }
    }
    #endregion

    protected void btnUpdate_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            SqlHelper.ExecuteNonQuery(Misc.connStr, CommandType.Text, "Update userShippingInfo set Address='" + txtAddress.Text.Trim() + "',City='" + txtCity.Text.Trim() + "',State='" + txtProvince.Text.Trim() + "',ZipCode='" + txtZipCode.Text.Trim() + "',shippingCountry='" + ddlShippingCountry.SelectedValue.Trim() + "' where shippingInfoId=" + Request.QueryString["sid"].Trim());
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
