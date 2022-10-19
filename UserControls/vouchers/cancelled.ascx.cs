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

public partial class UserControls_vouchers_cancelled : System.Web.UI.UserControl
{
    BLLDealOrders objOrders = new BLLDealOrders();
    BLLDealOrderDetail objDetail = new BLLDealOrderDetail();

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {            
            if (!IsPostBack)
            {

                if (Session["member"] != null || Session["restaurant"] != null || Session["sale"] != null)
                {
                    bindGridCancelled();
                }
                else
                {
                    Response.Redirect("Default.aspx", false);
                }
            }
        }
        catch (Exception ex)
        {
            lblMessage.Visible = true;
            lblMessage.Text = "There is an error occur, please email us at support@tazzling.com or call 1855-295-1771.";
            imgGridMessage.Visible = true;
            imgGridMessage.ImageUrl = "~/images/error.png";
            lblMessage.ForeColor = System.Drawing.Color.Red;
        }
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

    protected string getDealCode(object objCode, object status)
    {
        if (objCode.ToString() != "")
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
        return "";
    }

    protected bool getDetailStatus(object status)
    {
        if (status.ToString() != "")
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
                    if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + "Images\\dealfood\\" + restaurantId.ToString().Trim() + "\\" + strDealImages[i]))
                    {
                        imageFound = true;
                        break;
                    }
                }
            }
            if (imageFound)
            {
                return "Images/dealfood/" + restaurantId.ToString().Trim() + "/" + strDealImages[i];
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
    protected void bindGridCancelled()
    {
        try
        {
            if (Session["member"] != null || Session["restaurant"] != null || Session["sale"] != null)
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
            lblMessage.Visible = true;
            lblMessage.Text = "There is an error occur, please email us at support@tazzling.com or call 1855-295-1771.";
            imgGridMessage.Visible = true;
            imgGridMessage.ImageUrl = "~/images/error.png";
            lblMessage.ForeColor = System.Drawing.Color.Red;
        }
    }
    #endregion

    public bool displayPrevious = false;
    public bool displayNext = true;
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
            this.bindGridCancelled();
        }
        catch (Exception ex)
        {
            lblMessage.Visible = true;
            lblMessage.Text = "There is an error occur, please email us at support@tazzling.com or call 1855-295-1771.";
            imgGridMessage.Visible = true;
            imgGridMessage.ImageUrl = "~/images/error.png";
            lblMessage.ForeColor = System.Drawing.Color.Red;
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
            lblMessage.Visible = true;
            lblMessage.Text = "There is an error occur, please email us at support@tazzling.com or call 1855-295-1771.";
            imgGridMessage.Visible = true;
            imgGridMessage.ImageUrl = "~/images/error.png";
            lblMessage.ForeColor = System.Drawing.Color.Red;
        }
    }
    protected void gvcancelled_Login(object sender, GridViewCommandEventArgs e)
    {

        //int value = Convert.ToInt32(e.CommandArgument);
        if (e.CommandArgument.ToString().ToLower().Trim() == "next" || e.CommandArgument.ToString().ToLower().Trim() == "prev")
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

                bindGridCancelled();
                lblMessage.Text = "Status has been changed successfully.";
                lblMessage.Visible = true;
                imgGridMessage.Visible = true;
                imgGridMessage.ImageUrl = "~/images/Checked.png";
                lblMessage.ForeColor = System.Drawing.Color.Black;
                DataTable dtOrderDetail = objOrders.getDealOrderDetailByOrderID();
                if (dtOrderDetail != null && dtOrderDetail.Rows.Count > 0)
                {
                    SendMailToAdminForDealStatus(dtOrderDetail, objOrders.status);
                }

            }
            else
            {
                bindGridCancelled();
                lblMessage.Text = "Status has not been changed successfully.";
                lblMessage.Visible = true;
                imgGridMessage.Visible = true;
                imgGridMessage.ImageUrl = "~/images/error.png";
                lblMessage.ForeColor = System.Drawing.Color.Red;
            }
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.ToString();
            lblMessage.Visible = true;
            imgGridMessage.Visible = true;
            imgGridMessage.ImageUrl = "~/images/error.png";
            lblMessage.ForeColor = System.Drawing.Color.Red;
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

            //if (Session["member"] != null || Session["restaurant"] != null || Session["sale"] != null)
            //{

            //    DataTable dtUser = null;
            //    if (Session["member"] != null)
            //    {
            //        dtUser = (DataTable)Session["member"];
            //    }
            //    else if (Session["restaurant"] != null)
            //    {
            //        dtUser = (DataTable)Session["restaurant"];
            //    }
            //    else if (Session["sale"] != null)
            //    {
            //        dtUser = (DataTable)Session["sale"];
            //    }
            //    if (dtUser != null && dtUser.Rows.Count > 0)
            //    {
            //        objOrders.createdBy = Convert.ToInt64(dtUser.Rows[0]["userId"].ToString());
            //        message = new MailMessage();
            //        mailBody = new StringBuilder();
            //        string toAddress = "support@tazzling.com";
            //        string fromAddress = ConfigurationManager.AppSettings["AdminEmail"].ToString().Trim();
            //        string Subject = ConfigurationManager.AppSettings["EmailSubjectChageOrderStatus"].ToString().Trim() + " " + strStatus;
            //        message.IsBodyHtml = true;
            //        mailBody.Append("<!DOCTYPE html PUBLIC '-//W3C//DTD XHTML 1.0 Transitional//EN' 'http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd'>");
            //        mailBody.Append("<html xmlns='http://www.w3.org/1999/xhtml'><head><title></title></head><body style='font-family: Century;'>");
            //        mailBody.Append("<h4>Deal Admin.");
            //        mailBody.Append("</h4>");
            //        mailBody.Append("<p><font size='3'>Status for order number \"" + dtOrderDetail.Rows[0]["orderNo"] + "\", " + ViewState["userName"].ToString() + ", has been changed to " + strStatus + " by user</font></p>");

            //        mailBody.Append("<p>" + ConfigurationManager.AppSettings["EmailSignature"].ToString().Trim() + "</p></body></html>");
            //        message.Body = mailBody.ToString();

            //        return Misc.SendEmail(toAddress, "", "", fromAddress, Subject, message.Body);
            //    }
            //}



        }
        catch (Exception ex)
        {
            //lblErrorMessage.Text = "There is an error occur, please email us at support@tazzling.com or call 1855-295-1771.";
            //lblErrorMessage.Visible = true;
            imgGridMessage.Visible = true;
            imgGridMessage.ImageUrl = "~/images/error.png";
            //lblErrorMessage.ForeColor = System.Drawing.Color.Red;
            return false;
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


                    bindGridCancelled();
                    lblMessage.Text = "Status has been changed successfully.";
                    lblMessage.Visible = true;
                    imgGridMessage.Visible = true;
                    imgGridMessage.ImageUrl = "~/images/Checked.png";
                    lblMessage.ForeColor = System.Drawing.Color.Black;

                }
                else
                {

                    bindGridCancelled();
                    lblMessage.Text = "Status has not been changed successfully.";
                    lblMessage.Visible = true;
                    imgGridMessage.Visible = true;
                    imgGridMessage.ImageUrl = "~/images/error.png";
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                }
            }
            else if (e.CommandName == "download")
            {
                string strOrderNumber = "";
                string[] strOrderIDs = e.CommandArgument.ToString().Split(',');
                string strOrderID = strOrderIDs[1].ToString();
                string strDetailOrderId = strOrderIDs[0].ToString();
                strOrderNumber = Misc.createPDFForGift(strDetailOrderId, strOrderID, " ");

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




                //GECEncryption objEnc = new GECEncryption();
                //string strFilePath = HttpContext.Current.Server.MapPath("Images\\ClientData\\" + objEnc.DecryptData("deatailOrder", e.CommandArgument.ToString()) + ".pdf");


                //try
                //{
                //    string contentType = "";
                //    //Get the physical path to the file.
                //    string FilePath = AppDomain.CurrentDomain.BaseDirectory + "Images\\ClientData\\" + objEnc.DecryptData("deatailOrder", e.CommandArgument.ToString()) + ".pdf";

                //    //Set the appropriate ContentType.
                //    contentType = "Application/pdf";

                //    //Set the appropriate ContentType.

                //    Response.ContentType = contentType;
                //    Response.AppendHeader("content-disposition", "attachment; filename=" + (new FileInfo(objEnc.DecryptData("deatailOrder", e.CommandArgument.ToString()) + ".pdf")).Name);

                //    //Write the file directly to the HTTP content output stream.
                //    Response.WriteFile(FilePath);
                //    Response.End();
                //}
                //catch
                //{
                //    //To Do
                //}

            }
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.ToString();
            lblMessage.Visible = true;
            imgGridMessage.Visible = true;
            imgGridMessage.ImageUrl = "~/images/error.png";
            lblMessage.ForeColor = System.Drawing.Color.Red;
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

                this.bindGridCancelled();
            }
        }
        catch (Exception ex)
        {
            lblMessage.Visible = true;
            lblMessage.Text = "There is an error occur, please email us at support@tazzling.com or call 1855-295-1771.";
            imgGridMessage.Visible = true;
            imgGridMessage.ImageUrl = "~/images/error.png";
            lblMessage.ForeColor = System.Drawing.Color.Red;
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
}
