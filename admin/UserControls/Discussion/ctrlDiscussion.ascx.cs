using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;

public partial class UserControls_Discussion_ctrlDiscussion : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            try
            {
                if (Request.QueryString["did"] != null)
                {
                    int iDealId = int.Parse(Request.QueryString["did"].ToString().Trim());

                    this.hfDealId.Value = Request.QueryString["did"].ToString().Trim();

                    //Get And Set Posts By Deal Id
                    GetAndSetPostsByDealId(iDealId);

                    SetFeilds(0);
                }
                else
                {
                    Response.Redirect(ResolveUrl("Default.aspx"), false);
                }
            }
            catch (Exception ex)
            {
                Response.Redirect(ResolveUrl("Default.aspx"), false);
            }
        }
    }

    #region "Check user Is Logged In or not"

    private void SetFeilds(int set)
    {
        try
        {
            if (set==1)
            {
                this.txtComment.Enabled = true;

                this.btnPost.Visible = true;

                this.btnCancel.Visible = true;
            }
            else
            {
                ViewState["DiscussionId"] = null;
                txtComment.Text = "";
                this.txtComment.Enabled = false;

                this.btnPost.Visible = false;

                this.btnCancel.Visible = false;
            }
        }
        catch (Exception ex)
        {
            string strException = "There is an error occur, please email us at support@tazzling.com or call 1855-295-1771.";
        }
    }

    #endregion

    protected void btnPost_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            if (ViewState["DiscussionId"] != null)
            {
                int iDealId = int.Parse(this.hfDealId.Value);
                BLLDealDiscussion obj = new BLLDealDiscussion();
                obj.Comments = txtComment.Text.Trim().Replace("\n", "<br>");
                obj.DiscussionId = Convert.ToInt64(ViewState["DiscussionId"].ToString());
                obj.updateDealDiscussionInfoByDiscussionId();
                SetFeilds(0);
                //Get All the Posts here By Deal Id
                GetAndSetPostsByDealId(iDealId);
                lblMessage.Visible = true;
                lblMessage.Text = "Comment update successfully.";

                imgGridMessage.Visible = true;
                imgGridMessage.ImageUrl = "~/Images/checked.png";
                
             
            }
        }
        catch (Exception ex)
        {
            lblMessage.Visible = true;
            lblMessage.Text = "There is an error occur, please email us at support@tazzling.com or call 1855-295-1771.";

            imgGridMessage.Visible = true;
            imgGridMessage.ImageUrl = "~/Images/error.png";

            lblMessage.ForeColor = System.Drawing.Color.Red;
        }
    }

    protected void btnCancel_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            SetFeilds(0);
        }
        catch (Exception ex)
        {
            lblMessage.Visible = true;
            lblMessage.Text = "There is an error occur, please email us at support@tazzling.com or call 1855-295-1771.";

            imgGridMessage.Visible = true;
            imgGridMessage.ImageUrl = "~/Images/error.png";

            lblMessage.ForeColor = System.Drawing.Color.Red;
        }
    }
       
    private void GetAndSetPostsByDealId(int iDealId)
    {
        try 
        {
            BLLDealDiscussion objBLLDealDiscussion = new BLLDealDiscussion();

            objBLLDealDiscussion.DealId = iDealId;

            DataTable dtPosts = objBLLDealDiscussion.getDealForAdminDiscussionByDealId();

            if ((dtPosts != null) && (dtPosts.Rows.Count > 0))
            {
                this.rptrDiscussion.DataSource = dtPosts;
                this.rptrDiscussion.DataBind();
            }
        }
        catch (Exception ex)
        {
            lblMessage.Visible = true;
            lblMessage.Text = "There is an error occur, please email us at support@tazzling.com or call 1855-295-1771.";

            imgGridMessage.Visible = true;
            imgGridMessage.ImageUrl = "~/Images/error.png";

            lblMessage.ForeColor = System.Drawing.Color.Red;
        }
    }

    BLLUser objUser = new BLLUser();

    protected void DataListItemDataBound(Object src, DataListItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            ImageButton btnDelete = (ImageButton)e.Item.FindControl("Delete");
            Button BtnHidden = (Button)e.Item.FindControl("BtnHidden");
            HiddenField hfEmail = (HiddenField)e.Item.FindControl("hfEmail");
            btnDelete.OnClientClick = "return RunPopup('" + BtnHidden.ClientID + "', '" + hfEmail.Value + "')";                


            Image imgDis = (Image)e.Item.FindControl("imgDis");

            //if (Session["FBImage"] == null)
            //{
            HiddenField hfUserID = (HiddenField)e.Item.FindControl("hfUserID");
            objUser.userId = Convert.ToInt32(hfUserID.Value);
            DataTable dtUserInfo = objUser.getUserByID();

            string strFileName = AppDomain.CurrentDomain.BaseDirectory + "images\\ProfilePictures\\" + imgDis.ImageUrl.Trim().Trim();
            if (File.Exists(strFileName))
            {
                ViewState["PicName"] = imgDis.ImageUrl.Trim().Trim();
                imgDis.ImageUrl = "~/images/ProfilePictures/" + imgDis.ImageUrl.Trim().Trim();
            }
            else if (dtUserInfo != null && dtUserInfo.Rows.Count > 0 && (dtUserInfo.Rows[0]["FB_userID"].ToString().Trim() != ""))
            {
                imgDis.ImageUrl = "https://graph.facebook.com/" + dtUserInfo.Rows[0]["FB_userID"].ToString().Trim() + "/picture";
            }
            else
            {
                imgDis.ImageUrl = "~/Images/disImg.gif";
            }



        }
    }

    protected void Edit_Command(Object sender, DataListCommandEventArgs e)
    {
        BLLDealDiscussion obj = new BLLDealDiscussion();
        obj.DiscussionId = Convert.ToInt64(e.CommandArgument.ToString());
        DataTable dtDiscuess = obj.getDealDiscussionByDiscussionId();
        if (dtDiscuess != null && dtDiscuess.Rows.Count > 0)
        {
            SetFeilds(1);
            ViewState["DiscussionId"] = e.CommandArgument.ToString();
            txtComment.Text = dtDiscuess.Rows[0]["comments"].ToString().Replace("<br>","\n");
        }
    }

    protected void Delete_Command(Object sender, DataListCommandEventArgs e)
    {
        try
        {
            BLLDealDiscussion obj = new BLLDealDiscussion();
            obj.DiscussionId = Convert.ToInt64(e.CommandArgument.ToString());
            obj.deleteDealDiscussionByDiscussionId();
            GetAndSetPostsByDealId( Convert.ToInt32(this.hfDealId.Value));

            SetFeilds(0);
            lblMessage.Visible = true;
            lblMessage.Text = "Comment delete successfully.";

            imgGridMessage.Visible = true;
            imgGridMessage.ImageUrl = "~/Images/checked.png";  
          
        }
        catch (Exception ex)
        {
            lblMessage.Visible = true;
            lblMessage.Text = "There is an error occur, please email us at support@tazzling.com or call 1855-295-1771.";

            imgGridMessage.Visible = true;
            imgGridMessage.ImageUrl = "~/Images/error.png";

            lblMessage.ForeColor = System.Drawing.Color.Red;
        }
    }

}