using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

/// <summary>
/// Summary description for BLLCities
/// </summary>
public class BLLSupplierOrders
{
    private long SupplierOrdersID;
    private long SupplierOrdersQty;
    private long ProductID;
    private long SizeID;
    private long CreatedBy;
    private DateTime CreatedDate;
    
    public BLLSupplierOrders()
    {
        SupplierOrdersID = 0;
        SupplierOrdersQty = 0;
        ProductID = 0;
        SizeID = 0;
        CreatedBy = 0;
        CreatedDate = DateTime.Now;
    }

    public DateTime createdDate
    {
        get { return CreatedDate; }
        set { CreatedDate = value; }
    }

    public long createdBy
    {
        get { return CreatedBy; }
        set { CreatedBy = value; }
    }

    public long supplierOrdersID
    {
        get { return SupplierOrdersID; }
        set { SupplierOrdersID = value; }
    }

    public long supplierOrdersQty
    {
        get { return SupplierOrdersQty; }
        set { SupplierOrdersQty = value; }
    }

    public long productID
    {
        get { return ProductID; }
        set { ProductID = value; }
    }

    public long sizeID
    {
        get { return SizeID; }
        set { SizeID = value; }
    }


    public int createSupplierOrders()
    {
        return DALSupplierOrders.createSupplierOrders(this);
    }
    /*
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
    }*/


}
