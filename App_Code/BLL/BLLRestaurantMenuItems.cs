using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

/// <summary>
/// Summary description for BLLRestaurantMenuItems
/// </summary>
public class BLLRestaurantMenuItems
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
    public BLLRestaurantMenuItems()
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
    public int createRestaurantMenuItems()
    {
        return DALRestaurantMenuItems.createRestaurantMenuItems(this);
    }

    public int deleteRestaurantMenuItems()
    {
        return DALRestaurantMenuItems.deleteRestaurantMenuItems(this);
    }

    public int updateRestaurantMenuItems()
    {
        return DALRestaurantMenuItems.updateRestaurantMenuItems(this);
    }

    public DataTable getRestaurantMenuItemsByFoodTypeID()
    {
        return DALRestaurantMenuItems.getRestaurantMenuItemsByFoodTypeID(this);
    }

    public DataSet getRestaurantMenuItemsByFoodTypeIDForExport()
    {
        return DALRestaurantMenuItems.getRestaurantMenuItemsByFoodTypeIDForExport(this);
    }

    #endregion
}
