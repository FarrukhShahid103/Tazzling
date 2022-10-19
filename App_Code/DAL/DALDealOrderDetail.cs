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
/// Summary description for DALComments
/// </summary>
public static class DALDealOrderDetail
{
    #region Function to create new Comments
    public static long createDealOrderDetail(BLLDealOrderDetail obj)
    {
        long result = 0;
        try
        {
            SqlParameter[] param = new SqlParameter[8];
            param[0] = new SqlParameter("@dealOrderCode", obj.dealOrderCode);
            param[1] = new SqlParameter("@dOrderID", obj.dOrderID);
            param[2] = new SqlParameter("@isRedeemed", obj.isRedeemed);
            param[3] = new SqlParameter("@isGift", obj.isGift);
            param[4] = new SqlParameter("@voucherSecurityCode", obj.voucherSecurityCode);
            param[5] = new SqlParameter("@displayIt", obj.displayIt);
            if (obj.trackingNumber == string.Empty)
            {
                param[6] = new SqlParameter("@trackingNumber", DBNull.Value);
            }
            else
            {
                param[6] = new SqlParameter("@trackingNumber", obj.trackingNumber);
            }
            if (obj.trackingUpdateDate == DateTime.Now)
            {
                param[7] = new SqlParameter("@trackerupdateDate", DBNull.Value);
            }
            else
            {
                param[7] = new SqlParameter("@trackerupdateDate", obj.trackingUpdateDate);
            }

            result = Convert.ToInt64(SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spCreateDealOrderDetail", param).Tables[0].Rows[0][0]);
        }
        catch (Exception ex)
        {
            return 0;
        }
        return result;
    }
    #endregion

    public static bool changeOrderDetailStatus(BLLDealOrderDetail obj)
    {
        int result = 0;
        try
        {
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@detailID", obj.detailID);
            param[1] = new SqlParameter("@markUsed", obj.markUsed);
            result = SqlHelper.ExecuteNonQuery(Misc.connStr, CommandType.StoredProcedure, "spChangeOrderDetailStatus", param);
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

    public static bool updateTrackingNumber(BLLDealOrderDetail obj)
    {
        int result = 0;
        try
        {
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@detailID", obj.detailID);
            param[1] = new SqlParameter("@trackingNumber", obj.trackingNumber);
            result = SqlHelper.ExecuteNonQuery(Misc.connStr, CommandType.StoredProcedure, "spUpdateTrackingNumber", param);
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

    public static bool changeOrderDetailDisplayStatus(BLLDealOrderDetail obj)
    {
        int result = 0;
        try
        {
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@detailID", obj.detailID);
            param[1] = new SqlParameter("@displayIt", obj.displayIt);
            result = SqlHelper.ExecuteNonQuery(Misc.connStr, CommandType.StoredProcedure, "spChangeOrderDetailDisplayStatus", param);
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

    //#region function to delete Comments
    //public static int deleteComments(BLLComments obj)
    //{
    //    int result = 0;
    //    try
    //    {
    //        SqlParameter[] param = new SqlParameter[1];
    //        param[0] = new SqlParameter("@commentId", obj.commentId);
    //        result = SqlHelper.ExecuteNonQuery(Misc.connStr, CommandType.StoredProcedure, "spDeleteComments", param);
    //    }
    //    catch (Exception ex)
    //    {
    //        return 0;
    //    }
    //    return result;
    //}
    //#endregion

    //#region Function to Update Comments
    //public static int updateComments(BLLComments obj)
    //{
    //    int result = 0;
    //    try
    //    {
    //        SqlParameter[] param = new SqlParameter[6];
    //        param[0] = new SqlParameter("@commentId", obj.commentId);
    //        param[1] = new SqlParameter("@title", obj.title);
    //        param[2] = new SqlParameter("@comment", obj.comment);
    //        param[3] = new SqlParameter("@restaurantID", obj.restaurantID);
    //        param[4] = new SqlParameter("@ModifiedDate", obj.modifiedDate);
    //        param[5] = new SqlParameter("@modifiedBy", obj.modifiedBy);            
    //        result = SqlHelper.ExecuteNonQuery(Misc.connStr, CommandType.StoredProcedure, "spUpdateComments", param);
    //    }
    //    catch (Exception ex)
    //    {
    //        return 0;
    //    }
    //    return result;
    //}
    //#endregion

    #region Functions to get comments by RestaurantID
    public static DataTable getAllUserDealOrderDetailByOrderID(BLLDealOrderDetail obj)
    {
        DataTable dt = null;
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@dOrderID", obj.dOrderID);
            dt = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetAllUserDealOrderDetailByOrderID", param).Tables[0];
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

    public static DataTable getAllUsedUserDealOrderDetailByOrderID(BLLDealOrderDetail obj)
    {
        DataTable dt = null;
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@dOrderID", obj.dOrderID);
            dt = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetAllUsedUserDealOrderDetailByOrderID", param).Tables[0];
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

    public static DataTable getAllUsedUserGiftDealOrderDetailByOrderID(BLLDealOrderDetail obj)
    {
        DataTable dt = null;
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@dOrderID", obj.dOrderID);
            dt = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetAllUsedUserGiftDealOrderDetailByOrderID", param).Tables[0];
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

    public static DataTable getAllAvailableUserDealOrderDetailByOrderID(BLLDealOrderDetail obj)
    {
        DataTable dt = null;
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@dOrderID", obj.dOrderID);
            dt = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetAllAvailableUserDealOrderDetailByOrderID", param).Tables[0];
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

    public static DataTable getAllAvailableGiftUserDealOrderDetailByOrderID(BLLDealOrderDetail obj)
    {
        DataTable dt = null;
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@dOrderID", obj.dOrderID);
            dt = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetAllAvailableGiftUserDealOrderDetailByOrderID", param).Tables[0];
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

    public static DataTable getAllDealOrderDetailByDealOrderCode(BLLDealOrderDetail obj)
    {
        DataTable dt = null;
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@dealOrderCode", obj.dealOrderCode);
            dt = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetAllDealOrderDetailByDealOrderCode", param).Tables[0];
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


    public static DataTable getUserReceivedDealsByUserID(BLLDealOrderDetail obj)
    {
        DataTable dt = null;
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@isGiftCapturedId", obj.isGiftCapturedId);
            dt = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetUserReceivedDealsByUserID", param).Tables[0];
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



    public static DataTable getAllUserDealOrderDetailByOrderIDAndUserID(BLLDealOrderDetail obj)
    {
        DataTable dt = null;
        try
        {
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@dOrderID", obj.dOrderID);
            param[1] = new SqlParameter("@isGiftCapturedId", obj.isGiftCapturedId);
            dt = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetAllUserDealOrderDetailByOrderIDAndUserID", param).Tables[0];
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


    public static DataTable getAllGiftUserDealOrderDetailByOrderID(BLLDealOrderDetail obj)
    {
        DataTable dt = null;
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@dOrderID", obj.dOrderID);
            dt = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetAllGiftUserDealOrderDetailByOrderID", param).Tables[0];
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

    public static DataTable getAllDealOrderDetailsByOrderID(BLLDealOrderDetail obj)
    {
        DataTable dt = null;
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@dOrderID", obj.dOrderID);
            dt = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetAllDealOrderDetailsByOrderID", param).Tables[0];
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

    public static DataTable getDealOrderDetailsByOrderID(BLLDealOrderDetail obj)
    {
        DataTable dt = null;
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@dOrderID", obj.dOrderID);
            dt = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetAllDealOrderDetailByOrderID", param).Tables[0];
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

    public static DataTable getVoucherNoteByDetailID(BLLDealOrderDetail obj)
    {
        DataTable dt = null;

        try
        {
            SqlParameter[] param = new SqlParameter[1];

            param[0] = new SqlParameter("@detailID", obj.detailID);
            dt = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetVoucherNoteByDetailID", param).Tables[0];
        }
        catch (Exception ex)
        {

        }

        return dt;
    }

    #endregion

    public static int updateVoucherNoteByDetailID(BLLDealOrderDetail obj)
    {
        int iChk = 0;

        try
        {
            SqlParameter[] param = new SqlParameter[2];

            param[0] = new SqlParameter("@detailID", obj.detailID);
            param[1] = new SqlParameter("@note", obj.note);
            iChk = SqlHelper.ExecuteNonQuery(Misc.connStr, CommandType.StoredProcedure, "spUpdateVoucherNoteByDetailID", param);
        }
        catch (Exception ex)
        {

        }

        return iChk;
    }


    public static int updateDealOrderDetailsByDetailID(BLLDealOrderDetail obj)
    {
        int iChk = 0;

        try
        {
            SqlParameter[] param = new SqlParameter[2];

            param[0] = new SqlParameter("@detailID", obj.detailID);
            param[1] = new SqlParameter("@isRedeemed", obj.isRedeemed);
            iChk = SqlHelper.ExecuteNonQuery(Misc.connStr, CommandType.StoredProcedure, "spUpdateDealOrderDetailByOrderDetailId", param);
        }
        catch (Exception ex)
        {

        }

        return iChk;
    }

    public static int updateDealOrderDetailEmailByID(BLLDealOrderDetail obj)
    {
        int iChk = 0;

        try
        {
            SqlParameter[] param = new SqlParameter[2];

            param[0] = new SqlParameter("@detailID", obj.detailID);
            param[1] = new SqlParameter("@receiverEmail", obj.receiverEmail);

            iChk = SqlHelper.ExecuteNonQuery(Misc.connStr, CommandType.StoredProcedure, "spUpdateDealOrderDetailEmailByOrderDetailId", param);
        }
        catch (Exception ex)
        {

        }

        return iChk;
    }

    public static DataTable updateDealOrderDetailCapIdByDealCode(BLLDealOrderDetail obj)
    {
        DataTable dt = null;

        try
        {
            SqlParameter[] param = new SqlParameter[2];

            param[0] = new SqlParameter("@dealOrderCode", obj.dealOrderCode);
            param[1] = new SqlParameter("@isGiftCapturedId", obj.isGiftCapturedId);

            dt = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spUpdateDealOrderDetailCapIdByDealCode", param).Tables[0];
        }
        catch (Exception ex)
        {

        }

        return dt;
    }
    public static bool changeOrderDetailStatusanddeleteComments(BLLDealOrderDetail obj)
    {
        int result = 0;
        try
        {
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@detailID", obj.detailID);
            param[1] = new SqlParameter("@markUsed", obj.markUsed);
            result = SqlHelper.ExecuteNonQuery(Misc.connStr, CommandType.StoredProcedure, "spChangeOrderDetailStatusandDeleteComments", param);
            if (result != 0)
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
}
