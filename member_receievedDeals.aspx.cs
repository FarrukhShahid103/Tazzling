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
using System.Net.Mail;
using System.IO;
using GecLibrary;

public partial class member_receievedDeals : System.Web.UI.Page
{

    BLLDealOrders objOrders = new BLLDealOrders();
    BLLDealOrderDetail objDetail = new BLLDealOrderDetail();
        
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            Page.Title = ConfigurationManager.AppSettings["pageTitleStart"].ToString().Trim() + " | Member | My TastyGos";

            if (!IsPostBack)
            {

                if (Session["member"] != null || Session["restaurant"] != null || Session["sale"] != null || Session["user"] != null)
                {
                    bindGrid();
                }
                else
                {
                    Response.Redirect("Default.aspx", false);
                }
            }
        }
        catch (Exception ex)
        {
            lblMessage.Visible = true;
            lblMessage.Text = "There is an error occur, please email us at support@tazzling.com or call 1855-295-1771.";
            imgGridMessage.Visible = true;
            imgGridMessage.ImageUrl = "images/error.png";
            lblMessage.ForeColor = System.Drawing.Color.Red; 
        }
    }

    protected string GetDateString(object objDate)
    {
        if (objDate.ToString() != "")
        {
            DateTime dt = Convert.ToDateTime(objDate);
            return dt.ToString("MM-dd-yyyy H.mm tt");
        }
        return "";
    }

    protected string getDealPrintPath(object objCode, object status)
    {
        if (objCode.ToString() != "")
        {
            if (status.ToString().ToLower().Trim() == "pending" || status.ToString().ToLower().Trim() == "cancelled")
            {
                return "#";
            }
            else
            {
                GECEncryption objEnc = new GECEncryption();
                return "Images/ClientData/" + objEnc.DecryptData("deatailOrder", objCode.ToString()) + ".pdf";
            }
        }
        return "";
    }

    protected string getOrderStatus(object status)
    {
        if (status.ToString() != "")
        {
            if (status.ToString().ToLower().Trim() == "pending")
            {
                return "Cancel";
            }
            else if (status.ToString().ToLower().Trim() == "cancelled")
            {
                return "Process";
            }
            else
            {
                return "";
            }
           
        }
        return "";
    }

    protected bool getDealOrderStatus(object status)
    {
        if (status.ToString() != "")
        {
            if (status.ToString().ToLower().Trim() == "pending" || status.ToString().ToLower().Trim() == "cancelled")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        return false;
    }
    
    protected string getDealCode(object objCode, object status)
    {
        if (objCode.ToString() != "")
        {
            if (status.ToString().ToLower().Trim() == "pending" || status.ToString().ToLower().Trim() == "cancelled")
            {
                return "# *******";
            }
            else
            {
                GECEncryption objEnc = new GECEncryption();
                return "# " + objEnc.DecryptData("deatailOrder", objCode.ToString());
            }
        }
        return "";
    }
    protected bool getDetailStatus(object status)
    {
        if (status.ToString() != "")
        {
            if (status.ToString().ToLower().Trim() == "pending" || status.ToString().ToLower().Trim() == "cancelled")
            {
                return false;
            }
            else
            {                
                return true;
            }
        }
        return false;
    }
    

    protected string getDealStatus(object objStatus)
    {
        if (objStatus.ToString() != "")
        {
            try
            {
                if (Convert.ToBoolean(objStatus))
                {
                    return "Undo";
                }
                else
                {
                    return "Mark as used";
                }
                
            }
            catch (Exception ex)
            {
                return "Mark as used";
            }
        }
        return "Mark as used";
    }
      
    protected string imagePath(object objImage, object restaurantId)
    {
        if (objImage.ToString() != "")
        {
            string[] strDealImages = objImage.ToString().Split(',');
            int i = 0;
            bool imageFound = false;
            if (strDealImages.Length > 0)
            {
                for (i = 0; i < strDealImages.Length; i++)
                {
                    if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + "Images\\dealfood\\" + restaurantId.ToString().Trim() + "\\" + strDealImages[i]))
                    {
                        imageFound = true;
                        break;
                    }
                }
            }
            if (imageFound)
            {
                return "Images/dealfood/" + restaurantId.ToString().Trim() + "/" + strDealImages[i];
            }
            else
            {
                return "Images/imageNotFound.jpg";
            }
        }
        else
        {
            return "Images/imageNotFound.jpg";
        }

    }
    
    #region Function to Bind Grid
    protected void bindGrid()
    {
        try
        {
            if (Session["member"] != null || Session["restaurant"] != null || Session["sale"] != null || Session["user"] != null)
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
                    objDetail.isGiftCapturedId = Convert.ToInt64(dtUser.Rows[0]["userId"].ToString());
                   // objOrders.createdBy = Convert.ToInt64(dtUser.Rows[0]["userId"].ToString());
                    DataTable dtOrders;
                    DataView dv;
                    gridview1.PageSize = Misc.clientPageSize;
                    dtOrders = objDetail.getUserReceivedDealsByUserID();
                    dv = new DataView(dtOrders);
                    ViewState["page"] = Math.Ceiling(Convert.ToDouble(dtOrders.Rows.Count) / Convert.ToDouble(gridview1.PageSize)).ToString();
                    if (dtOrders != null && dtOrders.Rows.Count > 0)
                    {
                        gridview1.DataSource = dv;
                        gridview1.DataBind();
                    }
                    else
                    {
                        gridview1.DataSource = null;
                        gridview1.DataBind();
                    }
                }
                else                
                {
                    Response.Redirect("Default.aspx",false);
                }
               
            }

          
        }
        catch (Exception ex)
        {
            lblMessage.Visible = true;
            lblMessage.Text = "There is an error occur, please email us at support@tazzling.com or call 1855-295-1771.";
            imgGridMessage.Visible = true;
            imgGridMessage.ImageUrl = "images/error.png";
            lblMessage.ForeColor = System.Drawing.Color.Red;
        }
    }
    #endregion
                 
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
            this.bindGrid();
        }
        catch (Exception ex)
        {
            lblMessage.Visible = true;
            lblMessage.Text = "There is an error occur, please email us at support@tazzling.com or call 1855-295-1771.";
            imgGridMessage.Visible = true;
            imgGridMessage.ImageUrl = "images/error.png";
            lblMessage.ForeColor = System.Drawing.Color.Red;
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

            //if (e.Row.RowType == DataControlRowType.DataRow)
            //{
            //    GridView gvSubItem = (GridView)e.Row.FindControl("gvSubItem");

            //    Label lblID = (Label)e.Row.FindControl("lblId");
            //    if (lblID != null && lblID.Text.ToString() != "")
            //    {
            //        objDetail.dOrderID = Convert.ToInt64(lblID.Text.ToString());
            //        gvSubItem.DataSource = objDetail.getAllUserDealOrderDetailByOrderID().DefaultView;
            //        gvSubItem.DataBind();
            //    }
            //}
        }
        catch (Exception ex)
        {
            lblMessage.Visible = true;
            lblMessage.Text = "There is an error occur, please email us at support@tazzling.com or call 1855-295-1771.";
            imgGridMessage.Visible = true;
            imgGridMessage.ImageUrl = "images/error.png";
            lblMessage.ForeColor = System.Drawing.Color.Red;
        }
    }


    protected void gridview1_Login(object sender, GridViewCommandEventArgs e)
    {

        //int value = Convert.ToInt32(e.CommandArgument);
        bool result = false;
        try
        {
            objDetail.detailID = Convert.ToInt64(e.CommandArgument);

            if (((LinkButton)e.CommandSource).Text.Trim() == "Mark as used")
            {
                objDetail.markUsed = true;
            }
            else
            {
                objDetail.markUsed = false;
            }
            result = objDetail.changeOrderDetailStatus();
            if (result)
            {


                bindGrid();
                lblMessage.Text = "Status has been changed successfully.";
                lblMessage.Visible = true;
                imgGridMessage.Visible = true;
                imgGridMessage.ImageUrl = "images/Checked.png";
                lblMessage.ForeColor = System.Drawing.Color.Black;

            }
            else
            {

                bindGrid();
                lblMessage.Text = "Status has not been changed successfully.";
                lblMessage.Visible = true;
                imgGridMessage.Visible = true;
                imgGridMessage.ImageUrl = "images/error.png";
                lblMessage.ForeColor = System.Drawing.Color.Red;
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

    protected void gvSubItem_Login(object sender, GridViewCommandEventArgs e)
    {

        //int value = Convert.ToInt32(e.CommandArgument);
        bool result = false;
        try
        {
            objDetail.detailID = Convert.ToInt64(e.CommandArgument);

            if (((LinkButton)e.CommandSource).Text.Trim() == "Mark as used")
            {
                objDetail.markUsed = true;
            }
            else
            {
                objDetail.markUsed = false;
            }
            result = objDetail.changeOrderDetailStatus();
            if (result)
            {


                bindGrid();
                lblMessage.Text = "Status has been changed successfully.";
                lblMessage.Visible = true;
                imgGridMessage.Visible = true;
                imgGridMessage.ImageUrl = "images/Checked.png";
                lblMessage.ForeColor = System.Drawing.Color.Black;

            }
            else
            {

                bindGrid();
                lblMessage.Text = "Status has not been changed successfully.";
                lblMessage.Visible = true;
                imgGridMessage.Visible = true;
                imgGridMessage.ImageUrl = "images/error.png";
                lblMessage.ForeColor = System.Drawing.Color.Red;
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

                this.bindGrid();
            }
        }
        catch (Exception ex)
        {
            lblMessage.Visible = true;
            lblMessage.Text = "There is an error occur, please email us at support@tazzling.com or call 1855-295-1771.";
            imgGridMessage.Visible = true;
            imgGridMessage.ImageUrl = "images/error.png";
            lblMessage.ForeColor = System.Drawing.Color.Red;
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
