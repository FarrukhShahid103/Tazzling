using System;
using System.Collections;
using System.IO;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GecLibrary;
using System.Web.UI.HtmlControls;

using System.Configuration;
using System.Web.Security;
using System.Web.UI.WebControls.WebParts;
using System.Threading;
using System.Reflection;

public partial class orders : System.Web.UI.Page
{
    BLLDealOrderDetail objBLLDealOrderDetail = new BLLDealOrderDetail();

    public bool displayPrevious = false;
    public bool displayNext = true;

    private const string ASCENDING = " ASC";
    private const string DESCENDING = " DESC";

    protected void Page_Load(object sender, EventArgs e)
    {       
        if (!IsPostBack)
        {
            try
            {
                if (Request.QueryString["did"] == null)
                {
                    Response.Redirect(ResolveUrl("~/admin/onlinedealManagement.aspx"));
                }
                GetAndSetDealAddress(Request.QueryString["did"].ToString());
                GetAllBusinessInfoAndFillGrid();
                SetSubDealsInfo();

            }
            catch (Exception ex)
            {             
            }
        }
    }

    protected void SetSubDealsInfo()
    {
        try
        {
            lblDealsDeatail.Text = "";
            BLLDeals objDeal = new BLLDeals();
            objDeal.ParentDealId = Convert.ToInt64(Request.QueryString["did"].ToString());
            DataTable dtSubDealsInfo = objDeal.getCurrentSubDealInfoByParnetDealIDForDealPage();
            if ((dtSubDealsInfo != null) && (dtSubDealsInfo.Rows.Count > 1))
            {
                for (int i = 0; i < dtSubDealsInfo.Rows.Count; i++)
                {
                    lblDealsDeatail.Text += dtSubDealsInfo.Rows[i]["title"].ToString().Trim() + " : <b>" + dtSubDealsInfo.Rows[i]["Orders"].ToString().Trim()+" Sold</b></br>";
                }
            }
            else
            {
                lblDealsHeading.Visible = false;
                lblDealsDeatail.Visible = false;                
            }
        }
        catch (Exception ex)
        { }
    }

    

    #region "Get and Set the Deal Order Info here"

    private int GetAndSetDealAddress(string dealID)
    {
        int iDealAddCount = 0;

        try
        {
            DataTable dtResAddress = null;

            BLLRestaurantAddresses objBLLRestaurantAddresses = new BLLRestaurantAddresses();

            dtResAddress = objBLLRestaurantAddresses.getAllRestaurantAddressesByDealID(dealID);

            if ((dtResAddress != null) && (dtResAddress.Rows.Count > 0))
            {
                ddlDealAddress.DataSource = dtResAddress.DefaultView;
                ddlDealAddress.DataTextField = "Address";
                ddlDealAddress.DataValueField = "raID";
                ddlDealAddress.DataBind();
                ddlDealAddress.Items.Insert(0, "All");
            }
        }
        catch (Exception ex)
        {
            string strExceoption = "There is an error occur, please email us at support@tazzling.com or call 1855-295-1771.";
        }

        return iDealAddCount;
    }

