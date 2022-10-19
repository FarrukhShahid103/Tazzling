using System;
using System.Collections;
using System.Collections.Generic;
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


public partial class nodeal : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Title = " Tastygo | No Deal";
        
        if (!IsPostBack)
        {
            if (Request.QueryString["cid"] != null && Request.QueryString["cid"].ToString().Trim() != "" && Request.QueryString["cid"].ToString().Trim() != "0")
            {
                BLLCities objCity = new BLLCities();
                objCity.cityId = Convert.ToInt32(Request.QueryString["cid"].ToString());
                DataTable dtCity = objCity.getCityByCityId();
                if (dtCity != null && dtCity.Rows.Count > 0)
                {
                    lblTopTitle.Text = "No tasty deal for <b>" + dtCity.Rows[0]["cityName"].ToString().Trim() + "</b>";
                    lblHeading.Text = "Deals for " + dtCity.Rows[0]["cityName"].ToString().Trim() + " are coming soon!";
                    lblText.Text = "Signup to our free email alert and get first-hand deals when we launch!";

                    Page.Title = dtCity.Rows[0]["cityName"].ToString().Trim() + " | Tastygo | No Deal";
                }
            }
            else if (Request.QueryString["cid"] != null && Request.QueryString["cid"].ToString().Trim() == "0")
            {
                lblHeading.Text = "Page does not exist!";
                lblText.Text = "Select the city on the top right to continue.";
            }            
        }
    }
}
