using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

/// <summary>
/// Summary description for BLLCities
/// </summary>
public class BLLDealCity
{
    private long dealId;
    private int cityId;
    private int dealSlotC;
    private DateTime dealStartTimeC;
    private DateTime dealEndTimeC;
    public BLLDealCity()
    {
        dealId = 0;
        cityId = 0;
        dealSlotC = 0;
        dealStartTimeC = DateTime.Now;
        dealEndTimeC = DateTime.Now;
    }

    public DateTime DealStartTimeC
    {
        set { dealStartTimeC = value; }
        get { return dealStartTimeC; }
    }

    public DateTime DealEndTimeC
    {
        set { dealEndTimeC = value; }
        get { return dealEndTimeC; }
    }



    public int DealSlotC
    {
        get { return dealSlotC; }
        set { dealSlotC = value; }
    }
    public int CityId
    {
        get { return cityId; }
        set { cityId = value; }
    }

    public long DealId
    {
        get { return dealId; }
        set { dealId = value; }
    }
    public int createDealCity()
    {
        return DALDealCity.createDealCity(this);
    }

    public int updateDealCity()
    {
        return DALDealCity.updateDealCity(this);
    }

    public int deleteDealCityByDealID()
    {
        return DALDealCity.deleteDealCityByDealID(this);
    }

    public DataTable getDealCityListByDealId()
    {
        return DALDealCity.getDealCityListByDealId(this);
    }

    //public DataTable getCityByCityId()
    //{
    //    return DALDealCity.getCityByCityId(this);
    //}

    //public bool getCityByName()
    //{
    //    return DALDealCity.getCityByName(this);
    //}

    //public DataTable getCityDetailByName()
    //{
    //    return DALDealCity.getCityDetailByName(this);
    //}



    //public DataTable getCitiesByProvinceId()
    //{
    //    return DALDealCity.getCitiesByProvinceId(this);
    //}
}
