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
/// Summary description for DALOrderItem
/// </summary>
public static class DALOrderItem
{
    #region Function to create
    public static int createOrderItem(BLLOrderItem obj)
    {
        int result = 0;
        try
        {
            SqlParameter[] param = new SqlParameter[7];
            param[0] = new SqlParameter("@orderId", obj.orderId);
            param[1] = new SqlParameter("@quantity", obj.quantity);
            param[2] = new SqlParameter("@unitPrice", obj.unitPrice);
            param[3] = new SqlParameter("@creationDate", obj.creationDate);
            param[4] = new SqlParameter("@createdBy", obj.createdBy);
            param[5] = new SqlParameter("@itemName", obj.itemName);
            param[6] = new SqlParameter("@instruction", obj.instruction);
            result = SqlHelper.ExecuteNonQuery(Misc.connStr, CommandType.StoredProcedure, "spCreateOrderItem", param);
        }
        catch (Exception ex)
        {
            return 0;
        }
        return result;
    }
    #endregion

    #region function to delete
    public static int deleteOrderItem(BLLOrderItem obj)
    {
        int result = 0;
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@orderItemId", obj.orderItemId);
            result = SqlHelper.ExecuteNonQuery(Misc.connStr, CommandType.StoredProcedure, "spDeleteOrderItem", param);
        }
        catch (Exception ex)
        {
            return 0;
        }
        return result;
    }
    #endregion

    #region Functions to get all
    public static DataTable getAllOrderItemByOrderID(BLLOrderItem obj)
    {
        DataTable dt = null;
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@orderId", obj.orderId);
            dt = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetAllOrderItemByOrderID", param).Tables[0];
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
