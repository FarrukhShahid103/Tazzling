using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

/// <summary>
/// Summary description for BLLUser
/// </summary>
public class BLLUser
{
    private int UserId;
    private int UserTypeID;
    private string UserName;
    private string Email;
    private string Password;
    private string ReferralId;
    private string FirstName;
    private string LastName;
    private string FriendsReferralId;
    private string ProfilePicture;
    private int CountryId;
    private int ProvinceId;
    private string HowYouKnowUs;
    private DateTime CreationDate;
    private int CreatedBy;
    private DateTime ModifiedDate;
    private int ModifiedBy;
    private bool IsActive;
    private bool IsDeleted;
    private DateTime DeletedDate;
    private string fb_access_token;
    private bool fb_share;
    private string fb_userID;
    private string AffiliateReq;
    private DateTime AffiliateReqDate;
    private int AffComID;
    private DateTime AffComEndDate;
    private string PhoneNo;
    private bool IsAffiliate;
    private int CityId;
    private string ZipCode;
    private bool Gender;
    private string Age;
    private string IPAddress;
    private string notes;
    private float bonus;
    private float adjustment;
    private DateTime Dateofbirth;
    private string DealsPreferFor;

    public BLLUser()
    {
        adjustment = 0;
        bonus = 0;
        notes = "";
        fb_access_token = "";
        fb_userID = "";
        UserId = 0;
        UserTypeID = 0;
        UserName = "";
        Email = "";
        Password = "";
        ReferralId = "";
        FirstName = "";
        LastName = "";
        ProfilePicture = "";
        FriendsReferralId = "";
        CountryId = 0;
        ProvinceId = 0;
        HowYouKnowUs = "Facebook";
        fb_share = true;
        Dateofbirth = DateTime.Now;
        CreationDate = DateTime.Now;
        CreatedBy = 0;
        ModifiedDate = DateTime.Now;
        ModifiedBy = 0;
        IsActive = false;
        IsDeleted = false;
        DeletedDate = DateTime.Now;
        AffiliateReq = "";
        AffiliateReqDate = DateTime.Now;
        AffComID = 0;
        AffComEndDate = DateTime.Now;
        PhoneNo = "";
        IsAffiliate = false;
        CityId = 0;
        ZipCode = "";
        Gender = true; ;
        Age = "Select";
        IPAddress = "";
        DealsPreferFor = "";
    }
    public string DealsPreferfor
    {
        set { DealsPreferFor = value; }
        get { return DealsPreferFor; }
    }


    public DateTime DateOfBirth
    {
        set { Dateofbirth = value; }
        get { return Dateofbirth; }
    }

    public float Adjustment
    {
        set { adjustment = value; }
        get { return adjustment; }
    }
    public float Bonus
    {
        set { bonus = value; }
        get { return bonus; }
    }

    public string Notes
    {
        set { notes = value; }
        get { return notes; }
    }

    public int cityId
    {
        set { CityId = value; }
        get { return CityId; }
    }
    public string zipcode
    {
        set { ZipCode = value; }
        get { return ZipCode; }
    }


    public string ipAddress
    {
        set { IPAddress = value; }
        get { return IPAddress; }
    }

    public string age
    {
        set { Age = value; }
        get { return Age; }
    }

    public bool gender
    {
        set { Gender = value; }
        get { return Gender; }
    }

    public bool isAffiliate
    {
        set { IsAffiliate = value; }
        get { return IsAffiliate; }
    }

    public bool FB_Share 
    {
        set { fb_share = value; }
        get { return fb_share; }
    }
    
    public string FB_access_token 
    {
        set { fb_access_token = value; }
        get { return fb_access_token; }
    }

    public string FB_userID
    {
        set { fb_userID = value; }
        get { return fb_userID; }
    }


    public int userId 
    {
        set { UserId = value; }
        get { return UserId; }
    }
    public int userTypeID
    {
        set { UserTypeID = value; }
        get { return UserTypeID; }
    }

