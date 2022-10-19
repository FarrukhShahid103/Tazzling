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


public partial class addEditProductManagement : System.Web.UI.Page
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
                        if ((Request.QueryString["Mode"].ToString().Trim().ToLower() == "new") && (int.Parse(Request.QueryString["resID"].ToString()) > 0) && (int.Parse(Request.QueryString["cid"].ToString()) > 0))
                        {
                            if (int.Parse(Request.QueryString["resID"].ToString()) != 31)
                            {
                                ddlTracking.SelectedIndex = 2;
                            }
                        }
                        else if ((Request.QueryString["Mode"].ToString().Trim().ToLower() == "edit") && (int.Parse(Request.QueryString["resID"].ToString()) > 0) && (int.Parse(Request.QueryString["cid"].ToString()) > 0) && (int.Parse(Request.QueryString["did"].ToString()) > 0))
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

    protected void GridView2_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (cbEnableSize.Checked)
            {
                RequiredFieldValidator7.Enabled = false;
            }
            else
            {
                RequiredFieldValidator7.Enabled = true;
            }
            BLLProductProperties objProduct = new BLLProductProperties();
            if (hfDealId.Value != "")
            {
                objProduct.productPropertiesID = Convert.ToInt64(GridView2.SelectedDataKey.Value);
                hfProductPropertiesId.Value = GridView2.SelectedDataKey.Value.ToString();
                DataTable dtProductProperty = objProduct.getProductPropertiesByPropertiesID();
                if ((dtProductProperty != null) && (dtProductProperty.Rows.Count > 0))
                {
                    this.txtProductLabel.Text = dtProductProperty.Rows[0]["propertiesLabel"].ToString().Trim();
                    this.txtProductLabelDescription.Text = dtProductProperty.Rows[0]["propertiesDescription"].ToString().Trim();
                }
            }
            else
            {
                int intRow = Convert.ToInt32(GridView2.SelectedDataKey.Value);
                DataTable dtProductProperty = (DataTable)ViewState["dtProductProperty"];
                if (dtProductProperty != null && dtProductProperty.Rows.Count > 0)
                {
                    this.txtProductLabel.Text = DBNull.Value.Equals(dtProductProperty.Rows[intRow]["propertiesLabel"]) ? "" : dtProductProperty.Rows[intRow]["propertiesLabel"].ToString().Trim();
                    this.txtProductLabelDescription.Text = DBNull.Value.Equals(dtProductProperty.Rows[intRow]["propertiesDescription"]) ? "" : dtProductProperty.Rows[intRow]["propertiesDescription"].ToString().Trim();
                }
                hfProductPropertiesId.Value = intRow.ToString();
            }
            btnAddProductSize.ImageUrl = "~/admin/images/btnUpdate.jpg";
        }
        catch (Exception ex)
        {

        }
    }

    protected void GridView2_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int result = 0;
        try
        {
            if (cbEnableSize.Checked)
            {
                RequiredFieldValidator7.Enabled = false;
            }
            else
            {
                RequiredFieldValidator7.Enabled = true;
            }
            BLLProductProperties objProduct = new BLLProductProperties();
            if (hfDealId.Value != "")
            {
                Label lblProductPropertiesID = (Label)GridView2.Rows[e.RowIndex].FindControl("lblProductPropertiesID");
                if (lblProductPropertiesID != null)
                {
                    objProduct.productPropertiesID = Convert.ToInt32(lblProductPropertiesID.Text.Trim());
                    result = objProduct.deleteProductProperties();
                }
                BindProductPropertiesGrid(Convert.ToInt64(hfDealId.Value.Trim()));
            }
            else
            {
                DataTable dtProductProperty = (DataTable)ViewState["dtProductProperty"];
                if (dtProductProperty != null && dtProductProperty.Rows.Count > 0)
                {
                    dtProductProperty.Rows[e.RowIndex].Delete();
                }
                for (int i = 0; i < dtProductProperty.Rows.Count; i++)
                {
                    dtProductProperty.Rows[i]["productPropertiesID"] = i;
                }
                GridView2.DataSource = dtProductProperty.DefaultView;
                GridView2.DataBind();
                ViewState["dtProductProperty"] = dtProductProperty;
            }
        }
        catch (Exception ex)
        {

        }
    }

    protected void btnProductProperties_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            if (cbEnableSize.Checked)
            {
                RequiredFieldValidator7.Enabled = false;
            }
            else
            {
                RequiredFieldValidator7.Enabled = true;
            }
            
            if (hfDealId.Value == "")
            {
                if (hfProductPropertiesId.Value == "")
                {
                    if (ViewState["dtProductProperty"] == null)
                    {
                        DataTable dtProductProperty = new DataTable("dtProductProperty");
                        DataColumn productPropertiesID = new DataColumn("productPropertiesID");
                        DataColumn propertiesLabel = new DataColumn("propertiesLabel");
                        DataColumn propertiesDescription = new DataColumn("propertiesDescription");

                        dtProductProperty.Columns.Add(productPropertiesID);
                        dtProductProperty.Columns.Add(propertiesLabel);
                        dtProductProperty.Columns.Add(propertiesDescription);
                        DataRow dRow;
                        dRow = dtProductProperty.NewRow();
                        dRow["productPropertiesID"] = "0";
                        dRow["propertiesLabel"] = txtProductLabel.Text.Trim();
                        dRow["propertiesDescription"] = txtProductLabelDescription.Text.Trim();
                        dtProductProperty.Rows.Add(dRow);
                        GridView2.DataSource = dtProductProperty.DefaultView;
                        GridView2.DataBind();
                        ViewState["dtProductProperty"] = dtProductProperty;
                    }
                    else
                    {
                        DataTable dtProductProperty = (DataTable)ViewState["dtProductProperty"];
                        if (dtProductProperty != null)
                        {
                            DataRow dRow;
                            dRow = dtProductProperty.NewRow();
                            dRow["productPropertiesID"] = dtProductProperty.Rows.Count;
                            dRow["propertiesLabel"] = txtProductLabel.Text.Trim();
                            dRow["propertiesDescription"] = txtProductLabelDescription.Text.Trim();
                            dtProductProperty.Rows.Add(dRow);
                            GridView2.DataSource = dtProductProperty.DefaultView;
                            GridView2.DataBind();
                            ViewState["dtProductProperty"] = dtProductProperty;
                        }
                    }
                }
                else
                {
                    DataTable dtProductProperty = (DataTable)ViewState["dtProductProperty"];
                    int intEditRow = 0;
                    Int32.TryParse(hfProductPropertiesId.Value, out intEditRow);
                    dtProductProperty.Rows[intEditRow]["propertiesLabel"] = txtProductLabel.Text.Trim();
                    dtProductProperty.Rows[intEditRow]["propertiesDescription"] = txtProductLabelDescription.Text.Trim();

                    GridView2.DataSource = dtProductProperty.DefaultView;
                    GridView2.DataBind();
                    ViewState["dtProductProperty"] = dtProductProperty;
                }
            }
            else
            {
                BLLProductProperties objSize = new BLLProductProperties();
                if (hfProductPropertiesId.Value == "")
                {
                    objSize.productID = Convert.ToInt64(this.hfDealId.Value.Trim());
                    objSize.propertiesLabel = txtProductLabel.Text.Trim();
                    objSize.propertiesDescription = txtProductLabelDescription.Text.Trim();
                    objSize.createProductProperties();
                }
                else
                {
                    objSize.productPropertiesID = Convert.ToInt64(this.hfProductPropertiesId.Value.Trim());
                    objSize.productID = Convert.ToInt64(this.hfDealId.Value.Trim());
                    objSize.propertiesLabel = txtProductLabel.Text.Trim();
                    objSize.propertiesDescription = txtProductLabelDescription.Text.Trim();
                    objSize.updateProductProperties();
                }
                BindProductPropertiesGrid(Convert.ToInt64(hfDealId.Value.Trim()));
            }
        }
        catch (Exception ex)
        { }
        btnAddProductSize.ImageUrl = "~/admin/images/btnSave.jpg";
        hfProductPropertiesId.Value = "";
        txtProductLabel.Text = "";
        txtProductLabelDescription.Text = "";
    }

    private void BindProductPropertiesGrid(long productID)
    {
        try
        {
            BLLProductProperties objSize = new BLLProductProperties();
            objSize.productID = productID;
            DataTable dtProductSize = objSize.getProductPropertiesByProductID();
            GridView2.DataSource = dtProductSize.DefaultView;
            GridView2.DataBind();
        }
        catch (Exception ex)
        { }
    }
    
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (cbEnableSize.Checked)
            {
                RequiredFieldValidator7.Enabled = false;
            }
            else
            {
                RequiredFieldValidator7.Enabled = true;
            }
            BLLProductSize objProduct = new BLLProductSize();
            if (hfDealId.Value != "")
            {
                objProduct.sizeID = Convert.ToInt64(GridView1.SelectedDataKey.Value);
                hfProductSize.Value = GridView1.SelectedDataKey.Value.ToString();
                DataTable dtProductSize = objProduct.getProductSizeBySizeID();
                if ((dtProductSize != null) && (dtProductSize.Rows.Count > 0))
                {
                    this.txtProductSize.Text = dtProductSize.Rows[0]["sizeText"].ToString().Trim();
                    this.txtProductSizeQuantity.Text = dtProductSize.Rows[0]["quantity"].ToString().Trim();
                }
            }
            else
            {
                int intRow = Convert.ToInt32(GridView1.SelectedDataKey.Value);
                DataTable dtProductSize = (DataTable)ViewState["dtProductSize"];
                if (dtProductSize != null && dtProductSize.Rows.Count > 0)
                {
                    this.txtProductSize.Text = DBNull.Value.Equals(dtProductSize.Rows[intRow]["sizeText"]) ? "" : dtProductSize.Rows[intRow]["sizeText"].ToString().Trim();
                    this.txtProductSizeQuantity.Text = DBNull.Value.Equals(dtProductSize.Rows[intRow]["quantity"]) ? "" : dtProductSize.Rows[intRow]["quantity"].ToString().Trim();
                }
                hfProductSize.Value = intRow.ToString();
            }
            btnAddProductSize.ImageUrl = "~/admin/images/btnUpdate.jpg";
        }
        catch (Exception ex)
        {
           
        }
    }

    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int result = 0;
        try
        {
            if (cbEnableSize.Checked)
            {
                RequiredFieldValidator7.Enabled = false;
            }
            else
            {
                RequiredFieldValidator7.Enabled = true;
            }

            BLLProductSize objProduct = new BLLProductSize();
            if (hfDealId.Value != "")
            {                
                Label lblSizeID = (Label)GridView1.Rows[e.RowIndex].FindControl("lblSizeID");
                if (lblSizeID != null)
                {
                    objProduct.sizeID = Convert.ToInt32(lblSizeID.Text.Trim());
                    result = objProduct.deleteProductSize();
                }
                BindProductSizeGrid(Convert.ToInt64(hfDealId.Value.Trim()));                
            }
            else
            {
                DataTable dtProductSize = (DataTable)ViewState["dtProductSize"];
                if (dtProductSize != null && dtProductSize.Rows.Count > 0)
                {
                    dtProductSize.Rows[e.RowIndex].Delete();
                }
                for (int i = 0; i < dtProductSize.Rows.Count; i++)
                {
                    dtProductSize.Rows[i]["sizeID"] = i;
                }
                GridView1.DataSource = dtProductSize.DefaultView;
                GridView1.DataBind();
                ViewState["dtProductSize"] = dtProductSize;
            }
        }
        catch (Exception ex)
        {
          
        }
    }

    protected void btnAddProductSize_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            if (cbEnableSize.Checked)
            {
                RequiredFieldValidator7.Enabled = false;
            }
            else
            {
                RequiredFieldValidator7.Enabled = true;
            }
            if (hfDealId.Value == "")
            {
                if (hfProductSize.Value == "")
                {
                    if (ViewState["dtProductSize"] == null)
                    {
                        DataTable dtProductSize = new DataTable("dtProductSize");
                        DataColumn sizeID = new DataColumn("sizeID");
                        DataColumn sizeText = new DataColumn("sizeText");
                        DataColumn quantity = new DataColumn("quantity");
                        
                        dtProductSize.Columns.Add(sizeID);
                        dtProductSize.Columns.Add(sizeText);
                        dtProductSize.Columns.Add(quantity);  
                        DataRow dRow;
                        dRow = dtProductSize.NewRow();
                        dRow["sizeID"] = "0";                        
                        dRow["sizeText"] = txtProductSize.Text.Trim();
                        dRow["quantity"] = txtProductSizeQuantity.Text.Trim();
                        dtProductSize.Rows.Add(dRow);
                        GridView1.DataSource = dtProductSize.DefaultView;
                        GridView1.DataBind();
                        ViewState["dtProductSize"] = dtProductSize;
                    }
                    else
                    {
                        DataTable dtProductSize = (DataTable)ViewState["dtProductSize"];
                        if (dtProductSize != null)
                        {
                            DataRow dRow;
                            dRow = dtProductSize.NewRow();
                            dRow["sizeID"] = dtProductSize.Rows.Count;                            
                            dRow["sizeText"] = txtProductSize.Text.Trim();
                            dRow["quantity"] = txtProductSizeQuantity.Text.Trim();
                            dtProductSize.Rows.Add(dRow);
                            GridView1.DataSource = dtProductSize.DefaultView;
                            GridView1.DataBind();
                            ViewState["dtProductSize"] = dtProductSize;
                        }
                    }
                }
                else
                {
                    DataTable dtProductSize = (DataTable)ViewState["dtProductSize"];
                    int intEditRow = 0;
                    Int32.TryParse(hfProductSize.Value, out intEditRow);
                    dtProductSize.Rows[intEditRow]["sizeText"] = txtProductSize.Text.Trim();
                    dtProductSize.Rows[intEditRow]["quantity"] = txtProductSizeQuantity.Text.Trim();
                    
                    GridView1.DataSource = dtProductSize.DefaultView;
                    GridView1.DataBind();
                    ViewState["dtProductSize"] = dtProductSize;                    
                }
            }
            else
            {
                BLLProductSize objSize = new BLLProductSize();
                if (hfProductSize.Value == "")
                {
                    objSize.productID = Convert.ToInt64(this.hfDealId.Value.Trim());
                    objSize.sizeText = txtProductSize.Text.Trim();
                    objSize.quantity = Convert.ToInt32(txtProductSizeQuantity.Text.Trim());
                    objSize.createProductSize();
                }
                else
                {
                    objSize.sizeID = Convert.ToInt64(this.hfProductSize.Value.Trim());
                    objSize.productID = Convert.ToInt64(this.hfDealId.Value.Trim());
                    objSize.sizeText = txtProductSize.Text.Trim();
                    objSize.quantity = Convert.ToInt32(txtProductSizeQuantity.Text.Trim());
                    objSize.updateProductSize();
                }
                BindProductSizeGrid(Convert.ToInt64(hfDealId.Value.Trim()));
            }
        }
        catch (Exception ex)
        { }
        btnAddProductSize.ImageUrl = "~/admin/images/btnSave.jpg";
        hfProductSize.Value = "";
        txtProductSize.Text = "";
        txtProductSizeQuantity.Text = "";
    }

    private void BindProductSizeGrid(long productID)
    {
        try
        {
            BLLProductSize objSize = new BLLProductSize();
            objSize.productID = productID;
            DataTable dtProductSize = objSize.getProductSizeByProductID();
            GridView1.DataSource = dtProductSize.DefaultView;
            GridView1.DataBind();
        }
        catch (Exception ex)
        { }
    }

    private void GetAndShowDealInfoByDealId(long iDealId)
    {
        try
        {
            BLLProducts objProducts = new BLLProducts();
            objProducts.productID = iDealId;
            DataTable dtProduct = objProducts.getProductsByProductID();
            if ((dtProduct != null) && (dtProduct.Rows.Count > 0))
            {
                
                                
                this.ddlStatus.SelectedValue = DBNull.Value.Equals(dtProduct.Rows[0]["isActive"]) ? "No" : (bool.Parse(dtProduct.Rows[0]["isActive"].ToString()) == true ? "Yes" : "No");

                this.ddlVoucherProduct.SelectedValue = DBNull.Value.Equals(dtProduct.Rows[0]["isVoucherProduct"]) ? "No" : (bool.Parse(dtProduct.Rows[0]["isVoucherProduct"].ToString()) == true ? "Yes" : "No");

                this.ddlTracking.SelectedValue = DBNull.Value.Equals(dtProduct.Rows[0]["tracking"]) ? "No" : (bool.Parse(dtProduct.Rows[0]["tracking"].ToString()) == true ? "Yes" : "No");                

                

                txtDescription.Text = dtProduct.Rows[0]["description"].ToString().Trim();

                txtWeight.Text = dtProduct.Rows[0]["weight"].ToString().Trim();
                txtWidth.Text = dtProduct.Rows[0]["height"].ToString().Trim();
                txtHeight.Text = dtProduct.Rows[0]["width"].ToString().Trim();
                txtDimension.Text = dtProduct.Rows[0]["dimension"].ToString().Trim();
                               
                
                txtMaxOrders.Text = dtProduct.Rows[0]["maxOrdersPerUser"].ToString().Trim();
                txtMaxQty.Text = dtProduct.Rows[0]["maxQty"].ToString().Trim();
                
                               
                //txtMinQty.Text = dtProduct.Rows[0]["minQty"].ToString().Trim();
                
                txtOurComission.Text = dtProduct.Rows[0]["OurCommission"].ToString().Trim();
                txtReturnPolicy.Text = dtProduct.Rows[0]["returnPolicy"].ToString().Trim();
                txtDealPrice.Text = dtProduct.Rows[0]["sellingPrice"].ToString().Trim();

                txtShippingInfo.Text = dtProduct.Rows[0]["shippingInfo"].ToString().Trim();
                txtShortDescription.Text = dtProduct.Rows[0]["shortDescription"].ToString().Trim();
                
                txtSubTitle.Text = dtProduct.Rows[0]["subTitle"].ToString().Trim();
                txtTitle.Text = dtProduct.Rows[0]["title"].ToString().Trim();
                txtActualPrice.Text = dtProduct.Rows[0]["valuePrice"].ToString().Trim();

                if (Convert.ToBoolean(dtProduct.Rows[0]["enableSize"].ToString()))
                {
                    cbEnableSize.Checked = true;
                    RequiredFieldValidator7.Enabled = false;
                }
                else
                {
                    RequiredFieldValidator7.Enabled = true;
                }

                BindProductSizeGrid(Convert.ToInt64(dtProduct.Rows[0]["productID"].ToString().Trim()));
                BindProductPropertiesGrid(Convert.ToInt64(dtProduct.Rows[0]["productID"].ToString().Trim())); 


                string strImageNames = dtProduct.Rows[0]["images"].ToString().Trim();

                ArrayList arrImages = new ArrayList();
                arrImages.AddRange(strImageNames.Split(','));
                string strRedID = Request.QueryString["resID"].ToString().Trim();

                if (arrImages.Count > 0)
                {
                    string strImageName = arrImages[0].ToString();

                    string path = AppDomain.CurrentDomain.BaseDirectory + "Images\\dealFood\\" + strRedID + "\\" + strImageName;

                    if (File.Exists(path))
                    {
                        this.rfvDealImage1.ValidationGroup = "";

                        //Set the First Image here
                        this.imgUpload1.Src = "../Images/dealFood/" + strRedID + "/" + strImageName;
                        this.imgUpload1.Visible = true;
                    }
                }
                else
                {
                    imgUpload1.Src = "";
                }

                if (arrImages.Count > 1)
                {
                    string strImageName = arrImages[1].ToString();

                    string path = AppDomain.CurrentDomain.BaseDirectory + "Images\\dealFood\\" + strRedID + "\\" + strImageName;

                    if (File.Exists(path))
                    {
                        //Set the Second Image here
                        this.imgUpload2.Src = "../Images/dealFood/" + strRedID + "/" + strImageName;
                        this.imgUpload2.Visible = true;
                        imgUpload2Remove.Visible = true;
                    }
                }
                else
                {
                    imgUpload2.Src = "";
                }

                if (arrImages.Count > 2)
                {
                    string strImageName = arrImages[2].ToString();

                    string path = AppDomain.CurrentDomain.BaseDirectory + "Images\\dealFood\\" + strRedID + "\\" + strImageName;

                    if (File.Exists(path))
                    {
                        //Set the Third Image here
                        this.imgUpload3.Src = "../Images/dealFood/" + strRedID + "/" + strImageName;
                        this.imgUpload3.Visible = true;
                        imgUpload3Remove.Visible = true;
                    }
                }
                else
                {
                    imgUpload3.Src = "";
                }

                //objProduct. = ;
                //objProduct. = cbMedium.Checked; 
                //objProduct. = cbEnableSize.Checked;
                // objProduct. = cbLarge.Checked;
                //objProduct. = cbXL.Checked;
                //objProduct. = cbXXL.Checked;            

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
        if (cbEnableSize.Checked)
        {
            RequiredFieldValidator7.Enabled = false;
            if (!(GridView1.Rows.Count > 0))
            {
                lblMessage.Text = "Please add product size.";
                lblMessage.Visible = true;
                lblMessage.ForeColor = System.Drawing.Color.Red;
                imgGridMessage.Visible = true;
                imgGridMessage.ImageUrl = "images/error.png";
                return;
            }
        }

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
            //If Image 2 exists
            if (fpDealImage2.HasFile)
            {
                //upload the Image here
                strImageName += "," + ImageUploadHere(fpDealImage2, iResID);
            }

            //If Image 3 exists
            if (fpDealImage3.HasFile)
            {
                //upload the Image here
                strImageName += "," + ImageUploadHere(fpDealImage3, iResID);
            }

            long compaignID = AddNewDealInfo(strImageName, iResID);

            if (ViewState["dtProductSize"] != null)
            {
                DataTable dtProductSize = (DataTable)ViewState["dtProductSize"];
                if (dtProductSize != null && dtProductSize.Rows.Count > 0)
                {
                    BLLProductSize objProductSize = new BLLProductSize();
                    for (int i = 0; i < dtProductSize.Rows.Count; i++)
                    {
                        objProductSize.sizeText = dtProductSize.Rows[i]["sizeText"].ToString().Trim();
                        try
                        {
                            objProductSize.quantity = Convert.ToInt32(dtProductSize.Rows[i]["quantity"].ToString().Trim());
                        }
                        catch (Exception ex)
                        {}
                        objProductSize.productID = compaignID;
                        objProductSize.createProductSize();
                    }
                }
            }

            if (ViewState["dtProductProperty"] != null)
            {
                DataTable dtProductProperty = (DataTable)ViewState["dtProductProperty"];
                if (dtProductProperty != null && dtProductProperty.Rows.Count > 0)
                {
                    BLLProductProperties objProductProperties = new BLLProductProperties();
                    for (int i = 0; i < dtProductProperty.Rows.Count; i++)
                    {
                        objProductProperties.propertiesDescription = dtProductProperty.Rows[i]["propertiesDescription"].ToString().Trim();
                        objProductProperties.propertiesLabel = dtProductProperty.Rows[i]["propertiesLabel"].ToString().Trim();
                        objProductProperties.productID = compaignID;
                        objProductProperties.createProductProperties();
                    }
                }
            }

            Response.Redirect("productManagement.aspx?Res=Add&cid=" + Request.QueryString["cid"].ToString().Trim(), false);

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

                    string path = AppDomain.CurrentDomain.BaseDirectory + "Images\\dealFood\\" + hfResturnatID.Value.Trim() + "\\" + strImgName;

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
            //If Image 2 exists
            if (fpDealImage2.HasFile)
            {
                if (this.imgUpload2.Src.ToString().Length > 2)
                {
                    string strImgName = "";

                    strImgName = this.imgUpload2.Src.ToString().Substring(this.imgUpload2.Src.ToString().LastIndexOf("/") + 1, (this.imgUpload2.Src.ToString().Length - (this.imgUpload2.Src.ToString().LastIndexOf("/") + 1)));

                    string path = AppDomain.CurrentDomain.BaseDirectory + "Images\\dealFood\\" + hfResturnatID.Value.Trim() + "\\" + strImgName;

                    if (File.Exists(path))
                    {
                        try
                        {
                            this.imgUpload2.Src = "";

                            //Delete the File
                            File.Delete(path);
                        }
                        catch (Exception ex) { }
                    }
                }
                //upload the Image here
                strImageName += "," + ImageUploadHere(fpDealImage2, iResID);
            }
            else
            {
                //upload the Image here
                strImageName += "," + this.imgUpload2.Src.ToString().Substring(this.imgUpload2.Src.ToString().LastIndexOf("/") + 1, (this.imgUpload2.Src.ToString().Length - (this.imgUpload2.Src.ToString().LastIndexOf("/") + 1)));
            }

            //If Image 3 exists
            if (fpDealImage3.HasFile)
            {
                if (this.imgUpload3.Src.ToString().Length > 2)
                {
                    string strImgName = "";

                    strImgName = this.imgUpload3.Src.ToString().Substring(this.imgUpload3.Src.ToString().LastIndexOf("/") + 1, (this.imgUpload3.Src.ToString().Length - (this.imgUpload3.Src.ToString().LastIndexOf("/") + 1)));

                    string path = AppDomain.CurrentDomain.BaseDirectory + "Images\\dealFood\\" + hfResturnatID.Value.Trim() + "\\" + strImgName;

                    if (File.Exists(path))
                    {
                        try
                        {
                            this.imgUpload3.Src = "";

                            //Delete the File
                            File.Delete(path);
                        }
                        catch (Exception ex) { }
                    }
                }

                //upload the Image here
                strImageName += "," + ImageUploadHere(fpDealImage3, iResID);
            }
            else
            {
                //upload the Image here
                strImageName += "," + this.imgUpload3.Src.ToString().Substring(this.imgUpload3.Src.ToString().LastIndexOf("/") + 1, (this.imgUpload3.Src.ToString().Length - (this.imgUpload3.Src.ToString().LastIndexOf("/") + 1)));
            }
            UpdateDealInfoByDealId(strImageName, iResID);
            Response.Redirect("productManagement.aspx?Res=Update&cid=" + Request.QueryString["cid"].ToString().Trim(), false);
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

                string strthumbSave = AppDomain.CurrentDomain.BaseDirectory + "Images\\dealFood\\" + strResID + "\\ ";

                if (!Directory.Exists(strthumbSave))
                {
                    Directory.CreateDirectory(strthumbSave);
                }
                string filename = Path.GetFileName(fileUploadDealImg.PostedFile.FileName);
                string targetPath = AppDomain.CurrentDomain.BaseDirectory + "Images\\dealFood\\" + strResID + "\\" + strUniqueID;
                Stream strm = fileUploadDealImg.PostedFile.InputStream;
                var targetFile = targetPath;
                //Based on scalefactor image size will vary
                Misc.GenerateThumbnails(strm, targetFile, AppDomain.CurrentDomain.BaseDirectory + "Images\\dealFood\\" + strResID + "\\", strUniqueID);
            }
        }
        catch (Exception ex)
        {

        }

        return strUniqueID;
    }
      
    #region"Save and Update Campaign Info here"
    private long AddNewDealInfo(string strImageNames, int iResId)
    {
        try
        {
            BLLProducts objProduct = new BLLProducts();
            
            objProduct.images = strImageNames;
            objProduct.campaignID = Convert.ToInt64(Request.QueryString["cid"].Trim());
            
            objProduct.createdBy = Convert.ToInt64(ViewState["userID"]);// this.txtTitle.Text.Trim();
            objProduct.createdDate = DateTime.Now;
            objProduct.isActive = this.ddlStatus.SelectedValue == "Yes" ? true : false;
            objProduct.isVoucherProduct = this.ddlVoucherProduct.SelectedValue == "Yes" ? true : false;
            objProduct.tracking = this.ddlTracking.SelectedValue == "Yes" ? true : false;
                        

            objProduct.description = txtDescription.Text.Trim();
            
            objProduct.enableSize = cbEnableSize.Checked;
            
            objProduct.maxOrdersPerUser = Convert.ToInt32(txtMaxOrders.Text.Trim());
            if (cbEnableSize.Checked && txtMaxQty.Text.Trim() == "")
            {
                objProduct.maxQty = 0;
            }
            else
            {
                objProduct.maxQty = Convert.ToInt32(txtMaxQty.Text.Trim());
            }

            objProduct.weight= txtWeight.Text.Trim();
            objProduct.width= txtWidth.Text.Trim();
            objProduct.height = txtHeight.Text.Trim();
            objProduct.dimension= txtDimension.Text.Trim(); 
            
            objProduct.minOrdersPerUser = 0;
            objProduct.minQty = 1;
            
            objProduct.OurCommission = float.Parse(txtOurComission.Text.Trim());
            objProduct.returnPolicy = txtReturnPolicy.Text.Trim();
            objProduct.sellingPrice = float.Parse(txtDealPrice.Text.Trim());
            objProduct.shippingInfo = txtShippingInfo.Text.Trim();
            objProduct.shortDescription = txtShortDescription.Text.Trim();
            
            objProduct.subTitle = txtSubTitle.Text.Trim();
            objProduct.title = txtTitle.Text.Trim();
            objProduct.valuePrice = float.Parse(txtActualPrice.Text.Trim());
            return objProduct.createProduct();
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
            BLLProducts objProduct = new BLLProducts();
            
            objProduct.images = strImageNames;
            objProduct.campaignID = Convert.ToInt64(Request.QueryString["cid"].Trim());
            objProduct.productID = Convert.ToInt64(Request.QueryString["did"].Trim());

            objProduct.weight = txtWeight.Text.Trim();
            objProduct.width = txtWidth.Text.Trim();
            objProduct.height = txtHeight.Text.Trim();
            objProduct.dimension = txtDimension.Text.Trim(); 
            
            objProduct.modifiedBy = Convert.ToInt64(ViewState["userID"]);// this.txtTitle.Text.Trim();
            objProduct.modifiedDate = DateTime.Now;
            objProduct.isActive = this.ddlStatus.SelectedValue == "Yes" ? true : false;
            objProduct.isVoucherProduct = this.ddlVoucherProduct.SelectedValue == "Yes" ? true : false;
            objProduct.tracking = this.ddlTracking.SelectedValue == "Yes" ? true : false;
            
            objProduct.description = txtDescription.Text.Trim();
            
            objProduct.enableSize = cbEnableSize.Checked;
            
            
            
            objProduct.maxOrdersPerUser = Convert.ToInt32(txtMaxOrders.Text.Trim());
            //objProduct.maxQty = Convert.ToInt32(txtMaxQty.Text.Trim());
            if (cbEnableSize.Checked && txtMaxQty.Text.Trim() == "")
            {
                objProduct.maxQty = 0;
            }
            else
            {
                objProduct.maxQty = Convert.ToInt32(txtMaxQty.Text.Trim());
            }
            
            
            objProduct.minOrdersPerUser = 0;
            objProduct.minQty = 1;
            
            objProduct.OurCommission = float.Parse(txtOurComission.Text.Trim());
            objProduct.returnPolicy = txtReturnPolicy.Text.Trim();
            objProduct.sellingPrice = float.Parse(txtDealPrice.Text.Trim());
            objProduct.shippingInfo = txtShippingInfo.Text.Trim();
            objProduct.shortDescription = txtShortDescription.Text.Trim();
            
            objProduct.subTitle = txtSubTitle.Text.Trim();
            objProduct.title = txtTitle.Text.Trim();
            objProduct.valuePrice = float.Parse(txtActualPrice.Text.Trim());
            
            objProduct.updateProduct();
        }
        catch (Exception ex)
        {
            
        }
    }    
    #endregion

    protected void imgUpload2Remove_Click(object sender, EventArgs e)
    {
        try
        {
            if (cbEnableSize.Checked)
            {
                RequiredFieldValidator7.Enabled = false;
            }
            imgUpload2.Src = "";
            imgUpload2.Visible = false;
            imgUpload2Remove.Visible = false;
        }
        catch (Exception ex)
        { }
    }

    protected void imgUpload3Remove_Click(object sender, EventArgs e)
    {
        try
        {
            if (cbEnableSize.Checked)
            {
                RequiredFieldValidator7.Enabled = false;
            }
            imgUpload3.Src = "";
            imgUpload3.Visible = false;
            imgUpload3Remove.Visible = false;
        }
        catch (Exception ex)
        { }
    }
    
    #region Add SubDeal Funcitons
    protected void btnImgCancel_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            if (Request.QueryString["Mode"] != null && Request.QueryString["Mode"].ToString().Trim().ToLower() == "new")
            {
                Response.Redirect("campaignManagement.aspx?Mode=All&resID=" + Request.QueryString["resID"].ToString().Trim(), true);
            }
            else if (Request.QueryString["resID"] != null && Request.QueryString["resID"].ToString().Trim() != "")
            {
                Response.Redirect("productManagement.aspx?Mode=All&cid=" + Request.QueryString["cid"].ToString().Trim(), true);
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