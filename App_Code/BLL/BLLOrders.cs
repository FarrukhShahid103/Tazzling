using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

/// <summary>
/// Summary description for BLLOrders
/// </summary>
public class BLLOrders
{

    #region Private Variables
    private long OrderId;
    private string OrderNumber;
    private string OrderStatus;
    private DateTime OrderDate;
    private float TotalAmount;
    private float SubTotalAmount;
    private string DeliveryMethod;
    private float DeliveryAmount;
    private float TaxAmount;
    private string CurrencyCode;
    private string UserIP;
    private long ProviderId;
    private DateTime CreationDate;
    private long CreatedBy;
    private DateTime ModifiedDate;
    private long ModifiedBy;
    private string ShipToAddress;
    private string ShipPhone;
    private string BillToAddress;
    private string BillPhone;
    private float UseCreditCardAmount;
    private bool AddComment;
    private bool ToMemberRewards;
    private float Commission;
    private float Tips;
    private string SpecialRequest;
    private DateTime OrderConfirmedOn;
    private float Redeem;
    private string ShippingRule;
    private string OrderType;
    private bool EnabledReferral;
    private bool IsPrinted;
    private int Month;
    private int Year;
    private double ShippingDistance;
    private string pTNumber;
    private float BelowLimtAmount;
    #endregion

    #region Constructor
    public BLLOrders()
    {
        OrderId = 0;
        OrderNumber = "";
        OrderStatus = "";
        OrderDate = DateTime.Now;
        TotalAmount = 0;
        SubTotalAmount = 0;
        DeliveryMethod = "";
        DeliveryAmount = 0;
        TaxAmount = 0;
        CurrencyCode = "";
        UserIP = "";
        ProviderId = 0;
        CreationDate = DateTime.Now;
        CreatedBy = 0;
        ModifiedDate = DateTime.Now;
        ModifiedBy = 0;
        ShipToAddress = "";
        ShipPhone = "";
        BillPhone = "";
        BillToAddress = "";
        UseCreditCardAmount = 0;
        AddComment = false;
        ToMemberRewards = false;
        IsPrinted = false;
        Commission = 0;
        Tips = 0;
        SpecialRequest = "";
        OrderConfirmedOn = DateTime.Now;
        Redeem = 0;
        ShippingRule = "";
        OrderType = "";
        EnabledReferral = false;
        Month = 0;
        Year = 0;
        ShippingDistance = 0.0;
        pTNumber = "";
        BelowLimtAmount = 0;
    }
    #endregion

    #region Properties

    public double shippingDistance
    {
        set { ShippingDistance = value; }
        get { return ShippingDistance; }
    }

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


    public long orderId
    {
        set { OrderId = value; }
        get { return OrderId; }
    }

    public long providerId
    {
        set { ProviderId = value; }
        get { return ProviderId; }
    }

    public float totalAmount
    {
        set { TotalAmount = value; }
        get { return TotalAmount; }
    }

    public float subTotalAmount
    {
        set { SubTotalAmount = value; }
        get { return SubTotalAmount; }
    }

    public float deliveryAmount
    {
        set { DeliveryAmount = value; }
        get { return DeliveryAmount; }
    }

    public float taxAmount
    {
        set { TaxAmount = value; }
        get { return TaxAmount; }
    }

    public float useCreditCardAmount
    {
        set { UseCreditCardAmount = value; }
        get { return UseCreditCardAmount; }
    }

    public float commission
    {
        set { Commission = value; }
        get { return Commission; }
    }

    public float tips
    {
        set { Tips = value; }
        get { return Tips; }
    }

    public float redeem
    {
        set { Redeem = value; }
        get { return Redeem; }
    }

    public string orderNumber 
    {
        set { OrderNumber = value; }
        get { return OrderNumber; }
    }

    public string orderStatus 
    {
        set { OrderStatus = value; }
        get { return OrderStatus; }
    }

    public string deliveryMethod 
    {
        set { DeliveryMethod = value; }
        get { return DeliveryMethod; }
    }

    public string billPhone 
    {
        set { BillPhone = value; }
        get { return BillPhone; }
    }

