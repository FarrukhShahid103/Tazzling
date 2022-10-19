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
using System.IO;

public partial class tastypdfdownload : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        Page.Title = ConfigurationManager.AppSettings["pageTitleStart"].ToString().Trim() + " | Member | My Vouchers";      
        if (!IsPostBack)
        {
            DataTable dtUser = null;
            if (Request.QueryString["pdf"] != null && Request.QueryString["did"] != null
                && Request.QueryString["did"].ToString().Trim() != "")
            {
                try
                {
                    if (Request.QueryString["pdf"].ToString().Trim() != "TRUE")
                    {
                        if (Session["restaurant"] != null)
                        {
                            dtUser = (DataTable)Session["restaurant"];
                        }
                        else
                        {
                            Response.Redirect("default.aspx");
                        }
                        DataTable dtTemp = Misc.search("select userinfo.userid from userinfo inner join restaurant on (restaurant.userid = userInfo.userid) inner join deals on (deals.restaurantId = restaurant.restaurantId) where userinfo.userid=" + dtUser.Rows[0]["userid"].ToString().Trim() + " and dealid=" + Request.QueryString["did"].Trim());
                        if (dtTemp != null && dtTemp.Rows.Count == 0)
                        {
                            Response.Redirect(ResolveUrl("~/UserDashBoard.aspx"));
                        }
                    }
                    if (!IsPostBack)
                    {
                        SearchhDealInfoByDifferentParams(); 
                    }
                }
                catch (Exception ex)
                { }
            }
            try
            {
               
            }
            catch (Exception ex)
            {
            
            }
        }
    }

    protected string getDealCode(object objCode)
    {
        if (objCode.ToString().Contains("# "))
        {
            return objCode.ToString();
        }
        else if (objCode.ToString() != "")
        {
            GECEncryption objEnc = new GECEncryption();
            return "# " + objEnc.DecryptData("deatailOrder", objCode.ToString());
        }
        return "";
    }

    private void SearchhDealInfoByDifferentParams()
    {
        DataTable dtOrderDetailInfo = null;

        string strQuery = "";

        try
        {
            strQuery = "SELECT";
            //strQuery += " ROW_NUMBER() OVER (ORDER BY [dealOrders].dOrderID) AS 'RowNumber'";
            strQuery += " [dealOrders].[dOrderID]";
            strQuery += " ,dealOrders.dealId";
            strQuery += " ,rtrim(userInfo.firstname) +' ' + (case when (len(rtrim(userInfo.lastName))>1) then upper(substring(rtrim(userInfo.lastName),1,1)) else '' end )as 'Name'";
            strQuery += " ,[dealOrders].[status]";
            strQuery += ", deals.shippingAndTax";
            strQuery += ",userShippingInfo.Name as 'shippingName'";
            strQuery += ",userShippingInfo.Address+', '+userShippingInfo.City+', '+userShippingInfo.State+', '+userShippingInfo.ZipCode+', '+userShippingInfo.shippingCountry as 'ShippingAddress'";
            strQuery += ",userShippingInfo.Telephone";
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
            strQuery += " inner join userInfo on (userInfo.userId = dealOrders.userId) ";
            strQuery += " inner join dealOrderDetail on dealOrderDetail.[dOrderID] = [dealOrders].[dOrderID]";
            strQuery += " where ";
            strQuery += " (dealOrders.dealId =" + Request.QueryString["did"].ToString().Trim() + ")";                      
            strQuery += " Order by dOrderID asc";
            // this.pageGrid.PageIndex = 0;
            // GridView1.PageIndex = 0;

            //Get & Set the DataTable here
            dtOrderDetailInfo = Misc.search(strQuery);
            if(dtOrderDetailInfo!=null && dtOrderDetailInfo.Rows.Count>0)
            {
                  pageGrid.DataSource = dtOrderDetailInfo.DefaultView;
            pageGrid.DataBind();
            }
        }
        catch (Exception ex)
        {          
        }
       
    }
}
