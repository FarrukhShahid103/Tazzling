using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SQLHelper;
using System.Data;
using System.Data.SqlClient;
/// <summary>
/// Summary cityDescription for DALCities
/// </summary>
public static class DALDealCity
{
    public static int createDealCity(BLLDealCity obj)
    {
        int result = 0;
        try 
        {
            SqlParameter[] param = new SqlParameter[5];
            param[0] = new SqlParameter("@CityId", obj.CityId);
            param[1] = new SqlParameter("@DealId", obj.DealId);
            param[2] = new SqlParameter("@DealEndTimeC", obj.DealEndTimeC);
            param[3] = new SqlParameter("@DealStartTimeC", obj.DealStartTimeC);
            param[4] = new SqlParameter("@DealSlotC", obj.DealSlotC);
            result = SqlHelper.ExecuteNonQuery(Misc.connStr, CommandType.StoredProcedure, "spCreateDealCity", param);
        }
        catch (Exception ex)
        { 
        
        }
        return result;
    }
    public static int updateDealCity(BLLDealCity obj)
    {
        int result = 0;
        try
        {
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@CityId", obj.CityId);
            param[1] = new SqlParameter("@DealId", obj.DealId);
            result = SqlHelper.ExecuteNonQuery(Misc.connStr, CommandType.StoredProcedure, "spUpdateDealCity", param);
        }
        catch (Exception ex)
        {

        }
        return result;
    }
    public static int deleteDealCityByDealID(BLLDealCity obj)
    {
        int result = 0;
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@DealId", obj.DealId);
            result = SqlHelper.ExecuteNonQuery(Misc.connStr, CommandType.StoredProcedure, "spDeleteDealCityByDealID", param);
        }
        catch (Exception ex)
        {

        }
        return result;
    }
    public static DataTable getDealCityListByDealId(BLLDealCity obj)
    {
        DataTable dt = null; 
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@DealId", obj.DealId);
            dt = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetDealCityListByDealId", param).Tables[0];
        }
        catch (Exception ex)
        {

        }
        return dt;
    }

}
