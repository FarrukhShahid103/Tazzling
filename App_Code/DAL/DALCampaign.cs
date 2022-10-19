using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SQLHelper;
using System.Data;
using System.Data.SqlClient;
/// <summary>
/// Summary CampaignDescription for DALCities
/// </summary>
public static class DALCampaign
{
    public static int createCampaign(BLLCampaign obj)
    {
        int result = 0;
        try 
        {
            SqlParameter[] param = new SqlParameter[23];
            param[0] = new SqlParameter("@campaignCategory", obj.campaignCategory);
            param[1] = new SqlParameter("@campaignDescription", obj.campaignDescription);
            param[2] = new SqlParameter("@campaignEndTime", obj.campaignEndTime);
            param[3] = new SqlParameter("@campaignpicture", obj.campaignpicture);
            param[4] = new SqlParameter("@campaignQuote", obj.campaignQuote);
            param[5] = new SqlParameter("@campaignSlot", obj.campaignSlot);
            param[6] = new SqlParameter("@campaignStartTime", obj.campaignStartTime);
            param[7] = new SqlParameter("@campaignTitle", obj.campaignTitle);
            param[8] = new SqlParameter("@createdBy", obj.createdBy);
            param[9] = new SqlParameter("@creationDate", obj.creationDate);
            param[10] = new SqlParameter("@estimatedArivalTime", obj.estimatedArivalTime);
            param[11] = new SqlParameter("@isActive", obj.isActive);
            param[12] = new SqlParameter("@restaurantId", obj.restaurantId);
            param[13] = new SqlParameter("@shipCanada", obj.shipCanada);
            param[14] = new SqlParameter("@shippingFromAddress", obj.shippingFromAddress);
            param[15] = new SqlParameter("@shipUSA", obj.shipUSA);
            param[16] = new SqlParameter("@campaignURL", obj.campaignURL);
            param[17] = new SqlParameter("@shippingFromCity", obj.shippingFromCity);
            param[18] = new SqlParameter("@shippingFromCountry", obj.shippingFromCountry);
            param[19] = new SqlParameter("@shippingFromprovince", obj.shippingFromprovince);
            param[20] = new SqlParameter("@shippingFromZipCode", obj.shippingFromZipCode);
            param[21] = new SqlParameter("@campaignShortDescription", obj.campaignShortDescription);
            param[22] = new SqlParameter("@isFeatured", obj.isFeatured);
                            
            result = SqlHelper.ExecuteNonQuery(Misc.connStr, CommandType.StoredProcedure, "spCreateCampaign", param);
        }
        catch (Exception ex)
        { 
        
        }
        return result;
    }

    public static int updateCampaign(BLLCampaign obj)
    {
        int result = 0;
        try
        {
            SqlParameter[] param = new SqlParameter[24];
            param[0] = new SqlParameter("@campaignCategory", obj.campaignCategory);
            param[1] = new SqlParameter("@campaignDescription", obj.campaignDescription);
            param[2] = new SqlParameter("@campaignEndTime", obj.campaignEndTime);
            param[3] = new SqlParameter("@campaignpicture", obj.campaignpicture);
            param[4] = new SqlParameter("@campaignQuote", obj.campaignQuote);
            param[5] = new SqlParameter("@campaignSlot", obj.campaignSlot);
            param[6] = new SqlParameter("@campaignStartTime", obj.campaignStartTime);
            param[7] = new SqlParameter("@campaignTitle", obj.campaignTitle);
            param[8] = new SqlParameter("@modifiedBy", obj.modifiedBy);
            param[9] = new SqlParameter("@modifiedDate", obj.modifiedDate);
            param[10] = new SqlParameter("@estimatedArivalTime", obj.estimatedArivalTime);
            param[11] = new SqlParameter("@isActive", obj.isActive);
            param[12] = new SqlParameter("@restaurantId", obj.restaurantId);
            param[13] = new SqlParameter("@shipCanada", obj.shipCanada);
            param[14] = new SqlParameter("@shippingFromAddress", obj.shippingFromAddress);
            param[15] = new SqlParameter("@shipUSA", obj.shipUSA);
            param[16] = new SqlParameter("@campaignID", obj.campaignID);
            param[17] = new SqlParameter("@campaignURL", obj.campaignURL);
            param[18] = new SqlParameter("@campaignShortDescription", obj.campaignShortDescription);
            param[19] = new SqlParameter("@shippingFromCity", obj.shippingFromCity);
            param[20] = new SqlParameter("@shippingFromCountry", obj.shippingFromCountry);
            param[21] = new SqlParameter("@shippingFromprovince", obj.shippingFromprovince);
            param[22] = new SqlParameter("@shippingFromZipCode", obj.shippingFromZipCode);
            param[23] = new SqlParameter("@isFeatured", obj.isFeatured);
            result = SqlHelper.ExecuteNonQuery(Misc.connStr, CommandType.StoredProcedure, "spUpdateCampaign", param);
        }
        catch (Exception ex)
        {

        }
        return result;
    }

