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
/// Summary description for DALFavoriteRestaurants
/// </summary>
public static class DALFavoriteRestaurants
{
    #region Function to create new FavoriteRestaurants
    public static int createFavoriteRestaurants(BLLFavoriteRestaurants obj)
    {
        int result = 0;
        try
        {
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@restaurantId", obj.restaurantId);            
            param[1] = new SqlParameter("@creationDate", obj.creationDate);
            param[2] = new SqlParameter("@createdBy", obj.createdBy);
            result = SqlHelper.ExecuteNonQuery(Misc.connStr, CommandType.StoredProcedure, "spCreateFavoriteRestaurants", param);
        }
        catch (Exception ex)
        {
            return 0;
        }
        return result;
    }
    #endregion

    #region function to delete Favorite Restaurants
    public static int deleteFavoriteRestaurants(BLLFavoriteRestaurants obj)
    {
        int result = 0;
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@favoriteId", obj.favoriteId);
            result = SqlHelper.ExecuteNonQuery(Misc.connStr, CommandType.StoredProcedure, "spDeleteFavoriteRestaurants", param);
        }
        catch (Exception ex)
        {
            return 0;
        }
        return result;
    }
    #endregion

    #region Function to Update Favorite Restaurants
    public static int updateFavoriteRestaurants(BLLFavoriteRestaurants obj)
    {
        int result = 0;
        try
        {
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@favoriteId", obj.favoriteId);
            param[1] = new SqlParameter("@restaurantId", obj.restaurantId);
            param[2] = new SqlParameter("@ModifiedDate", obj.modifiedDate);
            param[3] = new SqlParameter("@ModifiedBy", obj.modifiedBy);
            result = SqlHelper.ExecuteNonQuery(Misc.connStr, CommandType.StoredProcedure, "spUpdateFavoriteRestaurants", param);
        }
        catch (Exception ex)
        {
            return 0;
        }
        return result;
    }
    #endregion

    #region Functions to get Favorite Restaurants by userID
    public static DataTable getAllFavoriteRestaurantsByUserID(BLLFavoriteRestaurants obj)
    {
        DataTable dt = null;
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@createdBy", obj.createdBy);
            dt = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetAllFavoriteRestaurantsByUserID",param).Tables[0];
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

    #region Functions to get Favorite Restaurants by userID
    public static DataTable getFavoriteRestaurantByUserIDAndRestaurantID(BLLFavoriteRestaurants obj)
    {
        DataTable dt = null;
        try
        {
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@createdBy", obj.createdBy);
            param[1] = new SqlParameter("@restaurantId", obj.restaurantId);
            dt = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetFavoriteRestaurantByUserIDAndRestaurantID", param).Tables[0];
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
