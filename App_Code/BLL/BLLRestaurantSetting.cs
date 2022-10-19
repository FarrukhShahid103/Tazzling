using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;


/// <summary>
/// Summary description for BLLRestaurantSetting
/// </summary>
public class BLLRestaurantSetting
{
    #region Private Variables
    private long SettingID;
    private bool IsDelivery;
    private bool ExceededLimitsOK;
    private float FreeDeliveryDistance;
    private float ExceededLimitCharge;
    private float ExceededLimitChargeUnit;
    private float MinimumOrderAmount;
    private float DeliveryChargeUnit;
    private float DeliveryCharge;
    private string RealMenuImage;            
    private long createdby;
    private DateTime creationdate;
    private long modifiedby;
    private DateTime modifieddate;
    private long RestaurantId;
    private bool BelowLimitsOK;
    private float BelowLimitCharge;
    #endregion

    #region Constructor
    public BLLRestaurantSetting()
	{
        SettingID = 0;
        RestaurantId = 0;
        IsDelivery = false;
        ExceededLimitsOK =false;
        FreeDeliveryDistance = 0;
        ExceededLimitCharge = 0;
        ExceededLimitChargeUnit = 0;
        MinimumOrderAmount = 0;
        DeliveryChargeUnit = 0;
        DeliveryCharge = 0;
        RealMenuImage = "";
        createdby = 0;
        creationdate = DateTime.Now;
        modifiedby = 0;
        modifieddate = DateTime.Now;
        BelowLimitsOK = false;
        BelowLimitCharge = 0;
    }
    #endregion

    #region Properties
    public long settingID
    {
        set { SettingID = value; }
        get { return SettingID; }
    }

    public long restaurantId
    {
        set { RestaurantId = value; }
        get { return RestaurantId; }
    }

    public bool isDelivery
    {
        set { IsDelivery = value; }
        get { return IsDelivery; }
    }

    public bool exceededLimitsOK
    {
        set { ExceededLimitsOK = value; }
        get { return ExceededLimitsOK; }
    }

    public float freeDeliveryDistance
    {
        set { FreeDeliveryDistance = value; }
        get { return FreeDeliveryDistance; }
    }

    public float exceededLimitCharge
    {
        set { ExceededLimitCharge = value; }
        get { return ExceededLimitCharge; }
    }

    public float exceededLimitChargeUnit
    {
        set { ExceededLimitChargeUnit = value; }
        get { return ExceededLimitChargeUnit; }
    }

    public float minimumOrderAmount
    {
        set { MinimumOrderAmount = value; }
        get { return MinimumOrderAmount; }
    }

    public float deliveryChargeUnit
    {
        set { DeliveryChargeUnit = value; }
        get { return DeliveryChargeUnit; }
    }

    public float deliveryCharge
    {
        set { DeliveryCharge = value; }
        get { return DeliveryCharge; }
    }

    public string realMenuImage
    {
        set { RealMenuImage = value; }
        get { return RealMenuImage; }
    }
   
    public long createdBy
    {
        set { createdby = value; }
        get { return createdby; }
    }

    public DateTime creationDate
    {
        set { creationdate = value; }
        get { return creationdate; }
    }

    public long modifiedBy
    {
        set { modifiedby = value; }
        get { return modifiedby; }
    }

    public DateTime modifiedDate
    {
        set { modifieddate = value; }
        get { return modifieddate; }
    }

    public bool belowLimitsOK
    {
        set { BelowLimitsOK = value; }
        get { return BelowLimitsOK; }
    }
    public float belowLimitCharge
    {
        set { BelowLimitCharge = value; }
        get { return BelowLimitCharge; }
    }

    #endregion

    #region Functions
    public long createRestaurantSetting()
    {
        return DALRestaurantSetting.createRestaurantSetting(this);
    }

    public int deleteRestaurantSetting()
    {
        return DALRestaurantSetting.deleteRestaurantSetting(this);
    }

    public int updateRestaurantSetting()
    {
        return DALRestaurantSetting.updateRestaurantSetting(this);
    }

    public DataTable getRestaurantSettingByResturantOwnerID()
    {
        return DALRestaurantSetting.getRestaurantSettingByResturantOwnerID(this);
    }
    public DataTable getRestaurantSettingByResturantID()
    {
        return DALRestaurantSetting.getRestaurantSettingByResturantID(this);
    }
    #endregion
}
