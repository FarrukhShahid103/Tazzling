using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using SQLHelper;

/// <summary>
/// Summary description for DALDeals
/// </summary>
public static class DALDeals
{
    #region Function to Create New Deal

    public static int AddNewDeal(BLLDeals objBLLDeals)
    {
        int iDealId = 0;

        try
        {
            SqlParameter[] param = new SqlParameter[37];
            param[0] = new SqlParameter("@restaurantId", objBLLDeals.RestaurantId);
            param[1] = new SqlParameter("@title", objBLLDeals.Title);
            param[2] = new SqlParameter("@finePrint", objBLLDeals.FinePrint);
            param[3] = new SqlParameter("@dealHightlights", objBLLDeals.DealHightlights);
            param[4] = new SqlParameter("@description", objBLLDeals.Description);
            param[5] = new SqlParameter("@sellingPrice", objBLLDeals.SellingPrice);
            param[6] = new SqlParameter("@valuePrice", objBLLDeals.ValuePrice);
            param[7] = new SqlParameter("@dealStartTime", objBLLDeals.DealStartTime);
            param[8] = new SqlParameter("@dealEndTime", objBLLDeals.DealEndTime);
            param[9] = new SqlParameter("@images", objBLLDeals.Images);
            param[10] = new SqlParameter("@dealDelMinLmt", objBLLDeals.DealDelMinLmt);
            param[11] = new SqlParameter("@dealDelMaxLmt", objBLLDeals.DealDelMaxLmt);
            param[12] = new SqlParameter("@dealStatus", objBLLDeals.DealStatus);
            param[13] = new SqlParameter("@createdBy", objBLLDeals.CreatedBy);
            param[14] = new SqlParameter("@createdDate", objBLLDeals.CreatedDate);
            param[15] = new SqlParameter("@minOrdersPerUser", objBLLDeals.MinOrdersPerUser);
            param[16] = new SqlParameter("@maxOrdersPerUser", objBLLDeals.MaxOrdersPerUser);
            param[17] = new SqlParameter("@maxGiftsPerOrder", objBLLDeals.MaxGiftsPerOrder);
            param[18] = new SqlParameter("@howtouse", objBLLDeals.howtouse);
            param[19] = new SqlParameter("@dealSlot", objBLLDeals.DealSlot);
            param[20] = new SqlParameter("@affComm", objBLLDeals.AffComm);
            param[21] = new SqlParameter("@parentDealId", objBLLDeals.ParentDealId);
            param[22] = new SqlParameter("@dealPageTitle", objBLLDeals.DealPageTitle);
            //param[23] = new SqlParameter("@voucherExpiryDate", objBLLDeals.voucherExpiryDate);
            if (objBLLDeals.voucherExpiryDateAvailable)
            {
                param[23] = new SqlParameter("@voucherExpiryDate", objBLLDeals.voucherExpiryDate);
            }
            else
            {
                param[23] = new SqlParameter("@voucherExpiryDate", DBNull.Value);
            }
            param[24] = new SqlParameter("@topTitle", objBLLDeals.topTitle);
            param[25] = new SqlParameter("@shortTitle", objBLLDeals.shortTitle);
            param[26] = new SqlParameter("@shippingAndTax", objBLLDeals.shippingAndTax);
            param[27] = new SqlParameter("@shippingAndTaxAmount", objBLLDeals.shippingAndTaxAmount);
            param[28] = new SqlParameter("@OurCommission", objBLLDeals.ourCommission);
            param[29] = new SqlParameter("@salePersonAccountName", objBLLDeals.SalePersonAccountName);
            param[30] = new SqlParameter("@urlTitle", objBLLDeals.urlTitle);
            param[31] = new SqlParameter("@yelpLink", objBLLDeals.yelpLink);
            param[32] = new SqlParameter("@yelpRate", objBLLDeals.yelpRate);
            param[33] = new SqlParameter("@yelpText", objBLLDeals.yelpText);
            param[34] = new SqlParameter("@reviewExist", objBLLDeals.reviewExist);
            param[35] = new SqlParameter("@tracking", objBLLDeals.tracking);
            param[36] = new SqlParameter("@doublePoints", objBLLDeals.doublePoints);

            DataTable dtDealIdInfo = null;

            dtDealIdInfo = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spCreateNewDeal", param).Tables[0];

            //Check the Data Table of Deal info & get the Deal ID
            if ((dtDealIdInfo != null) && (dtDealIdInfo.Rows.Count > 0))
                iDealId = int.Parse(dtDealIdInfo.Rows[0][0].ToString());
        }
        catch (Exception ex)
        {
            return 0;
        }
        return iDealId;
    }

