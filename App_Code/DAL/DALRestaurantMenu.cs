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
public static class DALRestaurantMenu
{
    #region Function to create 
    public static long createRestaurantMenu(BLLRestaurantMenu obj)
    {
        long result = 0;
        try
        {
            SqlParameter[] param = new SqlParameter[5];
            param[0] = new SqlParameter("@foodType", obj.foodType);
            param[1] = new SqlParameter("@CuisineID", obj.cuisineID);
            param[2] = new SqlParameter("@foodImage", obj.foodImage);            
            param[3] = new SqlParameter("@creationDate", obj.creationDate);
            param[4] = new SqlParameter("@createdBy", obj.createdBy);
            result = Convert.ToInt64(SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spCreateRestaurantMenu", param).Tables[0].Rows[0][0]);
        }
        catch (Exception ex)
        {
            return 0;
        }
        return result;
    }
    #endregion

    #region function to delete
    public static int deleteRestaurantMenu(BLLRestaurantMenu obj)
    {
        int result = 0;
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@foodTypeID", obj.foodTypeId);
            result = SqlHelper.ExecuteNonQuery(Misc.connStr, CommandType.StoredProcedure, "spDeleteRestaurantMenu", param);
        }
        catch (Exception ex)
        {
            return 0;
        }
        return result;
    }
    #endregion


    #region function to delete
    public static int deleteRestaurantMenuByCuisineID(BLLRestaurantMenu obj)
    {
        int result = 0;
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@cuisineId", obj.cuisineID);
            result = SqlHelper.ExecuteNonQuery(Misc.connStr, CommandType.StoredProcedure, "spDeleteRestaurantMenuByCuisineID", param);
        }
        catch (Exception ex)
        {
            return 0;
        }
        return result;
    }
    #endregion

    #region Function to Update
    public static int updateRestaurantMenu(BLLRestaurantMenu obj)
    {
        int result = 0;
        try
        {
            SqlParameter[] param = new SqlParameter[6];
            param[0] = new SqlParameter("@foodTypeID", obj.foodTypeId);
            param[1] = new SqlParameter("@foodType", obj.foodType);
            param[2] = new SqlParameter("@CuisineID", obj.cuisineID);
            param[3] = new SqlParameter("@modifiedDate", obj.modifiedDate);
            param[4] = new SqlParameter("@modifiedBy", obj.modifiedBy);
            result = SqlHelper.ExecuteNonQuery(Misc.connStr, CommandType.StoredProcedure, "spUpdateRestaurantMenu", param);
        }
        catch (Exception ex)
        {
            return 0;
        }
        return result;
    }
    #endregion

    #region Functions to get all
    public static DataTable getRestaurantMenuByCuisineID(BLLRestaurantMenu obj)
    {
        DataTable dt = null;
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@CuisineID", obj.cuisineID);
            dt = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetRestaurantMenuByCuisineID",param).Tables[0];
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
