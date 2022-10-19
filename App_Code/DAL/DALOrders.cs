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
/// Summary description for DALOrders
/// </summary>
public static class DALOrders
{
    #region Function to create
    public static long createOrders(BLLOrders obj)
    {
        long result = 0;
        try
        {
            SqlParameter[] param = new SqlParameter[31];
            param[0] = new SqlParameter("@orderNumber", obj.orderNumber);
            param[1] = new SqlParameter("@orderStatus", obj.orderStatus);
            param[2] = new SqlParameter("@orderDate", obj.orderDate);
            param[3] = new SqlParameter("@totalAmount", obj.totalAmount);
            param[4] = new SqlParameter("@subTotalAmount", obj.subTotalAmount);
            param[5] = new SqlParameter("@deliveryMethod", obj.deliveryMethod);
            param[6] = new SqlParameter("@deliveryAmount", obj.deliveryAmount);
            param[7] = new SqlParameter("@taxAmount", obj.taxAmount);
            param[8] = new SqlParameter("@currencyCode", obj.currencyCode);
            param[9] = new SqlParameter("@userIP", obj.userIP);
            param[10] = new SqlParameter("@providerId", obj.providerId);
            param[11] = new SqlParameter("@creationDate", obj.creationDate);
            param[12] = new SqlParameter("@createdBy", obj.createdBy);
            param[13] = new SqlParameter("@shipToAddress", obj.shipToAddress);
            param[14] = new SqlParameter("@billToAddress", obj.billToAddress);
            param[15] = new SqlParameter("@useCreditCardAmount", obj.useCreditCardAmount);
            param[16] = new SqlParameter("@addComment", obj.addComment);
            param[17] = new SqlParameter("@toMemberRewards", obj.toMemberRewards);
            param[18] = new SqlParameter("@commission", obj.commission);
            param[19] = new SqlParameter("@tips", obj.tips);
            param[20] = new SqlParameter("@specialRequest", obj.specialRequest);
            param[21] = new SqlParameter("@orderConfirmedOn", obj.orderConfirmedOn);
            param[22] = new SqlParameter("@redeem", obj.redeem);
            param[23] = new SqlParameter("@shippingRule", obj.shippingRule);
            param[24] = new SqlParameter("@orderType", obj.orderType);
            param[25] = new SqlParameter("@enabledReferral", obj.enabledReferral);
            param[26] = new SqlParameter("@shippingDistance", obj.shippingDistance);
            param[27] = new SqlParameter("@isPrinted", obj.isPrinted);
            param[28] = new SqlParameter("@shipPhone", obj.shipPhone);
            param[29] = new SqlParameter("@billPhone", obj.billPhone);
            param[30] = new SqlParameter("@belowLimtAmount", obj.belowLimtAmount);
            
            result = Convert.ToInt64(SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spCreateOrders", param).Tables[0].Rows[0][0]);
        }
        catch (Exception ex)
        {
            return 0;
        }
        return result;
    }
    #endregion

    #region function to delete
    public static int deleteOrders(BLLOrders obj)
    {
        int result = 0;
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@orderId", obj.orderId);
            result = SqlHelper.ExecuteNonQuery(Misc.connStr, CommandType.StoredProcedure, "spDeleteOrders", param);
        }
        catch (Exception ex)
        {
            return 0;
        }
        return result;
    }
    #endregion

