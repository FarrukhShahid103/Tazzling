using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Takeout_UserControls_restaurant_ctrlFeaturedFood : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["RESID"] != null)
            {
                //Get the Restaurant ID
                string strRestaurantUser = Request.QueryString["RESID"].ToString();

                GetAndSetFeaturedFoods(strRestaurantUser);
            }
        }
    }

    private void GetAndSetFeaturedFoods(string strRestaurantUser)
    {
        try
        {
            BLLFeaturedFoods objBLLFeaturedFoods = new BLLFeaturedFoods();

            DataTable dtFeaturedFood = objBLLFeaturedFoods.GetFeaturedFoodByRestaurantID(strRestaurantUser);

            if ((dtFeaturedFood != null) && (dtFeaturedFood.Rows.Count > 0))
            {
                this.rptrFeaturedRes.DataSource = dtFeaturedFood;
                this.rptrFeaturedRes.DataBind();
            }
        }
        catch (Exception ex)
        {
            string strException = "There is an error occur, please email us at support@tazzling.com or call 1855-295-1771.";
        }
    }
}