    protected void GetAllBusinessInfoAndFillGrid()
    {
        try
        {
            if (ViewState["ddlPage"] != null && ViewState["ddlPage"].ToString() != "")
            {
                pageGrid.PageSize = Convert.ToInt32(ViewState["ddlPage"]);
            }
            else
            {
                pageGrid.PageSize = Convert.ToInt32(SearchhDealInfoByDifferentParams().Rows.Count.ToString());
                ViewState["ddlPage"] = Convert.ToInt32(SearchhDealInfoByDifferentParams().Rows.Count.ToString());
                
            }


            DataTable dtUser;


            if (ViewState["Query"] == null)
            {
                dtUser = SearchhDealInfoByDifferentParams();               
            }
            else
            {
                dtUser = Misc.search(ViewState["Query"].ToString());              
            }

            if (dtUser != null && dtUser.Rows.Count > 0)
            {
                pageGrid.DataSource = dtUser.DefaultView;
                pageGrid.DataBind();
             

                Label lblPageCount = (Label)pageGrid.BottomPagerRow.FindControl("lblPageCount");
                Label lblTotalRecords = (Label)pageGrid.BottomPagerRow.FindControl("lblTotalRecords");

                lblTotalRecords.Text = dtUser.Rows.Count.ToString();
                lblPageCount.Text = pageGrid.PageCount.ToString();

                DropDownList ddlPage = bindPageDropDown();
                if (ViewState["ddlPage"] != null && ViewState["ddlPage"].ToString() != "")
                {
                    ddlPage.SelectedValue = ViewState["ddlPage"].ToString();
                }
                pageGrid.BottomPagerRow.Visible = true;

                TextBox txtPage = (TextBox)pageGrid.BottomPagerRow.Cells[0].FindControl("txtPage");
                if (pageGrid.PageIndex == 0)
                {
                    displayPrevious = false;
                }
                else
                {
                    displayPrevious = true;
                    txtPage.Text = (pageGrid.PageIndex + 1).ToString();
                }
                if (pageGrid.PageIndex == pageGrid.PageCount - 1)
                {
                    displayNext = false;
                }
                else
                {
                    displayNext = true;
                    txtPage.Text = (pageGrid.PageIndex + 1).ToString();
                }

                if (pageGrid.PageCount == 1)
                {
                    ImageButton imgPrev = (ImageButton)pageGrid.BottomPagerRow.FindControl("btnPrev");
                    ImageButton imgNext = (ImageButton)pageGrid.BottomPagerRow.FindControl("btnNext");

                    imgNext.Enabled = false;
                    imgPrev.Enabled = false;
                }

            }
            else
            {
                pageGrid.DataSource = null;
                pageGrid.DataBind();
                //GridView1.DataSource = null;
                //GridView1.DataBind();
            }
        }
        catch (Exception ex)
        {

            lblMessage.Text = ex.ToString();
            lblMessage.Visible = true;
            imgGridMessage.Visible = true;
            imgGridMessage.ImageUrl = "images/error.png";
            lblMessage.ForeColor = System.Drawing.Color.Red;
        }
    }

    private DropDownList bindPageDropDown()
    {
        try
        {
            DropDownList ddlPage = (DropDownList)pageGrid.BottomPagerRow.Cells[0].FindControl("ddlPage");
            ddlPage.Items.Insert(0, "5");
            ddlPage.Items.Insert(1, "10");
            ddlPage.Items.Insert(2, "20");
            ddlPage.Items.Insert(3, "30");
            ddlPage.Items.Insert(4, "50");
            ListItem objList = new ListItem("All", SearchhDealInfoByDifferentParams().Rows.Count.ToString());
            ddlPage.Items.Insert(5, objList);
            return ddlPage;
        }
        catch (Exception ex)
        {

            lblMessage.Text = ex.ToString();
            lblMessage.Visible = true;
            imgGridMessage.Visible = true;
            imgGridMessage.ImageUrl = "images/error.png";
            lblMessage.ForeColor = System.Drawing.Color.Red;
            return null;
        }
    }

    protected void pageGrid_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            lblMessage.Visible = false;
            imgGridMessage.Visible = false;

