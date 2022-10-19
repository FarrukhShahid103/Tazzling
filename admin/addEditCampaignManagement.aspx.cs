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
using GecLibrary;
using System.Text;
using System.IO;
using System.Net;
using iTextSharp.text.pdf;
using iTextSharp.text;
using iTextSharp.text.html;
using iTextSharp.text.html.simpleparser;
using System.Threading;
using System.Text.RegularExpressions;
using System.Xml;


public partial class addEditCampaignManagement : System.Web.UI.Page
{
    public string strIDs = "";
    public int start = 2;
    public int NewDealID;
    public static bool notExist = true;
    BLLDealOrders objOrders = new BLLDealOrders();
    
    protected void Page_Load(object sender, EventArgs e)
    {        
        if (!IsPostBack)
        {                        
            if (Session["user"] != null)
            {
                if ((Request.QueryString["Mode"] != null) && (Request.QueryString["resID"] != null))
                {
                    try
                    {
                        BindCategories();
                        if ((Request.QueryString["Mode"].ToString().Trim().ToLower() == "new") && (int.Parse(Request.QueryString["resID"].ToString()) > 0))
                        {

                        }
                        else if ((Request.QueryString["Mode"].ToString().Trim().ToLower() == "edit") && (int.Parse(Request.QueryString["resID"].ToString()) > 0) && (int.Parse(Request.QueryString["did"].ToString()) > 0))
                        {                           
                            GetAndShowDealInfoByDealId(Convert.ToInt64(Request.QueryString["did"].ToString()));
                            this.btnImgSave.ImageUrl = "~/admin/images/btnUpdate.jpg";

                            this.btnImgSave.ToolTip = "Update Deal Info";

                            this.hfDealId.Value = Request.QueryString["did"].ToString();

                            this.lblDealInfoHeading.Text = "Update Deal Info";

                            this.imgGridMessage.Visible = false;
                            this.lblMessage.Text = "";
                        }
                        else
                        {
                            Response.Redirect(ResolveUrl("~/admin/restaurantManagement.aspx"), true);
                        }
                    }
                    catch (Exception ex)
                    {                       
                        Response.Redirect(ResolveUrl("~/admin/restaurantManagement.aspx"), true);
                    }
                }
                else//If any one of "Mode" and "ResId" is Null then it will redirects you towards the Business Form
                {
                    Response.Redirect(ResolveUrl("~/admin/restaurantManagement.aspx"), true);
                }
            }
            else
            {
               
            }
        }

        if (ViewState["userID"] == null) { GetAndSetUserID(); }
    }

    protected void BindCategories()
    {
        BLLCategories objCategory = new BLLCategories();
        DataTable dtCategory = objCategory.getAllActiveCategoriesAndSubCategories();
        if (dtCategory != null && dtCategory.Rows.Count > 0)
        {
            ddlCategory.DataSource = dtCategory;
            ddlCategory.DataValueField = "categoryId";
            ddlCategory.DataTextField = "categoryName";
            ddlCategory.DataBind();
        }

    }

