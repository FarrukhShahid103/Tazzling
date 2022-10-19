using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

/// <summary>
/// Summary description for BLLCities
/// </summary>
///
public class BLLProductFavorit
{

    #region Global Veriable
    private long ProductsFavoritesID;
    private long ProductID;
    private long UserId;
    private DateTime FavoritDate;
    #endregion

    #region Constructor
    public BLLProductFavorit()
    {
        ProductsFavoritesID = 0;
        ProductID = 0;
        UserId = 0;
        FavoritDate = DateTime.Now;
    }
    #endregion

    #region getter Setter

    public long userId
    {
        get { return UserId; }
        set { UserId = value; }
    }

    public long productID
    {
        get { return ProductID; }
        set { ProductID = value; }
    }

    public long productsFavoritesID
    {
        get { return ProductsFavoritesID; }
        set { ProductsFavoritesID = value; }
    }

    public DateTime favoritDate
    {
        get { return FavoritDate; }
        set { FavoritDate = value; }
    }

   
    #endregion

    #region Functions

     public int createProductFavorit()
    {
        return DALProductFavorit.createProductFavorit(this);
    }
    /*
    public int updateCampaign()
    {
        return DALCampaign.updateCampaign(this);
    }

    public DataTable getCampaignByBusinessID()
    {
        return DALCampaign.getCampaignByBusinessID(this);
    }

    public DataTable getCampaignByCampaignId()
    {
        return DALCampaign.getCampaignByCampaignId(this);
    }

    public DataTable getCurrentCampaignByGivenDate()
    {
        return DALCampaign.getCurrentCampaignByGivenDate(this);
    }

    public DataTable GetCurrentDealByDate()
    {
        return DALCampaign.GetCurrentDealByDate(this);
    }


    public int AddToMyFavorites()
    {
        return DALCampaign.AddToMyFavorites(this);
    }

*/

    #endregion

}
