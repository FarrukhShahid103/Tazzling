using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using SQLHelper;

/// <summary>
/// Summary description for DALFeaturedFoods
/// </summary>
public class DALFeaturedFoods
{

    #region "Function of Add New Featured Food"

    public static int createNewFeaturedFood(BLLFeaturedFoods objBLLFeaturedFoods)
    {
        int iResult = 0;

        try 
        {
            SqlParameter[] param = new SqlParameter[11];
            param[0] = new SqlParameter("@restaurantId", objBLLFeaturedFoods.restaurantID);
            param[1] = new SqlParameter("@foodName", objBLLFeaturedFoods.foodName);
            param[2] = new SqlParameter("@foodDescription", objBLLFeaturedFoods.foodDescription);
            param[3] = new SqlParameter("@foodPrice", objBLLFeaturedFoods.foodPrice);
            param[4] = new SqlParameter("@foodImage", objBLLFeaturedFoods.foodImage);
            param[5] = new SqlParameter("@creationDate", objBLLFeaturedFoods.creationDate);
            param[6] = new SqlParameter("@createdBy", objBLLFeaturedFoods.createdBy);
            param[7] = new SqlParameter("@modifiedDate", objBLLFeaturedFoods.modifiedDate);
            param[8] = new SqlParameter("@modifiedBy", objBLLFeaturedFoods.modifiedBy);
            param[9] = new SqlParameter("@isFeatured", objBLLFeaturedFoods.isFeatured);
            param[10] = new SqlParameter("@foodAddedBy", objBLLFeaturedFoods.foodAddedBy);

            iResult = SqlHelper.ExecuteNonQuery(Misc.connStr, CommandType.StoredProcedure, "SpCreateFeaturedFood", param);
        }
        catch (Exception ex)
        {
            string strException = "There is an error occur, please email us at support@tazzling.com or call 1855-295-1771.";
        }
        return iResult;
    }

    #endregion

    #region "Get Featured Food By Created By ID"

    public static DataTable getFeaturedFoodByCreatedByID(BLLFeaturedFoods objBLLFeaturedFoods)
    {
        DataTable dtFeaturedFood = null;

        try {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@userID", objBLLFeaturedFoods.createdBy);

            dtFeaturedFood = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetFeaturedFoodByCreatedByID", param).Tables[0];
        }
        catch (Exception ex) 
        { }

        return dtFeaturedFood;
    }

    #endregion

    #region "Delete Featured Food on the basis of the Featured Food ID"

    public static int DeleteFeaturedFoodByFoodID(BLLFeaturedFoods objBLLFeaturedFoods)
    {
        int iResult = 0;

        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@featuredFoodId", objBLLFeaturedFoods.featuredFoodID);

            iResult = SqlHelper.ExecuteNonQuery(Misc.connStr, CommandType.StoredProcedure, "spDeleteFeaturedFoodByFeaturedFoodID", param);
        }
        catch (Exception ex)
        {
            string strException = "There is an error occur, please email us at support@tazzling.com or call 1855-295-1771.";
        }
        return iResult;
    }

    #endregion

    #region "Update Featured Food on the basis of the Featured Food ID"

    public static int UpdateFeaturedFoodByFoodID(BLLFeaturedFoods objBLLFeaturedFoods)
    {
        int iResult = 0;

        try
        {
            SqlParameter[] param = new SqlParameter[5];
            param[0] = new SqlParameter("@featuredFoodId", objBLLFeaturedFoods.featuredFoodID);
            param[1] = new SqlParameter("@foodName", objBLLFeaturedFoods.foodName);
            param[2] = new SqlParameter("@foodDescription", objBLLFeaturedFoods.foodDescription);
            param[3] = new SqlParameter("@foodPrice", objBLLFeaturedFoods.foodPrice);
            param[4] = new SqlParameter("@restaurantId", objBLLFeaturedFoods.restaurantID);

            iResult = SqlHelper.ExecuteNonQuery(Misc.connStr, CommandType.StoredProcedure, "spUpdateFeaturedFoodByFeaturedFoodID", param);
        }
        catch (Exception ex)
        {
            string strException = "There is an error occur, please email us at support@tazzling.com or call 1855-295-1771.";
        }
        return iResult;
    }

    #endregion

    #region "Get Featured Food added by the Administrator"

    public static DataTable GetFeaturedFoodAddedByAdmin()
    {
        DataTable dtFoods = null;

        try
        {
            dtFoods = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetFeaturedFoodByAdmin").Tables[0];
        }
        catch (Exception ex)
        {
            string strException = "There is an error occur, please email us at support@tazzling.com or call 1855-295-1771.";
        }
        return dtFoods;
    }

    #endregion

    #region "Get Featured Food added by the Restaurant ID"

    public static DataTable GetFeaturedFoodByRestaurantID(string strResUser)
    {
        DataTable dtFoods = null;

        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@restaurantUser", strResUser);
            dtFoods = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetFeaturedFoodByRestaurantID", param).Tables[0];
        }
        catch (Exception ex)
        {
            string strException = "There is an error occur, please email us at support@tazzling.com or call 1855-295-1771.";
        }
        return dtFoods;
    }

    #endregion
}