    public string profilePicture
    {
        set { ProfilePicture = value; }
        get { return ProfilePicture; }
    }

    public string userName
    {
        set { UserName = value; }
        get { return UserName; }
    }
    public string userPassword
    {
        set { Password = value; }
        get { return Password; }
    }
    public string email
    {
        set { Email = value; }
        get { return Email; }
    }
  
    public string referralId
    {
        set { ReferralId = value; }
        get { return ReferralId; }
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
    public string friendsReferralId
    {
        set { FriendsReferralId = value; }
        get { return FriendsReferralId; }
    }
    public int countryId
    {
        set { CountryId = value; }
        get { return CountryId; }
    }
    public int provinceId
    {
        set { ProvinceId = value; }
        get { return ProvinceId; }
    }
    public string howYouKnowUs
    {
        set { HowYouKnowUs = value; }
        get { return HowYouKnowUs; }
    }
    public DateTime creationDate
    {
        set { CreationDate = value; }
        get { return CreationDate; }
    }
    public int createdBy
    {
        set { CreatedBy = value; }
        get { return CreatedBy; }
    }
    public DateTime modifiedDate
    {
        set { ModifiedDate = value; }
        get { return ModifiedDate; }
    }
    public int modifiedBy
    {
        set { ModifiedBy = value; }
        get { return ModifiedBy; }
    }
    public bool isActive
    {
        set { IsActive = value; }
        get { return IsActive; }
    }
    public bool isDeleted
    {
        set {  IsDeleted = value; }
        get { return IsDeleted; }
    }
    public DateTime deletedDate
    {
        set { DeletedDate = value; }
        get { return DeletedDate; }
    }
    public string affiliateReq
    {
        set { AffiliateReq = value; }
        get { return AffiliateReq; }
    }
    public DateTime affiliateReqDate
    {
        set { AffiliateReqDate = value; }
        get { return AffiliateReqDate; }
    }
     public int affComID
    {
        set { AffComID = value; }
        get { return AffComID; }
    }
    public DateTime affComEndDate
    {
        set { AffComEndDate = value; }
        get { return AffComEndDate; }
    }
    public string phoneNo
    {
        set { PhoneNo = value; }
        get { return PhoneNo; }
    }

    public DataTable validateUserNamePassword()
    {
        return DALUser.validateUserNamePassword(this);
    }
    public int createUser()
    {
        return DALUser.createUser(this);
    }

    public int createUserForFB()
    {
        return DALUser.createUserForFB(this);
    }
   
    public int updateUserProfile()
    {
        return DALUser.updateUserProfile(this);
    }

    public int updateUserAffiliateReqByUserId()
    {
        return DALUser.updateUserAffiliateReqByUserId(this);
    }

    public int updateUsersFriendsReferralId()
    {
        return DALUser.updateUsersFriendsReferralId(this);
    }

    public int updateUserPassword()
    {
        return DALUser.updateUsersPassword(this);
    }

    public int UpdateUserAccountInfo()
    {
        return DALUser.UpdateUserAccountInfo(this);
    }
    public int updateUserInfoByUsername(string strUserNameOld)
    {
        return DALUser.updateUserInfoByUsername(this, strUserNameOld);
    }
    
    public int updateUser()
    {
        return DALUser.updateUser(this);
    }

    public int updateUserFields()
    {
        return DALUser.updateUserFields(this);
    }

    public DataTable getAllUsers()
    {
        return DALUser.getAllUsers();
    }

    public DataSet getAllUsersWithIndexing(int intStartIndex, int intMaxRecords)
    {
        return DALUser.getAllUsersWithIndexing(intStartIndex, intMaxRecords);
    }

    public DataTable getAllInActiveUsers()
    {
        return DALUser.getAllInActiveUsers();
    }

    public DataTable getMemberUserByEmail()
    {
        return DALUser.getMemberUserByEmail(this);
    }

       

    

    public DataTable getMemberUsers()
    {
        return DALUser.getMemberUsers();
    }

    public DataTable getMemberResturantAndSalesUsers()
    {
        return DALUser.getMemberResturantAndSalesUsers();
    }    

    public bool deleteUser()
    {
        return DALUser.deleteUser(this);
    }
    public bool changeUserStatus()
    {
        return DALUser.changeUserStatus(this);
    }

    public bool changeUserAffiliateStatus()
    {
        return DALUser.changeUserAffiliateStatus(this);
    }

    

    public DataTable getUserByID()
    {
        return DALUser.getUserByID(this);
    }
    public bool getUserByUserName()
    {
        return DALUser.getUserByUserName(this);
    }

    public bool getResturantOwnerUserByUserName()
    {
        return DALUser.getResturantOwnerUserByUserName(this);
    }

    

    public bool getUserByUserNameForUpdate(string strUserName)
    {
        return DALUser.getUserByUserNameForUpdate(this, strUserName);
    }

    public bool getUserByEmail()
    {
        return DALUser.getUserByEmail(this);
    }

    public DataTable getUserDetailByEmail()
    {
        return DALUser.getUserDetailByEmail(this);
    }

    public bool getUserByReferralId()
    {
        return DALUser.getUserByReferralId(this);
    }

    public int updateUserByID()
    {
        return DALUser.updateUserByID(this);
    }


    public int updateUserAffCommIDByUserId()
    {
        return DALUser.updateUserAffCommIDByUserId(this);
    }

    public bool getMemberUserByReferralId()
    {
        return DALUser.getMemberUserByReferralId(this);
    }

    public bool getMemberUserWithNoChildByReferralId()
    {
        return DALUser.getMemberUserWithNoChildByReferralId(this);
    }
    public DataTable getUserByFriendsReferralId()
    {
        return DALUser.getUserByFriendsReferralId(this);
    }
    public DataTable getUserByReferralIdForPayment()
    {
        return DALUser.getUserByReferralIdForPayment(this);
    }
    public DataTable getUserRestaurantIDbyUserName()
    {
        return DALUser.getUserRestaurantIDbyUserName(this);
    }

    public int updateUserFirstAndLastNameByID()
    {
        return DALUser.updateUserFirstAndLastNameByID(this);
    }

    public int updateUserTypeByUserName()
    {
        return DALUser.updateUserTypeByUserName(this);
    }

    public int getUserIdByUserName()
    {
        return DALUser.getUserIdByUserName(this);
    }
    public DataTable getGetAffiliatePartnerMembers()
    {
        return DALUser.getGetAffiliatePartnerMembers(this);
    }

    public DataTable GetAllSalesAccountNames()
    {
        return DALUser.GetAllSalesAccountNames();
    }


    //New (02-01-2012)
    public DataTable GetAllSalesUsers()
    {
        return DALUser.GetAllSalesUsers();
    }

   

    public DataTable GetSalePersonBounsAndAdjustment()
    {
        return DALUser.GetSalePersonBounsAndAdjustment(this);
    }
    public int AddUpdateSalePersonBonus()
    {
        return DALUser.AddUpdateSalePersonBonus(this);
    }
    public int AddUpdateSalePersonAdjustment()
    {
        return DALUser.AddUpdateSalePersonAdjustment(this);
    }



    public int AddUpdateUserNotes()
    {
        return DALUser.AddUpdateUserNotes(this);
    }

    public DataTable GetUserNotesByUserID()
    {
        return DALUser.GetUserNotesByUserID(this);
    }

    public int UserUpdateWhoTab()
    {
        return DALUser.UserUpdateWhoTab(this);
    }

    public int UpdateUserFBShare()
    {
        return DALUser.UpdateUserFBShare(this);
    }





    public object newPassword { get; set; }
}