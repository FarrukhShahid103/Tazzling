using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

/// <summary>
/// Summary description for BALDealCatagories
/// </summary>
public class BLLDealCatagories
{
    #region Local Veriables
    private Int64 UserID;
    private Int64 UserFavoriteDealID;
    private Int64 DealCategoryID;
    private Int64 DealSubCategoryID;
    #endregion

    #region Constructer

    public BLLDealCatagories()
	{
        UserID = 0;
        UserFavoriteDealID = 0;
        DealCategoryID = 0;
        DealSubCategoryID = 0;
	}
    #endregion


    public Int64 Userid
    {
        set { UserID = value; }
        get { return UserID; }
    }

    public Int64 DealSubCategoryid
    {
        set { DealSubCategoryID = value; }
        get { return DealSubCategoryID; }
    }

   

    public Int64 DealCategoryid
    {
        set { DealCategoryID = value; }
        get { return DealCategoryID; }
    }

    public Int64 UserFavoritedealid
    {
        set { UserFavoriteDealID = value; }
        get { return UserFavoriteDealID; }
    }



    public DataTable GetAllDealCatagories()
    {
        return DALDealCatagories.GetAllDealCategories();
    }

    public DataTable GetUserFavoriteDealCategoriesByUserID()
    {
        return DALDealCatagories.GetUserFavoriteDealCategoriesByUserID(this);
    }


    public int DeleteUserFavoriteDeal()
    {
        return DALDealCatagories.DeleteUserFavoriteDeal(this);
    }


    public int AddUserFavoriteDeal()
    {
        return DALDealCatagories.AddUserFavoriteDeal(this);
    }

    public DataTable GetAllDealCategoriesbyDealCategoryID()
    {
        return DALDealCatagories.GetAllDealCategoriesbyDealCategoryID(this);
    }
}