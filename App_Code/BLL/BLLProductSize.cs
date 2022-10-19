using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

/// <summary>
/// Summary description for BLLRestaurantAddresses
/// </summary>
public class BLLProductSize
{
    private long SizeID;
    private long ProductID;
    private string SizeText;
    private int Quantity;
    public BLLProductSize()
    {
        Quantity = 0;
        SizeID = 0;
        ProductID = 0;
        SizeText = "";
    }


    public string sizeText
    {
        get { return SizeText; }
        set { SizeText = value; }
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

    public int quantity
    {
        get { return Quantity; }
        set { Quantity = value; }
    }

    

    #region Functions
    public long createProductSize()
    {
        return DALProductSize.createProductSize(this);
    }

    public long updateProductSize()
    {
        return DALProductSize.updateProductSize(this);
    }

    public DataTable getProductSizeByProductID()
    {
        return DALProductSize.getProductSizeByProductID(this);
    }

    public DataTable getProductSizeBySizeID()
    {
        return DALProductSize.getProductSizeBySizeID(this);
    }

    public int deleteProductSize()
    {
        return DALProductSize.deleteProductSize(this);
    }

    #endregion

}