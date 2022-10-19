using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Takeout_UserControls_ctrlGiftCart : System.Web.UI.UserControl
{
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

                HiddenField hfdcGiftUnitPrice = (HiddenField)e.Item.FindControl("hfdcGiftUnitPrice");

                if (Session["dtGiftCard"] != null)
                {
                    //Get the Cart values from the Application variable
                    DataTable dtGiftCart = (DataTable)Session["dtGiftCard"];

                    //Status variable for validating whether Gift Card is removed from the Cart or Not
                    bool bGiftCart = false;

                    //Initilize the Total variable
                    double dTotal = 0;

                    //validate the Gift Cart DataTable
                    if ((dtGiftCart != null) && (dtGiftCart.Rows.Count > 0))
                    {
                        for (int i = 0; i < dtGiftCart.Rows.Count; i++)
                        {
                            //Finds the Selected Gift Card into the DataTable
                            if (hfdcGiftUnitPrice.Value == dtGiftCart.Rows[i]["dcGiftUnitPrice"].ToString().Trim())
                            {
                                //Validates that selected Gift Card Quantity is greater than 1 or not
                                if ((int.Parse(dtGiftCart.Rows[i]["dcGiftQty"].ToString().Trim())) > 1)
                                {
                                    //Decrease the Gift Cart Quantity by 1
                                    dtGiftCart.Rows[i]["dcGiftQty"] = int.Parse(dtGiftCart.Rows[i]["dcGiftQty"].ToString().Trim()) - 1;

                                    //Set the Gift Cart variable
                                    bGiftCart = true;
                                }
                                else
                                {
                                    //Delete the Gift Cart Row if Gift Cart Quanity is Single or 1
                                    dtGiftCart.Rows[i].Delete();

                                    //Set the Gift Cart variable
                                    bGiftCart = true;
                                }
                            }
                        }

                        for (int i = 0; i < dtGiftCart.Rows.Count; i++)
                        {
                            //Calculate the Total                            
                            dTotal += double.Parse(dtGiftCart.Rows[i]["dcGiftUnitPrice"].ToString().Trim()) * double.Parse(dtGiftCart.Rows[i]["dcGiftQty"].ToString().Trim());
                        }
                    }

                    if (bGiftCart == true)
                    {
                        Session["dtGiftCard"] = dtGiftCart;

                        rptGiftCard.DataSource = dtGiftCart;
                        rptGiftCard.DataBind();

                        this.lblTotal.Text = "$ " + dTotal.ToString() + ".00";

                        Session["GiftCardTotal"] = this.lblTotal.Text;
                    }
                }
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
            Response.Redirect(ResolveUrl("~/giftcard_step2.aspx"), false);
        }
        catch (Exception ex)
        {
            string strException = "There is an error occur, please email us at support@tazzling.com or call 1855-295-1771.";
        }
    }
}
