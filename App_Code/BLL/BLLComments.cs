using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;


/// <summary>
/// Summary description for BLLComments
/// </summary>
public class BLLComments
{
    #region Private Variables
    private long commentid;
    private string Title;
    private string Comment;
    private long RestaurantID;
    private DateTime CreationDate;
    private long CreatedBy;
    private DateTime Modifieddate;
    private long ModifiedBy;
    #endregion

    #region Constructor
    public BLLComments()
    {
        commentid = 0;
        Title = "";
        Comment = "";
        RestaurantID = 0;
        CreationDate = DateTime.Now;
        CreatedBy = 0;
        Modifieddate = DateTime.Now;
        ModifiedBy = 0;
    }
    #endregion

    #region Properties
    public long commentId
    {
        set { commentid = value; }
        get { return commentid; }
    }

    public long restaurantID
    {
        set { RestaurantID = value; }
        get { return RestaurantID; }
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

    public string title
    {
        set { Title = value; }
        get { return Title; }
    }

    public string comment
    {
        set { Comment = value; }
        get { return Comment; }
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
    public long createComments()
    {
        return DALComments.createComments(this);
    }

    public int deleteComments()
    {
        return DALComments.deleteComments(this);
    }

    public int updateComments()
    {
        return DALComments.updateComments(this);
    }

    public DataTable getCommentsByRestaurantID()
    {
        return DALComments.getCommentsByRestaurantID(this);
    }
    #endregion

}
