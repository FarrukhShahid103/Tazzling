using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

/// <summary>
/// Summary description for BLLCities
/// </summary>
public class BLLContestWinner
{
    private long ContestWinner_ID;
    private string ContestWinner_Name;
    private string ContestWinner_Title;
    private string ContestWinner_Image;
    private DateTime ContestWinner_startTime;
    private DateTime ContestWinner_endTime;

    public BLLContestWinner()
    {
        ContestWinner_ID = 0;
        ContestWinner_Name = "";
        ContestWinner_Title = "";
        ContestWinner_Image = "";
        ContestWinner_startTime = DateTime.Now;
        ContestWinner_endTime = DateTime.Now;
    }

    public DateTime contestWinner_startTime
    { 
        get { return ContestWinner_startTime; }
        set { ContestWinner_startTime = value; }
    }

    public DateTime contestWinner_endTime
    {
        get { return ContestWinner_endTime; }
        set { ContestWinner_endTime = value; }
    }

    public long contestWinner_ID
    {
        get { return ContestWinner_ID; }
        set { ContestWinner_ID = value; }
    }

    public string contestWinner_Name 
    {
        get { return ContestWinner_Name; }
        set { ContestWinner_Name = value; }
    }

    public string contestWinner_Title
    {
        get { return ContestWinner_Title; }
        set { ContestWinner_Title = value; }
    }
    public string contestWinner_Image
    {
        get { return ContestWinner_Image; }
        set { ContestWinner_Image = value; }
    }


    public int createContestWinner()
    {
        return DALContestWinner.createContestWinner(this);
    }

    public int updateContestWinner()
    {
        return DALContestWinner.updateContestWinner(this);
    }

    public int deleteContestWinner()
    {
        return DALContestWinner.deleteContestWinner(this);
    }

    public DataTable getAllContestWinner()
    {
        return DALContestWinner.getAllContestWinner();
    }

    public DataTable getContestWinnerByID()
    {
        return DALContestWinner.getContestWinnerByID(this);
    }
      
}
