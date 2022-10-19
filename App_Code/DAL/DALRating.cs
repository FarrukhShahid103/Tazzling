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
using System.Data.SqlClient;
using SQLHelper;

/// <summary>
/// Summary description for DALRating
/// </summary>
public class DALRating
{
	public DALRating()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public static int modifyRestaurantRating(BLLRating obj)
    {
        int result = 0;
        try
        {
            SqlParameter[] param = new SqlParameter[5];
            param[0] = new SqlParameter("@rating", obj.rating);
            param[1] = new SqlParameter("@restaurantId", obj.restaurantId);
            param[2] = new SqlParameter("@modifiedBy", obj.modifiedBy);
            param[3] = new SqlParameter("@modifiedOn", obj.modifiedOn);
            param[4] = new SqlParameter("@commentId", obj.commentId);

            result = SqlHelper.ExecuteNonQuery(Misc.connStr, CommandType.StoredProcedure, "spModifyRestaurantRating", param);
        }
        catch (Exception ex)
        {
            
        }
        return result;
    }

    public static DataTable getAllRestauarntRatingsByRestaurantID(BLLRating obj)
    {
        DataTable dt = null;
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@restaurantId", obj.restaurantId);
            dt = SqlHelper.ExecuteDataset(Misc.connStr, CommandType.StoredProcedure, "spGetAllRestauarntRatingsByRestaurantID", param).Tables[0];
        }
        catch (Exception ex)
        {
            
        }
        return dt;
    }
}
