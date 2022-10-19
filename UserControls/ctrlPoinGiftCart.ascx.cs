using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Takeout_UserControls_ctrlPoinGiftCart : System.Web.UI.UserControl
{
    BLLMemberPointGiftsRequests objRequests = new BLLMemberPointGiftsRequests();
    BLLMemberPoints objUserPoints = new BLLMemberPoints();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        { }
    }
    protected void rptGiftCard_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        try
        {

            if (e.CommandName == "Delete")
            {
                // Add code here to add the item to the shopping cart.
                // Use the value of e.Item.ItemIndex to find the data row
                // in the data source.

                HiddenField dcGiftID = (HiddenField)e.Item.FindControl("dcGiftID");

                if (Session["dtPointGifts"] != null)
                {
                    //Get the Cart values from the Application variable
                    DataTable dtGiftCart = (DataTable)Session["dtPointGifts"];

                    //Status variable for validating whether Gift Card is removed from the Cart or Not
                    bool bGiftCart = false;

                    //Initilize the Total variable
                    long dTotal = 0;

                    //validate the Gift Cart DataTable
                    if ((dtGiftCart != null) && (dtGiftCart.Rows.Count > 0))
                    {
                        for (int i = 0; i < dtGiftCart.Rows.Count; i++)
                        {
                            //Finds the Selected Gift Card into the DataTable
                            if (dcGiftID.Value == dtGiftCart.Rows[i]["dcGiftID"].ToString().Trim())
                            {
                                //Validates that selected Gift Card Quantity is greater than 1 or not
                                if ((int.Parse(dtGiftCart.Rows[i]["dcGiftQty"].ToString().Trim())) > 1)
                                {
                                    //Decrease the Gift Cart Quantity by 1
                                    dtGiftCart.Rows[i]["dcGiftQty"] = int.Parse(dtGiftCart.Rows[i]["dcGiftQty"].ToString().Trim()) - 1;
                                    //Set the Sub-Total here
                                    dtGiftCart.Rows[i]["dcGiftSubTotalPrice"] = (int.Parse(dtGiftCart.Rows[i]["dcGiftQty"].ToString())) * (int.Parse(dtGiftCart.Rows[i]["dcGiftUnitPrice"].ToString()));
                                    
                                    Session["PTotal"] = Convert.ToInt64(Session["PTotal"].ToString()) + int.Parse(dtGiftCart.Rows[i]["dcGiftUnitPrice"].ToString().Trim());
                                    //Set the Gift Cart variable
                                    bGiftCart = true;
                                }
                                else
                                {
                                    //Delete the Gift Cart Row if Gift Cart Quanity is Single or 1
                                    Session["PTotal"] = Convert.ToInt64(Session["PTotal"].ToString()) + int.Parse(dtGiftCart.Rows[i]["dcGiftUnitPrice"].ToString().Trim());
                                    dtGiftCart.Rows[i].Delete();

                                    //Set the Gift Cart variable
                                    bGiftCart = true;
                                }
                            }
                        }

                        for (int i = 0; i < dtGiftCart.Rows.Count; i++)
                        {
                            //Calculate the Total                            
                            dTotal += long.Parse(dtGiftCart.Rows[i]["dcGiftUnitPrice"].ToString().Trim()) * long.Parse(dtGiftCart.Rows[i]["dcGiftQty"].ToString().Trim());
                        }
                    }
                    if (bGiftCart == true)
                    {
                        Session["dtPointGifts"] = dtGiftCart;

                        rptGiftCard.DataSource = dtGiftCart;
                        rptGiftCard.DataBind();

                        this.lblTotal.Text =   dTotal.ToString();

                        Session["PointGiftTotal"] = this.lblTotal.Text;
                    }
                }

                Response.Redirect("~/points_gifts.aspx", false);
            }
        }
        catch (Exception ex)
        {
            string strException = "There is an error occur, please email us at support@tazzling.com or call 1855-295-1771.";
        }
    }
    
    protected void btnContinue_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable dtUser = null;
            long intUserID = 0;
            if (Session["member"] != null || Session["restaurant"] != null || Session["sale"] != null)
            {
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
                if (dtUser != null && dtUser.Rows.Count > 0 && dtUser.Rows[0]["userId"].ToString().Trim() != "")
                {
                    intUserID = long.Parse(dtUser.Rows[0]["userId"].ToString().Trim());
                }

                if (Session["dtPointGifts"] != null)
                {
                    DataTable dtGiftCart = (DataTable)Session["dtPointGifts"];
                    if (dtGiftCart != null && dtGiftCart.Rows.Count > 0)
                    {
                        for (int i = 0; i < dtGiftCart.Rows.Count; i++)
                        {
                            if ((int.Parse(dtGiftCart.Rows[i]["dcGiftQty"].ToString().Trim())) > 1)
                            {
                                for (int a = 0; a < int.Parse(dtGiftCart.Rows[i]["dcGiftQty"].ToString().Trim()); a++)
                                {
                                    objRequests.pgID = long.Parse(dtGiftCart.Rows[i]["dcGiftID"].ToString().Trim());
                                    objRequests.mrpgName = dtGiftCart.Rows[i]["dcGiftName"].ToString().Trim();
                                    objRequests.mrpgPoints = long.Parse(dtGiftCart.Rows[i]["dcGiftUnitPrice"].ToString().Trim());
                                    objRequests.mrpgDescription = dtGiftCart.Rows[i]["dcGiftDescription"].ToString().Trim();
                                    objRequests.createdBy = intUserID;
                                    objRequests.status = "Pending";
                                    objRequests.createMemberPointGiftsRequests();

                                    objUserPoints.createdBy = intUserID;
                                    objUserPoints.description = "Spend these points to request gift.";
                                    objUserPoints.points = long.Parse(dtGiftCart.Rows[i]["dcGiftUnitPrice"].ToString().Trim());
                                    objUserPoints.pointsGetsFrom = "Request for gift";
                                    objUserPoints.userID = intUserID;
                                    objUserPoints.createMemberPoints();
                                }
                            }
                            else
                            {
                                objRequests.pgID = long.Parse(dtGiftCart.Rows[i]["dcGiftID"].ToString().Trim());
                                objRequests.mrpgName = dtGiftCart.Rows[i]["dcGiftName"].ToString().Trim();
                                objRequests.mrpgPoints = long.Parse(dtGiftCart.Rows[i]["dcGiftUnitPrice"].ToString().Trim());
                                objRequests.mrpgDescription = dtGiftCart.Rows[i]["dcGiftDescription"].ToString().Trim();
                                objRequests.createdBy = intUserID;
                                objRequests.status = "Pending";
                                objRequests.createMemberPointGiftsRequests();

                                objUserPoints.createdBy = intUserID;
                                objUserPoints.description = "Spend these points to request gift.";
                                objUserPoints.points = long.Parse(dtGiftCart.Rows[i]["dcGiftUnitPrice"].ToString().Trim());
                                objUserPoints.pointsGetsFrom = "Request for gift";
                                objUserPoints.userID = intUserID;
                                objUserPoints.createMemberPoints();
                            }
                        }
                    }


                }

                Response.Redirect(ResolveUrl("~/points_gift_step2.aspx"), false);

            }
            else
            {
                 Response.Redirect(ResolveUrl("~/login.aspx"), false);  
            }
            
        }
        catch (Exception ex)
        {
            string strException = "There is an error occur, please email us at support@tazzling.com or call 1855-295-1771.";
        }
    }
}
