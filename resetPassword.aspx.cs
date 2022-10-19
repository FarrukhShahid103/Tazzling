using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using GecLibrary;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using SQLHelper;

public partial class resetPassword : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            try
            {
                if (Request.QueryString["uid"] != null && Request.QueryString["uid"].Trim() != ""
                    && Request.QueryString["ud"] != null && Request.QueryString["ud"].Trim() != "")
                {
                    if (!Page.IsPostBack)
                    {
                        /*string jScript;
                        jScript = "<script>";                        
                        jScript += "MessegeArea('Your link has been expired.' , 'error');";
                        jScript += "</script>";
                        ScriptManager.RegisterClientScriptBlock(btnSignin, typeof(Button), "Javascript", jScript, false);*/
                        
                        GECEncryption objEnc = new GECEncryption();
                        string strDate = objEnc.DecryptData("sherazam", Request.QueryString["ud"].Trim());
                        DateTime dt=DateTime.Now;
                        if (DateTime.TryParse(strDate, out dt))
                        {
                            if (dt.ToString("MM/dd/yyyy").Trim() == DateTime.Now.ToString("MM/dd/yyyy"))
                            {
                                hfUserID.Value = objEnc.DecryptData("sherazam", Request.QueryString["uid"].Trim());
                            }
                            else
                            {
                                lblMessage.ForeColor = System.Drawing.Color.Red;
                                lblMessage.Text = "Your link has been expired.";
                            }
                        }
                        else
                        {
                            lblMessage.ForeColor = System.Drawing.Color.Red;
                            lblMessage.Text = "Your link has been expired.";
                        }
                    }
                }
                else
                {
                    Response.Redirect("default.aspx");
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
    
    protected void btnSignin_Click(object sender, EventArgs e)
    {
        try
        {
            if (hfUserID.Value.Trim() != "")
            {
                GECEncryption objEnc = new GECEncryption();
                string strDate = objEnc.DecryptData("sherazam", Request.QueryString["ud"].Trim());
                DateTime dt = DateTime.Now;
                if (DateTime.TryParse(strDate, out dt))
                {
                    if (dt.ToString("MM/dd/yyyy").Trim() == DateTime.Now.ToString("MM/dd/yyyy"))
                    {
                        SqlParameter[] param = new SqlParameter[2];                        
                        param[0] = new SqlParameter("@userID", hfUserID.Value);
                        param[1] = new SqlParameter("@newPassword", txtPwd.Text.Trim());
                        SqlHelper.ExecuteNonQuery(Misc.connStr, CommandType.StoredProcedure, "spUpdateUserPassword", param);
                        lblMessage.ForeColor = System.Drawing.Color.Black;
                        lblMessage.Text = "Your password has been reset successfully Please <a href='login.aspx'>click here</a> to login.";
                    }
                    else
                    {
                        lblMessage.ForeColor = System.Drawing.Color.Red;
                        lblMessage.Text = "Your link has been expired.";
                    }
                }
                else
                {
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                    lblMessage.Text = "Your link has been expired.";
                }
            }
        }
        catch (Exception ex)
        {
            lblMessage.ForeColor = System.Drawing.Color.Red;
            lblMessage.Text = "There is an error occur, please email us at support@tazzling.com or call 1855-295-1771.";
        }
    }
}