using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

/// <summary>
/// Summary description for BLLDealOrders
/// </summary>
public class BLLDealOrders
{
    #region"Private Variables"

    private long DOrderID;
    private long UserId;
    private long DealId;    
    private int qty;
    private int PersonalQty;
    private int GiftQty;
    private long CcInfoID;
    private string Status;
    private string PsgTranNo;
    private string PsgError;
    private string OrderNo;
    private DateTime CreatedDate;
    private long CreatedBy;
    private long ModifiedBy;
    private DateTime ModifiedDate;
    private float CcCreditUsed;
    private float TastyCreditUsed;
    private float ComissionMoneyUsed;
    private float TotalAmt;
    private long AddressId;
    private string OrderFrom;
    private string CustomerNote;
    private float ShippingAndTaxAmount;
    private long ShippingInfoId;
    private string OrderIPAddress;
    private string year;
    private string month;
    private string SalepersonaccountName;
    private string Size;
    private bool ResendOrders;
    private bool IsDeleted;


    #endregion

    public BLLDealOrders()
    {
        //
        // TODO: Add constructor logic here
        //
        year = "";
        SalepersonaccountName = "";
        month = "";
        DOrderID = 0;
        UserId = 0;
        DealId = 0;
        PersonalQty = 0;
        GiftQty = 0;
        qty = 0;
        CcInfoID = 0;
        Status = "";
        PsgTranNo = "";
        OrderNo = "";
        PsgError = "";
        CreatedDate = DateTime.Now;
        CreatedBy = 0;
        ModifiedBy = 0;
        ModifiedDate = DateTime.Now;
        CcCreditUsed = 0;
        TastyCreditUsed = 0;
        ComissionMoneyUsed = 0;
        TotalAmt = 0;
        AddressId = 0;
        OrderFrom = "Web";
        CustomerNote = "";
        ShippingAndTaxAmount = 0;
        ShippingInfoId = 0;
        OrderIPAddress = "";
        Size = "";
        ResendOrders = false;
        IsDeleted = false;
    }

    public bool isDeleted
    {
        set { IsDeleted = value; }
        get { return IsDeleted; }
    }

    public bool resendOrders
    {
        set { ResendOrders = value; }
        get { return ResendOrders; }
    }

    public string size
    {
        set { Size = value; }
        get { return Size; }
    }
    public string SalePersonAccountName
    {
        set { SalepersonaccountName = value; }
        get { return SalepersonaccountName; }
    }

    public string Year
    {
        set { year = value; }
        get { return year; }
    }

    public string Month
    {
        set { month = value; }
        get { return month; }
    }

    public long shippingInfoId
    {
        set { ShippingInfoId = value; }
        get { return ShippingInfoId; }
    }

    public float shippingAndTaxAmount
    {
        set { ShippingAndTaxAmount = value; }
        get { return ShippingAndTaxAmount; }
    }

    public string orderIPAddress
    {
        set { OrderIPAddress = value; }
        get { return OrderIPAddress; }
    }


    public string customerNote
    {
        set { CustomerNote = value; }
        get { return CustomerNote; }
    }

    public string orderFrom
    {
        set { OrderFrom = value; }
        get { return OrderFrom; }
    }

    public float comissionMoneyUsed
    {
        set { ComissionMoneyUsed = value; }
        get { return ComissionMoneyUsed; }
    }


    public long ccInfoID
    {
        set { CcInfoID = value; }
        get { return CcInfoID; }
    }
    public long dOrderID
    {
        set { DOrderID = value; }
        get { return DOrderID; }
    }
    public long userId
    {
        set { UserId = value; }
        get { return UserId; }
    }
    public long dealId
    {
        set { DealId = value; }
        get { return DealId; }
    }
    public int Qty
    {
        set { qty = value; }
        get { return qty; }
    }

    public int personalQty
    {
        set { PersonalQty = value; }
        get { return PersonalQty; }
    }

    public int giftQty
    {
        set { GiftQty = value; }
        get { return GiftQty; }
    }

    public string status
    {
        set { Status = value; }
        get { return Status; }
    }

    public string psgError
    {
        set { PsgError = value; }
        get { return PsgError; }
    }
    
