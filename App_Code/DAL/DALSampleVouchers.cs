using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SQLHelper;
using System.Data;
using System.Data.SqlClient;
/// <summary>
/// Summary cityDescription for DALSampleVouchers
/// </summary>
public static class DALSampleVouchers
{
    public static int createSampleVouchers(BLLSampleVouchers obj)
    {
        int result = 0;
        try 
        {
            SqlParameter[] param = new SqlParameter[5];
            param[0] = new SqlParameter("@dealId", obj.dealId);
            param[1] = new SqlParameter("@dealOrderCode", obj.dealOrderCode);
            param[2] = new SqlParameter("@isUsed", obj.isUsed);
            param[3] = new SqlParameter("@voucherSecurityCode", obj.voucherSecurityCode);
            param[4] = new SqlParameter("@detailID", obj.detailID);
            result = SqlHelper.ExecuteNonQuery(Misc.connStr, CommandType.StoredProcedure, "spCreateSampleVouchers", param);
        }
        catch (Exception ex)
        { 
        
        }
        return result;
    }
    public static int updateSampleVouchers(BLLSampleVouchers obj)
    {
        int result = 0;
        try
        {
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@vID", obj.vID);
            param[1] = new SqlParameter("@detailID", obj.detailID);
            param[2] = new SqlParameter("@isUsed", obj.isUsed);            
            result = SqlHelper.ExecuteNonQuery(Misc.connStr, CommandType.StoredProcedure, "spUpdateSampleVouchers", param);
        }
        catch (Exception ex)
        {

        }
        return result;
    }

    public static DataTable getSampleVoucherByDealOrderCode(BLLSampleVouchers obj)
    {
        DataTable dt = null;
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@dealOrderCode", obj.dealOrderCode);
            dt = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetSampleVoucherByDealOrderCode", param).Tables[0];
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

    public static int deleteSampleVouchers(BLLSampleVouchers obj)
    {
        int result = 0;
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@vID", obj.vID);
            result = SqlHelper.ExecuteNonQuery(Misc.connStr, CommandType.StoredProcedure, "spDeleteSampleVouchers", param);
        }
        catch (Exception ex)
        {

        }
        return result;
    }

    public static DataTable getTop1UnusedSampleVouchersByDealID(BLLSampleVouchers obj)
    {
        DataTable dt = null; 
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@dealId", obj.dealId);
            dt = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetTop1UnusedSampleVouchersByDealID", param).Tables[0];
        }
        catch (Exception ex)
        {
        }
        return dt;
    }

    public static DataTable getAllSampleVouchersByDealID(BLLSampleVouchers obj)
    {
        DataTable dt = null;
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@dealId", obj.dealId);
            dt = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetAllSampleVouchersByDealID", param).Tables[0];
        }
        catch (Exception ex)
        {

        }
        return dt;
    }   

}
