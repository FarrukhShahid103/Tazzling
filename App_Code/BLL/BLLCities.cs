using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

/// <summary>
/// Summary description for BLLCities
/// </summary>
public class BLLCities
{
    private int CityId;
    private string CityName;
    private int ProvinceId;
    private string CityDescription;
    public BLLCities()
    {
        CityId = 0;
        CityName = "";
        ProvinceId = 0;
        CityDescription = "";
    }

    public int cityId
    {
        get { return CityId; }
        set { CityId = value; }
    }

    public string cityName
    {
        get { return CityName; }
        set { CityName = value; }
    }
    public int provinceId
    {
        get { return ProvinceId; }
        set { ProvinceId = value; }
    }
    public string cityDescription
    {
        get { return CityDescription; }
        set { CityDescription = value; }
    }



    public int createCity()
    {
        return DALCities.createCity(this);
    }

    public int updateCity()
    {
        return DALCities.updateCity(this);
    }

    public int deleteCity()
    {
        return DALCities.deleteCity(this);
    }

    public DataTable getAllCities()
    {
        return DALCities.getAllCities(this);
    }

    public DataTable getAllCitiesWithProvinceAndCountryInfo()
    {
        return DALCities.getAllCitiesWithProvinceAndCountryInfo(this);
    }

    public DataTable getCityByCityId()
    {
        return DALCities.getCityByCityId(this);
    }

    public bool getCityByName()
    {
        return DALCities.getCityByName(this);
    }

    public DataTable getCityDetailByName()
    {
        return DALCities.getCityDetailByName(this);
    }



    public DataTable getCitiesByProvinceId()
    {
        return DALCities.getCitiesByProvinceId(this);
    }

    public DataTable getAllCitiesForAdmin()
    {
        return DALCities.getAllCitiesForAdmin(this);
    }

    public DataTable GetAllCitiesForSearch()
    {
        return DALCities.GetAllCitiesForSearch();
    }


}
