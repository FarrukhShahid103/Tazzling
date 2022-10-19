using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Web.Services.Protocols;

public partial class fedexTest : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
 
        }        
    }

    #region Fedex Code
    private ProcessShipmentRequest CreateShipmentRequest(bool isCodShipment)
    {
        // Build the ShipmentRequest
        ProcessShipmentRequest request = new ProcessShipmentRequest();
        //
        request.WebAuthenticationDetail = new WebAuthenticationDetail();
        request.WebAuthenticationDetail.UserCredential = new WebAuthenticationCredential();
        request.WebAuthenticationDetail.UserCredential.Key = "1ta3eDjylYboBZ7z"; // Replace "XXX" with the Key
        request.WebAuthenticationDetail.UserCredential.Password = "7wdYIT5Zge0M8k3FndaKuE5hf"; // Replace "XXX" with the Password
        //
        request.ClientDetail = new ClientDetail();
        request.ClientDetail.AccountNumber = "510087240"; // Replace "XXX" with the client's account number
        request.ClientDetail.MeterNumber = "118562743"; // Replace "XXX" with the client's meter number
        //
        request.TransactionDetail = new TransactionDetail();
        request.TransactionDetail.CustomerTransactionId = "***Ground Domestic Ship v10 Request using VC#***"; // The client will get the same value back in the response
        //
        request.Version = new VersionId(); // WSDL version information, value is automatically set from wsdl
        //
        SetShipmentDetails(request);
        //
        SetSender(request);
        //
        SetRecipient(request);
        //
        SetPayment(request);
        //
        SetLabelDetails(request);
        //
        SetPackageLineItems(request, isCodShipment);
        //
        return request;
    }

    private static void SetShipmentDetails(ProcessShipmentRequest request)
    {
        request.RequestedShipment = new RequestedShipment();
        request.RequestedShipment.ShipTimestamp = DateTime.Now; // Ship date and time
        request.RequestedShipment.ServiceType = ServiceType.FEDEX_GROUND; // Service types are FEDEX_GROUND, GROUND_HOME_DELIVERY ...
        request.RequestedShipment.PackagingType = PackagingType.YOUR_PACKAGING; // Packaging type YOUR_PACKAGING, ...
        //
        request.RequestedShipment.RateRequestTypes = new RateRequestType[1] { RateRequestType.ACCOUNT }; // Rate types requested LIST, MULTIWEIGHT, ...
        request.RequestedShipment.PackageCount = "1";
        // set HAL
        bool isHALShipment = false;
        if (isHALShipment)
            SetHAL(request);
    }

    private void SetSender(ProcessShipmentRequest request)
    {
        request.RequestedShipment.Shipper = new Party();
        request.RequestedShipment.Shipper.Contact = new Contact();
        request.RequestedShipment.Shipper.Contact.PersonName = txtSenderName.Text.Trim();
        request.RequestedShipment.Shipper.Contact.CompanyName = txtSenderCompanyName.Text.Trim();
        request.RequestedShipment.Shipper.Contact.PhoneNumber = txtSenderPhoneNumber.Text.Trim();
        //
        request.RequestedShipment.Shipper.Address = new Address();
        request.RequestedShipment.Shipper.Address.StreetLines = new string[1] { txtSenderAddressLane1.Text.Trim() };
        request.RequestedShipment.Shipper.Address.City = txtSenderCity.Text.Trim();
        request.RequestedShipment.Shipper.Address.StateOrProvinceCode = txtSenderStateOrProvinceCode.Text.Trim();
        request.RequestedShipment.Shipper.Address.PostalCode = txtSenderPostalCode.Text.Trim();
        request.RequestedShipment.Shipper.Address.CountryCode = txtSenderCountryCode.Text.Trim();
    }

    private void SetRecipient(ProcessShipmentRequest request)
    {
        request.RequestedShipment.Recipient = new Party();
        request.RequestedShipment.Recipient.Contact = new Contact();
        request.RequestedShipment.Recipient.Contact.PersonName = txtRecipientName.Text.Trim();
        request.RequestedShipment.Recipient.Contact.CompanyName = txtRecipientCompanyName.Text.Trim();
        request.RequestedShipment.Recipient.Contact.PhoneNumber = txtRecipientPhoneNumber.Text.Trim();
        //
        request.RequestedShipment.Recipient.Address = new Address();
        request.RequestedShipment.Recipient.Address.StreetLines = new string[1] { txtRecipientAddress.Text.Trim() };
        request.RequestedShipment.Recipient.Address.City = txtRecipientCity.Text.Trim();
        request.RequestedShipment.Recipient.Address.StateOrProvinceCode = txtRecipientState.Text.Trim();
        request.RequestedShipment.Recipient.Address.PostalCode = txtRecipientPostalCode.Text.Trim();
        request.RequestedShipment.Recipient.Address.CountryCode = txtRecipientCountryCode.Text.Trim();
    }

    private static void SetPayment(ProcessShipmentRequest request)
    {
        request.RequestedShipment.ShippingChargesPayment = new Payment();
        request.RequestedShipment.ShippingChargesPayment.PaymentType = PaymentType.SENDER;
        request.RequestedShipment.ShippingChargesPayment.Payor = new Payor();
        request.RequestedShipment.ShippingChargesPayment.Payor.AccountNumber = "510087240"; // Replace "XXX" with the payor account number
        request.RequestedShipment.ShippingChargesPayment.Payor.CountryCode = "US";
    }

    private static void SetLabelDetails(ProcessShipmentRequest request)
    {
        request.RequestedShipment.LabelSpecification = new LabelSpecification();
        request.RequestedShipment.LabelSpecification.ImageType = ShippingDocumentImageType.PDF; // Image types PDF, PNG, DPL, ...
        request.RequestedShipment.LabelSpecification.ImageTypeSpecified = true;
        request.RequestedShipment.LabelSpecification.LabelFormatType = LabelFormatType.COMMON2D;
    }

    private static void SetPackageLineItems(ProcessShipmentRequest request, bool isCodShipment)
    {
        request.RequestedShipment.RequestedPackageLineItems = new RequestedPackageLineItem[1];
        request.RequestedShipment.RequestedPackageLineItems[0] = new RequestedPackageLineItem();
        request.RequestedShipment.RequestedPackageLineItems[0].SequenceNumber = "1";
        // Package weight information
        request.RequestedShipment.RequestedPackageLineItems[0].Weight = new Weight();
        request.RequestedShipment.RequestedPackageLineItems[0].Weight.Value = 50.0M;
        request.RequestedShipment.RequestedPackageLineItems[0].Weight.Units = WeightUnits.LB;
        //
        request.RequestedShipment.RequestedPackageLineItems[0].Dimensions = new Dimensions();
        request.RequestedShipment.RequestedPackageLineItems[0].Dimensions.Length = "12";
        request.RequestedShipment.RequestedPackageLineItems[0].Dimensions.Width = "13";
        request.RequestedShipment.RequestedPackageLineItems[0].Dimensions.Height = "14";
        request.RequestedShipment.RequestedPackageLineItems[0].Dimensions.Units = LinearUnits.IN;
        // Reference details
        request.RequestedShipment.RequestedPackageLineItems[0].CustomerReferences = new CustomerReference[3] { new CustomerReference(), new CustomerReference(), new CustomerReference() };
        request.RequestedShipment.RequestedPackageLineItems[0].CustomerReferences[0].CustomerReferenceType = CustomerReferenceType.CUSTOMER_REFERENCE;
        request.RequestedShipment.RequestedPackageLineItems[0].CustomerReferences[0].Value = "GR4567892";
        request.RequestedShipment.RequestedPackageLineItems[0].CustomerReferences[1].CustomerReferenceType = CustomerReferenceType.INVOICE_NUMBER;
        request.RequestedShipment.RequestedPackageLineItems[0].CustomerReferences[1].Value = "INV4567892";
        request.RequestedShipment.RequestedPackageLineItems[0].CustomerReferences[2].CustomerReferenceType = CustomerReferenceType.P_O_NUMBER;
        request.RequestedShipment.RequestedPackageLineItems[0].CustomerReferences[2].Value = "PO4567892";
        //
        if (isCodShipment)
        {
            SetCOD(request);
        }
    }

    private static void SetHAL(ProcessShipmentRequest request)
    {
        request.RequestedShipment.SpecialServicesRequested = new ShipmentSpecialServicesRequested();
        request.RequestedShipment.SpecialServicesRequested.SpecialServiceTypes = new ShipmentSpecialServiceType[1];
        request.RequestedShipment.SpecialServicesRequested.SpecialServiceTypes[0] = ShipmentSpecialServiceType.HOLD_AT_LOCATION;
        //
        request.RequestedShipment.SpecialServicesRequested.HoldAtLocationDetail = new HoldAtLocationDetail();
        request.RequestedShipment.SpecialServicesRequested.HoldAtLocationDetail.PhoneNumber = "9011234567";
        request.RequestedShipment.SpecialServicesRequested.HoldAtLocationDetail.LocationContactAndAddress = new ContactAndAddress();
        request.RequestedShipment.SpecialServicesRequested.HoldAtLocationDetail.LocationContactAndAddress.Contact = new Contact();
        request.RequestedShipment.SpecialServicesRequested.HoldAtLocationDetail.LocationContactAndAddress.Contact.PersonName = "Tester";
        //
        request.RequestedShipment.SpecialServicesRequested.HoldAtLocationDetail.LocationContactAndAddress.Address = new Address();
        request.RequestedShipment.SpecialServicesRequested.HoldAtLocationDetail.LocationContactAndAddress.Address.StreetLines = new string[1];
        request.RequestedShipment.SpecialServicesRequested.HoldAtLocationDetail.LocationContactAndAddress.Address.StreetLines[0] = "45 Noblestown Road";
        request.RequestedShipment.SpecialServicesRequested.HoldAtLocationDetail.LocationContactAndAddress.Address.City = "Pittsburgh";
        request.RequestedShipment.SpecialServicesRequested.HoldAtLocationDetail.LocationContactAndAddress.Address.StateOrProvinceCode = "PA";
        request.RequestedShipment.SpecialServicesRequested.HoldAtLocationDetail.LocationContactAndAddress.Address.PostalCode = "15220";
        request.RequestedShipment.SpecialServicesRequested.HoldAtLocationDetail.LocationContactAndAddress.Address.CountryCode = "US";
    }

    private static void SetCOD(ProcessShipmentRequest request)
    {
        request.RequestedShipment.RequestedPackageLineItems[0].SpecialServicesRequested = new PackageSpecialServicesRequested();
        request.RequestedShipment.RequestedPackageLineItems[0].SpecialServicesRequested.SpecialServiceTypes = new PackageSpecialServiceType[1];
        request.RequestedShipment.RequestedPackageLineItems[0].SpecialServicesRequested.SpecialServiceTypes[0] = PackageSpecialServiceType.COD;
        //
        request.RequestedShipment.RequestedPackageLineItems[0].SpecialServicesRequested.CodDetail = new CodDetail();
        request.RequestedShipment.RequestedPackageLineItems[0].SpecialServicesRequested.CodDetail.CollectionType = CodCollectionType.GUARANTEED_FUNDS;
        request.RequestedShipment.RequestedPackageLineItems[0].SpecialServicesRequested.CodDetail.CodCollectionAmount = new Money();
        request.RequestedShipment.RequestedPackageLineItems[0].SpecialServicesRequested.CodDetail.CodCollectionAmount.Amount = 250.00M;
        request.RequestedShipment.RequestedPackageLineItems[0].SpecialServicesRequested.CodDetail.CodCollectionAmount.Currency = "USD";
    }

    private void ShowShipmentReply(bool isCodShipment, ProcessShipmentReply reply)
    {
        lblResult.Text += "Shipment Reply details:<br>";
        lblResult.Text += "Package details<br>";
        // Details for each package
        foreach (CompletedPackageDetail packageDetail in reply.CompletedShipmentDetail.CompletedPackageDetails)
        {
            ShowTrackingDetails(packageDetail.TrackingIds);
            ShowPackageRateDetails(packageDetail.PackageRating.PackageRateDetails);
            ShowBarcodeDetails(packageDetail.OperationalDetail.Barcodes);
            ShowShipmentLabels(isCodShipment, reply.CompletedShipmentDetail, packageDetail);
        }
        ShowPackageRouteDetails(reply.CompletedShipmentDetail.OperationalDetail);
    }

    private static void ShowShipmentLabels(bool isCodShipment, CompletedShipmentDetail completedShipmentDetail, CompletedPackageDetail packageDetail)
    {
        if (null != packageDetail.Label.Parts[0].Image)
        {
            // Save outbound shipping label
            string LabelFileName = AppDomain.CurrentDomain.BaseDirectory + "Images\\fedex\\" + packageDetail.TrackingIds[0].TrackingNumber + ".pdf";
            SaveLabel(LabelFileName, packageDetail.Label.Parts[0].Image);
            if (isCodShipment)
            {
                // Save COD label
                LabelFileName = AppDomain.CurrentDomain.BaseDirectory + "Images\\fedex\\" + completedShipmentDetail.CompletedPackageDetails[0].TrackingIds[0].TrackingNumber + "CR.pdf";
                SaveLabel(LabelFileName, completedShipmentDetail.CompletedPackageDetails[0].CodReturnDetail.Label.Parts[0].Image);
            }
        }
    }

    private void ShowTrackingDetails(TrackingId[] TrackingIds)
    {
        // Tracking information for each package
        lblResult.Text += "Tracking details<br>";
        if (TrackingIds != null)
        {
            for (int i = 0; i < TrackingIds.Length; i++)
            {
                lblResult.Text += "Tracking # " + TrackingIds[i].TrackingNumber + " Form ID " + TrackingIds[i].FormId + "<br>";
            }
        }
    }

    private void ShowPackageRateDetails(PackageRateDetail[] PackageRateDetails)
    {
        foreach (PackageRateDetail ratedPackage in PackageRateDetails)
        {
            lblResult.Text += "Rate details<br>";
            if (ratedPackage.BillingWeight != null)
                lblResult.Text += "Billing weight " + ratedPackage.BillingWeight.Value + " " + ratedPackage.BillingWeight.Units + "<br>";
            if (ratedPackage.BaseCharge != null)
                lblResult.Text += "Base charge " + ratedPackage.BaseCharge.Amount + " " + ratedPackage.BaseCharge.Currency + "<br>";
            if (ratedPackage.TotalSurcharges != null)
                lblResult.Text += "Total surcharge " + ratedPackage.TotalSurcharges.Amount + " " + ratedPackage.TotalSurcharges.Currency + "<br>";
            if (ratedPackage.Surcharges != null)
            {
                // Individual surcharge for each package
                foreach (Surcharge surcharge in ratedPackage.Surcharges)
                    lblResult.Text += surcharge.SurchargeType + "surcharge " + surcharge.Amount.Amount + " " + surcharge.Amount.Currency + "<br>";
            }
            if (ratedPackage.NetCharge != null)
                lblResult.Text += "Net charge " + ratedPackage.NetCharge.Amount + " " + ratedPackage.NetCharge.Currency + "<br>";
        }
    }

    private void ShowBarcodeDetails(PackageBarcodes barcodes)
    {
        // Barcode information for each package
        lblResult.Text += "Barcode details<br>";
        if (barcodes != null)
        {
            if (barcodes.StringBarcodes != null)
            {
                for (int i = 0; i < barcodes.StringBarcodes.Length; i++)
                {
                    lblResult.Text += "String barcode " + barcodes.StringBarcodes[i].Value + " Type " + barcodes.StringBarcodes[i].Type + "<br>";
                }
            }

            if (barcodes.BinaryBarcodes != null)
            {
                for (int i = 0; i < barcodes.BinaryBarcodes.Length; i++)
                {
                    lblResult.Text += "Binary barcode Type " + barcodes.BinaryBarcodes[i].Type + "<br>";
                }
            }
        }
    }

    private void ShowPackageRouteDetails(ShipmentOperationalDetail routingDetail)
    {
        lblResult.Text += "Routing details<br>";
        lblResult.Text += "URSA prefix " + routingDetail.UrsaPrefixCode + " suffix " + routingDetail.UrsaSuffixCode + "<br>";
        lblResult.Text += "Service commitment " + routingDetail.DestinationLocationId + " Airport ID " + routingDetail.AirportId + "<br>";

        if (routingDetail.DeliveryDaySpecified)
        {
            lblResult.Text += "Delivery day " + routingDetail.DeliveryDay + "<br>";
        }
        if (routingDetail.DeliveryDateSpecified)
        {
            lblResult.Text += "Delivery date " + routingDetail.DeliveryDate.ToShortDateString() + "<br>";
        }
        if (routingDetail.TransitTimeSpecified)
        {
            lblResult.Text += "Transit time " + routingDetail.TransitTime + "<br>";
        }
    }

    private static void SaveLabel(string labelFileName, byte[] labelBuffer)
    {
        // Save label buffer to file
        FileStream LabelFile = new FileStream(labelFileName, FileMode.Create);
        LabelFile.Write(labelBuffer, 0, labelBuffer.Length);
        LabelFile.Close();
        // Display label in Acrobat
        DisplayLabel(labelFileName);
    }

    private static void DisplayLabel(string labelFileName)
    {
        System.Diagnostics.ProcessStartInfo info = new System.Diagnostics.ProcessStartInfo(labelFileName);
        info.UseShellExecute = true;
        info.Verb = "open";
        System.Diagnostics.Process.Start(info);
    }

    private void ShowNotifications(ProcessShipmentReply reply)
    {
        lblResult.Text += "Notifications<br>";
        for (int i = 0; i < reply.Notifications.Length; i++)
        {
            Notification notification = reply.Notifications[i];
            lblResult.Text += "Notification no. " + i + "<br>";
            lblResult.Text += " Severity: " + notification.Severity + "<br>";
            lblResult.Text += " Code: " + notification.Code + "<br>";
            lblResult.Text += " Message: " + notification.Message + "<br>";
            lblResult.Text += " Source: " + notification.Source + "<br>";
        }
    }
    #endregion
    protected void btnShippCalculation_Click(object sender, EventArgs e)
    {
        bool isCodShipment = true;
        ProcessShipmentRequest request = CreateShipmentRequest(isCodShipment);
        //
        ShipService service = new ShipService(); // Initialize the service
        //
        try
        {
            // Call the ship web service passing in a ProcessShipmentRequest and returning a ProcessShipmentReply
            ProcessShipmentReply reply = service.processShipment(request);
            if (reply.HighestSeverity == NotificationSeverityType.SUCCESS || reply.HighestSeverity == NotificationSeverityType.NOTE || reply.HighestSeverity == NotificationSeverityType.WARNING)
            {
                ShowShipmentReply(isCodShipment, reply);
            }
            ShowNotifications(reply);
        }
        catch (SoapException ex)
        {
            lblResult.Text += "ex.Detail.InnerText<br>";
        }           
    }
}