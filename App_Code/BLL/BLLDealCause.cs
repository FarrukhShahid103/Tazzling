using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

/// <summary>
/// Summary description for BLLCities
/// </summary>
public class BLLDealCause
{
    private long Cause_ID;
    private long DealId;
    private string Cause_title;
    private string Cause_shortDescription;
    private string Cause_longDescription;
    private string Cause_link;
    private string Cause_image;
    private DateTime Cause_startTime;
    private DateTime Cause_endTime;
    private int Cause_city;
   
    public BLLDealCause()
    {
        DealId = 0;
        Cause_ID = 0;
        Cause_title = "";
        Cause_shortDescription = "";
        Cause_longDescription = "";
        Cause_link = "";
        Cause_image = "";
        Cause_startTime = DateTime.Now;
        Cause_endTime = DateTime.Now;
        Cause_city = 337;
    }

    public int cause_city
    {
        get { return Cause_city; }
        set { Cause_city = value; }
    }


    public DateTime cause_startTime
    {
        get { return Cause_startTime; }
        set { Cause_startTime = value; }
    }

    public DateTime cause_endTime
    {
        get { return Cause_endTime; }
        set { Cause_endTime = value; }
    }

    public long dealId
    {
        get { return DealId; }
        set { DealId = value; }
    }

    public long cause_ID 
    {
        get { return Cause_ID; }
        set { Cause_ID = value; }
    }

    public string cause_title
    {
        get { return Cause_title; }
        set { Cause_title = value; }
    }
    public string cause_shortDescription
    {
        get { return Cause_shortDescription; }
        set { Cause_shortDescription = value; }
    }
    public string cause_longDescription
    {
        get { return Cause_longDescription; }
        set { Cause_longDescription = value; }
    }

    public string cause_link
    {
        get { return Cause_link; }
        set { Cause_link = value; }
    }

    public string cause_image
    {
        get { return Cause_image; }
        set { Cause_image = value; }
    }

    public int createDealCause()
    {
        return  DALDealCause.createDealCause(this);
    }

    public int updateDealCause()
    {
        return DALDealCause.updateDealCause(this);
    }

    public int deleteDealCause()
    {
        return DALDealCause.deleteDealCause(this);
    }

    public DataTable getAllDealCause()
    {
        return DALDealCause.getAllDealCause();
    }

    public DataTable getDealCauseByStartTime()
    {
        return DALDealCause.getDealCauseByStartTime(this);
    }

    public DataTable getDealCauseByID()
    {
        return DALDealCause.getDealCauseByID(this);
    }

    public DataTable getDealCauseByDealID()
    {
        return DALDealCause.getDealCauseByDealID(this);
    }

  
}
