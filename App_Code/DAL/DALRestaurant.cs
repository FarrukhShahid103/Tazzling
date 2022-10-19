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
/// Summary description for DALRestaurant
/// </summary>
public static class DALRestaurant
{
    #region Function to create
    public static int createRestaurant(BLLRestaurant objBLLRestaurant)
    {
        int result = 0;
        try
        {
            SqlParameter[] param = new SqlParameter[27];
            param[0] = new SqlParameter("@firstName", objBLLRestaurant.firstName);
            param[1] = new SqlParameter("@lastName", objBLLRestaurant.lastName);
            param[2] = new SqlParameter("@restaurantBusinessName", objBLLRestaurant.restaurantBusinessName);
            param[3] = new SqlParameter("@restaurantAddress", objBLLRestaurant.restaurantAddress);
            param[4] = new SqlParameter("@cityid", objBLLRestaurant.cityId);
            param[5] = new SqlParameter("@provinceId", objBLLRestaurant.provinceId);
            param[6] = new SqlParameter("@phone", objBLLRestaurant.phone);
            param[7] = new SqlParameter("@fax", objBLLRestaurant.fax);
            param[8] = new SqlParameter("@zipCode", objBLLRestaurant.zipCode);
            param[9] = new SqlParameter("@restaurantPicture", objBLLRestaurant.restaurantPicture);
            param[10] = new SqlParameter("@detail", objBLLRestaurant.detail);
            param[11] = new SqlParameter("@createdDate", objBLLRestaurant.createdDate);
            param[12] = new SqlParameter("@createdBy", objBLLRestaurant.createdBy);
            param[13] = new SqlParameter("@email", objBLLRestaurant.email);
            param[14] = new SqlParameter("@isActive", objBLLRestaurant.isActive);
            param[15] = new SqlParameter("@userID", objBLLRestaurant.userID);
            param[16] = new SqlParameter("@businessPaymentTitle", objBLLRestaurant.businessPaymentTitle);
            param[17] = new SqlParameter("@commission", objBLLRestaurant.commission);
            param[18] = new SqlParameter("@paymentStatus", objBLLRestaurant.paymentStatus);
            param[19] = new SqlParameter("@restaurantpaymentAddress", objBLLRestaurant.restaurantpaymentAddress);
            param[20] = new SqlParameter("@alternativeEmail", objBLLRestaurant.alternativeEmail);
            param[21] = new SqlParameter("@url", objBLLRestaurant.url);
            param[22] = new SqlParameter("@cellNumber", objBLLRestaurant.cellNumber);
            param[23] = new SqlParameter("@preDealVerification", objBLLRestaurant.preDealVerification);
            param[24] = new SqlParameter("@postDealVerification", objBLLRestaurant.postDealVerification);
            param[25] = new SqlParameter("@restaurantlogo", objBLLRestaurant.restaurantlogo);
            param[26] = new SqlParameter("@ownerSignature", objBLLRestaurant.ownerSignature);
            
           // result = SqlHelper.ExecuteNonQuery(Misc.connStr, CommandType.StoredProcedure, "spCreateRestaurant", param);
            result = Convert.ToInt32(SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spCreateRestaurant", param).Tables[0].Rows[0][0]);
        }
        catch (Exception ex)
        {
            return 0;
        }
        return result;
    }

    public static int updateRestaurantInfo(BLLRestaurant objBLLRestaurant)
    {
        int result = 0;

        try
        {
            SqlParameter[] param = new SqlParameter[23];

            param[0] = new SqlParameter("@restaurantId", objBLLRestaurant.restaurantId);
            param[1] = new SqlParameter("@firstName", objBLLRestaurant.firstName);
            param[2] = new SqlParameter("@lastName", objBLLRestaurant.lastName);
            param[3] = new SqlParameter("@restaurantBusinessName", objBLLRestaurant.restaurantBusinessName);
            param[4] = new SqlParameter("@phone", objBLLRestaurant.phone);
            param[5] = new SqlParameter("@fax", objBLLRestaurant.fax);
            param[6] = new SqlParameter("@isActive", objBLLRestaurant.isActive);
            param[7] = new SqlParameter("@restaurantPicture", objBLLRestaurant.restaurantPicture);
            param[8] = new SqlParameter("@detail", objBLLRestaurant.detail);
            param[9] = new SqlParameter("@modifiedDate", objBLLRestaurant.modifiedDate);
            param[10] = new SqlParameter("@modifiedBy", objBLLRestaurant.modifiedBy);
            param[11] = new SqlParameter("@email", objBLLRestaurant.email);
            param[12] = new SqlParameter("@businessPaymentTitle", objBLLRestaurant.businessPaymentTitle);
            param[13] = new SqlParameter("@commission", objBLLRestaurant.commission);
            param[14] = new SqlParameter("@restaurantpaymentAddress", objBLLRestaurant.restaurantpaymentAddress);
            param[15] = new SqlParameter("@alternativeEmail", objBLLRestaurant.alternativeEmail);
            param[16] = new SqlParameter("@url", objBLLRestaurant.url);
            param[17] = new SqlParameter("@restaurantAddress", objBLLRestaurant.restaurantAddress);
            param[18] = new SqlParameter("@cellNumber", objBLLRestaurant.cellNumber);
            param[19] = new SqlParameter("@preDealVerification", objBLLRestaurant.preDealVerification);
            param[20] = new SqlParameter("@postDealVerification", objBLLRestaurant.postDealVerification);
            param[21] = new SqlParameter("@restaurantlogo", objBLLRestaurant.restaurantlogo);
            param[22] = new SqlParameter("@ownerSignature", objBLLRestaurant.ownerSignature);
            result = SqlHelper.ExecuteNonQuery(Misc.connStr, CommandType.StoredProcedure, "spUpdateRestaurantInfo", param);
        }
        catch (Exception ex)
        {
            return 0;
        }

        return result;
    }
    #endregion

    #region function to delete
    public static int deleteRestaurant(BLLRestaurant obj)
    {
        int result = 0;
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@restaurantId", obj.restaurantId);
            result = SqlHelper.ExecuteNonQuery(Misc.connStr, CommandType.StoredProcedure, "spDeleteRestaurant", param);
        }
        catch (Exception ex)
        {
            return 0;
        }
        return result;
    }
    #endregion