    #endregion

    #region Function to Update Deal Info by Deal Id

    public static int updateDealInfoByDealId(BLLDeals objBLLDeals)
    {
        int result = 0;

        try
        {
            SqlParameter[] param = new SqlParameter[37];

            param[0] = new SqlParameter("@dealId", objBLLDeals.DealId);
            param[1] = new SqlParameter("@restaurantId", objBLLDeals.RestaurantId);
            param[2] = new SqlParameter("@title", objBLLDeals.Title);
            param[3] = new SqlParameter("@finePrint", objBLLDeals.FinePrint);
            param[4] = new SqlParameter("@dealHightlights", objBLLDeals.DealHightlights);
            param[5] = new SqlParameter("@description", objBLLDeals.Description);
            param[6] = new SqlParameter("@sellingPrice", objBLLDeals.SellingPrice);
            param[7] = new SqlParameter("@valuePrice", objBLLDeals.ValuePrice);
            param[8] = new SqlParameter("@dealStartTime", objBLLDeals.DealStartTime);
            param[9] = new SqlParameter("@dealEndTime", objBLLDeals.DealEndTime);
            param[10] = new SqlParameter("@images", objBLLDeals.Images);
            param[11] = new SqlParameter("@dealDelMinLmt", objBLLDeals.DealDelMinLmt);
            param[12] = new SqlParameter("@dealDelMaxLmt", objBLLDeals.DealDelMaxLmt);
            param[13] = new SqlParameter("@dealStatus", objBLLDeals.DealStatus);
            param[14] = new SqlParameter("@modifiedBy", objBLLDeals.ModifiedBy);
            param[15] = new SqlParameter("@modifiedDate", objBLLDeals.ModifiedDate);
            param[16] = new SqlParameter("@minOrdersPerUser", objBLLDeals.MinOrdersPerUser);
            param[17] = new SqlParameter("@maxOrdersPerUser", objBLLDeals.MaxOrdersPerUser);
            param[18] = new SqlParameter("@maxGiftsPerOrder", objBLLDeals.MaxGiftsPerOrder);
            param[19] = new SqlParameter("@howtouse", objBLLDeals.howtouse);
            param[20] = new SqlParameter("@dealSlot", objBLLDeals.DealSlot);
            param[21] = new SqlParameter("@affComm", objBLLDeals.AffComm);
            param[22] = new SqlParameter("@dealPageTitle", objBLLDeals.DealPageTitle);
            //param[23] = new SqlParameter("@voucherExpiryDate", objBLLDeals.voucherExpiryDate);
            if (objBLLDeals.voucherExpiryDateAvailable)
            {
                param[23] = new SqlParameter("@voucherExpiryDate", objBLLDeals.voucherExpiryDate);
            }
            else
            {
                param[23] = new SqlParameter("@voucherExpiryDate", DBNull.Value);
            }
            param[24] = new SqlParameter("@topTitle", objBLLDeals.topTitle);
            param[25] = new SqlParameter("@shortTitle", objBLLDeals.shortTitle);
            param[26] = new SqlParameter("@shippingAndTax", objBLLDeals.shippingAndTax);
            param[27] = new SqlParameter("@shippingAndTaxAmount", objBLLDeals.shippingAndTaxAmount);
            param[28] = new SqlParameter("@OurCommission", objBLLDeals.ourCommission);
            param[29] = new SqlParameter("@salePersonAccountName", objBLLDeals.SalePersonAccountName);
            param[30] = new SqlParameter("@urlTitle", objBLLDeals.urlTitle);
            param[31] = new SqlParameter("@yelpLink", objBLLDeals.yelpLink);
            param[32] = new SqlParameter("@yelpRate", objBLLDeals.yelpRate);
            param[33] = new SqlParameter("@yelpText", objBLLDeals.yelpText);
            param[34] = new SqlParameter("@reviewExist", objBLLDeals.reviewExist);
            param[35] = new SqlParameter("@tracking", objBLLDeals.tracking);
            param[36] = new SqlParameter("@doublePoints", objBLLDeals.doublePoints);
            result = SqlHelper.ExecuteNonQuery(Misc.connStr, CommandType.StoredProcedure, "spUpdateDealByDealId", param);
        }
        catch (Exception ex)
        {
            return 0;
        }
        return result;
    }

