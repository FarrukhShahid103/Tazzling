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
/// Summary description for DALRestaurantDealItems
/// </summary>
public static class DALRestaurantDealItems
{

    #region Function to create
    public static int createRestaurantDealItems(BLLRestaurantDealItems obj)
    {
        int result = 0;
        try
        {
            SqlParameter[] param = new SqlParameter[6];
            param[0] = new SqlParameter("@dealId", obj.dealId);
            param[1] = new SqlParameter("@dealItemName", obj.dealItemName);
            param[2] = new SqlParameter("@dealItemSubname", obj.dealItemSubname);
            param[3] = new SqlParameter("@dealItemDescription", obj.dealItemDescription);            
            param[4] = new SqlParameter("@creationDate", obj.creationDate);
            param[5] = new SqlParameter("@createdBy", obj.createdBy);
            result = SqlHelper.ExecuteNonQuery(Misc.connStr, CommandType.StoredProcedure, "spCreateRestaurantDealItems", param);
        }
        catch (Exception ex)
        {
            return 0;
        }
        return result;
    }
    #endregion

    #region function to delete
    public static int deleteRestaurantDealItems(BLLRestaurantDealItems obj)
    {
        int result = 0;
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@dealItemId", obj.dealItemId);
            result = SqlHelper.ExecuteNonQuery(Misc.connStr, CommandType.StoredProcedure, "spDeleteRestaurantDealItems", param);
        }
        catch (Exception ex)
        {
            return 0;
        }
        return result;
    }
    #endregion

    #region Function to Update
    public static int updateRestaurantDealItems(BLLRestaurantDealItems obj)
    {
        int result = 0;
        try
        {
            SqlParameter[] param = new SqlParameter[6];
            param[0] = new SqlParameter("@dealItemId", obj.dealItemId);
            param[1] = new SqlParameter("@dealItemName", obj.dealItemName);
            param[2] = new SqlParameter("@dealItemSubname", obj.dealItemSubname);
            param[3] = new SqlParameter("@dealItemDescription", obj.dealItemDescription);    
            param[4] = new SqlParameter("@modifiedDate", obj.modifiedDate);
            param[5] = new SqlParameter("@modifiedBy", obj.modifiedBy);
            result = SqlHelper.ExecuteNonQuery(Misc.connStr, CommandType.StoredProcedure, "spUpdateRestaurantDealItems", param);
        }
        catch (Exception ex)
        {
            return 0;
        }
        return result;
    }
    #endregion

    #region Functions to get all
    public static DataTable getRestaurantDealItemsByDealID(BLLRestaurantDealItems obj)
    {
        DataTable dt = null;
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@dealId", obj.dealId);
            dt = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetRestaurantDealItemsByDealID", param).Tables[0];
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
    public static DataSet getRestaurantDealItemsByDealIDForExport(BLLRestaurantDealItems obj)
    {
        DataSet dst = null;
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@dealId", obj.dealId);
            dst = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetRestaurantDealItemsByDealIDForExport", param);
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
