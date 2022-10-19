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


public class BLLRestaurantComents
{   private string Feedback;
    private string UserComments;
    private int BID;
    private int UserID;
    private int DetailID;
   

    public BLLRestaurantComents()
    {
       
        Feedback = "";
        UserComments = "";
        BID = 0;
        UserID = 0;
        DetailID = 0;
    
    }

	
		 public string feedback
    { 
        set { Feedback = value; }
        get { return Feedback; }
       
    }

    public string userComments
    {
        set { UserComments = value; }
        get { return UserComments; }
       
    }

    public int bID
    { 
        set { BID = value; }
        get { return BID; }
       
    }

    public int userID
    {
        set { UserID = value; }
        get { return UserID; }
        
    }

    public int detailID
    {
        set { DetailID = value; }
        get { return DetailID; }
    }






    public int restaurantComments()
    {
        return DALRestaurantComments.createRestaurantComments(this);
    }
    public DataTable GetUserValue()
    {
        return DALRestaurantComments.GetUserValue(this);
    }

    public DataTable GetRestaurantComments()
    {
        return DALRestaurantComments.GetRestaurantComments(this);
    }
}

