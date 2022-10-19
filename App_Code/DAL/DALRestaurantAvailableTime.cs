using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using SQLHelper;
using System.Data.SqlClient;
/// <summary>
/// Summary description for DALRestaurantAvailableTime
/// </summary>
public static class DALRestaurantAvailableTime
{
    #region Function to create
    public static int createRestaurantAvailableTime(BLLRestaurantAvailableTime obj)
    {
        int result = 0;
        try
        {
            SqlParameter[] param = new SqlParameter[9];
            param[0] = new SqlParameter("@settingID", obj.settingID);
            param[1] = new SqlParameter("@dayOfWeek", obj.dayOfWeek);
            param[2] = new SqlParameter("@open1", obj.open1);
            param[3] = new SqlParameter("@open2", obj.open2);
            param[4] = new SqlParameter("@close1", obj.close1);
            param[5] = new SqlParameter("@close2", obj.close2);
            param[6] = new SqlParameter("@isDay", obj.isDay);            
            param[7] = new SqlParameter("@creationDate", obj.creationDate);
            param[8] = new SqlParameter("@createdBy", obj.createdBy);
            result = SqlHelper.ExecuteNonQuery(Misc.connStr, CommandType.StoredProcedure, "spCreateRestaurantAvailableTime", param);
        }
        catch (Exception ex)
        {
            return 0;
        }
        return result;
    }
    #endregion

    #region function to delete
    public static int deleteRestaurantAvailableTime(BLLRestaurantAvailableTime obj)
    {
        int result = 0;
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@timeId", obj.timeId);
            result = SqlHelper.ExecuteNonQuery(Misc.connStr, CommandType.StoredProcedure, "spDeleteRestaurantAvailableTime", param);
        }
        catch (Exception ex)
        {
            return 0;
        }
        return result;
    }
    #endregion

    #region Function to Update
    public static int updateRestaurantAvailableTime(BLLRestaurantAvailableTime obj)
    {
        int result = 0;
        try
        {
            SqlParameter[] param = new SqlParameter[8];

            param[0] = new SqlParameter("@timeId", obj.timeId);
            param[1] = new SqlParameter("@open1", obj.open1);
            param[2] = new SqlParameter("@open2", obj.open2);
            param[3] = new SqlParameter("@close1", obj.close1);
            param[4] = new SqlParameter("@close2", obj.close2);
            param[5] = new SqlParameter("@isDay", obj.isDay);
            param[6] = new SqlParameter("@modifiedDate", obj.modifiedDate);
            param[7] = new SqlParameter("@modifiedBy", obj.modifiedBy);
            
            result = SqlHelper.ExecuteNonQuery(Misc.connStr, CommandType.StoredProcedure, "spUpdateRestaurantAvailableTime", param);
        }
        catch (Exception ex)
        {
            return 0;
        }
        return result;
    }
    #endregion

    #region Functions to get All Cuisine
    public static DataTable getRestaurantAvailableTimeBySettingID(BLLRestaurantAvailableTime obj)
    {
        DataTable dt = null;
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@settingID", obj.settingID);
            dt = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetRestaurantAvailableTimeBySettingID",param).Tables[0];
        }
        catch (Exception ex)
        {
            return null;
        }
        return dt;
    }
    #endregion

    #region Functions to get All Available Time
    public static DataTable getRestaurantAvailableTimeByRestaurantID(BLLRestaurantAvailableTime obj)
    {
        DataTable dt = null;
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@restaurantID", obj.restaurantID);
            dt = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetRestaurantAvailableTimeByRestaurantID", param).Tables[0];
        }
        catch (Exception ex)
        {
            return null;
        }
        return dt;
    }
    #endregion
}