    public string shipPhone 
    {
        set { ShipPhone = value; }
        get { return ShipPhone; }
    }
        

    public string PTNumber 
    {
        set { pTNumber = value; }
        get { return pTNumber; }
    }       

    public string currencyCode 
    {
        set { CurrencyCode = value; }
        get { return CurrencyCode; }
    }

    public string userIP 
    {
        set { UserIP = value; }
        get { return UserIP; }
    }

    public string shipToAddress 
    {
        set { ShipToAddress = value; }
        get { return ShipToAddress; }
    }

    public string billToAddress 
    {
        set { BillToAddress = value; }
        get { return BillToAddress; }
    }

    public string specialRequest 
    {
        set { SpecialRequest = value; }
        get { return SpecialRequest; }
    }

    public string shippingRule 
    {
        set { ShippingRule = value; }
        get { return ShippingRule; }
    }

    public string orderType 
    {
        set { OrderType = value; }
        get { return OrderType; }
    }

    public bool addComment
    {
        set { AddComment = value; }
        get { return AddComment; }
    }

    public bool toMemberRewards
    {
        set { ToMemberRewards = value; }
        get { return ToMemberRewards; }
    }

    public bool enabledReferral
    {
        set { EnabledReferral = value; }
        get { return EnabledReferral; }
    }

    public bool isPrinted
    {
        set { IsPrinted = value; }
        get { return IsPrinted; }
    }    
   
    public long createdBy
    {
        set { CreatedBy = value; }
        get { return CreatedBy; }
    }

    public DateTime creationDate
    {
        set { CreationDate = value; }
        get { return CreationDate; }
    }

    public long modifiedBy
    {
        set { ModifiedBy = value; }
        get { return ModifiedBy; }
    }

    public DateTime modifiedDate
    {
        set { ModifiedDate = value; }
        get { return ModifiedDate; }
    }

    public DateTime orderDate
    {
        set { OrderDate = value; }
        get { return OrderDate; }
    }

    public DateTime orderConfirmedOn
    {
        set { OrderConfirmedOn = value; }
        get { return OrderConfirmedOn; }
    }

    public float belowLimtAmount
    {
        set { BelowLimtAmount = value; }
        get { return BelowLimtAmount; }
    }

    #endregion

    #region Functions
    public long createOrders()
    {
        return DALOrders.createOrders(this);
    }

    public int deleteOrders()
    {
        return DALOrders.deleteOrders(this);
    }

    public int spUpdateOrders()
    {
        return DALOrders.updateOrders(this);
    }

    public int updateOrderStatusByOrderID()
    {
        return DALOrders.updateOrderStatusByOrderID(this);
    }

    public int updateOrderStatusAndOnlinePTNumberByOrderID()
    {
        return DALOrders.updateOrderStatusAndOnlinePTNumberByOrderID(this);
    }

    

    public DataTable getAllOrders()
    {
        return DALOrders.getAllOrders();
    }

    public DataTable getAllOrdersByProviderID()
    {
        return DALOrders.getAllOrdersByProviderID(this);
    }

    public DataTable getAllOrdersByProviderIDCreationDate(DateTime dtStartDate, DateTime dtEndDate)
    {
        return DALOrders.getAllOrdersByProviderIDCreationDate(this, dtStartDate, dtEndDate);
    }

    public DataTable getAllOrdersByCreatedByCreationDate(DateTime dtStartDate, DateTime dtEndDate)
    {
        return DALOrders.getAllOrdersByCreatedByCreationDate(this, dtStartDate, dtEndDate);
    }

    public DataTable getAllOrdersByCreationDate(DateTime dtStartDate, DateTime dtEndDate)
    {
        return DALOrders.getAllOrdersByCreationDate(dtStartDate, dtEndDate);
    }

    public DataTable getAllOrdersByUserID()
    {
        return DALOrders.getAllOrdersByUserID(this);
    }

    public DataTable getAllOrdersByProviderIdAndUserID()
    {
        return DALOrders.getAllOrdersByProviderIdAndUserID(this);
    }

    public DataTable getOrderDetilByOrderID()
    {
        return DALOrders.getOrderDetilByOrderID(this);
    }

