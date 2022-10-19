using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

/// <summary>
/// Summary description for BLLGiftCard
/// </summary>
public class BLLGiftCard
{
    #region Private Variables
    private long GiftCardId;
    private long GiftCardOrderId;
    private string GiftCardStyle;
    private string Comments;
    private string GiftCardCode;
    private float GiftCardAmount;
    private string CurrencyCode;
    private long TakenBy;
    private DateTime ExpirationDate;
    private DateTime CreationDate;
    private long CreatedBy;
    private DateTime Modifieddate;
    private long ModifiedBy;
    private string OrderRefNoSent;
    private string OrderIdReceived;
    private string OrderStatus;
    #endregion

    #region Constructor
    public BLLGiftCard()
    {
        GiftCardId = 0;
        GiftCardStyle = "";
        Comments = "";
        GiftCardCode = "";
        GiftCardAmount = 0;
        CurrencyCode = "";
        TakenBy = 0;
        ExpirationDate = DateTime.Now;
        CreationDate = DateTime.Now;
        CreatedBy = 0;
        Modifieddate = DateTime.Now;
        ModifiedBy = 0;
        OrderRefNoSent = "";
        OrderIdReceived = "";
        OrderStatus = "";
        GiftCardOrderId = 0;
    }
    #endregion

    #region Properties
    public long giftCardOrderId
    {
        set { GiftCardOrderId = value; }
        get { return GiftCardOrderId; }
    }

    public long giftCardId
    {
        set { GiftCardId = value; }
        get { return GiftCardId; }
    }

    public float giftCardAmount
    {
        set { GiftCardAmount = value; }
        get { return GiftCardAmount; }
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

    public long takenBy
    {
        set { TakenBy = value; }
        get { return TakenBy; }
    }

    public string giftCardStyle
    {
        set { GiftCardStyle = value; }
        get { return GiftCardStyle; }
    }

    public string comments
    {
        set { Comments = value; }
        get { return Comments; }
    }

    public string giftCardCode
    {
        set { GiftCardCode = value; }
        get { return GiftCardCode; }
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

    public DateTime expirationDate
    {
        set { ExpirationDate = value; }
        get { return ExpirationDate; }
    }

    public string orderRefNoSent
    {
        set { OrderRefNoSent = value; }
        get { return OrderRefNoSent; }
    }

    public string orderIdReceived
    {
        set { OrderIdReceived = value; }
        get { return OrderIdReceived; }
    }

    public string orderStatus
    {
        set { OrderStatus = value; }
        get { return OrderStatus; }
    }

    #endregion

    #region Functions
    public int createGiftCardByUser()
    {
        return DALGiftCard.createGiftCardByUser(this);
    }

    public int createGiftCard()
    {
        return DALGiftCard.createGiftCard(this);
    }

    public long createGiftCardByAdmin()
    {
        return DALGiftCard.createGiftCardByAdmin(this);
    }




    public int deleteGiftCardByUser()
    {
        return DALGiftCard.deleteGiftCardByUser(this);
    }

    public int updateGiftCardByUser()
    {
        return DALGiftCard.updateGiftCardByUser(this);
    }

    public int updateGiftCardByCodeAndUserID()
    {
        return DALGiftCard.updateGiftCardByCodeAndUserID(this);
    }

    public int updateGiftCardByOrderRefNoSent()
    {
        return DALGiftCard.updateGiftCardByOrderRefNoSent(this);
    }

    public DataTable getGiftCardByCode()
    {
        return DALGiftCard.getGiftCardByCode(this);
    }

    public DataTable getGiftCardByUserID()
    {
        return DALGiftCard.getGiftCardByUserID(this);
    }

    public DataTable getGiftCardInfoByOrderRefNoSent()
    {
        return DALGiftCard.getGiftCardInfoByOrderRefNoSent(this);
    }

    public DataTable getApprovedGiftCardByCode()
    {
        return DALGiftCard.getApprovedGiftCardByCode(this);
    }

    public int updateGiftCardTakenByValue()
    {
        return DALGiftCard.updateGiftCardTakenByValue(this);
    }

    public DataTable getAllApprovedGiftCards()
    {
        return DALGiftCard.getAllApprovedGiftCards();
    }
    public int updateGiftCardSetUnApproved()
    {
        return DALGiftCard.updateGiftCardSetUnApproved(this);
    }

    public DataTable getGiftCardByID()
    {
        return DALGiftCard.getGiftCardByID(this);
    }


    public int createGiftCardAssignedByAdmin()
    {
        return DALGiftCard.createGiftCardAssignedByAdmin(this);
    }
    #endregion

}
