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
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Net.Sockets;
using System.IO;
using System.Net.Security;
using System.Security.Authentication;

public partial class deviceInformation : System.Web.UI.Page
{  
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ddlDeviceType_SelectedIndexChanged(sender, e);
            lblMessage.Visible = false;        
        }     

    }
    protected void btnSendMessage_Click(object sender, EventArgs e)
    {
        try
        {
            BLLDeviceVersionInformation objInfo = new BLLDeviceVersionInformation();
            objInfo.deviceType = ddlDeviceType.SelectedValue.ToString();
            objInfo.message = txtMessage.Text.Trim();
            objInfo.newVersion = txtVersion.Text.Trim();
            objInfo.title = txtTitle.Text.Trim();
            objInfo.createUpdateDeviceVersionInformation();
            lblMessage.Visible = true;
            lblMessage.Text = "Device Version information has been updated Successfully.";
        }
        catch (Exception ex)
        { }
    }

    protected void ddlDeviceType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lblMessage.Visible = false;
            lblMessage.Text = "";
            BLLDeviceVersionInformation objInfo = new BLLDeviceVersionInformation();
            objInfo.deviceType = ddlDeviceType.SelectedValue.ToString();
            DataTable dtDeviceInfo = objInfo.getDeviceVersionInformationByDeviceType();
            if (dtDeviceInfo != null && dtDeviceInfo.Rows.Count > 0)
            {
                txtTitle.Text = dtDeviceInfo.Rows[0]["title"].ToString().Trim();
                txtMessage.Text = dtDeviceInfo.Rows[0]["title"].ToString().Trim();
                txtVersion.Text = dtDeviceInfo.Rows[0]["newVersion"].ToString().Trim();
            }
            else
            {
                txtTitle.Text = "";
                txtMessage.Text ="";
                txtVersion.Text = "";
            }
        }
        catch (Exception ex)
        { }
    }
}
