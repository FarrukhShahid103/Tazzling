using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

/// <summary>
/// Summary description for BLLCities
/// </summary>
public class BLLKarmaPoints
{
    private long KarmaPointsID;
    private int KarmaPoints;
    private string KarmaPointsType;
    private long UserId;
    private long CreatedBy;
    private DateTime CreatedDate;
    public BLLKarmaPoints()
    {
        KarmaPointsID = 0;
        KarmaPoints = 0;
        KarmaPointsType = "";
        UserId = 0;
        CreatedBy = 0;
        CreatedDate = DateTime.Now;
    }

    public long karmaPointsID 
    {
        get { return KarmaPointsID; }
        set { KarmaPointsID = value; }
    }

    public int karmaPoints
    {
        get { return KarmaPoints; }
        set { KarmaPoints = value; }
    }

    public string karmaPointsType
    {
        get { return KarmaPointsType; }
        set { KarmaPointsType = value; }
    }

    public long userId
    {
        get { return UserId; }
        set { UserId = value; }
    }

    public long createdBy
    {
        get { return CreatedBy; }
        set { CreatedBy = value; }
    }

    public DateTime createdDate
    {
        get { return CreatedDate; }
        set { CreatedDate = value; }
    }
    
    public int createKarmaPoints()
    {
        return DALKarmaPoints.createKarmaPoints(this);
    }

    public DataTable getKarmaPointsByUserId()
    {
        return DALKarmaPoints.getKarmaPointsByUserId(this);
    }

    public DataTable getKarmaPointsTotalByUserId()
    {
        return DALKarmaPoints.getKarmaPointsTotalByUserId(this);
    }

    public DataTable getKarmaTodayLoginPointsByUserId()
    {
        return DALKarmaPoints.getKarmaTodayLoginPointsByUserId(this);
    }
   
}
