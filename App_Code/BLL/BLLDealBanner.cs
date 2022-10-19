using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

/// <summary>
/// Summary description for BLLCities
/// </summary>
public class BLLDealBanner
{
    private long Banner_ID;        
    private string Banner_link;
    private string Banner_image;
    private DateTime Banner_startTime;
    private DateTime Banner_endTime;
    private int Banner_city;
   
    public BLLDealBanner()
    {        
        Banner_ID = 0;     
        Banner_link = "";
        Banner_image = "";
        Banner_startTime = DateTime.Now;
        Banner_endTime = DateTime.Now;
        Banner_city = 337;
    }

    public int banner_city
    {
        get { return Banner_city; }
        set { Banner_city = value; }
    }


    public DateTime banner_startTime
    {
        get { return Banner_startTime; }
        set { Banner_startTime = value; }
    }

    public DateTime banner_endTime
    {
        get { return Banner_endTime; }
        set { Banner_endTime = value; }
    }  

    public long banner_ID 
    {
        get { return Banner_ID; }
        set { Banner_ID = value; }
    }  

    public string banner_link
    {
        get { return Banner_link; }
        set { Banner_link = value; }
    }

    public string banner_image
    {
        get { return Banner_image; }
        set { Banner_image = value; }
    }

    public int createDealBanner()
    {
        return  DALDealBanner.createDealBanner(this);
    }

    public int updateDealBanner()
    {
        return DALDealBanner.updateDealBanner(this);
    }

    public int deleteDealBanner()
    {
        return DALDealBanner.deleteDealBanner(this);
    }

    public DataTable getAllDealBanner()
    {
        return DALDealBanner.getAllDealBanner();
    }

    public DataTable getDealBannerByStartTime()
    {
        return DALDealBanner.getDealBannerByStartTime(this);
    }

    public DataTable getDealBannerByID()
    {
        return DALDealBanner.getDealBannerByID(this);
    }

      
}
