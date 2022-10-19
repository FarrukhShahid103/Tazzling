using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

/// <summary>
/// Summary description for BLLRestaurantAvailableTime
/// </summary>
public class BLLRestaurantAvailableTime
{

    #region Private Variables
    private long TimeId;
    private long SettingID;
    private int DayOfWeek;
    private string Open1;
    private string Open2;
    private string Close1;
    private string Close2;
    private bool IsDay;
    private DateTime CreationDate;
    private long CreatedBy;
    private DateTime ModifiedDate;
    private long ModifiedBy;
    private long RestaurantID;
    #endregion

    #region Constructor
    public BLLRestaurantAvailableTime()
    {
        TimeId = 0;
        SettingID = 0;
        DayOfWeek = 0;
        Open1 = "";
        Open2 = "";
        Close1 = "";
        Close2 = "";
        IsDay = false;
        CreationDate = DateTime.Now;
        CreatedBy = 0;
        ModifiedDate = DateTime.Now;
        ModifiedBy = 0;
        RestaurantID = 0;
    }
    #endregion

    #region Properties
    public long restaurantID
    {
        set { RestaurantID = value; }
        get { return RestaurantID; }
    }

    public long timeId
    {
        set { TimeId = value; }
        get { return TimeId; }
    }

    public long settingID
    {
        set { SettingID = value; }
        get { return SettingID; }
    }

    public int dayOfWeek
    {
        set { DayOfWeek = value; }
        get { return DayOfWeek; }
    }

    public string open1
    {
        set { Open1 = value; }
        get { return Open1; }
    }

    public string open2
    {
        set { Open2 = value; }
        get { return Open2; }
    }

    public string close1
    {
        set { Close1 = value; }
        get { return Close1; }
    }

    public string close2
    {
        set { Close2 = value; }
        get { return Close2; }
    }

    public bool isDay
    {
        set { IsDay = value; }
        get { return IsDay; }
    }
  
    public long createdBy
    {
        set { CreatedBy = value; }
        get { return CreatedBy; }
    }

    public long modifiedBy
    {
        set { ModifiedBy = value; }
        get { return ModifiedBy; }
    }

    public DateTime creationDate
    {
        set { CreationDate = value; }
        get { return CreationDate; }
    }

    public DateTime modifiedDate
    {
        set { ModifiedDate = value; }
        get { return ModifiedDate; }
    }
   
    #endregion

    #region Functions
    public int createRestaurantAvailableTime()
    {
        return DALRestaurantAvailableTime.createRestaurantAvailableTime(this);
    }

    public int deleteRestaurantAvailableTime()
    {
        return DALRestaurantAvailableTime.deleteRestaurantAvailableTime(this);
    }

    public int updateRestaurantAvailableTime()
    {
        return DALRestaurantAvailableTime.updateRestaurantAvailableTime(this);
    }

    public DataTable getRestaurantAvailableTimeBySettingID()
    {
        return DALRestaurantAvailableTime.getRestaurantAvailableTimeBySettingID(this);
    }
    public DataTable getRestaurantAvailableTimeByRestaurantID()
    {
        return DALRestaurantAvailableTime.getRestaurantAvailableTimeByRestaurantID(this);
    }
    
    #endregion
}