            if (e.NewPageIndex == 0)
            {
                displayPrevious = false;
            }
            else
            {
                displayPrevious = true;
            }
            if (e.NewPageIndex == pageGrid.PageCount - 1)
            {
                displayNext = false;
            }
            else
            {
                displayNext = true;
            }
            this.pageGrid.PageIndex = e.NewPageIndex;
            this.GetAllBusinessInfoAndFillGrid();
            TextBox txtPage = (TextBox)pageGrid.BottomPagerRow.Cells[0].FindControl("txtPage");
            txtPage.Text = (e.NewPageIndex + 1).ToString();
        }
        catch (Exception ex)
        {

            lblMessage.Text = ex.ToString();
            lblMessage.Visible = true;
            imgGridMessage.Visible = true;
            imgGridMessage.ImageUrl = "images/error.png";
            lblMessage.ForeColor = System.Drawing.Color.Red;
        }
    }

    #endregion

    #region Event to take to required page
    protected void txtPage_TextChanged(object sender, EventArgs e)
    {
        try
        {
            lblMessage.Visible = false;
            imgGridMessage.Visible = false;

            TextBox txtPage = (TextBox)pageGrid.BottomPagerRow.Cells[0].FindControl("txtPage");
            int intPageindex = 0;
            if (txtPage.Text != null && txtPage.Text.ToString() != "")
            {
                intPageindex = Convert.ToInt32(txtPage.Text);
                if (intPageindex > 0)
                {
                    intPageindex--;
                }
            }

            if (intPageindex < pageGrid.PageCount && intPageindex > 0)
            {
                pageGrid.PageIndex = intPageindex;
            }
            else
            {
                pageGrid.PageIndex = 0;
            }


            txtPage.Text = (pageGrid.PageIndex + 1).ToString();

            if (pageGrid.PageIndex == pageGrid.PageCount - 1)
            {
                displayNext = false;
                displayPrevious = true;
            }

            else if (pageGrid.PageIndex == 0)
            {
                displayPrevious = false;
                displayNext = true;
            }
            else
            {
                displayPrevious = true;
                displayNext = true;
            }
            this.GetAllBusinessInfoAndFillGrid();
        }
        catch (Exception ex)
        {

            lblMessage.Text = ex.ToString();
            lblMessage.Visible = true;
            imgGridMessage.Visible = true;
            imgGridMessage.ImageUrl = "images/error.png";
            lblMessage.ForeColor = System.Drawing.Color.Red;
        }
    }
    #endregion
    protected string GetDateString(object objDate)
    {
        if (objDate.ToString() != "")
        {
            DateTime dt = Convert.ToDateTime(objDate);
            return dt.ToString("MM-dd-yyyy H.mm tt");
        }
        return "";
    }

    #region Event of dropdown to take to selected page

    protected void ddlPage_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lblMessage.Visible = false;
            imgGridMessage.Visible = false;
            pageGrid.PageIndex = 0;
            DropDownList ddlPage = (DropDownList)pageGrid.BottomPagerRow.Cells[0].FindControl("ddlPage");
            ViewState["ddlPage"] = ddlPage.SelectedValue.ToString();
            setPageValueInCookie(ddlPage);
            this.GetAllBusinessInfoAndFillGrid();
        }
        catch (Exception ex)
        {

            lblMessage.Text = ex.ToString();
            lblMessage.Visible = true;
            imgGridMessage.Visible = true;
            imgGridMessage.ImageUrl = "images/error.png";
            lblMessage.ForeColor = System.Drawing.Color.Red;
        }
    }

    private void setPageValueInCookie(DropDownList ddlPage)
    {
        HttpCookie cookie = Request.Cookies["ddlPage"];
        if (cookie == null)
        {
            cookie = new HttpCookie("ddlPage");
        }
        cookie.Expires = DateTime.Now.AddYears(1);

        Response.Cookies.Add(cookie);

        cookie["ddlPage"] = ddlPage.SelectedValue.ToString();
    }

    #endregion

    public string getImagePath(object resID, object imgName)
    {
        try
        {
            ArrayList arrImage = new ArrayList();
            arrImage.AddRange(imgName.ToString().Split(','));

            if (arrImage.Count > 0)
            {
                string strImageName = arrImage[0].ToString();

                string path = AppDomain.CurrentDomain.BaseDirectory + "Images\\dealFood\\" + resID.ToString() + "\\" + strImageName;
                if (File.Exists(path))
                {
                    return "../Images/dealFood/" + resID.ToString() + "/" + strImageName;
                }
                else
                {
                    return "../Images/dealFood/noMenuImage.gif";
                }
            }
        }
        catch (Exception ex)
        {
            lblMessage.Text = "There is an error occur, please email us at support@tazzling.com or call 1855-295-1771.";
            lblMessage.Visible = true;
            imgGridMessage.Visible = true;
            imgGridMessage.ImageUrl = "~/Images/error.png"; lblMessage.ForeColor = System.Drawing.Color.Red;
            return "";
        }
        return "";
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



    protected string getComment(object strComment)
    {
        if (strComment.ToString() != "")
        {
            string[] strTem = strComment.ToString().Split(':');
            if (strTem.Length > 1)
            {
                return strTem[strTem.Length - 1];
            }
            else
            {
                return strTem[0];
            }
        }
        return "";
    }
       
    protected string getDealCodeSrch(string strDealCode)
    {
        if (strDealCode.ToString() != "")
        {
            GECEncryption objEnc = new GECEncryption();
            return objEnc.EncryptData("deatailOrder", strDealCode);
        }
        return "";
    }

    private DataTable SearchhDealInfoByDifferentParams()
    {
        DataTable dtOrderDetailInfo = null;

        string strQuery = "";

        try
        {
            strQuery = "SELECT";
            //strQuery += " ROW_NUMBER() OVER (ORDER BY [dealOrders].dOrderID) AS 'RowNumber'";
            strQuery += " [dealOrders].[dOrderID]";
            strQuery += " ,dealOrders.dealId";
            strQuery += " ,deals.title";            
            strQuery += " ,userInfo.userName";
            //strQuery += " ,dealOrders.createdDate";
            strQuery += " ,rtrim(userInfo.firstname) +' ' + rtrim(userInfo.lastName) as 'Name'";
            strQuery += " ,[dealOrders].[status]";
            strQuery += " ,[userComments].[comment]";            
            strQuery += ", deals.shippingAndTax";
            strQuery += " ,[dealOrderDetail].[voucherSecurityCode]";
            strQuery += " ,[dealOrderDetail].[detailID]";
            strQuery += " ,[dealOrderDetail].[isRedeemed]";
            strQuery += " ,[dealOrderDetail].[trackingNumber]";
            strQuery += " ,[dealOrderDetail].[dealOrderCode]";
            strQuery += " ,[dealOrderDetail].[markUsed]";
            strQuery += " FROM ";
            strQuery += " [dealOrders]";
            strQuery += " inner join deals on (deals.dealId = dealOrders.dealId)";
            strQuery += " left outer join userComments on (userComments.dOrderID = dealOrders.dOrderID)";
            strQuery += " inner join userInfo on (userInfo.userId = dealOrders.userId) ";
            strQuery += " inner join dealOrderDetail on dealOrderDetail.[dOrderID] = [dealOrders].[dOrderID]";
            if (Request.QueryString["order"] != null && Request.QueryString["order"].ToString().Trim() != ""
                && Request.QueryString["order"].Trim().ToLower() == "cancel")
            {
                strQuery += " where (dealOrders.status='Cancelled' OR  dealOrders.status='Refunded') and ";
            }
            else
            {
                strQuery += " where dealOrders.status='Successful' and ";
            }
            strQuery += " (deals.dealId =" + Request.QueryString["did"].ToString() + " OR deals.parentDealId=" + Request.QueryString["did"].ToString() + ")";
           // strQuery += " (dealOrders.dealId =" + Request.QueryString["did"].ToString() + ")";

            if (this.txtSrchDealCode.Text.Trim().Length > 0)
            {
                strQuery += " and [dealOrderDetail].[dealOrderCode] = '" + getDealCodeSrch(this.txtSrchDealCode.Text.Trim().ToUpper().Replace("#", "")).Trim() + "'";
            }
            if (this.txtUserEmail.Text.Trim().Length > 0)
            {
                strQuery += " and [userInfo].[userName] like '%" + txtUserEmail.Text.Trim() + "%'";
            }
            if (this.txtUserName.Text.Trim().Length > 0)
            {
                strQuery += " and (userInfo.firstname like '%" + txtUserName.Text.Trim() + "%' OR userInfo.lastname like '%" + txtUserName.Text.Trim() + "%') ";
            }
            if (ddlDealAddress.SelectedIndex != 0)
            {
                strQuery += " and dealOrders.addressID=" + ddlDealAddress.SelectedValue.ToString().Trim();
            }

            //Get the Deal
            if (this.ddlShowMe.SelectedValue == "available")
            {
                strQuery += " and [dealOrderDetail].isRedeemed = 0";
            }
            else if (this.ddlShowMe.SelectedValue == "used")
            {
                strQuery += " and [dealOrderDetail].isRedeemed = 1";
            }
            else if (this.ddlShowMe.SelectedValue == "all")
            {
                strQuery += "";
            }
            strQuery += " Order by Name asc";
            // this.pageGrid.PageIndex = 0;
            // GridView1.PageIndex = 0;

            //Get & Set the DataTable here
            dtOrderDetailInfo = Misc.search(strQuery);
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.ToString();
            lblMessage.Visible = true;
            imgGridMessage.Visible = true;
            imgGridMessage.ImageUrl = "images/error.png";
            lblMessage.ForeColor = System.Drawing.Color.Red;
        }

        return dtOrderDetailInfo;
    }
     
    
    private DataTable SortDataTable(DataTable dt)
    {
        try
        {
            GECEncryption objEnc = new GECEncryption();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dt.Rows[i]["dealOrderCode"] = objEnc.EncryptData("deatailOrder", dt.Rows[i]["dealOrderCode"].ToString());
            }
            return dt;

        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.ToString();
            lblMessage.Visible = true;
            imgGridMessage.Visible = true;
            imgGridMessage.ImageUrl = "images/error.png";
            lblMessage.ForeColor = System.Drawing.Color.Red;
        }

        return dt;
    }

    protected void btnSearch_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            lblMessage.Visible = false;
            imgGridMessage.Visible = false;

            GetAllBusinessInfoAndFillGrid();

            //if (txtSrchDealCode.Text.Trim() != "")
            //{
            //    lblMessage.Visible = false;
            //    imgGridMessage.Visible = false;
            //    if (ViewState["dtUser"] != null)
            //    {
            //        DataTable dtUser = (DataTable)ViewState["dtUser"];
            //        DataView dv;
            //        if (dtUser != null && dtUser.Rows.Count > 0)
            //        {
            //            DataRow[] dtRow = dtUser.Select("dealOrderCode like '%" + txtSrchDealCode.Text.Trim() + "%'");
            //            DataTable dtSortTable = dtUser.Clone();
            //            foreach (DataRow dr in dtRow)
            //                dtSortTable.ImportRow(dr);

            //            pageGrid.DataSource = dtSortTable.DefaultView;
            //            pageGrid.DataBind();
            //            //GridView1.DataSource = dtSortTable.DefaultView;
            //            //GridView1.DataBind();

            //            Label lblPageCount = (Label)pageGrid.BottomPagerRow.FindControl("lblPageCount");
            //            Label lblTotalRecords = (Label)pageGrid.BottomPagerRow.FindControl("lblTotalRecords");

            //            lblTotalRecords.Text = dtUser.Rows.Count.ToString();
            //            lblPageCount.Text = pageGrid.PageCount.ToString();

            //            DropDownList ddlPage = bindPageDropDown();
            //            if (ViewState["ddlPage"] != null && ViewState["ddlPage"].ToString() != "")
            //            {
            //                ddlPage.SelectedValue = ViewState["ddlPage"].ToString();
            //            }
            //            pageGrid.BottomPagerRow.Visible = true;

            //            TextBox txtPage = (TextBox)pageGrid.BottomPagerRow.Cells[0].FindControl("txtPage");
            //            if (pageGrid.PageIndex == 0)
            //            {
            //                displayPrevious = false;
            //            }
            //            else
            //            {
            //                displayPrevious = true;
            //                txtPage.Text = (pageGrid.PageIndex + 1).ToString();
            //            }
            //            if (pageGrid.PageIndex == pageGrid.PageCount - 1)
            //            {
            //                displayNext = false;
            //            }
            //            else
            //            {
            //                displayNext = true;
            //                txtPage.Text = (pageGrid.PageIndex + 1).ToString();
            //            }

            //            if (pageGrid.PageCount == 1)
            //            {
            //                ImageButton imgPrev = (ImageButton)pageGrid.BottomPagerRow.FindControl("btnPrev");
            //                ImageButton imgNext = (ImageButton)pageGrid.BottomPagerRow.FindControl("btnNext");

            //                imgNext.Enabled = false;
            //                imgPrev.Enabled = false;
            //            }
            //        }
            //        else
            //        {
            //            GetAllBusinessInfoAndFillGrid();
            //        }

            //    }
            //    else
            //    {
            //        GetAllBusinessInfoAndFillGrid();
            //    }
            //}
            //else
            //{
            //    GetAllBusinessInfoAndFillGrid();
            //}
        }
        catch (Exception ex)
        { }
    }
}