    #region Function to Update
    public static int updateOrders(BLLOrders obj)
    {
        int result = 0;
        try
        {
            SqlParameter[] param = new SqlParameter[29];
            param[0] = new SqlParameter("@isPrinted", obj.isPrinted);
            param[1] = new SqlParameter("@orderStatus", obj.orderStatus);
            param[2] = new SqlParameter("@orderDate", obj.orderDate);
            param[3] = new SqlParameter("@totalAmount", obj.totalAmount);
            param[4] = new SqlParameter("@subTotalAmount", obj.subTotalAmount);
            param[5] = new SqlParameter("@deliveryMethod", obj.deliveryMethod);
            param[6] = new SqlParameter("@deliveryAmount", obj.deliveryAmount);
            param[7] = new SqlParameter("@taxAmount", obj.taxAmount);
            param[8] = new SqlParameter("@currencyCode", obj.currencyCode);
            param[9] = new SqlParameter("@userIP", obj.userIP);
            param[10] = new SqlParameter("@providerId", obj.providerId);
            param[11] = new SqlParameter("@modifiedDate", obj.modifiedDate);
            param[12] = new SqlParameter("@modifiedBy", obj.modifiedBy);
            param[13] = new SqlParameter("@shipToAddress", obj.shipToAddress);
            param[14] = new SqlParameter("@billToAddress", obj.billToAddress);
            param[15] = new SqlParameter("@useCreditCardAmount", obj.useCreditCardAmount);
            param[16] = new SqlParameter("@addComment", obj.addComment);
            param[17] = new SqlParameter("@toMemberRewards", obj.toMemberRewards);
            param[18] = new SqlParameter("@commission", obj.commission);
            param[19] = new SqlParameter("@tips", obj.tips);
            param[20] = new SqlParameter("@specialRequest", obj.specialRequest);
            param[21] = new SqlParameter("@orderConfirmedOn", obj.orderConfirmedOn);
            param[22] = new SqlParameter("@redeem", obj.redeem);
            param[23] = new SqlParameter("@shippingRule", obj.shippingRule);
            param[24] = new SqlParameter("@orderType", obj.orderType);
            param[25] = new SqlParameter("@enabledReferral", obj.enabledReferral);
            param[26] = new SqlParameter("@shippingDistance", obj.shippingDistance);
            param[27] = new SqlParameter("@orderId", obj.orderId);
            param[28] = new SqlParameter("@orderNumber", obj.orderNumber);
            result = SqlHelper.ExecuteNonQuery(Misc.connStr, CommandType.StoredProcedure, "spUpdateOrders", param);
        }
        catch (Exception ex)
        {
            return 0;
        }
        return result;
    }
    #endregion

    #region Function to Update Order Status By Order ID
    public static int updateOrderStatusByOrderID(BLLOrders obj)
    {
        int result = 0;
        try
        {
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@orderId", obj.orderId);
            param[1] = new SqlParameter("@orderStatus", obj.orderStatus);

            result = SqlHelper.ExecuteNonQuery(Misc.connStr, CommandType.StoredProcedure, "spUpdateOrderStatusByOrderID", param);
        }
        catch (Exception ex)
        {
            return 0;
        }
        return result;
    }

    public static int updateOrderStatusAndOnlinePTNumberByOrderID(BLLOrders obj)
    {
        int result = 0;
        try
        {
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@orderId", obj.orderId);
            param[1] = new SqlParameter("@orderStatus", obj.orderStatus);
            param[2] = new SqlParameter("@PTNumber", obj.PTNumber);

            result = SqlHelper.ExecuteNonQuery(Misc.connStr, CommandType.StoredProcedure, "spUpdateOrderStatusAndOnlinePTNumberByOrderID", param);
        }
        catch (Exception ex)
        {
            return 0;
        }
        return result;
    }

    #endregion

