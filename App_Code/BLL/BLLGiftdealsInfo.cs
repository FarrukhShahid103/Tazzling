using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;


/// <summary>
/// Summary description for BLLGiftdealsInfo
/// </summary>
public class BLLGiftdealsInfo
{
    #region Private Variables
    private long DGiftId;
    private string GiftTo;
    private string GiftFrom;
    private long DOrderID;
    private string GiftSendEmail;
    private string Message;
    #endregion

    #region Constructor
    public BLLGiftdealsInfo()
    {
        DGiftId = 0;
        GiftTo = "";
        GiftFrom = "";
        DOrderID = 0;
        GiftSendEmail = "";
        Message = "";
    }
    #endregion

    #region Properties
    public long dGiftId
    {
        set { DGiftId = value; }
        get { return DGiftId; }
    }

    public long dOrderID
    {
        set { DOrderID = value; }
        get { return DOrderID; }
    }

    public string giftTo
    {
        set { GiftTo = value; }
        get { return GiftTo; }
    }

    public string giftFrom
    {
        set { GiftFrom = value; }
        get { return GiftFrom; }
    }

    public string giftSendEmail
    {
        set { GiftSendEmail = value; }
        get { return GiftSendEmail; }
    }

    public string message
    {
        set { Message = value; }
        get { return Message; }
    }  
    #endregion

    #region Functions
    public long createGiftdealsInfo()
    {
        return DALGiftdealsInfo.createGiftdealsInfo(this);
    }

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
