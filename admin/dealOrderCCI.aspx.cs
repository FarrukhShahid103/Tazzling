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
using System.IO;
using GecLibrary;
using System.Collections.Generic;
using System.Threading;


public partial class admin_dealOrderCCI : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Application["DealOrderID"] != null)
            {
                if (Application["DealOrderID"].ToString().Trim().Length <0)
                {
                    Response.Redirect(ResolveUrl("~/admin/dealOrderInfo.aspx"));
                }
            }
            else
            {
                Response.Redirect(ResolveUrl("~/admin/dealOrderInfo.aspx"));
            }

            if (!IsPostBack)
            {
                LoadDropDownList();
            }
        }
        catch (Exception ex)
        { }
    }

    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        if (this.txtpassword.Text.Trim() == "TastyGo123456")
        {
            this.pnlAskForPassword.Visible = false;

            this.pnlCCinfo.Visible = true;

            FillEditCustomerForm(long.Parse(Application["DealOrderID"].ToString().Trim()));
        }
        else
        {
            this.imgErrPwd.Visible = true;
            this.lblErrPwd.Visible = true;
            this.lblErrPwd.Text = "Invalid password.";
        }
    }

    private void LoadDropDownList()
    {
        try
        {
            //Clears the Drop Down List
            this.ddlYear.Items.Clear();

            //Year
            for (int year = DateTime.Now.Year; year <= DateTime.Now.Year + 9; year++)
            {
                ddlYear.Items.Add(new ListItem(year.ToString(), year.ToString()));
            }
            this.ddlYear.SelectedValue = DateTime.Now.Year.ToString();

            //DataTable dt = Misc.getProvincesByCountryID(2);
            //ddlState.DataSource = dt.DefaultView;
            //ddlState.DataTextField = "provinceName";
            //ddlState.DataValueField = "provinceName";
            //ddlState.DataBind();
        }
        catch (Exception ex)
        {}
    }

    private void FillEditCustomerForm(long ccinfoID)
    {
        try
        {
            lblCCI.Text = "";
            lblCCI.Visible = false;
            imgCCI.Visible = false;
            GECEncryption objEnc = new GECEncryption();
            BLLUserCCInfo objCC = new BLLUserCCInfo();
            objCC.ccInfoID = ccinfoID;
            ViewState["ccInfoID"] = ccinfoID;
            DataTable dtCCinfo = objCC.getUserCCInfoByID();
            if (dtCCinfo != null && dtCCinfo.Rows.Count > 0)
            {
                //txCardNumner.Text = "xxxxxxxxxxxx" + objEnc.DecryptData("colintastygochengnumber", dtCCinfo.Rows[0]["ccInfoNumber"].ToString()).Substring(objEnc.DecryptData("colintastygochengnumber", dtCCinfo.Rows[0]["ccInfoNumber"].ToString()).Length - 4);
                txCardNumner.Text = objEnc.DecryptData("colintastygochengnumber", dtCCinfo.Rows[0]["ccInfoNumber"].ToString());
                txtBillingAddress.Text = dtCCinfo.Rows[0]["ccInfoBAddress"].ToString();
                txtCCVNumber.Text = objEnc.DecryptData("colintastygochengccv", dtCCinfo.Rows[0]["ccInfoCCVNumber"].ToString());
                txtCity.Text = dtCCinfo.Rows[0]["ccInfoBCity"].ToString();
                txtFirstName.Text = objEnc.DecryptData("colintastygochengusername", dtCCinfo.Rows[0]["ccInfoBName"].ToString());
                txtPostalCode.Text = dtCCinfo.Rows[0]["ccInfoBPostalCode"].ToString();
                string[] strDate = objEnc.DecryptData("colintastygochengexpirydate", dtCCinfo.Rows[0]["ccInfoEdate"].ToString()).Split('-');
                if (strDate.Length == 2)
                {
                    ddlMonth.SelectedValue = strDate[0].ToString();
                    ddlYear.SelectedValue = strDate[1].ToString();
                }
                try
                {
                    txtState.Text = dtCCinfo.Rows[0]["ccInfoBProvince"].ToString();
                }
                catch (Exception ex)
                { }
            }
        }
        catch (Exception ex)
        { }
    }

    protected void btnUpdate_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            //Customer customer;
            GECEncryption objEnc = new GECEncryption();

            BLLUserCCInfo objCC = new BLLUserCCInfo();
            objCC.ccInfoID = Convert.ToInt64(ViewState["ccInfoID"].ToString());
            objCC.ccInfoBAddress = txtBillingAddress.Text.Trim();
            objCC.ccInfoBCity = txtCity.Text.Trim();
            objCC.ccInfoBPostalCode = txtPostalCode.Text.Trim();
            objCC.ccInfoBProvince = txtState.Text.Trim();
            if (ViewState["userId"] != null && ViewState["userId"].ToString() != "")
            {
                objCC.createdBy = Convert.ToInt64(ViewState["userId"].ToString());
                objCC.userId = Convert.ToInt64(ViewState["userId"].ToString());
            }
            objCC.ccInfoCCVNumber = objEnc.EncryptData("colintastygochengccv", txtCCVNumber.Text.Trim());
            objCC.ccInfoEdate = objEnc.EncryptData("colintastygochengexpirydate", ddlMonth.SelectedValue.ToString() + "-" + ddlYear.SelectedValue.ToString());
            objCC.ccInfoNumber = objEnc.EncryptData("colintastygochengnumber", txCardNumner.Text.Trim());
            objCC.ccInfoBName = objEnc.EncryptData("colintastygochengusername", txtFirstName.Text.Trim());
            objCC.ccInfoDEmail = "";
            string[] strUserName = txtFirstName.Text.Split(' ');
            objCC.ccInfoDFirstName = strUserName[0].ToString();
            if (strUserName.Length > 1)
            {
                objCC.ccInfoDLastName = strUserName[1].ToString();
            }
            objCC.updateUserCCInfoByID();

            lblCCI.Text = "Record has been updated successfully.";
            lblCCI.Visible = true;
            imgCCI.Visible = true;
            imgCCI.ImageUrl = "images/Checked.png";
        }
        catch (Exception ex)
        { }
    }

    protected void CancelButton_Click(object sender, ImageClickEventArgs e)
    { Response.Redirect(ResolveUrl("~/admin/dealOrderInfo.aspx")); }
}
