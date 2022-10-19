using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

/// <summary>
/// Summary description for BLLCities
/// </summary>
///
public class BLLCampaign
{

    #region Global Veriable
    private long CampaignID;
    private long RestaurantId;
    private string CampaignTitle;
    private string CampaignDescription;
    private string CampaignShortDescription;
    private string CampaignURL;
    private int CampaignCategory;
    private string CampaignQuote;
    private string Campaignpicture;
    private DateTime CampaignStartTime;
    private DateTime CampaignEndTime;
    private int CampaignSlot;
    private bool ShipCanada;
    private bool ShipUSA;
    private string ShippingFromAddress;
    private string ShippingFromCity;
    private string ShippingFromZipCode;
    private string ShippingFromprovince;
    private string ShippingFromCountry;
    private string EstimatedArivalTime;
    private bool IsActive;
    private DateTime CreationDate;
    private long CreatedBy;
    private DateTime ModifiedDate;
    private long ModifiedBy;
    private bool IsFeatured;
    private long userID;
    #endregion

    #region Constructor
    public BLLCampaign()
    {
        CampaignID = 0;
        userID = 0;
        RestaurantId = 0;
        CampaignTitle = "";
        CampaignDescription = "";
        CampaignShortDescription = "";
        CampaignURL = "";
        CampaignCategory = 0;
        CampaignQuote = "";
        Campaignpicture = "";
        CampaignStartTime = DateTime.Now;
        CampaignEndTime = DateTime.Now;
        CampaignSlot = 0;
        ShipCanada = false;
        ShipUSA = false;
        ShippingFromAddress = "";
        ShippingFromCity = "";
        ShippingFromZipCode = "";
        ShippingFromprovince = "";
        ShippingFromCountry = "";        
        EstimatedArivalTime = "";
        IsActive = false;
        CreationDate = DateTime.Now;
        CreatedBy = 0;
        ModifiedDate = DateTime.Now;
        ModifiedBy = 0;
        IsFeatured = false;
    }
    #endregion

    #region getter Setter

    public long UserID
    {
        get { return userID; }
        set { userID = value; }
    }


    public bool isFeatured
    {
        get { return IsFeatured; }
        set { IsFeatured = value; }
    }
    
    public string shippingFromCity
    {
        get { return ShippingFromCity; }
        set { ShippingFromCity = value; }
    }

    public string shippingFromZipCode
    {
        get { return ShippingFromZipCode; }
        set { ShippingFromZipCode = value; }
    }

    public string shippingFromprovince
    {
        get { return ShippingFromprovince; }
        set { ShippingFromprovince = value; }
    }

    public string shippingFromCountry
    {
        get { return ShippingFromCountry; }
        set { ShippingFromCountry = value; }
    }

    public bool shipCanada
    {
        get { return ShipCanada; }
        set { ShipCanada = value; }
    }

    public bool shipUSA
    {
        get { return ShipUSA; }
        set { ShipUSA = value; }
    }

    public bool isActive
    {
        get { return IsActive; }
        set { IsActive = value; }
    }

    public DateTime campaignStartTime
    {
        get { return CampaignStartTime; }
        set { CampaignStartTime = value; }
    }

    public DateTime campaignEndTime
    {
        get { return CampaignEndTime; }
        set { CampaignEndTime = value; }
    }

    public DateTime creationDate
    {
        get { return CreationDate; }
        set { CreationDate = value; }
    }

    public DateTime modifiedDate
    {
        get { return ModifiedDate; }
        set { ModifiedDate = value; }
    }


    public string campaignURL
    {
        get { return CampaignURL; }
        set { CampaignURL = value; }
    }

    public string campaignTitle
    {
        get { return CampaignTitle; }
        set { CampaignTitle = value; }
    }

    public string campaignDescription
    {
        get { return CampaignDescription; }
        set { CampaignDescription = value; }
    }

    public string campaignShortDescription
    {
        get { return CampaignShortDescription; }
        set { CampaignShortDescription = value; }
    }
    
    public string campaignQuote
    {
        get { return CampaignQuote; }
        set { CampaignQuote = value; }
    }

