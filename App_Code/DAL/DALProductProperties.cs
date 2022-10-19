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
/// 
public static class DALProductProperties
{
    public static int createProductProperties(BLLProductProperties obj)
    {
        int result = 0;
        try
        {
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@productID", obj.productID);
            param[1] = new SqlParameter("@propertiesLabel", obj.propertiesLabel);
            param[2] = new SqlParameter("@propertiesDescription", obj.propertiesDescription);
            result = SqlHelper.ExecuteNonQuery(Misc.connStr, CommandType.StoredProcedure, "spCreateProductProperties", param);
        }
        catch (Exception ex)
        {

        }
        return result;
    }

    public static int updateProductProperties(BLLProductProperties obj)
    {
        int result = 0;
        try
        {
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@productID", obj.productID);
            param[1] = new SqlParameter("@propertiesLabel", obj.propertiesLabel);
            param[2] = new SqlParameter("@propertiesDescription", obj.propertiesDescription);
            param[3] = new SqlParameter("@productPropertiesID", obj.productPropertiesID);
            result = SqlHelper.ExecuteNonQuery(Misc.connStr, CommandType.StoredProcedure, "spUpdateProductProperties", param);
        }
        catch (Exception ex)
        {

        }
        return result;
    }

    public static DataTable getProductPropertiesByPropertiesID(BLLProductProperties obj)
    {
        DataTable dt = null;
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@productPropertiesID", obj.productPropertiesID);
            dt = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetProductPropertiesByPropertiesID", param).Tables[0];
        }
        catch (Exception ex)
        {

        }
        return dt;
    }

    public static DataTable getProductPropertiesByProductID(BLLProductProperties obj)
    {
        DataTable dt = null;
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@productID", obj.productID);
            dt = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetProductPropertiesByProductID", param).Tables[0];
        }
        catch (Exception ex)
        {

        }
        return dt;
    }



    public static int deleteProductProperties(BLLProductProperties obj)
    {
        int result = 0;
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@productPropertiesID", obj.productPropertiesID);
            result = SqlHelper.ExecuteNonQuery(Misc.connStr, CommandType.StoredProcedure, "spDeleteProductProperties", param);
        }
        catch (Exception ex)
        {

        }
        return result;
    }
}


