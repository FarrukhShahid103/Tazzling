using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Takeout_UserControls_Menu_ctrlClientMenu : System.Web.UI.UserControl
{
    public string strMySite = System.Configuration.ConfigurationSettings.AppSettings["YourSite"].ToString();

    protected void Page_Load(object sender, EventArgs e)
    {        
        if (!IsPostBack)
        {
         //   bindProvincesInMenu();            
        }
    }
    protected void bindProvincesInMenu()
    {
        try
        {
            DataTable dtProvinces = Misc.getProvincesByCountryID(2);
            if (dtProvinces != null && dtProvinces.Rows.Count > 0)
            {
                ltProvinces.Text = "";

                for (int i = 0; i < dtProvinces.Rows.Count; i++)
                {
                    DataTable dtCities = Misc.getCitiesByProvinceID(Convert.ToInt32(dtProvinces.Rows[i]["provinceId"].ToString()));
                    if (dtCities != null && dtCities.Rows.Count > 0)
                    {
                        ltProvinces.Text += "<a href='" + strMySite + "/searchResult.aspx?pid=" + dtProvinces.Rows[i]["provinceId"].ToString() + "' style='font-family: Arial; font-size: 12px; font-weight: bold; text-align:left;'>&nbsp;&nbsp;" + dtProvinces.Rows[i]["provinceName"].ToString() + "</a>";

                        for (int j = 0; j < dtCities.Rows.Count; j++)
                        {
                            ltProvinces.Text += "<a href='" + strMySite + "/searchResult.aspx?pid=" + dtProvinces.Rows[i]["provinceId"].ToString() + "&cName=" + dtCities.Rows[j]["cityName"].ToString() + "' style=' font-family: Arial; text-align:left;font-size: 12px; font-weight: normal;'>&nbsp;&nbsp;&nbsp;&nbsp;-" + dtCities.Rows[j]["cityName"].ToString() + "</a>";
                        }
                    }
                    else
                    {
                            ltProvinces.Text += "<a href='" + strMySite + "/searchResult.aspx?pid=" + dtProvinces.Rows[i]["provinceId"].ToString() + "' style='font-family: Arial; font-size: 12px; font-weight: bold; text-align:left; border-bottom: 1px solid #b45801;'>&nbsp;&nbsp;" + dtProvinces.Rows[i]["provinceName"].ToString() + "</a>";
                    }
                }
            }
        }
        catch (Exception ex)
        {
            
        }
    }
    protected void txt_Click(object sender, EventArgs e)
    {

    }
}
