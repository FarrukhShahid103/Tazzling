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
/// Summary description for DALGiftCard
/// </summary>
public static class DALGiftCard
{
    #region Function to create new gift card by User ID

    public static long createGiftCardByAdmin(BLLGiftCard obj)
    {
        int result = 0;
        try
        {
            SqlParameter[] param = new SqlParameter[8];
            param[0] = new SqlParameter("@comments", obj.comments);
            param[1] = new SqlParameter("@giftCardCode", obj.giftCardCode);
            param[2] = new SqlParameter("@giftCardAmount", obj.giftCardAmount);
            param[3] = new SqlParameter("@currencyCode", obj.currencyCode);
            param[4] = new SqlParameter("@expirationDate", obj.expirationDate);
            param[5] = new SqlParameter("@creationDate", obj.creationDate);
            param[6] = new SqlParameter("@createdBy", obj.createdBy);
            param[7] = new SqlParameter("@orderStatus", "APPROVED");
            result = SqlHelper.ExecuteNonQuery(Misc.connStr, CommandType.StoredProcedure, "spCreateGiftCardByAdmin", param);
        }
        catch (Exception ex)
        {
            return 0;
        }
        return result;
    }

    public static long createGiftCardOrder(BLLGiftCard obj)
    {
        int result = 0;
        try
        {
            SqlParameter[] param = new SqlParameter[8];
            param[0] = new SqlParameter("@comments", obj.comments);
            param[1] = new SqlParameter("@giftCardCode", obj.giftCardCode);
            param[2] = new SqlParameter("@giftCardAmount", obj.giftCardAmount);
            param[3] = new SqlParameter("@currencyCode", obj.currencyCode);
            param[4] = new SqlParameter("@expirationDate", obj.expirationDate);
            param[5] = new SqlParameter("@creationDate", obj.creationDate);
            param[6] = new SqlParameter("@createdBy", obj.createdBy);
            param[7] = new SqlParameter("@orderStatus", "APPROVED");
            result = SqlHelper.ExecuteNonQuery(Misc.connStr, CommandType.StoredProcedure, "spCreateGiftCardByAdmin", param);
        }
        catch (Exception ex)
        {
            return 0;
        }
        return result;
    }  

    public static int createGiftCardByUser(BLLGiftCard obj)
    {
        int result = 0;
        try
        {
            SqlParameter[] param = new SqlParameter[7];

            param[0] = new SqlParameter("@createdBy", obj.createdBy);
            param[1] = new SqlParameter("@giftCardCode", obj.giftCardCode);
            param[2] = new SqlParameter("@giftCardAmount", obj.giftCardAmount);
            param[3] = new SqlParameter("@currencyCode", obj.currencyCode);
            param[4] = new SqlParameter("@expirationDate", obj.expirationDate);
            param[5] = new SqlParameter("@creationDate", obj.creationDate);
            param[6] = new SqlParameter("@giftCardOrderId", obj.giftCardOrderId);
            
            result = SqlHelper.ExecuteNonQuery(Misc.connStr, CommandType.StoredProcedure, "spCreateGiftCardByUser", param);
        }
        catch (Exception ex)
        {
            return 0;
        }
        return result;
    }    
    #endregion

    #region Function to create new gift card

    public static int createGiftCard(BLLGiftCard obj)
    {
        int result = 0;
        try
        {
            SqlParameter[] param = new SqlParameter[6];

            param[0] = new SqlParameter("@creationDate", obj.creationDate);
            param[1] = new SqlParameter("@giftCardCode", obj.giftCardCode);
            param[2] = new SqlParameter("@giftCardAmount", obj.giftCardAmount);
            param[3] = new SqlParameter("@currencyCode", obj.currencyCode);
            param[4] = new SqlParameter("@expirationDate", obj.expirationDate);
            param[5] = new SqlParameter("@giftCardOrderId", obj.giftCardOrderId);

            result = SqlHelper.ExecuteNonQuery(Misc.connStr, CommandType.StoredProcedure, "spCreateGiftCard", param);
        }
        catch (Exception ex)
        {
            return 0;
        }
        return result;
    }