    #endregion

    #region Function to Update Deal Status by Deal Id

    public static int updateDealStatusByDealId(BLLDeals objBLLDeals)
    {
        int result = 0;
        try
        {
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@dealId", objBLLDeals.DealId);
            param[1] = new SqlParameter("@dealStatus", objBLLDeals.DealStatus);
            param[2] = new SqlParameter("@modifiedBy", objBLLDeals.ModifiedBy);
            param[3] = new SqlParameter("@modifiedDate", objBLLDeals.ModifiedDate);
            result = SqlHelper.ExecuteNonQuery(Misc.connStr, CommandType.StoredProcedure, "spUpdateDealStatusByDealId", param);
        }
        catch (Exception ex)
        {
            return 0;
        }
        return result;
    }

    public static int updateDealNoteByDealId(BLLDeals objBLLDeals)
    {
        int result = 0;
        try
        {
            SqlParameter[] param = new SqlParameter[4];

            param[0] = new SqlParameter("@dealId", objBLLDeals.DealId);
            param[1] = new SqlParameter("@dealNote", objBLLDeals.DealNote);
            param[2] = new SqlParameter("@modifiedBy", objBLLDeals.ModifiedBy);
            param[3] = new SqlParameter("@modifiedDate", objBLLDeals.ModifiedDate);

            result = SqlHelper.ExecuteNonQuery(Misc.connStr, CommandType.StoredProcedure, "spUpdateDealNoteByDealId", param);
        }
        catch (Exception ex)
        {
            return 0;
        }
        return result;
    }

    public static int updateDealPaymentType(BLLDeals objBLLDeals)
    {
        int result = 0;
        try
        {
            SqlParameter[] param = new SqlParameter[2];

            param[0] = new SqlParameter("@dealId", objBLLDeals.DealId);
            param[1] = new SqlParameter("@dealpayment", objBLLDeals.dealpayment);

            result = SqlHelper.ExecuteNonQuery(Misc.connStr, CommandType.StoredProcedure, "spUpdateDealPaymentType", param);
        }
        catch (Exception ex)
        {
            return 0;
        }
        return result;
    }

    public static int updateDealSlotByDealId(BLLDeals objBLLDeals)
    {
        int result = 0;
        try
        {
            SqlParameter[] param = new SqlParameter[3];

            param[0] = new SqlParameter("@dealId", objBLLDeals.DealId);
            param[1] = new SqlParameter("@DealSlot", objBLLDeals.DealSlot);
            param[2] = new SqlParameter("@cityId", objBLLDeals.cityId);

            result = SqlHelper.ExecuteNonQuery(Misc.connStr, CommandType.StoredProcedure, "spUpdateDealSlotByDealId", param);
        }
        catch (Exception ex)
        {
            return 0;
        }
        return result;
    }
    #endregion

    #region Functions to get all Deals

