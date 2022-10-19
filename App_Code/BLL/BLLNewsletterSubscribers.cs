using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;


/// <summary>
/// Summary description for BLLNewsletterSubscribers
/// </summary>
public class BLLNewsletterSubscribers
{
    #region Private Variables
    private long SID;
    private string Email;
    private bool Status;
    private long ProvinceId;
    private long CityId;
    #endregion

    #region Constructor
    public BLLNewsletterSubscribers()
    {
        SID = 0;
        ProvinceId = 0;
        CityId = 0;
        Email = "";
        Status = true;       
    }
    #endregion

    #region Properties
    public long sID
    {
        set { SID = value; }
        get { return SID; }
    }

    public long cityId
    {
        set { CityId = value; }
        get { return CityId; }
    }

    public long provinceId
    {
        set { ProvinceId = value; }
        get { return ProvinceId; }
    }
    

    public string email
    {
        set { Email = value; }
        get { return Email; }
    }

    public bool status
    {
        set { Status = value; }
        get { return Status; }
    }
 
    #endregion

    #region Functions
    //public long createComments()
    //{
    //    return DALComments.createComments(this);
    //}

    //public int deleteComments()
    //{
    //    return DALComments.deleteComments(this);
    //}

    //public int updateComments()
    //{
    //    return DALComments.updateComments(this);
    //}

    //public DataTable getCommentsByRestaurantID()
    //{
    //    return DALComments.getCommentsByRestaurantID(this);
    //}
    #endregion

}
