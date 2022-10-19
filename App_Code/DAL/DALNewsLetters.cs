using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using SQLHelper;

/// <summary>
/// Summary description for DALNewsLetters
/// </summary>
public class DALNewsLetters
{
    public static DataTable getAllNewsLetter()
    {
        DataTable dtNewsLetter = null;
        try
        {
            dtNewsLetter = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetAllNewsLetter").Tables[0];
        }
        catch (Exception ex)
        {

        }
        return dtNewsLetter;
    }

    public static int deleteNewsLetterById(BLLNewsLetters objBLLNewsLetters)
    {
        int result = 0;

        try
        {
            SqlParameter[] param = new SqlParameter[1];

            param[0] = new SqlParameter("@nlID", objBLLNewsLetters.nlId);

            result = SqlHelper.ExecuteNonQuery(Misc.connStr, CommandType.StoredProcedure, "spDeleteNewsLetterByNlID", param);
        }
        catch (Exception ex)
        {

        }
        return result;
    }

    public static int updateNewsLetterById(BLLNewsLetters objBLLNewsLetters)
    {
        int result = 0;
        
        try
        {
            SqlParameter[] param = new SqlParameter[5];
            
            param[0] = new SqlParameter("@nlID", objBLLNewsLetters.nlId);
            
            param[1] = new SqlParameter("@title", objBLLNewsLetters.title);
            
            param[2] = new SqlParameter("@newsLetter", objBLLNewsLetters.newsLetter);
            
            param[3] = new SqlParameter("@modifiedBy", objBLLNewsLetters.modifiedBy);
            
            param[4] = new SqlParameter("@modifiedDate", objBLLNewsLetters.modifiedDate);

            result = SqlHelper.ExecuteNonQuery(Misc.connStr, CommandType.StoredProcedure, "spUpdateNewsLetterByNlID", param);
        }
        catch (Exception ex)
        {}

        return result;
    }

    public static int createNewsLetter(BLLNewsLetters objBLLNewsLetters)
    {
        int result = 0;

        try
        {
            SqlParameter[] param = new SqlParameter[4];

            param[0] = new SqlParameter("@title", objBLLNewsLetters.title);

            param[1] = new SqlParameter("@newsLetter", objBLLNewsLetters.newsLetter);

            param[2] = new SqlParameter("@createdBy", objBLLNewsLetters.createdBy);

            param[3] = new SqlParameter("@creationDate", objBLLNewsLetters.createdDate);

            result = SqlHelper.ExecuteNonQuery(Misc.connStr, CommandType.StoredProcedure, "spCreateNewsLetter", param);
        }
        catch (Exception ex)
        { }

        return result;
    }

    public static DataTable getNewsLetterInfoById(BLLNewsLetters objBLLNewsLetters)
    {
        DataTable dtNewsLetter = null;

        try
        {
            SqlParameter[] param = new SqlParameter[1];

            param[0] = new SqlParameter("@nlID", objBLLNewsLetters.nlId);

            dtNewsLetter = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetNewsLetterInfoById", param).Tables[0];
        }
        catch (Exception ex)
        { }

        return dtNewsLetter;
    }
}