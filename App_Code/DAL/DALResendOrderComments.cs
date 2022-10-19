using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using SQLHelper;

/// <summary>
/// Summary description for DalResendOrderComments
/// </summary>
public class DALResendOrderComments
{
    #region Function to Create New Deal Discussion

    public static int createResendOrderComments(BLLResendOrderComments objBLLResendOrderComments)
    {
        int result = 0;

        try
        {
            SqlParameter[] param = new SqlParameter[5];

            param[0] = new SqlParameter("@cmtDatetime", objBLLResendOrderComments.cmtDatetime);
            param[1] = new SqlParameter("@resendOrderID", objBLLResendOrderComments.resendOrderID);
            param[2] = new SqlParameter("@comment", objBLLResendOrderComments.comment);            
            param[3] = new SqlParameter("@commentby", objBLLResendOrderComments.commentby);
           
            result = SqlHelper.ExecuteNonQuery(Misc.connStr, CommandType.StoredProcedure, "spCreateResendOrderComments", param);
        }
        catch (Exception ex)
        {
            return 0;
        }

        return result;
    }

    #endregion
  

    #region Functions to get Deal Discussion By Deal Id

    public static DataTable getResendOrderCommentsByResendOrderId(BLLResendOrderComments objBLLResendOrderComments)
    {
        DataTable dtResendOrderComments = null;
        try
        {
            SqlParameter[] param = new SqlParameter[1];

            param[0] = new SqlParameter("@resendOrderID", objBLLResendOrderComments.resendOrderID);

            dtResendOrderComments = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetResendOrderCommentsByResendOrderId", param).Tables[0];

            if (dtResendOrderComments != null && dtResendOrderComments.Rows.Count > 0)
            {
                return dtResendOrderComments;
            }
        }
        catch (Exception ex)
        {
            return null;
        }
        return dtResendOrderComments;
    }
            
    #endregion

    #region Function to Delete Deal Discussion By Discussion Id

    public static int deleteResendOrderCommentsByCommentID(BLLResendOrderComments objBLLResendOrderComments)
    {
        int iChk = 0;

        try
        {
            SqlParameter[] param = new SqlParameter[1];

            param[0] = new SqlParameter("@resendOrdercommentId", objBLLResendOrderComments.resendOrdercommentId);

            iChk = SqlHelper.ExecuteNonQuery(Misc.connStr, CommandType.StoredProcedure, "spDeleteResendOrderCommentsByCommentID", param);
        }
        catch (Exception ex)
        {
            return iChk;
        }

        return iChk;
    }
    #endregion
}
