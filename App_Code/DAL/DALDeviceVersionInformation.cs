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
public static class DALDeviceVersionInformation
{
    public static int createUpdateDeviceVersionInformation(BLLDeviceVersionInformation obj)
    {
        int result = 0;
        try 
        {
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@deviceType", obj.deviceType);
            param[1] = new SqlParameter("@message", obj.message);
            param[2] = new SqlParameter("@newVersion", obj.newVersion);
            param[3] = new SqlParameter("@title", obj.title);

            result = SqlHelper.ExecuteNonQuery(Misc.connStr, CommandType.StoredProcedure, "spCreateUpdateDeviceVersionInformation", param);
        }
        catch (Exception ex)
        { 
        
        }
        return result;
    }

    public static DataTable getDeviceVersionInformationByDeviceType(BLLDeviceVersionInformation obj)
    {
        DataTable dt = null;
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@deviceType", obj.deviceType);
            dt = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetDeviceVersionInformationByDeviceType", param).Tables[0];
            return dt;
        }
        catch (Exception ex)
        {
            return dt;
        }
    }
}
