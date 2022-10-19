using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;


/// <summary>
/// Summary description for BLLComments
/// </summary>
public class BLLCategories
{
    #region Private Variables
    private long CategoryId;
    private string CategoryName;
    private string CategoryDescription;
    private long CategoryParentID;
    private bool Status;
    private DateTime CreationDate;
    private long CreatedBy;
    private DateTime Modifieddate;
    private long ModifiedBy;
    #endregion

    #region Constructor
    public BLLCategories()
    {
        CategoryId = 0;
        CategoryName = "";
        CategoryDescription = "";
        CategoryParentID = 0;
        Status = true;
        CreationDate = DateTime.Now;
        CreatedBy = 0;
        Modifieddate = DateTime.Now;
        ModifiedBy = 0;
    }
    #endregion

    #region Properties
    public long categoryId
    {
        set { CategoryId = value; }
        get { return CategoryId; }
    }

    public long categoryParentID
    {
        set { CategoryParentID = value; }
        get { return CategoryParentID; }
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

    public string categoryName 
    {
        set { CategoryName = value; }
        get { return CategoryName; }
    }

    public string categoryDescription
    {
        set { CategoryDescription = value; }
        get { return CategoryDescription; }
    }

    public bool status
    {
        set { Status = value; }
        get { return Status; }
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
    public long createCategory()
    {
        return DALCategories.createCategory(this);
    }

    public int deleteCategory()
    {
        return DALCategories.deleteCategory(this);
    }

    public int updateCategory()
    {
        return DALCategories.updateCategory(this);
    }

    public DataTable getAllActiveCategories()
    {
        return DALCategories.getAllActiveCategories();
    }
    public int GetNewDealsCount()
    {
        return DALCategories.GetNewDealsCount();
    }
    public DataTable getAllActiveCategoriesAndSubCategories()
    {
        return DALCategories.getAllActiveCategoriesAndSubCategories();
    }

    public DataTable getAllCategories()
    {
        return DALCategories.getAllCategories();
    }

    public DataTable getCategoryByID()
    {
        return DALCategories.getCategoryByID(this);
    }

    public DataTable getSubCategoryByParentID()
    {
        return DALCategories.getSubCategoryByParentID(this);
    }

    public bool changeCategoryStatus()
    {
        return DALCategories.changeCategoryStatus(this);
    }
    
    #endregion

}
