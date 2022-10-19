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
public static class DALDealBanner
{
    public static int createDealBanner(BLLDealBanner obj)
    {
        int result = 0;
        try 
        {
            SqlParameter[] param = new SqlParameter[5];
            param[0] = new SqlParameter("@banner_image", obj.banner_image);
            param[1] = new SqlParameter("@banner_link", obj.banner_link);            
            param[2] = new SqlParameter("@banner_endTime", obj.banner_endTime);
            param[3] = new SqlParameter("@banner_startTime", obj.banner_startTime);
            param[4] = new SqlParameter("@banner_city", obj.banner_city);
            
            result = SqlHelper.ExecuteNonQuery(Misc.connStr, CommandType.StoredProcedure, "spCreateDealBanner", param);
        }
        catch (Exception ex)
        { 

        }
        return result;
    }

    public static int updateDealBanner(BLLDealBanner obj)
    {
        int result = 0;
        try
        {
            SqlParameter[] param = new SqlParameter[6];
            param[0] = new SqlParameter("@banner_image", obj.banner_image);
            param[1] = new SqlParameter("@banner_link", obj.banner_link);            
            param[2] = new SqlParameter("@banner_endTime", obj.banner_endTime);
            param[3] = new SqlParameter("@banner_ID", obj.banner_ID);
            param[4] = new SqlParameter("@banner_startTime", obj.banner_startTime);
            param[5] = new SqlParameter("@banner_city", obj.banner_city);
            result = SqlHelper.ExecuteNonQuery(Misc.connStr, CommandType.StoredProcedure, "spUpdateDealBanner", param);
        }
        catch (Exception ex)
        {

        }
        return result;
    }

    public static int deleteDealBanner(BLLDealBanner obj)
    {
        int result = 0;
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@banner_ID", obj.banner_ID);
            result = SqlHelper.ExecuteNonQuery(Misc.connStr, CommandType.StoredProcedure, "spDeleteDealBanners", param);
        }
        catch (Exception ex)
        {

        }
        return result;
    }

    public static DataTable getDealBannerByID(BLLDealBanner obj)
    {
        DataTable dt = null; 
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@banner_ID", obj.banner_ID);
            dt = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetDealBannerByID", param).Tables[0];
        }
        catch (Exception ex)
        {

        }
        return dt;
    }
   
    public static DataTable getDealBannerByStartTime(BLLDealBanner obj)
    {
        DataTable dt = null;
        try
        {
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@banner_startTime", obj.banner_startTime);
            param[1] = new SqlParameter("@banner_city", obj.banner_city);
            dt = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetDealBannerByStartTime", param).Tables[0];
        }
        catch (Exception ex)
        {

        }
        return dt;
    }


    public static DataTable getAllDealBanner()
    {
        DataTable dt = null;
        try
        {
            dt = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetAllDealBanner").Tables[0];
        }
        catch (Exception ex)
        {

        }
        return dt;
    }
 
}
