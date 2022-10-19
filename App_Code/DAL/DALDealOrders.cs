using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using SQLHelper;

/// <summary>
/// Summary description for DALDealOrders
/// </summary>
public class DALDealOrders
{

    #region Function to Create New Deal Order

    public static long createNewDealOrder(BLLDealOrders objBLLDealOrders)
    {
        long result = 0;
        try
        {
            SqlParameter[] param = new SqlParameter[23];
            param[0] = new SqlParameter("@dealId", objBLLDealOrders.dealId);
            param[1] = new SqlParameter("@orderNo", objBLLDealOrders.orderNo);
            param[2] = new SqlParameter("@createdBy", objBLLDealOrders.createdBy);
            param[3] = new SqlParameter("@createdDate", objBLLDealOrders.createdDate);
            param[4] = new SqlParameter("@psgTranNo", objBLLDealOrders.psgTranNo);
            param[5] = new SqlParameter("@Qty", objBLLDealOrders.Qty);
            param[6] = new SqlParameter("@status", objBLLDealOrders.status);
            param[7] = new SqlParameter("@userId", objBLLDealOrders.userId);
            param[8] = new SqlParameter("@ccInfoID", objBLLDealOrders.ccInfoID);
            param[9] = new SqlParameter("@personalQty", objBLLDealOrders.personalQty);
            param[10] = new SqlParameter("@giftQty", objBLLDealOrders.giftQty);
            param[11] = new SqlParameter("@ccCreditUsed", objBLLDealOrders.ccCreditUsed);
            param[12] = new SqlParameter("@tastyCreditUsed", objBLLDealOrders.tastyCreditUsed);
            param[13] = new SqlParameter("@totalAmt", objBLLDealOrders.totalAmt);
            param[14] = new SqlParameter("@addressId", objBLLDealOrders.addressId);
            param[15] = new SqlParameter("@psgError", objBLLDealOrders.psgError);
            param[16] = new SqlParameter("@comissionMoneyUsed", objBLLDealOrders.comissionMoneyUsed);
            param[17] = new SqlParameter("@orderFrom", objBLLDealOrders.orderFrom);
            param[18] = new SqlParameter("@shippingAndTaxAmount", objBLLDealOrders.shippingAndTaxAmount);
            if (objBLLDealOrders.shippingInfoId == 0)
            {
                param[19] = new SqlParameter("@ShippingInfoId", DBNull.Value);
            }
            else
            {
                param[19] = new SqlParameter("@ShippingInfoId", objBLLDealOrders.shippingInfoId);
            }
            param[20] = new SqlParameter("@orderIPAddress", objBLLDealOrders.orderIPAddress);
            if (objBLLDealOrders.size == "sher")
            {
                param[21] = new SqlParameter("@size", "");
            }
            else
            {
                param[21] = new SqlParameter("@size", objBLLDealOrders.size);
            }
            param[22] = new SqlParameter("@resendOrders", objBLLDealOrders.resendOrders);            
            result = Convert.ToInt64(SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spCreateNewDealOrder", param).Tables[0].Rows[0][0]);
        }
        catch (Exception ex)
        {
            return 0;
        }
        return result;
    }

    #endregion

    public static bool changeDealOrderStatus(BLLDealOrders obj)
    {
        int result = 0;
        try
        {
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@dOrderID", obj.dOrderID);
            param[1] = new SqlParameter("@status", obj.status);
            param[2] = new SqlParameter("@modifiedDate", obj.modifiedDate);
            result = SqlHelper.ExecuteNonQuery(Misc.connStr, CommandType.StoredProcedure, "spChangeDealOrderStatus", param);
            if (result == -1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public static DataTable getAllGiftAvailableDealOrderDetailByUserID(BLLDealOrders obj)
    {
        DataTable dt = null;
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@createdBy", obj.createdBy);
            dt = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetAllGiftAvailableDealOrderDetailByUserID", param).Tables[0];
            return dt;
        }
        catch (Exception ex)
        {
            return dt;
        }
    }

    public static DataTable getAllGiftUsedDealOrderDetailByUserID(BLLDealOrders obj)
    {
        DataTable dt = null;
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@createdBy", obj.createdBy);
            dt = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetAllGiftUsedDealOrderDetailByUserID", param).Tables[0];
            return dt;
        }
        catch (Exception ex)
        {
            return dt;
        }
    }

    public static DataTable getAllGiftCancelledDealOrderDetailByUserID(BLLDealOrders obj)
    {
        DataTable dt = null;
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@createdBy", obj.createdBy);
            dt = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetAllGiftCancelledDealOrderDetailByUserID", param).Tables[0];
            return dt;
        }
        catch (Exception ex)
        {
            return dt;
        }
    }

    #region Function to Update Deal order Status by Order Id

    public static int updateDealOrderStatusByOrderId(BLLDealOrders objBLLDealOrders)
    {
        int result = 0;
        try
        {
            SqlParameter[] param = new SqlParameter[4];

            param[0] = new SqlParameter("@dOrderID", objBLLDealOrders.dOrderID);
            param[1] = new SqlParameter("@status", objBLLDealOrders.status);
            param[2] = new SqlParameter("@psgTranNo", objBLLDealOrders.psgTranNo);
            param[3] = new SqlParameter("@psgError", objBLLDealOrders.psgError);
            result = SqlHelper.ExecuteNonQuery(Misc.connStr, CommandType.StoredProcedure, "spUpdateDealOrderStatusBydOrderId", param);
        }
        catch (Exception ex)
        {
            return 0;
        }
        return result;
    }

    #endregion

    #region Functions to get all Deal Orders

    public static DataTable getTotalDealOrdersCountByDealId(BLLDealOrders obj)
    {
        DataTable dt = null;
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@dealId", obj.dealId);
            dt = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetTotalDealOrdersCountByDealId", param).Tables[0];
            return dt;
        }
        catch (Exception ex)
        {
            return dt;
        }
    }

