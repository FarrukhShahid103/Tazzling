using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

/// <summary>
/// Summary description for BLLNewsLetterSubscriber
/// </summary>
public class BLLNewsLetterSubscriber
{
    #region Private Variables

    private long sId;
    private string email;
    private bool status;
    private long cityId;
    private DateTime createdDate;
    private DateTime modifiedDate;
    private string FriendsReferralId;
    private long UserId;
    #endregion

    public BLLNewsLetterSubscriber()
    {
        //
        // TODO: Add constructor logic here
        //
        sId = 0;
        email = "";
        status = false;
        cityId = 0;
        CreatedDate = DateTime.Now;
        ModifiedDate = DateTime.Now;
        FriendsReferralId = "";
        UserId = 0;
    }

    public long SId
    {
        set { sId = value; }
        get { return sId; }
    }
    public long userId
    {
        set { UserId = value; }
        get { return UserId; }
    }
    public string Email
    {
        set { email = value; }
        get { return email; }
    }
    public string friendsReferralId
    {
        set { FriendsReferralId = value; }
        get { return FriendsReferralId; }
    }
    
    public bool Status
    {
        set { status = value; }
        get { return status; }
    }
    public long CityId
    {
        set { cityId = value; }
        get { return cityId; }
    }
    public DateTime CreatedDate
    {
        set { createdDate = value; }
        get { return createdDate; }
    }
    public DateTime ModifiedDate
    {
        set { modifiedDate = value; }
        get { return modifiedDate; }
    }

    #region Functions

    public DataTable getAllSubscribesCityByUserID()
    {
        return DALNewsLetterSubscriber.getAllSubscribesCityByUserID(this);
    }

    public DataTable getAllNewsLetterSubscribers()
    {
        return DALNewsLetterSubscriber.getAllNewsLetterSubscribers();
    }

    public DataTable getNewsLetterSubscriberById()
    {
        return DALNewsLetterSubscriber.getNewsLetterSubscriberById(this);
    }

    public bool changeSubscriberStatus()
    {
        return DALNewsLetterSubscriber.changeSubscriberStatus(this);
    }
    
    public DataTable getNewsLetterSubscriberByCityId()
    {
        return DALNewsLetterSubscriber.getNewsLetterSubscriberByCityId(this);
    }

    public DataTable getNewsLetterSubscriberByEmailCityId()
    {
        return DALNewsLetterSubscriber.getNewsLetterSubscriberByEmailCityId(this);
    }

    public DataTable getNewsLetterSubscriberByEmailCityId2()
    {
        return DALNewsLetterSubscriber.getNewsLetterSubscriberByEmailCityId2(this);
    }


    public int unsubscribeAllCityByEmail()
    {
        return DALNewsLetterSubscriber.unsubscribeAllCityByEmail(this);
    }

    public int deleteNewsLetterSubscriberById()
    {
        return DALNewsLetterSubscriber.deleteNewsLetterSubscriberById(this);
    }

    public int updateNewsLetterSubscriberById()
    {
        return DALNewsLetterSubscriber.updateNewsLetterSubscriberById(this);
    }

    public int createNewsLetterSubscriber()
    {
        return DALNewsLetterSubscriber.createNewsLetterSubscriber(this);
    }

    #endregion
}