    #region Functions to get All Resturants
    public static DataTable getAllResturantsForAdmin()
    {
        DataTable dt = null;
        try
        {
            dt = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetAllResturantsForAdmin").Tables[0];
        }
        catch (Exception ex)
        {
            return null;
        }
        return dt;
    }

    public static DataSet getAllResturantsForAdminWithIndexing(int intStartIndex, int intMaxRecords)
    {
        DataSet dst = null;
        try
        {
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@startRowIndex", intStartIndex);
            param[1] = new SqlParameter("@maximumRows", intMaxRecords);
            dst = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetAllResturantsForAdminWithIndexing", param);            
        }
        catch (Exception ex)
        {
            return null;
        }
        return dst;
    }

    #endregion

    #region Functions to get Resturant By ID
    public static DataTable getRestaurantInfoByID(BLLRestaurant obj)
    {
        DataTable dt = null;
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@restaurantId", obj.restaurantId);
            dt = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetRestaurantInfoByID", param).Tables[0];
        }
        catch (Exception ex)
        {
            return null;
        }
        return dt;
    }

    public static DataTable getRestaurantInfoByResturantID(BLLRestaurant obj)
    {
        DataTable dt = null;
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@restaurantId", obj.restaurantId);
            dt = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetRestaurantInfoByResturantID", param).Tables[0];
        }
        catch (Exception ex)
        {
            return null;
        }
        return dt;
    }

    public static DataTable getFeaturedRestaurant()
    {
        DataTable dt = null;

        try
        {
            dt = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "SpGetFeaturedRestaurants").Tables[0];
        }
        catch (Exception ex)
        {
            return null;
        }
        return dt;
    }

  

    public static int updateRestaurantStatusByResID(BLLRestaurant objBLLRestaurant)
    {
        int result = 0;

        try
        {
            SqlParameter[] param = new SqlParameter[4];

            param[0] = new SqlParameter("@restaurantId", objBLLRestaurant.restaurantId);
            param[1] = new SqlParameter("@modifiedDate", objBLLRestaurant.modifiedDate);
            param[2] = new SqlParameter("@modifiedBy", objBLLRestaurant.modifiedBy);
            param[3] = new SqlParameter("@isActive", objBLLRestaurant.isActive);

            result = SqlHelper.ExecuteNonQuery(Misc.connStr, CommandType.StoredProcedure, "spUpdateRestaurantStatusInfoByResID", param);
        }
        catch (Exception ex)
        {
            return 0;
        }

        return result;
    }

    public static int updateRestaurantPaymentStatusByResID(BLLRestaurant objBLLRestaurant)
    {
        int result = 0;

        try
        {
            SqlParameter[] param = new SqlParameter[4];

            param[0] = new SqlParameter("@restaurantId", objBLLRestaurant.restaurantId);
            param[1] = new SqlParameter("@modifiedDate", objBLLRestaurant.modifiedDate);
            param[2] = new SqlParameter("@modifiedBy", objBLLRestaurant.modifiedBy);
            param[3] = new SqlParameter("@paymentStatus", objBLLRestaurant.paymentStatus);

            result = SqlHelper.ExecuteNonQuery(Misc.connStr, CommandType.StoredProcedure, "spUpdateRestaurantPaymentStatusByResID", param);
        }
        catch (Exception ex)
        {
            return 0;
        }

        return result;
    }

    public static int updateRestaurantDealVerificationInfoByResID(BLLRestaurant objBLLRestaurant)
    {
        int result = 0;

        try
        {
            SqlParameter[] param = new SqlParameter[3];

            param[0] = new SqlParameter("@restaurantId", objBLLRestaurant.restaurantId);
            param[1] = new SqlParameter("@preDealVerification", objBLLRestaurant.preDealVerification);
            param[2] = new SqlParameter("@postDealVerification", objBLLRestaurant.postDealVerification);

            result = SqlHelper.ExecuteNonQuery(Misc.connStr, CommandType.StoredProcedure, "spUpdateRestaurantDealVerificationInfoByResID", param);
        }
        catch (Exception ex)
        {
            return 0;
        }

        return result;
    }

    #endregion

    public static DataSet spGetAllResturantsForAdminWithIndexingByUserId(int intStartIndex, int intMaxRecords, BLLRestaurant obj)
    {
        DataSet dst = null;
        try
        {
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@startRowIndex", intStartIndex);
            param[1] = new SqlParameter("@maximumRows", intMaxRecords);
            param[2] = new SqlParameter("@userId", obj.userID);
            dst = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetAllResturantsForAdminWithIndexingByUserId", param);
        }
        catch (Exception ex)
        {
            return null;
        }
        return dst;
    }

    public static int updateRestaurantInfoByID(BLLRestaurant objRes, BLLUser obj)
    {
        int result = 0;
        try
        {
            SqlParameter[] param = new SqlParameter[18];
            /*
            param[0] = new SqlParameter("@restaurantId", objRes.restaurantId);
            param[1] = new SqlParameter("@restaurantName", objRes.restaurantName);
            param[2] = new SqlParameter("@restaurantAddress", objRes.restaurantAddress);
            param[3] = new SqlParameter("@city", objRes.city);
            param[4] = new SqlParameter("@zipCode", objRes.zipCode);
            param[5] = new SqlParameter("@phone", objRes.phone);
            param[6] = new SqlParameter("@fax", objRes.fax);
            param[7] = new SqlParameter("@cuisineId", objRes.cuisineId);
            param[8] = new SqlParameter("@detail", objRes.detail);
            param[9] = new SqlParameter("@isActive", obj.isActive);
            param[10] = new SqlParameter("@provinceId", obj.provinceId);
            param[11] = new SqlParameter("@modifiedDate", objRes.modifiedDate);
            param[12] = new SqlParameter("@modifiedBy", objRes.modifiedBy);            
            param[13] = new SqlParameter("@restaurantBusinessName", objRes.restaurantBusinessName);
            param[14] = new SqlParameter("@printerid", objRes.printerid);
            param[15] = new SqlParameter("@isFaxPhone", objRes.isFaxPhone);
            param[16] = new SqlParameter("@isFeatured", objRes.isFeatured);
            param[17] = new SqlParameter("@isReservatoin", objRes.isReservatoin);
            
            */
            result = SqlHelper.ExecuteNonQuery(Misc.connStr, CommandType.StoredProcedure, "spUpdateRestaurantInfoByID", param);
        }
        catch (Exception ex)
        {
            return 0;
        }
        return result;
    }
    public static DataTable getFriendUserInfoByRestauarntId(BLLRestaurant obj)
    {
        DataTable dt = null;
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@restaurantId", obj.restaurantId);
            dt = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetFriendUserInfoByRestauarntId", param).Tables[0];
        }
        catch (Exception ex)
        {
            return null;
        }
        return dt;
    }


    public static DataTable getBussinessByUserId(BLLRestaurant obj)
    {
        DataTable dt = null;
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@userId", obj.userID);
            dt = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "getAllInBussinessByUserId", param).Tables[0];
        }
        catch (Exception ex)
        {
            return null;
        }
        return dt;
    }
    
}
