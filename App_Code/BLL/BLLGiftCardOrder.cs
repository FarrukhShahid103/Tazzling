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
/// Summary description for BLLGiftCardOrder
/// </summary>
public class BLLGiftCardOrder
{
    private long GiftCardOrderId;
	private double SubTotalAmount;
    private double Commission;
    private double Redeem;
	private long CreatedBy;
	private DateTime CreatedOn;
    private long ModifiedBy;
    private DateTime ModifiedOn;
    private string OrderRefNo;
    private string Comments;
    private double CreditCard;
    private string OrderStatus;
    private string OrderIdReceived;
    private long CCInfoID = 0;

    public BLLGiftCardOrder()
    {
        GiftCardOrderId = 0;
        SubTotalAmount = 0;
        Commission = 0;
        Redeem = 0;
        CreatedBy = 0;
        CreatedOn = DateTime.Now;
        ModifiedBy = 0;
        CCInfoID = 0;
        ModifiedOn = DateTime.Now;
        OrderRefNo = "";
        Comments = "";
        CreditCard=0;
        OrderStatus = "";
        OrderIdReceived = "";
    }

    public string orderStatus
    {
        set { OrderStatus = value; }
        get { return OrderStatus; }
    }
    public string orderIdReceived
    {
        set { OrderIdReceived = value; }
        get { return OrderIdReceived; }
    }

    public long ccInfoID
    {
        set { CCInfoID = value; }
        get { return CCInfoID; }
    }
    
    public long giftCardOrderId
    {
        set { GiftCardOrderId = value; }
        get { return GiftCardOrderId; }
    }
    public double subTotalAmount
    {
        set { SubTotalAmount = value; }
        get { return SubTotalAmount; }
    }
    public double commission
    {
        set { Commission = value; }
        get { return Commission; }
    }
    public double creditCard
    {
        set { CreditCard = value; }
        get { return CreditCard; }
    }
    public double redeem
    {
        set { Redeem = value; }
        get { return Redeem; }
    }
    public long createdBy
    {
        set { CreatedBy = value; }
        get { return CreatedBy; }
    }
    public DateTime createdOn
    {
        set { CreatedOn = value; }
        get { return CreatedOn; }
    }
    public long modifiedBy
    {
        set { ModifiedBy = value; }
        get { return ModifiedBy; }
    }
    public DateTime modifiedOn
    {
        set { ModifiedOn = value; }
        get { return ModifiedOn; }
    }
    public string orderRefNo
    {
        set { OrderRefNo = value; }
        get { return OrderRefNo; }
    }
    public string comments
    {
        set { Comments = value; }
        get { return Comments; }
    }

    public long createGiftCardOrderByUser()
    {
        return DALGiftCardOrder.createGiftCardOrderByUser(this);
    }
    public long createGiftCardOrder()
    {
        return DALGiftCardOrder.createGiftCardOrder(this);
    }
    public int updateGiftCardOrderByOrderRefNoSent()
    {
        return DALGiftCardOrder.updateGiftCardOrderByOrderRefNoSent(this);
    }
    public DataTable getGiftCardInfoByOrderRefNoSent()
    {
        return DALGiftCardOrder.getGiftCardInfoByOrderRefNoSent(this);
    }
    
}
