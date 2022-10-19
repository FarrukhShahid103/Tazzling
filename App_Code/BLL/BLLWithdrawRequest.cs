using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

/// <summary>
/// Summary description for BLLWithdrawRequest
/// </summary>
public class BLLWithdrawRequest
{
    #region Private Variables
    private long RequestId;
    private string RequestAction;
    private string RequestUserType;
    private float RequestAmount;
    private string RequestCurrencyCode;
    private DateTime CreationDate;
    private long CreatedBy;
    private DateTime ModifiedDate;
    private long ModifiedBy;
    private string preferredMethod;
    private int Month;
    private int Year;
    #endregion

    #region Private Constructor
    public BLLWithdrawRequest()
    {
        RequestId = 0;
        RequestAction = "";
        RequestUserType = "";
        RequestAmount = 0;
        RequestCurrencyCode = "CAD";
        CreationDate = DateTime.Now;
        CreatedBy = 0;
        ModifiedDate = DateTime.Now;
        ModifiedBy = 0;
        preferredMethod = "Check";
        Month = 0;
        Year = 0;
    }
    #endregion

    #region Properties

    public int month
    {
        set { Month = value; }
        get { return Month; }
    }

    public int year
    {
        set { Year = value; }
        get { return Year; }
    }

    public long requestId
    {
        set { RequestId = value; }
        get { return RequestId; }
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

    public string requestAction
    {
        set { RequestAction = value; }
        get { return RequestAction; }
    }

    public string PreferredMethod
    {
        set { preferredMethod = value; }
        get { return preferredMethod; }
    }

    public string requestUserType
    {
        set { RequestUserType = value; }
        get { return RequestUserType; }
    }

    public string requestCurrencyCode
    {
        set { RequestCurrencyCode = value; }
        get { return RequestCurrencyCode; }
    }

    public float requestAmount
    {
        set { RequestAmount = value; }
        get { return RequestAmount; }
    }

    public DateTime creationDate
    {
        set { CreationDate = value; }
        get { return CreationDate; }
    }

    public DateTime modifiedDate
    {
        set { ModifiedDate = value; }
        get { return ModifiedDate; }
    }


    #endregion

    #region Functions
    public int createWithdrawRequest()
    {
        return DALWithdrawRequest.createWithdrawRequest(this);
    }

    public int updateWithdrawRequestByAdmin()
    {
        return DALWithdrawRequest.updateWithdrawRequestByAdmin(this);
    }
    

    public DataTable getWithdrawRequestByUserID()
    {
        return DALWithdrawRequest.getWithdrawRequestByUserID(this);
    }

    public DataTable getWithdrawRequestForReturantOwnerByUserID()
    {
        return DALWithdrawRequest.getWithdrawRequestForReturantOwnerByUserID(this);
    }

    public DataTable getAllWithdrawRequests()
    {
        return DALWithdrawRequest.getAllWithdrawRequests(this);
    }

    public DataTable getAllWithdrawmMoneyByUserIDTillGivenMonth()
    {
        return DALWithdrawRequest.getAllWithdrawmMoneyByUserIDTillGivenMonth(this);
    }     
    
    public DataTable getWithdrawRequestForReturantOwnerByUserIDOfGivenMonth()
    {
        return DALWithdrawRequest.getWithdrawRequestForReturantOwnerByUserIDOfGivenMonth(this);
    } 
       
    #endregion
}
