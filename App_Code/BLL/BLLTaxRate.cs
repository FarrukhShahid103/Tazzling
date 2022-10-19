using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

/// <summary>
/// Summary description for BLLTaxRate
/// </summary>
public class BLLTaxRate
{
    #region Private Variables
    private long TaxID;
    private long ProvinceId;
    private string ProvinceName;
    private float TaxRates;
    private DateTime CreationDate;
    private long CreatedBy;
    private DateTime Modifieddate;
    private long ModifiedBy;
    #endregion

    #region Constructor
    public BLLTaxRate()
	{
        TaxID = 0;
        ProvinceId = 0;
        TaxRates = 0;
        CreationDate = DateTime.Now;
        CreatedBy = 0;
        Modifieddate = DateTime.Now;
        ModifiedBy = 0;
        ProvinceName = "";
    }
    #endregion

    #region Properties
    public long taxID
    {
        set { TaxID = value; }
        get { return TaxID; }
    }

    public long provinceId
    {
        set { ProvinceId = value; }
        get { return ProvinceId; }
    }

    public float taxRates
    {
        set { TaxRates = value; }
        get { return TaxRates; }
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
    public string provinceName
    {
        set { ProvinceName = value; }
        get { return ProvinceName; }
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
    public int createTaxRate()
    {
        return DALTaxRate.createTaxRate(this);
    }

    public int updateTaxRate()
    {
        return DALTaxRate.updateTaxRate(this);
    }

    public DataTable getProvinceTaxRateByProvinceID()
    {
        return DALTaxRate.getProvinceTaxRateByProvinceID(this);
    }

    public DataTable getAllProvinceTaxRate()
    {
        return DALTaxRate.getAllProvinceTaxRate();
    }

    #endregion
}
