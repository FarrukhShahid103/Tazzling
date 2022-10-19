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
public static class DALUpcommingDeals
{
    public static int createUpcommingDeals(BLLUpcommingDeals obj)
    {
        int result = 0;
        try 
        {
            SqlParameter[] param = new SqlParameter[9];
            param[0] = new SqlParameter("@createdBy", obj.createdBy);
            param[1] = new SqlParameter("@createdDate", obj.createdDate);
            param[2] = new SqlParameter("@description", obj.description);
            param[3] = new SqlParameter("@modifiedBy", obj.modifiedBy);
            param[4] = new SqlParameter("@modifiedDate", obj.modifiedDate);
            param[5] = new SqlParameter("@restaurantId", obj.restaurantId);
            param[6] = new SqlParameter("@title", obj.title);
            param[7] = new SqlParameter("@postDealVerification", obj.postDealVerification);
            param[8] = new SqlParameter("@preDealVerification", obj.preDealVerification);
            result = SqlHelper.ExecuteNonQuery(Misc.connStr, CommandType.StoredProcedure, "spCreateUpcommingDeals", param);
        }
        catch (Exception ex)
        { 
        
        }
        return result;
    }
    public static int updateUpcommingDeals(BLLUpcommingDeals obj)
    {
        int result = 0;
        try
        {
            SqlParameter[] param = new SqlParameter[6];            
            param[0] = new SqlParameter("@description", obj.description);
            param[1] = new SqlParameter("@modifiedBy", obj.modifiedBy);
            param[2] = new SqlParameter("@modifiedDate", obj.modifiedDate);
            param[3] = new SqlParameter("@updealId", obj.updealId);
            param[4] = new SqlParameter("@postDealVerification", obj.postDealVerification);
            param[5] = new SqlParameter("@preDealVerification", obj.preDealVerification);
            result = SqlHelper.ExecuteNonQuery(Misc.connStr, CommandType.StoredProcedure, "spUpdateUpcommingDeals", param);
        }
        catch (Exception ex)
        {
        }
        return result;
    }

    public static DataTable getupCommingDealForDealId(BLLUpcommingDeals objBLLDeals)
    {
        DataTable dtDeals = null;
        try
        {
            SqlParameter[] param = new SqlParameter[1];

            param[0] = new SqlParameter("@updealId", objBLLDeals.updealId);
            dtDeals = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetupCommingDealForDealId", param).Tables[0];

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

    public static int deleteUpcommingDeals(BLLUpcommingDeals obj)
    {
        int result = 0;
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@updealId", obj.updealId);
            result = SqlHelper.ExecuteNonQuery(Misc.connStr, CommandType.StoredProcedure, "spDeleteUpcommingDeals", param);
        }
        catch (Exception ex)
        {

        }
        return result;
    }

    

}
