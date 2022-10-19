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

public partial class importMenuItems : System.Web.UI.Page
{
    BLLRestaurantMenu objResturantMenu = new BLLRestaurantMenu();
    BLLRestaurantMenuItems objRestMenuItems = new BLLRestaurantMenuItems();
    BLLSubMenuItem objResSubMenuItems = new BLLSubMenuItem();

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
                        ViewState["cuisineName"] = Request.QueryString["CN"].ToString();
                        ViewState["cuisineID"] = Request.QueryString["CID"].ToString();
                        // downloadExcelFile(Convert.ToInt64(ViewState["cuisineID"].ToString()));
                    }
                    else
                    {
                        Response.Redirect(ResolveUrl("~/admin/default.aspx"), false);
                    }
                }
                else
                {
                    Response.Redirect(ResolveUrl("~/admin/default.aspx"), false);
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

    #region button to Import Excel File
    protected void btnImport_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            if (ViewState["cuisineName"] != null)
            {
                if (cbDeleteOldMenu.Checked)
                {
                    objResturantMenu.cuisineID = Convert.ToInt64(ViewState["cuisineID"].ToString());
                    objResturantMenu.deleteRestaurantMenuByCuisineID();
                }
                lblResult.Text = string.Empty;

                string result = string.Empty;
                string folderPath = AppDomain.CurrentDomain.BaseDirectory + "Takeout\\ClientData\\Excel\\" + ViewState["userName"].ToString();
                DirectoryInfo dirtemp = new DirectoryInfo(folderPath);
                if (!dirtemp.Exists)
                {
                    dirtemp.Create();
                }
                if (fuExcelFile.HasFile)
                {
                    fuExcelFile.SaveAs(folderPath + "\\" + fuExcelFile.FileName);
                    DataSet ds = new DataSet();
                    bool loadSuccess = LoadExcel(folderPath, fuExcelFile.FileName, ref ds);
                    if (loadSuccess)
                    {
                        lblMessage.Text = "Load excel file successfully.";
                        lblMessage.Visible = true;
                        imgGridMessage.Visible = true;
                        imgGridMessage.ImageUrl = "~/admin/images/Checked.png"; lblMessage.ForeColor = System.Drawing.Color.Black;

                    }
                    else
                    {
                        lblMessage.Text = "Load excel faild. Process stopped.";
                        lblMessage.Visible = true;
                        imgGridMessage.Visible = true;
                        imgGridMessage.ImageUrl = "~/admin/images/error.png"; lblMessage.ForeColor = System.Drawing.Color.Black;
                        return;
                    }

                    if (ds.Tables.Count == 0)
                    {
                        lblMessage.Text = "There is no menu or may be incorrect format, Please check your excel file. Process stopped.";
                        lblMessage.Visible = true;
                        imgGridMessage.Visible = true;
                        imgGridMessage.ImageUrl = "~/Images/error.png"; lblMessage.ForeColor = System.Drawing.Color.Black;
                        return;
                    }
                    int intRecordCount = 0;

                    for (int i = 0; i < ds.Tables.Count; i++)
                    {
                        if (ds.Tables[i].TableName.EndsWith("$") && ds.Tables[i].Columns.Count == 5)
                        {
                            if (ds.Tables[i].Columns.Count == 5)
                            {
                                long intMenuID = 0;
                                long intMenuItemID = 0;
                                DataTable dtTemp = ds.Tables[i];
                                objResturantMenu.cuisineID = Convert.ToInt64(ViewState["cuisineID"].ToString());
                                objResturantMenu.createdBy = Convert.ToInt64(ViewState["userID"].ToString());
                                 bool insert = false;
                                int counter = 1;
                                while (!insert)
                                {

                                    string strFileName = dtTemp.Rows[dtTemp.Rows.Count - counter][1].ToString();
                                    char[] invalidFileChars = Path.GetInvalidFileNameChars();
                                    foreach (char invalidFChar in invalidFileChars)
                                    {
                                        strFileName = strFileName.Replace(invalidFChar.ToString(), "");
                                    }

                                    objResturantMenu.foodImage = strFileName + ".gif";
                                    objResturantMenu.foodType = dtTemp.Rows[dtTemp.Rows.Count - counter][1].ToString();
                                    if (objResturantMenu.foodType.Trim() != "")
                                    {
                                        intMenuID = objResturantMenu.createRestaurantMenu();
                                        insert = true;
                                    }
                                    counter++;
                                }
                                for (int a = 1; a < dtTemp.Rows.Count - (counter-1); a++)
                                {
                                    if (dtTemp.Rows[a][0].ToString().ToLower() == "item")
                                    {
                                        objRestMenuItems.foodTypeId = intMenuID;
                                        objRestMenuItems.createdBy = Convert.ToInt64(ViewState["userID"].ToString());
                                        objRestMenuItems.itemName = dtTemp.Rows[a][1].ToString();
                                        objRestMenuItems.itemDescription = dtTemp.Rows[a][2].ToString();
                                        objRestMenuItems.itemSubname = dtTemp.Rows[a][3].ToString();
                                        if (dtTemp.Rows[a][4].ToString() != "")
                                        {
                                            objRestMenuItems.itemPrice = float.Parse(dtTemp.Rows[a][4].ToString());
                                        }
                                        intMenuItemID = objRestMenuItems.createRestaurantMenuItems();
                                        intRecordCount++;
                                    }
                                    else
                                    {
                                        objResSubMenuItems.menuItemId = intMenuItemID;
                                        objResSubMenuItems.createdBy = Convert.ToInt64(ViewState["userID"].ToString());
                                        objResSubMenuItems.subItemName = dtTemp.Rows[a][1].ToString();
                                        objResSubMenuItems.subItemDescription = dtTemp.Rows[a][2].ToString();
                                        objResSubMenuItems.subItemSubname = dtTemp.Rows[a][3].ToString();
                                        if (dtTemp.Rows[a][4].ToString() != "")
                                        {
                                            objResSubMenuItems.subItemPrice = float.Parse(dtTemp.Rows[a][4].ToString());
                                        }
                                        objResSubMenuItems.createSubItems();
                                        intRecordCount++;
                                    }
                                }
                            }
                            else
                            {

                                lblMessage.Text = "Excel file is not in a correct format. Process stopped.";
                                lblMessage.Visible = true;
                                imgGridMessage.Visible = true;
                                imgGridMessage.ImageUrl = "~/Images/error.png"; lblMessage.ForeColor = System.Drawing.Color.Black;
                                return;
                            }
                        }                       
                    }
                    if (intRecordCount == 0)
                    {
                        lblMessage.Text = "No record inserted. There is noting in excel file or may be incorrect format.";
                        lblMessage.Visible = true;
                        imgGridMessage.Visible = true;
                        imgGridMessage.ImageUrl = "~/Images/error.png"; lblMessage.ForeColor = System.Drawing.Color.Black;
                        return;
                    }
                    else
                    {
                        lblMessage.Text = "Load excel file successfully. " + intRecordCount + " record(s) has been successfully inserted.";
                        lblMessage.Visible = true;
                        imgGridMessage.Visible = true;
                        imgGridMessage.ImageUrl = "~/Images/Checked.png"; lblMessage.ForeColor = System.Drawing.Color.Black;
                    }

                }
                if (fuImageZip.HasFile)
                {
                    string strDirectoryName = AppDomain.CurrentDomain.BaseDirectory + "Takeout\\MenuImages\\" + ViewState["cuisineName"].ToString() + "\\Images";
                    DirectoryInfo dirImage = new DirectoryInfo(strDirectoryName);
                    if (!dirImage.Exists)
                    {
                        dirImage.Create();
                    }
                    string strFileName = strDirectoryName + "\\" + "DealImage" + DateTime.Now.ToString("MMddyyyyHHmmss") + ".zip";
                    fuImageZip.SaveAs(strFileName);
                    FastZip unzip = new FastZip();
                    unzip.ExtractZip(strFileName, strDirectoryName, "");
                    try
                    {
                        File.Delete(strFileName);
                    }
                    catch (Exception ex)
                    { }
                }
            }
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.ToString();
            lblMessage.Visible = true;
            imgGridMessage.Visible = true;
            imgGridMessage.ImageUrl = "~/admin/images/error.png"; lblMessage.ForeColor = System.Drawing.Color.Red;
        }
    }
    #endregion


    #region Deal with excel
    private bool LoadExcel(string folderPath, string uploadedFileName, ref DataSet ds)
    {
        string excelFilePath = folderPath + "\\" + uploadedFileName;
        try
        {
            ds = ExcelProcessor.ExcelToDS(excelFilePath);
            return true;
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.ToString();
            lblMessage.Visible = true;
            imgGridMessage.Visible = true;
            imgGridMessage.ImageUrl = "images/error.png"; lblMessage.ForeColor = System.Drawing.Color.Red;
            return false;
        }
    }
    #endregion

}
