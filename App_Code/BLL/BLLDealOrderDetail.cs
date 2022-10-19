using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;


/// <summary>
/// Summary description for BLLComments
/// </summary>
public class BLLDealOrderDetail
{
    #region Private Variables
    private long DetailID;
    private long DOrderID;
    private string ReceiverEmail;
    private bool IsRedeemed;
    private DateTime RedeemedDate;
    private long RedeemedBy;
    private string DealOrderCode;
    private bool IsGift;
    private bool MarkUsed;
    private long IsGiftCapturedId;
    private string VoucherSecurityCode;
    private bool DisplayIt;
    private string Note;
    private string TrackingNumber;
    private DateTime TrackingUpdateDate;

    
    #endregion

    #region Constructor

    public BLLDealOrderDetail()
    {
        DetailID = 0;
        DOrderID = 0;
        ReceiverEmail = "";
        IsRedeemed = false;
        MarkUsed = false;
        RedeemedDate = DateTime.Now;
        DealOrderCode = "";
        RedeemedBy = 0;
        IsGift = false;
        IsGiftCapturedId = 0;
        VoucherSecurityCode = "";
        DisplayIt = true;
        Note = "";
        TrackingNumber = "";
        TrackingUpdateDate = DateTime.Now;
    }

    #endregion

    #region Properties

    public DateTime trackingUpdateDate
    {
        set { TrackingUpdateDate = value; }
        get { return TrackingUpdateDate; }
    }

    public string trackingNumber
    {
        set { TrackingNumber = value; }
        get { return TrackingNumber; }
    }

    public string note
    {
        set { Note = value; }
        get { return Note; }
    }

    public bool displayIt
    {
        set { DisplayIt = value; }
        get { return DisplayIt; }
    }

    public bool markUsed
    {
        set { MarkUsed = value; }
        get { return MarkUsed; }
    }

    public bool isGift
    {
        set { IsGift = value; }
        get { return IsGift; }
    }

    public long detailID
    {
        set { DetailID = value; }
        get { return DetailID; }
    }

    public long dOrderID
    {
        set { DOrderID = value; }
        get { return DOrderID; }
    }

    public long redeemedBy
    {
        set { RedeemedBy = value; }
        get { return RedeemedBy; }
    }


    public string voucherSecurityCode
    {
        set { VoucherSecurityCode = value; }
        get { return VoucherSecurityCode; }
    }

    public string receiverEmail
    {
        set { ReceiverEmail = value; }
        get { return ReceiverEmail; }
    }

    public string dealOrderCode
    {
        set { DealOrderCode = value; }
        get { return DealOrderCode; }
    }

    public DateTime redeemedDate
    {
        set { RedeemedDate = value; }
        get { return RedeemedDate; }
    }

    public bool isRedeemed
    {
        set { IsRedeemed = value; }
        get { return IsRedeemed; }
    }

    public long isGiftCapturedId
    {
        set { IsGiftCapturedId = value; }
        get { return IsGiftCapturedId; }
    }
    #endregion

    #region Functions

    public long createDealOrderDetail()
    {
        return DALDealOrderDetail.createDealOrderDetail(this);
    }

    public DataTable getAllUserDealOrderDetailByOrderID()
    {
        return DALDealOrderDetail.getAllUserDealOrderDetailByOrderID(this);
    }

    public DataTable getAllAvailableUserDealOrderDetailByOrderID()
    {
        return DALDealOrderDetail.getAllAvailableUserDealOrderDetailByOrderID(this);
    }

    public DataTable getAllUsedUserGiftDealOrderDetailByOrderID()
    {
        return DALDealOrderDetail.getAllUsedUserGiftDealOrderDetailByOrderID(this);
    }

    public DataTable getAllAvailableGiftUserDealOrderDetailByOrderID()
    {
        return DALDealOrderDetail.getAllAvailableGiftUserDealOrderDetailByOrderID(this);
    }


    public DataTable getAllUsedUserDealOrderDetailByOrderID()
    {
        return DALDealOrderDetail.getAllUsedUserDealOrderDetailByOrderID(this);
    }



    public bool changeOrderDetailStatus()
    {
        return DALDealOrderDetail.changeOrderDetailStatus(this);
    }
    public bool changeOrderDetailStatusanddeleteComments()
    {
        return DALDealOrderDetail.changeOrderDetailStatusanddeleteComments(this);
    }
    public bool changeOrderDetailDisplayStatus()
    {
        return DALDealOrderDetail.changeOrderDetailDisplayStatus(this);
    }


    public DataTable getAllGiftUserDealOrderDetailByOrderID()
    {
        return DALDealOrderDetail.getAllGiftUserDealOrderDetailByOrderID(this);
    }

    public DataTable getAllDealOrderDetailByDealOrderCode()
    {
        return DALDealOrderDetail.getAllDealOrderDetailByDealOrderCode(this);
    }



    public DataTable getAllDealOrderDetailsByOrderID()
    {
        return DALDealOrderDetail.getAllDealOrderDetailsByOrderID(this);
    }

    public DataTable getDealOrderDetailsByOrderID()
    {
        return DALDealOrderDetail.getDealOrderDetailsByOrderID(this);
    }

    //public int deleteComments()
    //{
    //    return DALComments.deleteComments(this);
    //}

    //public int updateComments()
    //{
    //    return DALComments.updateComments(this);
    //}

    public DataTable getVoucherNoteByDetailID()
    {
        return DALDealOrderDetail.getVoucherNoteByDetailID(this);
    }

    public int updateVoucherNoteByDetailID()
    {
        return DALDealOrderDetail.updateVoucherNoteByDetailID(this);
    }


    public DataTable getUserReceivedDealsByUserID()
    {
        return DALDealOrderDetail.getUserReceivedDealsByUserID(this);
    }

    public DataTable getAllUserDealOrderDetailByOrderIDAndUserID()
    {
        return DALDealOrderDetail.getAllUserDealOrderDetailByOrderIDAndUserID(this);
    }

    public int updateDealOrderDetailsByDetailID()
    {
        return DALDealOrderDetail.updateDealOrderDetailsByDetailID(this);
    }

    public int updateDealOrderDetailEmailByID()
    {
        return DALDealOrderDetail.updateDealOrderDetailEmailByID(this);
    }

    public DataTable updateDealOrderDetailCapIdByDealCode()
    {
        return DALDealOrderDetail.updateDealOrderDetailCapIdByDealCode(this);
    }

    public bool updateTrackingNumber()
    {
        return DALDealOrderDetail.updateTrackingNumber(this);
    }

    
    #endregion
}