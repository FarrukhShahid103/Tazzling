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
using System.IO;
using System.Text;
using System.Net;
using System.Net.Mail;
using ExactTargetAPI;
public partial class EditContract : System.Web.UI.Page
{

    BLLContractDtail objcontract = new BLLContractDtail();
    public string strImageName = "";
    public string strIDs = "";
    public int start = 2;
    public string strtblHide = "none";
    public string strRestHide = "none";
    public string strGoogleAddress = "";
    protected void Page_Load(object sender, EventArgs e)
    {

        if(!IsPostBack)
        {
            if (Request.QueryString["contractId"] != null && Request.QueryString["contractId"].ToString().Trim() != "")
            {
                DataTable dtUsercheck = (DataTable)Session["user"];

                if (dtUsercheck.Rows[0]["userTypeID"].ToString() == "4")
                {


                    DataTable dtTemp = Misc.search("SELECT     contractDetail.userId FROM contractDetail INNER JOIN userInfo ON contractDetail.userId = userInfo.userId where userinfo.userid=" + dtUsercheck.Rows[0]["userid"].ToString().Trim() + " and contractDetail.restaurantId=" + Request.QueryString["resid"].ToString().Trim());
                    if (dtTemp != null && dtTemp.Rows.Count == 0)
                    {
                        Response.Redirect(ResolveUrl("~/contract/restaurantManagement.aspx"));
                    }
                }
                objcontract.contractid = Convert.ToInt16(Request.QueryString["contractId"].ToString().Trim());
                DataTable dtcontractDetail = null;
                dtcontractDetail = objcontract.GetCintractDetailByResId();
                if (dtcontractDetail != null && dtcontractDetail.Rows.Count > 0)
                {

                    txtHeight.Text = dtcontractDetail.Rows[0]["height"].ToString().Trim();
                    txtItemName.Text = dtcontractDetail.Rows[0]["itemName"].ToString().Trim();
                    txtLength.Text = dtcontractDetail.Rows[0]["length"].ToString().Trim();
                    txtPrice.Text = dtcontractDetail.Rows[0]["Price"].ToString().Trim();
                    
                    txtWeight.Text = dtcontractDetail.Rows[0]["weight"].ToString().Trim();
                    txtWidth.Text = dtcontractDetail.Rows[0]["width"].ToString().Trim();
                    this.imgUpload1.Src = "../Images/createContract/" + dtcontractDetail.Rows[0]["image"].ToString().Trim();
                    this.imgUpload1.Visible = true;
                    rfvDealImage1.ValidationGroup = "";

                }
            }


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
  
  

  

    private string ImageUploadHere(FileUpload fileUploadDealImg)
    {
        string strUniqueID = "";

        try
        {

            if (fileUploadDealImg.HasFile)
            {

                string[] strExtension = fileUploadDealImg.FileName.Split('.');

                strUniqueID = Guid.NewGuid().ToString() + "." + strExtension[strExtension.Length - 1];

                string strSrcPath = AppDomain.CurrentDomain.BaseDirectory + "Images\\createContract\\" + fileUploadDealImg.FileName;

                fileUploadDealImg.SaveAs(strSrcPath);

                string strthumbSave = AppDomain.CurrentDomain.BaseDirectory + "Images\\createContract\\";

                if (!Directory.Exists(strthumbSave))
                {
                    Directory.CreateDirectory(strthumbSave);
                }

                string SrcFileName = fileUploadDealImg.FileName;

                Misc.CreateThumbnailForBusinessOwner(strSrcPath, strthumbSave, SrcFileName, strUniqueID);

                File.Delete(strSrcPath);
            }
        }
        catch (Exception ex)
        {
            
        }

        return strUniqueID;
    }




    protected void btnSave_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            objcontract.length = txtLength.Text.ToString().Trim();
            objcontract.itemName = txtItemName.Text.ToString().Trim();
            objcontract.weight = txtWeight.Text.ToString().Trim();
            objcontract.width = txtWidth.Text.ToString().Trim();
            objcontract.price = txtPrice.Text.ToString().Trim();
            objcontract.haight = txtHeight.Text.ToString().Trim();
            DataTable dtUsercheck = (DataTable)Session["user"];
            objcontract.userID = Convert.ToInt16(dtUsercheck.Rows[0]["userID"].ToString().Trim());
            objcontract.contractid = Convert.ToInt16(Request.QueryString["contractId"].ToString().Trim());
            if (fpBusinessImg.HasFile)
            {
                //upload the Image here
                strImageName = ImageUploadHere(fpBusinessImg);
                objcontract.image = strImageName;
            }
            else
            {
                objcontract.contractid = Convert.ToInt16(Request.QueryString["contractId"].ToString().Trim());
                DataTable dtcontractDetail = null;
                dtcontractDetail = objcontract.GetCintractDetailByResId();
                if (dtcontractDetail != null && dtcontractDetail.Rows.Count > 0)
                {
                    strImageName = dtcontractDetail.Rows[0]["image"].ToString().Trim();
                    objcontract.image = strImageName;
                }
            }
          


            int updateContract = objcontract.UpdateContractDetail();
            lblAddressError.Text = "Contract has been updated successfully.";
            lblAddressError.Visible = true;
            ImgAddError.Visible = true;
            ImgAddError.ImageUrl = "images/Checked.png"; lblAddressError.ForeColor = System.Drawing.Color.Black;

        }
        catch (Exception ex)
        {

            lblAddressError.Text = ex.ToString();
            lblAddressError.Visible = true;
            ImgAddError.Visible = true;
            ImgAddError.ImageUrl = "images/error.png";
            lblAddressError.ForeColor = System.Drawing.Color.Red;
        }


    }



    
}