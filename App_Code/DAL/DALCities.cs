using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SQLHelper;
using System.Data;
using System.Data.SqlClient;
/// <summary>
/// Summary cityDescription for DALCities
/// </summary>
public static class DALCities
{
    public static int createCity(BLLCities obj)
    {
        int result = 0;
        try 
        {
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@cityName", obj.cityName);
            param[1] = new SqlParameter("@provinceId", obj.provinceId);
            param[2] = new SqlParameter("@cityDescription", obj.cityDescription);
            
            result = SqlHelper.ExecuteNonQuery(Misc.connStr, CommandType.StoredProcedure, "spCreateCity", param);
        }
        catch (Exception ex)
        { 
        
        }
        return result;
    }
    public static int updateCity(BLLCities obj)
    {
        int result = 0;
        try
        {
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@cityName", obj.cityName);
            param[1] = new SqlParameter("@provinceId", obj.provinceId);
            param[2] = new SqlParameter("@cityId", obj.cityId);
            param[3] = new SqlParameter("@cityDescription", obj.cityDescription);
            result = SqlHelper.ExecuteNonQuery(Misc.connStr, CommandType.StoredProcedure, "spUpdateCity", param);
        }
        catch (Exception ex)
        {

        }
        return result;
    }
    public static int deleteCity(BLLCities obj)
    {
        int result = 0;
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@cityId", obj.cityId);
            result = SqlHelper.ExecuteNonQuery(Misc.connStr, CommandType.StoredProcedure, "spDeleteCity", param);
        }
        catch (Exception ex)
        {

        }
        return result;
    }
    public static DataTable getCityByCityId(BLLCities obj)
    {
        DataTable dt = null; 
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@cityId", obj.cityId);
            dt = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetCityById", param).Tables[0];
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

    public static bool getCityByName(BLLCities obj)
    {
        DataTable dt = null;
        try
        {
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@cityName", obj.cityName);
            param[1] = new SqlParameter("@provinceId", obj.provinceId);
            dt = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetCityByName", param).Tables[0];
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


    public static DataTable getCityDetailByName(BLLCities obj)
    {
        DataTable dt = null;
        try
        {
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@cityName", obj.cityName);
            dt = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetCityDetailByName", param).Tables[0];
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
    

}
