using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Data;

public partial class admin_UserControls_ctrlResFeaturedFood : System.Web.UI.UserControl
{
    BLLFeaturedFoods objBLLFeaturedFoods = new BLLFeaturedFoods();
    BLLRestaurant objResturantInfo = new BLLRestaurant();

    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);        

        if (!IsPostBack)
        {
            //Get the Admin User Session here
            if (Session["user"] != null)
            {
                //Get the Admin Sesssion here
                DataTable dtUser = (DataTable)Session["user"];
                //Get the Admin Logged In UserID
                ViewState["userID"] = dtUser.Rows[0]["userID"];

                //Show the Drop Down List in Case of Admin User
                //Show the Table Row here
                this.trSelectRes.Visible = true;
                GetAndSetRestaurantInfo();

                //Set the Restaurant session to null if it exists
                if (Session["restaurant"] != null) Session["restaurant"] = null;

                //Show the Admin button with Black Back-Ground here
                this.btnSave.Visible = false;
                this.btnImgSave.Visible = true;

                this.gridViewMenus.HeaderStyle.CssClass = "gridHeader";
                this.gridViewMenus.HeaderStyle.ForeColor = System.Drawing.Color.White;

                //Get & Set the Featured Food here
                BindFeaturedFoods();
            }
            //Get the Restaurant User Session here
            else if (Session["restaurant"] != null)
            {
                DataTable dtUser = (DataTable)Session["restaurant"];
                ViewState["userID"] = dtUser.Rows[0]["userID"];
                ViewState["restaurantID"] = dtUser.Rows[0]["restaurantId"];

                //Show the Drop Down List in Case of Admin User
                //Show the Table Row here
                this.trSelectRes.Visible = false;

                //Show the User button with Orange Back-Ground here
                this.btnSave.Visible = true;
                this.btnImgSave.Visible = false;

                //Get & Set the Featured Food here
                BindFeaturedFoods();
            }
            else
            {
                Response.Redirect(ResolveUrl("~/takeout/default.aspx"), false);
            }

        }
    }

    #region "Get & Set Restaurant Management"

    private void GetAndSetRestaurantInfo()
    {
        try
        {
            BLLRestaurant objBLLRestaurant = new BLLRestaurant();

            DataTable dtResInfo = objBLLRestaurant.getAllResturantsForAdmin();

            if ((dtResInfo != null) && (dtResInfo.Rows.Count > 0))
            {
                DataView dv = new DataView(dtResInfo);
                dv.Sort = "restaurantName asc";
                
                this.ddlSelectRes.DataTextField = "restaurantName";
                this.ddlSelectRes.DataValueField = "restaurantId";
                this.ddlSelectRes.DataSource = dv;
                this.ddlSelectRes.DataBind();
            }
        }
        catch (Exception ex)
        { string strException = "There is an error occur, please email us at support@tazzling.com or call 1855-295-1771."; }
    }

    #endregion

    private void BindFeaturedFoods()
    {
        try
        {
            DataTable dtFeaturedFoods;
            objBLLFeaturedFoods.createdBy = Convert.ToInt64(ViewState["userID"]);
            dtFeaturedFoods = objBLLFeaturedFoods.getFeaturedFoodByCreatedByID();

            if ((dtFeaturedFoods != null) && (dtFeaturedFoods.Rows.Count > 0))
            {
                gridViewMenus.DataSource = dtFeaturedFoods.DefaultView;
                gridViewMenus.DataBind();

                this.gridViewMenus.Visible = true;
            }
            else
            {
                this.gridViewMenus.Visible = false;
            }
        }
        catch (Exception ex)
        {
            lblMessage.Text = "There is an error occur, please email us at support@tazzling.com or call 1855-295-1771.";
            lblMessage.Visible = true;
            imgGridMessage.Visible = true;
            imgGridMessage.ImageUrl = "~/Images/error.png"; lblMessage.ForeColor = System.Drawing.Color.Red;
        }
    }

    protected void gridViewMenus_CancelCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
    {
        //gridViewMenus.ShowFooter = true;
        gridViewMenus.EditItemIndex = -1;
        BindFeaturedFoods();
    }

    protected void gridViewMenus_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
    {

        if (e.CommandName == "DeleteItems")
        {
            Label lblFeaturedFoodID = (Label)e.Item.FindControl("lblFeaturedFoodID");
            if (lblFeaturedFoodID != null)
            {
                objBLLFeaturedFoods.featuredFoodID = Convert.ToInt64(lblFeaturedFoodID.Text.Trim());
                objBLLFeaturedFoods.DeleteFeaturedFoodByFoodID();
                BindFeaturedFoods();
                lblMessage.Text = "Featured food has been deleted successfully.";
                lblMessage.Visible = true;
                imgGridMessage.Visible = true;
                imgGridMessage.ImageUrl = "~/Images/Checked.png"; lblMessage.ForeColor = System.Drawing.Color.Black;
            }
        }
    }

    protected void gridViewMenus_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
    {
        Control container = e.Item;
        ListItemType itemType = e.Item.ItemType;

        if (itemType == ListItemType.Item || itemType == ListItemType.Footer || itemType == ListItemType.AlternatingItem)
        {
            if (e.Item.DataItem == null)
            {
                return;
            }

            HtmlImage btnExpandButton = (HtmlImage)container.FindControl("image_");
            if (btnExpandButton != null)
            {
                btnExpandButton.Attributes.Add("OnClick", "Toggle('ctl00_ContentPlaceHolder1_gridViewMenus_ctl0" + (e.Item.ItemIndex + 2) + "_divOrders', 'ctl00_ContentPlaceHolder1_gridViewMenus_ctl0" + (e.Item.ItemIndex + 2) + "_image_');");
            }            
        }

        if (itemType == ListItemType.EditItem)
        {
            DropDownList ddlUpdateSelectRes = (DropDownList)e.Item.FindControl("ddlUpdateSelectRes");
            //Set the Restaurant Info here
            BLLRestaurant objBLLRestaurant = new BLLRestaurant();
            //Set the Restaurant Info to DataTable here
            DataTable dtResInfo = objBLLRestaurant.getAllResturantsForAdmin();

            if ((dtResInfo != null) && (dtResInfo.Rows.Count > 0))
            {
                DataView dv = new DataView(dtResInfo);
                dv.Sort = "restaurantName asc";

                ddlUpdateSelectRes.DataTextField = "restaurantName";
                ddlUpdateSelectRes.DataValueField = "restaurantId";
                ddlUpdateSelectRes.DataSource = dv;
                ddlUpdateSelectRes.DataBind();

                ddlUpdateSelectRes.SelectedValue = ViewState["ResID"].ToString();
            }
        }


        LinkButton lbUpdateItem = (LinkButton)e.Item.FindControl("lbUpdateItem");
        if (lbUpdateItem != null)
        {
            TextBox txtUpdateFoodName = (TextBox)e.Item.FindControl("txtUpdateFoodName");
            TextBox txtUpdateDescription = (TextBox)e.Item.FindControl("txtUpdateDescription");
            TextBox txtUpdateFoodPrice = (TextBox)e.Item.FindControl("txtUpdateFoodPrice");
            string strArguments = "'" + txtUpdateFoodName.ClientID + "','" + txtUpdateDescription.ClientID + "','"
            + txtUpdateFoodPrice.ClientID + "'";
            
            lbUpdateItem.Attributes.Add("onclick", "return ValidateFoodFields(" + strArguments + ");");
        }
    }

    protected void gridViewMenus_EditCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
    {
        try
        {
            Label lblFeaturedFoodID = (Label)e.Item.FindControl("lblFeaturedFoodID");
            if (lblFeaturedFoodID != null)
            {
                HiddenField hfResID = (HiddenField)e.Item.FindControl("hfResID");

                //gridViewItems.ShowFooter = false;
                gridViewMenus.EditItemIndex = -1;
                if (lblFeaturedFoodID.Text == e.Item.Cells[0].Text)
                {
                    if (null != gridViewMenus)
                    {
                        gridViewMenus.EditItemIndex = e.Item.ItemIndex;
                        ViewState["ResID"] = hfResID.Value;
                    }
                }
            }
            BindFeaturedFoods();
        }
        catch (Exception ex)
        {
            lblMessage.Text = "There is an error occur, please email us at support@tazzling.com or call 1855-295-1771.";
            lblMessage.Visible = true;
            imgGridMessage.Visible = true;
            imgGridMessage.ImageUrl = "~/Images/error.png"; lblMessage.ForeColor = System.Drawing.Color.Red;
        }
    }

    public string getImagePath(object imgName)
    {
        try
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + "takeout\\MenuImages\\FeaturedFood\\" + ViewState["userID"] + "\\images\\" + imgName;
            if (File.Exists(path))
            {
                return "../MenuImages/FeaturedFood/" + ViewState["userID"] + "/Images/" + imgName.ToString();
            }
            else
            {
                return "../MenuImages/FeaturedFood/noMenuImage.gif";
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
    }

    protected void gridViewMenus_UpdateCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
    {
        try
        {
            if (ListItemType.EditItem == e.Item.ItemType)
            {
                Label lblFeaturedFoodID = (Label)e.Item.FindControl("lblFeaturedFoodID");
                String FeaturedFoodId = lblFeaturedFoodID.Text.Trim();

                TextBox txtUpdateFoodName = (TextBox)e.Item.FindControl("txtUpdateFoodName");
                TextBox txtUpdateDescription = (TextBox)e.Item.FindControl("txtUpdateDescription");
                DropDownList ddlUpdateSelectRes = (DropDownList)e.Item.FindControl("ddlUpdateSelectRes");
                TextBox txtUpdateFoodPrice = (TextBox)e.Item.FindControl("txtUpdateFoodPrice");

                if (txtUpdateFoodName != null && txtUpdateDescription != null && txtUpdateFoodPrice != null)
                {
                    objBLLFeaturedFoods.featuredFoodID = Convert.ToInt64(FeaturedFoodId);
                    objBLLFeaturedFoods.foodName = txtUpdateFoodName.Text.Trim();
                    objBLLFeaturedFoods.foodDescription = txtUpdateDescription.Text.Trim();
                    objBLLFeaturedFoods.foodPrice = float.Parse(txtUpdateFoodPrice.Text.Trim());
                    objBLLFeaturedFoods.restaurantID = int.Parse(ddlUpdateSelectRes.SelectedItem.Value);

                    objBLLFeaturedFoods.UpdateFeaturedFoodByFoodID();

                    //Hide the Edit portion
                    gridViewMenus.EditItemIndex = -1;
                }

                BindFeaturedFoods();
                lblMessage.Text = "Featured food has been updated successfully.";
                lblMessage.Visible = true;
                imgGridMessage.Visible = true;
                imgGridMessage.ImageUrl = "~/Images/Checked.png"; lblMessage.ForeColor = System.Drawing.Color.Black;
            }
        }
        catch (Exception ex)
        {
            lblMessage.Text = "There is an error occur, please email us at support@tazzling.com or call 1855-295-1771.";
            lblMessage.Visible = true;
            imgGridMessage.Visible = true;
            imgGridMessage.ImageUrl = "~/Images/error.png"; lblMessage.ForeColor = System.Drawing.Color.Red;
        }
    }

    protected void btnChange_Click(object sender, EventArgs e)
    {
        try
        {
            if (fpChangeFoodImage.HasFile)
            {
                string foodType = hidFoodTypeId.Text.Trim();
                string[] strExtension = fpChangeFoodImage.FileName.Split('.');
                string strSrcPath = AppDomain.CurrentDomain.BaseDirectory + "Takeout\\MenuImages\\FeaturedFood\\" + fpChangeFoodImage.FileName;
                fpChangeFoodImage.SaveAs(strSrcPath);
                string strthumbSave = AppDomain.CurrentDomain.BaseDirectory + "Takeout\\MenuImages\\FeaturedFood\\" + ViewState["userID"] + "\\images\\";
                string SrcFileName = fpChangeFoodImage.FileName;

                Misc.CreateThumbnailFeaturedFood(strSrcPath, strthumbSave, SrcFileName, foodType);

                BindFeaturedFoods();
            }
        }
        catch (Exception ex)
        {
            lblMessage.Text = "There is an error occur, please email us at support@tazzling.com or call 1855-295-1771.";
            lblMessage.Visible = true;
            imgGridMessage.Visible = true;
            imgGridMessage.ImageUrl = "~/Images/error.png"; lblMessage.ForeColor = System.Drawing.Color.Red;
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        ButtonClickFunction();
    }

    protected void btnImgSave_Click(object sender, ImageClickEventArgs e)
    {
        ButtonClickFunction();
    }

    private void ButtonClickFunction()
    {
        try
        {
            if (fpFoodImage.HasFile)
            {
                string[] strExtension = fpFoodImage.FileName.Split('.');
                string strUniqueID = Guid.NewGuid().ToString() + "." + strExtension[strExtension.Length - 1];
                string strSrcPath = AppDomain.CurrentDomain.BaseDirectory + "Takeout\\MenuImages\\FeaturedFood\\" + fpFoodImage.FileName;
                fpFoodImage.SaveAs(strSrcPath);
                string strthumbSave = AppDomain.CurrentDomain.BaseDirectory + "Takeout\\MenuImages\\FeaturedFood\\" + ViewState["userID"] + "\\images\\";
                if (!Directory.Exists(strthumbSave))
                {
                    Directory.CreateDirectory(strthumbSave);
                }
                string SrcFileName = fpFoodImage.FileName;

                Misc.CreateThumbnailFeaturedFood(strSrcPath, strthumbSave, SrcFileName, strUniqueID);
                objBLLFeaturedFoods.restaurantID = ViewState["restaurantID"] != null ? Convert.ToInt64(ViewState["restaurantID"]) : Convert.ToInt64(this.ddlSelectRes.SelectedItem.Value.ToString().Trim());
                objBLLFeaturedFoods.foodName = this.txtFoodName.Text.Trim();
                objBLLFeaturedFoods.foodDescription = this.txtFoodDescription.Text.Trim();
                objBLLFeaturedFoods.foodPrice = float.Parse(txtFoodPrice.Text.Trim());
                objBLLFeaturedFoods.foodImage = strUniqueID;
                objBLLFeaturedFoods.creationDate = DateTime.Now;
                objBLLFeaturedFoods.createdBy = Convert.ToInt64(ViewState["userID"]);
                objBLLFeaturedFoods.modifiedDate = DateTime.Now;
                objBLLFeaturedFoods.modifiedBy = Convert.ToInt64(ViewState["userID"]);

                bool bChkAdmin = false; //if true means that Admin is Logged in otherwise restaurant
                if (Session["restaurant"] != null)
                { bChkAdmin = false; }
                else if (Session["user"] != null)
                { bChkAdmin = true; }

                objBLLFeaturedFoods.foodAddedBy = bChkAdmin;

                long MenuID = objBLLFeaturedFoods.createNewFeaturedFood();
                if (MenuID != 0)
                {
                    this.txtFoodName.Text = "";
                    this.txtFoodDescription.Text = "";
                    this.txtFoodPrice.Text = "";
                    this.txtFoodPrice.Text = "";
                    lblMessage.Text = "Food has been added successfully.";
                    lblMessage.Visible = true;
                    imgGridMessage.Visible = true;
                    imgGridMessage.ImageUrl = "~/Images/Checked.png"; lblMessage.ForeColor = System.Drawing.Color.Black;
                }
            }
            //Bind the Featured Food Items here
            BindFeaturedFoods();
        }
        catch (Exception ex)
        {
            lblMessage.Text = "There is an error occur, please email us at support@tazzling.com or call 1855-295-1771.";
            lblMessage.Visible = true;
            imgGridMessage.Visible = true;
            imgGridMessage.ImageUrl = "~/Images/error.png"; lblMessage.ForeColor = System.Drawing.Color.Red;
        }
    }

}
