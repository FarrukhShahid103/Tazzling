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
using System.Diagnostics;

public partial class member_MyGiftTastygo : System.Web.UI.Page
{

    BLLDealOrders objOrders = new BLLDealOrders();
    BLLDealOrderDetail objDetail = new BLLDealOrderDetail();

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                if (Session["orderID"] != null)
                {
                    Session.Remove("orderID");
                }
                if (Session["detailID"] != null)
                {
                    Session.Remove("detailID");
                }
                if (Session["gift"] != null)
                {
                    Session.Remove("gift");
                }              

                Page.Title = ConfigurationManager.AppSettings["pageTitleStart"].ToString().Trim() + " | Member | My Gift TastyGos";
                if (Session["member"] != null || Session["restaurant"] != null || Session["sale"] != null || Session["user"] != null)
                {
                    bindAllGrids();
                    PageTabScript();
                }
                else
                {
                    Response.Redirect("Default.aspx", false);
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

    private void bindAllGrids()
    {
        bindGrid();
        bindGridUsed();
        bindGridExpired();
        bindGridCancelled();
    }
    private void PageTabScript()
    {
        TabScript.Text = "<script type='text/javascript'>";
        TabScript.Text += "$(document).ready(function() {";
        TabScript.Text += "$('#coda-slider-1').codaSlider({";
        TabScript.Text += "dynamicArrows: false";
        TabScript.Text += " });";
        TabScript.Text += "$('#Itemtab1').hide();";
        TabScript.Text += "$('#Itemtab2').hide();";
        TabScript.Text += "$('#Itemtab3').hide();";
        TabScript.Text += "$('#Itemtab4').hide();";
        TabScript.Text += "$('#Itemtab5').hide();";
        TabScript.Text += "});";
        TabScript.Text += "</script>";

    }
    protected void gvExpired_PageIndexChanging(object sender, GridViewPageEventArgs e)
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
            if (e.NewPageIndex == gvExpired.PageCount - 1)
            {
                displayNext = false;
            }
            else
            {
                displayNext = true;
            }
            this.gvExpired.PageIndex = e.NewPageIndex;
            ViewState["pageText"] = (Convert.ToInt32(e.NewPageIndex) + 1).ToString();
            this.bindGridExpired();
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "Paging", "<script>$('#Itemtab4').click();</script>", false);
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
    protected void gvExpired_RowDataBound(object sender, GridViewRowEventArgs e)
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
                GridView gvSubExpired = (GridView)e.Row.FindControl("gvSubExpired");