    private void GetAndShowDealInfoByDealId(long iDealId)
    {
        try
        {
            BLLCampaign objCampaign = new BLLCampaign();
            objCampaign.campaignID = iDealId;
            DataTable dtCampaign = objCampaign.getCampaignByCampaignId();
            if ((dtCampaign != null) && (dtCampaign.Rows.Count > 0))
            {                
                txtArrivalTime.Text = dtCampaign.Rows[0]["estimatedArivalTime"].ToString().Trim();
                txtCampaignQuote.Text = dtCampaign.Rows[0]["campaignQuote"].ToString().Trim();
                txtShipFromAddress.Text = dtCampaign.Rows[0]["shippingFromAddress"].ToString().Trim();
                txtShippFromCity.Text = dtCampaign.Rows[0]["shippingFromCity"].ToString().Trim();
                txtShippFromZipCode.Text = dtCampaign.Rows[0]["shippingFromZipCode"].ToString().Trim();
                txtShippingFromCountry.Text = dtCampaign.Rows[0]["shippingFromCountry"].ToString().Trim();
                txtShippingFromState.Text = dtCampaign.Rows[0]["shippingFromprovince"].ToString().Trim();
                txtTitle.Text = dtCampaign.Rows[0]["campaignTitle"].ToString().Trim();
                txtURL.Text = dtCampaign.Rows[0]["campaignURL"].ToString().Trim();
                ddlCategory.SelectedValue = dtCampaign.Rows[0]["campaignCategory"].ToString().Trim();
                txtDescription.Text = dtCampaign.Rows[0]["campaignDescription"].ToString().Trim();
                txtShortDescription.Text = dtCampaign.Rows[0]["campaignShortDescription"].ToString().Trim();
                this.ddlStatus.SelectedValue = DBNull.Value.Equals(dtCampaign.Rows[0]["isActive"]) ? "No" : (bool.Parse(dtCampaign.Rows[0]["isActive"].ToString()) == true ? "Yes" : "No");
                this.ddlFeatured.SelectedValue = DBNull.Value.Equals(dtCampaign.Rows[0]["isFeatured"]) ? "No" : (bool.Parse(dtCampaign.Rows[0]["isFeatured"].ToString()) == true ? "Yes" : "No");
                
                string strStartDate = dtCampaign.Rows[0]["campaignStartTime"].ToString().Trim();
                txtStartDate.Text = DateTime.Parse(strStartDate).ToString("MM-dd-yyyy");
                if (DateTime.Parse(strStartDate).Hour < 12)
                {
                    ddlDLStartHH.SelectedValue = DateTime.Parse(strStartDate).Hour.ToString();
                    ddlDLStartPortion.SelectedValue = "AM";
                }
                else
                {
                    ddlDLStartHH.SelectedValue = (DateTime.Parse(strStartDate).Hour - 12).ToString();
                    ddlDLStartPortion.SelectedValue = "PM";
                }
                ddlDLStartMM.SelectedValue = DateTime.Parse(strStartDate).Minute.ToString();

                string strEndDate = dtCampaign.Rows[0]["campaignEndTime"].ToString().Trim();
                txtEndDate.Text = DateTime.Parse(strEndDate).ToString("MM-dd-yyyy");
                if (DateTime.Parse(strEndDate).Hour < 12)
                {
                    ddlDLEndHH.SelectedValue = DateTime.Parse(strEndDate).Hour.ToString();
                    ddlDLEndPortion.SelectedValue = "AM";
                }
                else
                {
                    ddlDLEndHH.SelectedValue = (DateTime.Parse(strEndDate).Hour - 12).ToString();
                    ddlDLEndPortion.SelectedValue = "PM";
                }
                ddlDLEndMM.SelectedValue = DateTime.Parse(strEndDate).Minute.ToString();
                string path = AppDomain.CurrentDomain.BaseDirectory + "Images\\dealFood\\" + dtCampaign.Rows[0]["restaurantId"].ToString().Trim() + "\\" + dtCampaign.Rows[0]["campaignpicture"].ToString().Trim();
                if (File.Exists(path))
                {
                    this.rfvDealImage1.ValidationGroup = "";
                    this.imgUpload1.Src = "../Images/dealFood/" + dtCampaign.Rows[0]["restaurantId"].ToString().Trim() + "/" + dtCampaign.Rows[0]["campaignpicture"].ToString().Trim();
                    this.imgUpload1.Visible = true;
                }

                if (Convert.ToBoolean(dtCampaign.Rows[0]["shipUSA"].ToString().Trim()) && Convert.ToBoolean(dtCampaign.Rows[0]["shipCanada"].ToString().Trim()))
                {
                    rbBoth.Checked = true;
                }
                else if (Convert.ToBoolean(dtCampaign.Rows[0]["shipUSA"].ToString().Trim()))
                {
                    rbUSA.Checked = true;
                }
                else if (Convert.ToBoolean(dtCampaign.Rows[0]["shipCanada"].ToString().Trim()))
                {
                    rbCanada.Checked = true;
                }
            }
        }
        catch (Exception ex)
        {

        }
    }
   
    private void GetAndSetUserID()
    {
        try
        {
            DataTable dtUser = (DataTable)Session["user"];

            if ((dtUser != null) && (dtUser.Rows.Count > 0))
            {
                ViewState["userID"] = dtUser.Rows[0]["userID"];
            }
        }
        catch (Exception ex)
        {
            string strException = "There is an error occur, please email us at support@tazzling.com or call 1855-295-1771.";
        }
    }

