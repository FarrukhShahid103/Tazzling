using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.SqlClient;
using SQLHelper;

/// <summary>
/// Summary description for DALAffiliatePartnerGained
/// </summary>
public class DALAffiliatePartnerGained
{
	public DALAffiliatePartnerGained()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    #region Function to Get Affiliate Partner Gained Credits By UserID

    public static DataTable getGetAffiliatePartnerGainedCreditsByUserID(BLLAffiliatePartnerGained obj)
    {
        DataTable dtMemberGiftCard = null;
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@UserId", obj.UserId);
            dtMemberGiftCard = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetAffiliatePartnerGainedCreditsByUserID", param).Tables[0];
        }
        catch (Exception ex)
        {
            return dtMemberGiftCard;
        }
        return dtMemberGiftCard;
    }

    public static DataTable getGetAffiliatePartnerTotalEarnedByUserID(BLLAffiliatePartnerGained obj)
    {
        DataTable dtMemberGiftCard = null;
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@UserId", obj.UserId);
            dtMemberGiftCard = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetAffiliatePartnerTotalEarnedByUserID", param).Tables[0];
        }
        catch (Exception ex)
        {
            return dtMemberGiftCard;
        }
        return dtMemberGiftCard;
    }

    #endregion

    #region Function to Get Affiliate Gained Credits By UserID

    public static DataTable getGetAffiliateGainedCreditsByUserID(BLLAffiliatePartnerGained obj, DateTime dtStartDate, DateTime dtEndDate)
    {
        DataTable dtMemberGiftCard = null;

        try
        {
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@userId", obj.UserId);
            param[1] = new SqlParameter("@CreationStartDate", dtStartDate);
            param[2] = new SqlParameter("@CreationEndDate", dtEndDate);
            dtMemberGiftCard = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetAffiliateGainedCreditsByUserID", param).Tables[0];
        }
        catch (Exception ex)
        {
            return dtMemberGiftCard;
        }
        return dtMemberGiftCard;
    }

    #endregion

    #region Function to create new useable Affiliate Partner Credit

    public static int createAffiliatePartnerGained(BLLAffiliatePartnerGained obj)
    {
        int result = 0;
        try
        {
            SqlParameter[] param = new SqlParameter[9];
            param[0] = new SqlParameter("@userId", obj.UserId);
            param[1] = new SqlParameter("@gainedType", obj.GainedType);
            param[2] = new SqlParameter("@gainedAmount", obj.GainedAmount);
            param[3] = new SqlParameter("@remainAmount", obj.RemainAmount);
            param[4] = new SqlParameter("@fromId", obj.FromId);
            param[5] = new SqlParameter("@createdDate", obj.CreatedDate);
            param[6] = new SqlParameter("@createdBy", obj.CreatedBy);
            param[7] = new SqlParameter("@orderId", obj.OrderId);
            param[8] = new SqlParameter("@AffCommPer", obj.AffCommPer);
            result = SqlHelper.ExecuteNonQuery(Misc.connStr, CommandType.StoredProcedure, "spCreateAffiliatePartnerGained", param);
        }
        catch (Exception ex)
        {
            return 0;
        }
        return result;
    }

    #endregion

    public static DataTable getGetAllAffiliatePartnerGainedByUserID(BLLAffiliatePartnerGained obj)
    {
        DataTable dtMemberGiftCard = null;
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@userId", obj.UserId);
            dtMemberGiftCard = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetAllAffiliatePartnerGainedByUserID", param).Tables[0];
        }
        catch (Exception ex)
        {
            return dtMemberGiftCard;
        }
        return dtMemberGiftCard;
    }

    public static int updateAffiliateRemainingUsableAmount(BLLAffiliatePartnerGained obj)
    {
        int result = 0;
        try
        {
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@affiliatePartnerId", obj.AffiliatePartnerId);
            param[1] = new SqlParameter("@remainAmount", obj.RemainAmount);
            param[2] = new SqlParameter("@modifiedBy", obj.ModifiedBy);
            param[3] = new SqlParameter("@modifiedDate", obj.ModifiedDate);

            result = SqlHelper.ExecuteNonQuery(Misc.connStr, CommandType.StoredProcedure, "spUpdateAffiliateRemainingUsableAmount", param);
        }
        catch (Exception ex)
        {
            return result;
        }

        return result;
    }

    public static int updateAffiliateCommisionByOrderId(BLLAffiliatePartnerGained obj)
    {
        int result = 0;

        try
        {
            SqlParameter[] param = new SqlParameter[6];

            param[0] = new SqlParameter("@gainedType", obj.GainedType);
            param[1] = new SqlParameter("@gainedAmount", obj.GainedAmount);
            param[2] = new SqlParameter("@remainAmount", obj.RemainAmount);
            param[3] = new SqlParameter("@modifiedDate", obj.ModifiedDate);
            param[4] = new SqlParameter("@modifiedBy", obj.ModifiedBy);
            param[5] = new SqlParameter("@orderId", obj.OrderId);

            result = SqlHelper.ExecuteNonQuery(Misc.connStr, CommandType.StoredProcedure, "spUpdateAffiliatePartnerGainedByOrderId", param);
        }
        catch (Exception ex)
        {
            return result;
        }

        return result;
    }

    public static DataTable getGetAffiliatePartnerCommisionInfoByOrderID(BLLAffiliatePartnerGained obj)
    {
        DataTable dtMemberGiftCard = null;
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@orderId", obj.OrderId);
            dtMemberGiftCard = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetAffiliatePartnerCommisionInfoByOrderID", param).Tables[0];
        }
        catch (Exception ex)
        {
            return dtMemberGiftCard;
        }
        return dtMemberGiftCard;
    }
}