    public bool getTotalAmountOfGivenMonthByUserID()
    {
        return DALOrders.getTotalAmountOfGivenMonthByUserID(this);
    }

    public DataTable getTotalAndSubTotalOfCredidCardAndPartialOrdersByProviderID()
    {
        return DALOrders.getTotalAndSubTotalOfCredidCardAndPartialOrdersByProviderID(this);
    }

    public DataTable getTotalAndSubTotalOfCashOrdersByProviderID()
    {
        return DALOrders.getTotalAndSubTotalOfCashOrdersByProviderID(this);
    }

    public DataTable getTotalAndSubTotalOfQualifiedOrderByUserID()
    {
        return DALOrders.getTotalAndSubTotalOfQualifiedOrderByUserID(this);
    }

    public DataTable getTotalAndSubTotalOfNonQualifiedOrderByUserID()
    {
        return DALOrders.getTotalAndSubTotalOfNonQualifiedOrderByUserID(this);
    }

    public DataTable getTotalAndSubTotalOfNonQualifiedOrderByUserIDAndGivenMonth()
    {
        return DALOrders.getTotalAndSubTotalOfNonQualifiedOrderByUserIDAndGivenMonth(this);
    }

    public DataTable getTotalAndSubTotalOfQualifiedOrderByUserIDAndGivenMonth()
    {
        return DALOrders.getTotalAndSubTotalOfQualifiedOrderByUserIDAndGivenMonth(this);
    }

    public DataTable getLifeTimeSalesOfResturantByProviderID()
    {
        return DALOrders.getLifeTimeSalesOfResturantByProviderID(this);
    }

    public DataTable getTotalAndSubTotalOfOrdersByProviderID()
    {
        return DALOrders.getTotalAndSubTotalOfOrdersByProviderID(this);
    }

    public DataTable getTotalAndSubTotalOfOrdersByProviderIDAndGivenMonth()
    {
        return DALOrders.getTotalAndSubTotalOfOrdersByProviderIDAndGivenMonth(this);
    }

    public DataTable getTotalAndSubTotalOfQualifiedOrderByProviderID()
    {
        return DALOrders.getTotalAndSubTotalOfQualifiedOrderByProviderID(this);
    }

    public DataTable getTotalAndSubTotalOfNonQualifiedOrderByProviderID()
    {
        return DALOrders.getTotalAndSubTotalOfNonQualifiedOrderByProviderID(this);
    }

    public DataTable getTotalAndSubTotalOfNonQualifiedOrderByProviderIDAndGivenMonth()
    {
        return DALOrders.getTotalAndSubTotalOfNonQualifiedOrderByProviderIDAndGivenMonth(this);
    }

    public DataTable getTotalAndSubTotalOfQualifiedOrderByProviderIDAndGivenMonth()
    {
        return DALOrders.getTotalAndSubTotalOfQualifiedOrderByProviderIDAndGivenMonth(this);
    }

    public DataTable getOrderDetilByOrderNumber()
    {
        return DALOrders.getOrderDetilByOrderNumber(this);
    }

    public DataTable getTotalAndSubTotalOfCredidCardAndPartialOrdersByProviderIDAndGivenMonth()
    {
        return DALOrders.getTotalAndSubTotalOfCredidCardAndPartialOrdersByProviderIDAndGivenMonth(this);
    }

    public DataTable getTotalAndSubTotalOfCredidCardAndPartialOrdersByProviderIDFromStartToGivenMonth()
    {
        return DALOrders.getTotalAndSubTotalOfCredidCardAndPartialOrdersByProviderIDFromStartToGivenMonth(this);
    }

    public DataTable getTotalAndSubTotalOfCashOrdersByProviderIDFromStartToGivenMonth()
    {
        return DALOrders.getTotalAndSubTotalOfCashOrdersByProviderIDFromStartToGivenMonth(this);
    }
    

    public DataTable getTotalAndSubTotalOfCashOrdersByProviderIDAndGivenMonth()
    {
        return DALOrders.getTotalAndSubTotalOfCashOrdersByProviderIDAndGivenMonth(this);
    }  

    

    
    
    
    
    #endregion
}
