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

public partial class invoice2 : System.Web.UI.Page
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
        { }
    }

    protected void renderFirstDealInvoice()
    {
        try
        {

            bool paymentDone = false;
            DateTime dtFirstPaymentDate = DateTime.Now;
            DateTime dtPaymentDate = DateTime.Now;
            string strQueryForExisting = "Select * from businessInvoiceInfo where dealID='" + Request.QueryString["did"].Trim() + "' and invoiceType='1st'";
            DataTable dtFirstInvoiceResult = Misc.search(strQueryForExisting);
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
            DataTable dtResult = Misc.search(strQueryForExisting);
            if (dtResult != null && dtResult.Rows.Count > 0
                && Convert.ToBoolean(dtResult.Rows[0]["paymentDone"].ToString().Trim()))
            {              
                paymentDone = true;
                dtPaymentDate = Convert.ToDateTime(dtResult.Rows[0]["paymentdate"].ToString().Trim());
            }

            string strQuery = "Select [deals].[dealId],deals.dealNote,[deals].restaurantId ,restaurantBusinessName";
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
            DataTable dtDealDetail = Misc.search(strQuery);
            double dDealAmountTotal = 0;
            double dTotalCancelOrders = 0;
            int i = 0;
            if (dtDealDetail != null && dtDealDetail.Rows.Count > 0)
            {
                lblBusinessAddress.Text = dtDealDetail.Rows[0]["restaurantAddress"].ToString().Trim();
                lblBusinessName.Text = dtDealDetail.Rows[0]["restaurantBusinessName"].ToString().Trim();
                lblBusinessPaymentAddress.Text = dtDealDetail.Rows[0]["restaurantpaymentAddress"].ToString().Trim();
                lblBusinessPaymentPhone.Text = dtDealDetail.Rows[0]["cellNumber"].ToString().Trim();
                lblBusinessPaymentTitle.Text = dtDealDetail.Rows[0]["businessPaymentTitle"].ToString().Trim();
                lblBusinessPhone.Text = dtDealDetail.Rows[0]["phone"].ToString().Trim();              

                lblInvoiceDate.Text = "INVOICE DATE: " + DateTime.Now.ToString("yyyy/MM/dd");               
                lblInvoiceNumber.Text = "INVOICE #" + (1800 + Convert.ToInt32(dtDealDetail.Rows[0]["dealId"].ToString().Trim())).ToString() + "-2";
               
            }
            for (i = 0; i < dtDealDetail.Rows.Count; i++)
            {
                DateTime dtStartDate = DateTime.Now;
                DateTime dtEndDate = DateTime.Now;
                DateTime.TryParse(dtDealDetail.Rows[i]["dealStartTime"].ToString().Trim(), out dtStartDate);
                DateTime.TryParse(dtDealDetail.Rows[i]["dealEndTime"].ToString().Trim(), out dtEndDate);
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

                if (i % 2 == 0)
                {
                    try
                    {
                        //Code for the Deal Titles to show
                        ltDealsDetails.Text += "<div style='width: 100%; clear: both; font-size: 12px; font-weight: bold; background-color: #c0c0c0;min-height: 20px;overflow: hidden;'><div style='width: 650px; float: left; text-align: center;'>";
                        ltDealsDetails.Text += dtDealDetail.Rows[i]["title"].ToString().Trim();
                        ltDealsDetails.Text += "</div><div style='width: 250px; float: left; text-align: center;'>";
                        ltDealsDetails.Text += dtStartDate.ToString("yyyy/MM/dd") + " - " + dtEndDate.ToString("yyyy/MM/dd") + "</div></div>";

                       
                        double dDealTotal = Math.Round(Convert.ToDouble(dtDealDetail.Rows[i]["TotalOrders"].ToString().Trim()) * Convert.ToDouble(dtDealDetail.Rows[i]["sellingPrice"].ToString().Trim()), 2, MidpointRounding.AwayFromZero);
                        dDealAmountTotal += dDealTotal;
                    }
                    catch (Exception ex)
                    {

                    }
                }
                else
                {
                    try
                    {
                        //Code for the Deal Titles to show
                        ltDealsDetails.Text += "<div style='width: 100%; clear: both; font-size: 12px; font-weight: bold; min-height: 20px;overflow: hidden;'><div style='width: 650px; float: left; text-align: center;'>";
                        ltDealsDetails.Text += dtDealDetail.Rows[i]["title"].ToString().Trim();
                        ltDealsDetails.Text += "</div><div style='width: 250px; float: left; text-align: center;'>";
                        ltDealsDetails.Text += dtStartDate.ToString("yyyy/MM/dd") + " - " + dtEndDate.ToString("yyyy/MM/dd") + "</div></div>";

                        double dDealTotal = Math.Round(Convert.ToDouble(dtDealDetail.Rows[i]["TotalOrders"].ToString().Trim()) * Convert.ToDouble(dtDealDetail.Rows[i]["sellingPrice"].ToString().Trim()), 2, MidpointRounding.AwayFromZero);
                        dDealAmountTotal += dDealTotal;
                    }
                    catch (Exception ex)
                    { }
                }
            }
           
            // New Code
 //Tasty Comission Calculation and show
            ltServiceFeeDetail.Text += "<div style='width: 100%; clear: both; font-size: 12px; font-weight: bold; background-color: #c0c0c0;min-height: 20px;overflow: hidden;'><div style='width: 440px; float: left; text-align: left; padding-left: 10px;'>";
            ltServiceFeeDetail.Text += "Second Payment";
            ltServiceFeeDetail.Text += "</div><div style='width: 300px; float: left; text-align: left; font-weight: bold;'>$";
            ltServiceFeeDetail.Text += dtResult.Rows[0]["paymentAmount"].ToString().Trim();
            ltServiceFeeDetail.Text += "</div><div style='width: 150px; float: left; text-align: left; font-weight: normal;'>";
            double dSecondPayment = Math.Round((Convert.ToDouble(dtResult.Rows[0]["paymentAmount"].ToString().Trim())), 2, MidpointRounding.AwayFromZero);
            ltServiceFeeDetail.Text += "$" + dSecondPayment + "</div></div>";




            dTotalCancelOrders = dTotalCancelOrders * (-1);

                //Calculate Refund
                ltServiceFeeDetail.Text += "<div style='width: 100%; clear: both; font-size: 12px; font-weight: bold;min-height: 20px;overflow: hidden;'><div style='width: 440px; float: left; text-align: left; padding-left: 10px;'>";
                ltServiceFeeDetail.Text += "Refunds since " + DateTime.Now.ToString("yyyy/MM/dd");
                ltServiceFeeDetail.Text += "</div><div style='width: 300px; float: left; text-align: left; font-weight: bold;'>";
                ltServiceFeeDetail.Text += "$" + dTotalCancelOrders;
                ltServiceFeeDetail.Text += "</div><div style='width: 150px; float: left; text-align: left; font-weight: normal;'>";
                ltServiceFeeDetail.Text += "-$" + dTotalCancelOrders + "</div></div>";
                
                

                //SubTotal of Comission + Credit Card Fee
                ltServiceFeeDetail.Text += "<div style='width: 100%; clear: both; font-size: 12px; font-weight: bold; background-color: #c0c0c0;min-height: 20px;overflow: hidden;'><div style='width: 440px; float: left; text-align: left; padding-left: 10px;'>";
                ltServiceFeeDetail.Text += "Amount Forward to You";
                ltServiceFeeDetail.Text += "</div><div style='width: 300px; float: left; text-align: left; font-weight: bold;'>";
                ltServiceFeeDetail.Text += "$" + dSecondPayment + " - $" + dTotalCancelOrders;
                ltServiceFeeDetail.Text += "</div><div style='width: 150px; float: left; text-align: left; font-weight: normal;'>";
                double dTotalPayoutToBusiness = Math.Round(dSecondPayment - dTotalCancelOrders, 2, MidpointRounding.AwayFromZero);
                ltServiceFeeDetail.Text += "$" + dTotalPayoutToBusiness + "</div></div>";
               
                // Adjustments 

                DataTable dtpayout = null;

                if (paymentDone)
                {
                    dtpayout = Misc.search("SELECT * FROM payOut Where dealId = " + Request.QueryString["did"].Trim() + " and poDate between '" + dtFirstPaymentDate + "' and '" + dtPaymentDate + "' Order by poID asc");
                }
                else
                {
                    dtpayout = Misc.search("SELECT * FROM payOut Where dealId = " + Request.QueryString["did"].Trim() + " and poDate > CONVERT(DATETIME,'" + dtFirstPaymentDate + "') Order by poID asc");
                }                
                double dTotalAdjustment = 0;

               
                
                if (dtpayout != null && dtpayout.Rows.Count > 0)
                {
                    for (i = 0; i < dtpayout.Rows.Count; i++)
                    {
                        if (i % 2 == 0)
                        {
                            ltServiceFeeDetail.Text += "<div style='width: 100%; clear: both; font-size: 12px; font-weight: bold;min-height: 20px;overflow: hidden;'><div style='width: 440px; float: left; text-align: left; padding-left: 10px;'>";
                            ltServiceFeeDetail.Text += dtpayout.Rows[i]["poType"].ToString().Trim() == "" ? "&nbsp;" : dtpayout.Rows[i]["poType"].ToString().Trim();
                            ltServiceFeeDetail.Text += "</div><div style='width: 300px; float: left; text-align: left; font-weight: bold;'>";
                            ltServiceFeeDetail.Text += dtpayout.Rows[i]["poDescription"].ToString().Trim();
                            ltServiceFeeDetail.Text += "</div><div style='width: 150px; float: left; text-align: left; font-weight: normal;'>";
                            dTotalAdjustment += Math.Round(Convert.ToDouble(dtpayout.Rows[i]["poAmount"].ToString().Trim()), 2, MidpointRounding.AwayFromZero);
                            ltServiceFeeDetail.Text += "$" + dtpayout.Rows[i]["poAmount"].ToString().Trim() + "</div></div>";
                        }
                        else
                        {
                            ltServiceFeeDetail.Text += "<div style='width: 100%; clear: both; font-size: 12px; font-weight: bold; background-color: #c0c0c0;min-height: 20px;overflow: hidden;'><div style='width: 440px; float: left; text-align: left; padding-left: 10px;'>";
                            ltServiceFeeDetail.Text += dtpayout.Rows[i]["poType"].ToString().Trim() == "" ? "&nbsp;" : dtpayout.Rows[i]["poType"].ToString().Trim();
                            ltServiceFeeDetail.Text += "</div><div style='width: 300px; float: left; text-align: left; font-weight: bold;'>";
                            ltServiceFeeDetail.Text += dtpayout.Rows[i]["poDescription"].ToString().Trim();
                            ltServiceFeeDetail.Text += "</div><div style='width: 150px; float: left; text-align: left; font-weight: normal;'>";
                            dTotalAdjustment += Math.Round(Convert.ToDouble(dtpayout.Rows[i]["poAmount"].ToString().Trim()), 2, MidpointRounding.AwayFromZero);
                            ltServiceFeeDetail.Text += "$" + dtpayout.Rows[i]["poAmount"].ToString().Trim() + "</div></div>";
                        }
                    }
                    if (i % 2 == 0)
                    {
                        ltServiceFeeDetail.Text += "<div style='width: 100%; clear: both; font-size: 12px; font-weight: bold;min-height: 20px;overflow: hidden;'><div style='width: 440px; float: left; text-align: left; padding-left: 10px;'>";
                        ltServiceFeeDetail.Text += "Amount After Adjustment";
                        ltServiceFeeDetail.Text += "</div><div style='width: 300px; float: left; text-align: left; font-weight: bold;'>";
                        ltServiceFeeDetail.Text += "$" + dTotalPayoutToBusiness + " - $" + dTotalAdjustment;
                        ltServiceFeeDetail.Text += "</div><div style='width: 150px; float: left; text-align: left; font-weight: normal;'>";
                        ltServiceFeeDetail.Text += "$" + (dTotalPayoutToBusiness - dTotalAdjustment) + "</div></div>";
                    }
                    else
                    {
                        ltServiceFeeDetail.Text += "<div style='width: 100%; clear: both; font-size: 12px; font-weight: bold;background-color: #c0c0c0;min-height: 20px;overflow: hidden;'><div style='width: 440px; float: left; text-align: left; padding-left: 10px;'>";
                        ltServiceFeeDetail.Text += "Amount After Adjustment";
                        ltServiceFeeDetail.Text += "</div><div style='width: 300px; float: left; text-align: left; font-weight: bold;'>";
                        ltServiceFeeDetail.Text += "$" + dTotalPayoutToBusiness + " + $" + dTotalAdjustment;
                        ltServiceFeeDetail.Text += "</div><div style='width: 150px; float: left; text-align: left; font-weight: normal;'>";
                        ltServiceFeeDetail.Text += "$" + (dTotalPayoutToBusiness + dTotalAdjustment) + "</div></div>";
                    }
                    dTotalPayoutToBusiness = dTotalPayoutToBusiness + dTotalAdjustment;
                }

           


          
            BLLUser objAccRep = new BLLUser();
            objAccRep.email = dtDealDetail.Rows[0]["salePersonAccountName"].ToString().Trim();
            DataTable dtAccRep = objAccRep.getUserDetailByEmail();
            if (dtAccRep != null && dtAccRep.Rows.Count > 0)
            {
                lblAccountRepDetail.Text = "Your Account Manager: " + dtAccRep.Rows[0]["firstName"].ToString().Trim() + " " + dtAccRep.Rows[0]["lastName"].ToString().Trim() + " (<span style='color:Blue;'>" + dtAccRep.Rows[0]["userName"].ToString().Trim() + "</span>)";
            }
           
            if (dtResult != null && dtResult.Rows.Count > 0)
            {
                lblBusinessAddress.Text = dtResult.Rows[0]["bAddress"].ToString().Trim();
                lblBusinessName.Text = dtResult.Rows[0]["bName"].ToString().Trim();
                lblBusinessPaymentAddress.Text = dtResult.Rows[0]["cAddress"].ToString().Trim();
                lblBusinessPaymentPhone.Text = dtResult.Rows[0]["cPhone"].ToString().Trim();
                lblBusinessPaymentTitle.Text = dtResult.Rows[0]["cName"].ToString().Trim();
                lblBusinessPhone.Text = dtResult.Rows[0]["bPhone"].ToString().Trim();
                lblFirstPaymentPer.Text = dtResult.Rows[0]["firstPer"].ToString().Trim();
                lblSecondPaymentPer.Text = dtResult.Rows[0]["secondPer"].ToString().Trim();
                lblThirdPaymentPercent.Text = dtResult.Rows[0]["thirdPer"].ToString().Trim();

                lblFirstPaymentDateTime.Text = dtResult.Rows[0]["firstPaymentText"].ToString().Trim();
                lblSecondPaymentDateTime.Text = dtResult.Rows[0]["secondPaymentText"].ToString().Trim();
                lblThirdPaymentDateTime.Text = dtResult.Rows[0]["thirdPaymenText"].ToString().Trim();

                double dTempTotalAmount = Math.Round(Convert.ToDouble(dtResult.Rows[0]["paymentAmount"].ToString().Trim()) * (100 / Convert.ToDouble(dtResult.Rows[0]["secondPer"].ToString().Trim())), 2, MidpointRounding.AwayFromZero);

                lblInvoiceDate.Text = "INVOICE DATE: " + Convert.ToDateTime(dtResult.Rows[0]["createdDate"].ToString().Trim()).ToString("yyyy/MM/dd");
                lblFirstPaymentAmount.Text = Math.Round((Convert.ToDouble(lblFirstPaymentPer.Text) / 100) * dTempTotalAmount, 2, MidpointRounding.AwayFromZero).ToString();
                if (paymentDone)
                {
                    lblSecondPaymentAmount.Text = dtResult.Rows[0]["paymentSend"].ToString().Trim();
                }
                else
                {
                    lblSecondPaymentAmount.Text = dTotalPayoutToBusiness.ToString();
                }                
                lblThirdPaymentAmount.Text = Math.Round((Convert.ToDouble(lblThirdPaymentPercent.Text) / 100) * dTempTotalAmount, 2, MidpointRounding.AwayFromZero).ToString();
            }           
        }
        catch (Exception ex)
        { }
    }
}
