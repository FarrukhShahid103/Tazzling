using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

/// <summary>
/// Summary description for BLLDeals
/// </summary>
public class BLLDeals
{
    #region "Private Members here"
    private long dealId;
    private long restaurantId;
    private string TopTitle;
    private string title;
    private string ShortTitle;
    private string finePrint;
    private string dealHightlights;
    private string description;
    private float sellingPrice;
    private float valuePrice;
    private DateTime dealStartTime;
    private DateTime dealEndTime;
    private string images;
    private long dealDelMinLmt;
    private long dealDelMaxLmt;
    private bool dealStatus;
    private long createdBy;
    private DateTime createdDate;
    private long modifiedBy;
    private DateTime modifiedDate;
    private long minOrdersPerUser;
    private long maxOrdersPerUser;
    private int CityID;
    private int maxGiftsPerOrder;
    private string Howtouse;
    private int dealSlot;
    private string cityName;
    private float affComm;
    private long parentDealId;
    private string dealPageTitle;
    private string dealNote;
    private DateTime VoucherExpiryDate;
    private bool VoucherExpiryDateAvailable;
    private bool ShippingAndTax;
    private float ShippingAndTaxAmount;
    private float OurCommission;
    private string salePersonAccountName;
    private int Dealpayment;
    private string UrlTitle;
    private bool ReviewExist;
    private float YelpRate;
    private string YelpText;
    private string YelpLink;
    private bool Tracking;
    private bool DoublePoints;
    #endregion

    #region Deal Constructer
    public BLLDeals()
    {
        //
        // TODO: Add constructor logic here
        //
        Tracking = false;
        DoublePoints = false;
        ReviewExist = false;
        dealId = 0;
        restaurantId = 0;
        TopTitle = "";
        ShortTitle = "";
        title = "";
        finePrint = "";
        cityName = "";
        dealHightlights = "";
        description = "";
        sellingPrice = 0;
        valuePrice = 0;
        dealStartTime = DateTime.Now;
        dealEndTime = DateTime.Now;
        images = "";
        dealDelMinLmt = 0;
        dealDelMaxLmt = 0;
        dealStatus = false;
        createdBy = 0;
        createdDate = DateTime.Now;
        modifiedBy = 0;
        modifiedDate = DateTime.Now;
        minOrdersPerUser = 0;
        maxOrdersPerUser = 0;
        CityID = 0;
        maxGiftsPerOrder = 0;
        Howtouse = "";
        dealSlot = 0;
        affComm = 0;
        parentDealId = 0;
        dealPageTitle = "";
        dealNote = "";
        VoucherExpiryDate = DateTime.Now;
        ShippingAndTax = false;
        ShippingAndTaxAmount = 0;
        OurCommission = 0;
        salePersonAccountName = "";
        Dealpayment = 0;
        UrlTitle = "";
        VoucherExpiryDateAvailable = true;
        YelpRate = 0;
        YelpText = "";
        YelpLink = "";
    }

    public bool tracking
    {
        set { Tracking = value; }
        get { return Tracking; }
    }

    public bool doublePoints
    {
        set { DoublePoints = value; }
        get { return DoublePoints; }
    }

    public bool reviewExist
    {
        set { ReviewExist = value; }
        get { return ReviewExist; }
    }

    public string yelpText
    {
        set { YelpText = value; }
        get { return YelpText; }
    }

    public string yelpLink
    {
        set { YelpLink = value; }
        get { return YelpLink; }
    }

    public float yelpRate
    {
        set { YelpRate = value; }
        get { return YelpRate; }
    }

    public bool voucherExpiryDateAvailable
    {
        set { VoucherExpiryDateAvailable = value; }
        get { return VoucherExpiryDateAvailable; }
    }

    public int dealpayment
    {
        set { Dealpayment = value; }
        get { return Dealpayment; }
    }

    public string urlTitle
    {
        set { UrlTitle = value; }
        get { return UrlTitle; }
    }

    public string SalePersonAccountName
    {
        set { salePersonAccountName = value; }
        get { return salePersonAccountName; }
    }

    public float ourCommission
    {
        set { OurCommission = value; }
        get { return OurCommission; }
    }

