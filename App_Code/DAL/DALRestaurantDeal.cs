using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using SQLHelper;
using System.Data.SqlClient;

/// <summary>
/// Summary description for DALRestaurantMenu
/// </summary>
public static class DALRestaurantDeal
{
    #region Function to create 
    public static long createRestaurantDeal(BLLRestaurantDeal obj)
    {
        long result = 0;
        try
        {
            SqlParameter[] param = new SqlParameter[7];
            param[0] = new SqlParameter("@dealName", obj.dealName);
            param[1] = new SqlParameter("@CuisineID", obj.cuisineID);
            param[2] = new SqlParameter("@dealImage", obj.dealImage);
            param[3] = new SqlParameter("@dealOrderItemsQty", obj.dealOrderItemsQty);
            param[4] = new SqlParameter("@dealPrice", obj.dealPrice);
            param[5] = new SqlParameter("@creationDate", obj.creationDate);
            param[6] = new SqlParameter("@createdBy", obj.createdBy);
            result = Convert.ToInt64(SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spCreateRestaurantDeal", param).Tables[0].Rows[0][0]);
        }
        catch (Exception ex)
        {
            return 0;
        }
        return result;
    }
    #endregion

    #region function to delete
    public static int deleteRestaurantDeal(BLLRestaurantDeal obj)
    {
        int result = 0;
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@dealID", obj.dealId);
            result = SqlHelper.ExecuteNonQuery(Misc.connStr, CommandType.StoredProcedure, "spDeleteRestaurantDeal", param);
        }
        catch (Exception ex)
        {
            return 0;
        }
        return result;
    }
    #endregion

    #region function to delete
    public static int deleteRestaurantDealByCuisineID(BLLRestaurantDeal obj)
    {
        int result = 0;
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@cuisineID", obj.cuisineID);
            result = SqlHelper.ExecuteNonQuery(Misc.connStr, CommandType.StoredProcedure, "spDeleteRestaurantDealByCuisineID", param);
        }
        catch (Exception ex)
        {
            return 0;
        }
        return result;
    }
    #endregion

    

    #region Function to Update
    public static int updateRestaurantDeal(BLLRestaurantDeal obj)
    {
        int result = 0;
        try
        {
            SqlParameter[] param = new SqlParameter[7];
            param[0] = new SqlParameter("@dealID", obj.dealId);
            param[1] = new SqlParameter("@dealName", obj.dealName);
            param[2] = new SqlParameter("@CuisineID", obj.cuisineID);            
            param[3] = new SqlParameter("@dealOrderItemsQty", obj.dealOrderItemsQty);
            param[4] = new SqlParameter("@dealPrice", obj.dealPrice);
            param[5] = new SqlParameter("@modifiedDate", obj.modifiedDate);
            param[6] = new SqlParameter("@modifiedBy", obj.modifiedBy);
            result = SqlHelper.ExecuteNonQuery(Misc.connStr, CommandType.StoredProcedure, "spUpdateRestaurantDeal", param);
        }
        catch (Exception ex)
        {
            return 0;
        }
        return result;
    }
    #endregion

    #region Functions to get all
    public static DataTable getRestaurantDealByCuisineID(BLLRestaurantDeal obj)
    {
        DataTable dt = null;
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@CuisineID", obj.cuisineID);
            dt = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetRestaurantDealByCuisineID", param).Tables[0];
            if (dt != null && dt.Rows.Count > 0)
            {
                return dt;
            }
        }
        catch (Exception ex)
        {
            return null;
        }
        return dt;
    }
    #endregion
}
