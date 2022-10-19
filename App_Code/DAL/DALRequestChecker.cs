using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using SQLHelper;

/// <summary>
/// Summary description for DALRequestChecker
/// </summary>
public class DALRequestChecker
{
    public static DataTable AddRequestToRequestChecker(BLLRequestChecker obj)
    {
        DataTable dt = null;
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@RequestIP", obj.requestIP);
            dt = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "sp_AddRequestToRequestChecker", param).Tables[0];
        }
        catch (Exception ex)
        {

        }
        return dt;
    }



    public static int DeleteFromRequestChecker(BLLRequestChecker obj)
    {
        int result = 0;
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@RequestIP", obj.requestIP);
            result = SqlHelper.ExecuteNonQuery(Misc.connStr, CommandType.StoredProcedure, "SP_DeleteFromRequestChecker", param);
        }
        catch (Exception ex)
        {
            return 0;
        }
        return result;
    }


    public static DataTable CheckIPIsBlocked(BLLRequestChecker obj)
    {
        DataTable dt = null;
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@RequestIP", obj.requestIP);
            dt = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "SP_CheckIPIsBlocked", param).Tables[0];
        }
        catch (Exception ex)
        {

        }
        return dt;
    }

}