    #region "Get All Deal Info & Fill the GridView"
    public string getImagePath(object resID, object imgName)
    {
        try
        {
            ArrayList arrImage = new ArrayList();
            arrImage.AddRange(imgName.ToString().Split(','));

            if (arrImage.Count > 0)
            {
                string strImageName = arrImage[0].ToString();

                string path = AppDomain.CurrentDomain.BaseDirectory + "Images\\dealFood\\" + resID.ToString() + "\\" + strImageName;
                if (File.Exists(path))
                {
                    return "../Images/dealFood/" + resID.ToString() + "/" + strImageName;
                }
                else
                {
                    return "../Images/dealFood/noMenuImage.gif";
                }
            }
        }
        catch (Exception ex)
        {
            lblMessage.Text = "There is an error occur, please email us at support@tazzling.com or call 1855-295-1771.";
            lblMessage.Visible = true;
            imgGridMessage.Visible = true;
            imgGridMessage.ImageUrl = "~/Images/error.png"; lblMessage.ForeColor = System.Drawing.Color.Red;
            return "";
        }
        return "";
    }
    #endregion

    protected void btnImgSave_Click(object sender, ImageClickEventArgs e)
    {
        int iResID = 0;
        string strImageName = "";
        try
        {
            int.TryParse(Request.QueryString["resID"].ToString().Trim(),out iResID);            
        }
        catch (Exception ex)
        { }
        //Save the Deal Info
        if (this.btnImgSave.ToolTip == "Save Deal Info")
        {

            if (fpDealImage1.HasFile)
            {
                strImageName = ImageUploadHere(fpDealImage1, iResID);
            }
            int compaignID = AddNewDealInfo(strImageName, iResID);
            Response.Redirect("campaignManagement.aspx?Res=Add&Mode=All&resID=" + Request.QueryString["resID"].ToString().Trim(), false);
        }
        //Update the Deal Info
        else if (this.btnImgSave.ToolTip == "Update Deal Info")
        {
            if (fpDealImage1.HasFile)
            {
                if (this.imgUpload1.Src.ToString().Length > 2)
                {
                    string strImgName = "";

                    strImgName = this.imgUpload1.Src.ToString().Substring(this.imgUpload1.Src.ToString().LastIndexOf("/") + 1, (this.imgUpload1.Src.ToString().Length - (this.imgUpload1.Src.ToString().LastIndexOf("/") + 1)));

                    string path = AppDomain.CurrentDomain.BaseDirectory + "Images\\dealFood\\" + iResID + "\\" + strImgName;

                    if (File.Exists(path))
                    {
                        try
                        {
                            this.imgUpload1.Src = "";
                            //Delete the File
                            File.Delete(path);
                        }
                        catch (Exception ex) { }
                    }
                }
                //upload the Image here
                strImageName = ImageUploadHere(fpDealImage1, iResID);
            }
            else
            {
                strImageName = this.imgUpload1.Src.ToString().Substring(this.imgUpload1.Src.ToString().LastIndexOf("/") + 1, (this.imgUpload1.Src.ToString().Length - (this.imgUpload1.Src.ToString().LastIndexOf("/") + 1)));
            }
            UpdateDealInfoByDealId(strImageName, iResID);
            Response.Redirect("campaignManagement.aspx?Res=Update&Mode=All&resID=" + Request.QueryString["resID"].ToString().Trim(), false);
        }
    }

    private string ImageUploadHere(FileUpload fileUploadDealImg, int strResID)
    {
        string strUniqueID = "";

        try
        {

            if (fileUploadDealImg.HasFile)
            {
                //string strResID = this.ddlSelectRes.SelectedItem.Value.Trim();

                string[] strExtension = fileUploadDealImg.FileName.Split('.');

                strUniqueID = Guid.NewGuid().ToString() + "." + strExtension[strExtension.Length - 1];

                string strthumbSave = AppDomain.CurrentDomain.BaseDirectory + "Images\\dealFood\\" + strResID + "\\";

                if (!Directory.Exists(strthumbSave))
                {
                    Directory.CreateDirectory(strthumbSave);
                }
                string filename = Path.GetFileName(fileUploadDealImg.PostedFile.FileName);
                string targetPath = AppDomain.CurrentDomain.BaseDirectory + "Images\\dealFood\\" + strResID + "\\" + strUniqueID;
                Stream strm = fileUploadDealImg.PostedFile.InputStream;
                var targetFile = targetPath;
                //Based on scalefactor image size will vary
                Misc.GenerateCampaignThumbnails(strm, targetFile, AppDomain.CurrentDomain.BaseDirectory + "Images\\dealFood\\" + strResID + "\\", strUniqueID);
            }
        }
        catch (Exception ex)
        {

        }

        return strUniqueID;
    }
      