    public float shippingAndTaxAmount
    {
        set { ShippingAndTaxAmount = value; }
        get { return ShippingAndTaxAmount; }
    }

    public bool shippingAndTax
    {
        set { ShippingAndTax = value; }
        get { return ShippingAndTax; }
    }

    public string shortTitle
    {
        set { ShortTitle = value; }
        get { return ShortTitle; }
    }

    public DateTime voucherExpiryDate
    {
        set { VoucherExpiryDate = value; }
        get { return VoucherExpiryDate; }
    }

    public long DealId
    {
        set { dealId = value; }
        get { return dealId; }
    }

    public long RestaurantId
    {
        set { restaurantId = value; }
        get { return restaurantId; }
    }

    public string DealNote
    {
        set { dealNote = value; }
        get { return dealNote; }
    }

    public string CityName
    {
        set { cityName = value; }
        get { return cityName; }
    }

    public string topTitle
    {
        set { TopTitle = value; }
        get { return TopTitle; }
    }
  
    public string Title
    {
        set { title = value; }
        get { return title; }
    }
   
    public string howtouse
    {
        set { Howtouse = value; }
        get { return Howtouse; }
    }
   
    public string FinePrint
    {
        set { finePrint = value; }
        get { return finePrint; }
    }
    
    public string DealHightlights
    {
        set { dealHightlights = value; }
        get { return dealHightlights; }
    }
   
    public string Description
    {
        set { description = value; }
        get { return description; }
    }
    
    public float SellingPrice
    {
        set { sellingPrice = value; }
        get { return sellingPrice; }
    }
    
    public float ValuePrice
    {
        set { valuePrice = value; }
        get { return valuePrice; }
    }
   
    public DateTime DealStartTime
    {
        set { dealStartTime = value; }
        get { return dealStartTime; }
    }
    
    public DateTime DealEndTime
    {
        set { dealEndTime = value; }
        get { return dealEndTime; }
    }
   
    public string Images
    {
        set { images = value; }
        get { return images; }
    }
    
    public long DealDelMinLmt
    {
        set { dealDelMinLmt = value; }
        get { return dealDelMinLmt; }
    }
   
    public long DealDelMaxLmt
    {
        set { dealDelMaxLmt = value; }
        get { return dealDelMaxLmt; }
    }
    
    public int cityId
    {
        set { CityID = value; }
        get { return CityID; }
    }
    
    public bool DealStatus
    {
        set { dealStatus = value; }
        get { return dealStatus; }
    }
    
    public long CreatedBy
    {
        set { createdBy = value; }
        get { return createdBy; }
    }
    
    public DateTime CreatedDate
    {
        set { createdDate = value; }
        get { return createdDate; }
    }
    
    public long ModifiedBy
    {
        set { modifiedBy = value; }
        get { return modifiedBy; }
    }
    
    public DateTime ModifiedDate
    {
        set { modifiedDate = value; }
        get { return modifiedDate; }
    }

    public long MinOrdersPerUser
    {
        set { minOrdersPerUser = value; }
        get { return minOrdersPerUser; }
    }

    public long MaxOrdersPerUser
    {
        set { maxOrdersPerUser = value; }
        get { return maxOrdersPerUser; }
    }

    public int MaxGiftsPerOrder
    {
        set { maxGiftsPerOrder = value; }
        get { return maxGiftsPerOrder; }
    }

    public int DealSlot
    {
        set { dealSlot = value; }
        get { return dealSlot; }
    }

    public float AffComm
    {
        set { affComm = value; }
        get { return affComm; }
    }

    public long ParentDealId
    {
        set { parentDealId = value; }
        get { return parentDealId; }
    }

    public string DealPageTitle
    {
        set { dealPageTitle = value; }
        get { return dealPageTitle; }
    }
    #endregion 

    #region Function to Create New Deal

    public int AddNewDeal()
    {
        return DALDeals.AddNewDeal(this);
    }

    #endregion

    #region Function to Update Deal Info by Deal Id

    public int updateDealInfoByDealId()
    {
        return DALDeals.updateDealInfoByDealId(this);
    }
    #endregion

    #region Function to Update Deal Status by Deal Id

