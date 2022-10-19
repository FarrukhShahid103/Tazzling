using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

/// <summary>
/// Summary description for BLLMemberMenuItems
/// </summary>
public class BLLMemberMenuItems
{
    #region Private Variables
    private long MenuItemId;
    private string ItemName;
    private string ItemSubname;
    private string ItemDescription;
    private float ItemPrice;
    private long FoodTypeId;    
    private long createdby;
    private DateTime creationdate;
    private long modifiedby;
    private DateTime modifieddate;    
    #endregion

    #region Constructor
    public BLLMemberMenuItems()
	{
        MenuItemId = 0;
        ItemPrice = 0;
        ItemName = "";
        ItemSubname = "";
        ItemDescription = "";
        FoodTypeId = 0;
        createdby = 0;
        creationdate = DateTime.Now;
        modifiedby = 0;
        modifieddate = DateTime.Now;
        
    }
    #endregion

    #region Properties
    public long menuItemId
    {
        set { MenuItemId = value; }
        get { return MenuItemId; }
    }

    public float itemPrice
    {
        set { ItemPrice = value; }
        get { return ItemPrice; }
    }

    public long foodTypeId
    {
        set { FoodTypeId = value; }
        get { return FoodTypeId; }
    }

    public string itemName
    {
        set { ItemName = value; }
        get { return ItemName; }
    }

    public string itemSubname
    {
        set { ItemSubname = value; }
        get { return ItemSubname; }
    }

    public string itemDescription
    {
        set { ItemDescription = value; }
        get { return ItemDescription; }
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
    public int createMemberMenuItems()
    {
        return DALMemberMenuItems.createMemberMenuItems(this);
    }

    public int deleteMemberMenuItems()
    {
        return DALMemberMenuItems.deleteMemberMenuItems(this);
    }

    public int updateMemberMenuItems()
    {
        return DALMemberMenuItems.updateMemberMenuItems(this);
    }

    public DataTable getMemberMenuItemsByFoodTypeID()
    {
        return DALMemberMenuItems.getMemberMenuItemsByFoodTypeID(this);
    }

    public DataTable getMemberMenuItemsByFoodTypeIDAndMenuName()
    {
        return DALMemberMenuItems.getMemberMenuItemsByFoodTypeIDAndMenuName(this);
    }

    public DataTable spGetMemberMenuItemsUniqueNamesByFoodTypeID()
    {
        return DALMemberMenuItems.spGetMemberMenuItemsUniqueNamesByFoodTypeID(this);
    }

    public DataSet getMemberMenuItemsByFoodTypeIDForExport()
    {
        return DALMemberMenuItems.getMemberMenuItemsByFoodTypeIDForExport(this);
    }

    public DataTable getMemberMenuItemsFeaturedFood()
    {
        return DALMemberMenuItems.getMemberMenuItemsFeaturedFood();
    }
    #endregion
}
