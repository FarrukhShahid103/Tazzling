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
public static class DALResendOrders
{
    public static int createResendOrders(BLLResendOrders obj)
    {
        int result = 0;
        try 
        {
            SqlParameter[] param = new SqlParameter[9];
            param[0] = new SqlParameter("@dealId", obj.dealId);
            param[1] = new SqlParameter("@resendOrder_Address", obj.resendOrder_Address);
            param[2] = new SqlParameter("@resendOrder_CustomerName", obj.resendOrder_CustomerName);
            param[3] = new SqlParameter("@resendOrder_Image", obj.resendOrder_Image);
            param[4] = new SqlParameter("@resendOrder_Note", obj.resendOrder_Note);
            param[5] = new SqlParameter("@resendOrder_Telephone", obj.resendOrder_Telephone);
            param[6] = new SqlParameter("@resendOrder_VoucherNumber", obj.resendOrder_VoucherNumber);
            param[7] = new SqlParameter("@resendOrder_createdDate", obj.resendOrder_createdDate);
            param[8] = new SqlParameter("@resendOrder_trackingNumber", obj.resendOrder_trackingNumber);
                       
            result = SqlHelper.ExecuteNonQuery(Misc.connStr, CommandType.StoredProcedure, "spCreateResendOrders", param);
        }
        catch (Exception ex)
        { 
        
        }
        return result;
    }
    public static int updateResendOrders(BLLResendOrders obj)
    {
        int result = 0;
        try
        {
            SqlParameter[] param = new SqlParameter[8];
            param[0] = new SqlParameter("@resendOrderID", obj.resendOrderID);
            param[1] = new SqlParameter("@resendOrder_Address", obj.resendOrder_Address);
            param[2] = new SqlParameter("@resendOrder_CustomerName", obj.resendOrder_CustomerName);
            param[3] = new SqlParameter("@resendOrder_Image", obj.resendOrder_Image);
            param[4] = new SqlParameter("@resendOrder_Note", obj.resendOrder_Note);
            param[5] = new SqlParameter("@resendOrder_Telephone", obj.resendOrder_Telephone);
            param[6] = new SqlParameter("@resendOrder_VoucherNumber", obj.resendOrder_VoucherNumber);            
            param[7] = new SqlParameter("@resendOrder_trackingNumber", obj.resendOrder_trackingNumber);
            result = SqlHelper.ExecuteNonQuery(Misc.connStr, CommandType.StoredProcedure, "spUpdateResendOrders", param);
        }
        catch (Exception ex)
        {

        }
        return result;
    }

    public static int updateResendOrdersTrackingNumber(BLLResendOrders obj)
    {
        int result = 0;
        try
        {
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@resendOrderID", obj.resendOrderID);
            param[1] = new SqlParameter("@resendOrder_trackingNumber", obj.resendOrder_trackingNumber);
            result = SqlHelper.ExecuteNonQuery(Misc.connStr, CommandType.StoredProcedure, "spUpdateResendOrdersTrackingNumber", param);
        }
        catch (Exception ex)
        {

        }
        return result;
    }

    public static int deleteResendOrders(BLLResendOrders obj)
    {
        int result = 0;
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@resendOrderID", obj.resendOrderID);
            result = SqlHelper.ExecuteNonQuery(Misc.connStr, CommandType.StoredProcedure, "spDeleteResendOrderByID", param);
        }
        catch (Exception ex)
        {

        }
        return result;
    }
    public static DataTable getResendOrdersById(BLLResendOrders obj)
    {
        DataTable dt = null; 
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@resendOrderID", obj.resendOrderID);
            dt = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetResendOrdersById", param).Tables[0];
        }
        catch (Exception ex)
        {

        }
        return dt;
    }
    public static DataTable getResendOrdersByDealId(BLLResendOrders obj)
    {
        DataTable dt = null;
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@dealId", obj.dealId);
            dt = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetResendOrdersByDealId", param).Tables[0];
        }
        catch (Exception ex)
        {

        }
        return dt;
    }   

}
