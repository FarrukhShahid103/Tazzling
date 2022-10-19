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
/// Summary description for DALAdminGiftCard
/// </summary>
public static class DALContractDetail
{
   

    public static int CreateContractDetail(BLLContractDtail objBLLContract)
    {
        int result = 0;

        try
        {
            SqlParameter[] param = new SqlParameter[9];

            param[0] = new SqlParameter("@itemName", objBLLContract.itemName);
            param[1] = new SqlParameter("@images", objBLLContract.image);
            param[2] = new SqlParameter("@Price", objBLLContract.price);
            param[3] = new SqlParameter("@weights", objBLLContract.weight);
            param[4] = new SqlParameter("@width", objBLLContract.width);
            param[5] = new SqlParameter("@lengths", objBLLContract.length);
            param[6] = new SqlParameter("@height", objBLLContract.haight);
            param[7] = new SqlParameter("@userId", objBLLContract.userID);
            param[8] = new SqlParameter("@restaurantId", objBLLContract.restaurantId);

            result = SqlHelper.ExecuteNonQuery(Misc.connStr, CommandType.StoredProcedure, "spCreateContractDetail", param);
        }
        catch (Exception ex)
        {
            return 0;
        }

        return result;
    }

    public static DataSet GetContractDetail(int intStartIndex, int intMaxRecords, BLLContractDtail obj)
    {
        DataSet dst = null;
        try
        {
            SqlParameter[] param = new SqlParameter[3];
           
            param[0] = new SqlParameter("@restaurantId", obj.restaurantId);
            param[1] = new SqlParameter("@startRowIndex", intStartIndex);
            param[2] = new SqlParameter("@maximumRows", intMaxRecords);

            dst = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetContractDetail", param);
        }
        catch (Exception ex)
        {
            return null;
        }
        return dst;
    }




    public static DataTable GetCintractDetailByResId(BLLContractDtail obj)
    {
        DataTable dt = null;
        try
        {
            SqlParameter[] param = new SqlParameter[1];

            param[0] = new SqlParameter("@contractid", obj.contractid);
            dt = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "sp_GetCintractDetailByResId", param).Tables[0];
        }
        catch (Exception ex)
        {
            return null;
        }
        return dt;
    }


    public static int UpdateContractDetail(BLLContractDtail objBLLContract)
    {
        int result = 0;

        try
        {
            SqlParameter[] param = new SqlParameter[9];

            param[0] = new SqlParameter("@contractid", objBLLContract.contractid);
            param[1] = new SqlParameter("@userId", objBLLContract.userID);
            param[2] = new SqlParameter("@itemName", objBLLContract.itemName);
            param[3] = new SqlParameter("@images", objBLLContract.image);
            param[4] = new SqlParameter("@Price", objBLLContract.price);
            param[5] = new SqlParameter("@weights", objBLLContract.weight);
            param[6] = new SqlParameter("@width", objBLLContract.width);

            param[7] = new SqlParameter("@lengths", objBLLContract.length);
            param[8] = new SqlParameter("@height", objBLLContract.haight);

            result = SqlHelper.ExecuteNonQuery(Misc.connStr, CommandType.StoredProcedure, "spUpdateContractDetail", param);
        }
        catch (Exception ex)
        {
            return 0;
        }

        return result;
    }
}