    #region"Save and Update Campaign Info here"
    private int AddNewDealInfo(string strImageNames, int iResId)
    {
        try
        {
            BLLCampaign objCam = new BLLCampaign();
            objCam.campaignCategory = Convert.ToInt32(ddlCategory.SelectedValue.ToString());
            objCam.campaignDescription = txtDescription.Text.Trim();
            objCam.campaignShortDescription = txtShortDescription.Text.Trim();            
            objCam.campaignpicture = strImageNames.ToString();
            objCam.campaignQuote = txtCampaignQuote.Text.Trim();
            objCam.campaignSlot = 0;
            objCam.campaignTitle = txtTitle.Text.Trim();
            objCam.campaignURL = txtURL.Text.Trim();
            objCam.estimatedArivalTime = txtArrivalTime.Text.Trim();
            objCam.restaurantId = iResId;
            objCam.shippingFromAddress = txtShipFromAddress.Text.Trim();
            objCam.shippingFromCity = txtShippFromCity.Text.Trim();
            objCam.shippingFromCountry = txtShippingFromCountry.Text.Trim();
            objCam.shippingFromprovince = txtShippingFromState.Text.Trim();
            objCam.shippingFromZipCode = txtShippFromZipCode.Text.Trim();
            
            if (rbBoth.Checked)
            {
                objCam.shipCanada = true;
                objCam.shipUSA = true;
            }
            else if (rbUSA.Checked)
            {
                objCam.shipCanada = false;
                objCam.shipUSA = true;
            }
            else if (rbCanada.Checked)
            {
                objCam.shipCanada = true;
                objCam.shipUSA = false;
            }
            string strStartDate = "";
            string strEndDate = "";

            try
            {
                strStartDate = txtStartDate.Text.Trim() + " " + ((ddlDLStartPortion.SelectedItem.Text.Trim() == "PM") ? (int.Parse(ddlDLStartHH.SelectedItem.Text.Trim()) + 12).ToString() : ddlDLStartHH.SelectedItem.Text.Trim()).ToString() + ":" + ddlDLStartMM.SelectedItem.Text + ":" + "00";
            }
            catch (Exception ex)
            {
                strStartDate = DateTime.Now.ToString();
            }
            try
            {
                strEndDate = txtEndDate.Text.Trim() + " " + ((ddlDLEndPortion.SelectedItem.Text.Trim() == "PM") ? (int.Parse(ddlDLEndHH.SelectedItem.Text.Trim()) + 12).ToString() : ddlDLEndHH.SelectedItem.Text.Trim()).ToString() + ":" + ddlDLEndMM.SelectedItem.Text + ":" + "00";
            }
            catch (Exception ex)
            {
                strEndDate = DateTime.Now.ToString();
            }

            try
            {
                objCam.campaignStartTime = Convert.ToDateTime(strStartDate);
                objCam.campaignEndTime = Convert.ToDateTime(strEndDate);
            }
            catch (Exception ex)
            {
                objCam.campaignStartTime = DateTime.Now;
                objCam.campaignEndTime = DateTime.Now;
            }
            objCam.createdBy = Convert.ToInt64(ViewState["userID"]);// this.txtTitle.Text.Trim();
            objCam.creationDate = DateTime.Now;
            objCam.isActive = this.ddlStatus.SelectedValue == "Yes" ? true : false;
            objCam.isFeatured = this.ddlFeatured.SelectedValue == "Yes" ? true : false;            

            return objCam.createCampaign();
        }
        catch (Exception ex)
        {
            return 0;
        }
    }

