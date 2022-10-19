using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using SQLHelper;

/// <summary>
/// Summary description for DalUserComments
/// </summary>
public class DALUserComments
{
    #region Function to Create New Deal Discussion

    public static int AddNewUserComments(BLLUserComments objBLLUserComments)
    {
        int result = 0;

        try
        {
            SqlParameter[] param = new SqlParameter[5];

            param[0] = new SqlParameter("@cmtDatetime", objBLLUserComments.cmtDatetime);
            param[1] = new SqlParameter("@userId", objBLLUserComments.userId);
            param[2] = new SqlParameter("@comment", objBLLUserComments.comment);            
            param[3] = new SqlParameter("@commentby", objBLLUserComments.commentby);
            if (objBLLUserComments.dOrderID == 0)
            {
                param[4] = new SqlParameter("@dOrderID", DBNull.Value);
            }
            else
            {
                param[4] = new SqlParameter("@dOrderID", objBLLUserComments.dOrderID);
            }

            result = SqlHelper.ExecuteNonQuery(Misc.connStr, CommandType.StoredProcedure, "spCreateNewUserComments", param);
        }
        catch (Exception ex)
        {
            return 0;
        }

        return result;
    }

    #endregion
  

    #region Functions to get Deal Discussion By Deal Id

    public static DataTable getUserCommentsByUserId(BLLUserComments objBLLUserComments)
    {
        DataTable dtUserComments = null;
        try
        {
            SqlParameter[] param = new SqlParameter[1];

            param[0] = new SqlParameter("@userId", objBLLUserComments.userId);

            dtUserComments = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetUserCommentsByUserId", param).Tables[0];

            if (dtUserComments != null && dtUserComments.Rows.Count > 0)
            {
                return dtUserComments;
            }
        }
        catch (Exception ex)
        {
            return null;
        }
        return dtUserComments;
    }
            
    #endregion

    #region Function to Delete Deal Discussion By Discussion Id

    public static int deleteUserCommentsByCommentID(BLLUserComments objBLLUserComments)
    {
        int iChk = 0;

        try
        {
            SqlParameter[] param = new SqlParameter[1];

            param[0] = new SqlParameter("@commentId", objBLLUserComments.commentId);

            iChk = SqlHelper.ExecuteNonQuery(Misc.connStr, CommandType.StoredProcedure, "spDeleteUserCommentsByCommentID", param);
        }
        catch (Exception ex)
        {
            return iChk;
        }

        return iChk;
    }

    #endregion
}
