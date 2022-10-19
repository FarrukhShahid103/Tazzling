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
/// Summary description for DALMemberMenu
/// </summary>
public static class DALMemberMenu
{
    #region Function to create Member Menu
    public static long createMemberMenu(BLLMemberMenu obj)
    {
        long result = 0;
        try
        {
            SqlParameter[] param = new SqlParameter[6];
            param[0] = new SqlParameter("@foodType", obj.foodType);
            if (obj.foodTemplateId == 0)
            {
                param[1] = new SqlParameter("@foodTemplateId", DBNull.Value);
            }
            else
            {
                param[1] = new SqlParameter("@foodTemplateId", obj.foodTemplateId);
            }
            param[2] = new SqlParameter("@foodImage", obj.foodImage);
            param[3] = new SqlParameter("@cuisineID", obj.cuisineID);
            param[4] = new SqlParameter("@creationDate", obj.creationDate);
            param[5] = new SqlParameter("@createdBy", obj.createdBy);
            result =Convert.ToInt64(SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spCreateMemberMenu", param).Tables[0].Rows[0][0]);
        }
        catch (Exception ex)
        {
            return 0;
        }
        return result;
    }
    #endregion

    #region function to delete Member Menu
    public static int deleteMemberMenu(BLLMemberMenu obj)
    {
        int result = 0;
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@foodTypeId", obj.foodTypeId);
            result = SqlHelper.ExecuteNonQuery(Misc.connStr, CommandType.StoredProcedure, "spDeleteMemberMenu", param);
        }
        catch (Exception ex)
        {
            return 0;
        }
        return result;
    }
   
    public static int deleteMemberMenuByCuisineIDAndUserID(BLLMemberMenu obj)
    {
        int result = 0;
        try
        {
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@cuisineId", obj.cuisineID);
            param[1] = new SqlParameter("@createdBy", obj.createdBy);
            result = SqlHelper.ExecuteNonQuery(Misc.connStr, CommandType.StoredProcedure, "spDeleteMemberMenuByCuisineIDAndUserID", param);
        }
        catch (Exception ex)
        {
            return 0;
        }
        return result;
    }

    

    #endregion

    #region Function to Update
    public static int updateMemberMenu(BLLMemberMenu obj)
    {
        int result = 0;
        try
        {
            SqlParameter[] param = new SqlParameter[5];
            param[0] = new SqlParameter("@foodTypeID", obj.foodTypeId);
            param[1] = new SqlParameter("@foodType", obj.foodType);
            param[2] = new SqlParameter("@modifiedDate", obj.modifiedDate);
            param[3] = new SqlParameter("@modifiedBy", obj.modifiedBy);
            result = SqlHelper.ExecuteNonQuery(Misc.connStr, CommandType.StoredProcedure, "spUpdateMemberMenu", param);
        }
        catch (Exception ex)
        {
            return 0;
        }
        return result;
    }
    #endregion

    
    #region Function to Update
    public static int updateMemberMenuByCosineIDAndUserID(BLLMemberMenu obj)
    {
        int result = 0;
        try
        {
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@cuisineID", obj.cuisineID);
            param[1] = new SqlParameter("@createdBy", obj.createdBy);            
            result = SqlHelper.ExecuteNonQuery(Misc.connStr, CommandType.StoredProcedure, "spUpdateMemberMenuByCosineIDAndUserID", param);
        }
        catch (Exception ex)
        {
            return 0;
        }
        return result;
    }
    #endregion
    

    #region Functions to get all
 
    public static DataTable getAllMemberMenuByCuisineIDAndUserID(BLLMemberMenu obj)
    {
        DataTable dt = null;
        try
        {
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@cuisineID", obj.cuisineID);
            param[1] = new SqlParameter("@createdBy", obj.createdBy);
            dt = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetAllMemberMenuByCuisineIDAndUserID", param).Tables[0];
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

    public static DataTable getAllMemberMenuByRestaurantID(BLLMemberMenu obj)
    {
        DataTable dt = null;
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@foodTemplateId", obj.foodTemplateId);
            dt = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetAllMemberMenuByRestaurantID", param).Tables[0];
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


    public static DataSet getAllMemberMenuAndItemsByRestaurantID(BLLMemberMenu obj)
    {
        DataSet dt = null;
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@foodTemplateId", obj.foodTemplateId);
            dt = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetAllMemberMenuAndItemsByRestaurantID", param);
            if (dt != null && dt.Tables.Count > 0)
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
    public static DataTable getRestaurantMenuByRestaurantID(BLLMemberMenu obj)
    {
        DataTable dt = null;
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@foodTemplateId", obj.foodTemplateId);
            dt = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetRestaurantMenuByRestaurantID", param).Tables[0];
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
}
