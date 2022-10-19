using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using SQLHelper;
using System.Data;

/// <summary>
/// Summary description for DALLiveFeed
/// </summary>
public class DALLiveFeed
{
		 public DataTable GetFeeds()
        {
            DataTable dt = null;
            try
            {
                dt = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "sp_GetFeeds").Tables[0];
            }
            catch (Exception ex)
            {
                return dt = null;
            }
            return dt;
        }
         public static int UpdateTotalFvrtInFeeds(BLLLiveFeed obj)
         {
             int result = 0;
             try
             {
                 SqlParameter[] param = new SqlParameter[2];
                 param[0] = new SqlParameter("@dealId", obj.dealId);
                 param[1] = new SqlParameter("@userId", obj.userId);
                
                 result = SqlHelper.ExecuteNonQuery(Misc.connStr, CommandType.StoredProcedure, "UpdateTotalFvrtInFeeds", param);

             }
             catch (Exception ex)
             {
                 return 0;
             }
             return result;
         }
         public static int UpdateTotalFav(BLLLiveFeed obj)
         {
             int result = 0;
             try
             {
                 SqlParameter[] param = new SqlParameter[1];
                 param[0] = new SqlParameter("@dealId", obj.dealId);
                 

                 result = SqlHelper.ExecuteNonQuery(Misc.connStr, CommandType.StoredProcedure, "UpdateTotalFav", param);

             }
             catch (Exception ex)
             {
                 return 0;
             }
             return result;
         }


         public static int UpdateTotalCommentsInFeeds(BLLLiveFeed obj)
         {
             int result = 0;
             try
             {
                 SqlParameter[] param = new SqlParameter[2];
                 param[0] = new SqlParameter("@dealId", obj.@dealId);
                 param[1] = new SqlParameter("@totalcomments", obj.totalComments);

                 result = SqlHelper.ExecuteNonQuery(Misc.connStr, CommandType.StoredProcedure, "UpdateTotalCommentsInFeeds", param);

             }
             catch (Exception ex)
             {
                 return 0;
             }
             return result;
         }

         public static DataTable getDealDetailByDealDealID(BLLLiveFeed obj)
         {
             DataTable dt = null;
             try
             {
                 SqlParameter[] param = new SqlParameter[1];
                 param[0] = new SqlParameter("@dealId", obj.dealId);

                 dt = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "getDealDetailByDealDealID", param).Tables[0];
             }
             catch (Exception ex)
             {
                 return dt = null;
             }
             return dt;
         }
}




