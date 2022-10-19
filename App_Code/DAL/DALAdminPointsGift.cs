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
public static class DALAdminPointsGift
{
    #region Function to create new 
    public static long createAdminPointsGift(BLLAdminPointsGift obj)
    {
        long result = 0;
        try
        {
            SqlParameter[] param = new SqlParameter[6];
            param[0] = new SqlParameter("@pgName", obj.pgName);
            param[1] = new SqlParameter("@pgPoints", obj.pgPoints);
            param[2] = new SqlParameter("@pgImage", obj.pgImage);
            param[3] = new SqlParameter("@pgDescription", obj.pgDescription);            
            param[4] = new SqlParameter("@creationDate", obj.creationDate);
            param[5] = new SqlParameter("@createdBy", obj.createdBy);
            result = Convert.ToInt64(SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spCreateAdminPointsGift", param).Tables[0].Rows[0][0]);
        }
        catch (Exception ex)
        {
            return 0;
        }
        return result;
    }
    #endregion

    #region function to delete 
    public static int deleteAdminPointsGift(BLLAdminPointsGift obj)
    {
        int result = 0;
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@pgID", obj.pgID);
            result = SqlHelper.ExecuteNonQuery(Misc.connStr, CommandType.StoredProcedure, "spDeleteAdminPointsGift", param);
        }
        catch (Exception ex)
        {
            return 0;
        }
        return result;
    }
    #endregion

    #region Function to Update 
    public static int updateAdminPointsGift(BLLAdminPointsGift obj)
    {
        int result = 0;
        try
        {
            SqlParameter[] param = new SqlParameter[7];
            param[0] = new SqlParameter("@pgName", obj.pgName);
            param[1] = new SqlParameter("@pgPoints", obj.pgPoints);
            param[2] = new SqlParameter("@pgImage", obj.pgImage);
            param[3] = new SqlParameter("@pgDescription", obj.pgDescription);
            param[4] = new SqlParameter("@modifiedDate", obj.modifiedDate);
            param[5] = new SqlParameter("@modifiedBy", obj.modifiedBy);
            param[6] = new SqlParameter("@pgID", obj.pgID);
            result = SqlHelper.ExecuteNonQuery(Misc.connStr, CommandType.StoredProcedure, "spUpdateAdminPointsGift", param);
        }
        catch (Exception ex)
        {
            return 0;
        }
        return result;
    }
    #endregion

    #region Functions to get
    public static DataTable getAllAdminPointsGift()
    {
        DataTable dt = null;
        try
        {
            dt = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetAllAdminPointsGift").Tables[0];
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

    public static DataTable getAdminPointsGiftByID(BLLAdminPointsGift obj)
    {
        DataTable dt = null;
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@pgID", obj.pgID);
            dt = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetAdminPointsGiftByID", param).Tables[0];
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
