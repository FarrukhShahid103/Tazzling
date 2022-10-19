using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

/// <summary>
/// Summary description for BLLFavoriteRestaurants
/// </summary>
public class BLLFavoriteRestaurants
{
    #region Private Variables
    private long FavoriteId;
    private long RestaurantId;
    private DateTime CreationDate;
    private long CreatedBy;
    private DateTime Modifieddate;
    private long ModifiedBy;
    #endregion

    #region Constructor
    public BLLFavoriteRestaurants()
    {
        FavoriteId = 0;
        RestaurantId = 0;
        CreationDate = DateTime.Now;
        CreatedBy = 0;
        Modifieddate = DateTime.Now;
        ModifiedBy = 0;
    }
    #endregion

    #region Properties
    public long favoriteId
    {
        set { FavoriteId = value; }
        get { return FavoriteId; }
    }

    public long createdBy
    {
        set { CreatedBy = value; }
        get { return CreatedBy; }
    }

    public long modifiedBy
    {
        set { ModifiedBy = value; }
        get { return ModifiedBy; }
    }

    public long restaurantId
    {
        set { RestaurantId = value; }
        get { return RestaurantId; }
    }

    public DateTime creationDate
    {
        set { CreationDate = value; }
        get { return CreationDate; }
    }

    public DateTime modifiedDate
    {
        set { Modifieddate = value; }
        get { return Modifieddate; }
    }
    #endregion

    #region Functions
    public int createFavoriteRestaurants()
    {
        return DALFavoriteRestaurants.createFavoriteRestaurants(this);
    }

    public int deleteFavoriteRestaurants()
    {
        return DALFavoriteRestaurants.deleteFavoriteRestaurants(this);
    }

    public int updateFavoriteRestaurants()
    {
        return DALFavoriteRestaurants.updateFavoriteRestaurants(this);
    }

    public DataTable getAllFavoriteRestaurantsByUserID()
    {
        return DALFavoriteRestaurants.getAllFavoriteRestaurantsByUserID(this);
    }

    public DataTable getFavoriteRestaurantByUserIDAndRestaurantID()
    {
        return DALFavoriteRestaurants.getFavoriteRestaurantByUserIDAndRestaurantID(this);
    }
    #endregion

}
