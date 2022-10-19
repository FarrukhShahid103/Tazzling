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
using SQLHelper;

public partial class admin_invoice : System.Web.UI.Page
{


    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["pdfdownload"] != null && Request.QueryString["pdfdownload"].Trim().ToLower() == "true"
                    && Request.QueryString["did"] != null && Request.QueryString["did"].ToString().Trim() != ""
                        && Request.QueryString["myname"] != null && Request.QueryString["myname"].ToString().Trim() != ""
                        && Request.QueryString["myname"].ToString().Trim() == "colinazam"
                         && Request.QueryString["invoicetype"] != null && Request.QueryString["invoicetype"].ToString().Trim() != "")
                {
                    renderFirstDealInvoice();
                }
            }
        }
        catch (Exception ex)
        {

        }
    }

    protected void renderFirstDealInvoice()
    {
        try
        {
            bool paymentDone = false;
            DateTime dtFirstPaymentDate = DateTime.Now;
            DateTime dtPaymentDate = DateTime.Now;
            string strQueryForExisting = "";
            string strQuery = "";
            DataTable dtFirstInvoiceResult = null;
            DataTable dtResult = null;
            lblInvoiceDate.Text = DateTime.Now.ToString("yyyy/MM/dd");
            if (Request.QueryString["invoicetype"] != null && Request.QueryString["invoicetype"].ToLower().Trim() == "1st")
            {
                strQueryForExisting = "Select * from businessInvoiceInfo where dealID='" + Request.QueryString["did"].Trim() + "' and invoiceType='" + Request.QueryString["invoicetype"].Trim() + "'";
                dtResult = Misc.search(strQueryForExisting);
                if (dtResult != null && dtResult.Rows.Count > 0
                    && Convert.ToBoolean(dtResult.Rows[0]["paymentDone"].ToString().Trim()))
                {
                    paymentDone = true;
                    dtPaymentDate = Convert.ToDateTime(dtResult.Rows[0]["paymentdate"].ToString().Trim());
                }

                strQuery = "Select [deals].[dealId] ,[deals].restaurantId ,restaurantBusinessName";
                strQuery += ",restaurantAddress,phone,businessPaymentTitle,restaurantpaymentAddress,cellNumber ";
                strQuery += ",salePersonAccountName,OurCommission,shippingAndTax,shippingAndTaxAmount";
                strQuery += ",isnull((SELECT sum(qty) FROM  [dealOrders] where [dealOrders].dealId= [deals].dealId),0) 'TotalOrders'";
                // strQuery += ",isnull((SELECT sum(qty) FROM  [dealOrders] where [dealOrders].dealId= [deals].dealId  and dealOrders.status='Successful'),0) 'SuccessfulOrders'";
                if (paymentDone)
                {
                    strQuery += ",isnull((SELECT sum(qty) FROM  [dealOrders] where [dealOrders].dealId= [deals].dealId  and dealOrders.status<>'Successful' and dealOrders.modifiedDate<=CONVERT(DATETIME,'" + dtPaymentDate + "')),0) 'CancelOrders'";
                }
                else
                {
                    strQuery += ",isnull((SELECT sum(qty) FROM  [dealOrders] where [dealOrders].dealId= [deals].dealId  and dealOrders.status<>'Successful'),0) 'CancelOrders'";
                }
                strQuery += ",[title],[sellingPrice],[valuePrice],dealStartTime,dealEndTime";
                strQuery += " from deals";
                strQuery += " inner join restaurant on restaurant.restaurantId=deals.restaurantId";
                strQuery += " where ([deals].[dealId] = " + Request.QueryString["did"].Trim() + " or [deals].parentdealId = " + Request.QueryString["did"].Trim() + ") Order by [deals].[dealId]";              
            }
            else if (Request.QueryString["invoicetype"] != null && Request.QueryString["invoicetype"].ToLower().Trim() == "2nd")
            {
                strQueryForExisting = "Select * from businessInvoiceInfo where dealID='" + Request.QueryString["did"].Trim() + "' and invoiceType='1st'";
                dtFirstInvoiceResult = Misc.search(strQueryForExisting);
                if (dtFirstInvoiceResult != null && dtFirstInvoiceResult.Rows.Count > 0
                    && Convert.ToBoolean(dtFirstInvoiceResult.Rows[0]["paymentDone"].ToString().Trim()))
                {
                    dtFirstPaymentDate = Convert.ToDateTime(dtFirstInvoiceResult.Rows[0]["paymentdate"].ToString().Trim());
                }
                else
                {
                    Response.Redirect("dealInvoice.aspx");
                }
                strQueryForExisting = "Select * from businessInvoiceInfo where dealID='" + Request.QueryString["did"].Trim() + "' and invoiceType='2nd'";
                dtResult = Misc.search(strQueryForExisting);
                if (dtResult != null && dtResult.Rows.Count > 0
                    && Convert.ToBoolean(dtResult.Rows[0]["paymentDone"].ToString().Trim()))
                {
                    paymentDone = true;
                    dtPaymentDate = Convert.ToDateTime(dtResult.Rows[0]["paymentdate"].ToString().Trim());
                }

                strQuery = "Select [deals].[dealId],deals.dealNote,[deals].restaurantId ,restaurantBusinessName";
                strQuery += ",restaurantAddress,phone,businessPaymentTitle,restaurantpaymentAddress,cellNumber,dealpayment ";
                strQuery += ",salePersonAccountName,OurCommission,shippingAndTax,shippingAndTaxAmount";
                strQuery += ",isnull((SELECT sum(qty) FROM  [dealOrders] where [dealOrders].dealId= [deals].dealId),0) 'TotalOrders'";
                //strQuery += ",isnull((SELECT sum(qty) FROM  [dealOrders] where [dealOrders].dealId= [deals].dealId  and dealOrders.status='Successful'),0) 'SuccessfulOrders'";
                if (paymentDone)
                {
                    strQuery += ",isnull((SELECT sum(qty) FROM  [dealOrders] where [dealOrders].dealId= [deals].dealId  and dealOrders.status<>'Successful' and dealOrders.modifiedDate between '" + dtFirstPaymentDate + "' and '" + dtPaymentDate + "'),0) 'CancelOrders'";
                }
                else
                {
                    strQuery += ",isnull((SELECT sum(qty) FROM  [dealOrders] where [dealOrders].dealId= [deals].dealId  and dealOrders.status<>'Successful' and dealOrders.modifiedDate>CONVERT(DATETIME,'" + dtFirstPaymentDate + "')),0) 'CancelOrders'";
                }
                strQuery += ",[title],[sellingPrice],[valuePrice],dealStartTime,dealEndTime";
                strQuery += " from deals";
                strQuery += " inner join restaurant on restaurant.restaurantId=deals.restaurantId";
                strQuery += " where ([deals].[dealId] = " + Request.QueryString["did"].Trim() + " or [deals].parentdealId = " + Request.QueryString["did"].Trim() + ") Order by [deals].[dealId]";
            }
            else if (Request.QueryString["invoicetype"] != null && Request.QueryString["invoicetype"].ToLower().Trim() == "3rd")
            {
                strQueryForExisting = "Select * from businessInvoiceInfo where dealID='" + Request.QueryString["did"].Trim() + "' and invoiceType='2nd'";
                dtFirstInvoiceResult = Misc.search(strQueryForExisting);
                if (dtFirstInvoiceResult != null && dtFirstInvoiceResult.Rows.Count > 0
                    && Convert.ToBoolean(dtFirstInvoiceResult.Rows[0]["paymentDone"].ToString().Trim()))
                {
                    dtFirstPaymentDate = Convert.ToDateTime(dtFirstInvoiceResult.Rows[0]["paymentdate"].ToString().Trim());
                }
                else
                {
                    Response.Redirect("dealInvoice.aspx");
                }
                strQueryForExisting = "Select * from businessInvoiceInfo where dealID='" + Request.QueryString["did"].Trim() + "' and invoiceType='3rd'";
                dtResult = Misc.search(strQueryForExisting);
                if (dtResult != null && dtResult.Rows.Count > 0
                    && Convert.ToBoolean(dtResult.Rows[0]["paymentDone"].ToString().Trim()))
                {
                    paymentDone = true;
                    dtPaymentDate = Convert.ToDateTime(dtResult.Rows[0]["paymentdate"].ToString().Trim());
                }
                strQuery = "Select [deals].[dealId],deals.dealNote,[deals].restaurantId ,restaurantBusinessName";
                strQuery += ",restaurantAddress,phone,businessPaymentTitle,restaurantpaymentAddress,cellNumber,dealpayment ";
                strQuery += ",salePersonAccountName,OurCommission,shippingAndTax,shippingAndTaxAmount";
                strQuery += ",isnull((SELECT sum(qty) FROM  [dealOrders] where [dealOrders].dealId= [deals].dealId),0) 'TotalOrders'";
                //strQuery += ",isnull((SELECT sum(qty) FROM  [dealOrders] where [dealOrders].dealId= [deals].dealId  and dealOrders.status='Successful'),0) 'SuccessfulOrders'";
                if (paymentDone)
                {
                    strQuery += ",isnull((SELECT sum(qty) FROM  [dealOrders] where [dealOrders].dealId= [deals].dealId  and dealOrders.status<>'Successful' and dealOrders.modifiedDate between '" + dtFirstPaymentDate + "' and '" + dtPaymentDate + "'),0) 'CancelOrders'";
                }
                else
                {
                    strQuery += ",isnull((SELECT sum(qty) FROM  [dealOrders] where [dealOrders].dealId= [deals].dealId  and dealOrders.status<>'Successful' and dealOrders.modifiedDate>CONVERT(DATETIME,'" + dtFirstPaymentDate + "')),0) 'CancelOrders'";
                }
                strQuery += ",[title],[sellingPrice],[valuePrice],dealStartTime,dealEndTime";
                strQuery += " from deals";
                strQuery += " inner join restaurant on restaurant.restaurantId=deals.restaurantId";
                strQuery += " where ([deals].[dealId] = " + Request.QueryString["did"].Trim() + " or [deals].parentdealId = " + Request.QueryString["did"].Trim() + ") Order by [deals].[dealId]";
            }

           



           
            DataTable dtDealDetail = Misc.search(strQuery);
            double dDealAmountTotal = 0;
            double dTotalCancelOrders = 0;
            int i = 0;

           /* string strQuery = "Select [deals].[dealId] ,[deals].restaurantId ,restaurantBusinessName";
            strQuery += ",restaurantAddress,phone,businessPaymentTitle,restaurantpaymentAddress,cellNumber ";
            strQuery += ",salePersonAccountName,OurCommission,shippingAndTax,shippingAndTaxAmount";
            strQuery += ",isnull((SELECT sum(qty) FROM  [dealOrders] where [dealOrders].dealId= [deals].dealId),0) 'TotalOrders'";
            strQuery += ",isnull((SELECT sum(qty) FROM  [dealOrders] where [dealOrders].dealId= [deals].dealId  and dealOrders.status='Successful'),0) 'SuccessfulOrders'";
            strQuery += ",isnull((SELECT sum(qty) FROM  [dealOrders] where [dealOrders].dealId= [deals].dealId  and dealOrders.status<>'Successful'),0) 'CancelOrders'";
            strQuery += ",[title],[sellingPrice],[valuePrice],dealStartTime,dealEndTime";
            strQuery += " from deals";
            strQuery += " inner join restaurant on restaurant.restaurantId=deals.restaurantId";
            strQuery += " where ([deals].[dealId] = " + Request.QueryString["did"].Trim() + " or [deals].parentdealId = " + Request.QueryString["did"].Trim() + ") Order by [deals].[dealId]";
            DataTable dtDealDetail = Misc.search(strQuery);
            double dTotalCancelOrders = 0;
            double dDealAmountTotal = 0;
            int i = 0;*/
            if (dtDealDetail != null && dtDealDetail.Rows.Count > 0)
            {
                lblBusinessAddress.Text = dtDealDetail.Rows[0]["restaurantAddress"].ToString().Trim();
                lblBusinessName.Text = dtDealDetail.Rows[0]["restaurantBusinessName"].ToString().Trim();

                lblBusinessPaymentTitle.Text = dtDealDetail.Rows[0]["businessPaymentTitle"].ToString().Trim();
                lblBusinessPhone.Text = dtDealDetail.Rows[0]["phone"].ToString().Trim();

                if (Request.QueryString["invoicetype"] != null && Request.QueryString["invoicetype"].ToLower().Trim() == "1st")
                {
                    lblInvoiceNumber.Text = "INVOICE #" + (1800 + Convert.ToInt32(dtDealDetail.Rows[0]["dealId"].ToString().Trim())).ToString() + "-1";
                }
                else if (Request.QueryString["invoicetype"] != null && Request.QueryString["invoicetype"].ToLower().Trim() == "2nd")
                {
                    lblInvoiceNumber.Text = "INVOICE #" + (1800 + Convert.ToInt32(dtDealDetail.Rows[0]["dealId"].ToString().Trim())).ToString() + "-2";
                }
                else if (Request.QueryString["invoicetype"] != null && Request.QueryString["invoicetype"].ToLower().Trim() == "3rd")
                {
                    lblInvoiceNumber.Text = "INVOICE #" + (1800 + Convert.ToInt32(dtDealDetail.Rows[0]["dealId"].ToString().Trim())).ToString() + "-3";
                }
                else
                {
                    lblInvoiceNumber.Text = "INVOICE #" + (1800 + Convert.ToInt32(dtDealDetail.Rows[0]["dealId"].ToString().Trim())).ToString() + "-1";
                }
            }
            for (i = 0; i < dtDealDetail.Rows.Count; i++)
            {
                double tempRefunded = 0;
                double tempccTransactionFee = 0;
                if (Convert.ToDouble(dtDealDetail.Rows[i]["CancelOrders"].ToString().Trim()) > 0)
                {
                    tempRefunded = Math.Round((Convert.ToDouble(dtDealDetail.Rows[i]["sellingPrice"].ToString().Trim()) * Convert.ToDouble(dtDealDetail.Rows[i]["CancelOrders"].ToString().Trim())), 2, MidpointRounding.AwayFromZero);
                    double tempAdveriseFee = Math.Round((Convert.ToDouble(dtDealDetail.Rows[i]["OurCommission"].ToString().Trim()) / 100) * tempRefunded, 2, MidpointRounding.AwayFromZero);

                    if (dtDealDetail.Rows[i]["shippingAndTax"] != null && Convert.ToBoolean(dtDealDetail.Rows[i]["shippingAndTax"].ToString().Trim()))
                    {
                        tempRefunded = (Convert.ToDouble(dtDealDetail.Rows[i]["sellingPrice"].ToString().Trim()) + Convert.ToDouble(dtDealDetail.Rows[i]["shippingAndTaxAmount"].ToString().Trim())) * Convert.ToDouble(dtDealDetail.Rows[i]["CancelOrders"].ToString().Trim());
                        tempccTransactionFee = Math.Round((3.9 / 100) * tempRefunded, 2, MidpointRounding.AwayFromZero);
                    }
                    else
                    {
                        tempccTransactionFee = Math.Round((3.9 / 100) * tempRefunded, 2, MidpointRounding.AwayFromZero);
                    }
                    double tempTax = Math.Round((12.00 / 100) * tempAdveriseFee, 2, MidpointRounding.AwayFromZero);
                    dTotalCancelOrders += Math.Round(((tempAdveriseFee + tempTax - tempccTransactionFee) - (tempRefunded)), 2, MidpointRounding.AwayFromZero);
                }
                double dDealTotal = Math.Round(Convert.ToDouble(dtDealDetail.Rows[i]["TotalOrders"].ToString().Trim()) * Convert.ToDouble(dtDealDetail.Rows[i]["sellingPrice"].ToString().Trim()), 2, MidpointRounding.AwayFromZero);
                dDealAmountTotal += dDealTotal;
            }

            double dTotalOurComission = Math.Round((Convert.ToDouble(dtDealDetail.Rows[0]["OurCommission"].ToString().Trim()) / 100) * dDealAmountTotal, 2, MidpointRounding.AwayFromZero);
            double dTotalShippingFee = 0;
            double dTotalCreditCardFee = 0;
            double dTotalCommionPlusCreditCardFee = 0;
            double dTotalSalesTax = 0;
            double dTotalCommionPlusCreditCardFeePlusTax = 0;
            double dTotalPayoutToBusiness = 0;
            double dTotalAdjustment = 0;
            i = 0;
            if (dtDealDetail.Rows[0]["shippingAndTax"] != null && Convert.ToBoolean(dtDealDetail.Rows[0]["shippingAndTax"].ToString().Trim()))
            {
                i = 0;
                for (i = 0; i < dtDealDetail.Rows.Count; i++)
                {
                    //Shipping and Tax fee Calculation and show
                    double dTempShippingFee = Math.Round(Convert.ToDouble(dtDealDetail.Rows[i]["shippingAndTaxAmount"].ToString().Trim()) * Convert.ToDouble(dtDealDetail.Rows[i]["TotalOrders"].ToString().Trim()), 2, MidpointRounding.AwayFromZero);
                    dTotalShippingFee += dTempShippingFee;
                }
            }
            dTotalCreditCardFee = Math.Round((3.9 / 100) * (dDealAmountTotal + dTotalShippingFee), 2, MidpointRounding.AwayFromZero);
            dTotalCommionPlusCreditCardFee = Math.Round(dTotalOurComission + dTotalCreditCardFee, 2, MidpointRounding.AwayFromZero);
            dTotalSalesTax = Math.Round((12.0 / 100) * dTotalCommionPlusCreditCardFee, 2, MidpointRounding.AwayFromZero);
            dTotalCommionPlusCreditCardFeePlusTax = Math.Round(dTotalCommionPlusCreditCardFee + dTotalSalesTax, 2, MidpointRounding.AwayFromZero);

            dTotalCancelOrders = dTotalCancelOrders * (-1);
            dTotalPayoutToBusiness = Math.Round(dDealAmountTotal + dTotalShippingFee - dTotalCommionPlusCreditCardFeePlusTax - dTotalCancelOrders, 2, MidpointRounding.AwayFromZero);

            // Adjustments 
            BLLPayOut objPayout = new BLLPayOut();
            objPayout.dealId = Convert.ToInt64(Request.QueryString["did"].Trim());
            DataTable dtpayout = objPayout.getPayOutByDealID();
            if (dtpayout != null && dtpayout.Rows.Count > 0)
            {
                for (i = 0; i < dtpayout.Rows.Count; i++)
                {
                    dTotalAdjustment += Math.Round(Convert.ToDouble(dtpayout.Rows[i]["poAmount"].ToString().Trim()), 2, MidpointRounding.AwayFromZero);
                }
                dTotalPayoutToBusiness = dTotalPayoutToBusiness + dTotalAdjustment;
            }

            lblFirstPayment.Text = "10 Business Days After Deal Ends \t\t";
            lblFirstPayment.Text += Math.Round((30.0 / 100) * dTotalPayoutToBusiness, 2, MidpointRounding.AwayFromZero).ToString();


            //SecondPayment            
            lblSecondPayment.Text = "50 Business Days After Deal Ends \t\t";
            lblSecondPayment.Text += Math.Round((50.0 / 100) * dTotalPayoutToBusiness, 2, MidpointRounding.AwayFromZero).ToString();

            //Third Payment Schedule            
            lblThirdPayment.Text = "90 Business Days After Deal Ends\t\t";
            lblThirdPayment.Text += Math.Round((20.0 / 100) * dTotalPayoutToBusiness, 2, MidpointRounding.AwayFromZero).ToString();

            //string strQueryForExisting = "Select * from businessInvoiceInfo where dealID='" + Request.QueryString["did"].Trim() + "' and invoiceType='" + Request.QueryString["invoicetype"].Trim() + "'";
            //DataTable dtResult = Misc.search(strQueryForExisting);
            if (dtResult != null && dtResult.Rows.Count > 0)
            {
                lblInvoiceDate.Text=  Convert.ToDateTime(dtResult.Rows[0]["paymentdate"].ToString().Trim()).ToString("yyyy/MM/dd");
                lblBusinessAddress.Text = dtResult.Rows[0]["cAddress"].ToString().Trim();
                lblBusinessName.Text = dtResult.Rows[0]["cName"].ToString().Trim();
                lblBusinessPaymentTitle.Text = dtResult.Rows[0]["cName"].ToString().Trim();
                lblBusinessPhone.Text = dtResult.Rows[0]["cPhone"].ToString().Trim();

                lblInvoiceTopAmount.Text = dtResult.Rows[0]["paymentSend"].ToString().Trim();
                double dAmount = 0;
                double.TryParse(dtResult.Rows[0]["paymentSend"].ToString().Trim(), out dAmount);
                lblAmountInWords.Text = changeCurrencyToWords(dAmount).ToUpper();

                if (Request.QueryString["invoicetype"] != null && Request.QueryString["invoicetype"].ToString().Trim() == "1st")
                {

                    lblFirstPayment.Text = "<div><div style='float:left; width:350px;'>" + dtResult.Rows[0]["firstPaymentText"].ToString().Trim() + "</div><div style='float:left; width:350px;'>" + dtResult.Rows[0]["firstPer"].ToString().Trim() + "% First Payment</div><div style='float:left; width:200px;'>$" + Math.Round((Convert.ToDouble(dtResult.Rows[0]["firstPer"].ToString().Trim()) / 100) * dTotalPayoutToBusiness, 2, MidpointRounding.AwayFromZero).ToString() + "</div></div>";
                    lblSecondPayment.Text = "<div><div style='float:left; width:350px;'>" + dtResult.Rows[0]["secondPaymentText"].ToString().Trim() + "</div><div style='float:left; width:350px;'>" + dtResult.Rows[0]["secondPer"].ToString().Trim() + "% Second Payment</div><div style='float:left; width:200px;'>$" + Math.Round((Convert.ToDouble(dtResult.Rows[0]["secondPer"].ToString().Trim()) / 100) * dTotalPayoutToBusiness, 2, MidpointRounding.AwayFromZero).ToString() + "</div></div>";
                    lblThirdPayment.Text = "<div><div style='float:left; width:350px;'>" + dtResult.Rows[0]["thirdPaymenText"].ToString().Trim() + "</div><div style='float:left; width:350px;'>" + dtResult.Rows[0]["thirdPer"].ToString().Trim() + "% Third Payment</div><div style='float:left; width:200px;'>$" + Math.Round((Convert.ToDouble(dtResult.Rows[0]["thirdPer"].ToString().Trim()) / 100) * dTotalPayoutToBusiness, 2, MidpointRounding.AwayFromZero).ToString() + "</div></div>";
                }
                else if (Request.QueryString["invoicetype"] != null && Request.QueryString["invoicetype"].ToString().Trim() == "2nd")
                {
                    double dTempTotalAmount = Math.Round(Convert.ToDouble(dtResult.Rows[0]["paymentAmount"].ToString().Trim()) * (100 / Convert.ToDouble(dtResult.Rows[0]["secondPer"].ToString().Trim())), 2, MidpointRounding.AwayFromZero);
                    //lblInvoiceTopAmount.Text = dtResult.Rows[0]["paymentSend"].ToString().Trim();
                    lblFirstPayment.Text = "<div><div style='float:left; width:350px;'>" + dtResult.Rows[0]["firstPaymentText"].ToString().Trim() + "</div><div style='float:left; width:350px;'>" + dtResult.Rows[0]["firstPer"].ToString().Trim() + "% First Payment</div><div style='float:left; width:200px;'>$" + Math.Round((Convert.ToDouble(dtResult.Rows[0]["firstPer"].ToString().Trim()) / 100) * dTempTotalAmount, 2, MidpointRounding.AwayFromZero).ToString() + "</div></div>";
                    if (dtResult.Rows[0]["paymentSend"] != null && dtResult.Rows[0]["paymentSend"].ToString().Trim() != "")
                    {
                        lblSecondPayment.Text = "<div><div style='float:left; width:350px;'>" + dtResult.Rows[0]["secondPaymentText"].ToString().Trim() + "</div><div style='float:left; width:350px;'>" + dtResult.Rows[0]["secondPer"].ToString().Trim() + "% Second Payment</div><div style='float:left; width:200px;'>$" + dtResult.Rows[0]["paymentSend"].ToString().Trim() + "</div></div>";
                    }
                    else
                    {
                        lblSecondPayment.Text = "<div><div style='float:left; width:350px;'>" + dtResult.Rows[0]["secondPaymentText"].ToString().Trim() + "</div><div style='float:left; width:350px;'>" + dtResult.Rows[0]["secondPer"].ToString().Trim() + "% Second Payment</div><div style='float:left; width:200px;'>$" + Math.Round((Convert.ToDouble(dtResult.Rows[0]["secondPer"].ToString().Trim()) / 100) * dTempTotalAmount, 2, MidpointRounding.AwayFromZero).ToString() + "</div></div>";
                    }

                    lblThirdPayment.Text = "<div><div style='float:left; width:350px;'>" + dtResult.Rows[0]["thirdPaymenText"].ToString().Trim() + "</div><div style='float:left; width:350px;'>" + dtResult.Rows[0]["thirdPer"].ToString().Trim() + "% Third Payment</div><div style='float:left; width:200px;'>$" + Math.Round((Convert.ToDouble(dtResult.Rows[0]["thirdPer"].ToString().Trim()) / 100) * dTempTotalAmount, 2, MidpointRounding.AwayFromZero).ToString() + "</div></div>";
                }
                else if (Request.QueryString["invoicetype"] != null && Request.QueryString["invoicetype"].ToString().Trim() == "3rd")
                {
                    double dTempTotalAmount = Math.Round(Convert.ToDouble(dtResult.Rows[0]["paymentAmount"].ToString().Trim()) * (100 / Convert.ToDouble(dtResult.Rows[0]["thirdPer"].ToString().Trim())), 2, MidpointRounding.AwayFromZero);
                    //lblInvoiceTopAmount.Text = dtResult.Rows[0]["paymentSend"].ToString().Trim();
                    lblFirstPayment.Text = "<div><div style='float:left; width:350px;'>" + dtResult.Rows[0]["firstPaymentText"].ToString().Trim() + "</div><div style='float:left; width:350px;'>" + dtResult.Rows[0]["firstPer"].ToString().Trim() + "% First Payment</div><div style='float:left; width:200px;'>$" + Math.Round((Convert.ToDouble(dtResult.Rows[0]["firstPer"].ToString().Trim()) / 100) * dTempTotalAmount, 2, MidpointRounding.AwayFromZero).ToString() + "</div></div>";

                    lblSecondPayment.Text = "<div><div style='float:left; width:350px;'>" + dtResult.Rows[0]["secondPaymentText"].ToString().Trim() + "</div><div style='float:left; width:350px;'>" + dtResult.Rows[0]["secondPer"].ToString().Trim() + "% Second Payment</div><div style='float:left; width:200px;'>$" + Math.Round((Convert.ToDouble(dtResult.Rows[0]["secondPer"].ToString().Trim()) / 100) * dTempTotalAmount, 2, MidpointRounding.AwayFromZero).ToString() + "</div></div>";

                    if (dtResult.Rows[0]["paymentSend"] != null && dtResult.Rows[0]["paymentSend"].ToString().Trim() != "")
                    {
                        lblThirdPayment.Text = "<div><div style='float:left; width:350px;'>" + dtResult.Rows[0]["thirdPaymenText"].ToString().Trim() + "</div><div style='float:left; width:350px;'>" + dtResult.Rows[0]["thirdPer"].ToString().Trim() + "% Third Payment</div><div style='float:left; width:200px;'>$" + dtResult.Rows[0]["paymentSend"].ToString().Trim() + "</div></div>";
                    }
                    else
                    {
                        lblThirdPayment.Text = "<div><div style='float:left; width:350px;'>" + dtResult.Rows[0]["thirdPaymenText"].ToString().Trim() + "</div><div style='float:left; width:350px;'>" + dtResult.Rows[0]["thirdPer"].ToString().Trim() + "% Third Payment</div><div style='float:left; width:200px;'>$" + Math.Round((Convert.ToDouble(dtResult.Rows[0]["thirdPer"].ToString().Trim()) / 100) * dTempTotalAmount, 2, MidpointRounding.AwayFromZero).ToString() + "</div></div>";
                    }
                    dtResult = Misc.search("Select * from businessInvoiceInfo where dealID='" + Request.QueryString["did"].Trim() + "' and invoiceType='2nd'");
                    if (dtResult != null && dtResult.Rows.Count > 0)
                    {
                        lblSecondPayment.Text = "<div><div style='float:left; width:350px;'>" + dtResult.Rows[0]["secondPaymentText"].ToString().Trim() + "</div><div style='float:left; width:350px;'>" + dtResult.Rows[0]["secondPer"].ToString().Trim() + "% Second Payment</div><div style='float:left; width:200px;'>$" + dtResult.Rows[0]["paymentSend"].ToString().Trim() + "</div></div>";
                    }
                }
            }

            if (Request.QueryString["invoicetype"] != null && Request.QueryString["invoicetype"].ToString().Trim() == "1st")
            {
                lblIPaymentType.Text = "Tastygo first payment";
            }
            else if (Request.QueryString["invoicetype"] != null && Request.QueryString["invoicetype"].ToString().Trim() == "2nd")
            {
                lblIPaymentType.Text = "Tastygo second payment";
            }
            else if (Request.QueryString["invoicetype"] != null && Request.QueryString["invoicetype"].ToString().Trim() == "3rd")
            {
                lblIPaymentType.Text = "Tastygo third payment";
            }
        }
        catch (Exception ex)
        { }
    }

    #region Convert
    public static string NumberToWords(int number)
    {
        if (number == 0)
            return "zero";

        if (number < 0)
            return "minus " + NumberToWords(Math.Abs(number));

        string words = "";

        if ((number / 1000000) > 0)
        {
            words += NumberToWords(number / 1000000) + " million ";
            number %= 1000000;
        }

        if ((number / 1000) > 0)
        {
            words += NumberToWords(number / 1000) + " thousand ";
            number %= 1000;
        }

        if ((number / 100) > 0)
        {
            words += NumberToWords(number / 100) + " hundred ";
            number %= 100;
        }

        if (number > 0)
        {
            if (words != "")
                words += "and ";

            var unitsMap = new[] { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "ten", "eleven", "twelve", "thirteen", "fourteen", "fifteen", "sixteen", "seventeen", "eighteen", "nineteen" };
            var tensMap = new[] { "zero", "ten", "twenty", "thirty", "forty", "fifty", "sixty", "seventy", "eighty", "ninety" };

            if (number < 20)
                words += unitsMap[number];
            else
            {
                words += tensMap[number / 10];
                if ((number % 10) > 0)
                    words += "-" + unitsMap[number % 10];
            }
        }

        return words;
    }
    #endregion

    #region Convert Decimal To Word Code
    public String changeNumericToWords(double numb)
    {
        String num = numb.ToString();
        return changeToWords(num, false);
    }
    public String changeCurrencyToWords(String numb)
    {
        return changeToWords(numb, true);
    }
    public String changeNumericToWords(String numb)
    {
        return changeToWords(numb, false);
    }
    public String changeCurrencyToWords(double numb)
    {
        return changeToWords(numb.ToString(), true);
    }
    private String changeToWords(String numb, bool isCurrency)
    {
        String val = "", wholeNo = numb, points = "", andStr = "", pointStr = "";
        String endStr = (isCurrency) ? ("") : ("");
        try
        {
            int decimalPlace = numb.IndexOf(".");
            if (decimalPlace > 0)
            {
                wholeNo = numb.Substring(0, decimalPlace);
                points = numb.Substring(decimalPlace + 1);
                //if (Convert.ToInt32(points) > 0)
                //{
                //    andStr = (isCurrency) ? ("and ") : ("point");// just to separate whole numbers from points/cents
                //    endStr = (isCurrency) ? ("Cents " + endStr) : ("");
                //    pointStr = translateWholeNumber(points);
                //}
            }
            val = String.Format("{0} {1}{2} {3}", translateWholeNumber(wholeNo).Trim(), andStr, pointStr, endStr);
        }
        catch { ;}
        return val;
    }
    private String translateWholeNumber(String number)
    {
        string word = "";
        try
        {
            bool beginsZero = false;//tests for 0XX
            bool isDone = false;//test if already translated
            double dblAmt = (Convert.ToDouble(number));
            //if ((dblAmt > 0) && number.StartsWith("0"))
            if (dblAmt > 0)
            {//test for zero or digit zero in a nuemric
                beginsZero = number.StartsWith("0");
                int numDigits = number.Length;
                int pos = 0;//store digit grouping
                String place = "";//digit grouping name:hundres,thousand,etc...
                switch (numDigits)
                {
                    case 1://ones' range
                        word = ones(number);
                        isDone = true;
                        break;
                    case 2://tens' range
                        word = tens(number);
                        isDone = true;
                        break;
                    case 3://hundreds' range
                        pos = (numDigits % 3) + 1;
                        place = " Hundred ";
                        break;
                    case 4://thousands' range
                    case 5:
                    case 6:
                        pos = (numDigits % 4) + 1;
                        place = " Thousand ";
                        break;
                    case 7://millions' range
                    case 8:
                    case 9:
                        pos = (numDigits % 7) + 1;
                        place = " Million ";
                        break;
                    case 10://Billions's range
                        pos = (numDigits % 10) + 1;
                        place = " Billion ";
                        break;
                    //add extra case options for anything above Billion...
                    default:
                        isDone = true;
                        break;
                }
                if (!isDone)
                {//if transalation is not done, continue...(Recursion comes in now!!)
                    word = translateWholeNumber(number.Substring(0, pos)) + place + translateWholeNumber(number.Substring(pos));
                    //check for trailing zeros
                    if (beginsZero) word = " and " + word.Trim();
                }
                //ignore digit grouping names
                if (word.Trim().Equals(place.Trim())) word = "";
            }
        }
        catch { ;}
        return word.Trim();
    }
    private String tens(String digit)
    {
        int digt = Convert.ToInt32(digit);
        String name = null;
        switch (digt)
        {
            case 10:
                name = "Ten";
                break;
            case 11:
                name = "Eleven";
                break;
            case 12:
                name = "Twelve";
                break;
            case 13:
                name = "Thirteen";
                break;
            case 14:
                name = "Fourteen";
                break;
            case 15:
                name = "Fifteen";
                break;
            case 16:
                name = "Sixteen";
                break;
            case 17:
                name = "Seventeen";
                break;
            case 18:
                name = "Eighteen";
                break;
            case 19:
                name = "Nineteen";
                break;
            case 20:
                name = "Twenty";
                break;
            case 30:
                name = "Thirty";
                break;
            case 40:
                name = "Fourty";
                break;
            case 50:
                name = "Fifty";
                break;
            case 60:
                name = "Sixty";
                break;
            case 70:
                name = "Seventy";
                break;
            case 80:
                name = "Eighty";
                break;
            case 90:
                name = "Ninety";
                break;
            default:
                if (digt > 0)
                {
                    name = tens(digit.Substring(0, 1) + "0") + " " + ones(digit.Substring(1));
                }
                break;
        }
        return name;
    }
    private String ones(String digit)
    {
        int digt = Convert.ToInt32(digit);
        String name = "";
        switch (digt)
        {
            case 1:
                name = "One";
                break;
            case 2:
                name = "Two";
                break;
            case 3:
                name = "Three";
                break;
            case 4:
                name = "Four";
                break;
            case 5:
                name = "Five";
                break;
            case 6:
                name = "Six";
                break;
            case 7:
                name = "Seven";
                break;
            case 8:
                name = "Eight";
                break;
            case 9:
                name = "Nine";
                break;
        }
        return name;
    }
    private String translateCents(String cents)
    {
        String cts = "", digit = "", engOne = "";
        for (int i = 0; i < cents.Length; i++)
        {
            digit = cents[i].ToString();
            if (digit.Equals("0"))
            {
                engOne = "Zero";
            }
            else
            {
                engOne = ones(digit);
            }
            cts += " " + engOne;
        }
        return cts;
    }
    #endregion
}
