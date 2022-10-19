using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

/// <summary>
/// Summary description for BLLCities
/// </summary>
public class BLLProducts
{

    private long ProductID;
    private long CampaignID;
    private string Title;
    private string SubTitle;
    private string Description;
    private string ShortDescription;
    private float SellingPrice;
    private float ValuePrice;
    private DateTime ProductStartTime;
    private DateTime ProductEndTime;
    private string Images;
    private int MinQty;
    private int MaxQty;
    private bool IsActive;
    private bool IsVoucherProduct;
    private int MinOrdersPerUser;
    private int MaxOrdersPerUser;
    private int ProductPosition;
    private int ProductPositionForCategory;
    private DateTime VoucherExpiryDate;
    private float ourCommission;
    private string ReturnPolicy;
    private string ShippingInfo;
    private string Height;
    private string Width;
    private string Weight;
    private string Dimension;  
    private bool EnableSize;
    private bool Tracking;
    private DateTime CreationDate;
    private long CreatedBy;
    private DateTime ModifiedDate;
    private long ModifiedBy;

    public BLLProducts()
    {
        ProductID = 0;
        CampaignID = 0;
        Title = "";
        SubTitle = "";
        Description = "";
        ShortDescription = "";
        SellingPrice = 0;
        ValuePrice = 0;
        ProductStartTime = DateTime.Now;
        ProductEndTime = DateTime.Now;
        Images = "";
        MinQty = 0;
        MaxQty = 0;
        IsVoucherProduct = false;
        IsActive = false;
        MinOrdersPerUser = 0;
        MaxOrdersPerUser = 0;
        ProductPosition = 0;        
        ProductPositionForCategory = 0;
        VoucherExpiryDate = DateTime.Now;
        ourCommission = 0;
        ReturnPolicy = "";
        ShippingInfo = "";
        Height = "";
        Width = "";
        Weight = "";
        Dimension = "";
        
        CreationDate = DateTime.Now;
        CreatedBy = 0;
        ModifiedDate = DateTime.Now;
        ModifiedBy = 0;
        EnableSize = false;
        Tracking = false;
    }

    public bool tracking
    {
        get { return Tracking; }
        set { Tracking = value; }
    }

    public bool enableSize
    {
        get { return EnableSize; }
        set { EnableSize = value; }
    }
    
    public DateTime voucherExpiryDate
    {
        get { return VoucherExpiryDate; }
        set { VoucherExpiryDate = value; }
    }

    public DateTime productStartTime
    {
        get { return ProductStartTime; }
        set { ProductStartTime = value; }
    }

    public DateTime productEndTime
    {
        get { return ProductEndTime; }
        set { ProductEndTime = value; }
    }


    public bool isVoucherProduct
    {
        get { return IsVoucherProduct; }
        set { IsVoucherProduct = value; }
    }

    public bool isActive
    {
        get { return IsActive; }
        set { IsActive = value; }
    }

    public string height
    {
        get { return Height; }
        set { Height = value; }
    }

    public string width
    {
        get { return Width; }
        set { Width = value; }
    }

    public string dimension
    {
        get { return Dimension; }
        set { Dimension = value; }
    }
      
    public string title
    {
        get { return Title; }
        set { Title = value; }
    }

    public string subTitle
    {
        get { return SubTitle; }
        set { SubTitle = value; }
    }

    public string description
    {
        get { return Description; }
        set { Description = value; }
    }

    public string shortDescription
    {
        get { return ShortDescription; }
        set { ShortDescription = value; }
    }

    public string images
    {
        get { return Images; }
        set { Images = value; }
    }

    public string returnPolicy
    {
        get { return ReturnPolicy; }
        set { ReturnPolicy = value; }
    }

    public string shippingInfo
    {
        get { return ShippingInfo; }
        set { ShippingInfo = value; }
    }

    public string weight
    {
        get { return Weight; }
        set { Weight = value; }
    }


    public float OurCommission
    {
        get { return ourCommission; }
        set { ourCommission = value; }
    }

    public int productPosition
    {
        get { return ProductPosition; }
        set { ProductPosition = value; }
    }

    public int productPositionForCategory
    {
        get { return ProductPositionForCategory; }
        set { ProductPositionForCategory = value; }
    }

    public int maxOrdersPerUser
    {
        get { return MaxOrdersPerUser; }
        set { MaxOrdersPerUser = value; }
    }

    public int minOrdersPerUser
    {
        get { return MinOrdersPerUser; }
        set { MinOrdersPerUser = value; }
    }

    public long productID
    {
        get { return ProductID; }
        set { ProductID = value; }
    }

    public long campaignID
    {
        get { return CampaignID; }
        set { CampaignID = value; }
    }

    public float sellingPrice
    {
        get { return SellingPrice; }
        set { SellingPrice = value; }
    }

    public float valuePrice
    {
        get { return ValuePrice; }
        set { ValuePrice = value; }
    }

    public int minQty
    {
        get { return MinQty; }
        set { MinQty = value; }
    }

    public int maxQty
    {
        get { return MaxQty; }
        set { MaxQty = value; }
    }

    public DateTime createdDate
    {
        get { return CreationDate; }
        set { CreationDate = value; }
    }

    public DateTime modifiedDate
    {
        get { return ModifiedDate; }
        set { ModifiedDate = value; }
    }

    public long createdBy
    {
        get { return CreatedBy; }
        set { CreatedBy = value; }
    }

    public long modifiedBy
    {
        get { return ModifiedBy; }
        set { ModifiedBy = value; }
    }

    #region Functions
    public long createProduct()
    {
        return  DALProducts.createProduct(this);
    }

    public long updateProduct()
    {
        return DALProducts.updateProduct(this);
    }

    public DataTable getProductsByCampaignID()
    {
        return DALProducts.getProductsByCampaignID(this);
    }

    public DataTable getProductsByProductID()
    {
        return DALProducts.getProductsByProductID(this);
    }

    public DataTable getProductsByCampaignIDForClientSide()
    {
        return DALProducts.getProductsByCampaignIDForClientSide(this);
    }

    public DataSet getProductsByCategoryAndDateTimeForClient()
    {
        return DALProducts.getProductsByCategoryAndDateTimeForClient(this);
    }      

    public DataTable getProductsByProductIDForClient()
    {
        return DALProducts.getProductsByProductIDForClient(this);
    }

    public int updateProductSlots(string strProduct)
    {
        return DALProducts.updateProductSlots(strProduct);
    }

    public int updateProductCategorySlots(string strProduct)
    {
        return DALProducts.updateProductCategorySlots(strProduct);
    }
    

    #endregion

}
