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
using System.Text;
using System.Net;
using System.Net.Mail;
using ExactTargetAPI;
public partial class addEditRestaurantManagement : System.Web.UI.Page
{
    BLLUser obj = new BLLUser();
    BLLRestaurant objRes = new BLLRestaurant();                   
             
    public string strIDs = "";
    public int start = 2;
    public string strtblHide = "none";
    public string strRestHide = "none";
    public string strGoogleAddress = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {           
            if (Request.QueryString["addNew"] != null && Request.QueryString["addNew"].ToString().Trim() != "")
            {
                addNew();
            }
            else if (Request.QueryString["edit"] != null && Request.QueryString["edit"].ToString().Trim() != "")
            {
                editResturantInfo(Convert.ToInt32(Request.QueryString["edit"].Trim()));
            }
            else
            {
                Response.Redirect("restaurantManagement.aspx", true);
            }            
        }

        if (ViewState["userID"] == null) { GetAndSetUserID(); }
    }

    protected void txtEmail_Changed(object sender, EventArgs e)
    {
        try
        {
            if (hfEmail.Value.Trim() == "")
            {                
                string strQuery= "select userTypeID,tblUserinfo.userId,userPassword,restaurantBusinessName from tblUserinfo";
                strQuery+= " left outer join tblRestaurant on (tblRestaurant.userId = tblUserinfo.userId)";
                strQuery += " where userName='"+txtEmail.Text.Trim()+ "' and tblUserinfo.isActive=1 and isDeleted=0";
                DataTable dtuser = Misc.search(strQuery);
                if (dtuser != null && dtuser.Rows.Count > 0)
                {
                    if (dtuser.Rows[0]["userTypeID"].ToString().Trim() == "3")
                    {
                        cbUseThisEmail.Enabled = false;
                        txtPwd.Text = dtuser.Rows[0]["userPassword"].ToString().Trim();
                        txtPwdConfirm.Text = dtuser.Rows[0]["userPassword"].ToString().Trim();
                        cbUseThisEmail.Text = "Email already exists as another business user, this entry will be added as sub business for the main business \"" + dtuser.Rows[0]["restaurantBusinessName"].ToString().Trim() + "\".";
                        cbUseThisEmail.ForeColor = System.Drawing.Color.Red;
                        cbUseThisEmail.Visible = true;
                        txtEmail.Focus();
                    }
                    else
                    {
                        cbUseThisEmail.Enabled = true;
                        txtPwd.Text = dtuser.Rows[0]["userPassword"].ToString().Trim();
                        txtPwdConfirm.Text = dtuser.Rows[0]["userPassword"].ToString().Trim();
                        cbUseThisEmail.Text = "Email already registered as member, check to convert into business account.";
                        cbUseThisEmail.ForeColor = System.Drawing.Color.Black;
                        cbUseThisEmail.Visible = true;
                        txtEmail.Focus();
                    }
                }
                else
                {
                    cbUseThisEmail.Visible = false;
                    txtPwd.Focus();
                }
            }
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
         
    private bool SendMailForNewAccount(string strPassword, string strUserName)
    {
        MailMessage message = new MailMessage();
        StringBuilder sb = new StringBuilder();
        try
        {
            string toAddress = strUserName;
            string fromAddress = ConfigurationManager.AppSettings["AdminEmail"].ToString().Trim();
            string Subject = "Merchant Account with Tastygo";
            message.IsBodyHtml = true;
         
            sb.Append("<!DOCTYPE html PUBLIC '-//W3C//DTD HTML 4.01 Transitional//EN'><html><head><meta http-equiv='Content-Type' content='text/html; charset=utf-8'><meta name='viewport' content='width = 800'><title>Merchant Account with Tastygo!</title><style type='text/css'>a.aapl-link{text-decoration: none;}a.aapl-link:hover{text-decoration: underline;}</style><style media='only screen and (max-device-width: 680px)' type='text/css'>*{line-height: normal !important;}</style></head>");
            sb.Append("<body bgcolor='#E4E4E4' style='margin: 0; padding: 0'><table width='100%' bgcolor='#E4E4E4' cellpadding='0' cellspacing='0' align='center'><tr><td><table width='800' align='center' border='0' cellspacing='0' cellpadding='0'><tr><td><div style='margin: 10px 0px 12px 0px; font-family: Arial;color: #333333; font-size: 14px; line-height: 1.3em;'><img src='http://tazzling.com/images/logoForMail.png' alt='TastyGo' border='0'></div></td></tr></table>");
            sb.Append("<table width='800' align='center' border='0' cellspacing='0' cellpadding='0'><tr><td style='-webkit-border-radius: 8px; background-color: #ffffff' bgcolor='#ffffff'><table width='720' align='center' border='0' cellspacing='0' cellpadding='0'><tr valign='top'><td width='720' bgcolor='#FFFFFF' align='left'>");
            sb.Append("<div style='margin: 40px 0px 0px 15px; font-family: Arial;color: #333333; font-size: 14px; line-height: 1.3em;'>");
            sb.Append("<strong>Dear Business Owner,</strong></div>");
            sb.Append("<div style='margin: 20px 0px 20px 15px; font-family: Arial;color: #000000; font-size: 18px; line-height: 1.3em;'><strong>Thanks for choosing Tazzling.com, where we bring unique customers into your door, 100%, guaranteed!</strong></div>");
            sb.Append("<div style='margin: 0px 0px 10px 15px; font-family: Arial;color: #333333; font-size: 14px; line-height: 1.4em;'>Your merchant account has been successfully created.</div>");
            sb.Append("<div style='margin: 0px 0px 10px 15px; font-family: Arial;color: #333333; font-size: 14px; line-height: 1.4em;'>You may access to your merchant tools by login onto <a href='" + ConfigurationManager.AppSettings["YourSite"].ToString().Trim() + "'>" + ConfigurationManager.AppSettings["YourSite"].ToString().Trim() + "</a></div>");
            sb.Append("<div style='margin: 0px 0px 10px 15px; font-family: Arial;color: #333333; font-size: 14px; line-height: 1.4em;'>User Name : <a href='mailto:" + strUserName + "'>" + strUserName + "</a></div>");
            sb.Append("<div style='margin: 0px 0px 10px 15px; font-family: Arial;color: #333333; font-size: 14px; line-height: 1.4em;'>Password :" + strPassword.ToString().Trim() + "</div>");

            sb.Append("<div style='margin: 0px 0px 10px 15px; font-family: Arial;border-top: 1px solid #eeeeee; font-size: 12px; line-height: 1.3em;'>&nbsp;</div>");
            sb.Append("<div style='margin: 0px 0px 5px 15px; font-family: Arial;color: #333333; font-size: 14px; line-height: 1.4em; font-weight: bold;'>Whats Next?</div>");
            sb.Append("<div style='margin: 0px 0px 5px 20px; font-family: Arial;color: #333333; font-size: 14px; line-height: 1.4em;'>- Our verification manager will get in touch with you over the phone and send your preview through email.</div>");
            sb.Append("<div style='margin: 0px 0px 5px 20px; font-family: Arial;color: #333333; font-size: 14px; line-height: 1.4em;'>- You will be able to access the deal preview before the deal launches</div>");
            sb.Append("<div style='margin: 0px 0px 5px 20px; font-family: Arial;color: #333333; font-size: 14px; line-height: 1.4em;'>- Feel free to make unlimited changes! We will try our very best to please you and make your ads stand out!</div>");
            sb.Append("<div style='margin: 0px 0px 5px 20px; font-family: Arial;color: #333333; font-size: 14px; line-height: 1.4em;'>- Once you've approved the ad, we will start your campaign at the scheduled time.</div>");

            sb.Append("<div style='margin: 0px 0px 10px 15px; font-family: Arial;border-top: 1px solid #eeeeee; font-size: 12px; line-height: 1.3em;'>&nbsp;</div>");
            sb.Append("<div style='margin: 0px 0px 5px 15px; font-family: Arial;color: #333333; font-size: 14px; line-height: 1.4em; font-weight: bold;'>What can I do while the Deal is running?</div>");
            sb.Append("<div style='margin: 0px 0px 5px 20px; font-family: Arial;color: #333333; font-size: 14px; line-height: 1.4em;'>- Login Tazzling.com to view your campaign status, customer's list, and all voucher codes.</div>");
            sb.Append("<div style='margin: 0px 0px 5px 20px; font-family: Arial;color: #333333; font-size: 14px; line-height: 1.4em;'>-	Interact with customers under \"deal discussion\"</div>");

            sb.Append("<div style='margin: 0px 0px 10px 15px; font-family: Arial;border-top: 1px solid #eeeeee; font-size: 12px; line-height: 1.3em;'>&nbsp;</div>");
            sb.Append("<div style='margin: 0px 0px 5px 15px; font-family: Arial;color: #333333; font-size: 14px; line-height: 1.4em; font-weight: bold;'>What happen when deal ends?</div>");
            sb.Append("<div style='margin: 0px 0px 5px 20px; font-family: Arial;color: #333333; font-size: 14px; line-height: 1.4em;'>- Manage your voucher codes by login Tazzling.com ->merchant area</div>");
            sb.Append("<div style='margin: 0px 0px 5px 20px; font-family: Arial;color: #333333; font-size: 14px; line-height: 1.4em;'>- You will also receive your customer's list electronically</div>");
            sb.Append("<div style='margin: 0px 0px 5px 20px; font-family: Arial;color: #333333; font-size: 14px; line-height: 1.4em;'>- In 10 days, you will also receive our detail invoice, and as well as our payment.</div>");


            sb.Append("<div style='margin: 0px 0px 10px 15px; font-family: Arial;color: #333333; font-size: 14px; line-height: 1.4em;'>*If you have any concerns, questions, or feel you are not recipient of this email, please contact <a href='mailto:support@tazzling.com' target='_blanck'>support@tazzling.com</a></div>");
            sb.Append("<div style='margin: 0px 0px 10px 15px; font-family: Arial;color: #333333; font-size: 14px; line-height: 1.4em;'>Or Call 1-855-295-1771</div>");
            sb.Append("<div style='margin: 0px 0px 10px 15px; font-family: Arial;border-top: 1px solid #eeeeee; font-size: 12px; line-height: 1.3em;'>&nbsp;</div>");
            sb.Append("<div style='margin: 0px 10px 20px 15px; font-family: Arial;color: #333333; font-size: 14px; line-height: 1.3em; clear: both;'><strong>Best regards,</strong><br>");
            sb.Append(ConfigurationManager.AppSettings["EmailSignature"].ToString().Trim() + "</div>");
            sb.Append("</td></tr></table></td></tr></table><table width='560' border='0' cellspacing='0' cellpadding='0' align='center'><tr><td style='padding: 20px 20px 10px 24px;'><div style='font-family: Geneva, Verdana, Arial, Arial, sans-serif; font-size: 9px;line-height: 12px; color: #858585;'></div></td></tr>");
            sb.Append("<tr><td style='padding: 0 20px 10px 24px;'>    <div style='font-family: Geneva, Verdana, Arial, Arial, sans-serif; font-size: 9px;        line-height: 12px; color: #858585;'>        Copyright &copy; 2011 Tazzling.Com. All Rights Reserved</div>    <div style='font-family: Geneva, Verdana, Arial, Arial, sans-serif; font-size: 9px;        line-height: 12px; color: #858585;'>        <a href='http://www.tazzling.com/' style='font-family: Geneva, Verdana, Arial, Arial, sans-serif;");
            sb.Append("font-size: 9px; line-height: 12px; color: #858585; text-decoration: underline;'>Keep Informed</a> / <a href='http://www.tazzling.com/terms-customer.aspx' style='font-family: Geneva, Verdana, Arial, Arial, sans-serif;    font-size: 9px; line-height: 12px; color: #858585; text-decoration: underline;'>    Privacy Policy</a> / <a href='http://www.tazzling.com/contact-us.aspx' style='font-family: Geneva, Verdana, Arial, Arial, sans-serif;  font-size: 9px; line-height: 12px; color: #858585; text-decoration: underline;'>Contact Us</a></div>");
            sb.Append("</td></tr><tr></tr></table></td></tr></table></body></html>");
                        
            message.Body = sb.ToString();
            return SendEmail(toAddress, "", "", fromAddress, Subject, message.Body);
        }
        catch (Exception ex)
        {
            lblAddressError.Text = "There is an error occur, please email us at support@tazzling.com or call 1855-295-1771.";
            lblAddressError.Visible = true;
            ImgAddError.Visible = true;
            ImgAddError.ImageUrl = "images/error.png";
            lblAddressError.ForeColor = System.Drawing.Color.Red;
            return false;
        }
    }

    private bool SendEmail(string to, string cc, string bcc, string from, string subject, string body)
    {
        try
        {
            NetworkCredential loginInfo = new NetworkCredential("business@tastyinc.biz", "hinshou8");            
            MailMessage msg = new MailMessage();
            msg.From = new MailAddress("business@tastyinc.biz", "Tastygo");            
            msg.To.Add(new MailAddress(to));
            msg.Subject = subject;
            msg.Body = body;
            msg.IsBodyHtml = true;
            SmtpClient client = new SmtpClient(ConfigurationManager.AppSettings["SMTPServer"].ToString(), Convert.ToInt32(ConfigurationManager.AppSettings["PortNumber"].ToString()));            
            object userState = msg;
            client.EnableSsl = true;
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
    
    protected void btnAddDealAddress_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            if (hfBusinessId.Value == "")
            {
                if (hfDealAddress.Value == "")
                {
                    if (ViewState["dtDealAddress"] == null)
                    {
                        DataTable dtDealAddress = new DataTable("dtDealAddress");
                        DataColumn raID = new DataColumn("raID");
                        DataColumn restaurantId = new DataColumn("restaurantId");
                        DataColumn restaurantAddress = new DataColumn("restaurantAddress");
                        dtDealAddress.Columns.Add(raID);
                        dtDealAddress.Columns.Add(restaurantId);
                        dtDealAddress.Columns.Add(restaurantAddress);
                        DataRow dRow;
                        dRow = dtDealAddress.NewRow();
                        dRow["raID"] = "0";
                        dRow["restaurantId"] = "0";
                        dRow["restaurantAddress"] = txtDealAddress.Text.Trim();
                        dtDealAddress.Rows.Add(dRow);
                        GridView1.DataSource = dtDealAddress.DefaultView;
                        GridView1.DataBind();
                        ViewState["dtDealAddress"] = dtDealAddress;
                    }
                    else
                    {
                        DataTable dtDealAddress = (DataTable)ViewState["dtDealAddress"];
                        if (dtDealAddress != null)
                        {
                            DataRow dRow;
                            dRow = dtDealAddress.NewRow();
                            dRow["raID"] = dtDealAddress.Rows.Count;
                            dRow["restaurantId"] = "0";
                            dRow["restaurantAddress"] = txtDealAddress.Text.Trim();
                            dtDealAddress.Rows.Add(dRow);
                            GridView1.DataSource = dtDealAddress.DefaultView;
                            GridView1.DataBind();
                            ViewState["dtDealAddress"] = dtDealAddress;
                        }
                    }
                }
                else
                {
                    DataTable dtDealAddress = (DataTable)ViewState["dtDealAddress"];
                    int intEditRow = 0;
                    Int32.TryParse(hfDealAddress.Value, out intEditRow);
                    dtDealAddress.Rows[intEditRow]["restaurantAddress"] = txtDealAddress.Text.Trim();
                    GridView1.DataSource = dtDealAddress.DefaultView;
                    GridView1.DataBind();
                    ViewState["dtDealAddress"] = dtDealAddress;
                    // dtDealAddress.Rows[]
                }
            }
            else
            {
                if (hfDealAddress.Value == "")
                {
                    objRaddress = new BLLRestaurantAddresses();
                    objRaddress.restaurantAddress = this.txtDealAddress.Text.Trim();
                    objRaddress.restaurantId = Convert.ToInt64(hfBusinessId.Value.Trim());
                    objRaddress.createRestaurantAddresse();
                }
                else
                {
                    objRaddress = new BLLRestaurantAddresses();
                    objRaddress.raID = Convert.ToInt64(hfDealAddress.Value);
                    objRaddress.restaurantAddress = this.txtDealAddress.Text.Trim();
                    objRaddress.restaurantId = Convert.ToInt64(hfBusinessId.Value.Trim());
                    objRaddress.updateRestaurantAddresses();
                }
                BindAddressGrid(Convert.ToInt64(hfBusinessId.Value.Trim()));
            }
        }
        catch (Exception ex)
        { }
        btnAddDealAddress.ImageUrl = "~/admin/images/btnSave.jpg";                                 
        hfDealAddress.Value = "";
        txtDealAddress.Text = "";
    }

    protected void btnAddGoogleAddress_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            if (hfBusinessId.Value == "")
            {
                
                if (hfGoogleAddress.Value == "")
                {
                    if (ViewState["dtGoogleAddress"] == null)
                    {
                        DataTable dtGoogleAddress = new DataTable("dtGoogleAddress");
                        DataColumn rgaID = new DataColumn("rgaID");
                        DataColumn restaurantId = new DataColumn("restaurantId");
                        DataColumn restaurantGoogleAddress = new DataColumn("restaurantGoogleAddress");
                        dtGoogleAddress.Columns.Add(rgaID);
                        dtGoogleAddress.Columns.Add(restaurantId);
                        dtGoogleAddress.Columns.Add(restaurantGoogleAddress);
                        DataRow dRow;
                        dRow = dtGoogleAddress.NewRow();
                        dRow["rgaID"] = "0";
                        dRow["restaurantId"] = "0";
                        dRow["restaurantGoogleAddress"] = txtGoogleAddress.Text.Trim();
                        dtGoogleAddress.Rows.Add(dRow);
                        GridView2.DataSource = dtGoogleAddress.DefaultView;
                        GridView2.DataBind();
                        ViewState["dtGoogleAddress"] = dtGoogleAddress;
                    }
                    else
                    {
                        DataTable dtGoogleAddress = (DataTable)ViewState["dtGoogleAddress"];
                        if (dtGoogleAddress != null)
                        {
                            DataRow dRow;
                            dRow = dtGoogleAddress.NewRow();
                            dRow["rgaID"] = dtGoogleAddress.Rows.Count;
                            dRow["restaurantId"] = "0";
                            dRow["restaurantGoogleAddress"] = txtGoogleAddress.Text.Trim();
                            dtGoogleAddress.Rows.Add(dRow);
                            GridView2.DataSource = dtGoogleAddress.DefaultView;
                            GridView2.DataBind();
                            ViewState["dtGoogleAddress"] = dtGoogleAddress;
                        }
                    }
                }
                else
                {
                    DataTable dtGoogleAddress = (DataTable)ViewState["dtGoogleAddress"];
                    int intEditRow = 0;
                    Int32.TryParse(hfGoogleAddress.Value, out intEditRow);
                    dtGoogleAddress.Rows[intEditRow]["restaurantGoogleAddress"] = txtGoogleAddress.Text.Trim();
                    GridView2.DataSource = dtGoogleAddress.DefaultView;
                    GridView2.DataBind();
                    ViewState["dtGoogleAddress"] = dtGoogleAddress;
                    // dtGoogleAddress.Rows[]
                }
            }
            else
            {
                if (hfGoogleAddress.Value == "")
                {                    
                    objGoogleAddress = new BLLRestaurantGoogleAddresses();
                    objGoogleAddress.restaurantGoogleAddress = this.txtGoogleAddress.Text.Trim();
                    objGoogleAddress.restaurantId = Convert.ToInt64(hfBusinessId.Value.Trim());
                    objGoogleAddress.createRestaurantGoogleAddresses();
                }
                else
                {
                    objGoogleAddress = new BLLRestaurantGoogleAddresses();
                    objGoogleAddress.rgaID = Convert.ToInt64(hfGoogleAddress.Value);
                    objGoogleAddress.restaurantGoogleAddress = this.txtGoogleAddress.Text.Trim();
                    objGoogleAddress.restaurantId = Convert.ToInt64(hfBusinessId.Value.Trim());
                    objGoogleAddress.updateRestaurantGoogleAddresses();
                }
                BindGoogleAddressGrid(Convert.ToInt64(hfBusinessId.Value.Trim()));
            }
        }
        catch (Exception ex)
        { }
        btnAddGoogleAddress.ImageUrl = "~/admin/images/btnSave.jpg";
        hfGoogleAddress.Value = "";
        txtGoogleAddress.Text = "";
    }

    protected void btnSave_Click(object sender, ImageClickEventArgs e)
    {
        try
        {          
            if (!(GridView1.Rows.Count > 0))
            {
                lblAddressError.Text = "Please add deal address.";
                lblAddressError.Visible = true;
                lblAddressError.ForeColor = System.Drawing.Color.Red;
                ImgAddError.Visible = true;
                ImgAddError.ImageUrl = "images/error.png";
                return;
            }

            if (!cbOnlineDeal.Checked)
            {
                if (!(GridView2.Rows.Count > 0))
                {
                    lblAddressError.Text = "Please add google address.";
                    lblAddressError.Visible = true;
                    lblAddressError.ForeColor = System.Drawing.Color.Red;
                    ImgAddError.Visible = true;
                    ImgAddError.ImageUrl = "images/error.png";
                    return;
                }
            }

            string strImageName = "";
            string strLogoName = "";

            //Save the Deal Info
            if (this.btnSave.ToolTip == "Add New Business Info")
            {

                //Check email already exists or not
                if (cbUseThisEmail.Visible && cbUseThisEmail.Checked)
                {
                    obj.userName = this.txtEmail.Text.Trim();
                    obj.userPassword = this.txtPwd.Text.Trim();
                    long result = obj.updateUserTypeByUserName();

                    //If Image 1 exists
                    if (fpBusinessImg.HasFile)
                    {
                        //upload the Image here
                        strImageName = ImageUploadHere(fpBusinessImg);
                    }

                    if (fuLogo.HasFile)
                    {
                        strLogoName = LogoUploadHere(fuLogo);
                    }

                    //Add New Deal Info
                    AddNewBusniessInfo(strImageName, strLogoName, result);
                }
                else
                {
                    //if (cbUseThisEmail.Visible && !(cbUseThisEmail.Checked))
                    //{
                    //    lblAddressError.Text = "Email already exists. Please choose another.";
                    //    lblAddressError.Visible = true;
                    //    lblAddressError.ForeColor = System.Drawing.Color.Red;
                    //    ImgAddError.Visible = true;
                    //    ImgAddError.ImageUrl = "images/error.png";
                    //    return;
                    //}
                    obj.userName = this.txtEmail.Text.Trim();
                    long result = obj.getUserIdByUserName();
                    if (result > 0)
                    {
                        //obj.firstName = this.txtFname.Text.Trim();
                        //obj.lastName = this.txtLname.Text.Trim();
                        //obj.userName = this.txtEmail.Text.Trim();
                        //string strEmail = this.txtEmail.Text.Trim();
                        ////obj.userPassword = GetPassword();
                        //obj.userPassword = this.txtPwd.Text.Trim();
                        //string strPassword = obj.userPassword;
                        //obj.email = this.txtEmail.Text.Trim();

                        ////For Business Account (Restaurant Account)
                        //obj.userTypeID = 3;
                        //obj.isActive = true;
                        //obj.countryId = 2;
                        //obj.provinceId = 3;
                        //obj.ipAddress = Request.UserHostAddress.ToString();
                        //result = obj.createUser();
                        //if (result > 0)
                        //{
                        //    try
                        //    {
                        //        SendMailForNewAccount(strPassword, strEmail);
                        //    }
                        //    catch (Exception ex)
                        //    { }
                        //}
                        //If Image 1 exists
                        if (fpBusinessImg.HasFile)
                        {
                            //upload the Image here
                            strImageName = ImageUploadHere(fpBusinessImg);
                        }

                        if (fuLogo.HasFile)
                        {
                            strLogoName = LogoUploadHere(fuLogo);
                        }

                        //Add New Deal Info
                        AddNewBusniessInfo(strImageName, strLogoName,result);
                    }
                    else
                    {
                        obj.firstName = this.txtFname.Text.Trim();
                        obj.lastName = this.txtLname.Text.Trim();
                        obj.userName = this.txtEmail.Text.Trim();
                        string strEmail = this.txtEmail.Text.Trim();
                        //obj.userPassword = GetPassword();
                        obj.userPassword = this.txtPwd.Text.Trim();
                        string strPassword = obj.userPassword;
                        obj.email = this.txtEmail.Text.Trim();

                        //For Business Account (Restaurant Account)
                        obj.userTypeID = 3;
                        obj.isActive = true;
                        obj.countryId = 2;
                        obj.provinceId = 3;
                        obj.ipAddress = Request.UserHostAddress.ToString();
                        result = obj.createUser();
                        if (result > 0)
                        {
                            try
                            {
                                SendMailForNewAccount(strPassword, strEmail);
                            }
                            catch (Exception ex)
                            { }
                        }
                        //If Image 1 exists
                        if (fpBusinessImg.HasFile)
                        {
                            //upload the Image here
                            strImageName = ImageUploadHere(fpBusinessImg);
                        }

                        if (fuLogo.HasFile)
                        {
                            strLogoName = LogoUploadHere(fuLogo);
                        }


                        //Add New Deal Info
                        AddNewBusniessInfo(strImageName, strLogoName, result);
                    }
                }
            }
            //Update the Deal Info
            else if (this.btnSave.ToolTip == "Update Business Info")
            {
                if (txtEmail.Text.Trim() != hfEmail.Value.Trim())
                {
                    obj.userName = this.txtEmail.Text.Trim();
                    if (obj.getUserByUserName())
                    {
                        lblAddressError.Text = "Email already exists. Please choose another.";
                        lblAddressError.Visible = true;
                        lblAddressError.ForeColor = System.Drawing.Color.Red;
                        ImgAddError.Visible = true;
                        ImgAddError.ImageUrl = "images/error.png";
                        return;
                    }
                    else
                    {
                        //For Updating the Business User Account Info
                        obj.userName = this.txtEmail.Text.Trim();
                        //Set the Password field here
                        obj.userPassword = this.txtPwd.Text.Trim();
                        //Create New Busineess Account
                        int ichk = obj.updateUserInfoByUsername(this.hfEmail.Value);
                    }
                }
                else
                {
                    //For Updating the Business User Account Info
                    obj.userName = this.txtEmail.Text.Trim();
                    //Set the Password field here
                    obj.userPassword = this.txtPwd.Text.Trim();
                    //Create New Busineess Account
                    int ichk = obj.updateUserInfoByUsername(this.hfEmail.Value);
                }

                //If Image 1 exists
                try
                {
                    if (fpBusinessImg.HasFile)
                    {
                        if (this.imgUpload1.Src.ToString().Length > 2)
                        {
                            string strImgName = "";

                            strImgName = this.imgUpload1.Src.ToString().Substring(this.imgUpload1.Src.ToString().LastIndexOf("/") + 1, (this.imgUpload1.Src.ToString().Length - (this.imgUpload1.Src.ToString().LastIndexOf("/") + 1)));

                            string path = AppDomain.CurrentDomain.BaseDirectory + "Images\\RestaurantImages\\" + strImgName;

                            this.imgUpload1.Src = "";

                            if (File.Exists(path))
                            {
                                try
                                {
                                    //Delete the File
                                    File.Delete(path);
                                }
                                catch (Exception ex) { }
                            }
                        }
                        //upload the Image here
                        strImageName = ImageUploadHere(fpBusinessImg);
                    }
                    else
                    {
                        strImageName = this.imgUpload1.Src.ToString().Substring(this.imgUpload1.Src.ToString().LastIndexOf("/") + 1, (this.imgUpload1.Src.ToString().Length - (this.imgUpload1.Src.ToString().LastIndexOf("/") + 1)));
                    }
                }
                catch (Exception ex)
                { }
                string strLogoImage = "";
                try
                {
                    if (fuLogo.HasFile)
                    {
                        if (this.imglogo.Src.ToString().Length > 2)
                        {
                            string strImgName = "";

                            strImgName = this.imglogo.Src.ToString().Substring(this.imglogo.Src.ToString().LastIndexOf("/") + 1, (this.imglogo.Src.ToString().Length - (this.imglogo.Src.ToString().LastIndexOf("/") + 1)));

                            string path = AppDomain.CurrentDomain.BaseDirectory + "Images\\RestaurantImages\\" + strImgName;

                            this.imglogo.Src = "";

                            if (File.Exists(path))
                            {
                                try
                                {
                                    //Delete the File
                                    File.Delete(path);
                                }
                                catch (Exception ex) { }
                            }
                        }
                        //upload the Image here
                        strLogoImage = LogoUploadHere(fuLogo);
                    }
                    else
                    {
                        strLogoImage = this.imglogo.Src.ToString().Substring(this.imglogo.Src.ToString().LastIndexOf("/") + 1, (this.imglogo.Src.ToString().Length - (this.imglogo.Src.ToString().LastIndexOf("/") + 1)));
                    }
                }
                catch (Exception ex)
                { }

                //Update the Deal info by Deal Id
                UpdateBusinessInfo(strImageName,strLogoImage, hfBusinessId.Value);
                try
                {
                    if (!cbOnlineDeal.Checked)
                    {
                        if (GridView2 != null && GridView2.Rows.Count > 0)
                        {
                            string strgoogleMapURL = "";
                            for (int i = 0; i < GridView2.Rows.Count; i++)
                            {
                                Label lblCommentText = (Label)GridView2.Rows[i].FindControl("lblCommentText");
                                if (i == 0)
                                {
                                    strgoogleMapURL = "http://maps.google.com/maps/api/staticmap?center=" + lblCommentText.Text.Trim() + "&size=248x220&maptype=roadmap%20";
                                }
                                if (lblCommentText != null && lblCommentText.Text.Trim() != "")
                                {

                                    //strgoogleMapURL += "&markers=icon:http://www.tazzling.com/Images/gmap/" + (i + 1).ToString() + ".png|52.083033, -113.444910";
                                    strgoogleMapURL += "&markers=icon:http://www.tazzling.com/Images/gmap/" + (i + 1).ToString() + ".png|" + Misc.getMapValues(lblCommentText.Text.Trim().Replace("#", "No."));
                                }
                            }

                            System.Drawing.Image _Image = null;
                            _Image = DownloadImage(strgoogleMapURL + "&sensor=false");

                            // check for valid image
                            if (_Image != null)
                            {
                                try
                                {
                                    if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + "Images\\dealfood\\" + hfBusinessId.Value + "\\" + hfBusinessId.Value + ".png"))
                                    {
                                        File.Delete(AppDomain.CurrentDomain.BaseDirectory + "Images\\dealfood\\" + hfBusinessId.Value + "\\" + hfBusinessId.Value + ".png");
                                    }
                                }
                                catch (Exception ex)
                                {

                                }

                                if (Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + "Images\\dealfood\\" + hfBusinessId.Value))
                                {
                                    _Image.Save(AppDomain.CurrentDomain.BaseDirectory + "Images\\dealfood\\" + hfBusinessId.Value + "\\" + hfBusinessId.Value + ".png");
                                }
                                else
                                {
                                    Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "Images\\dealfood\\" + hfBusinessId.Value);
                                    _Image.Save(AppDomain.CurrentDomain.BaseDirectory + "Images\\dealfood\\" + hfBusinessId.Value + "\\" + hfBusinessId.Value + ".png");
                                }
                            }
                        }
                    }
                    else
                    {
                        try
                        {
                            BLLRestaurantGoogleAddresses objGoogleAddress = new BLLRestaurantGoogleAddresses();
                            objGoogleAddress.restaurantId = Convert.ToInt64(hfBusinessId.Value);
                            objGoogleAddress.deleteAllRestaurantGoogleAddressesByRestaurantID();
                            objGoogleAddress = new BLLRestaurantGoogleAddresses();
                            objGoogleAddress.restaurantGoogleAddress = "Online";
                            objGoogleAddress.restaurantId = Convert.ToInt64(hfBusinessId.Value);
                            objGoogleAddress.createRestaurantGoogleAddresses();
                        }
                        catch (Exception ex)
                        {

                        }
                    }
                }
                catch (Exception ex)
                { }

                Response.Redirect("CloseForm.aspx", true);


            }
        }
        catch (Exception ex)
        { }
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

    public string GetPassword()
    {
        StringBuilder builder = new StringBuilder();
        builder.Append(RandomNumber(10000, 99999));
        builder.Append(RandomNumber(10000, 99999));
        return builder.ToString();
    }

    private string RandomString(int size, bool lowerCase)
    {
        StringBuilder builder = new StringBuilder();
        Random random = new Random();
        char ch;
        for (int i = 0; i < size; i++)
        {
            ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
            builder.Append(ch);
        }
        if (lowerCase)
            return builder.ToString().ToLower();
        return builder.ToString();
    }

    private int RandomNumber(int min, int max)
    {
        Random random = new Random();
        return random.Next(min, max);
    }

    protected void UpdateBusinessInfo(string strImageName,string strLogoImage,  string strBusinessID)
    {
        try
        {
            BLLRestaurant objBLLRestaurant = new BLLRestaurant();
            objBLLRestaurant.restaurantId = int.Parse(strBusinessID);
            objBLLRestaurant.firstName = this.txtFname.Text.Trim();
            objBLLRestaurant.lastName = this.txtLname.Text.Trim();
            objBLLRestaurant.restaurantBusinessName = this.txtBusinessName.Text.Trim();
            objBLLRestaurant.businessPaymentTitle = this.txtBPaymentTitle.Text.Trim();
            objBLLRestaurant.commission = "0";
            objBLLRestaurant.restaurantlogo = strLogoImage;
            objBLLRestaurant.email = this.txtEmail.Text.Trim();
            //if (txtEmail.Text.Trim() != "")
            //{
            //    Misc.addSubscriberEmail(txtEmail.Text.Trim(), "337");
            //}
            objBLLRestaurant.cellNumber = txtCellNumber.Text.Trim();
            objBLLRestaurant.preDealVerification = ddlPreDealVerification.SelectedValue.ToString().Trim();
            objBLLRestaurant.postDealVerification = ddlPostDealVerification.SelectedValue.ToString().Trim();

            objBLLRestaurant.restaurantAddress = txtBusinessAddress.Text.Trim().Replace("\n", "<br>");
            objBLLRestaurant.restaurantpaymentAddress = txtBusinessPaymentAddress.Text.Trim().Replace("\n", "<br>");

            objBLLRestaurant.alternativeEmail = txtAlternativeEmail.Text.Trim();
            //if (txtAlternativeEmail.Text.Trim() != "")
            //{
            //    Misc.addSubscriberEmail(txtAlternativeEmail.Text.Trim(), "337");
            //}
            objBLLRestaurant.url = txtLink.Text.Trim();

            //  objBLLRestaurant.cityId = Convert.ToInt32(this.ddlSelectCity.SelectedValue);
            // objBLLRestaurant.provinceId = Convert.ToInt32(this.ddlProvinceLive.SelectedValue);
            objBLLRestaurant.phone = this.txtPhone1.Text.Trim();
            objBLLRestaurant.fax = this.txtFax.Text.Trim();
            // objBLLRestaurant.zipCode = this.txtZipCode.Text.Trim();
            objBLLRestaurant.restaurantPicture = strImageName;
            objBLLRestaurant.detail = this.txtResDetail.Text.Trim();
            objBLLRestaurant.modifiedBy = Convert.ToInt64(ViewState["userID"]);
            objBLLRestaurant.modifiedDate = DateTime.Now;
            objBLLRestaurant.isActive = this.chkIsActive.Checked ? true : false;
            objRaddress = new BLLRestaurantAddresses();
            //if (ViewState["raID"] != null)
            //{
            //    objRaddress.raID = Convert.ToInt64(ViewState["raID"].ToString());
            //    objRaddress.restaurantId = Convert.ToInt64(strBusinessID);
            //    objRaddress.restaurantAddress = txtDealAddress.Text.Trim();
            //    objRaddress.updateRestaurantAddresses();
            //    ViewState["raID"] = null;
            //}
            //else
            //{
            //    objRaddress.restaurantId = Convert.ToInt64(strBusinessID);
            //    objRaddress.restaurantAddress = txtDealAddress.Text.Trim();

            //    objRaddress.createRestaurantAddresse();
            //}
            int iChk = objBLLRestaurant.updateRestaurantInfo();
            if (hfUserID.Value != "" && hfUserID.Value != "0")
            {
                obj = new BLLUser();
                obj.userId = Convert.ToInt32(hfUserID.Value.ToString());
                obj.isActive = this.chkIsActive.Checked ? true : false;
                obj.changeUserStatus();
            }


            lblAddressError.Text = "Busniess has been updated successfully.";
            lblAddressError.Visible = true;
            ImgAddError.Visible = true;
            ImgAddError.ImageUrl = "images/Checked.png"; lblAddressError.ForeColor = System.Drawing.Color.Black;

        }
        catch (Exception ex)
        {
                        
            lblAddressError.Text = ex.ToString();
            lblAddressError.Visible = true;
            ImgAddError.Visible = true;
            ImgAddError.ImageUrl = "images/error.png";
            lblAddressError.ForeColor = System.Drawing.Color.Red;
        }
    }
        
    protected void editResturantInfo(int restID)
    {
        try
        {
            cbOnlineDeal.Checked = false;
            lblAddressError.Visible = false;
            lblAddressError.Text = "";
            ImgAddError.Visible = false;
            upBusinessMgmtForm.Visible = true;
            
            //Change the Image URL of the Save button
            this.btnSave.ImageUrl = "~/admin/images/btnUpdate.jpg";

            this.btnSave.ToolTip = "Update Business Info";

            cbUseThisEmail.Checked = false;
            cbUseThisEmail.Visible = false;

            //Initilize the BLLRestaurant here
            BLLRestaurant objBLLRestaurant = new BLLRestaurant();

            //Initilize the DataTable here
            DataTable dtRestaurant = null;

            this.hfBusinessId.Value = restID.ToString();

            objBLLRestaurant.restaurantId = restID;

            dtRestaurant = objBLLRestaurant.getRestaurantInfoByResturantID();

            if ((dtRestaurant != null) && (dtRestaurant.Rows.Count > 0))
            {

                txtBusinessPaymentAddress.Text = DBNull.Value.Equals(dtRestaurant.Rows[0]["restaurantpaymentAddress"]) ? "" : dtRestaurant.Rows[0]["restaurantpaymentAddress"].ToString().Trim().Replace("<br>", "\n"); ;
                txtBusinessAddress.Text = DBNull.Value.Equals(dtRestaurant.Rows[0]["restaurantAddress"]) ? "" : dtRestaurant.Rows[0]["restaurantAddress"].ToString().Trim().Replace("<br>", "\n"); 
                txtAlternativeEmail.Text = DBNull.Value.Equals(dtRestaurant.Rows[0]["alternativeEmail"]) ? "" : dtRestaurant.Rows[0]["alternativeEmail"].ToString().Trim(); 
                txtLink.Text = DBNull.Value.Equals(dtRestaurant.Rows[0]["url"]) ? "" : dtRestaurant.Rows[0]["url"].ToString().Trim();
                txtCellNumber.Text = dtRestaurant.Rows[0]["cellNumber"].ToString().Trim();
                if (dtRestaurant.Rows[0]["preDealVerification"] != null && dtRestaurant.Rows[0]["preDealVerification"].ToString().Trim() != "")
                {
                    try
                    {
                        ddlPreDealVerification.SelectedValue = dtRestaurant.Rows[0]["preDealVerification"].ToString().Trim();
                    }
                    catch (Exception ex)
                    {
                        ddlPreDealVerification.SelectedIndex = 0;
                    }
                }
                else
                {
                    ddlPreDealVerification.SelectedIndex = 0;
                }

                if (dtRestaurant.Rows[0]["postDealVerification"] != null && dtRestaurant.Rows[0]["postDealVerification"].ToString().Trim() != "")
                {
                    try
                    {
                        ddlPostDealVerification.SelectedValue = dtRestaurant.Rows[0]["postDealVerification"].ToString().Trim();
                    }
                    catch (Exception ex)
                    {
                        ddlPostDealVerification.SelectedIndex = 0;
                    }
                }
                else
                {
                    ddlPostDealVerification.SelectedIndex = 0;
                }


                this.txtFname.Text = DBNull.Value.Equals(dtRestaurant.Rows[0]["firstName"]) ? "" : dtRestaurant.Rows[0]["firstName"].ToString().Trim();

                this.txtLname.Text = DBNull.Value.Equals(dtRestaurant.Rows[0]["lastName"]) ? "" : dtRestaurant.Rows[0]["lastName"].ToString().Trim();

                this.txtBPaymentTitle.Text = DBNull.Value.Equals(dtRestaurant.Rows[0]["businessPaymentTitle"]) ? "" : dtRestaurant.Rows[0]["businessPaymentTitle"].ToString().Trim();
                
                
                this.txtBusinessName.Text = DBNull.Value.Equals(dtRestaurant.Rows[0]["restaurantBusinessName"]) ? "" : dtRestaurant.Rows[0]["restaurantBusinessName"].ToString().Trim();
                hfUserID.Value = dtRestaurant.Rows[0]["userID"].ToString();
                this.txtEmail.Text = DBNull.Value.Equals(dtRestaurant.Rows[0]["email"]) ? "" : dtRestaurant.Rows[0]["email"].ToString().Trim();
                //Set the Business Password here
                this.txtPwd.Text = DBNull.Value.Equals(dtRestaurant.Rows[0]["userPassword"]) ? "" : dtRestaurant.Rows[0]["userPassword"].ToString().Trim();
                this.txtPwdConfirm.Text = DBNull.Value.Equals(dtRestaurant.Rows[0]["userPassword"]) ? "" : dtRestaurant.Rows[0]["userPassword"].ToString().Trim();
                this.hfEmail.Value = this.txtEmail.Text;
                
                this.txtDealAddress.Text = "";
                           

                BindAddressGrid(Convert.ToInt64(dtRestaurant.Rows[0]["restaurantId"].ToString().Trim()));
                BindGoogleAddressGrid(Convert.ToInt64(dtRestaurant.Rows[0]["restaurantId"].ToString().Trim()));
               
                txtPhone1.Text = dtRestaurant.Rows[0]["phone"].ToString().Trim();
                this.txtFax.Text = DBNull.Value.Equals(dtRestaurant.Rows[0]["fax"]) ? "" : dtRestaurant.Rows[0]["fax"].ToString().Trim();

                // this.txtZipCode.Text = DBNull.Value.Equals(dtRestaurant.Rows[0]["zipCode"]) ? "" : dtRestaurant.Rows[0]["zipCode"].ToString().Trim();

                string strImageName = DBNull.Value.Equals(dtRestaurant.Rows[0]["restaurantPicture"]) ? "" : dtRestaurant.Rows[0]["restaurantPicture"].ToString().Trim();
                

                if (strImageName != "")
                {
                    string strSrcPath = AppDomain.CurrentDomain.BaseDirectory + "Images\\RestaurantImages\\" + strImageName;

                    if (File.Exists(strSrcPath))
                    {
                        //Set the First Image here
                        this.imgUpload1.Src = "../Images/RestaurantImages/" + strImageName;
                        this.imgUpload1.Visible = true;
                    }
                }

                string strLogoName = DBNull.Value.Equals(dtRestaurant.Rows[0]["restaurantlogo"]) ? "" : dtRestaurant.Rows[0]["restaurantlogo"].ToString().Trim();

                if (strLogoName != "")
                {
                    string strSrcPath = AppDomain.CurrentDomain.BaseDirectory + "Images\\RestaurantImages\\" + strLogoName;

                    if (File.Exists(strSrcPath))
                    {
                        //Set the First Image here
                        this.imglogo.Src = "../Images/RestaurantImages/" + strLogoName;
                        this.imglogo.Visible = true;
                    }
                }

                this.txtResDetail.Text = DBNull.Value.Equals(dtRestaurant.Rows[0]["detail"]) ? "" : dtRestaurant.Rows[0]["detail"].ToString().Trim();

                this.chkIsActive.Checked = DBNull.Value.Equals(dtRestaurant.Rows[0]["isActive"]) ? false : bool.Parse(dtRestaurant.Rows[0]["isActive"].ToString());
            }
        }
        catch (Exception ex)
        {
                        
            lblAddressError.Text = ex.ToString();
            lblAddressError.Visible = true;
            ImgAddError.Visible = true;
            ImgAddError.ImageUrl = "images/error.png";
            lblAddressError.ForeColor = System.Drawing.Color.Red;
        }
    }

    private void BindAddressGrid(long restaurantID)
    {
        try
        {
            objRaddress = new BLLRestaurantAddresses();
            objRaddress.restaurantId = restaurantID;
            DataTable dtRestAddress = objRaddress.getAllRestaurantAddressesByRestaurantID();

            GridView1.DataSource = dtRestAddress.DefaultView;
            GridView1.DataBind();
        }
        catch (Exception ex)
        { }
    }

    private void BindGoogleAddressGrid(long restaurantID)
    {
        try
        {
            objGoogleAddress = new BLLRestaurantGoogleAddresses();
            objGoogleAddress.restaurantId = restaurantID;
            DataTable dtRestAddress = objGoogleAddress.getAllRestaurantGoogleAddressesByRestaurantID();
            if (dtRestAddress != null && dtRestAddress.Rows.Count > 0)
            {
                GridView2.DataSource = dtRestAddress.DefaultView;
                GridView2.DataBind();
                if (dtRestAddress.Rows[0]["restaurantGoogleAddress"].ToString().Trim().ToLower() == "online")
                {
                    strGoogleAddress = "none";
                    cbOnlineDeal.Checked = true;
                }                
            }
        }
        catch (Exception ex)
        { }
    }
               
    protected void CancelButton_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            Response.Redirect("CloseForm.aspx", true);            
        }
        catch (Exception ex)
        {                       
            lblAddressError.Text = ex.ToString();
            lblAddressError.Visible = true;
            ImgAddError.Visible = true;
            ImgAddError.ImageUrl = "images/error.png";
            lblAddressError.ForeColor = System.Drawing.Color.Red;
        }
    }
  
    protected void addNew()
    {
        try
        {
            //Hide the message area
            lblAddressError.Visible = false;
            lblAddressError.Text = "";
            ImgAddError.Visible = false;            
                        
            //Change the Image URL of the Save button
            this.btnSave.ImageUrl = "~/admin/images/btnSave.jpg";

            this.btnSave.ToolTip = "Add New Business Info";

            //Clear All the fields
            this.txtFname.Text = "";
            this.txtLname.Text = "";
            this.txtBusinessName.Text = "";
            this.txtEmail.Text = "";
            this.hfEmail.Value = "";
            this.txtDealAddress.Text = "";
            txtBPaymentTitle.Text = "";
           
            cbUseThisEmail.Checked = false;
            cbUseThisEmail.Visible = false;
            cbOnlineDeal.Checked = false;
                        
            //this.ddlProvinceLive.SelectedValue = "";
            txtBusinessPaymentAddress.Text = "";
            txtBusinessAddress.Text = "";
            txtAlternativeEmail.Text = "";
            txtLink.Text = "";
            
            this.txtPwd.Text = "";
            this.txtPwdConfirm.Text = "";

            this.txtPhone1.Text = "";            
            this.txtFax.Text = "";

            txtCellNumber.Text = "";
            ddlPostDealVerification.SelectedIndex = 0;
            ddlPreDealVerification.SelectedIndex = 0;

            this.txtResDetail.Text = "";
            this.imgUpload1.Visible = false;
            hfBusinessId.Value = "";
            hfDealAddress.Value = "";
            hfGoogleAddress.Value = "";
            this.chkIsActive.Checked = true;
            ViewState["dtDealAddress"] = null;
            ViewState["dtGoogleAddress"] = null;
            GridView1.DataSource = null;
            GridView1.DataBind();
            GridView2.DataSource = null;
            GridView2.DataBind();
            
        }
        catch (Exception ex)
        {
            string strException = "There is an error occur, please email us at support@tazzling.com or call 1855-295-1771.";
        }
    }

    private string LogoUploadHere(FileUpload fileUploadDealImg)
    {
        string strUniqueID = "";

        try
        {

            if (fileUploadDealImg.HasFile)
            {
                string[] strExtension = fileUploadDealImg.FileName.Split('.');
                strUniqueID = Guid.NewGuid().ToString() + "." + strExtension[strExtension.Length - 1];
                string strDestPath = AppDomain.CurrentDomain.BaseDirectory + "Images\\RestaurantImages\\" + strUniqueID;
                string strSrcPath = AppDomain.CurrentDomain.BaseDirectory + "Images\\RestaurantImages\\" + fileUploadDealImg.FileName;
                fileUploadDealImg.SaveAs(strSrcPath);
                Misc.SaveBusinessLogoImage(strSrcPath, strDestPath);
            }
        }
        catch (Exception ex)
        {

        }

        return strUniqueID;
    }

    private string ImageUploadHere(FileUpload fileUploadDealImg)
    {
        string strUniqueID = "";

        try
        {

            if (fileUploadDealImg.HasFile)
            {

                string[] strExtension = fileUploadDealImg.FileName.Split('.');

                strUniqueID = Guid.NewGuid().ToString() + "." + strExtension[strExtension.Length - 1];

                string strSrcPath = AppDomain.CurrentDomain.BaseDirectory + "Images\\RestaurantImages\\" + fileUploadDealImg.FileName;

                fileUploadDealImg.SaveAs(strSrcPath);

                string strthumbSave = AppDomain.CurrentDomain.BaseDirectory + "Images\\RestaurantImages\\";

                if (!Directory.Exists(strthumbSave))
                {
                    Directory.CreateDirectory(strthumbSave);
                }

                string SrcFileName = fileUploadDealImg.FileName;

                Misc.CreateThumbnailFeaturedFood(strSrcPath, strthumbSave, SrcFileName, strUniqueID);

                File.Delete(strSrcPath);
            }
        }
        catch (Exception ex)
        {
            
        }

        return strUniqueID;
    }

    #region"Save New Business Info here"
    BLLRestaurantAddresses objRaddress = new BLLRestaurantAddresses();
    BLLRestaurantGoogleAddresses objGoogleAddress = new BLLRestaurantGoogleAddresses();
    private void AddNewBusniessInfo(string strImageNames, string strLogoImage, long userid)
    {
        try
        {
            hfBusinessId.Value = "";
            BLLRestaurant objBLLRestaurant = new BLLRestaurant();
            objBLLRestaurant.firstName = this.txtFname.Text.Trim();
            objBLLRestaurant.lastName = this.txtLname.Text.Trim();
            objBLLRestaurant.restaurantBusinessName = this.txtBusinessName.Text.Trim();
            objBLLRestaurant.restaurantlogo = strLogoImage;
            objBLLRestaurant.email = this.txtEmail.Text.Trim();
            if (txtEmail.Text.Trim() != "")
            {
                AddSubscriberToExactTargetList(txtEmail.Text.Trim());
            }
            objBLLRestaurant.restaurantAddress = txtBusinessAddress.Text.Trim().Replace("\n", "<br>");
            objBLLRestaurant.restaurantpaymentAddress = txtBusinessPaymentAddress.Text.Trim().Replace("\n", "<br>"); 
            objBLLRestaurant.alternativeEmail = txtAlternativeEmail.Text.Trim();
            //if (txtAlternativeEmail.Text.Trim() != "")
            //{
            //    Misc.addSubscriberEmail(txtAlternativeEmail.Text.Trim(), "337");
            //}
            objBLLRestaurant.url = txtLink.Text.Trim();
            objBLLRestaurant.phone = this.txtPhone1.Text.Trim();
            objBLLRestaurant.cellNumber = txtCellNumber.Text.Trim();
            objBLLRestaurant.preDealVerification = ddlPreDealVerification.SelectedValue.ToString().Trim();
            objBLLRestaurant.postDealVerification = ddlPostDealVerification.SelectedValue.ToString().Trim();
            objBLLRestaurant.fax = this.txtFax.Text.Trim();
            objBLLRestaurant.businessPaymentTitle = txtBPaymentTitle.Text.Trim();
            objBLLRestaurant.commission = "0";
           
            
            objBLLRestaurant.restaurantPicture = strImageNames;
            objBLLRestaurant.detail = this.txtResDetail.Text.Trim();
            objBLLRestaurant.createdBy = Convert.ToInt64(ViewState["userID"]);
            objBLLRestaurant.createdDate = DateTime.Now;
            objBLLRestaurant.isActive = this.chkIsActive.Checked ? true : false;
            objBLLRestaurant.userID = userid;
            int iChk = objBLLRestaurant.createRestaurant();
            hfBusinessId.Value = iChk.ToString();
            try
            {
                if (!cbOnlineDeal.Checked)
                {
                    if (GridView2 != null && GridView2.Rows.Count > 0)
                    {

                        string strgoogleMapURL = "";
                        for (int i = 0; i < GridView2.Rows.Count; i++)
                        {
                            Label lblCommentText = (Label)GridView2.Rows[i].FindControl("lblCommentText");
                            if (i == 0)
                            {
                                strgoogleMapURL = "http://maps.google.com/maps/api/staticmap?center=" + lblCommentText.Text.Trim() + "&size=248x220&maptype=roadmap%20";
                            }
                            if (lblCommentText != null && lblCommentText.Text.Trim() != "")
                            {
                                strgoogleMapURL += "&markers=icon:http://www.tazzling.com/Images/gmap/" + (i + 1).ToString() + ".png|" + Misc.getMapValues(lblCommentText.Text.Trim().Replace("#", "No."));                                    
                            }
                        }

                        System.Drawing.Image _Image = null;
                        _Image = DownloadImage(strgoogleMapURL + "&sensor=false");

                        // check for valid image
                        if (_Image != null)
                        {
                            try
                            {
                                if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + "Images\\dealfood\\" + hfBusinessId.Value + "\\" + hfBusinessId.Value + ".png"))
                                {
                                    File.Delete(AppDomain.CurrentDomain.BaseDirectory + "Images\\dealfood\\" + hfBusinessId.Value + "\\" + hfBusinessId.Value + ".png");
                                }
                            }
                            catch (Exception ex)
                            {
 
                            }
                            if (Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + "Images\\dealfood\\" + hfBusinessId.Value))
                            {
                                _Image.Save(AppDomain.CurrentDomain.BaseDirectory + "Images\\dealfood\\" + hfBusinessId.Value + "\\" + hfBusinessId.Value + ".png");
                            }
                            else
                            {
                                Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "Images\\dealfood\\" + hfBusinessId.Value);
                                _Image.Save(AppDomain.CurrentDomain.BaseDirectory + "Images\\dealfood\\" + hfBusinessId.Value + "\\" + hfBusinessId.Value + ".png");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            { }

            if (ViewState["dtDealAddress"] != null)
            {
                DataTable dtDealAddress = (DataTable)ViewState["dtDealAddress"];
                if (dtDealAddress != null && dtDealAddress.Rows.Count > 0)
                {
                    objRaddress = new BLLRestaurantAddresses();
                    for (int i = 0; i < dtDealAddress.Rows.Count; i++)
                    {                     
                        objRaddress.restaurantAddress = dtDealAddress.Rows[i]["restaurantAddress"].ToString().Trim();
                        objRaddress.restaurantId = iChk;
                        objRaddress.createRestaurantAddresse();
                    }
                }
            }
            if (cbOnlineDeal.Checked)
            {
                objGoogleAddress = new BLLRestaurantGoogleAddresses();
                objGoogleAddress.restaurantGoogleAddress = "Online";
                objGoogleAddress.restaurantId = iChk;
                objGoogleAddress.createRestaurantGoogleAddresses();
            }
            else
            {
                if (ViewState["dtGoogleAddress"] != null)
                {
                    DataTable dtGoogleAddress = (DataTable)ViewState["dtGoogleAddress"];
                    if (dtGoogleAddress != null && dtGoogleAddress.Rows.Count > 0)
                    {
                        objGoogleAddress = new BLLRestaurantGoogleAddresses();
                        for (int i = 0; i < dtGoogleAddress.Rows.Count; i++)
                        {
                            objGoogleAddress.restaurantGoogleAddress = dtGoogleAddress.Rows[i]["restaurantGoogleAddress"].ToString().Trim();
                            objGoogleAddress.restaurantId = iChk;
                            objGoogleAddress.createRestaurantGoogleAddresses();
                        }
                    }
                }
            }

            Response.Redirect("CloseForm.aspx", true);
        }
        catch (Exception ex)
        {
            string strException = "There is an error occur, please email us at support@tazzling.com or call 1855-295-1771.";
        }
    }

    private static void AddSubscriberToExactTargetList(string strEmail)
    {
        try
        {
            BLLNewsLetterSubscriber obj = new BLLNewsLetterSubscriber();
            obj.Email = strEmail;
            obj.CityId = 337;
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
            sub.Lists[0].ID = 643;//Available in the UI via List Properties
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
        catch (Exception ex)
        { }
    }

    #endregion
    
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int result = 0;
        try
        {
            if (hfBusinessId.Value != "")
            {
                objRaddress = new BLLRestaurantAddresses();
                Label lblraID = (Label)GridView1.Rows[e.RowIndex].FindControl("lblraID");
                Label lblGridrestaurantId = (Label)GridView1.Rows[e.RowIndex].FindControl("lblGridrestaurantId");                
                if (lblraID != null)
                {
                    objRaddress.raID = Convert.ToInt32(lblraID.Text.Trim());
                    result = objRaddress.deleteRestaurantAddresses();
                }
                if (lblGridrestaurantId != null)
                {
                    BindAddressGrid(Convert.ToInt64(lblGridrestaurantId.Text.Trim()));
                }
            }
            else
            {
                DataTable dtDealAddress = (DataTable)ViewState["dtDealAddress"];
                if (dtDealAddress != null && dtDealAddress.Rows.Count > 0)
                {
                    dtDealAddress.Rows[e.RowIndex].Delete();                    
                }
                for (int i = 0; i < dtDealAddress.Rows.Count; i++)
                {
                    dtDealAddress.Rows[i]["raID"] = i;
                }
                GridView1.DataSource = dtDealAddress.DefaultView;
                GridView1.DataBind();
                ViewState["dtDealAddress"] = dtDealAddress;                                                   
            }
        }
        catch (Exception ex)
        {
            lblAddressError.Text = ex.ToString();
            lblAddressError.Visible = true;
            ImgAddError.Visible = true;
            ImgAddError.ImageUrl = "images/error.png";
            lblAddressError.ForeColor = System.Drawing.Color.Red;
        }
    }

    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (hfBusinessId.Value != "")
            {
                objRaddress = new BLLRestaurantAddresses();
                objRaddress.raID = Convert.ToInt32(GridView1.SelectedDataKey.Value);
                hfDealAddress.Value = GridView1.SelectedDataKey.Value.ToString();                
                DataTable dtRestaurant = objRaddress.getRestaurantAddressesByID();
                if ((dtRestaurant != null) && (dtRestaurant.Rows.Count > 0))
                {
                    this.txtDealAddress.Text = DBNull.Value.Equals(dtRestaurant.Rows[0]["restaurantAddress"]) ? "" : dtRestaurant.Rows[0]["restaurantAddress"].ToString().Trim();
                }
            }
            else
            {
                int intRow = Convert.ToInt32(GridView1.SelectedDataKey.Value);
                DataTable dtDealAddress = (DataTable)ViewState["dtDealAddress"];
                if (dtDealAddress != null && dtDealAddress.Rows.Count > 0)
                {
                    this.txtDealAddress.Text = DBNull.Value.Equals(dtDealAddress.Rows[intRow]["restaurantAddress"]) ? "" : dtDealAddress.Rows[intRow]["restaurantAddress"].ToString().Trim();
                }
                hfDealAddress.Value = intRow.ToString();                
            }
            btnAddDealAddress.ImageUrl = "~/admin/images/btnUpdate.jpg";                                 
        }
        catch (Exception ex)
        {            
            lblAddressError.Text = ex.ToString();
            lblAddressError.Visible = true;
            ImgAddError.Visible = true;
            ImgAddError.ImageUrl = "images/error.png";
            lblAddressError.ForeColor = System.Drawing.Color.Red;
        }
    }

    protected void GridView2_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int result = 0;
        try
        {
            if (hfBusinessId.Value != "")
            {
                objGoogleAddress = new BLLRestaurantGoogleAddresses();
                Label lblrgaID = (Label)GridView2.Rows[e.RowIndex].FindControl("lblrgaID");
                Label lblGridrestaurantId = (Label)GridView2.Rows[e.RowIndex].FindControl("lblGridrestaurantId");                
                if (lblrgaID != null)
                {
                    objGoogleAddress.rgaID = Convert.ToInt32(lblrgaID.Text.Trim());
                    result = objGoogleAddress.deleteRestaurantGoogleAddresses();
                }
                if (lblGridrestaurantId != null)
                {
                    BindGoogleAddressGrid(Convert.ToInt64(lblGridrestaurantId.Text.Trim()));
                }
            }
            else
            {
                DataTable dtGoogleAddress = (DataTable)ViewState["dtGoogleAddress"];
                if (dtGoogleAddress != null && dtGoogleAddress.Rows.Count > 0)
                {
                    dtGoogleAddress.Rows[e.RowIndex].Delete();
                }
                for (int i = 0; i < dtGoogleAddress.Rows.Count; i++)
                {
                    dtGoogleAddress.Rows[i]["rgaID"] = i;
                }
                GridView2.DataSource = dtGoogleAddress.DefaultView;
                GridView2.DataBind();
                ViewState["dtGoogleAddress"] = dtGoogleAddress;
            }
        }
        catch (Exception ex)
        {           
            lblAddressError.Text = ex.ToString();
            lblAddressError.Visible = true;
            ImgAddError.Visible = true;
            ImgAddError.ImageUrl = "images/error.png";
            lblAddressError.ForeColor = System.Drawing.Color.Red;
        }
    }

    protected void GridView2_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (hfBusinessId.Value != "")
            {
                objGoogleAddress = new BLLRestaurantGoogleAddresses();
                objGoogleAddress.rgaID = Convert.ToInt32(GridView2.SelectedDataKey.Value);
                hfGoogleAddress.Value = GridView2.SelectedDataKey.Value.ToString();                
                DataTable dtRestaurant = objGoogleAddress.getRestaurantGoogleAddressesByID();
                if ((dtRestaurant != null) && (dtRestaurant.Rows.Count > 0))
                {
                    this.txtGoogleAddress.Text = DBNull.Value.Equals(dtRestaurant.Rows[0]["restaurantGoogleAddress"]) ? "" : dtRestaurant.Rows[0]["restaurantGoogleAddress"].ToString().Trim();
                }
            }
            else
            {
                int intRow = Convert.ToInt32(GridView2.SelectedDataKey.Value);
                DataTable dtGoogleAddress = (DataTable)ViewState["dtGoogleAddress"];
                if (dtGoogleAddress != null && dtGoogleAddress.Rows.Count > 0)
                {
                    this.txtGoogleAddress.Text = DBNull.Value.Equals(dtGoogleAddress.Rows[intRow]["restaurantGoogleAddress"]) ? "" : dtGoogleAddress.Rows[intRow]["restaurantGoogleAddress"].ToString().Trim();
                }
                hfGoogleAddress.Value = intRow.ToString();                
            }
            btnAddGoogleAddress.ImageUrl = "~/admin/images/btnUpdate.jpg";            
        }
        catch (Exception ex)
        {                        
            lblAddressError.Text = ex.ToString();
            lblAddressError.Visible = true;
            ImgAddError.Visible = true;
            ImgAddError.ImageUrl = "images/error.png";
            lblAddressError.ForeColor = System.Drawing.Color.Red;
        }
    }

}