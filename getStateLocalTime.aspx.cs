using System;
using System.Text;
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
using System.Net;
using System.Net.Mail;

public partial class getStateLocalTime : System.Web.UI.Page
{
    BLLNewsLetterSubscriber obj = new BLLNewsLetterSubscriber();
    BLLUserCCInfo objCC = new BLLUserCCInfo();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Request.QueryString["isFeaturedCampaign"] != null && Request.QueryString["isFeaturedCampaign"].Trim() != ""
                && Request.QueryString["isFeatured"] != null && Request.QueryString["isFeatured"].Trim() != "")
            {
                BLLCampaign objCampaign = new BLLCampaign();
                if (Request.QueryString["isFeatured"].Trim() == "1")
                {
                    objCampaign.isFeatured = true;
                }
                else
                {
                    objCampaign.isFeatured = false;
                }
                objCampaign.campaignID = Convert.ToInt64(Request.QueryString["isFeaturedCampaign"].ToString().Trim());
                objCampaign.updateCampaignisFeaturedStatus();
                Response.Write("update");
                Response.End();
            }

            if (Request.QueryString["RemoveFromCart"] != null && Request.QueryString["RemoveFromCart"].Trim() != "")
            {
                if (Session["dtProductCart"] != null)
                {
                    DataTable dtProductCart = (DataTable)Session["dtProductCart"];
                    if (dtProductCart != null)
                    {
                        DataRow[] foundRows = dtProductCart.Select("productID ='" + Request.QueryString["RemoveFromCart"].Trim() + "'");                        
                        if (foundRows.Length > 0)
                        {                            
                            //int rows = foundRows[0].Table.Rows.IndexOf(foundRows[0]);
                            dtProductCart.Rows.Remove(foundRows[0]);
                            Session["dtProductCart"] = dtProductCart;
                            Response.Write(dtProductCart.Rows.Count.ToString());
                            Response.End();
                            /*if (Convert.ToInt32(foundRows[0]["maxQty"].ToString().Trim()) > Convert.ToInt32(foundRows[0]["Qty"].ToString().Trim()))
                            {
                                dtProductCart.Rows[rows]["Qty"] = Convert.ToInt32(dtProductCart.Rows[rows]["Qty"]) + 1;
                            }*/
                        }
                    }
                }
            }


            if (Request.QueryString["RemoveFromCartWithSize"] != null && Request.QueryString["RemoveFromCartWithSize"].Trim() != ""
            && Request.QueryString["size"] != null && Request.QueryString["size"].Trim() != "")
            {
                if (Session["dtProductCart"] != null)
                {
                    DataTable dtProductCart = (DataTable)Session["dtProductCart"];
                    if (dtProductCart != null)
                    {
                        DataRow[] foundRows = dtProductCart.Select("productID ='" + Request.QueryString["RemoveFromCartWithSize"].Trim() + "' and Size='" + Request.QueryString["size"].Trim() + "'");
                        if (foundRows.Length > 0)
                        {
                            dtProductCart.Rows.Remove(foundRows[0]);
                            Session["dtProductCart"] = dtProductCart;
                            Response.Write(dtProductCart.Rows.Count.ToString());
                            Response.End();
                        }
                    }
                }
            }

            if (Request.QueryString["updateCartWithSize"] != null && Request.QueryString["updateCartWithSize"].Trim() != ""
                && Request.QueryString["size"] != null && Request.QueryString["size"].Trim() != ""
                && Request.QueryString["qty"] != null && Request.QueryString["qty"].Trim() != "")
            {
                DataTable dtUser = null;
                long userID = 0;
                if (Session["member"] != null)
                {
                    dtUser = (DataTable)Session["member"];
                }
                else if (Session["restaurant"] != null)
                {
                    dtUser = (DataTable)Session["restaurant"];
                }
                else if (Session["sale"] != null)
                {
                    dtUser = (DataTable)Session["sale"];
                }
                else if (Session["user"] != null)
                {
                    dtUser = (DataTable)Session["user"];
                }
                if (dtUser != null && dtUser.Rows.Count > 0)
                {
                    long.TryParse(dtUser.Rows[0]["userId"].ToString().Trim(), out userID);
                }
                if (Session["dtProductCart"] == null)
                {
                    BLLProducts objProduct = new BLLProducts();
                    objProduct.productID = Convert.ToInt64(Request.QueryString["updateCartWithSize"].Trim());
                    objProduct.createdBy = userID;
                    DataTable dtProduct = objProduct.getProductsByProductIDForClient();
                    if (dtProduct != null && dtProduct.Rows.Count > 0)
                    {
                        DataTable dtProductCart = new DataTable("dtProductCart");
                        DataColumn productID = new DataColumn("productID");
                        DataColumn title = new DataColumn("title");
                        DataColumn valuePrice = new DataColumn("valuePrice");
                        DataColumn sellingPrice = new DataColumn("sellingPrice");
                        DataColumn image = new DataColumn("image");
                        DataColumn enableSize = new DataColumn("enableSize");
                        DataColumn Qty = new DataColumn("Qty", typeof(int));
                        DataColumn Size = new DataColumn("Size");
                        DataColumn shipUSA = new DataColumn("shipUSA");
                        DataColumn shipCanada = new DataColumn("shipCanada");
                        DataColumn estimatedArivalTime = new DataColumn("estimatedArivalTime");
                        DataColumn minQty = new DataColumn("minQty");
                        DataColumn maxQty = new DataColumn("maxQty");
                        DataColumn returnPolicy = new DataColumn("returnPolicy");
                        DataColumn weight = new DataColumn("weight");
                        DataColumn height = new DataColumn("height");
                        DataColumn width = new DataColumn("width");
                        DataColumn dimension = new DataColumn("dimension");
                        DataColumn isVoucherProduct = new DataColumn("isVoucherProduct", typeof(int));

                        dtProductCart.Columns.Add(productID);
                        dtProductCart.Columns.Add(title);
                        dtProductCart.Columns.Add(valuePrice);
                        dtProductCart.Columns.Add(sellingPrice);
                        dtProductCart.Columns.Add(image);
                        dtProductCart.Columns.Add(enableSize);
                        dtProductCart.Columns.Add(Qty);
                        dtProductCart.Columns.Add(Size);
                        dtProductCart.Columns.Add(shipUSA);
                        dtProductCart.Columns.Add(shipCanada);
                        dtProductCart.Columns.Add(estimatedArivalTime);
                        dtProductCart.Columns.Add(minQty);
                        dtProductCart.Columns.Add(maxQty);
                        dtProductCart.Columns.Add(returnPolicy);
                        dtProductCart.Columns.Add(weight);
                        dtProductCart.Columns.Add(height);
                        dtProductCart.Columns.Add(width);
                        dtProductCart.Columns.Add(dimension);
                        dtProductCart.Columns.Add(isVoucherProduct);

                        DataRow dRow;
                        dRow = dtProductCart.NewRow();
                        dRow["productID"] = dtProduct.Rows[0]["productID"].ToString().Trim();
                        dRow["title"] = dtProduct.Rows[0]["title"].ToString().Trim();
                        dRow["valuePrice"] = dtProduct.Rows[0]["valuePrice"].ToString().Trim();
                        dRow["sellingPrice"] = dtProduct.Rows[0]["sellingPrice"].ToString().Trim();
                        dRow["image"] = ConfigurationManager.AppSettings["YourSite"].ToString().Trim() + "/Images/dealfood/" + dtProduct.Rows[0]["restaurantId"].ToString().Trim() + "/thumb/" + dtProduct.Rows[0]["image1"].ToString().Trim();
                        dRow["enableSize"] = dtProduct.Rows[0]["enableSize"].ToString().Trim();
                        dRow["Qty"] = Request.QueryString["qty"].Trim();
                        if (Request.QueryString["size"].Trim() == "sher")
                        {
                            dRow["Size"] = "";
                        }
                        else
                        {
                            dRow["Size"] = Request.QueryString["size"].Trim();
                        }
                        dRow["shipUSA"] = dtProduct.Rows[0]["shipUSA"].ToString().Trim();
                        dRow["shipCanada"] = dtProduct.Rows[0]["shipCanada"].ToString().Trim();
                        dRow["estimatedArivalTime"] = dtProduct.Rows[0]["estimatedArivalTime"].ToString().Trim();
                        dRow["minQty"] = dtProduct.Rows[0]["minQty"].ToString().Trim();
                        dRow["maxQty"] = dtProduct.Rows[0]["maxQty"].ToString().Trim();
                        dRow["returnPolicy"] = dtProduct.Rows[0]["returnPolicy"].ToString().Trim();
                        dRow["weight"] = dtProduct.Rows[0]["weight"].ToString().Trim();
                        dRow["height"] = dtProduct.Rows[0]["height"].ToString().Trim();
                        dRow["width"] = dtProduct.Rows[0]["width"].ToString().Trim();
                        dRow["dimension"] = dtProduct.Rows[0]["dimension"].ToString().Trim();
                        if (Convert.ToBoolean(dtProduct.Rows[0]["isVoucherProduct"].ToString().Trim()))
                        {
                            dRow["isVoucherProduct"] = "1";
                        }
                        else
                        {
                            dRow["isVoucherProduct"] = "0";
                        }

                        dtProductCart.Rows.Add(dRow);
                        Session["dtProductCart"] = dtProductCart;
                        Response.Write(dtProductCart.Rows.Count.ToString());
                        Response.End();
                    }
                }
                else
                {
                    DataTable dtProductCart = (DataTable)Session["dtProductCart"];
                    if (dtProductCart != null)
                    {
                        bool needToAdd = true;
                        if (Request.QueryString["size"].Trim() == "sher")
                        {
                            DataRow[] foundRows = dtProductCart.Select("productID ='" + Request.QueryString["updateCartWithSize"].Trim() + "'");
                            if (foundRows.Length > 0)
                            {
                                needToAdd = false;
                                int rows = foundRows[0].Table.Rows.IndexOf(foundRows[0]);
                                dtProductCart.Rows[rows]["Qty"] = Request.QueryString["qty"].Trim();
                            }
                        }
                        else
                        {
                            DataRow[] foundRows = dtProductCart.Select("productID ='" + Request.QueryString["updateCartWithSize"].Trim() + "' and Size='" + Request.QueryString["size"].Trim() + "'");
                            if (foundRows.Length > 0)
                            {
                                needToAdd = false;
                                int rows = foundRows[0].Table.Rows.IndexOf(foundRows[0]);
                                dtProductCart.Rows[rows]["Qty"] = Request.QueryString["qty"].Trim();
                            }
                        }
                        if (needToAdd)
                        {
                            BLLProducts objProduct = new BLLProducts();
                            objProduct.productID = Convert.ToInt64(Request.QueryString["updateCartWithSize"].Trim());
                            objProduct.createdBy = Convert.ToInt64(userID);
                            DataTable dtProduct = objProduct.getProductsByProductIDForClient();
                            if (dtProduct != null && dtProduct.Rows.Count > 0)
                            {
                                DataRow dRow;
                                dRow = dtProductCart.NewRow();
                                dRow["productID"] = dtProduct.Rows[0]["productID"].ToString().Trim();
                                dRow["title"] = dtProduct.Rows[0]["title"].ToString().Trim();
                                dRow["valuePrice"] = dtProduct.Rows[0]["valuePrice"].ToString().Trim();
                                dRow["sellingPrice"] = dtProduct.Rows[0]["sellingPrice"].ToString().Trim();
                                dRow["image"] = ConfigurationManager.AppSettings["YourSite"].ToString().Trim() + "/Images/dealfood/" + dtProduct.Rows[0]["restaurantId"].ToString().Trim() + "/thumb/" + dtProduct.Rows[0]["image1"].ToString().Trim();
                                dRow["enableSize"] = dtProduct.Rows[0]["enableSize"].ToString().Trim();
                                dRow["Qty"] = Request.QueryString["qty"].Trim();
                                if (Request.QueryString["size"].Trim() == "sher")
                                {
                                    dRow["Size"] = "";
                                }
                                else
                                {
                                    dRow["Size"] = Request.QueryString["size"].Trim();
                                }

                                dRow["shipUSA"] = dtProduct.Rows[0]["shipUSA"].ToString().Trim();
                                dRow["shipCanada"] = dtProduct.Rows[0]["shipCanada"].ToString().Trim();
                                dRow["estimatedArivalTime"] = dtProduct.Rows[0]["estimatedArivalTime"].ToString().Trim();
                                dRow["minQty"] = dtProduct.Rows[0]["minQty"].ToString().Trim();
                                dRow["maxQty"] = dtProduct.Rows[0]["maxQty"].ToString().Trim();
                                dRow["returnPolicy"] = dtProduct.Rows[0]["returnPolicy"].ToString().Trim();
                                dRow["weight"] = dtProduct.Rows[0]["weight"].ToString().Trim();
                                dRow["height"] = dtProduct.Rows[0]["height"].ToString().Trim();
                                dRow["width"] = dtProduct.Rows[0]["width"].ToString().Trim();
                                dRow["dimension"] = dtProduct.Rows[0]["dimension"].ToString().Trim();
                                if (Convert.ToBoolean(dtProduct.Rows[0]["isVoucherProduct"].ToString().Trim()))
                                {
                                    dRow["isVoucherProduct"] = "1";
                                }
                                else
                                {
                                    dRow["isVoucherProduct"] = "0";
                                }
                                dtProductCart.Rows.Add(dRow);
                                Session["dtProductCart"] = dtProductCart;
                            }
                        }
                    }
                    Response.Write(dtProductCart.Rows.Count.ToString());
                    Response.End();
                }

            }


            if (Request.QueryString["RemoveFromCartWithSize"] != null && Request.QueryString["RemoveFromCartWithSize"].Trim() != ""
                 && Request.QueryString["size"] != null && Request.QueryString["size"].Trim() != "")
            {
                if (Session["dtProductCart"] != null)
                {
                    DataTable dtProductCart = (DataTable)Session["dtProductCart"];
                    if (dtProductCart != null)
                    {
                        DataRow[] foundRows = dtProductCart.Select("productID ='" + Request.QueryString["RemoveFromCartWithSize"].Trim() + "' and Size='" + Request.QueryString["size"].Trim() + "'");
                        if (foundRows.Length > 0)
                        {
                            dtProductCart.Rows.Remove(foundRows[0]);
                            Session["dtProductCart"] = dtProductCart;
                            Response.Write(dtProductCart.Rows.Count.ToString());
                            Response.End();
                        }
                    }
                }
            }

            if (Request.QueryString["AddToCartWithSize"] != null && Request.QueryString["AddToCartWithSize"].Trim() != ""
                && Request.QueryString["size"] != null && Request.QueryString["size"].Trim() != ""
                && Request.QueryString["qty"] != null && Request.QueryString["qty"].Trim() != "")
            {
                DataTable dtUser = null;
                long userID = 0;
                if (Session["member"] != null)
                {
                    dtUser = (DataTable)Session["member"];
                }
                else if (Session["restaurant"] != null)
                {
                    dtUser = (DataTable)Session["restaurant"];
                }
                else if (Session["sale"] != null)
                {
                    dtUser = (DataTable)Session["sale"];
                }
                else if (Session["user"] != null)
                {
                    dtUser = (DataTable)Session["user"];
                }
                if (dtUser != null && dtUser.Rows.Count > 0)
                {
                    long.TryParse(dtUser.Rows[0]["userId"].ToString().Trim(), out userID);
                }
                if (Session["dtProductCart"] == null)
                {

                    BLLProducts objProduct = new BLLProducts();
                    objProduct.productID = Convert.ToInt64(Request.QueryString["AddToCartWithSize"].Trim());
                    objProduct.createdBy = userID;
                    DataTable dtProduct = objProduct.getProductsByProductIDForClient();
                    if (dtProduct != null && dtProduct.Rows.Count > 0)
                    {
                        DataTable dtProductCart = new DataTable("dtProductCart");
                        DataColumn productID = new DataColumn("productID");
                        DataColumn title = new DataColumn("title");
                        DataColumn valuePrice = new DataColumn("valuePrice");
                        DataColumn sellingPrice = new DataColumn("sellingPrice");
                        DataColumn image = new DataColumn("image");
                        DataColumn enableSize = new DataColumn("enableSize");
                        DataColumn Qty = new DataColumn("Qty", typeof(int));
                        DataColumn Size = new DataColumn("Size");
                        DataColumn shipUSA = new DataColumn("shipUSA");
                        DataColumn shipCanada = new DataColumn("shipCanada");
                        DataColumn estimatedArivalTime = new DataColumn("estimatedArivalTime");
                        DataColumn minQty = new DataColumn("minQty");
                        DataColumn maxQty = new DataColumn("maxQty");
                        DataColumn returnPolicy = new DataColumn("returnPolicy");
                        DataColumn weight = new DataColumn("weight");
                        DataColumn height = new DataColumn("height");
                        DataColumn width = new DataColumn("width");
                        DataColumn dimension = new DataColumn("dimension");
                        DataColumn isVoucherProduct = new DataColumn("isVoucherProduct", typeof(int));

                        dtProductCart.Columns.Add(productID);
                        dtProductCart.Columns.Add(title);
                        dtProductCart.Columns.Add(valuePrice);
                        dtProductCart.Columns.Add(sellingPrice);
                        dtProductCart.Columns.Add(image);
                        dtProductCart.Columns.Add(enableSize);
                        dtProductCart.Columns.Add(Qty);
                        dtProductCart.Columns.Add(Size);
                        dtProductCart.Columns.Add(shipUSA);
                        dtProductCart.Columns.Add(shipCanada);
                        dtProductCart.Columns.Add(estimatedArivalTime);
                        dtProductCart.Columns.Add(minQty);
                        dtProductCart.Columns.Add(maxQty);
                        dtProductCart.Columns.Add(returnPolicy);
                        dtProductCart.Columns.Add(weight);
                        dtProductCart.Columns.Add(height);
                        dtProductCart.Columns.Add(width);
                        dtProductCart.Columns.Add(dimension);
                        dtProductCart.Columns.Add(isVoucherProduct);


                        DataRow dRow;
                        dRow = dtProductCart.NewRow();
                        dRow["productID"] = dtProduct.Rows[0]["productID"].ToString().Trim();
                        dRow["title"] = dtProduct.Rows[0]["title"].ToString().Trim();
                        dRow["valuePrice"] = dtProduct.Rows[0]["valuePrice"].ToString().Trim();
                        dRow["sellingPrice"] = dtProduct.Rows[0]["sellingPrice"].ToString().Trim();
                        dRow["image"] = ConfigurationManager.AppSettings["YourSite"].ToString().Trim() + "/Images/dealfood/" + dtProduct.Rows[0]["restaurantId"].ToString().Trim() + "/thumb/" + dtProduct.Rows[0]["image1"].ToString().Trim();
                        dRow["enableSize"] = dtProduct.Rows[0]["enableSize"].ToString().Trim();
                        dRow["Qty"] = Request.QueryString["qty"].Trim();
                        dRow["Size"] = Request.QueryString["size"].Trim();
                        dRow["shipUSA"] = dtProduct.Rows[0]["shipUSA"].ToString().Trim();
                        dRow["shipCanada"] = dtProduct.Rows[0]["shipCanada"].ToString().Trim();
                        dRow["estimatedArivalTime"] = dtProduct.Rows[0]["estimatedArivalTime"].ToString().Trim();
                        dRow["minQty"] = dtProduct.Rows[0]["minQty"].ToString().Trim();
                        dRow["maxQty"] = dtProduct.Rows[0]["maxQty"].ToString().Trim();
                        dRow["returnPolicy"] = dtProduct.Rows[0]["returnPolicy"].ToString().Trim();
                        dRow["weight"] = dtProduct.Rows[0]["weight"].ToString().Trim();
                        dRow["height"] = dtProduct.Rows[0]["height"].ToString().Trim();
                        dRow["width"] = dtProduct.Rows[0]["width"].ToString().Trim();
                        dRow["dimension"] = dtProduct.Rows[0]["dimension"].ToString().Trim();
                        if (Convert.ToBoolean(dtProduct.Rows[0]["isVoucherProduct"].ToString().Trim()))
                        {
                            dRow["isVoucherProduct"] = "1";
                        }
                        else
                        {
                            dRow["isVoucherProduct"] = "0";
                        }

                        dtProductCart.Rows.Add(dRow);
                        Session["dtProductCart"] = dtProductCart;
                        Response.Write(dtProductCart.Rows.Count.ToString());
                        Response.End();
                    }
                }
                else
                {
                    DataTable dtProductCart = (DataTable)Session["dtProductCart"];
                    if (dtProductCart != null)
                    {
                        DataRow[] foundRows = dtProductCart.Select("productID ='" + Request.QueryString["AddToCartWithSize"].Trim() + "' and Size='" + Request.QueryString["size"].Trim() + "'");
                        bool needToAdd = true;
                        if (foundRows.Length > 0)
                        {
                            needToAdd = false;
                            int rows = foundRows[0].Table.Rows.IndexOf(foundRows[0]);
                            dtProductCart.Rows[rows]["Qty"] = Request.QueryString["qty"].Trim();
                        }
                        if (needToAdd)
                        {
                            BLLProducts objProduct = new BLLProducts();
                            objProduct.productID = Convert.ToInt64(Request.QueryString["AddToCartWithSize"].Trim());
                            objProduct.createdBy = userID;
                            DataTable dtProduct = objProduct.getProductsByProductIDForClient();
                            if (dtProduct != null && dtProduct.Rows.Count > 0)
                            {
                                DataRow dRow;
                                dRow = dtProductCart.NewRow();
                                dRow["productID"] = dtProduct.Rows[0]["productID"].ToString().Trim();
                                dRow["title"] = dtProduct.Rows[0]["title"].ToString().Trim();
                                dRow["valuePrice"] = dtProduct.Rows[0]["valuePrice"].ToString().Trim();
                                dRow["sellingPrice"] = dtProduct.Rows[0]["sellingPrice"].ToString().Trim();
                                dRow["image"] = ConfigurationManager.AppSettings["YourSite"].ToString().Trim() + "/Images/dealfood/" + dtProduct.Rows[0]["restaurantId"].ToString().Trim() + "/thumb/" + dtProduct.Rows[0]["image1"].ToString().Trim();
                                dRow["enableSize"] = dtProduct.Rows[0]["enableSize"].ToString().Trim();
                                dRow["Qty"] = Request.QueryString["qty"].Trim();
                                dRow["Size"] = Request.QueryString["size"].Trim();
                                dRow["shipUSA"] = dtProduct.Rows[0]["shipUSA"].ToString().Trim();
                                dRow["shipCanada"] = dtProduct.Rows[0]["shipCanada"].ToString().Trim();
                                dRow["estimatedArivalTime"] = dtProduct.Rows[0]["estimatedArivalTime"].ToString().Trim();
                                dRow["minQty"] = dtProduct.Rows[0]["minQty"].ToString().Trim();
                                dRow["maxQty"] = dtProduct.Rows[0]["maxQty"].ToString().Trim();
                                dRow["returnPolicy"] = dtProduct.Rows[0]["returnPolicy"].ToString().Trim();
                                dRow["weight"] = dtProduct.Rows[0]["weight"].ToString().Trim();
                                dRow["height"] = dtProduct.Rows[0]["height"].ToString().Trim();
                                dRow["width"] = dtProduct.Rows[0]["width"].ToString().Trim();
                                dRow["dimension"] = dtProduct.Rows[0]["dimension"].ToString().Trim();
                                if (Convert.ToBoolean(dtProduct.Rows[0]["isVoucherProduct"].ToString().Trim()))
                                {
                                    dRow["isVoucherProduct"] = "1";
                                }
                                else
                                {
                                    dRow["isVoucherProduct"] = "0";
                                }
                                dtProductCart.Rows.Add(dRow);
                                Session["dtProductCart"] = dtProductCart;
                            }
                        }
                    }
                    Response.Write(dtProductCart.Rows.Count.ToString());
                    Response.End();
                }

            }

            if (Request.QueryString["AddToCart"] != null && Request.QueryString["AddToCart"].Trim() != "")
            {
                DataTable dtUser = null;
                long userID = 0;
                if (Session["member"] != null)
                {
                    dtUser = (DataTable)Session["member"];
                }
                else if (Session["restaurant"] != null)
                {
                    dtUser = (DataTable)Session["restaurant"];
                }
                else if (Session["sale"] != null)
                {
                    dtUser = (DataTable)Session["sale"];
                }
                else if (Session["user"] != null)
                {
                    dtUser = (DataTable)Session["user"];
                }
                if (dtUser != null && dtUser.Rows.Count > 0)
                {
                    long.TryParse(dtUser.Rows[0]["userId"].ToString().Trim(), out userID);
                }
                if (Session["dtProductCart"] == null)
                {

                    BLLProducts objProduct = new BLLProducts();
                    objProduct.productID = Convert.ToInt64(Request.QueryString["AddToCart"].Trim());
                    objProduct.createdBy = userID;
                    DataTable dtProduct = objProduct.getProductsByProductIDForClient();
                    if (dtProduct != null && dtProduct.Rows.Count > 0)
                    {
                        DataTable dtProductCart = new DataTable("dtProductCart");
                        DataColumn productID = new DataColumn("productID");
                        DataColumn title = new DataColumn("title");
                        DataColumn valuePrice = new DataColumn("valuePrice");
                        DataColumn sellingPrice = new DataColumn("sellingPrice");
                        DataColumn image = new DataColumn("image");
                        DataColumn enableSize = new DataColumn("enableSize");
                        DataColumn Qty = new DataColumn("Qty", typeof(int));
                        DataColumn Size = new DataColumn("Size");
                        DataColumn shipUSA = new DataColumn("shipUSA");
                        DataColumn shipCanada = new DataColumn("shipCanada");
                        DataColumn estimatedArivalTime = new DataColumn("estimatedArivalTime");
                        DataColumn minQty = new DataColumn("minQty");
                        DataColumn maxQty = new DataColumn("maxQty");
                        DataColumn returnPolicy = new DataColumn("returnPolicy");
                        DataColumn weight = new DataColumn("weight");
                        DataColumn height = new DataColumn("height");
                        DataColumn width = new DataColumn("width");
                        DataColumn dimension = new DataColumn("dimension");
                        DataColumn isVoucherProduct = new DataColumn("isVoucherProduct", typeof(int));

                        dtProductCart.Columns.Add(productID);
                        dtProductCart.Columns.Add(title);
                        dtProductCart.Columns.Add(valuePrice);
                        dtProductCart.Columns.Add(sellingPrice);
                        dtProductCart.Columns.Add(image);
                        dtProductCart.Columns.Add(enableSize);
                        dtProductCart.Columns.Add(Qty);
                        dtProductCart.Columns.Add(Size);
                        dtProductCart.Columns.Add(shipUSA);
                        dtProductCart.Columns.Add(shipCanada);
                        dtProductCart.Columns.Add(estimatedArivalTime);
                        dtProductCart.Columns.Add(minQty);
                        dtProductCart.Columns.Add(maxQty);
                        dtProductCart.Columns.Add(returnPolicy);
                        dtProductCart.Columns.Add(weight);
                        dtProductCart.Columns.Add(height);
                        dtProductCart.Columns.Add(width);
                        dtProductCart.Columns.Add(dimension);
                        dtProductCart.Columns.Add(isVoucherProduct);

                        DataRow dRow;
                        dRow = dtProductCart.NewRow();
                        dRow["productID"] = dtProduct.Rows[0]["productID"].ToString().Trim();
                        dRow["title"] = dtProduct.Rows[0]["title"].ToString().Trim();
                        dRow["valuePrice"] = dtProduct.Rows[0]["valuePrice"].ToString().Trim();
                        dRow["sellingPrice"] = dtProduct.Rows[0]["sellingPrice"].ToString().Trim();
                        dRow["image"] = ConfigurationManager.AppSettings["YourSite"].ToString().Trim() + "/Images/dealfood/" + dtProduct.Rows[0]["restaurantId"].ToString().Trim() + "/thumb/" + dtProduct.Rows[0]["image1"].ToString().Trim();
                        dRow["enableSize"] = dtProduct.Rows[0]["enableSize"].ToString().Trim();
                        dRow["Qty"] = "1";
                        dRow["Size"] = "";
                        dRow["shipUSA"] = dtProduct.Rows[0]["shipUSA"].ToString().Trim();
                        dRow["shipCanada"] = dtProduct.Rows[0]["shipCanada"].ToString().Trim();
                        dRow["estimatedArivalTime"] = dtProduct.Rows[0]["estimatedArivalTime"].ToString().Trim();
                        dRow["minQty"] = dtProduct.Rows[0]["minQty"].ToString().Trim();
                        dRow["maxQty"] = dtProduct.Rows[0]["maxQty"].ToString().Trim();
                        dRow["returnPolicy"] = dtProduct.Rows[0]["returnPolicy"].ToString().Trim();
                        dRow["weight"] = dtProduct.Rows[0]["weight"].ToString().Trim();
                        dRow["height"] = dtProduct.Rows[0]["height"].ToString().Trim();
                        dRow["width"] = dtProduct.Rows[0]["width"].ToString().Trim();
                        dRow["dimension"] = dtProduct.Rows[0]["dimension"].ToString().Trim();
                        if (Convert.ToBoolean(dtProduct.Rows[0]["isVoucherProduct"].ToString().Trim()))
                        {
                            dRow["isVoucherProduct"] = "1";
                        }
                        else
                        {
                            dRow["isVoucherProduct"] = "0";
                        }

                        dtProductCart.Rows.Add(dRow);
                        Session["dtProductCart"] = dtProductCart;
                        Response.Write(dtProductCart.Rows.Count.ToString());
                        Response.End();
                    }
                }
                else
                {
                    DataTable dtProductCart = (DataTable)Session["dtProductCart"];
                    if (dtProductCart != null)
                    {
                        DataRow[] foundRows = dtProductCart.Select("productID ='" + Request.QueryString["AddToCart"].Trim() + "'");
                        bool needToAdd = true;
                        if (foundRows.Length > 0)
                        {
                            needToAdd = false;
                            int rows = foundRows[0].Table.Rows.IndexOf(foundRows[0]);
                            if (Convert.ToInt32(foundRows[0]["maxQty"].ToString().Trim()) > Convert.ToInt32(foundRows[0]["Qty"].ToString().Trim()))
                            {
                                dtProductCart.Rows[rows]["Qty"] = Convert.ToInt32(dtProductCart.Rows[rows]["Qty"]) + 1;
                            }
                        }
                        if (needToAdd)
                        {
                            BLLProducts objProduct = new BLLProducts();
                            objProduct.productID = Convert.ToInt64(Request.QueryString["AddToCart"].Trim());
                            objProduct.createdBy = userID;
                            DataTable dtProduct = objProduct.getProductsByProductIDForClient();
                            if (dtProduct != null && dtProduct.Rows.Count > 0)
                            {
                                DataRow dRow;
                                dRow = dtProductCart.NewRow();
                                dRow["productID"] = dtProduct.Rows[0]["productID"].ToString().Trim();
                                dRow["title"] = dtProduct.Rows[0]["title"].ToString().Trim();
                                dRow["valuePrice"] = dtProduct.Rows[0]["valuePrice"].ToString().Trim();
                                dRow["sellingPrice"] = dtProduct.Rows[0]["sellingPrice"].ToString().Trim();
                                dRow["image"] = ConfigurationManager.AppSettings["YourSite"].ToString().Trim() + "/Images/dealfood/" + dtProduct.Rows[0]["restaurantId"].ToString().Trim() + "/thumb/" + dtProduct.Rows[0]["image1"].ToString().Trim();
                                dRow["enableSize"] = dtProduct.Rows[0]["enableSize"].ToString().Trim();
                                dRow["Qty"] = "1";
                                dRow["Size"] = "";
                                dRow["shipUSA"] = dtProduct.Rows[0]["shipUSA"].ToString().Trim();
                                dRow["shipCanada"] = dtProduct.Rows[0]["shipCanada"].ToString().Trim();
                                dRow["estimatedArivalTime"] = dtProduct.Rows[0]["estimatedArivalTime"].ToString().Trim();
                                dRow["minQty"] = dtProduct.Rows[0]["minQty"].ToString().Trim();
                                dRow["maxQty"] = dtProduct.Rows[0]["maxQty"].ToString().Trim();
                                dRow["returnPolicy"] = dtProduct.Rows[0]["returnPolicy"].ToString().Trim();
                                dRow["weight"] = dtProduct.Rows[0]["weight"].ToString().Trim();
                                dRow["height"] = dtProduct.Rows[0]["height"].ToString().Trim();
                                dRow["width"] = dtProduct.Rows[0]["width"].ToString().Trim();
                                dRow["dimension"] = dtProduct.Rows[0]["dimension"].ToString().Trim();
                                if (Convert.ToBoolean(dtProduct.Rows[0]["isVoucherProduct"].ToString().Trim()))
                                {
                                    dRow["isVoucherProduct"] = "1";
                                }
                                else
                                {
                                    dRow["isVoucherProduct"] = "0";
                                }
                                dtProductCart.Rows.Add(dRow);
                                Session["dtProductCart"] = dtProductCart;
                            }
                        }
                    }
                    Response.Write(dtProductCart.Rows.Count.ToString());
                    Response.End();
                }
            }


            if (Request.QueryString["BillingName"] != null && Request.QueryString["BillingName"].Trim() != "" && 
                Request.QueryString["BillingAddress"] != null && Request.QueryString["BillingAddress"].Trim() != "" &&
                Request.QueryString["CardNumber"] != null && Request.QueryString["CardNumber"].Trim() != "" &&
                Request.QueryString["SecurityCode"] != null && Request.QueryString["SecurityCode"].Trim() != "" &&
                Request.QueryString["Month"] != null && Request.QueryString["Month"].Trim() != "" &&
                Request.QueryString["Year"] != null && Request.QueryString["Year"].Trim() != "" &&
                Request.QueryString["City"] != null && Request.QueryString["City"].Trim() != "" &&
                Request.QueryString["State"] != null && Request.QueryString["State"].Trim() != "" &&
                Request.QueryString["PostalCode"] != null && Request.QueryString["PostalCode"].Trim() != "" &&
                Request.QueryString["CCInfoID"] != null)
            {
                DataTable dtUser = null;
                long userID = 0;
                if (Session["member"] != null)
                {
                    dtUser = (DataTable)Session["member"];
                }
                else if (Session["restaurant"] != null)
                {
                    dtUser = (DataTable)Session["restaurant"];
                }
                else if (Session["sale"] != null)
                {
                    dtUser = (DataTable)Session["sale"];
                }
                else if (Session["user"] != null)
                {
                    dtUser = (DataTable)Session["user"];
                }
                if (dtUser != null && dtUser.Rows.Count > 0)
                {
                    long.TryParse(dtUser.Rows[0]["userId"].ToString().Trim(), out userID);
                }
                GECEncryption objEnc = new GECEncryption();
                if (dtUser.Rows.Count > 0 && dtUser != null)
                {
                    if (Request.QueryString["BillingName"] != "undefined" &&
                        Request.QueryString["BillingAddress"] != "undefined" &&
                        Request.QueryString["CardNumber"] != "undefined" &&
                        Request.QueryString["SecurityCode"] != "undefined" &&
                        Request.QueryString["Month"] != "undefined" &&
                        Request.QueryString["Year"] != "undefined" &&
                        Request.QueryString["City"] != "undefined" &&
                        Request.QueryString["State"] != "undefined" &&
                        Request.QueryString["PostalCode"] != "undefined")
                        //Request.QueryString["CCInfoID"] != "undefined")
                    {

                        if (Request.QueryString["CCInfoID"] == null || Request.QueryString["CCInfoID"].Trim() == "" || Request.QueryString["CCInfoID"].ToString() == "undefined")
                        {
                            objCC = new BLLUserCCInfo();
                            objCC.ccInfoBAddress = HtmlRemoval.StripTagsRegexCompiled(Request.QueryString["BillingAddress"].ToString().Trim());
                            objCC.ccInfoBCity = HtmlRemoval.StripTagsRegexCompiled(Request.QueryString["City"].ToString().Trim());
                            objCC.ccInfoBPostalCode = HtmlRemoval.StripTagsRegexCompiled(Request.QueryString["PostalCode"].ToString().Trim());
                            objCC.ccInfoBProvince = Request.QueryString["State"].ToString().Trim();
                            objCC.createdBy = Convert.ToInt64(dtUser.Rows[0]["userId"].ToString().Trim());
                            objCC.userId = Convert.ToInt64(dtUser.Rows[0]["userId"].ToString().Trim());
                            objCC.ccInfoCCVNumber = objEnc.EncryptData("colintastygochengccv", HtmlRemoval.StripTagsRegexCompiled(Request.QueryString["SecurityCode"].ToString().Trim()));
                            objCC.ccInfoEdate = objEnc.EncryptData("colintastygochengexpirydate", Request.QueryString["Month"].ToString().Trim() + "-" + Request.QueryString["Year"].ToString().Trim());
                            objCC.ccInfoNumber = objEnc.EncryptData("colintastygochengnumber", HtmlRemoval.StripTagsRegexCompiled(Request.QueryString["CardNumber"].ToString().Trim()));
                            objCC.ccInfoBName = objEnc.EncryptData("colintastygochengusername", HtmlRemoval.StripTagsRegexCompiled(Request.QueryString["BillingName"].ToString().Trim()));
                            objCC.ccInfoDEmail = dtUser.Rows[0]["username"].ToString().Trim();
                            string[] strUserName = Request.QueryString["BillingName"].ToString().Trim().Split(' ');
                            objCC.ccInfoDFirstName = strUserName[0].ToString();
                            if (strUserName.Length > 1)
                            {
                                objCC.ccInfoDLastName = strUserName[1].ToString();
                            }
                            objCC.createUserCCInfo();
                            //For New record Inserted
                            Response.Write("1");
                            Response.End();
                        }
                        else
                        {
                            objCC = new BLLUserCCInfo();
                            objCC.ccInfoID = Convert.ToInt64(Request.QueryString["CCInfoID"].ToString().Trim());
                            objCC.ccInfoBAddress = HtmlRemoval.StripTagsRegexCompiled(Request.QueryString["BillingAddress"].ToString().Trim());
                            objCC.ccInfoBCity = HtmlRemoval.StripTagsRegexCompiled(Request.QueryString["City"].ToString().Trim());
                            objCC.ccInfoBPostalCode = HtmlRemoval.StripTagsRegexCompiled(Request.QueryString["PostalCode"].ToString().Trim());
                            objCC.ccInfoBProvince = Request.QueryString["State"].ToString().Trim();
                            objCC.createdBy = Convert.ToInt64(dtUser.Rows[0]["userId"].ToString().Trim());
                            objCC.userId = Convert.ToInt64(dtUser.Rows[0]["userId"].ToString().Trim());
                            objCC.ccInfoCCVNumber = objEnc.EncryptData("colintastygochengccv", HtmlRemoval.StripTagsRegexCompiled(Request.QueryString["SecurityCode"].ToString().Trim()));
                            objCC.ccInfoEdate = objEnc.EncryptData("colintastygochengexpirydate", Request.QueryString["Month"].ToString().Trim() + "-" + Request.QueryString["Year"].ToString().Trim());
                            objCC.ccInfoNumber = objEnc.EncryptData("colintastygochengnumber", HtmlRemoval.StripTagsRegexCompiled(Request.QueryString["CardNumber"].ToString().Trim()));
                            objCC.ccInfoBName = objEnc.EncryptData("colintastygochengusername", HtmlRemoval.StripTagsRegexCompiled(Request.QueryString["BillingName"].ToString().Trim()));
                            objCC.ccInfoDEmail = dtUser.Rows[0]["username"].ToString().Trim();
                            string[] strUserName = Request.QueryString["BillingName"].ToString().Trim().Split(' ');
                            objCC.ccInfoDFirstName = HtmlRemoval.StripTagsRegexCompiled(strUserName[0].ToString());
                            if (strUserName.Length > 1)
                            {
                                objCC.ccInfoDLastName = strUserName[1].ToString();
                            }
                            objCC.updateUserCCInfoByID();
                            //For Record Updated
                            Response.Write("2");
                            Response.End();
                        }
                    }
                    else
                    {
                        //For undefined/invalid field value
                        Response.Write("3");
                        Response.End();
                    }
                }
            }


            if (Request.QueryString["AddToMyFavourite"] != null && Request.QueryString["AddToMyFavourite"].Trim() != "")
            {
                DataTable dtUser = null;
                if (Session["member"] != null)
                {
                    dtUser = (DataTable)Session["member"];
                }
                else if (Session["restaurant"] != null)
                {
                    dtUser = (DataTable)Session["restaurant"];
                }
                else if (Session["sale"] != null)
                {
                    dtUser = (DataTable)Session["sale"];
                }
                else if (Session["user"] != null)
                {
                    dtUser = (DataTable)Session["user"];
                }
                if (dtUser != null && dtUser.Rows.Count > 0)
                {
                    int productID = Convert.ToInt32(Request.QueryString["AddToMyFavourite"].Trim());
                    BLLCampaign objCampaign = new BLLCampaign();
                    objCampaign.campaignID = productID;
                    objCampaign.UserID = Convert.ToInt64(dtUser.Rows[0]["userId"].ToString().Trim());
                    DALCampaign.AddToMyFavorites(objCampaign);
                }
            }

            if (Request.QueryString["slotCampaign"] != null && Request.QueryString["slotCampaign"].Trim().Trim() != "")
            {
                string strReponse = Request.QueryString["slotCampaign"];
                BLLCampaign objCampaign=new BLLCampaign();
                objCampaign.updateCampaignSlots(strReponse.Replace("listItem[]=", ""));                
            }

            if (Request.QueryString["slotProduct"] != null && Request.QueryString["slotProduct"].Trim().Trim() != "")
            {
                string strReponse = Request.QueryString["slotProduct"];
                BLLProducts objproducts = new BLLProducts();
                objproducts.updateProductSlots(strReponse.Replace("listItem[]=", ""));
            }

            if (Request.QueryString["slotProductCategory"] != null && Request.QueryString["slotProductCategory"].Trim().Trim() != "")
            {
                string strReponse = Request.QueryString["slotProductCategory"];
                BLLProducts objproducts = new BLLProducts();
                objproducts.updateProductCategorySlots(strReponse.Replace("listItem[]=", ""));
            }
                        
            
            if (Request.QueryString["DetailID"] != null && Request.QueryString["DetailID"] != ""
               && Request.QueryString["FeedbackData"] != null && Request.QueryString["FeedbackData"].ToString() != ""
               && Request.QueryString["UserComments"] != null && Request.QueryString["UserComments"].ToString().Trim() != ""
               && Request.QueryString["BID"] != null && Request.QueryString["BID"].ToString().Trim() != "" && Request.QueryString["UserID"] != null && Request.QueryString["UserID"].ToString().Trim() != "")
            {

                BLLRestaurantComents RestaurantComments = new BLLRestaurantComents();
                RestaurantComments.bID = Convert.ToInt32(Request.QueryString["BID"].ToString().Trim());
                RestaurantComments.userID = Convert.ToInt32(Request.QueryString["UserID"].ToString().Trim());
                RestaurantComments.detailID = Convert.ToInt32(Request.QueryString["DetailID"].ToString().Trim());
                RestaurantComments.feedback = Request.QueryString["FeedbackData"].ToString().Trim();
                RestaurantComments.userComments = HtmlRemoval.StripTagsRegexCompiled(Uri.UnescapeDataString(Request.QueryString["UserComments"].ToString().Trim()).Replace("\n", "<br>").Replace("\"", "''").Replace(";", ""));
                int Result = RestaurantComments.restaurantComments();
                if (Result != 0)
                {
                    Response.Write("True");
                    Response.End();
                }
                else
                {
                    Response.Write("False");
                    Response.End();
                }
            }
            else if (Request.QueryString["BID"] != null 
                && Request.QueryString["BID"].ToString().Trim() != "" 
                && Request.QueryString["UserID"] != null 
                && Request.QueryString["UserID"].ToString().Trim() != ""
                && Request.QueryString["DetailID"] != null 
                && Request.QueryString["DetailID"] != "")
            {
                BLLRestaurantComents RestaurantComments = new BLLRestaurantComents();
                RestaurantComments.bID = Convert.ToInt32(Request.QueryString["BID"].ToString().Trim());
                RestaurantComments.userID = Convert.ToInt32(Request.QueryString["UserID"].ToString().Trim());
                RestaurantComments.detailID = Convert.ToInt32(Request.QueryString["DetailID"].ToString().Trim());
                RestaurantComments.feedback = null;
                RestaurantComments.userComments = null;
                
                int Result = RestaurantComments.restaurantComments();
                if (Result != 0)
                {
                    Response.Write("True");
                    Response.End();
                }
                else
                {
                    Response.Write("False");
                    Response.End();
                }
            }
        }
        catch (Exception ex)
        {

        }
        if (Request.QueryString["GetAllCities"] != null && Request.QueryString["GetAllCities"].ToString().Trim() != "" )
        {

            BLLCities ObjCities = new BLLCities();
            DataTable dt = ObjCities.GetAllCitiesForSearch();
            if (dt != null && dt.Rows.Count > 0)
            {
                string CompleteData = "";
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    CompleteData += dt.Rows[i]["cityName"].ToString().Trim() + ";";
                    CompleteData += dt.Rows[i]["latitude"].ToString().Trim() + ";";
                    CompleteData += dt.Rows[i]["longitude"].ToString().Trim() + "|";
                }
                Response.Write(CompleteData);
                Response.End();
            }
        }
        if (Request.QueryString["SendEmailToUser"] != null && Request.QueryString["SendEmailToUser"].ToString().Trim() != "" && Request.QueryString["Message"] != null && Request.QueryString["Message"].ToString().Trim() != "")
        {
           SendEmailForDeleteComment(Request.QueryString["SendEmailToUser"].ToString().Trim(), Uri.UnescapeDataString(Request.QueryString["Message"].ToString().Trim()));
           Response.Write("true");
           Response.End();
        
        }
        if (Request.QueryString["UpdateFBShare"] != null && Request.QueryString["UpdateFBShare"].ToString().Trim() != "")
        {
            DataTable dtUser = null;
            if (Session["member"] != null)
            {
                dtUser = (DataTable)Session["member"];
            }
            else if (Session["restaurant"] != null)
            {
                dtUser = (DataTable)Session["restaurant"];
            }
            else if (Session["sale"] != null)
            {
                dtUser = (DataTable)Session["sale"];
            }
            else if (Session["user"] != null)
            {
                dtUser = (DataTable)Session["user"];
            }
            if (dtUser != null && dtUser.Rows.Count > 0)
            {
                BLLUser objuser = new BLLUser();
                objuser.userId = Convert.ToInt32(dtUser.Rows[0]["UserID"].ToString().Trim());

                int Result = objuser.UpdateUserFBShare();
                if (Result != 0)
                {
                    Response.Write("True");
                    Response.End();
                }
                else
                {
                    Response.Write("False");
                    Response.End();
                }
            }
            else
            {
                Response.Write("False");
                Response.End();
            }
        }
        if (Request.QueryString["AddFavDealID"] != null && Request.QueryString["AddFavDealID"].ToString().Trim() != "")
        {

            DataTable dtUser = null;
            if (Session["member"] != null)
            {
                dtUser = (DataTable)Session["member"];
            }
            else if (Session["restaurant"] != null)
            {
                dtUser = (DataTable)Session["restaurant"];
            }
            else if (Session["sale"] != null)
            {
                dtUser = (DataTable)Session["sale"];
            }
            else if (Session["user"] != null)
            {
                dtUser = (DataTable)Session["user"];
            }
            if (dtUser != null && dtUser.Rows.Count > 0)
            {
                BLLDealCatagories ObjDealCatagories = new BLLDealCatagories();
                ObjDealCatagories.Userid = Convert.ToInt64(dtUser.Rows[0]["UserID"].ToString().Trim());
                ObjDealCatagories.DealSubCategoryid = Convert.ToInt64(Request.QueryString["AddFavDealID"].ToString().Trim());
                int Result = ObjDealCatagories.AddUserFavoriteDeal();
                if (Result != 0)
                {
                    Response.Write("True");
                    Response.End();
                }
                else
                {
                    Response.Write("False");
                    Response.End();
                }
            }
            else
            {
                Response.Write("False");
                Response.End();
            }
        }

        if (Request.QueryString["DeleteFavDealID"] != null && Request.QueryString["DeleteFavDealID"].ToString().Trim() != "")
        {

            DataTable dtUser = null;
            if (Session["member"] != null)
            {
                dtUser = (DataTable)Session["member"];
            }
            else if (Session["restaurant"] != null)
            {
                dtUser = (DataTable)Session["restaurant"];
            }
            else if (Session["sale"] != null)
            {
                dtUser = (DataTable)Session["sale"];
            }
            else if (Session["user"] != null)
            {
                dtUser = (DataTable)Session["user"];
            }
            if (dtUser != null && dtUser.Rows.Count > 0)
            {
                BLLDealCatagories ObjDealCatagories = new BLLDealCatagories();
                ObjDealCatagories.Userid = Convert.ToInt64(dtUser.Rows[0]["UserID"].ToString().Trim());
                ObjDealCatagories.DealSubCategoryid = Convert.ToInt64(Request.QueryString["DeleteFavDealID"].ToString().Trim());
                int Result = ObjDealCatagories.DeleteUserFavoriteDeal();
                if (Result != 0)
                {
                    Response.Write("True");
                    Response.End();
                }
                else
                {
                    Response.Write("False");
                    Response.End();
                }
            }
            else
            {
                Response.Write("False");
                Response.End();
            }
        }
        if (Request.QueryString["Subscribe"] != null && Request.QueryString["Subscribe"].ToString().Trim() != "")
        {

            DataTable dtUser = null;
            if (Session["member"] != null)
            {
                dtUser = (DataTable)Session["member"];
            }
            else if (Session["restaurant"] != null)
            {
                dtUser = (DataTable)Session["restaurant"];
            }
            else if (Session["sale"] != null)
            {
                dtUser = (DataTable)Session["sale"];
            }
            else if (Session["user"] != null)
            {
                dtUser = (DataTable)Session["user"];
            }

            BLLNewsLetterSubscriber objSub = new BLLNewsLetterSubscriber();
            Misc.addSubscriberEmail(dtUser.Rows[0]["userName"].ToString().Trim(), Request.QueryString["Subscribe"].ToString().Trim());
            Response.Write("True");
            Response.End();
        }

        if (Request.QueryString["UnSubscribe"] != null && Request.QueryString["UnSubscribe"].ToString().Trim() != "")
        {

            DataTable dtUser = null;
            if (Session["member"] != null)
            {
                dtUser = (DataTable)Session["member"];
            }
            else if (Session["restaurant"] != null)
            {
                dtUser = (DataTable)Session["restaurant"];
            }
            else if (Session["sale"] != null)
            {
                dtUser = (DataTable)Session["sale"];
            }
            else if (Session["user"] != null)
            {
                dtUser = (DataTable)Session["user"];
            }
            BLLNewsLetterSubscriber objSub = new BLLNewsLetterSubscriber();
            Misc.unSubscribeUser(dtUser.Rows[0]["userName"].ToString().Trim(), Convert.ToInt32(Request.QueryString["UnSubscribe"].ToString().Trim()));
            Response.Write("True");
            Response.End();

        }

        if (Request.QueryString["UID"] != null && Request.QueryString["UID"].ToString().Trim() != "" 
            && Request.QueryString["Notes"] != null && Request.QueryString["Notes"].ToString().Trim() != ""
            && Request.QueryString["OrderID"] != null && Request.QueryString["OrderID"].ToString().Trim() != "")
        {
            try
            {
                DataTable dtUser = (DataTable)Session["user"];
                if (dtUser != null && dtUser.Rows.Count > 0)
                {
                    BLLUserComments objuser = new BLLUserComments();
                    objuser.userId = Convert.ToInt64(Request.QueryString["UID"].ToString().Trim());
                    objuser.comment = HtmlRemoval.StripTagsRegexCompiled(Uri.UnescapeDataString(Request.QueryString["Notes"].ToString().Trim()).Replace("\n", "<br>").Replace("\"", "''").Replace(";", ""));
                    objuser.commentby = Convert.ToInt64(dtUser.Rows[0]["userid"].ToString().Trim());
                    objuser.dOrderID = Convert.ToInt64(Request.QueryString["OrderID"].ToString().Trim());
                    int result = objuser.AddNewUserComments();
                    if (result != 0)
                    {
                        Response.Write("true");
                        Response.End();
                    }
                    else
                    {
                        Response.Write("false");
                        Response.End();
                    }


                }
            }
            catch (Exception ex)
            {

            }



        }

        if (Request.QueryString["FBLogin"] != null && Request.QueryString["FBLogin"].ToString().Trim() != "")
        {
            FaceBookLogin(Request.QueryString["FBLogin"].ToString().Trim());
        }

        if (Request.QueryString["colorBoxClose"] != null)
        {            
            HttpCookie colorBoxClose = Request.Cookies["colorBoxClose"];
            if (colorBoxClose == null)  
            {
                colorBoxClose = new HttpCookie("colorBoxClose");
            }
            colorBoxClose.Expires = DateTime.Now.AddHours(20);
            Response.Cookies.Add(colorBoxClose);
            colorBoxClose["colorBoxClose"] = Request.QueryString["colorBoxClose"].ToString();
            Response.Write("Email already exists. Please choose another.");
            Response.End();

        }
        if (Request.QueryString["topslide"] != null)
        {
            HttpCookie topslide = Request.Cookies["topslide"];
            if (topslide == null)
            {
                topslide = new HttpCookie("topslide");
            }
            topslide.Expires = DateTime.Now.AddHours(12);
            Response.Cookies.Add(topslide);
            topslide["topslide"] = Request.QueryString["topslide"].ToString();
            Response.Write("Box Close.");
            Response.End();
        }
        if (Request.QueryString["colorBoxCloseSubscribe"] != null)
        {
            HttpCookie cookie = Request.Cookies["newslettersubscribe"];
            if (cookie == null)
            {
                cookie = new HttpCookie("newslettersubscribe");
            }
            cookie.Expires = DateTime.Now.AddMonths(3);
            Response.Cookies.Add(cookie);
            cookie["newslettersubscribe"] = "";
            Response.Write("Thank you for subscribe with TastyGO.");
            Response.End();

        }
        if (Request.QueryString["sid"] != null)
        {
            try
            {
                Response.Write(DateTime.Now.ToString("MMMM dd, yyyy HH:mm:ss"));
                Response.End();
            }
            catch (Exception ex)
            {               
            }
        }
        if (Request.QueryString["karma"] != null)
        {
            try
            {
                if (Session["member"] != null || Session["restaurant"] != null || Session["sale"] != null || Session["user"]!=null)
                {
                    DataTable dtUser = null;

                    if (Session["member"] != null)
                    {
                        dtUser = (DataTable)Session["member"];
                    }
                    else if (Session["restaurant"] != null)
                    {
                        dtUser = (DataTable)Session["restaurant"];
                    }
                    else if (Session["sale"] != null)
                    {
                        dtUser = (DataTable)Session["sale"];
                    }
                    else if (Session["user"] != null)
                    {
                        dtUser = (DataTable)Session["user"];
                    }
                    if (dtUser != null && dtUser.Rows.Count > 0)
                    {
                        //BLLKarmaPoints bllKarma = new BLLKarmaPoints();
                        //bllKarma.userId = Convert.ToInt64(dtUser.Rows[0]["userId"].ToString().Trim());
                        //bllKarma.karmaPoints = 1;
                        //bllKarma.karmaPointsType = "Share";
                        //bllKarma.createdBy = Convert.ToInt64(dtUser.Rows[0]["userId"].ToString().Trim());
                        //bllKarma.createdDate = DateTime.Now;
                        //bllKarma.createKarmaPoints();
                    }
                }
            }
            catch (Exception ex)
            {
              
            }
        }
        if (Request.QueryString["eid"] != null && Request.QueryString["fName"] != null && Request.QueryString["uPass"] != null)
        {
            try
            {
                if (!Misc.validateEmailAddress(Request.QueryString["eid"].ToString().Trim()))
                {
                    Response.Write("Please enter a valid Email ID");
                    Response.End();
                    return;
                }

                if (!Misc.validateUserName(Request.QueryString["fName"].ToString().Trim()))
                {
                    Response.Write("Please enter a valid Name");
                    Response.End();
                    return;
                }

                if (!Misc.validatePasswordForSignup(Request.QueryString["uPass"].ToString().Trim()))
                {
                    Response.Write("Please enter a valid Password");
                    Response.End();
                    return;
                }

                BLLUser obj = new BLLUser();
                obj.userName = Request.QueryString["eid"].Trim();
                obj.email = Request.QueryString["eid"].Trim();
                obj.referralId = "";
                if (!obj.getUserByUserName())
                {
                    string[] strName = Request.QueryString["fName"].Split(' ');
                    if (strName.Length == 2)
                    {
                        obj.firstName = strName[0].ToString();
                        obj.lastName = strName[1].ToString();
                    }
                    else
                    {
                        obj.firstName = Request.QueryString["fName"].Trim();
                        obj.lastName = "";
                    }
                    obj.userName = Request.QueryString["eid"].Trim();
                    obj.userPassword = Request.QueryString["uPass"].Trim();
                    obj.email = Request.QueryString["eid"].Trim();

                    //For Customer 
                    obj.userTypeID = 4;
                    obj.isActive = false;

                    obj.referralId = "";
                    obj.countryId = 2;
                    //if (hfProvince.Value != "0")
                    //{
                    obj.provinceId = 3;
                    //}
                    obj.friendsReferralId = GetUserRefferalId();
                    obj.howYouKnowUs = "";
                    obj.ipAddress = Request.UserHostAddress.ToString();
                    long result = obj.createUser();
                    HttpCookie yourCity = Request.Cookies["yourCity"];
                    string strCityid = "337";
                    if (yourCity != null)
                    {
                        strCityid = yourCity.Values[0].ToString().Trim();
                    }
                    Misc.addSubscriberEmail(Request.QueryString["eid"].Trim(), strCityid);

                    HttpCookie cookie2 = Request.Cookies["newslettersubscribe"];
                    if (cookie2 == null)
                    {
                        cookie2 = new HttpCookie("newslettersubscribe");
                    }
                    cookie2.Expires = DateTime.Now.AddMonths(1);
                    Response.Cookies.Add(cookie2);
                    cookie2["newslettersubscribe"] = Request.QueryString["eid"].Trim();

                    if (result != 0)
                    {
                        //If exits then it will update into the User Info data table
                        GetAndSetAffInfoFromCookieInUserInfo(int.Parse(result.ToString().Trim()));


                        string strEncryptUserID = (Convert.ToInt64(result.ToString()) + 111111).ToString();
                        SendMailWithActiveCode(Request.QueryString["eid"].Trim(), Request.QueryString["uPass"].Trim(), Request.QueryString["eid"].Trim(), strEncryptUserID);
                        HttpCookie cookie = Request.Cookies["tastygoSignup"];
                        if (cookie == null)
                        {
                            cookie = new HttpCookie("tastygoSignup");
                        }
                        cookie.Expires = DateTime.Now.AddMonths(1);
                        Response.Cookies.Add(cookie);
                        cookie["tastygoSignup"] = Request.QueryString["eid"].ToString();
                        Response.Write("Please check your email inbox for your activation email.");
                        Response.End();
                    }
                    else
                    {
                        Response.Write("Sorry you could not register for right now please try again.");
                        Response.End();
                    }
                }
                else
                {
                    Response.Write("Email already exists. Please choose another.");
                    Response.End();
                }
            }
            catch (Exception ex)
            {
            }
        }
        if (Request.QueryString["loginID"] != null && Request.QueryString["loginPass"] != null)
        {

            try
            {
                
                if (!Misc.validateEmailAddress(Request.QueryString["loginID"].ToString().Trim()))
                {
                    Response.Write("Invalid user name or password.");
                    Response.End();
                    return;
                }
                BLLUser obj = new BLLUser();

                obj.userName = Request.QueryString["loginID"].Trim();
                obj.userPassword = Request.QueryString["loginPass"].Trim();
                //Session["userEmail"] = Request.QueryString["loginID"].Trim();


                DataTable dtUser = obj.validateUserNamePassword();

                if (dtUser != null && dtUser.Rows.Count > 0)
                {
                    if (dtUser.Rows[0]["userTypeID"].ToString() == "4")
                    {
                        Session["member"] = dtUser;
                        Session.Remove("restaurant");
                        Session.Remove("sale");
                        Session.Remove("user");
                        //Get the AffiliateInfo from Cookie 
                        //If exits then it will update into the User Info data table
                        GetAndSetAffInfoFromCookieInUserInfo(int.Parse(dtUser.Rows[0]["userId"].ToString().Trim()));
                    }
                    else if (dtUser.Rows[0]["userTypeID"].ToString() == "3")
                    {
                        Session["restaurant"] = dtUser;
                        Session.Remove("member");
                        Session.Remove("sale");
                        Session.Remove("user");
                        //Get the AffiliateInfo from Cookie 
                        //If exits then it will update into the User Info data table
                        GetAndSetAffInfoFromCookieInUserInfo(int.Parse(dtUser.Rows[0]["userId"].ToString().Trim()));
                    }
                    else if (dtUser.Rows[0]["userTypeID"].ToString() == "5")
                    {
                        Session["sale"] = dtUser;
                        Session.Remove("member");
                        Session.Remove("restaurant");
                        Session.Remove("user");
                        //Get the AffiliateInfo from Cookie 
                        //If exits then it will update into the User Info data table
                        GetAndSetAffInfoFromCookieInUserInfo(int.Parse(dtUser.Rows[0]["userId"].ToString().Trim()));
                    }
                    else
                    {
                        Session["user"] = dtUser;
                        Session.Remove("member");
                        Session.Remove("restaurant");
                        Session.Remove("sale");

                        GetAndSetAffInfoFromCookieInUserInfo(int.Parse(dtUser.Rows[0]["userId"].ToString().Trim()));
                    }
                    HttpCookie cookie = Request.Cookies["tastygoSignup"];
                    if (cookie == null)
                    {
                        cookie = new HttpCookie("tastygoSignup");
                    }
                    cookie.Expires = DateTime.Now.AddMonths(1);
                    Response.Cookies.Add(cookie);
                    cookie["tastygoSignup"] = Request.QueryString["loginID"].Trim();
                    HttpCookie cookie2 = Request.Cookies["tastygoLogin"];
                    if (cookie2 == null)
                    {
                        cookie2 = new HttpCookie("tastygoLogin");
                    }
                    cookie2.Expires = DateTime.Now.AddHours(1);
                    Response.Cookies.Add(cookie2);
                    cookie2["tastygoLogin"] = "true";
                    Response.Write("User login successfully.");
                    Response.End();

                }
                else
                {
                    Response.Write("Invalid user name or password.");
                    Response.End();
                }
            }
            catch (Exception ex)
            {

            }
        }

        if (Request.QueryString["subEmail"] != null && Request.QueryString["subCity"] != null)
        {
            if (!Misc.validateEmailAddress(Request.QueryString["subEmail"].ToString().Trim()))
            {
                Response.Write("Please enter a valid Email ID");
                Response.End();
                return;
            }

            Misc.addSubscriberEmail(Request.QueryString["subEmail"].ToString().Trim(), Request.QueryString["subCity"].ToString().Trim());
            HttpCookie cookie = Request.Cookies["newslettersubscribe"];
            if (cookie == null)
            {
                cookie = new HttpCookie("newslettersubscribe");
            }
            cookie.Expires = DateTime.Now.AddMonths(3);
            Response.Cookies.Add(cookie);
            cookie["newslettersubscribe"] = Request.QueryString["subEmail"].ToString().Trim();
            Response.Write("Thank you for subscribe with TastyGO.");
            Response.End();
        }


        if (Request.QueryString["forgetpassword"] != null && Request.QueryString["forgetpassword"].Trim() != "")
        {
            try
            {
                if (!Misc.validateEmailAddress(Request.QueryString["forgetpassword"].ToString().Trim()))
                {
                    Response.Write("Please enter a valid Email ID");
                    Response.End();
                    return;
                }
                BLLUser obj = new BLLUser();

                obj.email = Request.QueryString["forgetpassword"].Trim();
                if (obj.getUserByEmail())
                {
                    DataTable dtUser = obj.getUserDetailByEmail();
                    if (dtUser != null && dtUser.Rows.Count > 0)
                    {
                        if (Convert.ToBoolean(dtUser.Rows[0]["isActive"].ToString()))
                        {

                            if (SendMailForPassword(dtUser.Rows[0]["email"].ToString(), dtUser.Rows[0]["userid"].ToString(), dtUser.Rows[0]["userName"].ToString(), dtUser.Rows[0]["firstName"].ToString() + " " + dtUser.Rows[0]["lastName"].ToString()))
                            {
                                Response.Write("Account information sent to your email address.");
                                Response.End();

                            }
                            else
                            {
                                Response.Write("Email sending failed. Please try again.");
                                Response.End();

                            }
                        }
                        else
                        {

                            string strUserID = (Convert.ToInt32(dtUser.Rows[0]["userID"].ToString().Trim()) + 111111).ToString();
                            if (SendMailWithActiveCode(dtUser.Rows[0]["email"].ToString(), dtUser.Rows[0]["userPassword"].ToString(), dtUser.Rows[0]["userName"].ToString(), strUserID, dtUser.Rows[0]["firstName"].ToString() + " " + dtUser.Rows[0]["lastName"].ToString()))
                            {
                                Response.Write("Account information sent to your email address.");
                                Response.End();
                            }
                            else
                            {
                                Response.Write("Email sending failed. Please try again.");
                                Response.End();

                            }
                        }
                    }
                }
                else
                {
                    Response.Write("This email address does not exist.");
                    Response.End();
                }
            }
            catch (Exception ex)
            {


            }
        }

        if (Request.QueryString["FeedBackEmail"] != null && Request.QueryString["FeedBackText"] != null && Request.QueryString["FeedbackYourName"] != null)
        {
            string SenderEmail = Request.QueryString["FeedBackEmail"].Trim();
            string FeedBackText = Request.QueryString["FeedBackText"].Trim();
            string FeedbackYourName = Request.QueryString["FeedbackYourName"].Trim();

            MailMessage message = new MailMessage();

            StringBuilder sb = new StringBuilder();
            try
            {
                if (!Misc.validateEmailAddress(Request.QueryString["FeedBackEmail"].ToString().Trim()))
                {
                    Response.Write("Please enter a valid Email ID");
                    Response.End();
                    return;
                }
                string fromAddress = ConfigurationManager.AppSettings["AdminEmail"].ToString().Trim();

                message.IsBodyHtml = true;
                sb.Append("<!DOCTYPE html PUBLIC '-//W3C//DTD HTML 4.01 Transitional//EN'><html><head><meta http-equiv='Content-Type' content='text/html; charset=utf-8'><meta name='viewport' content='width = 800'><title>Order Confirmation!</title><style type='text/css'>a.aapl-link{text-decoration: none;}a.aapl-link:hover{text-decoration: underline;}</style><style media='only screen and (max-device-width: 680px)' type='text/css'>*{line-height: normal !important;}</style></head>");
                sb.Append("<body bgcolor='#E4E4E4' style='margin: 0; padding: 0'><table width='100%' bgcolor='#E4E4E4' cellpadding='0' cellspacing='0' align='center'><tr><td><table width='800' align='center' border='0' cellspacing='0' cellpadding='0'><tr><td><div style='margin: 10px 0px 12px 0px; font-family:  Arial;color: #333333; font-size: 14px; line-height: 1.3em;'><img src='http://tazzling.com/images/logoForMail.png' alt='TastyGo' border='0'></div></td></tr></table>");
                sb.Append("<table width='800' align='center' border='0' cellspacing='0' cellpadding='0'><tr><td style='-webkit-border-radius: 8px; background-color: #ffffff' bgcolor='#ffffff'><table width='720' align='center' border='0' cellspacing='0' cellpadding='0'><tr valign='top'><td width='720' bgcolor='#FFFFFF' align='left'>");
                sb.Append("<div style='margin: 40px 0px 0px 15px; font-family:  Arial;color: #333333; font-size: 14px; line-height: 1.3em;'>");
                sb.Append("<strong>Dear Admin,</strong></div>");
                sb.Append("<div style='margin: 20px 0px 20px 15px; font-family: Arial;color: #000000; font-size: 18px; line-height: 1.3em;'><strong>You have received following question from " + FeedbackYourName + "(<a href='mailto:" + SenderEmail + "'>" + SenderEmail + "</a>)</strong></div>");
                sb.Append("<div style='margin: 0px 0px 10px 15px; font-family:  Arial;color: #333333; font-size: 14px; line-height: 1.4em;'>Question : " + FeedBackText + "</div>");



                sb.Append("<div style='margin: 0px 0px 10px 15px; font-family: Arial;border-top: 1px solid #eeeeee; font-size: 12px; line-height: 1.3em;'>&nbsp;</div>");
                sb.Append("<div style='margin: 0px 10px 20px 15px; font-family:  Arial;color: #333333; font-size: 14px; line-height: 1.3em; clear: both;'><strong>Best regards,</strong><br>");
                sb.Append(ConfigurationManager.AppSettings["EmailSignature"].ToString().Trim() + "</div>");
                sb.Append("</td></tr></table></td></tr></table><table width='560' border='0' cellspacing='0' cellpadding='0' align='center'><tr><td style='padding: 20px 20px 10px 24px;'><div style='font-family: Geneva, Verdana, Arial, Helvetica, sans-serif; font-size: 9px;line-height: 12px; color: #858585;'></div></td></tr>");
                sb.Append("<tr><td style='padding: 0 20px 10px 24px;'>    <div style='font-family: Geneva, Verdana, Arial, Helvetica, sans-serif; font-size: 9px;        line-height: 12px; color: #858585;'>        Copyright &copy; 2011 Tazzling.Com. All Rights Reserved</div>    <div style='font-family: Geneva, Verdana, Arial, Helvetica, sans-serif; font-size: 9px;        line-height: 12px; color: #858585;'>        <a href='http://www.tazzling.com/' style='font-family: Geneva, Verdana, Arial, Helvetica, sans-serif;");
                sb.Append("font-size: 9px; line-height: 12px; color: #858585; text-decoration: underline;'>Keep Informed</a> / <a href='http://www.tazzling.com/terms-customer.aspx' style='font-family: Geneva, Verdana, Arial, Helvetica, sans-serif;    font-size: 9px; line-height: 12px; color: #858585; text-decoration: underline;'>    Privacy Policy</a> / <a href='http://www.tazzling.com/contact-us.aspx' style='font-family: Geneva, Verdana, Arial, Helvetica, sans-serif;  font-size: 9px; line-height: 12px; color: #858585; text-decoration: underline;'>Contact Us</a></div>");
                sb.Append("</td></tr><tr></tr></table></td></tr></table></body></html>");
                message.Body = sb.ToString();
                try
                {
                    Misc.SendEmail("info@tazzling.com", "", "", SenderEmail, "Question from Tazzling.Com", message.Body);
                }
                catch (Exception ex)
                {

                }

            }
            catch (Exception ex)
            {
            }
        }

        if (Request.QueryString["TrackFriendEmail"] != null && Request.QueryString["TrachFriendMessage"] != null && Request.QueryString["TrachFriendTitle"] != null && Request.QueryString["TrachDealID"] != null)
        {
            try
            {
                if (!Misc.validateEmailAddress(Request.QueryString["TrackFriendEmail"].ToString().Trim()))
                {
                    Response.Write("Please enter a valid Email ID");
                    Response.End();
                    return;
                }
                //watermark.waterMark("str");

                //return;
                if (Session["member"] != null || Session["restaurant"] != null || Session["sale"] != null || Session["user"]!=null)
                {
                    DataTable dtUser = null;
                    if (Session["member"] != null)
                    {
                        dtUser = (DataTable)Session["member"];
                    }
                    else if (Session["restaurant"] != null)
                    {
                        dtUser = (DataTable)Session["restaurant"];
                    }
                    else if (Session["sale"] != null)
                    {
                        dtUser = (DataTable)Session["sale"];
                    }
                    else if (Session["user"] != null)
                    {
                        dtUser = (DataTable)Session["user"];
                    }
                    //if (dtUser != null && dtUser.Rows.Count > 0)
                    //{
                    //    BLLKarmaPoints bllKarma = new BLLKarmaPoints();
                    //    bllKarma.userId = Convert.ToInt64(dtUser.Rows[0]["userId"].ToString().Trim());
                    //    bllKarma.karmaPoints = 1;
                    //    bllKarma.karmaPointsType = "Share";
                    //    bllKarma.createdBy = Convert.ToInt64(dtUser.Rows[0]["userId"].ToString().Trim());
                    //    bllKarma.createdDate = DateTime.Now;
                    //    bllKarma.createKarmaPoints();
                    //}
                    //string strCityID = "337";
                    //HttpCookie yourCity = Request.Cookies["yourCity"];
                    //if (yourCity != null)
                    //{
                    //    strCityID = yourCity.Values[0].ToString().Trim();
                    //}
                    //Misc.addSubscriberEmail(Request.QueryString["TrackFriendEmail"].Trim(), strCityID);


                    string strUid = (Convert.ToInt64(dtUser.Rows[0]["userId"].ToString().Trim()) + 111111).ToString();
                    string strUserName = dtUser.Rows[0]["firstName"].ToString() + " " + dtUser.Rows[0]["lastName"].ToString();
                    if (SendMailToTrackFriends(Request.QueryString["TrackFriendEmail"].Trim(), strUserName, strUid, Request.QueryString["TrachFriendTitle"].Trim(), Request.QueryString["TrachDealID"].Trim()))
                    {
                        Response.Write("Email has been sent successfully.");
                        Response.End();
                    }
                    else
                    {
                        Response.Write("Email sent failed.");
                        Response.End();
                    }

                }

            }
            catch (Exception ex)
            { }
        }


    }
   
    private bool SendMailToTrackFriends(string strEmailAddress, string strUserName, string strUserID, string strTitle, string strDealID)
    {
        MailMessage message = new MailMessage();

        StringBuilder sb = new StringBuilder();

        
     
           try
           {
               strUserID = strUserID + "_" + strDealID.Trim();
               string toAddress = strEmailAddress;
               string fromAddress = ConfigurationManager.AppSettings["AdminEmail"].ToString().Trim();
               string Subject = "You got a message from \"" + strUserName + "\"";
               message.IsBodyHtml = true;
               sb.Append("<!DOCTYPE html PUBLIC '-//W3C//DTD HTML 4.01 Transitional//EN'><html><head><meta http-equiv='Content-Type' content='text/html; charset=utf-8'><meta name='viewport' content='width = 800'><title>Order Confirmation!</title><style type='text/css'>a.aapl-link{text-decoration: none;}a.aapl-link:hover{text-decoration: underline;}</style><style media='only screen and (max-device-width: 680px)' type='text/css'>*{line-height: normal !important;}</style></head>");
               sb.Append("<body bgcolor='#E4E4E4' style='margin: 0; padding: 0'><table width='100%' bgcolor='#E4E4E4' cellpadding='0' cellspacing='0' align='center'><tr><td><table width='800' align='center' border='0' cellspacing='0' cellpadding='0'><tr><td><div style='margin: 10px 0px 12px 0px; font-family: Arial;color: #333333; font-size: 14px; line-height: 1.3em;'><img src='http://tazzling.com/images/logoForMail.png' alt='TastyGo' border='0'></div></td></tr></table>");
               sb.Append("<table width='800' align='center' border='0' cellspacing='0' cellpadding='0'><tr><td style='-webkit-border-radius: 8px; background-color: #ffffff' bgcolor='#ffffff'><table width='720' align='center' border='0' cellspacing='0' cellpadding='0'><tr valign='top'><td width='720' bgcolor='#FFFFFF' align='left'>");
               sb.Append("<div style='margin: 40px 0px 0px 15px; font-family: Arial;color: #333333; font-size: 14px; line-height: 1.3em;'>");
               sb.Append("<strong>Dear " + strEmailAddress + ",</strong></div>");
               sb.Append("<div style='margin: 20px 0px 20px 15px; font-family: Arial;color: #000000; font-size: 18px; line-height: 1.3em;'><strong>Your friend " + strUserName + " has purchased amazing deal on Tazzling.com.</strong></div>");
               sb.Append("<div style='margin: 0px 0px 10px 15px; font-family: Arial;color: #333333; font-size: 14px; line-height: 1.4em;'>I just found a great daily deal site. They offer huge discounts on food fares, spa and other great outdoor adventures for more than 50% off! Best of all, signup is free! One of their today’s deal is  \"<a href='" + ConfigurationManager.AppSettings["YourSite"].ToString().Trim() + "/r/" + strUserID + "'>" + strTitle.Trim() + "</a>\".</div>");
               sb.Append("<div style='margin: 0px 0px 10px 15px; font-family: Arial;color: #333333; font-size: 14px; line-height: 1.4em;'>Check it out <a href='" + ConfigurationManager.AppSettings["YourSite"].ToString().Trim() + "/r/" + strUserID + "'>http://www.tazzling.com</a></div>");
               sb.Append("<div style='margin: 0px 0px 10px 15px; font-family: Arial;border-top: 1px solid #eeeeee; font-size: 12px; line-height: 1.3em;'>&nbsp;</div>");
               sb.Append("<div style='margin: 0px 10px 20px 15px; font-family: Arial;color: #333333; font-size: 14px; line-height: 1.3em; clear: both;'><strong>Best regards,</strong><br>");
               sb.Append(strUserName + "</div>");
               sb.Append("</td></tr></table></td></tr></table><table width='560' border='0' cellspacing='0' cellpadding='0' align='center'><tr><td style='padding: 20px 20px 10px 24px;'><div style='font-family: Geneva, Verdana, Arial, Helvetica, sans-serif; font-size: 9px;line-height: 12px; color: #858585;'></div></td></tr>");
               sb.Append("<tr><td style='padding: 0 20px 10px 24px;'>    <div style='font-family: Geneva, Verdana, Arial, Helvetica, sans-serif; font-size: 9px;        line-height: 12px; color: #858585;'>        Copyright &copy; 2011 Tazzling.Com. All Rights Reserved</div>    <div style='font-family: Geneva, Verdana, Arial, Helvetica, sans-serif; font-size: 9px;        line-height: 12px; color: #858585;'>        <a href='http://www.tazzling.com/' style='font-family: Geneva, Verdana, Arial, Helvetica, sans-serif;");
               sb.Append("font-size: 9px; line-height: 12px; color: #858585; text-decoration: underline;'>Keep Informed</a> / <a href='http://www.tazzling.com/terms-customer.aspx' style='font-family: Geneva, Verdana, Arial, Helvetica, sans-serif;    font-size: 9px; line-height: 12px; color: #858585; text-decoration: underline;'>    Privacy Policy</a> / <a href='http://www.tazzling.com/contact-us.aspx' style='font-family: Geneva, Verdana, Arial, Helvetica, sans-serif;  font-size: 9px; line-height: 12px; color: #858585; text-decoration: underline;'>Contact Us</a></div>");
               sb.Append("</td></tr><tr></tr></table></td></tr></table></body></html>");

               message.Body = sb.ToString();

               return Misc.SendEmail(toAddress, "", "", fromAddress, Subject, message.Body);
           }
           catch (Exception ex)
           {
               return false;
           }
       }

    #region Send Email for Forgot Password

    private bool SendMailWithActiveCode(string strEmailAddress, string strPassword, string strUserName, string strUserID, string strUserFullName)
    {
        MailMessage message = new MailMessage();
        StringBuilder mailBody = new StringBuilder();

       try
              {

                  string toAddress = strEmailAddress;
                  string fromAddress = ConfigurationManager.AppSettings["AdminEmail"].ToString().Trim();
                  string Subject = ConfigurationManager.AppSettings["EmailSubjectForgetPassword"].ToString().Trim();
                  message.IsBodyHtml = true;
                  mailBody.Append("<!DOCTYPE html PUBLIC '-//W3C//DTD HTML 4.01 Transitional//EN'>");
                  mailBody.Append("<html><head><meta http-equiv='Content-Type' content='text/html; charset=utf-8'><meta name='viewport' content='width = 600'><title>Thank You for Registering with Tastygo</title>");
                  mailBody.Append("<style type='text/css'>a.aapl-link{text-decoration: none;}a.aapl-link:hover{text-decoration: underline;}</style><style media='only screen and (max-device-width: 480px)' type=text/css>*{line-height: normal !important;}</style></head>");
                  mailBody.Append("<body bgcolor='#E4E4E4' style='margin: 0; padding: 0'><table width='100%' bgcolor='#E4E4E4' cellpadding='0' cellspacing='0' align='center'><tr><td><table width='560' align='center' border='0' cellspacing='0' cellpadding='0'><tr><td><div style='margin: 10px 0px 12px 0px; font-family: Arial;color: #333333; font-size: 14px; line-height: 1.3em;'><img src='http://tazzling.com/images/logoForMail.png' alt='TastyGo' border='0'></div></td></tr></table>");
                  mailBody.Append("<table width='560' align='center' border='0' cellspacing='0' cellpadding='0'><tr><td style='-webkit-border-radius: 8px; background-color: #ffffff' bgcolor='#ffffff'><table width='520' align='center' border='0' cellspacing='0' cellpadding='0'><tr valign='top'><td width='520' bgcolor='#FFFFFF' align='left'><div style='margin: 40px 0px 20px 15px; font-family: Arial;color: #000000; font-size: 18px; line-height: 1.3em;'> <strong>Thank you for choosing Tastygo, Your One-Stop Online  Daily Deal Website.</strong>");
                  mailBody.Append("</div>");
                  mailBody.Append("<div style='margin: 0px 0px 20px 15px; font-family: Arial;border-top: 1px solid #eeeeee; font-size: 12px; line-height: 1.3em;'>&nbsp;</div><div style='margin: 0px 60px 15px 15px; font-family: Arial;color: #333333; font-size: 14px; line-height: 1.3em;'>");
                  mailBody.Append("<strong>Dear " + strUserFullName + "</strong></div>");

                  mailBody.Append("<div style='margin: 0px 0px 10px 15px; font-family: Arial; color: #333333; font-size: 14px; line-height: 1.4em;'>");
                  mailBody.Append("With the power of group ordering concept, Tastygo brings amazing deal, from 50%~ 90% off  around your neighbourhood.");
                  mailBody.Append("</div>");

                  mailBody.Append("<div style='margin: 0px 0px 10px 15px; font-family: Arial; color: #333333; font-size: 14px; line-height: 1.4em;'>");
                  mailBody.Append("First to activate your account, please click the follow the link below:<br> <a href='" + ConfigurationManager.AppSettings["YourSite"].ToString().Trim() + "/confirmcontact.aspx?c=" + strUserID + "'>" + ConfigurationManager.AppSettings["YourSite"].ToString().Trim() + "/confirmcontact.aspx?c=" + strUserID + "</a>");
                  mailBody.Append("</div>");
                  mailBody.Append("<div style='margin: 0px 0px 10px 15px; font-family: Arial; color: #333333; font-size: 14px; line-height: 1.4em;'>");
                  mailBody.Append("If clicking on the link doesn't work, try copy & paste it into your browser.");
                  mailBody.Append("</div>");
                  mailBody.Append("<div style='margin: 0px 0px 10px 15px; font-family: Arial; color: #333333; font-size: 14px; line-height: 1.4em;'>");
                  mailBody.Append("Account detail:");
                  mailBody.Append("</div>");
                  mailBody.Append("<div style='margin: 0px 0px 10px 15px; font-family: Arial; color: #333333; font-size: 14px; line-height: 1.4em;'>");
                  mailBody.Append("User Name :  " + strUserName);
                  mailBody.Append("</div>");
                  mailBody.Append("<div style='margin: 0px 0px 10px 15px; font-family: Arial; color: #333333; font-size: 14px; line-height: 1.4em;'>");
                  mailBody.Append("Password :" + strPassword.ToString().Trim());
                  mailBody.Append("</div>");
                  mailBody.Append("<div style='margin: 0px 0px 10px 15px; font-family: Arial; color: #333333; font-size: 14px; line-height: 1.4em;'>");
                  mailBody.Append("If you have any questions, feel free to contact us at <a href='mailto:support@tazzling.com'>support@tazzling.com</a>");
                  mailBody.Append("</div>");
                  mailBody.Append("<div style='margin: 0px 0px 10px 15px; font-family: Arial; color: #333333; font-size: 14px; line-height: 1.4em;'>");
                  mailBody.Append("We wish you enjoy our deal experience.");
                  mailBody.Append("</div>");
                  mailBody.Append("<div style='margin: 0px 0px 10px 15px; font-family: Arial; color: #333333; font-size: 14px; line-height: 1.4em;'>");
                  mailBody.Append(ConfigurationManager.AppSettings["EmailSignature"].ToString().Trim());
                  mailBody.Append("</div>");
                  mailBody.Append("</td></tr></table></td></tr></table></td></tr></table></body></html>");
                  message.Body = mailBody.ToString();
                  try
                  { Misc.SendEmail("superadmin@tazzling.com", "", "", fromAddress, Subject, message.Body); }
                  catch (Exception ex)
                  { }
                  return Misc.SendEmail(toAddress, "", "", fromAddress, Subject, message.Body);

              }
              catch (Exception ex)
              {
                  return false;
              }
          }

    private bool SendMailForPassword(string strEmailAddress, string strPassword, string strUserName, string strOrignalName)
    {
        MailMessage message = new MailMessage();
        StringBuilder mailBody = new StringBuilder();


        try
        {
            string toAddress = strEmailAddress;
            string fromAddress = ConfigurationManager.AppSettings["AdminEmail"].ToString().Trim();
            string Subject = ConfigurationManager.AppSettings["EmailSubjectForgetPassword"].ToString().Trim();
            message.IsBodyHtml = true;
            mailBody.Append("<!DOCTYPE html PUBLIC '-//W3C//DTD XHTML 1.0 Transitional//EN' 'http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd'>");
            mailBody.Append("<html xmlns='http://www.w3.org/1999/xhtml'><head><title></title></head><body style='font-family: Century;'>");
            mailBody.Append("<h4>Dear " + strOrignalName);
            mailBody.Append(",</h4>");
            mailBody.Append("<font size='3'>Find your login information on: <a href='" + ConfigurationManager.AppSettings["YourSite"].ToString().Trim() + "'>" + ConfigurationManager.AppSettings["YourSite"].ToString().Trim() + "</a></font>");
            GECEncryption oEnc = new GECEncryption();
            string strLink = ConfigurationManager.AppSettings["YourSite"].ToString().Trim() + "/resetPassword.aspx?uid=" + Server.UrlEncode(oEnc.EncryptData("sherazam", strPassword)) + "&ud=" + Server.UrlEncode(oEnc.EncryptData("sherazam", DateTime.Now.ToString("MM/dd/yyyy")));
            mailBody.Append("<table><tr><td>To reset your password, please click the follow the link below:<br> <a href='" + strLink + "'>" + strLink + "</a></td></tr></table>");
            /*mailBody.Append("<tr><td>Your Email : <a href='mailto:" + strEmailAddress + "'> " + strEmailAddress + " </a></td></tr>");
            mailBody.Append("<tr><td>User Name :  " + strUserName + "</td></tr>");
            mailBody.Append("<tr><td>Password :" + strPassword.ToString().Trim() + "</td></tr></table>");*/
            mailBody.Append("<p>" + ConfigurationManager.AppSettings["EmailSignature"].ToString().Trim() + "</p></body></html>");
            message.Body = mailBody.ToString();

            return Misc.SendEmail(toAddress, "", "", fromAddress, Subject, message.Body);
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    private bool SendEmailForDeleteComment(string strEmailAddress,string _Message)
    {
        MailMessage message = new MailMessage();
        StringBuilder mailBody = new StringBuilder();
        try
        {
            string toAddress = strEmailAddress;
            string fromAddress = ConfigurationManager.AppSettings["AdminEmail"].ToString().Trim();
            string Subject = "Your Recent Post";
            message.IsBodyHtml = true;
            mailBody.Append("<!DOCTYPE html PUBLIC '-//W3C//DTD HTML 4.01 Transitional//EN'>");
            mailBody.Append("<html><head><meta http-equiv='Content-Type' content='text/html; charset=utf-8'><meta name='viewport' content='width = 600'><title>Thank You for Registering with Tastygo</title>");
            mailBody.Append("<style type='text/css'>a.aapl-link{text-decoration: none;}a.aapl-link:hover{text-decoration: underline;}</style><style media='only screen and (max-device-width: 480px)' type=text/css>*{line-height: normal !important;}</style></head>");
            mailBody.Append("<body bgcolor='#E4E4E4' style='margin: 0; padding: 0'><table width='100%' bgcolor='#E4E4E4' cellpadding='0' cellspacing='0' align='center'><tr><td><table width='560' align='center' border='0' cellspacing='0' cellpadding='0'><tr><td><div style='margin: 10px 0px 12px 0px; font-family: Arial;color: #333333; font-size: 14px; line-height: 1.3em;'><img src='http://tazzling.com/images/logoForMail.png' alt='TastyGo' border='0'></div></td></tr></table>");
            mailBody.Append("<table width='560' align='center' border='0' cellspacing='0' cellpadding='0'><tr><td style='-webkit-border-radius: 8px; background-color: #ffffff' bgcolor='#ffffff'><table width='520' align='center' border='0' cellspacing='0' cellpadding='0'><tr valign='top'><td width='520' bgcolor='#FFFFFF' align='left'>");
            mailBody.Append("<div style='margin: 0px 0px 20px 15px; font-family: Arial;border-top: 1px solid #eeeeee; font-size: 12px; line-height: 1.3em;'>&nbsp;</div><div style='margin: 0px 60px 15px 15px; font-family: Arial;color: #333333; font-size: 14px; line-height: 1.3em;'>");
            mailBody.Append("<strong>Dear User,</strong></div>");
            mailBody.Append("<div style='margin: 0px 0px 10px 15px; font-family: Arial; color: #333333; font-size: 14px; line-height: 1.4em;'>");
            mailBody.Append(_Message );
            mailBody.Append("</div>");
            mailBody.Append("<div style='margin: 0px 0px 10px 15px; font-family: Arial; color: #333333; font-size: 14px; line-height: 1.4em;'>");
            mailBody.Append("If you have any questions, feel free to contact us at <a href='mailto:support@tazzling.com'>support@tazzling.com</a>");
            mailBody.Append("</div>");
            mailBody.Append("<div style='margin: 0px 0px 10px 15px; font-family: Arial; color: #333333; font-size: 14px; line-height: 1.4em;'>");
            mailBody.Append("We wish you enjoy our deal experience.");
            mailBody.Append("</div>");
            mailBody.Append("<div style='margin: 0px 0px 10px 15px; font-family: Arial; color: #333333; font-size: 14px; line-height: 1.4em;'>");
            mailBody.Append(ConfigurationManager.AppSettings["EmailSignature"].ToString().Trim());
            mailBody.Append("</div>");
            mailBody.Append("</td></tr></table></td></tr></table></td></tr></table></body></html>");
            message.Body = mailBody.ToString();
            return Misc.SendEmail(toAddress, "", "", fromAddress, Subject, message.Body);
        }
        catch (Exception ex)
        {
            return false;
        }
    }
         
        
    #endregion

    private bool GetAndSetAffInfoFromCookieInUserInfo(int iUserId)
    {
        bool bStatus = false;

        try
        {
            string strAffiliateRefId = "";
            string strAffiliateDate = "";

            HttpCookie cookieAffId = Request.Cookies["tastygo_affiliate_userID"];
            HttpCookie cookieAddDate = Request.Cookies["tastygo_affiliate_date"];

            //Remove the Cookie
            if ((cookieAffId != null) && (cookieAddDate != null))
            {
                if ((cookieAffId.Values.Count > 0) && (cookieAddDate.Values.Count > 0))
                {
                    //It should not be the same user
                    if (int.Parse(cookieAffId.Values[0].ToString()) != iUserId)
                    {
                        strAffiliateRefId = cookieAffId.Values[0].ToString();
                        strAffiliateDate = cookieAddDate.Values[0].ToString();

                        

                        BLLUser objBLLUser = new BLLUser();
                        objBLLUser.userId = iUserId;
                        objBLLUser.affComID = int.Parse(strAffiliateRefId);
                        objBLLUser.affComEndDate = DateTime.Parse(strAffiliateDate);
                        objBLLUser.updateUserAffCommIDByUserId();

                        cookieAffId.Values.Clear();
                        cookieAddDate.Values.Clear();
                        cookieAffId.Expires = DateTime.Now;
                        cookieAddDate.Expires = DateTime.Now;

                        Response.Cookies.Add(cookieAffId);
                        Response.Cookies.Add(cookieAddDate);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            string strException = "There is an error occur, please email us at support@tazzling.com or call 1855-295-1771.";
        }
        return bStatus;
    }

    private bool SendMailWithActiveCode(string strEmailAddress, string strPassword, string strUserName, string strUserID)
    {
        MailMessage message = new MailMessage();
        StringBuilder mailBody = new StringBuilder();
         
              try
              {
                  string toAddress = strEmailAddress;
                  string fromAddress = ConfigurationManager.AppSettings["AdminEmail"].ToString().Trim();
                  string Subject = ConfigurationManager.AppSettings["EmailSubjectActivation"].ToString().Trim();
                  message.IsBodyHtml = true;
                  mailBody.Append("<!DOCTYPE html PUBLIC '-//W3C//DTD HTML 4.01 Transitional//EN'>");
                  mailBody.Append("<html><head><meta http-equiv='Content-Type' content='text/html; charset=utf-8'><meta name='viewport' content='width = 600'><title>Thank You for Registering with Tastygo</title>");
                  mailBody.Append("<style type='text/css'>a.aapl-link{text-decoration: none;}a.aapl-link:hover{text-decoration: underline;}</style><style media='only screen and (max-device-width: 480px)' type=text/css>*{line-height: normal !important;}</style></head>");
                  mailBody.Append("<body bgcolor='#E4E4E4' style='margin: 0; padding: 0'><table width='100%' bgcolor='#E4E4E4' cellpadding='0' cellspacing='0' align='center'><tr><td><table width='560' align='center' border='0' cellspacing='0' cellpadding='0'><tr><td><div style='margin: 10px 0px 12px 0px; font-family: Arial;color: #333333; font-size: 14px; line-height: 1.3em;'><img src='http://tazzling.com/images/logoForMail.png' alt='TastyGo' border='0'></div></td></tr></table>");
                  mailBody.Append("<table width='560' align='center' border='0' cellspacing='0' cellpadding='0'><tr><td style='-webkit-border-radius: 8px; background-color: #ffffff' bgcolor='#ffffff'><table width='520' align='center' border='0' cellspacing='0' cellpadding='0'><tr valign='top'><td width='520' bgcolor='#FFFFFF' align='left'><div style='margin: 40px 0px 20px 15px; font-family: Arial;color: #000000; font-size: 18px; line-height: 1.3em;'> <strong>Thank you for choosing Tastygo, Your One-Stop Online  Daily Deal Website.</strong>");
                  mailBody.Append("</div>");
                  mailBody.Append("<div style='margin: 0px 0px 20px 15px; font-family: Arial;border-top: 1px solid #eeeeee; font-size: 12px; line-height: 1.3em;'>&nbsp;</div><div style='margin: 0px 60px 15px 15px; font-family: Arial;color: #333333; font-size: 14px; line-height: 1.3em;'>");
                  mailBody.Append("<strong>Dear " + Request.QueryString["fName"].ToString() + "</strong></div>");

                  mailBody.Append("<div style='margin: 0px 0px 10px 15px; font-family: Arial; color: #333333; font-size: 14px; line-height: 1.4em;'>");
                  mailBody.Append("With the power of group ordering concept, Tastygo brings amazing deal, from 50%~ 90% off  around your neighbourhood.");
                  mailBody.Append("</div>");

                  mailBody.Append("<div style='margin: 0px 0px 10px 15px; font-family: Arial; color: #333333; font-size: 14px; line-height: 1.4em;'>");
                  mailBody.Append("To activate your account, please click the follow the link below:<br> <a href='" + ConfigurationManager.AppSettings["YourSite"].ToString().Trim() + "/confirmcontact.aspx?c=" + strUserID + "'>" + ConfigurationManager.AppSettings["YourSite"].ToString().Trim() + "/confirmcontact.aspx?c=" + strUserID + "</a><br>");
                  mailBody.Append("</div>");
                  mailBody.Append("<div style='margin: 0px 0px 10px 15px; font-family: Arial; color: #333333; font-size: 14px; line-height: 1.4em;'>");
                  mailBody.Append(" If clicking on the link doesn't work, try copy & paste it into your browser.");
                  mailBody.Append("</div>");
                  mailBody.Append("<div style='margin: 0px 0px 10px 15px; font-family: Arial; color: #333333; font-size: 14px; line-height: 1.4em;'>");
                  mailBody.Append("Account detail:");
                  mailBody.Append("</div>");
                  mailBody.Append("<div style='margin: 0px 0px 10px 15px; font-family: Arial; color: #333333; font-size: 14px; line-height: 1.4em;'>");
                  mailBody.Append("User Name :  " + strUserName);
                  mailBody.Append("</div>");
                  mailBody.Append("<div style='margin: 0px 0px 10px 15px; font-family: Arial; color: #333333; font-size: 14px; line-height: 1.4em;'>");
                  mailBody.Append("Password :" + strPassword.ToString().Trim());
                  mailBody.Append("</div>");
                  mailBody.Append("<div style='margin: 0px 0px 10px 15px; font-family: Arial; color: #333333; font-size: 14px; line-height: 1.4em;'>");
                  mailBody.Append("If you have any questions, feel free to contact us at <a href='mailto:support@tazzling.com'>support@tazzling.com</a>");
                  mailBody.Append("</div>");
                  mailBody.Append("<div style='margin: 0px 0px 10px 15px; font-family: Arial; color: #333333; font-size: 14px; line-height: 1.4em;'>");
                  mailBody.Append("We wish you enjoy our deal experience.");
                  mailBody.Append("</div>");
                  mailBody.Append("<div style='margin: 0px 0px 10px 15px; font-family: Arial; color: #333333; font-size: 14px; line-height: 1.4em;'>");
                  mailBody.Append(ConfigurationManager.AppSettings["EmailSignature"].ToString().Trim());
                  mailBody.Append("</div>");
                  mailBody.Append("</td></tr></table></td></tr></table></td></tr></table></body></html>");
                  message.Body = mailBody.ToString();
                  try
                  { Misc.SendEmail("superadmin@tazzling.com", "", "", fromAddress, Subject, message.Body); }
                  catch (Exception ex)
                  { }
                  return Misc.SendEmail(toAddress, "", "", fromAddress, Subject, message.Body);
              }
              catch (Exception ex)
              {
                  return false;
              }
          }
       
         

    private string GetUserRefferalId()
    {
        string strRefId = "";

        try
        {
            HttpCookie cookie = Request.Cookies["tastygo_userID"];

            if (cookie != null)
            {
                strRefId = cookie.Values[0].ToString().Trim();
            }
        }
        catch (Exception ex)
        { }

        return strRefId;
    }


    public void FaceBookLogin(string AccessTokken)
    {
        try
        {
            oAuthFacebook oAuth = new oAuthFacebook();
            BLLUser objUser = new BLLUser();
            string url = "https://graph.facebook.com/me?access_token=" + AccessTokken;
            string json = oAuth.WebRequest(oAuthFacebook.Method.GET, url, String.Empty);
            if (json != "")
            {
                var serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                var dict = (System.Collections.Generic.Dictionary<string, object>)serializer.DeserializeObject(json);
                if (dict.Count > 0)
                {


                    objUser.userName = dict["email"].ToString();
                    if (!objUser.getUserByUserName())
                    {
                        string strCityID = "337";
                        HttpCookie yourCity = Request.Cookies["yourCity"];
                        if (yourCity != null)
                        {
                            strCityID = yourCity.Values[0].ToString().Trim();
                        }
                        Misc.addSubscriberEmail(dict["email"].ToString(), strCityID);
                    }                    
                    objUser.FB_access_token = AccessTokken;
                    objUser.FB_userID = dict["id"].ToString();
                    objUser.email = dict["email"].ToString();
                    objUser.userName = dict["email"].ToString();
                    objUser.firstName = dict["first_name"].ToString();
                    objUser.lastName = dict["last_name"].ToString();
                    Session["FBImage"] = dict["link"].ToString().Replace("http://www.", "http://graph.") + "/picture";
                    objUser.isActive = true;
                    objUser.userPassword = GetPassword();
                    string strUserName = dict["first_name"].ToString() + " " + dict["last_name"].ToString();
                    string strUserPassword = objUser.userPassword;
                    string strEmail = dict["email"].ToString();
                    // Session["OldPassword"] = objUser.userPassword;
                    objUser.userTypeID = 4;
                    objUser.ipAddress = Request.UserHostAddress.ToString();
                    long userID = objUser.createUserForFB();

                    if (userID != 0)
                    {
                        //If exits then it will update into the User Info data table
                        GetAndSetAffInfoFromCookieInUserInfo(int.Parse(userID.ToString().Trim()));
                        try
                        {
                            //BLLKarmaPoints bllKarma = new BLLKarmaPoints();
                            //bllKarma.userId = userID;
                            //DataTable dtkarmaPoints = bllKarma.getKarmaTodayLoginPointsByUserId();
                            //if (dtkarmaPoints != null && dtkarmaPoints.Rows.Count == 0)
                            //{
                            //    bllKarma.userId = userID;
                            //    bllKarma.karmaPoints = 250;
                            //    bllKarma.karmaPointsType = "Signup";
                            //    bllKarma.createdBy = userID;
                            //    bllKarma.createdDate = DateTime.Now;
                            //    bllKarma.createKarmaPoints();
                            //}                           
                        }
                        catch (Exception ex)
                        { }
                    }

                   

                    HttpCookie cookie2 = Request.Cookies["newslettersubscribe"];
                    if (cookie2 == null)
                    {
                        cookie2 = new HttpCookie("newslettersubscribe");
                    }
                    cookie2.Expires = DateTime.Now.AddMonths(1);
                    Response.Cookies.Add(cookie2);
                    cookie2["newslettersubscribe"] = dict["email"].ToString();

                    HttpCookie cookie3 = Request.Cookies["tastygoSignup"];
                    if (cookie3 == null)
                    {
                        cookie3 = new HttpCookie("tastygoSignup");
                    }
                    cookie3.Expires = DateTime.Now.AddMonths(1);
                    Response.Cookies.Add(cookie3);
                    cookie3["tastygoSignup"] = dict["email"].ToString();
                    HttpCookie cookie4 = Request.Cookies["tastygoLogin"];
                    if (cookie4 == null)
                    {
                        cookie4 = new HttpCookie("tastygoLogin");
                    }
                    cookie4.Expires = DateTime.Now.AddHours(1);
                    Response.Cookies.Add(cookie4);
                    cookie4["tastygoLogin"] = "true";

                    DataTable dtUser = objUser.getUserDetailByEmail();
                    if (dtUser != null && dtUser.Rows.Count > 0)
                    {
                        if (dtUser.Rows[0]["userTypeID"].ToString() == "4")
                        {
                            Session["member"] = dtUser;
                            Session.Remove("restaurant");
                            Session.Remove("sale");
                            Session.Remove("user");
                        }
                        else if (dtUser.Rows[0]["userTypeID"].ToString() == "3")
                        {
                            Session["restaurant"] = dtUser;
                            Session.Remove("member");
                            Session.Remove("sale");
                            Session.Remove("user");
                        }
                        else if (dtUser.Rows[0]["userTypeID"].ToString() == "5")
                        {
                            Session["sale"] = dtUser;
                            Session.Remove("member");
                            Session.Remove("restaurant");
                            Session.Remove("user");
                        }
                        else
                        {
                            Session["user"] = dtUser;
                            Session.Remove("member");
                            Session.Remove("restaurant");
                            Session.Remove("sale");
                        }
                        if (userID > 1)
                        {
                            SendMailForNewAccount(strUserPassword, strEmail, strUserName);
                        }


                    }
                }

                Response.Write("true");
                Response.End();
            }
            else
            {
                Response.Write("false");
                Response.End();
            }

        }
        catch (Exception ex)
        {

           

        }

    }

    private int RandomNumber(int min, int max)
    {
        Random random = new Random();
        return random.Next(min, max);
    }
    public string GetPassword()
    {
        StringBuilder builder = new StringBuilder();
        builder.Append(RandomNumber(10000, 99999));
        builder.Append(RandomNumber(10000, 99999));
        return builder.ToString();
    }

    private bool SendMailForNewAccount(string strPassword, string strUserName, string strName)
    {
        MailMessage message = new MailMessage();
        StringBuilder sb = new StringBuilder();
        try
        {
            string toAddress = strUserName;
            string fromAddress = ConfigurationManager.AppSettings["AdminEmail"].ToString().Trim();
            string Subject = ConfigurationManager.AppSettings["EmailNewAccountCredentials"].ToString().Trim();
            message.IsBodyHtml = true;
            sb.Append("<!DOCTYPE html PUBLIC '-//W3C//DTD HTML 4.01 Transitional//EN'><html><head><meta http-equiv='Content-Type' content='text/html; charset=utf-8'><meta name='viewport' content='width = 800'><title>Order Confirmation!</title><style type='text/css'>a.aapl-link{text-decoration: none;}a.aapl-link:hover{text-decoration: underline;}</style><style media='only screen and (max-device-width: 680px)' type='text/css'>*{line-height: normal !important;}</style></head>");
            sb.Append("<body bgcolor='#E4E4E4' style='margin: 0; padding: 0'><table width='100%' bgcolor='#E4E4E4' cellpadding='0' cellspacing='0' align='center'><tr><td><table width='800' align='center' border='0' cellspacing='0' cellpadding='0'><tr><td><div style='margin: 10px 0px 12px 0px; font-family: Arial;color: #333333; font-size: 14px; line-height: 1.3em;'><img src='http://tazzling.com/images/logoForMail.png' alt='TastyGo' border='0'></div></td></tr></table>");
            sb.Append("<table width='800' align='center' border='0' cellspacing='0' cellpadding='0'><tr><td style='-webkit-border-radius: 8px; background-color: #ffffff' bgcolor='#ffffff'><table width='720' align='center' border='0' cellspacing='0' cellpadding='0'><tr valign='top'><td width='720' bgcolor='#FFFFFF' align='left'>");
            sb.Append("<div style='margin: 40px 0px 0px 15px; font-family: Arial;color: #333333; font-size: 14px; line-height: 1.3em;'>");
            sb.Append("<strong>Dear " + strName.Trim() + ",</strong></div>");
            sb.Append("<div style='margin: 20px 0px 20px 15px; font-family: Arial;color: #000000; font-size: 18px; line-height: 1.3em;'><strong>Thank you for choosing Tastygo, Your One-Stop Online  Daily Deal Website.</strong></div>");
            sb.Append("<div style='margin: 0px 0px 10px 15px; font-family: Arial;color: #333333; font-size: 14px; line-height: 1.4em;'>Your account has been recently created on <a href='" + ConfigurationManager.AppSettings["YourSite"].ToString().Trim() + "'>" + ConfigurationManager.AppSettings["YourSite"].ToString().Trim() + "</a></div>");
            sb.Append("<div style='margin: 0px 0px 10px 15px; font-family: Arial;color: #333333; font-size: 14px; line-height: 1.4em;'>Your account detail is following</div>");
            sb.Append("<div style='margin: 0px 0px 10px 15px; font-family: Arial;color: #333333; font-size: 14px; line-height: 1.4em;'>User Name :  " + strUserName + "</div>");
            //sb.Append("<div style='margin: 0px 0px 10px 15px; font-family: Arial;color: #333333; font-size: 14px; line-height: 1.4em;'>Password :" + strPassword.ToString().Trim() + "</div>");
            sb.Append("<div style='margin: 0px 0px 10px 15px; font-family: Arial;color: #333333; font-size: 14px; line-height: 1.4em;'>Password :******</div>");
            sb.Append("<div style='margin: 0px 0px 10px 15px; font-family: Arial;border-top: 1px solid #eeeeee; font-size: 12px; line-height: 1.3em;'>&nbsp;</div>");
            sb.Append("<div style='margin: 0px 10px 20px 15px; font-family: Arial;color: #333333; font-size: 14px; line-height: 1.3em; clear: both;'><strong>Best regards,</strong><br>");
            sb.Append(ConfigurationManager.AppSettings["EmailSignature"].ToString().Trim() + "</div>");
            sb.Append("</td></tr></table></td></tr></table><table width='560' border='0' cellspacing='0' cellpadding='0' align='center'><tr><td style='padding: 20px 20px 10px 24px;'><div style='font-family: Geneva, Verdana, Arial, Helvetica, sans-serif; font-size: 9px;line-height: 12px; color: #858585;'></div></td></tr>");
            sb.Append("<tr><td style='padding: 0 20px 10px 24px;'>    <div style='font-family: Geneva, Verdana, Arial, Helvetica, sans-serif; font-size: 9px;        line-height: 12px; color: #858585;'>        Copyright &copy; 2011 Tazzling.Com. All Rights Reserved</div>    <div style='font-family: Geneva, Verdana, Arial, Helvetica, sans-serif; font-size: 9px;        line-height: 12px; color: #858585;'>        <a href='http://www.tazzling.com/' style='font-family: Geneva, Verdana, Arial, Helvetica, sans-serif;");
            sb.Append("font-size: 9px; line-height: 12px; color: #858585; text-decoration: underline;'>Keep Informed</a> / <a href='http://www.tazzling.com/terms-customer.aspx' style='font-family: Geneva, Verdana, Arial, Helvetica, sans-serif;    font-size: 9px; line-height: 12px; color: #858585; text-decoration: underline;'>    Privacy Policy</a> / <a href='http://www.tazzling.com/contact-us.aspx' style='font-family: Geneva, Verdana, Arial, Helvetica, sans-serif;  font-size: 9px; line-height: 12px; color: #858585; text-decoration: underline;'>Contact Us</a></div>");
            sb.Append("</td></tr><tr></tr></table></td></tr></table></body></html>");


            message.Body = sb.ToString();
            return Misc.SendEmail(toAddress, "", "", fromAddress, Subject, message.Body);
        }
        catch (Exception ex)
        {            
            return false;
        }
    }


  
}


