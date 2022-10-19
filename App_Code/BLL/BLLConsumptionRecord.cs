using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

/// <summary>
/// Summary description for BLLConsumptionRecord
/// </summary>
public class BLLConsumptionRecord
{
    #region Private Variables
    private long ConsumptionRecordId;
    private long OrderId;
    private bool ConsumptionType;
    private float ConsumptionAmount;
    private string CurrencyCode;
    private DateTime CreationDate;
    private long CreatedBy;
    private DateTime Modifieddate;
    private long ModifiedBy;
    private bool IsOrder;
    #endregion

    #region Constructor
    public BLLConsumptionRecord()
    {
        ConsumptionRecordId = 0;
        OrderId = 0;
        ConsumptionType = false;
        ConsumptionAmount = 0;
        CurrencyCode = "CAD";
        CreationDate = DateTime.Now;
        CreatedBy = 0;
        Modifieddate = DateTime.Now;
        ModifiedBy = 0;
        IsOrder = false;
    }
    #endregion

    #region Properties
    public long consumptionRecordId
    {
        set { ConsumptionRecordId = value; }
        get { return ConsumptionRecordId; }
    }

    public long orderId
    {
        set { OrderId = value; }
        get { return OrderId; }
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

    public bool consumptionType
    {
        set { ConsumptionType = value; }
        get { return ConsumptionType; }
    }

    public float consumptionAmount
    {
        set { ConsumptionAmount = value; }
        get { return ConsumptionAmount; }
    }
    public bool isOrder
    {
        set { IsOrder = value; }
        get { return IsOrder; }
    }
    public string currencyCode
    {
        set { CurrencyCode = value; }
        get { return CurrencyCode; }
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

    public int createConsumptionRecord()
    {
        return DALConsumptionRecord.createConsumptionRecord(this);
    }

    public DataTable getAllUsedCommssionByUserID()
    {
        return DALConsumptionRecord.getAllUsedCommssionByUserID(this);
    }

    public DataTable getAllWithdrawmMoneyByUserID()
    {
        return DALConsumptionRecord.getAllWithdrawmMoneyByUserID(this);
    }
    public int deleteConsumptionRecordByOrderId()
    {
        return DALConsumptionRecord.deleteConsumptionRecordByOrderId(this);
    }
    #endregion
}
