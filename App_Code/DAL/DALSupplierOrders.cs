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
public static class DALSupplierOrders
{
    public static int createSupplierOrders(BLLSupplierOrders obj)
    {
        int result = 0;
        try
        {
            SqlParameter[] param = new SqlParameter[5];
            param[0] = new SqlParameter("@createdBy", obj.createdBy);
            param[1] = new SqlParameter("@createdDate", obj.createdDate);
            param[2] = new SqlParameter("@productID", obj.productID);
            if (obj.sizeID == 0)
            {
                param[3] = new SqlParameter("@sizeID", DBNull.Value);
            }
            else
            {
                param[3] = new SqlParameter("@sizeID", obj.sizeID);
            }
            param[4] = new SqlParameter("@supplierOrdersQty", obj.supplierOrdersQty);
            result = SqlHelper.ExecuteNonQuery(Misc.connStr, CommandType.StoredProcedure, "spCreateSupplierOrders", param);
        }
        catch (Exception ex)
        {

        }
        return result;
    }

}
