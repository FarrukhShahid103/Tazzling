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
using System.Threading;
using System.Security.Cryptography;
using System.Text;
using com.optimalpayments.webservices;
using CampaignMonitorAPIWrapper;
using SQLHelper;
public partial class Default4 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        DataTable dtCities = Misc.search("select * from city");
        if (dtCities != null && dtCities.Rows.Count > 0)
        {
            for (int i = 0; i < dtCities.Rows.Count; i++)
            {
               /* if (dtCities.Rows[i]["cityId"].ToString().Trim() != "337"
                    && dtCities.Rows[i]["cityId"].ToString().Trim() != "338"
                    && dtCities.Rows[i]["cityId"].ToString().Trim() != "1376"
                    && dtCities.Rows[i]["cityId"].ToString().Trim() != "1710"
                    && dtCities.Rows[i]["cityId"].ToString().Trim() != "1720"
                    && dtCities.Rows[i]["cityId"].ToString().Trim() != "1709"
                    && dtCities.Rows[i]["cityId"].ToString().Trim() != "1726"
                    && dtCities.Rows[i]["cityId"].ToString().Trim() != "1712"
                    && dtCities.Rows[i]["cityId"].ToString().Trim() != "1713"
                    )
                {
                    CampaignMonitorAPIWrapper.Result<string> result = CampaignMonitorAPIWrapper.List.Create("544aa0620ffc18cb21229986fbf79123", "b88559e05d78a8a5f93970f4b08c82d2", dtCities.Rows[i]["cityName"].ToString().Trim() + " Daily Deal", "", false, "");
                    if (result.Message.ToLower() == "success")
                    {
                        SqlHelper.ExecuteNonQuery(Misc.connStr, CommandType.Text, "update city set campaignMonitorIDs='" + result.ReturnObject.Trim() + "' where cityId=" + dtCities.Rows[i]["cityId"].ToString().Trim());
                    }
                }*/
            }
        }

        

        #region Payment Request
        //Payment();
        #endregion

        #region Authenticatoin Request
       // Authentication();
        #endregion

        #region Credit
        //Credit();
        #endregion

        #region Settlement
       // Settlement();
        #endregion

    }
    private void Settlement()
    {
        CCPostAuthRequestV1 ccPostAuthRequest = new CCPostAuthRequestV1();
        ccPostAuthRequest.confirmationNumber = "129472667";
        MerchantAccountV1 merchantAccount = new MerchantAccountV1();
        merchantAccount.accountNum = "89994681";
        merchantAccount.storeID = "test";
        merchantAccount.storePwd = "test";
        ccPostAuthRequest.merchantAccount = merchantAccount;
        ccPostAuthRequest.merchantRefNum = "Ref-12345";
        ccPostAuthRequest.amount = "10.00";
        // Perform the Web Service call for the Settlement
        CreditCardServiceV1 ccService = new CreditCardServiceV1();
        CCTxnResponseV1 ccTxnResponse = ccService.ccSettlement(ccPostAuthRequest);
        // Print out the result
        String responseTxt = ccTxnResponse.code + " - " + ccTxnResponse.decision +
        " - " + ccTxnResponse.description;
        responseTxt += "Details:" + Environment.NewLine;
        if (ccTxnResponse.detail != null)
        {
            for (int i = 0; i < ccTxnResponse.detail.Length; i++)
            {
                responseTxt += " - " + ccTxnResponse.detail[i].tag + " - " +
                ccTxnResponse.detail[i].value + Environment.NewLine;
            }
        }
        lblMessage.Text = responseTxt.Replace("\n", "<br>");
        responseTxt = responseTxt.Replace("\n", Environment.NewLine);
        System.Console.WriteLine(responseTxt);
        if (DecisionV1.ACCEPTED.Equals(ccTxnResponse.decision))
        {
            System.Console.WriteLine("Transaction Successful.");
        }
        else
        {
            System.Console.WriteLine("Transaction Failed with decision: " +
            ccTxnResponse.decision);
        }
    }

    private void Credit()
    {
        //Prepare the call to the Credit Card Web Service
        CCPostAuthRequestV1 ccPostAuthRequest = new CCPostAuthRequestV1();
        ccPostAuthRequest.confirmationNumber = "129469177";
        MerchantAccountV1 merchantAccount = new MerchantAccountV1();
        merchantAccount.accountNum = "89994681";
        merchantAccount.storeID = "test";
        merchantAccount.storePwd = "test";
        ccPostAuthRequest.merchantAccount = merchantAccount;
        ccPostAuthRequest.merchantRefNum = Guid.NewGuid().ToString();
        ccPostAuthRequest.amount = "5.00";
        // Perform the Web Service call for the Settlement
        CreditCardServiceV1 ccService = new CreditCardServiceV1();
        CCTxnResponseV1 ccTxnResponse = ccService.ccCredit(ccPostAuthRequest);
        // Print out the result
        String responseTxt = ccTxnResponse.code + " - " + ccTxnResponse.decision +
        " - " + ccTxnResponse.description;
        responseTxt += "Details:" + Environment.NewLine;
        if (ccTxnResponse.detail != null)
        {
            for (int i = 0; i < ccTxnResponse.detail.Length; i++)
            {
                responseTxt += " - " + ccTxnResponse.detail[i].tag + " - " +
                ccTxnResponse.detail[i].value + Environment.NewLine;
            }
        }
        lblMessage.Text = responseTxt.Replace("\n", "<br>");
        responseTxt = responseTxt.Replace("\n", Environment.NewLine);
        System.Console.WriteLine(responseTxt);
        if (DecisionV1.ACCEPTED.Equals(ccTxnResponse.decision))
        {
            System.Console.WriteLine("Transaction Successful.");
        }
        else
        {
            System.Console.WriteLine("Transaction Failed with decision: " +
            ccTxnResponse.decision);
        }
    }

    private void Authentication()
    {
        //Prepare the call to the Credit Card Web Service
        CCAuthRequestV1 ccAuthRequest = new CCAuthRequestV1();
        MerchantAccountV1 merchantAccount = new MerchantAccountV1();
        merchantAccount.accountNum = "89994681";

        merchantAccount.storeID = "test";
        merchantAccount.storePwd = "test";

        
        
        
        ccAuthRequest.merchantAccount = merchantAccount;
        ccAuthRequest.merchantRefNum = Guid.NewGuid().ToString();
        ccAuthRequest.amount = "10.00";
        CardV1 card = new CardV1();
        card.cardNum = "4715320629000001";
        CardExpiryV1 cardExpiry = new CardExpiryV1();
        cardExpiry.month = 11;
        cardExpiry.year = 2012;
        card.cardExpiry = cardExpiry;
        //card.cardType = CardTypeV1.VI;
        card.cardTypeSpecified = true;
        card.cvdIndicator = 1;
        card.cvdIndicatorSpecified = true;
        card.cvd = "111";
        ccAuthRequest.card = card;
        BillingDetailsV1 billingDetails = new BillingDetailsV1();
        billingDetails.cardPayMethod = CardPayMethodV1.WEB; //WEB = Card Number Provided
        billingDetails.cardPayMethodSpecified = true;
        billingDetails.firstName = "Jane";
        billingDetails.lastName = "Jones";
        billingDetails.street = "123 Main Street";
        billingDetails.city = "LA";
        billingDetails.Item = (object)StateV1.BC; // California
        billingDetails.country = CountryV1.CA; // United States
        billingDetails.countrySpecified = true;
        billingDetails.zip = "90210";
        billingDetails.phone = "555-555-5555";
        billingDetails.email = "janejones@emailserver.com";
        ccAuthRequest.billingDetails = billingDetails;
        ccAuthRequest.customerIP = "127.0.0.1";
        ccAuthRequest.productType = ProductTypeV1.M;

        ccAuthRequest.productTypeSpecified = true;
        // Perform the Web Services call for the purchase
        CreditCardServiceV1 ccService = new CreditCardServiceV1();
        CCTxnResponseV1 ccTxnResponse = ccService.ccPurchase(ccAuthRequest);
        // Print out the result
        String responseTxt = ccTxnResponse.code + " - " + ccTxnResponse.decision + " - "
        + ccTxnResponse.description + Environment.NewLine;
        if (ccTxnResponse.detail != null)
        {
            for (int i = 0; i < ccTxnResponse.detail.Length; i++)
            {
                responseTxt += " - " + ccTxnResponse.detail[i].tag + " - " +
                ccTxnResponse.detail[i].value + Environment.NewLine;
            }
        }
        lblMessage.Text = responseTxt.Replace("\n", "<br>");
        responseTxt = responseTxt.Replace("\n", Environment.NewLine);
        System.Console.WriteLine(responseTxt);
        if (DecisionV1.ACCEPTED.Equals(ccTxnResponse.decision))
        {
            System.Console.WriteLine("Transaction Successful.");
        }
        else
        {
            System.Console.WriteLine("Transaction Failed with decision: " +
            ccTxnResponse.decision);
        }
    }

    private void Payment()
    {
        try
        {
            //Prepare the call to the Credit Card Web Service
            CCPaymentRequestV1 ccPaymentRequest = new CCPaymentRequestV1();
            MerchantAccountV1 merchantAccount = new MerchantAccountV1();

            //merchantAccount.accountNum = "12345678";
            //merchantAccount.storeID = "myStoreID";
            //merchantAccount.storePwd = "myStorePWD";
            //ccPaymentRequest.merchantAccount = merchantAccount;
            //ccPaymentRequest.merchantRefNum = "Ref-12345";
            //ccPaymentRequest.amount = "10.00";
            //CardV1 card = new CardV1();
            //card.cardNum = "4653111111111111";
            //CardExpiryV1 cardExpiry = new CardExpiryV1();
            //cardExpiry.month = 11;
            //cardExpiry.year = 2006;

            merchantAccount.accountNum = "89994681";

            merchantAccount.storeID = "test";
            merchantAccount.storePwd = "test";
            ccPaymentRequest.merchantAccount = merchantAccount;
            ccPaymentRequest.merchantRefNum = Guid.NewGuid().ToString();
            ccPaymentRequest.amount = "10.00";
            CardV1 card = new CardV1();

            card.cardNum = "4715320629000001";
            CardExpiryV1 cardExpiry = new CardExpiryV1();
            cardExpiry.month = 11;
            cardExpiry.year = 2012;

            card.cardExpiry = cardExpiry;
            card.cardType = CardTypeV1.VI;
            card.cardTypeSpecified = true;
            card.cvdIndicator = 1;
            card.cvdIndicatorSpecified = true;
            card.cvd = "111";
            ccPaymentRequest.card = card;
            BillingDetailsV1 billingDetails = new BillingDetailsV1();
            billingDetails.cardPayMethod = CardPayMethodV1.WEB; //WEB = Card Number Provided
            billingDetails.cardPayMethodSpecified = true;
            billingDetails.firstName = "Jane";
            billingDetails.lastName = "Jones";
            billingDetails.street = "123 Main Street";
            billingDetails.city = "LA";
            billingDetails.Item = (object)StateV1.CA; // California
            billingDetails.country = CountryV1.US; // United States
            billingDetails.countrySpecified = true;
            billingDetails.zip = "90210";
            billingDetails.phone = "555-555-5555";
            billingDetails.email = "janejones@emailserver.com";
            ccPaymentRequest.billingDetails = billingDetails;
            // Perform the Web Services call for the payment request
            CreditCardServiceV1 ccService = new CreditCardServiceV1();
            CCTxnResponseV1 ccTxnResponse = ccService.ccPayment(ccPaymentRequest);
            // Print out the result
            String responseTxt = ccTxnResponse.code + " - " + ccTxnResponse.decision + " - "
                + ccTxnResponse.description;
            responseTxt += "Details:" + Environment.NewLine;
            if (ccTxnResponse.detail != null)
            {
                for (int i = 0; i < ccTxnResponse.detail.Length; i++)
                {
                    responseTxt += " - " + ccTxnResponse.detail[i].tag + " - " +
                    ccTxnResponse.detail[i].value + Environment.NewLine;
                }
            }
            lblMessage.Text = responseTxt.Replace("\n", "<br>");
            responseTxt = responseTxt.Replace("\n", Environment.NewLine);
            System.Console.WriteLine(responseTxt);
            if (DecisionV1.ACCEPTED.Equals(ccTxnResponse.decision))
            {
                System.Console.WriteLine("Transaction Successful.");
            }
            else
            {
                System.Console.WriteLine("Transaction Failed with decision: " +
                ccTxnResponse.decision);
            }
        }
        catch (Exception ex)
        {

        }
    }


    private long GenerateId()
    {
        byte[] buffer = Guid.NewGuid().ToByteArray();
        return BitConverter.ToInt64(buffer, 0);
    }


    private string GetUniqueKey()
    {
        int maxSize = 7;
       
        char[] chars = new char[62];
        string a;
        a = "1234567890";
        chars = a.ToCharArray();
        int size = maxSize;
        byte[] data = new byte[1];
        RNGCryptoServiceProvider crypto = new RNGCryptoServiceProvider();
        crypto.GetNonZeroBytes(data);
        size = maxSize;
        data = new byte[size];
        crypto.GetNonZeroBytes(data);
        StringBuilder result = new StringBuilder(size);
        foreach (byte b in data)
        {
            result.Append(chars[b % (chars.Length - 1)]);
        }
        return result.ToString();
    }

    private void randerHTML()
    {
        string strHTML = "";
        strHTML += "<!DOCTYPE html PUBLIC '-//W3C//DTD HTML 4.0 Transitional//EN' 'http://www.w3.org/TR/REC-html40/loose.dtd'>";
        strHTML += "<html xmlns='http://www.w3.org/1999/xhtml'>";
        strHTML += "<head>";
        strHTML += "<title></title>";
        strHTML += "<style type='text/css'>";
        strHTML += ".appleDevice a";
        strHTML += "{";
        strHTML += "color: #1D81C1;";
        strHTML += "text-decoration: none;";
        strHTML += "}";
        strHTML += "#newsletter img, #newsletter a img";
        strHTML += "{";
        strHTML += "color: #666666;";
        strHTML += "text-decoration: none;";
        strHTML += "}";
        strHTML += "#header img";
        strHTML += "{";
        strHTML += "color: #f1c52c;";
        strHTML += "}";
        strHTML += "</style>";
        strHTML += "</head>";
        strHTML += "<table bgcolor='#f99d1c' border='0' cellpadding='10' cellspacing='0' width='100%'>";
        strHTML += "<tbody>";
        strHTML += "<tr>";
        strHTML += "<td align='center'>";
        strHTML += "<table id='ecxnewsletter' border='0' cellpadding='0' cellspacing='0' width='600'>";
        strHTML += "<tbody>";
        strHTML += "<tr>";
        strHTML += "<td style='font-family: 'Arial Rounded MT Bold',Helvetica,Arial,sans-serif; color: rgb(102, 102, 102);";
        strHTML += "padding-bottom: 10px;' align='left' bgcolor='#f99d1c' width='600'>";
        strHTML += "<table id='ecxheader' style='background-color:#ffffff;' border='0' cellpadding='5px' cellspacing='0' width='600'>";
        strHTML += "<tbody>";
        strHTML += "<tr>";
        strHTML += "<td style='font-size: 18px; font-family: 'Arial Rounded MT Bold',Helvetica,Arial,sans-serif;";
        strHTML += "color: rgb(102, 102, 102); text-transform: uppercase;' valign='bottom'";
        strHTML += "width='400'>";
        strHTML += "your daily deal";
        strHTML += "</td>";
        strHTML += "<td rowspa='2' rowspan='3'>";
        strHTML += "<img src='http://demo.tazzling.com/images/logoForMail.png' style='border: medium none;";
        strHTML += "display: block;'>";
        strHTML += "</td>";
        strHTML += "</tr>";
        strHTML += "<tr>";
        strHTML += "<td style='font-size: 32px; font-family: 'Arial Rounded MT Bold',Helvetica,Arial,sans-serif;";
        strHTML += "font-weight: bold; color: rgb(241, 197, 44);' valign='middle'";
        strHTML += "width='400'>";
        strHTML += "<a href='http://t.livingsocial.com/track/1747111024-c37463a51f94e6f381e8726833979964?url=http://livingsocial.com/deals/29091-teeth-whitening-and-paraffin-wax-hand-treatment?msdc_id%3D57%26ref%3DVANDeal031611_57_7027email'";
        strHTML += "style='color: rgb(233, 186, 38); text-decoration: none;' target='_blank'>vancouver</a>";
        strHTML += "</td>";
        strHTML += "</tr>";
        strHTML += "</tbody>";
        strHTML += "</table>";
        strHTML += "</td>";
        strHTML += "</tr>";
        strHTML += "<tr>";
        strHTML += "<td align='left' bgcolor='#ffffff' valign='top' width='600'>";
        strHTML += "<table id='ecxcontent' style='font-family: Helvetica,Arial,sans-serif; font-size: 14px;";
        strHTML += "color: rgb(102, 102, 102);' border='0' cellpadding='0' cellspacing='0' width='600'>";
        strHTML += "<tbody>";
        strHTML += "<tr>";
        strHTML += "<td bgcolor='#ffffff' valign='top' width='350'>";
        strHTML += "<table id='ecxleftcol' border='0' cellpadding='10' cellspacing='0' width='330'>";
        strHTML += "<tbody>";
        strHTML += "<tr>";
        strHTML += "<td style='font-size: 30px; font-family: 'Arial Rounded MT Bold',Helvetica,Arial,sans-serif;";
        strHTML += "font-weight: bold; padding: 10px 10px 5px;'>";
        strHTML += "<a href='http://t.livingsocial.com/track/1747111024-c37463a51f94e6f381e8726833979964?url=http://livingsocial.com/deals/29091-teeth-whitening-and-paraffin-wax-hand-treatment?msdc_id%3D57%26ref%3DVANDeal031611_57_7027email'";
        strHTML += "style='text-decoration: none; color: rgb(38, 38, 38);' target='_blank'>Teeth-Whitening";
        strHTML += "and Paraffin Wax Hand Treatment</a>";
        strHTML += "</td>";
        strHTML += "</tr>";
        strHTML += "<tr>";
        strHTML += "<td style='font-size: 16px; padding-top: 0pt; color: rgb(102, 102, 102); font-family: Helvetica,Arial,sans-serif;'>";
        strHTML += "Yaletown Laser &amp; Cosmetic Dentistry";
        strHTML += "</td>";
        strHTML += "</tr>";
        strHTML += "<tr>";
        strHTML += "<td style='padding-top: 0pt; padding-bottom: 0pt;' width='330'>";
        strHTML += "<table border='0' cellpadding='0' cellspacing='0' width='300'>";
        strHTML += "<tbody>";
        strHTML += "<tr>";
        strHTML += "<td style='font-size: 30px; font-family: 'Arial Rounded MT Bold',Helvetica,Arial,sans-serif;";
        strHTML += "font-weight: bold; color: rgb(240, 129, 43); padding-left: 10px;' align='right'";
        strHTML += "valign='top' width='16'>";
        strHTML += "C$";
        strHTML += "</td>";
        strHTML += "<td style='font-size: 58px; font-family: 'Arial Rounded MT Bold',Helvetica,Arial,sans-serif;";
        strHTML += "font-weight: bold; color: rgb(240, 129, 43); padding: 5px 10px 0pt 0pt;' align='left'>";
        strHTML += "99";
        strHTML += "</td>";
        strHTML += "<td style='padding-right: 10px;' align='center' valign='middle' width='205'>";
        strHTML += "<a href='http://t.livingsocial.com/track/1747111024-c37463a51f94e6f381e8726833979964?url=http://livingsocial.com/deals/29091-teeth-whitening-and-paraffin-wax-hand-treatment?msdc_id%3D57%26ref%3DVANDeal031611_57_7027email'";
        strHTML += "style='text-decoration: none; font-family: 'Arial Rounded MT Bold',Helvetica,Arial,sans-serif;";
        strHTML += "font-size: 22px; color: rgb(255, 255, 255); display: block; width: 130px; padding: 10px;";
        strHTML += "background-color: rgb(29, 129, 193); text-shadow: -1px -1px 0pt rgb(88, 89, 91);'";
        strHTML += "target='_blank'><span style='color: rgb(255, 255, 255);'>view deal</span></a>";
        strHTML += "</td>";
        strHTML += "</tr>";
        strHTML += "</tbody>";
        strHTML += "</table>";
        strHTML += "</td>";
        strHTML += "</tr>";
        strHTML += "<tr>";
        strHTML += "<td width='330'>";
        strHTML += "<table border='0' cellpadding='0' cellspacing='0'>";
        strHTML += "<tbody>";
        strHTML += "<tr>";
        strHTML += "<td style='padding-right: 10px; padding-top: 5px; padding-bottom: 5px; border-top: 1px solid rgb(221, 221, 221);";
        strHTML += "border-bottom: 1px solid rgb(221, 221, 221);' align='center' valign='middle'";
        strHTML += "width='65'>";
        strHTML += "<a href='http://t.livingsocial.com/track/1747111024-c37463a51f94e6f381e8726833979964?url=http://livingsocial.com/deals/29091-teeth-whitening-and-paraffin-wax-hand-treatment?msdc_id%3D57%26ref%3DVANDeal031611_57_7027email'";
        strHTML += "target='_blank'>";
        strHTML += "<img src='http://a3.ak.lscdn.net/imgs/41b3b5bb-535b-4995-b420-5e38cf123dac' alt='Get today's deal for free'";
        strHTML += "border='0' width='61' height='60'></a>";
        strHTML += "</td>";
        strHTML += "<td style='padding-top: 5px; padding-bottom: 5px; border-top: 1px solid rgb(221, 221, 221);";
        strHTML += "border-bottom: 1px solid rgb(221, 221, 221); font-family: Helvetica,Arial,sans-serif;";
        strHTML += "color: rgb(102, 102, 102); font-size: 11px;' align='left' valign='middle' width='265'>";
        strHTML += "<a href='http://t.livingsocial.com/track/1747111024-c37463a51f94e6f381e8726833979964?url=http://livingsocial.com/deals/29091-teeth-whitening-and-paraffin-wax-hand-treatment?msdc_id%3D57%26ref%3DVANDeal031611_57_7027email'";
        strHTML += "style='text-decoration: none; color: rgb(102, 102, 102); font-family: 'Arial Rounded MT Bold',Helvetica,Arial,sans-serif;";
        strHTML += "text-transform: lowercase; text-align: left;' target='_blank'><strong style='font-weight: bold;";
        strHTML += "color: rgb(29, 129, 193); font-size: 16px;'>Get Today's Deal For Free!</strong><br>";
        strHTML += "when you and three of your friends buy it.</a>";
        strHTML += "</td>";
        strHTML += "</tr>";
        strHTML += "</tbody>";
        strHTML += "</table>";
        strHTML += "</td>";
        strHTML += "</tr>";
        strHTML += "<tr>";
        strHTML += "<td style='color: rgb(102, 102, 102); font-size: 14px; font-family: Helvetica,Arial,sans-serif;";
        strHTML += "line-height: 18px; padding-top: 10px; padding-bottom: 10px;'>";
        strHTML += "You wouldn't settle for a sub-par sandwich or a boorish boyfriend -- and you shouldn't";
        strHTML += "settle for off-white enamel when you can have a snow-bright smile. Continue your";
        strHTML += "campaign against mediocrity with today's ivory-enhancing deal: for $99, you'll receive";
        strHTML += "a professional laser teeth-whitening session and complimentary paraffin wax hand";
        strHTML += "treatment from Yaletown Laser &amp; Cosmetic Dentistry (regularly $300). Unlike";
        strHTML += "messy whitening strips and trays, laser teeth-whitening is safe, effective, and";
        strHTML += "as comfortable as possible for you. Your treatment will begin with an application";
        strHTML += "of whitening gel, which is then activated by a light. This allows the gel to penetrate";
        strHTML += "tooth enamel and gently lift stains away, leaving your chompers two to six shades";
        strHTML += "whiter. There's no need to fight tooth and nail for the perfect grin -- this deal";
        strHTML += "will score you a radiant smile and savings to smile about.<br>";
        strHTML += "</td>";
        strHTML += "</tr>";
        strHTML += "<tr>";
        strHTML += "<td>";
        strHTML += "<a href='http://t.livingsocial.com/track/1747111024-c37463a51f94e6f381e8726833979964?url=http://livingsocial.com/deals/29091-teeth-whitening-and-paraffin-wax-hand-treatment?msdc_id%3D57%26ref%3DVANDeal031611_57_7027email'";
        strHTML += "style='color: rgb(29, 129, 193); font-size: 28px; font-weight: bold; font-family: 'Arial Rounded MT Bold',Helvetica,Arial,sans-serif;";
        strHTML += "text-decoration: none;' target='_blank'>Find out more » </a>";
        strHTML += "</td>";
        strHTML += "</tr>";
        strHTML += "</tbody>";
        strHTML += "</table>";
        strHTML += "</td>";
        strHTML += "<td valign='top' width='250'>";
        strHTML += "<table id='ecxrightcol' border='0' cellpadding='0' cellspacing='0' width='250'>";
        strHTML += "<tbody>";
        strHTML += "<tr>";
        strHTML += "<td width='250'>";
        strHTML += "<a href='http://t.livingsocial.com/track/1747111024-c37463a51f94e6f381e8726833979964?url=http://livingsocial.com/deals/29091-teeth-whitening-and-paraffin-wax-hand-treatment?msdc_id%3D57%26ref%3DVANDeal031611_57_7027email'";
        strHTML += "target='_blank'>";
        strHTML += "<img src='http://lscdn.net/imgs/3fea6c29-d488-4216-8a9a-b495d87f8f1a' alt='Laser Teeth-Whitening Treatment and Paraffin Wax Hand Treatment'";
        strHTML += "style='border: medium none; display: block; background-color: rgb(238, 238, 238);";
        strHTML += "width: 250px; height: 358px;' width='250' height='358'></a>";
        strHTML += "</td>";
        strHTML += "</tr>";
        strHTML += "<tr>";
        strHTML += "<td style='padding: 10px 0pt;' width='250'>";
        strHTML += "<table border='0' cellpadding='10' cellspacing='0'>";
        strHTML += "<tbody>";
        strHTML += "<tr>";
        strHTML += "<td style='font-size: 12px; font-family: Helvetica,Arial,sans-serif; color: rgb(102, 102, 102);'>";
        strHTML += "<p style='margin-bottom: 5px;'>";
        strHTML += "<strong>1 Deal Location</strong></p>";
        strHTML += "<p style=''>";
        strHTML += "1010 Mainland Street<br>";
        strHTML += "Vancouver, BC, V6B 2T4</p>";
        strHTML += "</td>";
        strHTML += "</tr>";
        strHTML += "</tbody>";
        strHTML += "</table>";
        strHTML += "</td>";
        strHTML += "</tr>";
        strHTML += "</tbody>";
        strHTML += "</table>";
        strHTML += "</td>";
        strHTML += "</tr>";
        strHTML += "<tr>";
        strHTML += "<td colspan='2' align='center'>";
        strHTML += "<a href='http://t.livingsocial.com/track/1747111024-c37463a51f94e6f381e8726833979964?url=http://www.globalgiving.org/lsjapan/?RF%3DLSJapan'";
        strHTML += "target='_blank'>";
        strHTML += "<img src='http://a1.ak.lscdn.net/imgs/73432eb0-439b-4806-b0b7-defe54bfe3ac' style='border: medium none;'";
        strHTML += "alt='Donate to Japan Earthquake and Tsunami Relief' width='580' height='72'></a>";
        strHTML += "</td>";
        strHTML += "</tr>";
        strHTML += "</tbody>";
        strHTML += "</table>";
        strHTML += "</td>";
        strHTML += "</tr>";
        strHTML += "<tr>";
        strHTML += "<td style='background: none repeat scroll 0% 0% rgb(255, 255, 255); padding: 10px;'>";
        strHTML += "<table border='0' cellpadding='0' cellspacing='0' width='580'>";
        strHTML += "<tbody>";
        strHTML += "<tr>";
        strHTML += "<td style='padding-right: 10px; padding-top: 10px; border-top: 1px solid rgb(230, 231, 232);'";
        strHTML += "align='left' valign='top' width='320'>";
        strHTML += "<table border='0' cellpadding='0' cellspacing='0' width='320'>";
        strHTML += "<tbody>";
        strHTML += "<tr>";
        strHTML += "<td colspan='2' style='color: rgb(240, 129, 43); font-size: 20px; font-weight: bold;";
        strHTML += "font-family: 'Arial Rounded MT Bold',Helvetica,Arial,sans-serif; padding-bottom: 10px;'>";
        strHTML += "More Deals Near You";
        strHTML += "</td>";
        strHTML += "</tr>";
        strHTML += "<tr>";
        strHTML += "<td style='padding-right: 10px; width: 75px;' align='right' valign='top'>";
        strHTML += "<a href='http://t.livingsocial.com/track/1747111024-c37463a51f94e6f381e8726833979964?url=http://livingsocial.com/deals/32073-30-to-spend-on-watches?msdc_id%3D57%26ref%3DVANDeal031611_57_7027email'";
        strHTML += "target='_blank'>";
        strHTML += "<img src='http://a5.ak.lscdn.net/imgs/3335a2f9-cf88-4d89-a66f-99d3c05473c8/75.png'";
        strHTML += "style='width: 75px; border: medium none;' alt='$30 to Spend on Watches'></a>";
        strHTML += "</td>";
        strHTML += "<td style='color: rgb(102, 102, 102); font-size: 12px; font-family: Helvetica,Arial,sans-serif;";
        strHTML += "padding-bottom: 20px;' align='left' valign='top'>";
        strHTML += "<strong style='color: rgb(102, 102, 102); font-size: 14px;'>Vancouver</strong>";
        strHTML += "<br>";
        strHTML += "<a href='http://t.livingsocial.com/track/1747111024-c37463a51f94e6f381e8726833979964?url=http://livingsocial.com/deals/32073-30-to-spend-on-watches?msdc_id%3D57%26ref%3DVANDeal031611_57_7027email'";
        strHTML += "style='color: rgb(29, 129, 193); font-size: 14px; font-weight: bold; text-decoration: none;'";
        strHTML += "target='_blank'>RumbaTime</a>";
        strHTML += "<br>";
        strHTML += "<span style='font-size: 14px;'>$30 to Spend on Watches</span>";
        strHTML += "<br>";
        strHTML += "Rumba, like all dances, is not <em>just </em>about timing. It's also about looking";
        strHTML += "good. Master both when you snag a RumbaTime watch online using today's deal: $30";
        strHTML += "to spend for just $15. Start a...";
        strHTML += "<br>";
        strHTML += "<a href='http://t.livingsocial.com/track/1747111024-c37463a51f94e6f381e8726833979964?url=http://livingsocial.com/deals/32073-30-to-spend-on-watches?msdc_id%3D57%26ref%3DVANDeal031611_57_7027email'";
        strHTML += "style='text-decoration: none; color: rgb(29, 129, 193);' target='_blank'>Find out";
        strHTML += "more</a>";
        strHTML += "<br>";
        strHTML += "</td>";
        strHTML += "</tr>";
        strHTML += "<tr>";
        strHTML += "<td style='padding-right: 10px; width: 75px;' align='right' valign='top'>";
        strHTML += "<a href='http://t.livingsocial.com/track/1747111024-c37463a51f94e6f381e8726833979964?url=http://livingsocial.com/deals/30254-40-to-spend-on-children-s-accessories?msdc_id%3D57%26ref%3DVANDeal031611_57_7027email'";
        strHTML += "target='_blank'>";
        strHTML += "<img src='http://a1.ak.lscdn.net/imgs/098a45ff-4d84-4758-9303-b9dabbe3efec/75.png'";
        strHTML += "style='width: 75px; border: medium none;' alt='$40 to Spend on Children's Accessories'></a>";
        strHTML += "</td>";
        strHTML += "<td style='color: rgb(102, 102, 102); font-size: 12px; font-family: Helvetica,Arial,sans-serif;";
        strHTML += "padding-bottom: 20px;' align='left' valign='top'>";
        strHTML += "<strong style='color: rgb(102, 102, 102); font-size: 14px;'>Vancouver Family Edition</strong>";
        strHTML += "<br>";
        strHTML += "<a href='http://t.livingsocial.com/track/1747111024-c37463a51f94e6f381e8726833979964?url=http://livingsocial.com/deals/30254-40-to-spend-on-children-s-accessories?msdc_id%3D57%26ref%3DVANDeal031611_57_7027email'";
        strHTML += "style='color: rgb(29, 129, 193); font-size: 14px; font-weight: bold; text-decoration: none;'";
        strHTML += "target='_blank'>Sprogs</a>";
        strHTML += "<br>";
        strHTML += "<span style='font-size: 14px;'>$40 to Spend on Children's Accessories</span>";
        strHTML += "<br>";
        strHTML += "Like small dogs and divas, children must be properly accessorized. Outfit your offspring";
        strHTML += "in style with today's stellar deal: pay $20 and get $40 to spend at Sprogs, an online";
        strHTML += "children's boutique...";
        strHTML += "<br>";
        strHTML += "<a href='http://t.livingsocial.com/track/1747111024-c37463a51f94e6f381e8726833979964?url=http://livingsocial.com/deals/30254-40-to-spend-on-children-s-accessories?msdc_id%3D57%26ref%3DVANDeal031611_57_7027email'";
        strHTML += "style='text-decoration: none; color: rgb(29, 129, 193);' target='_blank'>Find out";
        strHTML += "more</a>";
        strHTML += "<br>";
        strHTML += "</td>";
        strHTML += "</tr>";
        strHTML += "</tbody>";
        strHTML += "</table>";
        strHTML += "</td>";
        strHTML += "<td style='padding-top: 10px; border-top: 1px solid rgb(230, 231, 232);' align='left'";
        strHTML += "valign='top' width='240'>";
        strHTML += "<table border='0' cellpadding='0' cellspacing='0' width='240'>";
        strHTML += "<tbody>";
        strHTML += "<tr>";
        strHTML += "<td colspan='2' style='color: rgb(240, 129, 43); font-size: 20px; font-weight: bold;";
        strHTML += "font-family: 'Arial Rounded MT Bold',Helvetica,Arial,sans-serif; padding-bottom: 10px;";
        strHTML += "padding-top: 0pt;'>";
        strHTML += "365 things to do";
        strHTML += "</td>";
        strHTML += "</tr>";
        strHTML += "<tr>";
        strHTML += "<td style='font-family: Helvetica,Arial,sans-serif; padding-right: 10px;' align='right'";
        strHTML += "valign='top' width='45'>";
        strHTML += "<div style='color: rgb(38, 38, 38); background-color: rgb(233, 186, 38); font-size: 18px;";
        strHTML += "font-weight: bold; line-height: 18px; padding: 5px;'>";
        strHTML += "<span style='font-size: 12px; line-height: 12px; vertical-align: top;'>#</span>204";
        strHTML += "</div>";
        strHTML += "</td>";
        strHTML += "<td style='color: rgb(102, 102, 102); font-size: 12px; font-family: Helvetica,Arial,sans-serif;";
        strHTML += "padding-bottom: 10px;' align='left' valign='top' width='195'>";
        strHTML += "<span style='font-size: 14px;'>Best art gallery? </span>";
        strHTML += "<br>";
        strHTML += "<a href='http://t.livingsocial.com/track/1747111024-c37463a51f94e6f381e8726833979964?url=http://livingsocial.com/deals/29091-teeth-whitening-and-paraffin-wax-hand-treatment?msdc_id%3D57%26ref%3DVANDeal031611_57_7027email'";
        strHTML += "style='text-decoration: none; color: rgb(29, 129, 193);' target='_blank'>See our";
        strHTML += "pick</a>";
        strHTML += "</td>";
        strHTML += "</tr>";
        strHTML += "</tbody>";
        strHTML += "</table>";
        strHTML += "</td>";
        strHTML += "</tr>";
        strHTML += "</tbody>";
        strHTML += "</table>";
        strHTML += "</td>";
        strHTML += "</tr>";
        strHTML += "<tr>";
        strHTML += "<td>";
        strHTML += "<table style='color: rgb(102, 102, 102); font-size: 12px; font-family: Helvetica,Arial,sans-serif;'";
        strHTML += "bgcolor='#ededed' border='0' cellpadding='20' cellspacing='0' width='600'>";
        strHTML += "<tbody>";
        strHTML += "<tr>";
        strHTML += "<td style='font-family: Helvetica,Arial,sans-serif; border-right: 1px solid #f99d1c;";
        strHTML += "border-top: 1px solid #f99d1c;' align='left' valign='top' width='300'>";
        strHTML += "<table border='0' cellpadding='0' cellspacing='0'>";
        strHTML += "<tbody>";
        strHTML += "<tr>";
        strHTML += "<td style='padding-right: 10px;' align='center' valign='top' width='65'>";
        strHTML += "<img src='http://a2.ak.lscdn.net/imgs/a65fd50e-436f-469b-9fe6-c77980f2fc9f' width='58'";
        strHTML += "height='58'>";
        strHTML += "</td>";
        strHTML += "<td style='font-family: Helvetica,Arial,sans-serif; font-size: 12px; color: rgb(102, 102, 102);'";
        strHTML += "valign='top' width='195'>";
        strHTML += "<a href='http://t.livingsocial.com/track/1747111024-c37463a51f94e6f381e8726833979964?url=http://livingsocial.com/deals/29091-teeth-whitening-and-paraffin-wax-hand-treatment?msdc_id%3D57%26ref%3DVANDeal031611_57_7027email'";
        strHTML += "style='color: rgb(29, 129, 193); font-weight: bold; font-size: 14px; font-family: 'Arial Rounded MT Bold',Helvetica,Arial,sans-serif;";
        strHTML += "text-decoration: none; text-transform: lowercase;' target='_blank'>Give the gift";
        strHTML += "of deals</a>";
        strHTML += "<br>";
        strHTML += "Find the deal, buy, and send - it's easy!";
        strHTML += "</td>";
        strHTML += "</tr>";
        strHTML += "</tbody>";
        strHTML += "</table>";
        strHTML += "</td>";
        strHTML += "<td style='border-top: 1px solid #f99d1c; font-family: Helvetica,Arial,sans-serif;'";
        strHTML += "align='left' valign='top' width='300'>";
        strHTML += "<table border='0' cellpadding='0' cellspacing='0'>";
        strHTML += "<tbody>";
        strHTML += "<tr>";
        strHTML += "<td style='padding-right: 10px;' align='center' valign='top' width='65'>";
        strHTML += "<a href='http://t.livingsocial.com/track/1747111024-c37463a51f94e6f381e8726833979964?url=http://livingsocial.com/deals/30254-40-to-spend-on-children-s-accessories?msdc_id%3D57%26ref%3DVANDeal031611_57_7027email'";
        strHTML += "target='_blank'>";
        strHTML += "<img src='http://a1.ak.lscdn.net/deals/images/bingy/family_edition/email-promo-badge.gif'";
        strHTML += "style='border: medium none;' alt='Family Edition'></a>";
        strHTML += "</td>";
        strHTML += "<td style='font-family: Helvetica,Arial,sans-serif; font-size: 12px; color: rgb(102, 102, 102);'";
        strHTML += "valign='top' width='230'>";
        strHTML += "<a href='http://t.livingsocial.com/track/1747111024-c37463a51f94e6f381e8726833979964?url=http://livingsocial.com/deals/30254-40-to-spend-on-children-s-accessories?msdc_id%3D57%26ref%3DVANDeal031611_57_7027email'";
        strHTML += "style='color: rgb(29, 129, 193); font-size: 14px; font-weight: bold; font-family: 'Arial Rounded MT Bold',Helvetica,Arial,sans-serif;";
        strHTML += "text-decoration: none; text-transform: lowercase;' target='_blank'>Just for Families!</a><br>";
        strHTML += "Want to explore the city with your kids? Sign up for LivingSocial Family Edition";
        strHTML += "and we'll send you deals just for the fam.";
        strHTML += "</td>";
        strHTML += "</tr>";
        strHTML += "</tbody>";
        strHTML += "</table>";
        strHTML += "</td>";
        strHTML += "</tr>";
        strHTML += "</tbody>";
        strHTML += "</table>";
        strHTML += "</td>";
        strHTML += "</tr>";
        strHTML += "<tr>";
        strHTML += "<td style='border-top: 10px solid #f99d1c; padding: 10px; font-size: 10px;";
        strHTML += "font-family: Helvetica,Arial,sans-serif; color: #ffffff;' bgcolor='#f35b00'>";
        strHTML += "<p style=''>";
        strHTML += "<strong style='font-size: 16px;'>Have a question?</strong> <a href='http://t.livingsocial.com/track/1747111024-c37463a51f94e6f381e8726833979964?url=http://help.livingsocial.com/'";
        strHTML += "style='text-decoration: none; color: #ffffff;' target='_blank'>Visit our";
        strHTML += "support portal</a> or call us: <span class='ecxappleDevice'>877-521-4191</span>";
        strHTML += "(US/CA), <span class='ecxappleDevice'>0-800-014-8431</span> (UK), <span class='ecxappleDevice'>";
        strHTML += "877-498-0128</span> (Français), <span class='ecxappleDevice'>353-1-234-2557</span>";
        strHTML += "(Ireland), or <span class='ecxappleDevice'>1800 586 766</span> (Australia).</p>";
        strHTML += "Please add <a href='mailto:deals@livingsocial.com' style='text-decoration: none;";
        strHTML += "color: #ffffff;'>deals@livingsocial.com</a> to your address book or";
        strHTML += "safe sender list so our emails get to your inbox.<br>";
        strHTML += "This message was sent by LivingSocial, <span class='ecxappleDevice'>829 7th St NW, Third";
        strHTML += "Floor, Washington, DC 20001</span>.<br>";
        strHTML += "<p style='margin-bottom: 0pt;'>";
        strHTML += "You are receiving this email because you have an existing relationship with <a href='http://t.livingsocial.com/track/1747111024-c37463a51f94e6f381e8726833979964?url=http://livingsocial.com'";
        strHTML += "style='text-decoration: none; color: #ffffff;' target='_blank'>LivingSocial</a>.";
        strHTML += "If you no longer wish to receive marketing communications, you can <a href='http://t.livingsocial.com/track/1747111024-c37463a51f94e6f381e8726833979964?url=http://livingsocial.com/email_settings?token%3D1747111024-c37463a51f94e6f381e8726833979964'";
        strHTML += "style='text-decoration: none; color: #ffffff;' target='_blank'>unsubscribe</a>.";
        strHTML += "</p>";
        strHTML += "</td>";
        strHTML += "</tr>";
        strHTML += "</tbody>";
        strHTML += "</table>";
        strHTML += "</td>";
        strHTML += "</tr>";
        strHTML += "</tbody>";
        strHTML += "</table>";
        strHTML += "<img alt='' src='http://t.livingsocial.com/track/1747111024-c37463a51f94e6f381e8726833979964'";
        strHTML += "border='0' width='0' height='0'>";
        strHTML += "</body>";
        strHTML += "</html>";

    }
    protected void btnSendEmail_Click(object sender, EventArgs e)
    {
        //for (int i = 0; i < 10; i++)
        //{
        //    Misc.SendEmail(TextBox1.Text.Trim(), "", "", "sher.azam@redsignal.biz", "Test " + i.ToString(), "<body style='font:normal 13px tahoma,arial,helvetica;'>I am testing  and this is the email number " + i.ToString() + "</body>");
        //    int intSleep=0;
        //    if(Int32.TryParse(TextBox2.Text.Trim(),out intSleep))
        //    {
        //        Thread.Sleep(intSleep);
        //    }
        //}
        Label1.Text = "Emails send successfully";
    }
}
