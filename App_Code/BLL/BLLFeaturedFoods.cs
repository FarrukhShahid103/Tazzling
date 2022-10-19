using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

/// <summary>
/// Summary description for BLLFeaturedFoods
/// </summary>
public class BLLFeaturedFoods
{
    #region "Private Variables"

    private long FeaturedFoodID;
    private long RestaurantID;
    private string FoodName;
    private string FoodDescription;
    private float FoodPrice;
    private string FoodImage;
    private DateTime CreationDate;
    private long CreatedBy;
    private DateTime ModifiedDate;
    private long ModifiedBy;
    private bool IsFeatured;
    private bool FoodAddedBy;

    #endregion

    #region "Constructor"

    public BLLFeaturedFoods()
    {
        //
        // TODO: Add constructor logic here
        //

        FeaturedFoodID = 0;
        RestaurantID = 0;
        FoodName = "";
        FoodDescription = "";
        FoodPrice = 0;
        FoodImage = "";
        CreationDate = DateTime.Now;
        CreatedBy = 0;
        ModifiedDate = DateTime.Now;
        ModifiedBy = 0;
        IsFeatured = true;
        FoodAddedBy = false;
    }

    #endregion

    #region "Properties"

    public long featuredFoodID
    {
        set { FeaturedFoodID = value; }
        get { return FeaturedFoodID; }        
    }

    public long restaurantID
    {
        set { RestaurantID = value; }
        get { return RestaurantID; }
    }

    public string foodName
    {
        set { FoodName = value; }
        get { return FoodName; }
    }

    public string foodDescription
    {
        set { FoodDescription = value; }
        get { return FoodDescription; }
    }

    public float foodPrice
    {
        set { FoodPrice = value; }
        get { return FoodPrice; }
    }

    public string foodImage
    {
        set { FoodImage = value; }
        get { return FoodImage; }
    }

    public DateTime creationDate
    {
        set { CreationDate = value; }
        get { return CreationDate; }
    }

    public long createdBy
    {
        set { CreatedBy = value; }
        get { return CreatedBy; }
    }

    public DateTime modifiedDate
    {
        set { ModifiedDate = value; }
        get { return ModifiedDate; }
    }

    public long modifiedBy
    {
        set { ModifiedBy = value; }
        get { return ModifiedBy; }
    }

    public bool isFeatured
    {
        set { IsFeatured = value; }
        get { return IsFeatured; }
    }

    public bool foodAddedBy
    {
        set { FoodAddedBy = value; }
        get { return FoodAddedBy; }
    }

    #endregion

    #region "Function of Add New Featured Food"

    public int createNewFeaturedFood()
    {
        return DALFeaturedFoods.createNewFeaturedFood(this);
    }

    #endregion

    #region "Get Featured Food By Created By ID"

    public DataTable getFeaturedFoodByCreatedByID()
    {
        return DALFeaturedFoods.getFeaturedFoodByCreatedByID(this);
    }

    #endregion

    #region "Delete Featured Food on the basis of the Featured Food ID"

    public int DeleteFeaturedFoodByFoodID()
    {
        return DALFeaturedFoods.DeleteFeaturedFoodByFoodID(this);
    }

    #endregion

    #region "Update Featured Food on the basis of the Featured Food ID"

    public int UpdateFeaturedFoodByFoodID()
    {
        return DALFeaturedFoods.UpdateFeaturedFoodByFoodID(this);
    }

    #endregion


    #region "Get Featured Food added by the Administrator"

    public DataTable GetFeaturedFoodAddedByAdmin()
    {
        return DALFeaturedFoods.GetFeaturedFoodAddedByAdmin();
    }

    #endregion

    #region "Get Featured Food added by the Restaurant ID"

    public DataTable GetFeaturedFoodByRestaurantID(string strResUser)
    {
        return DALFeaturedFoods.GetFeaturedFoodByRestaurantID(strResUser);
    }

    #endregion
}