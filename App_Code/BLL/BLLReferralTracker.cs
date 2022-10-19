using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

/// <summary>
/// Summary description for BLLCities
/// </summary>
public class BLLReferralTracker
{
    private long TrackerID;
    private string Email;
    private string TrackerName; 
    private DateTime TrackerDate;
    private long TrackBy;
    private DateTime SignupDate;
    private bool IsSignup;
    private long SignupID;
    public BLLReferralTracker()
    {
        SignupID = 0;
        TrackerID = 0;
        Email = "";
        TrackerName = "";
        TrackerDate = DateTime.Now;
        TrackBy = 0;
        SignupDate = DateTime.Now;
        IsSignup = false;        
    }


    public long signupID 
    {
        get { return SignupID; }
        set { SignupID = value; }
    }

    public long trackerID 
    {
        get { return TrackerID; }
        set { TrackerID = value; }
    }


    public string trackerName
    {
        get { return TrackerName; }
        set { TrackerName = value; }
    }

    public string email
    {
        get { return Email; }
        set { Email = value; }
    }

    public long trackBy
    {
        get { return TrackBy; }
        set { TrackBy = value; }
    }

    public DateTime trackerDate
    {
        get { return TrackerDate; }
        set { TrackerDate = value; }
    }

    public DateTime signupDate
    {
        get { return SignupDate; }
        set { SignupDate = value; }
    }
    public bool isSignup
    {
        get { return IsSignup; }
        set { IsSignup = value; }
    }

    public int createReferralTracker()
    {
        return DALReferralTracker.createReferralTracker(this);
    }

    public int updateReferralTrackerByTrackerIDAndEmail()
    {
        return DALReferralTracker.updateReferralTrackerByTrackerIDAndEmail(this);
    }

    public DataTable getReferralTrackerByTrackerID()
    {
        return DALReferralTracker.getReferralTrackerByTrackerID(this);
    }

    public DataTable getReferralTrackerByEmail()
    {
        return DALReferralTracker.getReferralTrackerByEmail(this);
    }
   
 
}
