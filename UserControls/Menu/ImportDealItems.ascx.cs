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

public partial class Takeout_UserControls_Menu_ImportDealItems : System.Web.UI.UserControl
{
    BLLRestaurantDeal objResturantDeal = new BLLRestaurantDeal();
    BLLRestaurantDealItems objRestDealItems = new BLLRestaurantDealItems();
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

                }
            }
        }
        catch (Exception ex)
        {
            lblMessage.Text = lblMessage.Text = "There is an error occur, please email us at support@tazzling.com or call 1855-295-1771.";
            lblMessage.Visible = true;
            imgGridMessage.Visible = true;
            imgGridMessage.ImageUrl = "~/admin/images/error.png"; lblMessage.ForeColor = System.Drawing.Color.Red;
        }
    }
    protected void btnImport_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            if (ViewState["cuisineName"] != null)
            {
                if (cbDeleteOldMenu.Checked)
                {
                    objResturantDeal.cuisineID = Convert.ToInt64(ViewState["cuisineID"].ToString());
                    objResturantDeal.deleteRestaurantDealByCuisineID();
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
                        lblMessage.Text = "There is no deal or may be incorrect format, Please check your excel file. Process stopped.";
                        lblMessage.Visible = true;
                        imgGridMessage.Visible = true;
                        imgGridMessage.ImageUrl = "~/images/error.png"; lblMessage.ForeColor = System.Drawing.Color.Black;
                        return;
                    }
                    int intRecordCount = 0;
                    for (int i = 0; i < ds.Tables.Count; i++)
                    {
                        if (ds.Tables[i].TableName.EndsWith("$") && ds.Tables[i].Columns.Count == 3)
                        {
                            if (ds.Tables[i].Columns.Count == 3)
                            {
                                long intDealID = 0;
                                DataTable dtTemp = ds.Tables[i];
                                objResturantDeal.cuisineID = Convert.ToInt64(ViewState["cuisineID"].ToString());
                                objResturantDeal.createdBy = Convert.ToInt64(ViewState["userID"].ToString());
                                  bool insert = false;
                                int counter = 1;
                                while (!insert)
                                {
                                    try
                                    {
                                        string strFileName = dtTemp.Rows[dtTemp.Rows.Count - counter][0].ToString();
                                        char[] invalidFileChars = Path.GetInvalidFileNameChars();
                                        foreach (char invalidFChar in invalidFileChars)
                                        {
                                            strFileName = strFileName.Replace(invalidFChar.ToString(), "");
                                        }
                                        objResturantDeal.dealImage = strFileName + ".gif";
                                        objResturantDeal.dealName = dtTemp.Rows[dtTemp.Rows.Count - counter][0].ToString();
                                        objResturantDeal.dealOrderItemsQty = Convert.ToInt32(dtTemp.Rows[dtTemp.Rows.Count - counter][1].ToString());
                                        objResturantDeal.dealPrice = float.Parse(dtTemp.Rows[dtTemp.Rows.Count - counter][2].ToString().Remove(0, 1));
                                        if (objResturantDeal.dealName.Trim() != "")
                                        {
                                            intDealID = objResturantDeal.createRestaurantDeal();
                                            insert = true;
                                        }
                                        counter++;
                                    }
                                    catch (Exception ex)
                                    { counter++; }
                                }                                
                                for (int a = 1; a < dtTemp.Rows.Count - 1; a++)
                                {
                                    objRestDealItems.dealId = intDealID;
                                    objRestDealItems.createdBy = Convert.ToInt64(ViewState["userID"].ToString());
                                    objRestDealItems.dealItemName = dtTemp.Rows[a][0].ToString();
                                    objRestDealItems.dealItemDescription = dtTemp.Rows[a][1].ToString();
                                    objRestDealItems.dealItemSubname = dtTemp.Rows[a][2].ToString();
                                    objRestDealItems.createRestaurantDealItems();
                                    intRecordCount++;
                                }
                            }
                            else
                            {
                                lblMessage.Text = "Excel file is not in a correct format. Process stopped.";
                                lblMessage.Visible = true;
                                imgGridMessage.Visible = true;
                                imgGridMessage.ImageUrl = "~/images/error.png"; lblMessage.ForeColor = System.Drawing.Color.Black;
                                return;
                            }
                        }
                        
                    }
                    if (intRecordCount == 0)
                    {
                        lblMessage.Text = "No record inserted. There is noting in excel file or may be incorrect format.";
                        lblMessage.Visible = true;
                        imgGridMessage.Visible = true;
                        imgGridMessage.ImageUrl = "~/images/error.png"; lblMessage.ForeColor = System.Drawing.Color.Black;
                        return;
                    }
                    else
                    {
                        lblMessage.Text = "Load excel file successfully. " + intRecordCount + " record(s) has been successfully inserted.";
                        lblMessage.Visible = true;
                        imgGridMessage.Visible = true;
                        imgGridMessage.ImageUrl = "~/images/Checked.png"; lblMessage.ForeColor = System.Drawing.Color.Black;
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
                    string strFileName=strDirectoryName+"\\"+"DealImage"+DateTime.Now.ToString("MMddyyyyHHmmss")+".zip";
                    fuImageZip.SaveAs(strFileName);
                    FastZip unzip = new FastZip();
                    unzip.ExtractZip(strFileName, strDirectoryName,"");
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
            lblMessage.Text = lblMessage.Text = "There is an error occur, please email us at support@tazzling.com or call 1855-295-1771.";
            lblMessage.Visible = true;
            imgGridMessage.Visible = true;
            imgGridMessage.ImageUrl = "~/admin/images/error.png"; lblMessage.ForeColor = System.Drawing.Color.Red;
        }
    }

    #region Deal with excel

    private bool LoadExcel(string folderPath, string uploadedFileName, ref DataSet ds)
    {          
        string excelFilePath = folderPath+"\\" + uploadedFileName;
        try
        {
            ds = ExcelProcessor.ExcelToDS(excelFilePath);
            return true;
        }
        catch (Exception ex)
        {
            lblMessage.Text = lblMessage.Text = "There is an error occur, please email us at support@tazzling.com or call 1855-295-1771.";
            lblMessage.Visible = true;
            imgGridMessage.Visible = true;
            imgGridMessage.ImageUrl = "~/admin/images/error.png"; lblMessage.ForeColor = System.Drawing.Color.Red;
            return false;
        }
    }
    #endregion
    
}
