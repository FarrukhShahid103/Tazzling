using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;


/// <summary>
/// Summary description for BLLAdminPointsGift
/// </summary>
public class BLLMemberPointGiftsRequests
{
    #region Private Variables
    private long MrpgID;
    private long PgID;
    private string MrpgName;
    private string MrpgDescription;
    private long MrpgPoints;
    private string MrpgImage;
    private string Status;
    private DateTime CreationDate;
    private long CreatedBy;
    private DateTime Modifieddate;
    private long ModifiedBy;    
    #endregion

    #region Constructor
    public BLLMemberPointGiftsRequests()
    {
        MrpgID = 0;
        PgID = 0;
        MrpgName = "";
        MrpgDescription = "";
        MrpgPoints = 0;
        MrpgImage = "";
        Status = "";
        CreationDate = DateTime.Now;
        CreatedBy = 0;
        Modifieddate = DateTime.Now;
        ModifiedBy = 0;       
    }
    #endregion

    #region Properties

    public long mrpgID
    {
        set { MrpgID = value; }
        get { return MrpgID; }
    }
    
    public long pgID
    {
        set { PgID = value; }
        get { return PgID; }
    }


    public string status
    {
        set { Status = value; }
        get { return Status; }
    }
    

    public string mrpgName
    {
        set { MrpgName = value; }
        get { return MrpgName; }
    }

    public string mrpgDescription
    {
        set { MrpgDescription = value; }
        get { return MrpgDescription; }
    }

    public string mrpgImage
    {
        set { MrpgImage = value; }
        get { return MrpgImage; }
    }

    public long mrpgPoints
    {
        set { MrpgPoints = value; }
        get { return MrpgPoints; }
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
    public long createMemberPointGiftsRequests()
    {
        return DALMemberPointGiftsRequests.createMemberPointGiftsRequests(this);
    }

    public int deleteMemberPointGiftsRequests()
    {
        return DALMemberPointGiftsRequests.deleteMemberPointGiftsRequests(this);
    }

    public int updateMemberPointGiftsRequests()
    {
        return DALMemberPointGiftsRequests.updateMemberPointGiftsRequests(this);
    }

    public DataTable getAllMemberPointGiftsRequests()
    {
        return DALMemberPointGiftsRequests.getAllMemberPointGiftsRequests();
    }
    
    public DataTable getAllMemberPointGiftsRequestsByUserID()
    {
        return DALMemberPointGiftsRequests.getAllMemberPointGiftsRequestsByUserID(this);
    }

    public DataTable getMemberPointGiftsRequestsByID()
    {
        return DALMemberPointGiftsRequests.getMemberPointGiftsRequestsByID(this);
    }

    public DataTable getTotalUsedPointsByUserID()
    {
        return DALMemberPointGiftsRequests.getTotalUsedPointsByUserID(this);
    }    
    
    #endregion

}
