using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

/// <summary>
/// Summary description for BLLRequestChecker
/// </summary>
public class BLLRequestChecker
{
    private long RequestIPID;
    private string RequestIP;
    private DateTime RequestDate;
	public BLLRequestChecker()
	{
        RequestIPID = 0;
        RequestIP = "";
        RequestDate = DateTime.Now;
	}
    public long requestIpID
    {
        get { return RequestIPID; }
        set { RequestIPID = value; }
    }
    public string requestIP
    {
        get { return RequestIP; }
        set { RequestIP = value; }
    }
    public DateTime requestDate
    {
        get { return RequestDate; }
        set { RequestDate = value; }
    }

    public DataTable AddRequestToRequestChecker()
    {
        return DALRequestChecker.AddRequestToRequestChecker(this);
    }
    public int DeleteFromRequestChecker()
    {
        return DALRequestChecker.DeleteFromRequestChecker(this);
    }
    public DataTable CheckIPIsBlocked()
    {
        return DALRequestChecker.CheckIPIsBlocked(this);
    }
}