    public static DataTable getCampaignByCampaignId(BLLCampaign obj)
    {
        DataTable dt = null;
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@campaignID", obj.campaignID);
            dt = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetCampaignByCampaignId", param).Tables[0];
        }
        catch (Exception ex)
        {

        }
        return dt;
    }

    public static DataTable getCampaignByBusinessID(BLLCampaign obj)
    {
        DataTable dt = null;
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@restaurantId", obj.restaurantId);
            dt = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetCampaignByBusinessID", param).Tables[0];
        }
        catch (Exception ex)
        {

        }
        return dt;
    }

    public static DataTable getFeaturedCampaignByGivenDate(BLLCampaign obj)
    {
        DataTable dt = null;
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@campaignTime", obj.creationDate);
            dt = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetFeaturedCampaignByGivenDate", param).Tables[0];
        }
        catch (Exception ex)
        {

        }
        return dt;
    }
    
    public static DataTable getCurrentCampaignByGivenDate(BLLCampaign obj)
    {
        DataTable dt = null;
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@campaignTime", obj.creationDate);
            dt = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetCurrentCampaignByGivenDate", param).Tables[0];
        }
        catch (Exception ex)
        {

        }
        return dt;
    }

    public static DataTable getCurrentCampaignByGivenDateAndCampaignID(BLLCampaign obj)
    {
        DataTable dt = null;
        try
        {
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@campaignTime", obj.creationDate);
            param[1] = new SqlParameter("@campaignID", obj.campaignID);
            dt = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetCurrentCampaignByGivenDateAndCampaignID", param).Tables[0];
        }
        catch (Exception ex)
        {

        }
        return dt;
    }

    public static DataTable GetCurrentDealByDate(BLLCampaign objBLLCampaign)
    {
        DataTable dtDeals = null;
        try
        {
            SqlParameter[] param = new SqlParameter[1];

            //param[0] = new SqlParameter("@cityId", objBLLDeals.cityId);
            param[0] = new SqlParameter("@campaignTime", objBLLCampaign.creationDate);
            dtDeals = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetCurrentCampaignByGivenDate", param).Tables[0];

            if (dtDeals != null && dtDeals.Rows.Count > 0)
            {
                return dtDeals;
            }
        }
        catch (Exception ex)
        {
            return null;
        }
        return dtDeals;
    }
    
    public static DataTable getEndingSoonCampaigns(BLLCampaign obj)
    {
        DataTable dt = null;
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@campaignTime", obj.creationDate);
            dt = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetEndingSoonCampaigns", param).Tables[0];
        }
        catch (Exception ex)
        {

        }
        return dt;
    }

    

    public static DataTable getCurrentProductsByGivenDateAndCategory(BLLCampaign obj)
    {
        DataTable dt = null;
        try
        {
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@campaignTime", obj.creationDate);
            param[1] = new SqlParameter("@campaignCategory", obj.campaignCategory);
            dt = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetCurrentProductsByGivenDateAndCategory", param).Tables[0];
        }
        catch (Exception ex)
        {

        }
        return dt;
    }

    public static int updateCampaignSlots(string strCampaignIDs)
    {
        int result = 0;
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@campaignIDs", strCampaignIDs);
            result = SqlHelper.ExecuteNonQuery(Misc.connStr, CommandType.StoredProcedure, "spUpdateCampaignSlots", param);
        }
        catch (Exception ex)
        {
        }
        return result;
    }

    public static int updateCampaignisFeaturedStatus(BLLCampaign objCampaign)
    {
        int result = 0;
        try
        {
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@campaignID", objCampaign.campaignID);
            param[1] = new SqlParameter("@isFeatured", objCampaign.isFeatured);
            result = SqlHelper.ExecuteNonQuery(Misc.connStr, CommandType.StoredProcedure, "spUpdateCampaignisFeaturedStatus", param);
        }
        catch (Exception ex)
        {
        }
        return result;
    }

    public static DataSet getCampaignCalanderByDate()
    {
        
        DataSet ds = new DataSet();
        try
        {
            ds = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "getCampaignCalanderByDate");
        }
        catch (Exception ex)
        {

        }
        return ds;
    }

    public static DataTable getCompaignCalander(BLLCampaign obj)
    {
        DataTable dt = null;

        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@campaignStartTime", obj.campaignStartTime);
            dt = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "getCompaignCalander", param).Tables[0];

        }
        catch (Exception ex)
        {

        }
        return dt;
    }

    public static int AddToMyFavorites(BLLCampaign obj)
    {
        int result = 0;
        try
        {
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@UserID", obj.UserID);
            param[1] = new SqlParameter("@campaignID", obj.campaignID);
            result = SqlHelper.ExecuteNonQuery(Misc.connStr, CommandType.StoredProcedure, "sp_AddToMyFavorites", param);
        }
        catch (Exception ex)
        {
        }
        return result;
    }

    public static bool GetCampaginSoldOutStatus(BLLCampaign obj)
    {
        bool result = true;
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@campaignID", obj.campaignID);
            result =Convert.ToBoolean(SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "sp_GetCampaginSoldOutStatus", param).Tables[0].Rows[0]["Result"].ToString().Trim());
        }
        catch (Exception ex)
        {
        }
        return result;
    }

   
    
 /*  
    public static int deleteCampaign(BLLCities obj)
    {
        int result = 0;
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@CampaignId", obj.CampaignId);
            result = SqlHelper.ExecuteNonQuery(Misc.connStr, CommandType.StoredProcedure, "spDeleteCampaign", param);
        }
        catch (Exception ex)
        {

        }
        return result;
    }
    public static DataTable getCampaignByCampaignId(BLLCities obj)
    {
        DataTable dt = null; 
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@CampaignId", obj.CampaignId);
            dt = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetCampaignById", param).Tables[0];
        }
        catch (Exception ex)
        {

        }
        return dt;
    }
    public static DataTable getCitiesByProvinceId(BLLCities obj)
    {
        DataTable dt = null;
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@provinceId", obj.provinceId);
            dt = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetCitiesByProvinceID", param).Tables[0];
        }
        catch (Exception ex)
        {

        }
        return dt;
    }
    public static DataTable getAllCities(BLLCities obj)
    {
        DataTable dt = null;
        try
        {
            dt = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetAllCities").Tables[0];
        }
        catch (Exception ex)
        {

        }
        return dt;
    }

    public static DataTable getAllCitiesWithProvinceAndCountryInfo(BLLCities obj)
    {
        DataTable dt = null;
        try
        {
            dt = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetAllCitiesWithProvinceAndCountryInfo").Tables[0];
        }
        catch (Exception ex)
        {

        }
        return dt;
    }

    public static bool getCampaignByName(BLLCities obj)
    {
        DataTable dt = null;
        try
        {
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@CampaignName", obj.CampaignName);
            param[1] = new SqlParameter("@provinceId", obj.provinceId);
            dt = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetCampaignByName", param).Tables[0];
            if (dt != null && dt.Rows.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        catch (Exception ex)
        {
            return false;
        }
    }


    public static DataTable getCampaignDetailByName(BLLCities obj)
    {
        DataTable dt = null;
        try
        {
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@CampaignName", obj.CampaignName);
            dt = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetCampaignDetailByName", param).Tables[0];
            return dt;
        }
        catch (Exception ex)
        {
            return dt;
        }
    }

    public static DataTable getAllCitiesForAdmin(BLLCities obj)
    {
        DataTable dt = null;
        try
        {
            dt = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetAllCitiesForAdmin").Tables[0];
        }
        catch (Exception ex)
        {

        }
        return dt;
    }

    public static DataTable GetAllCitiesForSearch()
    {
        DataTable dt = null;
        try
        {
            dt = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetAllCitiesForSearch").Tables[0];
        }
        catch (Exception ex)
        {

        }
        return dt;
    }
    
    */
}
