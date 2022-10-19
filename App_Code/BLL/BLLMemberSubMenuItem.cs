using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

/// <summary>
/// Summary description for BLLMemberSubMenuItem
/// </summary>
public class BLLMemberSubMenuItem
{
    #region Private Variables
    private long MenuSubItemId;
    private string SubItemDescription;
    private string SubItemName;
    private string SubItemSubname;
    private float SubItemPrice;
    private long MenuItemId;
    private long Createdby;
    private DateTime Creationdate;
    private long Modifiedby;
    private DateTime Modifieddate;
    #endregion
	public BLLMemberSubMenuItem()
	{
        MenuSubItemId = 0;
        SubItemPrice = 0;
        SubItemName = "";
        SubItemSubname = "";
        SubItemDescription = "";
        MenuItemId = 0;
        Createdby = 0;
        Creationdate = DateTime.Now;
        Modifiedby = 0;
        Modifieddate = DateTime.Now;
	}
    #region Properties
    public string subItemSubname
    {
        set { SubItemSubname = value; }
        get { return SubItemSubname; }
    }

    public long menuSubItemId
    {
        set { MenuSubItemId = value; }
        get { return MenuSubItemId; }
    }

    public float subItemPrice
    {
        set { SubItemPrice = value; }
        get { return SubItemPrice; }
    }

    public long menuItemId
    {
        set { MenuItemId = value; }
        get { return MenuItemId; }
    }

    public string subItemName
    {
        set { SubItemName = value; }
        get { return SubItemName; }
    }

    public string subItemDescription
    {
        set { SubItemDescription = value; }
        get { return SubItemDescription; }
    }

    public long createdBy
    {
        set { Createdby = value; }
        get { return Createdby; }
    }

    public DateTime creationDate
    {
        set { Creationdate = value; }
        get { return Creationdate; }
    }

    public long modifiedBy
    {
        set { Modifiedby = value; }
        get { return Modifiedby; }
    }

    public DateTime modifiedDate
    {
        set { Modifieddate = value; }
        get { return Modifieddate; }
    }
    #endregion

    #region Functions
    public int createSubItems()
    {
        return DalMemberSubMenuItem.createSubItems(this);
    }

    public int deleteSubItems()
    {
        return DalMemberSubMenuItem.deleteSubItems(this);
    }

    public int updateSubItems()
    {
        return DalMemberSubMenuItem.updateSubItems(this);
    }

    public DataTable getSubItemsByMenuID()
    {
        return DalMemberSubMenuItem.getSubItemsByMenuID(this);
    }
    public DataTable getSubItemsBySubMenuID()
    {
        return DalMemberSubMenuItem.getSubItemsBySubMenuID(this);
    }

    public DataSet getMemberSubItemsByMenuItemIDForExport()
    {
        return DalMemberSubMenuItem.getMemberSubItemsByMenuItemIDForExport(this);
    }

    
    #endregion
}
