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
using System.Security.Cryptography.X509Certificates;

public partial class Default9 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        System.Net.ServicePointManager.CertificatePolicy = new TrustAllCertificatePolicy();
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
        try
        {
            //Prepare the call to the Credit Card Web Service
            System.Net.ServicePointManager.CertificatePolicy = new TrustAllCertificatePolicy();
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
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
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

}
