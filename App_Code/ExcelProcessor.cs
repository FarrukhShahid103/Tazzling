using System.Linq;
using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.OleDb;
using System.Collections.Generic;
using System.Text;
/// <summary>
/// Summary description for ExcelProcessor
/// </summary>
public static class ExcelProcessor
{
    public static string GetConnectionString(string fileName)
    {
        return GetConnectionString(fileName, false);
    }

    public static string GetConnectionString(string fileName, bool forWrite)
    {
        string connect = string.Empty;
        string imex = forWrite ? "0" : "1";
        if (fileName.ToLower().EndsWith(".xls"))
            connect = "Provider=Microsoft.Jet.OLEDB.4.0;" + "Data Source={0};" + "Extended Properties=\"Excel 8.0;HDR=NO;IMEX=" + imex + "\"";
        else if (fileName.ToLower().EndsWith(".xlsx"))
            connect = "Provider=Microsoft.ACE.OLEDB.12.0;" + "Data Source={0};" + "Extended Properties=\"Excel 12.0;HDR=NO;IMEX=" + imex + "\"";
        return string.Format(connect, fileName);
    }

    /// <summary>
    /// You need to know the sheet name to form sql string.
    /// sql can be "SELECT * FROM [Sheet1$]";//[Sheet1$A1:B10]
    /// </summary>
    /// <param name="Path"></param>
    /// <param name="sql"></param>
    /// <returns></returns>
    public static DataSet ExcelToDS(string path, string sql)
    {
        string strConn = GetConnectionString(path);
        OleDbDataAdapter myCommand = new OleDbDataAdapter(sql, strConn);
        DataSet ds = new DataSet();
        myCommand.Fill(ds, "table1");
        return ds;
    }

    /// <summary>
    /// Return all sheets as tables in the excel
    /// </summary>
    /// <param name="Path"></param>
    /// <returns></returns>
    public static DataSet ExcelToDS(string path)
    {
        string strConn = GetConnectionString(path);
        string sql_F = "SELECT * FROM [{0}]";

        OleDbConnection conn = null;
        OleDbDataAdapter da = null;
        DataTable tblSchema = null;
        IList<string> tblNames = null;

        //Init conn and open
        conn = new OleDbConnection(strConn);
        conn.Open();

        //Get tables names from excel
        tblSchema = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new object[] { null, null, null, "TABLE" });

        tblNames = new List<string>();
        foreach (DataRow row in tblSchema.Rows)
        {
            tblNames.Add((string)row["TABLE_NAME"]);
        }

        //Init adapter
        da = new OleDbDataAdapter();

        DataSet ds = new DataSet();

        foreach (string tblName in tblNames)
        {
            da.SelectCommand = new OleDbCommand(String.Format(sql_F, tblName), conn);
            try
            {
                da.Fill(ds, tblName);
            }
            catch(Exception ex)
            {
                //close connection
                if (conn.State == ConnectionState.Open)
                    conn.Close();
                throw;
            }
        }

        //close connection
        if (conn.State == ConnectionState.Open)
            conn.Close();

        return ds;
    }

    public static string DataSetToExcel(DataSet ds, string path)
    {
        string result = string.Empty;

        string connString = GetConnectionString(path, true);
        OleDbConnection objConn = new OleDbConnection(connString);
        try
        {
            objConn.Open();
        }
        catch (Exception ex)
        {
            result = "Connect Excel file error: " + "There is an error occur, please email us at support@tazzling.com or call 1855-295-1771.";
            return result;
        }

        for (int i = 0; i < ds.Tables.Count; i++)
        {
            DataTable dt = ds.Tables[i];
            result += "<br />No." + i.ToString() + " " + DataTableToExcel(dt, path, objConn);
        }

        if (objConn.State == ConnectionState.Open)
            objConn.Close();

        return result;
    }

    public static string DataTableToExcel(DataTable dt, string path)
    {
        return DataTableToExcel(dt, path, null);
    }

    public static string DataTableToExcel(DataTable dt, string path, OleDbConnection _objConn)
    {
        if (dt == null)
            return "DataTable can not be empty.";

        int rows = dt.Rows.Count;
        int cols = dt.Columns.Count;
        StringBuilder sb = new StringBuilder();

        if (rows == 0)
            return "No Data.";

        sb.Append("CREATE TABLE ");
        sb.Append(dt.TableName + " ( ");

        for (int i = 0; i < cols; i++)
        {
            if (i < cols - 1)
                sb.Append(string.Format("{0} varchar,", dt.Columns[i].ColumnName));
            else
                sb.Append(string.Format("{0} varchar)", dt.Columns[i].ColumnName));
        }

        OleDbConnection objConn;
        if (_objConn == null)
        {
            string connString = GetConnectionString(path, true);
            objConn = new OleDbConnection(connString);
        }
        else
            objConn = _objConn;

        OleDbCommand objCmd = new OleDbCommand();
        objCmd.Connection = objConn;

        objCmd.CommandText = sb.ToString();

        try
        {
            if (_objConn == null)
            {
                objConn.Open();
            }
            objCmd.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            if (_objConn == null)
            {
                if (objConn.State == ConnectionState.Open)
                    objConn.Close();
            }
            return "Create table in Excel failed. Error:" + "There is an error occur, please email us at support@tazzling.com or call 1855-295-1771.";
        }

        sb.Remove(0, sb.Length);
        sb.Append("INSERT INTO [");
        sb.Append(dt.TableName + "] ( ");

        for (int i = 0; i < cols; i++)
        {
            if (i < cols - 1)
                sb.Append(dt.Columns[i].ColumnName + ",");
            else
                sb.Append(dt.Columns[i].ColumnName + ") values (");
        }

        for (int i = 0; i < cols; i++)
        {
            if (i < cols - 1)
                sb.Append("@" + dt.Columns[i].ColumnName + ",");
            else
                sb.Append("@" + dt.Columns[i].ColumnName + ")");
        }

        objCmd.CommandText = sb.ToString();
        OleDbParameterCollection param = objCmd.Parameters;

        for (int i = 0; i < cols; i++)
        {
            param.Add(new OleDbParameter("@" + dt.Columns[i].ColumnName, OleDbType.VarChar));
        }

        foreach (DataRow row in dt.Rows)
        {
            for (int i = 0; i < param.Count; i++)
            {
                param[i].Value = row[i];
            }
            try
            {
                objCmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                
            }
        }

        if (_objConn == null)
        {
            if (objConn.State == ConnectionState.Open)
                objConn.Close();
        }

        return "Insert table done.";
    }
}
