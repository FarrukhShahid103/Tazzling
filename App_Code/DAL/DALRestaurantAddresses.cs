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
public static class DALRestaurantAddresses
{
    public static long createRestaurantAddresse(BLLRestaurantAddresses obj)
    {
        long result = 0;
        try 
        {
            SqlParameter[] param = new SqlParameter[6];
            param[0] = new SqlParameter("@provinceId", obj.provinceId);
            param[1] = new SqlParameter("@cityid", obj.cityid);
            param[2] = new SqlParameter("@restaurantAddress", obj.restaurantAddress);
            param[3] = new SqlParameter("@restaurantId", obj.restaurantId);
            param[4] = new SqlParameter("@zipCode", obj.zipCode);
            param[5] = new SqlParameter("@dealCity", obj.dealCity);
            
            result = Convert.ToInt64(SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spCreateRestaurantAddresse", param).Tables[0].Rows[0][0]);            
        }
        catch (Exception ex)
        { 
        
        }
        return result;
    }
    public static int updateRestaurantAddresses(BLLRestaurantAddresses obj)
    {
        int result = 0;
        try
        {
            SqlParameter[] param = new SqlParameter[7];
            param[0] = new SqlParameter("@provinceId", obj.provinceId);
            param[1] = new SqlParameter("@cityid", obj.cityid);
            param[2] = new SqlParameter("@restaurantAddress", obj.restaurantAddress);
            param[3] = new SqlParameter("@restaurantId", obj.restaurantId);
            param[4] = new SqlParameter("@raID", obj.raID);
            param[5] = new SqlParameter("@zipCode", obj.zipCode);
            param[6] = new SqlParameter("@dealCity", obj.dealCity);
            result = SqlHelper.ExecuteNonQuery(Misc.connStr, CommandType.StoredProcedure, "spUpdateRestaurantAddresses", param);
        }
        catch (Exception ex)
        {

        }
        return result;
    }
    public static int deleteRestaurantAddresses(BLLRestaurantAddresses obj)
    {
        int result = 0;
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@raID", obj.raID);
            result = SqlHelper.ExecuteNonQuery(Misc.connStr, CommandType.StoredProcedure, "spDeleteRestaurantAddresses", param);
        }
        catch (Exception ex)
        {

        }
        return result;
    }
    public static DataTable getRestaurantAddressesByID(BLLRestaurantAddresses obj)
    {
        DataTable dt = null; 
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@raID", obj.raID);
            dt = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetRestaurantAddressesByID", param).Tables[0];
        }
        catch (Exception ex)
        {

        }
        return dt;
    }

    public static DataTable getAllRestaurantAddressesByRestaurantID(BLLRestaurantAddresses obj)
    {
        DataTable dt = null;
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@restaurantId", obj.restaurantId);
            dt = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetAllRestaurantAddressesByRestaurantID", param).Tables[0];
        }
        catch (Exception ex)
        {

        }
        return dt;
    }

    public static DataTable getAllRestaurantAddressesByDealID(string strDealId)
    {
        DataTable dt = null;
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@DealID", strDealId);
            dt = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetRestaurantAddressesByDealID", param).Tables[0];
        }
        catch (Exception ex)
        {}
        return dt;
    }
}