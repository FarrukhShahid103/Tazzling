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
/// Summary description for DALCommission
/// </summary>
public static class DALCommission
{
    public static int createCommission(BLLCommission objCom)
    {
        int result = 0;
        try 
        {
            SqlParameter[] param = new SqlParameter[7];
            param[0] = new SqlParameter("@cMoney", objCom.cMoney);
            param[1] = new SqlParameter("@isDoubled", objCom.isDoubled);
            param[2] = new SqlParameter("@orderId", objCom.orderId);
            param[3] = new SqlParameter("@userId", objCom.userId);
            param[4] = new SqlParameter("@refreeId", objCom.refreeId);
            param[5] = new SqlParameter("@createdDate", objCom.createdDate);
            param[6] = new SqlParameter("@isSalesComission", objCom.isSalesComission);
            result = SqlHelper.ExecuteNonQuery(Misc.connStr, CommandType.StoredProcedure, "spCreateCommission", param);
        }
        catch (Exception ex)
        {
            
        }
        return result;
    }
    public static int updateCommission(BLLCommission objCom)
    {
        int result = 0;
        try
        {
            DateTime start = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            DateTime end = start.AddMonths(1).AddDays(-1);
            SqlParameter[] param = new SqlParameter[3];                                   
            param[0] = new SqlParameter("@userId", objCom.userId);
            param[1] = new SqlParameter("@startDate", start.ToShortDateString());
            param[2] = new SqlParameter("@endDate", end.ToShortDateString());
            result = SqlHelper.ExecuteNonQuery(Misc.connStr, CommandType.StoredProcedure, "spUpdateCommission", param);
        }
        catch (Exception ex)
        {
            
        }
        return result;
    }

    public static int updateCommissionSingle(BLLCommission objCom)
    {
        int result = 0;
        try
        {
            DateTime start = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            DateTime end = start.AddMonths(1).AddDays(-1);
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@userId", objCom.userId);
            param[1] = new SqlParameter("@startDate", start.ToShortDateString());
            param[2] = new SqlParameter("@endDate", end.ToShortDateString());
            result = SqlHelper.ExecuteNonQuery(Misc.connStr, CommandType.StoredProcedure, "spUpdateCommissionSingle", param);
        }
        catch (Exception ex)
        {
            
        }
        return result;
    }

    public static int deleteCommission(BLLCommission objCom)
    {
        int result = 0;
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@orderId", objCom.orderId);
            result = SqlHelper.ExecuteNonQuery(Misc.connStr, CommandType.StoredProcedure, "spDeleteCommission", param);
        }
        catch (Exception ex)
        {
            
        }
        return result;
    }
    public static DataTable getCommissionByOrderID(BLLCommission objCom)
    {
        DataTable dt = null;
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@orderId", objCom.orderId);
            dt = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetCommissionByOrderID", param).Tables[0];
        }
        catch (Exception ex)
        {
            return null;
        }
        return dt;
    }
    public static DataTable getAllCommssionByUserID(BLLCommission objCom)
    {
        DataTable dt = null;
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@userId", objCom.userId);
            dt = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetAllCommssionByUserID", param).Tables[0];
        }
        catch (Exception ex)
        {
            return null;
        }
        return dt;
    }
    public static DataTable getCommissionOfCurrentMonthByUserID(BLLCommission objCom)
    {
        DataTable dt = null;
        try
        {
            DateTime start = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            DateTime end = start.AddMonths(1).AddDays(-1);
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@userId", objCom.userId);
            param[1] = new SqlParameter("@startDate", start.ToShortDateString());
            param[2] = new SqlParameter("@endDate", end.ToShortDateString());
            dt = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetCommissionOfCurrentMonthByUserID", param).Tables[0];
            return dt;
        }
        catch (Exception ex)
        {
            return dt;
        }

    }
    public static DataTable getCommissionMoneyByUserIDAndRefreeIDAndGivenDate(BLLCommission objCom)
    {
        DataTable dt = null;
        try
        {
            DateTime start = new DateTime(objCom.comissionYear, objCom.comissionMonth, 1);
            DateTime end = start.AddMonths(1).AddDays(-1);

            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@userId", objCom.userId);
            param[1] = new SqlParameter("@refreeId", objCom.refreeId);
            param[2] = new SqlParameter("@startDate", start);
            param[3] = new SqlParameter("@endDate", end);
            dt = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetCommissionMoneyByUserIDAndRefreeIDAndGivenDate", param).Tables[0];
            return dt;
        }
        catch (Exception ex)
        {
            return dt;
        }

    }

    public static DataTable getCommissionMoneyByUserIDAndRefreeIDAndGivenDateForSalePerson(BLLCommission objCom)
    {
        DataTable dt = null;
        try
        {
            DateTime start = new DateTime(objCom.comissionYear, objCom.comissionMonth, 1);
            DateTime end = start.AddMonths(1).AddDays(-1);

            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@userId", objCom.userId);
            param[1] = new SqlParameter("@refreeId", objCom.refreeId);
            param[2] = new SqlParameter("@startDate", start);
            param[3] = new SqlParameter("@endDate", end);
            dt = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetCommissionMoneyByUserIDAndRefreeIDAndGivenDateForSalePerson", param).Tables[0];
            return dt;
        }
        catch (Exception ex)
        {
            return dt;
        }

    }

    public static DataTable getCommissionMoneyByUserIDAndRefreeID(BLLCommission objCom)
    {
        DataTable dt = null;
        try
        {
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@userId", objCom.userId);
            param[1] = new SqlParameter("@refreeId", objCom.refreeId);
            dt = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetCommissionMoneyByUserIDAndRefreeID", param).Tables[0];
            return dt;
        }
        catch (Exception ex)
        {
            return dt;
        }

    }

    public static DataTable getCommissionMoneyByUserIDAndRefreeIDForSalePerson(BLLCommission objCom)
    {
        DataTable dt = null;
        try
        {
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@userId", objCom.userId);
            param[1] = new SqlParameter("@refreeId", objCom.refreeId);
            dt = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetCommissionMoneyByUserIDAndRefreeIDForSalePerson", param).Tables[0];
            return dt;
        }
        catch (Exception ex)
        {
            return dt;
        }

    }

    
    
    
}
