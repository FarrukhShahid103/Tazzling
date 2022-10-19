using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

/// <summary>
/// Summary description for BLLUpcommingDealDiscussion
/// </summary>
public class BLLPayOut
{
    #region "Private Variables"

    private long PoID;
    private float PoAmount;
    private DateTime PoDate;
    private long PoBy;
    private long DealId;
    private string PoDescription;
    private string PoType;
    #endregion

    public BLLPayOut()
    {
        //
        // TODO: Add constructor logic here
        //
        PoID = 0;
        PoAmount = 0;
        DealId = 0;
        PoBy = 0;
        PoDate = DateTime.Now;
        PoDescription = "";
        PoType = "";
    }

    public string poType
    {
        set { PoType = value; }
        get { return PoType; }
    }

    public string poDescription
    {
        set { PoDescription = value; }
        get { return PoDescription; }
    }

    public long poID
    {
        set { PoID = value; }
        get { return PoID; }
    }
    public long poBy
    {
        set { PoBy = value; }
        get { return PoBy; }
    }
    public long dealId
    {
        set { DealId = value; }
        get { return DealId; }
    }
    public float poAmount
    {
        set { PoAmount = value; }
        get { return PoAmount; }
    }
    public DateTime poDate
    {
        set { PoDate = value; }
        get { return PoDate; }
    }

    #region Function to Create New Deal Discussion

    public int AddNewPayOut()
    {
        return DALPayOut.AddNewPayOut(this);
    }

    #endregion


    #region Functions to get Deal Discussion By Deal Id
    public DataTable getPayOutByDealID()
    {
        return DALPayOut.getPayOutByDealID(this);
    }

    #endregion

    #region Function to Delete Deal Discussion By Discussion Id

    public int deletePayOutByID()
    {
        return DALPayOut.deletePayOutByID(this);
    }

    #endregion
}
