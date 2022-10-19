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
/// Summary description for DALTaxRate
/// </summary>
public class DALTaxRate
{
    public static int createTaxRate(BLLTaxRate obj)
    {
        int result = 0;
        try
        {
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@createdBy", obj.createdBy);
            param[1] = new SqlParameter("@creationDate", obj.creationDate);
            param[2] = new SqlParameter("@provinceId", obj.provinceId);
            param[3] = new SqlParameter("@taxRates", obj.taxRates);
            result = SqlHelper.ExecuteNonQuery(Misc.connStr, CommandType.StoredProcedure, "spCreateTaxRate", param);
        }
        catch (Exception ex)
        {
            
        }
        return result;
    }

    public static int updateTaxRate(BLLTaxRate obj)
    {
        int result = 0;
        try
        {
            SqlParameter[] param = new SqlParameter[8];
            param[0] = new SqlParameter("@modifiedBy", obj.modifiedBy);
            param[1] = new SqlParameter("@modifiedDate", obj.modifiedDate);            
            param[2] = new SqlParameter("@taxRates", obj.taxRates);
            param[3] = new SqlParameter("@taxID", obj.taxID);
            param[4] = new SqlParameter("@provinceId", obj.provinceId);
            param[5] = new SqlParameter("@provinceName", obj.provinceName);
            param[6] = new SqlParameter("@createdBy", obj.createdBy);
            param[7] = new SqlParameter("@creationDate", obj.creationDate);
            result = SqlHelper.ExecuteNonQuery(Misc.connStr, CommandType.StoredProcedure, "spUpdateTaxRate", param);
        }
        catch (Exception ex)
        {
            
        }
        return result;
    }

    public static DataTable getAllProvinceTaxRate()
    {
        DataTable dt = null;
        try
        {
            dt = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetAllProvinceTaxRate").Tables[0];
            return dt;
        }
        catch (Exception ex)
        {
            return dt;
        }
    }

    public static DataTable getProvinceTaxRateByProvinceID(BLLTaxRate obj)
    {
        DataTable dt = null;
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@provinceId", obj.provinceId);
            dt = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetProvinceTaxRateByProvinceID",param).Tables[0];
            return dt;
        }
        catch (Exception ex)
        {
            return dt;
        }
    }
}
