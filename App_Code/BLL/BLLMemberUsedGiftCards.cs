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
using SQLHelper;
using System.Data.SqlClient;
/// <summary>
/// Summary description for BLLMemberUsedGiftCards
/// </summary>
public class BLLMemberUsedGiftCards
{
    #region Private Variables
    private long GainedId;
    private string GainedType;
    private float GainedAmount;
    private float RemainAmount;
    private long FromId;
    private string CurrencyCode;
    private DateTime CreationDate;
    private long CreatedBy;
    private DateTime ModifiedDate;
    private long ModifiedBy;
    private DateTime TargetDate;
    private long OrderId;
    #endregion

    #region Constructor
    public BLLMemberUsedGiftCards()
    {
        GainedId = 0;
        GainedType = "Gift Card";
        GainedAmount = 0;
        RemainAmount = 0;
        FromId = 0;
        CurrencyCode = "CAD";
        CreationDate = DateTime.Now;
        CreatedBy = 0;
        ModifiedDate = DateTime.Now;
        ModifiedBy = 0;
        TargetDate = DateTime.Now;
        OrderId = 0;
    }
    #endregion

    #region Properties
    public long gainedId
    {
        set { GainedId = value; }
        get { return GainedId; }
    }

    public long fromId
    {
        set { FromId = value; }
        get { return FromId; }
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

    public string gainedType
    {
        set { GainedType = value; }
        get { return GainedType; }
    }

    public string currencyCode
    {
        set { CurrencyCode = value; }
        get { return CurrencyCode; }
    }

    public float gainedAmount
    {
        set { GainedAmount = value; }
        get { return GainedAmount; }
    }

    public float remainAmount
    {
        set { RemainAmount = value; }
        get { return RemainAmount; }
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

    public DateTime targetDate
    {
        set { TargetDate = value; }
        get { return TargetDate; }
    }

    public long orderId
    {
        set { OrderId = value; }
        get { return OrderId; }
    }

    #endregion

    #region Functions
    public int createMemberUseableGiftCard()
    {
        return DALMemberUsedGiftCards.createMemberUseableGiftCard(this);
    }

    public DataTable getAllUseableCardsByUserID()
    {
        return DALMemberUsedGiftCards.getAllUseableCardsByUserID(this);
    }

    public DataTable getAllRefferalTastyCreditsByUserID()
    {
        return DALMemberUsedGiftCards.getAllRefferalTastyCreditsByUserID(this);
    }

    public DataTable getTastyCreditsByOrderID()
    {
        return DALMemberUsedGiftCards.getTastyCreditsByOrderID(this);
    }

    

    public int updateRemainingUsableAmount()
    {
        return DALMemberUsedGiftCards.updateRemainingUsableAmount(this);
    }

    public DataTable getUseableFoodCreditByUserID()
    {
        return DALMemberUsedGiftCards.getUseableFoodCreditByUserID(this);
    }

    public DataTable getUseableFoodCreditRefferalByUserID()
    {
        return DALMemberUsedGiftCards.getUseableFoodCreditRefferalByUserID(this);
    }

    public int updateCreditOnDecByOrderId()
    {
        return DALMemberUsedGiftCards.updateCreditOnDecByOrderId(this);
    }

    public int updateCreditByOrderId()
    {
        return DALMemberUsedGiftCards.updateCreditByOrderId(this);
    }           
    #endregion
}
