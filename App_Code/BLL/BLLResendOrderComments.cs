using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

/// <summary>
/// Summary description for BLLUpcommingDealDiscussion
/// </summary>
public class BLLResendOrderComments
{
    #region "Private Variables"

    private long ResendOrdercommentId;    
    private long Commentby;
    private long ResendOrderID;
    private string Comment;
    private DateTime CmtDatetime;
    
    #endregion

    public BLLResendOrderComments()
    {
        //
        // TODO: Add constructor logic here
        //
        ResendOrdercommentId = 0;
        Commentby = 0;
        ResendOrderID = 0;        
        Comment = "";
        CmtDatetime = DateTime.Now;
    }

    public long resendOrdercommentId
    {
        set { ResendOrdercommentId = value; }
        get { return ResendOrdercommentId; }
    }
    
    public long commentby
    {
        set { Commentby = value; }
        get { return Commentby; }
    }
    public long resendOrderID
    {
        set { ResendOrderID = value; }
        get { return ResendOrderID; }
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

    public int createResendOrderComments()
    {
        return DALResendOrderComments.createResendOrderComments(this);
    }

    #endregion



    #region Functions to get Deal Discussion By Deal Id
    public DataTable getResendOrderCommentsByResendOrderId()
    {
        return DALResendOrderComments.getResendOrderCommentsByResendOrderId(this);
    }

    #endregion

    #region Function to Delete Deal Discussion By Discussion Id

    public int deleteResendOrderCommentsByCommentID()
    {
        return DALResendOrderComments.deleteResendOrderCommentsByCommentID(this);
    }

    #endregion
}
