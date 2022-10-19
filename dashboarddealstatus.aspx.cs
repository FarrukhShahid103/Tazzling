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
using System.Diagnostics;
using System.IO;

public partial class dashboarddealstatus : System.Web.UI.Page
{
    public string TotalSold = "";
    public string TotalEarn = "";
    public string Redeemed = "";
    public string Unredeemed = "";
    public string SuccessfulOrder = "";
    public string PerRedeemed = "";
    public string TotalOrder = "";
    public string Divmaparea = "";
    public long Male = 0;
    public long Female = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
       if (Request.QueryString["sidedealId"] != null && Request.QueryString["sidedealId"].ToString().Trim() != "")
        {
            BLLDealOrders objdealorder = new BLLDealOrders();
            objdealorder.dealId = Convert.ToInt32(Request.QueryString["sidedealId"]);
            DataTable dtorderfrom = objdealorder.GetOrderfromDetail();
            DataTable dtRedeemed = objdealorder.GetRedeemedDetail();
            DataTable dtdate = objdealorder.GetOrderDate();
           // DataTable dtIpaddress = objdealorder.GetIpAddressfromOrders();
            if (dtorderfrom != null && dtorderfrom.Rows.Count > 0)
            {
                long webpurchase = 0;
                long AppPurchase = 0;
                long MobPurchase = 0;
                long AndroidPurchase = 0;
                string javascript = "";
                webpurchase = Convert.ToInt32(dtorderfrom.Rows[0]["Web"]);
                AppPurchase = Convert.ToInt32(dtorderfrom.Rows[0]["App"]);
                MobPurchase = Convert.ToInt32(dtorderfrom.Rows[0]["Mob"]);
                AndroidPurchase = Convert.ToInt32(dtorderfrom.Rows[0]["Android"]);
                lblDealName.Text = Convert.ToString(dtorderfrom.Rows[0]["DealTitle"]);
                TotalSold = Convert.ToString(dtorderfrom.Rows[0]["sold"]);
                TotalEarn = Convert.ToString(dtorderfrom.Rows[0]["Earn"]);
                TotalOrder = Convert.ToString(dtorderfrom.Rows[0]["TotalOrder"]);
                
                if (webpurchase.ToString().Trim() == "0" && AppPurchase.ToString().Trim() == "0" && MobPurchase.ToString().Trim() == "0" && AndroidPurchase.ToString().Trim() == "0")
                {
                    javascript = "<script>$(document).ready(function(){";
                    javascript += "jQuery.jqplot('chart3',";
                    javascript += "[[['No Order', " + 100 + "]]], ";
                    javascript += "{";
                    javascript += " title: ' ',";
                    javascript += " seriesDefaults: {";
                    javascript += "shadow: false,";
                    javascript += " renderer: jQuery.jqplot.PieRenderer,";
                    javascript += "rendererOptions: {";
                    javascript += " sliceMargin: 4,";
                    javascript += " showDataLabels: true";
                    javascript += "}},";
                    javascript += " legend:{ ";
                    javascript += "  show: true, location: 'e' ";
                    javascript += "}} );";
                    javascript += "});</script>";
                   ltlchart.Text += javascript;
                }
                else
                {

                    javascript = "<script>$(document).ready(function(){";
                    javascript += "jQuery.jqplot('chart3',";
                    javascript += "[[['Web Order', " + webpurchase + "],['Iphone Order', " + AppPurchase + "], ['Mobile Order', " + MobPurchase + "], ['Android Order', " + AndroidPurchase + "]]], ";
                    javascript += "{";
                    javascript += " title: ' ',";
                    javascript += " seriesDefaults: {";
                    javascript += "shadow: false,";
                    javascript += " renderer: jQuery.jqplot.PieRenderer,";
                    javascript += "rendererOptions: {";
                    javascript += " sliceMargin: 4,";
                    javascript += " showDataLabels: true";
                    javascript += "}},";
                    javascript += " legend:{ ";
                    javascript += "  show: true, location: 'e' ";
                    javascript += "}} );";
                    javascript += "});</script>";
                    ltlchart.Text += javascript;
                }




            }
            else
            {
                string javascript = "";
                javascript = "<script>$(document).ready(function(){";
                javascript += "jQuery.jqplot('chart3',";
                javascript += "[[['No Order', " + 100 + "]]], ";
                javascript += "{";
                javascript += " title: ' ',";
                javascript += " seriesDefaults: {";
                javascript += "shadow: false,";
                javascript += " renderer: jQuery.jqplot.PieRenderer,";
                javascript += "rendererOptions: {";
                javascript += " sliceMargin: 4,";
                javascript += " showDataLabels: true";
                javascript += "}},";
                javascript += " legend:{ ";
                javascript += "  show: true, location: 'e' ";
                javascript += "}} );";
                javascript += "});</script>";
               ltlchart.Text += javascript;

            }

            if (dtorderfrom != null && dtorderfrom.Rows.Count > 0)
            {
                Male = Convert.ToInt32(dtorderfrom.Rows[0]["Male"]);
                Female = Convert.ToInt32(dtorderfrom.Rows[0]["Female"]);
                if (Male.ToString().Trim() == "0" && Female.ToString().Trim() == "0")
                {
                    string javascript2 = "";
                    javascript2 = "<script>$(document).ready(function(){";
                    javascript2 += "jQuery.jqplot('chart4',";
                    javascript2 += "[[['No Order', " + 100 + "]]], ";
                    javascript2 += "{";
                    javascript2 += " title: ' ',";
                    javascript2 += " seriesDefaults: {";
                    javascript2 += "shadow: false,";
                    javascript2 += " renderer: jQuery.jqplot.PieRenderer,";
                    javascript2 += "rendererOptions: {";
                    javascript2 += " sliceMargin: 4,";
                    javascript2 += " showDataLabels: true";
                    javascript2 += "}},";
                    javascript2 += " legend:{ ";
                    javascript2 += "  show: true, location: 'e' ";
                    javascript2 += "}} );";
                    javascript2 += "});</script>";
                    ltlchart.Text += javascript2;

                }

                else
                {
                    string javascript2 = "";
                    javascript2 = "<script>$(document).ready(function(){";
                    javascript2 += "jQuery.jqplot('chart4',";
                    javascript2 += "[[['Male Ordres', " + Male + "],['Female Orders', " + Female + "]]], ";
                    javascript2 += "{";
                    javascript2 += " title: ' ',";
                    javascript2 += " seriesDefaults: {";
                    javascript2 += "shadow: false,";
                    javascript2 += " renderer: jQuery.jqplot.PieRenderer,";
                    javascript2 += "rendererOptions: {";
                    javascript2 += " sliceMargin: 4,";
                    javascript2 += " showDataLabels: true";
                    javascript2 += "}},";
                    javascript2 += " legend:{ ";
                    javascript2 += "  show: true, location: 'e' ";
                    javascript2 += "}} );";
                    javascript2 += "});</script>";
                    ltlchart.Text += javascript2;
                }
            }
            else
            {
                string javascript2 = "";
                javascript2 = "<script>$(document).ready(function(){";
                javascript2 += "jQuery.jqplot('chart4',";
                javascript2 += "[[['No Order', " + 100 + "]]], ";
                javascript2 += "{";
                javascript2 += " title: ' ',";
                javascript2 += " seriesDefaults: {";
                javascript2 += "shadow: false,";
                javascript2 += " renderer: jQuery.jqplot.PieRenderer,";
                javascript2 += "rendererOptions: {";
                javascript2 += " sliceMargin: 4,";
                javascript2 += " showDataLabels: true";
                javascript2 += "}},";
                javascript2 += " legend:{ ";
                javascript2 += "  show: true, location: 'e' ";
                javascript2 += "}} );";
                javascript2 += "});</script>";
               ltlchart.Text += javascript2;
            }

            if (dtRedeemed != null && dtRedeemed.Rows.Count > 0)
            {
                Redeemed = Convert.ToString(dtRedeemed.Rows[0]["UsedCount"]);
                Unredeemed = Convert.ToString(dtRedeemed.Rows[0]["UnUsedCount"]);
                SuccessfulOrder = dtRedeemed.Rows[0]["Successful Ordered"].ToString().Trim();
                if (Redeemed.ToString().Trim() == "0" && TotalOrder.ToString().Trim() == "0")
                {
                    PerRedeemed = "0";
                }
                else
                {
                    float d = float.Parse(Redeemed.ToString()) / float.Parse(TotalOrder.ToString());
                    PerRedeemed = Convert.ToInt32(d * 100).ToString();
                }
                string javascript = "";
                javascript += "<script type='text/javascript'>";
                javascript += "$(document).ready(function() {";
                javascript += "$('#progressbar').progressbar({";
                javascript += "value: " + PerRedeemed + "";
                javascript += "});";
                javascript += "});";
                javascript += " </script>";
                ltlprogree.Text = javascript;

            }
            else
            {
                PerRedeemed = "0";
                string javascript = "";
                javascript += "<script type='text/javascript'>";
                javascript += "$(document).ready(function() {";
                javascript += "$('#progressbar').progressbar({";
                javascript += "value: " + 0 + "";
                javascript += "});";
                javascript += "});";
                javascript += " </script>";
                ltlprogree.Text = javascript;


            }

            if (dtdate != null && dtdate.Rows.Count > 0)
            {
                string month1 = "";
                string dmonth2 = "";
                string dmonth3 = "";
                string dmonth4 = "";
                string dmonth5 = "";
                string dmonth6 = "";
                string dmonth7 = "";
                string dmonth8 = "";
                string dmonth9 = "";
                string dmonth10 = "";
                string dmonth11 = "";
                string dmonth12 = "";
                string dmonth13 = "";
                string dmonth14 = "";
                string dmonth15 = "";
                string dmonth16 = "";
                string dmonth17 = "";
                string dmonth18 = "";
                string dmonth19 = "";
                string dmonth20 = "";
                string dmonth21 = "";
                string dmonth22 = "";
                string dmonth23 = "";
                string dmonth24 = "";
               
                month1 = Convert.ToString(dtdate.Rows[0]["1stHourOrders"]);
                dmonth2 = Convert.ToString(dtdate.Rows[0]["2ndHourOrders"]);
                dmonth3 = Convert.ToString(dtdate.Rows[0]["3rdHourOrders"]);
                dmonth4 = Convert.ToString(dtdate.Rows[0]["4thHourOrders"]);
                dmonth5 = Convert.ToString(dtdate.Rows[0]["5thHourOrders"]);
                dmonth6 = Convert.ToString(dtdate.Rows[0]["6thHourOrders"]);
                dmonth7 = Convert.ToString(dtdate.Rows[0]["7thHourOrders"]);
                dmonth8 = Convert.ToString(dtdate.Rows[0]["8thHourOrders"]);
                dmonth9 = Convert.ToString(dtdate.Rows[0]["9thHourOrders"]);
                dmonth10 = Convert.ToString(dtdate.Rows[0]["10thHourOrders"]);
                dmonth11 = Convert.ToString(dtdate.Rows[0]["11thHourOrders"]);
                dmonth12 = Convert.ToString(dtdate.Rows[0]["12thHourOrders"]);
                dmonth13 = Convert.ToString(dtdate.Rows[0]["13thHourOrders"]);
                dmonth14 = Convert.ToString(dtdate.Rows[0]["14thHourOrders"]);
                dmonth15 = Convert.ToString(dtdate.Rows[0]["15thHourOrders"]);
                dmonth16 = Convert.ToString(dtdate.Rows[0]["16thHourOrders"]);
                dmonth17 = Convert.ToString(dtdate.Rows[0]["17thHourOrders"]);
                dmonth18 = Convert.ToString(dtdate.Rows[0]["18thHourOrders"]);
                dmonth19 = Convert.ToString(dtdate.Rows[0]["19thHourOrders"]);
                dmonth20 = Convert.ToString(dtdate.Rows[0]["20thHourOrders"]);
                dmonth21 = Convert.ToString(dtdate.Rows[0]["21thHourOrders"]);
                dmonth22 = Convert.ToString(dtdate.Rows[0]["22thHourOrders"]);
                dmonth23 = Convert.ToString(dtdate.Rows[0]["23thHourOrders"]);
                dmonth24 = Convert.ToString(dtdate.Rows[0]["24thHourOrders"]);

                string javascript = "";
                javascript += "<script type='text/javascript'>";
                javascript += "$(function () {";
                javascript += "var chart;";
                javascript += "$(document).ready(function () {";
                javascript += "chart = new Highcharts.Chart({";
                javascript += "chart: {";
                javascript += "renderTo: 'container',";
                javascript += "type: 'line',";
                javascript += "marginRight: 130,";
                javascript += "marginBottom: 25";
                javascript += "},";
                javascript += "title: {";
                javascript += "text: 'When they Buy Deals',";
                javascript += "x: 0";
                javascript += "},";
                javascript += "xAxis: {";
                javascript += "categories: ['1', '2', '3', '4', '5', '6',";
                javascript += "'7', '8', '9', '10', '11', '12', '13',";
                javascript += "'14', '15', '16', '17', '18', '19', '20',";
                javascript += "'21', '22', '23', '24']";
                javascript += "},";
                javascript += "yAxis: {";
                javascript += "title: {";
                javascript += "text: 'Orders'";
                javascript += "},";
                javascript += "plotLines: [{";
                javascript += "value: 0,";
                javascript += "width: 1,";
                javascript += "color: '#808080'";
                javascript += "}] },";
                javascript += "tooltip: {";
                javascript += "formatter: function () {";
                javascript += "return '<b>' + this.series.name + '</b><br/>' +";
                javascript += "this.x + ': ' + this.y;";
                javascript += "}},";
                javascript += "legend: {";
                javascript += "layout: 'vertical',";
                javascript += "align: 'right',";
                javascript += "verticalAlign: 'top',";
                javascript += "x: 0,";
                javascript += "y: 100,";
                javascript += "borderWidth: 0";
                javascript += "},";
                javascript += "series: [{";
                javascript += "name: 'Time:Orders',";
                javascript += "data: [" + month1 + ", " + dmonth2 + ", " + dmonth3 + ", " + dmonth4 + ", " + dmonth5 + ", " + dmonth6 + ", " + dmonth7 + ", " + dmonth8 + ", " + dmonth9 + ", " + dmonth10 + ", " + dmonth11 + ", " + dmonth12 + "," + dmonth13 + "," + dmonth14 + "," + dmonth15 + "," + dmonth16 + "," + dmonth17 + "," + dmonth18 + "," + dmonth19 + "," + dmonth20 + "," + dmonth21 + "," + dmonth22 + "," + dmonth23 + "," + dmonth24 + "]";
                javascript += "},]";
                javascript += "});";
                javascript += "});";
                javascript += "});";
                javascript += "</script>";
                ltlgraph.Text = javascript;
            }
            else
            {
                string month1 = "0";
                string dmonth2 = "0";
                string dmonth3 = "0";
                string dmonth4 = "0";
                string dmonth5 = "0";
                string dmonth6 = "0";
                string dmonth7 = "0";
                string dmonth8 = "0";
                string dmonth9 = "0";
                string dmonth10 = "0";
                string dmonth11 = "0";
                string dmonth12 = "0";
                string dmonth13 = "0";
                string dmonth14 = "0";
                string dmonth15 = "0";
                string dmonth16 = "0";
                string dmonth17 = "0";
                string dmonth18 = "0";
                string dmonth19 = "0";
                string dmonth20 = "0";
                string dmonth21 = "0";
                string dmonth22 = "0";
                string dmonth23 = "0";
                string dmonth24 = "0";

                string javascript = "";
                javascript += "<script type='text/javascript'>";
                javascript += "$(function () {";
                javascript += "var chart;";
                javascript += "$(document).ready(function () {";
                javascript += "chart = new Highcharts.Chart({";
                javascript += "chart: {";
                javascript += "renderTo: 'container',";
                javascript += "type: 'line',";
                javascript += "marginRight: 130,";
                javascript += "marginBottom: 25";
                javascript += "},";
                javascript += "title: {";
                javascript += "text: 'When they Buy Deals',";
                javascript += "x: 0";
                javascript += "},";
                javascript += "xAxis: {";
                javascript += "categories: ['1', '2', '3', '4', '5', '6',";
                javascript += "'7', '8', '9', '10', '11', '12', '13',";
                javascript += "'14', '15', '16', '17', '18', '19', '20',";
                javascript += "'21', '22', '23', '24']";
                javascript += "},";
                javascript += "yAxis: {";
                javascript += "title: {";
                javascript += "text: 'Orders'";
                javascript += "},";
                javascript += "plotLines: [{";
                javascript += "value: 0,";
                javascript += "width: 1,";
                javascript += "color: '#808080'";
                javascript += "}] },";
                javascript += "tooltip: {";
                javascript += "formatter: function () {";
                javascript += "return '<b>' + this.series.name + '</b><br/>' +";
                javascript += "this.x + ': ' + this.y;";
                javascript += "}},";
                javascript += "legend: {";
                javascript += "layout: 'vertical',";
                javascript += "align: 'right',";
                javascript += "verticalAlign: 'top',";
                javascript += "x: 0,";
                javascript += "y: 100,";
                javascript += "borderWidth: 0";
                javascript += "},";
                javascript += "series: [{";
                javascript += "name: 'Time:Orders',";
                javascript += "data: [" + month1 + ", " + dmonth2 + ", " + dmonth3 + ", " + dmonth4 + ", " + dmonth5 + ", " + dmonth6 + ", " + dmonth7 + ", " + dmonth8 + ", " + dmonth9 + ", " + dmonth10 + ", " + dmonth11 + ", " + dmonth12 + "," + dmonth13 + "," + dmonth14 + "," + dmonth15 + "," + dmonth16 + "," + dmonth17 + "," + dmonth18 + "," + dmonth19 + "," + dmonth20 + "," + dmonth21 + "," + dmonth22 + "," + dmonth23 + "," + dmonth24 + "]";
                javascript += "},]";
                javascript += "});";
                javascript += "});";
                javascript += "});";
                javascript += "</script>";
                ltlgraph.Text = javascript;


            }


            if (Request.QueryString["sidedealId"] != null && Request.QueryString["sidedealId"].ToString().Trim() != "")
            {
                DataTable dtIpaddress = null;

                string strQuery = "";
                strQuery = "SELECT ";
                strQuery += " (orderIPAddress + '|' + cast(count(*) as nvarchar(50))) as IpadreeswithTotalOrder from dealorders ";
                strQuery += " where ";
                strQuery += " dealid = " + Request.QueryString["sidedealId"].Trim();
                strQuery += " group by orderIPAddress ";
                dtIpaddress = Misc.search(strQuery);
                if (dtIpaddress != null && dtIpaddress.Rows.Count > 0)
                {
                    string Script = "";
                    Script += "<script type='text/javascript'>";
                    Script += "$(document).ready(function() {";
                    Script += "IPMapper.initializeMap('ctl00_ContentPlaceHolder1_MapArea');";

                    for (int i = 0; i < dtIpaddress.Rows.Count; i++)
                    {
                        string ipAddress = "";
                        ipAddress += Convert.ToString(dtIpaddress.Rows[i]["IpadreeswithTotalOrder"]);
                        ipAddress += "|" + dtIpaddress.Rows.Count;
                        Script += "IPMapper.addIPMarker(\"" + ipAddress + "\");";
                    }
                    Script += "});";
                    Script += "</script>";

                    ltIPScript.Text = Script;
                }
                else
                {
                    mapareaheading.Visible = false;
                    MapArea.Visible = false;

                }

            }

           
        }
       

    }
    protected void btnRedeemed_Click(object sender, EventArgs e)
    {
        Response.Redirect("redemptions.aspx?dealid=" + Request.QueryString["sidedealId"]);
        Response.End();
    }

    private DataTable SearchhDealInfoByDifferentParamsForDownload(string strDealID)
    {
        DataTable dtOrderDetailInfo = null;

        string strQuery = "";

        try
        {
            strQuery = "SELECT";
            //strQuery += " ROW_NUMBER() OVER (ORDER BY [dealOrders].dOrderID) AS 'RowNumber'";
            strQuery += " [dealOrders].[dOrderID]";
            strQuery += " ,dealOrders.dealId";
            strQuery += " ,dealOrders.createdDate";
            strQuery += " ,rtrim(userCCInfo.ccInfoDFirstName) +' ' + rtrim(userCCInfo.ccInfoDLastName) as 'Name'";
            strQuery += " ,rtrim(userInfo.firstname) +' ' + rtrim(userInfo.lastName) as 'Name2'";
            strQuery += " ,rtrim(userShippingInfo.Name) as 'Name3'";
            strQuery += " ,shippingAndTax";
            strQuery += " ,[dealOrders].[status]";
            strQuery += " ,restaurantId";
            strQuery += " ,(userShippingInfo.Address+', '+userShippingInfo.City+', '+userShippingInfo.State+', '+userShippingInfo.ZipCode) as 'Address'";
            strQuery += " ,(userCCInfo.ccInfoBAddress+', '+userCCInfo.ccInfoBCity+', '+userCCInfo.ccInfoBProvince+', '+userCCInfo.ccInfoBPostalCode) as 'Address2',userinfo.phoneNo";
            strQuery += " ,userShippingInfo.Telephone";
            strQuery += ",userShippingInfo.shippingNote";
            strQuery += " ,[dealOrderDetail].[voucherSecurityCode]";
            strQuery += " ,[dealOrderDetail].[detailID]";
            strQuery += " ,[dealOrderDetail].[isRedeemed]";
            strQuery += " ,[dealOrderDetail].[redeemedDate]";
            strQuery += " ,[dealOrderDetail].[dealOrderCode]";
            strQuery += " ,[dealOrderDetail].[markUsed]";
            strQuery += " FROM ";
            strQuery += " [dealOrders]";
            strQuery += " inner join deals on (deals.dealId = dealOrders.dealId)";
            strQuery += " left outer join userShippingInfo on (userShippingInfo.shippingInfoId = dealOrders.shippingInfoId)";
            strQuery += " inner join userCCInfo on (userCCInfo.ccInfoID = dealOrders.ccInfoID)";
            strQuery += " inner join userInfo on (userInfo.userId = dealOrders.userId) ";
            strQuery += " inner join dealOrderDetail on dealOrderDetail.[dOrderID] = [dealOrders].[dOrderID]";
            strQuery += " where dealOrders.dealId =" + strDealID + "";
            strQuery += " Order by dOrderID asc";
            dtOrderDetailInfo = Misc.search(strQuery);
        }
        catch (Exception ex)
        {
            //lblMessage.Text = ex.ToString();
            //lblMessage.Visible = true;
            //imgGridMessage.Visible = true;
            //imgGridMessage.ImageUrl = "images/error.png";
            //lblMessage.ForeColor = System.Drawing.Color.Red;
        }

        return dtOrderDetailInfo;
    }

    private void ExportToUser(DataTable table, string strFileName)
    {
        GridView gv = new GridView();
        gv.DataSource = table;
        gv.DataBind();
        string attachment = "attachment; filename=" + strFileName;
        Response.ClearContent();
        Response.AddHeader("content-disposition", attachment);
        Response.ContentType = "application/ms-excel";
        StringWriter stw = new StringWriter();
        HtmlTextWriter htextw = new HtmlTextWriter(stw);
        gv.RenderControl(htextw);
        Response.Write(stw.ToString());
        Response.End();
    }
    protected void Download_Click(object sender, EventArgs e)
    {
        string test = "";
        //test = listboxdownloadoptions.SelectedValue.ToString();
        test = drpdownlistfordownload.SelectedValue.ToString();
        if (test == "xls")
        {
            try
            {
                DataTable dtUser = SearchhDealInfoByDifferentParamsForDownload((Request.QueryString["sidedealId"]).ToString());
                DataTable dtDealOrders = new DataTable("dealOrders");
                if (dtUser != null && dtUser.Rows.Count > 0
                    && dtUser.Rows[0]["shippingAndTax"].ToString().Trim() != ""
                    && Convert.ToBoolean(dtUser.Rows[0]["shippingAndTax"].ToString()))
                {
                    DataColumn Sr = new DataColumn("Sr.");
                    DataColumn Date = new DataColumn("Date");
                    DataColumn Customer = new DataColumn("Customer");
                    DataColumn dealOrderCode = new DataColumn("Voucher Code");
                    DataColumn voucherSecurityCode = new DataColumn("Voucher Security Code");
                    DataColumn Status = new DataColumn("Status");
                    DataColumn Telephone = new DataColumn("Telephone");
                    DataColumn Address = new DataColumn("Address");
                    DataColumn Note = new DataColumn("Note");
                    dtDealOrders.Columns.Add(Sr);
                    dtDealOrders.Columns.Add(Date);
                    dtDealOrders.Columns.Add(Customer);
                    dtDealOrders.Columns.Add(dealOrderCode);
                    dtDealOrders.Columns.Add(voucherSecurityCode);
                    dtDealOrders.Columns.Add(Status);
                    dtDealOrders.Columns.Add(Telephone);
                    dtDealOrders.Columns.Add(Address);
                    dtDealOrders.Columns.Add(Note);
                    DataRow dRow;
                    GECEncryption objEnc = new GECEncryption();
                    if (dtUser != null && dtUser.Rows.Count > 0)
                    {

                        for (int i = 0; i < dtUser.Rows.Count; i++)
                        {
                            dRow = dtDealOrders.NewRow();
                            dRow["Sr."] = i + 1;
                            try
                            {
                                dRow["Date"] = dtUser.Rows[i]["createdDate"].ToString().Trim() == "" ? "" : Convert.ToDateTime(dtUser.Rows[i]["createdDate"].ToString().Trim()).ToString("MM/dd/yyyy HH:mm");
                            }
                            catch (Exception ex)
                            {
                                dRow["Date"] = "";
                            }
                            dRow["Customer"] = dtUser.Rows[i]["Name3"].ToString().Trim();
                            dRow["Voucher Code"] = objEnc.DecryptData("deatailOrder", dtUser.Rows[i]["dealOrderCode"].ToString().Trim());
                            dRow["Voucher Security Code"] = dtUser.Rows[i]["voucherSecurityCode"].ToString().Trim();
                            dRow["Status"] = dtUser.Rows[i]["status"].ToString().Trim();
                            dRow["Telephone"] = dtUser.Rows[i]["Telephone"].ToString().Trim();
                            dRow["Address"] = dtUser.Rows[i]["Address"].ToString().Trim();
                            dRow["Note"] = dtUser.Rows[i]["shippingNote"].ToString().Trim();
                            dtDealOrders.Rows.Add(dRow);
                        }
                        ExportToUser(dtDealOrders, "DealOrders.xls");
                    }
                }
                else
                {
                    BLLRestaurantGoogleAddresses objGoogle = new BLLRestaurantGoogleAddresses();
                    objGoogle.restaurantId = Convert.ToInt64(dtUser.Rows[0]["restaurantId"].ToString().Trim());
                    DataTable dtAddress = objGoogle.getAllRestaurantGoogleAddressesByRestaurantID();
                    if (dtAddress != null && dtAddress.Rows.Count > 0
                        && dtAddress.Rows[0]["restaurantGoogleAddress"] != null && dtAddress.Rows[0]["restaurantGoogleAddress"].ToString().Trim().ToLower() == "online")
                    {
                        DataColumn Sr = new DataColumn("Sr.");
                        DataColumn Date = new DataColumn("Date");
                        DataColumn Customer = new DataColumn("Customer");
                        DataColumn dealOrderCode = new DataColumn("Voucher Code");
                        DataColumn voucherSecurityCode = new DataColumn("Voucher Security Code");
                        DataColumn Status = new DataColumn("Status");
                        DataColumn Telephone = new DataColumn("Telephone");
                        DataColumn Address = new DataColumn("Address");
                        dtDealOrders.Columns.Add(Sr);
                        dtDealOrders.Columns.Add(Date);
                        dtDealOrders.Columns.Add(Customer);
                        dtDealOrders.Columns.Add(dealOrderCode);
                        dtDealOrders.Columns.Add(voucherSecurityCode);
                        dtDealOrders.Columns.Add(Status);
                        dtDealOrders.Columns.Add(Telephone);
                        dtDealOrders.Columns.Add(Address);
                        DataRow dRow;
                        GECEncryption objEnc = new GECEncryption();
                        if (dtUser != null && dtUser.Rows.Count > 0)
                        {

                            for (int i = 0; i < dtUser.Rows.Count; i++)
                            {
                                dRow = dtDealOrders.NewRow();
                                dRow["Sr."] = i + 1;
                                try
                                {
                                    dRow["Date"] = dtUser.Rows[i]["createdDate"].ToString().Trim() == "" ? "" : Convert.ToDateTime(dtUser.Rows[i]["createdDate"].ToString().Trim()).ToString("MM/dd/yyyy HH:mm");
                                }
                                catch (Exception ex)
                                {
                                    dRow["Date"] = "";
                                }
                                dRow["Customer"] = dtUser.Rows[i]["Name"].ToString().Trim();
                                dRow["Voucher Code"] = objEnc.DecryptData("deatailOrder", dtUser.Rows[i]["dealOrderCode"].ToString().Trim());
                                dRow["Voucher Security Code"] = dtUser.Rows[i]["voucherSecurityCode"].ToString().Trim();
                                dRow["Status"] = dtUser.Rows[i]["status"].ToString().Trim();
                                dRow["Telephone"] = dtUser.Rows[i]["phoneNo"].ToString().Trim();
                                dRow["Address"] = dtUser.Rows[i]["Address2"].ToString().Trim();
                                dtDealOrders.Rows.Add(dRow);
                            }
                            ExportToUser(dtDealOrders, "DealOrders.xls");
                        }
                    }
                    else
                    {

                        DataColumn Sr = new DataColumn("Sr.");
                        DataColumn Date = new DataColumn("Date");
                        DataColumn Customer = new DataColumn("Customer");
                        DataColumn dealOrderCode = new DataColumn("Voucher Code");
                        DataColumn voucherSecurityCode = new DataColumn("Voucher Security Code");
                        DataColumn Status = new DataColumn("Status");
                        dtDealOrders.Columns.Add(Sr);
                        dtDealOrders.Columns.Add(Date);
                        dtDealOrders.Columns.Add(Customer);
                        dtDealOrders.Columns.Add(dealOrderCode);
                        dtDealOrders.Columns.Add(voucherSecurityCode);
                        dtDealOrders.Columns.Add(Status);
                        DataRow dRow;
                        GECEncryption objEnc = new GECEncryption();
                        if (dtUser != null && dtUser.Rows.Count > 0)
                        {

                            for (int i = 0; i < dtUser.Rows.Count; i++)
                            {
                                dRow = dtDealOrders.NewRow();
                                dRow["Sr."] = i + 1;
                                try
                                {
                                    dRow["Date"] = dtUser.Rows[i]["createdDate"].ToString().Trim() == "" ? "" : Convert.ToDateTime(dtUser.Rows[i]["createdDate"].ToString().Trim()).ToString("MM/dd/yyyy HH:mm");
                                }
                                catch (Exception ex)
                                {
                                    dRow["Date"] = "";
                                }
                                dRow["Customer"] = dtUser.Rows[i]["Name2"].ToString().Trim();
                                dRow["Voucher Code"] = objEnc.DecryptData("deatailOrder", dtUser.Rows[i]["dealOrderCode"].ToString().Trim());
                                dRow["Voucher Security Code"] = dtUser.Rows[i]["voucherSecurityCode"].ToString().Trim();
                                dRow["Status"] = dtUser.Rows[i]["status"].ToString().Trim();
                                dtDealOrders.Rows.Add(dRow);
                            }
                            ExportToUser(dtDealOrders, "DealOrders.xls");
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }

        }

        else
        {
            try
            {
                string url = ConfigurationManager.AppSettings["YourSite"] + "/tastypdfdownload.aspx?pdf=TRUE&did=" + Request.QueryString["sidedealId"].ToString().Trim();
                Process PDFProcess = new Process();
                string FileName = @" " + AppDomain.CurrentDomain.BaseDirectory + "Images\\ClientData\\invoice.pdf";
                PDFProcess.StartInfo.UseShellExecute = false;
                PDFProcess.StartInfo.CreateNoWindow = true;
                PDFProcess.StartInfo.FileName = AppDomain.CurrentDomain.BaseDirectory + @"bin\wkhtmltopdf.exe";
                PDFProcess.StartInfo.Arguments = url + FileName;
                PDFProcess.StartInfo.RedirectStandardOutput = true;
                PDFProcess.StartInfo.RedirectStandardError = true;
                PDFProcess.Start();
                PDFProcess.WaitForExit();
            }
            catch (Exception ex)
            {
                string xx = ex.Message.ToString();
                Response.Write("<br>" + xx);
            }

            string FilePath = AppDomain.CurrentDomain.BaseDirectory + "Images\\ClientData\\invoice.pdf";
            try
            {
                string contentType = "";
                contentType = "Application/pdf";
                Response.ContentType = contentType;
                Response.AppendHeader("content-disposition", "attachment; filename=" + (new FileInfo("invoice.pdf")).Name);
                Response.WriteFile(FilePath);
                Response.End();
            }
            catch
            {
                //To Do
            }
        
        }
    }
}
