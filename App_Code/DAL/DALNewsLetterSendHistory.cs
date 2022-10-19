using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using SQLHelper;

/// <summary>
/// Summary description for DALNewsLetterSendHistory
/// </summary>
public class DALNewsLetterSendHistory
{
    public static DataTable getAllNewsLetterSentHistory()
    {
        DataTable dtNewsLetter = null;
        try
        {
            dtNewsLetter = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetAllNewsLetterSendHistory").Tables[0];
        }
        catch (Exception ex)
        {

        }
        return dtNewsLetter;
    }

    public static int deleteNewsLetterSentHistoryById(BLLNewsLetterSendHistory objBLLNewsLetterSendHistory)
    {
        int result = 0;

        try
        {
            SqlParameter[] param = new SqlParameter[1];

            param[0] = new SqlParameter("@nlhID", objBLLNewsLetterSendHistory.NlhId);

            result = SqlHelper.ExecuteNonQuery(Misc.connStr, CommandType.StoredProcedure, "spDeleteNewsLetterSendHistoryByID", param);
        }
        catch (Exception ex)
        {

        }
        return result;
    }

    public static int updateNewsLetterSentHistoryById(BLLNewsLetterSendHistory objBLLNewsLetterSendHistory)
    {
        int result = 0;

        try
        {
            SqlParameter[] param = new SqlParameter[5];

            param[0] = new SqlParameter("@nlhID", objBLLNewsLetterSendHistory.NlhId);

            param[1] = new SqlParameter("@sID", objBLLNewsLetterSendHistory.SId);

            param[2] = new SqlParameter("@nlId", objBLLNewsLetterSendHistory.NlId);

            param[3] = new SqlParameter("@SentBy", objBLLNewsLetterSendHistory.SendBy);

            param[4] = new SqlParameter("@SendDate", objBLLNewsLetterSendHistory.SendDate);

            result = SqlHelper.ExecuteNonQuery(Misc.connStr, CommandType.StoredProcedure, "spUpdateNewsLetterSendHistoryByID", param);
        }
        catch (Exception ex)
        { }

        return result;
    }

    public static int createNewsLetterSentHistory(BLLNewsLetterSendHistory objBLLNewsLetterSendHistory)
    {
        int result = 0;

        try
        {
            SqlParameter[] param = new SqlParameter[4];

            param[0] = new SqlParameter("@sID", objBLLNewsLetterSendHistory.SId);

            param[1] = new SqlParameter("@nlID", objBLLNewsLetterSendHistory.NlId);

            param[2] = new SqlParameter("@sentBy", objBLLNewsLetterSendHistory.SendBy);

            param[3] = new SqlParameter("@SendDate", objBLLNewsLetterSendHistory.SendDate);

            result = SqlHelper.ExecuteNonQuery(Misc.connStr, CommandType.StoredProcedure, "spCreateNewsLetterSendHistory", param);
        }
        catch (Exception ex)
        { }

        return result;
    }
}