    public static DataTable getAllDeals()
    {
        DataTable dtDeals = null;
        try
        {
            dtDeals = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetAllDeals").Tables[0];
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

    public static DataTable getAllDealsTitleAndID()
    {
        DataTable dtDeals = null;
        try
        {

            SqlParameter[] param = new SqlParameter[1];
            DateTime dt = DateTime.Now;
            param[0] = new SqlParameter("@DealEndTime", dt);
            dtDeals = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetAllDealsTitleAndID", param).Tables[0];
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

    public static DataTable getdealDetailForCheckout(BLLDeals objBLLDeals)
    {
        DataTable dtDeals = null;
        try
        {

            SqlParameter[] param = new SqlParameter[1];            
            param[0] = new SqlParameter("@dealID", objBLLDeals.DealId);
            dtDeals = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetdealDetailForCheckout", param).Tables[0];
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


    


    #endregion

    #region Functions to get all Deal By RestaurantId

    public static DataTable getDealByRestaurantId(BLLDeals objBLLDeals)
    {
        DataTable dtDeals = null;
        try
        {
            SqlParameter[] param = new SqlParameter[1];

            param[0] = new SqlParameter("@restaurantId", objBLLDeals.RestaurantId);

            dtDeals = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetDealByRestaurantId", param).Tables[0];

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

    #endregion

    #region Functions to get Deal By Deal Start & End Time & By City

    public static DataTable getDealInfoByDealStartEndTime(BLLDeals objBLLDeals)
    {
        DataTable dtDealInfo = null;

        try
        {
            SqlParameter[] param = new SqlParameter[4];

            param[0] = new SqlParameter("@dealStartTime", objBLLDeals.DealStartTime);
            param[1] = new SqlParameter("@dealEndTime", objBLLDeals.DealEndTime);
            param[2] = new SqlParameter("@restaurantId", objBLLDeals.RestaurantId);
            param[3] = new SqlParameter("@dealSlot", objBLLDeals.DealSlot);

            dtDealInfo = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetDealInfoByDealTime", param).Tables[0];

            if (dtDealInfo != null && dtDealInfo.Rows.Count > 0)
            {
                return dtDealInfo;
            }
        }
        catch (Exception ex)
        {
            return null;
        }
        return dtDealInfo;
    }

    public static DataTable getDealInfoByDealStartEndTimeWithCityID(BLLDeals objBLLDeals)
    {
        DataTable dtDealInfo = null;

        try
        {
            SqlParameter[] param = new SqlParameter[5];

            param[0] = new SqlParameter("@dealStartTime", objBLLDeals.DealStartTime);
            param[1] = new SqlParameter("@dealEndTime", objBLLDeals.DealEndTime);
            param[2] = new SqlParameter("@restaurantId", objBLLDeals.RestaurantId);
            param[3] = new SqlParameter("@dealSlot", objBLLDeals.DealSlot);
            param[4] = new SqlParameter("@CityID", objBLLDeals.cityId);

            dtDealInfo = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetDealInfoByDealTimeWithCityID", param).Tables[0];

            if (dtDealInfo != null && dtDealInfo.Rows.Count > 0)
            {
                return dtDealInfo;
            }
        }
        catch (Exception ex)
        {
            return null;
        }
        return dtDealInfo;
    }


    public static DataTable getActiveDealSlotByDealStartEndTimeWithCityID(BLLDeals objBLLDeals)
    {
        DataTable dtDealInfo = null;

        try
        {
            SqlParameter[] param = new SqlParameter[3];

            param[0] = new SqlParameter("@dealStartTime", objBLLDeals.DealStartTime);
            param[1] = new SqlParameter("@dealEndTime", objBLLDeals.DealEndTime);
            param[2] = new SqlParameter("@CityID", objBLLDeals.cityId);

            dtDealInfo = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetActiveDealSlotByDealStartEndTimeWithCityID", param).Tables[0];

            if (dtDealInfo != null && dtDealInfo.Rows.Count > 0)
            {
                return dtDealInfo;
            }
        }
        catch (Exception ex)
        {
            return null;
        }
        return dtDealInfo;
    }

    #endregion

    #region Functions to get Deal By Deal Id

    public static DataTable getDealByDealId(BLLDeals objBLLDeals)
    {
        DataTable dtDeals = null;
        try
        {
            SqlParameter[] param = new SqlParameter[1];

            param[0] = new SqlParameter("@dealId", objBLLDeals.DealId);

            dtDeals = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetDealByDealId", param).Tables[0];

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

    public static DataTable getDealForPaymentFormByDealId(BLLDeals objBLLDeals)
    {
        DataTable dtDeals = null;
        try
        {
            SqlParameter[] param = new SqlParameter[2];

            param[0] = new SqlParameter("@dealId", objBLLDeals.DealId);
            param[1] = new SqlParameter("@cityId", objBLLDeals.cityId);

            dtDeals = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetDealForPaymentFormByDealId", param).Tables[0];

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

    public static DataTable getDealinfoByDealID(BLLDeals objBLLDeals)
    {
        DataTable dtDeals = null;
        try
        {
            SqlParameter[] param = new SqlParameter[1];

            param[0] = new SqlParameter("@DealId", objBLLDeals.DealId);

            dtDeals = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetDealinfoByDealID", param).Tables[0];

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

    public static DataTable getDealinfoByURLTitle(BLLDeals objBLLDeals)
    {
        DataTable dtDeals = null;
        try
        {
            SqlParameter[] param = new SqlParameter[1];

            param[0] = new SqlParameter("@urlTitle", objBLLDeals.urlTitle);

            dtDeals = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetDealinfoByURLTitle", param).Tables[0];

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

    public static DataTable getCurrentDealByCityIDForPage(BLLDeals objBLLDeals)
    {
        DataTable dtDeals = null;
        try
        {
            SqlParameter[] param = new SqlParameter[2];

            param[0] = new SqlParameter("@cityId", objBLLDeals.cityId);
            param[1] = new SqlParameter("@dtProvinceLocalTime", objBLLDeals.CreatedDate);
            dtDeals = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetCurrentDealByCityIDForPage", param).Tables[0];

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

    public static DataTable getCurrentDealByCityIDForPage2(BLLDeals objBLLDeals)
    {
        DataTable dtDeals = null;
        try
        {
            SqlParameter[] param = new SqlParameter[2];

            param[0] = new SqlParameter("@cityId", objBLLDeals.cityId);
            param[1] = new SqlParameter("@dtProvinceLocalTime", objBLLDeals.CreatedDate);
            dtDeals = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetCurrentDealByCityIDForPage2", param).Tables[0];

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

    public static DataTable getCurrentDealByCityIDForPage3(BLLDeals objBLLDeals)
    {
        DataTable dtDeals = null;
        try
        {
            SqlParameter[] param = new SqlParameter[2];

            param[0] = new SqlParameter("@cityId", objBLLDeals.cityId);
            param[1] = new SqlParameter("@dtProvinceLocalTime", objBLLDeals.CreatedDate);
            dtDeals = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetCurrentDealByCityIDForPage3", param).Tables[0];

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

    public static DataTable getCurrentDealByCityID(BLLDeals objBLLDeals)
    {
        DataTable dtDeals = null;
        try
        {
            SqlParameter[] param = new SqlParameter[2];

            param[0] = new SqlParameter("@cityId", objBLLDeals.cityId);
            param[1] = new SqlParameter("@dtProvinceLocalTime", objBLLDeals.CreatedDate);
            dtDeals = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetCurrentDealByCityID", param).Tables[0];

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

    public static DataTable getPreviousDealByCityID(BLLDeals objBLLDeals)
    {
        DataTable dtDeals = null;
        try
        {
            SqlParameter[] param = new SqlParameter[2];

            param[0] = new SqlParameter("@cityId", objBLLDeals.cityId);
            param[1] = new SqlParameter("@dtProvinceLocalTime", objBLLDeals.DealEndTime);
            dtDeals = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetPreviousDealByCityID", param).Tables[0];

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

    public static DataTable getPreviousDealByCityIDForShow(BLLDeals objBLLDeals)
    {
        DataTable dtDeals = null;
        try
        {
            SqlParameter[] param = new SqlParameter[2];

            param[0] = new SqlParameter("@cityId", objBLLDeals.cityId);
            param[1] = new SqlParameter("@dtProvinceLocalTime", objBLLDeals.DealEndTime);
            dtDeals = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetPreviousDealByCityIDForShow", param).Tables[0];

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



    public static DataTable getCurrentDealByDealIDForDealPage(BLLDeals objBLLDeals)
    {
        DataTable dtDeals = null;

        try
        {
            SqlParameter[] param = new SqlParameter[2];

            param[0] = new SqlParameter("@dealId", objBLLDeals.DealId);
            param[1] = new SqlParameter("@cityId", objBLLDeals.cityId);
            dtDeals = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetCurrentDealByDealIDForDealPage", param).Tables[0];

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

    public static DataTable getCurrentDealByURLTitleForDealPage(BLLDeals objBLLDeals)
    {
        DataTable dtDeals = null;

        try
        {
            SqlParameter[] param = new SqlParameter[2];

            param[0] = new SqlParameter("@urlTitle", objBLLDeals.urlTitle);
            param[1] = new SqlParameter("@cityId", objBLLDeals.cityId);
            dtDeals = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetCurrentDealByURLTitleForDealPage", param).Tables[0];

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



    public static DataTable getCurrentDealByDealIDForDealPage2(BLLDeals objBLLDeals)
    {
        DataTable dtDeals = null;

        try
        {
            SqlParameter[] param = new SqlParameter[2];

            param[0] = new SqlParameter("@dealId", objBLLDeals.DealId);
            param[1] = new SqlParameter("@cityId", objBLLDeals.cityId);
            dtDeals = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetCurrentDealByDealIDForDealPage2", param).Tables[0];

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


    public static DataTable getCurrentDealByDealID(BLLDeals objBLLDeals)
    {
        DataTable dtDeals = null;

        try
        {
            SqlParameter[] param = new SqlParameter[2];

            param[0] = new SqlParameter("@dealId", objBLLDeals.DealId);
            param[1] = new SqlParameter("@cityId", objBLLDeals.cityId);
            dtDeals = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetCurrentDealByDealID", param).Tables[0];

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

    public static DataTable getDealByDealAndCityID(BLLDeals objBLLDeals)
    {
        DataTable dtDeals = null;

        try
        {
            SqlParameter[] param = new SqlParameter[2];

            param[0] = new SqlParameter("@dealId", objBLLDeals.DealId);
            param[1] = new SqlParameter("@cityId", objBLLDeals.cityId);
            dtDeals = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetDealByDealAndCityID", param).Tables[0];

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



    public static DataTable getCurrentSubDealInfoByParnetDealIDForDealPage(BLLDeals objBLLDeals)
    {
        DataTable dtDeals = null;

        try
        {
            SqlParameter[] param = new SqlParameter[1];

            param[0] = new SqlParameter("@parentDealId", objBLLDeals.ParentDealId);
            dtDeals = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetCurrentSubDealInfoByParnetDealIDForDealPage", param).Tables[0];

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

    public static DataTable getCurrentSubDealInfoByParentDealID(BLLDeals objBLLDeals)
    {
        DataTable dtDeals = null;

        try
        {
            SqlParameter[] param = new SqlParameter[2];

            param[0] = new SqlParameter("@parentDealId", objBLLDeals.ParentDealId);
            param[1] = new SqlParameter("@cityId", objBLLDeals.cityId);
            dtDeals = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetCurrentSubDealInfoByParnetDealID", param).Tables[0];

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

    #endregion

    #region Function to Delete Deal By Deal Id

    public static int deleteDealByDealId(BLLDeals objBLLDeals)
    {
        int iChk = 0;

        try
        {
            SqlParameter[] param = new SqlParameter[1];

            param[0] = new SqlParameter("@dealId", objBLLDeals.DealId);

            if (objBLLDeals.DealId != 0)
                iChk = SqlHelper.ExecuteNonQuery(Misc.connStr, CommandType.StoredProcedure, "spDeleteDealByDealId", param);
        }
        catch (Exception ex)
        {
            return iChk;
        }

        return iChk;

    }

    #endregion
}