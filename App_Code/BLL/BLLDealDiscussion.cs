using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

/// <summary>
/// Summary description for BLLDealDiscussion
/// </summary>
public class BLLDealDiscussion
{
    #region "Private Variables"

    private long discussionId;
    private long dealId;
    private long userId;
    private string comments;
    private DateTime cmtDatetime;
    private long pDiscussionId;

    #endregion

    public BLLDealDiscussion()
    {
        //
        // TODO: Add constructor logic here
        //
        pDiscussionId = 0;
        discussionId = 0;
        dealId = 0;
        userId = 0;
        comments = "";
        cmtDatetime = DateTime.Now;
    }


    public long pdiscussionId
    {
        set { pDiscussionId = value; }
        get { return pDiscussionId; }
    }
    public long DiscussionId
    {
        set { discussionId = value; }
        get { return discussionId; }
    }
    public long DealId
    {
        set { dealId = value; }
        get { return dealId; }
    }
    public long UserId
    {
        set { userId = value; }
        get { return userId; }
    }
    public string Comments
    {
        set { comments = value; }
        get { return comments; }
    }
    public DateTime CmtDatetime
    {
        set { cmtDatetime = value; }
        get { return cmtDatetime; }
    }

    #region Function to Create New Deal Discussion

    public int AddNewDealDiscussion()
    {
        return DalDealDiscussion.AddNewDealDiscussion(this);
    }

    #endregion

    #region Function to Update Deal Discussion Info by Discussion Id

    public int updateDealDiscussionInfoByDiscussionId()
    {
        return DalDealDiscussion.updateDealDiscussionInfoByDiscussionId(this);
    }

    #endregion

    #region Functions to Get All Deal Discussions

    public DataTable getAllDealDiscussions()
    {
        return DalDealDiscussion.getAllDealDiscussions();
    }

    #endregion

    #region Functions to get Deal Discussion By Deal Id
    public DataTable getDealDiscussionByDealId()
    {
        return DalDealDiscussion.getDealDiscussionByDealId(this);
    }

    public DataTable getLatestDealDiscussionCommentByDealId()
    {
        return DalDealDiscussion.getLatestDealDiscussionCommentByDealId(this);
    }       

    public DataTable getDealForAdminDiscussionByDealId()
    {
        return DalDealDiscussion.getDealForAdminDiscussionByDealId(this);
    }

    public DataTable getDealDiscussionByParentID()
    {
        return DalDealDiscussion.getDealDiscussionByParentID(this);
    }

    public DataTable getAllDealDiscussionByParentID()
    {
        return DalDealDiscussion.getAllDealDiscussionByParentID(this);
    }

    public DataTable getDealDiscussionByDiscussionId()
    {
        return DalDealDiscussion.getDealDiscussionByDiscussionId(this);
    }

    

    #endregion

    #region Function to Delete Deal Discussion By Discussion Id

    public int deleteDealDiscussionByDiscussionId()
    {
        return DalDealDiscussion.deleteDealDiscussionByDiscussionId(this);
    }

    #endregion
}
