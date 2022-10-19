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
/// Summary description for DALComments
/// </summary>
public static class DALRestaurantLeads
{
    #region Function to create new
    public static long createRestaurantLeads(BLLRestaurantLeads obj)
    {
        long result = 0;
        try
        {
            SqlParameter[] param = new SqlParameter[13];
            param[0] = new SqlParameter("@restaurantLeadName", obj.restaurantLeadName);
            param[1] = new SqlParameter("@restaurantLeadOwnerName", obj.restaurantLeadOwnerName);
            param[2] = new SqlParameter("@restaurantLeadOwnerPhoneNumber", obj.restaurantLeadOwnerPhoneNumber);
            param[3] = new SqlParameter("@restaurantLeadPhoneNumber", obj.restaurantLeadPhoneNumber);
            param[4] = new SqlParameter("@restaurantLeadSignUpID", obj.restaurantLeadSignUpID);
            param[5] = new SqlParameter("@restaurantLeadStatus", obj.restaurantLeadStatus);
            param[6] = new SqlParameter("@creationDate", obj.creationDate);
            param[7] = new SqlParameter("@createdBy", obj.createdBy);
            param[8] = new SqlParameter("@restaurantLeadAddress", obj.restaurantLeadAddress);
            param[9] = new SqlParameter("@provinceId", obj.provinceId);
            if (obj.cityId == 0)
            {
                param[10] = new SqlParameter("@cityId", DBNull.Value);
            }
            else
            {
                param[10] = new SqlParameter("@cityId", obj.cityId);
            }
            param[11] = new SqlParameter("@restaurantAssignto", obj.restaurantAssignto);
            param[12] = new SqlParameter("@priority", obj.priority);
            
            result = Convert.ToInt64(SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spCreateRestaurantLeads", param).Tables[0].Rows[0][0]);
        }
        catch (Exception ex)
        {
            return 0;
        }
        return result;
    }
    #endregion

    #region function to delete
    public static int deleteRestaurantLeads(BLLRestaurantLeads obj)
    {
        int result = 0;
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@restaurantLeadID", obj.restaurantLeadID);
            result = SqlHelper.ExecuteNonQuery(Misc.connStr, CommandType.StoredProcedure, "spDeleteRestaurantLeads", param);
        }
        catch (Exception ex)
        {
            return 0;
        }
        return result;
    }
    #endregion

    #region Function to Update Comments
    public static int updateRestaurantLeads(BLLRestaurantLeads obj)
    {
        int result = 0;
        try
        {
            SqlParameter[] param = new SqlParameter[14];
            param[0] = new SqlParameter("@restaurantLeadName", obj.restaurantLeadName);
            param[1] = new SqlParameter("@restaurantLeadOwnerName", obj.restaurantLeadOwnerName);
            param[2] = new SqlParameter("@restaurantLeadOwnerPhoneNumber", obj.restaurantLeadOwnerPhoneNumber);
            param[3] = new SqlParameter("@restaurantLeadPhoneNumber", obj.restaurantLeadPhoneNumber);
            param[4] = new SqlParameter("@restaurantLeadSignUpID", obj.restaurantLeadSignUpID);
            param[5] = new SqlParameter("@restaurantLeadStatus", obj.restaurantLeadStatus);
            param[6] = new SqlParameter("@modifiedDate", obj.modifiedDate);
            param[7] = new SqlParameter("@modifiedBy", obj.modifiedBy);
            param[8] = new SqlParameter("@restaurantLeadID", obj.restaurantLeadID);
            param[9] = new SqlParameter("@restaurantLeadAddress", obj.restaurantLeadAddress);
            param[10] = new SqlParameter("@provinceId", obj.provinceId);
            if (obj.cityId == 0)
            {
                param[11] = new SqlParameter("@cityId", DBNull.Value);
            }
            else
            {
                param[11] = new SqlParameter("@cityId", obj.cityId);
            }
            param[12] = new SqlParameter("@restaurantAssignto", obj.restaurantAssignto);
            param[13] = new SqlParameter("@priority", obj.priority);
            result = SqlHelper.ExecuteNonQuery(Misc.connStr, CommandType.StoredProcedure, "spUpdateRestaurantLeads", param);
        }
        catch (Exception ex)
        {
            return 0;
        }
        return result;
    }
    #endregion

    #region Functions to get comments by 
    public static DataTable getRestaurantLeadsByID(BLLRestaurantLeads obj)
    {
        DataTable dt = null;
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@restaurantLeadID", obj.restaurantLeadID);
            dt = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetRestaurantLeadsByID", param).Tables[0];
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

    public static DataTable getRestaurantByUserName(BLLRestaurantLeads obj)
    {
        DataTable dt = null;
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@userName", obj.restaurantLeadSignUpID);
            dt = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetRestaurantByUserName", param).Tables[0];
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


    


    public static DataTable getAllRestaurantLeads()
    {
        DataTable dt = null;
        try
        {
            dt = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetAllRestaurantLeads").Tables[0];
        }
        catch (Exception ex)
        {

        }
        return dt;
    }
    #endregion
}
