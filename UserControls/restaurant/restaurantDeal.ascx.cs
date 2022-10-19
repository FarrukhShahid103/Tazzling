using System;
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

public partial class Takeout_UserControls_restaurant_restaurantDeal : System.Web.UI.UserControl
{
    BLLMemberDeal objResturantDeal = new BLLMemberDeal();
    BLLMemberDealItems objDealItems = new BLLMemberDealItems();    
    BLLRestaurant objResturantInfo = new BLLRestaurant();
    public int strButton1 = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {/*
                if (Session["restaurant"] != null)
                {
                    DataTable dtUser = (DataTable)Session["restaurant"];
                    ViewState["userID"] = dtUser.Rows[0]["userID"];
                    ViewState["userName"] = dtUser.Rows[0]["userName"];
                    objResturantInfo.userId = Convert.ToInt64(ViewState["userID"]);
                    DataTable dtResturanInfo = objResturantInfo.getRestaurantByUserID();
                    if (dtResturanInfo != null && dtResturanInfo.Rows.Count > 0)
                    {
                        ViewState["cuisineName"] = dtResturanInfo.Rows[0]["cuisineName"];
                        ViewState["cuisineID"] = dtResturanInfo.Rows[0]["cuisineId"];
                    }
                    else
                    {
                        Response.Redirect(ResolveUrl("~/Default.aspx"), false);
                    }
                }
                else
                {
                    Response.Redirect(ResolveUrl("~/Default.aspx"), false);
                }
                BindMenu();*/
            }
        }
        catch (Exception ex)
        {
            lblMessage.Text = "There is an error occur, please email us at support@tazzling.com or call 1855-295-1771.";
            lblMessage.Visible = true;
            imgGridMessage.Visible = true;
            imgGridMessage.ImageUrl = "~/images/error.png"; lblMessage.ForeColor = System.Drawing.Color.Red;
        }
    }

    private void BindMenu()
    {
        try
        {
            DataTable dtMenu;
            objResturantDeal.cuisineID = Convert.ToInt64(ViewState["cuisineID"]);
            objResturantDeal.createdBy = Convert.ToInt64(ViewState["userID"]);
            dtMenu = objResturantDeal.getMemberDealByCuisineIDAndUserID();

            gridViewMenus.DataSource = dtMenu.DefaultView;
            gridViewMenus.DataBind();
        }
        catch (Exception ex)
        {
            lblMessage.Text = "There is an error occur, please email us at support@tazzling.com or call 1855-295-1771.";
            lblMessage.Visible = true;
            imgGridMessage.Visible = true;
            imgGridMessage.ImageUrl = "~/images/error.png"; lblMessage.ForeColor = System.Drawing.Color.Red;
        }
    }

  

    private void downloadExcelFile(long cuisineID)
    {
        FileInfo fiDownOld;
        FileInfo fiDown;
        try
        {
            //objResturantMenu.createdBy = Convert.ToInt64(userID);
            objResturantDeal.cuisineID = cuisineID;
            objResturantDeal.createdBy = Convert.ToInt64(ViewState["userID"]);
            DataTable dtMenu = objResturantDeal.getMemberDealByCuisineIDAndUserID();
            DataSet dsImport = GetDSForImport(dtMenu);

            string emptyExcelFilePath = AppDomain.CurrentDomain.BaseDirectory + "Takeout\\ImportTemplates\\EmptyTemplate.xls";
            FileInfo fiEmpty = new FileInfo(emptyExcelFilePath);
            if (fiEmpty.Exists)
            {
                string folderPath = AppDomain.CurrentDomain.BaseDirectory + "Takeout\\ClientData\\Excel\\" + ViewState["userName"].ToString();
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
                    string saveFileName = System.Web.HttpUtility.UrlEncode("DealItems_" + DateTime.Now.ToString("yyyy_MM_dd"), System.Text.Encoding.UTF8) + ".xls";
                    ExportToUser(fiDown.FullName, saveFileName);
                }
            }
        }
        catch (Exception ex)
        {
            lblMessage.Text = "There is an error occur, please email us at support@tazzling.com or call 1855-295-1771.";
            lblMessage.Visible = true;
            imgGridMessage.Visible = true;
            imgGridMessage.ImageUrl = "~/images/error.png"; lblMessage.ForeColor = System.Drawing.Color.Red;

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
            GetDSForImport(ref dsImport, dtMenus.Rows[i]["dealID"].ToString(), dtMenus.Rows[i]["dealName"].ToString(), dtMenus.Rows[i]["dealOrderItemsQty"].ToString(), dtMenus.Rows[i]["dealPrice"].ToString());
        }
        return dsImport;
    }

    private void GetDSForImport(ref DataSet dsImport, string dealID, string dealName, string DealQty, string DealPrice)
    {
        DataSet dsSub = GetMenuItemSet(dealID);
        DataTable dtSub = dsSub.Tables[0].Copy();
        dtSub.TableName = Regex.Replace(dealName, "\\W", "_");
        DataRow bottomRow = dtSub.NewRow();
        bottomRow[0] = dealName;
        bottomRow[1] = DealQty;
        bottomRow[2] = "$" + DealPrice;
        dtSub.Rows.Add(bottomRow);
        dsImport.Tables.Add(dtSub);
    }
    
    private DataSet GetMenuItemSet(string foodTypeId)
    {
        objDealItems.dealId = Convert.ToInt64(foodTypeId);
        DataSet ds = objDealItems.getMemberDealItemsByDealIDForExport();
        return ds;
    }

    protected void gridViewMenus_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
    {
        try
        {
            LinkButton Source = e.CommandSource as LinkButton;
            LinkButton Edit = Source.Parent.FindControl("lbEdit") as LinkButton;
            LinkButton Update = Source.Parent.FindControl("lbUpdate") as LinkButton;
            LinkButton Cancel = Source.Parent.FindControl("lbCancel") as LinkButton;
            LinkButton Delete = Source.Parent.FindControl("lbDelete") as LinkButton;
            Label lblFoodType = Source.Parent.FindControl("lblFoodType") as Label;
            Label lblDealPrice = Source.Parent.FindControl("lblDealPrice") as Label;
            Label lblDealQty = Source.Parent.FindControl("lblDealQty") as Label;

            Label lblFoodTypeId = Source.Parent.FindControl("lblFoodTypeId") as Label;
            TextBox txtFoodType = Source.Parent.FindControl("txtdealName") as TextBox;
            TextBox txtDealPrice = Source.Parent.FindControl("txtDealPrice") as TextBox;
            TextBox txtDealQty = Source.Parent.FindControl("txtDealQty") as TextBox;


            Edit.Visible = true;
            Update.Visible = true;
            Cancel.Visible = true;
            Delete.Visible = true;
            lblFoodType.Visible = true;
            lblDealPrice.Visible = true;
            lblDealQty.Visible = true;
            txtFoodType.Visible = true;
            txtDealPrice.Visible = true;
            txtDealQty.Visible = true;

            switch (e.CommandName)
            {
                case "ToEdit":
                    Edit.Visible = false;
                    Delete.Visible = false;
                    lblFoodType.Visible = false;
                    lblDealPrice.Visible = false;
                    lblDealQty.Visible = false;
                    break;
                case "ToUpdate":
                    Update.Visible = false;
                    Cancel.Visible = false;
                    txtFoodType.Visible = false;
                    txtDealPrice.Visible = false;
                    txtDealQty.Visible = false;
                    UpdateMenuInfo(txtFoodType.Text.Trim(), float.Parse(txtDealPrice.Text.Trim()), Convert.ToInt32(txtDealQty.Text.Trim()), lblFoodTypeId.Text);
                    break;
                case "ToCancel":
                    Update.Visible = false;
                    Cancel.Visible = false;
                    txtFoodType.Visible = false;
                    txtDealPrice.Visible = false;
                    txtDealQty.Visible = false;
                    break;
                case "ToDelete":
                    DeleteMenus(lblFoodTypeId.Text);
                    break;
            }
        }
        catch (Exception ex)
        {
            lblMessage.Text = "There is an error occur, please email us at support@tazzling.com or call 1855-295-1771.";
            lblMessage.Visible = true;
            imgGridMessage.Visible = true;
            imgGridMessage.ImageUrl = "~/images/error.png"; lblMessage.ForeColor = System.Drawing.Color.Red;
        }
    }
    private void UpdateMenuInfo(string dealName, float dealPrice, int dealQty, string foodTypeId)
    {
        objResturantDeal.dealId = Convert.ToInt64(foodTypeId);
        objResturantDeal.dealName = dealName;
        objResturantDeal.dealPrice = dealPrice;
        objResturantDeal.dealOrderItemsQty = dealQty;
        objResturantDeal.cuisineID = Convert.ToInt64(ViewState["cuisineID"]);
        objResturantDeal.updateMemberDeal();
        BindMenu();
        lblMessage.Text = "Item updated successfully.";
        lblMessage.Visible = true;
        imgGridMessage.Visible = true;
        imgGridMessage.ImageUrl = "~/images/Checked.png"; lblMessage.ForeColor = System.Drawing.Color.Black;
    }

    private void DeleteMenus(string dealId)
    {
        try
        {
            objResturantDeal.dealId = Convert.ToInt64(dealId);
            objResturantDeal.deleteMemberDeal();
            BindMenu();
            lblMessage.Text = "Item deleted successfully.";
            lblMessage.Visible = true;
            imgGridMessage.Visible = true;
            imgGridMessage.ImageUrl = "~/images/Checked.png"; lblMessage.ForeColor = System.Drawing.Color.Black;
        }
        catch (Exception ex)
        {
            lblMessage.Text = "There is an error occur, please email us at support@tazzling.com or call 1855-295-1771.";
            lblMessage.Visible = true;
            imgGridMessage.Visible = true;
            imgGridMessage.ImageUrl = "~/images/error.png"; lblMessage.ForeColor = System.Drawing.Color.Red;
        }
    }


    protected void gridViewMenus_CancelCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
    {
        try
        {
            gridViewMenus.ShowFooter = true;
            gridViewMenus.EditItemIndex = -1;
            BindMenu();
        }
        catch (Exception ex)
        {
            lblMessage.Text = "There is an error occur, please email us at support@tazzling.com or call 1855-295-1771.";
            lblMessage.Visible = true;
            imgGridMessage.Visible = true;
            imgGridMessage.ImageUrl = "~/images/error.png"; lblMessage.ForeColor = System.Drawing.Color.Red;
        }
    }

    protected void gridViewMenus_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
    {
        try
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

                    objDealItems.dealId = Convert.ToInt64(lblFoodTypeID1.Text.ToString());
                    DataTable data = objDealItems.getMemberDealItemsByDealID();
                    gridViewItems.DataSource = data;
                    gridViewItems.DataBind();
                }
            }
        }
        catch (Exception ex)
        {
            lblMessage.Text = "There is an error occur, please email us at support@tazzling.com or call 1855-295-1771.";
            lblMessage.Visible = true;
            imgGridMessage.Visible = true;
            imgGridMessage.ImageUrl = "~/images/error.png"; lblMessage.ForeColor = System.Drawing.Color.Red;
        }
    }

    protected void gridViewItems_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
    {
        try
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
                    Button btnAddMenuItem = (Button)e.Item.FindControl("btnAddMenuItem");

                    TextBox txtAddItemName = (TextBox)e.Item.FindControl("txtAddItemName");
                    TextBox txtAddDescription = (TextBox)e.Item.FindControl("txtAddDescription");
                    TextBox txtAddItemSubName = (TextBox)e.Item.FindControl("txtAddItemSubName");

                    string strArguments = "'" + txtAddItemName.ClientID + "','" + txtAddDescription.ClientID + "','"
                        + txtAddItemSubName.ClientID + "'";

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
                            string strArguments = "'" + txtUpdateItemName.ClientID + "','" + txtUpdateItemDescription.ClientID + "','"
                            + txtUpdateItemSubName.ClientID + "'";

                            lbUpdateItem.Attributes.Add("onclick", "return ValidateFields(" + strArguments + ");");
                        }

                        btnDeleteItems = (System.Web.UI.WebControls.LinkButton)itemOption.FindControl("btnDeleteItems");
                        if (btnDeleteItems != null)
                        {
                            btnDeleteItems.Attributes.Add("onclick", "return confirm('Are you sure you want to delete?')");
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            lblMessage.Text = "There is an error occur, please email us at support@tazzling.com or call 1855-295-1771.";
            lblMessage.Visible = true;
            imgGridMessage.Visible = true;
            imgGridMessage.ImageUrl = "~/images/error.png"; lblMessage.ForeColor = System.Drawing.Color.Red;
        }
    }

    protected void gridViewItems_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "DeleteItems")
            {
                Label lbldealItemId = (Label)e.Item.FindControl("lbldealItemId");
                if (lbldealItemId != null)
                {
                    objDealItems.dealItemId = Convert.ToInt64(lbldealItemId.Text.Trim());
                    objDealItems.deleteMemberDealItems();
                    bindItemData();
                    BindMenu();
                    lblMessage.Text = "Item deleted successfully.";
                    lblMessage.Visible = true;
                    imgGridMessage.Visible = true;
                    imgGridMessage.ImageUrl = "~/images/Checked.png"; lblMessage.ForeColor = System.Drawing.Color.Black;
                }
            }


            if (e.CommandName == "AddItem")
            {
                TextBox txtAddItemName = (TextBox)e.Item.FindControl("txtAddItemName");
                TextBox txtAddDescription = (TextBox)e.Item.FindControl("txtAddDescription");
                TextBox txtAddItemSubName = (TextBox)e.Item.FindControl("txtAddItemSubName");

                if (txtAddItemName != null && txtAddDescription != null && txtAddItemSubName != null)
                {
                    objDealItems.dealItemName = txtAddItemName.Text.Trim();
                    objDealItems.dealItemDescription = txtAddDescription.Text.Trim();
                    objDealItems.dealItemSubname = txtAddItemSubName.Text.Trim();

                    objDealItems.createdBy = Convert.ToInt64(ViewState["userID"]);
                    objDealItems.dealId = Convert.ToInt64(e.CommandArgument.ToString());
                    objDealItems.createMemberDealItems();
                    bindItemData();
                    BindMenu();
                    lblMessage.Text = "Item added successfully.";
                    lblMessage.Visible = true;
                    imgGridMessage.Visible = true;
                    imgGridMessage.ImageUrl = "~/images/Checked.png"; lblMessage.ForeColor = System.Drawing.Color.Black;
                }
            }
        }
        catch (Exception ex)
        {
            lblMessage.Text = "There is an error occur, please email us at support@tazzling.com or call 1855-295-1771.";
            lblMessage.Visible = true;
            imgGridMessage.Visible = true;
            imgGridMessage.ImageUrl = "~/images/error.png"; lblMessage.ForeColor = System.Drawing.Color.Red;
        }
    }

    protected void gridViewItems_CancelCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
    {
        try
        {
            //lblError.Text = String.Empty;
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
            lblMessage.Text = "There is an error occur, please email us at support@tazzling.com or call 1855-295-1771.";
            lblMessage.Visible = true;
            imgGridMessage.Visible = true;
            imgGridMessage.ImageUrl = "~/images/error.png"; lblMessage.ForeColor = System.Drawing.Color.Red;
        }
    }

    protected void gridViewItems_EditCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
    {
        try
        {
            //lblError.Text = String.Empty;
            Label lblDealID = (Label)e.Item.FindControl("lblDealID");
            if (lblDealID != null)
            {
                foreach (DataGridItem item in gridViewMenus.Items)
                {
                    DataGrid gridViewItems = (DataGrid)item.FindControl("gridViewItems");
                    gridViewItems.ShowFooter = false;
                    gridViewItems.EditItemIndex = -1;
                    if (lblDealID.Text == item.Cells[0].Text)
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
            lblMessage.Text = "There is an error occur, please email us at support@tazzling.com or call 1855-295-1771.";
            lblMessage.Visible = true;
            imgGridMessage.Visible = true;
            imgGridMessage.ImageUrl = "~/images/error.png"; lblMessage.ForeColor = System.Drawing.Color.Red;
        }
    }

    protected void gridViewItems_UpdateCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
    {
        try
        {
            if (ListItemType.EditItem == e.Item.ItemType)
            {
                Label lbldealItemId = (Label)e.Item.FindControl("lbldealItemId");
                String MenuItemId = lbldealItemId.Text.Trim();

                TextBox txtUpdateItemName = (TextBox)e.Item.FindControl("txtUpdateItemName");
                TextBox txtUpdateItemDescription = (TextBox)e.Item.FindControl("txtUpdateItemDescription");
                TextBox txtUpdateItemSubName = (TextBox)e.Item.FindControl("txtUpdateItemSubName");

                if (txtUpdateItemName != null && txtUpdateItemDescription != null && txtUpdateItemSubName != null)
                {
                    objDealItems.dealItemId = Convert.ToInt64(MenuItemId);
                    objDealItems.dealItemName = txtUpdateItemName.Text.Trim();
                    objDealItems.dealItemDescription = txtUpdateItemDescription.Text.Trim();
                    objDealItems.dealItemSubname = txtUpdateItemSubName.Text.Trim();
                    objDealItems.modifiedBy = Convert.ToInt64(ViewState["userID"]);

                    objDealItems.updateMemberDealItems();

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
                    lblMessage.Text = "Item updated successfully.";
                    lblMessage.Visible = true;
                    imgGridMessage.Visible = true;
                    imgGridMessage.ImageUrl = "~/images/Checked.png"; lblMessage.ForeColor = System.Drawing.Color.Black;
                }
                BindMenu();
            }
        }
        catch (Exception ex)
        {
            lblMessage.Text = "There is an error occur, please email us at support@tazzling.com or call 1855-295-1771.";
            lblMessage.Visible = true;
            imgGridMessage.Visible = true;
            imgGridMessage.ImageUrl = "~/images/error.png"; lblMessage.ForeColor = System.Drawing.Color.Red;
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
            lblMessage.Text = "There is an error occur, please email us at support@tazzling.com or call 1855-295-1771.";
            lblMessage.Visible = true;
            imgGridMessage.Visible = true;
            imgGridMessage.ImageUrl = "~/images/error.png"; lblMessage.ForeColor = System.Drawing.Color.Red;
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
                    objDealItems.dealId = Convert.ToInt64(item.Cells[0].Text.Trim());
                    DataTable data = objDealItems.getMemberDealItemsByDealID();

                    gridViewItems.DataSource = data;
                    gridViewItems.DataBind();
                }
            }
        }
        catch (Exception ex)
        {
            lblMessage.Text = "There is an error occur, please email us at support@tazzling.com or call 1855-295-1771.";
            lblMessage.Visible = true;
            imgGridMessage.Visible = true;
            imgGridMessage.ImageUrl = "~/images/error.png"; lblMessage.ForeColor = System.Drawing.Color.Red;
        }
    }

    protected void btnAddMore_Click(object sender, EventArgs e)
    {

    }

    public string getImagePath(object imgName)
    {
        try                   
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + "takeout\\MenuImages\\" + ViewState["cuisineName"] + "\\" + ViewState["userID"] + "\\images\\" + imgName;
            if (File.Exists(path))
            {
                return "../MenuImages/" + ViewState["cuisineName"] + "/" + ViewState["userID"] + "/Images/" + imgName.ToString();
            }
            else
            {
                return "../MenuImages/noMenuImage.gif";
            }

        }
        catch (Exception ex)
        {
            lblMessage.Text = "There is an error occur, please email us at support@tazzling.com or call 1855-295-1771.";
            lblMessage.Visible = true;
            imgGridMessage.Visible = true;
            imgGridMessage.ImageUrl = "~/images/error.png"; lblMessage.ForeColor = System.Drawing.Color.Red;
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
                string strthumbSave = AppDomain.CurrentDomain.BaseDirectory + "Takeout\\MenuImages\\" + ViewState["cuisineName"].ToString() +"\\"+ViewState["userID"]+ "\\images\\";
                string SrcFileName = fpChange.FileName;

                Misc.CreateThumbnail(strSrcPath, strthumbSave, SrcFileName, foodType);
                BindMenu();
            }
        }
        catch (Exception ex)
        {
            lblMessage.Text = "There is an error occur, please email us at support@tazzling.com or call 1855-295-1771.";
            lblMessage.Visible = true;
            imgGridMessage.Visible = true;
            imgGridMessage.ImageUrl = "~/images/error.png"; lblMessage.ForeColor = System.Drawing.Color.Red;
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (fpImage.HasFile)
            {
                string[] strExtension = fpImage.FileName.Split('.');
                string strUniqueID = Guid.NewGuid().ToString() + "." + strExtension[strExtension.Length - 1];
                string strSrcPath = AppDomain.CurrentDomain.BaseDirectory + "Takeout\\MenuImages\\temp\\" + fpImage.FileName;
                fpImage.SaveAs(strSrcPath);
                string strthumbSave = AppDomain.CurrentDomain.BaseDirectory + "Takeout\\MenuImages\\" + ViewState["cuisineName"].ToString() + "\\" + ViewState["userID"] + "\\images\\";
                if (!Directory.Exists(strthumbSave))
                {
                    Directory.CreateDirectory(strthumbSave);
                }
                string SrcFileName = fpImage.FileName;

                Misc.CreateThumbnail(strSrcPath, strthumbSave, SrcFileName, strUniqueID);
                objResturantDeal.dealImage = strUniqueID;
                objResturantDeal.cuisineID = Convert.ToInt64(ViewState["cuisineID"]);
                objResturantDeal.createdBy = Convert.ToInt64(ViewState["userID"]);
                objResturantDeal.dealName = txtTypeofFood.Text.Trim();
                objResturantDeal.dealOrderItemsQty = Convert.ToInt32(txtQuantity.Text.Trim());
                objResturantDeal.dealPrice = float.Parse(txtDealPrice.Text.Trim());
                long MenuID = objResturantDeal.createMemberDeal();
                if (MenuID > 0)
                {
                    objDealItems.dealItemName = txtItemName.Text.Trim();
                    objDealItems.dealItemDescription = txtItemDescription.Text.Trim();

                    objDealItems.createdBy = Convert.ToInt64(ViewState["userID"]);
                    objDealItems.dealId = MenuID;

                    objDealItems.createMemberDealItems();
                    txtDealPrice.Text = "";
                    txtQuantity.Text = "";
                    txtTypeofFood.Text = "";
                    txtItemName.Text = "";
                    txtItemDescription.Text = "";
                    lblMessage.Text = "Item added successfully.";
                    lblMessage.Visible = true;
                    imgGridMessage.Visible = true;
                    imgGridMessage.ImageUrl = "~/images/Checked.png"; lblMessage.ForeColor = System.Drawing.Color.Black;
                }
            }
            BindMenu();
        }
        catch (Exception ex)
        {
            lblMessage.Text = "There is an error occur, please email us at support@tazzling.com or call 1855-295-1771.";
            lblMessage.Visible = true;
            imgGridMessage.Visible = true;
            imgGridMessage.ImageUrl = "~/images/error.png"; lblMessage.ForeColor = System.Drawing.Color.Red;
        }
    }

    #region creates thumbnail images for Products
    private string CreateThumb(string Filename)
    {


        Guid newGUID = Guid.NewGuid();

        string strFilename = null;        
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
            imgGridMessage.ImageUrl = "~/images/error.png"; lblMessage.ForeColor = System.Drawing.Color.Red;
        }

        //INSTANT C# NOTE: Inserted the following 'return' since all code paths must return a value in C#:
        return strReturn;
    }
    #endregion

    protected void lbDownloadMenu_Click(object sender, EventArgs e)
    {
        if (ViewState["cuisineID"] != null && ViewState["cuisineID"].ToString() != "")
        {
            downloadExcelFile(Convert.ToInt64(ViewState["cuisineID"].ToString()));
        }
    }
    protected void lbDownloadImages_Click(object sender, EventArgs e)
    {
        objResturantDeal.cuisineID = Convert.ToInt64(ViewState["cuisineID"].ToString());
        objResturantDeal.createdBy = Convert.ToInt64(ViewState["userID"].ToString());
        DataTable dtDeal = objResturantDeal.getMemberDealByCuisineIDAndUserID();
        string strZipFileName = AppDomain.CurrentDomain.BaseDirectory + "Takeout\\ClientData\\Excel\\" + ViewState["userName"].ToString() + "\\" + ViewState["cuisineName"].ToString() + ".zip";
        if (dtDeal.Rows.Count > 0)
        {
            ZipFolder(dtDeal, strZipFileName);
        }
        //ICSharpCode.SharpZipLib.Zip.FastZip zipfile = new ICSharpCode.SharpZipLib.Zip.FastZip();
        //zipfile.CreateZip(""

        string saveFileName = System.Web.HttpUtility.UrlEncode("DealImages_" + DateTime.Now.ToString("yyyy_MM_dd"), System.Text.Encoding.UTF8) + ".zip";
        ExportToUser(strZipFileName, saveFileName);
    }

    private void ZipFolder(DataTable dtDeal, string strZipFileName)
    {
        try
        {
            using (ZipOutputStream sOnHold = new ZipOutputStream(File.Create(strZipFileName)))
            {
                sOnHold.SetLevel(9);
                byte[] buffer = new byte[4096];

                //DirectoryInfo dir = new DirectoryInfo(strArchPath);
                for (int a = 0; a < dtDeal.Rows.Count; a++)
                {
                    FileInfo file;
                    file = new FileInfo(AppDomain.CurrentDomain.BaseDirectory + "Takeout\\MenuImages\\" + ViewState["cuisineName"].ToString() + "\\" + ViewState["userID"] + "\\images\\" + dtDeal.Rows[a]["dealImage"].ToString());
                    if (file.Exists)
                    {
                        if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + "Takeout\\MenuImages\\" + ViewState["cuisineName"].ToString() + "\\" + ViewState["userID"] + "\\images\\" + dtDeal.Rows[a]["dealName"].ToString() + ".gif"))
                        {
                            try
                            {
                                File.Delete(AppDomain.CurrentDomain.BaseDirectory + "Takeout\\MenuImages\\" + ViewState["cuisineName"].ToString() + "\\" + ViewState["userID"] + "\\images\\" + dtDeal.Rows[a]["dealName"].ToString() + ".gif");
                            }
                            catch (Exception ex)
                            { }
                        }
                        file.MoveTo(AppDomain.CurrentDomain.BaseDirectory + "Takeout\\MenuImages\\" + ViewState["cuisineName"].ToString() + "\\" + ViewState["userID"] + "\\images\\" + dtDeal.Rows[a]["dealName"].ToString() + ".gif");
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
            lblMessage.Text = "There is an error occur, please email us at support@tazzling.com or call 1855-295-1771.";
            lblMessage.Visible = true;
            imgGridMessage.Visible = true;
            imgGridMessage.ImageUrl = "~/images/error.png"; lblMessage.ForeColor = System.Drawing.Color.Red;
        }
    }
    protected void lbImportDealItems_Click(object sender, EventArgs e)
    {
        try
        {
            Response.Redirect("restaurant_importdeal.aspx", false);
        }
        catch (Exception ex)
        {
            lblMessage.Text = "There is an error occur, please email us at support@tazzling.com or call 1855-295-1771.";
            lblMessage.Visible = true;
            imgGridMessage.Visible = true;
            imgGridMessage.ImageUrl = "~/images/error.png"; lblMessage.ForeColor = System.Drawing.Color.Red;
        }
    }
}
