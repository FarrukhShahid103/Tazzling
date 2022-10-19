using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

/// <summary>
/// Summary description for BLLRestaurantMenu
/// </summary>
public class BLLRestaurantMenu
{
    #region Private Variables
    private long FoodTypeId;
    private string FoodType;
    private string FoodImage;    
    private long CosineID;
    private long createdby;
    private DateTime creationdate;
    private long modifiedby;
    private DateTime modifieddate;
    #endregion

    #region Constructor
    public BLLRestaurantMenu()
	{
        FoodTypeId = 0;        
        FoodType = "";
        FoodImage = "";
        CosineID = 0;
        createdby = 0;
        creationdate = DateTime.Now;
        modifiedby = 0;
        modifieddate = DateTime.Now;
    }
    #endregion

    #region Properties
    public long foodTypeId
    {
        set { FoodTypeId = value; }
        get { return FoodTypeId; }
    }
   
    public long cuisineID
    {
        set { CosineID = value; }
        get { return CosineID; }
    }

    public string foodImage
    {
        set { FoodImage = value; }
        get { return FoodImage; }
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

    #endregion

    #region Functions
    public long createRestaurantMenu()
    {
        return DALRestaurantMenu.createRestaurantMenu(this);
    }

    public int deleteRestaurantMenu()
    {
        return DALRestaurantMenu.deleteRestaurantMenu(this);
    }

    public int deleteRestaurantMenuByCuisineID()
    {
        return DALRestaurantMenu.deleteRestaurantMenuByCuisineID(this);
    }    

    public int updateRestaurantMenu()
    {
        return DALRestaurantMenu.updateRestaurantMenu(this);
    }

    public DataTable getRestaurantMenuByCuisineID()
    {
        return DALRestaurantMenu.getRestaurantMenuByCuisineID(this);
    }
    #endregion
}
