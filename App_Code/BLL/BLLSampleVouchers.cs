using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

/// <summary>
/// Summary description for BLLSampleVouchers
/// </summary>
public class BLLSampleVouchers
{
    private long VID;
    private long DealId;
    private long DetailID;
    private string DealOrderCode;
    private string VoucherSecurityCode;
    private bool IsUsed;
    public BLLSampleVouchers()
    {
        VID = 0;
        DealId = 0;
        DetailID = 0;
        DealOrderCode = "";
        VoucherSecurityCode = "";
        IsUsed = false;
    }

    public long vID 
    {
        get { return VID; }
        set { VID = value; }
    }

    public long dealId
    {
        get { return DealId; }
        set { DealId = value; }
    }

    public long detailID
    {
        get { return DetailID; }
        set { DetailID = value; }
    }

    public string dealOrderCode
    {
        get { return DealOrderCode; }
        set { DealOrderCode = value; }
    }
    public string voucherSecurityCode
    {
        get { return VoucherSecurityCode; }
        set { VoucherSecurityCode = value; }
    }
    public bool isUsed
    {
        get { return IsUsed; }
        set { IsUsed = value; }
    }


    public int createSampleVouchers()
    {
        return DALSampleVouchers.createSampleVouchers(this);
    }

    public int updateSampleVouchers()
    {
        return DALSampleVouchers.updateSampleVouchers(this);
    }

    public int deleteSampleVouchers()
    {
        return DALSampleVouchers.deleteSampleVouchers(this);
    }

    public DataTable getTop1UnusedSampleVouchersByDealID()
    {
        return DALSampleVouchers.getTop1UnusedSampleVouchersByDealID(this);
    }

    public DataTable getAllSampleVouchersByDealID()
    {
        return DALSampleVouchers.getAllSampleVouchersByDealID(this);
    }

    public DataTable getSampleVoucherByDealOrderCode()
    {
        return DALSampleVouchers.getSampleVoucherByDealOrderCode(this);
    }
}
