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
using System.Text.RegularExpressions;
using System.IO;

public partial class affiliate : System.Web.UI.Page
{
    
    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Title = getCityName() + "'s Tasty Daily Deal | Affiliate";
        if (!IsPostBack)
        {
               
        }
    }

    protected string getCityName()
    {
        BLLCities objCity = new BLLCities();
        objCity.cityId = 337;
        HttpCookie yourCity = Request.Cookies["yourCity"];
        if (yourCity != null)
        {
            objCity.cityId = Convert.ToInt32(yourCity.Values[0].ToString().Trim());
        }
        DataTable dtCity = objCity.getCityByCityId();
        if (dtCity != null && dtCity.Rows.Count > 0)
        {
            return dtCity.Rows[0]["cityName"].ToString().Trim();
        }
        return "";
    }
   
}
