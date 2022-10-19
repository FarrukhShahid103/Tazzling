using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using SQLHelper;

/// <summary>
/// Summary description for DALNewsLetterSubscriber
/// </summary>
public class DALNewsLetterSubscriber
{
    public static DataTable getAllNewsLetterSubscribers()
    {
        DataTable dtNewsLetter = null;
        try
        {
            dtNewsLetter = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetAllNewsLetterSubscribers").Tables[0];
        }
        catch (Exception ex)
        {

        }
        return dtNewsLetter;
    }

    public static DataTable getAllSubscribesCityByUserID(BLLNewsLetterSubscriber obj)
    {
        DataTable dt = null;
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@userId", obj.userId);
            dt = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetAllSubscribesCityByUserID", param).Tables[0];
        }
        catch (Exception ex)
        {

        }
        return dt;
    }
   
    public static int deleteNewsLetterSubscriberById(BLLNewsLetterSubscriber objBLLNewsLetterSubscriber)
    {
        int result = 0;

        try
        {
            SqlParameter[] param = new SqlParameter[1];

            param[0] = new SqlParameter("@sID", objBLLNewsLetterSubscriber.SId);

            result = SqlHelper.ExecuteNonQuery(Misc.connStr, CommandType.StoredProcedure, "spDeleteNewsLetterSubscriberById", param);
        }
        catch (Exception ex)
        {

        }
        return result;
    }

    public static int unsubscribeAllCityByEmail(BLLNewsLetterSubscriber objBLLNewsLetterSubscriber)
    {
        int result = 0;

        try
        {
            SqlParameter[] param = new SqlParameter[1];

            param[0] = new SqlParameter("@email", objBLLNewsLetterSubscriber.Email);

            result = SqlHelper.ExecuteNonQuery(Misc.connStr, CommandType.StoredProcedure, "spUnsubscribeAllCityByEmail", param);
        }
        catch (Exception ex)
        {

        }
        return result;
    }


    public static int updateNewsLetterSubscriberById(BLLNewsLetterSubscriber objBLLNewsLetterSubscriber)
    {
        int result = 0;

        try
        {
            SqlParameter[] param = new SqlParameter[5];

            param[0] = new SqlParameter("@sID", objBLLNewsLetterSubscriber.SId);

            param[1] = new SqlParameter("@email", objBLLNewsLetterSubscriber.Email);

            param[2] = new SqlParameter("@status", objBLLNewsLetterSubscriber.Status);

            param[3] = new SqlParameter("@cityId", objBLLNewsLetterSubscriber.CityId);

            param[4] = new SqlParameter("@ModifiedDateTime", objBLLNewsLetterSubscriber.ModifiedDate);

            result = SqlHelper.ExecuteNonQuery(Misc.connStr, CommandType.StoredProcedure, "spUpdateNewsLetterSubscriberById", param);
        }
        catch (Exception ex)
        { }

        return result;
    }

    public static int createNewsLetterSubscriber(BLLNewsLetterSubscriber objBLLNewsLetterSubscriber)
    {
        int result = 0;

        try
        {
            SqlParameter[] param = new SqlParameter[5];

            param[0] = new SqlParameter("@email", objBLLNewsLetterSubscriber.Email);
            param[1] = new SqlParameter("@status", objBLLNewsLetterSubscriber.Status);
            param[2] = new SqlParameter("@cityId", objBLLNewsLetterSubscriber.CityId);
            param[3] = new SqlParameter("@CreateDateTime", objBLLNewsLetterSubscriber.CreatedDate);
            param[4] = new SqlParameter("@friendsReferralId", objBLLNewsLetterSubscriber.friendsReferralId); 
            result = SqlHelper.ExecuteNonQuery(Misc.connStr, CommandType.StoredProcedure, "spCreateNewsLetterSubscriber", param);
        }
        catch (Exception ex)
        { }

        return result;
    }

    public static bool changeSubscriberStatus(BLLNewsLetterSubscriber obj)
    {
        int result = 0;
        try
        {
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@SId", obj.SId);
            param[1] = new SqlParameter("@Status", obj.Status);
            result = SqlHelper.ExecuteNonQuery(Misc.connStr, CommandType.StoredProcedure, "spChangeSubscriberStatus", param);
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

    public static DataTable getNewsLetterSubscriberById(BLLNewsLetterSubscriber objBLLNewsLetterSubscriber)
    {
        DataTable dtNewsLetter = null;

        try
        {
            SqlParameter[] param = new SqlParameter[1];

            param[0] = new SqlParameter("@sID", objBLLNewsLetterSubscriber.SId);

            dtNewsLetter = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetNewsLetterSubscriberById", param).Tables[0];
        }
        catch (Exception ex)
        { }

        return dtNewsLetter;
    }

    public static DataTable getNewsLetterSubscriberByCityId(BLLNewsLetterSubscriber objBLLNewsLetterSubscriber)
    {
        DataTable dtNewsLetter = null;

        try
        {
            SqlParameter[] param = new SqlParameter[1];

            param[0] = new SqlParameter("@CityId", objBLLNewsLetterSubscriber.CityId);

            dtNewsLetter = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetNewsLetterSubscriberByCityId", param).Tables[0];
        }
        catch (Exception ex)
        { }

        return dtNewsLetter;
    }

    public static DataTable getNewsLetterSubscriberByEmailCityId(BLLNewsLetterSubscriber objBLLNewsLetterSubscriber)
    {
        DataTable dtNewsLetter = null;

        try
        {
            SqlParameter[] param = new SqlParameter[2];

            param[0] = new SqlParameter("@email", objBLLNewsLetterSubscriber.Email);
            param[1] = new SqlParameter("@cityId", objBLLNewsLetterSubscriber.CityId);

            dtNewsLetter = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetNewsLetterSubsByEmailCityId", param).Tables[0];
        }
        catch (Exception ex)
        { }

        return dtNewsLetter;
    }
    public static DataTable getNewsLetterSubscriberByEmailCityId2(BLLNewsLetterSubscriber objBLLNewsLetterSubscriber)
    {
        DataTable dtNewsLetter = null;

        try
        {
            SqlParameter[] param = new SqlParameter[2];

            param[0] = new SqlParameter("@email", objBLLNewsLetterSubscriber.Email);
            param[1] = new SqlParameter("@cityId", objBLLNewsLetterSubscriber.CityId);

            dtNewsLetter = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetNewsLetterSubsByEmailCityId2", param).Tables[0];
        }
        catch (Exception ex)
        { }

        return dtNewsLetter;
    }
}