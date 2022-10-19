using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Collections.Generic;
using SQLHelper;
using System.Data.SqlClient;
/// <summary>
/// Summary description for DALRestaurantComments
/// </summary>
public class DALRestaurantComments
{

    public static int createRestaurantComments(BLLRestaurantComents obj)
    {
        int result = 0;
        try
        {
            SqlParameter[] param = new SqlParameter[5];



            param[0] = new SqlParameter("@userID", obj.userID);
            param[1] = new SqlParameter("@RestaurantID", obj.bID);
            param[2] = new SqlParameter("@UserComments", obj.userComments);
            param[3] = new SqlParameter("@Feedback", obj.feedback);
            param[4] = new SqlParameter("@DetailID", obj.detailID);
            result = SqlHelper.ExecuteNonQuery(Misc.connStr, CommandType.StoredProcedure, "spRestaurantComments", param);

        }
        catch (Exception ex)
        {
        }
        return result;

    }

    public static DataTable  GetUserValue(BLLRestaurantComents obj)
    {
        DataTable dtDeals = null;
        try
        {
            SqlParameter[] param = new SqlParameter[1];

            param[0] = new SqlParameter("@userId", obj.userID);

            dtDeals = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetUserValue", param).Tables[0];

            if (dtDeals != null && dtDeals.Rows.Count > 0)
            {
                return dtDeals;
            }
        }
        catch (Exception ex)
        {
            return null;
        }
        return dtDeals;
    }

    public static DataTable GetRestaurantComments(BLLRestaurantComents obj)
    {
        DataTable dtDeals = null;
        try
        {
            SqlParameter[] param = new SqlParameter[1];

            param[0] = new SqlParameter("@userId", obj.userID);

            dtDeals = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetRestaurantComments", param).Tables[0];

            if (dtDeals != null && dtDeals.Rows.Count > 0)
            {
                return dtDeals;
            }
        }
        catch (Exception ex)
        {
            return null;
        }
        return dtDeals;
    }

}


