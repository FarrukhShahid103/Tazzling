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

public partial class Redemptions : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["restaurant"] == null)
        {
            Response.Redirect("Default.aspx", false);
        }
        if (!IsPostBack)
        {
 
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if(txtVoucherNumber.Text.Trim() != "")
        {
            if (Session["restaurant"] != null)
            {
                if (Request.QueryString["dealid"] != null && Request.QueryString["dealid"].ToString().Trim() != "")
                {
                    DataTable dtuserinfo = (DataTable)Session["restaurant"];
                    string strdealID = Request.QueryString["dealid"].ToString().Trim();
                    GECEncryption objEnc = new GECEncryption();
                    string strQuery = "SELECT [dealOrders].[dOrderID], ";
                    strQuery += "[dealOrderDetail].[voucherSecurityCode] ,";
                    strQuery += "[dealOrderDetail].[detailID] ,[dealOrderDetail].[isRedeemed] ,[dealOrderDetail].[redeemedDate] ,";
                    strQuery += "[dealOrderDetail].[dealOrderCode] ,[dealOrderDetail].[markUsed] FROM  [dealOrders]";
                    strQuery += " inner join deals on (deals.dealId = dealOrders.dealId) ";
                    strQuery += " inner join dealOrderDetail on dealOrderDetail.[dOrderID] = [dealOrders].[dOrderID] ";
                    strQuery += " where  (dealOrders.dealId in (select dealid from deals ";
                    strQuery += " inner join restaurant on restaurant.restaurantId = deals.restaurantId  ";
                    strQuery += " where dealOrders.dealId=" + strdealID + ")) and [dealOrderDetail].[dealOrderCode]='" + objEnc.EncryptData("deatailOrder", txtVoucherNumber.Text.Trim()) + "'";

                    DataTable dtSearchResult = Misc.search(strQuery);
                    if (dtSearchResult != null && dtSearchResult.Rows.Count > 0)
                    {
                        BLLDealOrderDetail objBLLDealOrderDetail = new BLLDealOrderDetail();


                        bool check = true;
                        for (int i = 0; i < dtSearchResult.Rows.Count; i++)
                        {
                            objBLLDealOrderDetail.detailID = Convert.ToInt32(dtSearchResult.Rows[i]["detailID"]);
                            check = Convert.ToBoolean(dtSearchResult.Rows[i]["isRedeemed"]);
                            if (check == false)
                            {

                                objBLLDealOrderDetail.isRedeemed = true;
                                objBLLDealOrderDetail.updateDealOrderDetailsByDetailID();

                                string jScript;
                                jScript = "<script>";
                                jScript += "MessegeArea('Thanks the voucher successfully redeemed.' , 'success');";
                                jScript += "</script>";
                                ScriptManager.RegisterClientScriptBlock(this, typeof(Button), "Javascript", jScript, false);
                            }

                            else
                            {
                                string jScript;
                                jScript = "<script>";
                                jScript += "MessegeArea('This voucher already redeemed.' , 'error');";
                                jScript += "</script>";
                                ScriptManager.RegisterClientScriptBlock(this, typeof(Button), "Javascript", jScript, false);
                            }
                        }
                    }
                    else
                    {
                        string jScript;
                        jScript = "<script>";
                        jScript += "MessegeArea('This voucher code did not exist.' , 'error');";
                        jScript += "</script>";
                        ScriptManager.RegisterClientScriptBlock(this, typeof(Button), "Javascript", jScript, false);

                    }
                
                }
                else
                {
                DataTable dtuserinfo = (DataTable)Session["restaurant"];
                string strUserID = dtuserinfo.Rows[0]["userID"].ToString().Trim();
                GECEncryption objEnc = new GECEncryption();
                string strQuery = "SELECT [dealOrders].[dOrderID], ";
                strQuery += "[dealOrderDetail].[voucherSecurityCode] ,";
                strQuery += "[dealOrderDetail].[detailID] ,[dealOrderDetail].[isRedeemed] ,[dealOrderDetail].[redeemedDate] ,";
                strQuery += "[dealOrderDetail].[dealOrderCode] ,[dealOrderDetail].[markUsed] FROM  [dealOrders]";
                strQuery += " inner join deals on (deals.dealId = dealOrders.dealId) ";
                strQuery += " inner join dealOrderDetail on dealOrderDetail.[dOrderID] = [dealOrders].[dOrderID] ";
                strQuery += " where  (dealOrders.dealId in (select dealid from deals ";
                strQuery += " inner join restaurant on restaurant.restaurantId = deals.restaurantId  ";
                strQuery += " where restaurant.userID=" + strUserID + ")) and [dealOrderDetail].[dealOrderCode]='" + objEnc.EncryptData("deatailOrder", txtVoucherNumber.Text.Trim()) + "'";

                DataTable dtSearchResult = Misc.search(strQuery);
                if (dtSearchResult != null && dtSearchResult.Rows.Count > 0)
                {
                    BLLDealOrderDetail objBLLDealOrderDetail = new BLLDealOrderDetail();
                    
                    
                       bool check = true;
                    for(int i = 0; i < dtSearchResult.Rows.Count; i++)
                    {
                        objBLLDealOrderDetail.detailID = Convert.ToInt32(dtSearchResult.Rows[i]["detailID"]);
                       check = Convert.ToBoolean(dtSearchResult.Rows[i]["isRedeemed"]);
                    if (check == false)
                    {
                       
                        objBLLDealOrderDetail.isRedeemed = true;
                        objBLLDealOrderDetail.updateDealOrderDetailsByDetailID();
                        
                        string jScript;
                        jScript = "<script>";
                        jScript += "MessegeArea('Thanks the voucher successfully redeemed.' , 'success');";
                        jScript += "</script>";
                        ScriptManager.RegisterClientScriptBlock(this, typeof(Button), "Javascript", jScript, false);
                    }
                
                    else
                    {
                        string jScript;
                        jScript = "<script>";
                        jScript += "MessegeArea('This voucher already redeemed.' , 'error');";
                        jScript += "</script>";
                        ScriptManager.RegisterClientScriptBlock(this, typeof(Button), "Javascript", jScript, false);
                    }
                    }
                }
                else
                {
                    string jScript;
                    jScript = "<script>";
                    jScript += "MessegeArea('This voucher code did not exist.' , 'error');";
                    jScript += "</script>";
                    ScriptManager.RegisterClientScriptBlock(this, typeof(Button), "Javascript", jScript, false);

                }
            }
                //else end
        }
           
        }
        else
        {
            string jScript;
            jScript = "<script>";
            jScript += "MessegeArea('Please enter voucher code.' , 'error');";
            jScript += "</script>";
            ScriptManager.RegisterClientScriptBlock(this, typeof(Button), "Javascript", jScript, false);

        }
    }
}
