using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

/// <summary>
/// Summary description for BLLCities
/// </summary>
public class BLLUpcommingDeals
{
    private long UpdealId;
    private string Title;
    private long RestaurantId;
    private long CreatedBy;
    private DateTime CreatedDate;
    private long ModifiedBy;
    private DateTime ModifiedDate;
    private string Description;
    private string PreDealVerification;
    private string PostDealVerification;
    public BLLUpcommingDeals()
    {
        UpdealId = 0;
        Title = "";
        RestaurantId = 0;
        CreatedBy = 0;
        CreatedDate = DateTime.Now;
        ModifiedBy = 0;
        ModifiedDate = DateTime.Now;
        Description = "";
        PreDealVerification = "0% Data Entered";
        PostDealVerification = "0% Deal Ended";
    }

    public long updealId 
    {
        get { return UpdealId; }
        set { UpdealId = value; }
    }

    public string preDealVerification
    {
        get { return PreDealVerification; }
        set { PreDealVerification = value; }
    }

    public string postDealVerification
    {
        get { return PostDealVerification; }
        set { PostDealVerification = value; }
    }

    public string title
    {
        get { return Title; }
        set { Title = value; }
    }
    public long restaurantId
    {
        get { return RestaurantId; }
        set { RestaurantId = value; }
    }
    public long createdBy
    {
        get { return CreatedBy; }
        set { CreatedBy = value; }
    }
    public DateTime createdDate
    {
        get { return CreatedDate; }
        set { CreatedDate = value; }
    }
    public long modifiedBy
    {
        get { return ModifiedBy; }
        set { ModifiedBy = value; }
    }
    public DateTime modifiedDate
    {
        get { return ModifiedDate; }
        set { ModifiedDate = value; }
    }
    public string description
    {
        get { return Description; }
        set { Description = value; }
    }

    public int createUpcommingDeals()
    {
        return DALUpcommingDeals.createUpcommingDeals(this);
    }

    public int updateUpcommingDeals()
    {
        return DALUpcommingDeals.updateUpcommingDeals(this);
    }

    public DataTable getupCommingDealForDealId()
    {
        return DALUpcommingDeals.getupCommingDealForDealId(this);
    }

    public int deleteUpcommingDeals()
    {
        return DALUpcommingDeals.deleteUpcommingDeals(this);
    }


    

    
 
}
