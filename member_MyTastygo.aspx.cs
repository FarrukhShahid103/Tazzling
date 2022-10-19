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

public partial class member_MyTastygo : System.Web.UI.Page
{

    BLLDealOrders objOrders = new BLLDealOrders();
    BLLDealOrderDetail objDetail = new BLLDealOrderDetail();
    public string UserEmail = "";
    public string UserID = "";

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
                Page.Title = ConfigurationManager.AppSettings["pageTitleStart"].ToString().Trim() + " | Member | My TastyGos";
                if (Session["member"] != null || Session["restaurant"] != null || Session["sale"] != null || Session["user"] != null)
                {
                    bindAllGrids();
                }
                else
                {
                    Response.Redirect("Default.aspx", false);
                }
            }

            PageTabScript();
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
        bindGridCancelled();
        bindGridExpired();
    }

    protected string GetDateString(object objDate)
    {
        if (objDate.ToString() != "")
        {
            DateTime dt = Convert.ToDateTime(objDate);
            return dt.ToString("MM-dd-yyyy");
        }
        return "not available";
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

    protected bool getDealOrderStatus(object status)
    {
        if (status.ToString() != "")
        {
            if (status.ToString().ToLower().Trim() == "pending" || status.ToString().ToLower().Trim() == "cancelled" || status.ToString().ToLower().Trim() == "declined" || status.ToString().ToLower().Trim() == "refunded")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        return false;
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
                    UserEmail = dtUser.Rows[0]["userName"].ToString();
                    UserID = dtUser.Rows[0]["userid"].ToString();
                    DataTable dtOrders;
                    DataView dv;
                    gridview1.PageSize = Misc.clientPageSize;
                    dtOrders = objOrders.getAllOwnAvailableDealOrderDetailByUserID();
                    dv = new DataView(dtOrders);
                    ViewState["page"] = Math.Ceiling(Convert.ToDouble(dtOrders.Rows.Count) / Convert.ToDouble(gridview1.PageSize)).ToString();
                    if (dtOrders != null && dtOrders.Rows.Count > 0)
                    {
                        pnlAvailableVoucherEmpty.Visible = false;
                        pnlAvailableVoucherEmpty2.Visible = false;
                        AvailableVouchers.Visible = true;
                        gridview1.DataSource = dv;
                        gridview1.DataBind();
                    }
                    else
                    {
                        pnlAvailableVoucherEmpty.Visible = true;
                        pnlAvailableVoucherEmpty2.Visible = true;
                        AvailableVouchers.Visible = false;
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
    #endregion

    public bool displayPrevious = false;
    public bool displayNext = true;
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
                    gvSubItem.DataSource = objDetail.getAllAvailableUserDealOrderDetailByOrderID().DefaultView;
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


    protected void gridview1_Login(object sender, GridViewCommandEventArgs e)
    {

        //int value = Convert.ToInt32(e.CommandArgument);
        if (e.CommandArgument.ToString().ToLower().Trim() == "next" || e.CommandArgument.ToString().ToLower().Trim() == "prev" || e.CommandName.ToString().ToLower().Trim() == "edit")
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
                jScript += "MessegeArea('Status has been changed successfully.' , 'success');";
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
            mailBody.Append("<p><font size='3'>Status for order number \"" + dtOrderDetail.Rows[0]["orderNo"] + "\" has been changed to " + strStatus + " by user \"" + ViewState["userName"].ToString() + "\".<br>");
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
        // mark as used
        bool result = false;
        try
        {
            if (e.CommandName == "Login")
            {
                DataTable dtuser = null;
                if (dtuser != null && dtuser.Rows.Count > 0)
                {
                    objOrders.createdBy = Convert.ToInt64(dtuser.Rows[0]["userId"].ToString());
                    Int64 UserID = objOrders.createdBy;
                }
                string strIds = e.CommandArgument.ToString();
                objDetail.detailID = Convert.ToInt64(strIds);
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
                    jScript += "MessegeArea('Status has been changed successfully' , 'success');";
                    jScript += "TastyCommentCall('" + e.CommandArgument.ToString().Trim() + "');";
                    jScript += "</script>";
                    ScriptManager.RegisterClientScriptBlock(this, typeof(Button), "Javascript", jScript, false);
                }
                else
                {

                    bindAllGrids();
                    string jScript;
                    jScript = "<script>";

                    jScript += "MessegeArea('Status has not been changed successfully' , 'error');";

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
                   // string url = "http://www.tazzling.com/tastypdfdownload.aspx?did=894&pdf=true";
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
            jScript += "MessegeArea('There is an error occur, please email us at support@tazzling.com or call 1855-295-1771.' , 'error');";
            jScript += "</script>";
            ScriptManager.RegisterClientScriptBlock(this, typeof(Button), "Javascript", jScript, false);
        }

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
                jScript += "MessegeArea('Note has been sabed successfully' , 'success');";
                jScript += "</script>";
                ScriptManager.RegisterClientScriptBlock(this, typeof(Button), "Javascript", jScript, false);            
            }
        }
        catch (Exception ex)
        {

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
    #region Function to Bind Grid
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
                    dtOrders = objOrders.getAllOwnUsedDealOrderDetailByUserID();
                    dv = new DataView(dtOrders);
                    ViewState["page"] = Math.Ceiling(Convert.ToDouble(dtOrders.Rows.Count) / Convert.ToDouble(gvUsed.PageSize)).ToString();
                    if (dtOrders != null && dtOrders.Rows.Count > 0)
                    {
                        gvUsed.Visible = true;
                        pnlgvUsed.Visible = true;
                        gvUsedEmpty.Visible = false;
                        gvUsed.DataSource = dv;
                        gvUsed.DataBind();
                    }
                    else
                    {
                        gvUsed.Visible = false;
                        pnlgvUsed.Visible = false;
                        gvUsedEmpty.Visible = true;
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
    #endregion
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
                    gvSubUsed.DataSource = objDetail.getAllUsedUserDealOrderDetailByOrderID().DefaultView;
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
    protected void gvUsed_Login(object sender, GridViewCommandEventArgs e)
    {

        //int value = Convert.ToInt32(e.CommandArgument);
        if (e.CommandArgument.ToString().ToLower().Trim() == "next" || e.CommandArgument.ToString().ToLower().Trim() == "prev" || e.CommandName.ToString().ToLower().Trim() == "edit")
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
                jScript += "MessegeArea('Status has not been changed successfully.' , 'success');";
                jScript += "</script>";
                ScriptManager.RegisterClientScriptBlock(this, typeof(Button), "Javascript", jScript, false);                
            }
        }
        catch (Exception ex)
        {
            string jScript;
            jScript = "<script>";
            jScript += "MessegeArea('"+ex.Message+"' , 'success');";
            jScript += "</script>";
            ScriptManager.RegisterClientScriptBlock(this, typeof(Button), "Javascript", jScript, false);         
        }

    }

    protected void gvSubUsed_Login(object sender, GridViewCommandEventArgs e)
    {
        // undo
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
                result = objDetail.changeOrderDetailStatusanddeleteComments();
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

    #region Function to Bind Grid
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
                    dtOrders = objOrders.getAllOwnCancelledDealOrderDetailByUserID();
                    dv = new DataView(dtOrders);
                    ViewState["page"] = Math.Ceiling(Convert.ToDouble(dtOrders.Rows.Count) / Convert.ToDouble(gvcancelled.PageSize)).ToString();
                    if (dtOrders != null && dtOrders.Rows.Count > 0)
                    {
                        pnlgvcancelled.Visible = true;
                        pnlgvCancelledEmpty.Visible = false;

                        gvcancelled.DataSource = dv;
                        gvcancelled.DataBind();
                    }
                    else
                    {
                        pnlgvcancelled.Visible = false;
                        pnlgvCancelledEmpty.Visible = true;

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
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "Paging", "<script>$('#Itemtab3').click();</script>", false);
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

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                GridView gvSubcancelled = (GridView)e.Row.FindControl("gvSubcancelled");

                Label lblID = (Label)e.Row.FindControl("lblcancelledId");
                if (lblID != null && lblID.Text.ToString() != "")
                {
                    objDetail.dOrderID = Convert.ToInt64(lblID.Text.ToString());
                    gvSubcancelled.DataSource = objDetail.getAllUserDealOrderDetailByOrderID().DefaultView;
                    gvSubcancelled.DataBind();
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
    protected void gvcancelled_Login(object sender, GridViewCommandEventArgs e)
    {

        //int value = Convert.ToInt32(e.CommandArgument);
        if (e.CommandArgument.ToString().ToLower().Trim() == "next" || e.CommandArgument.ToString().ToLower().Trim() == "prev" || e.CommandName.ToString().ToLower().Trim() == "edit")
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
            jScript += "MessegeArea('There is an error occur, please email us at support@tazzling.com or call 1855-295-1771.' , 'error');";
            jScript += "</script>";
            ScriptManager.RegisterClientScriptBlock(this, typeof(Button), "Javascript", jScript, false);            
        }

    }

    protected void gvSubcancelled_Login(object sender, GridViewCommandEventArgs e)
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
            this.bindAllGrids();
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
                    gvSubExpired.DataSource = objDetail.getAllUserDealOrderDetailByOrderID().DefaultView;
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
        if (e.CommandArgument.ToString().ToLower().Trim() == "next" || e.CommandArgument.ToString().ToLower().Trim() == "prev" || e.CommandName.ToString().ToLower().Trim() == "edit")
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
            jScript += "MessegeArea('There is an error occur, please email us at support@tazzling.com or call 1855-295-1771.' , 'error');";

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
             jScript += "MessegeArea('There is an error occur, please email us at support@tazzling.com or call 1855-295-1771.' , 'error');";

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
                    dtOrders = objOrders.getAllOwnExpiredDealOrderDetailByUserID();
                    dv = new DataView(dtOrders);
                    ViewState["page"] = Math.Ceiling(Convert.ToDouble(dtOrders.Rows.Count) / Convert.ToDouble(gvExpired.PageSize)).ToString();
                    if (dtOrders != null && dtOrders.Rows.Count > 0)
                    {
                        pnlgvExpired.Visible = true;
                        gvExpiredEmpty.Visible = false;

                        gvExpired.DataSource = dv;
                        gvExpired.DataBind();
                    }
                    else
                    {
                        pnlgvExpired.Visible = false;
                        gvExpiredEmpty.Visible = true;
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

    protected void lnkBtnLogOut_Click(object sender, EventArgs e)
    {
        try
        {

            //btnSignIn.Visible = true;            


            Session.RemoveAll();

            //Remove the Cookie here
            RemoveUserInfoCookie();
            Response.Redirect(ConfigurationManager.AppSettings["YourSite"] + "/Login.aspx", false);
            Response.End();


        }
        catch (Exception ex)
        {

        }
    }

    #region "Remove the User Info Cookie"

    private void RemoveUserInfoCookie()
    {
        try
        {
            HttpCookie cookie = Request.Cookies["tastygo_ui"];
            //Remove the Cookie
            if (cookie != null)
            {
                //Response.Cookies.Remove("tastygo_ui");
                Response.Cookies["tastygo_ui"].Expires = DateTime.Now;
            }
            HttpCookie FB_cookie = Request.Cookies["fbsr_" + ConfigurationManager.AppSettings["Application_ID"].ToString()];
            if (FB_cookie != null)
            {
                Response.Cookies["fbsr_" + ConfigurationManager.AppSettings["Application_ID"].ToString()].Expires = DateTime.Now;
            }
            HttpCookie cookie2 = Request.Cookies["tastygoLogin"];
            //Remove the Cookie
            if (cookie2 != null)
            {
                //Response.Cookies.Remove("tastygo_ui");
                Response.Cookies["tastygoLogin"].Expires = DateTime.Now;
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


}
