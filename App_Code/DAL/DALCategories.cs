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
/// Summary description for DALCategories
/// </summary>
public static class DALCategories
{
    #region Function to create new Category
    public static long createCategory(BLLCategories obj)
    {
        long result = 0;
        try
        {
            SqlParameter[] param = new SqlParameter[6];
            param[0] = new SqlParameter("@categoryDescription", obj.categoryDescription);
            param[1] = new SqlParameter("@categoryName", obj.categoryName);
            param[2] = new SqlParameter("@categoryParentID", obj.categoryParentID);
            param[3] = new SqlParameter("@status", obj.status);            
            param[4] = new SqlParameter("@creationDate", obj.creationDate);
            param[5] = new SqlParameter("@createdBy", obj.createdBy);            
            result = Convert.ToInt64(SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spCreateCategory", param).Tables[0].Rows[0][0]);
        }
        catch (Exception ex)
        {
            return 0;
        }
        return result;
    }
    #endregion

    #region function to delete Category
    public static int deleteCategory(BLLCategories obj)
    {
        int result = 0;
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@categoryId", obj.categoryId);
            result = SqlHelper.ExecuteNonQuery(Misc.connStr, CommandType.StoredProcedure, "spDeleteCategory", param);
        }
        catch (Exception ex)
        {
            return 0;
        }
        return result;
    }
    #endregion

    #region Function to Update Category
    public static int updateCategory(BLLCategories obj)
    {
        int result = 0;
        try
        {
            SqlParameter[] param = new SqlParameter[7];
            param[0] = new SqlParameter("@categoryDescription", obj.categoryDescription);
            param[1] = new SqlParameter("@categoryName", obj.categoryName);
            param[2] = new SqlParameter("@categoryParentID", obj.categoryParentID);
            param[3] = new SqlParameter("@status", obj.status);            
            param[4] = new SqlParameter("@modifiedDate", obj.modifiedDate);
            param[5] = new SqlParameter("@modifiedBy", obj.modifiedBy);
            param[6] = new SqlParameter("@categoryId", obj.categoryId);            
            result = SqlHelper.ExecuteNonQuery(Misc.connStr, CommandType.StoredProcedure, "spUpdateCategory", param);
        }
        catch (Exception ex)
        {
            return 0;
        }
        return result;
    }

    public static bool changeCategoryStatus(BLLCategories obj)
    {
        int result = 0;
        try
        {
            SqlParameter[] param = new SqlParameter[2];          
            param[0] = new SqlParameter("@status", obj.status);                      
            param[1] = new SqlParameter("@categoryId", obj.categoryId);
            result = SqlHelper.ExecuteNonQuery(Misc.connStr, CommandType.StoredProcedure, "spChangeCategoryStatus", param);
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

    


    #endregion

    #region Functions to get Category by ID
    public static DataTable getCategoryByID(BLLCategories obj)
    {
        DataTable dt = null;
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@categoryId", obj.categoryId);
            dt = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetCategoryByID", param).Tables[0];
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

    public static DataTable getSubCategoryByParentID(BLLCategories obj)
    {
        DataTable dt = null;
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@categoryParentID", obj.categoryParentID);
            dt = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetSubCategoryByParentID", param).Tables[0];
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

    public static DataTable getAllCategories()
    {
        DataTable dt = null;
        try
        {
            dt = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetAllCategories").Tables[0];
        }
        catch (Exception ex)
        {

        }
        return dt;
    }

    public static DataTable getAllActiveCategories()
    {
        DataTable dt = null;
        try
        {
            dt = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetAllActiveCategories").Tables[0];
        }
        catch (Exception ex)
        {

        }
        return dt;
    }

    public static int GetNewDealsCount()
    {
        DataTable dt = null;
        int total = 0;
        try
        {
            dt = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "sp_GetNewDealsCount").Tables[0];
            if (dt != null && dt.Rows.Count > 0)
            {

               total = Convert.ToInt32(dt.Rows[0]["Total"].ToString().Trim());
            }
        }
        catch (Exception ex)
        {
            return total = 0;
        }
        return total;
    }


    public static DataTable getAllActiveCategoriesAndSubCategories()
    {
        DataTable dt = null;
        try
        {
            dt = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetAllActiveCategoriesAndSubCategories").Tables[0];
        }
        catch (Exception ex)
        {

        }
        return dt;
    }

    #endregion
}
