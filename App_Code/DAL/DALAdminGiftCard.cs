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
/// Summary description for DALAdminGiftCard
/// </summary>
public static class DALAdminGiftCard
{
    #region Function to create new admin gift card
    public static int createAdminGiftCard(BLLAdminGiftCard obj)
    {
        int result = 0;
        try
        {
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@cardAmount", obj.cardAmount);
            param[1] = new SqlParameter("@cardImage", obj.cardImage);
            param[2] = new SqlParameter("@createdBy", obj.createdBy);
            param[3] = new SqlParameter("@creationDate", obj.creationDate);
            result = SqlHelper.ExecuteNonQuery(Misc.connStr, CommandType.StoredProcedure, "spCreateAdminGiftCard", param);
        }
        catch (Exception ex)
        {
            return 0;
        }
        return result;
    }
    #endregion

    #region function to delete admin gift card
    public static int deleteAdminGiftCard(BLLAdminGiftCard obj)
    {
        int result = 0;
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@cardID", obj.cardID);            
            result = SqlHelper.ExecuteNonQuery(Misc.connStr, CommandType.StoredProcedure, "spDeleteAdminGiftCard", param);
        }
        catch (Exception ex)
        {
            return 0;
        }
        return result;
    }
    #endregion

    #region Function to Update 
    public static int updateAdminGiftCard(BLLAdminGiftCard obj)
    {
        int result = 0;
        try
        {
            SqlParameter[] param = new SqlParameter[5];
            param[0] = new SqlParameter("@cardAmount", obj.cardAmount);
            param[1] = new SqlParameter("@cardImage", obj.cardImage);
            param[2] = new SqlParameter("@modifiedBy", obj.modifiedBy);
            param[3] = new SqlParameter("@modifiedDate", obj.modifiedDate);
            param[4] = new SqlParameter("@cardID", obj.cardID);
            result = SqlHelper.ExecuteNonQuery(Misc.connStr, CommandType.StoredProcedure, "spUpdateAdminGiftCard", param);
        }
        catch (Exception ex)
        {
            return 0;
        }
        return result;
    }
    #endregion

    #region Functions to get all admin cards
    public static DataTable getAllAdminGiftCard()
    {
       DataTable dt = null;
        try 
        {            
            dt = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetAllAdminGiftCard").Tables[0];
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

    #region function to delete admin gift card
    public static DataTable getAdminGiftCardByID(BLLAdminGiftCard obj)
    {
        DataTable dt = null;
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@cardID", obj.cardID);
            dt = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetAdminGiftCardByID", param).Tables[0];
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

    #region function to delete admin gift card
    public static DataTable getAdminGiftCardByPrice(BLLAdminGiftCard obj)
    {
        DataTable dt = null;
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@cardAmount", obj.cardAmount);
            dt = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetAdminGiftCardByPrice", param).Tables[0];
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
