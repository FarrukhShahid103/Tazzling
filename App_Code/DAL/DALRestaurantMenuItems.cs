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
/// Summary description for DALRestaurantMenuItems
/// </summary>
public static class DALRestaurantMenuItems
{

    #region Function to create
    public static int createRestaurantMenuItems(BLLRestaurantMenuItems obj)
    {
        int result = 0;
        try
        {
            SqlParameter[] param = new SqlParameter[7];
            param[0] = new SqlParameter("@foodTypeId", obj.foodTypeId);
            param[1] = new SqlParameter("@itemName", obj.itemName);
            param[2] = new SqlParameter("@itemSubname", obj.itemSubname);
            param[3] = new SqlParameter("@itemDescription", obj.itemDescription);
            param[4] = new SqlParameter("@itemPrice", obj.itemPrice);
            param[5] = new SqlParameter("@creationDate", obj.creationDate);
            param[6] = new SqlParameter("@createdBy", obj.createdBy);
            result = Convert.ToInt32(SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spCreateRestaurantMenuItems", param).Tables[0].Rows[0][0]);
        }
        catch (Exception ex)
        {
            return 0;
        }
        return result;
    }
    #endregion

    #region function to delete
    public static int deleteRestaurantMenuItems(BLLRestaurantMenuItems obj)
    {
        int result = 0;
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@menuItemId", obj.menuItemId);
            result = SqlHelper.ExecuteNonQuery(Misc.connStr, CommandType.StoredProcedure, "spDeleteRestaurantMenuItems", param);
        }
        catch (Exception ex)
        {
            return 0;
        }
        return result;
    }
    #endregion

    #region Function to Update
    public static int updateRestaurantMenuItems(BLLRestaurantMenuItems obj)
    {
        int result = 0;
        try
        {
            SqlParameter[] param = new SqlParameter[7];
            param[0] = new SqlParameter("@menuItemId", obj.menuItemId);
            param[1] = new SqlParameter("@itemName", obj.itemName);
            param[2] = new SqlParameter("@itemSubname", obj.itemSubname);
            param[3] = new SqlParameter("@itemDescription", obj.itemDescription);
            param[4] = new SqlParameter("@itemPrice", obj.itemPrice);
            param[5] = new SqlParameter("@modifiedDate", obj.modifiedDate);
            param[6] = new SqlParameter("@modifiedBy", obj.modifiedBy);
            result = SqlHelper.ExecuteNonQuery(Misc.connStr, CommandType.StoredProcedure, "spUpdateRestaurantMenuItems", param);
        }
        catch (Exception ex)
        {
            return 0;
        }
        return result;
    }
    #endregion

    #region Functions to get all
    public static DataTable getRestaurantMenuItemsByFoodTypeID(BLLRestaurantMenuItems obj)
    {
        DataTable dt = null;
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@foodTypeId", obj.foodTypeId);
            dt = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetRestaurantMenuItemsByFoodTypeID", param).Tables[0];
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

    #region Functions to get all
    public static DataSet getRestaurantMenuItemsByFoodTypeIDForExport(BLLRestaurantMenuItems obj)
    {
        DataSet dst = null;
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@foodTypeId", obj.foodTypeId);
            dst = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetRestaurantMenuItemsByFoodTypeIDForExport", param);
            if (dst != null && dst.Tables[0].Rows.Count > 0)
            {
                return dst;
            }
        }
        catch (Exception ex)
        {
            return null;
        }
        return dst;
    }
    #endregion
}
