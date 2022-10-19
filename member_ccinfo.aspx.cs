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


public partial class member_ccinfo : System.Web.UI.Page
{

    BLLUserCCInfo objCC = new BLLUserCCInfo();
    BLLDealOrders objorders = new BLLDealOrders();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            Page.Title = ConfigurationManager.AppSettings["pageTitleStart"].ToString().Trim() + " | Member | Credit Cards";
            if (!IsPostBack)
            {
                LoadDropDownList();
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
                if (dtUser.Rows.Count > 0)
                {
                    ViewState["userId"] = dtUser.Rows[0]["userId"].ToString();
                    ViewState["userName"] = dtUser.Rows[0]["userName"].ToString();
                    GridDataBind(Convert.ToInt64(dtUser.Rows[0]["userId"].ToString()));
                }

            }
        }
        catch (Exception ex)
        { }
    }
    protected string GetCardExplain(object objCCNumber)
    {
        if (objCCNumber.ToString().Trim() != "")
        {
            GECEncryption objEnc = new GECEncryption();
            return "xxxxxxxxxxxx" + objEnc.DecryptData("colintastygochengnumber", objCCNumber.ToString()).Substring(objEnc.DecryptData("colintastygochengnumber", objCCNumber.ToString()).Length - 4);
           // objEnc.DecryptData("colintastygochengnumber", objCCNumber.ToString().Trim());
        }
        return "";
    }
    private void GridDataBind(long UserID)
    {
        objCC = new BLLUserCCInfo();
        objCC.userId = UserID;
        gvCustomers.DataSource = objCC.getUserCCInfoByUserID();
        gvCustomers.DataBind();
    }


    protected void btnAddCustomer_Click(object sender, EventArgs e)
    {
        lblMessage.Text = "";
        lblMessage.Visible = false;
        imgGridMessage.Visible = false;
        ddlMonth.SelectedIndex = 0;
        ddlState.SelectedIndex = 0;
        ddlYear.SelectedIndex = 0;
        ViewState["ccInfoID"] = null;
        ClearEditCustomerForm();	//In case using non-modal

        RegisterStartupScript("jsUnblockDialog", "unblockDialog();");
    }


    private void ClearEditCustomerForm()
    {
        //Empty out text boxes
        var textBoxes = new List<Control>();
        FindControlsOfType(this.phrEditCustomer, typeof(TextBox), textBoxes);

        foreach (TextBox textBox in textBoxes)
            textBox.Text = "";

        //Clear validators
        var validators = new List<Control>();
        FindControlsOfType(this.phrEditCustomer, typeof(BaseValidator), validators);

        foreach (BaseValidator validator in validators)
            validator.IsValid = true;
    }


    static public void FindControlsOfType(Control root, Type controlType, List<Control> list)
    {
        if (root.GetType() == controlType || root.GetType().IsSubclassOf(controlType))
        {
            list.Add(root);
        }

        //Skip input controls
        if (!root.HasControls())
            return;

        foreach (Control control in root.Controls)
        {
            FindControlsOfType(control, controlType, list);
        }
    }


    protected void gvCustomer_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.DataItem == null)
            return;

        LinkButton btnEdit = (LinkButton)e.Row.FindControl("btnEdit");
        btnEdit.OnClientClick = "openDialogAndBlock('Edit Credit Card', '" + btnEdit.ClientID + "')";
    }


    protected void gvCustomers_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        objCC = new BLLUserCCInfo();
        objCC.ccInfoID = Convert.ToInt64(e.CommandArgument);

        switch (e.CommandName)
        {
            //Can't use just Edit and Delete or need to bypass RowEditing and Deleting
            case "EditCustomer":
                FillEditCustomerForm(Convert.ToInt64(e.CommandArgument));

               // this.EditCustomerID = customerID;
                RegisterStartupScript("jsUnblockDialog", "unblockDialog();");

                //Setting timer to test longer loading
                //Thread.Sleep(2000);
                break;

            case "DeleteCustomer":
                objorders.ccInfoID = Convert.ToInt64(e.CommandArgument);
                DataTable dtOrder = objorders.getDealOrderDetailByCCInfoID();
                if (dtOrder != null && dtOrder.Rows.Count > 0)
                {
                    lblMessage.Text = "Sorry you could not delete this record, as it is used in an order.";
                    lblMessage.Visible = true;
                    imgGridMessage.Visible = true;
                    imgGridMessage.ImageUrl = "images/error.png";
                }
                else
                {
                    objCC.deleteUserCCInfo();
                    GridDataBind(Convert.ToInt64(ViewState["userId"].ToString()));
                    lblMessage.Text = "Record has been deleted successfully.";
                    lblMessage.Visible = true;
                    imgGridMessage.Visible = true;
                    imgGridMessage.ImageUrl = "images/Checked.png";
                }
                break;

        }
    }

    private void FillEditCustomerForm(long ccinfoID)
   {
       try
       {
           lblMessage.Text = "";
           lblMessage.Visible = false;
           imgGridMessage.Visible = false;
           GECEncryption objEnc = new GECEncryption();
           objCC = new BLLUserCCInfo();
           objCC.ccInfoID = ccinfoID;
           ViewState["ccInfoID"] = ccinfoID;
           DataTable dtCCinfo = objCC.getUserCCInfoByID();
           if (dtCCinfo != null && dtCCinfo.Rows.Count > 0)
           {
               txCardNumner.Text = "xxxxxxxxxxxx" + objEnc.DecryptData("colintastygochengnumber", dtCCinfo.Rows[0]["ccInfoNumber"].ToString()).Substring(objEnc.DecryptData("colintastygochengnumber", dtCCinfo.Rows[0]["ccInfoNumber"].ToString()).Length - 4);
               txtBillingAddress.Text = dtCCinfo.Rows[0]["ccInfoBAddress"].ToString();
               txtCCVNumber.Text = objEnc.DecryptData("colintastygochengccv", dtCCinfo.Rows[0]["ccInfoCCVNumber"].ToString());
               txtCity.Text = dtCCinfo.Rows[0]["ccInfoBCity"].ToString();
               txtFirstName.Text = objEnc.DecryptData("colintastygochengusername", dtCCinfo.Rows[0]["ccInfoBName"].ToString());
               txtPostalCode.Text = dtCCinfo.Rows[0]["ccInfoBPostalCode"].ToString();
               if (dtCCinfo.Rows[0]["ccInfoDEmail"] != null && dtCCinfo.Rows[0]["ccInfoDEmail"].ToString() != "")
               {
                   ViewState["userName"] = dtCCinfo.Rows[0]["ccInfoDEmail"].ToString();
               }
               string[] strDate = objEnc.DecryptData("colintastygochengexpirydate", dtCCinfo.Rows[0]["ccInfoEdate"].ToString()).Split('-');
               if (strDate.Length == 2)
               {
                   ddlMonth.SelectedValue = strDate[0].ToString();
                   ddlYear.SelectedValue = strDate[1].ToString();
               }
               try
               {
                   ddlState.SelectedValue = dtCCinfo.Rows[0]["ccInfoBProvince"].ToString();
               }
               catch (Exception ex)
               { }
           }
       }
       catch (Exception ex)
       { }        
    }


    private void TriggerClientGridRefresh()
    {
        string script = "__doPostBack(\"" + btnRefreshGrid.ClientID + "\", \"\");";
        RegisterStartupScript("jsGridRefresh", script);
    }


    private void RegisterStartupScript(string key, string script)
    {
        ScriptManager.RegisterStartupScript(phrJsRunner, phrJsRunner.GetType(), key, script, true);
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (!Page.IsValid)
            return;

        //Customer customer;
        GECEncryption objEnc = new GECEncryption();
        if (ViewState["ccInfoID"]==null)
        {
            objCC = new BLLUserCCInfo();           
            objCC.ccInfoBAddress = txtBillingAddress.Text.Trim();
            objCC.ccInfoBCity = txtCity.Text.Trim();
            objCC.ccInfoBPostalCode = txtPostalCode.Text.Trim();
            objCC.ccInfoBProvince = ddlState.SelectedValue.Trim();
            if (ViewState["userId"] != null && ViewState["userId"].ToString() != "")
            {
                objCC.createdBy = Convert.ToInt64(ViewState["userId"].ToString());
                objCC.userId = Convert.ToInt64(ViewState["userId"].ToString());
            }
            objCC.ccInfoCCVNumber = objEnc.EncryptData("colintastygochengccv", txtCCVNumber.Text.Trim());
            objCC.ccInfoEdate = objEnc.EncryptData("colintastygochengexpirydate", ddlMonth.SelectedValue.ToString() + "-" + ddlYear.SelectedValue.ToString());
            objCC.ccInfoNumber = objEnc.EncryptData("colintastygochengnumber", txCardNumner.Text.Trim());
            objCC.ccInfoBName = objEnc.EncryptData("colintastygochengusername", txtFirstName.Text.Trim());
            objCC.ccInfoDEmail = ViewState["userName"].ToString();
            string[] strUserName = txtFirstName.Text.Split(' ');
            objCC.ccInfoDFirstName = strUserName[0].ToString();
            if (strUserName.Length > 1)
            {
                objCC.ccInfoDLastName = strUserName[1].ToString();
            }
            objCC.createUserCCInfo();           
        }
        else
        {
            objCC = new BLLUserCCInfo();
            objCC.ccInfoID = Convert.ToInt64(ViewState["ccInfoID"].ToString());
            objCC.ccInfoBAddress = txtBillingAddress.Text.Trim();
            objCC.ccInfoBCity = txtCity.Text.Trim();
            objCC.ccInfoBPostalCode = txtPostalCode.Text.Trim();
            objCC.ccInfoBProvince = ddlState.SelectedValue.Trim();
            if (ViewState["userId"] != null && ViewState["userId"].ToString() != "")
            {
                objCC.createdBy = Convert.ToInt64(ViewState["userId"].ToString());
                objCC.userId = Convert.ToInt64(ViewState["userId"].ToString());
            }
            objCC.ccInfoCCVNumber = objEnc.EncryptData("colintastygochengccv", txtCCVNumber.Text.Trim());
            objCC.ccInfoEdate = objEnc.EncryptData("colintastygochengexpirydate", ddlMonth.SelectedValue.ToString() + "-" + ddlYear.SelectedValue.ToString());
            objCC.ccInfoNumber = objEnc.EncryptData("colintastygochengnumber", txCardNumner.Text.Trim());
            objCC.ccInfoBName = objEnc.EncryptData("colintastygochengusername", txtFirstName.Text.Trim());
            objCC.ccInfoDEmail = ViewState["userName"].ToString();
            string[] strUserName = txtFirstName.Text.Split(' ');
            objCC.ccInfoDFirstName = strUserName[0].ToString();
            if (strUserName.Length > 1)
            {
                objCC.ccInfoDLastName = strUserName[1].ToString();
            }
            objCC.updateUserCCInfoByID();           
        }     
        HideEditCustomer();
        TriggerClientGridRefresh();
        GridDataBind(Convert.ToInt64(ViewState["userId"].ToString()));
        if (ViewState["ccInfoID"]==null)
        {
            lblMessage.Text = "Record has been added successfully.";
            lblMessage.Visible = true;
            imgGridMessage.Visible = true;
            imgGridMessage.ImageUrl = "images/Checked.png";
        }
        else
        {
            lblMessage.Text = "Record has been updated successfully.";
            lblMessage.Visible = true;
            imgGridMessage.Visible = true;
            imgGridMessage.ImageUrl = "images/Checked.png";
        }

    }

    private void LoadDropDownList()
    {
        try
        {
            //Clears the Drop Down List
            this.ddlYear.Items.Clear();

            //Year
            for (int year = DateTime.Now.Year; year <= DateTime.Now.Year + 7; year++)
            {
                ddlYear.Items.Add(new ListItem(year.ToString(), year.ToString()));
            }
            this.ddlYear.SelectedValue = DateTime.Now.Year.ToString();
           
            DataTable dt = Misc.getProvincesByCountryID(2);
            ddlState.DataSource = dt.DefaultView;
            ddlState.DataTextField = "provinceName";
            ddlState.DataValueField = "provinceName";
            ddlState.DataBind();         
        }
        catch (Exception ex)
        {

        }
    }


    private void HideEditCustomer()
    {
        ClearEditCustomerForm();

        RegisterStartupScript("jsCloseDialg", "closeDialog();");
    }


    protected void btnCancel_Click(object sender, EventArgs e)
    {       
        ViewState["ccInfoID"] = null;
        HideEditCustomer();
    }


    protected void btnRefreshGrid_Click(object sender, EventArgs e)
    {
        GridDataBind(Convert.ToInt64(ViewState["userId"].ToString()));
    }

}
