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
using GecLibrary;
using iTextSharp.text;
using iTextSharp.text.pdf;
public partial class dealInvoiceManagment : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        lblMessage.Visible = false;
        imgError.Visible = false;
        if (!IsPostBack)
        {
            if (Request.QueryString["did"] != null && Request.QueryString["did"].ToString().Trim() != "")
            {
                RenderDealInvoice();
                //downloadDoc();
            }
        }
        if (ViewState["userID"] == null) { GetAndSetUserID(); }
    }

    protected void downloadDoc()
    {
        try
        {
            string attachment = "attachment; filename=NewsLetter.doc";
            Response.ClearContent();
            Response.AddHeader("content-disposition", attachment);
            Response.ContentType = "text/DOC";
            StringWriter stw = new StringWriter();
            HtmlTextWriter htextw = new HtmlTextWriter(stw);
            divTop.RenderControl(htextw);
            Response.Write(stw.ToString());
            Response.End();
        }
        catch (Exception ex)
        { }
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

    protected void RenderDealInvoice()
    {
        try
        {
            ltMain.Text = "";
            string strQuery = "SELECT  [deals].[dealId],dealpayment, [deals].[restaurantId],OurCommission, [deals].[title], [deals].[sellingPrice],";
            strQuery += " isnull((SELECT sum(qty) FROM  [dealOrders] where [dealOrders].dealId = [deals].dealId),0) 'Orders', dealStartTimeC as 'dealStartTime',";
            // strQuery += " isnull((SELECT sum(qty) FROM  [dealOrders] where [dealOrders].dealId = [deals].dealId and status<>'Successful' ),0) 'RefundedOrders',";
            strQuery += " dealEndTimeC as 'dealEndTime', [restaurant].[email], [restaurant].[phone], [restaurant].[restaurantBusinessName] FROM [deals]";
            strQuery += " INNER join restaurant On restaurant.[restaurantId]= deals.[restaurantId]";
            strQuery += " INNER join dealcity On dealcity.[dealid]= deals.[dealid] ";
            strQuery += " where restaurant.[restaurantId]= deals.[restaurantId] and [deals].dealid=" + Request.QueryString["did"].ToString().Trim();
            DataTable dtDealInfo = Misc.search(strQuery);
            if (dtDealInfo != null && dtDealInfo.Rows.Count > 0)
            {
                if (dtDealInfo.Rows[0]["dealpayment"] != null && dtDealInfo.Rows[0]["dealpayment"].ToString().Trim() != "")
                {
                    if (dtDealInfo.Rows[0]["dealpayment"].ToString().Trim() == "1")
                    {
                        rdFirstPayment.Checked = true;
                    }
                    else if (dtDealInfo.Rows[0]["dealpayment"].ToString().Trim() == "2")
                    {
                        rdSecondPayment.Checked = true;
                    }
                    else if (dtDealInfo.Rows[0]["dealpayment"].ToString().Trim() == "3")
                    {
                        rdThirdPayment.Checked = true;
                    }
                    else
                    {
                        rdFirstPayment.Checked = false;
                        rdSecondPayment.Checked = false;
                        rdThirdPayment.Checked = false;
                    }
                }
                ViewState["restaurantId"] = dtDealInfo.Rows[0]["restaurantId"].ToString().Trim();
                double dealUnitPrice = 0;
                double advertisingFee = 0;
                double ccTransactionFee = 0;
                double companyComission = 0;
                double taxAmount = 0;
                int totalOrders = 0;
                // int refundedOrders = 0;
                double refundedAmount = 0;
                double.TryParse(dtDealInfo.Rows[0]["sellingPrice"].ToString().Trim(), out dealUnitPrice);
                int.TryParse(dtDealInfo.Rows[0]["Orders"].ToString().Trim(), out totalOrders);
                double.TryParse(dtDealInfo.Rows[0]["OurCommission"].ToString().Trim(), out companyComission);
                ccTransactionFee = Math.Round((3.9 / 100) * (dealUnitPrice * totalOrders), 2, MidpointRounding.AwayFromZero);
                advertisingFee = Math.Round((companyComission / 100) * (dealUnitPrice * totalOrders), 2, MidpointRounding.AwayFromZero);
                // int.TryParse(dtDealInfo.Rows[0]["RefundedOrders"].ToString().Trim(), out refundedOrders);
                ltMain.Text += "<div class='height10'></div><div id='search'>Invoice</div><div class='height10'></div>";
                ltMain.Text += "<div class='indent30'><table cellpadding='3' cellspacing='7' width='100%' border='0' class='fontStyle'><tr><td align='left'><strong>Business Name:</strong></td><td align='left'><div style='width: 400px;'>";
                ltMain.Text += dtDealInfo.Rows[0]["restaurantBusinessName"].ToString().Trim();
                ltMain.Text += "</div></td></tr><tr> <td width='141px' align='left' class='colLeftOrderDetail'>";
                ltMain.Text += "<strong>Deal Title:</strong></td><td align='left'>";
                ltMain.Text += dtDealInfo.Rows[0]["title"].ToString().Trim();

                ltMain.Text += "</td></tr><tr><td align='left'><strong>Deal End Time:</strong></td><td align='left'>";
                ltMain.Text += dtDealInfo.Rows[0]["dealEndTime"].ToString().Trim();
                ltMain.Text += "</td></tr></table></div><div class='height10'></div>";
                ltMain.Text += "<div id='formHeading2'><h3>Invoice Detail</h3></div>";

                string strDate = "";

                DateTime dtDealEndTime = Convert.ToDateTime(dtDealInfo.Rows[0]["dealEndTime"].ToString().Trim());
                if (dtDealEndTime > DateTime.Now)
                {
                    strDate = DateTime.Now.ToString("MM/dd/yyyy");
                }
                else
                {
                    strDate = dtDealEndTime.ToString("MM/dd/yyyy HH:mm:ss");
                }


                double.TryParse(Convert.ToString(Math.Round((advertisingFee) * (12.00 / 100), 2, MidpointRounding.AwayFromZero)), out taxAmount);


                ltMain.Text += "<div class='indent30'><table cellpadding='3' cellspacing='7' width='100%' border='0' class='fontStyle'><tr><td style='float: left; width: 120px;'><strong>Date</strong></td><td style='float: left; width: 300px;'><strong>Description</strong></td><td style='float: left; width: 150px;'><strong>Detail</strong></td><td style='float: left; width: 100px;'><strong>Amount</strong></td><td style='float: left; width: 100px;'><strong>Action</strong></td></tr>";
                ltMain.Text += "<tr><td style='float: left; width: 120px;'>";
                ltMain.Text += strDate;
                ltMain.Text += "</td><td style='float: left; width: 300px;'>";
                ltMain.Text += dtDealInfo.Rows[0]["title"].ToString().Trim();
                ltMain.Text += "</td><td style='float: left; width: 150px;'>";
                ltMain.Text += "$" + dtDealInfo.Rows[0]["sellingPrice"].ToString().Trim() + " x " + dtDealInfo.Rows[0]["Orders"].ToString().Trim();
                ltMain.Text += "</td><td style='float: left; width: 100px;'><strong>";
                ltMain.Text += "$" + Convert.ToDouble(dealUnitPrice * totalOrders).ToString("###.00");
                ltMain.Text += "</td><td style='float: left; width: 100px;'>n/a</td></tr>";


                ltMain.Text += "<tr><td style='float: left; width: 120px;'>";
                ltMain.Text += strDate;
                ltMain.Text += "</td><td style='float: left; width: 300px;'>Advertising Fee</td><td style='float: left; width: 150px;'>";
                ltMain.Text += companyComission.ToString() + "% x " + Convert.ToDouble(dealUnitPrice * totalOrders).ToString("###.00");
                ltMain.Text += "</td><td style='float: left; width: 100px;'><strong>";
                ltMain.Text += "-$" + advertisingFee.ToString("###.00");
                ltMain.Text += "</strong></td><td style='float: left; width: 100px;'>n/a</td></tr>";

                ltMain.Text += "<tr><td style='float: left; width: 120px;'>";
                ltMain.Text += strDate;
                ltMain.Text += "</td><td style='float: left; width: 300px;'>Credit Card Transaction Fee</td><td style='float: left; width: 150px;'>";
                ltMain.Text += "3.9% x " + Convert.ToDouble(dealUnitPrice * totalOrders).ToString("###.00");
                ltMain.Text += "</td><td style='float: left; width: 100px;'><strong>";
                ltMain.Text += "-$" + ccTransactionFee.ToString("###.00");
                ltMain.Text += "</strong></td><td style='float: left; width: 100px;'>n/a</td></tr>";

                ltMain.Text += "<tr><td style='float: left; width: 120px;'>";
                ltMain.Text += strDate;
                ltMain.Text += "</td><td style='float: left; width: 300px;'>HST #849725056</td><td style='float: left; width: 150px;'>";
                ltMain.Text += "$" + advertisingFee.ToString("###.00") + " x 12%";
                ltMain.Text += "</td><td style='float: left; width: 100px;'><strong>";
                ltMain.Text += "-$" + taxAmount.ToString("###.00");
                ltMain.Text += "</strong></td><td style='float: left; width: 100px;'>n/a</td></tr><tr><td colspan='5' style='width: 100%; border-top: solid 2px gray;'>&nbsp;</td></tr>";

                double TopBalance = Math.Round((dealUnitPrice * totalOrders) - advertisingFee - taxAmount - ccTransactionFee, 2, MidpointRounding.AwayFromZero);

                ltMain.Text += "<tr><td style='float: left; width: 120px;'>";
                ltMain.Text += strDate;
                ltMain.Text += "</td><td style='float: left; width: 300px;'><strong>Balance</strong></td><td style='float: left; width: 150px;'></td><td style='float: left; width: 100px;'><strong>";
                ltMain.Text += "$" + TopBalance.ToString();
                ltMain.Text += "</strong></td><td style='float: left; width: 100px;'>n/a</td></tr>";


                GECEncryption objEnc = new GECEncryption();
                strQuery = "select dealOrderCode,createdDate, modifiedDate, qty from dealorders inner join dealOrderDetail on dealOrderDetail.dOrderID = dealOrders.dOrderID where status<>'Successful' and dealid=" + Request.QueryString["did"].ToString().Trim() + " order by modifiedDate desc";
                DataTable dtRefundedOrders = Misc.search(strQuery);
                if (dtRefundedOrders != null && dtRefundedOrders.Rows.Count > 0)
                {

                    for (int i = 0; i < dtRefundedOrders.Rows.Count; i++)
                    {
                        ltMain.Text += "<tr><td style='float: left; width: 120px;'>" + (dtRefundedOrders.Rows[i]["modifiedDate"].ToString().Trim() != "" ? dtRefundedOrders.Rows[i]["modifiedDate"].ToString().Trim() : dtRefundedOrders.Rows[i]["createdDate"].ToString().Trim()) + "</td>";
                        ltMain.Text += "<td style='float: left; width: 300px;'><strong>Refund # " + objEnc.DecryptData("deatailOrder", dtRefundedOrders.Rows[i]["dealOrderCode"].ToString()) + "</strong></td>";
                        ltMain.Text += "<td style='float: left; width: 150px;'>$" + dealUnitPrice.ToString() + " x " + dtRefundedOrders.Rows[i]["qty"].ToString().Trim() + "</td>";
                        double tempRefunded = Math.Round((dealUnitPrice * Convert.ToDouble(dtRefundedOrders.Rows[i]["qty"].ToString().Trim())), 2, MidpointRounding.AwayFromZero);
                        double tempAdveriseFee = Math.Round((companyComission / 100) * tempRefunded, 2, MidpointRounding.AwayFromZero);
                        double tempccTransactionFee = Math.Round((3.9 / 100) * tempRefunded, 2, MidpointRounding.AwayFromZero);
                        double tempTax = Math.Round((12.00 / 100) * tempAdveriseFee, 2, MidpointRounding.AwayFromZero);

                        ltMain.Text += "<td style='float: left; width: 100px;'><strong> -$" + tempRefunded.ToString() + "</strong></td>";
                        ltMain.Text += "<td style='float: left; width: 100px;'>n/a</td></tr>";
                        ltMain.Text += "<tr><td style='float: left; width: 120px;'>" + (dtRefundedOrders.Rows[i]["modifiedDate"].ToString().Trim() != "" ? dtRefundedOrders.Rows[i]["modifiedDate"].ToString().Trim() : dtRefundedOrders.Rows[i]["createdDate"].ToString().Trim()) + "</td>";
                        ltMain.Text += "<td style='float: left; width: 300px;'>Reverse Advertising Fee</td>";
                        ltMain.Text += "<td style='float: left; width: 150px;'>" + companyComission.ToString() + "% x $" + tempRefunded.ToString() + "</td>";
                        ltMain.Text += "<td style='float: left; width: 100px;'><strong> $" + tempAdveriseFee.ToString() + "</strong></td>";
                        ltMain.Text += "<td style='float: left; width: 100px;'>n/a</td></tr>";
                        ltMain.Text += "<tr><td style='float: left; width: 120px;'>" + (dtRefundedOrders.Rows[i]["modifiedDate"].ToString().Trim() != "" ? dtRefundedOrders.Rows[i]["modifiedDate"].ToString().Trim() : dtRefundedOrders.Rows[i]["createdDate"].ToString().Trim()) + "</td>";
                        ltMain.Text += "<td style='float: left; width: 300px;'>Credit Card Transaction Fee</td>";
                        ltMain.Text += "<td style='float: left; width: 150px;'>3.9% x $" + tempRefunded.ToString() + "</td>";
                        ltMain.Text += "<td style='float: left; width: 100px;'><strong> -$" + tempccTransactionFee.ToString() + "</strong></td>";
                        ltMain.Text += "<td style='float: left; width: 100px;'>n/a</td></tr>";
                        ltMain.Text += "<tr><td style='float: left; width: 120px;'>" + (dtRefundedOrders.Rows[i]["modifiedDate"].ToString().Trim() != "" ? dtRefundedOrders.Rows[i]["modifiedDate"].ToString().Trim() : dtRefundedOrders.Rows[i]["createdDate"].ToString().Trim()) + "</td>";
                        ltMain.Text += "<td style='float: left; width: 300px;'>Reverse HST #849725056</td>";
                        ltMain.Text += "<td style='float: left; width: 150px;'>12% x $" + tempAdveriseFee.ToString().Trim() + "</td>";
                        ltMain.Text += "<td style='float: left; width: 100px;'><strong> $" + tempTax.ToString() + "</strong></td>";
                        ltMain.Text += "<td style='float: left; width: 100px;'>n/a</td></tr>";
                        refundedAmount += Math.Round(((tempAdveriseFee + tempTax - tempccTransactionFee) - (tempRefunded)), 2, MidpointRounding.AwayFromZero);
                    }
                }
                double grandTotal = Math.Round(TopBalance + refundedAmount, 2, MidpointRounding.AwayFromZero);




                BLLPayOut objPayout = new BLLPayOut();
                objPayout.dealId = Convert.ToInt64(dtDealInfo.Rows[0]["dealId"].ToString().Trim());
                DataTable dtpayout = objPayout.getPayOutByDealID();
                if (dtpayout != null && dtpayout.Rows.Count > 0)
                {
                    ltMain.Text += "<tr><td colspan='5' style='width: 100%; border-top: solid 2px gray;'>&nbsp;</td></tr><tr><td style='float: left; width: 120px;'>";
                    ltMain.Text += strDate;
                    ltMain.Text += "</td><td style='float: left; width: 300px;'><strong>Balance</strong></td><td style='float: left; width: 150px;'></td><td style='float: left; width: 100px;'><strong>";
                    ltMain.Text += "$" + grandTotal.ToString();
                    ltMain.Text += "</td><td style='float: left; width: 100px;'>n/a</td></tr>";

                    for (int i = 0; i < dtpayout.Rows.Count; i++)
                    {
                        ltMain.Text += "<tr><td style='float: left; width: 120px;'>" + dtpayout.Rows[i]["poDate"].ToString().Trim() + "</td>";
                        ltMain.Text += "<td style='float: left; width: 300px;'><strong>TastyGo Payout #" + (i + 1).ToString() + "</strong></td>";
                        ltMain.Text += "<td style='float: left; width: 150px;'>" + dtpayout.Rows[i]["poDescription"].ToString().Trim() + "</td>";
                        double dTemPayout = Convert.ToDouble(dtpayout.Rows[i]["poAmount"].ToString().Trim());
                        if (dTemPayout < 0)
                        {
                            dTemPayout = Math.Round((dTemPayout) * (-1), 2, MidpointRounding.AwayFromZero);
                            ltMain.Text += "<td style='float: left; width: 100px;'><strong> -$" + dTemPayout.ToString() + "</strong></td>";
                        }
                        else
                        {
                            ltMain.Text += "<td style='float: left; width: 100px;'><strong> $" + dTemPayout.ToString() + "</strong></td>";
                        }
                        ltMain.Text += "<td style='float: left; width: 100px;'>n/a</td></tr>";
                    }
                    ltMain.Text += "<tr><td colspan='5' style='width: 100%; border-top: solid 2px gray;'>&nbsp;</td></tr>";
                    ltMain.Text += "<tr><td style='float: left; width: 120px;'></td><td style='float: left; width: 300px;'><strong>Payout Balance </strong></td>";
                    ltMain.Text += "<td style='float: left; width: 150px;'></td><td style='float: left; width: 100px;'><strong>";
                    object sumObject;
                    sumObject = dtpayout.Compute("Sum(poAmount)", "");
                    ltMain.Text += "$" + (grandTotal + (Math.Round(Convert.ToDouble(sumObject.ToString()), 2, MidpointRounding.AwayFromZero)));
                    ltMain.Text += "</strong></td><td style='float: left; width: 100px;'>n/a</td></tr>";
                }
                else
                {
                    ltMain.Text += "<tr><td colspan='5' style='width: 100%; border-top: solid 2px gray;'>&nbsp;</td></tr><tr><td style='float: left; width: 120px;'>";
                    ltMain.Text += strDate;
                    ltMain.Text += "</td><td style='float: left; width: 300px;'><strong>Payout Balance</strong></td><td style='float: left; width: 150px;'></td><td style='float: left; width: 100px;'><strong>";
                    ltMain.Text += "$" + grandTotal.ToString();
                    ltMain.Text += "</td><td style='float: left; width: 100px;'>n/a</td></tr>";
                }

                ltMain.Text += "</table></div>";
            }
        }
        catch (Exception ex)
        { }

    }

    protected void btnPayOut_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            if (Request.QueryString["did"] != null && Request.QueryString["did"].ToString().Trim() != "" && ViewState["userID"] != null)
            {
                BLLPayOut objPayout = new BLLPayOut();
                float amount = 0;
                float.TryParse(txtPayOut.Text.Trim(), out amount);
                objPayout.poAmount = (-1) * amount;
                objPayout.poDescription = txtDescription.Text.Trim();
                objPayout.poBy = Convert.ToInt64(ViewState["userID"].ToString());
                objPayout.dealId = Convert.ToInt64(Request.QueryString["did"].ToString());
                objPayout.AddNewPayOut();
                txtPayOut.Text = "";
                txtDescription.Text = "";
            }
            RenderDealInvoice();
        }
        catch (Exception ex)
        { }
    }

    protected void btnAdjustment_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            if (Request.QueryString["did"] != null && Request.QueryString["did"].ToString().Trim() != "" && ViewState["userID"] != null)
            {
                BLLPayOut objPayout = new BLLPayOut();
                float amount = 0;
                float.TryParse(txtAdjustmentAmount.Text.Trim(), out amount);
                objPayout.poAmount = amount;
                objPayout.poDescription = txtAdjustmentDescription.Text.Trim();
                objPayout.poBy = Convert.ToInt64(ViewState["userID"].ToString());
                objPayout.dealId = Convert.ToInt64(Request.QueryString["did"].ToString());
                objPayout.AddNewPayOut();
                txtAdjustmentDescription.Text = "";
                txtAdjustmentAmount.Text = "";
            }
            RenderDealInvoice();
        }
        catch (Exception ex)
        { }
    }

    protected void imgbtnClearPayment_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            lblMessage.Visible = false;
            imgError.Visible = false;
            rdFirstPayment.Checked = false;
            rdSecondPayment.Checked = false;
            rdThirdPayment.Checked = false;
        }
        catch (Exception ex)
        { }
    }

    protected void btnPaymentToBusiness_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            BLLDeals objDeal = new BLLDeals();
            objDeal.DealId = Convert.ToInt64(Request.QueryString["did"].ToString().Trim());
            if (rdFirstPayment.Checked)
            {
                objDeal.dealpayment = 1;
            }
            else if (rdSecondPayment.Checked)
            {
                objDeal.dealpayment = 2;
            }
            else if (rdThirdPayment.Checked)
            {
                objDeal.dealpayment = 3;
            }
            else
            {
                objDeal.dealpayment = 0;
            }
            objDeal.updateDealPaymentType();
            lblMessage.Visible = true;
            imgError.Visible = true;
            imgError.ImageUrl = "~/admin/images/Checked.png";
            lblMessage.Text = "Payment Status has been updated successfully";
        }
        catch (Exception ex)
        {

        }
    }

    protected bool createPDFForInvoice(long dealID)
    {
        try
        {
            GECEncryption objEnc = new GECEncryption();
            string strQuery = "SELECT  [deals].[dealId], [deals].[restaurantId],OurCommission, [deals].[title], [deals].[sellingPrice],";
            strQuery += " isnull((SELECT sum(qty) FROM  [dealOrders] where [dealOrders].dealId = [deals].dealId),0) 'Orders', dealStartTimeC as 'dealStartTime',";
            // strQuery += " isnull((SELECT sum(qty) FROM  [dealOrders] where [dealOrders].dealId = [deals].dealId and status<>'Successful' ),0) 'RefundedOrders',";
            strQuery += " dealEndTimeC as 'dealEndTime', [restaurant].[email], [restaurant].[phone], [restaurant].[restaurantAddress],[restaurant].[restaurantBusinessName],[restaurant].firstName, [restaurant].lastName FROM [deals]";
            strQuery += " INNER join restaurant On restaurant.[restaurantId]= deals.[restaurantId]";
            strQuery += " INNER join dealcity On dealcity.[dealid]= deals.[dealid] ";
            strQuery += " where restaurant.[restaurantId]= deals.[restaurantId] and [deals].dealid=" + dealID.ToString().Trim();
            DataTable dtDealInfo = Misc.search(strQuery);

            if ((dtDealInfo != null) && (dtDealInfo.Rows.Count > 0))
            {

                try
                {
                    Document doc = new Document(PageSize.A4, 10, 10, 20, 10);
                    PdfWriter.GetInstance(doc, new FileStream(AppDomain.CurrentDomain.BaseDirectory + "Admin\\Images\\Invoice\\" + dealID.ToString() + "_" + dtDealInfo.Rows[0]["restaurantId"] + ".pdf", FileMode.Create));
                    doc.Open();

                    PdfPTable table = new PdfPTable(4);
                    iTextSharp.text.Color color = new iTextSharp.text.Color(System.Drawing.ColorTranslator.FromHtml("#c8ccb3"));
                    iTextSharp.text.Font OrderNumberRight = new iTextSharp.text.Font(iTextSharp.text.Font.GetFamilyIndex("Book Antiqua"), 32, iTextSharp.text.Font.BOLD, color);
                    PdfPCell cell = new PdfPCell(new iTextSharp.text.Phrase("TASTYGO INVOICE", OrderNumberRight));
                    cell.BackgroundColor = new iTextSharp.text.Color(249, 249, 246);
                    cell.PaddingTop = 20;
                    cell.PaddingLeft = 5;
                    cell.PaddingBottom = 15;
                    cell.BorderWidthRight = 0f;
                    cell.BorderWidthBottom = 0f;
                    cell.BorderColorBottom = iTextSharp.text.Color.WHITE;
                    cell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
                    cell.Colspan = 2;
                    table.AddCell(cell);




                    string imageFilePath = AppDomain.CurrentDomain.BaseDirectory + "\\images\\biglogo.png";
                    iTextSharp.text.Image jpg = iTextSharp.text.Image.GetInstance(imageFilePath);
                    jpg.ScaleToFit(124f, 51f);
                    PdfPCell cell0 = new PdfPCell(jpg);
                    cell0.BackgroundColor = new iTextSharp.text.Color(249, 249, 246);
                    cell0.HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT;
                    cell0.BorderColorLeft = iTextSharp.text.Color.WHITE;
                    cell0.BorderColorBottom = iTextSharp.text.Color.WHITE;
                    cell0.Colspan = 2;
                    cell0.PaddingTop = 20;
                    cell0.PaddingRight = 5;
                    cell0.PaddingBottom = 15;
                    cell0.BorderWidthLeft = 0f;
                    cell.BorderWidthBottom = 0f;
                    table.AddCell(cell0);

                    OrderNumberRight = new iTextSharp.text.Font(iTextSharp.text.Font.GetFamilyIndex("Verdana"), 8, iTextSharp.text.Font.NORMAL, iTextSharp.text.Color.BLACK);
                    PdfPCell cell1 = new PdfPCell(new iTextSharp.text.Phrase("[Tastygo Online Inc.]", OrderNumberRight));
                    cell1.PaddingLeft = 5;
                    cell1.BackgroundColor = new iTextSharp.text.Color(249, 249, 246);
                    cell1.BorderWidthRight = 0f;
                    cell1.BorderWidthBottom = 0f;
                    cell1.BorderWidthTop = 0f;
                    cell1.BorderColorBottom = iTextSharp.text.Color.WHITE;
                    cell1.BorderColorTop = iTextSharp.text.Color.WHITE;
                    cell1.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
                    cell1.Colspan = 2;
                    table.AddCell(cell1);

                    PdfPCell cell01 = new PdfPCell(new iTextSharp.text.Phrase("Attention To:", OrderNumberRight));
                    cell01.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
                    cell01.BorderColorLeft = iTextSharp.text.Color.WHITE;
                    cell01.BorderColorBottom = iTextSharp.text.Color.WHITE;
                    cell01.BorderColorTop = iTextSharp.text.Color.WHITE;
                    cell01.BackgroundColor = new iTextSharp.text.Color(249, 249, 246);
                    cell01.Colspan = 2;
                    cell01.PaddingRight = 5;
                    cell01.BorderWidthTop = 0f;
                    cell01.BorderWidthLeft = 0f;
                    cell01.BorderWidthBottom = 0f;
                    table.AddCell(cell01);

                    PdfPCell cell2 = new PdfPCell(new iTextSharp.text.Phrase("#20 206 6th Ave East", OrderNumberRight));
                    cell2.PaddingLeft = 5;
                    cell2.BackgroundColor = new iTextSharp.text.Color(249, 249, 246);
                    cell2.BorderWidthRight = 0f;
                    cell2.BorderWidthBottom = 0f;
                    cell2.BorderWidthTop = 0f;
                    cell2.BorderColorBottom = iTextSharp.text.Color.WHITE;
                    cell2.BorderColorTop = iTextSharp.text.Color.WHITE;
                    cell2.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
                    cell2.Colspan = 2;
                    table.AddCell(cell2);

                    PdfPCell cell02 = new PdfPCell(new iTextSharp.text.Phrase(dtDealInfo.Rows[0]["firstName"].ToString().Trim() + " " + dtDealInfo.Rows[0]["lastName"].ToString().Trim(), OrderNumberRight));
                    cell02.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
                    cell02.BorderColorLeft = iTextSharp.text.Color.WHITE;
                    cell02.BorderColorBottom = iTextSharp.text.Color.WHITE;
                    cell02.BorderColorTop = iTextSharp.text.Color.WHITE;
                    cell02.BackgroundColor = new iTextSharp.text.Color(249, 249, 246);
                    cell02.Colspan = 2;
                    cell02.PaddingRight = 5;
                    cell02.BorderWidthTop = 0f;
                    cell02.BorderWidthLeft = 0f;
                    cell02.BorderWidthBottom = 0f;
                    table.AddCell(cell02);

                    PdfPCell cell3 = new PdfPCell(new iTextSharp.text.Phrase("[Vancouver BC, V5T 1J7]", OrderNumberRight));
                    cell3.PaddingLeft = 5;
                    cell3.BackgroundColor = new iTextSharp.text.Color(249, 249, 246);
                    cell3.BorderWidthRight = 0f;
                    cell3.BorderWidthBottom = 0f;
                    cell3.BorderWidthTop = 0f;
                    cell3.BorderColorBottom = iTextSharp.text.Color.WHITE;
                    cell3.BorderColorTop = iTextSharp.text.Color.WHITE;
                    cell3.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
                    cell3.Colspan = 2;
                    table.AddCell(cell3);

                    PdfPCell cell03 = new PdfPCell(new iTextSharp.text.Phrase(dtDealInfo.Rows[0]["restaurantBusinessName"].ToString().Trim(), OrderNumberRight));
                    cell03.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
                    cell03.BackgroundColor = new iTextSharp.text.Color(249, 249, 246);
                    cell03.BorderColorLeft = iTextSharp.text.Color.WHITE;
                    cell03.BorderColorBottom = iTextSharp.text.Color.WHITE;
                    cell03.BorderColorTop = iTextSharp.text.Color.WHITE;
                    cell03.Colspan = 2;
                    cell03.PaddingRight = 5;
                    cell03.BorderWidthTop = 0f;
                    cell03.BorderWidthLeft = 0f;
                    cell03.BorderWidthBottom = 0f;
                    table.AddCell(cell03);

                    PdfPCell cell4 = new PdfPCell(new iTextSharp.text.Phrase("[1855-295-1771 | 1855-295-1771]", OrderNumberRight));
                    cell4.PaddingLeft = 5;
                    cell4.BackgroundColor = new iTextSharp.text.Color(249, 249, 246);
                    cell4.BorderWidthRight = 0f;
                    cell4.BorderWidthBottom = 0f;
                    cell4.BorderWidthTop = 0f;
                    cell4.BorderColorBottom = iTextSharp.text.Color.WHITE;
                    cell4.BorderColorTop = iTextSharp.text.Color.WHITE;
                    cell4.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
                    cell4.Colspan = 2;
                    table.AddCell(cell4);

                    PdfPCell cell04 = new PdfPCell(new iTextSharp.text.Phrase(dtDealInfo.Rows[0]["restaurantAddress"].ToString().Trim().Replace("</p>", "\n").Replace("<br>", "\n").Replace("</br>", "\n").Replace("&nbsp;", " "), OrderNumberRight));
                    cell04.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
                    cell04.BorderColorLeft = iTextSharp.text.Color.WHITE;
                    cell04.BorderColorBottom = iTextSharp.text.Color.WHITE;
                    cell04.BorderColorTop = iTextSharp.text.Color.WHITE;
                    cell04.BackgroundColor = new iTextSharp.text.Color(249, 249, 246);
                    cell04.Colspan = 2;
                    cell04.PaddingRight = 5;
                    cell04.BorderWidthTop = 0f;
                    cell04.BorderWidthLeft = 0f;
                    cell04.BorderWidthBottom = 0f;
                    table.AddCell(cell04);

                    PdfPCell cell5 = new PdfPCell(new iTextSharp.text.Phrase("Fax [1888-717-7073]", OrderNumberRight));
                    cell5.PaddingLeft = 5;
                    cell5.BackgroundColor = new iTextSharp.text.Color(249, 249, 246);
                    cell5.BorderWidthRight = 0f;
                    cell5.BorderWidthBottom = 0f;
                    cell5.BorderWidthTop = 0f;
                    cell5.BorderColorBottom = iTextSharp.text.Color.WHITE;
                    cell5.BorderColorTop = iTextSharp.text.Color.WHITE;
                    cell5.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
                    cell5.Colspan = 2;
                    table.AddCell(cell5);

                    PdfPCell cell05 = new PdfPCell(new iTextSharp.text.Phrase(dtDealInfo.Rows[0]["phone"].ToString().Trim(), OrderNumberRight));
                    cell05.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
                    cell05.BorderColorLeft = iTextSharp.text.Color.WHITE;
                    cell05.BorderColorBottom = iTextSharp.text.Color.WHITE;
                    cell05.BorderColorTop = iTextSharp.text.Color.WHITE;
                    cell05.BackgroundColor = new iTextSharp.text.Color(249, 249, 246);
                    cell05.Colspan = 2;
                    cell05.PaddingRight = 5;
                    cell05.BorderWidthTop = 0f;
                    cell05.BorderWidthLeft = 0f;
                    cell05.BorderWidthBottom = 0f;
                    table.AddCell(cell05);

                    PdfPCell cell6 = new PdfPCell(new iTextSharp.text.Phrase("[info@tazzling.com]", OrderNumberRight));
                    cell6.PaddingLeft = 5;
                    cell6.PaddingBottom = 20;
                    cell6.BorderWidthRight = 0f;
                    cell6.BackgroundColor = new iTextSharp.text.Color(249, 249, 246);
                    cell6.BorderWidthTop = 0f;

                    cell6.BorderColorTop = iTextSharp.text.Color.WHITE;
                    cell6.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
                    cell6.Colspan = 2;
                    table.AddCell(cell6);

                    PdfPCell cell06 = new PdfPCell(new iTextSharp.text.Phrase(dtDealInfo.Rows[0]["email"].ToString().Trim(), OrderNumberRight));
                    cell06.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
                    cell06.BorderColorLeft = iTextSharp.text.Color.WHITE;
                    cell06.BackgroundColor = new iTextSharp.text.Color(249, 249, 246);
                    cell06.BorderColorTop = iTextSharp.text.Color.WHITE;
                    cell06.Colspan = 2;
                    cell06.PaddingRight = 5;
                    cell06.PaddingBottom = 20;
                    cell06.BorderWidthTop = 0f;
                    cell06.BorderWidthLeft = 0f;
                    table.AddCell(cell06);

                    OrderNumberRight = new iTextSharp.text.Font(iTextSharp.text.Font.GetFamilyIndex("Book Antiqua"), 8, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.BLACK);
                    PdfPCell cell7 = new PdfPCell(new iTextSharp.text.Phrase("BUSINESS LEGAL NAME", OrderNumberRight));
                    cell7.PaddingLeft = 5;
                    cell7.BackgroundColor = new iTextSharp.text.Color(219, 221, 204);
                    cell7.BorderWidthTop = 0f;
                    cell7.BorderColorTop = iTextSharp.text.Color.WHITE;
                    cell7.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
                    table.AddCell(cell7);

                    PdfPCell cell07 = new PdfPCell(new iTextSharp.text.Phrase("NAME", OrderNumberRight));
                    cell07.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
                    cell07.BackgroundColor = new iTextSharp.text.Color(219, 221, 204);
                    cell07.BorderColorLeft = iTextSharp.text.Color.WHITE;
                    cell07.PaddingLeft = 5;
                    cell07.BorderWidthLeft = 0f;
                    cell07.BorderWidthTop = 0f;
                    cell07.BorderColorTop = iTextSharp.text.Color.WHITE;
                    table.AddCell(cell07);

                    PdfPCell cell17 = new PdfPCell(new iTextSharp.text.Phrase("LENGTH", OrderNumberRight));
                    cell17.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
                    cell17.BackgroundColor = new iTextSharp.text.Color(219, 221, 204);
                    cell17.BorderColorLeft = iTextSharp.text.Color.WHITE;
                    cell17.PaddingLeft = 5;
                    cell17.BorderWidthLeft = 0f;
                    cell17.BorderWidthTop = 0f;
                    cell17.BorderColorTop = iTextSharp.text.Color.WHITE;
                    table.AddCell(cell17);

                    PdfPCell cell27 = new PdfPCell(new iTextSharp.text.Phrase("TOTAL", OrderNumberRight));
                    cell27.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
                    cell27.BackgroundColor = new iTextSharp.text.Color(219, 221, 204);
                    cell27.BorderColorLeft = iTextSharp.text.Color.WHITE;
                    cell27.PaddingLeft = 5;
                    cell27.BorderWidthLeft = 0f;
                    cell27.BorderWidthTop = 0f;
                    cell27.BorderColorTop = iTextSharp.text.Color.WHITE;
                    table.AddCell(cell27);


                    OrderNumberRight = new iTextSharp.text.Font(iTextSharp.text.Font.GetFamilyIndex("Book Antiqua"), 8, iTextSharp.text.Font.NORMAL, iTextSharp.text.Color.BLACK);

                    PdfPCell cell8 = new PdfPCell(new iTextSharp.text.Phrase(dtDealInfo.Rows[0]["restaurantBusinessName"].ToString().Trim(), OrderNumberRight));
                    cell8.PaddingLeft = 5;
                    cell8.BackgroundColor = new iTextSharp.text.Color(249, 249, 246);
                    cell8.BorderWidthTop = 0f;
                    cell8.BorderColorTop = iTextSharp.text.Color.WHITE;
                    cell8.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
                    table.AddCell(cell8);

                    PdfPCell cell08 = new PdfPCell(new iTextSharp.text.Phrase(dtDealInfo.Rows[0]["firstName"].ToString().Trim() + " " + dtDealInfo.Rows[0]["lastName"].ToString().Trim(), OrderNumberRight));
                    cell08.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
                    cell08.BorderColorLeft = iTextSharp.text.Color.WHITE;
                    cell08.BackgroundColor = new iTextSharp.text.Color(249, 249, 246);
                    cell08.PaddingLeft = 5;
                    cell08.BorderWidthLeft = 0f;
                    cell08.BorderWidthTop = 0f;
                    cell08.BorderColorTop = iTextSharp.text.Color.WHITE;
                    table.AddCell(cell08);

                    DateTime dtStart = Convert.ToDateTime(dtDealInfo.Rows[0]["dealStartTime"].ToString());
                    DateTime dtEndTime = Convert.ToDateTime(dtDealInfo.Rows[0]["dealEndTime"].ToString());

                    TimeSpan dtDifference = dtEndTime - dtStart;

                    PdfPCell cell18 = new PdfPCell(new iTextSharp.text.Phrase(dtDifference.Days.ToString(), OrderNumberRight));
                    cell18.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
                    cell18.BorderColorLeft = iTextSharp.text.Color.WHITE;
                    cell18.BackgroundColor = new iTextSharp.text.Color(249, 249, 246);
                    cell18.PaddingLeft = 5;
                    cell18.BorderWidthLeft = 0f;
                    cell18.BorderWidthTop = 0f;
                    cell18.BorderColorTop = iTextSharp.text.Color.WHITE;
                    table.AddCell(cell18);


                    PdfPCell cell28 = new PdfPCell(new iTextSharp.text.Phrase(dtDealInfo.Rows[0]["Orders"].ToString().Trim(), OrderNumberRight));
                    cell28.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
                    cell28.BorderColorLeft = iTextSharp.text.Color.WHITE;
                    cell28.PaddingLeft = 5;
                    cell28.BackgroundColor = new iTextSharp.text.Color(249, 249, 246);
                    cell28.BorderWidthLeft = 0f;
                    cell28.BorderWidthTop = 0f;
                    cell28.BorderColorTop = iTextSharp.text.Color.WHITE;
                    table.AddCell(cell28);


                    PdfPCell cell7TopBorder8 = new PdfPCell(new iTextSharp.text.Phrase(" ", OrderNumberRight));
                    cell7TopBorder8.PaddingBottom = 10;
                    cell7TopBorder8.BorderWidthTop = 0f;
                    cell7TopBorder8.BackgroundColor = new iTextSharp.text.Color(249, 249, 246);
                    cell7TopBorder8.BorderColorTop = iTextSharp.text.Color.WHITE;
                    cell7TopBorder8.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
                    cell7TopBorder8.Colspan = 4;
                    table.AddCell(cell7TopBorder8);


                    OrderNumberRight = new iTextSharp.text.Font(iTextSharp.text.Font.GetFamilyIndex("Book Antiqua"), 8, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.BLACK);
                    PdfPCell cell9 = new PdfPCell(new iTextSharp.text.Phrase("DATE", OrderNumberRight));
                    cell9.PaddingLeft = 5;
                    cell9.BackgroundColor = new iTextSharp.text.Color(219, 221, 204);
                    cell9.BorderWidthTop = 0f;
                    cell9.BorderColorTop = iTextSharp.text.Color.WHITE;
                    cell9.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
                    table.AddCell(cell9);

                    PdfPCell cell09 = new PdfPCell(new iTextSharp.text.Phrase("DESCRIPTION", OrderNumberRight));
                    cell09.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
                    cell09.BorderColorLeft = iTextSharp.text.Color.WHITE;
                    cell09.PaddingLeft = 5;
                    cell09.BackgroundColor = new iTextSharp.text.Color(219, 221, 204);
                    cell09.BorderWidthLeft = 0f;
                    cell09.BorderWidthTop = 0f;
                    cell09.BorderColorTop = iTextSharp.text.Color.WHITE;
                    table.AddCell(cell09);

                    PdfPCell cell19 = new PdfPCell(new iTextSharp.text.Phrase("UNIT PRICE", OrderNumberRight));
                    cell19.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
                    cell19.BorderColorLeft = iTextSharp.text.Color.WHITE;
                    cell19.PaddingLeft = 5;
                    cell19.BackgroundColor = new iTextSharp.text.Color(219, 221, 204);
                    cell19.BorderWidthLeft = 0f;
                    cell19.BorderWidthTop = 0f;
                    cell19.BorderColorTop = iTextSharp.text.Color.WHITE;
                    table.AddCell(cell19);

                    PdfPCell cell29 = new PdfPCell(new iTextSharp.text.Phrase("LINE TOTAL", OrderNumberRight));
                    cell29.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
                    cell29.BorderColorLeft = iTextSharp.text.Color.WHITE;
                    cell29.PaddingLeft = 5;
                    cell29.BackgroundColor = new iTextSharp.text.Color(219, 221, 204);
                    cell29.BorderWidthLeft = 0f;
                    cell29.BorderWidthTop = 0f;
                    cell29.BorderColorTop = iTextSharp.text.Color.WHITE;
                    table.AddCell(cell29);


                    DateTime dtDealEndTime = Convert.ToDateTime(dtDealInfo.Rows[0]["dealEndTime"].ToString().Trim());
                    string strDate = "";
                    if (dtDealEndTime > DateTime.Now)
                    {
                        strDate = DateTime.Now.ToString("MM/dd/yyyy");
                    }
                    else
                    {
                        strDate = dtDealEndTime.ToString("MM/dd/yyyy HH:mm:ss");
                    }

                    double dealUnitPrice = 0;
                    double advertisingFee = 0;
                    double ccTransactionFee = 0;
                    double companyComission = 0;
                    double taxAmount = 0;
                    int totalOrders = 0;
                    // int refundedOrders = 0;
                    double refundedAmount = 0;
                    double.TryParse(dtDealInfo.Rows[0]["sellingPrice"].ToString().Trim(), out dealUnitPrice);
                    int.TryParse(dtDealInfo.Rows[0]["Orders"].ToString().Trim(), out totalOrders);
                    double.TryParse(dtDealInfo.Rows[0]["OurCommission"].ToString().Trim(), out companyComission);
                    ccTransactionFee = Math.Round((3.9 / 100) * (dealUnitPrice * totalOrders), 2, MidpointRounding.AwayFromZero);
                    advertisingFee = Math.Round((companyComission / 100) * (dealUnitPrice * totalOrders), 2, MidpointRounding.AwayFromZero);
                    double.TryParse(Convert.ToString(Math.Round((advertisingFee) * (12.00 / 100), 2, MidpointRounding.AwayFromZero)), out taxAmount);
                    double TopBalance = Math.Round((dealUnitPrice * totalOrders) - advertisingFee - taxAmount - ccTransactionFee, 2, MidpointRounding.AwayFromZero);


                    OrderNumberRight = new iTextSharp.text.Font(iTextSharp.text.Font.GetFamilyIndex("Book Antiqua"), 8, iTextSharp.text.Font.NORMAL, iTextSharp.text.Color.BLACK);
                    PdfPCell cell10 = new PdfPCell(new iTextSharp.text.Phrase(strDate, OrderNumberRight));
                    cell10.PaddingLeft = 5;
                    cell10.BackgroundColor = new iTextSharp.text.Color(249, 249, 246);
                    cell10.BorderWidthTop = 0f;
                    cell10.BorderColorTop = iTextSharp.text.Color.WHITE;
                    cell10.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
                    table.AddCell(cell10);

                    PdfPCell cell010 = new PdfPCell(new iTextSharp.text.Phrase(dtDealInfo.Rows[0]["title"].ToString().Trim(), OrderNumberRight));
                    cell010.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
                    cell010.BorderColorLeft = iTextSharp.text.Color.WHITE;
                    cell010.BackgroundColor = new iTextSharp.text.Color(249, 249, 246);
                    cell010.PaddingLeft = 5;
                    cell010.BorderWidthLeft = 0f;
                    cell010.BorderWidthTop = 0f;
                    cell010.BorderColorTop = iTextSharp.text.Color.WHITE;
                    table.AddCell(cell010);

                    PdfPCell cell110 = new PdfPCell(new iTextSharp.text.Phrase("$" + dtDealInfo.Rows[0]["sellingPrice"].ToString().Trim() + " x " + dtDealInfo.Rows[0]["Orders"].ToString().Trim(), OrderNumberRight));
                    cell110.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
                    cell110.BorderColorLeft = iTextSharp.text.Color.WHITE;
                    cell110.BackgroundColor = new iTextSharp.text.Color(249, 249, 246);
                    cell110.PaddingLeft = 5;
                    cell110.BorderWidthLeft = 0f;
                    cell110.BorderWidthTop = 0f;
                    cell110.BorderColorTop = iTextSharp.text.Color.WHITE;
                    table.AddCell(cell110);

                    PdfPCell cell210 = new PdfPCell(new iTextSharp.text.Phrase("$" + Convert.ToDouble(dealUnitPrice * totalOrders).ToString("###.00"), OrderNumberRight));
                    cell210.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
                    cell210.BorderColorLeft = iTextSharp.text.Color.WHITE;
                    cell210.BackgroundColor = new iTextSharp.text.Color(249, 249, 246);
                    cell210.PaddingLeft = 5;
                    cell210.BorderWidthLeft = 0f;
                    cell210.BorderWidthTop = 0f;
                    cell210.BorderColorTop = iTextSharp.text.Color.WHITE;
                    table.AddCell(cell210);



                    PdfPCell cell11 = new PdfPCell(new iTextSharp.text.Phrase(strDate, OrderNumberRight));
                    cell11.PaddingLeft = 5;
                    cell11.BackgroundColor = new iTextSharp.text.Color(237, 238, 229);
                    cell11.BorderWidthTop = 0f;
                    cell11.BorderColorTop = iTextSharp.text.Color.WHITE;
                    cell11.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
                    table.AddCell(cell11);

                    PdfPCell cell011 = new PdfPCell(new iTextSharp.text.Phrase("Advertising Fee", OrderNumberRight));
                    cell011.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
                    cell011.BorderColorLeft = iTextSharp.text.Color.WHITE;
                    cell011.BackgroundColor = new iTextSharp.text.Color(237, 238, 229);
                    cell011.PaddingLeft = 5;
                    cell011.BorderWidthLeft = 0f;
                    cell011.BorderWidthTop = 0f;
                    cell011.BorderColorTop = iTextSharp.text.Color.WHITE;
                    table.AddCell(cell011);

                    PdfPCell cell111 = new PdfPCell(new iTextSharp.text.Phrase(companyComission.ToString() + "% x " + Convert.ToDouble(dealUnitPrice * totalOrders).ToString("###.00"), OrderNumberRight));
                    cell111.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
                    cell111.BorderColorLeft = iTextSharp.text.Color.WHITE;
                    cell111.BackgroundColor = new iTextSharp.text.Color(237, 238, 229);
                    cell111.PaddingLeft = 5;
                    cell111.BorderWidthLeft = 0f;
                    cell111.BorderWidthTop = 0f;
                    cell111.BorderColorTop = iTextSharp.text.Color.WHITE;
                    table.AddCell(cell111);

                    OrderNumberRight = new iTextSharp.text.Font(iTextSharp.text.Font.GetFamilyIndex("Book Antiqua"), 8, iTextSharp.text.Font.NORMAL, iTextSharp.text.Color.RED);
                    PdfPCell cell211 = new PdfPCell(new iTextSharp.text.Phrase("-$" + advertisingFee.ToString("###.00"), OrderNumberRight));
                    cell211.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
                    cell211.BorderColorLeft = iTextSharp.text.Color.WHITE;
                    cell211.BackgroundColor = new iTextSharp.text.Color(237, 238, 229);
                    cell211.PaddingLeft = 5;
                    cell211.BorderWidthLeft = 0f;
                    cell211.BorderWidthTop = 0f;
                    cell211.BorderColorTop = iTextSharp.text.Color.WHITE;
                    table.AddCell(cell211);


                    OrderNumberRight = new iTextSharp.text.Font(iTextSharp.text.Font.GetFamilyIndex("Book Antiqua"), 8, iTextSharp.text.Font.NORMAL, iTextSharp.text.Color.BLACK);
                    PdfPCell cell12 = new PdfPCell(new iTextSharp.text.Phrase(strDate, OrderNumberRight));
                    cell12.BackgroundColor = new iTextSharp.text.Color(249, 249, 246);
                    cell12.PaddingLeft = 5;
                    cell12.BorderWidthTop = 0f;
                    cell12.BorderColorTop = iTextSharp.text.Color.WHITE;
                    cell12.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
                    table.AddCell(cell12);

                    PdfPCell cell012 = new PdfPCell(new iTextSharp.text.Phrase("Credit Card Transaction Fee", OrderNumberRight));
                    cell012.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
                    cell012.BorderColorLeft = iTextSharp.text.Color.WHITE;
                    cell012.BackgroundColor = new iTextSharp.text.Color(249, 249, 246);
                    cell012.PaddingLeft = 5;
                    cell012.BorderWidthLeft = 0f;
                    cell012.BorderWidthTop = 0f;
                    cell012.BorderColorTop = iTextSharp.text.Color.WHITE;
                    table.AddCell(cell012);

                    PdfPCell cell112 = new PdfPCell(new iTextSharp.text.Phrase("3.9% x " + Convert.ToDouble(dealUnitPrice * totalOrders).ToString("###.00"), OrderNumberRight));
                    cell112.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
                    cell112.BorderColorLeft = iTextSharp.text.Color.WHITE;
                    cell112.BackgroundColor = new iTextSharp.text.Color(249, 249, 246);
                    cell112.PaddingLeft = 5;
                    cell112.BorderWidthLeft = 0f;
                    cell112.BorderWidthTop = 0f;
                    cell112.BorderColorTop = iTextSharp.text.Color.WHITE;
                    table.AddCell(cell112);

                    OrderNumberRight = new iTextSharp.text.Font(iTextSharp.text.Font.GetFamilyIndex("Book Antiqua"), 8, iTextSharp.text.Font.NORMAL, iTextSharp.text.Color.RED);
                    PdfPCell cell212 = new PdfPCell(new iTextSharp.text.Phrase("-$" + ccTransactionFee.ToString("###.00"), OrderNumberRight));
                    cell212.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
                    cell212.BorderColorLeft = iTextSharp.text.Color.WHITE;
                    cell212.BackgroundColor = new iTextSharp.text.Color(249, 249, 246);
                    cell212.PaddingLeft = 5;
                    cell212.BorderWidthLeft = 0f;
                    cell212.BorderWidthTop = 0f;
                    cell212.BorderColorTop = iTextSharp.text.Color.WHITE;
                    table.AddCell(cell212);

                    OrderNumberRight = new iTextSharp.text.Font(iTextSharp.text.Font.GetFamilyIndex("Book Antiqua"), 8, iTextSharp.text.Font.NORMAL, iTextSharp.text.Color.BLACK);
                    PdfPCell cell13 = new PdfPCell(new iTextSharp.text.Phrase(strDate, OrderNumberRight));
                    cell13.BackgroundColor = new iTextSharp.text.Color(237, 238, 229);
                    cell13.PaddingLeft = 5;
                    cell13.BorderWidthTop = 0f;
                    cell13.BorderColorTop = iTextSharp.text.Color.WHITE;
                    cell13.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
                    table.AddCell(cell13);

                    PdfPCell cell013 = new PdfPCell(new iTextSharp.text.Phrase("HST #849725056", OrderNumberRight));
                    cell013.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
                    cell013.BorderColorLeft = iTextSharp.text.Color.WHITE;
                    cell013.BackgroundColor = new iTextSharp.text.Color(237, 238, 229);
                    cell013.PaddingLeft = 5;
                    cell013.BorderWidthLeft = 0f;
                    cell013.BorderWidthTop = 0f;
                    cell013.BorderColorTop = iTextSharp.text.Color.WHITE;
                    table.AddCell(cell013);

                    PdfPCell cell113 = new PdfPCell(new iTextSharp.text.Phrase("$" + advertisingFee.ToString("###.00") + " x 12%", OrderNumberRight));
                    cell113.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
                    cell113.BorderColorLeft = iTextSharp.text.Color.WHITE;
                    cell113.BackgroundColor = new iTextSharp.text.Color(237, 238, 229);
                    cell113.PaddingLeft = 5;
                    cell113.BorderWidthLeft = 0f;
                    cell113.BorderWidthTop = 0f;
                    cell113.BorderColorTop = iTextSharp.text.Color.WHITE;
                    table.AddCell(cell113);

                    OrderNumberRight = new iTextSharp.text.Font(iTextSharp.text.Font.GetFamilyIndex("Book Antiqua"), 8, iTextSharp.text.Font.NORMAL, iTextSharp.text.Color.RED);
                    PdfPCell cell213 = new PdfPCell(new iTextSharp.text.Phrase("-$" + taxAmount.ToString("###.00"), OrderNumberRight));
                    cell213.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
                    cell213.BackgroundColor = new iTextSharp.text.Color(237, 238, 229);
                    cell213.BorderColorLeft = iTextSharp.text.Color.WHITE;
                    cell213.PaddingLeft = 5;
                    cell213.BorderWidthLeft = 0f;
                    cell213.BorderWidthTop = 0f;
                    cell213.BorderColorTop = iTextSharp.text.Color.WHITE;
                    table.AddCell(cell213);
                    OrderNumberRight = new iTextSharp.text.Font(iTextSharp.text.Font.GetFamilyIndex("Book Antiqua"), 8, iTextSharp.text.Font.NORMAL, iTextSharp.text.Color.BLACK);
                    strQuery = "select dealOrderCode,createdDate, modifiedDate, qty from dealorders inner join dealOrderDetail on dealOrderDetail.dOrderID = dealOrders.dOrderID where status<>'Successful' and dealid=" + dealID.ToString() + " order by modifiedDate desc";
                    DataTable dtRefundedOrders = Misc.search(strQuery);
                    if (dtRefundedOrders != null && dtRefundedOrders.Rows.Count > 0)
                    {
                        for (int i = 0; i < dtRefundedOrders.Rows.Count; i++)
                        {
                            PdfPCell cell14 = new PdfPCell(new iTextSharp.text.Phrase((dtRefundedOrders.Rows[i]["modifiedDate"].ToString().Trim() != "" ? dtRefundedOrders.Rows[i]["modifiedDate"].ToString().Trim() : dtRefundedOrders.Rows[i]["createdDate"].ToString().Trim()), OrderNumberRight));
                            cell14.PaddingLeft = 5;
                            cell14.BackgroundColor = new iTextSharp.text.Color(249, 249, 246);

                            cell14.BorderWidthTop = 0f;
                            cell14.BorderColorTop = iTextSharp.text.Color.WHITE;
                            cell14.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
                            table.AddCell(cell14);

                            PdfPCell cell014 = new PdfPCell(new iTextSharp.text.Phrase("Refund # " + objEnc.DecryptData("deatailOrder", dtRefundedOrders.Rows[i]["dealOrderCode"].ToString()), OrderNumberRight));
                            cell014.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
                            cell014.BorderColorLeft = iTextSharp.text.Color.WHITE;
                            cell014.BackgroundColor = new iTextSharp.text.Color(249, 249, 246);

                            cell014.PaddingLeft = 5;
                            cell014.BorderWidthLeft = 0f;
                            cell014.BorderWidthTop = 0f;
                            cell014.BorderColorTop = iTextSharp.text.Color.WHITE;
                            table.AddCell(cell014);

                            PdfPCell cell114 = new PdfPCell(new iTextSharp.text.Phrase("$" + dealUnitPrice.ToString() + " x " + dtRefundedOrders.Rows[i]["qty"].ToString().Trim(), OrderNumberRight));
                            cell114.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
                            cell114.BorderColorLeft = iTextSharp.text.Color.WHITE;
                            cell114.PaddingLeft = 5;
                            cell114.BackgroundColor = new iTextSharp.text.Color(249, 249, 246);

                            cell114.BorderWidthLeft = 0f;
                            cell114.BorderWidthTop = 0f;
                            cell114.BorderColorTop = iTextSharp.text.Color.WHITE;
                            table.AddCell(cell114);

                            double tempRefunded = Math.Round((dealUnitPrice * Convert.ToDouble(dtRefundedOrders.Rows[i]["qty"].ToString().Trim())), 2, MidpointRounding.AwayFromZero);
                            double tempAdveriseFee = Math.Round((companyComission / 100) * tempRefunded, 2, MidpointRounding.AwayFromZero);
                            double tempccTransactionFee = Math.Round((3.9 / 100) * tempRefunded, 2, MidpointRounding.AwayFromZero);
                            double tempTax = Math.Round((12.00 / 100) * tempAdveriseFee, 2, MidpointRounding.AwayFromZero);

                            OrderNumberRight = new iTextSharp.text.Font(iTextSharp.text.Font.GetFamilyIndex("Book Antiqua"), 8, iTextSharp.text.Font.NORMAL, iTextSharp.text.Color.RED);
                            PdfPCell cell214 = new PdfPCell(new iTextSharp.text.Phrase("-$" + tempRefunded.ToString(), OrderNumberRight));
                            cell214.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
                            cell214.BorderColorLeft = iTextSharp.text.Color.WHITE;
                            cell214.BackgroundColor = new iTextSharp.text.Color(249, 249, 246);
                            cell214.PaddingLeft = 5;
                            cell214.BorderWidthLeft = 0f;
                            cell214.BorderWidthTop = 0f;
                            cell214.BorderColorTop = iTextSharp.text.Color.WHITE;
                            table.AddCell(cell214);

                            OrderNumberRight = new iTextSharp.text.Font(iTextSharp.text.Font.GetFamilyIndex("Book Antiqua"), 8, iTextSharp.text.Font.NORMAL, iTextSharp.text.Color.BLACK);
                            PdfPCell cell15 = new PdfPCell(new iTextSharp.text.Phrase((dtRefundedOrders.Rows[i]["modifiedDate"].ToString().Trim() != "" ? dtRefundedOrders.Rows[i]["modifiedDate"].ToString().Trim() : dtRefundedOrders.Rows[i]["createdDate"].ToString().Trim()), OrderNumberRight));
                            cell15.PaddingLeft = 5;
                            cell15.BackgroundColor = new iTextSharp.text.Color(237, 238, 229);

                            cell15.BorderWidthTop = 0f;
                            cell15.BorderColorTop = iTextSharp.text.Color.WHITE;
                            cell15.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
                            table.AddCell(cell15);

                            PdfPCell cell015 = new PdfPCell(new iTextSharp.text.Phrase("Reverse Advertising Fee", OrderNumberRight));
                            cell015.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
                            cell015.BorderColorLeft = iTextSharp.text.Color.WHITE;
                            cell015.PaddingLeft = 5;
                            cell015.BackgroundColor = new iTextSharp.text.Color(237, 238, 229);

                            cell015.BorderWidthLeft = 0f;
                            cell015.BorderWidthTop = 0f;
                            cell015.BorderColorTop = iTextSharp.text.Color.WHITE;
                            table.AddCell(cell015);

                            PdfPCell cell115 = new PdfPCell(new iTextSharp.text.Phrase(companyComission.ToString() + "% x $" + tempRefunded.ToString(), OrderNumberRight));
                            cell115.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
                            cell115.BorderColorLeft = iTextSharp.text.Color.WHITE;
                            cell115.PaddingLeft = 5;
                            cell115.BackgroundColor = new iTextSharp.text.Color(237, 238, 229);

                            cell115.BorderWidthLeft = 0f;
                            cell115.BorderWidthTop = 0f;
                            cell115.BorderColorTop = iTextSharp.text.Color.WHITE;
                            table.AddCell(cell115);

                            PdfPCell cell215 = new PdfPCell(new iTextSharp.text.Phrase("$" + tempAdveriseFee.ToString(), OrderNumberRight));
                            cell215.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
                            cell215.BorderColorLeft = iTextSharp.text.Color.WHITE;
                            cell215.PaddingLeft = 5;
                            cell215.BackgroundColor = new iTextSharp.text.Color(237, 238, 229);
                            cell215.BorderWidthLeft = 0f;
                            cell215.BorderWidthTop = 0f;
                            cell215.BorderColorTop = iTextSharp.text.Color.WHITE;
                            table.AddCell(cell215);


                            PdfPCell cell16 = new PdfPCell(new iTextSharp.text.Phrase((dtRefundedOrders.Rows[i]["modifiedDate"].ToString().Trim() != "" ? dtRefundedOrders.Rows[i]["modifiedDate"].ToString().Trim() : dtRefundedOrders.Rows[i]["createdDate"].ToString().Trim()), OrderNumberRight));
                            cell16.PaddingLeft = 5;
                            cell16.BackgroundColor = new iTextSharp.text.Color(249, 249, 246);

                            cell16.BorderWidthTop = 0f;
                            cell16.BorderColorTop = iTextSharp.text.Color.WHITE;
                            cell16.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
                            table.AddCell(cell16);

                            PdfPCell cell016 = new PdfPCell(new iTextSharp.text.Phrase("Credit Card Transaction Fee", OrderNumberRight));
                            cell016.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
                            cell016.BorderColorLeft = iTextSharp.text.Color.WHITE;
                            cell016.PaddingLeft = 5;
                            cell016.BackgroundColor = new iTextSharp.text.Color(249, 249, 246);

                            cell016.BorderWidthLeft = 0f;
                            cell016.BorderWidthTop = 0f;
                            cell016.BorderColorTop = iTextSharp.text.Color.WHITE;
                            table.AddCell(cell016);

                            PdfPCell cell116 = new PdfPCell(new iTextSharp.text.Phrase("3.9% x $" + tempRefunded.ToString(), OrderNumberRight));
                            cell116.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
                            cell116.BorderColorLeft = iTextSharp.text.Color.WHITE;
                            cell116.BackgroundColor = new iTextSharp.text.Color(249, 249, 246);

                            cell116.PaddingLeft = 5;
                            cell116.BorderWidthLeft = 0f;
                            cell116.BorderWidthTop = 0f;
                            cell116.BorderColorTop = iTextSharp.text.Color.WHITE;
                            table.AddCell(cell116);

                            OrderNumberRight = new iTextSharp.text.Font(iTextSharp.text.Font.GetFamilyIndex("Book Antiqua"), 8, iTextSharp.text.Font.NORMAL, iTextSharp.text.Color.RED);
                            PdfPCell cell216 = new PdfPCell(new iTextSharp.text.Phrase("-$" + tempccTransactionFee.ToString(), OrderNumberRight));
                            cell216.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
                            cell216.BorderColorLeft = iTextSharp.text.Color.WHITE;
                            cell216.BackgroundColor = new iTextSharp.text.Color(249, 249, 246);
                            cell216.PaddingLeft = 5;
                            cell216.BorderWidthLeft = 0f;
                            cell216.BorderWidthTop = 0f;
                            cell216.BorderColorTop = iTextSharp.text.Color.WHITE;
                            table.AddCell(cell216);

                            OrderNumberRight = new iTextSharp.text.Font(iTextSharp.text.Font.GetFamilyIndex("Book Antiqua"), 8, iTextSharp.text.Font.NORMAL, iTextSharp.text.Color.BLACK);
                            PdfPCell cell170 = new PdfPCell(new iTextSharp.text.Phrase((dtRefundedOrders.Rows[i]["modifiedDate"].ToString().Trim() != "" ? dtRefundedOrders.Rows[i]["modifiedDate"].ToString().Trim() : dtRefundedOrders.Rows[i]["createdDate"].ToString().Trim()), OrderNumberRight));
                            cell170.PaddingLeft = 5;
                            cell170.BackgroundColor = new iTextSharp.text.Color(237, 238, 229);

                            cell170.BorderWidthTop = 0f;
                            cell170.BorderColorTop = iTextSharp.text.Color.WHITE;
                            cell170.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
                            table.AddCell(cell170);

                            PdfPCell cell017 = new PdfPCell(new iTextSharp.text.Phrase("Reverse HST #849725056", OrderNumberRight));
                            cell017.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
                            cell017.BackgroundColor = new iTextSharp.text.Color(237, 238, 229);

                            cell017.BorderColorLeft = iTextSharp.text.Color.WHITE;
                            cell017.PaddingLeft = 5;
                            cell017.BorderWidthLeft = 0f;
                            cell017.BorderWidthTop = 0f;
                            cell017.BorderColorTop = iTextSharp.text.Color.WHITE;
                            table.AddCell(cell017);

                            PdfPCell cell117 = new PdfPCell(new iTextSharp.text.Phrase("$" + tempAdveriseFee.ToString().Trim() + " x 12%", OrderNumberRight));
                            cell117.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
                            cell117.BorderColorLeft = iTextSharp.text.Color.WHITE;
                            cell117.BackgroundColor = new iTextSharp.text.Color(237, 238, 229);

                            cell117.PaddingLeft = 5;
                            cell117.BorderWidthLeft = 0f;
                            cell117.BorderWidthTop = 0f;
                            cell117.BorderColorTop = iTextSharp.text.Color.WHITE;
                            table.AddCell(cell117);

                            PdfPCell cell217 = new PdfPCell(new iTextSharp.text.Phrase("$" + tempTax.ToString(), OrderNumberRight));
                            cell217.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
                            cell217.BorderColorLeft = iTextSharp.text.Color.WHITE;
                            cell217.BackgroundColor = new iTextSharp.text.Color(237, 238, 229);
                            cell217.PaddingLeft = 5;
                            cell217.BorderWidthLeft = 0f;
                            cell217.BorderWidthTop = 0f;
                            cell217.BorderColorTop = iTextSharp.text.Color.WHITE;
                            table.AddCell(cell217);
                            refundedAmount += Math.Round(((tempAdveriseFee + tempTax - tempccTransactionFee) - (tempRefunded)), 2, MidpointRounding.AwayFromZero);
                        }
                    }
                    double grandTotal = Math.Round(TopBalance + refundedAmount, 2, MidpointRounding.AwayFromZero);


                    BLLPayOut objPayout = new BLLPayOut();
                    objPayout.dealId = dealID;
                    DataTable dtpayout = objPayout.getPayOutByDealID();
                    if (dtpayout != null && dtpayout.Rows.Count > 0)
                    {

                        PdfPCell cell181 = new PdfPCell(new iTextSharp.text.Phrase(strDate, OrderNumberRight));
                        cell181.PaddingLeft = 5;
                        cell181.BackgroundColor = new iTextSharp.text.Color(219, 221, 204);
                        cell181.BorderWidthTop = 0f;
                        cell181.BorderColorTop = iTextSharp.text.Color.WHITE;
                        cell181.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
                        table.AddCell(cell181);

                        PdfPCell cell081 = new PdfPCell(new iTextSharp.text.Phrase(" ", OrderNumberRight));
                        cell081.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
                        cell081.BorderColorLeft = iTextSharp.text.Color.WHITE;
                        cell081.BackgroundColor = new iTextSharp.text.Color(219, 221, 204);
                        cell081.PaddingLeft = 5;
                        cell081.BorderWidthLeft = 0f;
                        cell081.BorderWidthTop = 0f;
                        cell081.BorderColorTop = iTextSharp.text.Color.WHITE;
                        table.AddCell(cell081);

                        OrderNumberRight = new iTextSharp.text.Font(iTextSharp.text.Font.GetFamilyIndex("Book Antiqua"), 8, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.BLACK);
                        PdfPCell cell1812 = new PdfPCell(new iTextSharp.text.Phrase("Balance", OrderNumberRight));
                        cell1812.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
                        cell1812.BackgroundColor = new iTextSharp.text.Color(219, 221, 204);
                        cell1812.BorderColorLeft = iTextSharp.text.Color.WHITE;
                        cell1812.PaddingLeft = 5;
                        cell1812.BorderWidthLeft = 0f;
                        cell1812.BorderWidthTop = 0f;
                        cell1812.BorderColorTop = iTextSharp.text.Color.WHITE;
                        table.AddCell(cell1812);

                        PdfPCell cell281 = new PdfPCell(new iTextSharp.text.Phrase("$" + grandTotal.ToString(), OrderNumberRight));
                        cell281.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
                        cell281.BorderColorLeft = iTextSharp.text.Color.WHITE;
                        cell281.BackgroundColor = new iTextSharp.text.Color(219, 221, 204);
                        cell281.PaddingLeft = 5;
                        cell281.BorderWidthLeft = 0f;
                        cell281.BorderWidthTop = 0f;
                        cell281.BorderColorTop = iTextSharp.text.Color.WHITE;
                        table.AddCell(cell281);
                        for (int i = 0; i < dtpayout.Rows.Count; i++)
                        {

                            OrderNumberRight = new iTextSharp.text.Font(iTextSharp.text.Font.GetFamilyIndex("Book Antiqua"), 8, iTextSharp.text.Font.NORMAL, iTextSharp.text.Color.BLACK);

                            PdfPCell cell191 = new PdfPCell(new iTextSharp.text.Phrase(dtpayout.Rows[i]["poDate"].ToString().Trim(), OrderNumberRight));
                            cell191.PaddingLeft = 5;
                            cell191.BorderWidthTop = 0f;
                            if (i % 2 == 0)
                            {
                                cell191.BackgroundColor = new iTextSharp.text.Color(249, 249, 246);
                            }
                            else
                            {
                                cell191.BackgroundColor = new iTextSharp.text.Color(237, 238, 229);
                            }
                            cell191.BorderColorTop = iTextSharp.text.Color.WHITE;
                            cell191.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
                            table.AddCell(cell191);

                            PdfPCell cell091 = new PdfPCell(new iTextSharp.text.Phrase("TastyGo Payout #" + (i + 1).ToString(), OrderNumberRight));
                            cell091.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
                            cell091.BorderColorLeft = iTextSharp.text.Color.WHITE;
                            cell091.PaddingLeft = 5;
                            if (i % 2 == 0)
                            {
                                cell091.BackgroundColor = new iTextSharp.text.Color(249, 249, 246);
                            }
                            else
                            {
                                cell091.BackgroundColor = new iTextSharp.text.Color(237, 238, 229);
                            }
                            cell091.BorderWidthLeft = 0f;
                            cell091.BorderWidthTop = 0f;
                            cell091.BorderColorTop = iTextSharp.text.Color.WHITE;
                            table.AddCell(cell091);

                            PdfPCell cell192 = new PdfPCell(new iTextSharp.text.Phrase(dtpayout.Rows[i]["poDescription"].ToString().Trim(), OrderNumberRight));
                            cell192.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
                            cell192.BorderColorLeft = iTextSharp.text.Color.WHITE;
                            cell192.PaddingLeft = 5;
                            if (i % 2 == 0)
                            {
                                cell192.BackgroundColor = new iTextSharp.text.Color(249, 249, 246);
                            }
                            else
                            {
                                cell192.BackgroundColor = new iTextSharp.text.Color(237, 238, 229);
                            }
                            cell192.BorderWidthLeft = 0f;
                            cell192.BorderWidthTop = 0f;
                            cell192.BorderColorTop = iTextSharp.text.Color.WHITE;
                            table.AddCell(cell192);

                            double dTemPayout = Convert.ToDouble(dtpayout.Rows[i]["poAmount"].ToString().Trim());
                            if (dTemPayout < 0)
                            {
                                OrderNumberRight = new iTextSharp.text.Font(iTextSharp.text.Font.GetFamilyIndex("Book Antiqua"), 8, iTextSharp.text.Font.NORMAL, iTextSharp.text.Color.RED);
                                dTemPayout = Math.Round((dTemPayout) * (-1), 2, MidpointRounding.AwayFromZero);
                                PdfPCell cell291 = new PdfPCell(new iTextSharp.text.Phrase("-$" + dTemPayout.ToString(), OrderNumberRight));
                                cell291.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
                                cell291.BorderColorLeft = iTextSharp.text.Color.WHITE;
                                cell291.PaddingLeft = 5;
                                if (i % 2 == 0)
                                {
                                    cell291.BackgroundColor = new iTextSharp.text.Color(249, 249, 246);
                                }
                                else
                                {
                                    cell291.BackgroundColor = new iTextSharp.text.Color(237, 238, 229);
                                }
                                cell291.BorderWidthLeft = 0f;
                                cell291.BorderWidthTop = 0f;
                                cell291.BorderColorTop = iTextSharp.text.Color.WHITE;
                                table.AddCell(cell291);

                            }
                            else
                            {
                                OrderNumberRight = new iTextSharp.text.Font(iTextSharp.text.Font.GetFamilyIndex("Book Antiqua"), 8, iTextSharp.text.Font.NORMAL, iTextSharp.text.Color.BLACK);
                                PdfPCell cell293 = new PdfPCell(new iTextSharp.text.Phrase("$" + dTemPayout.ToString(), OrderNumberRight));
                                cell293.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
                                cell293.BorderColorLeft = iTextSharp.text.Color.WHITE;
                                if (i % 2 == 0)
                                {
                                    cell293.BackgroundColor = new iTextSharp.text.Color(249, 249, 246);
                                }
                                else
                                {
                                    cell293.BackgroundColor = new iTextSharp.text.Color(237, 238, 229);
                                }
                                cell293.PaddingLeft = 5;
                                cell293.BorderWidthLeft = 0f;
                                cell293.BorderWidthTop = 0f;
                                cell293.BorderColorTop = iTextSharp.text.Color.WHITE;
                                table.AddCell(cell293);
                            }


                        }

                        OrderNumberRight = new iTextSharp.text.Font(iTextSharp.text.Font.GetFamilyIndex("Book Antiqua"), 8, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.BLACK);
                        PdfPCell cell182 = new PdfPCell(new iTextSharp.text.Phrase(" ", OrderNumberRight));
                        cell182.PaddingLeft = 5;
                        cell182.BorderWidthTop = 0f;
                        cell182.BorderColorTop = iTextSharp.text.Color.WHITE;
                        cell182.BackgroundColor = new iTextSharp.text.Color(219, 221, 204);
                        cell182.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
                        table.AddCell(cell182);

                        PdfPCell cell082 = new PdfPCell(new iTextSharp.text.Phrase(" ", OrderNumberRight));
                        cell082.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
                        cell082.BorderColorLeft = iTextSharp.text.Color.WHITE;
                        cell082.PaddingLeft = 5;
                        cell082.BackgroundColor = new iTextSharp.text.Color(219, 221, 204);
                        cell082.BorderWidthLeft = 0f;
                        cell082.BorderWidthTop = 0f;
                        cell082.BorderColorTop = iTextSharp.text.Color.WHITE;
                        table.AddCell(cell082);

                        PdfPCell cell183 = new PdfPCell(new iTextSharp.text.Phrase("Payout Balance", OrderNumberRight));
                        cell183.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
                        cell183.BorderColorLeft = iTextSharp.text.Color.WHITE;
                        cell183.BackgroundColor = new iTextSharp.text.Color(219, 221, 204);
                        cell183.PaddingLeft = 5;
                        cell183.BorderWidthLeft = 0f;
                        cell183.BorderWidthTop = 0f;
                        cell183.BorderColorTop = iTextSharp.text.Color.WHITE;
                        table.AddCell(cell183);

                        object sumObject;
                        sumObject = dtpayout.Compute("Sum(poAmount)", "");

                        PdfPCell cell282 = new PdfPCell(new iTextSharp.text.Phrase("$" + (grandTotal + (Math.Round(Convert.ToDouble(sumObject.ToString()), 2, MidpointRounding.AwayFromZero))), OrderNumberRight));
                        cell282.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
                        cell282.BorderColorLeft = iTextSharp.text.Color.WHITE;
                        cell282.PaddingLeft = 5;
                        cell282.BackgroundColor = new iTextSharp.text.Color(219, 221, 204);
                        cell282.BorderWidthLeft = 0f;
                        cell282.BorderWidthTop = 0f;
                        cell282.BorderColorTop = iTextSharp.text.Color.WHITE;
                        table.AddCell(cell282);
                    }
                    else
                    {
                        OrderNumberRight = new iTextSharp.text.Font(iTextSharp.text.Font.GetFamilyIndex("Book Antiqua"), 8, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.BLACK);
                        PdfPCell cell182 = new PdfPCell(new iTextSharp.text.Phrase(" ", OrderNumberRight));
                        cell182.PaddingLeft = 5;
                        cell182.BorderWidthTop = 0f;
                        cell182.BackgroundColor = new iTextSharp.text.Color(219, 221, 204);
                        cell182.BorderColorTop = iTextSharp.text.Color.WHITE;
                        cell182.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
                        table.AddCell(cell182);

                        PdfPCell cell082 = new PdfPCell(new iTextSharp.text.Phrase(" ", OrderNumberRight));
                        cell082.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
                        cell082.BorderColorLeft = iTextSharp.text.Color.WHITE;
                        cell082.PaddingLeft = 5;
                        cell082.BackgroundColor = new iTextSharp.text.Color(219, 221, 204);
                        cell082.BorderWidthLeft = 0f;
                        cell082.BorderWidthTop = 0f;
                        cell082.BorderColorTop = iTextSharp.text.Color.WHITE;
                        table.AddCell(cell082);

                        PdfPCell cell184 = new PdfPCell(new iTextSharp.text.Phrase("Payout Balance", OrderNumberRight));
                        cell184.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
                        cell184.BorderColorLeft = iTextSharp.text.Color.WHITE;
                        cell184.PaddingLeft = 5;
                        cell184.BackgroundColor = new iTextSharp.text.Color(219, 221, 204);
                        cell184.BorderWidthLeft = 0f;
                        cell184.BorderWidthTop = 0f;
                        cell184.BorderColorTop = iTextSharp.text.Color.WHITE;
                        table.AddCell(cell184);


                        PdfPCell cell282 = new PdfPCell(new iTextSharp.text.Phrase("$" + grandTotal.ToString(), OrderNumberRight));
                        cell282.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
                        cell282.BorderColorLeft = iTextSharp.text.Color.WHITE;
                        cell282.PaddingLeft = 5;
                        cell282.BackgroundColor = new iTextSharp.text.Color(219, 221, 204);
                        cell282.BorderWidthLeft = 0f;
                        cell282.BorderWidthTop = 0f;
                        cell282.BorderColorTop = iTextSharp.text.Color.WHITE;
                        table.AddCell(cell282);

                    }


                    doc.Add(table);
                    doc.Close();
                }
                catch (Exception ex)
                { }
                return true;
            }
        }
        catch (Exception ex)
        {
            return false;
        }
        return true;
    }

    protected void btnSave_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            if (ViewState["restaurantId"] != null && Request.QueryString["did"] != null
                && ViewState["restaurantId"].ToString().Trim() != "" && Request.QueryString["did"].ToString().Trim() != ""
                && createPDFForInvoice(Convert.ToInt64(Request.QueryString["did"].ToString().Trim())))
            {

                string FilePath = AppDomain.CurrentDomain.BaseDirectory + "Admin\\Images\\Invoice\\" + Request.QueryString["did"].ToString().Trim() + "_" + ViewState["restaurantId"].ToString().Trim() + ".pdf";
                try
                {
                    string contentType = "";
                    //Get the physical path to the file.
                    // string FilePath = AppDomain.CurrentDomain.BaseDirectory + "Images\\ClientData\\" + objEnc.DecryptData("deatailOrder", e.CommandArgument.ToString()) + ".pdf";

                    //Set the appropriate ContentType.
                    contentType = "Application/pdf";

                    //Set the appropriate ContentType.

                    Response.ContentType = contentType;
                    Response.AppendHeader("content-disposition", "attachment; filename=" + (new FileInfo("Invoice.pdf")).Name);

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
        { }
    }

}
