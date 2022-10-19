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
/// Summary description for DALComments
/// </summary>
public static class DALComments
{
    #region Function to create new Comments
    public static long createComments(BLLComments obj)
    {
        long result = 0;
        try
        {
            SqlParameter[] param = new SqlParameter[5];
            param[0] = new SqlParameter("@title", obj.title);
            param[1] = new SqlParameter("@comment", obj.comment);
            param[2] = new SqlParameter("@restaurantID", obj.restaurantID);
            param[3] = new SqlParameter("@creationDate", obj.creationDate);
            param[4] = new SqlParameter("@createdBy", obj.createdBy);            
            result = Convert.ToInt64(SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spCreateComments", param).Tables[0].Rows[0][0]);
        }
        catch (Exception ex)
        {
            return 0;
        }
        return result;
    }
    #endregion

    #region function to delete Comments
    public static int deleteComments(BLLComments obj)
    {
        int result = 0;
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@commentId", obj.commentId);
            result = SqlHelper.ExecuteNonQuery(Misc.connStr, CommandType.StoredProcedure, "spDeleteComments", param);
        }
        catch (Exception ex)
        {
            return 0;
        }
        return result;
    }
    #endregion

    #region Function to Update Comments
    public static int updateComments(BLLComments obj)
    {
        int result = 0;
        try
        {
            SqlParameter[] param = new SqlParameter[6];
            param[0] = new SqlParameter("@commentId", obj.commentId);
            param[1] = new SqlParameter("@title", obj.title);
            param[2] = new SqlParameter("@comment", obj.comment);
            param[3] = new SqlParameter("@restaurantID", obj.restaurantID);
            param[4] = new SqlParameter("@ModifiedDate", obj.modifiedDate);
            param[5] = new SqlParameter("@modifiedBy", obj.modifiedBy);            
            result = SqlHelper.ExecuteNonQuery(Misc.connStr, CommandType.StoredProcedure, "spUpdateComments", param);
        }
        catch (Exception ex)
        {
            return 0;
        }
        return result;
    }
    #endregion

    #region Functions to get comments by RestaurantID
    public static DataTable getCommentsByRestaurantID(BLLComments obj)
    {
        DataTable dt = null;
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@restaurantID", obj.restaurantID);
            dt = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetAllCommentsByRestaurantID", param).Tables[0];
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
