using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

/// <summary>
/// Summary description for BLLMemberMenuItems
/// </summary>
public class BLLMemberDealItems
{
    #region Private Variables
    private long DealItemId;
    private string DealItemName;
    private string DealItemSubname;
    private string DealItemDescription;    
    private long DealId;
    private long createdby;
    private DateTime creationdate;
    private long modifiedby;
    private DateTime modifieddate;
    #endregion

    #region Constructor
    public BLLMemberDealItems()
	{
        DealItemId = 0;       
        DealItemName = "";
        DealItemSubname = "";
        DealItemDescription = "";
        DealId = 0;
        createdby = 0;
        creationdate = DateTime.Now;
        modifiedby = 0;
        modifieddate = DateTime.Now;
    }
    #endregion

    #region Properties
    public long dealItemId
    {
        set { DealItemId = value; }
        get { return DealItemId; }
    }
   
    public long dealId
    {
        set { DealId = value; }
        get { return DealId; }
    }

    public string dealItemName
    {
        set { DealItemName = value; }
        get { return DealItemName; }
    }

    public string dealItemSubname
    {
        set { DealItemSubname = value; }
        get { return DealItemSubname; }
    }

    public string dealItemDescription
    {
        set { DealItemDescription = value; }
        get { return DealItemDescription; }
    }


    public long createdBy
    {
        set { createdby = value; }
        get { return createdby; }
    }

    public DateTime creationDate
    {
        set { creationdate = value; }
        get { return creationdate; }
    }

    public long modifiedBy
    {
        set { modifiedby = value; }
        get { return modifiedby; }
    }

    public DateTime modifiedDate
    {
        set { modifieddate = value; }
        get { return modifieddate; }
    }
    #endregion

    #region Functions
    public int createMemberDealItems()
    {
        return DALMemberDealItems.createMemberDealItems(this);
    }

    public int deleteMemberDealItems()
    {
        return DALMemberDealItems.deleteMemberDealItems(this);
    }

    public int updateMemberDealItems()
    {
        return DALMemberDealItems.updateMemberDealItems(this);
    }

    public DataTable getMemberDealItemsByDealID()
    {
        return DALMemberDealItems.getMemberDealItemsByDealID(this);
    }

    public DataSet getMemberDealItemsByDealIDForExport()
    {
        return DALMemberDealItems.getMemberDealItemsByDealIDForExport(this);
    }
    
    #endregion
}
