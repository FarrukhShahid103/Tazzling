using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

/// <summary>
/// Summary description for BLLRestaurantAddresses
/// </summary>
public class BLLRestaurantAddresses
{
    private long RaID;
    private long RestaurantId;
    private string RestaurantAddress;
    private string ZipCode;
    private int Cityid;
    private int ProvinceId;
    private string DealCity;
    public BLLRestaurantAddresses()
    {
        RaID = 0;
        RestaurantId = 0;
        RestaurantAddress = "";
        ZipCode = "";
        Cityid = 337;
        ProvinceId = 3;
        DealCity = "";
    }


    public string dealCity
    {
        get { return DealCity; }
        set { DealCity = value; }
    }
    public int cityid
    {
        get { return Cityid; }
        set { Cityid = value; }
    }

    public int provinceId
    {
        get { return ProvinceId; }
        set { ProvinceId = value; }
    }

    public long raID
    {
        get { return RaID; }
        set { RaID = value; }
    }

    public long restaurantId
    {
        get { return RestaurantId; }
        set { RestaurantId = value; }
    }
    public string restaurantAddress
    {
        get { return RestaurantAddress; }
        set { RestaurantAddress = value; }
    }

    public string zipCode
    {
        get { return ZipCode; }
        set { ZipCode = value; }
    }

    

    public long createRestaurantAddresse()
    {
        return DALRestaurantAddresses.createRestaurantAddresse(this);
    }

    public int updateRestaurantAddresses()
    {
        return DALRestaurantAddresses.updateRestaurantAddresses(this);
    }

    public int deleteRestaurantAddresses()
    {
        return DALRestaurantAddresses.deleteRestaurantAddresses(this);
    }

    public DataTable getAllRestaurantAddressesByRestaurantID()
    {
        return DALRestaurantAddresses.getAllRestaurantAddressesByRestaurantID(this);
    }

    public DataTable getRestaurantAddressesByID()
    {
        return DALRestaurantAddresses.getRestaurantAddressesByID(this);
    }

    public DataTable getAllRestaurantAddressesByDealID(string strDealId)
    {
        return DALRestaurantAddresses.getAllRestaurantAddressesByDealID(strDealId);
    }

    //public bool getCityByName()
    //{
    //    return DALRestaurantAddresses.getCityByName(this);
    //}

    //public DataTable getCitiesByProvinceId()
    //{
    //    return DALRestaurantAddresses.getCitiesByProvinceId(this);
    //}
}
