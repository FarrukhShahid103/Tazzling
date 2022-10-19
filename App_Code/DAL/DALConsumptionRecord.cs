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
/// Summary description for DALConsumptionRecord
/// </summary>
public class DALConsumptionRecord
{

    public static int createConsumptionRecord(BLLConsumptionRecord obj)
    {
        int result = 0;
        try
        {
            SqlParameter[] param = new SqlParameter[7];

            param[0] = new SqlParameter("@consumptionAmount", obj.consumptionAmount);
            param[1] = new SqlParameter("@consumptionType", obj.consumptionType);
            if (obj.orderId == 0)
            {
                param[2] = new SqlParameter("@orderId", DBNull.Value);
            }
            else
            {
                param[2] = new SqlParameter("@orderId", obj.orderId);
            }
            param[3] = new SqlParameter("@createdBy", obj.createdBy);
            param[4] = new SqlParameter("@creationDate", obj.creationDate);
            param[5] = new SqlParameter("@currencyCode", obj.currencyCode);
            param[6] = new SqlParameter("@isOrder", obj.isOrder);

            result = SqlHelper.ExecuteNonQuery(Misc.connStr, CommandType.StoredProcedure, "spCreateConsumptionRecord", param);
        }
        catch (Exception ex)
        {
            return 0;
        }
        return result;
    }

    public static DataTable getAllUsedCommssionByUserID(BLLConsumptionRecord obj)
    {
        DataTable dt = null;
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@createdBy", obj.createdBy);
            dt = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetAllUsedCommssionByUserID", param).Tables[0];
        }
        catch (Exception ex)
        {
            return null;
        }
        return dt;
    }

    public static DataTable getAllWithdrawmMoneyByUserID(BLLConsumptionRecord obj)
    {
        DataTable dt = null;
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@createdBy", obj.createdBy);
            dt = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetAllWithdrawmMoneyByUserID", param).Tables[0];
        }
        catch (Exception ex)
        {
            return null;
        }
        return dt;
    }

    public static int deleteConsumptionRecordByOrderId(BLLConsumptionRecord obj)
    {
        int result = 0;
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@orderId", obj.orderId);
            result = SqlHelper.ExecuteNonQuery(Misc.connStr, CommandType.StoredProcedure, "spDeleteConsumptionRecordByOrderId", param);
        }
        catch (Exception ex)
        {
            return 0;
        }
        return result;
    }
}
