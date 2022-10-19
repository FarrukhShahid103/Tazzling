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
using System.Text;
using System.IO;
using System.Net;
using iTextSharp.text.pdf;
using iTextSharp.text;
using iTextSharp.text.html;
using iTextSharp.text.html.simpleparser;
using System.Threading;
using System.Text.RegularExpressions;
using System.Xml;
using System.Net.Mail;
using System.Data.SqlClient;
using SQLHelper;
using System.Drawing;
using System.Security.Cryptography;
using System.ComponentModel;
using ExactTargetAPI;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;


/// <summary>
/// Summary description for Misc
/// </summary>
public static class Misc
{
    public static string connStr = System.Configuration.ConfigurationManager.ConnectionStrings["CRMConnectionString"].ToString();
    
    public static int pageSize = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["pageSize"]);
    
    public static int clientPageSize = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["clienPageSize"]);

    public static bool validateUserName(string strName)
    {
        try
        {
            string strREemail = "^[a-zA-Z ]+$";
            if (Regex.Match(strName, strREemail).Success)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public static bool validatePasswordForSignup(string strPassword)
    {
        try
        {
            string strREemail = "([a-zA-Z0-9]{6,15})$";
            if (Regex.Match(strPassword, strREemail).Success)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public static bool validateEmailAddress(string strEmail)
    {
        try
        {
            string strREemail = "^(([a-zA-Z0-9_\\-\\.]+)@([a-zA-Z0-9_\\-\\.]+)\\.([a-zA-Z]{2,5}){1,25})+([;.](([a-zA-Z0-9_\\-\\.]+)@([a-zA-Z0-9_\\-\\.]+)\\.([a-zA-Z]{2,5}){1,25})+)*$";
            if (Regex.Match(strEmail, strREemail).Success)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public static bool validateAddress(string strAddress)
    {
        try
        {
            string sPath = "http://maps.google.com/maps/geo?q=" + strAddress + "&output=csv&key=" + System.Configuration.ConfigurationManager.AppSettings.Get("GoogleKey").ToString();
            WebClient client = new WebClient();
            string[] eResult = client.DownloadString(sPath).ToString().Split(',');
            if (eResult.Length > 3 && Convert.ToDouble(eResult.GetValue(3).ToString()) != 0 && Convert.ToDouble(eResult.GetValue(1).ToString()) != 0)
            {
                return true;
            }
            return false;
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public static void DeleteWorksheet(string phFileName)
    {
        //Microsoft.Office.Interop.Excel.Application xlApp = default(Microsoft.Office.Interop.Excel.Application);
        //Microsoft.Office.Interop.Excel.Workbook xlWorkBook = default(Microsoft.Office.Interop.Excel.Workbook);

        //Microsoft.Office.Interop.Excel.Worksheet deleteWS = default(Microsoft.Office.Interop.Excel.Worksheet);
        //xlApp = new Microsoft.Office.Interop.Excel.ApplicationClass();
        //xlWorkBook = xlApp.Workbooks.Open(phFileName, System.Reflection.Missing.Value, false, System.Reflection.Missing.Value, System.Reflection.Missing.Value, System.Reflection.Missing.Value, System.Reflection.Missing.Value, System.Reflection.Missing.Value, System.Reflection.Missing.Value, System.Reflection.Missing.Value, System.Reflection.Missing.Value, System.Reflection.Missing.Value, System.Reflection.Missing.Value, System.Reflection.Missing.Value, System.Reflection.Missing.Value);//uc 4 plugin


        //deleteWS = (Microsoft.Office.Interop.Excel.Worksheet)xlWorkBook.Worksheets[1];
        //deleteWS.Delete();
        //xlWorkBook.Save();
        ////xlWorkBook.Close();
        //xlApp.Quit();

        //xlApp = null;
        //xlWorkBook = null;
    }


    public static string getMapValues(string strAddress)
    {
        try
        {
            string sPath = "http://maps.google.com/maps/geo?q=" + strAddress + "&output=csv&key=" + System.Configuration.ConfigurationManager.AppSettings.Get("GoogleKey").ToString();
            WebClient client = new WebClient();
            string[] eResult = client.DownloadString(sPath).ToString().Split(',');
            if (eResult.Length > 3)
            {
                return eResult[2] + ", " + eResult[3];
            }
            return "";
            //if (eResult.Length > 3 && Convert.ToDouble(eResult.GetValue(3).ToString()) != 0 && Convert.ToDouble(eResult.GetValue(1).ToString()) != 0)
            //{
            //    return "";
            //}
            //return "";
        }
        catch (Exception ex)
        {
            return "";
        }
    }

    public static string getMapValuesString(string strAddress)
    {
        try
        {
            string sPath = "http://maps.google.com/maps/geo?q=" + strAddress + "&output=csv&key=" + System.Configuration.ConfigurationManager.AppSettings.Get("GoogleKey").ToString();
            string strReturn = "";
            WebClient client = new WebClient();
            string[] eResult = client.DownloadString(sPath).ToString().Split(',');
            string strSecondValue="Not Assigned";
            if (eResult.Length > 3)
            {
                strSecondValue = eResult[2] + ", " + eResult[3];
            }
            else
            {
                strSecondValue= eResult.Length.ToString();
            }
            string strNew = client.DownloadString(sPath).ToString();
            return strReturn =sPath+" "+ strSecondValue + " " + strNew;
            //if (eResult.Length > 3 && Convert.ToDouble(eResult.GetValue(3).ToString()) != 0 && Convert.ToDouble(eResult.GetValue(1).ToString()) != 0)
            //{
            //    return "";
            //}
            //return "";
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }

    public static void addSubscriberEmail(string strEmail, string strCityID)
    {
        try
        {           
            BLLNewsLetterSubscriber obj = new BLLNewsLetterSubscriber();
            obj.Email = strEmail;
            obj.CityId = Convert.ToInt64(strCityID);
            DataTable dtEmail = obj.getNewsLetterSubscriberByEmailCityId2();            
            if (dtEmail != null && dtEmail.Rows.Count == 0)
            {
                obj.Status = true;
                obj.friendsReferralId = "";
                obj.createNewsLetterSubscriber();
            }
            else
            {
                obj.Status = true;
                obj.SId = Convert.ToInt64(dtEmail.Rows[0]["SId"].ToString().Trim());
                obj.changeSubscriberStatus();
            }
            manageExactTargetSubscriberList(strEmail, Convert.ToInt32(strCityID));
            try
            {
                unSubscribeFromCompaignMonitor(strEmail);
            }
            catch (Exception ex)
            {
 
            }
        }
        catch (Exception ex)
        {
            unSubscribeFromCompaignMonitor(strEmail);
        }
    }

    public static void unSubscribeUser(string strEmail, int listID)
    {
        if (listID == 337)
        {
            listID = 215;
        }
        else if (listID == 338)        
        {
            listID = 613;
        }

        SoapClient client = new SoapClient();
        client.ClientCredentials.UserName.UserName = ConfigurationManager.AppSettings["ExactTargetAPIID"].ToString();
        client.ClientCredentials.UserName.Password = ConfigurationManager.AppSettings["ExactTargetAPIPwd"].ToString();

        Subscriber sub = new Subscriber();
        sub.SubscriberKey = strEmail;//required //may not be active in all accounts //use EmailAddress instead  
        sub.EmailAddress = strEmail;//required  

        sub.Status = SubscriberStatus.Active;
        sub.StatusSpecified = true;

        //Create an Array of Lists  
        sub.Lists = new SubscriberList[1];//If a list is not specified the Subscriber will be added to the "All Subscribers" List  

        sub.Lists[0] = new SubscriberList();
        sub.Lists[0].ID = listID;//Available in the UI via List Properties  
        sub.Lists[0].IDSpecified = true;
        sub.Lists[0].Status = SubscriberStatus.Unsubscribed;
        sub.Lists[0].StatusSpecified = true;
        sub.Lists[0].Action = "update";
        sub.Attributes = new ExactTargetAPI.Attribute[1];

        sub.Attributes[0] = new ExactTargetAPI.Attribute();
        sub.Attributes[0].Name = "Status";
        sub.Attributes[0].Value = "Unsubscribed";
        try
        {
            string uRequestID = String.Empty;
            string uStatus = String.Empty;
            UpdateResult[] uResults = client.Update(new UpdateOptions(), new APIObject[] { sub }, out uRequestID, out uStatus);
        }
        catch (Exception ex)
        {
        }
    }

    public static void unSubscribeFromCompaignMonitor(string strEmail)
    {        
        try
        {
            createsend_dotnet.Subscriber subsc1 = new createsend_dotnet.Subscriber("98dda76b7a23901888451f2a87081b28");
            bool str = subsc1.Unsubscribe(strEmail);
        }
        catch (Exception ex)
        {
        }

        try
        {
            createsend_dotnet.Subscriber subsc1 = new createsend_dotnet.Subscriber("d15ee73cf8443219031eb3d9c3ccab47");
            bool str = subsc1.Unsubscribe(strEmail);
        }
        catch (Exception ex)
        {
        }

        try
        {
            createsend_dotnet.Subscriber subsc1 = new createsend_dotnet.Subscriber("0a628e9eda46d1be9c06d2cd08b7f499");
            bool str = subsc1.Unsubscribe(strEmail);
        }
        catch (Exception ex)
        {
        }

        try
        {
            createsend_dotnet.Subscriber subsc1 = new createsend_dotnet.Subscriber("142c46265b5cf54d50faeb1cb93a58b6");
            bool str = subsc1.Unsubscribe(strEmail);
        }
        catch (Exception ex)
        {
        }

        try
        {
            createsend_dotnet.Subscriber subsc1 = new createsend_dotnet.Subscriber("257ae9d7c53ced27772e0f7468eb8c56");
            bool str = subsc1.Unsubscribe(strEmail);
        }
        catch (Exception ex)
        {
        }

        try
        {
            createsend_dotnet.Subscriber subsc1 = new createsend_dotnet.Subscriber("57b1c18efcd4a489be956cef28df7ab6");
            bool str = subsc1.Unsubscribe(strEmail);
        }
        catch (Exception ex)
        {
        }

        try
        {
            createsend_dotnet.Subscriber subsc1 = new createsend_dotnet.Subscriber("140556b11bfa3f7075df5db55ce97749");
            bool str = subsc1.Unsubscribe(strEmail);
        }
        catch (Exception ex)
        {
        }

        try
        {
            createsend_dotnet.Subscriber subsc1 = new createsend_dotnet.Subscriber("4f1c53c0ab2572319474d78d570070b3");
            bool str = subsc1.Unsubscribe(strEmail);
        }
        catch (Exception ex)
        {
        }

        try
        {
            createsend_dotnet.Subscriber subsc1 = new createsend_dotnet.Subscriber("ea499736c8dfc227a81511c30fa16ca6");
            bool str = subsc1.Unsubscribe(strEmail);
        }
        catch (Exception ex)
        {
        }

        try
        {
            createsend_dotnet.Subscriber subsc1 = new createsend_dotnet.Subscriber("1d1cf4cc7d49c20fd89eff3493c6f1ce");
            bool str = subsc1.Unsubscribe(strEmail);
        }
        catch (Exception ex)
        {
        }

        try
        {
            createsend_dotnet.Subscriber subsc1 = new createsend_dotnet.Subscriber("5ded7bafacc4ec6e0c58eae5429fb0ae");
            bool str = subsc1.Unsubscribe(strEmail);
        }
        catch (Exception ex)
        {
        }

        try
        {
            createsend_dotnet.Subscriber subsc1 = new createsend_dotnet.Subscriber("71c5249d1aaa13c696b10aebbde7dddd");
            bool str = subsc1.Unsubscribe(strEmail);
        }
        catch (Exception ex)
        {
        }
        try
        {
            createsend_dotnet.Subscriber subsc1 = new createsend_dotnet.Subscriber("8db2753c4cf483b0dd5ee29ef7619d3a");
            bool str = subsc1.Unsubscribe(strEmail);
        }
        catch (Exception ex)
        {
        }        
    }

    private static void manageExactTargetSubscriberList(string strEmail, int cityID)
    {
        try
        {
            SoapClient client = new SoapClient();
            client.ClientCredentials.UserName.UserName = ConfigurationManager.AppSettings["ExactTargetAPIID"].ToString();
            client.ClientCredentials.UserName.Password = ConfigurationManager.AppSettings["ExactTargetAPIPwd"].ToString();

            SimpleFilterPart sfp = new SimpleFilterPart();
            sfp.Property = "SubscriberKey";
            sfp.SimpleOperator = SimpleOperators.equals;
            sfp.Value = new string[] { strEmail };

            // Create the RetrieveRequest ListSubscriber objects
            RetrieveRequest rr = new RetrieveRequest();
            rr.ObjectType = "ListSubscriber";
            rr.Properties = new string[] { "ListID", "SubscriberKey", "Status" };
            rr.Filter = sfp;

            // Execute the Retrieve call
            APIObject[] results;
            string requestID;
            //Change integrationFramework to the name of your own development environment
            string status = client.Retrieve(rr, out requestID, out results);
            bool changeStatusNot = true;
            bool existInNationalList = false;
            if (status.Trim().ToLower() == "ok")
            {
                for (int i = 0; i < results.Length; i++)
                {
                    ListSubscriber ls = (ListSubscriber)results[i];
                    if (cityID == 337)
                    {
                        if (ls.ListID == 215 && ls.Status == SubscriberStatus.Unsubscribed)
                        {
                            changeStatusNot = false;
                            updateSubscriberStatus(strEmail, 215);
                        }
                    }
                    else if (cityID == 338)
                    {
                        if (ls.ListID == 613 && ls.Status == SubscriberStatus.Unsubscribed)
                        {
                            changeStatusNot = false;
                            updateSubscriberStatus(strEmail, 613);
                        }
                    }
                    else if (ls.ListID == cityID)
                    {
                        changeStatusNot = false;
                        updateSubscriberStatus(strEmail, cityID);
                    }

                    if (ls.ListID == 8129 && ls.Status == SubscriberStatus.Unsubscribed)
                    {
                        existInNationalList = true;
                        updateSubscriberStatus(strEmail, 8129);
                    }

                    // Console.WriteLine("List Subscriber Details:\t{0}\t{1}\t{2}", ls.ListID, ls.SubscriberKey, ls.Status);
                }
                if (changeStatusNot)
                {
                    if (cityID == 337)
                    {
                        AddSubscriberToExactTargetList(strEmail, 215);
                    }
                    else if (cityID == 338)
                    {
                        AddSubscriberToExactTargetList(strEmail, 613);
                    }
                    else
                    {
                        AddSubscriberToExactTargetList(strEmail, cityID);
                    }
                    if (!existInNationalList)
                    {
                        AddSubscriberToExactTargetList(strEmail, 8129);
                    }
                }

                if (!existInNationalList && !changeStatusNot)
                {
                    AddSubscriberToExactTargetList(strEmail, 8129);
                }
            }
        }
        catch (Exception ex)
        {

        }
    }

    private static void updateSubscriberStatus(string strEmail, int listID)
    {
        SoapClient client = new SoapClient();
        client.ClientCredentials.UserName.UserName = ConfigurationManager.AppSettings["ExactTargetAPIID"].ToString();
        client.ClientCredentials.UserName.Password = ConfigurationManager.AppSettings["ExactTargetAPIPwd"].ToString();

        Subscriber sub = new Subscriber();
        sub.SubscriberKey = strEmail;//required //may not be active in all accounts //use EmailAddress instead  
        sub.EmailAddress = strEmail;//required  
        
        sub.Status = SubscriberStatus.Active;
        sub.StatusSpecified = true;
        //Create an Array of Lists  
        sub.Lists = new SubscriberList[1];//If a list is not specified the Subscriber will be added to the "All Subscribers" List  

        sub.Lists[0] = new SubscriberList();
        sub.Lists[0].ID = listID;//Available in the UI via List Properties  
        sub.Lists[0].IDSpecified = true;
        sub.Lists[0].Status = SubscriberStatus.Active;
        sub.Lists[0].StatusSpecified = true;
        sub.Lists[0].Action = "update";
        sub.Attributes = new ExactTargetAPI.Attribute[1];

        sub.Attributes[0] = new ExactTargetAPI.Attribute();
        sub.Attributes[0].Name = "Status";
        sub.Attributes[0].Value = "Active";
        try
        {
            string uRequestID = String.Empty;
            string uStatus = String.Empty;
            UpdateResult[] uResults = client.Update(new UpdateOptions(), new APIObject[] { sub }, out uRequestID, out uStatus);
        }
        catch (Exception ex)
        {
        }

    }

    private static void AddSubscriberToExactTargetList(string strEmail, int listID)
    {
        SoapClient client = new SoapClient();
        client.ClientCredentials.UserName.UserName = ConfigurationManager.AppSettings["ExactTargetAPIID"].ToString();
        client.ClientCredentials.UserName.Password = ConfigurationManager.AppSettings["ExactTargetAPIPwd"].ToString();

        Subscriber sub = new Subscriber();
        sub.SubscriberKey = strEmail;//required //may not be active in all accounts //some choose to set this to email address
        sub.EmailAddress = strEmail;//required

        sub.Status = SubscriberStatus.Active;
        sub.StatusSpecified = true;

        //Create an Array of Lists
        sub.Lists = new SubscriberList[1];//If a list is not specified the Subscriber will be added to the "All Subscribers" List
        sub.Lists[0] = new SubscriberList();
        sub.Lists[0].ID = listID;//Available in the UI via List Properties
        sub.Lists[0].IDSpecified = true;

        //Subscriber Attributes differ per account.  Some may be required to create a Subscriber
        sub.Attributes = new ExactTargetAPI.Attribute[1];
        sub.Attributes[0] = new ExactTargetAPI.Attribute();
        sub.Attributes[0].Name = "Status";
        sub.Attributes[0].Value = "Active";
        //Create the CreateOptions object for the Create method
        //UnsubEvent eventun = new UnsubEvent();
        //eventun.ema
        CreateOptions co = new CreateOptions();
        co.SaveOptions = new SaveOption[1];
        co.SaveOptions[0] = new SaveOption();
        co.SaveOptions[0].SaveAction = SaveAction.UpdateAdd;//This set this call to act as an UpSert, meaning if the Subscriber doesn't exist it will Create if it does it will Update
        co.SaveOptions[0].PropertyName = "*";

        try
        {
            string cRequestID = String.Empty;
            string cStatus = String.Empty;
            //Call the Create method on the Subscriber object
            CreateResult[] cResults = client.Create(co, new APIObject[] { sub }, out cRequestID, out cStatus);
        }
        catch (Exception ex)
        { }
    }

    public static void addSubscriberEmailForAdmin(string strEmail, string strCityID)
    {
        try
        {
            BLLNewsLetterSubscriber obj = new BLLNewsLetterSubscriber();
            obj.Email = strEmail;
            obj.CityId = Convert.ToInt64(strCityID);
            DataTable dtEmail = obj.getNewsLetterSubscriberByEmailCityId2();
            if (dtEmail != null && dtEmail.Rows.Count == 0)
            {
                obj.Status = true;
                obj.friendsReferralId = "";
                obj.createNewsLetterSubscriber();
            }
        }
        catch (Exception ex)
        {

        }
    }

    #region Functions that help to create deal PDF File
    
    static string StripHTML(string inputString)
    {
        return Regex.Replace
          (inputString, @"</?\w+((\s+\w+(\s*=\s*(?:"".*?""|'.*?'|[^'"">\s]+))?)+\s*|\s*)/?>", string.Empty);
    }

    public static void createPDF(string orderID)
    {
        try
        {
            BLLDealOrders objOrders = new BLLDealOrders();
            BLLDealOrderDetail objDetail = new BLLDealOrderDetail();
            DataTable dtOrdersInfo = null;
            DataTable dtOrdersdetail = null;
            GECEncryption objEnc = new GECEncryption();

            objOrders.dOrderID = Convert.ToInt64(orderID);
            dtOrdersInfo = objOrders.getDealOrderDetailByOrderID();
            //string strImage = "";
            if ((dtOrdersInfo != null) && (dtOrdersInfo.Rows.Count > 0))
            {
                int a = 0;
                objDetail.dOrderID = Convert.ToInt64(orderID);
                dtOrdersdetail = objDetail.getAllDealOrderDetailsByOrderID();
                if (dtOrdersdetail != null && dtOrdersdetail.Rows.Count > 0)
                {
                    string strMapImagPath = "";
                    try
                    {
                        if (dtOrdersInfo.Rows[0]["Address"] != null && dtOrdersInfo.Rows[0]["Address"].ToString() != "")
                        {
                            System.Drawing.Image _Image = null;
                            _Image = DownloadImage("http://maps.google.com/maps/api/staticmap?center=" + dtOrdersInfo.Rows[0]["Address"].ToString() + "&zoom=14&size=300x200&maptype=roadmap%20&markers=color:red|color:red|label:A|" + getMapValues(dtOrdersInfo.Rows[0]["Address"].ToString()) + "&sensor=false");

                            // check for valid image
                            if (_Image != null)
                            {
                                _Image.Save(AppDomain.CurrentDomain.BaseDirectory + "Images\\ClientData\\" + orderID + ".png");
                                strMapImagPath = AppDomain.CurrentDomain.BaseDirectory + "Images\\ClientData\\" + orderID + ".png";
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        strMapImagPath = "";
                    }
                    for (int i = 0; i < dtOrdersdetail.Rows.Count; i++)
                    {
                        try
                        {
                            Document doc = new Document(PageSize.A4, 10, 10, 20, 10);
                            PdfWriter.GetInstance(doc, new FileStream(AppDomain.CurrentDomain.BaseDirectory + "Images\\ClientData\\" + objEnc.DecryptData("deatailOrder", dtOrdersdetail.Rows[i]["dealOrderCode"].ToString()) + ".pdf", FileMode.Create));
                            doc.Open();

                            PdfPTable table = new PdfPTable(4);

                            string imageFilePath = AppDomain.CurrentDomain.BaseDirectory + "\\images\\biglogo.png";
                            iTextSharp.text.Image jpg = iTextSharp.text.Image.GetInstance(imageFilePath);
                            jpg.ScaleToFit(124f, 51f);
                            PdfPCell cell = new PdfPCell(jpg);
                            cell.PaddingLeft = 5;
                            cell.PaddingTop = 10;
                            cell.BorderWidthRight = 0;
                            cell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
                            cell.Colspan = 1;
                            table.AddCell(cell);

                            iTextSharp.text.Font OrderNumberRight = new iTextSharp.text.Font(iTextSharp.text.Font.GetFamilyIndex("Arial"), 26, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.BLACK);
                            PdfPCell cell0 = new PdfPCell(new iTextSharp.text.Phrase("# " + objEnc.DecryptData("deatailOrder", dtOrdersdetail.Rows[i]["dealOrderCode"].ToString()), OrderNumberRight));
                            cell0.HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT;
                            cell0.BorderColorLeft = iTextSharp.text.Color.WHITE;
                            cell0.PaddingRight = 5;
                            cell0.PaddingTop = 10;
                            cell0.Colspan = 3;
                            
                            cell0.BorderWidthLeft = 0;
                            cell.BorderWidthBottom = 0;
                            table.AddCell(cell0);

                            OrderNumberRight = new iTextSharp.text.Font(iTextSharp.text.Font.GetFamilyIndex("Arial"), 18, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.BLACK);
                            PdfPCell cell_VSC = new PdfPCell(new iTextSharp.text.Phrase(dtOrdersdetail.Rows[i]["voucherSecurityCode"].ToString(), OrderNumberRight));
                            cell_VSC.HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT;
                            cell_VSC.BorderColorLeft = iTextSharp.text.Color.WHITE;
                            cell_VSC.Colspan = 4;
                            cell_VSC.Padding = 3;
                            cell.BorderWidthTop = 0;
                            table.AddCell(cell_VSC);

                            OrderNumberRight = new iTextSharp.text.Font(iTextSharp.text.Font.GetFamilyIndex("Arial"), 13, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.BLACK);
                            PdfPCell cellTitle = new PdfPCell(new iTextSharp.text.Phrase(dtOrdersInfo.Rows[0]["restaurantBusinessName"].ToString(), OrderNumberRight));
                            cellTitle.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
                            cellTitle.Colspan = 4;
                            cellTitle.PaddingLeft = 15;
                            cellTitle.BorderWidthBottom = 0f;
                            cellTitle.BorderWidthTop = 0f;
                            table.AddCell(cellTitle);

                            OrderNumberRight = new iTextSharp.text.Font(iTextSharp.text.Font.GetFamilyIndex("Arial"), 10, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.BLACK);
                            PdfPCell cell1 = new PdfPCell(new iTextSharp.text.Phrase(dtOrdersInfo.Rows[0]["title"].ToString(), OrderNumberRight));
                            cell1.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
                            cell1.Colspan = 4;
                            cell1.PaddingLeft = 15;
                            cell1.BorderWidthBottom = 0f;
                            cell1.BorderWidthTop = 0f;
                            table.AddCell(cell1);

                            OrderNumberRight = new iTextSharp.text.Font(iTextSharp.text.Font.GetFamilyIndex("Arial"), 10, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.BLACK);
                            PdfPCell cellRescipent = new PdfPCell(new iTextSharp.text.Phrase("Recipients:", OrderNumberRight));
                            cellRescipent.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
                            cellRescipent.Colspan = 4;
                            cellRescipent.PaddingTop = 20;
                            cellRescipent.PaddingLeft = 15;
                            cellRescipent.BorderWidthBottom = 0f;
                            cellRescipent.BorderWidthTop = 0f;
                            
                            table.AddCell(cellRescipent);

                            //OrderNumberRight = new iTextSharp.text.Font(iTextSharp.text.Font.GetFamilyIndex("Arial"), 10, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.BLACK);
                            //PdfPCell cellRedeem = new PdfPCell(new iTextSharp.text.Phrase("Redeem at:", OrderNumberRight));
                            //cellRedeem.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
                            //cellRedeem.Colspan = 2;
                            //cellRedeem.PaddingLeft = 15;
                            //cellRedeem.PaddingTop = 20;
                            //cellRedeem.BorderWidthBottom = 0f;
                            //cellRedeem.BorderWidthTop = 0f;
                            //cellRedeem.BorderWidthLeft = 0f;
                            //table.AddCell(cellRedeem);

                            OrderNumberRight = new iTextSharp.text.Font(iTextSharp.text.Font.GetFamilyIndex("Arial"), 10, iTextSharp.text.Font.NORMAL, iTextSharp.text.Color.BLACK);
                            PdfPCell cellRescipentName = new PdfPCell(new iTextSharp.text.Phrase(dtOrdersInfo.Rows[0]["ccInfoDFirstName"].ToString() + " " + dtOrdersInfo.Rows[0]["ccInfoDLastName"].ToString(), OrderNumberRight));
                            cellRescipentName.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
                            cellRescipentName.Colspan = 4;
                            cellRescipentName.PaddingLeft = 15;
                            cellRescipentName.BorderWidthBottom = 0f;
                            cellRescipentName.BorderWidthTop = 0f;
                            
                            table.AddCell(cellRescipentName);

                            //OrderNumberRight = new iTextSharp.text.Font(iTextSharp.text.Font.GetFamilyIndex("Arial"), 10, iTextSharp.text.Font.NORMAL, iTextSharp.text.Color.BLACK);
                            //PdfPCell cellRedeemAt = new PdfPCell(new iTextSharp.text.Phrase("Redeem on location", OrderNumberRight));
                            //cellRedeemAt.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
                            //cellRedeemAt.Colspan = 2;
                            //cellRedeemAt.PaddingLeft = 15;
                            //cellRedeemAt.BorderWidthBottom = 0f;
                            //cellRedeemAt.BorderWidthTop = 0f;
                            //cellRedeemAt.BorderWidthLeft = 0f;
                            //table.AddCell(cellRedeemAt);

                            OrderNumberRight = new iTextSharp.text.Font(iTextSharp.text.Font.GetFamilyIndex("Arial"), 10, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.BLACK);
                            PdfPCell cellFinePrintTitle = new PdfPCell(new iTextSharp.text.Phrase("Fine Print:", OrderNumberRight));
                            cellFinePrintTitle.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
                            cellFinePrintTitle.Colspan = 4;
                            cellFinePrintTitle.PaddingTop = 10;
                            cellFinePrintTitle.PaddingLeft = 15;
                            cellFinePrintTitle.BorderWidthBottom = 0f;
                            cellFinePrintTitle.BorderWidthTop = 0f;
                            table.AddCell(cellFinePrintTitle);

                            OrderNumberRight = new iTextSharp.text.Font(iTextSharp.text.Font.GetFamilyIndex("Arial"), 10, iTextSharp.text.Font.NORMAL, iTextSharp.text.Color.BLACK);
                            string str = StripHTML(dtOrdersInfo.Rows[0]["finePrint"].ToString().Replace("</p>", "\n").Replace("<br>", "\n").Replace("</br>", "\n").Replace("&nbsp;", " "));
                            PdfPCell cellPrintDesc = new PdfPCell(new iTextSharp.text.Phrase(str, OrderNumberRight));
                            cellPrintDesc.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
                            cellPrintDesc.Colspan = 4;
                            cellPrintDesc.SpaceCharRatio = 10;
                            cellPrintDesc.PaddingLeft = 15;
                            cellPrintDesc.BorderWidthBottom = 0f;
                            cellPrintDesc.BorderWidthTop = 0f;
                            table.AddCell(cellPrintDesc);

                            OrderNumberRight = new iTextSharp.text.Font(iTextSharp.text.Font.GetFamilyIndex("Arial"), 12, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.BLACK);
                            PdfPCell cellCompanyInfo = new PdfPCell(new iTextSharp.text.Phrase("Merchant Info:", OrderNumberRight));
                            cellCompanyInfo.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
                            cellCompanyInfo.Colspan = 4;
                            cellCompanyInfo.PaddingTop = 10;
                            cellCompanyInfo.PaddingLeft = 15;
                            cellCompanyInfo.BorderWidthBottom = 0f;
                            cellCompanyInfo.BorderWidthTop = 0f;
                            table.AddCell(cellCompanyInfo);

                            OrderNumberRight = new iTextSharp.text.Font(iTextSharp.text.Font.GetFamilyIndex("Arial"), 10, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.BLACK);
                            PdfPCell cellCompanyName = new PdfPCell(new iTextSharp.text.Phrase("Merchant Name:", OrderNumberRight));
                            cellCompanyName.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
                            cellCompanyName.PaddingTop = 10;
                            cellCompanyName.PaddingLeft = 15;
                            cellCompanyName.BorderWidthBottom = 0f;
                            cellCompanyName.BorderWidthRight = 0f;
                            cellCompanyName.BorderWidthTop = 0f;
                            table.AddCell(cellCompanyName);

                            OrderNumberRight = new iTextSharp.text.Font(iTextSharp.text.Font.GetFamilyIndex("Arial"), 10, iTextSharp.text.Font.NORMAL, iTextSharp.text.Color.BLACK);
                            PdfPCell cellCompanyBName = new PdfPCell(new iTextSharp.text.Phrase(dtOrdersInfo.Rows[0]["restaurantBusinessName"].ToString(), OrderNumberRight));
                            cellCompanyBName.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
                            cellCompanyBName.Colspan = 3;
                            cellCompanyBName.PaddingTop = 10;
                            cellCompanyBName.PaddingLeft = 15;
                            cellCompanyBName.BorderWidthBottom = 0f;
                            cellCompanyBName.BorderWidthLeft = 0f;
                            cellCompanyBName.BorderWidthTop = 0f;
                            table.AddCell(cellCompanyBName);

                            OrderNumberRight = new iTextSharp.text.Font(iTextSharp.text.Font.GetFamilyIndex("Arial"), 10, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.BLACK);
                            PdfPCell cellCompanyAddress = new PdfPCell(new iTextSharp.text.Phrase("Merchant Address:", OrderNumberRight));
                            cellCompanyAddress.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
                            cellCompanyAddress.PaddingTop = 10;
                            cellCompanyAddress.PaddingLeft = 15;
                            cellCompanyAddress.BorderWidthBottom = 0f;
                            cellCompanyAddress.BorderWidthRight = 0f;
                            cellCompanyAddress.BorderWidthTop = 0f;
                            table.AddCell(cellCompanyAddress);

                            OrderNumberRight = new iTextSharp.text.Font(iTextSharp.text.Font.GetFamilyIndex("Arial"), 10, iTextSharp.text.Font.NORMAL, iTextSharp.text.Color.BLACK);
                            PdfPCell cellCompanyBAddress = new PdfPCell(new iTextSharp.text.Phrase(dtOrdersInfo.Rows[0]["Address"].ToString(), OrderNumberRight));
                            cellCompanyBAddress.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
                            cellCompanyBAddress.Colspan = 3;
                            cellCompanyBAddress.PaddingTop = 10;
                            cellCompanyBAddress.PaddingLeft = 15;
                            cellCompanyBAddress.BorderWidthBottom = 0f;
                            cellCompanyBAddress.BorderWidthLeft = 0f;
                            cellCompanyBAddress.BorderWidthTop = 0f;
                            table.AddCell(cellCompanyBAddress);


                            OrderNumberRight = new iTextSharp.text.Font(iTextSharp.text.Font.GetFamilyIndex("Arial"), 10, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.BLACK);
                            PdfPCell cellFinePrintUniversal = new PdfPCell(new iTextSharp.text.Phrase("Universal Fine Print:", OrderNumberRight));
                            cellFinePrintUniversal.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
                            cellFinePrintUniversal.Colspan = 4;
                            cellFinePrintUniversal.PaddingTop = 15;
                            cellFinePrintUniversal.PaddingLeft = 15;
                            cellFinePrintUniversal.BorderWidthBottom = 0f;
                            cellFinePrintUniversal.BorderWidthTop = 0f;
                            table.AddCell(cellFinePrintUniversal);

                            OrderNumberRight = new iTextSharp.text.Font(iTextSharp.text.Font.GetFamilyIndex("Arial"), 7, iTextSharp.text.Font.NORMAL, iTextSharp.text.Color.BLACK);
                            PdfPCell cellUniversalPrintDesc = new PdfPCell(new iTextSharp.text.Phrase("Effective 48 hours after deal ends unless specified in fine print", OrderNumberRight));
                            cellUniversalPrintDesc.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
                            cellUniversalPrintDesc.Colspan = 4;
                            cellUniversalPrintDesc.PaddingLeft = 15;
                            cellUniversalPrintDesc.PaddingBottom = 15;
                            cellUniversalPrintDesc.BorderWidthTop = 0f;
                            table.AddCell(cellUniversalPrintDesc);



                            PdfPTable table2 = new PdfPTable(4);

                            OrderNumberRight = new iTextSharp.text.Font(iTextSharp.text.Font.GetFamilyIndex("Arial"), 14, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.BLACK);
                            PdfPCell HowToUse = new PdfPCell(new iTextSharp.text.Phrase("How to use this:", OrderNumberRight));
                            HowToUse.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
                            HowToUse.Colspan = 2;
                            HowToUse.PaddingTop = 30;
                            HowToUse.PaddingLeft = 15;
                            HowToUse.BorderWidth = 0;
                            table2.AddCell(HowToUse);

                            OrderNumberRight = new iTextSharp.text.Font(iTextSharp.text.Font.GetFamilyIndex("Arial"), 14, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.BLACK);
                            PdfPCell map = new PdfPCell(new iTextSharp.text.Phrase("Map:", OrderNumberRight));
                            map.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
                            map.Colspan = 2;
                            map.PaddingTop = 30;
                            map.PaddingBottom = 10;
                            map.PaddingLeft = 15;
                            map.BorderWidth = 0;
                            table2.AddCell(map);



                            OrderNumberRight = new iTextSharp.text.Font(iTextSharp.text.Font.GetFamilyIndex("Arial"), 11, iTextSharp.text.Font.NORMAL, iTextSharp.text.Color.BLACK);
                            string strGowToUse = StripHTML(dtOrdersInfo.Rows[0]["howtouse"].ToString().Replace("</p>", "\n").Replace("<br>", "\n").Replace("</br>", "\n").Replace("&nbsp;", " "));
                            PdfPCell HowToUseDesc = new PdfPCell(new iTextSharp.text.Phrase(strGowToUse, OrderNumberRight));
                            cellPrintDesc.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
                            HowToUseDesc.PaddingLeft = 15;
                            HowToUseDesc.Colspan = 2;
                            HowToUseDesc.PaddingBottom = 15;
                            HowToUseDesc.BorderWidth = 0;
                            table2.AddCell(HowToUseDesc);
                            if (strMapImagPath != "" && File.Exists(strMapImagPath))
                            {
                                string imageFileMap = strMapImagPath;
                                iTextSharp.text.Image jpgmap = iTextSharp.text.Image.GetInstance(imageFileMap);
                                jpgmap.ScaleToFit(200f, 130f);
                                PdfPCell cellMap = new PdfPCell(jpgmap);
                                cellMap.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
                                cellMap.PaddingLeft = 15;
                                HowToUseDesc.PaddingBottom = 15;
                                cellMap.Colspan = 2;
                                cellMap.BorderWidth = 0;
                                table2.AddCell(cellMap);
                            }
                            else
                            {
                                string imageFileMap = AppDomain.CurrentDomain.BaseDirectory + "\\images\\biglogo.png";
                                iTextSharp.text.Image jpgmap = iTextSharp.text.Image.GetInstance(imageFileMap);
                                jpgmap.ScaleToFit(124f, 51f);
                                PdfPCell cellMap = new PdfPCell(jpgmap);
                                cellMap.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
                                cellMap.PaddingLeft = 15;
                                HowToUseDesc.PaddingBottom = 15;
                                cellMap.Colspan = 2;
                                cellMap.BorderWidth = 0;
                                table2.AddCell(cellMap);
                            }
                            PdfPCell cellSpacing = new PdfPCell(new iTextSharp.text.Phrase(" "));
                            cellSpacing.Colspan = 4;
                            cellSpacing.Padding = 10;
                            cellSpacing.Border = 0;
                            table2.AddCell(cellSpacing);
                            OrderNumberRight = new iTextSharp.text.Font(iTextSharp.text.Font.GetFamilyIndex("Arial"), 7, iTextSharp.text.Font.NORMAL, iTextSharp.text.Color.BLACK);
                            PdfPCell cellSupport = new PdfPCell(new iTextSharp.text.Phrase("TastyGo \bSupport:\b (604) 295-1777 Monday-Sunday 10am-6pm PST         Email \bTastyGo\b: support@tazzling.com", OrderNumberRight));
                            cellSupport.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                            cellSupport.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
                            cellSupport.Colspan = 4;
                            cellSupport.BackgroundColor = new iTextSharp.text.Color(192, 192, 192);
                            cellSupport.PaddingTop = 5;
                            cellSupport.BorderWidth = 0;
                            cellSupport.PaddingBottom = 5;
                            table2.AddCell(cellSupport);

                            
                            doc.Add(table);
                            doc.Add(table2);
                            doc.Close();
                        }
                        catch (Exception ex)
                        { }
                        // strPDF = "ClientData/" + dtOrdersInfo.Rows[0]["orderNumber"].ToString() + ".pdf";
                        //ExportToUser(AppDomain.CurrentDomain.BaseDirectory + "Takeout\\ClientData\\" + dtOrdersInfo.Rows[0]["orderNumber"].ToString() + ".pdf", dtOrdersInfo.Rows[0]["orderNumber"].ToString() + ".pdf");
                        //Response.Redirect("~/ClientData/" + dtOrdersInfo.Rows[0]["orderNumber"].ToString() + ".pdf");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            //lblMessage.Visible = true;
            //lblMessage.Text = "There is an error occur, please email us at support@tazzling.com or call 1855-295-1771.";
            //imgGridMessage.Visible = true;
            //imgGridMessage.ImageUrl = "images/error.png";
            //lblMessage.ForeColor = System.Drawing.Color.Red;
        }
    }
    
    public static string createPDFForGift(string detailID, string orderID, string strRescipentName)
    {
        try
        {
            BLLDealOrders objOrders = new BLLDealOrders();
            BLLDealOrderDetail objDetail = new BLLDealOrderDetail();
            DataTable dtOrdersInfo = null;
            DataTable dtOrdersdetail = null;
            GECEncryption objEnc = new GECEncryption();

            objOrders.dOrderID = Convert.ToInt64(orderID);
            dtOrdersInfo = objOrders.getDealOrderDetailByOrderID();
            //string strImage = "";
            if ((dtOrdersInfo != null) && (dtOrdersInfo.Rows.Count > 0))
            {
                int a = 0;
                objDetail.dOrderID = Convert.ToInt64(orderID);
                dtOrdersdetail = objDetail.getAllDealOrderDetailsByOrderID();
                if (dtOrdersdetail != null && dtOrdersdetail.Rows.Count > 0)
                {
                    string strMapImagPath = "";
                    try
                    {
                        if (dtOrdersInfo.Rows[0]["Address"] != null && dtOrdersInfo.Rows[0]["Address"].ToString() != "")
                        {
                            System.Drawing.Image _Image = null;
                            _Image = DownloadImage("http://maps.google.com/maps/api/staticmap?center=" + dtOrdersInfo.Rows[0]["Address"].ToString() + "&zoom=14&size=300x200&maptype=roadmap%20&markers=color:red|color:red|label:A|" + getMapValues(dtOrdersInfo.Rows[0]["Address"].ToString()) + "&sensor=false");

                            // check for valid image
                            if (_Image != null)
                            {
                                _Image.Save(AppDomain.CurrentDomain.BaseDirectory + "Images\\ClientData\\" + orderID + ".png");
                                strMapImagPath = AppDomain.CurrentDomain.BaseDirectory + "Images\\ClientData\\" + orderID + ".png";
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        strMapImagPath = "";
                    }
                    for (int i = 0; i < dtOrdersdetail.Rows.Count; i++)
                    {
                        if (detailID.Trim() == dtOrdersdetail.Rows[i]["detailID"].ToString().Trim())
                        {
                            try
                            {
                                Document doc = new Document(PageSize.A4, 10, 10, 20, 10);
                                PdfWriter.GetInstance(doc, new FileStream(AppDomain.CurrentDomain.BaseDirectory + "Images\\ClientData\\" + objEnc.DecryptData("deatailOrder", dtOrdersdetail.Rows[i]["dealOrderCode"].ToString()) + ".pdf", FileMode.Create));
                                doc.Open();

                                PdfPTable table = new PdfPTable(4);

                                string imageFilePath = AppDomain.CurrentDomain.BaseDirectory + "\\images\\biglogo.png";
                                iTextSharp.text.Image jpg = iTextSharp.text.Image.GetInstance(imageFilePath);
                                jpg.ScaleToFit(124f, 51f);
                                PdfPCell cell = new PdfPCell(jpg);                                
                                cell.PaddingLeft = 5;
                                cell.PaddingTop = 10;
                                cell.BorderWidthRight = 0f;
                                cell.BorderWidthBottom = 0f;
                                cell.BorderColorBottom = iTextSharp.text.Color.WHITE;
                                cell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
                                cell.BackgroundColor = new iTextSharp.text.Color(0,174,255); 
                                                                
                                cell.Colspan = 1;
                                table.AddCell(cell);


                                iTextSharp.text.Font OrderNumberRight = new iTextSharp.text.Font(iTextSharp.text.Font.GetFamilyIndex("Arial"), 26, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.BLACK);
                                PdfPCell cell0 = new PdfPCell(new iTextSharp.text.Phrase("# " + objEnc.DecryptData("deatailOrder", dtOrdersdetail.Rows[i]["dealOrderCode"].ToString()), OrderNumberRight));
                                cell0.HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT;
                                cell0.BorderColorLeft = iTextSharp.text.Color.WHITE;
                                cell0.BorderColorBottom = iTextSharp.text.Color.WHITE;
                                cell0.PaddingRight = 5;
                                cell0.PaddingTop = 10;
                                cell0.Colspan = 3;                                
                                cell0.BorderWidthLeft = 0f;
                                cell0.BorderWidthBottom = 0f;
                                cell0.BackgroundColor = new iTextSharp.text.Color(0, 174, 255); 
                                table.AddCell(cell0);

                                OrderNumberRight = new iTextSharp.text.Font(iTextSharp.text.Font.GetFamilyIndex("Arial"), 18, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.BLACK);
                                PdfPCell cell_VSC = new PdfPCell(new iTextSharp.text.Phrase(dtOrdersdetail.Rows[i]["voucherSecurityCode"].ToString(), OrderNumberRight));
                                cell_VSC.HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT;                                
                                cell_VSC.Colspan = 4;
                                cell_VSC.Padding = 3;
                                cell_VSC.BorderWidthTop = 0f;
                                cell_VSC.BorderColorTop = iTextSharp.text.Color.WHITE;
                                table.AddCell(cell_VSC);

                                OrderNumberRight = new iTextSharp.text.Font(iTextSharp.text.Font.GetFamilyIndex("Arial"), 13, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.BLACK);
                                PdfPCell cellTitle = new PdfPCell(new iTextSharp.text.Phrase(dtOrdersInfo.Rows[0]["restaurantBusinessName"].ToString(), OrderNumberRight));
                                cellTitle.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
                                cellTitle.Colspan = 4;
                                cellTitle.PaddingLeft = 15;
                                cellTitle.BorderWidthBottom = 0f;
                                cellTitle.BorderWidthTop = 0f;
                                table.AddCell(cellTitle);

                                OrderNumberRight = new iTextSharp.text.Font(iTextSharp.text.Font.GetFamilyIndex("Arial"), 10, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.BLACK);
                                PdfPCell cell1 = new PdfPCell(new iTextSharp.text.Phrase(dtOrdersInfo.Rows[0]["title"].ToString(), OrderNumberRight));
                                cell1.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
                                cell1.Colspan = 4;
                                cell1.PaddingLeft = 15;
                                cell1.BorderWidthBottom = 0f;
                                cell1.BorderWidthTop = 0f;
                                table.AddCell(cell1);
                                if (Convert.ToBoolean(dtOrdersdetail.Rows[i]["isGift"]))
                                {
                                    OrderNumberRight = new iTextSharp.text.Font(iTextSharp.text.Font.GetFamilyIndex("Arial"), 10, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.BLACK);
                                    PdfPCell cellRescipent = new PdfPCell(new iTextSharp.text.Phrase("Gift To:", OrderNumberRight));
                                    cellRescipent.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
                                    cellRescipent.Colspan = 4;
                                    cellRescipent.PaddingTop = 20;
                                    cellRescipent.PaddingLeft = 15;
                                    cellRescipent.BorderWidthBottom = 0f;
                                    cellRescipent.BorderWidthTop = 0f;                                    
                                    table.AddCell(cellRescipent);
                                }
                                else
                                {
                                    OrderNumberRight = new iTextSharp.text.Font(iTextSharp.text.Font.GetFamilyIndex("Arial"), 10, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.BLACK);
                                    PdfPCell cellRescipent = new PdfPCell(new iTextSharp.text.Phrase("Recipients:", OrderNumberRight));
                                    cellRescipent.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
                                    cellRescipent.Colspan = 4;
                                    cellRescipent.PaddingTop = 20;
                                    cellRescipent.PaddingLeft = 15;
                                    cellRescipent.BorderWidthBottom = 0f;
                                    cellRescipent.BorderWidthTop = 0f;                                    
                                    table.AddCell(cellRescipent);
                                }
                                //OrderNumberRight = new iTextSharp.text.Font(iTextSharp.text.Font.GetFamilyIndex("Arial"), 10, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.BLACK);
                                //PdfPCell cellRedeem = new PdfPCell(new iTextSharp.text.Phrase("Redeem at:", OrderNumberRight));
                                //cellRedeem.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
                                //cellRedeem.Colspan = 2;
                                //cellRedeem.PaddingLeft = 15;
                                //cellRedeem.PaddingTop = 20;
                                //cellRedeem.BorderWidthBottom = 0f;
                                //cellRedeem.BorderWidthTop = 0f;
                                //cellRedeem.BorderWidthLeft = 0f;
                                //table.AddCell(cellRedeem);
                                if (Convert.ToBoolean(dtOrdersdetail.Rows[i]["isGift"]))
                                {
                                    OrderNumberRight = new iTextSharp.text.Font(iTextSharp.text.Font.GetFamilyIndex("Arial"), 10, iTextSharp.text.Font.NORMAL, iTextSharp.text.Color.BLACK);
                                    PdfPCell cellRescipentName = new PdfPCell(new iTextSharp.text.Phrase(strRescipentName, OrderNumberRight));
                                    cellRescipentName.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
                                    cellRescipentName.Colspan = 4;
                                    cellRescipentName.PaddingLeft = 15;
                                    cellRescipentName.BorderWidthBottom = 0f;
                                    cellRescipentName.BorderWidthTop = 0f;                                    
                                    table.AddCell(cellRescipentName);
                                }
                                else
                                {
                                    OrderNumberRight = new iTextSharp.text.Font(iTextSharp.text.Font.GetFamilyIndex("Arial"), 10, iTextSharp.text.Font.NORMAL, iTextSharp.text.Color.BLACK);
                                    PdfPCell cellRescipentName = new PdfPCell(new iTextSharp.text.Phrase(dtOrdersInfo.Rows[0]["ccInfoDFirstName"].ToString() + " " + dtOrdersInfo.Rows[0]["ccInfoDLastName"].ToString(), OrderNumberRight));
                                    cellRescipentName.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
                                    cellRescipentName.Colspan = 4;
                                    cellRescipentName.PaddingLeft = 15;
                                    cellRescipentName.BorderWidthBottom = 0f;
                                    cellRescipentName.BorderWidthTop = 0f;                                    
                                    table.AddCell(cellRescipentName);
                                }
                                //OrderNumberRight = new iTextSharp.text.Font(iTextSharp.text.Font.GetFamilyIndex("Arial"), 10, iTextSharp.text.Font.NORMAL, iTextSharp.text.Color.BLACK);
                                //PdfPCell cellRedeemAt = new PdfPCell(new iTextSharp.text.Phrase("Redeem on location", OrderNumberRight));
                                //cellRedeemAt.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
                                //cellRedeemAt.Colspan = 2;
                                //cellRedeemAt.PaddingLeft = 15;
                                //cellRedeemAt.BorderWidthBottom = 0f;
                                //cellRedeemAt.BorderWidthTop = 0f;
                                //cellRedeemAt.BorderWidthLeft = 0f;
                                //table.AddCell(cellRedeemAt);

                                OrderNumberRight = new iTextSharp.text.Font(iTextSharp.text.Font.GetFamilyIndex("Arial"), 10, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.BLACK);
                                PdfPCell cellFinePrintTitle = new PdfPCell(new iTextSharp.text.Phrase("Fine Print:", OrderNumberRight));
                                cellFinePrintTitle.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
                                cellFinePrintTitle.Colspan = 4;
                                cellFinePrintTitle.PaddingTop = 10;
                                cellFinePrintTitle.PaddingLeft = 15;
                                cellFinePrintTitle.BorderWidthBottom = 0f;
                                cellFinePrintTitle.BorderWidthTop = 0f;
                                table.AddCell(cellFinePrintTitle);

                                OrderNumberRight = new iTextSharp.text.Font(iTextSharp.text.Font.GetFamilyIndex("Arial"), 10, iTextSharp.text.Font.NORMAL, iTextSharp.text.Color.BLACK);
                                string str = StripHTML(dtOrdersInfo.Rows[0]["finePrint"].ToString().Replace("</p>", "\n").Replace("<br>", "\n").Replace("</br>", "\n").Replace("&nbsp;", " "));
                                PdfPCell cellPrintDesc = new PdfPCell(new iTextSharp.text.Phrase(str, OrderNumberRight));
                                cellPrintDesc.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
                                cellPrintDesc.Colspan = 4;
                                cellPrintDesc.SpaceCharRatio = 10;
                                cellPrintDesc.PaddingLeft = 15;
                                cellPrintDesc.BorderWidthBottom = 0f;
                                cellPrintDesc.BorderWidthTop = 0f;
                                table.AddCell(cellPrintDesc);

                                OrderNumberRight = new iTextSharp.text.Font(iTextSharp.text.Font.GetFamilyIndex("Arial"), 12, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.BLACK);
                                PdfPCell cellCompanyInfo = new PdfPCell(new iTextSharp.text.Phrase("Merchant Info:", OrderNumberRight));
                                cellCompanyInfo.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
                                cellCompanyInfo.Colspan = 4;
                                cellCompanyInfo.PaddingTop = 10;
                                cellCompanyInfo.PaddingLeft = 15;
                                cellCompanyInfo.BorderWidthBottom = 0f;
                                cellCompanyInfo.BorderWidthTop = 0f;
                                table.AddCell(cellCompanyInfo);

                                OrderNumberRight = new iTextSharp.text.Font(iTextSharp.text.Font.GetFamilyIndex("Arial"), 10, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.BLACK);
                                PdfPCell cellCompanyName = new PdfPCell(new iTextSharp.text.Phrase("Merchant Name:", OrderNumberRight));
                                cellCompanyName.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
                                cellCompanyName.PaddingTop = 10;
                                cellCompanyName.PaddingLeft = 15;
                                cellCompanyName.BorderWidthBottom = 0f;
                                cellCompanyName.BorderWidthRight = 0f;
                                cellCompanyName.BorderWidthTop = 0f;
                                table.AddCell(cellCompanyName);

                                OrderNumberRight = new iTextSharp.text.Font(iTextSharp.text.Font.GetFamilyIndex("Arial"), 10, iTextSharp.text.Font.NORMAL, iTextSharp.text.Color.BLACK);
                                PdfPCell cellCompanyBName = new PdfPCell(new iTextSharp.text.Phrase(dtOrdersInfo.Rows[0]["restaurantBusinessName"].ToString(), OrderNumberRight));
                                cellCompanyBName.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
                                cellCompanyBName.Colspan = 3;
                                cellCompanyBName.PaddingTop = 10;
                                cellCompanyBName.PaddingLeft = 15;
                                cellCompanyBName.BorderWidthBottom = 0f;
                                cellCompanyBName.BorderWidthLeft = 0f;
                                cellCompanyBName.BorderWidthTop = 0f;
                                table.AddCell(cellCompanyBName);

                                OrderNumberRight = new iTextSharp.text.Font(iTextSharp.text.Font.GetFamilyIndex("Arial"), 10, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.BLACK);
                                PdfPCell cellCompanyAddress = new PdfPCell(new iTextSharp.text.Phrase("Merchant Address:", OrderNumberRight));
                                cellCompanyAddress.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
                                cellCompanyAddress.PaddingTop = 10;
                                cellCompanyAddress.PaddingLeft = 15;
                                cellCompanyAddress.BorderWidthBottom = 0f;
                                cellCompanyAddress.BorderWidthRight = 0f;
                                cellCompanyAddress.BorderWidthTop = 0f;
                                table.AddCell(cellCompanyAddress);

                                OrderNumberRight = new iTextSharp.text.Font(iTextSharp.text.Font.GetFamilyIndex("Arial"), 10, iTextSharp.text.Font.NORMAL, iTextSharp.text.Color.BLACK);
                                PdfPCell cellCompanyBAddress = new PdfPCell(new iTextSharp.text.Phrase(dtOrdersInfo.Rows[0]["Address"].ToString(), OrderNumberRight));
                                cellCompanyBAddress.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
                                cellCompanyBAddress.Colspan = 3;
                                cellCompanyBAddress.PaddingTop = 10;
                                cellCompanyBAddress.PaddingLeft = 15;
                                cellCompanyBAddress.BorderWidthBottom = 0f;
                                cellCompanyBAddress.BorderWidthLeft = 0f;
                                cellCompanyBAddress.BorderWidthTop = 0f;
                                table.AddCell(cellCompanyBAddress);


                                OrderNumberRight = new iTextSharp.text.Font(iTextSharp.text.Font.GetFamilyIndex("Arial"), 10, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.BLACK);
                                PdfPCell cellFinePrintUniversal = new PdfPCell(new iTextSharp.text.Phrase("Universal Fine Print:", OrderNumberRight));
                                cellFinePrintUniversal.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
                                cellFinePrintUniversal.Colspan = 4;
                                cellFinePrintUniversal.PaddingTop = 15;
                                cellFinePrintUniversal.PaddingLeft = 15;
                                cellFinePrintUniversal.BorderWidthBottom = 0f;
                                cellFinePrintUniversal.BorderWidthTop = 0f;
                                table.AddCell(cellFinePrintUniversal);

                                OrderNumberRight = new iTextSharp.text.Font(iTextSharp.text.Font.GetFamilyIndex("Arial"), 7, iTextSharp.text.Font.NORMAL, iTextSharp.text.Color.BLACK);
                                PdfPCell cellUniversalPrintDesc = new PdfPCell(new iTextSharp.text.Phrase("Effective 48 hours after deal ends unless specified in fine print", OrderNumberRight));
                                cellUniversalPrintDesc.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
                                cellUniversalPrintDesc.Colspan = 4;
                                cellUniversalPrintDesc.PaddingLeft = 15;
                                cellUniversalPrintDesc.PaddingBottom = 15;
                                cellUniversalPrintDesc.BorderWidthTop = 0f;
                                table.AddCell(cellUniversalPrintDesc);



                                PdfPTable table2 = new PdfPTable(4);

                                OrderNumberRight = new iTextSharp.text.Font(iTextSharp.text.Font.GetFamilyIndex("Arial"), 14, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.BLACK);
                                PdfPCell HowToUse = new PdfPCell(new iTextSharp.text.Phrase("How to use this:", OrderNumberRight));
                                HowToUse.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
                                HowToUse.Colspan = 2;
                                HowToUse.PaddingTop = 30;
                                HowToUse.PaddingLeft = 15;
                                HowToUse.BorderWidth = 0;
                                table2.AddCell(HowToUse);

                                OrderNumberRight = new iTextSharp.text.Font(iTextSharp.text.Font.GetFamilyIndex("Arial"), 14, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.BLACK);
                                PdfPCell map = new PdfPCell(new iTextSharp.text.Phrase("Map:", OrderNumberRight));
                                map.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
                                map.Colspan = 2;
                                map.PaddingTop = 30;
                                map.PaddingBottom = 10;
                                map.PaddingLeft = 15;
                                map.BorderWidth = 0;
                                table2.AddCell(map);



                                OrderNumberRight = new iTextSharp.text.Font(iTextSharp.text.Font.GetFamilyIndex("Arial"), 11, iTextSharp.text.Font.NORMAL, iTextSharp.text.Color.BLACK);
                                string strGowToUse = StripHTML(dtOrdersInfo.Rows[0]["howtouse"].ToString().Replace("</p>", "\n").Replace("<br>", "\n").Replace("</br>", "\n").Replace("&nbsp;", " "));
                                PdfPCell HowToUseDesc = new PdfPCell(new iTextSharp.text.Phrase(strGowToUse, OrderNumberRight));
                                cellPrintDesc.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
                                HowToUseDesc.PaddingLeft = 15;
                                HowToUseDesc.Colspan = 2;
                                HowToUseDesc.PaddingBottom = 15;
                                HowToUseDesc.BorderWidth = 0;
                                table2.AddCell(HowToUseDesc);
                                if (strMapImagPath != "" && File.Exists(strMapImagPath))
                                {
                                    string imageFileMap = strMapImagPath;
                                    iTextSharp.text.Image jpgmap = iTextSharp.text.Image.GetInstance(imageFileMap);
                                    jpgmap.ScaleToFit(200f, 130f);
                                    PdfPCell cellMap = new PdfPCell(jpgmap);
                                    cellMap.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
                                    cellMap.PaddingLeft = 15;
                                    HowToUseDesc.PaddingBottom = 15;
                                    cellMap.Colspan = 2;
                                    cellMap.BorderWidth = 0;
                                    table2.AddCell(cellMap);
                                }
                                else
                                {
                                    string imageFileMap = AppDomain.CurrentDomain.BaseDirectory + "\\images\\biglogo.png";
                                    iTextSharp.text.Image jpgmap = iTextSharp.text.Image.GetInstance(imageFileMap);
                                    jpgmap.ScaleToFit(124f, 51f);
                                    PdfPCell cellMap = new PdfPCell(jpgmap);
                                    cellMap.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
                                    cellMap.PaddingLeft = 15;
                                    HowToUseDesc.PaddingBottom = 15;
                                    cellMap.Colspan = 2;
                                    cellMap.BorderWidth = 0;
                                    table2.AddCell(cellMap);
                                }
                                PdfPCell cellSpacing = new PdfPCell(new iTextSharp.text.Phrase(" "));
                                cellSpacing.Colspan = 4;
                                cellSpacing.Padding = 10;
                                cellSpacing.Border = 0;
                                table2.AddCell(cellSpacing);
                                OrderNumberRight = new iTextSharp.text.Font(iTextSharp.text.Font.GetFamilyIndex("Arial"), 7, iTextSharp.text.Font.NORMAL, iTextSharp.text.Color.BLACK);
                                PdfPCell cellSupport = new PdfPCell(new iTextSharp.text.Phrase("TastyGo \bSupport:\b (604) 295-1777 Monday-Sunday 10am-6pm PST         Email \bTastyGo\b: support@tazzling.com", OrderNumberRight));
                                cellSupport.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                                cellSupport.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
                                cellSupport.Colspan = 4;
                                cellSupport.BackgroundColor = new iTextSharp.text.Color(192, 192, 192);
                                cellSupport.PaddingTop = 5;
                                cellSupport.BorderWidth = 0;
                                cellSupport.PaddingBottom = 5;
                                table2.AddCell(cellSupport);


                                doc.Add(table);
                                doc.Add(table2);
                                doc.Close();
                            }
                            catch (Exception ex)
                            { }
                            return objEnc.DecryptData("deatailOrder", dtOrdersdetail.Rows[i]["dealOrderCode"].ToString());
                        }
                        // strPDF = "ClientData/" + dtOrdersInfo.Rows[0]["orderNumber"].ToString() + ".pdf";
                        //ExportToUser(AppDomain.CurrentDomain.BaseDirectory + "Takeout\\ClientData\\" + dtOrdersInfo.Rows[0]["orderNumber"].ToString() + ".pdf", dtOrdersInfo.Rows[0]["orderNumber"].ToString() + ".pdf");
                        //Response.Redirect("~/ClientData/" + dtOrdersInfo.Rows[0]["orderNumber"].ToString() + ".pdf");
                    }
                }
            }

        }
        catch (Exception ex)
        {
            //lblMessage.Visible = true;
            //lblMessage.Text = "There is an error occur, please email us at support@tazzling.com or call 1855-295-1771.";
            //imgGridMessage.Visible = true;
            //imgGridMessage.ImageUrl = "images/error.png";
            //lblMessage.ForeColor = System.Drawing.Color.Red;
        }
        return "";
    }

    public static bool createPDFForInvoice(long dealID, int intCityID)
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
            strQuery += " where restaurant.[restaurantId]= deals.[restaurantId] and [deals].dealid=" + dealID.ToString().Trim() + " and dealcity.cityid=" + intCityID.ToString().Trim() + "";
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

    public static System.Drawing.Image DownloadImage(string _URL)
    {
        System.Drawing.Image _tmpImage = null;

        try
        {
            // Open a connection
            System.Net.HttpWebRequest _HttpWebRequest = (System.Net.HttpWebRequest)System.Net.HttpWebRequest.Create(_URL);

            _HttpWebRequest.AllowWriteStreamBuffering = true;

            // You can also specify additional header values like the user agent or the referer: (Optional)
            _HttpWebRequest.UserAgent = "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 5.1)";
            _HttpWebRequest.Referer = "http://www.google.com/";

            // set timeout for 20 seconds (Optional)
            _HttpWebRequest.Timeout = 20000;

            // Request response:
            System.Net.WebResponse _WebResponse = _HttpWebRequest.GetResponse();

            // Open data stream:
            System.IO.Stream _WebStream = _WebResponse.GetResponseStream();

            // convert webstream to image
            _tmpImage = System.Drawing.Image.FromStream(_WebStream);

            // Cleanup
            _WebResponse.Close();
            _WebResponse.Close();
        }
        catch (Exception _Exception)
        {
            // Error
            Console.WriteLine("Exception caught in process: {0}", _Exception.ToString());
            return null;
        }

        return _tmpImage;
    }
    #endregion

    #region Public Method For The Sending Mail
    public static bool SendEmail(string to, string cc, string bcc, string from, string subject, string body)
    {
        try
        {
            //System.Net.Mail.MailMessage Mail = new System.Net.Mail.MailMessage();
            //Mail.To.Add(new MailAddress(to));
            ////Mail.Cc = cc;
            ////Mail.Bcc = bcc;            
            //Mail.From = new MailAddress(from);
            //Mail.Subject = subject;
            //Mail.IsBodyHtml = true;
            //Mail.Body = body;

            //SmtpClient emailClient = new SmtpClient(ConfigurationManager.AppSettings["SMTPServer"].ToString());

            //emailClient.Send(Mail);

            //return true;
            //*-********************************************************************
            NetworkCredential loginInfo = new NetworkCredential(ConfigurationManager.AppSettings["AdminEmail"].ToString(), ConfigurationManager.AppSettings["AdminPass"].ToString());
            //NetworkCredential loginInfo = new NetworkCredential("noreply@tazzling.com", "hinshou8");

            MailMessage msg = new MailMessage();
            msg.From = new MailAddress(ConfigurationManager.AppSettings["AdminEmail"].ToString(),"Tastygo");
            //msg.From = new MailAddress("noreply@tazzling.com", "Tastygo");
            if (to.Trim() == "noreply@tazzling.com")
            {
                to = "info@tazzling.com";
            }

            msg.To.Add(new MailAddress(to));

            msg.Subject = subject;
            
            msg.Body = body;
            msg.IsBodyHtml = true;
            SmtpClient client = new SmtpClient(ConfigurationManager.AppSettings["SMTPServer"].ToString(), Convert.ToInt32(ConfigurationManager.AppSettings["PortNumber"].ToString()));
            //SmtpClient client = new SmtpClient("mail.tazzling.com", 25);
            object userState = msg; 
            client.EnableSsl = true;
            
            client.UseDefaultCredentials = false;
            client.Credentials = loginInfo;            
           // client.SendCompleted += new SendCompletedEventHandler(SmtpClient_OnCompleted);
            //client.SendAsync(msg, userState);
            client.Send(msg);

            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public static bool SendEmailWithAttachment(string to, string cc, string bcc, string from, string subject, string body, string filePath)
    {
        try
        {
            NetworkCredential loginInfo = new NetworkCredential(ConfigurationManager.AppSettings["AdminEmail"].ToString(), ConfigurationManager.AppSettings["AdminPass"].ToString());
            //NetworkCredential loginInfo = new NetworkCredential("noreply@tazzling.com", "hinshou8");

            MailMessage msg = new MailMessage();
            msg.From = new MailAddress(ConfigurationManager.AppSettings["AdminEmail"].ToString(),"Tastygo");
            //msg.From = new MailAddress("noreply@tazzling.com", "Tastygo");
            if (to.Trim() == "noreply@tazzling.com")
            {
                to = "info@tazzling.com";
            }

            Attachment attachfile = new Attachment(filePath);
            msg.To.Add(new MailAddress(to));

            msg.Subject = subject;
            msg.Attachments.Add(attachfile);
            msg.Body = body;
            msg.IsBodyHtml = true;
            SmtpClient client = new SmtpClient(ConfigurationManager.AppSettings["SMTPServer"].ToString(), Convert.ToInt32(ConfigurationManager.AppSettings["PortNumber"].ToString()));
            //SmtpClient client = new SmtpClient("mail.tazzling.com", 25);
            object userState = msg;
            client.EnableSsl = true;
            client.UseDefaultCredentials = false;
            client.Credentials = loginInfo;
            // client.SendCompleted += new SendCompletedEventHandler(SmtpClient_OnCompleted);
            //client.SendAsync(msg, userState);
            client.Send(msg);

            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public static void SmtpClient_OnCompleted(object sender, AsyncCompletedEventArgs e)
    {
        //Get the Original MailMessage object
        MailMessage mail = (MailMessage)e.UserState;

        //write out the subject
        string subject = mail.Subject;

        if (e.Cancelled)
        {
          //  Console.WriteLine("Send canceled for mail with subject [{0}].", subject);
        }
        if (e.Error != null)
        {
          //  Console.WriteLine("Error {1} occurred when sending mail [{0}] ", subject, e.Error.ToString());
        }
        else
        {
           // Console.WriteLine("Message [{0}] sent.", subject);
        }
    }

    public static bool SendFAX(string to, string cc, string bcc, string from, string subject, string body)
    {
        try
        {
            NetworkCredential loginInfo = new NetworkCredential("info@tazzling.com", "hinshou8");
            MailMessage msg = new MailMessage();
            msg.From = new MailAddress("info@tazzling.com");
            msg.To.Add(new MailAddress(to));
            msg.Subject = subject;
            msg.Body = body;
            msg.IsBodyHtml = true;
            SmtpClient client = new SmtpClient(ConfigurationManager.AppSettings["SMTPServer"].ToString(), Convert.ToInt32(ConfigurationManager.AppSettings["PortNumber"].ToString()));
            client.EnableSsl = false;
            client.UseDefaultCredentials = false;
            client.Credentials = loginInfo;
            client.Send(msg);
            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }
    
    #endregion

    public static DataTable getAllProvinces()
    {
        DataTable dtProvinces = null;
        try
        {
            dtProvinces = SqlHelper.ExecuteDataset(connStr, CommandType.StoredProcedure, "spGetAllProvince").Tables[0];
        }
        catch (Exception ex)
        {
            return null;
        }
        return dtProvinces;
    }

    public static DataTable getAllProvincesByCountryID(Int32 CountryID)
    {
        DataTable dtProvinces = null;
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@provinceId", CountryID);
            dtProvinces = SqlHelper.ExecuteDataset(connStr, CommandType.StoredProcedure, "spAllProvincesByCountryID", param).Tables[0];
        }
        catch (Exception ex)
        {
            return null;
        }
        return dtProvinces;
    }

    public static DataTable getAllCountries()
    {
        DataTable dtCountries = null;
        try
        {
            dtCountries = SqlHelper.ExecuteDataset(connStr, CommandType.StoredProcedure, "spGetAllCountries").Tables[0];
        }
        catch (Exception ex)
        {
            return null;
        }
        return dtCountries;
    }

    public static DataTable getAllCities()
    {
        DataTable dtCities = null;
        try
        {
            dtCities = SqlHelper.ExecuteDataset(connStr, CommandType.StoredProcedure, "spGetAllCitiesForSearch").Tables[0];
        }
        catch (Exception ex)
        {
            return null;
        }
        return dtCities;
    }

    public static DataTable getAllZipCodes()
    {
        DataTable dtZipCodes = null;
        try
        {
            dtZipCodes = SqlHelper.ExecuteDataset(connStr, CommandType.StoredProcedure, "spGetAllZipCodes").Tables[0];
        }
        catch (Exception ex)
        {
            return null;
        }
        return dtZipCodes;
    }

    public static DataTable getZipCodeByName(string strZip)
    {
        DataTable dtZipCode = null;
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@zip", strZip);
            dtZipCode = SqlHelper.ExecuteDataset(connStr, CommandType.StoredProcedure, "spGetZipCodeByName", param).Tables[0];
        }
        catch (Exception ex)
        {
            return null;
        }
        return dtZipCode;
    }

    public static DataTable getHSTForGivenDates(DateTime dtStartTime, DateTime dtEndTime)
    {
        DataTable dtHST = null;
        try
        {
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@dtStartTime", dtStartTime);
            param[1] = new SqlParameter("@dtEndTime", dtEndTime);
            dtHST = SqlHelper.ExecuteDataset(connStr, CommandType.StoredProcedure, "spGetHSTForGivenDates", param).Tables[0];
        }
        catch (Exception ex)
        {
            return null;
        }
        return dtHST;
    }
   
    public static DataTable getProvincesByCountryID(int counrtyID)
    {
        DataTable dtCountries = null;
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@countryID", counrtyID);
            dtCountries = SqlHelper.ExecuteDataset(connStr, CommandType.StoredProcedure, "spGetProvincesByID", param).Tables[0];
        }
        catch (Exception ex)
        {
            return null;
        }
        return dtCountries;
    }

    public static DataTable getAllCitiesWithProvinceAndCountryInfoByCountryID(int counrtyID)
    {
        DataTable dtCountries = null;
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@countryID", counrtyID);
            dtCountries = SqlHelper.ExecuteDataset(connStr, CommandType.StoredProcedure, "spGetAllCitiesWithProvinceAndCountryInfoByCountryID", param).Tables[0];
        }
        catch (Exception ex)
        {
            return null;
        }
        return dtCountries;
    }

    public static DataTable getCitiesByProvinceID(int provinceId)
    {
        DataTable dtProvinces = null;
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@provinceId", provinceId);
            dtProvinces = SqlHelper.ExecuteDataset(connStr, CommandType.StoredProcedure, "spGetCitiesByProvinceID", param).Tables[0];
        }
        catch (Exception ex)
        {
            return null;
        }
        return dtProvinces;
    }

    public static DataTable getProvinceByProvinceID(int provinceId)
    {
        DataTable dtprovince = null;
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@provinceId", provinceId);
            dtprovince = SqlHelper.ExecuteDataset(connStr, CommandType.StoredProcedure, "spGetProvinceByProvinceID", param).Tables[0];
        }
        catch (Exception ex)
        {
            return null;
        }
        return dtprovince;
    }

    public static DataTable search(string Qry)
    {
        DataTable dt = null;
        try
        {
            dt = SqlHelper.ExecuteDataset(connStr, CommandType.Text, Qry).Tables[0];
            return dt;
        }
        catch (Exception ex)
        {
            string strError = ex.ToString();
            return dt;
        }
    }

    public static DataSet searchDataSet(string Qry)
    {
        DataSet dst = null;
        try
        {
            dst = SqlHelper.ExecuteDataset(connStr, CommandType.Text, Qry);
            return dst;
        }
        catch (Exception ex)
        {
            string strError = ex.ToString();
            return dst;
        }
    }

    public static bool SaveBusinessLogoImage(string srcPath, string destiPath)
    {
        try
        {
            FileInfo file = new FileInfo(srcPath);
            if (file.Exists)
            {
                System.Drawing.Image bitmap = new Bitmap(srcPath);
                if (bitmap.Width > 84)
                {
                    System.Drawing.Image image = new Bitmap(bitmap, 84, 84 * bitmap.Height / bitmap.Width);
                    image.Save(destiPath);
                    image.Dispose();
                }
                else
                {
                    System.Drawing.Image image = new Bitmap(bitmap, bitmap.Width, bitmap.Height);
                    image.Save(destiPath);
                    image.Dispose();
                }
                bitmap.Dispose();
                try
                {
                    file.Delete();
                }
                catch (Exception ex)
                { }
                return true;
            }
            else
            {
                return false;
            }
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public static bool CreateThumbnail(string srcPath, string destiPath, string src, string destiFileName)
    {
        try
        {
            FileInfo file = new FileInfo(srcPath);
            if (file.Exists)
            {
                if (!Directory.Exists(destiPath))
                {
                    Directory.CreateDirectory(destiPath);
                }
                System.Drawing.Image bitmap = new Bitmap(srcPath);
                if (src != "res")
                {
                    if (bitmap.Width > 88)
                    {
                        System.Drawing.Image image = new Bitmap(bitmap, 88, 88 * bitmap.Height / bitmap.Width);
                        image.Save(destiPath + destiFileName);
                        image.Dispose();
                    }
                    else
                    {
                        System.Drawing.Image image = new Bitmap(bitmap, bitmap.Width, bitmap.Height);
                        image.Save(destiPath + destiFileName);
                        image.Dispose();
                    }
                }
                else
                {
                    if (bitmap.Width > 272)
                    {
                        System.Drawing.Image image = new Bitmap(bitmap, 272, 272 * bitmap.Height / bitmap.Width);
                        image.Save(destiPath + destiFileName);
                        image.Dispose();
                    }
                    else
                    {
                        System.Drawing.Image image = new Bitmap(bitmap, bitmap.Width, bitmap.Height);
                        image.Save(destiPath + destiFileName);
                        image.Dispose();
                    }
                }
                bitmap.Dispose();
                return true;

            }
            else
            {
                return false;
            }
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public static bool CreateThumbnailForBusinessOwner(string srcPath, string destiPath, string src, string destiFileName)
    {
        try
        {
            FileInfo file = new FileInfo(srcPath);
            if (file.Exists)
            {
                if (!Directory.Exists(destiPath))
                {
                    Directory.CreateDirectory(destiPath);
                }
                System.Drawing.Image bitmap = new Bitmap(srcPath);
                if (bitmap.Width > 100)
                {
                    System.Drawing.Image image = new Bitmap(bitmap, 100, 100);
                    image.Save(destiPath + destiFileName);
                    image.Dispose();
                }
                else
                {
                    System.Drawing.Image image = new Bitmap(bitmap, bitmap.Width, bitmap.Height);
                    image.Save(destiPath + destiFileName);
                    image.Dispose();
                }

                bitmap.Dispose();
                return true;

            }
            else
            {
                return false;
            }
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public static bool CreateThumbnailFeaturedFood(string srcPath, string destiPath, string src, string destiFileName)
    {
        try
        {
            FileInfo file = new FileInfo(srcPath);
            if (file.Exists)
            {
                if (!Directory.Exists(destiPath))
                {
                    Directory.CreateDirectory(destiPath);
                }
                System.Drawing.Image bitmap = new Bitmap(srcPath);
                if (bitmap.Width > 187)
                {
                    System.Drawing.Image image = new Bitmap(bitmap, 187, 125);
                    image.Save(destiPath + destiFileName);
                    image.Dispose();
                }
                else
                {
                    System.Drawing.Image image = new Bitmap(bitmap, bitmap.Width, bitmap.Height);
                    image.Save(destiPath + destiFileName);
                    image.Dispose();
                }

                bitmap.Dispose();
                return true;

            }
            else
            {
                return false;
            }
        }
        catch (Exception ex)
        {
            return false;
        }
    }


    public static void GenerateCampaignThumbnails(Stream sourcePath, string targetPath, string destiPath, string destiFileName)
    {
        try
        {
            if (!Directory.Exists(destiPath))
            {
                Directory.CreateDirectory(destiPath);
            }

            using (var image = System.Drawing.Image.FromStream(sourcePath))
            {

                var thumbnailImg = new Bitmap(480, 240);
                var thumbGraph = Graphics.FromImage(thumbnailImg);
                thumbGraph.CompositingQuality = CompositingQuality.HighQuality;
                thumbGraph.SmoothingMode = SmoothingMode.HighQuality;
                thumbGraph.InterpolationMode = InterpolationMode.HighQualityBicubic;
                var imageRectangle = new System.Drawing.Rectangle(0, 0, 480, 240);
                thumbGraph.DrawImage(image, imageRectangle);
                thumbnailImg.Save(targetPath, image.RawFormat);
            }
            CreateSmallThumbnailFromStreamCampaign(sourcePath, destiPath + "mobile\\", destiPath + "mobile\\" + destiFileName);
            CreateSmallThumbnailFromStreamCampaign2(sourcePath, destiPath + "thumb\\", destiPath + "thumb\\" + destiFileName);
        }
        catch (Exception ex)
        { }
    }

    public static void CreateSmallThumbnailFromStreamCampaign(Stream sourcePath, string destiPath, string targetPath)
    {

        try
        {
            if (!Directory.Exists(destiPath))
            {
                Directory.CreateDirectory(destiPath);
            }
            using (var image = System.Drawing.Image.FromStream(sourcePath))
            {

                var thumbnailImg = new Bitmap(360, 180);
                var thumbGraph = Graphics.FromImage(thumbnailImg);
                thumbGraph.CompositingQuality = CompositingQuality.HighQuality;
                thumbGraph.SmoothingMode = SmoothingMode.HighQuality;
                thumbGraph.InterpolationMode = InterpolationMode.HighQualityBicubic;
                var imageRectangle = new System.Drawing.Rectangle(0, 0, 360, 180);
                thumbGraph.DrawImage(image, imageRectangle);
                thumbnailImg.Save(targetPath, image.RawFormat);
            }

        }
        catch (Exception ex)
        {

        }
    }

    public static void CreateSmallThumbnailFromStreamCampaign2(Stream sourcePath, string destiPath, string targetPath)
    {
        try
        {
            if (!Directory.Exists(destiPath))
            {
                Directory.CreateDirectory(destiPath);
            }

            using (var image = System.Drawing.Image.FromStream(sourcePath))
            {

                var thumbnailImg = new Bitmap(240, 120);
                var thumbGraph = Graphics.FromImage(thumbnailImg);
                thumbGraph.CompositingQuality = CompositingQuality.HighQuality;
                thumbGraph.SmoothingMode = SmoothingMode.HighQuality;
                thumbGraph.InterpolationMode = InterpolationMode.HighQualityBicubic;
                var imageRectangle = new System.Drawing.Rectangle(0, 0, 240, 120);
                thumbGraph.DrawImage(image, imageRectangle);
                thumbnailImg.Save(targetPath, image.RawFormat);
            }

        }
        catch (Exception ex)
        {

        }
    }

    public static void GenerateThumbnails(Stream sourcePath, string targetPath, string destiPath, string destiFileName)
    {
        try
        {
            if (!Directory.Exists(destiPath))
            {
                Directory.CreateDirectory(destiPath);
            }

            if (!Directory.Exists(destiPath+"Large\\"))
            {
                Directory.CreateDirectory(destiPath+"Large\\");
            }

            using (var image = System.Drawing.Image.FromStream(sourcePath))
            {

                var thumbnailImg_Large = new Bitmap(720, 523);
                var thumbGraph_Large = Graphics.FromImage(thumbnailImg_Large);
                thumbGraph_Large.CompositingQuality = CompositingQuality.HighQuality;
                thumbGraph_Large.SmoothingMode = SmoothingMode.HighQuality;
                thumbGraph_Large.InterpolationMode = InterpolationMode.HighQualityBicubic;
                var imageRectangle_Large = new System.Drawing.Rectangle(0, 0, 720, 523);
                thumbGraph_Large.DrawImage(image, imageRectangle_Large);
                thumbnailImg_Large.Save(destiPath + "Large\\" + destiFileName, image.RawFormat);


                var thumbnailImg = new Bitmap(380, 276);
                var thumbGraph = Graphics.FromImage(thumbnailImg);
                thumbGraph.CompositingQuality = CompositingQuality.HighQuality;
                thumbGraph.SmoothingMode = SmoothingMode.HighQuality;
                thumbGraph.InterpolationMode = InterpolationMode.HighQualityBicubic;
                var imageRectangle = new System.Drawing.Rectangle(0, 0, 380, 276);
                thumbGraph.DrawImage(image, imageRectangle);
                thumbnailImg.Save(targetPath, image.RawFormat);
            }
            CreateSmallThumbnailFromStreamDealFood(sourcePath, destiPath + "mobile\\", destiPath + "mobile\\" + destiFileName);
            CreateSmallThumbnailFromStreamDealFood2(sourcePath, destiPath + "thumb\\", destiPath + "thumb\\" + destiFileName);
        }
        catch (Exception ex)
        { }
    }

    public static void CreateSmallThumbnailFromStreamDealFood(Stream sourcePath, string destiPath, string targetPath)
    {

        try
        {
            if (!Directory.Exists(destiPath))
            {
                Directory.CreateDirectory(destiPath);
            }
            using (var image = System.Drawing.Image.FromStream(sourcePath))
            {

                var thumbnailImg = new Bitmap(316, 230);
                var thumbGraph = Graphics.FromImage(thumbnailImg);
                thumbGraph.CompositingQuality = CompositingQuality.HighQuality;
                thumbGraph.SmoothingMode = SmoothingMode.HighQuality;
                thumbGraph.InterpolationMode = InterpolationMode.HighQualityBicubic;
                var imageRectangle = new System.Drawing.Rectangle(0, 0, 316, 230);
                thumbGraph.DrawImage(image, imageRectangle);
                thumbnailImg.Save(targetPath, image.RawFormat);
            }

        }
        catch (Exception ex)
        {

        }
    }

    public static void CreateSmallThumbnailFromStreamDealFood2(Stream sourcePath, string destiPath, string targetPath)
    {
        try
        {
            if (!Directory.Exists(destiPath))
            {
                Directory.CreateDirectory(destiPath);
            }

            using (var image = System.Drawing.Image.FromStream(sourcePath))
            {

                var thumbnailImg = new Bitmap(170, 123);
                var thumbGraph = Graphics.FromImage(thumbnailImg);
                thumbGraph.CompositingQuality = CompositingQuality.HighQuality;
                thumbGraph.SmoothingMode = SmoothingMode.HighQuality;
                thumbGraph.InterpolationMode = InterpolationMode.HighQualityBicubic;
                var imageRectangle = new System.Drawing.Rectangle(0, 0, 170, 123);
                thumbGraph.DrawImage(image, imageRectangle);
                thumbnailImg.Save(targetPath, image.RawFormat);
            }

        }
        catch (Exception ex)
        {

        }
    }
    
    public static bool CreateThumbnailDealFood(string srcPath, string destiPath, string src, string destiFileName)
    {
        try
        {
            FileInfo file = new FileInfo(srcPath);
            if (file.Exists)
            {
                if (!Directory.Exists(destiPath))
                {
                    Directory.CreateDirectory(destiPath);
                }


                System.Drawing.Image imgToResize = new Bitmap(srcPath);

                int sourceWidth = imgToResize.Width;
                int sourceHeight = imgToResize.Height;

                float nPercent = 0;
                float nPercentW = 0;
                float nPercentH = 0;

                nPercentW = ((float)464 / (float)sourceWidth);
                nPercentH = ((float)333 / (float)sourceHeight);

                if (nPercentH < nPercentW)
                    nPercent = nPercentH;
                else
                    nPercent = nPercentW;

                int destWidth = (int)(sourceWidth * nPercent);
                int destHeight = (int)(sourceHeight * nPercent);

                Bitmap b = new Bitmap(destWidth, destHeight);
                Graphics g = Graphics.FromImage((System.Drawing.Image)b);
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                //  System.Drawing.Image imgdestImage = new Bitmap(destiPath + destiFileName);

                System.Drawing.Image image = new Bitmap(imgToResize, destWidth, destHeight);
                image.Save(destiPath + destiFileName);
                image.Dispose();

                //g.DrawImage(imgdestImage, 0, 0, destWidth, destHeight);
                g.Dispose();
                //imgdestImage.Dispose();
                imgToResize.Dispose();
                CreateSmallThumbnailDealFood2(srcPath, destiPath + "mobile\\", src, destiFileName);
                CreateSmallThumbnailDealFood(srcPath, destiPath + "thumb\\", src, destiFileName);
                return true;
            }
            else
            {
                return false;
            }
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public static bool CreateBannerImage(string srcPath, string destiPath, string src, string destiFileName)
    {
        try
        {
            FileInfo file = new FileInfo(srcPath);
            if (file.Exists)
            {
                if (!Directory.Exists(destiPath))
                {
                    Directory.CreateDirectory(destiPath);
                }

                System.Drawing.Image imgToResize = new Bitmap(srcPath);

                int sourceWidth = imgToResize.Width;
                int sourceHeight = imgToResize.Height;

                float nPercent = 0;
                float nPercentW = 0;
                float nPercentH = 0;

                nPercentW = ((float)980 / (float)sourceWidth);
                nPercentH = ((float)90 / (float)sourceHeight);

                if (nPercentH < nPercentW)
                    nPercent = nPercentH;
                else
                    nPercent = nPercentW;

                int destWidth = (int)(sourceWidth * nPercent);
                int destHeight = (int)(sourceHeight * nPercent);

                Bitmap b = new Bitmap(destWidth, destHeight);
                Graphics g = Graphics.FromImage((System.Drawing.Image)b);
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                //  System.Drawing.Image imgdestImage = new Bitmap(destiPath + destiFileName);

                System.Drawing.Image image = new Bitmap(imgToResize, destWidth, destHeight);
                image.Save(destiPath + destiFileName);
                image.Dispose();

                //g.DrawImage(imgdestImage, 0, 0, destWidth, destHeight);
                g.Dispose();
                //imgdestImage.Dispose();
                imgToResize.Dispose();
                return true;
            }
            else
            {
                return false;
            }
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public static bool CreateSmallThumbnailDealFood(string srcPath, string destiPath, string src, string destiFileName)
    {
        try
        {
            FileInfo file = new FileInfo(srcPath);
            if (file.Exists)
            {
                if (!Directory.Exists(destiPath))
                {
                    Directory.CreateDirectory(destiPath);
                }

                System.Drawing.Image imgToResize = new Bitmap(srcPath);

                int sourceWidth = imgToResize.Width;
                int sourceHeight = imgToResize.Height;

                float nPercent = 0;
                float nPercentW = 0;
                float nPercentH = 0;

                nPercentW = ((float)244 / (float)sourceWidth);
                nPercentH = ((float)176 / (float)sourceHeight);

                if (nPercentH < nPercentW)
                    nPercent = nPercentH;
                else
                    nPercent = nPercentW;

                int destWidth = (int)(sourceWidth * nPercent);
                int destHeight = (int)(sourceHeight * nPercent);

                Bitmap b = new Bitmap(destWidth, destHeight);
                Graphics g = Graphics.FromImage((System.Drawing.Image)b);
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                //  System.Drawing.Image imgdestImage = new Bitmap(destiPath + destiFileName);

                System.Drawing.Image image = new Bitmap(imgToResize, destWidth, destHeight);
                image.Save(destiPath + destiFileName);
                image.Dispose();

                //g.DrawImage(imgdestImage, 0, 0, destWidth, destHeight);
                g.Dispose();
                //imgdestImage.Dispose();
                imgToResize.Dispose();
                return true;
            }
            else
            {
                return false;
            }
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public static bool CreateSmallThumbnailDealFood2(string srcPath, string destiPath, string src, string destiFileName)
    {
        try
        {
            FileInfo file = new FileInfo(srcPath);
            if (file.Exists)
            {
                if (!Directory.Exists(destiPath))
                {
                    Directory.CreateDirectory(destiPath);
                }

                System.Drawing.Image imgToResize = new Bitmap(srcPath);

                int sourceWidth = imgToResize.Width;
                int sourceHeight = imgToResize.Height;

                float nPercent = 0;
                float nPercentW = 0;
                float nPercentH = 0;

                nPercentW = ((float)168 / (float)sourceWidth);
                nPercentH = ((float)121 / (float)sourceHeight);

                if (nPercentH < nPercentW)
                    nPercent = nPercentH;
                else
                    nPercent = nPercentW;

                int destWidth = (int)(sourceWidth * nPercent);
                int destHeight = (int)(sourceHeight * nPercent);

                Bitmap b = new Bitmap(destWidth, destHeight);
                Graphics g = Graphics.FromImage((System.Drawing.Image)b);
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                //  System.Drawing.Image imgdestImage = new Bitmap(destiPath + destiFileName);

                System.Drawing.Image image = new Bitmap(imgToResize, destWidth, destHeight);
                image.Save(destiPath + destiFileName);
                image.Dispose();

                //g.DrawImage(imgdestImage, 0, 0, destWidth, destHeight);
                g.Dispose();
                //imgdestImage.Dispose();
                imgToResize.Dispose();
                return true;
            }
            else
            {
                return false;
            }
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public static bool CreateThumbnailForGiftCard(string srcPath, string destiPath, string src, string destiFileName)
    {
        try
        {
            FileInfo file = new FileInfo(srcPath);
            if (file.Exists)
            {
                if (Directory.Exists(destiPath))
                {
                    System.Drawing.Image bitmap = new Bitmap(srcPath);
                    //System.Drawing.Image image = new Bitmap(bitmap, 220, 140);
                    if (bitmap.Width > 220)
                    {
                        System.Drawing.Image image = new Bitmap(bitmap, 220, 142);
                        image.Save(destiPath + destiFileName);
                        image.Dispose();
                    }
                    else
                    {
                        System.Drawing.Image image = new Bitmap(bitmap, bitmap.Width, bitmap.Height);
                        image.Save(destiPath + destiFileName);
                        image.Dispose();
                    }
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        catch (Exception ex)
        {
            return false;
        }
    }



    public static string FormatDateTime(object date, string format)
    {
        if (date == null)
            return string.Empty;
        if (format == string.Empty)
            format = "yyyy-MM-dd";
        return FormatDateTime(Convert.ToDateTime(date), format);
    }

    public static string FormatDateTime(DateTime date, string format)
    {
        System.IFormatProvider provider = new System.Globalization.CultureInfo("en-US", true);
        return date.ToString(format, provider);
    }

    public static string GetWebSite()
    {
        string Port = System.Web.HttpContext.Current.Request.ServerVariables["SERVER_PORT"];
        if (Port == null || Port == "80" || Port == "443")
            Port = "";
        else
            Port = ":" + Port;

        string Protocol = System.Web.HttpContext.Current.Request.ServerVariables["SERVER_PORT_SECURE"];
        if (Protocol == null || Protocol == "0")
            Protocol = "http://";
        else
            Protocol = "https://";

        string sOut = Protocol + System.Web.HttpContext.Current.Request.ServerVariables["SERVER_NAME"] + Port;
        return sOut;
    }

    /// <summary>
    /// Encrypt a string using dual encryption method. Return a encrypted cipher Text
    /// </summary>
    /// <param name="toEncrypt">string to be encrypted</param>
    /// <param name="useHashing">use hashing? send to for extra secirity</param>
    /// <returns></returns>
    public static string Encrypt(string toEncrypt, bool useHashing)
    {
        byte[] keyArray;
        byte[] toEncryptArray = UTF8Encoding.UTF8.GetBytes(toEncrypt);
        string key = "redsignal";
        //System.Windows.Forms.MessageBox.Show(key);
        if (useHashing)
        {
            MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
            keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
            hashmd5.Clear();
        }
        else
            keyArray = UTF8Encoding.UTF8.GetBytes(key);

        TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
        tdes.Key = keyArray;
        tdes.Mode = CipherMode.ECB;
        tdes.Padding = PaddingMode.PKCS7;

        ICryptoTransform cTransform = tdes.CreateEncryptor();
        byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
        tdes.Clear();
        return Convert.ToBase64String(resultArray, 0, resultArray.Length);
    }

    /// <summary>
    /// DeCrypt a string using dual encryption method. Return a DeCrypted clear string
    /// </summary>
    /// <param name="cipherString">encrypted string</param>
    /// <param name="useHashing">Did you use hashing to encrypt this data? pass true is yes</param>
    /// <returns></returns>
    public static string Decrypt(string cipherString, bool useHashing)
    {
        byte[] keyArray;
        byte[] toEncryptArray = Convert.FromBase64String(cipherString);
        string key = "redsignal";
        if (useHashing)
        {
            MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
            keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
            hashmd5.Clear();
        }
        else
            keyArray = UTF8Encoding.UTF8.GetBytes(key);

        TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
        tdes.Key = keyArray;
        tdes.Mode = CipherMode.ECB;
        tdes.Padding = PaddingMode.PKCS7;

        ICryptoTransform cTransform = tdes.CreateDecryptor();
        byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

        tdes.Clear();
        return UTF8Encoding.UTF8.GetString(resultArray);
    }

    public static string GenerateId()
    {
        long i = 1;
        foreach (byte b in Guid.NewGuid().ToByteArray())
        {
            i *= ((int)b + 1);
        }
        return string.Format("{0:x}", i - DateTime.Now.Ticks);
    }

    public static double CalculateDistance(double lat1, double lon1, double lat2, double lon2)
    {
        double t = lon1 - lon2;
        double distance = Math.Sin(Degree2Radius(lat1)) * Math.Sin(Degree2Radius(lat2)) + Math.Cos(Degree2Radius(lat1)) * Math.Cos(Degree2Radius(lat2)) * Math.Cos(Degree2Radius(t));
        distance = Math.Acos(distance);
        distance = Radius2Degree(distance);
        distance = distance * 60 * 1.1515;

        return distance;

    }

    private static double Degree2Radius(double deg)
    {
        return (deg * Math.PI / 180.0);
    }

    private static double Radius2Degree(double rad)
    {
        return rad / Math.PI * 180.0;
    }

    public static DataTable SortDataTable(DataTable dt, string sort)
    {
        DataTable newDT = dt.Clone();
        int rowCount = dt.Rows.Count;
        DataRow[] foundRows = dt.Select(null, sort); // Sort with Column name
        for (int i = 0; i < rowCount; i++)
        {
            object[] arr = new object[dt.Columns.Count];
            for (int j = 0; j < dt.Columns.Count; j++)
            {
                arr[j] = foundRows[i][j];
            }
            DataRow data_row = newDT.NewRow();
            data_row.ItemArray = arr;
            newDT.Rows.Add(data_row);
        }
        //clear the incoming dt
        dt.Rows.Clear();
        for (int i = 0; i < newDT.Rows.Count; i++)
        {
            object[] arr = new object[dt.Columns.Count];
            for (int j = 0; j < dt.Columns.Count; j++)
            {
                arr[j] = newDT.Rows[i][j];
            }
            DataRow data_row = dt.NewRow();
            data_row.ItemArray = arr;
            dt.Rows.Add(data_row);
        }
        return dt;
    }

    public static DateTime getResturantLocalTime(int provinceID)
    {


        DateTime dt = DateTime.Now;
        if (provinceID != 0)
        {
            //if (Session["member"] != null || Session["restaurant"] != null || Session["sale"] != null)
            //{
            double provinceTimeZone = 0;
            DataTable dtProvinceInfo = null;
            dtProvinceInfo = Misc.getProvinceByProvinceID(provinceID);
            if (dtProvinceInfo != null && dtProvinceInfo.Rows[0]["timeZone"].ToString() != "")
            {
                provinceTimeZone = Convert.ToDouble(dtProvinceInfo.Rows[0]["timeZone"].ToString());
                dt = GetRestaurantLocalTime(provinceTimeZone);
            }
            //}
        }
        return dt;
    }

    public static DateTime GetRestaurantLocalTime(double restaurantTimezone)
    {
        return ConvertToLocalDateTime(DateTime.Now, restaurantTimezone);
    }

    public static DateTime ConvertToLocalDateTime(DateTime serverTime, double localTimeZone)
    {
        double serverTimeZone = Convert.ToDouble(System.Configuration.ConfigurationManager.AppSettings.Get("ServerTimeZone"));
        return serverTime.AddHours(-serverTimeZone + localTimeZone);
    }

}

