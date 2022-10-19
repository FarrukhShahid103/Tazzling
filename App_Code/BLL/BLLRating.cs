using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

/// <summary>
/// Summary description for BLLRating
/// </summary>
public class BLLRating
{
    private long RateId;
    private double Rating;
    private DateTime CreatedOn;
    private long CreatedBy;
    private DateTime ModifiedOn;
    private long ModifiedBy;
    private long RestaurantId;
    private long CommentId;

    public BLLRating()
    {
        RateId = 0;
        Rating = 0.0;
        CreatedOn = DateTime.Now;
        CreatedBy = 0;
        ModifiedOn = DateTime.Now;
        ModifiedBy = 0;
        RestaurantId = 0;
        CommentId = 0;
    }

    public long restaurantId 
    {
        set { RestaurantId = value; }
        get { return RestaurantId; }
    }
    public long commentId
    {
        set { CommentId = value; }
        get { return CommentId; }
    }
    public long rateId
    {
        set { RateId = value; }
        get { return RateId; }
    }
    public double rating
    {
        set { Rating = value; }
        get { return Rating; }
    }
    public DateTime createdOn
    {
        set { CreatedOn = value; }
        get { return CreatedOn; }
    }
    public long createdBy
    {
        set { CreatedBy = value; }
        get { return CreatedBy; }
    }
    public DateTime modifiedOn
    {
        set { ModifiedOn = value; }
        get { return ModifiedOn; }
    }
    public long modifiedBy
    {
        set { ModifiedBy = value; }
        get { return ModifiedBy; }
    }

    public int modifyRestaurantRating()
    {
        return DALRating.modifyRestaurantRating(this);
    }
    public DataTable getAllRestauarntRatingsByRestaurantID()
    {
        return DALRating.getAllRestauarntRatingsByRestaurantID(this);
    }
}
