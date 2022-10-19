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
public static class DALKarmaPoints
{
    public static int createKarmaPoints(BLLKarmaPoints obj)
    {
        int result = 0;
        try
        {
            SqlParameter[] param = new SqlParameter[5];
            param[0] = new SqlParameter("@createdDate", obj.createdDate);
            param[1] = new SqlParameter("@createdBy", obj.createdBy);
            param[2] = new SqlParameter("@karmaPoints", obj.karmaPoints);
            param[3] = new SqlParameter("@karmaPointsType", obj.karmaPointsType);
            param[4] = new SqlParameter("@userId", obj.userId);
            result = SqlHelper.ExecuteNonQuery(Misc.connStr, CommandType.StoredProcedure, "spCreateKarmaPoints", param);
        }
        catch (Exception ex)
        {

        }
        return result;
    } 
    public static DataTable getKarmaPointsByUserId(BLLKarmaPoints obj)
    {
        DataTable dt = null;
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@userId", obj.userId);
            dt = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetKarmaPointsByUserId", param).Tables[0];
        }
        catch (Exception ex)
        {

        }
        return dt;
    }

    public static DataTable getKarmaTodayLoginPointsByUserId(BLLKarmaPoints obj)
    {
        DataTable dt = null;
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@userId", obj.userId);            
            dt = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetKarmaTodayLoginPointsByUserId", param).Tables[0];
        }
        catch (Exception ex)
        {

        }
        return dt;
    }

    public static DataTable getKarmaPointsTotalByUserId(BLLKarmaPoints obj)
    {
        DataTable dt = null;
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@userId", obj.userId);
            dt = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetKarmaPointsTotalByUserId", param).Tables[0];
        }
        catch (Exception ex)
        {

        }
        return dt;
    }      
}