    private void UpdateDealInfoByDealId(string strImageNames, int iResId)
    {
        try
        {
            BLLCampaign objCam = new BLLCampaign();
            objCam.campaignCategory = Convert.ToInt32(ddlCategory.SelectedValue.ToString());
            objCam.campaignID = Convert.ToInt64(Request.QueryString["did"].ToString().Trim());
            objCam.campaignDescription = txtDescription.Text.Trim();
            objCam.campaignShortDescription = txtShortDescription.Text.Trim();
            objCam.campaignpicture = strImageNames.ToString();
            objCam.campaignQuote = txtCampaignQuote.Text.Trim();            
            objCam.campaignTitle = txtTitle.Text.Trim();
            objCam.campaignURL = txtURL.Text.Trim();
            objCam.estimatedArivalTime = txtArrivalTime.Text.Trim();
            objCam.restaurantId = iResId;
            objCam.shippingFromAddress = txtShipFromAddress.Text.Trim();
            objCam.shippingFromCity = txtShippFromCity.Text.Trim();
            objCam.shippingFromCountry = txtShippingFromCountry.Text.Trim();
            objCam.shippingFromprovince = txtShippingFromState.Text.Trim();
            objCam.shippingFromZipCode = txtShippFromZipCode.Text.Trim();

            if (rbBoth.Checked)
            {
                objCam.shipCanada = true;
                objCam.shipUSA = true;
            }
            else if (rbUSA.Checked)
            {
                objCam.shipCanada = false;
                objCam.shipUSA = true;
            }
            else if (rbCanada.Checked)
            {
                objCam.shipCanada = true;
                objCam.shipUSA = false;
            }
            string strStartDate = "";
            string strEndDate = "";

            try
            {
                strStartDate = txtStartDate.Text.Trim() + " " + ((ddlDLStartPortion.SelectedItem.Text.Trim() == "PM") ? (int.Parse(ddlDLStartHH.SelectedItem.Text.Trim()) + 12).ToString() : ddlDLStartHH.SelectedItem.Text.Trim()).ToString() + ":" + ddlDLStartMM.SelectedItem.Text + ":" + "00";
            }
            catch (Exception ex)
            {
                strStartDate = DateTime.Now.ToString();
            }
            try
            {
                strEndDate = txtEndDate.Text.Trim() + " " + ((ddlDLEndPortion.SelectedItem.Text.Trim() == "PM") ? (int.Parse(ddlDLEndHH.SelectedItem.Text.Trim()) + 12).ToString() : ddlDLEndHH.SelectedItem.Text.Trim()).ToString() + ":" + ddlDLEndMM.SelectedItem.Text + ":" + "00";
            }
            catch (Exception ex)
            {
                strEndDate = DateTime.Now.ToString();
            }

            try
            {
                objCam.campaignStartTime = Convert.ToDateTime(strStartDate);
                objCam.campaignEndTime = Convert.ToDateTime(strEndDate);
            }
            catch (Exception ex)
            {
                objCam.campaignStartTime = DateTime.Now;
                objCam.campaignEndTime = DateTime.Now;
            }
            objCam.modifiedBy = Convert.ToInt64(ViewState["userID"]);// this.txtTitle.Text.Trim();
            objCam.modifiedDate = DateTime.Now;
            objCam.isActive = this.ddlStatus.SelectedValue == "Yes" ? true : false;
            objCam.isFeatured = this.ddlFeatured.SelectedValue == "Yes" ? true : false;            
            objCam.updateCampaign();
        }
        catch (Exception ex)
        {
            
        }
    }    
    #endregion

    
    #region Add SubDeal Funcitons

    protected void btnImgCancel_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            if (Request.QueryString["Mode"] != null && Request.QueryString["Mode"].ToString().Trim().ToLower() == "new")
            {
                Response.Redirect("restaurantManagement.aspx", true);
            }
            else if (Request.QueryString["resID"] != null && Request.QueryString["resID"].ToString().Trim() != "")
            {
                Response.Redirect("campaignManagement.aspx?Mode=All&resID=" + Request.QueryString["resID"].ToString().Trim(), true);
            }
            else
            {
                Response.Redirect("restaurantManagement.aspx", true);
            }
        }
        catch (Exception ex)
        {
            string strException = "There is an error occur, please email us at support@tazzling.com or call 1855-295-1771.";
        }
    }
  
    #endregion

  
}