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
/// Summary description for DALUserCCInfo
/// </summary>
public static class DALUserCCInfo
{
    #region Function to create new 
    public static long createUserCCInfo(BLLUserCCInfo obj)
    {
        long result = 0;
        try
        {
            SqlParameter[] param = new SqlParameter[15];
            param[0] = new SqlParameter("@ccInfoBAddress", obj.ccInfoBAddress);
            param[1] = new SqlParameter("@ccInfoBCity", obj.ccInfoBCity);
            param[2] = new SqlParameter("@ccInfoBName", obj.ccInfoBName);
            param[3] = new SqlParameter("@ccInfoBPostalCode", obj.ccInfoBPostalCode);
            param[4] = new SqlParameter("@ccInfoBProvince", obj.ccInfoBProvince);
            param[5] = new SqlParameter("@ccInfoCCVNumber", obj.ccInfoCCVNumber);
            param[6] = new SqlParameter("@ccInfoDEmail", obj.ccInfoDEmail);
            param[7] = new SqlParameter("@ccInfoDFirstName", obj.ccInfoDFirstName);
            param[8] = new SqlParameter("@ccInfoDLastName", obj.ccInfoDLastName);
            param[9] = new SqlParameter("@ccInfoEdate", obj.ccInfoEdate);
            param[10] = new SqlParameter("@ccInfoNumber", obj.ccInfoNumber);
            param[11] = new SqlParameter("@userId", obj.userId);
            param[12] = new SqlParameter("@creationDate", obj.creationDate);
            param[13] = new SqlParameter("@createdBy", obj.createdBy);
            param[14] = new SqlParameter("@ccInfoBAddress2", obj.ccInfoBAddress2);
            result = Convert.ToInt64(SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spCreateUserCCInfo", param).Tables[0].Rows[0][0]);
        }
        catch (Exception ex)
        {
            return 0;
        }
        return result;
    }
    #endregion

    #region function to delete 
    public static int deleteUserCCInfo(BLLUserCCInfo obj)
    {
        int result = 0;
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@ccInfoID", obj.ccInfoID);
            result = SqlHelper.ExecuteNonQuery(Misc.connStr, CommandType.StoredProcedure, "spDeleteUserCCInfo", param);
        }
        catch (Exception ex)
        {
            return 0;
        }
        return result;
    }
    #endregion

    #region Function to Update 
    public static int updateUserCCInfoByID(BLLUserCCInfo obj)
    {
        int result = 0;
        try
        {
            SqlParameter[] param = new SqlParameter[16];
            param[0] = new SqlParameter("@ccInfoBAddress", obj.ccInfoBAddress);
            param[1] = new SqlParameter("@ccInfoBCity", obj.ccInfoBCity);
            param[2] = new SqlParameter("@ccInfoBName", obj.ccInfoBName);
            param[3] = new SqlParameter("@ccInfoBPostalCode", obj.ccInfoBPostalCode);
            param[4] = new SqlParameter("@ccInfoBProvince", obj.ccInfoBProvince);
            param[5] = new SqlParameter("@ccInfoCCVNumber", obj.ccInfoCCVNumber);
            param[6] = new SqlParameter("@ccInfoDEmail", obj.ccInfoDEmail);
            param[7] = new SqlParameter("@ccInfoDFirstName", obj.ccInfoDFirstName);
            param[8] = new SqlParameter("@ccInfoDLastName", obj.ccInfoDLastName);
            param[9] = new SqlParameter("@ccInfoEdate", obj.ccInfoEdate);
            param[10] = new SqlParameter("@ccInfoNumber", obj.ccInfoNumber);
            param[11] = new SqlParameter("@userId", obj.userId);            
            param[12] = new SqlParameter("@ModifiedDate", obj.modifiedDate);
            param[13] = new SqlParameter("@modifiedBy", obj.modifiedBy);
            param[14] = new SqlParameter("@ccInfoID", obj.ccInfoID);
            param[15] = new SqlParameter("@ccInfoBAddress2", obj.ccInfoBAddress2);
            result = SqlHelper.ExecuteNonQuery(Misc.connStr, CommandType.StoredProcedure, "spUpdateUserCCInfoByID", param);
        }
        catch (Exception ex)
        {
            return 0;
        }
        return result;
    }
    #endregion

    #region Functions to get CCInfo by ID
    public static DataTable getUserCCInfoByID(BLLUserCCInfo obj)
    {
        DataTable dt = null;
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@ccInfoID", obj.ccInfoID);
            dt = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetUserCCInfoByID", param).Tables[0];
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

    public static DataTable getUserCCInfoByUserID(BLLUserCCInfo obj)
    {
        DataTable dt = null;
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@userId", obj.userId);
            dt = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetUserCCInfoByUserID", param).Tables[0];
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
