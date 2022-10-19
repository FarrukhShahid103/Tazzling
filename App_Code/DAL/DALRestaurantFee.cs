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
/// Summary description for DALRestaurantFee
/// </summary>
public static class DALRestaurantFee
{
    #region Function to create new
    public static long createRF(BLLRestaurantFee  obj)
    {
        long result = 0;
        try
        {
            SqlParameter[] param = new SqlParameter[6];
            param[0] = new SqlParameter("@rfAmount", obj.rfAmount);
            param[1] = new SqlParameter("@rfDescription", obj.rfDescription);
            param[2] = new SqlParameter("@restaurantId", obj.restaurantId);
            param[3] = new SqlParameter("@creationDate", obj.creationDate);
            param[4] = new SqlParameter("@createdBy", obj.createdBy);
            param[5] = new SqlParameter("@isFee", obj.isFee);
            result = Convert.ToInt64(SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spCreateRF", param).Tables[0].Rows[0][0]);
        }
        catch (Exception ex)
        {
            return 0;
        }
        return result;
    }
    #endregion

    #region function to delete 
    public static int deleteRF(BLLRestaurantFee obj)
    {
        int result = 0;
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@rfID", obj.rfID);
            result = SqlHelper.ExecuteNonQuery(Misc.connStr, CommandType.StoredProcedure, "spDeleteRF", param);
        }
        catch (Exception ex)
        {
            return 0;
        }
        return result;
    }
    #endregion

    #region Function to Update
    public static int updateRF(BLLRestaurantFee obj)
    {
        int result = 0;
        try
        {
            SqlParameter[] param = new SqlParameter[7];
            param[0] = new SqlParameter("@rfID", obj.rfID);
            param[1] = new SqlParameter("@rfAmount", obj.rfAmount);
            param[2] = new SqlParameter("@rfDescription", obj.rfDescription);
            param[3] = new SqlParameter("@restaurantId", obj.restaurantId);
            param[4] = new SqlParameter("@ModifiedDate", obj.modifiedDate);
            param[5] = new SqlParameter("@modifiedBy", obj.modifiedBy);
            param[6] = new SqlParameter("@isFee", obj.isFee);            
            result = SqlHelper.ExecuteNonQuery(Misc.connStr, CommandType.StoredProcedure, "spUpdateRF", param);
        }
        catch (Exception ex)
        {
            return 0;
        }
        return result;
    }
    #endregion

    #region Functions to get comments by RestaurantID
    public static DataTable getRFByID(BLLRestaurantFee obj)
    {
        DataTable dt = null;
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@rfID", obj.rfID);
            dt = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetRFByID", param).Tables[0];
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

    public static DataTable getAllRFForAdmin(BLLRestaurantFee obj)
    {
        DataTable dt = null;
        try
        {            
            dt = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetAllRFForAdmin").Tables[0]; 
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

    public static DataTable getAllRFByRestaurantID(BLLRestaurantFee obj)
    {
        DataTable dt = null;
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@restaurantId", obj.restaurantId);
            dt = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetAllRFByRestaurantID", param).Tables[0];
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

    public static DataTable getTotalOfRestaurantFeeByRestaurantID(BLLRestaurantFee obj)
    {
        DataTable dt = null;
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@restaurantId", obj.restaurantId);
            dt = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetTotalOfRestaurantFeeByRestaurantID", param).Tables[0];
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



    public static DataTable getTotalOfRestaurantFeeByRestaurantIDTillGivenMonth(BLLRestaurantFee obj)
    {
        DataTable dt = null;
        try
        {
            DateTime start = new DateTime(obj.year, obj.month, 1);
            DateTime end = start.AddMonths(1).AddDays(-1);
            start = new DateTime(2009, 1, 1);
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@restaurantId", obj.restaurantId);
            param[1] = new SqlParameter("@startDate", start.ToShortDateString());
            param[2] = new SqlParameter("@endDate", end.ToShortDateString());
            dt = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetTotalOfRestaurantFeeByRestaurantIDTillGivenMonth", param).Tables[0];
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

    public static DataTable getRestaurantFeeOfGivenMonthByRestaurantID(BLLRestaurantFee obj)
    {
        DataTable dt = null;
        try
        {
            DateTime start = new DateTime(obj.year, obj.month, 1);
            DateTime end = start.AddMonths(1).AddDays(-1);
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@restaurantId", obj.restaurantId);
            param[1] = new SqlParameter("@startDate", start.ToShortDateString());
            param[2] = new SqlParameter("@endDate", end.ToShortDateString());            
            dt = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetRestaurantFeeOfGivenMonthByRestaurantID", param).Tables[0];
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
