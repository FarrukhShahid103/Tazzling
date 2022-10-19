using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

/// <summary>
/// Summary description for BLLUserShippingInfo
/// </summary>
public class BLLUserShippingInfo
{
    #region Private Variables
    private long ShippingInfoId;
    private long UserID;
    private string address;
    private string address2;
    private string city;               
    private string name;
    private string state;
    private string telephone;
    private string zipCode;        
    private DateTime CreationDate;
    private long CreatedBy;
    private DateTime modifiedDate;
    private long modifiedBy;
    private string ShippingNote;
    private string ShippingCountry;
    #endregion

    #region Constructor
    public BLLUserShippingInfo()
    {
        ShippingInfoId = 0;
        UserID = 0;
        address = "";
        address2 = "";
        city = "";
        name = "";
        state = "";
        telephone = "";
        zipCode = "";       
        CreationDate = DateTime.Now;
        CreatedBy = 0;
        modifiedDate = DateTime.Now;
        modifiedBy = 0;
        ShippingNote = "";
        ShippingCountry = "";
    }
    #endregion

    #region Properties

    public string shippingCountry
    {
        set { ShippingCountry = value; }
        get { return ShippingCountry; }
    }
  
    public long shippingInfoId
    {
        set { ShippingInfoId = value; }
        get { return ShippingInfoId; }
    }
  
    public long createdBy
    {
        set { CreatedBy = value; }
        get { return CreatedBy; }
    }

    public long ModifiedBy
    {
        set { modifiedBy = value; }
        get { return modifiedBy; }
    }

    public long userID
    {
        set { UserID = value; }
        get { return UserID; }
    }


    public string shippingNote
    {
        set { ShippingNote = value; }
        get { return ShippingNote; }
    }

    public string Address
    {
        set { address = value; }
        get { return address; }
    }

    public string Address2
    {
        set { address2 = value; }
        get { return address2; }
    }

    public string City 
    {
        set { city = value; }
        get { return city; }
    }

    public string Name 
    {
        set { name = value; }
        get { return name; }
    }

    public string State 
    {
        set { state = value; }
        get { return state; }
    }

    public string Telephone 
    {
        set { telephone = value; }
        get { return telephone; }
    }

    public string ZipCode 
    {
        set { zipCode = value; }
        get { return zipCode; }
    }
  
    public DateTime creationDate
    {
        set { CreationDate = value; }
        get { return CreationDate; }
    }

    public DateTime ModifiedDate
    {
        set { modifiedDate = value; }
        get { return modifiedDate; }
    }
    #endregion

    #region Functions
    public long createUserShippingInfo()
    {
        return DALUserShippingInfo.createUserShippingInfo(this);
    }

    //public int deleteMemberDeliveryInfoByUserID()
    //{
    //    return DALMemberDeliveryInfo.deleteMemberDeliveryInfoByUserID(this);
    //}

    //public int updateMemberDeliveryInfoByUserID()
    //{
    //    return DALMemberDeliveryInfo.updateMemberDeliveryInfoByUserID(this);
    //}

    //public int updateMemberBillingInfoByUserID()
    //{
    //    return DALMemberDeliveryInfo.updateMemberBillingInfoByUserID(this);
    //}

    //public DataTable getMemberDeliveryInfoByUserID()
    //{
    //    return DALMemberDeliveryInfo.getMemberDeliveryInfoByUserID(this);
    //}
    #endregion
}
