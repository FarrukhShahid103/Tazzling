using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using GecLibrary;
using System.Diagnostics;
using System.Configuration;

public partial class MyOrder : System.Web.UI.Page
{
    BLLDealOrders objOrders = new BLLDealOrders();
    BLLDealOrderDetail objDetail = new BLLDealOrderDetail();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            bindOrder();
        }
    }

    public bool displayPrevious = false;
    public bool displayNext = true;
    public void bindOrder()
    {
        if (Session["member"] != null || Session["restaurant"] != null || Session["sale"] != null || Session["user"] != null)
        {

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
            if (dtUser != null && dtUser.Rows.Count > 0)
            {
                objOrders.createdBy = Convert.ToInt64(dtUser.Rows[0]["userId"].ToString());
                ViewState["userName"] = dtUser.Rows[0]["userName"].ToString();
                DataTable dtOrders;
                DataView dv;
                gvOrderItems.PageSize = Misc.clientPageSize;
                dtOrders = objOrders.getAllOwnAvailableDealOrderDetailByUserID();
                dv = new DataView(dtOrders);
                ViewState["page"] = Math.Ceiling(Convert.ToDouble(dtOrders.Rows.Count) / Convert.ToDouble(gvOrderItems.PageSize)).ToString();
                if (dtOrders != null && dtOrders.Rows.Count > 0)
                {
                    gvOrderItems.DataSource = dtOrders;
                    gvOrderItems.DataBind();
                }
                else
                {
                    gvOrderItems.DataSource = null;
                    gvOrderItems.DataBind();
                }
            }
            else
            {
                Response.Redirect("Default.aspx", false);
            }
        }
    }

    protected void gvOrderItems_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            if (e.NewPageIndex == 0)
            {
                displayPrevious = false;
            }
            else
            {
                displayPrevious = true;
            }
            if (e.NewPageIndex == gvOrderItems.PageCount - 1)
            {
                displayNext = false;
            }
            else
            {
                displayNext = true;
            }
            this.gvOrderItems.PageIndex = e.NewPageIndex;
            ViewState["pageText"] = (Convert.ToInt32(e.NewPageIndex) + 1).ToString();
            this.bindOrder();
            //ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "Paging", "<script>$('#Itemtab2').click();</script>", false);
        }
        catch (Exception ex)
        {

            string jScript;
            jScript = "<script>";
            jScript += "MessegeArea('There is an error occur, please email us at support@tazzling.com or call 1855-295-1771.' , 'error');";
            jScript += "</script>";
            ScriptManager.RegisterClientScriptBlock(this, typeof(Button), "Javascript", jScript, false);
        }
    }

    protected void gvOrderItems_Edit(object sender, GridViewEditEventArgs e)
    {
        try
        {
            TextBox txtTextArea = (TextBox)gvOrderItems.Rows[e.NewEditIndex].FindControl("txtTextArea");
            if (txtTextArea != null)
            {
                BLLDealOrders objDealNote = new BLLDealOrders();
                objDealNote.customerNote = txtTextArea.Text.Trim();
                objDealNote.dOrderID = Convert.ToInt64(gvOrderItems.DataKeys[e.NewEditIndex].Value);
                objDealNote.updateDealOrderNoteByOrderID();
                bindOrder();
                string jScript;
                jScript = "<script>";
                jScript += "MessegeArea('Note has been saved successfully.' , 'success');";
                jScript += "</script>";
                ScriptManager.RegisterClientScriptBlock(this, typeof(Button), "Javascript", jScript, false);
            }
        }
        catch (Exception ex)
        {

        }
    }

    protected void gvOrderItems_Command(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandArgument.ToString().ToLower().Trim() == "next" || e.CommandArgument.ToString().ToLower().Trim() == "prev" || e.CommandName.ToString().ToLower().Trim() == "edit")
        {
            return;
        }

        if (e.CommandName == "download")
        {
            string strOrderNumber = "";
            string[] strOrderIDs = e.CommandArgument.ToString().Split(',');
            string strOrderID = strOrderIDs[1].ToString();
            string strDetailOrderId = strOrderIDs[0].ToString();
            GECEncryption objEnc = new GECEncryption();
            strOrderNumber = objEnc.DecryptData("deatailOrder", strOrderIDs[2].ToString());
            //strOrderNumber = Misc.createPDFForGift(strDetailOrderId, strOrderID, " ");
            try
            {
                string url = ConfigurationManager.AppSettings["YourSite"] + "/tazzlingVoucher.aspx?pdf=TRUE&oid=" + strOrderID + "&did=" + strDetailOrderId;
                //string url = "http://www.tazzling.com/tastypdfdownload.aspx?did=894&pdf=true";
                //string url = ConfigurationManager.AppSettings["YourSite"] + "/tastypdfdownload.aspx?did=66&pdf=true";
                Process PDFProcess = new Process();
                string FileName = @" " + AppDomain.CurrentDomain.BaseDirectory + "Images\\ClientData\\" + strOrderNumber + ".pdf";
                PDFProcess.StartInfo.UseShellExecute = false;
                PDFProcess.StartInfo.CreateNoWindow = true;
                PDFProcess.StartInfo.FileName = AppDomain.CurrentDomain.BaseDirectory + @"bin\wkhtmltopdf.exe";
                PDFProcess.StartInfo.Arguments = url + FileName;
                PDFProcess.StartInfo.RedirectStandardOutput = true;
                PDFProcess.StartInfo.RedirectStandardError = true;
                PDFProcess.Start();
                PDFProcess.WaitForExit();
            }
            catch (Exception ex)
            {
                string xx = ex.Message.ToString();
                Response.Write("<br>" + xx);
            }

            string FilePath = AppDomain.CurrentDomain.BaseDirectory + "Images\\ClientData\\" + strOrderNumber + ".pdf";
            try
            {
                string contentType = "";
                //Get the physical path to the file.
                // string FilePath = AppDomain.CurrentDomain.BaseDirectory + "Images\\ClientData\\" + objEnc.DecryptData("deatailOrder", e.CommandArgument.ToString()) + ".pdf";

                //Set the appropriate ContentType.
                contentType = "Application/pdf";

                //Set the appropriate ContentType.

                Response.ContentType = contentType;
                Response.AppendHeader("content-disposition", "attachment; filename=" + (new FileInfo(strOrderNumber + ".pdf")).Name);

                //Write the file directly to the HTTP content output stream.
                Response.WriteFile(FilePath);
                Response.End();
            }
            catch
            {
                //To Do
            }

        }
    }

    protected void gvOrderItems_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            DataTable pageSize = new DataTable("pager");
            DataColumn pageNo = new DataColumn("pageNo");
            pageSize.Columns.Add(pageNo);

            if (e.Row.RowType == DataControlRowType.Pager)
            {
                Repeater rptrPager = (Repeater)e.Row.FindControl("rptrPage");

                if (ViewState["page"] != null)
                {
                    int count = Convert.ToInt32(ViewState["page"]);
                    for (int i = 0; i < count; i++)
                    {
                        DataRow drNewRow = pageSize.NewRow();
                        drNewRow["pageNo"] = (i + 1).ToString();
                        pageSize.Rows.Add(drNewRow);
                    }
                    rptrPager.DataSource = pageSize;
                    rptrPager.DataBind();
                }
            }

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                GridView gvSubItem = (GridView)e.Row.FindControl("gvSubItem");
                Label lblID = (Label)e.Row.FindControl("lblId");
                if (lblID != null && lblID.Text.ToString() != "" && gvSubItem != null)
                {
                    objDetail.dOrderID = Convert.ToInt64(lblID.Text.ToString());
                    //gvSubItem.DataSource = objDetail.getAllAvailableUserDealOrderDetailByOrderID().DefaultView;
                    //gvSubItem.DataBind();
                    
                }
            }
        }
        catch (Exception ex)
        {
            string jScript;
            jScript = "<script>";
            jScript += "MessegeArea('There is an error occur, please email us at support@tazzling.com or call 1855-295-1771.' , 'error');";
            jScript += "</script>";
            ScriptManager.RegisterClientScriptBlock(this, typeof(Button), "Javascript", jScript, false);
        }
    }

    protected string getDealCode(object objCode, object status, object displayIt)
    {
        if (objCode.ToString() != "")
        {
            if (Convert.ToBoolean(displayIt.ToString()))
            {
                if (status.ToString().ToLower().Trim() == "pending" || status.ToString().ToLower().Trim() == "cancelled" || status.ToString().ToLower().Trim() == "declined" || status.ToString().ToLower().Trim() == "refunded")
                {
                    return "# *******";
                }
                else
                {
                    GECEncryption objEnc = new GECEncryption();
                    return "# " + objEnc.DecryptData("deatailOrder", objCode.ToString());
                }
            }
            else
            {
                return "# *******";
            }
        }
        return "";
    }


    protected string imagePath(object objImage, object restaurantId)
    {
        if (objImage.ToString() != "")
        {
            string[] strDealImages = objImage.ToString().Split(',');
            int i = 0;
            bool imageFound = false;
            if (strDealImages.Length > 0)
            {
                for (i = 0; i < strDealImages.Length; i++)
                {
                    if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + "Images\\dealfood\\" + restaurantId.ToString().Trim() + "\\thumb\\" + strDealImages[i]))
                    {
                        imageFound = true;
                        break;
                    }
                }
            }
            if (imageFound)
            {
                return "Images/dealfood/" + restaurantId.ToString().Trim() + "/thumb/" + strDealImages[i];
            }
            else
            {
                return "Images/imageNotFound.jpg";
            }
        }
        else
        {
            return "Images/imageNotFound.jpg";
        }

    }

    protected void lnkPage_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton pageLink = (LinkButton)sender;
            ViewState["pageText"] = pageLink.Text.ToString();
            if (pageLink.CommandName == "Page")
            {
                if (Convert.ToInt32(pageLink.CommandArgument) - 1 == 0)
                {
                    displayPrevious = false;
                }
                else
                {
                    displayPrevious = true;
                }
                if (Convert.ToInt32(pageLink.CommandArgument) == Convert.ToInt32(ViewState["page"]))
                {
                    displayNext = false;
                }
                else
                {
                    displayNext = true;
                }

                this.gvOrderItems.PageIndex = Convert.ToInt32(pageLink.CommandArgument) - 1;

                this.bindOrder();
                string jScript;

            }
        }
        catch (Exception ex)
        {
            string jScript;
            jScript = "<script>";
            jScript += "MessegeArea('There is an error occur, please email us at support@tazzling.com or call 1855-295-1771.' , 'error');";
            jScript += "</script>";
            ScriptManager.RegisterClientScriptBlock(this, typeof(Button), "Javascript", jScript, false);

        }
    }

    protected bool GetStatus(object objPageNum)
    {
        string pageNum = objPageNum.ToString();
        string selectedPageNum = "";
        if (ViewState["pageText"] != null)
        {
            selectedPageNum = ViewState["pageText"].ToString();
        }
        else
        {
            ViewState["pageText"] = 1;
            selectedPageNum = 1.ToString();
        }

        if (pageNum == selectedPageNum)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    protected bool getDetailStatus(object status, object displayIt)
    {
        if (status.ToString() != "")
        {
            if (Convert.ToBoolean(displayIt.ToString()))
            {
                if (status.ToString().ToLower().Trim() == "pending" || status.ToString().ToLower().Trim() == "cancelled" || status.ToString().ToLower().Trim() == "declined" || status.ToString().ToLower().Trim() == "refunded")
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return false;
            }
        }
        return false;
    }

}
