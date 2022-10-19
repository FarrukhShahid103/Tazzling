using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

/// <summary>
/// Summary description for BLLRestaurant
/// </summary>
public class BLLRestaurant
{
    #region Private Variables

    private long RestaurantId;
    private string FirstName;
    private string LastName;
    private string RestaurantBusinessName;
    private string BusinessPaymentTitle;
    private string Commission;
    private string PaymentStatus;
    private string RestaurantpaymentAddress;
    private string AlternativeEmail;
    private string URL;
    private string RestaurantAddress;
    private string Email;
    private long CityId;
    private long ProvinceId;
    private string Phone;
    private string Fax;
    private string ZipCode;
    private string RestaurantPicture;
    private string Detail;
    private DateTime CreatedDate;
    private long CreatedBy;
    private DateTime ModifiedDate;
    private long ModifiedBy;
    private bool IsActive;
    private long userid;
    private string CellNumber;
    private string PreDealVerification;
    private string PostDealVerification;
    private string Restaurantlogo;
    private string OwnerSignature;
    #endregion

    #region Constructor
    public BLLRestaurant()
    {
        Restaurantlogo = "";
        RestaurantpaymentAddress = "";
        AlternativeEmail = "";
        URL = "";
        RestaurantId = 0;
        FirstName = "";
        LastName = "";
        BusinessPaymentTitle = "";
        Commission = "";
        restaurantBusinessName = "";
        RestaurantAddress = "";
        PaymentStatus = "Not Paid";
        Email = "";
        CityId = 337;
        ProvinceId = 3;
        Phone = "";
        Fax = "";
        ZipCode = "";
        RestaurantPicture = "";
        Detail = "";
        CreatedDate = DateTime.Now;
        CreatedBy = 0;
        ModifiedDate = DateTime.Now;
        ModifiedBy = 0;
        IsActive = false;
        CellNumber = "";
        PreDealVerification = "";
        PostDealVerification = "";
        OwnerSignature = "";
    }
    #endregion

    #region Properties


    public string ownerSignature
    {
        set { OwnerSignature = value; }
        get { return OwnerSignature; }
    }

    public string restaurantlogo
    {
        set { Restaurantlogo = value; }
        get { return Restaurantlogo; }
    }

    public string cellNumber
    {
        set { CellNumber = value; }
        get { return CellNumber; }
    }

    public string preDealVerification
    {
        set { PreDealVerification = value; }
        get { return PreDealVerification; }
    }

    public string postDealVerification
    {
        set { PostDealVerification = value; }
        get { return PostDealVerification; }
    }


    public string restaurantpaymentAddress
    {
        set { RestaurantpaymentAddress = value; }
        get { return RestaurantpaymentAddress; }
    }

    public string alternativeEmail
    {
        set { AlternativeEmail = value; }
        get { return AlternativeEmail; }
    }

    public string url
    {
        set { URL = value; }
        get { return URL; }
    }

    public string paymentStatus
    {
        set { PaymentStatus = value; }
        get { return PaymentStatus; }
    }

    public string businessPaymentTitle
    {
        set { BusinessPaymentTitle = value; }
        get { return BusinessPaymentTitle; }
    }

    public string commission
    {
        set { Commission = value; }
        get { return Commission; }
    }


    public long userID
    {
        set { userid = value; }
        get { return userid; }
    }

    public long restaurantId
    {
        set { RestaurantId = value; }
        get { return RestaurantId; }
    }

    public string firstName
    {
        set { FirstName = value; }
        get { return FirstName; }
    }

    public string lastName
    {
        set { LastName = value; }
        get { return LastName; }
    }

    public string restaurantBusinessName
    {
        set { RestaurantBusinessName = value; }
        get { return RestaurantBusinessName; }
    }

    public string restaurantAddress
    {
        set { RestaurantAddress = value; }
        get { return RestaurantAddress; }
    }

    public string email
    {
        set { Email = value; }
        get { return Email; }
    }

    public long cityId
    {
        set { CityId = value; }
        get { return CityId; }
    }

    public long provinceId
    {
        set { ProvinceId = value; }
        get { return ProvinceId; }
    }

    public string phone
    {
        set { Phone = value; }
        get { return Phone; }
    }

    public string fax
    {
        set { Fax = value; }
        get { return Fax; }
    }

    public string zipCode
    {
        set { ZipCode = value; }
        get { return ZipCode; }
    }

    public string restaurantPicture
    {
        set { RestaurantPicture = value; }
        get { return RestaurantPicture; }
    }

    public string detail
    {
        set { Detail = value; }
        get { return Detail; }
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

    public DateTime createdDate
    {
        set { CreatedDate = value; }
        get { return CreatedDate; }
    }

    public DateTime modifiedDate
    {
        set { ModifiedDate = value; }
        get { return ModifiedDate; }
    }

    public bool isActive
    {
        set { IsActive = value; }
        get { return IsActive; }
    }

    #endregion

    #region Functions
    public int createRestaurant()
    {
        return DALRestaurant.createRestaurant(this);
    }

    public int deleteRestaurant()
    {
        return DALRestaurant.deleteRestaurant(this);
    }

    public DataTable getAllResturantsForAdmin()
    {
        return DALRestaurant.getAllResturantsForAdmin();
    }

    public DataSet getAllResturantsForAdminWithIndexing(int intStartIndex, int intMaxRecords)
    {
        return DALRestaurant.getAllResturantsForAdminWithIndexing(intStartIndex,intMaxRecords);
    }

    public DataTable getRestaurantInfoByResturantID()
    {
        return DALRestaurant.getRestaurantInfoByResturantID(this);
    }

    public DataTable getRestaurantInfoByID()
    {
        return DALRestaurant.getRestaurantInfoByID(this);
    }

    public DataTable getFeaturedRestaurant()
    {
        return DALRestaurant.getFeaturedRestaurant();
    }

    public int updateRestaurantInfo()
    {
        return DALRestaurant.updateRestaurantInfo(this);
    }

    public int updateRestaurantStatusByResID()
    {
        return DALRestaurant.updateRestaurantStatusByResID(this);
    }

    public int updateRestaurantPaymentStatusByResID()
    {
        return DALRestaurant.updateRestaurantPaymentStatusByResID(this);
    }

    public int updateRestaurantDealVerificationInfoByResID()
    {
        return DALRestaurant.updateRestaurantDealVerificationInfoByResID(this);
    }

    public int updateRestaurantInfoByID(BLLUser obj)
    {
        return DALRestaurant.updateRestaurantInfoByID(this, obj);
    }

    public DataTable getFriendUserInfoByRestauarntId()
    {
        return DALRestaurant.getFriendUserInfoByRestauarntId(this);
    }



    //Ali RAza 6-20-2012

    public DataTable getBussinessByUserId()
    {
        return DALRestaurant.getBussinessByUserId(this);
    }

    public DataSet spGetAllResturantsForAdminWithIndexingByUserId(int intStartIndex, int intMaxRecords)
    {
        return DALRestaurant.spGetAllResturantsForAdminWithIndexingByUserId(intStartIndex, intMaxRecords, this);
    }
    #endregion
}
