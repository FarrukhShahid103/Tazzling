using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;

public partial class Apps : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Title = getCityName() + "'s Tasty Daily Deal | App";
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
