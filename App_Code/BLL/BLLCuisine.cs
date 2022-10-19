using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
/// <summary>
/// Summary description for BLLCuisine
/// </summary>
public class BLLCuisine
{
    #region Private Variables
    private long CuisineId;
    private string CuisineName;
    private DateTime CreationDate;
    private long CreatedBy;
    private DateTime Modifieddate;
    private long ModifiedBy;
    #endregion

    #region Constructor
    public BLLCuisine()
	{
        CuisineId = 0;
        CuisineName = "";      
        CreationDate = DateTime.Now;
        CreatedBy = 0;
        Modifieddate = DateTime.Now;
        ModifiedBy = 0;
    }
    #endregion

    #region Properties
    public long cuisineId
    {
        set { CuisineId = value; }
        get { return CuisineId; }
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

    public string cuisineName
    {
        set { CuisineName = value; }
        get { return CuisineName; }
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
    public int createCuisine()
    {
        return DALCuisine.createCuisine(this);
    }

    public int deleteCuisine()
    {
        return DALCuisine.deleteCuisine(this);
    }

    public int updateCuisine()
    {
        return DALCuisine.updateCuisine(this);
    }

    public DataTable getAllCuisine()
    {
        return DALCuisine.getAllCuisine();
    }

    public DataTable getCuisineByID()
    {
        return DALCuisine.getCuisineByID(this);
    }
    #endregion
}
