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
/// Summary description for DALWithdrawRequest
/// </summary>
public class DALWithdrawRequest
{
    public static int createWithdrawRequest(BLLWithdrawRequest obj)
    {
        int result = 0;
        try
        {
            SqlParameter[] param = new SqlParameter[7];
            param[0] = new SqlParameter("@createdBy", obj.createdBy);
            param[1] = new SqlParameter("@creationDate", obj.creationDate);
            param[2] = new SqlParameter("@PreferredMethod", obj.PreferredMethod);
            param[3] = new SqlParameter("@requestAction", obj.requestAction);
            param[4] = new SqlParameter("@requestAmount", obj.requestAmount);
            param[5] = new SqlParameter("@requestCurrencyCode", obj.requestCurrencyCode);
            param[6] = new SqlParameter("@requestUserType", obj.requestUserType);            
            result = SqlHelper.ExecuteNonQuery(Misc.connStr, CommandType.StoredProcedure, "spCreateWithdrawRequest", param);
        }
        catch (Exception ex)
        {
            
        }
        return result;
    }


    public static int updateWithdrawRequestByAdmin(BLLWithdrawRequest obj)
    {
        int result = 0;
        try
        {
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@requestId", obj.requestId);
            param[1] = new SqlParameter("@modifiedBy", obj.modifiedBy);
            param[2] = new SqlParameter("@modifiedDate", obj.modifiedDate);
            param[3] = new SqlParameter("@requestAction", obj.requestAction);                                    
            result = SqlHelper.ExecuteNonQuery(Misc.connStr, CommandType.StoredProcedure, "spUpdateWithdrawRequestByAdmin", param);
        }
        catch (Exception ex)
        {
            
        }
        return result;
    }


    

    public static DataTable getWithdrawRequestByUserID(BLLWithdrawRequest obj)
    {
        DataTable dt = null;
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@createdBy", obj.createdBy);
            dt = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetWithdrawRequestByUserID", param).Tables[0];
            return dt;
        }
        catch (Exception ex)
        {
            
        }
        return dt;
    }

    public static DataTable getWithdrawRequestForReturantOwnerByUserID(BLLWithdrawRequest obj)
    {
        DataTable dt = null;
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@createdBy", obj.createdBy);
            dt = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetWithdrawRequestForReturantOwnerByUserID", param).Tables[0];
            return dt;
        }
        catch (Exception ex)
        {
            
        }
        return dt;
    }

    public static DataTable getWithdrawRequestForReturantOwnerByUserIDOfGivenMonth(BLLWithdrawRequest obj)
    {
        DataTable dt = null;
        try
        {
            DateTime start = new DateTime(obj.year, obj.month, 1);
            DateTime end = start.AddMonths(1).AddDays(-1);
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@createdBy", obj.createdBy);
            param[1] = new SqlParameter("@startDate", start.ToShortDateString());
            param[2] = new SqlParameter("@endDate", end.ToShortDateString());            
            dt = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetWithdrawRequestForReturantOwnerByUserIDOfGivenMonth", param).Tables[0];
            return dt;
        }
        catch (Exception ex)
        {
            
        }
        return dt;
    }


    public static DataTable getAllWithdrawmMoneyByUserIDTillGivenMonth(BLLWithdrawRequest obj)
    {
        DataTable dt = null;
        try
        {
            DateTime start = new DateTime(obj.year, obj.month, 1);
            DateTime end = start.AddMonths(1).AddDays(-1);
            start = new DateTime(2009, 1, 1);
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@createdBy", obj.createdBy);
            param[1] = new SqlParameter("@startDate", start.ToShortDateString());
            param[2] = new SqlParameter("@endDate", end.ToShortDateString());
            dt = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetAllWithdrawmMoneyByUserIDTillGivenMonth", param).Tables[0];
            return dt;
        }
        catch (Exception ex)
        {
            
        }
        return dt;
    }


    

    public static DataTable getAllWithdrawRequests(BLLWithdrawRequest obj)
    {
        DataTable dt = null;
        try
        {            
            dt = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetAllWithdrawRequests").Tables[0];
            return dt;
        }
        catch (Exception ex)
        {
            
        }
        return dt;
    }

    
    
}
