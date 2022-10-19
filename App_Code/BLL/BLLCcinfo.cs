using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;


/// <summary>
/// Summary description for BLLComments
/// </summary>
public class BLLCcinfo
{
    #region Private Variables
    private long RcciID;
    private long UserId;
    private string Rccin;
    private string Rcci_ccvn;
    private string Rccied;
    private string Rcciun;
    private string Rccit;
    private string Package;
    private string Cycle;
    private string Fee;
    
    private DateTime CreationDate;
    private long CreatedBy;
    private DateTime Modifieddate;
    private long ModifiedBy;
    #endregion

    #region Constructor
    public BLLCcinfo()
    {
        RcciID = 0;
        UserId = 0;
        Rccin = "";
        Rcci_ccvn = "";
        Rccied = "";
        Rcciun = "";
        Rccit = "";
        Package = "";
        Cycle = "";
        Fee = "";
        CreationDate = DateTime.Now;
        CreatedBy = 0;
        Modifieddate = DateTime.Now;
        ModifiedBy = 0;
    }
    #endregion

    #region Properties

    public string package
    {
        set { Package = value; }
        get { return Package; }
    }

    public string cycle
    {
        set { Cycle = value; }
        get { return Cycle; }
    }

    public string fee
    {
        set { Fee = value; }
        get { return Fee; }
    }

    public long rcciID
    {
        set { RcciID = value; }
        get { return RcciID; }
    }

    public long userId
    {
        set { UserId = value; }
        get { return UserId; }
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

    public string rccin
    {
        set { Rccin = value; }
        get { return Rccin; }
    }

    public string rcci_ccvn
    {
        set { Rcci_ccvn = value; }
        get { return Rcci_ccvn; }
    }

    public string rccied
    {
        set { Rccied = value; }
        get { return Rccied; }
    }

    public string rcciun
    {
        set { Rcciun = value; }
        get { return Rcciun; }
    }

    public string rccit
    {
        set { Rccit = value; }
        get { return Rccit; }
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
    public long createRCcinfo()
    {
        return DALCcinfo.createRCcinfo(this);
    }

    public int deleteRCcinfo()
    {
        return DALCcinfo.deleteRCcinfo(this);
    }

    public int updateRCcInfo()
    {
        return DALCcinfo.updateRCcInfo(this);
    }

    public DataTable getRCcinfoByUserID()
    {
        return DALCcinfo.getRCcinfoByUserID(this);
    }
    #endregion

}
