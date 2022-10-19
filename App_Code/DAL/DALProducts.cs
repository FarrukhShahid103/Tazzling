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
/// Summary description for DALCategories
/// </summary>
public static class DALProducts
{
    #region Function to create new Product
    public static long createProduct(BLLProducts obj)
    {
        long result = 0;
        try
        {
            SqlParameter[] param = new SqlParameter[30];
            param[0] = new SqlParameter("@height", obj.height);
            param[1] = new SqlParameter("@campaignID", obj.campaignID);
            param[2] = new SqlParameter("@width", obj.width);
            param[3] = new SqlParameter("@dimension", obj.dimension);
            param[4] = new SqlParameter("@createdBy", obj.createdBy);
            param[5] = new SqlParameter("@createdDate", obj.createdDate);
            param[6] = new SqlParameter("@description", obj.description);            
            param[7] = new SqlParameter("@enableSize", obj.enableSize);            
            param[8] = new SqlParameter("@images", obj.images);
            param[9] = new SqlParameter("@isActive", obj.isActive);                        
            param[10] = new SqlParameter("@maxOrdersPerUser", obj.maxOrdersPerUser);
            param[11] = new SqlParameter("@maxQty", obj.maxQty);            
            param[12] = new SqlParameter("@minOrdersPerUser", obj.minOrdersPerUser);
            param[13] = new SqlParameter("@minQty", obj.minQty);            
            param[14] = new SqlParameter("@OurCommission", obj.OurCommission);
            param[15] = new SqlParameter("@productEndTime", obj.productEndTime);
            param[16] = new SqlParameter("@productPosition", obj.productPosition);
            param[17] = new SqlParameter("@productStartTime", obj.productStartTime);
            param[18] = new SqlParameter("@returnPolicy", obj.returnPolicy);
            param[19] = new SqlParameter("@sellingPrice", obj.sellingPrice);
            param[20] = new SqlParameter("@shippingInfo", obj.shippingInfo);
            param[21] = new SqlParameter("@shortDescription", obj.shortDescription);            
            param[22] = new SqlParameter("@subTitle", obj.subTitle);
            param[23] = new SqlParameter("@title", obj.title);
            param[24] = new SqlParameter("@valuePrice", obj.valuePrice);
            param[25] = new SqlParameter("@voucherExpiryDate", obj.voucherExpiryDate);
            param[26] = new SqlParameter("@weight", obj.weight);
            param[27] = new SqlParameter("@productPositionForCategory", obj.productPositionForCategory);
            param[28] = new SqlParameter("@isVoucherProduct", obj.isVoucherProduct);
            param[29] = new SqlParameter("@tracking", obj.tracking);
            
            result = Convert.ToInt32(SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spCreateProduct", param).Tables[0].Rows[0][0]);
            
        }
        catch (Exception ex)
        {
            return 0;
        }
        return result;
    }
    #endregion
     
    #region Function to Update Product
    public static long updateProduct(BLLProducts obj)
    {
        long result = 0;
        try
        {
            SqlParameter[] param = new SqlParameter[29];
            param[0] = new SqlParameter("@height", obj.height);
            param[1] = new SqlParameter("@campaignID", obj.campaignID);
            param[2] = new SqlParameter("@width", obj.width);
            param[3] = new SqlParameter("@dimension", obj.dimension);
            param[4] = new SqlParameter("@modifiedBy", obj.modifiedBy);
            param[5] = new SqlParameter("@modifiedDate", obj.modifiedDate);
            param[6] = new SqlParameter("@description", obj.description);            
            param[7] = new SqlParameter("@enableSize", obj.enableSize);            
            param[8] = new SqlParameter("@images", obj.images);
            param[9] = new SqlParameter("@isActive", obj.isActive);            
            param[10] = new SqlParameter("@maxOrdersPerUser", obj.maxOrdersPerUser);
            param[11] = new SqlParameter("@maxQty", obj.maxQty);            
            param[12] = new SqlParameter("@minOrdersPerUser", obj.minOrdersPerUser);
            param[13] = new SqlParameter("@minQty", obj.minQty);            
            param[14] = new SqlParameter("@OurCommission", obj.OurCommission);
            param[15] = new SqlParameter("@productEndTime", obj.productEndTime);
            param[16] = new SqlParameter("@productID", obj.productID);
            param[17] = new SqlParameter("@productStartTime", obj.productStartTime);
            param[18] = new SqlParameter("@returnPolicy", obj.returnPolicy);
            param[19] = new SqlParameter("@sellingPrice", obj.sellingPrice);
            param[20] = new SqlParameter("@shippingInfo", obj.shippingInfo);
            param[21] = new SqlParameter("@shortDescription", obj.shortDescription);            
            param[22] = new SqlParameter("@subTitle", obj.subTitle);
            param[23] = new SqlParameter("@title", obj.title);
            param[24] = new SqlParameter("@valuePrice", obj.valuePrice);
            param[25] = new SqlParameter("@voucherExpiryDate", obj.voucherExpiryDate);
            param[26] = new SqlParameter("@weight", obj.weight);
            param[27] = new SqlParameter("@isVoucherProduct", obj.isVoucherProduct);
            param[28] = new SqlParameter("@tracking", obj.tracking);
            
            result = SqlHelper.ExecuteNonQuery(Misc.connStr, CommandType.StoredProcedure, "spUpdateProduct", param);
            
        }
        catch (Exception ex)
        {
            return 0;
        }
        return result;
    }   
    #endregion

    #region Functions to get Product by ID
    public static DataTable getProductsByCampaignID(BLLProducts obj)
    {
        DataTable dt = null;
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@campaignID", obj.campaignID);
            dt = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetProductsByCampaignID", param).Tables[0];
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

    public static DataTable getProductsByCampaignIDForClientSide(BLLProducts obj)
    {
        DataTable dt = null;
        try
        {
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@campaignID", obj.campaignID);
            param[1] = new SqlParameter("@createdBy", obj.createdBy);
            dt = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetProductsByCampaignIDForClientSide", param).Tables[0];
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

    public static DataSet getProductsByCategoryAndDateTimeForClient(BLLProducts obj)
    {
        DataSet dst = null;
        try
        {
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@campaignID", obj.campaignID);
            param[1] = new SqlParameter("@createdBy", obj.createdBy);
            param[2] = new SqlParameter("@createdDate", obj.createdDate);
            dst = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetProductsByCategoryAndDateTimeForClient", param);
            if (dst != null && dst.Tables.Count>0)
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

    public static DataTable getProductsByProductID(BLLProducts obj)
    {
        DataTable dt = null;
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@productID", obj.productID);
            dt = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetProductsByProductID", param).Tables[0];
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

    public static DataTable getProductsByProductIDForClient(BLLProducts obj)
    {
        DataTable dt = null;
        try
        {
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@productID", obj.productID);
            param[1] = new SqlParameter("@createdBy", obj.createdBy);
            dt = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetProductsByProductIDForClient", param).Tables[0];
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

    public static int updateProductSlots(string strProductIDs)
    {
        int result = 0;
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@productIDs", strProductIDs);
            result = SqlHelper.ExecuteNonQuery(Misc.connStr, CommandType.StoredProcedure, "spUpdateProductSlots", param);
        }
        catch (Exception ex)
        {
        }
        return result;
    }

    public static int updateProductCategorySlots(string strProductIDs)
    {
        int result = 0;
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@productIDs", strProductIDs);
            result = SqlHelper.ExecuteNonQuery(Misc.connStr, CommandType.StoredProcedure, "spUpdateProductCategorySlots", param);
        }
        catch (Exception ex)
        {
        }
        return result;
    }

    #endregion
}
