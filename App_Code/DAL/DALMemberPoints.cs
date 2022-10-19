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
/// Summary description for DALMemberPoints
/// </summary>
public static class DALMemberPoints
{
    #region Function to create new 
    public static long createMemberPoints(BLLMemberPoints obj)
    {
        long result = 0;
        try
        {
            SqlParameter[] param = new SqlParameter[7];
            param[0] = new SqlParameter("@createdBy", obj.createdBy);
            param[1] = new SqlParameter("@creationDate", obj.creationDate);
            param[2] = new SqlParameter("@points", obj.points);
            param[3] = new SqlParameter("@pointsGetsFrom", obj.pointsGetsFrom);
            param[4] = new SqlParameter("@tID", obj.tID);
            param[5] = new SqlParameter("@userID", obj.userID);
            param[6] = new SqlParameter("@description", obj.description);
            
            result = Convert.ToInt64(SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spCreateMemberPoints", param).Tables[0].Rows[0][0]);
        }
        catch (Exception ex)
        {
            return 0;
        }
        return result;
    }
    #endregion

    #region function to delete
    public static int deleteMemberPoints(BLLMemberPoints obj)
    {
        int result = 0;
        try
        {
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@pID", obj.pID);
            param[1] = new SqlParameter("@isDeleted", obj.isDeleted);
            param[2] = new SqlParameter("@deletedBy", obj.deletedBy);
            param[3] = new SqlParameter("@deletedDate", obj.deletedDate);
            result = SqlHelper.ExecuteNonQuery(Misc.connStr, CommandType.StoredProcedure, "spDeleteMemberPoints", param);
        }
        catch (Exception ex)
        {
            return 0;
        }
        return result;
    }
    #endregion

    #region Function to Update
    public static int updateMemberPoints(BLLMemberPoints obj)
    {
        int result = 0;
        try
        {
            SqlParameter[] param = new SqlParameter[6];
            param[0] = new SqlParameter("@pID", obj.pID);
            param[1] = new SqlParameter("@modifiedBy", obj.modifiedBy);
            param[2] = new SqlParameter("@modifiedDate", obj.modifiedDate);
            param[3] = new SqlParameter("@points", obj.points);
            param[4] = new SqlParameter("@userID", obj.userID);
            param[5] = new SqlParameter("@description", obj.description);            
            result = SqlHelper.ExecuteNonQuery(Misc.connStr, CommandType.StoredProcedure, "spUpdateMemberPoints", param);
        }
        catch (Exception ex)
        {
            return 0;
        }
        return result;
    }
    #endregion

    #region Functions to get comments by RestaurantID
    public static DataTable getTotalPointsByUserID(BLLMemberPoints obj)
    {
        DataTable dt = null;
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@userID", obj.userID);
            dt = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetTotalPointsByUserID", param).Tables[0];
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

    public static DataTable spGetAllMemberPointsByUserID(BLLMemberPoints obj)
    {
        DataTable dt = null;
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@userID", obj.userID);
            dt = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetAllMemberPointsByUserID", param).Tables[0];
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



    public static DataTable getAllMemberPointsForAdmin(BLLMemberPoints obj)
    {
        DataTable dt = null;
        try
        {
            dt = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetAllMemberPointsForAdmin").Tables[0];
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


    


    #endregion
}
