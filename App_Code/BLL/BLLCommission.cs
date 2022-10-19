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
/// Summary description for BLLCommission
/// </summary>
public class BLLCommission
{
    #region Private Variables
    private long Cid;
	private double CMoney;
	private bool IsDoubled;
	private long OrderId;
	private long UserId;
	private long RefreeId;
    private DateTime CreatedDate;
    private DateTime ModifiedDate;
    private int ComissionMonth;
    private int ComissionYear;
    bool IsSalesComission;
    #endregion

    #region Constructor
    public BLLCommission()
    {
        Cid = 0;
        CMoney = 0.0;
        IsDoubled = false;
        OrderId = 0;
        UserId = 0;
        RefreeId = 0;
        CreatedDate = DateTime.Now;
        ComissionMonth = 0;
        ComissionYear = 0;
        IsSalesComission = false;
    }
    #endregion

    #region Properties
    public int comissionMonth 
    {
        get { return ComissionMonth; }
        set { ComissionMonth = value; }
    }

    public bool isSalesComission
    {
        get { return IsSalesComission; }
        set { IsSalesComission = value; }
    }

    public int comissionYear
    {
        get { return ComissionYear; }
        set { ComissionYear = value; }
    }

    public long cid 
    {
        get { return Cid; }
        set { Cid = value; }
    }
    public double cMoney
    {
        get { return CMoney; }
        set { CMoney = value; }
    }
    public bool isDoubled
    {
        get { return IsDoubled; }
        set { IsDoubled = value; }
    }
    public long orderId
    {
        get { return OrderId; }
        set { OrderId = value; }
    }
    public long userId
    {
        get { return UserId; }
        set { UserId = value; }
    }
    public long refreeId
    {
        get { return RefreeId; }
        set { RefreeId = value; }
    }
    public DateTime createdDate
    {
        get { return CreatedDate; }
        set { CreatedDate = value; }
    }
    public DateTime modifiedDate
    {
        get { return ModifiedDate; }
        set { ModifiedDate = value; }
    }
    #endregion

    #region Functions
    public int createCommission()
    {
        return DALCommission.createCommission(this);
    }
    public int updateCommission()
    {
        return DALCommission.updateCommission(this);
    }

    public int updateCommissionSingle()
    {
        return DALCommission.updateCommissionSingle(this);
    }
        
    public int deleteCommission()
    {
        return DALCommission.deleteCommission(this);
    }
    public DataTable getCommissionByOrderID()
    {
        return DALCommission.getCommissionByOrderID(this);
    }
    public DataTable getAllCommssionByUserID()
    {
        return DALCommission.getAllCommssionByUserID(this);
    }
    public DataTable getCommissionOfCurrentMonthByUserID()
    {
        return DALCommission.getCommissionOfCurrentMonthByUserID(this);
    }
    public DataTable getCommissionMoneyByUserIDAndRefreeID()
    {
        return DALCommission.getCommissionMoneyByUserIDAndRefreeID(this);
    }

    public DataTable getCommissionMoneyByUserIDAndRefreeIDForSalePerson()
    {
        return DALCommission.getCommissionMoneyByUserIDAndRefreeIDForSalePerson(this);
    }

    public DataTable getCommissionMoneyByUserIDAndRefreeIDAndGivenDate()
    {
        return DALCommission.getCommissionMoneyByUserIDAndRefreeIDAndGivenDate(this);
    }

    public DataTable getCommissionMoneyByUserIDAndRefreeIDAndGivenDateForSalePerson()
    {
        return DALCommission.getCommissionMoneyByUserIDAndRefreeIDAndGivenDateForSalePerson(this);
    }

    
    

    #endregion
}
