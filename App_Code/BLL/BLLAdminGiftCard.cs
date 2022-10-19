using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

/// <summary>
/// Summary description for BLLAdminGiftCard
/// </summary>
public class BLLAdminGiftCard
{
    #region Private Variables
    private long cardId;
    private float cardamount;
    private string cardimage;
    private long createdby;
    private DateTime creationdate;
    private long modifiedby;
    private DateTime modifieddate;
    #endregion

    #region Constructor
    public BLLAdminGiftCard()
    {
        cardId = 0;
        cardamount = 0;
        cardimage = "";
        createdby = 0;
        creationdate = DateTime.Now;
        modifiedby = 0;
        modifieddate = DateTime.Now;
    }
    #endregion

    #region Properties
    public long cardID
    {
        set { cardId = value; }
        get { return cardId; }
    }

    public float cardAmount
    {
        set { cardamount = value; }
        get { return cardamount; }
    }

    public string cardImage
    {
        set { cardimage = value; }
        get { return cardimage; }
    }

    public long createdBy
    {
        set { createdby = value; }
        get { return createdby; }
    }

    public DateTime creationDate
    {
        set { creationdate = value; }
        get { return creationdate; }
    }

    public long modifiedBy
    {
        set { modifiedby = value; }
        get { return modifiedby; }
    }

    public DateTime modifiedDate
    {
        set { modifieddate = value; }
        get { return modifieddate; }
    }

    #endregion

    #region Functions
    public int createAdminGiftCard()
    {
        return DALAdminGiftCard.createAdminGiftCard(this);
    }

    public int deleteAdminGiftCard()
    {
        return DALAdminGiftCard.deleteAdminGiftCard(this);
    }

    public int updateAdminGiftCard()
    {
        return DALAdminGiftCard.updateAdminGiftCard(this);
    }

    public DataTable getAllAdminGiftCard()
    {
        return DALAdminGiftCard.getAllAdminGiftCard();
    }

    public DataTable getAdminGiftCardByID()
    {
        return DALAdminGiftCard.getAdminGiftCardByID(this);
    }

    public DataTable getAdminGiftCardByPrice()
    {
        return DALAdminGiftCard.getAdminGiftCardByPrice(this);
    }
    
    
    #endregion

}