    public string psgTranNo
    {
        set { PsgTranNo = value; }
        get { return PsgTranNo; }
    }
    public string orderNo
    {
        set { OrderNo = value; }
        get { return OrderNo; }
    }
    public DateTime createdDate
    {
        set { CreatedDate = value; }
        get { return CreatedDate; }
    }
    public long createdBy
    {
        set { CreatedBy = value; }
        get { return CreatedBy; }
    }

    public DateTime modifiedDate
    {
        set { ModifiedDate = value; }
        get { return ModifiedDate; }
    }
    public long modifiedBy
    {
        set { ModifiedBy = value; }
        get { return ModifiedBy; }
    }

    public float ccCreditUsed
    {
        set { CcCreditUsed = value; }
        get { return CcCreditUsed; }
    }

    public float tastyCreditUsed
    {
        set { TastyCreditUsed = value; }
        get { return TastyCreditUsed; }
    }
    public float totalAmt
    {
        set { TotalAmt = value; }
        get { return TotalAmt; }
    }
    public long addressId
    {
        set { AddressId = value; }
        get { return AddressId; }
    }
    
    //#region Function to Create New Deal Order

    public long createNewDealOrder()
    {
        return DALDealOrders.createNewDealOrder(this);
    }

    public int updateDealOrderNoteByOrderID()
    {
        return DALDealOrders.updateDealOrderNoteByOrderID(this);
    }

    public DataTable getTotalDealOrdersCountByDealId()
    {
        return DALDealOrders.getTotalDealOrdersCountByDealId(this);
    }

    public DataTable getProductOrderDetailByOrderID()
    {
        return DALDealOrders.getProductOrderDetailByOrderID(this);
    }

    public DataTable getAllResendOrdersByProductID()
    {
        return DALDealOrders.getAllResendOrdersByProductID(this);
    }

    public DataTable getDealOrderDetailByOrderID()
    {
        return DALDealOrders.getDealOrderDetailByOrderID(this);
    }

    public DataTable getDealOrderDetailByOrderNoForProduct()
    {
        return DALDealOrders.getDealOrderDetailByOrderNoForProduct(this);
    }

    public DataTable getDealOrderDetailByOrderID_New()
    {
        return DALDealOrders.getDealOrderDetailByOrderID_New(this);
    }

    public DataTable getDealOrderDetailByOrderNo()
    {
        return DALDealOrders.getDealOrderDetailByOrderNo(this);
    }

    public DataTable getVouchersDetailByOrderID()
    {
        return DALDealOrders.getVouchersDetailByOrderID(this);
    }

    public DataTable getDealOrderDetailByUserID()
    {
        return DALDealOrders.getDealOrderDetailByUserID(this);
    }

    public DataTable getTotalPersonalQtyOfDealOrdersByDealAndUserId()
    {
        return DALDealOrders.getTotalPersonalQtyOfDealOrdersByDealAndUserId(this);
    }

    public DataTable getDealOrderDetailByCCInfoID()
    {
        return DALDealOrders.getDealOrderDetailByCCInfoID(this);
    }
    public bool changeDealOrderStatus()
    {
        return DALDealOrders.changeDealOrderStatus(this);
    }



    public DataTable getAllDealOrders()
    {
        return DALDealOrders.getAllDealOrders();
    }

    public DataSet getAllDealOrdersByPageIndex(int intStartIndex, int intMaxRecords)
    {
        return DALDealOrders.getAllDealOrdersByPageIndex(intStartIndex, intMaxRecords);
    }

    public DataTable getDealOrdersCountByUserId()
    {
        return DALDealOrders.getDealOrdersCountByUserId(this);
    }

    public DataTable getAllOwnDealOrderDetailByUserID()
    {
        return DALDealOrders.getAllOwnDealOrderDetailByUserID(this);
    }

    public DataTable getAllOwnUsedDealOrderDetailByUserID()
    {
        return DALDealOrders.getAllOwnUsedDealOrderDetailByUserID(this);
    }

    public DataTable getAllOwnAvailableDealOrderDetailByUserID()
    {
        return DALDealOrders.getAllOwnAvailableDealOrderDetailByUserID(this);
    }