    public static DataTable getTotalPersonalQtyOfDealOrdersByDealAndUserId(BLLDealOrders obj)
    {
        DataTable dt = null;
        try
        {
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@dealId", obj.dealId);
            param[1] = new SqlParameter("@userId", obj.userId);
            dt = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetTotalPersonalQtyOfDealOrdersByDealAndUserId", param).Tables[0];
            return dt;
        }
        catch (Exception ex)
        {
            return dt;
        }
    }

    public static DataTable getTotalSoldDealsQTY()
    {
        DataTable dtDealOrders = null;

        try
        {
            dtDealOrders = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetTotalSoldDealsQTY").Tables[0];

            if (dtDealOrders != null && dtDealOrders.Rows.Count > 0)
            {
                return dtDealOrders;
            }
        }
        catch (Exception ex)
        {
            return null;
        }

        return dtDealOrders;
    }

    public static DataTable getDealOrderDetailByUserID(BLLDealOrders obj)
    {
        DataTable dt = null;
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@createdBy", obj.createdBy);
            dt = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetDealOrderDetailByUserID", param).Tables[0];
            return dt;
        }
        catch (Exception ex)
        {
            return dt;
        }
    }

    public static DataTable getAllGiftDealOrderDetailByUserID(BLLDealOrders obj)
    {
        DataTable dt = null;
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@createdBy", obj.createdBy);
            dt = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetAllGiftDealOrderDetailByUserID", param).Tables[0];
            return dt;
        }
        catch (Exception ex)
        {
            return dt;
        }
    }

    public static DataTable getAllOwnDealOrderDetailByUserID(BLLDealOrders obj)
    {
        DataTable dt = null;
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@createdBy", obj.createdBy);
            dt = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetAllOwnDealOrderDetailByUserID", param).Tables[0];
            return dt;
        }
        catch (Exception ex)
        {
            return dt;
        }
    }

    public static DataTable getAllOwnExpiredDealOrderDetailByUserID(BLLDealOrders obj)
    {
        DataTable dt = null;
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@createdBy", obj.createdBy);
            dt = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetAllOwnExpiredDealOrderDetailByUserID", param).Tables[0];
            return dt;
        }
        catch (Exception ex)
        {
            return dt;
        }
    }

    public static DataTable getAllGiftExpiredDealOrderDetailByUserID(BLLDealOrders obj)
    {
        DataTable dt = null;
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@createdBy", obj.createdBy);
            dt = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetAllGiftExpiredDealOrderDetailByUserID", param).Tables[0];
            return dt;
        }
        catch (Exception ex)
        {
            return dt;
        }
    }

    public static DataTable getAllOwnAvailableDealOrderDetailByUserID(BLLDealOrders obj)
    {
        DataTable dt = null;
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@createdBy", obj.createdBy);
            dt = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetAllOwnAvailableDealOrderDetailByUserID", param).Tables[0];
            return dt;
        }
        catch (Exception ex)
        {
            return dt;
        }
    }

    public static DataTable getAllOwnUsedDealOrderDetailByUserID(BLLDealOrders obj)
    {
        DataTable dt = null;
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@createdBy", obj.createdBy);
            dt = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetAllOwnUsedDealOrderDetailByUserID", param).Tables[0];
            return dt;
        }
        catch (Exception ex)
        {
            return dt;
        }
    }

    public static DataTable getAllOwnCancelledDealOrderDetailByUserID(BLLDealOrders obj)
    {
        DataTable dt = null;
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@createdBy", obj.createdBy);
            dt = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetAllOwnCancelledDealOrderDetailByUserID", param).Tables[0];
            return dt;
        }
        catch (Exception ex)
        {
            return dt;
        }
    }

    public static DataTable getDealOrderDetailByCCInfoID(BLLDealOrders obj)
    {
        DataTable dt = null;
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@ccInfoID", obj.ccInfoID);
            dt = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetDealOrderDetailByCCInfoID", param).Tables[0];
            return dt;
        }
        catch (Exception ex)
        {
            return dt;
        }
    }

    public static DataTable getProductOrderDetailByOrderID(BLLDealOrders obj)
    {
        DataTable dt = null;
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@dOrderID", obj.dOrderID);
            dt = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetProductOrderDetailByOrderID", param).Tables[0];
            return dt;
        }
        catch (Exception ex)
        {
            return dt;
        }
    }

    public static DataTable getDealOrderDetailByOrderID(BLLDealOrders obj)
    {
        DataTable dt = null;
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@dOrderID", obj.dOrderID);
            dt = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetDealOrderDetailByOrderID", param).Tables[0];
            return dt;
        }
        catch (Exception ex)
        {
            return dt;
        }
    }
    
    public static DataTable getDealOrderDetailByOrderID_New(BLLDealOrders obj)
    {
        DataTable dt = null;
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@dOrderID", obj.dOrderID);
            dt = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetDealOrderDetailByOrderID_New", param).Tables[0];
            return dt;
        }
        catch (Exception ex)
        {
            return dt;
        }
    }

    public static DataTable getDealOrderDetailByOrderNo(BLLDealOrders obj)
    {
        DataTable dt = null;
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@orderNo", obj.orderNo);
            dt = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetDealOrderDetailByOrderNo", param).Tables[0];
            return dt;
        }
        catch (Exception ex)
        {
            return dt;
        }
    }

    public static DataTable getDealOrderDetailByOrderNoForProduct(BLLDealOrders obj)
    {
        DataTable dt = null;
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@orderNo", obj.orderNo);
            dt = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetDealOrderDetailByOrderNoForProduct", param).Tables[0];
            return dt;
        }
        catch (Exception ex)
        {
            return dt;
        }
    }

    public static DataTable getVouchersDetailByOrderID(BLLDealOrders obj)
    {
        DataTable dt = null;
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@dOrderID", obj.dOrderID);
            dt = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetVouchersDetailByOrderID", param).Tables[0];
            return dt;
        }
        catch (Exception ex)
        {
            return dt;
        }
    }

    public static DataTable getBusinessDealOrderBuIserID(int iUserId)
    {
        DataTable dt = null;

        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@userId", iUserId);
            dt = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetBusinessDealOrdersByUserID", param).Tables[0];
            return dt;
        }
        catch (Exception ex)
        {
            return dt;
        }
    }

    public static DataTable getAllOrdersWithCCDetailByDealID(BLLDealOrders obj)
    {
        DataTable dt = null;
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@dealId", obj.dealId);
            dt = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetAllOrdersWithCCDetailByDealID", param).Tables[0];
            return dt;
        }
        catch (Exception ex)
        {
            return dt;
        }
    }

    public static DataTable getTopTenOrdersWithCCDetailByDealID(BLLDealOrders obj)
    {
        DataTable dt = null;
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@dealId", obj.dealId);
            dt = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetTopTenOrdersWithCCDetailByDealID", param).Tables[0];
            return dt;
        }
        catch (Exception ex)
        {
            return dt;
        }
    }

    public static DataTable getAllSuccessfulOrdersByDealID(BLLDealOrders obj)
    {
        DataTable dt = null;
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@dealId", obj.dealId);
            dt = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetAllSuccessfulOrdersByDealID", param).Tables[0];
            return dt;
        }
        catch (Exception ex)
        {
            return dt;
        }
    }

    public static DataTable getAllDealOrders()
    {
        DataTable dtDealOrders = null;

        try
        {
            dtDealOrders = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetAllDealOrders").Tables[0];

            if (dtDealOrders != null && dtDealOrders.Rows.Count > 0)
            {
                return dtDealOrders;
            }
        }
        catch (Exception ex)
        {
            return null;
        }

        return dtDealOrders;
    }

    public static DataSet getAllDealOrdersByPageIndex(int intStartIndex, int intMaxRecords)
    {
        DataSet dstDealOrders = null;

        try
        {
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@startRowIndex", intStartIndex);
            param[1] = new SqlParameter("@maximumRows", intMaxRecords);
            dstDealOrders = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetAllDealOrdersByPageIndex", param);

            //dtDealOrders = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetAllDealOrders").Tables[0];

        }
        catch (Exception ex)
        {
            return null;
        }

        return dstDealOrders;
    }
    
    public static DataTable getDealOrdersCountByUserId(BLLDealOrders obj)
    {
        DataTable dtDealOrders = null;

        try
        {
            SqlParameter[] param = new SqlParameter[1];

            param[0] = new SqlParameter("@userId", obj.userId);

            dtDealOrders = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetDealOrderCountByUserId", param).Tables[0];

            if (dtDealOrders != null && dtDealOrders.Rows.Count > 0)
            {
                return dtDealOrders;
            }
        }
        catch (Exception ex)
        {
            return null;
        }

        return dtDealOrders;
    }

    public static DataTable getAllResendOrdersByProductID(BLLDealOrders obj)
    {
        DataTable dtDealOrders = null;
        try
        {
            SqlParameter[] param = new SqlParameter[1];

            param[0] = new SqlParameter("@dealId", obj.dealId);
            dtDealOrders = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetAllResendOrdersByProductID", param).Tables[0];
            if (dtDealOrders != null && dtDealOrders.Rows.Count > 0)
            {
                return dtDealOrders;
            }
        }
        catch (Exception ex)
        {
            return null;
        }
        return dtDealOrders;
    }

    


    //public static DataTable getAllDealOrders()
    //{
    //    DataTable dtDealOrders = null;

    //    try
    //    {
    //        dtDealOrders = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetAllDealOrders").Tables[0];
    //        if (dtDealOrders != null && dtDealOrders.Rows.Count > 0)
    //        {
    //            return dtDealOrders;
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        return null;
    //    }

    //    return dtDealOrders;
    //}

    #endregion

    public static int updateDealOrderNoteByOrderID(BLLDealOrders obj)
    {
        int result = 0;
        try
        {
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@dOrderID", obj.dOrderID);
            param[1] = new SqlParameter("@customerNote", obj.customerNote);
            result = SqlHelper.ExecuteNonQuery(Misc.connStr, CommandType.StoredProcedure, "spUpdateDealOrderNoteByOrderID", param);
        }
        catch (Exception ex)
        {
            return 0;
        }
        return result;
    }


    public static DataTable GetDealOrdersByUserIDWithDates(BLLDealOrders obj, DateTime dtStartDate, DateTime dtEndDate)
    {
        DataTable dtDealOrders = null;

        try
        {
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@UserID", obj.userId);
            param[1] = new SqlParameter("@CreationStartDate", dtStartDate);
            param[2] = new SqlParameter("@CreationEndDate", dtEndDate);
            dtDealOrders = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "SP_GetDealOrdersByUserIDWithDates", param).Tables[0];

            if (dtDealOrders != null && dtDealOrders.Rows.Count > 0)
            {
                return dtDealOrders;
            }
        }
        catch (Exception ex)
        {
            return null;
        }

        return dtDealOrders;
    }
    public static DataTable GetDealOrdersByUserID(BLLDealOrders obj)
    {
        DataTable dtDealOrders = null;

        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@UserID", obj.userId);
            dtDealOrders = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "SP_GetDealOrdersByUserID", param).Tables[0];

            if (dtDealOrders != null && dtDealOrders.Rows.Count > 0)
            {
                return dtDealOrders;
            }
        }
        catch (Exception ex)
        {
            return null;
        }

        return dtDealOrders;
    }


    public static DataTable ComissionHistoryForAllSalesPersons()
    {
        DataTable dtDealOrders = null;

        try
        {
            dtDealOrders = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "SP_ComissionHistoryForAllSalesPersons").Tables[0];

            if (dtDealOrders != null && dtDealOrders.Rows.Count > 0)
            {
                return dtDealOrders;
            }
        }
        catch (Exception ex)
        {
            return null;
        }

        return dtDealOrders;
    }
    public static DataTable ComissionHistoryForSalePerson(string Email)
    {
        DataTable dtDealOrders = null;

        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@SalePersonAccountName", Email);
            dtDealOrders = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "SP_ComissionHistoryForSalePerson", param).Tables[0];

            if (dtDealOrders != null && dtDealOrders.Rows.Count > 0)
            {
                return dtDealOrders;
            }
        }
        catch (Exception ex)
        {
            return null;
        }

        return dtDealOrders;
    }
    public static int AddUpdateDealCommissionEarnedByDealID(BLLDealOrders objBLLDealOrders, float CommissionEarned)
    {
        int result = 0;
        try
        {
            SqlParameter[] param = new SqlParameter[2];

            param[0] = new SqlParameter("@DealID", objBLLDealOrders.dealId);
            param[1] = new SqlParameter("@CommissionEarned", CommissionEarned);
            result = SqlHelper.ExecuteNonQuery(Misc.connStr, CommandType.StoredProcedure, "sp_AddUpdateDealCommissionEarnedByDealID", param);
        }
        catch (Exception ex)
        {
            return 0;
        }
        return result;
    }

    public static DataTable ComissionHistoryReportForSalePerson(BLLDealOrders obj)
    {
        DataTable dt = null;
        try
        {
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@SalePersonAccountName", obj.SalePersonAccountName);
            param[1] = new SqlParameter("@Year", obj.Year);
            param[2] = new SqlParameter("@Month", obj.Month);
            dt = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "SP_ComissionHistoryReportForSalePerson", param).Tables[0];
            return dt;
        }
        catch (Exception ex)
        {
            return dt;
        }
    }


    public static DataTable GetSalesPerformanceForSuperAdmin()
    {
        DataTable dtDealOrders = null;

        try
        {
            dtDealOrders = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "SP_GetSalesPerformanceForSuperAdmin").Tables[0];

            if (dtDealOrders != null && dtDealOrders.Rows.Count > 0)
            {
                return dtDealOrders;
            }
        }
        catch (Exception ex)
        {
            return null;
        }

        return dtDealOrders;
    }
    public static DataTable ComissionHistoryReportForAllSalesman(BLLDealOrders obj)
    {
        DataTable dt = null;
        try
        {
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@Year", obj.Year);
            param[1] = new SqlParameter("@Month", obj.Month);
            dt = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "SP_ComissionHistoryReportForAllSalesman", param).Tables[0];
            return dt;
        }
        catch (Exception ex)
        {
            return dt;
        }
    }

    public static DataTable GetOrderfromDetail(BLLDealOrders obj)
    {
        DataTable dt = null;
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@dealId", obj.dealId);

            dt = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spOrderfromDetail", param).Tables[0];
            return dt;
        }
        catch (Exception ex)
        {
            return dt;
        }
    }
    public static DataTable GetRedeemedDetail(BLLDealOrders obj)
    {
        DataTable dt = null;
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@dealId", obj.dealId);

            dt = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetRedeemedValue", param).Tables[0];
            return dt;
        }
        catch (Exception ex)
        {
            return dt;
        }
    }
    public static DataTable GetOrderDate(BLLDealOrders obj)
    {
        DataTable dt = null;
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@dealId", obj.dealId);

            dt = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetOrderDate", param).Tables[0];
            return dt;
        }
        catch (Exception ex)
        {
            return dt;
        }
    }
    public static DataTable GetIpAddressfromOrders(BLLDealOrders obj)
    {
        DataTable dt = null;
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@dealId", obj.dealId);

            dt = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetIpAddressfromorders", param).Tables[0];
            return dt;
        }
        catch (Exception ex)
        {
            return dt;
        }
    }

    public static DataTable EditOrderDetail(BLLDealOrders obj)
    {
        DataTable dt = null;
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@dOrderID", obj.dOrderID);
            dt = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spEditOrderDetail", param).Tables[0];
            return dt;
        }
        catch (Exception ex)
        {
            return dt;
        }
    }

    public static bool DeleteOrderDetail(BLLDealOrders obj)
    {
        bool isDelete = false;
        int result = 0;
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@dOrderID", obj.dOrderID);
            
            result = SqlHelper.ExecuteNonQuery(Misc.connStr, CommandType.StoredProcedure, "spDeleteOrderDetail", param);
            if (result != 0)
            {
                isDelete = true;
            }
            return isDelete;
        }
        catch (Exception ex)
        {
            return isDelete;
        }
    }
}