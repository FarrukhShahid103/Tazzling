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
public static class DALDeviceInfo
{
    public static int createAndUpdateDeviceInfo(BLLDeviceInfo obj)
    {
        int result = 0;
        try
        {
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@deviceToken", obj.deviceToken);
            param[1] = new SqlParameter("@notification_Counter", obj.notification_Counter);
            param[2] = new SqlParameter("@iPhone", obj.iPhone);
            param[3] = new SqlParameter("@cityId", obj.cityId);

            result = SqlHelper.ExecuteNonQuery(Misc.connStr, CommandType.StoredProcedure, "spCreateAndUpdateDeviceInfo", param);
        }
        catch (Exception ex)
        {

        }
        return result;
    }

    #region Functions to get all

    public static DataTable getAlliPhoneDevices()
    {
        DataTable dtDeals = null;
        try
        {
            dtDeals = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetAlliPhoneDevices").Tables[0];
            if (dtDeals != null && dtDeals.Rows.Count > 0)
            {
                return dtDeals;
            }
        }
        catch (Exception ex)
        {
            return null;
        }
        return dtDeals;
    }

    public static DataTable getAlliPhoneDevicesByCityID(BLLDeviceInfo obj)
    {
        DataTable dt = null;
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@cityId", obj.cityId);
            dt = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetAlliPhoneDevicesByCityID", param).Tables[0];
        }
        catch (Exception ex)
        {

        }
        return dt;
    }

    #endregion
}