    public DataTable getAllOwnCancelledDealOrderDetailByUserID()
    {
        return DALDealOrders.getAllOwnCancelledDealOrderDetailByUserID(this);
    }

    public DataTable getAllGiftAvailableDealOrderDetailByUserID()
    {
        return DALDealOrders.getAllGiftAvailableDealOrderDetailByUserID(this);
    }

    public DataTable getAllGiftCancelledDealOrderDetailByUserID()
    {
        return DALDealOrders.getAllGiftCancelledDealOrderDetailByUserID(this);
    }

    public DataTable getAllGiftUsedDealOrderDetailByUserID()
    {
        return DALDealOrders.getAllGiftUsedDealOrderDetailByUserID(this);
    }

    public DataTable getAllGiftExpiredDealOrderDetailByUserID()
    {
        return DALDealOrders.getAllGiftExpiredDealOrderDetailByUserID(this);
    }

    public DataTable getAllOwnExpiredDealOrderDetailByUserID()
    {
        return DALDealOrders.getAllOwnExpiredDealOrderDetailByUserID(this);
    }

    public DataTable getAllGiftDealOrderDetailByUserID()
    {
        return DALDealOrders.getAllGiftDealOrderDetailByUserID(this);
    }

    public DataTable getAllOrdersWithCCDetailByDealID()
    {
        return DALDealOrders.getAllOrdersWithCCDetailByDealID(this);
    }

    public DataTable getTopTenOrdersWithCCDetailByDealID()
    {
        return DALDealOrders.getTopTenOrdersWithCCDetailByDealID(this);
    }

    public DataTable getAllSuccessfulOrdersByDealID()
    {
        return DALDealOrders.getAllSuccessfulOrdersByDealID(this);
    }

    public DataTable getTotalSoldDealsQTY()
    {
        return DALDealOrders.getTotalSoldDealsQTY();
    }
    //#endregion

    #region Function to Update Deal order Status by Order Id

    public int updateDealOrderStatusByOrderId()
    {
        return DALDealOrders.updateDealOrderStatusByOrderId(this);
    }

    #endregion
    public DataTable GetDealOrdersByUserIDWithDates(DateTime start, DateTime end)
    {
        return DALDealOrders.GetDealOrdersByUserIDWithDates(this, start, end);
    }


    public DataTable GetDealOrdersByUserID()
    {
        return DALDealOrders.GetDealOrdersByUserID(this);
    }


    public DataTable ComissionHistoryForAllSalesPersons()
    {
        return DALDealOrders.ComissionHistoryForAllSalesPersons();
    }

    public DataTable ComissionHistoryForSalePerson(string Email)
    {
        return DALDealOrders.ComissionHistoryForSalePerson(Email);
    }


    public int AddUpdateDealCommissionEarnedByDealID(float CommissionEarned)
    {
        return DALDealOrders.AddUpdateDealCommissionEarnedByDealID(this, CommissionEarned);
    }


    public DataTable ComissionHistoryReportForSalePerson()
    {
        return DALDealOrders.ComissionHistoryReportForSalePerson(this);
    }

    public DataTable GetSalesPerformanceForSuperAdmin()
    {
        return DALDealOrders.GetSalesPerformanceForSuperAdmin();
    }

    public DataTable ComissionHistoryReportForAllSalesman()
    {
        return DALDealOrders.ComissionHistoryReportForAllSalesman(this);
    }
    public DataTable GetOrderfromDetail()
    {
        return DALDealOrders.GetOrderfromDetail(this);
    }
    public DataTable GetRedeemedDetail()
    {
        return DALDealOrders.GetRedeemedDetail(this);
    }
    public DataTable GetOrderDate()
    {
        return DALDealOrders.GetOrderDate(this);
    }
    public DataTable GetIpAddressfromOrders()
    {
        return DALDealOrders.GetIpAddressfromOrders(this);
    }

    public DataTable EditOrderDetail()
    {
        return DALDealOrders.EditOrderDetail(this);
    }

    public bool DeleteOrderDetail()
    {
        return DALDealOrders.DeleteOrderDetail(this);
    }
}
