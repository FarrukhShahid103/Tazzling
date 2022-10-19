using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

/// <summary>
/// Summary description for BLLMemberMenu
/// </summary>
public class BLLMemberMenu
{
    #region Private Variables
    private long FoodTypeId;
    private string FoodType;
    private string FoodImage;
    private long FoodTemplateId;
    private long CuisineID;
    private long createdby;
    private DateTime creationdate;
    private long modifiedby;
    private DateTime modifieddate;
    #endregion

    #region Constructor
    public BLLMemberMenu()
    {
        FoodTypeId = 0;
        FoodTemplateId = 0;
        FoodType = "";
        FoodImage = "";
        CuisineID = 0;
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

    public long foodTemplateId
    {
        set { FoodTemplateId = value; }
        get { return FoodTemplateId; }
    }

    public long cuisineID
    {
        set { CuisineID = value; }
        get { return CuisineID; }
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
    public long createMemberMenu()
    {
        return DALMemberMenu.createMemberMenu(this);
    }

    public int deleteMemberMenu()
    {
        return DALMemberMenu.deleteMemberMenu(this);
    }

    public int updateMemberMenu()
    {
        return DALMemberMenu.updateMemberMenu(this);
    }
    
    public int updateMemberMenuByCosineIDAndUserID()
    {
        return DALMemberMenu.updateMemberMenuByCosineIDAndUserID(this);
    }

    public DataTable getAllMemberMenuByCuisineIDAndUserID()
    {
        return DALMemberMenu.getAllMemberMenuByCuisineIDAndUserID(this);
    }

    public DataSet getAllMemberMenuAndItemsByRestaurantID()
    {
        return DALMemberMenu.getAllMemberMenuAndItemsByRestaurantID(this);
    }        

    public DataTable getAllMemberMenuByRestaurantID()
    {
        return DALMemberMenu.getAllMemberMenuByRestaurantID(this);
    }

    public int deleteMemberMenuByCuisineIDAndUserID()
    {
        return DALMemberMenu.deleteMemberMenuByCuisineIDAndUserID(this);
    }
    public DataTable getRestaurantMenuByRestaurantID()
    {
        return DALMemberMenu.getRestaurantMenuByRestaurantID(this);
    }
    
    #endregion
}
