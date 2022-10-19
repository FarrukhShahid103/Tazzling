using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;


/// <summary>
/// Summary description for BLLComments
/// </summary>
public class BLLRestaurantLeadComments
{
    #region Private Variables
    private long RestaurantLeadCommentID;
    private long RestaurantLeadID;
    private string RestaurantLeadComment;    
    private DateTime CreationDate;
    private long CreatedBy;    
    #endregion

    #region Constructor
    public BLLRestaurantLeadComments()
    {
        RestaurantLeadCommentID = 0;
        RestaurantLeadComment = "";
        RestaurantLeadID = 0;
        CreationDate = DateTime.Now;
        CreatedBy = 0;
    }
    #endregion

    #region Properties
    public long restaurantLeadCommentID
    {
        set { RestaurantLeadCommentID = value; }
        get { return RestaurantLeadCommentID; }
    }

    public long restaurantLeadID
    {
        set { RestaurantLeadID = value; }
        get { return RestaurantLeadID; }
    }

    public string restaurantLeadComment
    {
        set { RestaurantLeadComment = value; }
        get { return RestaurantLeadComment; }
    }

    public long createdBy
    {
        set { CreatedBy = value; }
        get { return CreatedBy; }
    }

    public DateTime creationDate
    {
        set { CreationDate = value; }
        get { return CreationDate; }
    }
    
    #endregion

    #region Functions
    public long createRestaurantLeadComments()
    {
        return DALRestaurantLeadComments.createRestaurantLeadComments(this);
    }

    public int deleteRestaurantLeadComments()
    {
        return DALRestaurantLeadComments.deleteRestaurantLeadComments(this);
    }

    public DataTable getRestaurantLeadCommentsByLeadID()
    {
        return DALRestaurantLeadComments.getRestaurantLeadCommentsByLeadID(this);
    }
    #endregion

}
