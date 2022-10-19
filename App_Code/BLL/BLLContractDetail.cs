using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;


/// <summary>
/// Summary description for BLLComments
/// </summary>
public class BLLContractDtail
{
    #region Private Variables
  
    private string ItemName = "";
    private string Image = "";
    private string Price = "";
    private string Weight = "";
    private string Width = "";
    private string Length = "";
    private string Haight = "";
    private long contractId;
     private long RestaurantId;
    private long userid;


    #endregion

    #region Constructor
    public BLLContractDtail()
    {
        contractId = 0;
        ItemName = "";
        Haight = "";
        Price = "";
        Image = "";
        Weight = "";
        Width = "";
        Length = "";
        RestaurantId = 0;
        userid = 0;
    }
    #endregion

    #region Properties



    public long userID
    {
        set { userid = value; }
        get { return userid; }
    }

    public long restaurantId
    {
        set { RestaurantId = value; }
        get { return RestaurantId; }
    }
    public string weight
    {
        set { Weight = value; }
        get { return Weight; }
    }
    public string haight
    {
        set { Haight = value; }
        get { return Haight; }
    }
    public string width
    {
        set { Width = value; }
        get { return Width; }
    }
    public string length
    {
        set { Length = value; }
        get { return Length; }
    }
    public string itemName
    {
        set { ItemName = value; }
        get { return ItemName; }
    }
    public string price
    {
        set { Price = value; }
        get { return Price; }
    }
    public string image
    {
        set { Image = value; }
        get { return Image; }
    }
    public long contractid
    {
        set { contractId = value; }
        get { return contractId; }
    }
    #endregion

    #region Functions
 

    public int CreateContractDetail()
    {
        return DALContractDetail.CreateContractDetail(this);
    }


    public DataSet GetContractDetail(int intStartIndex, int intMaxRecords)
    {
        return DALContractDetail.GetContractDetail( intStartIndex, intMaxRecords , this);
    }

   
    public DataTable GetCintractDetailByResId()
    {
        return DALContractDetail.GetCintractDetailByResId(this);
    }

    public int UpdateContractDetail()
    {
        return DALContractDetail.UpdateContractDetail(this);
    }
  
    
    #endregion

}
