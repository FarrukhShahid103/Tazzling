using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SQLHelper;
using System.Data;
using System.Data.SqlClient;
/// <summary>
/// Summary cityDescription for DALCities
/// </summary>
public static class DALReferralTracker
{
    public static int createReferralTracker(BLLReferralTracker obj)
    {
        int result = 0;
        try 
        {
            SqlParameter[] param = new SqlParameter[5];
            param[0] = new SqlParameter("@email", obj.email);
            param[1] = new SqlParameter("@isSignup", obj.isSignup);
            param[2] = new SqlParameter("@trackBy", obj.trackBy);
            param[3] = new SqlParameter("@trackerDate", obj.trackerDate);
            param[4] = new SqlParameter("@trackerName", obj.trackerName);
            
            result = SqlHelper.ExecuteNonQuery(Misc.connStr, CommandType.StoredProcedure, "spCreateReferralTracker", param);
        }
        catch (Exception ex)
        { 
        
        }
        return result;
    }
    
    public static int updateReferralTrackerByTrackerIDAndEmail(BLLReferralTracker obj)
    {
        int result = 0;
        try
        {
            SqlParameter[] param = new SqlParameter[5];
            param[0] = new SqlParameter("@isSignup", obj.isSignup);
            param[1] = new SqlParameter("@signupDate", obj.signupDate);
            param[2] = new SqlParameter("@trackBy", obj.trackBy);
            param[3] = new SqlParameter("@email", obj.email);
            param[4] = new SqlParameter("@signupID", obj.signupID);
            
            result = SqlHelper.ExecuteNonQuery(Misc.connStr, CommandType.StoredProcedure, "spUpdateReferralTrackerByTrackerIDAndEmail", param);
        }
        catch (Exception ex)
        {
        }
        return result;
    }

    public static DataTable getReferralTrackerByTrackerID(BLLReferralTracker objBLLDeals)
    {
        DataTable dtDeals = null;
        try
        {
            SqlParameter[] param = new SqlParameter[1];

            param[0] = new SqlParameter("@trackBy", objBLLDeals.trackBy);
            dtDeals = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetReferralTrackerByTrackerID", param).Tables[0];

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

    public static DataTable getReferralTrackerByEmail(BLLReferralTracker objBLLDeals)
    {
        DataTable dtDeals = null;
        try
        {
            SqlParameter[] param = new SqlParameter[1];

            param[0] = new SqlParameter("@email", objBLLDeals.email);
            dtDeals = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetReferralTrackerByEmail", param).Tables[0];

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
