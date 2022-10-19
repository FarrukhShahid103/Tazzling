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

public partial class dealVerificationDetail : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["did"] != null && Request.QueryString["did"].Trim() != "")
            {
                ResetControls();
                GetAndShowDealInfoByDealId(Request.QueryString["did"].Trim());
            }
        }
    }
    
    private void ResetControls()
    {
        ddlPostDealVerification.SelectedIndex = 0;
        ddlPreDealVerification.SelectedIndex = 0;
        lblAlternateEmailAddress.Text = "";
        lblBusinessEmailAddress.Text = "";
        lblBusinessName.Text = "";
        lblBusinessName.Text = "";
        lblOwnerFirstName.Text = "";
        lblOwnerLastName.Text = "";        
        lblCellNumber.Text = "";
        lblDealModifyBy.Text = "";
        lblDealModifyTime.Text = "";
        lblDealName.Text = "";        
        lblPhoneNumber.Text = "";
    }


    private void GetAndShowDealInfoByDealId(string strIDs)
    {
        try
        {
            BLLUpcommingDeals objBLLDeals = new BLLUpcommingDeals();

            //Set the Deal Id here            

            objBLLDeals.updealId = Convert.ToInt64(strIDs);
            DataTable dtDeals = objBLLDeals.getupCommingDealForDealId();

            if ((dtDeals != null) && (dtDeals.Rows.Count > 0))
            {
                lblBusinessName.Text = dtDeals.Rows[0]["restaurantBusinessName"].ToString().Trim();
                lblAlternateEmailAddress.Text = dtDeals.Rows[0]["alternativeEmail"].ToString().Trim();
                lblBusinessEmailAddress.Text = dtDeals.Rows[0]["email"].ToString().Trim();
                lblCellNumber.Text = dtDeals.Rows[0]["cellNumber"].ToString().Trim();
                lblPhoneNumber.Text = dtDeals.Rows[0]["phone"].ToString().Trim();
                try
                {
                    lblDealModifyBy.Text = dtDeals.Rows[0]["userName"].ToString().Trim();
                    lblDealModifyTime.Text = Convert.ToDateTime(dtDeals.Rows[0]["modifiedDate"].ToString().Trim()).ToString("yyyy-MM-dd");
                }
                catch (Exception ex)
                { }
                //hfDealID.Value = dtDeals.Rows[0]["dealId"].ToString().Trim();
                lblBusinessName.Text = dtDeals.Rows[0]["restaurantBusinessName"].ToString().Trim();
                lblOwnerFirstName.Text = dtDeals.Rows[0]["firstName"].ToString().Trim();
                lblOwnerLastName.Text = dtDeals.Rows[0]["lastName"].ToString().Trim();

                hfBusinessId.Value = dtDeals.Rows[0]["restaurantId"].ToString().Trim();
                lblDealName.Text = dtDeals.Rows[0]["title"].ToString().Trim();
                if (dtDeals.Rows[0]["preDealVerification"] != null && dtDeals.Rows[0]["preDealVerification"].ToString().Trim() != "")
                {
                    try
                    {
                        ddlPreDealVerification.SelectedValue = dtDeals.Rows[0]["preDealVerification"].ToString().Trim();
                    }
                    catch (Exception ex)
                    { }
                }
                if (dtDeals.Rows[0]["postDealVerification"] != null && dtDeals.Rows[0]["postDealVerification"].ToString().Trim() != "")
                {
                    try
                    {
                        ddlPostDealVerification.SelectedValue = dtDeals.Rows[0]["postDealVerification"].ToString().Trim();
                    }
                    catch (Exception ex)
                    { }
                }

            }

        }
        catch (Exception ex)
        {

        }
    }

    protected void btnImgSave_Click(object sender, ImageClickEventArgs e)
    {

        if (hfBusinessId.Value.Trim() != "")
        {
            BLLRestaurant objRest = new BLLRestaurant();
            objRest.restaurantId = Convert.ToInt64(hfBusinessId.Value);
            objRest.postDealVerification = ddlPostDealVerification.SelectedValue.ToString().Trim();
            objRest.preDealVerification = ddlPreDealVerification.SelectedValue.Trim();
            objRest.updateRestaurantDealVerificationInfoByResID();
           // SearchhDealInfoByDifferentParams(0);
            lblMessage.Text = "Deal verification information has updated successfully.";
            lblMessage.Visible = true;
            imgGridMessage.Visible = true;
            imgGridMessage.ImageUrl = "images/checked.png";
            Response.Redirect("CloseForm.aspx", true);
        }

    }


}
