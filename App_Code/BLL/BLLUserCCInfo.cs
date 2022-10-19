using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;


/// <summary>
/// Summary description for BLLComments
/// </summary>
public class BLLUserCCInfo
{
    #region Private Variables
    private long CcInfoID;
    private string CcInfoDFirstName;
    private string CcInfoDLastName;
    private string CcInfoDEmail;
    private string CcInfoBName;
    private string CcInfoBAddress;
    private string CcInfoBAddress2;
    private string CcInfoBCity;
    private string CcInfoBProvince;
    private string CcInfoBPostalCode;
    private string CcInfoNumber;
    private string CcInfoEdate;
    private string CcInfoCCVNumber;
    private long UserId;
    private DateTime CreationDate;
    private long CreatedBy;
    private DateTime Modifieddate;
    private long ModifiedBy;
    
    #endregion

    #region Constructor
    public BLLUserCCInfo()
    {
        CcInfoID = 0;
        CcInfoDFirstName = "";
        CcInfoDLastName = "";
        CcInfoDEmail = "";
        CcInfoBName = "";
        CcInfoBAddress = "";
        CcInfoBAddress2 = "";
        CcInfoBCity = "";
        CcInfoBProvince = "";
        CcInfoBPostalCode = "";
        CcInfoNumber = "";
        CcInfoEdate = "";
        CcInfoCCVNumber = "";
        UserId = 0;
        CreationDate = DateTime.Now;
        CreatedBy = 0;
        Modifieddate = DateTime.Now;
        ModifiedBy = 0;
        
    }
    #endregion

    #region Properties
    
  
    public long ccInfoID
    {
        set { CcInfoID = value; }
        get { return CcInfoID; }
    }

    public long userId
    {
        set { UserId = value; }
        get { return UserId; }
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

    public string ccInfoDFirstName 
    {
        set { CcInfoDFirstName = value; }
        get { return CcInfoDFirstName; }
    }

    public string ccInfoDLastName 
    {
        set { CcInfoDLastName = value; }
        get { return CcInfoDLastName; }
    }

    public string ccInfoDEmail 
    {
        set { CcInfoDEmail = value; }
        get { return CcInfoDEmail; }
    }

    public string ccInfoBName 
    {
        set { CcInfoBName = value; }
        get { return CcInfoBName; }
    }

    public string ccInfoBAddress 
    {
        set { CcInfoBAddress = value; }
        get { return CcInfoBAddress; }
    }

    public string ccInfoBAddress2 
    {
        set { CcInfoBAddress2 = value; }
        get { return CcInfoBAddress2; }
    }
    

    public string ccInfoBCity 
    {
        set { CcInfoBCity = value; }
        get { return CcInfoBCity; }
    }

    public string ccInfoBProvince 
    {
        set { CcInfoBProvince = value; }
        get { return CcInfoBProvince; }
    }

    public string ccInfoBPostalCode 
    {
        set { CcInfoBPostalCode = value; }
        get { return CcInfoBPostalCode; }
    }

    public string ccInfoNumber 
    {
        set { CcInfoNumber = value; }
        get { return CcInfoNumber; }
    }

    public string ccInfoEdate 
    {
        set { CcInfoEdate = value; }
        get { return CcInfoEdate; }
    }

    public string ccInfoCCVNumber 
    {
        set { CcInfoCCVNumber = value; }
        get { return CcInfoCCVNumber; }
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
    public long createUserCCInfo()
    {
        return DALUserCCInfo.createUserCCInfo(this);
    }

    public int deleteUserCCInfo()
    {
        return DALUserCCInfo.deleteUserCCInfo(this);
    }

    public int updateUserCCInfoByID()
    {
        return DALUserCCInfo.updateUserCCInfoByID(this);
    }

    public DataTable getUserCCInfoByID()
    {
        return DALUserCCInfo.getUserCCInfoByID(this);
    }

    public DataTable getUserCCInfoByUserID()
    {
        return DALUserCCInfo.getUserCCInfoByUserID(this);
    }

    

    #endregion

}
