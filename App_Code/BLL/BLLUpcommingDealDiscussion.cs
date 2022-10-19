using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

/// <summary>
/// Summary description for BLLUpcommingDealDiscussion
/// </summary>
public class BLLUpcommingDealDiscussion
{
    #region "Private Variables"

    private long discussionId;
    private long updealId;
    private long userId;
    private string comments;
    private DateTime cmtDatetime;
    private long pDiscussionId;

    #endregion

    public BLLUpcommingDealDiscussion()
    {
        //
        // TODO: Add constructor logic here
        //
        pDiscussionId = 0;
        discussionId = 0;
        updealId = 0;
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
    public long UpdealId
    {
        set { updealId = value; }
        get { return updealId; }
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

    public int AddNewUpcommingDealDiscussion()
    {
        return DALUpcommingDealDiscussion.AddNewUpcommingDealDiscussion(this);
    }

    #endregion

    #region Function to Update Deal Discussion Info by Discussion Id

    public int updateUpcommingDealDiscussionInfoByDiscussionId()
    {
        return DALUpcommingDealDiscussion.updateUpcommingDealDiscussionInfoByDiscussionId(this);
    }

    #endregion

    #region Functions to Get All Deal Discussions

    public DataTable getAllUpcommingDealDiscussions()
    {
        return DALUpcommingDealDiscussion.getAllUpcommingDealDiscussions();
    }

    #endregion

    #region Functions to get Deal Discussion By Deal Id
    public DataTable getUpcommingDealDiscussionByUpdealId()
    {
        return DALUpcommingDealDiscussion.getUpcommingDealDiscussionByUpdealId(this);
    }

    #endregion

    #region Function to Delete Deal Discussion By Discussion Id

    public int deleteUpcommingDealDiscussionByDiscussionId()
    {
        return DALUpcommingDealDiscussion.deleteUpcommingDealDiscussionByDiscussionId(this);
    }

    #endregion
}
