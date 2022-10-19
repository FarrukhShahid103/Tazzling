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
/// Summary description for DALSubMenuItem
/// </summary>
public static class DALSubMenuItem
{	
    #region Function to create
    public static int createSubItems(BLLSubMenuItem obj)
    {
        int result = 0;
        try
        {
            SqlParameter[] param = new SqlParameter[7];
            param[0] = new SqlParameter("@menuItemId", obj.menuItemId);
            param[1] = new SqlParameter("@subItemName", obj.subItemName);
            param[2] = new SqlParameter("@subItemSubname", obj.subItemSubname);
            param[3] = new SqlParameter("@subItemDescription", obj.subItemDescription);
            param[4] = new SqlParameter("@subItemPrice", obj.subItemPrice);
            param[5] = new SqlParameter("@creationDate", obj.creationDate);
            param[6] = new SqlParameter("@createdBy", obj.createdBy);
            result = SqlHelper.ExecuteNonQuery(Misc.connStr, CommandType.StoredProcedure, "spCreateSubItems", param);
        }
        catch (Exception ex)
        {
            return 0;
        }
        return result;
    }
    #endregion

    #region function to delete
    public static int deleteSubItems(BLLSubMenuItem obj)
    {
        int result = 0;
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@menuSubItemId", obj.menuSubItemId);
            result = SqlHelper.ExecuteNonQuery(Misc.connStr, CommandType.StoredProcedure, "spDeleteSubItems", param);
        }
        catch (Exception ex)
        {
            return 0;
        }
        return result;
    }
    #endregion

    #region Function to Update
    public static int updateSubItems(BLLSubMenuItem obj)
    {
        int result = 0;
        try
        {
            SqlParameter[] param = new SqlParameter[7];
            param[0] = new SqlParameter("@menuSubItemId", obj.menuSubItemId);
            param[1] = new SqlParameter("@subItemName", obj.subItemName);
            param[2] = new SqlParameter("@subItemSubname", obj.subItemSubname);
            param[3] = new SqlParameter("@subItemDescription", obj.subItemDescription);
            param[4] = new SqlParameter("@subItemPrice", obj.subItemPrice);
            param[5] = new SqlParameter("@modifiedDate", obj.modifiedDate);
            param[6] = new SqlParameter("@modifiedBy", obj.modifiedBy);
            result = SqlHelper.ExecuteNonQuery(Misc.connStr, CommandType.StoredProcedure, "spUpdateSubItems", param);
        }
        catch (Exception ex)
        {
            return 0;
        }
        return result;
    }
    #endregion

    #region Functions to get all
    public static DataTable getSubItemsByMenuID(BLLSubMenuItem obj)
    {
        DataTable dt = null;
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@menuItemId", obj.menuItemId);
            dt = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetSubItemsByMenuID", param).Tables[0];
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
    public static DataTable getSubItemsBySubMenuID(BLLSubMenuItem obj)
    {
        DataTable dt = null;
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@menuSubItemId", obj.menuSubItemId);
            dt = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetSubItemsByID", param).Tables[0];
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

    public static DataSet getSubItemsByMenuItemIDForExport(BLLSubMenuItem obj)
    {
        DataSet dst = null;
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@menuItemId", obj.menuItemId);
            dst = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetSubItemsByMenuItemIDForExport", param);
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
