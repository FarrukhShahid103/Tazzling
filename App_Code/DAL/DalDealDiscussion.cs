using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using SQLHelper;

/// <summary>
/// Summary description for DalDealDiscussion
/// </summary>
public class DalDealDiscussion
{
    #region Function to Create New Deal Discussion

    public static int AddNewDealDiscussion(BLLDealDiscussion objBLLDealDiscussion)
    {
        int result = 0;

        try
        {
            SqlParameter[] param = new SqlParameter[5];

            param[0] = new SqlParameter("@dealId", objBLLDealDiscussion.DealId);
            param[1] = new SqlParameter("@userId", objBLLDealDiscussion.UserId);
            param[2] = new SqlParameter("@comments", objBLLDealDiscussion.Comments);
            param[3] = new SqlParameter("@cmtDatetime", objBLLDealDiscussion.CmtDatetime);
            param[4] = new SqlParameter("@pdiscussionId", objBLLDealDiscussion.pdiscussionId);

            result = SqlHelper.ExecuteNonQuery(Misc.connStr, CommandType.StoredProcedure, "spCreateNewDealDiscussion", param);
        }
        catch (Exception ex)
        {
            return 0;
        }

        return result;
    }

    #endregion

    #region Function to Update Deal Discussion Info by Discussion Id

    public static int updateDealDiscussionInfoByDiscussionId(BLLDealDiscussion objBLLDealDiscussion)
    {
        int result = 0;
        try
        {
            SqlParameter[] param = new SqlParameter[3];

            param[0] = new SqlParameter("@discussionId", objBLLDealDiscussion.DiscussionId);            
            param[1] = new SqlParameter("@comments", objBLLDealDiscussion.Comments);            
            result = SqlHelper.ExecuteNonQuery(Misc.connStr, CommandType.StoredProcedure, "spUpdateDealDiscussionByDiscussionId", param);
        }
        catch (Exception ex)
        {
            return 0;
        }
        return result;
    }

    #endregion

    #region Functions to Get All Deal Discussions

    public static DataTable getAllDealDiscussions()
    {
        DataTable dtDealDiscussions = null;

        try
        {
            dtDealDiscussions = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetAllDealDiscussions").Tables[0];

            if (dtDealDiscussions != null && dtDealDiscussions.Rows.Count > 0)
            {
                return dtDealDiscussions;
            }
        }
        catch (Exception ex)
        {
            return null;
        }

        return dtDealDiscussions;
    }

    #endregion

    #region Functions to get Deal Discussion By Deal Id

    public static DataTable getDealDiscussionByDealId(BLLDealDiscussion objBLLDealDiscussion)
    {
        DataTable dtDealDiscussion = null;
        try
        {
            SqlParameter[] param = new SqlParameter[1];

            param[0] = new SqlParameter("@dealId", objBLLDealDiscussion.DealId);

            dtDealDiscussion = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetDealDiscussionByDealId", param).Tables[0];

            if (dtDealDiscussion != null && dtDealDiscussion.Rows.Count > 0)
            {
                return dtDealDiscussion;
            }
        }
        catch (Exception ex)
        {
            return null;
        }
        return dtDealDiscussion;
    }

    public static DataTable getLatestDealDiscussionCommentByDealId(BLLDealDiscussion objBLLDealDiscussion)
    {
        DataTable dtDealDiscussion = null;
        try
        {
            SqlParameter[] param = new SqlParameter[1];

            param[0] = new SqlParameter("@dealId", objBLLDealDiscussion.DealId);

            dtDealDiscussion = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetLatestDealDiscussionCommentByDealId", param).Tables[0];

            if (dtDealDiscussion != null && dtDealDiscussion.Rows.Count > 0)
            {
                return dtDealDiscussion;
            }
        }
        catch (Exception ex)
        {
            return null;
        }
        return dtDealDiscussion;
    }


    

    public static DataTable getDealForAdminDiscussionByDealId(BLLDealDiscussion objBLLDealDiscussion)
    {
        DataTable dtDealDiscussion = null;
        try
        {
            SqlParameter[] param = new SqlParameter[1];

            param[0] = new SqlParameter("@dealId", objBLLDealDiscussion.DealId);

            dtDealDiscussion = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetDealForAdminDiscussionByDealId", param).Tables[0];

            if (dtDealDiscussion != null && dtDealDiscussion.Rows.Count > 0)
            {
                return dtDealDiscussion;
            }
        }
        catch (Exception ex)
        {
            return null;
        }
        return dtDealDiscussion;
    }
    
    public static DataTable getAllDealDiscussionByParentID(BLLDealDiscussion objBLLDealDiscussion)
    {
        DataTable dtDealDiscussion = null;
        try
        {
            SqlParameter[] param = new SqlParameter[1];

            param[0] = new SqlParameter("@pdiscussionId", objBLLDealDiscussion.pdiscussionId);

            dtDealDiscussion = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetAllDealDiscussionByParentID", param).Tables[0];

            if (dtDealDiscussion != null && dtDealDiscussion.Rows.Count > 0)
            {
                return dtDealDiscussion;
            }
        }
        catch (Exception ex)
        {
            return null;
        }
        return dtDealDiscussion;
    }

    public static DataTable getDealDiscussionByParentID(BLLDealDiscussion objBLLDealDiscussion)
    {
        DataTable dtDealDiscussion = null;
        try
        {
            SqlParameter[] param = new SqlParameter[1];

            param[0] = new SqlParameter("@pdiscussionId", objBLLDealDiscussion.pdiscussionId);

            dtDealDiscussion = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetDealDiscussionByParentID", param).Tables[0];

            if (dtDealDiscussion != null && dtDealDiscussion.Rows.Count > 0)
            {
                return dtDealDiscussion;
            }
        }
        catch (Exception ex)
        {
            return null;
        }
        return dtDealDiscussion;
    }

    public static DataTable getDealDiscussionByDiscussionId(BLLDealDiscussion objBLLDealDiscussion)
    {
        DataTable dtDealDiscussion = null;
        try
        {
            SqlParameter[] param = new SqlParameter[1];

            param[0] = new SqlParameter("@DiscussionId", objBLLDealDiscussion.DiscussionId);

            dtDealDiscussion = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetDealDiscussionByDiscussionId", param).Tables[0];

            if (dtDealDiscussion != null && dtDealDiscussion.Rows.Count > 0)
            {
                return dtDealDiscussion;
            }
        }
        catch (Exception ex)
        {
            return null;
        }
        return dtDealDiscussion;
    }

    #endregion

    #region Function to Delete Deal Discussion By Discussion Id

    public static int deleteDealDiscussionByDiscussionId(BLLDealDiscussion objBLLDealDiscussion)
    {
        int iChk = 0;

        try
        {
            SqlParameter[] param = new SqlParameter[1];

            param[0] = new SqlParameter("@discussionId", objBLLDealDiscussion.DiscussionId);

            iChk = SqlHelper.ExecuteNonQuery(Misc.connStr, CommandType.StoredProcedure, "spDeleteDealDiscussionByDiscussionId", param);
        }
        catch (Exception ex)
        {
            return iChk;
        }

        return iChk;
    }

    #endregion
}
