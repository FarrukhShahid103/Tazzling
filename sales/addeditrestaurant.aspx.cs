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

public partial class addeditrestaurant : System.Web.UI.Page
{
    BLLRestaurantLeads objLead = new BLLRestaurantLeads();
    BLLRestaurantLeadComments objComments = new BLLRestaurantLeadComments();
    BLLUser objUser = new BLLUser();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                bindProvince();
                DataTable dtuser = null;
                if (Session["salesSection"] != null)
                {
                    dtuser = (DataTable)Session["salesSection"];
                }
                if (dtuser != null && dtuser.Rows.Count > 0)
                {
                    ViewState["userID"] = dtuser.Rows[0]["userID"].ToString();
                }

                if (Request.QueryString["rid"] != null && Request.QueryString["rid"].ToString() != "")
                {
                    btnUpdate.ImageUrl = "~/sales/images/btnUpdate.jpg";
                    objLead = new BLLRestaurantLeads();
                    objLead.restaurantLeadID = Convert.ToInt64(Request.QueryString["rid"].ToString());
                    DataTable dtLeadinfo = objLead.getRestaurantLeadsByID();
                    if (dtLeadinfo != null && dtLeadinfo.Rows.Count > 0)
                    {
                        txtComment.Text = "";

                        string[] strRestaurantPhoneNumber = dtLeadinfo.Rows[0]["restaurantLeadPhoneNumber"].ToString().Split('-');
                        if (strRestaurantPhoneNumber.Length == 3)
                        {
                            txtPhone1.Text = strRestaurantPhoneNumber[0].ToString();
                            txtPhone2.Text = strRestaurantPhoneNumber[1].ToString();
                            txtPhone3.Text = strRestaurantPhoneNumber[2].ToString();
                        }
                        string[] strRestaurantOwnerPhoneNumber = dtLeadinfo.Rows[0]["restaurantLeadOwnerPhoneNumber"].ToString().Split('-');
                        if (strRestaurantOwnerPhoneNumber.Length == 3)
                        {
                            //txtPhoneRO1.Text = strRestaurantOwnerPhoneNumber[0].ToString();
                            //txtPhoneRO2.Text = strRestaurantOwnerPhoneNumber[1].ToString();
                            //txtPhoneRO3.Text = strRestaurantOwnerPhoneNumber[2].ToString();
                        }
                        if (dtLeadinfo.Rows[0]["priority"] != null && dtLeadinfo.Rows[0]["priority"].ToString().Trim() != "")
                        {
                            cbPriority.Checked = Convert.ToBoolean(dtLeadinfo.Rows[0]["priority"].ToString());
                        }
                        txtResAddress.Text = dtLeadinfo.Rows[0]["restaurantLeadAddress"].ToString();
                        txtResName.Text = dtLeadinfo.Rows[0]["restaurantLeadName"].ToString();
                        txtResOwnerName.Text = dtLeadinfo.Rows[0]["restaurantLeadOwnerName"].ToString();
                        txtAssignTo.Text = dtLeadinfo.Rows[0]["restaurantAssignto"].ToString();
                        ddlStatus.SelectedValue = dtLeadinfo.Rows[0]["restaurantLeadStatus"].ToString();
                        try
                        {
                            if (dtLeadinfo.Rows[0]["provinceId"] != null && dtLeadinfo.Rows[0]["provinceId"].ToString().Trim() != "")
                            {
                                ddlState.SelectedValue = dtLeadinfo.Rows[0]["provinceId"].ToString();
                                ddlState_SelectedIndexChanged(sender, e);
                                if (ddCity.Items.Count > 0)
                                {
                                    if (dtLeadinfo.Rows[0]["cityId"] != null && dtLeadinfo.Rows[0]["cityId"].ToString().Trim() != "")
                                    {
                                        ddCity.SelectedValue = dtLeadinfo.Rows[0]["cityId"].ToString().Trim();
                                    }
                                }
                            }
                        }
                        catch (Exception ex)
                        { }
                        if (ddlStatus.SelectedValue.ToString() == "Successfully Signuped")
                        {
                            RequiredFieldValidator2.Enabled = true;
                            trLoginID.Visible = true;
                        }
                        objComments = new BLLRestaurantLeadComments();
                        objComments.restaurantLeadID = Convert.ToInt64(Request.QueryString["rid"].ToString());
                        DataTable dtComments = objComments.getRestaurantLeadCommentsByLeadID();
                        if (dtComments != null && dtComments.Rows.Count > 0)
                        {
                            pageGrid.DataSource = dtComments.DefaultView;
                            pageGrid.DataBind();
                        }

                        txtLoginID.Text = dtLeadinfo.Rows[0]["restaurantLeadSignUpID"].ToString();
                        if (dtLeadinfo.Rows[0]["restaurantLeadSignUpID"] != null && dtLeadinfo.Rows[0]["restaurantLeadSignUpID"].ToString() != "")
                        {
                            objLead.restaurantLeadSignUpID = dtLeadinfo.Rows[0]["restaurantLeadSignUpID"].ToString();
                            DataTable dtRestInfo = objLead.getRestaurantByUserName();
                            if (dtRestInfo != null && dtRestInfo.Rows.Count > 0)
                            {
                                trResturantInfo.Visible = true;
                                lblCity.Text = dtRestInfo.Rows[0]["city"].ToString();
                                lblEmail.Text = dtRestInfo.Rows[0]["email"].ToString();
                                lblPhoneSignUp.Text = dtRestInfo.Rows[0]["phone"].ToString();
                                lblRestaurantAddress.Text = dtRestInfo.Rows[0]["restaurantAddress"].ToString();
                                lblRestaurantBusinessName.Text = dtRestInfo.Rows[0]["restaurantBusinessName"].ToString();
                                lblRestaurantName.Text = dtRestInfo.Rows[0]["restaurantName"].ToString();
                                lblZipCode.Text = dtRestInfo.Rows[0]["zipCode"].ToString();

                            }
                            else
                            {
                                trResturantInfo.Visible = false;
                            }
                        }
                        if (dtLeadinfo.Rows[0]["createdBy"].ToString() != ViewState["userID"].ToString() && ViewState["userID"].ToString().Trim()!="1")
                        {
                            makeReadOnly();
                        }

                        objUser = new BLLUser();
                        objUser.userId = Convert.ToInt32(dtLeadinfo.Rows[0]["createdBy"].ToString().Trim());
                        DataTable dtUser = objUser.getUserByID();

                        if (dtUser.Rows[0]["userTypeID"].ToString().Trim() == "1" || dtUser.Rows[0]["userTypeID"].ToString().Trim() == "2")
                        {
                            RemoveReadOnly();
                        }

                        if (dtuser.Rows[0]["userTypeID"].ToString().Trim() == "5")
                        {
                            txtAssignTo.ReadOnly = true;
                        }
                    }
                }
                else
                {
                    if (dtuser.Rows[0]["userTypeID"].ToString().Trim() == "5")
                    {
                        txtAssignTo.ReadOnly = true;
                    }
                    btnUpdate.ImageUrl = "~/sales/images/btnSave.jpg";
                }
            }
        }
        catch (Exception ex)
        {
            lblMessage.Text = "There is an error occur, please email us at support@tazzling.com or call 1855-295-1771.";
            lblMessage.Visible = true;
            imgGridMessage.Visible = true;
            imgGridMessage.ImageUrl = "images/error.png";
            lblMessage.ForeColor = System.Drawing.Color.Red;
        }
    }


    protected void bindProvince()
    {
        try
        {
            ddlState.DataSource = Misc.getProvincesByCountryID(2).DefaultView;
            ddlState.DataTextField = "provinceName";
            ddlState.DataValueField = "provinceId";
            ddlState.DataBind();
            ddlState.Items.Insert(0, "Please Select");
        }
        catch (Exception ex)
        {
            lblMessage.Text = "There is an error occur while bind Provinces Please contact you technical support";
            lblMessage.Visible = true;
            imgGridMessage.Visible = true;
            imgGridMessage.ImageUrl = "images/error.png";
            lblMessage.ForeColor = System.Drawing.Color.Red;
        }
    }

    protected void makeReadOnly()
    {
        try
        {
            txtResName.ReadOnly=true;
            txtResOwnerName.ReadOnly=true;
            //txtPhoneRO1.ReadOnly=true;
            //txtPhoneRO2.ReadOnly = true;
            //txtPhoneRO3.ReadOnly = true;
            ddlState.Enabled = false;
            ddCity.Enabled = false;
            cbPriority.Enabled = false;
            txtPhone1.ReadOnly = true;
            txtPhone2.ReadOnly = true;
            txtPhone3.ReadOnly = true;
            txtLoginID.ReadOnly=true;
            ddlStatus.Enabled = false;            
            txtResAddress.ReadOnly=true;
            txtAssignTo.ReadOnly = true;
        }
        catch (Exception ex)
        {
 
        }
    }


    protected void RemoveReadOnly()
    {
        try
        {
            txtResName.ReadOnly = false;
            txtResOwnerName.ReadOnly = false;
            //txtPhoneRO1.ReadOnly = false;
            //txtPhoneRO2.ReadOnly = false;
            //txtPhoneRO3.ReadOnly = false;
            ddlState.Enabled = true;
            ddCity.Enabled = true;
            cbPriority.Enabled = true;
            txtPhone1.ReadOnly = false;
            txtPhone2.ReadOnly = false;
            txtPhone3.ReadOnly = false;
            txtLoginID.ReadOnly = false;
            ddlStatus.Enabled = true;
            txtResAddress.ReadOnly = false;
            txtAssignTo.ReadOnly = false;
        }
        catch (Exception ex)
        {

        }
    }


    protected string GetDateString(object objDate)
    {
        if (objDate.ToString() != "")
        {
            DateTime dt = Convert.ToDateTime(objDate);
            return dt.ToString("MM-dd-yyyy H.mm tt");
        }
        return "";
    }
       
    protected void btnUpdate_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            if (Request.QueryString["rid"] != null && Request.QueryString["rid"].ToString() != "")
            {
                //Update Functionallity
                objLead = new BLLRestaurantLeads();
                if (ViewState["userID"] != null && ViewState["userID"].ToString() != "")
                {
                    objLead.modifiedBy = Convert.ToInt64(ViewState["userID"].ToString());
                }
                objLead.restaurantLeadName = txtResName.Text.Trim();
                objLead.restaurantLeadOwnerName = txtResOwnerName.Text.Trim();
                objLead.priority = cbPriority.Checked;
               // objLead.restaurantLeadOwnerPhoneNumber = txtPhoneRO1.Text.Trim() + "-" + txtPhoneRO2.Text.Trim() + "-" + txtPhoneRO3.Text.Trim();
                objLead.restaurantLeadPhoneNumber = txtPhone1.Text.Trim() + "-" + txtPhone2.Text.Trim() + "-" + txtPhone3.Text.Trim();
                objLead.restaurantLeadSignUpID = txtLoginID.Text.Trim();
                objLead.provinceId = Convert.ToInt64(ddlState.SelectedValue.ToString());
                if (ddCity.Items.Count > 0)
                {
                    objLead.cityId = Convert.ToInt64(ddCity.SelectedValue.ToString());
                }
                else
                {
                    objLead.cityId = 0;
                }
                if (txtLoginID.Text.Trim() != "")
                {
                    objUser = new BLLUser();
                    objUser.userName = txtLoginID.Text.Trim();
                    DataTable dtRest = objUser.getUserRestaurantIDbyUserName();
                    if (dtRest != null && dtRest.Rows.Count == 0)
                    {
                        lblMessage.Text = "Restaurant with user id " + txtLoginID.Text.Trim() + " does not exist.";
                        lblMessage.Visible = true;
                        imgGridMessage.Visible = true;
                        imgGridMessage.ImageUrl = "images/error.png";
                        lblMessage.ForeColor = System.Drawing.Color.Red;
                        return;
                    }
                }
                objLead.restaurantLeadStatus = ddlStatus.SelectedValue.ToString();
                objLead.restaurantLeadAddress = txtResAddress.Text.Trim();
                objLead.restaurantLeadID= Convert.ToInt64(Request.QueryString["rid"].ToString());
                
                objLead.restaurantAssignto = txtAssignTo.Text.Trim();
                objLead.updateRestaurantLeads();
                if (txtComment.Text.Trim() != "")
                {
                    objComments = new BLLRestaurantLeadComments();
                    if (ViewState["userID"] != null && ViewState["userID"].ToString() != "")
                    {
                        objComments.createdBy = Convert.ToInt64(ViewState["userID"].ToString());
                    }
                    objComments.restaurantLeadComment = txtComment.Text.Trim();
                    objComments.restaurantLeadID = Convert.ToInt64(Request.QueryString["rid"].ToString());
                    objComments.createRestaurantLeadComments();
                }


                Response.Redirect("CloseForm.aspx", true);
            }
            else
            {
                //Add New Functionallity
                objLead = new BLLRestaurantLeads();
                if (ViewState["userID"] != null && ViewState["userID"].ToString() != "")
                {
                    objLead.createdBy = Convert.ToInt64(ViewState["userID"].ToString());
                }
                objLead.restaurantLeadName = txtResName.Text.Trim();
                objLead.restaurantLeadOwnerName = txtResOwnerName.Text.Trim();
                objLead.priority = cbPriority.Checked;
               // objLead.restaurantLeadOwnerPhoneNumber = txtPhoneRO1.Text.Trim() + "-" + txtPhoneRO2.Text.Trim() + "-" + txtPhoneRO3.Text.Trim();
                objLead.restaurantLeadPhoneNumber = txtPhone1.Text.Trim() + "-" + txtPhone2.Text.Trim() + "-" + txtPhone3.Text.Trim();
                objLead.provinceId = Convert.ToInt64(ddlState.SelectedValue.ToString());
                if (ddCity.Items.Count > 0)
                {
                    objLead.cityId = Convert.ToInt64(ddCity.SelectedValue.ToString());
                }
                else
                {
                    objLead.cityId = 0;
                }
                if (txtLoginID.Text.Trim() != "")
                {
                    objUser = new BLLUser();
                    objUser.userName = txtLoginID.Text.Trim();
                    DataTable dtRest=objUser.getUserRestaurantIDbyUserName();
                    if (dtRest!=null && dtRest.Rows.Count==0)
                    {
                        lblMessage.Text = "Restaurant with user id " + txtLoginID.Text.Trim() + " does not exist.";
                        lblMessage.Visible = true;
                        imgGridMessage.Visible = true;
                        imgGridMessage.ImageUrl = "images/error.png";
                        lblMessage.ForeColor = System.Drawing.Color.Red;
                        return;
                    }
                }
                objLead.restaurantLeadSignUpID = txtLoginID.Text.Trim();
                objLead.restaurantLeadStatus = ddlStatus.SelectedValue.ToString();
                objLead.restaurantLeadAddress = txtResAddress.Text.Trim();
                objLead.restaurantAssignto = txtAssignTo.Text.Trim();
                long createdID = objLead.createRestaurantLeads();
                if (createdID > 0)
                {
                    if (txtComment.Text.Trim() != "")
                    {
                        objComments = new BLLRestaurantLeadComments();
                        if (ViewState["userID"] != null && ViewState["userID"].ToString() != "")
                        {
                            objComments.createdBy = Convert.ToInt64(ViewState["userID"].ToString());
                        }
                        objComments.restaurantLeadComment = txtComment.Text.Trim();
                        objComments.restaurantLeadID = createdID;
                        objComments.createRestaurantLeadComments();
                    }

                    Response.Redirect("CloseForm.aspx", true);
                }
                else
                {
                    lblMessage.Text = "Some problem occour while saving information. Please contact with support.";
                    lblMessage.Visible = true;
                    imgGridMessage.Visible = true;
                    imgGridMessage.ImageUrl = "images/error.png";
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                }

            }
        }
        catch (Exception ex)
        {      
            lblMessage.Text = ex.ToString();
            lblMessage.Visible = true;
            imgGridMessage.Visible = true;
            imgGridMessage.ImageUrl = "images/error.png";
            lblMessage.ForeColor = System.Drawing.Color.Red;
        }
    }   
 
    protected void ddlStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlStatus.SelectedValue.ToString() == "Successfully Signuped")
            {
                RequiredFieldValidator2.Enabled = true;
                trLoginID.Visible = true;
            }
            else
            {
                if (!txtLoginID.ReadOnly)
                {
                    txtLoginID.Text = "";
                }
                RequiredFieldValidator2.Enabled = false;
                trLoginID.Visible = false;
            }
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.ToString();
            lblMessage.Visible = true;
            imgGridMessage.Visible = true;
            imgGridMessage.ImageUrl = "images/error.png";
            lblMessage.ForeColor = System.Drawing.Color.Red;
        }
    }
    protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlState.SelectedIndex == 0)
            {
                trCity.Visible = false;
                ddCity.Items.Clear();
                ddCity.DataSource = null;
                ddCity.DataBind();
            }
            else
            {
                DataTable dtCities = Misc.getCitiesByProvinceID(Convert.ToInt32(ddlState.SelectedValue.ToString()));
                if (dtCities != null && dtCities.Rows.Count > 0)
                {
                    trCity.Visible = true;
                    ddCity.DataSource = dtCities;
                    ddCity.DataTextField = "cityName";
                    ddCity.DataValueField = "cityId";
                    ddCity.DataBind();
                }
                else
                {
                    trCity.Visible = false;
                    ddCity.Items.Clear();
                    ddCity.DataSource = null;
                    ddCity.DataBind();
                }

            }
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.ToString();
            lblMessage.Visible = true;
            imgGridMessage.Visible = true;
            imgGridMessage.ImageUrl = "images/error.png";
            lblMessage.ForeColor = System.Drawing.Color.Red;
        }
    }
}
