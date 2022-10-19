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

public partial class pastDeals : System.Web.UI.Page
{
    BLLDeals objDeals = new BLLDeals();
    BLLCities objCities = new BLLCities();
    

    #region Private Properties
    private int CurrentPage
    {
        get
        {
            object objPage = ViewState["_CurrentPage"];
            int _CurrentPage = 0;
            if (objPage == null)
            {
                _CurrentPage = 0;
            }
            else
            {
                _CurrentPage = (int)objPage;
            }
            return _CurrentPage;
        }
        set { ViewState["_CurrentPage"] = value; }
    }
    private int fistIndex
    {
        get
        {

            int _FirstIndex = 0;
            if (ViewState["_FirstIndex"] == null)
            {
                _FirstIndex = 0;
            }
            else
            {
                _FirstIndex = Convert.ToInt32(ViewState["_FirstIndex"]);
            }
            return _FirstIndex;
        }
        set { ViewState["_FirstIndex"] = value; }
    }
    private int lastIndex
    {
        get
        {

            int _LastIndex = 0;
            if (ViewState["_LastIndex"] == null)
            {
                _LastIndex = 0;
            }
            else
            {
                _LastIndex = Convert.ToInt32(ViewState["_LastIndex"]);
            }
            return _LastIndex;
        }
        set { ViewState["_LastIndex"] = value; }
    }
    #endregion

    #region PagedDataSource
    PagedDataSource _PageDataSource = new PagedDataSource();
    #endregion

    #region Private Methods
    /// <summary>
    /// Binding Main Items List
    /// </summary>
    private void BindItemsList()
    {
        objDeals.DealEndTime = Misc.getResturantLocalTime(3);        
        DataTable dataTable = objDeals.getPreviousDealByCityIDForShow();
        _PageDataSource.DataSource = dataTable.DefaultView;
        _PageDataSource.AllowPaging = true;
        _PageDataSource.PageSize = 20;
        _PageDataSource.CurrentPageIndex = CurrentPage;
        ViewState["TotalPages"] = _PageDataSource.PageCount;

        this.lblPageInfo.Text = "Page " + (CurrentPage + 1) + " of " + _PageDataSource.PageCount;
        this.lbtnPrevious.Enabled = !_PageDataSource.IsFirstPage;
        this.lbtnNext.Enabled = !_PageDataSource.IsLastPage;
        this.lbtnFirst.Enabled = !_PageDataSource.IsFirstPage;
        this.lbtnLast.Enabled = !_PageDataSource.IsLastPage;

        this.dtlPastDeals.DataSource = _PageDataSource;
        this.dtlPastDeals.DataBind();
        this.doPaging();
    }

    /// <summary>
    /// Binding Paging List
    /// </summary>
    private void doPaging()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("PageIndex");
        dt.Columns.Add("PageText");

        fistIndex = CurrentPage - 5;


        if (CurrentPage > 5)
        {
            lastIndex = CurrentPage + 5;
        }
        else
        {
            lastIndex = 10;
        }
        if (lastIndex > Convert.ToInt32(ViewState["TotalPages"]))
        {
            lastIndex = Convert.ToInt32(ViewState["TotalPages"]);
            fistIndex = lastIndex - 10;
        }

        if (fistIndex < 0)
        {
            fistIndex = 0;
        }

        for (int i = fistIndex; i < lastIndex; i++)
        {
            DataRow dr = dt.NewRow();
            dr[0] = i;
            dr[1] = i + 1;
            dt.Rows.Add(dr);
        }

        this.dlPaging.DataSource = dt;
        this.dlPaging.DataBind();
    }
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Title = ConfigurationManager.AppSettings["pageTitleStart"].ToString().Trim() + " | Tastygo | Past Deals";
        if (!IsPostBack)
        {

            DataTable dtDeals = Misc.search("select customersaves from customerSaves");
            if (dtDeals!=null && dtDeals.Rows.Count > 0)
            {
                object sumObject;
                sumObject = Convert.ToInt64(dtDeals.Rows[0]["customersaves"].ToString().Trim());
                label8.Text = "Tastygo customers have saved over $" + String.Format("{0:0,0}", sumObject) + "!";
            }

            this.BindItemsList();

        }
    }


    protected string GetDateString(object objDate)
    {
        if (objDate.ToString() != "")
        {
            DateTime dt = Convert.ToDateTime(objDate);
            return dt.ToString("MMM dd, yyyy");
        }
        return "";
    }

    protected void lbtnNext_Click(object sender, EventArgs e)
    {

        CurrentPage += 1;
        this.BindItemsList();

    }
    protected void lbtnPrevious_Click(object sender, EventArgs e)
    {
        CurrentPage -= 1;
        this.BindItemsList();

    }
    protected void dlPaging_ItemCommand(object source, DataListCommandEventArgs e)
    {
        if (e.CommandName.Equals("Paging"))
        {
            CurrentPage = Convert.ToInt16(e.CommandArgument.ToString());
            this.BindItemsList();
        }
    }
    protected void dlPaging_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        LinkButton lnkbtnPage = (LinkButton)e.Item.FindControl("lnkbtnPaging");
        if (lnkbtnPage.CommandArgument.ToString() == CurrentPage.ToString())
        {
            lnkbtnPage.Enabled = false;
            lnkbtnPage.Style.Add("fone-size", "13px");
            lnkbtnPage.Style.Add("color", "#ff7800");
            lnkbtnPage.Style.Add("text-decoration", "none");
            lnkbtnPage.Font.Bold = true;    
        }
    }
    protected void lbtnLast_Click(object sender, EventArgs e)
    {

        CurrentPage = (Convert.ToInt32(ViewState["TotalPages"]) - 1);
        this.BindItemsList();

    }
    protected void lbtnFirst_Click(object sender, EventArgs e)
    {
        CurrentPage = 0;
        this.BindItemsList();
    }
}
