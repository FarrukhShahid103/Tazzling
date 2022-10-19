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
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Net.Sockets;
using System.IO;
using System.Net.Security;
using System.Security.Authentication;
using System.Threading;

public partial class admin_IPhoneNotification : System.Web.UI.Page
{
    SslStream sslStream;
    BLLDeviceInfo ObjDevice;
    public string strCertificates="";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            bindCityDD();
            lblMessage.Visible = false;        
        }     
    }
    protected void btnSendMessage_Click(object sender, EventArgs e)
    {
        lblMessage.Visible = false;
        SendMessage();
    }

    private void bindCityDD()
    {

        ddlCity.DataSource = Misc.search("select cityId,cityName +' ('+ convert(nvarchar(10), isnull((SELECT Count(deviceID) FROM  deviceInfo where deviceInfo.cityId = city.cityId) ,0))+')' as 'cityWithDevice' from city where city.cityId=337 or city.cityId=338 or city.cityId=1376 or city.cityId=1710 or city.cityId=1720 or city.cityId=1709 or city.cityId=1716 or city.cityId=1722 or city.cityId=1726 or city.cityId=1712 or city.cityId=1713 or city.cityId=1733 or city.cityId=1727 Order by cityName asc");
        ddlCity.DataValueField = "cityId";
        ddlCity.DataTextField = "cityWithDevice";
        ddlCity.DataBind();
        
        ddlCity.SelectedValue = "337";
    }

    public bool ConnectToAPNS(string DeviceTokken, int Counter)
    {
        try
        {
            X509Certificate2Collection certs = new X509Certificate2Collection();

            // Add the Apple cert to our collection
            certs.Add(getServerCert());

            // Apple development server address
            string apsHost;

            if (getServerCert().ToString().Contains("Production"))
                apsHost = "gateway.push.apple.com";
            else
                apsHost = "gateway.sandbox.push.apple.com";
            
            // Create a TCP socket connection to the Apple server on port 2195
            TcpClient tcpClient = new TcpClient(apsHost, 2195);

            // Create a new SSL stream over the connection            
            sslStream = new SslStream(tcpClient.GetStream());

            // Authenticate using the Apple cert
            sslStream.AuthenticateAsClient(apsHost, certs, SslProtocols.Default, false);

            if (PushMessage(DeviceTokken, txtMessage.Text.Trim(), Counter))
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

    private X509Certificate getServerCert()
    {
        X509Certificate test = new X509Certificate();

        //Open the cert store on local machine

        X509Store store = new X509Store(StoreLocation.LocalMachine);
       // strCertificates += "store is = " + store;
        if (store != null)
        {
            // store exists, so open it and search through the certs for the Apple Cert
            store.Open(OpenFlags.ReadOnly);
            X509Certificate2Collection certs = store.Certificates;            
            if (certs.Count > 0)    
            {
                int i;
                for (i = 0; i < certs.Count; i++)
                {
                    X509Certificate2 cert = certs[i];                    
                    if (cert.FriendlyName.Contains("Apple Production IOS Push Services: AG6QB9U876:H695ZY6F5B"))
                    {
                        //Cert found, so return it.
                        
                        return certs[i];
                    }
                }
            }
            return test;
        }
        return test;
    }

    private static byte[] HexToData(string hexString)
    {
        if (hexString == null)
            return null;

        if (hexString.Length % 2 == 1)
            hexString = '0' + hexString; // Up to you whether to pad the first or last byte

        byte[] data = new byte[hexString.Length / 2];

        for (int i = 0; i < data.Length; i++)
            data[i] = Convert.ToByte(hexString.Substring(i * 2, 2), 16);

        return data;
    }

    public bool PushMessage(string token, string message, int ibadge)
    {
        String cToken = token;
        String cAlert = message;
        int iBadge = ibadge;

        // Ready to create the push notification
        byte[] buf = new byte[256];
        MemoryStream ms = new MemoryStream();
        BinaryWriter bw = new BinaryWriter(ms);
        bw.Write(new byte[] { 0, 0, 32 });

        byte[] deviceToken = HexToData(cToken);
        bw.Write(deviceToken);

        bw.Write((byte)0);

        // Create the APNS payload - new.caf is an audio file saved in the application bundle on the device
        string msg = "{\"aps\":{\"alert\":\"" + cAlert + "\",\"badge\":" + iBadge.ToString() + ",\"sound\":\"new.caf\"}}";

        // Write the data out to the stream
        bw.Write((byte)msg.Length);
        bw.Write(msg.ToCharArray());
        bw.Flush();

        if (sslStream != null)
        {
            sslStream.Write(ms.ToArray());
            return true;
        }

        return false;
    }
    private void SendMessage()
    {
        DataTable DataTable = new DataTable();
        ObjDevice = new BLLDeviceInfo();
        ObjDevice.cityId = Convert.ToInt32(ddlCity.SelectedValue.ToString());
        DataTable = ObjDevice.getAlliPhoneDevicesByCityID();
        
        if (DataTable != null)
        {
            if (DataTable.Rows.Count > 0)
            {

                ThreadStart starter = delegate { notificationSend(DataTable); };
                new Thread(starter).Start();       
                lblMessage.Visible = true;
                lblMessage.Text = "Notificatin sent to " + DataTable.Rows.Count.ToString() + " devices.";
            }
            else
            {
                lblMessage.Visible = true;
                lblMessage.Text = "Our Iphone app did not install on any phone for this city yet.";
            }
        }
        else
        {
            lblMessage.Visible = true;
            lblMessage.Text = "Our Iphone app did not install on any phone yet.";
        }
        
        //(string DeviceTokken, int Counter)
    }

    private void notificationSend(DataTable DataTable)
    {
        for (int i = 0; i <= DataTable.Rows.Count - 1; i++)
        {
            try
            {
                string DeviceTokken = DataTable.Rows[i]["deviceToken"].ToString();
                int Notification_counter = Convert.ToInt32(DataTable.Rows[i]["Notification_counter"]);
                if (ConnectToAPNS(DeviceTokken, Notification_counter))
                {                  
                    ObjDevice.deviceToken = DeviceTokken;
                    ObjDevice.notification_Counter = Notification_counter + 1;
                    ObjDevice.iPhone = true;
                    ObjDevice.createAndUpdateDeviceInfo();
                }
            }
            catch
            {

            }
        }        
    }
}
