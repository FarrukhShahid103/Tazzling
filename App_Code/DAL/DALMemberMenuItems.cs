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
/// Summary description for DALMemberMenuItems
/// </summary>
public static class DALMemberMenuItems
{
    #region Function to create Member Menu Items
    public static int createMemberMenuItems(BLLMemberMenuItems obj)
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
            result = Convert.ToInt32(SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spCreateMemberMenuItems", param).Tables[0].Rows[0][0].ToString());
        }
        catch (Exception ex)
        {
            return 0;
        }
        return result;
    }
    #endregion

    #region function to delete Member Menu Items
    public static int deleteMemberMenuItems(BLLMemberMenuItems obj)
    {
        int result = 0;
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@menuItemId", obj.menuItemId);
            result = SqlHelper.ExecuteNonQuery(Misc.connStr, CommandType.StoredProcedure, "spDeleteMemberMenuItems", param);
        }
        catch (Exception ex)
        {
            return 0;
        }
        return result;
    }
    #endregion

    #region Function to Update
    public static int updateMemberMenuItems(BLLMemberMenuItems obj)
    {
        int result = 0;
        try
        {
            SqlParameter[] param = new SqlParameter[7];
            param[0] = new SqlParameter("@itemName", obj.itemName);
            param[1] = new SqlParameter("@itemSubname", obj.itemSubname);
            param[2] = new SqlParameter("@itemDescription", obj.itemDescription);
            param[3] = new SqlParameter("@itemPrice", obj.itemPrice);
            param[4] = new SqlParameter("@modifiedDate", obj.modifiedDate);
            param[5] = new SqlParameter("@modifiedBy", obj.modifiedBy);
            param[6] = new SqlParameter("@menuItemId", obj.menuItemId);            
            result = SqlHelper.ExecuteNonQuery(Misc.connStr, CommandType.StoredProcedure, "spUpdateMemberMenuItems", param);
        }
        catch (Exception ex)
        {
            return 0;
        }
        return result;
    }
    #endregion

    #region Functions to get all
    public static DataTable getMemberMenuItemsByFoodTypeID(BLLMemberMenuItems obj)
    {
        DataTable dt = null;
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@foodTypeId", obj.foodTypeId);
            dt = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetMemberMenuItemsByFoodTypeID", param).Tables[0];
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

    public static DataTable getMemberMenuItemsByFoodTypeIDAndMenuName(BLLMemberMenuItems obj)
    {
        DataTable dt = null;
        try
        {
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@foodTypeId", obj.foodTypeId);
            param[1] = new SqlParameter("@itemName", obj.itemName);
            dt = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetMemberMenuItemsByFoodTypeIDAndMenuName", param).Tables[0];
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

    public static DataTable spGetMemberMenuItemsUniqueNamesByFoodTypeID(BLLMemberMenuItems obj)
    {
        DataTable dt = null;
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@foodTypeId", obj.foodTypeId);
            dt = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetMemberMenuItemsUniqueNamesByFoodTypeID", param).Tables[0];
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

    public static DataSet getMemberMenuItemsByFoodTypeIDForExport(BLLMemberMenuItems obj)
    {
        DataSet dst = null;
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@foodTypeId", obj.foodTypeId);
            dst = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetMemberMenuItemsByFoodTypeIDForExport", param);
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

    public static DataTable getMemberMenuItemsFeaturedFood()
    {
        DataTable dtFeaturedFood = null;
        try
        {
            dtFeaturedFood = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetMemberMenuItemFeaturedFood").Tables[0];

            if (dtFeaturedFood != null && dtFeaturedFood.Rows.Count > 0)
            {
                return dtFeaturedFood;
            }
        }
        catch (Exception ex)
        {
            return null;
        }
        return dtFeaturedFood;
    }

    #endregion
}