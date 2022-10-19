using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using SQLHelper;

/// <summary>
/// Summary description for DalPayOut
/// </summary>
public class DALPayOut
{
    #region Function to Create New Deal Discussion

    public static int AddNewPayOut(BLLPayOut objBLLPayOut)
    {
        int result = 0;

        try
        {
            SqlParameter[] param = new SqlParameter[6];
            param[0] = new SqlParameter("@dealId", objBLLPayOut.dealId);
            param[1] = new SqlParameter("@poAmount", objBLLPayOut.poAmount);
            param[2] = new SqlParameter("@poBy", objBLLPayOut.poBy);
            param[3] = new SqlParameter("@poDate", objBLLPayOut.poDate);
            param[4] = new SqlParameter("@poDescription", objBLLPayOut.poDescription);
            param[5] = new SqlParameter("@poType", objBLLPayOut.poType);
            
            result = SqlHelper.ExecuteNonQuery(Misc.connStr, CommandType.StoredProcedure, "spCreateNewPayOut", param);
        }
        catch (Exception ex)
        {
            return 0;
        }

        return result;
    }

    #endregion
  
    #region Functions to get Deal Discussion By Deal Id

    public static DataTable getPayOutByDealID(BLLPayOut objBLLPayOut)
    {
        DataTable dtPayOut = null;
        try
        {
            SqlParameter[] param = new SqlParameter[1];

            param[0] = new SqlParameter("@dealId", objBLLPayOut.dealId);

            dtPayOut = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetPayOutByDealID", param).Tables[0];

            if (dtPayOut != null && dtPayOut.Rows.Count > 0)
            {
                return dtPayOut;
            }
        }
        catch (Exception ex)
        {
            return null;
        }
        return dtPayOut;
    }
            
    #endregion

    #region Function to Delete Deal Discussion By Discussion Id

    public static int deletePayOutByID(BLLPayOut objBLLPayOut)
    {
        int iChk = 0;

        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@poID", objBLLPayOut.poID);
            iChk = SqlHelper.ExecuteNonQuery(Misc.connStr, CommandType.StoredProcedure, "spDeletePayOutByID", param);
        }
        catch (Exception ex)
        {
            return iChk;
        }

        return iChk;
    }

    #endregion
}
