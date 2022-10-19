using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;


/// <summary>
/// Summary description for BLLAdminPointsGift
/// </summary>
public class BLLAdminPointsGift
{
    #region Private Variables
    private long PgID;
    private string PgName;
    private string PgDescription;
    private long PgPoints;
    private string PgImage;
    private DateTime CreationDate;
    private long CreatedBy;
    private DateTime Modifieddate;
    private long ModifiedBy;    
    #endregion

    #region Constructor
    public BLLAdminPointsGift()
    {
        PgID = 0;
        PgName = "";
        PgDescription = "";
        PgPoints = 0;
        PgImage = "";       
        CreationDate = DateTime.Now;
        CreatedBy = 0;
        Modifieddate = DateTime.Now;
        ModifiedBy = 0;       
    }
    #endregion

    #region Properties
    public long pgID
    {
        set { PgID = value; }
        get { return PgID; }
    }

    public string pgName
    {
        set { PgName = value; }
        get { return PgName; }
    }

    public string pgDescription
    {
        set { PgDescription = value; }
        get { return PgDescription; }
    }

    public string pgImage
    {
        set { PgImage = value; }
        get { return PgImage; }
    }

    public long pgPoints
    {
        set { PgPoints = value; }
        get { return PgPoints; }
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
    public long createAdminPointsGift()
    {
        return DALAdminPointsGift.createAdminPointsGift(this);
    }

    public int deleteAdminPointsGift()
    {
        return DALAdminPointsGift.deleteAdminPointsGift(this);
    }

    public int updateAdminPointsGift()
    {
        return DALAdminPointsGift.updateAdminPointsGift(this);
    }

    public DataTable getAllAdminPointsGift()
    {
        return DALAdminPointsGift.getAllAdminPointsGift();
    }

    public DataTable getAdminPointsGiftByID()
    {
        return DALAdminPointsGift.getAdminPointsGiftByID(this);
    }    
    #endregion

}
