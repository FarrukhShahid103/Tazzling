using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SQLHelper;
using System.Data;
using System.Data.SqlClient;
/// <summary>
/// Summary description for DALCities
/// </summary>
public static class DALRestaurantGoogleAddresses
{
    public static long createRestaurantGoogleAddresses(BLLRestaurantGoogleAddresses obj)
    {
        long result = 0;
        try 
        {
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@restaurantGoogleAddress", obj.restaurantGoogleAddress);
            param[1] = new SqlParameter("@restaurantId", obj.restaurantId);                        
            result = Convert.ToInt64(SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spCreateRestaurantGoogleAddresse", param).Tables[0].Rows[0][0]);            
        }
        catch (Exception ex)
        { 
        
        }
        return result;
    }
    public static int updateRestaurantGoogleAddresses(BLLRestaurantGoogleAddresses obj)
    {
        int result = 0;
        try
        {
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@restaurantGoogleAddress", obj.restaurantGoogleAddress);
            param[1] = new SqlParameter("@restaurantId", obj.restaurantId);
            param[2] = new SqlParameter("@rgaID", obj.rgaID);            
            result = SqlHelper.ExecuteNonQuery(Misc.connStr, CommandType.StoredProcedure, "spUpdateRestaurantGoogleAddresses", param);
        }
        catch (Exception ex)
        {

        }
        return result;
    }
    public static int deleteRestaurantGoogleAddresses(BLLRestaurantGoogleAddresses obj)
    {
        int result = 0;
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@rgaID", obj.rgaID);
            result = SqlHelper.ExecuteNonQuery(Misc.connStr, CommandType.StoredProcedure, "spDeleteRestaurantGoogleAddresses", param);
        }
        catch (Exception ex)
        {

        }
        return result;
    }

    public static int deleteAllRestaurantGoogleAddressesByRestaurantID(BLLRestaurantGoogleAddresses obj)
    {
        int result = 0;
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@restaurantId", obj.restaurantId);
            result = SqlHelper.ExecuteNonQuery(Misc.connStr, CommandType.StoredProcedure, "spDeleteAllRestaurantGoogleAddressesByRestaurantID", param);
        }
        catch (Exception ex)
        {

        }
        return result;
    }


    public static DataTable getRestaurantGoogleAddressesByID(BLLRestaurantGoogleAddresses obj)
    {
        DataTable dt = null; 
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@rgaID", obj.rgaID);
            dt = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetRestaurantGoogleAddressesByID", param).Tables[0];
        }
        catch (Exception ex)
        {

        }
        return dt;
    }

    public static DataTable getAllRestaurantGoogleAddressesByRestaurantID(BLLRestaurantGoogleAddresses obj)
    {
        DataTable dt = null;
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@restaurantId", obj.restaurantId);
            dt = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetAllRestaurantGoogleAddressesByRestaurantID", param).Tables[0];
        }
        catch (Exception ex)
        {

        }
        return dt;
    }

    public static DataTable getAllRestaurantGoogleAddressesByDealID(string strDealId)
    {
        DataTable dt = null;
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@DealID", strDealId);
            dt = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetRestaurantGoogleAddressesByDealID", param).Tables[0];
        }
        catch (Exception ex)
        {}
        return dt;
    }
}