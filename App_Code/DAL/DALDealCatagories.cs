using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using SQLHelper;
using System.Data.SqlClient;

/// <summary>
/// Summary description for DALDealCatagories
/// </summary>
public class DALDealCatagories
{

    public static DataTable GetAllDealCategories()
    {
        DataTable dt = null;
        try
        {
            dt = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "sp_GetAllDealCategories").Tables[0];
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



    public static DataTable GetUserFavoriteDealCategoriesByUserID(BLLDealCatagories obj)
    {
        DataTable dt = null;
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@UserID", obj.Userid);
            dt = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "SP_GetUserFavoriteDealCategoriesByUserID",param).Tables[0];
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


    public static int DeleteUserFavoriteDeal(BLLDealCatagories obj)
    {
        int result = 0;
        try
        {
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@UserID", obj.Userid);
            param[1] = new SqlParameter("@DealSubCategoryID", obj.DealSubCategoryid);
            result = SqlHelper.ExecuteNonQuery(Misc.connStr, CommandType.StoredProcedure, "SP_DeleteUserFavoriteDeal", param);
        }
        catch (Exception ex)
        {
            return 0;
        }
        return result;
    }



    public static int AddUserFavoriteDeal(BLLDealCatagories obj)
    {
        int result = 0;
        try
        {
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@UserID", obj.Userid);
            param[1] = new SqlParameter("@DealSubCategoryID", obj.DealSubCategoryid);
            result = SqlHelper.ExecuteNonQuery(Misc.connStr, CommandType.StoredProcedure, "SP_AddUserFavoriteDeal", param);
        }
        catch (Exception ex)
        {
            return 0;
        }
        return result;
    }

    public static DataTable GetAllDealCategoriesbyDealCategoryID(BLLDealCatagories obj)
    {
        DataTable dt = null;
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@DealCategoryID", obj.DealCategoryid);
            dt = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "sp_GetAllDealCategoriesbyDealCategoryID", param).Tables[0];
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