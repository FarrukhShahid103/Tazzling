using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

/// <summary>
/// Summary description for BLLNewsLetterSendHistory
/// </summary>
public class BLLNewsLetterSendHistory
{
    private long nlhId;
    private long sId;
    private long nlId;
    private long sendBy;
    private DateTime sendDate;

    public BLLNewsLetterSendHistory()
    {
        //
        // TODO: Add constructor logic here
        //
        nlhId = 0;
        sId = 0;
        nlId = 0;
        sendBy = 0;
        sendDate = DateTime.Now;
    }

    public long NlhId
    {
        set { nlhId = value; }
        get { return nlhId; }
    }
    public long SId
    {
        set { sId = value; }
        get { return sId; }
    }
    public long NlId
    {
        set { nlId = value; }
        get { return nlId; }
    }
    public long SendBy
    {
        set { sendBy = value; }
        get { return sendBy; }
    }
    public DateTime SendDate
    {
        set { sendDate = value; }
        get { return sendDate; }
    }

    #region Functions

    public DataTable getAllNewsLetterSentHistory()
    {
        return DALNewsLetterSendHistory.getAllNewsLetterSentHistory();
    }

    public int deleteNewsLetterSentHistoryById()
    {
        return DALNewsLetterSendHistory.deleteNewsLetterSentHistoryById(this);
    }

    public int updateNewsLetterSentHistoryById()
    {
        return DALNewsLetterSendHistory.updateNewsLetterSentHistoryById(this);
    }

    public int createNewsLetterSentHistory()
    {
        return DALNewsLetterSendHistory.createNewsLetterSentHistory(this);
    }

    #endregion
}