                Label lblExpiredID = (Label)e.Row.FindControl("lblExpiredID");
                if (lblExpiredID != null && lblExpiredID.Text.ToString() != "")
                {
                    objDetail.dOrderID = Convert.ToInt64(lblExpiredID.Text.ToString());
                    gvSubExpired.DataSource = objDetail.getAllGiftUserDealOrderDetailByOrderID().DefaultView;
                    gvSubExpired.DataBind();
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
    protected void gvExpired_Login(object sender, GridViewCommandEventArgs e)
    {

        //int value = Convert.ToInt32(e.CommandArgument);
        if (e.CommandArgument.ToString().ToLower().Trim() == "next" ||e.CommandArgument.ToString().ToLower().Trim() == "prev" || e.CommandName.ToString().ToLower().Trim() == "edit")
        {
            return;
        }
        bool result = false;
        try
        {
            objOrders.dOrderID = Convert.ToInt64(e.CommandArgument);
            if (((LinkButton)e.CommandSource).Text.Trim() == "Cancel")
            {
                objOrders.status = "Cancelled";
                CancelDeclinedAffiliateCommissionByOrderId(objOrders.dOrderID.ToString(), "Cancelled");
            }
            else
            {
                objOrders.status = "Pending";
            }
            result = objOrders.changeDealOrderStatus();
            if (result)
            {

                bindAllGrids();
                string jScript;
                jScript = "<script>";
                jScript += "MessegeArea('Status has been changed successfully.' , 'success');";
                jScript += "</script>";
                ScriptManager.RegisterClientScriptBlock(this, typeof(Button), "Javascript", jScript, false);                                                                              
                DataTable dtOrderDetail = objOrders.getDealOrderDetailByOrderID();
                if (dtOrderDetail != null && dtOrderDetail.Rows.Count > 0)
                {
                    SendMailToAdminForDealStatus(dtOrderDetail, objOrders.status);
                }

            }
            else
            {
                bindAllGrids();

                string jScript;
                jScript = "<script>";
                jScript += "MessegeArea('Status has not been changed successfully.' , 'error');";
                jScript += "</script>";
                ScriptManager.RegisterClientScriptBlock(this, typeof(Button), "Javascript", jScript, false);                                                                                            
            }
        }
        catch (Exception ex)
        {
            string jScript;
            jScript = "<script>";
            jScript += "MessegeArea('"+ex.Message+"' , 'error');";
            jScript += "</script>";
            ScriptManager.RegisterClientScriptBlock(this, typeof(Button), "Javascript", jScript, false);                                                                              
        }

    }
    protected void gvExpired_Edit(object sender, GridViewEditEventArgs e)
    {

        try
        {
            TextBox txtvoucherNoteExpired = (TextBox)gvExpired.Rows[e.NewEditIndex].FindControl("txtvoucherNoteExpired");
            if (txtvoucherNoteExpired != null)
            {
                BLLDealOrders objDealNote = new BLLDealOrders();
                objDealNote.customerNote = txtvoucherNoteExpired.Text.Trim();
                objDealNote.dOrderID = Convert.ToInt64(gvExpired.DataKeys[e.NewEditIndex].Value);
                objDealNote.updateDealOrderNoteByOrderID();
                bindAllGrids();
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
    protected void gvSubExpired_Login(object sender, GridViewCommandEventArgs e)
    {

        //int value = Convert.ToInt32(e.CommandArgument);
        bool result = false;
        try
        {
            if (e.CommandName == "Login")
            {
                objDetail.detailID = Convert.ToInt64(e.CommandArgument);
                if (((LinkButton)e.CommandSource).Text.Trim() == "Mark as used")
                {
                    objDetail.markUsed = true;
                }
                else
                {
                    objDetail.markUsed = false;
                }
                result = objDetail.changeOrderDetailStatus();
                if (result)
                {


                    bindAllGrids();

                    string jScript;
                    jScript = "<script>";
                    jScript += "MessegeArea('Status has been changed successfully.' , 'success');";
                    jScript += "</script>";
                    ScriptManager.RegisterClientScriptBlock(this, typeof(Button), "Javascript", jScript, false);                                                                      
                }
                else
                {

                    bindAllGrids();
                    string jScript;
                    jScript = "<script>";
                    jScript += "MessegeArea('Status has not been changed successfully.' , 'error');";
                    jScript += "</script>";
                    ScriptManager.RegisterClientScriptBlock(this, typeof(Button), "Javascript", jScript, false);                                                                      
                }
            }
            else if (e.CommandName == "download")
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
                    string url = ConfigurationManager.AppSettings["YourSite"] + "/tastyvoucher.aspx?pdf=TRUE&oid=" + strOrderID + "&did=" + strDetailOrderId;
                    //string url = "http://www.demo.tazzling.com/Default2.aspx?oid=243&did=305";
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
            else if (e.CommandName == "view")
            {
                string[] strOrderIDs = e.CommandArgument.ToString().Split(',');
                string strOrderID = strOrderIDs[1].ToString();
                string strDetailOrderId = strOrderIDs[0].ToString();
                Session["orderID"] = strOrderID;
                Session["detailID"] = strDetailOrderId;
                Response.Redirect("tastyvoucher.aspx", false);
            }
        }
        catch (Exception ex)
        {
            string jScript;
            jScript = "<script>";
            jScript += "MessegeArea('"+ex.Message+"' , 'error');";
            jScript += "</script>";
            ScriptManager.RegisterClientScriptBlock(this, typeof(Button), "Javascript", jScript, false);                                                                              
          
        }

    }
    protected void lnkPageExpired_Click(object sender, EventArgs e)
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

                this.gvExpired.PageIndex = Convert.ToInt32(pageLink.CommandArgument) - 1;

                this.bindAllGrids();
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "Paging", "<script>$('#Itemtab4').click();</script>", false);
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

     #region Function to Bind Grid
    protected void bindGridExpired()
    {
        try
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
                    gvExpired.PageSize = Misc.clientPageSize;
                    dtOrders = objOrders.getAllGiftExpiredDealOrderDetailByUserID();
                    dv = new DataView(dtOrders);
                    ViewState["page"] = Math.Ceiling(Convert.ToDouble(dtOrders.Rows.Count) / Convert.ToDouble(gvExpired.PageSize)).ToString();
                    if (dtOrders != null && dtOrders.Rows.Count > 0)
                    {
                        gvExpired.DataSource = dv;
                        gvExpired.DataBind();
                    }
                    else
                    {
                        gvExpired.DataSource = null;
                        gvExpired.DataBind();
                    }
                }
                else
                {
                    Response.Redirect("Default.aspx", false);
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
    #endregion

    #region Function to Bind Grid
    protected void bindGrid()
    {
        try
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
                    gridview1.PageSize = Misc.clientPageSize;
                    dtOrders = objOrders.getAllGiftAvailableDealOrderDetailByUserID();
                    dv = new DataView(dtOrders);
                    ViewState["page"] = Math.Ceiling(Convert.ToDouble(dtOrders.Rows.Count) / Convert.ToDouble(gridview1.PageSize)).ToString();
                    if (dtOrders != null && dtOrders.Rows.Count > 0)
                    {
                        gridview1.DataSource = dv;
                        gridview1.DataBind();
                    }
                    else
                    {
                        gridview1.DataSource = null;
                        gridview1.DataBind();
                    }
                }
                else
                {
                    Response.Redirect("Default.aspx", false);
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
    protected void bindGridUsed()
    {
        try
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
                    gvUsed.PageSize = Misc.clientPageSize;
                    dtOrders = objOrders.getAllGiftUsedDealOrderDetailByUserID();
                    dv = new DataView(dtOrders);
                    ViewState["page"] = Math.Ceiling(Convert.ToDouble(dtOrders.Rows.Count) / Convert.ToDouble(gvUsed.PageSize)).ToString();
                    if (dtOrders != null && dtOrders.Rows.Count > 0)
                    {
                        gvUsed.DataSource = dv;
                        gvUsed.DataBind();
                    }
                    else
                    {
                        gvUsed.DataSource = null;
                        gvUsed.DataBind();
                    }
                }
                else
                {
                    Response.Redirect("Default.aspx", false);
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
    protected void bindGridCancelled()
    {
        try
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
                    gvcancelled.PageSize = Misc.clientPageSize;
                    dtOrders = objOrders.getAllGiftCancelledDealOrderDetailByUserID();
                    dv = new DataView(dtOrders);
                    ViewState["page"] = Math.Ceiling(Convert.ToDouble(dtOrders.Rows.Count) / Convert.ToDouble(gvcancelled.PageSize)).ToString();
                    if (dtOrders != null && dtOrders.Rows.Count > 0)
                    {
                        gvcancelled.DataSource = dv;
                        gvcancelled.DataBind();
                    }
                    else
                    {
                        gvcancelled.DataSource = null;
                        gvcancelled.DataBind();
                    }
                }
                else
                {
                    Response.Redirect("Default.aspx", false);
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
    #endregion

    protected string GetDateString(object objDate)
    {
        if (objDate.ToString() != "")
        {
            DateTime dt = Convert.ToDateTime(objDate);
            return dt.ToString("MM-dd-yyyy");
        }
        return "not available";
    }


    protected void gridview1_Edit(object sender, GridViewEditEventArgs e)
    {

        try
        {
            TextBox txtvoucherNote = (TextBox)gridview1.Rows[e.NewEditIndex].FindControl("txtvoucherNote");
            if (txtvoucherNote != null)
            {
                BLLDealOrders objDealNote = new BLLDealOrders();
                objDealNote.customerNote = txtvoucherNote.Text.Trim();
                objDealNote.dOrderID = Convert.ToInt64(gridview1.DataKeys[e.NewEditIndex].Value);
                objDealNote.updateDealOrderNoteByOrderID();
                bindAllGrids();

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

    protected void gvUsed_Edit(object sender, GridViewEditEventArgs e)
    {

        try
        {
            TextBox txtvoucherNoteUsed = (TextBox)gvUsed.Rows[e.NewEditIndex].FindControl("txtvoucherNoteUsed");
            if (txtvoucherNoteUsed != null)
            {
                BLLDealOrders objDealNote = new BLLDealOrders();
                objDealNote.customerNote = txtvoucherNoteUsed.Text.Trim();
                objDealNote.dOrderID = Convert.ToInt64(gvUsed.DataKeys[e.NewEditIndex].Value);
                objDealNote.updateDealOrderNoteByOrderID();
                bindAllGrids();
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
    

    protected bool getDetailStatusForLink(object status,object userID)
    {
        if (status.ToString() != "")
        {
            if (status.ToString().ToLower().Trim() == "pending" || status.ToString().ToLower().Trim() == "cancelled" || status.ToString().ToLower().Trim() == "declined" || status.ToString().ToLower().Trim() == "refunded")
            {
                return false;
            }
            else
            {
                if (userID.ToString().Trim() != "" && userID.ToString().Trim() != "0")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        return false;
    }

    protected bool getDetailStatusForPopup(object status, object userID)
    {
        if (status.ToString() != "")
        {
            if (status.ToString().ToLower().Trim() == "pending" || status.ToString().ToLower().Trim() == "cancelled" || status.ToString().ToLower().Trim() == "declined" || status.ToString().ToLower().Trim() == "refunded")
            {
                return false;
            }
            else
            {
                if (userID.ToString().Trim() != "" && userID.ToString().Trim() != "0")
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }
        return false;
    }

    protected string getDealPrintPath(object objCode, object status)
    {
        if (objCode.ToString() != "")
        {
            if (status.ToString().ToLower().Trim() == "pending" || status.ToString().ToLower().Trim() == "cancelled" || status.ToString().ToLower().Trim() == "declined" || status.ToString().ToLower().Trim() == "refunded")
            {
                return "#";
            }
            else
            {
                GECEncryption objEnc = new GECEncryption();
                return "Images/ClientData/" + objEnc.DecryptData("deatailOrder", objCode.ToString()) + ".pdf";
            }
        }
        return "";
    }

    protected string getOrderStatus(object status)
    {
        if (status.ToString() != "")
        {
            if (status.ToString().ToLower().Trim() == "pending")
            {
                return "Cancel";
            }        
            else
            {
                return "";
            }

        }
        return "";
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

    protected string getDealCodeForEmail(object objCode)
    {
        if (objCode.ToString() != "")
        {
            GECEncryption objEnc = new GECEncryption();
            return objEnc.DecryptData("deatailOrder", objCode.ToString());
        }
        return "";
    }

    protected string getDealStatus(object objStatus)
    {
        if (objStatus.ToString() != "")
        {
            try
            {
                if (Convert.ToBoolean(objStatus))
                {
                    return "Undo";
                }
                else
                {
                    return "Mark as used";
                }

            }
            catch (Exception ex)
            {
                return "Mark as used";
            }
        }
        return "Mark as used";
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
      
                 
    public bool displayPrevious = false;
    
    public bool displayNext = true;

    protected void gvUsed_PageIndexChanging(object sender, GridViewPageEventArgs e)
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
            if (e.NewPageIndex == gvUsed.PageCount - 1)
            {
                displayNext = false;
            }
            else
            {
                displayNext = true;
            }
            this.gvUsed.PageIndex = e.NewPageIndex;
            ViewState["pageText"] = (Convert.ToInt32(e.NewPageIndex) + 1).ToString();
            this.bindAllGrids();
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "Paging", "<script>$('#Itemtab2').click();</script>", false);
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

    protected void gvcancelled_PageIndexChanging(object sender, GridViewPageEventArgs e)
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
            if (e.NewPageIndex == gvcancelled.PageCount - 1)
            {
                displayNext = false;
            }
            else
            {
                displayNext = true;
            }
            this.gvcancelled.PageIndex = e.NewPageIndex;
            ViewState["pageText"] = (Convert.ToInt32(e.NewPageIndex) + 1).ToString();
            this.bindAllGrids();
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

    protected void gridview1_PageIndexChanging(object sender, GridViewPageEventArgs e)
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
            if (e.NewPageIndex == gridview1.PageCount - 1)
            {
                displayNext = false;
            }
            else
            {
                displayNext = true;
            }
            this.gridview1.PageIndex = e.NewPageIndex;
            ViewState["pageText"] = (Convert.ToInt32(e.NewPageIndex) + 1).ToString();
            this.bindAllGrids();
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "Paging", "<script>$('#Itemtab1').click();</script>", false);
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
    
    protected void gvUsed_RowDataBound(object sender, GridViewRowEventArgs e)
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
                GridView gvSubUsed = (GridView)e.Row.FindControl("gvSubUsed");

                Label lblID = (Label)e.Row.FindControl("lblUsedID");
                if (lblID != null && lblID.Text.ToString() != "")
                {
                    objDetail.dOrderID = Convert.ToInt64(lblID.Text.ToString());
                    gvSubUsed.DataSource = objDetail.getAllUsedUserGiftDealOrderDetailByOrderID().DefaultView;
                    gvSubUsed.DataBind();
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

    protected void gvcancelled_RowDataBound(object sender, GridViewRowEventArgs e)
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

    protected void gridview1_RowDataBound(object sender, GridViewRowEventArgs e)
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
                if (lblID != null && lblID.Text.ToString() != "")
                {
                    objDetail.dOrderID = Convert.ToInt64(lblID.Text.ToString());
                    gvSubItem.DataSource = objDetail.getAllAvailableGiftUserDealOrderDetailByOrderID().DefaultView;
                    gvSubItem.DataBind();
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

    protected void gvUsed_Login(object sender, GridViewCommandEventArgs e)
    {

        if (e.CommandArgument.ToString().ToLower().Trim() == "next" ||e.CommandArgument.ToString().ToLower().Trim() == "prev" || e.CommandName.ToString().ToLower().Trim() == "edit")
        {
            return;
        }
        //int value = Convert.ToInt32(e.CommandArgument);
        bool result = false;
        try
        {
            objOrders.dOrderID = Convert.ToInt64(e.CommandArgument);
            if (((LinkButton)e.CommandSource).Text.Trim() == "Cancel")
            {
                objOrders.status = "Cancelled";
                CancelDeclinedAffiliateCommissionByOrderId(objOrders.dOrderID.ToString(), "Cancelled");
            }
            else
            {
                objOrders.status = "Pending";
            }
            result = objOrders.changeDealOrderStatus();
            if (result)
            {
                bindAllGrids();

                string jScript;
                jScript = "<script>";
                jScript += "MessegeArea('Status has been changed successfully.' , 'success');";
                jScript += "</script>";
                ScriptManager.RegisterClientScriptBlock(this, typeof(Button), "Javascript", jScript, false);                                                                              
                                                             
                DataTable dtOrderDetail = objOrders.getDealOrderDetailByOrderID();
                if (dtOrderDetail != null && dtOrderDetail.Rows.Count > 0)
                {
                    SendMailToAdminForDealStatus(dtOrderDetail, objOrders.status);
                }
            }
            else
            {
                bindAllGrids();
                string jScript;
                jScript = "<script>";
                jScript += "MessegeArea('Status has not been changed successfully.' , 'error');";
                jScript += "</script>";
                ScriptManager.RegisterClientScriptBlock(this, typeof(Button), "Javascript", jScript, false);                                                       
            }
        }
        catch (Exception ex)
        {
            string jScript;
            jScript = "<script>";
            jScript += "MessegeArea('"+ex.Message+"' , 'error');";
            jScript += "</script>";
            ScriptManager.RegisterClientScriptBlock(this, typeof(Button), "Javascript", jScript, false);                                                       
        }

    }

    protected void gvcancelled_Login(object sender, GridViewCommandEventArgs e)
    {

        if (e.CommandArgument.ToString().ToLower().Trim() == "next" ||e.CommandArgument.ToString().ToLower().Trim() == "prev" || e.CommandName.ToString().ToLower().Trim() == "edit")
        {
            return;
        }
        //int value = Convert.ToInt32(e.CommandArgument);
        bool result = false;
        try
        {
            objOrders.dOrderID = Convert.ToInt64(e.CommandArgument);
            if (((LinkButton)e.CommandSource).Text.Trim() == "Cancel")
            {
                objOrders.status = "Cancelled";
                CancelDeclinedAffiliateCommissionByOrderId(objOrders.dOrderID.ToString(), "Cancelled");
            }
            else
            {
                objOrders.status = "Pending";
            }
            result = objOrders.changeDealOrderStatus();
            if (result)
            {
                bindAllGrids();
                string jScript;
                jScript = "<script>";
                jScript += "MessegeArea('Status has been changed successfully.' , 'success');";
                jScript += "</script>";
                ScriptManager.RegisterClientScriptBlock(this, typeof(Button), "Javascript", jScript, false);                                                                              

                DataTable dtOrderDetail = objOrders.getDealOrderDetailByOrderID();
                if (dtOrderDetail != null && dtOrderDetail.Rows.Count > 0)
                {
                    SendMailToAdminForDealStatus(dtOrderDetail, objOrders.status);
                }
            }
            else
            {
                bindAllGrids();



                string jScript;
                jScript = "<script>";
                jScript += "MessegeArea('Status has not been changed successfully.' , 'error');";
                jScript += "</script>";
                ScriptManager.RegisterClientScriptBlock(this, typeof(Button), "Javascript", jScript, false);                                                                   
            }
        }
        catch (Exception ex)
        {
            
            string jScript;
            jScript = "<script>";
            jScript += "MessegeArea('"+ex.Message +"' , 'error');";
            jScript += "</script>";
            ScriptManager.RegisterClientScriptBlock(this, typeof(Button), "Javascript", jScript, false);                                                                   
           
        }

    }

    protected void gridview1_Login(object sender, GridViewCommandEventArgs e)
    {

        if (e.CommandArgument.ToString().ToLower().Trim() == "next" ||e.CommandArgument.ToString().ToLower().Trim() == "prev" || e.CommandName.ToString().ToLower().Trim() == "edit")
        {
            return;
        }
        //int value = Convert.ToInt32(e.CommandArgument);
        bool result = false;
        try
        {
            objOrders.dOrderID = Convert.ToInt64(e.CommandArgument);
            if (((LinkButton)e.CommandSource).Text.Trim() == "Cancel")
            {
                objOrders.status = "Cancelled";
                CancelDeclinedAffiliateCommissionByOrderId(objOrders.dOrderID.ToString(), "Cancelled");
            }
            else
            {
                objOrders.status = "Pending";
            }
            result = objOrders.changeDealOrderStatus();
            if (result)
            {
                bindAllGrids();
                string jScript;
                jScript = "<script>";
                jScript += "MessegeArea('Status has been changed successfully.' , 'success');";
                jScript += "</script>";
                ScriptManager.RegisterClientScriptBlock(this, typeof(Button), "Javascript", jScript, false);                                                                              

                DataTable dtOrderDetail = objOrders.getDealOrderDetailByOrderID();
                if (dtOrderDetail != null && dtOrderDetail.Rows.Count > 0)
                {
                    SendMailToAdminForDealStatus(dtOrderDetail, objOrders.status);
                }
            }
            else
            {
                bindAllGrids();



                string jScript;
                jScript = "<script>";
                jScript += "MessegeArea('Status has not been changed successfully.' , 'error');";
                jScript += "</script>";
                ScriptManager.RegisterClientScriptBlock(this, typeof(Button), "Javascript", jScript, false);                                                                   
            }
        }
        catch (Exception ex)
        {
            string jScript;
            jScript = "<script>";
            jScript += "MessegeArea('" + ex.Message + "' , 'error');";
            jScript += "</script>";
            ScriptManager.RegisterClientScriptBlock(this, typeof(Button), "Javascript", jScript, false);                                                                   
        }

    }

    private bool CancelDeclinedAffiliateCommissionByOrderId(string OrderID, string strGainedType)
    {
        bool bStatus = false;

        try
        {
            DataTable dtAdmin = (DataTable)Session["user"];

            DataTable dtAffiliateCommInfo = null;

            BLLAffiliatePartnerGained objBLLAffiliatePartnerGained = new BLLAffiliatePartnerGained();

            objBLLAffiliatePartnerGained.OrderId = Convert.ToInt32(OrderID);

            dtAffiliateCommInfo = objBLLAffiliatePartnerGained.getGetAffiliatePartnerCommisionInfoByOrderID();

            if ((dtAffiliateCommInfo != null) && (dtAffiliateCommInfo.Rows.Count > 0))
            {
                string strTotalAmount = dtAffiliateCommInfo.Rows[0]["totalAmt"] == DBNull.Value ? "0" : dtAffiliateCommInfo.Rows[0]["totalAmt"].ToString().Trim();
                string strAffComm = dtAffiliateCommInfo.Rows[0]["affCommPer"] == DBNull.Value ? "0" : dtAffiliateCommInfo.Rows[0]["affCommPer"].ToString().Trim();

                //objBLLAffiliatePartnerGained.GainedType = "Cancelled";
                objBLLAffiliatePartnerGained.GainedType = strGainedType;

                //Add $1.85 % Amount of Total Amount into the User Account
                //objBLLAffiliatePartnerGained.GainedAmount = (float.Parse(strTotalAmount) * (float.Parse(strAffComm) / 100));
                objBLLAffiliatePartnerGained.GainedAmount = 0;
                //objBLLAffiliatePartnerGained.RemainAmount = (float.Parse(strTotalAmount) * (float.Parse(strAffComm) / 100));
                objBLLAffiliatePartnerGained.RemainAmount = 0;

                objBLLAffiliatePartnerGained.ModifiedBy = Convert.ToInt32(dtAdmin.Rows[0]["userId"].ToString());

                objBLLAffiliatePartnerGained.OrderId = int.Parse(OrderID.ToString());

                if (objBLLAffiliatePartnerGained.updateAffiliateCommisionByOrderId() == -1)
                {
                    bStatus = true;
                }
            }
        }
        catch (Exception ex)
        { }

        return bStatus;
    }

    private bool SendMailToAdminForDealStatus(DataTable dtOrderDetail, string strStatus)
    {
        MailMessage message = new MailMessage();

        StringBuilder mailBody = new StringBuilder();

        try
        {
            string toAddress = "support@tazzling.com";
            string fromAddress = ConfigurationManager.AppSettings["AdminEmail"].ToString().Trim();
            string Subject = ConfigurationManager.AppSettings["EmailSubjectChageOrderStatus"].ToString().Trim() + " " + strStatus;
            message.IsBodyHtml = true;
            mailBody.Append("<!DOCTYPE html PUBLIC '-//W3C//DTD XHTML 1.0 Transitional//EN' 'http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd'>");
            mailBody.Append("<html xmlns='http://www.w3.org/1999/xhtml'><head><title></title></head><body style='font-family: Century;'>");
            mailBody.Append("<h4>Deal Admin.");
            mailBody.Append("</h4>");
            mailBody.Append("<p><font size='3'>Status for order number \"" + dtOrderDetail.Rows[0]["orderNo"] + "\" has been changed to " + strStatus + " by user \"" + ViewState["userName"].ToString()+"\".<br>");
            if (dtOrderDetail.Rows[0]["psgTranNo"].ToString().Trim() != "")
            {
                mailBody.Append("Psigate Transaction number is \"" + dtOrderDetail.Rows[0]["psgTranNo"].ToString().Trim() + "\"</font></p>");
            }
            else
            {
                mailBody.Append("User did not pay from credit card.</font></p>");
            }
            mailBody.Append("<p>" + ConfigurationManager.AppSettings["EmailSignature"].ToString().Trim() + "</p></body></html>");
            message.Body = mailBody.ToString();

            return Misc.SendEmail(toAddress, "", "", fromAddress, Subject, message.Body);
        }
        catch (Exception ex)
        {
            string jScript;
            jScript = "<script>";
            jScript += "MessegeArea('There is an error occur, please email us at support@tazzling.com or call 1855-295-1771.' , 'error');";
            jScript += "</script>";
            ScriptManager.RegisterClientScriptBlock(this, typeof(Button), "Javascript", jScript, false);                                                     
            return false;
        }
    }

    protected void gvSubItem_Login(object sender, GridViewCommandEventArgs e)
    {

        //int value = Convert.ToInt32(e.CommandArgument);
        bool result = false;
        try
        {
            if (e.CommandName == "Login")
            {
                objDetail.detailID = Convert.ToInt64(e.CommandArgument);

                if (((LinkButton)e.CommandSource).Text.Trim() == "Mark as used")
                {
                    objDetail.markUsed = true;
                }
                else
                {
                    objDetail.markUsed = false;
                }
                result = objDetail.changeOrderDetailStatus();
                if (result)
                {                    
                    bindAllGrids();
                    string jScript;
                    jScript = "<script>";
                    jScript += "MessegeArea('Status has been changed successfully.' , 'success');";
                    jScript += "</script>";
                    ScriptManager.RegisterClientScriptBlock(this, typeof(Button), "Javascript", jScript, false);                                                                              
                }
                else
                {

                    bindAllGrids();                    
                    string jScript;
                    jScript = "<script>";
                    jScript += "MessegeArea('Status has not been changed successfully.' , 'error');";
                    jScript += "</script>";
                    ScriptManager.RegisterClientScriptBlock(this, typeof(Button), "Javascript", jScript, false);                                             
                }
            }
            else if (e.CommandName == "download")
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
                    string url = ConfigurationManager.AppSettings["YourSite"] + "/tastyvoucher.aspx?pdf=TRUE&oid=" + strOrderID + "&did=" + strDetailOrderId;
                    //string url = "http://www.demo.tazzling.com/Default2.aspx?oid=243&did=305";
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
            else if (e.CommandName == "view")
            {
                string[] strOrderIDs = e.CommandArgument.ToString().Split(',');
                string strOrderID = strOrderIDs[1].ToString();
                string strDetailOrderId = strOrderIDs[0].ToString();
                Session["orderID"] = strOrderID;
                Session["detailID"] = strDetailOrderId;
                Session["gift"] = "true";
                Response.Redirect("tastyvoucher.aspx", false);
            }

        }
        catch (Exception ex)
        {

            string jScript;
            jScript = "<script>";
            jScript += "MessegeArea('"+ex.Message+"' , 'error');";
            jScript += "</script>";
            ScriptManager.RegisterClientScriptBlock(this, typeof(Button), "Javascript", jScript, false);                                             
        }

    }

    protected void gvSubUsed_Login(object sender, GridViewCommandEventArgs e)
    {

        //int value = Convert.ToInt32(e.CommandArgument);
        bool result = false;
        try
        {
            if (e.CommandName == "Login")
            {
                objDetail.detailID = Convert.ToInt64(e.CommandArgument);

                if (((LinkButton)e.CommandSource).Text.Trim() == "Mark as used")
                {
                    objDetail.markUsed = true;
                }
                else
                {
                    objDetail.markUsed = false;
                }
                result = objDetail.changeOrderDetailStatus();
                if (result)
                {                    
                    bindAllGrids();
                    string jScript;
                    jScript = "<script>";
                    jScript += "MessegeArea('Status has been changed successfully.' , 'success');";
                    jScript += "</script>";
                    ScriptManager.RegisterClientScriptBlock(this, typeof(Button), "Javascript", jScript, false);                                                                              


                }
                else
                {
                    bindAllGrids();
                    string jScript;
                    jScript = "<script>";
                    jScript += "MessegeArea('Status has not been changed successfully.' , 'error');";
                    jScript += "</script>";
                    ScriptManager.RegisterClientScriptBlock(this, typeof(Button), "Javascript", jScript, false);                                  
                }
            }
            else if (e.CommandName == "download")
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
                    string url = ConfigurationManager.AppSettings["YourSite"] + "/tastyvoucher.aspx?pdf=TRUE&oid=" + strOrderID + "&did=" + strDetailOrderId;
                    //string url = "http://www.demo.tazzling.com/Default2.aspx?oid=243&did=305";
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
            else if (e.CommandName == "view")
            {
                string[] strOrderIDs = e.CommandArgument.ToString().Split(',');
                string strOrderID = strOrderIDs[1].ToString();
                string strDetailOrderId = strOrderIDs[0].ToString();
                Session["orderID"] = strOrderID;
                Session["detailID"] = strDetailOrderId;
                Session["gift"] = "true";
                Response.Redirect("tastyvoucher.aspx", false);
            }

        }
        catch (Exception ex)
        {
            string jScript;
            jScript = "<script>";
            jScript += "MessegeArea('"+ex.Message+"' , 'error');";
            jScript += "</script>";
            ScriptManager.RegisterClientScriptBlock(this, typeof(Button), "Javascript", jScript, false);                                  
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

                this.gridview1.PageIndex = Convert.ToInt32(pageLink.CommandArgument) - 1;

                this.bindAllGrids();
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "Paging", "<script>$('#Itemtab1').click();</script>", false);
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

    protected void lnkPageUsed_Click(object sender, EventArgs e)
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

                this.gvUsed.PageIndex = Convert.ToInt32(pageLink.CommandArgument) - 1;

                this.bindAllGrids();
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "Paging", "<script>$('#Itemtab2').click();</script>", false);
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

    protected void lnkCancellPage_Click(object sender, EventArgs e)
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

                this.gvcancelled.PageIndex = Convert.ToInt32(pageLink.CommandArgument) - 1;

                this.bindAllGrids();
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
    
    protected System.Drawing.Color GetColor(object objPageNum)
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
            return System.Drawing.Color.FromArgb(255, 163, 112);
        }
        else
        {
            return System.Drawing.Color.FromArgb(38, 145, 191);
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
    
    protected void btnPrintVoucher_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            RegisterStartupScript("jsCloseDialg", "closeDialog();");

            //For Full name and Email in encrypted format
            string strOrderNumber = "";
            string strOrderID = this.hdDealCode.Value;
            string strDetailOrderId = this.hdId.Value;
            strOrderNumber = Misc.createPDFForGift(strDetailOrderId, strOrderID, txtFirstName.Text.Trim() + " " + txtLastName.Text.Trim());
            string strFilePath = AppDomain.CurrentDomain.BaseDirectory + "Images\\ClientData\\" + strOrderNumber + ".pdf";
            hdDealCode.Value = strOrderNumber + ".pdf";
            RegisterStartupScript("ShowPDF", "ShowPDF();");           
        }
        catch (Exception ex)
        {
         //Response.Redirect()
            string jScript;
            jScript = "<script>";
            jScript += "MessegeArea('There is an error occur, please email us at support@tazzling.com or call 1855-295-1771.' , 'error');";
            jScript += "</script>";
            ScriptManager.RegisterClientScriptBlock(this, typeof(Button), "Javascript", jScript, false);                                  
        }
    }
    protected void btnSendEmail_Click(object sender, EventArgs e)
    {
        try
        {
            RegisterStartupScript("jsCloseDialg", "closeDialog();");

            //For Full name and Email in encrypted format
            string strFromFullName = "";
            string strToEmailEncrypt = "";
            string strSenderEmailEncrypt = "";

            //Get the Detail Order Id
            string strDetailOrderId = this.hdId.Value;

           // string[] strIDs = this.hdDealCode.Value.Split('_');
            string strOrderID = this.hdDealCode.Value.Trim();
            
            string strOrderNumber = Misc.createPDFForGift(strDetailOrderId, strOrderID, txtEFirstName.Text.Trim() + " " + txtELastName.Text.Trim());
            string strFilePath = AppDomain.CurrentDomain.BaseDirectory + "Images\\ClientData\\" + strOrderNumber + ".pdf";

            //Get the To Email
            string strToEmail = this.txtEmil.Text.Trim();

            HttpCookie yourCity = Request.Cookies["yourCity"];
            string strCityid = "337";
            if (yourCity != null)
            {
                strCityid = yourCity.Values[0].ToString().Trim();
            }
            Misc.addSubscriberEmail(txtEmil.Text.Trim(), strCityid);

            //Get the Email Message
            string strEmailMessage = this.txtMesage.Text.Trim();

            //Get the Deal Code
            string strDealCode = "";

            //Get the Full Name and Email ID of the User
            if (Session["member"] != null || Session["restaurant"] != null || Session["sale"] != null || Session["user"]!=null)
            {
                DataTable dtUserInfo = null;

                if (Session["member"] != null)
                {
                    dtUserInfo = (DataTable)(Session["member"]);
                }
                else if (Session["restaurant"] != null)
                {
                    dtUserInfo = (DataTable)(Session["restaurant"]);
                }
                else if (Session["sale"] != null)
                {
                    dtUserInfo = (DataTable)(Session["sale"]);
                }
                else if (Session["user"] != null)
                {
                    dtUserInfo = (DataTable)Session["user"];
                }

                //Get the First Name & Last Name
                strFromFullName = dtUserInfo.Rows[0]["firstName"].ToString().Trim() + " " + dtUserInfo.Rows[0]["lastName"].ToString().Trim();

                //Encrypt the Sender Emailhere
                strSenderEmailEncrypt = EncryptUserName(dtUserInfo.Rows[0]["userName"].ToString().Trim());                

                //Encrypt the To Email here
                strToEmailEncrypt = EncryptUserName(strToEmail);
            }

            //Send the with User Encrypted Info & Deal Code
            if (SendMailWithDealCode(strFromFullName, strToEmailEncrypt, strSenderEmailEncrypt, strToEmail, strEmailMessage, strDealCode, txtEFirstName.Text.Trim() + " " + txtELastName.Text.Trim(), strFilePath))
            {
                if (UpdateOrderDeatilInfoById(int.Parse(strDetailOrderId), strToEmail))
                {
                    string jScript;
                    jScript = "<script>";
                    jScript += "MessegeArea('Email has been sent successfully.' , 'success');";
                    jScript += "</script>";
                    ScriptManager.RegisterClientScriptBlock(this, typeof(Button), "Javascript", jScript, false);                                                                              
                  
                }
                else
                {
                    string jScript;
                    jScript = "<script>";
                    jScript += "MessegeArea('Email has not been sent successfully.' , 'error');";
                    jScript += "</script>";
                    ScriptManager.RegisterClientScriptBlock(this, typeof(Button), "Javascript", jScript, false);                                                     
                }
            }
            else
            {
                string jScript;
                jScript = "<script>";
                jScript += "MessegeArea('Email has not been sent successfully.' , 'error');";
                jScript += "</script>";
                ScriptManager.RegisterClientScriptBlock(this, typeof(Button), "Javascript", jScript, false);                                                     
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

    private bool UpdateOrderDeatilInfoById(int iOrderDetailId, string strEmail)
    {
        bool bChk = false;

        try
        {
            BLLDealOrderDetail objBLLDealOrderDetail = new BLLDealOrderDetail();

            objBLLDealOrderDetail.detailID = iOrderDetailId;
            objBLLDealOrderDetail.receiverEmail = strEmail;

            int iChk = objBLLDealOrderDetail.updateDealOrderDetailEmailByID();

            if (iChk != 0)
                bChk = true;
        }
        catch (Exception ex)
        { }
        
        return bChk;
    }

    private void RegisterStartupScript(string key, string script)
    {
        ScriptManager.RegisterStartupScript(phrJsRunner, phrJsRunner.GetType(), key, script, true);
    }

    protected string EncryptDealCode(string strDealCode)
    {
        if (strDealCode.Trim() != "")
        {
            GECEncryption objEnc = new GECEncryption();
            return objEnc.EncryptData("deatailOrder", strDealCode);
        }
        return "";
    }

    protected string EncryptUserName(string strUserName)
    {
        if (strUserName.Trim() != "")
        {
            GECEncryption objEnc = new GECEncryption();
            return objEnc.EncryptData("userName", strUserName);
        }
        return "";
    }

    #region Send Email for Forgot Password

    private bool SendMailWithDealCode(string strSenderFullName,
                                      string strToEmailEncrypt,
                                      string strSenderEmailEncrypt,
                                      string strToEmail,
                                      string strEmailMessage,
                                      string strDealCode,
                                      string strToFullName,
                                      string strFilePath)
    {
        MailMessage message = new MailMessage();

        StringBuilder sb = new StringBuilder();

        try
        {
            string toAddress = strToEmail;
            string fromAddress = ConfigurationManager.AppSettings["AdminEmail"].ToString().Trim();
            string Subject = ConfigurationManager.AppSettings["EmailGiftDealForFriend"].ToString().Trim();
            message.IsBodyHtml = true;
            //mailBody.Append("<!DOCTYPE html PUBLIC '-//W3C//DTD XHTML 1.0 Transitional//EN' 'http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd'><html xmlns='http://www.w3.org/1999/xhtml'>");
            //mailBody.Append("<head><title></title></head><body style='font-family:Century;'><h4>Dear " + strToFullName + "</h4></br><h5>You have received a gift from " + strSenderFullName + "!");
            //mailBody.Append("</h5>");
            //mailBody.Append("<p><font size='3'>Message from " + strSenderFullName+": " + strEmailMessage + "</font></p>");
            //mailBody.Append("<font size='3'>To retrieve your gift, click on the link below:</font>");
            //mailBody.Append("<font size='3'>");
            //mailBody.Append("<br/><a href='" + ConfigurationManager.AppSettings["YourSite"].ToString().Trim() + "/confirmDeal.aspx?u=" + strToEmailEncrypt + "&dc=" + strDealCode + "&sid=" + strSenderEmailEncrypt + "&uName=" + strToFullName + "'>" + ConfigurationManager.AppSettings["YourSite"].ToString().Trim() + "/confirmDeal.aspx</a></font>");
            //mailBody.Append("<br /><p style='font-size:12px;'>*If you have any questions, or feel you are not recipient of this email, please contact <a href='mailto:support@tazzling.com'>support@tazzling.com</a></p>");
            //mailBody.Append("<p>" + ConfigurationManager.AppSettings["EmailSignature"].ToString().Trim() + "</p>");
            //mailBody.Append("<p style='font-size:12px; color:Gray;'>This is an automated Message from www.Tazzling.Com</p></body></html>");


            sb.Append("<!DOCTYPE html PUBLIC '-//W3C//DTD HTML 4.01 Transitional//EN'><html><head><meta http-equiv='Content-Type' content='text/html; charset=utf-8'><meta name='viewport' content='width = 800'><title>Order Confirmation!</title><style type='text/css'>a.aapl-link{text-decoration: none;}a.aapl-link:hover{text-decoration: underline;}</style><style media='only screen and (max-device-width: 680px)' type='text/css'>*{line-height: normal !important;}</style></head>");
            sb.Append("<body bgcolor='#E4E4E4' style='margin: 0; padding: 0'><table width='100%' bgcolor='#E4E4E4' cellpadding='0' cellspacing='0' align='center'><tr><td><table width='800' align='center' border='0' cellspacing='0' cellpadding='0'><tr><td><div style='margin: 10px 0px 12px 0px; font-family: Arial;color: #333333; font-size: 14px; line-height: 1.3em;'><img src='http://tazzling.com/images/logoForMail.png' alt='TastyGo' border='0'></div></td></tr></table>");
            sb.Append("<table width='800' align='center' border='0' cellspacing='0' cellpadding='0'><tr><td style='-webkit-border-radius: 8px; background-color: #ffffff' bgcolor='#ffffff'><table width='720' align='center' border='0' cellspacing='0' cellpadding='0'><tr valign='top'><td width='720' bgcolor='#FFFFFF' align='left'>");
            sb.Append("<div style='margin: 40px 0px 0px 15px; font-family: Arial;color: #333333; font-size: 14px; line-height: 1.3em;'>");
            sb.Append("<strong>Dear " + strToFullName + ",</strong></div>");
            sb.Append("<div style='margin: 20px 0px 20px 15px; font-family: Arial;color: #000000; font-size: 18px; line-height: 1.3em;'><strong>You have received a gift from " + strSenderFullName + "!</strong></div>");
            sb.Append("<div style='margin: 0px 0px 10px 15px; font-family: Arial;color: #333333; font-size: 14px; line-height: 1.4em;'>Message from " + strSenderFullName + ": " + strEmailMessage + "</div>");
            sb.Append("<div style='margin: 0px 0px 10px 15px; font-family: Arial;color: #333333; font-size: 14px; line-height: 1.4em;'>To retrieve your gift, please find attachment:</div>");
            //sb.Append("<div style='margin: 0px 0px 10px 15px; font-family: Arial;color: #333333; font-size: 14px; line-height: 1.4em;'><a href='" + ConfigurationManager.AppSettings["YourSite"].ToString().Trim() + "/confirmDeal.aspx?u=" + strToEmailEncrypt + "&dc=" + strDealCode + "&sid=" + strSenderEmailEncrypt + "&uName=" + strToFullName + "'>" + ConfigurationManager.AppSettings["YourSite"].ToString().Trim() + "/confirmDeal.aspx</a>");
            sb.Append("<div style='margin: 0px 0px 10px 15px; font-family: Arial;color: #333333; font-size: 14px; line-height: 1.4em;'>*If you have any questions, or feel you are not recipient of this email, please contact <a href='mailto:support@tazzling.com'>support@tazzling.com</a></div>");
                        

            sb.Append("<div style='margin: 0px 0px 10px 15px; font-family: Arial;border-top: 1px solid #eeeeee; font-size: 12px; line-height: 1.3em;'>&nbsp;</div>");
            sb.Append("<div style='margin: 0px 10px 20px 15px; font-family: Arial;color: #333333; font-size: 14px; line-height: 1.3em; clear: both;'><strong>Best regards,</strong><br>");
            sb.Append(ConfigurationManager.AppSettings["EmailSignature"].ToString().Trim() + "</div>");
            sb.Append("</td></tr></table></td></tr></table><table width='560' border='0' cellspacing='0' cellpadding='0' align='center'><tr><td style='padding: 20px 20px 10px 24px;'><div style='font-family: Geneva, Verdana, Arial, Helvetica, sans-serif; font-size: 9px;line-height: 12px; color: #858585;'></div></td></tr>");
            sb.Append("<tr><td style='padding: 0 20px 10px 24px;'>    <div style='font-family: Geneva, Verdana, Arial, Helvetica, sans-serif; font-size: 9px;        line-height: 12px; color: #858585;'>        Copyright &copy; 2011 Tazzling.Com. All Rights Reserved</div>    <div style='font-family: Geneva, Verdana, Arial, Helvetica, sans-serif; font-size: 9px;        line-height: 12px; color: #858585;'>        <a href='http://www.tazzling.com/' style='font-family: Geneva, Verdana, Arial, Helvetica, sans-serif;");
            sb.Append("font-size: 9px; line-height: 12px; color: #858585; text-decoration: underline;'>Keep Informed</a> / <a href='http://www.tazzling.com/terms-customer.aspx' style='font-family: Geneva, Verdana, Arial, Helvetica, sans-serif;    font-size: 9px; line-height: 12px; color: #858585; text-decoration: underline;'>    Privacy Policy</a> / <a href='http://www.tazzling.com/contact-us.aspx' style='font-family: Geneva, Verdana, Arial, Helvetica, sans-serif;  font-size: 9px; line-height: 12px; color: #858585; text-decoration: underline;'>Contact Us</a></div>");
            sb.Append("</td></tr><tr></tr></table></td></tr></table></body></html>");




            message.Body = sb.ToString();
            return Misc.SendEmailWithAttachment(toAddress, "", "", fromAddress, Subject, message.Body, strFilePath);
        }
        catch (Exception ex)
        {
            string jScript;
            jScript = "<script>";
            jScript += "MessegeArea('There is an error occur, please email us at support@tazzling.com or call 1855-295-1771.' , 'error');";
            jScript += "</script>";
            ScriptManager.RegisterClientScriptBlock(this, typeof(Button), "Javascript", jScript, false);                                                     

            return false;
        }
    }

    #endregion
}