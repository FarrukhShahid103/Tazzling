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
using System.Diagnostics;
using System.IO;
using SQLHelper;

public partial class editinvoice2 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {            
            btnEditFirstPayment.Visible = false;
            btnEditFirstPer.Visible = false;
            btnEditSecondPayment.Visible = false;
            btnEditSecondPer.Visible = false;
            btnEditThirdPayment.Visible = false;
            btnEditThirdPer.Visible = false;
                        
            if (ViewState["title"] != null)
            {
                Page.Title = ViewState["title"].ToString().Trim();
            }
            if (!IsPostBack)
            {
                if (Session["user"] != null)
                {
                    DataTable dtUser = (DataTable)Session["user"];
                    if ((dtUser != null) && (dtUser.Rows.Count > 0))
                    {
                        ViewState["userID"] = dtUser.Rows[0]["userID"];
                    }

                    if (Request.QueryString["did"] != null && Request.QueryString["did"].ToString().Trim() != ""
                        && Request.QueryString["myname"] != null && Request.QueryString["myname"].ToString().Trim() != ""
                        && Request.QueryString["myname"].ToString().Trim() == "colinazam")
                    {
                        renderFirstDealInvoice();
                    }
                }
                else
                {
                    Response.Redirect("Default.aspx");
                }
            }           
        }
        catch (Exception ex)
        { }
    }

    protected void pageGrid_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            SqlHelper.ExecuteNonQuery(Misc.connStr, CommandType.Text, "delete from payOut where poID=" + pageGrid.DataKeys[e.RowIndex].Value.ToString());
            renderFirstDealInvoice();
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
        }

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

    protected void renderFirstDealInvoice()
    {
        try
        {
            ltAdjustmentTotal.Text = "";
            ltDealsDetails.Text = "";

            ltRefundOrders.Text = "";
            ltServiceFeeDetail.Text = "";
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
                btnAdjustment.Visible = false;
                btnSendFistPayment.Visible = false;
                paymentDone = true;
                dtPaymentDate = Convert.ToDateTime(dtResult.Rows[0]["paymentdate"].ToString().Trim());
                lblLablePayments.Text = "Second Payment sent on " + dtPaymentDate.ToString("MM/dd/yyyy");
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
                txtDealNote.Text = dtDealDetail.Rows[0]["dealNote"].ToString().Trim();
                lblBusinessAddress.Text = dtDealDetail.Rows[0]["restaurantAddress"].ToString().Trim();
                lblBusinessName.Text = dtDealDetail.Rows[0]["restaurantBusinessName"].ToString().Trim();
                lblBusinessPaymentAddress.Text = dtDealDetail.Rows[0]["restaurantpaymentAddress"].ToString().Trim();
                lblBusinessPaymentPhone.Text = dtDealDetail.Rows[0]["cellNumber"].ToString().Trim();
                lblBusinessPaymentTitle.Text = dtDealDetail.Rows[0]["businessPaymentTitle"].ToString().Trim();
                lblBusinessPhone.Text = dtDealDetail.Rows[0]["phone"].ToString().Trim();

                txtBusinessAddress.Text = dtDealDetail.Rows[0]["restaurantAddress"].ToString().Trim();
                txtBusinessName.Text = dtDealDetail.Rows[0]["restaurantBusinessName"].ToString().Trim();
                txtBusinessPaymentAddress.Text = dtDealDetail.Rows[0]["restaurantpaymentAddress"].ToString().Trim();
                txtBusinessPaymentPhone.Text = dtDealDetail.Rows[0]["cellNumber"].ToString().Trim();
                txtBusinessPaymentTitle.Text = dtDealDetail.Rows[0]["businessPaymentTitle"].ToString().Trim();
                txtBusinessPhone.Text = dtDealDetail.Rows[0]["phone"].ToString().Trim();
                Page.Title = dtDealDetail.Rows[0]["title"].ToString().Trim();
                ViewState["title"] = dtDealDetail.Rows[0]["title"].ToString().Trim();
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
                pageGrid.Visible = true;
                pageGrid.DataSource = dtpayout.DefaultView;
                pageGrid.DataBind();
                if (paymentDone)
                {
                    pageGrid.Columns[3].Visible = false;
                }


                object objSum = dtpayout.Compute("Sum(poAmount)", string.Empty);
                dTotalAdjustment = Math.Round(Convert.ToDouble(objSum), 2, MidpointRounding.AwayFromZero);
                if (dtpayout.Rows.Count % 2 == 0)
                {
                    ltAdjustmentTotal.Text += "<div style='width: 100%; clear: both; font-size: 12px; font-weight: bold;min-height: 20px;overflow: hidden;'><div style='width: 440px; float: left; text-align: left; padding-left: 10px;'>";
                    ltAdjustmentTotal.Text += "Amount After Adjustment";
                    ltAdjustmentTotal.Text += "</div><div style='width: 300px; float: left; text-align: left; font-weight: bold;'>";
                    ltAdjustmentTotal.Text += "$" + dTotalPayoutToBusiness + " + $" + dTotalAdjustment;
                    ltAdjustmentTotal.Text += "</div><div style='width: 150px; float: left; text-align: left; font-weight: normal;'>";
                    ltAdjustmentTotal.Text += "$" + (dTotalPayoutToBusiness + dTotalAdjustment) + "</div></div>";
                }
                else
                {
                    ltAdjustmentTotal.Text += "<div style='width: 100%; clear: both; font-size: 12px; font-weight: bold;background-color: #c0c0c0;min-height: 20px;overflow: hidden;'><div style='width: 440px; float: left; text-align: left; padding-left: 10px;'>";
                    ltAdjustmentTotal.Text += "Amount After Adjustment";
                    ltAdjustmentTotal.Text += "</div><div style='width: 300px; float: left; text-align: left; font-weight: bold;'>";
                    ltAdjustmentTotal.Text += "$" + dTotalPayoutToBusiness + " + $" + dTotalAdjustment;
                    ltAdjustmentTotal.Text += "</div><div style='width: 150px; float: left; text-align: left; font-weight: normal;'>";
                    ltAdjustmentTotal.Text += "$" + (dTotalPayoutToBusiness + dTotalAdjustment) + "</div></div>";
                }
                dTotalPayoutToBusiness = dTotalPayoutToBusiness + dTotalAdjustment;
            }
            else
            {
                pageGrid.Visible = false;
                ltAdjustmentTotal.Text = "";
            }


            //Displaying Business Account represeter detail
            BLLUser objAccRep = new BLLUser();
            objAccRep.email = dtDealDetail.Rows[0]["salePersonAccountName"].ToString().Trim();
            DataTable dtAccRep = objAccRep.getUserDetailByEmail();
            if (dtAccRep != null && dtAccRep.Rows.Count > 0)
            {
                lblAccountRepDetail.Text = "Your Account Manager: " + dtAccRep.Rows[0]["firstName"].ToString().Trim() + " " + dtAccRep.Rows[0]["lastName"].ToString().Trim() + " (<span style='color:Blue;'>" + dtAccRep.Rows[0]["userName"].ToString().Trim() + "</span>)";
            }


            GECEncryption objEnc = new GECEncryption();
            strQuery = "";
            strQuery = "select dealOrderCode,dealOrders.createdDate, dealOrders.modifiedDate,shippingAndTax,deals.shippingAndTaxAmount, sellingPrice,qty ,title,qty, comment from dealorders ";
            strQuery += " inner join dealOrderDetail on dealOrderDetail.dOrderID = dealOrders.dOrderID";
            strQuery += " inner join deals on deals.dealID = dealOrders.dealID";
            strQuery += " left outer join userComments on (userComments.dOrderID = dealOrders.dOrderID) ";
            if (paymentDone)
            {
                strQuery += " where status<>'Successful' and dealOrders.modifiedDate between '" + dtFirstPaymentDate + "' and '" + dtPaymentDate + "' and dealOrders.dealid in (select dealId from deals where dealid=";
            }
            else
            {
                strQuery += " where status<>'Successful' and  dealOrders.modifiedDate > CONVERT(DATETIME,'" + dtFirstPaymentDate + "') and dealOrders.dealid in (select dealId from deals where dealid=";
            }
            strQuery += Request.QueryString["did"].Trim() + " or parentDealId=" + Request.QueryString["did"].Trim() + ") order by dealOrders.modifiedDate desc";
            DataTable dtRefundedOrders = Misc.search(strQuery);
            i = 0;
            if (dtRefundedOrders != null && dtRefundedOrders.Rows.Count > 0)
            {
                ltRefundOrders.Text += "<div style='width: 100%; clear: both; font-size: 16px; padding-top: 70px; padding-left: 15px;font-weight: bold;'>Refunds</div>    <div style='width: 100%; border-bottom: 1px solid black; clear: both;'></div>";
                ltRefundOrders.Text += "<div style='width: 100%; clear: both; font-size: 12px; height: 25px; padding-top: 5px;'><div style='width: 150px; float: left; text-align: left; padding-left: 15px;'>Date</div>";
                ltRefundOrders.Text += "<div style='width: 250px; float: left; text-align: left; font-weight: bold;'>Voucher Code</div><div style='width: 385px; float: left; text-align: left;'>Detail</div><div style='width: 100px; float: left; text-align: left;'>Line Total</div></div><div style='width: 100%; border-bottom: 1px solid black; clear: both;'></div>";

                for (i = 0; i < dtRefundedOrders.Rows.Count; i++)
                {
                    try
                    {
                        if (dtRefundedOrders.Rows[i]["shippingAndTax"] != null && Convert.ToBoolean(dtRefundedOrders.Rows[i]["shippingAndTax"].ToString().Trim()))
                        {

                            if (i % 2 == 0)
                            {
                                //Refunded Orders Display
                                DateTime dtModify = DateTime.Now;
                                DateTime.TryParse(dtRefundedOrders.Rows[i]["modifiedDate"].ToString().Trim(), out dtModify);
                                string[] strTempReasong = dtRefundedOrders.Rows[i]["comment"].ToString().Trim().Split(':');

                                //Refund First Line
                                ltRefundOrders.Text += "<div style='width: 100%; clear: both; font-size: 12px; font-weight: bold; background-color: #c0c0c0;min-height: 20px;overflow: hidden;'><div style='width: 150px; float: left; text-align: left; padding-left: 10px;font-weight: bold;'>";
                                ltRefundOrders.Text += dtModify.ToString("MM/dd/yyyy hh:mm:ss tt");
                                ltRefundOrders.Text += "</div><div style='width: 240px; float: left; text-align: left; font-weight: bold;padding-right:10px;'>";
                                ltRefundOrders.Text += "# " + objEnc.DecryptData("deatailOrder", dtRefundedOrders.Rows[i]["dealOrderCode"].ToString().Trim());
                                ltRefundOrders.Text += "</div><div style='width: 375px; float: left; text-align: left; font-weight: normal; padding-right:10px;'>";
                                if (strTempReasong.Length > 1)
                                {
                                    if (strTempReasong[strTempReasong.Length - 1].Trim() == "")
                                    {
                                        ltRefundOrders.Text += "-";
                                    }
                                    else
                                    {
                                        ltRefundOrders.Text += strTempReasong[strTempReasong.Length - 1].Trim();
                                    }
                                }
                                else
                                {
                                    if (dtRefundedOrders.Rows[i]["comment"].ToString().Trim() == "")
                                    {
                                        ltRefundOrders.Text += "-";
                                    }
                                    else
                                    {
                                        ltRefundOrders.Text += dtRefundedOrders.Rows[i]["comment"].ToString().Trim();
                                    }
                                }
                                ltRefundOrders.Text += "</div><div style='width: 80px; float: left; text-align: left; font-weight: normal; padding-left:20px;'>-$";

                                double dTempOrderValue = Convert.ToDouble(dtRefundedOrders.Rows[i]["sellingPrice"].ToString().Trim()) * Convert.ToDouble(dtRefundedOrders.Rows[i]["qty"].ToString().Trim());

                                ltRefundOrders.Text += dTempOrderValue;
                                ltRefundOrders.Text += "</div></div>";




                                //Refund Second Reverse Comission
                                ltRefundOrders.Text += "<div style='width: 100%; clear: both; font-size: 12px; font-weight: bold; min-height: 20px;overflow: hidden;'><div style='width: 150px; float: left; text-align: left; padding-left: 10px;font-weight: bold;'>";
                                ltRefundOrders.Text += dtModify.ToString("MM/dd/yyyy hh:mm:ss tt");
                                ltRefundOrders.Text += "</div><div style='width: 240px; float: left; text-align: left;font-weight: normal;padding-right:10px;'>";
                                ltRefundOrders.Text += "Reverse Commission";
                                ltRefundOrders.Text += "</div><div style='width: 375px; float: left; text-align: left; font-weight: normal; padding-right:10px;'>";
                                ltRefundOrders.Text += Convert.ToDouble(dtDealDetail.Rows[0]["OurCommission"].ToString().Trim()) + "% x $" + dTempOrderValue;
                                double dTempComission = Math.Round((Convert.ToDouble(dtDealDetail.Rows[0]["OurCommission"].ToString().Trim()) / 100) * dTempOrderValue, 2, MidpointRounding.AwayFromZero);
                                ltRefundOrders.Text += "</div><div style='width: 80px; float: left; text-align: left; font-weight: normal; padding-left:20px;'>$";
                                ltRefundOrders.Text += dTempComission;
                                ltRefundOrders.Text += "</div></div>";

                                double dTempShippingValue = Convert.ToDouble(dtRefundedOrders.Rows[i]["shippingAndTaxAmount"].ToString().Trim()) * Convert.ToDouble(dtRefundedOrders.Rows[i]["qty"].ToString().Trim());

                                dTempOrderValue += dTempShippingValue;

                                //Shipping And Tax Line
                                ltRefundOrders.Text += "<div style='width: 100%; clear: both; font-size: 12px; font-weight: bold; min-height: 20px;overflow: hidden; background-color: #c0c0c0'><div style='width: 150px; float: left; text-align: left; padding-left: 10px;font-weight: bold;'>";
                                ltRefundOrders.Text += dtModify.ToString("MM/dd/yyyy hh:mm:ss tt");
                                ltRefundOrders.Text += "</div><div style='width: 240px; float: left; text-align: left; font-weight: bold;padding-right:10px;'>";
                                ltRefundOrders.Text += "Shipping Charged";
                                ltRefundOrders.Text += "</div><div style='width: 375px; float: left; text-align: left; font-weight: normal; padding-right:10px;'>";
                                ltRefundOrders.Text += "-$" + dtRefundedOrders.Rows[i]["shippingAndTaxAmount"].ToString().Trim();
                                ltRefundOrders.Text += "</div><div style='width: 80px; float: left; text-align: left; font-weight: normal; padding-left:20px;'>-$";
                                ltRefundOrders.Text += dTempShippingValue;
                                ltRefundOrders.Text += "</div></div>";




                                //Refund Fourth Tax Reverse
                                ltRefundOrders.Text += "<div style='width: 100%; clear: both; font-size: 12px; font-weight: bold; min-height: 20px;overflow: hidden;'><div style='width: 150px; float: left; text-align: left; padding-left: 10px;font-weight: bold;'>";
                                ltRefundOrders.Text += dtModify.ToString("MM/dd/yyyy hh:mm:ss tt");
                                ltRefundOrders.Text += "</div><div style='width: 240px; float: left; text-align: left;font-weight: normal;padding-right:10px;'>";
                                ltRefundOrders.Text += "Reverse HST #849725056";
                                ltRefundOrders.Text += "</div><div style='width: 375px; float: left; text-align: left; font-weight: normal; padding-right:10px;'>";
                                ltRefundOrders.Text += "12% x $" + dTempComission;
                                double dTax = Math.Round((12.0 / 100) * dTempComission, 2, MidpointRounding.AwayFromZero);
                                ltRefundOrders.Text += "</div><div style='width: 80px; float: left; text-align: left; font-weight: normal; padding-left:20px;'>$";
                                ltRefundOrders.Text += dTax;
                                ltRefundOrders.Text += "</div></div>";


                                //Refund Third Deduct Credit Card Transaction Fee
                                ltRefundOrders.Text += "<div style='width: 100%; clear: both; font-size: 12px; font-weight: bold;min-height: 20px;overflow: hidden; background-color: #c0c0c0'><div style='width: 150px; float: left; text-align: left; padding-left: 10px;font-weight: bold;'>";
                                ltRefundOrders.Text += dtModify.ToString("MM/dd/yyyy hh:mm:ss tt");
                                ltRefundOrders.Text += "</div><div style='width: 240px; float: left; text-align: left;font-weight: normal;padding-right:10px;'>";
                                ltRefundOrders.Text += "Credit Card Transaction Fee";
                                ltRefundOrders.Text += "</div><div style='width: 375px; float: left; text-align: left; font-weight: normal; padding-right:10px;'>";
                                ltRefundOrders.Text += "3.9% x $" + dTempOrderValue;
                                double dTempCreditCardFee = Math.Round((3.9 / 100) * dTempOrderValue, 2, MidpointRounding.AwayFromZero);
                                ltRefundOrders.Text += "</div><div style='width: 80px; float: left; text-align: left; font-weight: normal; padding-left:20px;'>-$";
                                ltRefundOrders.Text += dTempCreditCardFee;
                                ltRefundOrders.Text += "</div></div>";

                            }
                            else
                            {
                                //Refunded Orders Display
                                DateTime dtModify = DateTime.Now;
                                DateTime.TryParse(dtRefundedOrders.Rows[i]["modifiedDate"].ToString().Trim(), out dtModify);
                                string[] strTempReasong = dtRefundedOrders.Rows[i]["comment"].ToString().Trim().Split(':');

                                //Refund First Line
                                ltRefundOrders.Text += "<div style='width: 100%; clear: both; font-size: 12px; font-weight: bold;min-height: 20px;overflow: hidden;'><div style='width: 150px; float: left; text-align: left; padding-left: 10px;font-weight: bold;'>";
                                ltRefundOrders.Text += dtModify.ToString("MM/dd/yyyy hh:mm:ss tt");
                                ltRefundOrders.Text += "</div><div style='width: 240px; float: left; text-align: left; font-weight: bold;padding-right:10px;'>";
                                ltRefundOrders.Text += "# " + objEnc.DecryptData("deatailOrder", dtRefundedOrders.Rows[i]["dealOrderCode"].ToString().Trim());
                                ltRefundOrders.Text += "</div><div style='width: 375px; float: left; text-align: left; font-weight: normal; padding-right:10px;'>";
                                if (strTempReasong.Length > 1)
                                {
                                    if (strTempReasong[strTempReasong.Length - 1].Trim() == "")
                                    {
                                        ltRefundOrders.Text += "-";
                                    }
                                    else
                                    {
                                        ltRefundOrders.Text += strTempReasong[strTempReasong.Length - 1].Trim();
                                    }
                                }
                                else
                                {
                                    if (dtRefundedOrders.Rows[i]["comment"].ToString().Trim() == "")
                                    {
                                        ltRefundOrders.Text += "-";
                                    }
                                    else
                                    {
                                        ltRefundOrders.Text += dtRefundedOrders.Rows[i]["comment"].ToString().Trim();
                                    }
                                }
                                ltRefundOrders.Text += "</div><div style='width: 80px; float: left; text-align: left; font-weight: normal; padding-left:20px;'>-$";

                                double dTempOrderValue = Convert.ToDouble(dtRefundedOrders.Rows[i]["sellingPrice"].ToString().Trim()) * Convert.ToDouble(dtRefundedOrders.Rows[i]["qty"].ToString().Trim());

                                ltRefundOrders.Text += dTempOrderValue;
                                ltRefundOrders.Text += "</div></div>";


                                //Refund Second Reverse Comission
                                ltRefundOrders.Text += "<div style='width: 100%; clear: both; font-size: 12px; font-weight: bold; min-height: 20px;overflow: hidden; background-color: #c0c0c0'><div style='width: 150px; float: left; text-align: left; padding-left: 10px;font-weight: bold;'>";
                                ltRefundOrders.Text += dtModify.ToString("MM/dd/yyyy hh:mm:ss tt");
                                ltRefundOrders.Text += "</div><div style='width: 240px; float: left; text-align: left;font-weight: normal;padding-right:10px;'>";
                                ltRefundOrders.Text += "Reverse Commission";
                                ltRefundOrders.Text += "</div><div style='width: 375px; float: left; text-align: left; font-weight: normal; padding-right:10px;'>";
                                ltRefundOrders.Text += Convert.ToDouble(dtDealDetail.Rows[0]["OurCommission"].ToString().Trim()) + "% x $" + dTempOrderValue;
                                double dTempComission = Math.Round((Convert.ToDouble(dtDealDetail.Rows[0]["OurCommission"].ToString().Trim()) / 100) * dTempOrderValue, 2, MidpointRounding.AwayFromZero);
                                ltRefundOrders.Text += "</div><div style='width: 80px; float: left; text-align: left; font-weight: normal; padding-left:20px;'>$";
                                ltRefundOrders.Text += dTempComission;
                                ltRefundOrders.Text += "</div></div>";

                                double dTempShippingValue = Convert.ToDouble(dtRefundedOrders.Rows[i]["shippingAndTaxAmount"].ToString().Trim()) * Convert.ToDouble(dtRefundedOrders.Rows[i]["qty"].ToString().Trim());
                                dTempOrderValue += dTempShippingValue;

                                //Shipping And Tax Line
                                ltRefundOrders.Text += "<div style='width: 100%; clear: both; font-size: 12px; font-weight: bold; min-height: 20px;overflow: hidden;'><div style='width: 150px; float: left; text-align: left; padding-left: 10px;font-weight: bold;'>";
                                ltRefundOrders.Text += dtModify.ToString("MM/dd/yyyy hh:mm:ss tt");
                                ltRefundOrders.Text += "</div><div style='width: 240px; float: left; text-align: left; font-weight: bold;padding-right:10px;'>";
                                ltRefundOrders.Text += "Shipping Charged";
                                ltRefundOrders.Text += "</div><div style='width: 375px; float: left; text-align: left; font-weight: normal; padding-right:10px;'>";

                                ltRefundOrders.Text += "-$" + dtRefundedOrders.Rows[i]["shippingAndTaxAmount"].ToString().Trim();
                                ltRefundOrders.Text += "</div><div style='width: 80px; float: left; text-align: left; font-weight: normal; padding-left:20px;'>-$";
                                ltRefundOrders.Text += dTempShippingValue;
                                ltRefundOrders.Text += "</div></div>";


                                //Refund Fourth Tax Reverse
                                ltRefundOrders.Text += "<div style='width: 100%; clear: both; font-size: 12px; font-weight: bold; min-height: 20px;overflow: hidden; background-color: #c0c0c0'><div style='width: 150px; float: left; text-align: left; padding-left: 10px;font-weight: bold;'>";
                                ltRefundOrders.Text += dtModify.ToString("MM/dd/yyyy hh:mm:ss tt");
                                ltRefundOrders.Text += "</div><div style='width: 240px; float: left; text-align: left;font-weight: normal;padding-right:10px;'>";
                                ltRefundOrders.Text += "Reverse HST #849725056";
                                ltRefundOrders.Text += "</div><div style='width: 375px; float: left; text-align: left; font-weight: normal; padding-right:10px;'>";
                                ltRefundOrders.Text += "12% x $" + dTempComission;
                                double dTax = Math.Round((12.0 / 100) * dTempComission, 2, MidpointRounding.AwayFromZero);
                                ltRefundOrders.Text += "</div><div style='width: 80px; float: left; text-align: left; font-weight: normal; padding-left:20px;'>$";
                                ltRefundOrders.Text += dTax;
                                ltRefundOrders.Text += "</div></div>";

                                //Refund Third Deduct Credit Card Transaction Fee
                                ltRefundOrders.Text += "<div style='width: 100%; clear: both; font-size: 12px; font-weight: bold;min-height: 20px;overflow: hidden;'><div style='width: 150px; float: left; text-align: left; padding-left: 10px;font-weight: bold;'>";
                                ltRefundOrders.Text += dtModify.ToString("MM/dd/yyyy hh:mm:ss tt");
                                ltRefundOrders.Text += "</div><div style='width: 240px; float: left; text-align: left;font-weight: normal;padding-right:10px;'>";
                                ltRefundOrders.Text += "Credit Card Transaction Fee";
                                ltRefundOrders.Text += "</div><div style='width: 375px; float: left; text-align: left; font-weight: normal; padding-right:10px;'>";
                                ltRefundOrders.Text += "3.9% x $" + dTempOrderValue;
                                double dTempCreditCardFee = Math.Round((3.9 / 100) * dTempOrderValue, 2, MidpointRounding.AwayFromZero);
                                ltRefundOrders.Text += "</div><div style='width: 80px; float: left; text-align: left; font-weight: normal; padding-left:20px;'>-$";
                                ltRefundOrders.Text += dTempCreditCardFee;
                                ltRefundOrders.Text += "</div></div>";

                            }

                        }
                        else
                        {
                            //Refunded Orders Display
                            DateTime dtModify = DateTime.Now;
                            DateTime.TryParse(dtRefundedOrders.Rows[i]["modifiedDate"].ToString().Trim(), out dtModify);
                            string[] strTempReasong = dtRefundedOrders.Rows[i]["comment"].ToString().Trim().Split(':');

                            //Refund First Line
                            ltRefundOrders.Text += "<div style='width: 100%; clear: both; font-size: 12px; font-weight: bold; background-color: #c0c0c0;min-height: 20px;overflow: hidden;'><div style='width: 150px; float: left; text-align: left; padding-left: 10px;font-weight: bold;'>";
                            ltRefundOrders.Text += dtModify.ToString("MM/dd/yyyy hh:mm:ss tt");
                            ltRefundOrders.Text += "</div><div style='width: 240px; float: left; text-align: left; font-weight: bold;padding-right:10px;'>";
                            ltRefundOrders.Text += "# " + objEnc.DecryptData("deatailOrder", dtRefundedOrders.Rows[i]["dealOrderCode"].ToString().Trim());
                            ltRefundOrders.Text += "</div><div style='width: 375px; float: left; text-align: left; font-weight: normal; padding-right:10px;'>";
                            if (strTempReasong.Length > 1)
                            {
                                if (strTempReasong[strTempReasong.Length - 1].Trim() == "")
                                {
                                    ltRefundOrders.Text += "-";
                                }
                                else
                                {
                                    ltRefundOrders.Text += strTempReasong[strTempReasong.Length - 1].Trim();
                                }
                            }
                            else
                            {
                                if (dtRefundedOrders.Rows[i]["comment"].ToString().Trim() == "")
                                {
                                    ltRefundOrders.Text += "-";
                                }
                                else
                                {
                                    ltRefundOrders.Text += dtRefundedOrders.Rows[i]["comment"].ToString().Trim();
                                }
                            }
                            ltRefundOrders.Text += "</div><div style='width: 80px; float: left; text-align: left; font-weight: normal; padding-left:20px;'>-$";

                            double dTempOrderValue = Convert.ToDouble(dtRefundedOrders.Rows[i]["sellingPrice"].ToString().Trim()) * Convert.ToDouble(dtRefundedOrders.Rows[i]["qty"].ToString().Trim());

                            ltRefundOrders.Text += dTempOrderValue;
                            ltRefundOrders.Text += "</div></div>";

                            //Refund Second Reverse Comission
                            ltRefundOrders.Text += "<div style='width: 100%; clear: both; font-size: 12px; font-weight: bold; min-height: 20px;overflow: hidden;'><div style='width: 150px; float: left; text-align: left; padding-left: 10px;font-weight: bold;'>";
                            ltRefundOrders.Text += dtModify.ToString("MM/dd/yyyy hh:mm:ss tt");
                            ltRefundOrders.Text += "</div><div style='width: 240px; float: left; text-align: left;font-weight: normal;padding-right:10px;'>";
                            ltRefundOrders.Text += "Reverse Commission";
                            ltRefundOrders.Text += "</div><div style='width: 375px; float: left; text-align: left; font-weight: normal; padding-right:10px;'>";
                            ltRefundOrders.Text += Convert.ToDouble(dtDealDetail.Rows[0]["OurCommission"].ToString().Trim()) + "% x $" + dTempOrderValue;
                            double dTempComission = Math.Round((Convert.ToDouble(dtDealDetail.Rows[0]["OurCommission"].ToString().Trim()) / 100) * dTempOrderValue, 2, MidpointRounding.AwayFromZero);
                            ltRefundOrders.Text += "</div><div style='width: 80px; float: left; text-align: left; font-weight: normal; padding-left:20px;'>$";
                            ltRefundOrders.Text += dTempComission;
                            ltRefundOrders.Text += "</div></div>";

                            //Refund Fourth Tax Reverse
                            ltRefundOrders.Text += "<div style='width: 100%; clear: both; font-size: 12px; font-weight: bold; background-color: #c0c0c0; min-height: 20px;overflow: hidden;'><div style='width: 150px; float: left; text-align: left; padding-left: 10px;font-weight: bold;'>";
                            ltRefundOrders.Text += dtModify.ToString("MM/dd/yyyy hh:mm:ss tt");
                            ltRefundOrders.Text += "</div><div style='width: 240px; float: left; text-align: left;font-weight: normal;padding-right:10px;'>";
                            ltRefundOrders.Text += "Reverse HST #849725056";
                            ltRefundOrders.Text += "</div><div style='width: 375px; float: left; text-align: left; font-weight: normal; padding-right:10px;'>";
                            ltRefundOrders.Text += "12% x $" + dTempComission;
                            double dTax = Math.Round((12.0 / 100) * dTempComission, 2, MidpointRounding.AwayFromZero);
                            ltRefundOrders.Text += "</div><div style='width: 80px; float: left; text-align: left; font-weight: normal; padding-left:20px;'>$";
                            ltRefundOrders.Text += dTax;
                            ltRefundOrders.Text += "</div></div>";


                            //Refund Third Deduct Credit Card Transaction Fee
                            ltRefundOrders.Text += "<div style='width: 100%; clear: both; font-size: 12px; font-weight: bold; min-height: 20px;overflow: hidden;'><div style='width: 150px; float: left; text-align: left; padding-left: 10px;font-weight: bold;'>";
                            ltRefundOrders.Text += dtModify.ToString("MM/dd/yyyy hh:mm:ss tt");
                            ltRefundOrders.Text += "</div><div style='width: 240px; float: left; text-align: left;font-weight: normal;padding-right:10px;'>";
                            ltRefundOrders.Text += "Credit Card Transaction Fee";
                            ltRefundOrders.Text += "</div><div style='width: 375px; float: left; text-align: left; font-weight: normal; padding-right:10px;'>";
                            ltRefundOrders.Text += "3.9% x $" + dTempOrderValue;
                            double dTempCreditCardFee = Math.Round((3.9 / 100) * dTempOrderValue, 2, MidpointRounding.AwayFromZero);
                            ltRefundOrders.Text += "</div><div style='width: 80px; float: left; text-align: left; font-weight: normal; padding-left:20px;'>-$";
                            ltRefundOrders.Text += dTempCreditCardFee;
                            ltRefundOrders.Text += "</div></div>";


                        }

                    }
                    catch (Exception ex)
                    {

                    }


                }

                if (i % 2 == 0)
                {
                    ltRefundOrders.Text += "<div style='width: 100%; clear: both; font-size: 12px; font-weight: bold; min-height: 20px;overflow: hidden;background-color: #c0c0c0;'><div style='width: 150px; float: left; text-align: left; padding-left: 10px;font-weight: bold;'>";
                }
                else
                {
                    ltRefundOrders.Text += "<div style='width: 100%; clear: both; font-size: 12px; font-weight: bold; min-height: 20px;overflow: hidden;'><div style='width: 150px; float: left; text-align: left; padding-left: 10px;font-weight: bold;'>";
                }
                ltRefundOrders.Text += "Total Refunds";
                ltRefundOrders.Text += "</div><div style='width: 240px; float: left; text-align: left;font-weight: normal;padding-right:10px;'>";
                ltRefundOrders.Text += "&nbsp;";
                ltRefundOrders.Text += "</div><div style='width: 375px; float: left; text-align: left; font-weight: normal; padding-right:10px;'>";
                ltRefundOrders.Text += "&nbsp;";
                ltRefundOrders.Text += "</div><div style='width: 80px; float: left; text-align: left; font-weight: normal; padding-left:20px;'>-$";
                ltRefundOrders.Text += dTotalCancelOrders;
                ltRefundOrders.Text += "</div></div><div style=width: 100%; border-bottom: 1px solid black; clear: both;></div>";


            }


            if (dtResult != null && dtResult.Rows.Count > 0)
            {
                txtBusinessAddress.Text = dtResult.Rows[0]["bAddress"].ToString().Trim();
                txtBusinessName.Text = dtResult.Rows[0]["bName"].ToString().Trim();
                txtBusinessPaymentAddress.Text = dtResult.Rows[0]["cAddress"].ToString().Trim();
                txtBusinessPaymentPhone.Text = dtResult.Rows[0]["cPhone"].ToString().Trim();
                txtBusinessPaymentTitle.Text = dtResult.Rows[0]["cName"].ToString().Trim();
                txtBusinessPhone.Text = dtResult.Rows[0]["bPhone"].ToString().Trim();
                txtFirstPaymentPer.Text = dtResult.Rows[0]["firstPer"].ToString().Trim();


                txtSecondPaymentPer.Text = dtResult.Rows[0]["secondPer"].ToString().Trim();
                txtThirdPaymentPercent.Text = dtResult.Rows[0]["thirdPer"].ToString().Trim();
                txtFirstPaymentDateTime.Text = dtResult.Rows[0]["firstPaymentText"].ToString().Trim();
                txtSecondPaymentDateTime.Text = dtResult.Rows[0]["secondPaymentText"].ToString().Trim();
                txtThirdPaymentDateTime.Text = dtResult.Rows[0]["thirdPaymenText"].ToString().Trim();


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
            else
            {
                string strTempInsertQuerty = "insert into businessInvoiceInfo (bName ,bAddress ,bPhone ,cName ,cAddress ,cPhone ,firstPer ,secondPer ,thirdPer ,dealID ,invoiceType ,createdDate ,createdBy,firstPaymentText,secondPaymentText,thirdPaymenText ) Values ('" + lblBusinessName.Text.Trim().Replace("'", "''") + "' ,'" + lblBusinessAddress.Text.Trim().Replace("'", "''") + "' ,'" + lblBusinessPhone.Text.Trim().Replace("'", "''") + "' ,'" + lblBusinessPaymentTitle.Text.Trim().Replace("'", "''") + "' ,'" + lblBusinessPaymentAddress.Text.Trim().Replace("'", "''") + "' ,'" + lblBusinessPaymentPhone.Text.Trim().Replace("'", "''") + "' ,'" + lblFirstPaymentPer.Text.Trim().Replace("'", "''") + "' ,'" + lblSecondPaymentPer.Text.Trim().Replace("'", "''") + "' ,'" + lblThirdPaymentPercent.Text.Trim().Replace("'", "''") + "' ,'" + Request.QueryString["did"].Trim() + "' ,'" + Request.QueryString["invoicetype"].Trim() + "' ,'" + DateTime.Now + "' ,'" + ViewState["userID"].ToString().Trim() + "' ,'" + lblFirstPaymentDateTime.Text.Trim().Replace("'", "''") + "'  ,'" + lblSecondPaymentDateTime.Text.Trim().Replace("'", "''") + "'  ,'" + lblThirdPaymentDateTime.Text.Trim().Replace("'", "''") + "' )";
                SqlHelper.ExecuteNonQuery(Misc.connStr, CommandType.Text, strTempInsertQuerty);
            }
        }
        catch (Exception ex)
        { }
    }
   
    protected void btnSendFistPayment_Click(object sender, ImageClickEventArgs e)
    {
        try
        {

            BLLDeals objDeal = new BLLDeals();
            objDeal.DealId = Convert.ToInt64(Request.QueryString["did"].ToString().Trim());
            objDeal.dealpayment = 2;
            objDeal.updateDealPaymentType();

            string strTempInsertQuerty = "update businessInvoiceInfo set paymentDone=1, paymentdate='" + DateTime.Now + "',  paymentSend='" + lblSecondPaymentAmount.Text.Trim().Replace("$", "") + "' , modifiedDate='" + DateTime.Now + "', modifiedBy='" + ViewState["userID"].ToString().Trim() + "' where dealID='" + Request.QueryString["did"].Trim() + "' and invoiceType='" + Request.QueryString["invoicetype"].Trim() + "'";
            SqlHelper.ExecuteNonQuery(Misc.connStr, CommandType.Text, strTempInsertQuerty);

            lblLablePayments.Text = "Second Payment sent on " + DateTime.Now.ToString("MM/dd/yyyy");
            imgGridMessage.Visible = true;
            lblMessage.Visible = true;
            lblMessage.Visible = true;
            imgGridMessage.ImageUrl = "~/admin/images/Checked.png";
            lblMessage.Text = "Information has been saved successfully";
            lblMessage.ForeColor = System.Drawing.Color.Black;
            btnSendFistPayment.Visible = false;
            btnAdjustment.Visible = false;
            btnEditFirstPayment.Visible = false;
            btnEditFirstPer.Visible = false;
            btnEditSecondPayment.Visible = false;
            btnEditSecondPer.Visible = false;
            btnEditThirdPayment.Visible = false;
            btnEditThirdPer.Visible = false;

            if (pageGrid.Rows.Count > 0)
            {
                pageGrid.Columns[3].Visible = false;
            }
        }
        catch (Exception ex)
        { 

        }
    }

    protected void btnPaymentToBusiness_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            BLLDeals objDeal = new BLLDeals();
            objDeal.DealId = Convert.ToInt64(Request.QueryString["did"].ToString().Trim());           
            objDeal.DealNote = txtDealNote.Text.Trim();            
            objDeal.updateDealNoteByDealId();

            imgGridMessage.Visible = true;
            lblMessage.Visible = true;
            lblMessage.Visible = true;           
            imgGridMessage.ImageUrl = "~/admin/images/Checked.png";
            lblMessage.Text = "Information has been updated successfully";
            lblMessage.ForeColor = System.Drawing.Color.Black;
        }
        catch (Exception ex)
        {

        }
    }

    protected void btnAdjustment_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            if (Request.QueryString["did"] != null
                && Request.QueryString["did"].ToString().Trim() != ""
                && Request.QueryString["myname"] != null
                && Request.QueryString["myname"].ToString().Trim() != "")
            {
                BLLPayOut objPayout = new BLLPayOut();
                float amount = 0;
                float.TryParse(txtAdjustmentAmount.Text.Trim(), out amount);
                objPayout.poAmount = amount;
                objPayout.poType = "Adjustment";
                objPayout.poDescription = txtAdjustmentDescription.Text.Trim();
                objPayout.poBy = Convert.ToInt64(ViewState["userID"].ToString());
                objPayout.dealId = Convert.ToInt64(Request.QueryString["did"].ToString());
                objPayout.AddNewPayOut();
                txtAdjustmentDescription.Text = "";
                txtAdjustmentAmount.Text = "";
            }
            renderFirstDealInvoice();
            imgGridMessage.Visible = true;
            lblMessage.Visible = true;
            lblMessage.Visible = true;
            imgGridMessage.ImageUrl = "~/admin/images/Checked.png";
            lblMessage.Text = "Information has been updated successfully";
            lblMessage.ForeColor = System.Drawing.Color.Black;
        }
        catch (Exception ex)
        {
            imgGridMessage.Visible = true;
            lblMessage.Visible = true;
            lblMessage.Visible = true;
            imgGridMessage.ImageUrl = "~/admin/images/error.png";
            lblMessage.Text = ex.Message;
            lblMessage.ForeColor = System.Drawing.Color.Black;
        }
    }
    
    protected void btnDownloadCheck_Click(object sender, ImageClickEventArgs e)
    {
        try
        {

            try
            {
                imgGridMessage.Visible = false;
                lblMessage.Visible = false;

                string url = ConfigurationManager.AppSettings["YourSite"] + "/admin/invoicecheck.aspx?did=" + Request.QueryString["did"].Trim() + "&pdfdownload=true&myname=" + Request.QueryString["myname"].Trim() + "&invoicetype=" + Request.QueryString["invoicetype"].Trim();
                Process PDFProcess = new Process();
                string FileName = @" " + AppDomain.CurrentDomain.BaseDirectory + "Images\\ClientData\\" + lblInvoiceNumber.Text.Trim().Replace("INVOICE", "CHECK").Replace("#", "").Replace(" ", "").Replace("-", "") + ".pdf";
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

            string FilePath = AppDomain.CurrentDomain.BaseDirectory + "Images\\ClientData\\" + lblInvoiceNumber.Text.Trim().Replace("INVOICE", "CHECK").Replace("#", "").Replace(" ", "").Replace("-", "") + ".pdf";
            try
            {
                string contentType = "";
                //Get the physical path to the file.
                // string FilePath = AppDomain.CurrentDomain.BaseDirectory + "Images\\ClientData\\" + objEnc.DecryptData("deatailOrder", e.CommandArgument.ToString()) + ".pdf";

                //Set the appropriate ContentType.
                contentType = "Application/pdf";

                //Set the appropriate ContentType.

                Response.ContentType = contentType;
                Response.AppendHeader("content-disposition", "attachment; filename=" + (new FileInfo(lblInvoiceNumber.Text.Trim().Replace("INVOICE", "CHECK").Replace("#", "").Replace(" ", "").Replace("-", "") + ".pdf")).Name);

                //Write the file directly to the HTTP content output stream.
                Response.WriteFile(FilePath);
                Response.End();
            }
            catch
            {
                //To Do
            }

        }
        catch (Exception ex)
        { }
    }

    protected void btnSaveRefund_Click(object sender, ImageClickEventArgs e)
    {
        try
        {

            try
            {
                imgGridMessage.Visible = false;
                lblMessage.Visible = false;

                string url = ConfigurationManager.AppSettings["YourSite"] + "/admin/refundinvoice2.aspx?did=" + Request.QueryString["did"].Trim() + "&pdfdownload=true&myname=" + Request.QueryString["myname"].Trim() + "&invoicetype=" + Request.QueryString["invoicetype"].Trim();
                Process PDFProcess = new Process();
                string FileName = @" " + AppDomain.CurrentDomain.BaseDirectory + "Images\\ClientData\\" + lblInvoiceNumber.Text.Trim().Replace("INVOICE", "REFUND").Replace("#", "").Replace(" ", "").Replace("-", "") + ".pdf";
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

            string FilePath = AppDomain.CurrentDomain.BaseDirectory + "Images\\ClientData\\" + lblInvoiceNumber.Text.Trim().Replace("INVOICE", "REFUND").Replace("#", "").Replace(" ", "").Replace("-", "") + ".pdf";
            try
            {
                string contentType = "";
                //Get the physical path to the file.
                // string FilePath = AppDomain.CurrentDomain.BaseDirectory + "Images\\ClientData\\" + objEnc.DecryptData("deatailOrder", e.CommandArgument.ToString()) + ".pdf";

                //Set the appropriate ContentType.
                contentType = "Application/pdf";

                //Set the appropriate ContentType.

                Response.ContentType = contentType;
                Response.AppendHeader("content-disposition", "attachment; filename=" + (new FileInfo(lblInvoiceNumber.Text.Trim().Replace("INVOICE", "REFUND").Replace("#", "").Replace(" ", "").Replace("-", "") + ".pdf")).Name);

                //Write the file directly to the HTTP content output stream.
                Response.WriteFile(FilePath);
                Response.End();
            }
            catch
            {
                //To Do
            }

        }
        catch (Exception ex)
        { }
    }
   
    protected void btnSaveAndDownlaodPDF_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
             double dAmountPercent = Convert.ToDouble(txtFirstPaymentPer.Text.Trim()) + Convert.ToDouble(txtSecondPaymentPer.Text.Trim()) + Convert.ToDouble(txtThirdPaymentPercent.Text.Trim());
            if (dAmountPercent == 100)
            {
                try
                {
                    imgGridMessage.Visible = false;
                    lblMessage.Visible = false;

                    string url = ConfigurationManager.AppSettings["YourSite"] + "/admin/invoice2.aspx?did=" + Request.QueryString["did"].Trim() + "&pdfdownload=true&myname=" + Request.QueryString["myname"].Trim() + "&invoicetype=" + Request.QueryString["invoicetype"].Trim();
                    Process PDFProcess = new Process();
                    string FileName = @" " + AppDomain.CurrentDomain.BaseDirectory + "Images\\ClientData\\" + lblInvoiceNumber.Text.Trim().Replace("#", "").Replace(" ", "").Replace("-", "") + ".pdf";
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

                string FilePath = AppDomain.CurrentDomain.BaseDirectory + "Images\\ClientData\\" + lblInvoiceNumber.Text.Trim().Replace("#", "").Replace(" ", "").Replace("-", "") + ".pdf";
                try
                {
                    string contentType = "";
                    //Get the physical path to the file.
                    // string FilePath = AppDomain.CurrentDomain.BaseDirectory + "Images\\ClientData\\" + objEnc.DecryptData("deatailOrder", e.CommandArgument.ToString()) + ".pdf";

                    //Set the appropriate ContentType.
                    contentType = "Application/pdf";

                    //Set the appropriate ContentType.

                    Response.ContentType = contentType;
                    Response.AppendHeader("content-disposition", "attachment; filename=" + (new FileInfo(lblInvoiceNumber.Text.Trim().Replace("#", "").Replace(" ", "").Replace("-", "") + ".pdf")).Name);

                    //Write the file directly to the HTTP content output stream.
                    Response.WriteFile(FilePath);
                    Response.End();
                }
                catch
                {
                    //To Do
                }
            }
            else
            {
                imgGridMessage.Visible = true;
                lblMessage.Visible = true;
                lblMessage.Text = "Your payment percentage is total " + dAmountPercent + " which is not correct. Please correct it first.";
                lblMessage.ForeColor = System.Drawing.Color.Red;
            }
        }
        catch (Exception ex)
        { }
    }


    #region All Edit Button Click events
    protected void btnEditBName_Click(object sender, ImageClickEventArgs e)
    {
        btnEditBName.Visible = false;
        btnSaveBName.Visible = true;
        btnCloseBName.Visible = true;
        lblBusinessName.Visible = false;
        txtBusinessName.Visible = true;
    }

    protected void btnEditBAddress_Click(object sender, ImageClickEventArgs e)
    {
        btnEditBAddress.Visible = false;
        btnSaveBAddress.Visible = true;
        btnCloseBAddress.Visible = true;
        lblBusinessAddress.Visible = false;
        txtBusinessAddress.Visible = true;
    }

    protected void btnEditBPhone_Click(object sender, ImageClickEventArgs e)
    {
        btnEditBPhone.Visible = false;
        btnSaveBPhone.Visible = true;
        btnCloseBPhone.Visible = true;
        lblBusinessPhone.Visible = false;
        txtBusinessPhone.Visible = true;
    }

    protected void btnEditCName_Click(object sender, ImageClickEventArgs e)
    {
        btnEditCName.Visible = false;
        btnSaveCName.Visible = true;
        btnCloseCName.Visible = true;
        lblBusinessPaymentTitle.Visible = false;
        txtBusinessPaymentTitle.Visible = true;
    }

    protected void btnEditCAddress_Click(object sender, ImageClickEventArgs e)
    {
        btnEditCAddress.Visible = false;
        btnSaveCAddress.Visible = true;
        btnCloseCAddress.Visible = true;
        lblBusinessPaymentAddress.Visible = false;
        txtBusinessPaymentAddress.Visible = true;
    }

    protected void btnEditCPhone_Click(object sender, ImageClickEventArgs e)
    {
        btnEditCPhone.Visible = false;
        btnSaveCPhone.Visible = true;
        btnCloseCPhone.Visible = true;
        lblBusinessPaymentPhone.Visible = false;
        txtBusinessPaymentPhone.Visible = true;
    }

    protected void btnEditFirstPer_Click(object sender, ImageClickEventArgs e)
    {
        btnEditFirstPer.Visible = false;
        btnSaveFirstPer.Visible = true;
        btnCloseFirstPer.Visible = true;
        lblFirstPaymentPer.Visible = false;
        txtFirstPaymentPer.Visible = true;
    }

    protected void btnEditSecondPer_Click(object sender, ImageClickEventArgs e)
    {
        btnEditSecondPer.Visible = false;
        btnSaveSecondPer.Visible = true;
        btnCloseSecondPer.Visible = true;
        lblSecondPaymentPer.Visible = false;
        txtSecondPaymentPer.Visible = true;
    }

    protected void btnEditThirdPer_Click(object sender, ImageClickEventArgs e)
    {
        btnEditThirdPer.Visible = false;
        btnSaveThirdPer.Visible = true;
        btnCloseThirdPer.Visible = true;
        lblThirdPaymentPercent.Visible = false;
        txtThirdPaymentPercent.Visible = true;
    }

    protected void btnEditFirstPayment_Click(object sender, ImageClickEventArgs e)
    {
        btnEditFirstPayment.Visible = false;
        btnSaveFirstPayment.Visible = true;
        btnCloseFirstPayment.Visible = true;
        lblFirstPaymentDateTime.Visible = false;
        txtFirstPaymentDateTime.Visible = true;
    }

    protected void btnEditSecondPayment_Click(object sender, ImageClickEventArgs e)
    {
        btnEditSecondPayment.Visible = false;
        btnSaveSecondPayment.Visible = true;
        btnCloseSecondPayment.Visible = true;
        lblSecondPaymentDateTime.Visible = false;
        txtSecondPaymentDateTime.Visible = true;       
    }

    protected void btnEditThirdPayment_Click(object sender, ImageClickEventArgs e)
    {
        btnEditThirdPayment.Visible = false;
        btnSaveThirdPayment.Visible = true;
        btnCloseThirdPayment.Visible = true;
        lblThirdPaymentDateTime.Visible = false;
        txtThirdPaymentDateTime.Visible = true;
    }
    
    #endregion


    #region All Close Button Click events
    protected void btnCloseBName_Click(object sender, ImageClickEventArgs e)
    {
        btnEditBName.Visible = true;
        btnSaveBName.Visible = false;
        btnCloseBName.Visible = false;
        lblBusinessName.Visible = true;
        txtBusinessName.Visible = false;
        txtBusinessName.Text = lblBusinessName.Text;
    }

    protected void btnCloseBAddress_Click(object sender, ImageClickEventArgs e)
    {
        btnEditBAddress.Visible = true;
        btnSaveBAddress.Visible = false;
        btnCloseBAddress.Visible = false;
        lblBusinessAddress.Visible = true;
        txtBusinessAddress.Visible = false;
        txtBusinessAddress.Text = lblBusinessAddress.Text;
    }

    protected void btnCloseBPhone_Click(object sender, ImageClickEventArgs e)
    {
        btnEditBPhone.Visible = true;
        btnSaveBPhone.Visible = false;
        btnCloseBPhone.Visible = false;
        lblBusinessPhone.Visible = true;
        txtBusinessPhone.Visible = false;
        txtBusinessPhone.Text = lblBusinessPhone.Text;
    }

    protected void btnCloseCName_Click(object sender, ImageClickEventArgs e)
    {
        btnEditCName.Visible = true;
        btnSaveCName.Visible = false;
        btnCloseCName.Visible = false;
        lblBusinessPaymentTitle.Visible = true;
        txtBusinessPaymentTitle.Visible = false;
        txtBusinessPaymentTitle.Text = lblBusinessPaymentTitle.Text;
    }

    protected void btnCloseCAddress_Click(object sender, ImageClickEventArgs e)
    {
        btnEditCAddress.Visible = true;
        btnSaveCAddress.Visible = false;
        btnCloseCAddress.Visible = false;
        lblBusinessPaymentAddress.Visible = true;
        txtBusinessPaymentAddress.Visible = false;
        txtBusinessPaymentAddress.Text = lblBusinessPaymentAddress.Text;
    }

    protected void btnCloseCPhone_Click(object sender, ImageClickEventArgs e)
    {
        btnEditCPhone.Visible = true;
        btnSaveCPhone.Visible = false;
        btnCloseCPhone.Visible = false;
        lblBusinessPaymentPhone.Visible = true;
        txtBusinessPaymentPhone.Visible = false;
        txtBusinessPaymentPhone.Text = lblBusinessPaymentPhone.Text;
    }

    protected void btnCloseFirstPer_Click(object sender, ImageClickEventArgs e)
    {
        btnEditFirstPer.Visible = true;
        btnSaveFirstPer.Visible = false;
        btnCloseFirstPer.Visible = false;
        lblFirstPaymentPer.Visible = true;
        txtFirstPaymentPer.Visible = false;
        txtFirstPaymentPer.Text = lblFirstPaymentPer.Text;
        if (ViewState["dTotalPayoutToBusiness"] != null)
        {
            lblFirstPaymentAmount.Text = Math.Round((Convert.ToDouble(lblFirstPaymentPer.Text) / 100) * Convert.ToDouble(ViewState["dTotalPayoutToBusiness"].ToString().Trim()), 2, MidpointRounding.AwayFromZero).ToString();
        }
    }

    protected void btnCloseSecondPer_Click(object sender, ImageClickEventArgs e)
    {
        btnEditSecondPer.Visible = true;
        btnSaveSecondPer.Visible = false;
        btnCloseSecondPer.Visible = false;
        lblSecondPaymentPer.Visible = true;
        txtSecondPaymentPer.Visible = false;
        txtSecondPaymentPer.Text = lblSecondPaymentPer.Text;
        if (ViewState["dTotalPayoutToBusiness"] != null)
        {
            lblSecondPaymentAmount.Text = Math.Round((Convert.ToDouble(lblSecondPaymentPer.Text) / 100) * Convert.ToDouble(ViewState["dTotalPayoutToBusiness"].ToString().Trim()), 2, MidpointRounding.AwayFromZero).ToString();
        }
    }

    protected void btnCloseThirdPer_Click(object sender, ImageClickEventArgs e)
    {
        btnEditThirdPer.Visible = true;
        btnSaveThirdPer.Visible = false;
        btnCloseThirdPer.Visible = false;
        lblThirdPaymentPercent.Visible = true;
        txtThirdPaymentPercent.Visible = false;
        txtThirdPaymentPercent.Text = lblThirdPaymentPercent.Text;
        if (ViewState["dTotalPayoutToBusiness"] != null)
        {
            lblThirdPaymentAmount.Text = Math.Round((Convert.ToDouble(lblThirdPaymentPercent.Text) / 100) * Convert.ToDouble(ViewState["dTotalPayoutToBusiness"].ToString().Trim()), 2, MidpointRounding.AwayFromZero).ToString();
        }
    }

    protected void btnCloseFirstPayment_Click(object sender, ImageClickEventArgs e)
    {
        btnEditFirstPayment.Visible = true;
        btnSaveFirstPayment.Visible = false;
        btnCloseFirstPayment.Visible = false;
        lblFirstPaymentDateTime.Visible = true;
        txtFirstPaymentDateTime.Visible = false;
        txtFirstPaymentDateTime.Text = lblFirstPaymentDateTime.Text;
    }

    protected void btnCloseSecondPayment_Click(object sender, ImageClickEventArgs e)
    {
        btnEditSecondPayment.Visible = true;
        btnSaveSecondPayment.Visible = false;
        btnCloseSecondPayment.Visible = false;
        lblSecondPaymentDateTime.Visible = true;
        txtSecondPaymentDateTime.Visible = false;
        txtSecondPaymentDateTime.Text = lblSecondPaymentDateTime.Text;     
    }

    protected void btnCloseThirdPayment_Click(object sender, ImageClickEventArgs e)
    {
        btnEditThirdPayment.Visible = true;
        btnSaveThirdPayment.Visible = false;
        btnCloseThirdPayment.Visible = false;
        lblThirdPaymentDateTime.Visible = true;
        txtThirdPaymentDateTime.Visible = false;
        txtThirdPaymentDateTime.Text = lblThirdPaymentDateTime.Text;
    }
    #endregion


    #region All Save Button Click events
    protected void btnSaveBName_Click(object sender, ImageClickEventArgs e)
    {
        btnEditBName.Visible = true;
        btnSaveBName.Visible = false;
        btnCloseBName.Visible = false;
        lblBusinessName.Visible = true;
        txtBusinessName.Visible = false;
        lblBusinessName.Text = txtBusinessName.Text;
        string strTempInsertQuerty = "update businessInvoiceInfo set bName='" + txtBusinessName.Text.Trim().Replace("'","''") + "', modifiedDate='" + DateTime.Now  + "', modifiedBy='" + ViewState["userID"].ToString().Trim() + "' where dealID='" + Request.QueryString["did"].Trim() + "' and invoiceType='" + Request.QueryString["invoicetype"].Trim() + "'";
        SqlHelper.ExecuteNonQuery(Misc.connStr, CommandType.Text, strTempInsertQuerty);  
    }

    protected void btnSaveBAddress_Click(object sender, ImageClickEventArgs e)
    {
        btnEditBAddress.Visible = true;
        btnSaveBAddress.Visible = false;
        btnCloseBAddress.Visible = false;
        lblBusinessAddress.Visible = true;
        txtBusinessAddress.Visible = false;
        lblBusinessAddress.Text = txtBusinessAddress.Text;
        string strTempInsertQuerty = "update businessInvoiceInfo set bAddress='" + txtBusinessAddress.Text.Trim().Replace("'","''") + "', modifiedDate='" + DateTime.Now  + "', modifiedBy='" + ViewState["userID"].ToString().Trim() + "' where dealID='" + Request.QueryString["did"].Trim() + "' and invoiceType='" + Request.QueryString["invoicetype"].Trim() + "'";
        SqlHelper.ExecuteNonQuery(Misc.connStr, CommandType.Text, strTempInsertQuerty); 
    }

    protected void btnSaveBPhone_Click(object sender, ImageClickEventArgs e)
    {
        btnEditBPhone.Visible = true;
        btnSaveBPhone.Visible = false;
        btnCloseBPhone.Visible = false;
        lblBusinessPhone.Visible = true;
        txtBusinessPhone.Visible = false;
        lblBusinessPhone.Text = txtBusinessPhone.Text;
        string strTempInsertQuerty = "update businessInvoiceInfo set bPhone='" + txtBusinessPhone.Text.Trim().Replace("'","''") + "', modifiedDate='" + DateTime.Now  + "', modifiedBy='" + ViewState["userID"].ToString().Trim() + "' where dealID='" + Request.QueryString["did"].Trim() + "' and invoiceType='" + Request.QueryString["invoicetype"].Trim() + "'";
        SqlHelper.ExecuteNonQuery(Misc.connStr, CommandType.Text, strTempInsertQuerty); 
    }

    protected void btnSaveCName_Click(object sender, ImageClickEventArgs e)
    {
        btnEditCName.Visible = true;
        btnSaveCName.Visible = false;
        btnCloseCName.Visible = false;
        lblBusinessPaymentTitle.Visible = true;
        txtBusinessPaymentTitle.Visible = false;
        lblBusinessPaymentTitle.Text = txtBusinessPaymentTitle.Text;
        string strTempInsertQuerty = "update businessInvoiceInfo set cName='" + txtBusinessPaymentTitle.Text.Trim().Replace("'","''") + "', modifiedDate='" + DateTime.Now  + "', modifiedBy='" + ViewState["userID"].ToString().Trim() + "' where dealID='" + Request.QueryString["did"].Trim() + "' and invoiceType='" + Request.QueryString["invoicetype"].Trim() + "'";
        SqlHelper.ExecuteNonQuery(Misc.connStr, CommandType.Text, strTempInsertQuerty); 
    }

    protected void btnSaveCAddress_Click(object sender, ImageClickEventArgs e)
    {
        btnEditCAddress.Visible = true;
        btnSaveCAddress.Visible = false;
        btnCloseCAddress.Visible = false;
        lblBusinessPaymentAddress.Visible = true;
        txtBusinessPaymentAddress.Visible = false;
        lblBusinessPaymentAddress.Text = txtBusinessPaymentAddress.Text;
        string strTempInsertQuerty = "update businessInvoiceInfo set cAddress='" + txtBusinessPaymentAddress.Text.Trim().Replace("'","''") + "', modifiedDate='" + DateTime.Now  + "', modifiedBy='" + ViewState["userID"].ToString().Trim() + "' where dealID='" + Request.QueryString["did"].Trim() + "' and invoiceType='" + Request.QueryString["invoicetype"].Trim() + "'";
        SqlHelper.ExecuteNonQuery(Misc.connStr, CommandType.Text, strTempInsertQuerty);
    }

    protected void btnSaveCPhone_Click(object sender, ImageClickEventArgs e)
    {
        btnEditCPhone.Visible = true;
        btnSaveCPhone.Visible = false;
        btnCloseCPhone.Visible = false;
        lblBusinessPaymentPhone.Visible = true;
        txtBusinessPaymentPhone.Visible = false;
        lblBusinessPaymentPhone.Text = txtBusinessPaymentPhone.Text;
        string strTempInsertQuerty = "update businessInvoiceInfo set cPhone='" + txtBusinessPaymentPhone.Text.Trim().Replace("'","''") + "', modifiedDate='" + DateTime.Now  + "', modifiedBy='" + ViewState["userID"].ToString().Trim() + "' where dealID='" + Request.QueryString["did"].Trim() + "' and invoiceType='" + Request.QueryString["invoicetype"].Trim() + "'";
        SqlHelper.ExecuteNonQuery(Misc.connStr, CommandType.Text, strTempInsertQuerty);
    }

    protected void btnSaveFirstPer_Click(object sender, ImageClickEventArgs e)
    {
        btnEditFirstPer.Visible = true;
        btnSaveFirstPer.Visible = false;
        btnCloseFirstPer.Visible = false;
        lblFirstPaymentPer.Visible = true;
        txtFirstPaymentPer.Visible = false;
        lblFirstPaymentPer.Text = txtFirstPaymentPer.Text;
        if (ViewState["dTotalPayoutToBusiness"] != null)
        {
            lblFirstPaymentAmount.Text = Math.Round((Convert.ToDouble(lblFirstPaymentPer.Text) / 100) * Convert.ToDouble(ViewState["dTotalPayoutToBusiness"].ToString().Trim()), 2, MidpointRounding.AwayFromZero).ToString();
        }

        string strTempInsertQuerty = "update businessInvoiceInfo set firstPer='" + lblFirstPaymentPer.Text.Trim().Replace("'","''") + "', modifiedDate='" + DateTime.Now  + "', modifiedBy='" + ViewState["userID"].ToString().Trim() + "' where dealID='" + Request.QueryString["did"].Trim() + "' and invoiceType='" + Request.QueryString["invoicetype"].Trim() + "'";
        SqlHelper.ExecuteNonQuery(Misc.connStr, CommandType.Text, strTempInsertQuerty);
    }

    protected void btnSaveSecondPer_Click(object sender, ImageClickEventArgs e)
    {
        btnEditSecondPer.Visible = true;
        btnSaveSecondPer.Visible = false;
        btnCloseSecondPer.Visible = false;
        lblSecondPaymentPer.Visible = true;
        txtSecondPaymentPer.Visible = false;
        lblSecondPaymentPer.Text = txtSecondPaymentPer.Text;
        if (ViewState["dTotalPayoutToBusiness"] != null)
        {
            lblSecondPaymentAmount.Text = Math.Round((Convert.ToDouble(lblSecondPaymentPer.Text) / 100) * Convert.ToDouble(ViewState["dTotalPayoutToBusiness"].ToString().Trim()), 2, MidpointRounding.AwayFromZero).ToString();
        }

        string strTempInsertQuerty = "update businessInvoiceInfo set secondPer='" + lblSecondPaymentPer.Text.Trim().Replace("'","''") + "', modifiedDate='" + DateTime.Now  + "', modifiedBy='" + ViewState["userID"].ToString().Trim() + "' where dealID='" + Request.QueryString["did"].Trim() + "' and invoiceType='" + Request.QueryString["invoicetype"].Trim() + "'";
        SqlHelper.ExecuteNonQuery(Misc.connStr, CommandType.Text, strTempInsertQuerty);
    }

    protected void btnSaveThirdPer_Click(object sender, ImageClickEventArgs e)
    {
        btnEditThirdPer.Visible = true;
        btnSaveThirdPer.Visible = false;
        btnCloseThirdPer.Visible = false;
        lblThirdPaymentPercent.Visible = true;
        txtThirdPaymentPercent.Visible = false;
        lblThirdPaymentPercent.Text = txtThirdPaymentPercent.Text;
        if (ViewState["dTotalPayoutToBusiness"] != null)
        {
            lblThirdPaymentAmount.Text = Math.Round((Convert.ToDouble(lblThirdPaymentPercent.Text) / 100) * Convert.ToDouble(ViewState["dTotalPayoutToBusiness"].ToString().Trim()), 2, MidpointRounding.AwayFromZero).ToString();
        }

        string strTempInsertQuerty = "update businessInvoiceInfo set thirdPer='" + lblThirdPaymentPercent.Text.Trim().Replace("'","''") + "', modifiedDate='" + DateTime.Now  + "', modifiedBy='" + ViewState["userID"].ToString().Trim() + "' where dealID='" + Request.QueryString["did"].Trim() + "' and invoiceType='" + Request.QueryString["invoicetype"].Trim() + "'";
        SqlHelper.ExecuteNonQuery(Misc.connStr, CommandType.Text, strTempInsertQuerty);
    }

    protected void btnSaveFirstPayment_Click(object sender, ImageClickEventArgs e)
    {
        btnEditFirstPayment.Visible = true;
        btnSaveFirstPayment.Visible = false;
        btnCloseFirstPayment.Visible = false;
        lblFirstPaymentDateTime.Visible = true;
        txtFirstPaymentDateTime.Visible = false;
        lblFirstPaymentDateTime.Text = txtFirstPaymentDateTime.Text;
        string strTempInsertQuerty = "update businessInvoiceInfo set firstPaymentText='" + lblFirstPaymentDateTime.Text.Trim().Replace("'","''") + "', modifiedDate='" + DateTime.Now + "', modifiedBy='" + ViewState["userID"].ToString().Trim() + "' where dealID='" + Request.QueryString["did"].Trim() + "' and invoiceType='" + Request.QueryString["invoicetype"].Trim() + "'";
        SqlHelper.ExecuteNonQuery(Misc.connStr, CommandType.Text, strTempInsertQuerty);
    }

    protected void btnSaveSecondPayment_Click(object sender, ImageClickEventArgs e)
    {
        btnEditSecondPayment.Visible = true;
        btnSaveSecondPayment.Visible = false;
        btnCloseSecondPayment.Visible = false;
        lblSecondPaymentDateTime.Visible = true;
        txtSecondPaymentDateTime.Visible = false;
        lblSecondPaymentDateTime.Text = txtSecondPaymentDateTime.Text;
        string strTempInsertQuerty = "update businessInvoiceInfo set secondPaymentText='" + lblSecondPaymentDateTime.Text.Trim().Replace("'","''") + "', modifiedDate='" + DateTime.Now + "', modifiedBy='" + ViewState["userID"].ToString().Trim() + "' where dealID='" + Request.QueryString["did"].Trim() + "' and invoiceType='" + Request.QueryString["invoicetype"].Trim() + "'";
        SqlHelper.ExecuteNonQuery(Misc.connStr, CommandType.Text, strTempInsertQuerty);
    }

    protected void btnSaveThirdPayment_Click(object sender, ImageClickEventArgs e)
    {
        btnEditThirdPayment.Visible = true;
        btnSaveThirdPayment.Visible = false;
        btnCloseThirdPayment.Visible = false;
        lblThirdPaymentDateTime.Visible = true;
        txtThirdPaymentDateTime.Visible = false;
        lblThirdPaymentDateTime.Text = txtThirdPaymentDateTime.Text;
        string strTempInsertQuerty = "update businessInvoiceInfo set thirdPaymentText='" + lblThirdPaymentDateTime.Text.Trim().Replace("'","''") + "', modifiedDate='" + DateTime.Now + "', modifiedBy='" + ViewState["userID"].ToString().Trim() + "' where dealID='" + Request.QueryString["did"].Trim() + "' and invoiceType='" + Request.QueryString["invoicetype"].Trim() + "'";
        SqlHelper.ExecuteNonQuery(Misc.connStr, CommandType.Text, strTempInsertQuerty);
    }

    #endregion


}
