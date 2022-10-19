using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

/// <summary>
/// Summary description for BLLCities
/// </summary>
public class BLLResendOrders
{
    private long ResendOrderID;
    private long DealId;
    private string ResendOrder_CustomerName;
    private string ResendOrder_VoucherNumber;
    private string ResendOrder_Telephone;
    private string ResendOrder_Address;
    private string ResendOrder_Note;
    private string ResendOrder_Image;
    private string ResendOrder_trackingNumber;
    private DateTime ResendOrder_createdDate;

    public BLLResendOrders()
    {
        ResendOrderID = 0;
        DealId = 0;
        ResendOrder_CustomerName = "";
        ResendOrder_VoucherNumber = "";
        ResendOrder_Telephone = "";
        ResendOrder_Address = "";
        ResendOrder_Note = "";
        ResendOrder_Image = "";
        ResendOrder_trackingNumber = "";
        ResendOrder_createdDate = DateTime.Now;
    }

    public long resendOrderID 
    {
        get { return ResendOrderID; }
        set { ResendOrderID = value; }
    }

    public long dealId
    {
        get { return DealId; }
        set { DealId = value; }
    }

    public string resendOrder_trackingNumber
    {
        get { return ResendOrder_trackingNumber; }
        set { ResendOrder_trackingNumber = value; }
    }

    public DateTime resendOrder_createdDate
    {
        get { return ResendOrder_createdDate; }
        set { ResendOrder_createdDate = value; }
    }

    public string resendOrder_Image
    {
        get { return ResendOrder_Image; }
        set { ResendOrder_Image = value; }
    }

    public string resendOrder_Address
    {
        get { return ResendOrder_Address; }
        set { ResendOrder_Address = value; }
    }

    public string resendOrder_Note
    {
        get { return ResendOrder_Note; }
        set { ResendOrder_Note = value; }
    }

    public string resendOrder_CustomerName
    {
        get { return ResendOrder_CustomerName; }
        set { ResendOrder_CustomerName = value; }
    }

    public string resendOrder_VoucherNumber
    {
        get { return ResendOrder_VoucherNumber; }
        set { ResendOrder_VoucherNumber = value; }
    }

    public string resendOrder_Telephone
    {
        get { return ResendOrder_Telephone; }
        set { ResendOrder_Telephone = value; }
    }



    public int createResendOrders()
    {
        return DALResendOrders.createResendOrders(this);
    }


    public int updateResendOrdersTrackingNumber()
    {
        return DALResendOrders.updateResendOrdersTrackingNumber(this);
    }

    public int updateResendOrders()
    {
        return DALResendOrders.updateResendOrders(this);
    }

    public int deleteResendOrders()
    {
        return DALResendOrders.deleteResendOrders(this);
    }

    public DataTable getResendOrdersById()
    {
        return DALResendOrders.getResendOrdersById(this);
    }

    public DataTable getResendOrdersByDealId()
    {
        return DALResendOrders.getResendOrdersByDealId(this);
    }        
   
}