    public int updateDealStatusByDealId()
    {
        return DALDeals.updateDealStatusByDealId(this);
    }

    public int updateDealSlotByDealId()
    {
        return DALDeals.updateDealSlotByDealId(this);
    }

    public int updateDealNoteByDealId()
    {
        return DALDeals.updateDealNoteByDealId(this);
    }

    public int updateDealPaymentType()
    {
        return DALDeals.updateDealPaymentType(this);
    }

    #endregion

    #region Functions to get Deal By Deal Start & End Time & By City

    public DataTable getDealInfoByDealStartEndTime()
    {
        return DALDeals.getDealInfoByDealStartEndTime(this);
    }
    public DataTable getDealInfoByDealStartEndTimeWithCityID()
    {
        return DALDeals.getDealInfoByDealStartEndTimeWithCityID(this);
    }

    public DataTable getActiveDealSlotByDealStartEndTimeWithCityID()
    {
        return DALDeals.getActiveDealSlotByDealStartEndTimeWithCityID(this);
    }

    public DataTable getPreviousDealByCityID()
    {
        return DALDeals.getPreviousDealByCityID(this);
    }

    public DataTable getPreviousDealByCityIDForShow()
    {
        return DALDeals.getPreviousDealByCityIDForShow(this);
    }



    #endregion

    #region Functions to get all Deals

    public DataTable getAllDeals()
    {
        return DALDeals.getAllDeals();
    }

    public DataTable getAllDealsTitleAndID()
    {
        return DALDeals.getAllDealsTitleAndID();
    }

    #endregion

    #region Functions to get all Deal By RestaurantId

    public DataTable getDealByRestaurantId()
    {
        return DALDeals.getDealByRestaurantId(this);
    }

    #endregion

    #region Functions to get Deal By Deal Id

    public DataTable getDealByDealId()
    {
        return DALDeals.getDealByDealId(this);
    }

    public DataTable getdealDetailForCheckout()
    {
        return DALDeals.getdealDetailForCheckout(this);
    }

    

    public DataTable getDealByDealAndCityID()
    {
        return DALDeals.getDealByDealAndCityID(this);
    }

    public DataTable getDealForPaymentFormByDealId()
    {
        return DALDeals.getDealForPaymentFormByDealId(this);
    }



    public DataTable getCurrentDealByCityID()
    {
        return DALDeals.getCurrentDealByCityID(this);
    }

    public DataTable getCurrentDealByCityIDForPage()
    {
        return DALDeals.getCurrentDealByCityIDForPage(this);
    }

    public DataTable getCurrentDealByCityIDForPage2()
    {
        return DALDeals.getCurrentDealByCityIDForPage2(this);
    }

    public DataTable getCurrentDealByCityIDForPage3()
    {
        return DALDeals.getCurrentDealByCityIDForPage3(this);
    }

    public DataTable getDealinfoByURLTitle()
    {
        return DALDeals.getDealinfoByURLTitle(this);
    }

    public DataTable getDealinfoByDealID()
    {
        return DALDeals.getDealinfoByDealID(this);
    }

    public DataTable getCurrentDealByDealID()
    {
        return DALDeals.getCurrentDealByDealID(this);
    }


    public DataTable getCurrentDealByURLTitleForDealPage()
    {
        return DALDeals.getCurrentDealByURLTitleForDealPage(this);
    }

    public DataTable getCurrentDealByDealIDForDealPage()
    {
        return DALDeals.getCurrentDealByDealIDForDealPage(this);
    }

    public DataTable getCurrentDealByDealIDForDealPage2()
    {
        return DALDeals.getCurrentDealByDealIDForDealPage2(this);
    }

    public DataTable getCurrentSubDealInfoByParentDealID()
    {
        return DALDeals.getCurrentSubDealInfoByParentDealID(this);
    }

    public DataTable getCurrentSubDealInfoByParnetDealIDForDealPage()
    {
        return DALDeals.getCurrentSubDealInfoByParnetDealIDForDealPage(this);
    }



    #endregion

    #region Function to Delete Deal By Deal Id

    public int deleteDealByDealId()
    {
        return DALDeals.deleteDealByDealId(this);
    }

    #endregion
}