    #endregion

    #region function to delete admin gift card
    public static int deleteGiftCardByUser(BLLGiftCard obj)
    {
        int result = 0;
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@giftCardId", obj.giftCardId);
            result = SqlHelper.ExecuteNonQuery(Misc.connStr, CommandType.StoredProcedure, "spDeleteGiftCardByUser", param);
        }
        catch (Exception ex)
        {
            return 0;
        }
        return result;
    }
    #endregion

    #region Function to Update GiftCardByUser
    public static int updateGiftCardByUser(BLLGiftCard obj)
    {
        int result = 0;
        try
        {
            SqlParameter[] param = new SqlParameter[10];
            param[0] = new SqlParameter("@giftCardId", obj.giftCardId);
            param[1] = new SqlParameter("@giftCardStyle", obj.giftCardStyle);
            param[2] = new SqlParameter("@comments", obj.comments);
            param[3] = new SqlParameter("@giftCardCode", obj.giftCardCode);
            param[4] = new SqlParameter("@giftCardAmount", obj.giftCardAmount);
            param[5] = new SqlParameter("@currencyCode", obj.currencyCode);            
            param[6] = new SqlParameter("@takenBy", obj.takenBy);
            param[7] = new SqlParameter("@expirationDate", obj.expirationDate);
            param[8] = new SqlParameter("@modifiedDate", obj.modifiedDate);
            param[9] = new SqlParameter("@modifiedBy", obj.modifiedBy);             		  		  		 
            result = SqlHelper.ExecuteNonQuery(Misc.connStr, CommandType.StoredProcedure, "spUpdateGiftCardByUser", param);
        }
        catch (Exception ex)
        {
            return 0;
        }
        return result;
    }
    #endregion

    #region Function to Update GiftCard Taken By Value
    public static int updateGiftCardTakenByValue(BLLGiftCard obj)
    {
        int result = 0;
        try
        {
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@giftCardCode", obj.giftCardCode);
            param[1] = new SqlParameter("@takenBy", obj.takenBy);            
            result = SqlHelper.ExecuteNonQuery(Misc.connStr, CommandType.StoredProcedure, "spUpdateGiftCardTakenByValue", param);
        }
        catch (Exception ex)
        {
            return 0;
        }
        return result;
    }
    #endregion
    

    #region Function to Update GiftCard By Code And UserID
    public static int updateGiftCardByCodeAndUserID(BLLGiftCard obj)
    {
        int result = 0;
        try
        {
            SqlParameter[] param = new SqlParameter[4];            
            param[0] = new SqlParameter("@giftCardCode", obj.giftCardCode);            
            param[1] = new SqlParameter("@takenBy", obj.takenBy);            
            param[2] = new SqlParameter("@modifiedDate", obj.modifiedDate);
            param[3] = new SqlParameter("@modifiedBy", obj.modifiedBy);
            result = SqlHelper.ExecuteNonQuery(Misc.connStr, CommandType.StoredProcedure, "spUpdateGiftCardByCodeAndUserID", param);
        }
        catch (Exception ex)
        {
            return 0;
        }
        return result;
    }
    #endregion

    #region Function to Update GiftCard By Order Reference # Sent
    public static int updateGiftCardByOrderRefNoSent(BLLGiftCard obj)
    {
        int result = 0;
        try
        {
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@OrderRefNoSent", obj.orderRefNoSent);
            param[1] = new SqlParameter("@OrderIdReceived", obj.orderIdReceived);
            param[2] = new SqlParameter("@OrderStatus", obj.orderStatus);
            result = SqlHelper.ExecuteNonQuery(Misc.connStr, CommandType.StoredProcedure, "spUpdateGiftCardByOrderRefNoSent", param);
        }
        catch (Exception ex)
        {
            return 0;
        }
        return result;
    }
    #endregion

    #region Functions to get gift card by code
    public static DataTable getGiftCardByCode(BLLGiftCard obj)
    {
        DataTable dt = null;
        try
        {

            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@giftCardCode", obj.giftCardCode);
            dt = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetGiftCardByCode",param).Tables[0];
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

    #region Functions to get gift card by code
    public static DataTable getApprovedGiftCardByCode(BLLGiftCard obj)
    {
        DataTable dt = null;
        try
        {

            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@giftCardCode", obj.giftCardCode);
            dt = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetApprovedGiftCardByCode", param).Tables[0];
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

    #region Functions to get gift card by UserID
    public static DataTable getGiftCardByUserID(BLLGiftCard obj)
    {
        DataTable dt = null;
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@createdBy", obj.createdBy);
            dt = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetGiftCardByUserID",param).Tables[0];
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

    #region Functions to get gift card info by Order Ref No Sent

    public static DataTable getGiftCardInfoByOrderRefNoSent(BLLGiftCard obj)
    {
        DataTable dtGiftCardInfo = null;
        
        try
        {
            SqlParameter[] param = new SqlParameter[1];

            param[0] = new SqlParameter("@OrderRefNoSent", obj.orderRefNoSent);
            
            dtGiftCardInfo = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetGiftCardInfoByOrderRefNoSent", param).Tables[0];
            
            if (dtGiftCardInfo != null && dtGiftCardInfo.Rows.Count > 0)
            {
                return dtGiftCardInfo;
            }
        }
        catch (Exception ex)
        {
            return null;
        }

        return dtGiftCardInfo;
    }

    public static int updateGiftCardSetUnApproved(BLLGiftCard obj)
    {
        int result=0;

        try
        {
            SqlParameter[] param = new SqlParameter[1];

            param[0] = new SqlParameter("@giftCardId", obj.giftCardId);

            result = SqlHelper.ExecuteNonQuery(Misc.connStr, CommandType.StoredProcedure, "spUpdateGiftCardSetUnApproved", param);
          
        }
        catch (Exception ex)
        {
            return 0;
        }

        return result;
    }


    public static DataTable getAllApprovedGiftCards()
    {
        DataTable dtGiftCardInfo = null;

        try
        {            
            dtGiftCardInfo = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetAllApprovedGiftCards").Tables[0];            
        }
        catch (Exception ex)
        {
            return null;
        }

        return dtGiftCardInfo;
    }

    public static DataTable getGiftCardByID(BLLGiftCard obj)
    {
        DataTable dtGiftCardInfo = null;
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@giftCardId", obj.giftCardId);
            dtGiftCardInfo = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetGiftCardByID", param).Tables[0];
        }
        catch (Exception ex)
        {
            return dtGiftCardInfo;
        }
        return dtGiftCardInfo;
    }

    


    #endregion

    public static int createGiftCardAssignedByAdmin(BLLGiftCard obj)
    {
        int result = 0;
        try
        {
            SqlParameter[] param = new SqlParameter[9];
            param[0] = new SqlParameter("@comments", obj.comments);
            param[1] = new SqlParameter("@giftCardCode", obj.giftCardCode);
            param[2] = new SqlParameter("@giftCardAmount", obj.giftCardAmount);
            param[3] = new SqlParameter("@currencyCode", obj.currencyCode);
            param[4] = new SqlParameter("@expirationDate", obj.expirationDate);
            param[5] = new SqlParameter("@creationDate", obj.creationDate);
            param[6] = new SqlParameter("@createdBy", obj.createdBy);
            param[7] = new SqlParameter("@takenBy", obj.takenBy);
            param[8] = new SqlParameter("@orderStatus", "APPROVED");
            result = SqlHelper.ExecuteNonQuery(Misc.connStr, CommandType.StoredProcedure, "spCreateGiftCardAssignedByAdmin", param);
        }
        catch (Exception ex)
        {
            return 0;
        }
        return result;
    }    
}
