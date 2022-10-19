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
/// Summary description for DALMemberDeliveryInfo
/// </summary>
public static class DALMemberDeliveryInfo
{
    #region Function to create
    public static int createMemberDeliveryInfoByUserID(BLLMemberDeliveryInfo obj)
    {
        int result = 0;
        try
        {
            SqlParameter[] param = new SqlParameter[26];
            param[0] = new SqlParameter("@userID", obj.userID);
            param[1] = new SqlParameter("@b_Address", obj.b_Address);
            param[2] = new SqlParameter("@b_City", obj.b_City);
            param[3] = new SqlParameter("@b_CompanyName", obj.b_CompanyName);
            param[4] = new SqlParameter("@b_Country", obj.b_Country);
            param[5] = new SqlParameter("@b_Fax", obj.b_Fax);
            param[6] = new SqlParameter("@b_FirstName", obj.b_FirstName);
            param[7] = new SqlParameter("@b_LastName", obj.b_LastName);
            param[8] = new SqlParameter("@b_State", obj.b_State);
            param[9] = new SqlParameter("@b_Telephone", obj.b_Telephone);
            param[10] = new SqlParameter("@b_ZipCode", obj.b_ZipCode);
            param[11] = new SqlParameter("@s_Address", obj.s_Address);
            param[12] = new SqlParameter("@s_City", obj.s_City);
            param[13] = new SqlParameter("@s_CompanyName", obj.s_CompanyName);
            param[14] = new SqlParameter("@s_Country", obj.s_Country);
            param[15] = new SqlParameter("@s_Fax", obj.s_Fax);
            param[16] = new SqlParameter("@s_FirstName", obj.s_FirstName);
            param[17] = new SqlParameter("@s_LastName", obj.s_LastName);
            param[18] = new SqlParameter("@s_State", obj.s_State);
            param[19] = new SqlParameter("@s_Telephone", obj.s_Telephone);
            param[20] = new SqlParameter("@s_ZipCode", obj.s_ZipCode);
            param[21] = new SqlParameter("@creationDate", obj.creationDate);
            param[22] = new SqlParameter("@createdBy", obj.createdBy);
            param[23] = new SqlParameter("@s_Buzzer_number", obj.s_Buzzer_number);
            param[24] = new SqlParameter("@b_Ext", obj.b_Ext);
            param[25] = new SqlParameter("@s_Ext", obj.s_Ext);
            result = SqlHelper.ExecuteNonQuery(Misc.connStr, CommandType.StoredProcedure, "spCreateMemberDeliveryInfo", param);
        }
        catch (Exception ex)
        {
            return 0;
        }
        return result;
    }
    #endregion

    #region function to delete
    public static int deleteMemberDeliveryInfoByUserID(BLLMemberDeliveryInfo obj)
    {
        int result = 0;
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@userID", obj.userID);
            result = SqlHelper.ExecuteNonQuery(Misc.connStr, CommandType.StoredProcedure, "spDeleteMemberDeliveryInfoByUserID", param);
        }
        catch (Exception ex)
        {
            return 0;
        }
        return result;
    }
    #endregion

    #region Function to Update
    public static int updateMemberDeliveryInfoByUserID(BLLMemberDeliveryInfo obj)
    {
        int result = 0;
        try
        {
            SqlParameter[] param = new SqlParameter[15];
            param[0] = new SqlParameter("@userID", obj.userID);            
            param[1] = new SqlParameter("@s_Address", obj.s_Address);
            param[2] = new SqlParameter("@s_City", obj.s_City);
            param[3] = new SqlParameter("@s_CompanyName", obj.s_CompanyName);
            param[4] = new SqlParameter("@s_Country", obj.s_Country);
            param[5] = new SqlParameter("@s_Fax", obj.s_Fax);
            param[6] = new SqlParameter("@s_FirstName", obj.s_FirstName);
            param[7] = new SqlParameter("@s_LastName", obj.s_LastName);
            param[8] = new SqlParameter("@s_State", obj.s_State);
            param[9] = new SqlParameter("@s_Telephone", obj.s_Telephone);
            param[10] = new SqlParameter("@s_ZipCode", obj.s_ZipCode);
            param[11] = new SqlParameter("@ModifiedDate", obj.creationDate);
            param[12] = new SqlParameter("@ModifiedBy", obj.createdBy);
            param[13] = new SqlParameter("@s_Buzzer_number", obj.s_Buzzer_number);
            param[14] = new SqlParameter("@s_Ext", obj.s_Ext);
            result = SqlHelper.ExecuteNonQuery(Misc.connStr, CommandType.StoredProcedure, "spUpdateMemberDeliveryInfoByUserID", param);
        }
        catch (Exception ex)
        {
            return 0;
        }
        return result;
    }
    #endregion

    #region Function to Update
    public static int updateMemberBillingInfoByUserID(BLLMemberDeliveryInfo obj)
    {
        int result = 0;
        try
        {
            SqlParameter[] param = new SqlParameter[14];
            param[0] = new SqlParameter("@userID", obj.userID);
            param[1] = new SqlParameter("@b_Address", obj.b_Address);
            param[2] = new SqlParameter("@b_City", obj.b_City);
            param[3] = new SqlParameter("@b_CompanyName", obj.b_CompanyName);
            param[4] = new SqlParameter("@b_Country", obj.b_Country);
            param[5] = new SqlParameter("@b_Fax", obj.b_Fax);
            param[6] = new SqlParameter("@b_FirstName", obj.b_FirstName);
            param[7] = new SqlParameter("@b_LastName", obj.b_LastName);
            param[8] = new SqlParameter("@b_State", obj.b_State);
            param[9] = new SqlParameter("@b_Telephone", obj.b_Telephone);
            param[10] = new SqlParameter("@b_ZipCode", obj.b_ZipCode);
            param[11] = new SqlParameter("@ModifiedDate", obj.creationDate);
            param[12] = new SqlParameter("@ModifiedBy", obj.createdBy);
            param[13] = new SqlParameter("@b_Ext", obj.b_Ext);
            result = SqlHelper.ExecuteNonQuery(Misc.connStr, CommandType.StoredProcedure, "spUpdateMemberBillingInfoByUserID", param);
        }
        catch (Exception ex)
        {
            return 0;
        }
        return result;
    }
    #endregion
    

    #region Functions to get 

    public static DataTable getMemberDeliveryInfoByUserID(BLLMemberDeliveryInfo obj)
    {
        DataTable dt = null;
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@userID", obj.userID);
            dt = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetMemberDeliveryInfoByUserID",param).Tables[0];
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

    #endregion
   
}
