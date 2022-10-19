using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SQLHelper;
using System.Data;
using System.Data.SqlClient;
/// <summary>
/// Summary productFavoritDescription for DALCities
/// </summary>
public static class DALProductFavorit
{
    public static int createProductFavorit(BLLProductFavorit obj)
    {
        int result = 0;
        try 
        {
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@favoritDate", obj.favoritDate);
            param[1] = new SqlParameter("@productID", obj.productID);
            param[2] = new SqlParameter("@userId", obj.userId);

            result = SqlHelper.ExecuteNonQuery(Misc.connStr, CommandType.StoredProcedure, "spCreateProductFavorit", param);
        }
        catch (Exception ex)
        { 
        
        }
        return result;
    }

}
