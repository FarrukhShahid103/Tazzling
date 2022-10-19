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
/// Summary description for DALRestaurantSetting
/// </summary>
public static class DALRestaurantSetting
{
    #region Function to create
    
    public static long createRestaurantSetting(BLLRestaurantSetting obj)
    {
        long result = 0;
        try
        {
            SqlParameter[] param = new SqlParameter[14];
            param[0] = new SqlParameter("@isDelivery", obj.isDelivery);
            param[1] = new SqlParameter("@freeDeliveryDistance", obj.freeDeliveryDistance);
            param[2] = new SqlParameter("@exceededLimitsOK", obj.exceededLimitsOK);
            param[3] = new SqlParameter("@exceededLimitCharge", obj.exceededLimitCharge);
            param[4] = new SqlParameter("@exceededLimitChargeUnit", obj.exceededLimitChargeUnit);
            param[5] = new SqlParameter("@creationDate", obj.creationDate);
            param[6] = new SqlParameter("@createdBy", obj.createdBy);
            param[7] = new SqlParameter("@realMenuImage", obj.realMenuImage);
            param[8] = new SqlParameter("@minimumOrderAmount", obj.minimumOrderAmount);
            param[9] = new SqlParameter("@deliveryChargeUnit", obj.deliveryChargeUnit);
            param[10] = new SqlParameter("@deliveryCharge", obj.deliveryCharge);
            param[11] = new SqlParameter("@restaurantId", obj.restaurantId);
            param[12] = new SqlParameter("@belowLimitOK", obj.belowLimitsOK);
            param[13] = new SqlParameter("@belowLimitCharge", obj.belowLimitCharge);            
            result = Convert.ToInt64(SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spCreateRestaurantSetting", param).Tables[0].Rows[0][0]);
        }
        catch (Exception ex)
        {
            return 0;
        }
        return result;
    }

    #endregion

    #region function to delete
    public static int deleteRestaurantSetting(BLLRestaurantSetting obj)
    {
        int result = 0;
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@settingID", obj.settingID);
            result = SqlHelper.ExecuteNonQuery(Misc.connStr, CommandType.StoredProcedure, "spDeleteRestaurantSetting", param);
        }
        catch (Exception ex)
        {
            return 0;
        }
        return result;
    }
    #endregion

    #region Function to Update
    public static int updateRestaurantSetting(BLLRestaurantSetting obj)
    {
        int result = 0;
        try
        {
            SqlParameter[] param = new SqlParameter[14];
            param[0] = new SqlParameter("@settingID", obj.settingID);
            param[1] = new SqlParameter("@isDelivery", obj.isDelivery);
            param[2] = new SqlParameter("@freeDeliveryDistance", obj.freeDeliveryDistance);
            param[3] = new SqlParameter("@exceededLimitsOK", obj.exceededLimitsOK);
            param[4] = new SqlParameter("@exceededLimitCharge", obj.exceededLimitCharge);
            param[5] = new SqlParameter("@exceededLimitChargeUnit", obj.exceededLimitChargeUnit);
            param[6] = new SqlParameter("@modifiedDate", obj.modifiedDate);
            param[7] = new SqlParameter("@modifiedBy", obj.modifiedBy);
            param[8] = new SqlParameter("@realMenuImage", obj.realMenuImage);
            param[9] = new SqlParameter("@deliveryChargeUnit", obj.deliveryChargeUnit);
            param[10] = new SqlParameter("@deliveryCharge", obj.deliveryCharge);
            param[11] = new SqlParameter("@minimumOrderAmount", obj.minimumOrderAmount);
            param[12] = new SqlParameter("@belowLimitOK", obj.belowLimitsOK);
            param[13] = new SqlParameter("@belowLimitCharge", obj.belowLimitCharge);            
            result = SqlHelper.ExecuteNonQuery(Misc.connStr, CommandType.StoredProcedure, "spUpdateRestaurantSetting", param);
        }
        catch (Exception ex)
        {
            return 0;
        }
        return result;
    }
    #endregion

    #region Functions to get all
    public static DataTable getRestaurantSettingByResturantOwnerID(BLLRestaurantSetting obj)
    {
        DataTable dt = null;
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@createdBy", obj.createdBy);
            dt = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetRestaurantSettingByResturantOwnerID",param).Tables[0];
            if (dt != null && dt.Rows.Count > 0)
            {
                return dt;
            }
        }
        catch (Exception ex)
        {
            return null;
        }
        return dt;
    }
    #endregion

    #region Functions to get all
    public static DataTable getRestaurantSettingByResturantID(BLLRestaurantSetting obj)
    {
        DataTable dt = null;
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@restaurantId", obj.restaurantId);
            dt = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetRestaurantSettingByResturantID", param).Tables[0];
            if (dt != null && dt.Rows.Count > 0)
            {
                return dt;
            }
        }
        catch (Exception ex)
        {
            return null;
        }
        return dt;
    }
    #endregion
}
