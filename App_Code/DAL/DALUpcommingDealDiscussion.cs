using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using SQLHelper;

/// <summary>
/// Summary description for DalUpcommingDealDiscussion
/// </summary>
public class DALUpcommingDealDiscussion
{
    #region Function to Create New Deal Discussion

    public static int AddNewUpcommingDealDiscussion(BLLUpcommingDealDiscussion objBLLUpcommingDealDiscussion)
    {
        int result = 0;

        try
        {
            SqlParameter[] param = new SqlParameter[5];

            param[0] = new SqlParameter("@updealId", objBLLUpcommingDealDiscussion.UpdealId);
            param[1] = new SqlParameter("@userId", objBLLUpcommingDealDiscussion.UserId);
            param[2] = new SqlParameter("@comments", objBLLUpcommingDealDiscussion.Comments);
            param[3] = new SqlParameter("@cmtDatetime", objBLLUpcommingDealDiscussion.CmtDatetime);
            param[4] = new SqlParameter("@pdiscussionId", objBLLUpcommingDealDiscussion.pdiscussionId);

            result = SqlHelper.ExecuteNonQuery(Misc.connStr, CommandType.StoredProcedure, "spCreateNewUpcommingDealDiscussion", param);
        }
        catch (Exception ex)
        {
            return 0;
        }

        return result;
    }

    #endregion

    #region Function to Update Deal Discussion Info by Discussion Id

    public static int updateUpcommingDealDiscussionInfoByDiscussionId(BLLUpcommingDealDiscussion objBLLUpcommingDealDiscussion)
    {
        int result = 0;
        try
        {
            SqlParameter[] param = new SqlParameter[2];

            param[0] = new SqlParameter("@discussionId", objBLLUpcommingDealDiscussion.DiscussionId);            
            param[1] = new SqlParameter("@comments", objBLLUpcommingDealDiscussion.Comments);
            result = SqlHelper.ExecuteNonQuery(Misc.connStr, CommandType.StoredProcedure, "spUpdateUpcommingDealDiscussionInfoByDiscussionId", param);
        }
        catch (Exception ex)
        {
            return 0;
        }
        return result;
    }

    #endregion

    #region Functions to Get All Deal Discussions

    public static DataTable getAllUpcommingDealDiscussions()
    {
        DataTable dtUpcommingDealDiscussions = null;

        try
        {
            dtUpcommingDealDiscussions = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetAllUpcommingDealDiscussions").Tables[0];

            if (dtUpcommingDealDiscussions != null && dtUpcommingDealDiscussions.Rows.Count > 0)
            {
                return dtUpcommingDealDiscussions;
            }
        }
        catch (Exception ex)
        {
            return null;
        }

        return dtUpcommingDealDiscussions;
    }

    #endregion

    #region Functions to get Deal Discussion By Deal Id

    public static DataTable getUpcommingDealDiscussionByUpdealId(BLLUpcommingDealDiscussion objBLLUpcommingDealDiscussion)
    {
        DataTable dtUpcommingDealDiscussion = null;
        try
        {
            SqlParameter[] param = new SqlParameter[1];

            param[0] = new SqlParameter("@updealId", objBLLUpcommingDealDiscussion.UpdealId);

            dtUpcommingDealDiscussion = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetUpcommingDealDiscussionByUpdealId", param).Tables[0];

            if (dtUpcommingDealDiscussion != null && dtUpcommingDealDiscussion.Rows.Count > 0)
            {
                return dtUpcommingDealDiscussion;
            }
        }
        catch (Exception ex)
        {
            return null;
        }
        return dtUpcommingDealDiscussion;
    }
            
    #endregion

    #region Function to Delete Deal Discussion By Discussion Id

    public static int deleteUpcommingDealDiscussionByDiscussionId(BLLUpcommingDealDiscussion objBLLUpcommingDealDiscussion)
    {
        int iChk = 0;

        try
        {
            SqlParameter[] param = new SqlParameter[1];

            param[0] = new SqlParameter("@discussionId", objBLLUpcommingDealDiscussion.DiscussionId);

            iChk = SqlHelper.ExecuteNonQuery(Misc.connStr, CommandType.StoredProcedure, "spDeleteUpcommingDealDiscussionByDiscussionId", param);
        }
        catch (Exception ex)
        {
            return iChk;
        }

        return iChk;
    }

    #endregion
}
