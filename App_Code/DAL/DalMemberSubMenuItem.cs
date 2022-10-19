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
using System.Data.SqlClient;
using SQLHelper;

/// <summary>
/// Summary description for DalMemberSubMenuItem
/// </summary>
public static class DalMemberSubMenuItem
{
    #region Function to create
    public static int createSubItems(BLLMemberSubMenuItem obj)
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
            result = SqlHelper.ExecuteNonQuery(Misc.connStr, CommandType.StoredProcedure, "spCreateMemberSubItems", param);
        }
        catch (Exception ex)
        {
            return 0;
        }
        return result;
    }
    #endregion

    #region function to delete
    public static int deleteSubItems(BLLMemberSubMenuItem obj)
    {
        int result = 0;
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@menuSubItemId", obj.menuSubItemId);
            result = SqlHelper.ExecuteNonQuery(Misc.connStr, CommandType.StoredProcedure, "spDeleteMemberSubItems", param);
        }
        catch (Exception ex)
        {
            return 0;
        }
        return result;
    }
    #endregion

    #region Function to Update
    public static int updateSubItems(BLLMemberSubMenuItem obj)
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
            result = SqlHelper.ExecuteNonQuery(Misc.connStr, CommandType.StoredProcedure, "spUpdateMemberSubItems", param);
        }
        catch (Exception ex)
        {
            return 0;
        }
        return result;
    }
    #endregion

    #region Functions to get all
    public static DataTable getSubItemsByMenuID(BLLMemberSubMenuItem obj)
    {
        DataTable dt = null;
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@menuItemId", obj.menuItemId);
            dt = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetMemberSubItemsByMenuID", param).Tables[0];
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
    public static DataTable getSubItemsBySubMenuID(BLLMemberSubMenuItem obj)
    {
        DataTable dt = null;
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@menuSubItemId", obj.menuSubItemId);
            dt = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetMemberMenuSubItemsByID", param).Tables[0];
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

    public static DataSet getMemberSubItemsByMenuItemIDForExport(BLLMemberSubMenuItem obj)
    {
        DataSet dst = null;
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@menuItemId", obj.menuItemId);
            dst = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetMemberSubItemsByMenuItemIDForExport", param);
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
