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
using System.Text;
using System.IO;
using System.Net;
using System.Threading;
using System.Text.RegularExpressions;
using System.Xml;

public partial class dealHSTManagement : System.Web.UI.Page
{
    public string strIDs = "";
    public int start = 2;
    BLLDealOrders objOrders = new BLLDealOrders();

    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);

        if (!IsPostBack)
        {
            //Get the Admin User Session here
            bindYear();
            if (Session["user"] != null)
            {

                try
                {
                    BindGrid();
                }
                catch (Exception ex)
                {

                }
            }
            else
            {
                Response.Redirect(ResolveUrl("~/admin/default.aspx"), false);
            }
        }
    }

    protected void BindGrid()
    {
        try
        {
            DateTime dtStartTime = new DateTime(Convert.ToInt32(ddlYear.SelectedValue.ToString()), Convert.ToInt32(ddlMonth.SelectedValue.ToString()), 1,0,0,0);
            DateTime dtEndTime = new DateTime(Convert.ToInt32(ddlYear.SelectedValue), Convert.ToInt32(ddlMonth.SelectedValue), 1,23,59,59).AddMonths(1).AddDays(-1);
            DataTable dtHSTTop = Misc.getHSTForGivenDates(dtStartTime, dtEndTime);

            if (dtHSTTop != null && dtHSTTop.Rows.Count > 0)
            {
                GECEncryption objEnc = new GECEncryption();

                ArrayList array = new ArrayList();

                DataTable dtHST = new DataTable("dtHST");
                DataColumn dealID = new DataColumn("dealID");
                DataColumn Date = new DataColumn("Date");
                DataColumn Title = new DataColumn("Title");
                DataColumn Type = new DataColumn("Type");
                DataColumn Amount = new DataColumn("Amount");
                
                dtHST.Columns.Add(dealID);
                dtHST.Columns.Add(Date);
                dtHST.Columns.Add(Title);
                dtHST.Columns.Add(Type);
                dtHST.Columns.Add(Amount);
                
                DataRow dRow;
                double dTotal = 0;
                for (int i = 0; i < dtHSTTop.Rows.Count; i++)
                {
                    if (!array.Contains(dtHSTTop.Rows[i]["dealId"].ToString().Trim()))
                    {
                        array.Add(dtHSTTop.Rows[i]["dealId"].ToString().Trim());
                        dRow = dtHST.NewRow();
                        dRow["dealID"] = dtHSTTop.Rows[i]["dealId"].ToString().Trim();
                        dRow["Date"] = Convert.ToDateTime(dtHSTTop.Rows[i]["dealEndTimeC"].ToString().Trim()).ToString("MM/dd/yyyy");
                        dRow["Title"] = dtHSTTop.Rows[i]["title"].ToString().Trim();
                        dRow["Type"] = "Sale";
                        dRow["Amount"] = "$"+ (Convert.ToDouble(dtHSTTop.Rows[i]["HST"].ToString().Trim())==0?"0":Convert.ToDouble(dtHSTTop.Rows[i]["HST"].ToString().Trim()).ToString("###.00"));
                        dTotal = dTotal + Convert.ToDouble(dtHSTTop.Rows[i]["HST"].ToString().Trim());
                        dtHST.Rows.Add(dRow);
                        DataTable dtRefundedOrders = Misc.search("select dealOrderCode,createdDate, modifiedDate, qty from dealorders inner join dealOrderDetail on dealOrderDetail.dOrderID = dealOrders.dOrderID where status<>'Successful' and dealid=" + dtHSTTop.Rows[i]["dealId"].ToString().Trim() + " order by modifiedDate asc");
                        if (dtRefundedOrders != null && dtRefundedOrders.Rows.Count > 0)
                        {
                            for (int a = 0; a < dtRefundedOrders.Rows.Count; a++)
                            {
                                double tempRefunded = Math.Round((Convert.ToDouble(dtHSTTop.Rows[i]["sellingPrice"].ToString().Trim()) * Convert.ToDouble(dtRefundedOrders.Rows[a]["qty"].ToString().Trim())), 2, MidpointRounding.AwayFromZero);
                                double tempAdveriseFee = Math.Round((Convert.ToDouble(dtHSTTop.Rows[i]["OurCommission"].ToString().Trim()) / 100) * tempRefunded, 2, MidpointRounding.AwayFromZero);                                
                                double tempTax = Math.Round((12.00 / 100) * tempAdveriseFee, 2, MidpointRounding.AwayFromZero);
                                dRow = dtHST.NewRow();
                                dRow["dealID"] = dtHSTTop.Rows[i]["dealId"].ToString().Trim();
                                dRow["Date"] = Convert.ToDateTime(dtRefundedOrders.Rows[a]["modifiedDate"].ToString().Trim() != "" ? dtRefundedOrders.Rows[a]["modifiedDate"].ToString().Trim() : dtRefundedOrders.Rows[a]["createdDate"].ToString().Trim()).ToString("MM/dd/yyyy");
                                dRow["Title"] = "Refund # " + objEnc.DecryptData("deatailOrder", dtRefundedOrders.Rows[a]["dealOrderCode"].ToString());
                                dRow["Type"] = "Refund";
                                dRow["Amount"] = "-$" + (tempTax == 0 ?"0":tempTax.ToString());
                                dTotal = dTotal - tempTax;
                                dtHST.Rows.Add(dRow);
                            }
                        }
                    }
                }
                dRow = dtHST.NewRow();
                dRow["dealID"] = "0";
                dRow["Date"] = DateTime.Now.ToString("MM/dd/yyyy");
                dRow["Title"] = "Total";
                dRow["Type"] = "";
                dRow["Amount"] ="$"+ dTotal.ToString("###.00");                
                dtHST.Rows.Add(dRow);
                ViewState["dtHST"] = dtHST;
                gvViewDeals.DataSource = dtHST.DefaultView;
                gvViewDeals.DataBind();
            }
            else
            {
                gvViewDeals.DataSource = null;
                gvViewDeals.DataBind();
            }            
        }
        catch (Exception ex)
        { }
    }


    protected void bindYear()
    {
        try
        {
            try
            {
                for (int year = 2008; year <= DateTime.Now.Year; year++)
                {
                    ddlYear.Items.Add(new ListItem(year.ToString(), year.ToString()));
                }
                ddlYear.SelectedValue = DateTime.Now.Year.ToString();
                ddlMonth.SelectedValue = DateTime.Now.Month.ToString();
            }
            catch (Exception ex)
            {
                string strException = "There is an error occur, please email us at support@tazzling.com or call 1855-295-1771.";
            }
        }
        catch (Exception ex)
        {
        }
    }

    protected void btnSearch_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            lblMessage.Visible = false;
            imgGridMessage.Visible = false;

            BindGrid();
        }
        catch (Exception ex)
        { }
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
    }

    protected void imgbtnExportToExcel_Click(object sender, ImageClickEventArgs e)
    {      
        if (ViewState["dtHST"] != null)
        {
            DataTable dtHST = (DataTable)ViewState["dtHST"];
            GridView gv = new GridView();
            gv.DataSource = dtHST;
            gv.DataBind();
            string attachment = "attachment; filename=HST.xls";
            Response.ClearContent();
            Response.AddHeader("content-disposition", attachment);
            Response.ContentType = "application/ms-excel";
            StringWriter stw = new StringWriter();
            HtmlTextWriter htextw = new HtmlTextWriter(stw);
            gv.RenderControl(htextw);
            Response.Write(stw.ToString());
            Response.End();
        }
    }
  
}