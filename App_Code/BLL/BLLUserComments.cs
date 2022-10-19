using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

/// <summary>
/// Summary description for BLLUpcommingDealDiscussion
/// </summary>
public class BLLUserComments
{
    #region "Private Variables"

    private long CommentId;
    private long UserId;
    private long Commentby;
    private long DOrderID;
    private string Comment;
    private DateTime CmtDatetime;
    
    #endregion

    public BLLUserComments()
    {
        //
        // TODO: Add constructor logic here
        //
        CommentId = 0;
        Commentby = 0;        
        UserId = 0;
        DOrderID = 0;
        Comment = "";
        CmtDatetime = DateTime.Now;
    }

    public long dOrderID
    {
        set { DOrderID = value; }
        get { return DOrderID; }
    }
    public long commentId
    {
        set { CommentId = value; }
        get { return CommentId; }
    }
    public long commentby
    {
        set { Commentby = value; }
        get { return Commentby; }
    }
    public long userId
    {
        set { UserId = value; }
        get { return UserId; }
    }
    public string comment
    {
        set { Comment = value; }
        get { return Comment; }
    }  
    public DateTime cmtDatetime
    {
        set { CmtDatetime = value; }
        get { return CmtDatetime; }
    }

    #region Function to Create New Deal Discussion

    public int AddNewUserComments()
    {
        return DALUserComments.AddNewUserComments(this);
    }

    #endregion

   

    #region Functions to get Deal Discussion By Deal Id
    public DataTable getUserCommentsByUserId()
    {
        return DALUserComments.getUserCommentsByUserId(this);
    }

    #endregion

    #region Function to Delete Deal Discussion By Discussion Id

    public int deleteUserCommentsByCommentID()
    {
        return DALUserComments.deleteUserCommentsByCommentID(this);
    }

    #endregion
}
