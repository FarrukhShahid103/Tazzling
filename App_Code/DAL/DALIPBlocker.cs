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
public static class DALIPBlocker
{
    public static int createIPBlocker(BLLIPBlocker obj)
    {
        int result = 0;
        try 
        {
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@RequestIP", obj.RequestIP);                        
            result = SqlHelper.ExecuteNonQuery(Misc.connStr, CommandType.StoredProcedure, "spCreateIPBlocker", param);
        }
        catch (Exception ex)
        { 
        
        }
        return result;
    }
    public static int deleteIPBlocker(BLLIPBlocker obj)
    {
        int result = 0;
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@BlackListID", obj.BlackListID);
            result = SqlHelper.ExecuteNonQuery(Misc.connStr, CommandType.StoredProcedure, "spDeleteIPBlocker", param);
        }
        catch (Exception ex)
        {

        }
        return result;
    }

    public static DataTable getAllIPBlocker(BLLIPBlocker obj)
    {
        DataTable dt = null;
        try
        {
            dt = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetAllIPBlocker").Tables[0];
        }
        catch (Exception ex)
        {

        }
        return dt;
    }
     
}