    public string campaignpicture
    {
        get { return Campaignpicture; }
        set { Campaignpicture = value; }
    }

    public string shippingFromAddress
    {
        get { return ShippingFromAddress; }
        set { ShippingFromAddress = value; }
    }

    public string estimatedArivalTime
    {
        get { return EstimatedArivalTime; }
        set { EstimatedArivalTime = value; }
    }

    public long campaignID
    {
        get { return CampaignID; }
        set { CampaignID = value; }
    }

    public long restaurantId
    {
        get { return RestaurantId; }
        set { RestaurantId = value; }
    }

    public int campaignCategory
    {
        get { return CampaignCategory; }
        set { CampaignCategory = value; }
    }

    public int campaignSlot
    {
        get { return CampaignSlot; }
        set { CampaignSlot = value; }
    }

    public long createdBy
    {
        get { return CreatedBy; }
        set { CreatedBy = value; }
    }

    public long modifiedBy
    {
        get { return ModifiedBy; }
        set { ModifiedBy = value; }
    }
    #endregion

    #region Functions

    public int createCampaign()
    {
        return DALCampaign.createCampaign(this);
    }

    public int updateCampaign()
    {
        return DALCampaign.updateCampaign(this);
    }

    public int updateCampaignisFeaturedStatus()
    {
        return DALCampaign.updateCampaignisFeaturedStatus(this);
    }
    

    public DataTable getCampaignByBusinessID()
    {
        return DALCampaign.getCampaignByBusinessID(this);
    }

    public DataTable getCampaignByCampaignId()
    {
        return DALCampaign.getCampaignByCampaignId(this);
    }

    public DataTable getCurrentCampaignByGivenDate()
    {
        return DALCampaign.getCurrentCampaignByGivenDate(this);
    }

    

    public DataTable GetCurrentDealByDate()
    {
        return DALCampaign.GetCurrentDealByDate(this);
    }


    public int AddToMyFavorites()
    {
        return DALCampaign.AddToMyFavorites(this);
    }

    
    

    public DataTable getCurrentCampaignByGivenDateAndCampaignID()
    {
        return DALCampaign.getCurrentCampaignByGivenDateAndCampaignID(this);
    }
    
    public DataTable getEndingSoonCampaigns()
    {
        return DALCampaign.getEndingSoonCampaigns(this);
    }

    public DataTable getFeaturedCampaignByGivenDate()
    {
        return DALCampaign.getFeaturedCampaignByGivenDate(this);
    }

    public DataTable getCurrentProductsByGivenDateAndCategory()
    {
        return DALCampaign.getCurrentProductsByGivenDateAndCategory(this);
    }

    public int updateCampaignSlots(string strCampaignIDs)
    {
        return DALCampaign.updateCampaignSlots(strCampaignIDs);
    }


    
    public DataSet getCampaignCalanderByDate()
    {
        return DALCampaign.getCampaignCalanderByDate();
    }

    public bool GetCampaginSoldOutStatus()
    {
        return DALCampaign.GetCampaginSoldOutStatus(this);
    }

    

    #endregion

    /*
     
     public DataTable getAllCities()
     {
         return DALCities.getAllCities(this);
     }

     public DataTable getAllCitiesWithProvinceAndCountryInfo()
     {
         return DALCities.getAllCitiesWithProvinceAndCountryInfo(this);
     }        

     public DataTable getCityByCityId()
     {
         return DALCities.getCityByCityId(this);
     }

     public bool getCityByName()
     {
         return DALCities.getCityByName(this);
     }

     public DataTable getCityDetailByName()
     {
         return DALCities.getCityDetailByName(this);
     }

    

     public DataTable getCitiesByProvinceId()
     {
         return DALCities.getCitiesByProvinceId(this);
     }

     public DataTable getAllCitiesForAdmin()
     {
         return DALCities.getAllCitiesForAdmin(this);
     }

     public DataTable GetAllCitiesForSearch()
     {
         return DALCities.GetAllCitiesForSearch();
     }
     */

}
