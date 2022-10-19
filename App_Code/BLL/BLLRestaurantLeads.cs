using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;


/// <summary>
/// Summary description for BLLRestaurantLeads
/// </summary>
public class BLLRestaurantLeads
{
    #region Private Variables
    private long RestaurantLeadID;
    private string RestaurantLeadName;
    private string RestaurantLeadOwnerName;
    private string RestaurantLeadAddress;
    private string RestaurantLeadPhoneNumber;
    private string RestaurantLeadOwnerPhoneNumber;
    private string RestaurantLeadStatus;
    private string RestaurantLeadSignUpID;
    private string RestaurantAssignto;
    private long ProvinceId;
    private long CityId;
    private DateTime CreationDate;
    private long CreatedBy;
    private DateTime Modifieddate;
    private long ModifiedBy;
    private bool Priority;
    #endregion

    #region Constructor
    public BLLRestaurantLeads()
    {
        RestaurantLeadID = 0;
        Priority = false;
        RestaurantLeadName = "";
        RestaurantLeadOwnerName = "";
        RestaurantLeadAddress = "";
        RestaurantLeadPhoneNumber = "";
        RestaurantLeadOwnerPhoneNumber = "";
        RestaurantLeadStatus = "";
        RestaurantLeadSignUpID = "";
        RestaurantAssignto = "";
        CreationDate = DateTime.Now;
        CreatedBy = 0;
        provinceId = 0;
        cityId = 0;
        Modifieddate = DateTime.Now;
        ModifiedBy = 0;
    }
    #endregion

    #region Properties
    public long restaurantLeadID
    {
        set { RestaurantLeadID = value; }
        get { return RestaurantLeadID; }
    }

    public bool priority
    {
        set { Priority = value; }
        get { return Priority; }
    }

    

    public long provinceId
    {
        set { ProvinceId = value; }
        get { return ProvinceId; }
    }

    public long cityId
    {
        set { CityId = value; }
        get { return CityId; }
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


    public string restaurantLeadAddress
    {
        set { RestaurantLeadAddress = value; }
        get { return RestaurantLeadAddress; }
    }

    public string restaurantAssignto
    {
        set { RestaurantAssignto = value; }
        get { return RestaurantAssignto; }
    }

    
    public string restaurantLeadName
    {
        set { RestaurantLeadName = value; }
        get { return RestaurantLeadName; }
    }

    public string restaurantLeadOwnerName
    {
        set { RestaurantLeadOwnerName = value; }
        get { return RestaurantLeadOwnerName; }
    }

    public string restaurantLeadPhoneNumber
    {
        set { RestaurantLeadPhoneNumber = value; }
        get { return RestaurantLeadPhoneNumber; }
    }

    public string restaurantLeadOwnerPhoneNumber
    {
        set { RestaurantLeadOwnerPhoneNumber = value; }
        get { return RestaurantLeadOwnerPhoneNumber; }
    }

    public string restaurantLeadStatus
    {
        set { RestaurantLeadStatus = value; }
        get { return RestaurantLeadStatus; }
    }

    public string restaurantLeadSignUpID
    {
        set { RestaurantLeadSignUpID = value; }
        get { return RestaurantLeadSignUpID; }
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
    public long createRestaurantLeads()
    {
        return DALRestaurantLeads.createRestaurantLeads(this);
    }

    public int deleteRestaurantLeads()
    {
        return DALRestaurantLeads.deleteRestaurantLeads(this);
    }

    public int updateRestaurantLeads()
    {
        return DALRestaurantLeads.updateRestaurantLeads(this);
    }

    public DataTable getRestaurantLeadsByID()
    {
        return DALRestaurantLeads.getRestaurantLeadsByID(this);
    }

    public DataTable getRestaurantByUserName()
    {
        return DALRestaurantLeads.getRestaurantByUserName(this);
    }

    public DataTable getAllRestaurantLeads()
    {
        return DALRestaurantLeads.getAllRestaurantLeads();
    }
    
    #endregion

}
