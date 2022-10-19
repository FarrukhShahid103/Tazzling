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
public static class DALContestWinner
{
    public static int createContestWinner(BLLContestWinner obj)
    {
        int result = 0;
        try 
        {
            SqlParameter[] param = new SqlParameter[5];
            param[0] = new SqlParameter("@contestWinner_endTime", obj.contestWinner_endTime);
            param[1] = new SqlParameter("@contestWinner_Image", obj.contestWinner_Image);
            param[2] = new SqlParameter("@contestWinner_Name", obj.contestWinner_Name);
            param[3] = new SqlParameter("@contestWinner_startTime", obj.contestWinner_startTime);
            param[4] = new SqlParameter("@contestWinner_Title", obj.contestWinner_Title);            

            result = SqlHelper.ExecuteNonQuery(Misc.connStr, CommandType.StoredProcedure, "spCreateContestWinner", param);
        }
        catch (Exception ex)
        { 

        }
        return result;
    }

    public static int updateContestWinner(BLLContestWinner obj)
    {
        int result = 0;
        try
        {
            SqlParameter[] param = new SqlParameter[6];
            param[0] = new SqlParameter("@contestWinner_endTime", obj.contestWinner_endTime);
            param[1] = new SqlParameter("@contestWinner_Image", obj.contestWinner_Image);
            param[2] = new SqlParameter("@contestWinner_Name", obj.contestWinner_Name);
            param[3] = new SqlParameter("@contestWinner_startTime", obj.contestWinner_startTime);
            param[4] = new SqlParameter("@contestWinner_Title", obj.contestWinner_Title);
            param[5] = new SqlParameter("@contestWinner_ID", obj.contestWinner_ID);            
            result = SqlHelper.ExecuteNonQuery(Misc.connStr, CommandType.StoredProcedure, "spUpdateContestWinner", param);
        }
        catch (Exception ex)
        {

        }
        return result;
    }

    public static int deleteContestWinner(BLLContestWinner obj)
    {
        int result = 0;
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@contestWinner_ID", obj.contestWinner_ID);
            result = SqlHelper.ExecuteNonQuery(Misc.connStr, CommandType.StoredProcedure, "spDeleteContestWinner", param);
        }
        catch (Exception ex)
        {

        }
        return result;
    }

    public static DataTable getContestWinnerByID(BLLContestWinner obj)
    {
        DataTable dt = null; 
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@contestWinner_ID", obj.contestWinner_ID);
            dt = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetContestWinnerByID", param).Tables[0];
        }
        catch (Exception ex)
        {

        }
        return dt;
    }  

    public static DataTable getAllContestWinner()
    {
        DataTable dt = null;
        try
        {
            dt = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetAllContestWinner").Tables[0];
        }
        catch (Exception ex)
        {

        }
        return dt;
    }
 
}
