using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

/// <summary>
/// Summary description for BLLCities
/// </summary>
public class BLLDeviceInfo
{
    private long DeviceID;
    private string DeviceToken;
    private int Notification_Counter;
    private bool iphone;
    private int CityId;
    public BLLDeviceInfo()
    {
        DeviceID = 0;
        DeviceToken = "";
        Notification_Counter = 0;
        iphone = true;
        CityId = 0;
    }

    public long deviceID 
    {
        get { return DeviceID; }
        set { DeviceID = value; }
    }

    public string deviceToken
    {
        get { return DeviceToken; }
        set { DeviceToken = value; }
    }

    public int notification_Counter
    {
        get { return Notification_Counter; }
        set { Notification_Counter = value; }
    }

    public int cityId
    {
        get { return CityId; }
        set { CityId = value; }
    }

    public bool iPhone
    {
        get { return iphone; }
        set { iphone = value; }
    }

    public int createAndUpdateDeviceInfo()
    {
        return DALDeviceInfo.createAndUpdateDeviceInfo(this);
    }

    public DataTable getAlliPhoneDevices()
    {
        return DALDeviceInfo.getAlliPhoneDevices();
    }

    public DataTable getAlliPhoneDevicesByCityID()
    {
        return DALDeviceInfo.getAlliPhoneDevicesByCityID(this);
    }

}
