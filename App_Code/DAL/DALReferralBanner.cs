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
/// Summary description for DALReferralBanner
/// </summary>
public static class DALReferralBanner
{
    #region Function to create
    public static int createReferralBanner(BLLReferralBanner obj)
    {
        int result = 0;
        try
        {
            SqlParameter[] param = new SqlParameter[6];
            param[0] = new SqlParameter("@bannerTitle", obj.bannerTitle);
            param[1] = new SqlParameter("@bannerWidth", obj.bannerWidth);
            param[2] = new SqlParameter("@bannerHeight", obj.bannerHeight);
            param[3] = new SqlParameter("@imageFile", obj.imageFile);
            param[4] = new SqlParameter("@createdBy", obj.createdBy);
            param[5] = new SqlParameter("@creationDate", obj.creationDate);
            result = SqlHelper.ExecuteNonQuery(Misc.connStr, CommandType.StoredProcedure, "spCreateReferralBanner", param);
        }
        catch (Exception ex)
        {
            return 0;
        }
        return result;
    }
    #endregion

    #region function to delete
    public static int deleteReferralBanner(BLLReferralBanner obj)
    {
        int result = 0;
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@bannerId", obj.bannerId);
            result = SqlHelper.ExecuteNonQuery(Misc.connStr, CommandType.StoredProcedure, "spDeleteReferralBanner", param);
        }
        catch (Exception ex)
        {
            return 0;
        }
        return result;
    }
    #endregion

    #region Function to Update
    public static int updateReferralBanner(BLLReferralBanner obj)
    {
        int result = 0;
        try
        {
            SqlParameter[] param = new SqlParameter[7];
            param[0] = new SqlParameter("@bannerId", obj.bannerId);
            param[1] = new SqlParameter("@bannerTitle", obj.bannerTitle);
            param[2] = new SqlParameter("@bannerWidth", obj.bannerWidth);
            param[3] = new SqlParameter("@bannerHeight", obj.bannerHeight);
            param[4] = new SqlParameter("@imageFile", obj.imageFile);
            param[5] = new SqlParameter("@modifiedBy", obj.modifiedBy);
            param[6] = new SqlParameter("@modifiedDate", obj.modifiedDate);
            result = SqlHelper.ExecuteNonQuery(Misc.connStr, CommandType.StoredProcedure, "spUpdateReferralBanner", param);
        }
        catch (Exception ex)
        {
            return 0;
        }
        return result;
    }
    #endregion

    #region Functions to get All Cuisine
    public static DataTable getAllReferralBanner()
    {
        DataTable dt = null;
        try
        {
            dt = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetAllReferralBanner").Tables[0];
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

    #region function Get Banner info By ID
    public static DataTable getReferralBannerByID(BLLReferralBanner obj)
    {
        DataTable dt = null;
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@bannerId", obj.bannerId);
            dt = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetReferralBannerByID", param).Tables[0];
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
