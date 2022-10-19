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

public partial class Takeout_UserControls_GiftCard_memberusedGiftCard : System.Web.UI.UserControl
{
    BLLGiftCard objMemberGiftCard = new BLLGiftCard();
    BLLAdminGiftCard objAdminGiftCard = new BLLAdminGiftCard();
    BLLUser objUser = new BLLUser();
    BLLMemberUsedGiftCards objUseableGiftCards = new BLLMemberUsedGiftCards();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                
                lblMessage.Visible = false;
                imgGridMessage.Visible = false;

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
                if (dtUser != null)
                {
                    ViewState["userID"] = dtUser.Rows[0]["userId"].ToString();
                    bindGrid(Convert.ToInt64(dtUser.Rows[0]["userId"].ToString()));
                }
            }
        }
        catch (Exception ex)
        {
            lblMessage.Visible = true;
            lblMessage.Text = "There is an error occur, please email us at support@tazzling.com or call 1855-295-1771.";
            imgGridMessage.Visible = true;
            imgGridMessage.ImageUrl = "~/images/error.png";
            lblMessage.ForeColor = System.Drawing.Color.Red;
        }
    }

    #region Function to Bind Grid
    protected void bindGrid(long UserID)
    {
        try
        {
            DataTable dtGiftCards;
            DataView dv;
            objUseableGiftCards = new BLLMemberUsedGiftCards();
            objUseableGiftCards.createdBy = UserID;
            dtGiftCards = objUseableGiftCards.getAllUseableCardsByUserID();
            dv = new DataView(dtGiftCards);
            gvGiftCards.PageSize = Misc.clientPageSize;
            ViewState["page"] = Math.Ceiling(Convert.ToDouble(dtGiftCards.Rows.Count) / Convert.ToDouble(gvGiftCards.PageSize)).ToString();
            if (dtGiftCards != null && dtGiftCards.Rows.Count > 0)
            {
                gvGiftCards.DataSource = dv;
                gvGiftCards.DataBind();
            }
            else
            {
                gvGiftCards.DataSource = null;
                gvGiftCards.DataBind();
            }
        }
        catch (Exception ex)
        {
            lblMessage.Visible = true;
            lblMessage.Text = "There is an error occur, please email us at support@tazzling.com or call 1855-295-1771.";
            imgGridMessage.Visible = true;
            imgGridMessage.ImageUrl = "~/images/error.png";
            lblMessage.ForeColor = System.Drawing.Color.Red;
        }
    }
    #endregion


    protected string GetGiftCardImage(string giftCardAmount)
    {
        DataTable dtGiftCard = null;
        if (giftCardAmount != "")
        {
            objAdminGiftCard.cardAmount = float.Parse(giftCardAmount);
            dtGiftCard = objAdminGiftCard.getAdminGiftCardByPrice();
            if (dtGiftCard != null && dtGiftCard.Rows.Count > 0)
            {
                return "<img src='images/giftcard/" + dtGiftCard.Rows[0]["cardImage"] + "' width=\"82\" height=\"50\"/>";
            }
        }
        return "";
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

    protected string GetExpirationDateString(object expirationDate)
    {
        if (expirationDate.ToString() != "")
        {
            DateTime dt = Convert.ToDateTime(expirationDate);
            return dt.ToString("MM-dd-yyyy H.mm tt");
        }
        return "";
    }

    protected string GetCardExplain(string createdBy, string fromId)
    {
        try
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


            string explain = "";
            string username = string.Empty;
            if (fromId != "")
            {
                objUser.userId = Convert.ToInt32(fromId);
                dtUser = objUser.getUserByID();
                explain = "Given By " + dtUser.Rows[0]["firstName"].ToString() + " " + dtUser.Rows[0]["lastName"].ToString();
            }
            else
            {
                explain = "Given By non-registered User";
            }
            return explain;
        }
        catch (Exception ex)
        {
            return "";
        }
    }

    protected void btnNext_Click(object sender, EventArgs e)
    {
        Response.Redirect("giftcard_step1.aspx", false);
    }

    BLLUser objFriendUser = new BLLUser();

    protected void btnVerifyCode_Click(object sender, EventArgs e)
    {
        try
        {
            lblMessage.Visible = false;
            imgGridMessage.Visible = false;

            objMemberGiftCard.giftCardCode =  HtmlRemoval.StripTagsRegexCompiled(txtGiftCardCode.Text.Trim());
            DataTable dtGiftCard = objMemberGiftCard.getApprovedGiftCardByCode();
            if (dtGiftCard != null && dtGiftCard.Rows.Count > 0)
            {
                if (dtGiftCard.Rows[0]["takenBy"].ToString() == "")
                {
                    objUseableGiftCards.createdBy = Convert.ToInt64(ViewState["userID"].ToString());
                    objUseableGiftCards.currencyCode = dtGiftCard.Rows[0]["currencyCode"].ToString();
                    objUseableGiftCards.fromId = Convert.ToInt64(dtGiftCard.Rows[0]["createdBy"].ToString());
                    objUseableGiftCards.gainedAmount = float.Parse(dtGiftCard.Rows[0]["giftCardAmount"].ToString());
                    objUseableGiftCards.remainAmount = float.Parse(dtGiftCard.Rows[0]["giftCardAmount"].ToString());
                    objUseableGiftCards.targetDate = Convert.ToDateTime(dtGiftCard.Rows[0]["expirationDate"].ToString());
                    if (objUseableGiftCards.createMemberUseableGiftCard() != 0)
                    {
                        objMemberGiftCard.takenBy = Convert.ToInt64(ViewState["userID"].ToString());
                        objMemberGiftCard.updateGiftCardTakenByValue();
                        try
                        {
                            objUser.userId = Convert.ToInt32(ViewState["userID"].ToString());
                            DataTable dtUserInfo = objUser.getUserByID();
                            if (dtUserInfo != null && dtUserInfo.Rows.Count > 0 && dtUserInfo.Rows[0]["friendsReferralId"].ToString() == "")
                            {
                                objUser.referralId = dtUserInfo.Rows[0]["referralId"].ToString().Trim();
                                if (objUser.getMemberUserWithNoChildByReferralId())
                                {
                                    objFriendUser.userId = Convert.ToInt32(dtGiftCard.Rows[0]["createdBy"].ToString());
                                    DataTable dtFriendUser = objFriendUser.getUserByID();
                                    if (dtFriendUser != null && dtFriendUser.Rows.Count > 0)
                                    {
                                        objUser.friendsReferralId = dtFriendUser.Rows[0]["referralId"].ToString();
                                        objUser.updateUsersFriendsReferralId();
                                    }
                                }
                            }
                        }
                        catch (Exception ex)
                        { }
                        bindGrid(Convert.ToInt64(ViewState["userID"].ToString()));
                        lblMessage.Visible = true;
                        lblMessage.Text = "Your food credit has been topped up with $" + dtGiftCard.Rows[0]["giftCardAmount"].ToString() + " " + dtGiftCard.Rows[0]["currencyCode"].ToString();
                        imgGridMessage.Visible = true;
                        imgGridMessage.ImageUrl = "~/images/Checked.png";
                        lblMessage.ForeColor = System.Drawing.Color.Black;
                    }
                    else
                    {
                        lblMessage.Visible = true;
                        lblMessage.Text = "Some problem accoured. Please try again.";
                        imgGridMessage.Visible = true;
                        imgGridMessage.ImageUrl = "~/images/error.png";
                        lblMessage.ForeColor = System.Drawing.Color.Red;
                    }
                }
                else
                {
                    lblMessage.Visible = true;
                    lblMessage.Text = "Gift card has been already used.";
                    imgGridMessage.Visible = true;
                    imgGridMessage.ImageUrl = "~/images/error.png";
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                }
            }
            else
            {
                lblMessage.Visible = true;
                lblMessage.Text = "Gift card with this code does not exist.";
                imgGridMessage.Visible = true;
                imgGridMessage.ImageUrl = "~/images/error.png";
                lblMessage.ForeColor = System.Drawing.Color.Red;
            }
        }
        catch (Exception ex)
        {
            lblMessage.Visible = true;
            lblMessage.Text = "There is an error occur, please email us at support@tazzling.com or call 1855-295-1771.";
            imgGridMessage.Visible = true;
            imgGridMessage.ImageUrl = "~/images/error.png";
            lblMessage.ForeColor = System.Drawing.Color.Red;
        }
    }


    public bool displayPrevious = false;
    public bool displayNext = true;
    protected void gvGiftCards_PageIndexChanging(object sender, GridViewPageEventArgs e)
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
            if (e.NewPageIndex == gvGiftCards.PageCount - 1)
            {
                displayNext = false;
            }
            else
            {
                displayNext = true;
            }
            this.gvGiftCards.PageIndex = e.NewPageIndex;
            ViewState["pageText"] = (Convert.ToInt32(e.NewPageIndex) + 1).ToString();
            this.bindGrid(Convert.ToInt64(ViewState["userID"]));
        }
        catch (Exception ex)
        {
            lblMessage.Visible = true;
            lblMessage.Text = "There is an error occur, please email us at support@tazzling.com or call 1855-295-1771.";
            imgGridMessage.Visible = true;
            imgGridMessage.ImageUrl = "~/images/error.png";
            lblMessage.ForeColor = System.Drawing.Color.Red;
        }
    }
    protected void gvGiftCards_RowDataBound(object sender, GridViewRowEventArgs e)
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
            lblMessage.Visible = true;
            lblMessage.Text = "There is an error occur, please email us at support@tazzling.com or call 1855-295-1771.";
            imgGridMessage.Visible = true;
            imgGridMessage.ImageUrl = "~/images/error.png";
            lblMessage.ForeColor = System.Drawing.Color.Red;
        }
    }
    protected void lnkPage_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton pageLink = (LinkButton)sender;
            ViewState["pageText"] = HtmlRemoval.StripTagsRegexCompiled(pageLink.Text.ToString());
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

                this.gvGiftCards.PageIndex = Convert.ToInt32(pageLink.CommandArgument) - 1;

                this.bindGrid(Convert.ToInt64(ViewState["userID"]));
            }
        }
        catch (Exception ex)
        {
            lblMessage.Visible = true;
            lblMessage.Text = "There is an error occur, please email us at support@tazzling.com or call 1855-295-1771.";
            imgGridMessage.Visible = true;
            imgGridMessage.ImageUrl = "~/images/error.png";
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
