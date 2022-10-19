﻿using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.IO;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using AjaxControlToolkit;
using System.Text.RegularExpressions;
using ICSharpCode.SharpZipLib.Zip;
using ICSharpCode.SharpZipLib.Core;

public partial class addmenuitems : System.Web.UI.Page
{
    BLLRestaurantMenu obj = new BLLRestaurantMenu();
    BLLRestaurantMenuItems objItems = new BLLRestaurantMenuItems();
    BLLSubMenuItem objSubItem = new BLLSubMenuItem();
    public int strButton1 = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                if (Session["user"] != null)
                {
                    if (Request.QueryString["CID"] != null && Request.QueryString["CID"] != "" &&
                        Request.QueryString["CN"] != null && Request.QueryString["CN"] != "")
                    {
                        DataTable dtUser = (DataTable)Session["user"];
                        ViewState["userID"] = dtUser.Rows[0]["userID"];
                        ViewState["userName"] = dtUser.Rows[0]["userName"];
                        ViewState["CuisineName"] = Request.QueryString["CN"].ToString();
                        ViewState["CuisineID"] = Request.QueryString["CID"].ToString();
                    }
                    else
                    {
                        Response.Redirect(ResolveUrl("~/admin/controlpanel.aspx"));
                    }
                }
                else
                {
                    Response.Redirect(ResolveUrl("~/admin/default.aspx"));
                }
                BindMenu();
            }
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.ToString();
            lblMessage.Visible = true;
            imgGridMessage.Visible = true;
            imgGridMessage.ImageUrl = "images/error.png"; lblMessage.ForeColor = System.Drawing.Color.Red;
        }
    }

    private void BindMenu()
    {
        try
        {
            DataTable dtMenu;
            obj.cuisineID = Convert.ToInt64(ViewState["CuisineID"]);
            dtMenu = obj.getRestaurantMenuByCuisineID();
            gridViewMenus.DataSource = dtMenu.DefaultView;
            gridViewMenus.DataBind();
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.ToString();
            lblMessage.Visible = true;
            imgGridMessage.Visible = true;
            imgGridMessage.ImageUrl = "images/error.png"; lblMessage.ForeColor = System.Drawing.Color.Red;
        }
    }

    protected void gridViewMenus_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
    {

        LinkButton Source = e.CommandSource as LinkButton;
        LinkButton Edit = Source.Parent.FindControl("lbEdit") as LinkButton;
        LinkButton Update = Source.Parent.FindControl("lbUpdate") as LinkButton;
        LinkButton Cancel = Source.Parent.FindControl("lbCancel") as LinkButton;
        LinkButton Delete = Source.Parent.FindControl("lbDelete") as LinkButton;
        Label lblFoodType = Source.Parent.FindControl("lblFoodType") as Label;
        Label lblFoodTypeId = Source.Parent.FindControl("lblFoodTypeId") as Label;
        TextBox txtFoodType = Source.Parent.FindControl("txtFoodType") as TextBox;

        Edit.Visible = true;
        Update.Visible = true;
        Cancel.Visible = true;
        Delete.Visible = true;
        lblFoodType.Visible = true;
        txtFoodType.Visible = true;

        switch (e.CommandName)
        {
            case "ToEdit":
                Edit.Visible = false;
                Delete.Visible = false;
                lblFoodType.Visible = false;
                break;
            case "ToUpdate":
                Update.Visible = false;
                Cancel.Visible = false;
                txtFoodType.Visible = false;
                UpdateMenuInfo(txtFoodType.Text, lblFoodTypeId.Text);
                break;
            case "ToCancel":
                Update.Visible = false;
                Cancel.Visible = false;
                txtFoodType.Visible = false;
                break;
            case "ToDelete":
                DeleteMenus(lblFoodTypeId.Text);
                break;
        }
    }
    private void UpdateMenuInfo(string foodType, string foodTypeId)
    {
        obj.foodTypeId = Convert.ToInt64(foodTypeId);
        obj.foodType = foodType;
        obj.cuisineID = Convert.ToInt64(ViewState["CuisineID"]);
        obj.updateRestaurantMenu();

        BindMenu();
        lblMessage.Text ="Item updated successfully.";
        lblMessage.Visible = true;
        imgGridMessage.Visible = true;
        imgGridMessage.ImageUrl = "images/Checked.png"; lblMessage.ForeColor = System.Drawing.Color.Black;
    }

    private void DeleteMenus(string foodTypeId)
    {
        obj.foodTypeId = Convert.ToInt64(foodTypeId);
        obj.deleteRestaurantMenu();
        BindMenu();
        lblMessage.Text = "Item deleted successfully.";
        lblMessage.Visible = true;
        imgGridMessage.Visible = true;
        imgGridMessage.ImageUrl = "images/Checked.png"; lblMessage.ForeColor = System.Drawing.Color.Black;
    }


    protected void gridViewMenus_CancelCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
    {
        gridViewMenus.ShowFooter = true;
        gridViewMenus.EditItemIndex = -1;
        BindMenu();
    }

    protected void gridViewMenus_EditCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
    {
        //lblError.Text = String.Empty;
        gridViewMenus.ShowFooter = false;
        gridViewMenus.EditItemIndex = e.Item.ItemIndex;
        BindMenu();
        Label lblCustomer = (Label)e.Item.FindControl("lblCustomer");
        Label lblCountry = (Label)e.Item.FindControl("lblCountry");
        if (lblCustomer != null && lblCountry != null)
        {
            this.ViewState["Customer"] = lblCustomer.Text.Trim();
            this.ViewState["Country"] = lblCountry.Text.Trim();
        }
    }

    protected void gridViewMenus_UpdateCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
    {
        //lblError.Text = String.Empty;
        if (ListItemType.EditItem == e.Item.ItemType)
        {
            Label lblFoodTypeID1 = (Label)e.Item.FindControl("lblFoodTypeID1");
            String customerid = lblFoodTypeID1.Text.Trim();
            TextBox txtCustomer = (TextBox)e.Item.FindControl("txtCustomer");
            FileUpload fuMenuImage = (FileUpload)e.Item.FindControl("fuMenuImage");
            if (txtCustomer != null && fuMenuImage != null)
            {
                if (txtCustomer.Text.Trim() == String.Empty)
                {
                    return;
                }

                if (fuMenuImage.FileName.Trim() == String.Empty)
                {
                    return;
                }

                String updateData = "UPDATE custs set CompanyName = '" + txtCustomer.Text.Trim() + "', Country = '" + fuMenuImage.FileName.Trim() + "' WHERE CustomerID = '" + lblFoodTypeID1.Text.Trim() + "'";


                gridViewMenus.EditItemIndex = -1;
                gridViewMenus.ShowFooter = true;
                BindMenu();
                lblMessage.Text = "Item updated successfully.";
                lblMessage.Visible = true;
                imgGridMessage.Visible = true;
                imgGridMessage.ImageUrl = "images/Checked.png"; lblMessage.ForeColor = System.Drawing.Color.Black;
            }
        }
    }

    protected void gridViewMenus_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
    {
        Control container = e.Item;
        ListItemType itemType = e.Item.ItemType;

        if (itemType == ListItemType.Item || itemType == ListItemType.Footer || itemType == ListItemType.AlternatingItem)
        {
            if (e.Item.DataItem == null)
            {
                return;
            }

            HtmlImage btnExpandButton = (HtmlImage)container.FindControl("image_");
            if (btnExpandButton != null)
            {
                btnExpandButton.Attributes.Add("OnClick", "Toggle('ctl00_ContentPlaceHolder1_gridViewMenus_ctl0" + (e.Item.ItemIndex + 2) + "_divOrders', 'ctl00_ContentPlaceHolder1_gridViewMenus_ctl0" + (e.Item.ItemIndex + 2) + "_image_');");
            }
            System.Web.UI.WebControls.LinkButton lbDelete;
            lbDelete = (System.Web.UI.WebControls.LinkButton)e.Item.FindControl("lbDelete");
            if (lbDelete != null)
            {
                lbDelete.Attributes.Add("onclick", "return confirm('Are you sure you want to delete?')");
            }

            DataGrid gridViewItems = (DataGrid)container.FindControl("gridViewItems");

            String customerid = String.Empty;
            if (null != gridViewItems)
            {
                Label lblFoodTypeID1 = (Label)e.Item.FindControl("lblFoodTypeID1");
                customerid = lblFoodTypeID1.Text.Trim();

                Button btnMenuID = (Button)container.FindControl("btnMenuID");
                if (lblFoodTypeID1 != null)
                {
                    this.ViewState["foodTypeID"] = lblFoodTypeID1.Text.Trim();
                }

                objItems.foodTypeId = Convert.ToInt64(lblFoodTypeID1.Text.ToString());
                DataTable data = objItems.getRestaurantMenuItemsByFoodTypeID();
                gridViewItems.DataSource = data;
                gridViewItems.DataBind();
            }
        }
    }

    protected void gridViewItems_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
    {
        Control container = e.Item;
        ListItemType itemType = e.Item.ItemType;

        if (itemType == ListItemType.Item || itemType == ListItemType.AlternatingItem)
        {
            if (e.Item.DataItem == null)
            {
                return;
            }
        }
        else if (itemType == ListItemType.Footer)
        {

            if (this.ViewState["foodTypeID"] != null)
            {
                ImageButton btnAddMenuItem = (ImageButton)e.Item.FindControl("btnAddMenuItem");

                TextBox txtAddItemName = (TextBox)e.Item.FindControl("txtAddItemName");
                TextBox txtAddDescription = (TextBox)e.Item.FindControl("txtAddDescription");
                TextBox txtAddItemSubName = (TextBox)e.Item.FindControl("txtAddItemSubName");
                TextBox txtAddItemPrice = (TextBox)e.Item.FindControl("txtAddItemPrice");

                string strArguments = "'" + txtAddItemName.ClientID + "','" + txtAddDescription.ClientID + "','"
                    + txtAddItemSubName.ClientID + "','" + txtAddItemPrice.ClientID + "'";

                btnAddMenuItem.Attributes.Add("onclick", "return ValidateFields(" + strArguments + ");");

                if (btnAddMenuItem != null)
                {

                    btnAddMenuItem.CommandArgument = (String)this.ViewState["foodTypeID"];
                }
            }
            this.ViewState["foodTypeID"] = null;
        }

        foreach (DataGridItem item in gridViewMenus.Items)
        {
            DataGrid gridViewItems = (DataGrid)item.FindControl("gridViewItems");
            if (null != gridViewItems)
            {
                foreach (DataGridItem itemOption in gridViewItems.Items)
                {
                    System.Web.UI.WebControls.LinkButton btnDeleteItems;

                    LinkButton lbUpdateItem = (LinkButton)itemOption.FindControl("lbUpdateItem");
                    if (lbUpdateItem != null)
                    {
                        TextBox txtUpdateItemName = (TextBox)itemOption.FindControl("txtUpdateItemName");
                        TextBox txtUpdateItemDescription = (TextBox)itemOption.FindControl("txtUpdateItemDescription");
                        TextBox txtUpdateItemSubName = (TextBox)itemOption.FindControl("txtUpdateItemSubName");
                        TextBox txtUpdateItemPrice = (TextBox)itemOption.FindControl("txtUpdateItemPrice");
                        string strArguments = "'" + txtUpdateItemName.ClientID + "','" + txtUpdateItemDescription.ClientID + "','"
                        + txtUpdateItemSubName.ClientID + "','" + txtUpdateItemPrice.ClientID + "'";

                        lbUpdateItem.Attributes.Add("onclick", "return ValidateFields(" + strArguments + ");");
                    }

                    //btnDeleteItems = (System.Web.UI.WebControls.LinkButton)itemOption.FindControl("btnDeleteItems");
                    //if (btnDeleteItems != null)
                    //{
                    //    btnDeleteItems.Attributes.Add("onclick", "return confirm('Are you sure you want to delete?')");
                    //}
                }
            }
        }
    }

    protected void gridViewItems_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
    {
        if (e.CommandName == "DeleteItems")
        {
            Label lblMenuItemID = (Label)e.Item.FindControl("lblMenuItemID");
            if (lblMenuItemID != null)
            {
                objItems.menuItemId = Convert.ToInt64(lblMenuItemID.Text.Trim());
                objItems.deleteRestaurantMenuItems();
                bindItemData();
                BindMenu();
                lblMessage.Text = "Item deleted successfully.";
                lblMessage.Visible = true;
                imgGridMessage.Visible = true;
                imgGridMessage.ImageUrl = "images/Checked.png"; lblMessage.ForeColor = System.Drawing.Color.Black;
            }
        }


        else if (e.CommandName == "AddItem")
        {
            TextBox txtAddItemName = (TextBox)e.Item.FindControl("txtAddItemName");
            TextBox txtAddDescription = (TextBox)e.Item.FindControl("txtAddDescription");
            TextBox txtAddItemSubName = (TextBox)e.Item.FindControl("txtAddItemSubName");
            TextBox txtAddItemPrice = (TextBox)e.Item.FindControl("txtAddItemPrice");

            if (txtAddItemName != null && txtAddDescription != null && txtAddItemSubName != null && txtAddItemPrice != null)
            {
                objItems.itemName = txtAddItemName.Text.Trim();
                objItems.itemDescription = txtAddDescription.Text.Trim();
                objItems.itemSubname = txtAddItemSubName.Text.Trim();
                objItems.itemPrice = float.Parse(txtAddItemPrice.Text.Trim());
                objItems.createdBy = Convert.ToInt64(ViewState["userID"]);
                objItems.foodTypeId = Convert.ToInt64(e.CommandArgument.ToString());

                objItems.createRestaurantMenuItems();
                bindItemData();
                BindMenu();
                lblMessage.Text = "Item added successfully.";
                lblMessage.Visible = true;
                imgGridMessage.Visible = true;
                imgGridMessage.ImageUrl = "images/Checked.png"; lblMessage.ForeColor = System.Drawing.Color.Black;
            }
        }
    }


    protected void gridViewItems_CancelCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
    {
        try
        {
            foreach (DataGridItem item in gridViewMenus.Items)
            {
                DataGrid gridViewItems = (DataGrid)item.FindControl("gridViewItems");
                if (null != gridViewItems)
                {
                    gridViewItems.ShowFooter = true;
                    gridViewItems.EditItemIndex = -1;
                }
            }
            bindItemData();
            BindMenu();
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.ToString();
            lblMessage.Visible = true;
            imgGridMessage.Visible = true;
            imgGridMessage.ImageUrl = "images/error.png"; lblMessage.ForeColor = System.Drawing.Color.Red;
        }
    }

    protected void gridViewItems_EditCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
    {
        try
        {
            Label lblMenuID = (Label)e.Item.FindControl("lblMenuID");
            if (lblMenuID != null)
            {
                foreach (DataGridItem item in gridViewMenus.Items)
                {
                    DataGrid gridViewItems = (DataGrid)item.FindControl("gridViewItems");
                    gridViewItems.ShowFooter = false;
                    gridViewItems.EditItemIndex = -1;
                    if (lblMenuID.Text == item.Cells[0].Text)
                    {
                        if (null != gridViewItems)
                        {
                            gridViewItems.EditItemIndex = e.Item.ItemIndex;
                        }
                    }
                }
            }
            bindItemData();
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.ToString();
            lblMessage.Visible = true;
            imgGridMessage.Visible = true;
            imgGridMessage.ImageUrl = "images/error.png"; lblMessage.ForeColor = System.Drawing.Color.Red;
        }
    }

    protected void gridViewItems_UpdateCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
    {
        try
        {
            if (ListItemType.EditItem == e.Item.ItemType)
            {
                Label lblMenuItemID = (Label)e.Item.FindControl("lblMenuItemID");
                String MenuItemId = lblMenuItemID.Text.Trim();

                TextBox txtUpdateItemName = (TextBox)e.Item.FindControl("txtUpdateItemName");
                TextBox txtUpdateItemDescription = (TextBox)e.Item.FindControl("txtUpdateItemDescription");
                TextBox txtUpdateItemSubName = (TextBox)e.Item.FindControl("txtUpdateItemSubName");
                TextBox txtUpdateItemPrice = (TextBox)e.Item.FindControl("txtUpdateItemPrice");

                if (txtUpdateItemName != null && txtUpdateItemDescription != null && txtUpdateItemSubName != null && txtUpdateItemPrice != null)
                {
                    objItems.menuItemId = Convert.ToInt64(MenuItemId);
                    objItems.itemName = txtUpdateItemName.Text.Trim();
                    objItems.itemDescription = txtUpdateItemDescription.Text.Trim();
                    objItems.itemSubname = txtUpdateItemSubName.Text.Trim();
                    objItems.itemPrice = float.Parse(txtUpdateItemPrice.Text.Trim());
                    objItems.modifiedBy = Convert.ToInt64(ViewState["userID"]);

                    objItems.updateRestaurantMenuItems();

                    foreach (DataGridItem item in gridViewMenus.Items)
                    {
                        DataGrid gridViewItems = (DataGrid)item.FindControl("gridViewItems");
                        if (null != gridViewItems)
                        {
                            gridViewItems.ShowFooter = true;
                            gridViewItems.EditItemIndex = -1;
                            bindItemData();
                        }
                    }
                }
                BindMenu();
                lblMessage.Text = "Item updated successfully.";
                lblMessage.Visible = true;
                imgGridMessage.Visible = true;
                imgGridMessage.ImageUrl = "images/Checked.png"; lblMessage.ForeColor = System.Drawing.Color.Black;
            }
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.ToString();
            lblMessage.Visible = true;
            imgGridMessage.Visible = true;
            imgGridMessage.ImageUrl = "images/error.png"; lblMessage.ForeColor = System.Drawing.Color.Red;
        }
    }

    protected void gridViewMenus_ItemCreated(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
    {
        try
        {
            DataGrid gridViewItems = (DataGrid)e.Item.FindControl("gridViewItems");
            if (null != gridViewItems)
            {
                gridViewItems.ItemDataBound += new DataGridItemEventHandler(this.gridViewItems_ItemDataBound);
                gridViewItems.ItemCommand += new DataGridCommandEventHandler(this.gridViewItems_ItemCommand);
                gridViewItems.CancelCommand += new DataGridCommandEventHandler(this.gridViewItems_CancelCommand);
                gridViewItems.EditCommand += new DataGridCommandEventHandler(this.gridViewItems_EditCommand);
                gridViewItems.UpdateCommand += new DataGridCommandEventHandler(this.gridViewItems_UpdateCommand);
            }
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.ToString();
            lblMessage.Visible = true;
            imgGridMessage.Visible = true;
            imgGridMessage.ImageUrl = "images/error.png"; lblMessage.ForeColor = System.Drawing.Color.Red;
        }
    }
    private void bindItemData()
    {
        try
        {
            foreach (DataGridItem item in gridViewMenus.Items)
            {
                DataGrid gridViewItems = (DataGrid)item.FindControl("gridViewItems");
                if (null != gridViewItems)
                {
                    objItems.foodTypeId = Convert.ToInt64(item.Cells[0].Text.Trim());
                    DataTable data = objItems.getRestaurantMenuItemsByFoodTypeID();

                    gridViewItems.DataSource = data;
                    gridViewItems.DataBind();
                }
            }
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.ToString();
            lblMessage.Visible = true;
            imgGridMessage.Visible = true;
            imgGridMessage.ImageUrl = "images/error.png"; lblMessage.ForeColor = System.Drawing.Color.Red;
        }
    }

    public string getImagePath(object imgName)
    {
        try
        {
            return "../MenuImages/" + ViewState["CuisineName"] + "/Images/" + imgName.ToString();
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.ToString();
            lblMessage.Visible = true;
            imgGridMessage.Visible = true;
            imgGridMessage.ImageUrl = "images/error.png"; lblMessage.ForeColor = System.Drawing.Color.Red;
            return "";
        }
    }

    protected void btnChange_Click(object sender, EventArgs e)
    {
        try
        {
            if (fpChange.HasFile)
            {
                string foodType = hidFoodTypeId.Text.Trim();
                string[] strExtension = fpImage.FileName.Split('.');
                string strSrcPath = AppDomain.CurrentDomain.BaseDirectory + "Takeout\\MenuImages\\temp\\" + fpChange.FileName;
                fpChange.SaveAs(strSrcPath);
                string strthumbSave = AppDomain.CurrentDomain.BaseDirectory + "Takeout\\MenuImages\\" + ViewState["CuisineName"].ToString() + "\\images\\";
                string SrcFileName = fpChange.FileName;

                Misc.CreateThumbnail(strSrcPath, strthumbSave, SrcFileName, foodType);
            }
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.ToString();
            lblMessage.Visible = true;
            imgGridMessage.Visible = true;
            imgGridMessage.ImageUrl = "images/error.png"; lblMessage.ForeColor = System.Drawing.Color.Red;
        }
    }
    protected void btnSave_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            if (fpImage.HasFile)
            {
                string[] strExtension = fpImage.FileName.Split('.');
                string strUniqueID = Guid.NewGuid().ToString() + "." + strExtension[strExtension.Length - 1];
                string strSrcPath = AppDomain.CurrentDomain.BaseDirectory + "Takeout\\MenuImages\\temp\\" + fpImage.FileName;
                fpImage.SaveAs(strSrcPath);
                string strthumbSave = AppDomain.CurrentDomain.BaseDirectory + "Takeout\\MenuImages\\" + ViewState["CuisineName"].ToString() + "\\images\\";
                if (!Directory.Exists(strthumbSave))
                {
                    Directory.CreateDirectory(strthumbSave);
                }
                string SrcFileName = fpImage.FileName;

                Misc.CreateThumbnail(strSrcPath, strthumbSave, SrcFileName, strUniqueID);
                obj.foodImage = strUniqueID;
                obj.cuisineID = Convert.ToInt64(ViewState["CuisineID"]);
                obj.foodType = txtTypeofFood.Text.Trim();
                long MenuID = obj.createRestaurantMenu();
                if (MenuID > 0)
                {
                    objItems.itemName = txtItemName.Text.Trim();
                    objItems.itemDescription = txtItemDescription.Text.Trim();
                    objItems.itemPrice = float.Parse(txtItemPrice.Text.Trim());
                    objItems.createdBy = Convert.ToInt64(ViewState["userID"]);
                    objItems.foodTypeId = MenuID;

                    objItems.createRestaurantMenuItems();

                    txtTypeofFood.Text = "";
                    txtItemName.Text = "";
                    txtItemDescription.Text = "";
                    txtItemPrice.Text = "";
                }
                lblMessage.Text = "Item added successfully.";
                lblMessage.Visible = true;
                imgGridMessage.Visible = true;
                imgGridMessage.ImageUrl = "images/Checked.png"; lblMessage.ForeColor = System.Drawing.Color.Black;
            }
            BindMenu();
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.ToString();
            lblMessage.Visible = true;
            imgGridMessage.Visible = true;
            imgGridMessage.ImageUrl = "images/error.png"; lblMessage.ForeColor = System.Drawing.Color.Red;
        }
    }

    #region creates thumbnail images for Products
    private string CreateThumb(string Filename)
    {


        Guid newGUID = Guid.NewGuid();

        string strFilename = null;
        string strthumbSave = null;
        string strReturn = null;

        try
        {
            if (Filename.Length > 0)
            {
                strFilename = Filename;
                string[] strSourceFileName = Filename.Split('\\');
                string strScurcePath = "";
                string strScurceFile = strSourceFileName[strSourceFileName.Length - 1];
                for (int i = 0; i < strSourceFileName.Length - 1; i++)
                {
                    if (strScurcePath == "")
                    {
                        strScurcePath = strSourceFileName[i];
                    }
                    else
                    {
                        strScurcePath = strScurcePath + "\\" + strSourceFileName[i];
                    }
                }
                strScurcePath = strScurcePath + "\\";


            }
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.ToString();
            lblMessage.Visible = true;
            imgGridMessage.Visible = true;
            imgGridMessage.ImageUrl = "images/error.png"; lblMessage.ForeColor = System.Drawing.Color.Red;
        }

        //INSTANT C# NOTE: Inserted the following 'return' since all code paths must return a value in C#:
        return strReturn;
    }
    #endregion
    protected void lbDownloadMenu_Click(object sender, EventArgs e)
    {
        if (ViewState["CuisineID"] != null && ViewState["CuisineID"].ToString() != "")
        {
            downloadExcelFileWithSubSubItem(Convert.ToInt64(ViewState["CuisineID"].ToString()));
        }
    }
    protected void lbDownloadImages_Click(object sender, EventArgs e)
    {
        try
        {
            objResturantMenu.cuisineID = Convert.ToInt64(ViewState["CuisineID"].ToString());
            DataTable dtMenu = objResturantMenu.getRestaurantMenuByCuisineID();
            string strZipFileName = AppDomain.CurrentDomain.BaseDirectory + "Takeout\\ClientData\\Excel\\" + ViewState["userName"].ToString() + "\\" + ViewState["CuisineName"].ToString() + ".zip";
            if (dtMenu.Rows.Count > 0)
            {
                ZipFolder(dtMenu, strZipFileName);
            }            
            string saveFileName = System.Web.HttpUtility.UrlEncode("MenuImages_" + DateTime.Now.ToString("yyyy_MM_dd"), System.Text.Encoding.UTF8) + ".zip";
            ExportToUser(strZipFileName, saveFileName);
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.ToString();
            lblMessage.Visible = true;
            imgGridMessage.Visible = true;
            imgGridMessage.ImageUrl = "images/error.png"; lblMessage.ForeColor = System.Drawing.Color.Red;
        }
    }
    protected void lbImportDealItems_Click(object sender, EventArgs e)
    {
        try
        {
            Response.Redirect("importMenuItems.aspx?CID=" + ViewState["CuisineID"].ToString() + "&CN=" + ViewState["CuisineName"].ToString(), false);
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.ToString();
            lblMessage.Visible = true;
            imgGridMessage.Visible = true;
            imgGridMessage.ImageUrl = "images/error.png"; lblMessage.ForeColor = System.Drawing.Color.Red;
        }
    }    

    #region Functions to create Excel file with menu data
    BLLRestaurantMenu objResturantMenu = new BLLRestaurantMenu();



    private void downloadExcelFileWithSubSubItem(long cuisineID)
    {
        FileInfo fiDownOld;
        FileInfo fiDown;
        try
        {
            //objResturantMenu.createdBy = Convert.ToInt64(userID);
            objResturantMenu.cuisineID = cuisineID;
            DataTable dtMenu = objResturantMenu.getRestaurantMenuByCuisineID();
            DataSet dsImport = GetDSForImport(dtMenu);

            string emptyExcelFilePath = AppDomain.CurrentDomain.BaseDirectory + "Takeout\\ImportTemplates\\EmptyTemplate.xls";
            FileInfo fiEmpty = new FileInfo(emptyExcelFilePath);
            if (fiEmpty.Exists)
            {
                string folderPath = AppDomain.CurrentDomain.BaseDirectory + "Takeout\\ClientData\\Excel\\" + ViewState["userName"].ToString(); ;
                DirectoryInfo dirtemp = new DirectoryInfo(folderPath);
                if (!dirtemp.Exists)
                {
                    dirtemp.Create();
                }
                string downloadFilePath = folderPath + "\\download.xls";

                fiDownOld = new FileInfo(downloadFilePath);
                if (fiDownOld.Exists)
                {
                    fiDownOld.IsReadOnly = false;
                    fiDownOld.Delete();
                }

                fiEmpty.CopyTo(downloadFilePath, true);

                fiDown = new FileInfo(downloadFilePath);
                if (fiDown.Exists)
                {
                    string result = ExcelProcessor.DataSetToExcel(dsImport, downloadFilePath);
                    string saveFileName = System.Web.HttpUtility.UrlEncode("MenuItems_" + DateTime.Now.ToString("yyyy_MM_dd"), System.Text.Encoding.UTF8) + ".xls";
                    ExportToUser(fiDown.FullName, saveFileName);
                }
            }
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.ToString();
            lblMessage.Visible = true;
            imgGridMessage.Visible = true;
            imgGridMessage.ImageUrl = "images/error.png"; lblMessage.ForeColor = System.Drawing.Color.Red;
        }
    }
       
    private void ExportToUser(string phFileName, string saveFileName)
    {
        Response.Clear();
        Response.Buffer = true;
        Response.ContentEncoding = System.Text.Encoding.UTF8;
        Response.AppendHeader("content-disposition", "attachment;filename=\"" + saveFileName + "\"");
        Response.ContentType = "application/octet-stream";
        Response.WriteFile(phFileName);
        Response.End();
    }

    private DataSet GetDSForImport(DataTable dtMenus)
    {
        DataSet dsImport = new DataSet();
        for (int i = 0; i < dtMenus.Rows.Count; i++)
        {
            objMenuItems.foodTypeId = Convert.ToInt64(dtMenus.Rows[i]["foodTypeID"].ToString());
            DataTable dtTemp = objMenuItems.getRestaurantMenuItemsByFoodTypeID();            
            GetSubMenuDSForImport(dsImport, dtTemp, dtMenus.Rows[i]["foodType"].ToString());            
            string strTablename= Regex.Replace(dtMenus.Rows[i]["foodType"].ToString(), "\\W", "_");
            if (dsImport.Tables.Contains(strTablename))
            {                
                    DataRow bottomRowtemp = dsImport.Tables[strTablename].NewRow();
                    bottomRowtemp[0] =  "FoodType";
                    bottomRowtemp[1] = dtMenus.Rows[i]["foodType"].ToString();
                    bottomRowtemp[2] = i+1;                    
                    dsImport.Tables[strTablename].Rows.Add(bottomRowtemp);                
            }
            
            //DataRow bottomRow = dtSub.NewRow();
            //bottomRow[0] = "FoodType";
            //bottomRow[1] = dtMenus.Rows[i]["foodType"].ToString();
            //bottomRow[2] = i+1;
            //dtSub.Rows.Add(bottomRow);
            //dsImport.Tables.Add(dtSub);          
        }       
        return dsImport;
    }

    private DataSet GetSubMenuDSForImport(DataSet dstmenu, DataTable dtItem, string foodTypeName)
    {
       
        for (int i = 0; i < dtItem.Rows.Count; i++)
        {
            GetSubMenuDSForImport(ref dstmenu, dtItem.Rows[i]["menuItemId"].ToString(), dtItem.Rows[i]["itemName"].ToString(), dtItem.Rows[i]["itemDescription"].ToString(), dtItem.Rows[i]["itemSubname"].ToString(), dtItem.Rows[i]["itemPrice"].ToString(), foodTypeName);
        }
        return dstmenu;
    }


    private void GetSubMenuDSForImport(ref DataSet dsImport, string menuItemId, string itemName, string itemDescription, string itemSubname, string itemPrice, string foodTypeName)
    {
        DataSet dsSub = GetMenuSub_SubItems(menuItemId);        
        DataTable dtSub = dsSub.Tables[0].Copy();
        string strTablename = Regex.Replace(foodTypeName, "\\W", "_");
        dtSub.TableName = Regex.Replace(foodTypeName, "\\W", "_");
        DataRow bottomRow = dtSub.NewRow();
        bottomRow[0] = "Item";
        bottomRow[1] = itemName;
        bottomRow[2] = itemDescription;
        bottomRow[3] = itemSubname;
        bottomRow[4] = itemPrice;
        dtSub.Rows.InsertAt(bottomRow,0);
        if (dsImport.Tables.Contains(strTablename))
        {
            for (int i = 0; i < dtSub.Rows.Count; i++)
            {
                DataRow bottomRowtemp = dsImport.Tables[strTablename].NewRow();
                bottomRowtemp[0] = dtSub.Rows[i][0].ToString();
                bottomRowtemp[1] = dtSub.Rows[i][1].ToString();
                bottomRowtemp[2] = dtSub.Rows[i][2].ToString();
                bottomRowtemp[3] = dtSub.Rows[i][3].ToString();
                bottomRowtemp[4] = dtSub.Rows[i][4].ToString();
                dsImport.Tables[strTablename].Rows.InsertAt(bottomRowtemp, dsImport.Tables[strTablename].Rows.Count);
            }
        }
        else
        {
            dsImport.Tables.Add(dtSub);
        }
    }
    BLLSubMenuItem objSubItems = new BLLSubMenuItem();
    private DataSet GetMenuSub_SubItems(string menuItemId)
    {
        objSubItems.menuItemId = Convert.ToInt64(menuItemId);
        DataSet ds = objSubItems.getSubItemsByMenuItemIDForExport();
        return ds;
    }
    
  

    BLLRestaurantMenuItems objMenuItems = new BLLRestaurantMenuItems();
   
       
    #endregion

    #region function to create Zip File
    private void ZipFolder(DataTable dtMenu, string strZipFileName)
    {
        try
        {
            using (ZipOutputStream sOnHold = new ZipOutputStream(File.Create(strZipFileName)))
            {
                sOnHold.SetLevel(9);
                byte[] buffer = new byte[4096];

                //DirectoryInfo dir = new DirectoryInfo(strArchPath);
                for (int a = 0; a < dtMenu.Rows.Count; a++)
                {
                    FileInfo file;
                    file = new FileInfo(AppDomain.CurrentDomain.BaseDirectory + "Takeout\\MenuImages\\" + ViewState["CuisineName"].ToString() + "\\images\\" + dtMenu.Rows[a]["foodImage"].ToString());
                    if (file.Exists)
                    {
                        if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + "Takeout\\MenuImages\\" + ViewState["CuisineName"].ToString() + "\\images\\" + dtMenu.Rows[a]["foodType"].ToString() + ".gif") && !(dtMenu.Rows[a]["foodImage"].ToString() == dtMenu.Rows[a]["foodType"].ToString() + ".gif"))
                        {
                            try
                            {
                                File.Delete(AppDomain.CurrentDomain.BaseDirectory + "Takeout\\MenuImages\\" + ViewState["CuisineName"].ToString() + "\\images\\" + dtMenu.Rows[a]["foodType"].ToString() + ".gif");
                            }
                            catch (Exception ex)
                            { }
                        }
                        file.MoveTo(AppDomain.CurrentDomain.BaseDirectory + "Takeout\\MenuImages\\" + ViewState["CuisineName"].ToString() + "\\images\\" + dtMenu.Rows[a]["foodType"].ToString() + ".gif");
                        //string ARfilePath1 = strArchPath + "\\" + file.Name.ToString();
                        ZipEntry entry = new ZipEntry(Path.GetFileName(file.FullName));

                        entry.DateTime = DateTime.Now;

                        sOnHold.PutNextEntry(entry);

                        using (FileStream fs = File.OpenRead(file.FullName))
                        {

                            int sourceBytes;

                            do
                            {
                                sourceBytes = fs.Read(buffer, 0, buffer.Length);

                                sOnHold.Write(buffer, 0, sourceBytes);

                            }
                            while (sourceBytes > 0);
                        }
                    }
                }

                sOnHold.Finish();
                sOnHold.Close();
            }
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.ToString();
            lblMessage.Visible = true;
            imgGridMessage.Visible = true;
            imgGridMessage.ImageUrl = "images/error.png"; lblMessage.ForeColor = System.Drawing.Color.Red;
        }
    }
    #endregion
}
