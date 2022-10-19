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
/// Summary description for BLLAffiliatePartnerGained
/// </summary>
public class BLLAffiliatePartnerGained
{
    private int affiliatePartnerId;
    private int userId;
    private double gainedAmount;
    private string gainedType;
    private double remainAmount;
    private int fromId;
    private DateTime createdDate;
    private int createdBy;
    private DateTime modifiedDate;
    private int modifiedBy;
    private int orderId;
    private float affCommPer;

    public BLLAffiliatePartnerGained()
    {
        //
        // TODO: Add constructor logic here
        //

        affiliatePartnerId = 0;
        userId = 0;
        gainedAmount = 0;
        gainedType = "";
        remainAmount = 0;
        fromId = 0;
        createdDate = DateTime.Now;
        createdBy = 0;
        modifiedDate = DateTime.Now;
        modifiedBy = 0;
        orderId = 0;
        affCommPer = 0;
    }

    public int AffiliatePartnerId
    {
        set { affiliatePartnerId = value; }
        get { return affiliatePartnerId; }
    }
    public int UserId
    {
        set { userId = value; }
        get { return userId; }
    }
    public double GainedAmount
    {
        set { gainedAmount = value; }
        get { return gainedAmount; }
    }
    public string GainedType
    {
        set { gainedType = value; }
        get { return gainedType; }
    }
    public double RemainAmount
    {
        set { remainAmount = value; }
        get { return remainAmount; }
    }
    public int FromId
    {
        set { fromId = value; }
        get { return fromId; }
    }
    public DateTime CreatedDate
    {
        set { createdDate = value; }
        get { return createdDate; }
    }
    public int CreatedBy
    {
        set { createdBy = value; }
        get { return createdBy; }
    }
    public DateTime ModifiedDate
    {
        set { modifiedDate = value; }
        get { return modifiedDate; }
    }
    public int ModifiedBy
    {
        set { modifiedBy = value; }
        get { return modifiedBy; }
    }
    public int OrderId
    {
        set { orderId = value; }
        get { return orderId; }
    }
    public float AffCommPer
    {
        set { affCommPer = value; }
        get { return affCommPer; }
    }

    public DataTable getGetAffiliatePartnerGainedCreditsByUserID()
    {
        return DALAffiliatePartnerGained.getGetAffiliatePartnerGainedCreditsByUserID(this);
    }

    public DataTable getGetAffiliatePartnerTotalEarnedByUserID()
    {
        return DALAffiliatePartnerGained.getGetAffiliatePartnerTotalEarnedByUserID(this);
    }

    public DataTable getGetAffiliateGainedCreditsByUserID(DateTime start, DateTime end)
    {
        return DALAffiliatePartnerGained.getGetAffiliateGainedCreditsByUserID(this, start, end);
    }

    public int createAffiliatePartnerGained()
    {
        return DALAffiliatePartnerGained.createAffiliatePartnerGained(this);
    }

    public DataTable getGetAllAffiliatePartnerGainedByUserID()
    {
        return DALAffiliatePartnerGained.getGetAllAffiliatePartnerGainedByUserID(this);
    }

    public int updateAffiliateRemainingUsableAmount()
    {
        return DALAffiliatePartnerGained.updateAffiliateRemainingUsableAmount(this);
    }

    public int updateAffiliateCommisionByOrderId()
    {
        return DALAffiliatePartnerGained.updateAffiliateCommisionByOrderId(this);
    }

    public DataTable getGetAffiliatePartnerCommisionInfoByOrderID()
    {
        return DALAffiliatePartnerGained.getGetAffiliatePartnerCommisionInfoByOrderID(this);
    }
}