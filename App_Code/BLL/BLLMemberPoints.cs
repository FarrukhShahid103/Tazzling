using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;


/// <summary>
/// Summary description for BLLComments
/// </summary>
public class BLLMemberPoints
{
    #region Private Variables
    private long PID;
    private long UserID;
    private long Points;
    private string PointsGetsFrom;
    private string Description;
    private long TID;
    private DateTime CreationDate;
    private long CreatedBy;
    private DateTime Modifieddate;
    private long ModifiedBy;
    private  bool IsDeleted;
    private long DeletedBy;
    private DateTime DeletedDate;
    #endregion

    #region Constructor
    public BLLMemberPoints()
    {
        PID = 0;
        UserID = 0;
        Points = 0;
        PointsGetsFrom = "";
        Description = "";
        TID = 0;
        CreationDate = DateTime.Now;
        CreatedBy = 0;
        Modifieddate = DateTime.Now;
        ModifiedBy = 0;
        IsDeleted = false;
        DeletedBy = 0;
        DeletedDate = DateTime.Now;
    }
    #endregion

    #region Properties
    public long pID
    {
        set { PID = value; }
        get { return PID; }
    }

    public long userID
    {
        set { UserID = value; }
        get { return UserID; }
    }

    public string pointsGetsFrom
    {
        set { PointsGetsFrom = value; }
        get { return PointsGetsFrom; }
    }

    public string description
    {
        set { Description = value; }
        get { return Description; }
    }

    public long points
    {
        set { Points = value; }
        get { return Points; }
    }

    public long tID
    {
        set { TID = value; }
        get { return TID; }
    }

    public bool isDeleted
    {
        set { IsDeleted = value; }
        get { return IsDeleted; }
    }

    public long deletedBy
    {
        set { DeletedBy = value; }
        get { return DeletedBy; }
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

    public DateTime deletedDate
    {
        set { DeletedDate = value; }
        get { return DeletedDate; }
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
    public long createMemberPoints()
    {
        return DALMemberPoints.createMemberPoints(this);
    }

    public int deleteMemberPoints()
    {
        return DALMemberPoints.deleteMemberPoints(this);
    }

    public int updateMemberPoints()
    {
        return DALMemberPoints.updateMemberPoints(this);
    }

    public DataTable getTotalPointsByUserID()
    {
        return DALMemberPoints.getTotalPointsByUserID(this);
    }

    public DataTable spGetAllMemberPointsByUserID()
    {
        return DALMemberPoints.spGetAllMemberPointsByUserID(this);
    }
    
    public DataTable getAllMemberPointsForAdmin()
    {
        return DALMemberPoints.getAllMemberPointsForAdmin(this);
    }
    #endregion

}
