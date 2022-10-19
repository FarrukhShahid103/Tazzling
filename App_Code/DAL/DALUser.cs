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
/// Summary description for DALUser
/// </summary>
public static class DALUser
{
    public static DataTable validateUserNamePassword(BLLUser obj)
    {
        DataTable dt = null;
        try 
        {
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@userName", obj.userName);
            param[1] = new SqlParameter("@userPassword", obj.userPassword);
            dt = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spSelectByUsernameAndPassword", param).Tables[0];
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

    public static DataTable getUserRestaurantIDbyUserName(BLLUser obj)
    {
        DataTable dt = null;
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@userName", obj.userName);
            dt = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetUserRestaurantIDbyUserName", param).Tables[0];
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



    public static int createUser(BLLUser obj)
    {
        int result = 0;
        try
        {
            SqlParameter[] param = new SqlParameter[21];


            param[0] = new SqlParameter("@userTypeID", obj.userTypeID);
            param[1] = new SqlParameter("@userPassword", obj.userPassword);
            param[2] = new SqlParameter("@userName", obj.userName);
            param[3] = new SqlParameter("@email", obj.email);
            param[4] = new SqlParameter("@referralId", obj.referralId);
            param[5] = new SqlParameter("@firstName", obj.firstName);
            param[6] = new SqlParameter("@lastName", obj.lastName);
            param[7] = new SqlParameter("@friendsReferralId", obj.friendsReferralId);
            if (obj.countryId != 0)
            {
                param[8] = new SqlParameter("@countryId", obj.countryId);
            }
            else
            {
                param[8] = new SqlParameter("@countryId", DBNull.Value);
            }
            if (obj.provinceId != 0)
            {
                param[9] = new SqlParameter("@provinceId", obj.provinceId);
            }
            else
            {
                param[9] = new SqlParameter("@provinceId", DBNull.Value);
            }
            param[10] = new SqlParameter("@phoneNo", obj.phoneNo);
            param[11] = new SqlParameter("@creationDate", obj.creationDate);
            if (obj.createdBy != 0)
            {
                param[12] = new SqlParameter("@createdBy", obj.createdBy);
            }
            else
            {
                param[12] = new SqlParameter("@createdBy", DBNull.Value);
            }
            param[13] = new SqlParameter("@isActive", obj.isActive);
            param[14] = new SqlParameter("@profilePicture", obj.profilePicture);            
            param[15] = new SqlParameter("@gender", obj.gender);
            param[16] = new SqlParameter("@age", obj.age);
            if (obj.cityId == 0)
            {
                param[17] = new SqlParameter("@cityId", DBNull.Value);
            }
            else
            {
                param[17] = new SqlParameter("@cityId", obj.cityId);
            }
            param[18] = new SqlParameter("@zipcode", obj.zipcode);
            param[19] = new SqlParameter("@howYouKnowUs", obj.howYouKnowUs);
            param[20] = new SqlParameter("@ipAddress", obj.ipAddress);
            
            result = Convert.ToInt32(SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spCreateUser", param).Tables[0].Rows[0][0]);
        }
        catch (Exception ex)
        {
            return 0;
        }
        return result;
    }


    public static int createUserForFB(BLLUser obj)
    {
        int result = 0;
        try
        {
            SqlParameter[] param = new SqlParameter[16];
            param[0] = new SqlParameter("@userTypeID", obj.userTypeID);
            param[1] = new SqlParameter("@FB_userID", obj.FB_userID);
            param[2] = new SqlParameter("@userName", obj.userName);
            param[3] = new SqlParameter("@email", obj.email);            
            param[4] = new SqlParameter("@firstName", obj.firstName);
            param[5] = new SqlParameter("@lastName", obj.lastName);                       
            param[6] = new SqlParameter("@countryId", "2");                                   
            param[7] = new SqlParameter("@provinceId", "3");                        
            param[8] = new SqlParameter("@creationDate", obj.creationDate);
            param[9] = new SqlParameter("@modifiedDate", obj.modifiedDate);
            param[10] = new SqlParameter("@isActive", obj.isActive);
            param[11] = new SqlParameter("@profilePicture", obj.profilePicture);
            param[12] = new SqlParameter("@FB_access_token", obj.FB_access_token);
            param[13] = new SqlParameter("@userPassword", obj.userPassword);
            param[14] = new SqlParameter("@cityId", "337");
            param[15] = new SqlParameter("@ipAddress", obj.ipAddress);

            result = Convert.ToInt32(SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spCreateUserForFB", param).Tables[0].Rows[0][0]);            
        }
        catch (Exception ex)
        {
            return 0;
        }
        return result;
    }
   
    public static int updateUserProfile(BLLUser obj)
    {
        int result = 0;
        try
        {
            SqlParameter[] param = new SqlParameter[10];
            param[0] = new SqlParameter("@userID", obj.userId);
            param[1] = new SqlParameter("@userPassword", obj.userPassword);
            param[2] = new SqlParameter("@userName", obj.userName);
            param[3] = new SqlParameter("@email", obj.email);
            param[4] = new SqlParameter("@howYouKnowUs", obj.howYouKnowUs);
            param[5] = new SqlParameter("@modifiedDate", obj.modifiedDate);
            param[6] = new SqlParameter("@modifiedBy", obj.modifiedBy);
            param[7] = new SqlParameter("@profilePicture", obj.profilePicture);
            param[8] = new SqlParameter("@FB_userID", obj.FB_userID);
            param[9] = new SqlParameter("@FB_access_token", obj.FB_access_token);
            param[10] = new SqlParameter("@gender", obj.gender);
            param[11] = new SqlParameter("@age", obj.age);
            param[12] = new SqlParameter("@cityId", obj.cityId);
            param[13] = new SqlParameter("@zipcode", obj.zipcode);            		
            result = SqlHelper.ExecuteNonQuery(Misc.connStr, CommandType.StoredProcedure, "spUpdateUserProfile", param);
        }
        catch (Exception ex)
        {
            return 0;
        }
        return result;
    }

    public static int updateUserAffiliateReqByUserId(BLLUser obj)
    {
        int result = 0;
        try
        {
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@userID", obj.userId);
            param[1] = new SqlParameter("@affiliateReq", obj.affiliateReq);
            
            result = SqlHelper.ExecuteNonQuery(Misc.connStr, CommandType.StoredProcedure, "spUpdateUserInfoAffiliateReqByID", param);
        }
        catch (Exception ex)
        {
            return 0;
        }
        return result;
    }

    public static int updateUserAffCommIDByUserId(BLLUser obj)
    {
        int result = 0;
        try
        {
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@userID", obj.userId);
            param[1] = new SqlParameter("@affComID", obj.affComID);
            param[2] = new SqlParameter("@affComEndDate", obj.affComEndDate);

            result = SqlHelper.ExecuteNonQuery(Misc.connStr, CommandType.StoredProcedure, "spUpdateUserInfoAffCommIDByUserID", param);
        }
        catch (Exception ex)
        {
            return 0;
        }
        return result;
    }    
    
    public static int updateUser(BLLUser obj)
    {
        int result = 0;
        try
        {
            SqlParameter[] param = new SqlParameter[17];


            param[0] = new SqlParameter("@userTypeID", obj.userTypeID);
            param[1] = new SqlParameter("@userPassword", obj.userPassword);
            param[2] = new SqlParameter("@userName", obj.userName);
            param[3] = new SqlParameter("@email", obj.email);
            param[4] = new SqlParameter("@referralId", obj.referralId);
            param[5] = new SqlParameter("@firstName", obj.firstName);
            param[6] = new SqlParameter("@lastName", obj.lastName);
            param[7] = new SqlParameter("@friendsReferralId", obj.friendsReferralId);
            if (obj.countryId != 0)
            {
                param[8] = new SqlParameter("@countryId", obj.countryId);
            }
            else
            {
                param[8] = new SqlParameter("@countryId", DBNull.Value);
            }
            if (obj.provinceId != 0)
            {
                param[9] = new SqlParameter("@provinceId", obj.provinceId);
            }
            else
            {
                param[9] = new SqlParameter("@provinceId", DBNull.Value);
            }
            param[10] = new SqlParameter("@phoneNo", obj.phoneNo);
            param[11] = new SqlParameter("@modifiedDate", obj.modifiedDate);
            param[12] = new SqlParameter("@modifiedBy", obj.modifiedBy);
            param[13] = new SqlParameter("@userID", obj.userId);
            param[14] = new SqlParameter("@isActive", obj.isActive);
            param[15] = new SqlParameter("@cityId", obj.cityId);
            param[16] = new SqlParameter("@profilePicture", obj.profilePicture);
            result = SqlHelper.ExecuteNonQuery(Misc.connStr, CommandType.StoredProcedure, "spUpdateUser", param);
        }
        catch (Exception ex)
        {
            return 0;
        }
        return result;
    }

    public static int updateUserFields(BLLUser obj)
    {
        int result = 0;
        try
        {
            SqlParameter[] param = new SqlParameter[18];


            param[0] = new SqlParameter("@userTypeID", obj.userTypeID);
            param[1] = new SqlParameter("@userPassword", obj.userPassword);
            param[2] = new SqlParameter("@userName", obj.userName);
            param[3] = new SqlParameter("@email", obj.email);
            param[4] = new SqlParameter("@referralId", obj.referralId);
            param[5] = new SqlParameter("@firstName", obj.firstName);
            param[6] = new SqlParameter("@lastName", obj.lastName);
            param[7] = new SqlParameter("@friendsReferralId", obj.friendsReferralId);
            if (obj.countryId != 0)
            {
                param[8] = new SqlParameter("@countryId", obj.countryId);
            }
            else
            {
                param[8] = new SqlParameter("@countryId", DBNull.Value);
            }
            if (obj.provinceId != 0)
            {
                param[9] = new SqlParameter("@provinceId", obj.provinceId);
            }
            else
            {
                param[9] = new SqlParameter("@provinceId", DBNull.Value);
            }
            param[10] = new SqlParameter("@phoneNo", obj.phoneNo);
            param[11] = new SqlParameter("@howYouHear", obj.howYouKnowUs);
            param[12] = new SqlParameter("@modifiedDate", obj.modifiedDate);
            param[13] = new SqlParameter("@modifiedBy", obj.modifiedBy);
            param[14] = new SqlParameter("@userID", obj.userId);
            param[15] = new SqlParameter("@isActive", obj.isActive);
            param[16] = new SqlParameter("@cityId", obj.cityId);
            param[17] = new SqlParameter("@profilePicture", obj.profilePicture);
            result = SqlHelper.ExecuteNonQuery(Misc.connStr, CommandType.StoredProcedure, "spUpdateUserFields", param);
        }
        catch (Exception ex)
        {
            return 0;
        }
        return result;
    }


    public static int updateUsersFriendsReferralId(BLLUser obj)
    {
        int result = 0;
        try
        {
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@friendsReferralId", obj.friendsReferralId);
            param[1] = new SqlParameter("@userID", obj.userId);
            result = SqlHelper.ExecuteNonQuery(Misc.connStr, CommandType.StoredProcedure, "spUpdateUsersFriendsReferralId", param);
        }
        catch (Exception ex)
        {
            return 0;
        }
        return result;
    }


    public static int updateUsersPassword(BLLUser obj)
    {
        int result = 0;
        try
        {
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@newPassword", obj.newPassword);
            param[1] = new SqlParameter("@userID", obj.userId);
            param[2] = new SqlParameter("@userPassword", obj.userPassword);
            param[3] = new SqlParameter("@userPassword", obj.userPassword);
            //@userPassword
            result = SqlHelper.ExecuteNonQuery(Misc.connStr, CommandType.StoredProcedure, "spUpdateUserPassword", param);
        }
        catch (Exception ex)
        {
            return 0;
        }
        return result;
    }


    public static int UpdateUserAccountInfo(BLLUser obj)
    {
        int result = 0;
        try
        {
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@userID", obj.userId);
            param[1] = new SqlParameter("@userPassword", obj.userPassword);
            param[2] = new SqlParameter("@Email", obj.email);
            result = SqlHelper.ExecuteNonQuery(Misc.connStr, CommandType.StoredProcedure, "spUpdateUserAccountInfo", param);
        }
        catch (Exception ex)
        {
            return 0;
        }
        return result;
    }


    

    public static int updateUserInfoByUsername(BLLUser obj, string strUserNameOld)
    {
        int result = 0;
        try
        {
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@userName", obj.userName);
            param[1] = new SqlParameter("@userNameOld", strUserNameOld);
            param[2] = new SqlParameter("@userPassword", obj.userPassword);
            result = SqlHelper.ExecuteNonQuery(Misc.connStr, CommandType.StoredProcedure, "spUpdateUserByUsername", param);
        }
        catch (Exception ex)
        {
            return 0;
        }

        return result;
    }

    public static int updateUserByID(BLLUser obj)
    {
        int result = 0;
        try
        {
            SqlParameter[] param = new SqlParameter[20];


            param[0] = new SqlParameter("@userID", obj.userId);
            param[1] = new SqlParameter("@userPassword", obj.userPassword);
            param[2] = new SqlParameter("@userName", obj.userName);
            param[3] = new SqlParameter("@email", obj.email);
            param[4] = new SqlParameter("@referralId", obj.referralId);
            param[5] = new SqlParameter("@firstName", obj.firstName);
            param[6] = new SqlParameter("@lastName", obj.lastName);
            param[7] = new SqlParameter("@friendsReferralId", obj.friendsReferralId);
            if (obj.countryId != 0)
            {
                param[8] = new SqlParameter("@countryId", obj.countryId);
            }
            else
            {
                param[8] = new SqlParameter("@countryId", DBNull.Value);
            }
            if (obj.provinceId != 0)
            {
                param[9] = new SqlParameter("@provinceId", obj.provinceId);
            }
            else
            {
                param[9] = new SqlParameter("@provinceId", DBNull.Value);
            }            
            param[10] = new SqlParameter("@modifiedDate", obj.modifiedDate);
            param[11] = new SqlParameter("@modifiedBy", obj.modifiedBy);
            param[12] = new SqlParameter("@profilePicture", obj.profilePicture);
            param[13] = new SqlParameter("@phoneNo", obj.phoneNo);
            
            param[15] = new SqlParameter("@gender", obj.gender);
            param[16] = new SqlParameter("@age", obj.age);
            param[17] = new SqlParameter("@cityId", obj.cityId);
            param[18] = new SqlParameter("@zipcode", obj.zipcode);
            param[19] = new SqlParameter("@howYouKnowUs", obj.howYouKnowUs);
            result = SqlHelper.ExecuteNonQuery(Misc.connStr, CommandType.StoredProcedure, "spUpdateUserByID", param);
        }
        catch (Exception ex)
        {
            return 0;
        }
        return result;
    }

    public static int updateUserFirstAndLastNameByID(BLLUser obj)
    {
        int result = 0;
        try
        {
            SqlParameter[] param = new SqlParameter[3];            
            param[0] = new SqlParameter("@userID", obj.userId);
            param[1] = new SqlParameter("@firstName", obj.firstName);
            param[2] = new SqlParameter("@lastName", obj.lastName);
            result = SqlHelper.ExecuteNonQuery(Misc.connStr, CommandType.StoredProcedure, "spUpdateUserFirstAndLastNameByID", param);
        }
        catch (Exception ex)
        {
            return 0;
        }
        return result;
    }

    public static int updateUserTypeByUserName(BLLUser obj)
    {
        int result = 0;
        try
        {
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@userName", obj.userName);
            param[1] = new SqlParameter("@userPassword", obj.userPassword);
            result = Convert.ToInt32(SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spUpdateUserTypeByUserName", param).Tables[0].Rows[0][0]);
        }
        catch (Exception ex)
        {
            return 0;
        }
        return result;
    }


    public static DataTable getAllUsers()
    {
        DataTable dt = null;
        try 
        {

            dt = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetAllUsers").Tables[0];
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

    public static DataSet getAllUsersWithIndexing(int intStartIndex, int intMaxRecords)
    {
        DataSet dst = null;
        try
        {
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@startRowIndex", intStartIndex);
            param[1] = new SqlParameter("@maximumRows", intMaxRecords);
            dst = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetAllUsersWithIndexing", param);
        }
        catch (Exception ex)
        {
            return null;
        }
        return dst;
    }

    public static DataTable getAllInActiveUsers()
    {
        DataTable dt = null;
        try
        {

            dt = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetAllInActiveUsers").Tables[0];
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

    public static DataTable getMemberUsers()
    {
        DataTable dt = null;
        try
        {
            dt = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetMemberUsers").Tables[0];
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

    public static DataTable getMemberResturantAndSalesUsers()
    {
        DataTable dt = null;
        try
        {
            dt = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetMemberResturantAndSalesUsers").Tables[0];
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

    public static bool deleteUser(BLLUser obj)
    {
        int result = 0;
        try
        {
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@userID", obj.userId);
            param[1] = new SqlParameter("@deletedDate", obj.deletedDate);
            result = SqlHelper.ExecuteNonQuery(Misc.connStr, CommandType.StoredProcedure, "spDeleteUser", param);
            if (result == -1)
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

    public static bool changeUserStatus(BLLUser obj)
    {
        int result = 0;
        try
        {
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@userID", obj.userId);
            param[1] = new SqlParameter("@isActive", obj.isActive);
            result = SqlHelper.ExecuteNonQuery(Misc.connStr, CommandType.StoredProcedure, "spChangeUserStatus", param);
            if (result == -1)
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

    public static bool changeUserAffiliateStatus(BLLUser obj)
    {
        int result = 0;
        try
        {
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@userID", obj.userId);
            param[1] = new SqlParameter("@isAffiliate", obj.isAffiliate);
            result = SqlHelper.ExecuteNonQuery(Misc.connStr, CommandType.StoredProcedure, "spChangeUserAffiliateStatus", param);
            if (result == -1)
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




    public static DataTable getMemberUserByEmail(BLLUser obj)
    {
        DataTable dt = null;
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@email", obj.email);
            dt = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetMemberUserByEmail", param).Tables[0];
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

    public static DataTable getUserDetailByEmail(BLLUser obj)
    {
        DataTable dt = null;
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@email", obj.email);
            dt = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetUserDetailByEmail", param).Tables[0];
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
    
    public static DataTable getUserByID(BLLUser obj)
    {
        DataTable dt = null;
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@userID", obj.userId);
            dt = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetUserById", param).Tables[0];
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
    
    public static bool getUserByEmail(BLLUser obj)
    {
        DataTable dt = null;
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@email", obj.email);
            dt = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetUserByEmail", param).Tables[0];
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



    public static bool getResturantOwnerUserByUserName(BLLUser obj)
    {
        DataTable dt = null;
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@userName", obj.userName);
            dt = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetResturantOwnerUserByUserName", param).Tables[0];
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
    
    public static bool getUserByUserName(BLLUser obj)
    {
        DataTable dt = null;
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@userName", obj.userName);
            dt = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetUserByUserName", param).Tables[0];
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

    public static int getUserIdByUserName(BLLUser obj)
    {
        int iUserId = 0;
        try
        {
            DataTable dt = null;

            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@userName", obj.userName);
            dt = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetUserByUserName", param).Tables[0];

            if (dt != null && dt.Rows.Count > 0)
            {
                iUserId = int.Parse(dt.Rows[0]["userID"].ToString().Trim());
            }
        }
        catch (Exception ex)
        {
            return iUserId;
        }

        return iUserId;
    }

    public static bool getUserByUserNameForUpdate(BLLUser obj, string strUserName)
    {
        DataTable dt = null;
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@userName", obj.userName);
            dt = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetUserByUserName", param).Tables[0];
            if (dt != null && dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["userName"].ToString().ToLower().Equals(strUserName.ToLower().Trim()))
                {
                    return true;
                }
                else
                {
                    return false;
                }
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

    public static bool getUserByReferralId(BLLUser obj)
    {
        DataTable dt = null;
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@referralId", obj.referralId);
            dt = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetUserByReferralId", param).Tables[0];
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
    
    public static DataTable getUserByReferralIdForPayment(BLLUser obj)
    {
        DataTable dt = null;
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@referralId", obj.referralId);
            dt = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetUserByReferralIdForPayment", param).Tables[0];
        }
        catch (Exception ex)
        {
            return null;
        }
        return dt;
    }

    public static bool getMemberUserByReferralId(BLLUser obj)
    {
        DataTable dt = null;
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@referralId", obj.referralId);
            dt = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetMemberUserByReferralId", param).Tables[0];
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

    public static bool getMemberUserWithNoChildByReferralId(BLLUser obj)
    {
        DataTable dt = null;
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@referralId", obj.referralId);
            dt = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetMemberUserWithNoChildByReferralId", param).Tables[0];
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
    
    public static DataTable getUserByFriendsReferralId(BLLUser obj)
    {
        DataTable dt = null;
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@friendsReferralId", obj.friendsReferralId);
            dt = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetUserByFriendsReferralId", param).Tables[0];          
                return dt;           
        }
        catch (Exception ex)
        {
            return dt;
        }
    }

    public static DataTable getGetAffiliatePartnerMembers(BLLUser obj)
    {
        DataTable dt = null;
        try
        {
            dt = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetAffiliatePartnerMembers").Tables[0];
            return dt;
        }
        catch (Exception ex)
        {
            return dt;
        }
    }

    public static DataTable GetAllSalesAccountNames()
    {
        DataTable dt = null;
        try
        {
            dt = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetAllSalesAccountNames").Tables[0];
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


    //New (02-01-2012)
    public static DataTable GetUserNotesByUserID(BLLUser obj)
    {
        DataTable dt = null;
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@UserID", obj.userId);
            dt = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "Sp_GetUserNotesByUserID", param).Tables[0];
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

    public static int AddUpdateUserNotes(BLLUser obj)
    {
        int result = 0;
        try
        {
            SqlParameter[] param = new SqlParameter[2];


            param[0] = new SqlParameter("@userID", obj.userId);
            param[1] = new SqlParameter("@Notes", obj.Notes);
            result = SqlHelper.ExecuteNonQuery(Misc.connStr, CommandType.StoredProcedure, "Sp_AddUserNotes", param);
        }
        catch (Exception ex)
        {
            return 0;
        }
        return result;
    }
    public static DataTable GetAllSalesUsers()
    {
        DataTable dt = null;
        try
        {

            dt = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetAllSalesUsers").Tables[0];
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


    public static DataTable GetSalePersonBounsAndAdjustment(BLLUser obj)
    {
        DataTable dt = null;
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@Email", obj.email);
            dt = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "SP_GetSalePersonBounsAndAdjustment", param).Tables[0];
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
    public static int AddUpdateSalePersonBonus(BLLUser obj)
    {
        int result = 0;
        try
        {
            SqlParameter[] param = new SqlParameter[2];


            param[0] = new SqlParameter("@Email", obj.email);
            param[1] = new SqlParameter("@Bonus", obj.Bonus);
            result = SqlHelper.ExecuteNonQuery(Misc.connStr, CommandType.StoredProcedure, "SP_AddUpdateSalePersonBonus", param);
        }
        catch (Exception ex)
        {
            return 0;
        }
        return result;
    }

    public static int AddUpdateSalePersonAdjustment(BLLUser obj)
    {
        int result = 0;
        try
        {
            SqlParameter[] param = new SqlParameter[2];


            param[0] = new SqlParameter("@Email", obj.email);
            param[1] = new SqlParameter("@Adjustment", obj.Adjustment);
            result = SqlHelper.ExecuteNonQuery(Misc.connStr, CommandType.StoredProcedure, "SP_AddUpdateSalePersonAdjustment", param);
        }
        catch (Exception ex)
        {
            return 0;
        }
        return result;
    }

    public static int UserUpdateWhoTab(BLLUser obj)
    {
        int result = 0;
        try
        {
            SqlParameter[] param = new SqlParameter[6];
            param[0] = new SqlParameter("@UserID", obj.userId);
            param[1] = new SqlParameter("@FirstName", obj.firstName);
            param[2] = new SqlParameter("@LastName", obj.lastName);
            param[3] = new SqlParameter("@DealsPreferFor", obj.DealsPreferfor);
            param[4] = new SqlParameter("@DateOfBirth", obj.DateOfBirth);
            param[5] = new SqlParameter("@ZipCode", obj.zipcode);
            result = SqlHelper.ExecuteNonQuery(Misc.connStr, CommandType.StoredProcedure, "sp_UserUpdateWhoTab", param);
        }
        catch (Exception ex)
        {
            return 0;
        }
        return result;
    }


    public static int UpdateUserFBShare(BLLUser obj)
    {
        int result = 0;
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@UserID", obj.userId);
            result = SqlHelper.ExecuteNonQuery(Misc.connStr, CommandType.StoredProcedure, "sp_UpdateUserFBShare", param);
        }
        catch (Exception ex)
        {
            return 0;
        }
        return result;
    }
}
