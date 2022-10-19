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
public static class DALProductSize
{
    public static int createProductSize(BLLProductSize obj)
    {
        int result = 0;
        try
        {
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@productID", obj.productID);
            param[1] = new SqlParameter("@sizeText", obj.sizeText);
            param[2] = new SqlParameter("@quantity", obj.quantity);
            result = SqlHelper.ExecuteNonQuery(Misc.connStr, CommandType.StoredProcedure, "spCreateProductSize", param);
        }
        catch (Exception ex)
        {

        }
        return result;
    }

    public static int updateProductSize(BLLProductSize obj)
    {
        int result = 0;
        try
        {
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@productID", obj.productID);
            param[1] = new SqlParameter("@sizeText", obj.sizeText);
            param[2] = new SqlParameter("@sizeID", obj.sizeID);
            param[3] = new SqlParameter("@quantity", obj.quantity);
            result = SqlHelper.ExecuteNonQuery(Misc.connStr, CommandType.StoredProcedure, "spUpdateProductSize", param);
        }
        catch (Exception ex)
        {

        }
        return result;
    }

    public static DataTable getProductSizeBySizeID(BLLProductSize obj)
    {
        DataTable dt = null;
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@sizeID", obj.sizeID);
            dt = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetProductSizeBySizeID", param).Tables[0];
        }
        catch (Exception ex)
        {

        }
        return dt;
    }

    public static DataTable getProductSizeByProductID(BLLProductSize obj)
    {
        DataTable dt = null;
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@productID", obj.productID);
            dt = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetProductSizeByProductID", param).Tables[0];
        }
        catch (Exception ex)
        {

        }
        return dt;
    }



    public static int deleteProductSize(BLLProductSize obj)
    {
        int result = 0;
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@sizeID", obj.sizeID);
            result = SqlHelper.ExecuteNonQuery(Misc.connStr, CommandType.StoredProcedure, "spDeleteProductSize", param);
        }
        catch (Exception ex)
        {

        }
        return result;
    }
}
