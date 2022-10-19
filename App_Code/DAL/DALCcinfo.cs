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
public static class DALCcinfo
{
    #region Function to create new
    public static long createRCcinfo(BLLCcinfo obj)
    {
        long result = 0;
        try
        {
            SqlParameter[] param = new SqlParameter[11];
            param[0] = new SqlParameter("@userId", obj.userId);
            param[1] = new SqlParameter("@rcci_ccvn", obj.rcci_ccvn);
            param[2] = new SqlParameter("@rccied", obj.rccied);
            param[3] = new SqlParameter("@rccin", obj.rccin);
            param[4] = new SqlParameter("@rccit", obj.rccit);
            param[5] = new SqlParameter("@rcciun", obj.rcciun);            
            param[6] = new SqlParameter("@creationDate", obj.creationDate);
            param[7] = new SqlParameter("@createdBy", obj.createdBy);
            param[8] = new SqlParameter("@package", obj.package);
            param[9] = new SqlParameter("@fee", obj.fee);
            param[10] = new SqlParameter("@cycle", obj.cycle); 
           
            result = Convert.ToInt64(SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spCreateRCcinfo", param).Tables[0].Rows[0][0]);
        }
        catch (Exception ex)
        {
            return 0;
        }
        return result;
    }
    #endregion

    #region function to delete
    public static int deleteRCcinfo(BLLCcinfo obj)
    {
        int result = 0;
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@rcciID", obj.rcciID);
            result = SqlHelper.ExecuteNonQuery(Misc.connStr, CommandType.StoredProcedure, "spDeleteRCcinfo", param);
        }
        catch (Exception ex)
        {
            return 0;
        }
        return result;
    }
    #endregion

    #region Function to Update
    public static int updateRCcInfo(BLLCcinfo obj)
    {
        int result = 0;
        try
        {
            SqlParameter[] param = new SqlParameter[11];
            param[0] = new SqlParameter("@modifiedBy", obj.modifiedBy);
            param[1] = new SqlParameter("@rcci_ccvn", obj.rcci_ccvn);
            param[2] = new SqlParameter("@rccied", obj.rccied);
            param[3] = new SqlParameter("@rccin", obj.rccin);
            param[4] = new SqlParameter("@rccit", obj.rccit);
            param[5] = new SqlParameter("@rcciun", obj.rcciun);
            param[6] = new SqlParameter("@userId", obj.userId);            
            param[7] = new SqlParameter("@ModifiedDate", obj.modifiedDate);
            param[8] = new SqlParameter("@package", obj.package);
            param[9] = new SqlParameter("@fee", obj.fee);
            param[10] = new SqlParameter("@cycle", obj.cycle); 
            result = SqlHelper.ExecuteNonQuery(Misc.connStr, CommandType.StoredProcedure, "spUpdateRCcInfo", param);
        }
        catch (Exception ex)
        {
            return 0;
        }
        return result;
    }
    #endregion

    #region Functions to get comments by User ID
    public static DataTable getRCcinfoByUserID(BLLCcinfo obj)
    {
        DataTable dt = null;
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@userId", obj.userId);
            dt = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetRCcinfoByUserID", param).Tables[0];
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
