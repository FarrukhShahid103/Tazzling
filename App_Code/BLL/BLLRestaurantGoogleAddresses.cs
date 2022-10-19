using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

/// <summary>
/// Summary description for BLLRestaurantAddresses
/// </summary>
public class BLLRestaurantGoogleAddresses
{
    private long RgaID;
    private long RestaurantId;
    private string RestaurantGoogleAddresses;    
    public BLLRestaurantGoogleAddresses()
    {
        RgaID = 0;
        RestaurantId = 0;
        RestaurantGoogleAddresses = "";      
    }

    public long rgaID
    {
        get { return RgaID; }
        set { RgaID = value; }
    }

    public long restaurantId
    {
        get { return RestaurantId; }
        set { RestaurantId = value; }
    }
    public string restaurantGoogleAddress
    {
        get { return RestaurantGoogleAddresses; }
        set { RestaurantGoogleAddresses = value; }
    }
     
    public long createRestaurantGoogleAddresses()
    {
        return DALRestaurantGoogleAddresses.createRestaurantGoogleAddresses(this);
    }

    public int updateRestaurantGoogleAddresses()
    {
        return DALRestaurantGoogleAddresses.updateRestaurantGoogleAddresses(this);
    }

    public int deleteRestaurantGoogleAddresses()
    {
        return DALRestaurantGoogleAddresses.deleteRestaurantGoogleAddresses(this);
    }

    public int deleteAllRestaurantGoogleAddressesByRestaurantID()
    {
        return DALRestaurantGoogleAddresses.deleteAllRestaurantGoogleAddressesByRestaurantID(this);
    }

    

    public DataTable getAllRestaurantGoogleAddressesByRestaurantID()
    {
        return DALRestaurantGoogleAddresses.getAllRestaurantGoogleAddressesByRestaurantID(this);
    }

    public DataTable getRestaurantGoogleAddressesByID()
    {
        return DALRestaurantGoogleAddresses.getRestaurantGoogleAddressesByID(this);
    }

    public DataTable getAllRestaurantGoogleAddressesByDealID(string strDealId)
    {
        return DALRestaurantGoogleAddresses.getAllRestaurantGoogleAddressesByDealID(strDealId);
    }
    
}
