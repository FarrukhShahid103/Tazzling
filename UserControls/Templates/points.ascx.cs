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
using System.Text;

public partial class Takeout_UserControls_Templates_points : System.Web.UI.UserControl
{

    BLLMemberPoints obj = new BLLMemberPoints();
    BLLMemberPointGiftsRequests objUsedPoints = new BLLMemberPointGiftsRequests();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                if (Session["member"] != null || Session["restaurant"] != null || Session["sale"] != null || Session["user"]!=null)
                {
                    DataTable dtUser = null;
                    if (Session["member"] != null)
                    {
                        dtUser = (DataTable)Session["member"];
                    }
                    else if (Session["restaurant"] != null)
                    {
                        dtUser = (DataTable)Session["restaurant"];
                    }
                    else if (Session["sale"] != null)
                    {
                        dtUser = (DataTable)Session["sale"];
                    }
                    else if (Session["user"] != null)
                    {
                        dtUser = (DataTable)Session["user"];
                    }
     
                    if (dtUser != null && dtUser.Rows.Count > 0)
                    {
                        ViewState["userId"] = dtUser.Rows[0]["userId"].ToString();
                        BindGrid(Convert.ToInt32(ViewState["userId"]));

                        obj = new BLLMemberPoints();
                        objUsedPoints = new BLLMemberPointGiftsRequests();


                        objUsedPoints.createdBy = Convert.ToInt64(ViewState["userId"]);
                        obj.userID = Convert.ToInt64(ViewState["userId"]);
                        string strUserPoints = obj.getTotalPointsByUserID().Rows[0][0].ToString().Trim();
                        string strUsedPoints = objUsedPoints.getTotalUsedPointsByUserID().Rows[0][0].ToString().Trim();
                        if (strUserPoints != "")
                        {
                            if (strUsedPoints != "")
                            {
                                lblPointsDetail.Text  = "You have "+  Convert.ToString(long.Parse(strUserPoints) - long.Parse(strUsedPoints))+" points.";

                            }
                            else
                            {
                                lblPointsDetail.Text = "You have " + strUserPoints + " points.";
                            }
                        }
                        else
                        {
                            lblPointsDetail.Text = "You have 0 point.";
                        }



                    }                    
                }
                else
                {
                    Response.Redirect("opportunity.aspx", false);
                }
            }
        }
        catch (Exception ex)
        {
            Response.Redirect("opportunity.aspx", false);
        }
    }

    protected void BindGrid(int userId)
    {
        obj.userID = userId;
        DataTable dtWithdraw = obj.spGetAllMemberPointsByUserID();
        gridview1.PageSize = Misc.clientPageSize;
        ViewState["page"] = Math.Ceiling(Convert.ToDouble(dtWithdraw.Rows.Count) / Convert.ToDouble(gridview1.PageSize)).ToString();
        gridview1.DataSource = dtWithdraw.DefaultView;
        gridview1.DataBind();
    }    

    protected string GetExpirationDateString(object expirationDate)
    {
        if (expirationDate.ToString() != "")
        {
            DateTime dt = Convert.ToDateTime(expirationDate);
            return dt.ToString("MM-dd-yyyy H.mm tt");
        }
        return "";
    } 

    public bool displayPrevious = false;
    public bool displayNext = true;
    protected void gridview1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            if (e.NewPageIndex == 0)
            {
                displayPrevious = false;
            }
            else
            {
                displayPrevious = true;
            }
            if (e.NewPageIndex == gridview1.PageCount - 1)
            {
                displayNext = false;
            }
            else
            {
                displayNext = true;
            }
            this.gridview1.PageIndex = e.NewPageIndex;
            ViewState["pageText"] = (Convert.ToInt32(e.NewPageIndex) + 1).ToString();
            this.BindGrid(Convert.ToInt32(ViewState["userId"]));
        }
        catch (Exception ex)
        {

        }
    }
    protected void gridview1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            DataTable pageSize = new DataTable("pager");
            DataColumn pageNo = new DataColumn("pageNo");
            pageSize.Columns.Add(pageNo);

            if (e.Row.RowType == DataControlRowType.Pager)
            {
                Repeater rptrPager = (Repeater)e.Row.FindControl("rptrPage");

                if (ViewState["page"] != null)
                {
                    int count = Convert.ToInt32(ViewState["page"]);
                    for (int i = 0; i < count; i++)
                    {
                        DataRow drNewRow = pageSize.NewRow();
                        drNewRow["pageNo"] = (i + 1).ToString();
                        pageSize.Rows.Add(drNewRow);
                    }
                    rptrPager.DataSource = pageSize;
                    rptrPager.DataBind();
                }
            }
        }
        catch (Exception ex)
        {
            
        }
    }
    protected void lnkPage_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton pageLink = (LinkButton)sender;
            ViewState["pageText"] = pageLink.Text.ToString();
            if (pageLink.CommandName == "Page")
            {
                if (Convert.ToInt32(pageLink.CommandArgument) - 1 == 0)
                {
                    displayPrevious = false;
                }
                else
                {
                    displayPrevious = true;
                }
                if (Convert.ToInt32(pageLink.CommandArgument) == Convert.ToInt32(ViewState["page"]))
                {
                    displayNext = false;
                }
                else
                {
                    displayNext = true;
                }

                this.gridview1.PageIndex = Convert.ToInt32(pageLink.CommandArgument) - 1;

                this.BindGrid(Convert.ToInt32(ViewState["userId"]));
            }
        }
        catch (Exception ex)
        {
            
        }
    }
    protected System.Drawing.Color GetColor(object objPageNum)
    {
        string pageNum = objPageNum.ToString();
        string selectedPageNum = "";
        if (ViewState["pageText"] != null)
        {
            selectedPageNum = ViewState["pageText"].ToString();
        }
        else
        {
            ViewState["pageText"] = 1;
            selectedPageNum = 1.ToString();
        }

        if (pageNum == selectedPageNum)
        {
            return System.Drawing.Color.FromArgb(255, 163, 112);
        }
        else
        {
            return System.Drawing.Color.FromArgb(38, 145, 191);
        }
    }
    protected bool GetStatus(object objPageNum)
    {
        string pageNum = objPageNum.ToString();
        string selectedPageNum = "";
        if (ViewState["pageText"] != null)
        {
            selectedPageNum = ViewState["pageText"].ToString();
        }
        else
        {
            ViewState["pageText"] = 1;
            selectedPageNum = 1.ToString();
        }

        if (pageNum == selectedPageNum)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
      
}
