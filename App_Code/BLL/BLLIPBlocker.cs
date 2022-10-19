using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

/// <summary>
/// Summary description for BLLCities
/// </summary>
public class BLLIPBlocker
{
    private long blackListID;
    private string requestIP;
    private DateTime requestDate;
    
    public BLLIPBlocker()
    {
        blackListID = 0;
        requestIP = "";
        requestDate = DateTime.Now;        
    }

    public long BlackListID 
    {
        get { return blackListID; }
        set { blackListID = value; }
    }

    public string RequestIP
    {
        get { return requestIP; }
        set { requestIP = value; }
    }

    public DateTime RequestDate
    {
        get { return requestDate; }
        set { requestDate = value; }
    }

    public int createIPBlocker()
    {
        return DALIPBlocker.createIPBlocker(this);
    }

    public int deleteIPBlocker()
    {
        return DALIPBlocker.deleteIPBlocker(this);
    }

    public DataTable getAllIPBlocker()
    {
        return DALIPBlocker.getAllIPBlocker(this);
    }
  
}
