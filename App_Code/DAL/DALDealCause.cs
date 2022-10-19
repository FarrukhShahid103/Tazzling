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
public static class DALDealCause
{
    public static int createDealCause(BLLDealCause obj)
    {
        int result = 0;
        try 
        {
            SqlParameter[] param = new SqlParameter[8];
            param[0] = new SqlParameter("@cause_image", obj.cause_image);
            param[1] = new SqlParameter("@cause_link", obj.cause_link);
            param[2] = new SqlParameter("@cause_longDescription", obj.cause_longDescription);
            param[3] = new SqlParameter("@cause_shortDescription", obj.cause_shortDescription);
            param[4] = new SqlParameter("@cause_title", obj.cause_title);
            param[5] = new SqlParameter("@cause_endTime", obj.cause_endTime);
            param[6] = new SqlParameter("@cause_startTime", obj.cause_startTime);
            param[7] = new SqlParameter("@cause_city", obj.cause_city);
            
            result = SqlHelper.ExecuteNonQuery(Misc.connStr, CommandType.StoredProcedure, "spCreateDealCause", param);
        }
        catch (Exception ex)
        { 

        }
        return result;
    }

    public static int updateDealCause(BLLDealCause obj)
    {
        int result = 0;
        try
        {
            SqlParameter[] param = new SqlParameter[9];
            param[0] = new SqlParameter("@cause_image", obj.cause_image);
            param[1] = new SqlParameter("@cause_link", obj.cause_link);
            param[2] = new SqlParameter("@cause_longDescription", obj.cause_longDescription);
            param[3] = new SqlParameter("@cause_shortDescription", obj.cause_shortDescription);
            param[4] = new SqlParameter("@cause_title", obj.cause_title);
            param[5] = new SqlParameter("@cause_endTime", obj.cause_endTime);
            param[6] = new SqlParameter("@cause_ID", obj.cause_ID);
            param[7] = new SqlParameter("@cause_startTime", obj.cause_startTime);
            param[8] = new SqlParameter("@cause_city", obj.cause_city);
            result = SqlHelper.ExecuteNonQuery(Misc.connStr, CommandType.StoredProcedure, "spUpdateDealCause", param);
        }
        catch (Exception ex)
        {

        }
        return result;
    }

    public static int deleteDealCause(BLLDealCause obj)
    {
        int result = 0;
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@cause_ID", obj.cause_ID);
            result = SqlHelper.ExecuteNonQuery(Misc.connStr, CommandType.StoredProcedure, "spDeleteDealCause", param);
        }
        catch (Exception ex)
        {

        }
        return result;
    }

    public static DataTable getDealCauseByID(BLLDealCause obj)
    {
        DataTable dt = null; 
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@cause_ID", obj.cause_ID);
            dt = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetDealCauseByID", param).Tables[0];
        }
        catch (Exception ex)
        {

        }
        return dt;
    }

    public static DataTable getDealCauseByDealID(BLLDealCause obj)
    {
        DataTable dt = null;
        try
        {
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@cause_startTime", obj.cause_startTime);
            param[1] = new SqlParameter("@cause_city", obj.cause_city);
            dt = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetDealCauseByStartTime", param).Tables[0];
        }
        catch (Exception ex)
        {

        }
        return dt;
    }

    public static DataTable getDealCauseByStartTime(BLLDealCause obj)
    {
        DataTable dt = null;
        try
        {
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@cause_startTime", obj.cause_startTime);
            param[1] = new SqlParameter("@cause_city", obj.cause_city);
            dt = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetDealCauseByStartTime", param).Tables[0];
        }
        catch (Exception ex)
        {

        }
        return dt;
    }


    public static DataTable getAllDealCause()
    {
        DataTable dt = null;
        try
        {
            dt = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetAllDealCause").Tables[0];
        }
        catch (Exception ex)
        {

        }
        return dt;
    }
 
}
