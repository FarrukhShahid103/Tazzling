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
/// Summary description for DALAdminPointsGift
/// </summary>
public static class DALMemberPointGiftsRequests
{
    #region Function to create new 
    public static long createMemberPointGiftsRequests(BLLMemberPointGiftsRequests obj)
    {
        long result = 0;
        try
        {
            SqlParameter[] param = new SqlParameter[8];
            param[0] = new SqlParameter("@mrpgName", obj.mrpgName);
            param[1] = new SqlParameter("@mrpgPoints", obj.mrpgPoints);
            param[2] = new SqlParameter("@mrpgImage", obj.mrpgImage);
            param[3] = new SqlParameter("@mrpgDescription", obj.mrpgDescription);            
            param[4] = new SqlParameter("@creationDate", obj.creationDate);
            param[5] = new SqlParameter("@createdBy", obj.createdBy);
            param[6] = new SqlParameter("@pgID", obj.pgID);
            param[7] = new SqlParameter("@status", obj.status);
            result = Convert.ToInt64(SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spCreateMemberPointGiftsRequests", param).Tables[0].Rows[0][0]);
        }
        catch (Exception ex)
        {
            return 0;
        }
        return result;
    }
    #endregion

    #region function to delete 
    public static int deleteMemberPointGiftsRequests(BLLMemberPointGiftsRequests obj)
    {
        int result = 0;
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@mrpgID", obj.mrpgID);
            result = SqlHelper.ExecuteNonQuery(Misc.connStr, CommandType.StoredProcedure, "spDeleteMemberPointGiftsRequests", param);
        }
        catch (Exception ex)
        {
            return 0;
        }
        return result;
    }
    #endregion

    #region Function to Update 
    public static int updateMemberPointGiftsRequests(BLLMemberPointGiftsRequests obj)
    {
        int result = 0;
        try
        {
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@mrpgID", obj.mrpgID);            
            param[1] = new SqlParameter("@modifiedDate", obj.modifiedDate);
            param[2] = new SqlParameter("@modifiedBy", obj.modifiedBy);
            param[3] = new SqlParameter("@status", obj.status);
            result = SqlHelper.ExecuteNonQuery(Misc.connStr, CommandType.StoredProcedure, "spUpdateMemberPointGiftsRequests", param);
        }
        catch (Exception ex)
        {
            return 0;
        }
        return result;
    }
    #endregion

    #region Functions to get


    public static DataTable getTotalUsedPointsByUserID(BLLMemberPointGiftsRequests obj)
    {
        DataTable dt = null;
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@createdBy", obj.createdBy);
            dt = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetTotalUsedPointsByUserID", param).Tables[0];
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


    public static DataTable getAllMemberPointGiftsRequestsByUserID(BLLMemberPointGiftsRequests obj)
    {
        DataTable dt = null;
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@createdBy", obj.createdBy);
            dt = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetAllMemberPointGiftsRequestsByUserID", param).Tables[0];
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


    public static DataTable getAllMemberPointGiftsRequests()
    {
        DataTable dt = null;
        try
        {
            dt = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetAllMemberPointGiftsRequests").Tables[0];
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

    public static DataTable getMemberPointGiftsRequestsByID(BLLMemberPointGiftsRequests obj)
    {
        DataTable dt = null;
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@mrpgID", obj.mrpgID);
            dt = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetMemberPointGiftsRequestsByID", param).Tables[0];
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
