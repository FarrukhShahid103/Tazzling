using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

/// <summary>
/// Summary description for BLLReferralBanner
/// </summary>
public class BLLReferralBanner
{
    #region Private Variables
    private long BannerId;
    private string BannerTitle;
    private float BannerWidth;
    private float BannerHeight;
    private string ImageFile;
    private long createdby;
    private DateTime creationdate;
    private long modifiedby;
    private DateTime modifieddate;

    #endregion

    #region Constructor
    public BLLReferralBanner()
    {
        BannerId = 0;
        BannerTitle = "";
        BannerWidth = 0;
        BannerHeight = 0;
        ImageFile = "";
        createdby = 0;
        creationdate = DateTime.Now;
        modifiedby = 0;
        modifieddate = DateTime.Now;
    }
    #endregion

    #region Properties
    public long bannerId
    {
        set { BannerId = value; }
        get { return BannerId; }
    }

    public float bannerWidth
    {
        set { BannerWidth = value; }
        get { return BannerWidth; }
    }

    public float bannerHeight
    {
        set { BannerHeight = value; }
        get { return BannerHeight; }
    }

    public string imageFile
    {
        set { ImageFile = value; }
        get { return ImageFile; }
    }

    public string bannerTitle
    {
        set { BannerTitle = value; }
        get { return BannerTitle; }
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
    public int createReferralBanner()
    {
        return DALReferralBanner.createReferralBanner(this);
    }

    public int deleteReferralBanner()
    {
        return DALReferralBanner.deleteReferralBanner(this);
    }

    public int updateReferralBanner()
    {
        return DALReferralBanner.updateReferralBanner(this);
    }

    public DataTable getAllReferralBanner()
    {
        return DALReferralBanner.getAllReferralBanner();
    }

    public DataTable getReferralBannerByID()
    {
        return DALReferralBanner.getReferralBannerByID(this);
    }
    #endregion
}
