using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

/// <summary>
/// Summary description for BLLMemberMenu
/// </summary>
public class BLLMemberDeal
{
    #region Private Variables
    private long DealId;
    private long RestaurantId;
    private string DealName;
    private string DealImage; 
    private int DealOrderItemsQty;
    private float DealPrice;
    private long CosineID;
    private long createdby;
    private DateTime creationdate;
    private long modifiedby;
    private DateTime modifieddate;
    #endregion

    #region Constructor
    public BLLMemberDeal()
	{
        DealId = 0;        
        DealName = "";
        DealImage = "";
        CosineID = 0;
        createdby = 0;
        creationdate = DateTime.Now;
        modifiedby = 0;
        modifieddate = DateTime.Now;
        RestaurantId = 0;
    }
    #endregion

    #region Properties
    public long restaurantId
    {
        set { RestaurantId = value; }
        get { return RestaurantId; }
    }

    public long dealId
    {
        set { DealId = value; }
        get { return DealId; }
    }
   
    public long cuisineID
    {
        set { CosineID = value; }
        get { return CosineID; }
    }

    public string dealImage
    {
        set { DealImage = value; }
        get { return DealImage; }
    }

    public string dealName
    {
        set { DealName = value; }
        get { return DealName; }
    }

    public int dealOrderItemsQty
    {
        set { DealOrderItemsQty = value; }
        get { return DealOrderItemsQty; }
    }

    public float dealPrice
    {
        set { DealPrice = value; }
        get { return DealPrice; }
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
    public long createMemberDeal()
    {
        return DALMemberDeal.createMemberDeal(this);
    }

    public int deleteMemberDeal()
    {
        return DALMemberDeal.deleteMemberDeal(this);
    }

    public int deleteMemberDealByCuisineIDAndUserID()
    {
        return DALMemberDeal.deleteMemberDealByCuisineIDAndUserID(this);
    }

    public int updateMemberDeal()
    {
        return DALMemberDeal.updateMemberDeal(this);
    }

    public DataTable getMemberDealByCuisineID()
    {
        return DALMemberDeal.getMemberDealByCuisineID(this);
    }

    public DataTable getMemberDealByRestaurantID()
    {
        return DALMemberDeal.getMemberDealByRestaurantID(this);
    }

    public DataTable getMemberDealByCuisineIDAndUserID()
    {
        return DALMemberDeal.getMemberDealByCuisineIDAndUserID(this);
    }
    
    #endregion
}
