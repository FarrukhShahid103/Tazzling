using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

/// <summary>
/// Summary description for BLLMemberDeliveryInfo
/// </summary>
public class BLLMemberDeliveryInfo
{
    #region Private Variables
    private long DeliveryInfoId;
    private long UserID;
    private string B_Address;
    private string B_City;
    private string B_CompanyName;
    private string B_Country;
    private string B_Fax;
    private string B_FirstName;
    private string B_LastName;
    private string B_State;
    private string B_Telephone;
    private string B_ZipCode;
    private string B_Ext;
    private string S_Address;
    private string S_Buzzer_number;
    private string S_City;
    private string S_CompanyName;
    private string S_Country;
    private string S_Fax;
    private string S_FirstName;
    private string S_LastName;
    private string S_State;
    private string S_Telephone;
    private string S_ZipCode;
    private string S_Ext;    
    private DateTime CreationDate;
    private long CreatedBy;
    private DateTime Modifieddate;
    private long ModifiedBy;
    #endregion

    #region Constructor
    public BLLMemberDeliveryInfo()
    {
        DeliveryInfoId = 0;
        UserID = 0;
        B_Address = "";
        B_City = "";
        B_CompanyName = "";
        B_Country = "";
        B_Fax = "";
        B_FirstName = "";
        B_LastName = "";
        B_State = "";
        B_Telephone = "";
        B_ZipCode = "";
        B_Ext = "";
        S_Ext = "";
        S_Address = "";
        S_Buzzer_number = "";
        S_City = "";
        S_CompanyName = "";
        S_Country = "";
        S_Fax = "";
        S_FirstName = "";
        S_LastName = "";
        S_State = "";
        S_Telephone = "";
        S_ZipCode = "";
        CreationDate = DateTime.Now;
        CreatedBy = 0;
        Modifieddate = DateTime.Now;
        ModifiedBy = 0;
    }
    #endregion

    #region Properties
    public long deliveryInfoId
    {
        set { DeliveryInfoId = value; }
        get { return DeliveryInfoId; }
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

    public long userID
    {
        set { UserID = value; }
        get { return UserID; }
    }

    public string b_Address
    {
        set { B_Address = value; }
        get { return B_Address; }
    }


    public string s_Ext
    {
        set { S_Ext = value; }
        get { return S_Ext; }
    }


    public string b_Ext
    {
        set { B_Ext = value; }
        get { return B_Ext; }
    }


    

    public string b_City 
    {
        set { B_City = value; }
        get { return B_City; }
    }

    public string b_CompanyName 
    {
        set { B_CompanyName = value; }
        get { return B_CompanyName; }
    }

    public string b_Country 
    {
        set { B_Country = value; }
        get { return B_Country; }
    }

    public string b_Fax 
    {
        set { B_Fax = value; }
        get { return B_Fax; }
    }

    public string b_FirstName 
    {
        set { B_FirstName = value; }
        get { return B_FirstName; }
    }

    public string b_LastName 
    {
        set { B_LastName = value; }
        get { return B_LastName; }
    }

    public string b_State 
    {
        set { B_State = value; }
        get { return B_State; }
    }

    public string b_Telephone 
    {
        set { B_Telephone = value; }
        get { return B_Telephone; }
    }

    public string b_ZipCode 
    {
        set { B_ZipCode = value; }
        get { return B_ZipCode; }
    }

    public string s_Address
    {
        set { S_Address = value; }
        get { return S_Address; }
    }

    public string s_Buzzer_number
    {
        set { S_Buzzer_number = value; }
        get { return S_Buzzer_number; }
    }
    
    public string s_City
    {
        set { S_City = value; }
        get { return S_City; }
    }

    public string s_CompanyName
    {
        set { S_CompanyName = value; }
        get { return S_CompanyName; }
    }

    public string s_Country
    {
        set { S_Country = value; }
        get { return S_Country; }
    }

    public string s_Fax
    {
        set { S_Fax = value; }
        get { return S_Fax; }
    }

    public string s_FirstName
    {
        set { S_FirstName = value; }
        get { return S_FirstName; }
    }

    public string s_LastName
    {
        set { S_LastName = value; }
        get { return S_LastName; }
    }

    public string s_State
    {
        set { S_State = value; }
        get { return S_State; }
    }

    public string s_Telephone
    {
        set { S_Telephone = value; }
        get { return S_Telephone; }
    }

    public string s_ZipCode
    {
        set { S_ZipCode = value; }
        get { return S_ZipCode; }
    }

    public DateTime creationDate
    {
        set { CreationDate = value; }
        get { return CreationDate; }
    }

    public DateTime modifiedDate
    {
        set { Modifieddate = value; }
        get { return Modifieddate; }
    }
    #endregion

    #region Functions
    public int createMemberDeliveryInfoByUserID()
    {
        return DALMemberDeliveryInfo.createMemberDeliveryInfoByUserID(this);
    }

    public int deleteMemberDeliveryInfoByUserID()
    {
        return DALMemberDeliveryInfo.deleteMemberDeliveryInfoByUserID(this);
    }

    public int updateMemberDeliveryInfoByUserID()
    {
        return DALMemberDeliveryInfo.updateMemberDeliveryInfoByUserID(this);
    }

    public int updateMemberBillingInfoByUserID()
    {
        return DALMemberDeliveryInfo.updateMemberBillingInfoByUserID(this);
    }

    public DataTable getMemberDeliveryInfoByUserID()
    {
        return DALMemberDeliveryInfo.getMemberDeliveryInfoByUserID(this);
    }
    #endregion
}
