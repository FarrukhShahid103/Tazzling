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
/// Summary description for DALMemberUsedGiftCards
/// </summary>
public class DALMemberUsedGiftCards
{
    #region Function to create new useable gift card
    public static int createMemberUseableGiftCard(BLLMemberUsedGiftCards obj)
    {
        int result = 0;
        try
        {
            SqlParameter[] param = new SqlParameter[9];
            param[0] = new SqlParameter("@targetDate", obj.targetDate);
            param[1] = new SqlParameter("@gainedType", obj.gainedType);
            param[2] = new SqlParameter("@gainedAmount", obj.gainedAmount);
            param[3] = new SqlParameter("@remainAmount", obj.remainAmount);
            param[4] = new SqlParameter("@fromId", obj.fromId);
            param[5] = new SqlParameter("@currencyCode", obj.currencyCode);
            param[6] = new SqlParameter("@creationDate", obj.creationDate);
            param[7] = new SqlParameter("@createdBy", obj.createdBy);
            param[8] = new SqlParameter("@orderId", obj.orderId);   
            result = SqlHelper.ExecuteNonQuery(Misc.connStr, CommandType.StoredProcedure, "spCreateMemberUseableGiftCard", param);
        }
        catch (Exception ex)
        {
            return 0;
        }
        return result;
    }
    #endregion

    #region Function to create new useable gift card
    public static DataTable getAllUseableCardsByUserID(BLLMemberUsedGiftCards obj)
    {
        DataTable dtMemberGiftCard = null;
        try
        {
            SqlParameter[] param = new SqlParameter[2];            
            param[0] = new SqlParameter("@createdBy", obj.createdBy);
            param[1] = new SqlParameter("@targetDate", obj.targetDate);
            dtMemberGiftCard = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetAllUseableCardsByUserID", param).Tables[0];
        }
        catch (Exception ex)
        {
            return dtMemberGiftCard;
        }
        return dtMemberGiftCard;
    }
    #endregion

    #region Function to get Tasty Refferal Credits 

    public static DataTable getAllRefferalTastyCreditsByUserID(BLLMemberUsedGiftCards obj)
    {
        DataTable dtMemberGiftCard = null;
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@createdBy", obj.createdBy);
            dtMemberGiftCard = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetAllRefferalCReditsByUserID", param).Tables[0];
        }
        catch (Exception ex)
        {
            return dtMemberGiftCard;
        }
        return dtMemberGiftCard;
    }


    public static DataTable getTastyCreditsByOrderID(BLLMemberUsedGiftCards obj)
    {
        DataTable dtMemberGiftCard = null;
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@orderId", obj.orderId);
            dtMemberGiftCard = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetTastyCreditsByOrderID", param).Tables[0];
        }
        catch (Exception ex)
        {
            return dtMemberGiftCard;
        }
        return dtMemberGiftCard;
    }

    #endregion

    #region Function to create new useable gift card credit

    public static DataTable getUseableFoodCreditByUserID(BLLMemberUsedGiftCards obj)
    {
        DataTable dtMemberGiftCard = null;
        try
        {
            SqlParameter[] param = new SqlParameter[2];            
            param[0] = new SqlParameter("@createdBy", obj.createdBy);
            param[1] = new SqlParameter("@targetDate", obj.targetDate);
            dtMemberGiftCard = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetUseableFoodCreditByUserID", param).Tables[0];
        }
        catch (Exception ex)
        {
            return dtMemberGiftCard;
        }
        return dtMemberGiftCard;
    }
    
    #endregion

    #region Function to create new useable gift card credit of Refferal

    public static DataTable getUseableFoodCreditRefferalByUserID(BLLMemberUsedGiftCards obj)
    {
        DataTable dtMemberGiftCard = null;
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@createdBy", obj.createdBy);
            dtMemberGiftCard = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetUseableFoodCreditRefferalByUserID", param).Tables[0];
        }
        catch (Exception ex)
        {
            return dtMemberGiftCard;
        }
        return dtMemberGiftCard;
    }

    #endregion

    public static int updateRemainingUsableAmount(BLLMemberUsedGiftCards obj)
    {
        int result = 0;
        try
        {
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@gainedId", obj.gainedId);
            param[1] = new SqlParameter("@remainAmount", obj.remainAmount);
            param[2] = new SqlParameter("@modifiedBy", obj.modifiedBy);
            param[3] = new SqlParameter("@modifiedDate", obj.modifiedDate);

            result = SqlHelper.ExecuteNonQuery(Misc.connStr, CommandType.StoredProcedure, "spUpdateRemainingUsableAmount", param);
        }
        catch (Exception ex)
        {
            return result;
        }
        return result;
    }

    public static int updateCreditOnDecByOrderId(BLLMemberUsedGiftCards obj)
    {
        int result = 0;
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@OrderId", obj.orderId);

            result = SqlHelper.ExecuteNonQuery(Misc.connStr, CommandType.StoredProcedure, "spUpdateMemberCreditDeclinedByOrderId", param);
        }
        catch (Exception ex)
        {
            return result;
        }
        return result;
    }



    public static int updateCreditByOrderId(BLLMemberUsedGiftCards obj)
    {
        int result = 0;
        try
        {
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@OrderId", obj.orderId);
            param[1] = new SqlParameter("@remainAmount", obj.remainAmount);
            result = SqlHelper.ExecuteNonQuery(Misc.connStr, CommandType.StoredProcedure, "spUpdateCreditByOrderId", param);
        }
        catch (Exception ex)
        {
            return result;
        }
        return result;
    }



}
