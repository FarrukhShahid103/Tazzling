using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;


/// <summary>
/// Summary description for BLLOrderItem
/// </summary>
public class BLLOrderItem
{
    #region Private Variables
    private long OrderItemId;
    private long MenuItemId;
    private long OrderId;
    private long FoodTypeID;
    private int Quantity;
    private float UnitPrice;
    private string FoodType;
    private string ItemName;    
    private long createdby;
    private DateTime creationdate;
    private long modifiedby;
    private DateTime modifieddate;
    private string Instruction;
    #endregion

    #region Constructor
	public BLLOrderItem()
	{
        OrderItemId = 0;
        MenuItemId = 0;
        OrderId = 0;
        FoodTypeID = 0;
        UnitPrice = 0;
        Quantity = 0;
        FoodType = "";
        ItemName = "";        
        createdby = 0;
        creationdate = DateTime.Now;
        modifiedby = 0;
        modifieddate = DateTime.Now;
        Instruction = "";
    }
    #endregion

    #region Properties
    public long orderItemId
    {
        set { OrderItemId = value; }
        get { return OrderItemId; }
    }

    public float unitPrice
    {
        set { UnitPrice = value; }
        get { return UnitPrice; }
    }

    public long menuItemId
    {
        set { MenuItemId = value; }
        get { return MenuItemId; }
    }

    public long orderId
    {
        set { OrderId = value; }
        get { return OrderId; }
    }

    public long foodTypeID
    {
        set { FoodTypeID = value; }
        get { return FoodTypeID; }
    }

    public int quantity
    {
        set { Quantity = value; }
        get { return Quantity; }
    }
    public string itemName
    {
        set { ItemName = value; }
        get { return ItemName; }
    }

    public string foodType
    {
        set { FoodType = value; }
        get { return FoodType; }
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
    public string instruction
    {
        set { Instruction = value; }
        get { return Instruction; }
    }

    #endregion

    #region Functions
    public int createOrderItem()
    {
        return DALOrderItem.createOrderItem(this);
    }

    public int deleteOrderItem()
    {
        return DALOrderItem.deleteOrderItem(this);
    }

    public DataTable getAllOrderItemByOrderID()
    {
        return DALOrderItem.getAllOrderItemByOrderID(this);
    }
    #endregion
}
