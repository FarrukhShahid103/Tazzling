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
/// Summary description for DALMemberDealItems
/// </summary>
public static class DALMemberDealItems
{

    #region Function to create
    public static int createMemberDealItems(BLLMemberDealItems obj)
    {
        int result = 0;
        try
        {
            SqlParameter[] param = new SqlParameter[6];
            param[0] = new SqlParameter("@dealId", obj.dealId);
            param[1] = new SqlParameter("@dealItemName", obj.dealItemName);
            param[2] = new SqlParameter("@dealItemSubname", obj.dealItemSubname);
            param[3] = new SqlParameter("@dealItemDescription", obj.dealItemDescription);            
            param[4] = new SqlParameter("@creationDate", obj.creationDate);
            param[5] = new SqlParameter("@createdBy", obj.createdBy);
            result = SqlHelper.ExecuteNonQuery(Misc.connStr, CommandType.StoredProcedure, "spCreateMemberDealItems", param);
        }
        catch (Exception ex)
        {
            return 0;
        }
        return result;
    }
    #endregion

    #region function to delete
    public static int deleteMemberDealItems(BLLMemberDealItems obj)
    {
        int result = 0;
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@dealItemId", obj.dealItemId);
            result = SqlHelper.ExecuteNonQuery(Misc.connStr, CommandType.StoredProcedure, "spDeleteMemberDealItems", param);
        }
        catch (Exception ex)
        {
            return 0;
        }
        return result;
    }
    #endregion

    #region Function to Update
    public static int updateMemberDealItems(BLLMemberDealItems obj)
    {
        int result = 0;
        try
        {
            SqlParameter[] param = new SqlParameter[6];
            param[0] = new SqlParameter("@dealItemId", obj.dealItemId);
            param[1] = new SqlParameter("@dealItemName", obj.dealItemName);
            param[2] = new SqlParameter("@dealItemSubname", obj.dealItemSubname);
            param[3] = new SqlParameter("@dealItemDescription", obj.dealItemDescription);    
            param[4] = new SqlParameter("@modifiedDate", obj.modifiedDate);
            param[5] = new SqlParameter("@modifiedBy", obj.modifiedBy);
            result = SqlHelper.ExecuteNonQuery(Misc.connStr, CommandType.StoredProcedure, "spUpdateMemberDealItems", param);
        }
        catch (Exception ex)
        {
            return 0;
        }
        return result;
    }
    #endregion

    #region Functions to get all
    public static DataTable getMemberDealItemsByDealID(BLLMemberDealItems obj)
    {
        DataTable dt = null;
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@dealId", obj.dealId);
            dt = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetMemberDealItemsByDealID", param).Tables[0];
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
    public static DataSet getMemberDealItemsByDealIDForExport(BLLMemberDealItems obj)
    {
        DataSet dst = null;
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@dealId", obj.dealId);
            dst = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetMemberDealItemsByDealIDForExport", param);
            if (dst != null && dst.Tables[0].Rows.Count > 0)
            {
                return dst;
            }
        }
        catch (Exception ex)
        {
            return null;
        }
        return dst;
    }
    #endregion
}
