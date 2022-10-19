using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;


/// <summary>
/// Summary description for BLLLiveFeed
/// </summary>
public class BLLLiveFeed
{
    private long DealId;
    private long TotalComments;
    private long UserId;

   

    public BLLLiveFeed()
    {
        UserId = 0;
        DealId = 0;
        totalComments = 0;


    }

    public long dealId
    {
        set { DealId = value; }
        get { return DealId; }
    }
  
    public long totalComments
    {
        set { TotalComments = value; }
        get { return TotalComments; }
    }
    public long userId
    {
        set { UserId = value; }
        get { return UserId; }
    }
    public DataTable GetFeeds()
    {
        DALLiveFeed dalLiveFeed = new DALLiveFeed();
        return dalLiveFeed.GetFeeds();
    }

    public int UpdateTotalFvrtInFeeds()
    {
        return DALLiveFeed.UpdateTotalFvrtInFeeds(this);
    }
    public int UpdateTotalFav()
    {
        return DALLiveFeed.UpdateTotalFav(this);
    }
    public int UpdateTotalCommentsInFeeds()
    {
        return DALLiveFeed.UpdateTotalCommentsInFeeds(this);
    }
   

    public DataTable getDealDetailByDealDealID()
    {
     
        return DALLiveFeed.getDealDetailByDealDealID(this);
    }

}