    #region Functions to get All Orders
    public static DataTable getAllOrders()
    {
        DataTable dt = null;
        try
        {
            dt = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetAllOrders").Tables[0];
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

    #region Functions to get all Order By Provider ID

    public static DataTable getAllOrdersByProviderID(BLLOrders obj)
    {
        DataTable dt = null;
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@providerId", obj.providerId);
            dt = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetAllOrdersByProviderID",param).Tables[0];
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

    #region Functions to get all Order By Provider ID and Creation Date

    public static DataTable getAllOrdersByProviderIDCreationDate(BLLOrders obj, DateTime dtStartDate, DateTime dtEndDate)
    {
        DataTable dt = null;
        try
        {
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@providerId", obj.providerId);
            param[1] = new SqlParameter("@CreationStartDate", dtStartDate);
            param[2] = new SqlParameter("@CreationEndDate", dtEndDate);
            dt = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetAllOrdersByProviderIDCreationDate", param).Tables[0];
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

    #region Functions to get all Order By Provider ID and Creation Date

    public static DataTable getAllOrdersByCreatedByCreationDate(BLLOrders obj, DateTime dtStartDate, DateTime dtEndDate)
    {
        DataTable dt = null;
        try
        {
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@createdBy", obj.createdBy);
            param[1] = new SqlParameter("@CreationStartDate", dtStartDate);
            param[2] = new SqlParameter("@CreationEndDate", dtEndDate);
            dt = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetAllOrdersByCreatedByCreationDate", param).Tables[0];
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

    #region Functions to get all Order By Creation Date

    public static DataTable getAllOrdersByCreationDate(DateTime dtStartDate, DateTime dtEndDate)
    {
        DataTable dt = null;
        try
        {
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@CreationStartDate", dtStartDate);
            param[1] = new SqlParameter("@CreationEndDate", dtEndDate);
            dt = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetAllOrdersByCreationDate", param).Tables[0];
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

    #region Functions to get all Order By User ID
    public static DataTable getAllOrdersByUserID(BLLOrders obj)
    {
        DataTable dt = null;
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@createdBy", obj.createdBy);
            dt = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetAllOrdersByUserID",param).Tables[0];
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

    #region Functions to get all Order By User ID
    public static DataTable getAllOrdersByProviderIdAndUserID(BLLOrders obj)
    {
        DataTable dt = null;
        try
        {
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@createdBy", obj.createdBy);
            param[1] = new SqlParameter("@providerId", obj.providerId);
            dt = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetAllOrdersByProviderIdAndUserID", param).Tables[0];
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

    public static DataTable getOrderDetilByOrderID(BLLOrders obj)
    {
        DataTable dt = null;
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@orderId", obj.orderId);
            dt = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetOrderDetilByOrderID", param).Tables[0];
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

    public static DataTable getOrderDetilByOrderNumber(BLLOrders obj)
    {
        DataTable dt = null;
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@orderNumber", obj.orderNumber);
            dt = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetOrderDetilByOrderNumber", param).Tables[0];
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

    public static bool getTotalAmountOfGivenMonthByUserID(BLLOrders obj)
    {
        DataTable dt = null;
        try
        {
            DateTime start = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            DateTime end = start.AddMonths(1).AddDays(-1);
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@createdBy", obj.createdBy);
            param[1] = new SqlParameter("@startDate", start.ToShortDateString());
            param[2] = new SqlParameter("@endDate", end.ToShortDateString());

            dt = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetTotalAmountOfGivenMonthByUserID", param).Tables[0];
            if (dt != null && dt.Rows.Count > 0)
            {
                if (dt.Rows[0][0].ToString()!="" && Convert.ToDouble(dt.Rows[0][0].ToString()) >= 20)
                {
                    return true;
                }
            }
            return false;
        }
        catch (Exception ex)
        {
            return false;
        }        
    }

    public static DataTable getTotalAndSubTotalOfCredidCardAndPartialOrdersByProviderID(BLLOrders obj)
    {
        DataTable dt = null;
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@providerId", obj.providerId);
            dt = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetTotalAndSubTotalOfCredidCardAndPartialOrdersByProviderID", param).Tables[0];

            return dt;
        }
        catch (Exception ex)
        {
            return null;
        }        
    }

    public static DataTable getTotalAndSubTotalOfCashOrdersByProviderID(BLLOrders obj)
    {
        DataTable dt = null;
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@providerId", obj.providerId);
            dt = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetTotalAndSubTotalOfCashOrdersByProviderID", param).Tables[0];
            return dt;
        }
        catch (Exception ex)
        {
            return null;
        }

    }

    public static DataTable getTotalAndSubTotalOfOrdersByProviderID(BLLOrders obj)
    {
        DataTable dt = null;
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@providerId", obj.providerId);
            dt = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetTotalAndSubTotalOfOrdersByProviderID", param).Tables[0];
            return dt;
        }
        catch (Exception ex)
        {
            return null;
        }

    }

    public static DataTable getTotalAndSubTotalOfOrdersByProviderIDAndGivenMonth(BLLOrders obj)
    {
        DataTable dt = null;
        try
        {
            DateTime start = new DateTime(obj.year, obj.month, 1);
            DateTime end = start.AddMonths(1).AddDays(-1);
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@providerId", obj.providerId);
            param[1] = new SqlParameter("@startDate", start.ToShortDateString());
            param[2] = new SqlParameter("@endDate", end.ToShortDateString());

            dt = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetTotalAndSubTotalOfOfOrdersByProviderIDAndGivenMonth", param).Tables[0];

            return dt;
        }
        catch (Exception ex)
        {
            return dt;
        }
    }

    public static DataTable getTotalAndSubTotalOfCredidCardAndPartialOrdersByProviderIDAndGivenMonth(BLLOrders obj)
    {
        DataTable dt = null;
        try
        {
            DateTime start = new DateTime(obj.year, obj.month, 1);
            DateTime end = start.AddMonths(1).AddDays(-1);
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@providerId", obj.providerId);
            param[1] = new SqlParameter("@startDate", start.ToShortDateString());
            param[2] = new SqlParameter("@endDate", end.ToShortDateString());
            dt = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetTotalAndSubTotalOfCredidCardAndPartialOrdersByProviderIDAndGivenMonth", param).Tables[0];

            return dt;
        }
        catch (Exception ex)
        {
            return null;
        }
    }


    public static DataTable getTotalAndSubTotalOfCredidCardAndPartialOrdersByProviderIDFromStartToGivenMonth(BLLOrders obj)
    {
        DataTable dt = null;
        try
        {
            DateTime start = new DateTime(obj.year, obj.month, 1);
            DateTime end = start.AddMonths(1).AddDays(-1);
            start = new DateTime(2009, 1, 1);
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@providerId", obj.providerId);
            param[1] = new SqlParameter("@startDate", start.ToShortDateString());
            param[2] = new SqlParameter("@endDate", end.ToShortDateString());
            dt = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetTotalAndSubTotalOfCredidCardAndPartialOrdersByProviderIDAndGivenMonth", param).Tables[0];

            return dt;
        }
        catch (Exception ex)
        {
            return null;
        }
    }

    public static DataTable getTotalAndSubTotalOfCashOrdersByProviderIDFromStartToGivenMonth(BLLOrders obj)
    {
        DataTable dt = null;
        try
        {
            DateTime start = new DateTime(obj.year, obj.month, 1);
            DateTime end = start.AddMonths(1).AddDays(-1);
            start = new DateTime(2009, 1, 1);
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@providerId", obj.providerId);
            param[1] = new SqlParameter("@startDate", start.ToShortDateString());
            param[2] = new SqlParameter("@endDate", end.ToShortDateString());
            dt = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetTotalAndSubTotalOfCashOrdersByProviderIDAndGivenMonth", param).Tables[0];

            return dt;
        }
        catch (Exception ex)
        {
            return null;
        }
    }


    public static DataTable getTotalAndSubTotalOfCashOrdersByProviderIDAndGivenMonth(BLLOrders obj)
    {
        DataTable dt = null;
        try
        {
            DateTime start = new DateTime(obj.year, obj.month, 1);
            DateTime end = start.AddMonths(1).AddDays(-1);
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@providerId", obj.providerId);
            param[1] = new SqlParameter("@startDate", start.ToShortDateString());
            param[2] = new SqlParameter("@endDate", end.ToShortDateString());
            dt = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetTotalAndSubTotalOfCashOrdersByProviderIDAndGivenMonth", param).Tables[0];

            return dt;
        }
        catch (Exception ex)
        {
            return null;
        }
    }



    public static DataTable getTotalAndSubTotalOfQualifiedOrderByUserID(BLLOrders obj)
    {
        DataTable dt = null;
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@createdBy", obj.createdBy);
            dt = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetTotalAndSubTotalOfQualifiedOrderByUserID", param).Tables[0];
            return dt;
        }
        catch (Exception ex)
        {
            return null;
        }

    }

    public static DataTable getTotalAndSubTotalOfNonQualifiedOrderByUserID(BLLOrders obj)
    {
        DataTable dt = null;
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@createdBy", obj.createdBy);
            dt = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetTotalAndSubTotalOfNonQualifiedOrderByUserID", param).Tables[0];
            return dt;
        }
        catch (Exception ex)
        {
            return null;
        }

    }

    public static DataTable getTotalAndSubTotalOfNonQualifiedOrderByUserIDAndGivenMonth(BLLOrders obj)
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

            dt = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetTotalAndSubTotalOfNonQualifiedOrderByUserIDAndGivenMonth", param).Tables[0];

            return dt;
        }
        catch (Exception ex)
        {
            return dt;
        }
    }

    public static DataTable getTotalAndSubTotalOfQualifiedOrderByUserIDAndGivenMonth(BLLOrders obj)
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

            dt = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetTotalAndSubTotalOfQualifiedOrderByUserIDAndGivenMonth", param).Tables[0];

            return dt;
        }
        catch (Exception ex)
        {
            return dt;
        }
    }

    public static DataTable getLifeTimeSalesOfResturantByProviderID(BLLOrders obj)
    {
        DataTable dt = null;
        try
        {            
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@providerId", obj.providerId);
            dt = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetLifeTimeSalesOfResturantByProviderID", param).Tables[0];

            return dt;
        }
        catch (Exception ex)
        {
            return dt;
        }
    }


    public static DataTable getTotalAndSubTotalOfQualifiedOrderByProviderID(BLLOrders obj)
    {
        DataTable dt = null;
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@providerId", obj.providerId);
            dt = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetTotalAndSubTotalOfQualifiedOrderByProviderID", param).Tables[0];
            return dt;
        }
        catch (Exception ex)
        {
            return null;
        }

    }

    public static DataTable getTotalAndSubTotalOfNonQualifiedOrderByProviderID(BLLOrders obj)
    {
        DataTable dt = null;
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@providerId", obj.providerId);
            dt = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetTotalAndSubTotalOfNonQualifiedOrderByProviderID", param).Tables[0];
            return dt;
        }
        catch (Exception ex)
        {
            return null;
        }

    }

    public static DataTable getTotalAndSubTotalOfNonQualifiedOrderByProviderIDAndGivenMonth(BLLOrders obj)
    {
        DataTable dt = null;
        try
        {
            DateTime start = new DateTime(obj.year, obj.month, 1);
            DateTime end = start.AddMonths(1).AddDays(-1);
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@providerId", obj.providerId);
            param[1] = new SqlParameter("@startDate", start.ToShortDateString());
            param[2] = new SqlParameter("@endDate", end.ToShortDateString());

            dt = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetTotalAndSubTotalOfNonQualifiedOrderByProviderIDAndGivenMonth", param).Tables[0];

            return dt;
        }
        catch (Exception ex)
        {
            return dt;
        }
    }

    public static DataTable getTotalAndSubTotalOfQualifiedOrderByProviderIDAndGivenMonth(BLLOrders obj)
    {
        DataTable dt = null;
        try
        {
            DateTime start = new DateTime(obj.year, obj.month, 1);
            DateTime end = start.AddMonths(1).AddDays(-1);
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@providerId", obj.providerId);
            param[1] = new SqlParameter("@startDate", start.ToShortDateString());
            param[2] = new SqlParameter("@endDate", end.ToShortDateString());

            dt = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetTotalAndSubTotalOfQualifiedOrderByProviderIDAndGivenMonth", param).Tables[0];

            return dt;
        }
        catch (Exception ex)
        {
            return dt;
        }
    }

    
    
    
}
