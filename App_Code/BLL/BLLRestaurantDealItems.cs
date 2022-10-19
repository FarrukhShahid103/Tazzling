using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

/// <summary>
/// Summary description for BLLRestaurantMenuItems
/// </summary>
public class BLLRestaurantDealItems
{
    #region Private Variables
    private long DealItemId;
    private string DealItemName;
    private string DealItemSubname;
    private string DealItemDescription;    
    private long DealId;
    private long createdby;
    private DateTime creationdate;
    private long modifiedby;
    private DateTime modifieddate;
    #endregion

    #region Constructor
    public BLLRestaurantDealItems()
	{
        DealItemId = 0;       
        DealItemName = "";
        DealItemSubname = "";
        DealItemDescription = "";
        DealId = 0;
        createdby = 0;
        creationdate = DateTime.Now;
        modifiedby = 0;
        modifieddate = DateTime.Now;
    }
    #endregion

    #region Properties
    public long dealItemId
    {
        set { DealItemId = value; }
        get { return DealItemId; }
    }
   
    public long dealId
    {
        set { DealId = value; }
        get { return DealId; }
    }

    public string dealItemName
    {
        set { DealItemName = value; }
        get { return DealItemName; }
    }

    public string dealItemSubname
    {
        set { DealItemSubname = value; }
        get { return DealItemSubname; }
    }

    public string dealItemDescription
    {
        set { DealItemDescription = value; }
        get { return DealItemDescription; }
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
    public int createRestaurantDealItems()
    {
        return DALRestaurantDealItems.createRestaurantDealItems(this);
    }

    public int deleteRestaurantDealItems()
    {
        return DALRestaurantDealItems.deleteRestaurantDealItems(this);
    }

    public int updateRestaurantDealItems()
    {
        return DALRestaurantDealItems.updateRestaurantDealItems(this);
    }

    public DataTable getRestaurantDealItemsByDealID()
    {
        return DALRestaurantDealItems.getRestaurantDealItemsByDealID(this);
    }
    
    public DataSet getRestaurantDealItemsByDealIDForExport()
    {
        return DALRestaurantDealItems.getRestaurantDealItemsByDealIDForExport(this);
    }
    
    #endregion
}
