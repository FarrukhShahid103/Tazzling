using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

/// <summary>
/// Summary description for BLLCities
/// </summary>
public class BLLDeviceVersionInformation
{
    private string DeviceType;
    private string NewVersion;
    private string Title;
    private string Message;
    public BLLDeviceVersionInformation()
    {
        DeviceType = "";
        NewVersion = "";
        Title = "";
        Message = "";
    }

    public string deviceType 
    {
        get { return DeviceType; }
        set { DeviceType = value; }
    }

    public string newVersion
    {
        get { return NewVersion; }
        set { NewVersion = value; }
    }
    public string title
    {
        get { return Title; }
        set { Title = value; }
    }
    public string message
    {
        get { return Message; }
        set { Message = value; }
    }



    public int createUpdateDeviceVersionInformation()
    {
        return DALDeviceVersionInformation.createUpdateDeviceVersionInformation(this);
    }

    public DataTable getDeviceVersionInformationByDeviceType()
    {
        return DALDeviceVersionInformation.getDeviceVersionInformationByDeviceType(this);
    }

}
