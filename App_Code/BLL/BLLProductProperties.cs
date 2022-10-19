using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

/// <summary>
/// Summary description for BLLRestaurantAddresses
/// </summary>
public class BLLProductProperties
{

    private long ProductPropertiesID;
    private long ProductID;
    private string PropertiesLabel;
    private string PropertiesDescription;
    
    public BLLProductProperties()
    {
        ProductPropertiesID = 0;
        ProductID = 0;
        PropertiesLabel = "";
        PropertiesDescription = "";
    }


    public string propertiesDescription
    {
        get { return PropertiesDescription; }
        set { PropertiesDescription = value; }
    }

    public string propertiesLabel
    {
        get { return PropertiesLabel; }
        set { PropertiesLabel = value; }
    }

    public long productPropertiesID
    {
        get { return ProductPropertiesID; }
        set { ProductPropertiesID = value; }
    }

    public long productID
    {
        get { return ProductID; }
        set { ProductID = value; }
    }

    
    #region Functions
    public long createProductProperties()
    {
        return DALProductProperties.createProductProperties(this);
    }

    public long updateProductProperties()
    {
        return DALProductProperties.updateProductProperties(this);
    }

    public DataTable getProductPropertiesByProductID()
    {
        return DALProductProperties.getProductPropertiesByProductID(this);
    }

    public DataTable getProductPropertiesByPropertiesID()
    {
        return DALProductProperties.getProductPropertiesByPropertiesID(this);
    }

    public int deleteProductProperties()
    {
        return DALProductProperties.deleteProductProperties(this);
    }
    
    #endregion

}