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
/// Summary description for DALGiftCardOrder
/// </summary>
public static class DALGiftCardOrder
{
    public static long createGiftCardOrderByUser(BLLGiftCardOrder obj)
    {
        long result = 0;
        try
        {
            SqlParameter[] param = new SqlParameter[11];

            param[0] = new SqlParameter("@createdBy", obj.createdBy);
            param[1] = new SqlParameter("@comments", obj.comments);
            param[2] = new SqlParameter("@commission", obj.commission);
            param[3] = new SqlParameter("@orderRefNo", obj.orderRefNo);
            param[4] = new SqlParameter("@redeem", obj.redeem);
            param[5] = new SqlParameter("@createdOn", obj.createdOn);
            param[6] = new SqlParameter("@subTotalAmount", obj.subTotalAmount);
            param[7] = new SqlParameter("@creditCard", obj.creditCard);
            param[8] = new SqlParameter("@ccInfoID", obj.ccInfoID);            
            param[9] = new SqlParameter("@orderIdReceived", obj.orderIdReceived);
            param[10] = new SqlParameter("@orderStatus", obj.orderStatus);
            result = Convert.ToInt64(SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spCreateGiftCardOrderByUser", param).Tables[0].Rows[0][0]);
        }
        catch (Exception ex)
        {
            return 0;
        }
        return result;
    }

    public static long createGiftCardOrder(BLLGiftCardOrder obj)
    {
        long result = 0;
        try
        {
            SqlParameter[] param = new SqlParameter[10];

            param[0] = new SqlParameter("@subTotalAmount", obj.subTotalAmount);
            param[1] = new SqlParameter("@comments", obj.comments);
            param[2] = new SqlParameter("@commission", obj.commission);
            param[3] = new SqlParameter("@orderRefNo", obj.orderRefNo);
            param[4] = new SqlParameter("@redeem", obj.redeem);
            param[5] = new SqlParameter("@createdOn", obj.createdOn);
            param[6] = new SqlParameter("@creditCard", obj.creditCard);
            param[7] = new SqlParameter("@ccInfoID", obj.ccInfoID);
            param[8] = new SqlParameter("@orderIdReceived", obj.orderIdReceived);
            param[9] = new SqlParameter("@orderStatus", obj.orderStatus);
            result = Convert.ToInt64(SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spCreateGiftCardOrder", param).Tables[0].Rows[0][0]);
        }
        catch (Exception ex)
        {
            return 0;
        }
        return result;
    }

    public static int updateGiftCardOrderByOrderRefNoSent(BLLGiftCardOrder obj)
    {
        int result = 0;
        try
        {
            SqlParameter[] param = new SqlParameter[7];

            param[0] = new SqlParameter("@orderRefNo", obj.orderRefNo);
            param[1] = new SqlParameter("@orderIdReceived", obj.orderIdReceived);
            param[2] = new SqlParameter("@orderStatus", obj.orderStatus);

            result = SqlHelper.ExecuteNonQuery(Misc.connStr, CommandType.StoredProcedure, "spUpdateGiftCardOrderByOrderRefNoSent", param);
        }
        catch (Exception ex)
        {
            return 0;
        }
        return result;
    }

    public static DataTable getGiftCardInfoByOrderRefNoSent(BLLGiftCardOrder obj)
    {
        DataTable dtGiftCardInfo = null;

        try
        {
            SqlParameter[] param = new SqlParameter[1];

            param[0] = new SqlParameter("@orderRefNo", obj.orderRefNo);

            dtGiftCardInfo = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetGiftCardInfoByOrderRefNoSent", param).Tables[0];

            if (dtGiftCardInfo != null && dtGiftCardInfo.Rows.Count > 0)
            {
                return dtGiftCardInfo;
            }
        }
        catch (Exception ex)
        {
            return null;
        }

        return dtGiftCardInfo;
    }
}
