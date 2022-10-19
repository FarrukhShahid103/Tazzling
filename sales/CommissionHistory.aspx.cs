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

public partial class sales_CommissionHistory : System.Web.UI.Page
{
    public bool displayPrevious = false;
    public bool displayNext = true;
    BLLDealOrders objBLLDealOrders = new BLLDealOrders();
    private const string ASCENDING = " ASC";
    private const string DESCENDING = " DESC";
    BLLUser obj = new BLLUser();
    DataTable dtUser;
    DataView dv;
    DataTable dt;
    float _Total = 0;
    float _GrandTotal = 0;
    float _TotalRevenue = 0;
    float _Adjustment = 0;
    float _Bonus = 0;
    
   
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            txtMonthYear.Text = DateTime.Now.Month.ToString() + "/" + DateTime.Now.Year.ToString();
            SearchAndBindMonthlyReport();
            dtUser = (DataTable)Session["salesSection"];

            if (dtUser != null && dtUser.Rows.Count > 0)
            {
                if (dtUser.Rows[0]["UsertypeID"].ToString().Trim() == "1" || dtUser.Rows[0]["UsertypeID"].ToString().Trim() == "2")
                {
                    lblSelectSalesman.Visible = true;
                    ddlSalePersonAccountName.Visible = true;


                    BounsTextBoxDiv.Visible = true;
                    lblBounsTextBoxDiv.Visible = false;

                    TextAdjustmentDiv.Visible = true;
                    lblTextAdjustmentDiv.Visible = false;

                }
                else
                {
                    lblSelectSalesman.Visible = false;
                    ddlSalePersonAccountName.Visible = false;


                    BounsTextBoxDiv.Visible = false;
                    lblBounsTextBoxDiv.Visible = true;

                    TextAdjustmentDiv.Visible = false;
                    lblTextAdjustmentDiv.Visible = true;
                }

                BindSaleAccounts();
                bindProvinces();
                GetAllBusinessInfoAndFillGrid();
            }
            else
            {
                Response.Redirect("Default.aspx",false);
                Response.End();
            }
        }
    }

    private void SalePersonAdjustmentAndbonus()
    {
        dtUser = (DataTable)Session["salesSection"];
        if (dtUser != null && dtUser.Rows.Count > 0)
        {


            if (dtUser.Rows[0]["UsertypeID"].ToString().Trim() == "1" || dtUser.Rows[0]["UsertypeID"].ToString().Trim() == "2")
            {
                if (ddlSalePersonAccountName.SelectedIndex == 0)
                {
                    return;
                }

                obj.email = ddlSalePersonAccountName.SelectedValue.ToString().Trim();

            }
            else
            {
                if (dtUser != null && dtUser.Rows.Count > 0)
                {
                    obj.email = dtUser.Rows[0]["userName"].ToString().Trim();
                }
            }

            dt = obj.GetSalePersonBounsAndAdjustment();




            if (dt != null && dt.Rows.Count > 0)
            {

                if (dt.Rows[0]["Bonus"] != null && dt.Rows[0]["Bonus"].ToString().Trim() != "")
                {
                    txtBonus.Text = dt.Rows[0]["Bonus"].ToString().Trim();
                    lblBonus.Text = "$" + dt.Rows[0]["Bonus"].ToString().Trim();
                    _Bonus = float.Parse(dt.Rows[0]["Bonus"].ToString().Trim());
                }
                else
                {
                    txtBonus.Text = "0";
                    lblBonus.Text = "$0";
                    _Bonus = 0;
                }


                if (dt.Rows[0]["Adjustment"] != null && dt.Rows[0]["Adjustment"].ToString().Trim() != "")
                {
                    txtAdjustment.Text = dt.Rows[0]["Adjustment"].ToString().Trim();
                    lblAdjustment.Text = "$" + dt.Rows[0]["Adjustment"].ToString().Trim();
                    _Adjustment = float.Parse(dt.Rows[0]["Adjustment"].ToString().Trim());
                }
                else
                {
                    txtAdjustment.Text = "0";
                    lblAdjustment.Text = "$0";
                    _Adjustment = 0;
                }

            }
            else
            {
                txtBonus.Text = "0";
                txtAdjustment.Text = "0";
                _Bonus = 0;
                _Adjustment = 0;

            }

        }
       
        if (hfGrandTotal.Value != null && hfGrandTotal.Value.ToString().Trim() != "")
        {
            lblGrandTotal.Text = "$" + (((float.Parse(hfGrandTotal.Value) + _Bonus) + _Adjustment)).ToString();
            lblTotalCommissionEarned.Text = "$" + hfGrandTotal.Value.ToString();
        }
        else
        {
            lblGrandTotal.Text = "$" + (((_Bonus) + _Adjustment)).ToString();
            lblTotalCommissionEarned.Text = "$0";
        }


        if (hfGrandTotalRevenue.Value != null && hfGrandTotalRevenue.Value.ToString().Trim() != "")
        {
            lblTotalRevenueGenerated.Text = "$" + hfGrandTotalRevenue.Value.ToString();
        }
        else
        {
            lblTotalRevenueGenerated.Text = "$0";
            
        }





       



    }
    protected void bindProvinces()
    {
        try
        {
            DataTable dt = Misc.getProvincesByCountryID(2);
            ddlSearchProvince.DataSource = dt.DefaultView;
            ddlSearchProvince.DataTextField = "provinceName";
            ddlSearchProvince.DataValueField = "provinceId";
            ddlSearchProvince.DataBind();
            ddlSearchProvince.Items.Insert(0, "Select One");
        }
        catch (Exception ex)
        {
        }
    }
    protected void ddlSearchProvince_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            FillCitpDroDownList(this.ddlSearchProvince.SelectedValue);
        }
        catch (Exception ex)
        {
            string strException = "There is an error occur, please email us at support@tazzling.com or call 1855-295-1771.";
        }
    }
    private void FillCitpDroDownList(string strProvinceId)
    {
        try
        {
            if (strProvinceId != "Select One")
            {
                BLLCities objBLLCities = new BLLCities();

                objBLLCities.provinceId = int.Parse(strProvinceId);

                DataTable dtCities = objBLLCities.getCitiesByProvinceId();

                ddlSearchCity.DataSource = dtCities;

                ddlSearchCity.DataTextField = "cityName";

                ddlSearchCity.DataValueField = "cityId";

                ddlSearchCity.DataBind();

                ddlSearchCity.Items.Insert(0, "Select One");
            }
            else
            {
                ddlSearchCity.Items.Clear();
                ddlSearchCity.DataSource = null;
                ddlSearchCity.DataBind();
            }
        }
        catch (Exception ex)
        {
            string strException = "There is an error occur, please email us at support@tazzling.com or call 1855-295-1771.";
        }
    }
    DataTable dtSubscribedCityList, DataTableAllCities = new DataTable();
    protected void dlCities_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        if (dtSubscribedCityList != null)
        {
            System.Web.UI.WebControls.CheckBox chk;
            chk = (System.Web.UI.WebControls.CheckBox)(e.Item.FindControl("chkbxSelect"));
            Label lblCityID = (Label)(e.Item.FindControl("lblCityID"));
            Label lblCityName = (Label)(e.Item.FindControl("lblCity"));
            try
            {
                if (lblCityID != null && chk != null)
                {
                    for (int j = 0; j < dtSubscribedCityList.Rows.Count; j++)
                    {

                        if (lblCityID.Text.Trim() == dtSubscribedCityList.Rows[j]["cityid"].ToString().Trim())
                        {
                            chk.Checked = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                string strException = "There is an error occur, please email us at support@tazzling.com or call 1855-295-1771.";
            }
        }

    }
    private void GetAndSetUserID()
    {
        try
        {
            DataTable dtUser = (DataTable)Session["user"];

            if ((dtUser != null) && (dtUser.Rows.Count > 0))
            {
                ViewState["userID"] = dtUser.Rows[0]["userID"];
            }
        }
        catch (Exception ex)
        {
            string strException = "There is an error occur, please email us at support@tazzling.com or call 1855-295-1771.";
        }
    }
    protected void btnSearch_Click(object sender, ImageClickEventArgs e)
    {
        string strQuery = "";
        try
        {
            if (ddlSearchCity.SelectedValue.ToString().Trim() != "" || txtBusinessName.Text.Trim() != "" || txtStartDate.Text.Trim() != "" || txtEndDate.Text.Trim() != "" || txtSalesPersonAccountName.Text.Trim() != "")
            {

                lblMessage.Visible = false;
                imgGridMessage.Visible = false;


                strQuery = "Select ";
                strQuery += " [deals].[dealId] ";
                strQuery += ",[deals].[restaurantId] ";
                strQuery += ",[deals].[title] ";
                strQuery += " ,[deals].[sellingPrice] ";
                strQuery += " ,userInfo.firstName ";
                strQuery += " ,userInfo.lastName ";
                
                strQuery += ",dealStartTimeC as 'dealStartTime' ";
                strQuery += ",dealEndTimeC as 'dealEndTime' ";
                strQuery += " ,[deals].[createdBy] ";
                strQuery += ",[deals].[createdDate] ";
                strQuery += ",isnull((SELECT sum(qty) FROM  [dealOrders] where [dealOrders].[status]='Successful' and [dealOrders].dealId = [deals].dealId),0) 'SuccessfulOrder' ";
                strQuery += ",[restaurant].[restaurantBusinessName] ";
                strQuery += ",[restaurant].[commission] AS 'NegotiatedCommission' ";
                strQuery += ",[deals].[salePersonAccountName] ";
                strQuery += " ,[deals].[SalesPersonCommission] AS 'SalePersonCommission' ";
                strQuery += ",[city].[cityName] ";
                strQuery += "FROM ";
                strQuery += "[deals] ";
                strQuery += "INNER join restaurant On restaurant.[restaurantId]= deals.[restaurantId] ";
                strQuery += " INNER JOIN userInfo ON userInfo.userName = deals.salePersonAccountName ";
                strQuery += "  INNER join dealcity On dealcity.[dealid]= deals.[dealid] ";
                strQuery += " INNER join city On dealCity.cityId= city.[cityId] ";
                strQuery += " where restaurant.[restaurantId]= deals.[restaurantId] ";
                
                if (ddlSearchCity.SelectedValue.ToString().Trim() != "" && ddlSearchCity.SelectedValue.ToString().Trim() != "Select One")
                {
                    strQuery += " and city.[cityName] like '%" + ddlSearchCity.SelectedItem.ToString().Trim() + "%' ";
                }
                if (txtBusinessName.Text.Trim() != "")
                {
                    strQuery += " and [restaurant].[restaurantBusinessName] like '%" + txtBusinessName.Text.Trim() + "%' ";
                }
                if (txtStartDate.Text.Trim() != "" && txtEndDate.Text.Trim() != "")
                {
                    strQuery += " and dealOrders.createdDate between '" + txtStartDate.Text.Trim() + "' AND '" + txtEndDate.Text.Trim() + "'";
                }

                if (txtSalesPersonAccountName.Text.Trim() != "")
                {
                    strQuery += " and [deals].[salePersonAccountName] like '%" + txtSalesPersonAccountName.Text.Trim() + "%' ";
                }

                if (Session["SalesPersonAccountName"] != null && Session["SalesPersonAccountName"].ToString().Trim() != "")
                {

                    strQuery += " and [deals].[salePersonAccountName] ='" + Session["SalesPersonAccountName"].ToString().Trim() + "'";

                }

                strQuery += " order by restaurant.restaurantId desc";

                

                pageGrid.PageIndex = 0;
                BindSearchedData(strQuery);
                ViewState["Query"] = strQuery;


            }

            else
            {
                pageGrid.PageIndex = 0;
                GetAllBusinessInfoAndFillGrid();
                ViewState["Query"] = null;
            }

        }
        catch
        {


        }

    }
    protected void btnClear_Click(object sender, ImageClickEventArgs e)
    {
        ddlSearchProvince.SelectedIndex = 0;
        ddlSearchCity.Items.Clear();
        txtDealName.Text = "";
        txtBusinessName.Text = "";
        txtStartDate.Text = "";
        txtEndDate.Text = "";
        txtSalesPersonAccountName.Text = "";


    }

    #region Function to bind Search data in Grid
    private void BindSearchedData(string Query)
    {
        try
        {
            if (Session["ddlPage"] != null && Session["ddlPage"].ToString() != "")
            {
                pageGrid.PageSize = Convert.ToInt32(Session["ddlPage"]);
            }
            else
            {
                pageGrid.PageSize = Misc.pageSize;
                Session["ddlPage"] = Misc.pageSize;
            }

            DataTable dtUser = Misc.search(Query);

            if ((dtUser != null) &&
                (dtUser.Columns.Count > 0) &&
                (dtUser.Rows.Count > 0))
            {
                pageGrid.DataSource = dtUser.DefaultView;
                pageGrid.DataBind();

                Label lblPageCount = (Label)pageGrid.BottomPagerRow.FindControl("lblPageCount");
                Label lblTotalRecords = (Label)pageGrid.BottomPagerRow.FindControl("lblTotalRecords");

                lblTotalRecords.Text = dtUser.Rows.Count.ToString();
                pageGrid.PageIndex = 0;

                ViewState["Query"] = Query;
                DropDownList ddlPage = bindPageDropDown();
                if (Session["ddlPage"] != null && Session["ddlPage"].ToString() != "")
                {
                    ddlPage.SelectedValue = Session["ddlPage"].ToString();
                }
                lblPageCount.Text = pageGrid.PageCount.ToString();
                pageGrid.BottomPagerRow.Visible = true;
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
            }

        }
        catch (Exception ex)
        {
            pnlGrid.Visible = true;
            lblMessage.Text = ex.ToString();
            lblMessage.Visible = true;
            imgGridMessage.Visible = true;
            imgGridMessage.ImageUrl = "~/admin/images/error.png";
            lblMessage.ForeColor = System.Drawing.Color.Red;
        }
    }
    #endregion
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
                pageGrid.PageSize = Misc.pageSize;
                ViewState["ddlPage"] = Misc.pageSize;
            }

            if (ViewState["Query"] == null)
            {
                dtUser = (DataTable)Session["salesSection"];

                if (dtUser != null && dtUser.Rows.Count > 0)
                {

                    if (dtUser.Rows[0]["UsertypeID"].ToString().Trim() == "1" || dtUser.Rows[0]["UsertypeID"].ToString().Trim() == "2")
                    {
                      dtUser = objBLLDealOrders.ComissionHistoryForAllSalesPersons();
                      DataTable dt = RemoveDuplicateRows(dtUser, "dealEndTime");
                      dv = new DataView(dt);
                    }
                    else
                    {
                        if (Session["salesSection"] != null && Session["salesSection"].ToString().Trim() != "")
                        {
                            lblSalesPerson.Visible = false;
                            txtSalesPersonAccountName.Visible = false;
                           
                            Session["SalesPersonAccountName"] = dtUser.Rows[0]["userName"].ToString().Trim();
                            BLLUser objuser = new BLLUser();
                            dtUser = null;

                            dtUser = objBLLDealOrders.ComissionHistoryForSalePerson(Session["SalesPersonAccountName"].ToString().Trim());
                            DataTable dt = RemoveDuplicateRows(dtUser, "dealEndTime");
                            dv = new DataView(dt);


                            dtUser = null;

                            dtUser = dt;
                            if (dtUser != null && dtUser.Rows.Count > 0)
                            {
                                dv = new DataView(dtUser);
                            }
                        }

                    }


                }



            }
            else
            {
                dtUser = Misc.search(ViewState["Query"].ToString());
                dv = new DataView(dtUser);
                if (ViewState["Direction"] != null)
                {
                    dv.Sort = ViewState["Direction"].ToString();
                }

            }
            if (dtUser != null && dtUser.Rows.Count > 0)
            {
                pageGrid.DataSource = dv;
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

            }

        }
        catch (Exception ex)
        {
            pnlGrid.Visible = true;
            lblMessage.Text = ex.ToString();
            lblMessage.Visible = true;
            imgGridMessage.Visible = true;
            imgGridMessage.ImageUrl = "~/admin/images/error.png";
            lblMessage.ForeColor = System.Drawing.Color.Red;
        }
    }
    protected string GetDateString(object objDate)
    {
        if (objDate.ToString() != "")
        {
            DateTime dt = Convert.ToDateTime(objDate);
            return dt.ToString("MM-dd-yyyy");
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
            pnlGrid.Visible = true;
            lblMessage.Text = ex.ToString();
            lblMessage.Visible = true;
            imgGridMessage.Visible = true;
            imgGridMessage.ImageUrl = "~/admin/images/error.png";
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
            ListItem objList = new ListItem("All", objBLLDealOrders.ComissionHistoryForAllSalesPersons().Rows.Count.ToString());
            ddlPage.Items.Insert(5, objList);
            return ddlPage;
        }
        catch (Exception ex)
        {
            pnlGrid.Visible = true;
            lblMessage.Text = ex.ToString();
            lblMessage.Visible = true;
            imgGridMessage.Visible = true;
            imgGridMessage.ImageUrl = "~/admin/images/error.png";
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
            pnlGrid.Visible = true;
            lblMessage.Text = ex.ToString();
            lblMessage.Visible = true;
            imgGridMessage.Visible = true;
            imgGridMessage.ImageUrl = "~/admin/images/error.png";
            lblMessage.ForeColor = System.Drawing.Color.Red;
        }
    }

    protected void pageGrid_OnRowCommand(object sender, GridViewCommandEventArgs e)
    {

        if (e.CommandName.ToString().Trim() == "SaveCommission")
        {
            string[] arr = e.CommandArgument.ToString().Trim().Split('_');

            TextBox tbx = (TextBox)pageGrid.Rows[Convert.ToInt32(arr[1])].FindControl("txtEarnedCommission");
            objBLLDealOrders.dealId = Convert.ToInt32(arr[0].ToString().Trim());
            int Result = objBLLDealOrders.AddUpdateDealCommissionEarnedByDealID(float.Parse(tbx.Text.ToString().Trim()));
            if (Result != 0)
            {
                lblMessage.Text = "Earned Commission Ammount has been saved successfully.";
                lblMessage.Visible = true;
                imgGridMessage.Visible = true;
                imgGridMessage.ImageUrl = "images/Checked.png";
                lblMessage.ForeColor = System.Drawing.Color.Black;

            }
            else
            {
                lblMessage.Text = "An error occured while saving Earned Commission Ammount.";
                lblMessage.Visible = true;
                imgGridMessage.Visible = true;
                imgGridMessage.ImageUrl = "images/error.png";
                lblMessage.ForeColor = System.Drawing.Color.Red;

            }

        }

    }

    public bool IsAdminLogin()
    { 
    
         DataTable dt;
            dt = (DataTable)Session["salesSection"];
            if (dt != null && dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["userTypeID"].ToString() == "1" || dt.Rows[0]["userTypeID"].ToString() == "2")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;

            }

    }
    public string EarnedCommission(string NegotiatedCommission,string SalePersonCommission,string SuccessfulOrder,string sellingPrice)
    {


        float _NegotiatedCommission = 0;
        float _SalePersonCommission = 0;
        float _SuccessfulOrder = 0;
        float _sellingPrice = 0;
        //float _SalePersonPercentageAmmount = 0;
        //float _NegotiatedCommissionPercentageAmmount = 0;
        float _TotalCommissionAmmount = 0;
        float _RoundedTotal = 0;


        if (NegotiatedCommission.Trim() != "" || NegotiatedCommission != null)
        {
            float.TryParse(NegotiatedCommission,out _NegotiatedCommission);
        }
        
        if (SalePersonCommission.ToString().Trim() != "" || SalePersonCommission != String.Empty)
        {
            float.TryParse(SalePersonCommission, out _SalePersonCommission);
        }

        if (SuccessfulOrder.Trim() != "" || SuccessfulOrder != null)
        {
            float.TryParse(SuccessfulOrder, out _SuccessfulOrder);
        }
       
        if (sellingPrice.Trim() != "" || sellingPrice != null)
        {
            float.TryParse(sellingPrice, out _sellingPrice);
        }


        _RoundedTotal = (float)Math.Round(_SuccessfulOrder * _sellingPrice * (_NegotiatedCommission / 100) * (_SalePersonCommission / 100), 2);
        
        _TotalCommissionAmmount = _RoundedTotal;
        return _TotalCommissionAmmount.ToString();
    }


    public string RevenueGenerated(string SuccessfulOrder, string sellingPrice)
    {
        float _SuccessfulOrder = 0;
        float _sellingPrice = 0;
        float _Total = 0;
        if (SuccessfulOrder.Trim() != "" || SuccessfulOrder != null)
        {
            float.TryParse(SuccessfulOrder, out _SuccessfulOrder);
        }

        if (sellingPrice.Trim() != "" || sellingPrice != null)
        {
            float.TryParse(sellingPrice, out _sellingPrice);
        }
        _Total = (float)Math.Round(_SuccessfulOrder * _sellingPrice,2);
        return  _Total.ToString();
    }
    protected void grdReport_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            HiddenField hfTotal = (HiddenField)e.Row.FindControl("hfTotal");
            HiddenField hfTotalRevenue = (HiddenField)e.Row.FindControl("hfTotalRevenue");

            if (hfTotal != null && hfTotal.Value != string.Empty && hfTotal.Value.ToString().Trim() != "")
            {

               
                _GrandTotal += float.Parse(hfTotal.Value);

                hfGrandTotal.Value = _GrandTotal.ToString();
            }


            if (hfTotalRevenue  != null && hfTotalRevenue.Value != string.Empty && hfTotalRevenue.Value.ToString().Trim() != "")
            {
                _TotalRevenue += float.Parse(hfTotalRevenue.Value);

                hfGrandTotalRevenue.Value = _TotalRevenue.ToString();
            }
        }
    }

    protected void btnGetReport_Click(object sender, ImageClickEventArgs e)
    {
        lblErrorForSectab.Visible = false;
        imgError.Visible = false;

        if (txtMonthYear.Text == string.Empty || txtMonthYear.Text == null || txtMonthYear.Text == "")
        {

            lblErrorForSectab.Visible = true;
            imgGridMessage.ImageUrl = "images/error.png";
            lblErrorForSectab.ForeColor = System.Drawing.Color.Red;
            imgError.Visible = true;
            lblErrorForSectab.Text = "Please select Month and Year to proceed";
            return;

        }
       
        _Total = 0;
        grdReport.DataSource = null;
        grdReport.DataBind();


        SearchAndBindMonthlyReport();
        
    }

    private void SearchAndBindMonthlyReport()
    {

        dtUser = (DataTable)Session["salesSection"];
       

        string[] MonthYear = txtMonthYear.Text.ToString().Trim().Split('/');
        string Month = MonthYear[0];
        string Year = MonthYear[1];

        

        if (dtUser != null && dtUser.Rows.Count > 0)
        {
            objBLLDealOrders.Month = Month;
            objBLLDealOrders.Year = Year;
            if (dtUser.Rows[0]["UsertypeID"].ToString().Trim() == "1" || dtUser.Rows[0]["UsertypeID"].ToString().Trim() == "2")
            {
                if (ddlSalePersonAccountName.SelectedIndex == 0)
                {

                    lblErrorForSectab.Visible = true;
                    lblErrorForSectab.ForeColor = System.Drawing.Color.Red;
                    imgError.Visible = true;
                    lblErrorForSectab.Text = "Please select Sale Person to proceed.";
                    return;

                }

                objBLLDealOrders.SalePersonAccountName = ddlSalePersonAccountName.SelectedValue.ToString().Trim();
                dtUser = objBLLDealOrders.ComissionHistoryReportForSalePerson();
                DataTable dt = RemoveDuplicateRows(dtUser, "dealEndTime");
                dv = new DataView(dt);
                dtUser = null;
                dtUser = dt;

                //float sumObject1 = 0;
                //float sumObject2 = 0;
                //float sumObject3 = 0;

                //float.TryParse(dtUser.Compute("Sum(saveAmount * saveAmount * saveAmount)", "saveAmount > 0").ToString(), out sumObject);



                if (dtUser != null && dtUser.Rows.Count > 0)
                {

                    grdReport.DataSource = dtUser;
                    grdReport.DataBind();
                    BottomArea.Visible = true;
                   
                }
                else
                {
                    lblGrandTotal.Text = "$" + (((_Bonus) + _Adjustment)).ToString();
                    grdReport.DataSource = null;
                    grdReport.DataBind();
                    BottomArea.Visible = false;
                }
            }
            else
            {
                if (Session["salesSection"] != null && Session["salesSection"].ToString().Trim() != "")
                {


                    dtUser = null;
                    dtUser = (DataTable)Session["salesSection"];
                    if (dtUser != null && dtUser.Rows.Count > 0)
                    {
                        objBLLDealOrders.SalePersonAccountName = dtUser.Rows[0]["userName"].ToString().Trim();

                        dtUser = objBLLDealOrders.ComissionHistoryReportForSalePerson();
                        DataTable dt = RemoveDuplicateRows(dtUser, "dealEndTime");
                        dv = new DataView(dt);
                        dtUser = null;
                        dtUser = dt;

                        if (dtUser != null && dtUser.Rows.Count > 0)
                        {
                            grdReport.DataSource = dtUser;
                            grdReport.DataBind();
                            BottomArea.Visible = true;
                           
                        }
                        else
                        {
                            lblGrandTotal.Text = "$" + (((_Bonus) + _Adjustment)).ToString();
                            grdReport.DataSource = null;
                            grdReport.DataBind();
                            BottomArea.Visible = false;
                        }
                    }
                }
            }
        }


        SalePersonAdjustmentAndbonus();

        
    }


    protected void BtnSaveBonus_Click(object sender, ImageClickEventArgs e)
    {
        if (txtBonus.Text == string.Empty || txtBonus.Text == null || txtBonus.Text == "")
        {
            lblErrorForSectab.Visible = true;
            lblErrorForSectab.ForeColor = System.Drawing.Color.Red;
            imgGridMessage.ImageUrl = "images/error.png";
            imgError.Visible = true;
            lblErrorForSectab.Text = "Please enter amount for Bonus. (only numeric value)";
            return;
        }

        if (ddlSalePersonAccountName.SelectedIndex == 0)
        {

            lblErrorForSectab.Visible = true;
            lblErrorForSectab.ForeColor = System.Drawing.Color.Red; 
            imgGridMessage.ImageUrl = "images/error.png";
            imgError.Visible = true;
            lblErrorForSectab.Text = "Please select Sale Person to proceed.";
            return;

        }
       
        obj.email = ddlSalePersonAccountName.SelectedValue.ToString().Trim();
        obj.Bonus = float.Parse(txtBonus.Text.ToString().Trim());
        int Result = obj.AddUpdateSalePersonBonus();
        if (Result != 0)
        {
            lblErrorForSectab.Visible = true;
            lblErrorForSectab.ForeColor = System.Drawing.Color.Green;
            imgError.ImageUrl = "images/Checked.png";
            imgError.Visible = true;
            lblErrorForSectab.Text = "Bonus has been saved successfully.";
        }
        else
        {
            lblErrorForSectab.Visible = true;
            lblErrorForSectab.ForeColor = System.Drawing.Color.Red;
            imgError.ImageUrl = "images/error.png";
            imgError.Visible = true;
            lblErrorForSectab.Text = "An error occurred while saving Bonus";
        }

        

        SearchAndBindMonthlyReport();




    }

    protected void BtnSaveAdjustment_Click(object sender, ImageClickEventArgs e)
    {

        if (txtAdjustment.Text == string.Empty || txtAdjustment.Text == null || txtAdjustment.Text == "")
        {
            lblErrorForSectab.Visible = true;
            lblErrorForSectab.ForeColor = System.Drawing.Color.Red;
            imgError.Visible = true;
            imgGridMessage.ImageUrl = "images/error.png";
            lblErrorForSectab.Text = "Please enter amount for Adjustment. (only numeric value)";
            return;
        }

        if (ddlSalePersonAccountName.SelectedIndex == 0)
        {

            lblErrorForSectab.Visible = true;
            lblErrorForSectab.ForeColor = System.Drawing.Color.Red;
            imgGridMessage.ImageUrl = "images/error.png";
            imgError.Visible = true;
            lblErrorForSectab.Text = "Please select Sale Person to proceed.";
            return;

        }

        obj.email = ddlSalePersonAccountName.SelectedValue.ToString().Trim();
        obj.Adjustment = float.Parse(txtAdjustment.Text.ToString().Trim());
        int Result = obj.AddUpdateSalePersonAdjustment();
        if (Result != 0)
        {
            lblErrorForSectab.Visible = true;
            lblErrorForSectab.ForeColor = System.Drawing.Color.Green;
            imgError.ImageUrl = "images/Checked.png";
            imgError.Visible = true;
            lblErrorForSectab.Text = "Adjustment has been saved successfully.";
        }
        else
        {
            lblErrorForSectab.Visible = true;
            lblErrorForSectab.ForeColor = System.Drawing.Color.Red;
            imgError.ImageUrl = "images/error.png";
            imgError.Visible = true;
            lblErrorForSectab.Text = "An error occurred while saving Adjustment";
        }

        

        SearchAndBindMonthlyReport();

        //SalePersonAdjustmentAndbonus();
    }

    
    protected void BindSaleAccounts()
    {
        try
        {
           
            DataTable dt = null;
            dt = obj.GetAllSalesAccountNames();
            if (dt != null && dt.Rows.Count > 0)
            {
                ddlSalePersonAccountName.DataSource = dt;
                ddlSalePersonAccountName.DataTextField = "email";
                ddlSalePersonAccountName.DataValueField = "email";
                ddlSalePersonAccountName.DataBind();
                ddlSalePersonAccountName.Items.Insert(0, "Please Select");
            }
        }
        catch (Exception ex)
        {
            lblMessage.Text = "There is an error occur while bind Sales Account Please contact you technical support";
            lblMessage.Visible = true;
            imgGridMessage.Visible = true;
            imgGridMessage.ImageUrl = "images/error.png";
            lblMessage.ForeColor = System.Drawing.Color.Red;
        }

        

    }
    #region Function to Sort Grid

    protected void pageGrid_Sorting(object sender, GridViewSortEventArgs e)
    {
        string sortExpression = e.SortExpression;

        if (GridViewSortDirection == SortDirection.Ascending)
        {
            GridViewSortDirection = SortDirection.Descending;
            SortGridView(sortExpression, DESCENDING);
        }
        else
        {
            GridViewSortDirection = SortDirection.Ascending;
            SortGridView(sortExpression, ASCENDING);
        }
    }

    public SortDirection GridViewSortDirection
    {
        get
        {
            if (ViewState["sortDirection"] == null)
                ViewState["sortDirection"] = SortDirection.Ascending;

            return (SortDirection)ViewState["sortDirection"];
        }
        set { ViewState["sortDirection"] = value; }
    }
    private void SortGridView(string sortExpression, string direction)
    {
        try
        {
            DataTable dtUser = null;
            TextBox txtPage = (TextBox)pageGrid.BottomPagerRow.Cells[0].FindControl("txtPage");
            if (ViewState["Query"] != null)
            {
                dtUser = Misc.search(ViewState["Query"].ToString());
            }
            else
            {
                dtUser = (DataTable)Session["salesSection"];
                if (dtUser != null && dtUser.Rows.Count > 0)
                {

                    if (dtUser.Rows[0]["UsertypeID"].ToString().Trim() == "1" || dtUser.Rows[0]["UsertypeID"].ToString().Trim() == "2")
                    {
                        dtUser = objBLLDealOrders.ComissionHistoryForAllSalesPersons();

                    }
                    else
                    {
                        Session["SalesPersonAccountName"] = dtUser.Rows[0]["userName"].ToString().Trim();
                        BLLUser objuser = new BLLUser();
                        dtUser = null;
                        dtUser = objBLLDealOrders.ComissionHistoryForSalePerson(Session["SalesPersonAccountName"].ToString().Trim());
                        if (dtUser != null && dtUser.Rows.Count > 0)
                        {
                            dv = new DataView(dtUser);
                        }

                    }
                }
            }

            if (ViewState["ddlPage"] != null && ViewState["ddlPage"].ToString() != "")
            {
                pageGrid.PageSize = Convert.ToInt32(ViewState["ddlPage"]);
            }
            else
            {
                pageGrid.PageSize = 5;
                ViewState["ddlPage"] = 5;
            }

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
            dv = new DataView(dtUser);
            dv.Sort = sortExpression + direction;
            ViewState["Direction"] = sortExpression + direction;
            pageGrid.DataSource = dv;
            pageGrid.DataBind();
            if (dtUser != null && dtUser.Rows.Count > 0)
            {
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
                if (pageGrid.PageCount == 1)
                {
                    ImageButton imgPrev = (ImageButton)pageGrid.BottomPagerRow.FindControl("btnPrev");
                    ImageButton imgNext = (ImageButton)pageGrid.BottomPagerRow.FindControl("btnNext");

                    imgNext.Enabled = false;
                    imgPrev.Enabled = false;
                }
            }
        }
        catch (Exception ex)
        {
            pnlGrid.Visible = true;
            lblMessage.Text = ex.ToString();
            lblMessage.Visible = true;
            imgGridMessage.Visible = true;
            imgGridMessage.ImageUrl = "~/admin/images/error.png";
            lblMessage.ForeColor = System.Drawing.Color.Red;
        }
    }
    #endregion


    public DataTable RemoveDuplicateRows(DataTable dTable, string colName)
    {
        Hashtable hTable = new Hashtable();
        ArrayList duplicateList = new ArrayList();

        //Add list of all the unique item value to hashtable, which stores combination of key, value pair.
        //And add duplicate item value in arraylist.
        foreach (DataRow drow in dTable.Rows)
        {
            if (hTable.Contains(drow[colName]))
                duplicateList.Add(drow);
            else
                hTable.Add(drow[colName], string.Empty);
        }

        //Removing a list of duplicate items from datatable.
        foreach (DataRow dRow in duplicateList)
            dTable.Rows.Remove(dRow);

        //Datatable which contains unique records will be return as output.
        return dTable;
    }
}
