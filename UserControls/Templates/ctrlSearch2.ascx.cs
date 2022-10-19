using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using GecLibrary;

public partial class Takeout_UserControls_Templates_ctrlSearch2 : System.Web.UI.UserControl
{
    BLLCuisine objCuisine = new BLLCuisine();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
               
            }
            catch (Exception ex)
            {
                
            }
        }
    }

    protected void btnSearchRestaurant_Click(object sender, ImageClickEventArgs e)
    {
        try
        {            

            //string strResName = txtSearchRes.Text.Trim();
            string strPostalCode = txtSearchPostalCode.Text.Trim();
            //string strAddress = txtSearchAddress.Text.Trim();
            //string strState = "";
            //string strCity = "";//txtCity.Text.Trim();
            //if (ddlProState.SelectedIndex != 0)
            //{
            //    strState = ddlProState.SelectedValue.ToString();
            //}
            //if (hfCity.Value.ToString() != "" && hfCity.Value.ToString() != "0")
            //{
            //    strCity = hfCity.Value.ToString();
            //}

            string typeOfCuisine = "";
            DataTable dtUserSearchZipCode = new DataTable();
            dtUserSearchZipCode.Columns.Add("ZipCode");
            dtUserSearchZipCode.Columns.Add("Distance", typeof(double));

            if (txtSearchPostalCode.Text.Trim() != "")
            {
                DataTable dtUserZip = Misc.getZipCodeByName(txtSearchPostalCode.Text.Trim());
                if (dtUserZip != null && dtUserZip.Rows.Count > 0)
                {
                    DataTable dtZipCodes = Misc.getAllZipCodes();
                    if (dtZipCodes != null && dtZipCodes.Rows.Count > 0)
                    {
                        for (int a = 0; a < dtZipCodes.Rows.Count; a++)
                        {
                            if (dtUserZip.Rows[0]["zip"].ToString() != dtZipCodes.Rows[a]["zip"].ToString())
                            {
                                double distance = Misc.CalculateDistance(Convert.ToDouble(dtUserZip.Rows[0]["latitude"].ToString()), Convert.ToDouble(dtUserZip.Rows[0]["longitude"].ToString()), Convert.ToDouble(dtZipCodes.Rows[a]["latitude"].ToString()), Convert.ToDouble(dtZipCodes.Rows[a]["longitude"].ToString())) * 1.609344;
                                if (distance < Convert.ToDouble(ConfigurationManager.AppSettings["SearchDistance"].ToString()))
                                {
                                    DataRow dr = dtUserSearchZipCode.NewRow();
                                    dr["ZipCode"] = dtZipCodes.Rows[a]["zip"].ToString();
                                    dr["Distance"] = distance.ToString();
                                    dtUserSearchZipCode.Rows.Add(dr);
                                }
                            }
                        }
                    }
                }
            }

            //int count = 0;
            //for (int i = 0; i < chkCuisines.Items.Count; i++)
            //{
            //    if (chkCuisines.Items[i].Selected)
            //    {
            //        if (count == 0)
            //        {
            //            typeOfCuisine += " and cuisineName = '" + chkCuisines.Items[i].Text.ToString() + "' ";
            //            count++;
            //        }
            //        else
            //        {
            //            typeOfCuisine += " or cuisineName = '" + chkCuisines.Items[i].Text.ToString() + "' ";
            //            count++;
            //        }
            //    }
            //}
            //string strDelivery = rblDelivery.SelectedValue.ToString();
            //string strDistance = txtDistance.Text.Trim();
            //string strMilesKM = ddlDistance.SelectedValue;

            string strQryHeader = "";
            strQryHeader += "Select ";
            strQryHeader += "restaurant.restaurantId ";
            strQryHeader += ",restaurant.userId ";
            strQryHeader += ",restaurantName ";
            strQryHeader += ",userName ";
            strQryHeader += ",userInfo.provinceId ";
            strQryHeader += ",restaurantAddress ";
            strQryHeader += ",restaurantPicture ";
            strQryHeader += ",city ";
            strQryHeader += ",zipCode ";
            strQryHeader += ",phone ";
            strQryHeader += ",isFaxPhone ";
            strQryHeader += ",isReservatoin ";             
            strQryHeader += ",fax ";
            strQryHeader += ",restaurant.cuisineId ";
            strQryHeader += ",cuisine.cuisineName ";
            strQryHeader += ",detail ";
            strQryHeader += ",restaurant.creationDate ";
            strQryHeader += ",restaurant.createdBy ";
            strQryHeader += ",restaurant.modifiedDate ";
            strQryHeader += ",restaurant.modifiedBy ";
            strQryHeader += ",province.provinceName ";
            strQryHeader += ",restaurantSetting.isDelivery ";
            strQryHeader += ",restaurantSetting.freeDeliveryDistance ";
            strQryHeader += ",restaurantSetting.realMenuImage ";
            strQryHeader += ",(select ceiling(avg(rating)) from rates where restaurantId=restaurant.restaurantId) as rating ";
            strQryHeader += "From restaurant ";
            strQryHeader += "INNER JOIN userInfo on userInfo.userId= restaurant.userId ";
            strQryHeader += "INNER JOIN province on province.provinceId= userInfo.provinceId ";
            strQryHeader += "INNER JOIN cuisine on cuisine.cuisineId= restaurant.cuisineId ";
            strQryHeader += "INNER JOIN restaurantSetting on restaurantSetting.restaurantId= restaurant.restaurantId ";
            strQryHeader += "where ";
            strQryHeader += "userInfo.isActive=1 and userInfo.isDeleted=0 and isDemo = 0 ";

            string strQryCondition = "";
            string strFirstChar = "";
            //if (strResName != "")
            //{
            //    strQryCondition += "and restaurantName like '%" + strResName + "%' ";
            //}
            if (strPostalCode != "")
            {
                strQryCondition += "and zipCode like '" + strPostalCode + "%' ";
                if (dtUserSearchZipCode.Rows.Count > 0)
                {
                    //DataView dv = new DataView(dtUserSearchZipCode);
                    //dv.Sort = "Distance ASC";
                    DataTable dt = Misc.SortDataTable(dtUserSearchZipCode, "Distance ASC");
                    Session["dtZipCode"] = dt;
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (i == 0)
                        {
                            strFirstChar += " and ( zipCode like '" + dt.Rows[i]["ZipCode"].ToString() + "%' ";
                        }
                        else
                        {
                            strFirstChar += " or zipCode like '" + dt.Rows[i]["ZipCode"].ToString() + "%' ";
                        }
                    }
                    if (dtUserSearchZipCode != null && dtUserSearchZipCode.Rows.Count > 0)
                    {
                        strFirstChar += " ) ";
                    }
                }
                //                strFirstChar = strPostalCode.Substring(0, 1).ToLower();
            }

            //if (btnSearchClick.ID == "btnInnerSearch")
            //{
            //    if (strAddress != "")
            //    {
            //        strQryCondition += "and restaurantAddress like '%" + strAddress + "%'";
            //    }
            //    if (strState != "")
            //    {
            //        strQryCondition += "and userInfo.provinceId ='" + strState + "'";
            //    }
            //    if (strCity != "")
            //    {
            //        strQryCondition += "and city like '%" + strCity + "%' ";
            //    }
            //    if (typeOfCuisine != "")
            //    {
            //        strQryCondition += typeOfCuisine;
            //    }
            //    double dDistance = 1.0;
            //    if (ddlDistance.SelectedIndex == 1)
            //    {
            //        dDistance = 1.6;
            //    }
            //    if (strDelivery != "")
            //    {
            //        strQryCondition += "and restaurantSetting.isDelivery ='" + strDelivery + "' ";
            //    }
            //    if (strDistance != "")
            //    {
            //        strQryCondition += "and restaurantSetting.freeDeliveryDistance <=" + (Convert.ToDouble(strDistance) * dDistance).ToString();
            //    }
            //}
            strQryCondition += " order by restaurant.zipCode ASC ";

            Session["strQryHeader"] = strQryHeader;
            Session["strQryCondition"] = strQryCondition;
            Session["strPostalCode"] = strFirstChar;


            Response.Redirect(ResolveUrl("~/searchResult.aspx"), false);
        }
        catch (Exception ex)
        {
            
        }
    }
}
