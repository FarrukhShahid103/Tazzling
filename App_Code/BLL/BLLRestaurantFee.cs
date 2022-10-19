using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
/// <summary>
/// Summary description for BLLRestaurantFee
/// </summary>
public class BLLRestaurantFee
{

    #region Private Variables
    private long RfID;
    private float RfAmount;
    private string RfDescription;
    private long RestaurantId;
    private DateTime CreationDate;
    private long CreatedBy;
    private DateTime Modifieddate;
    private long ModifiedBy;
    private bool IsFee;
    private int Year;
    private int Month;
    #endregion
    
    #region Constructor
    public BLLRestaurantFee()
    {
        //
        // TODO: Add constructor logic here
        //
        RfID = 0;
        RfAmount = 0;
        RfDescription = "";
        RestaurantId = 0;
        CreationDate = DateTime.Now;
        CreatedBy = 0;
        Modifieddate = DateTime.Now;
        ModifiedBy = 0;
        IsFee = true;
        Year = 0;
        Month = 0;
    }
    #endregion

    #region Properties

    public int year
    {
        set { Year = value; }
        get { return Year; }
    }

    public int month
    {
        set { Month = value; }
        get { return Month; }
    }

    public bool isFee
    {
        set { IsFee = value; }
        get { return IsFee; }
    }

    public long rfID
    {
        set { RfID = value; }
        get { return RfID; }
    }

    public float rfAmount
    {
        set { RfAmount = value; }
        get { return RfAmount; }
    }

    public long restaurantId
    {
        set { RestaurantId = value; }
        get { return RestaurantId; }
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

    public string rfDescription
    {
        set { RfDescription = value; }
        get { return RfDescription; }
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
    public long createRF()
    {
        return DALRestaurantFee.createRF(this);
    }

    public int deleteRF()
    {
        return DALRestaurantFee.deleteRF(this);
    }

    public int updateRF()
    {
        return DALRestaurantFee.updateRF(this);
    }

    public DataTable getAllRFForAdmin()
    {
        return DALRestaurantFee.getAllRFForAdmin(this);
    }

    public DataTable getRFByID()
    {
        return DALRestaurantFee.getRFByID(this);
    }

    public DataTable getAllRFByRestaurantID()
    {
        return DALRestaurantFee.getAllRFByRestaurantID(this);
    }

    public DataTable getTotalOfRestaurantFeeByRestaurantID()
    {
        return DALRestaurantFee.getTotalOfRestaurantFeeByRestaurantID(this);
    }

    public DataTable getTotalOfRestaurantFeeByRestaurantIDTillGivenMonth()
    {
        return DALRestaurantFee.getTotalOfRestaurantFeeByRestaurantIDTillGivenMonth(this);
    }        

    public DataTable getRestaurantFeeOfGivenMonthByRestaurantID()
    {
        return DALRestaurantFee.getRestaurantFeeOfGivenMonthByRestaurantID(this);
    }    
    
    #endregion
    
}
