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

public partial class restaurantccinfo : System.Web.UI.Page
{
    BLLCcinfo objCreditCard = new BLLCcinfo();
    BLLTaxRate objTax = new BLLTaxRate();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                LoadDropDownList();
                if (Request.QueryString["uid"] != null && Request.QueryString["uid"].ToString().Trim() != "")
                {
                    DataTable dtUser = null;
                    if (Session["user"] != null)
                    {
                        if (Session["pass"] != null)
                        {
                            pnlAskForPassword.Visible = false;
                            pnlCCinfo.Visible = true;
                            dtUser = (DataTable)Session["user"];
                            ViewState["userId"] = dtUser.Rows[0]["userId"].ToString();
                            objCreditCard.userId = Convert.ToInt32(Request.QueryString["uid"].ToString());
                            DataTable dtccinfo = objCreditCard.getRCcinfoByUserID();
                            if (dtccinfo != null && dtccinfo.Rows.Count > 0)
                            {
                                GECEncryption objdesc = new GECEncryption();
                                lblUserID.Text = dtccinfo.Rows[0]["userName"].ToString();
                                txtCardHolderName.Text = objdesc.DecryptData("colintastygochengusername", dtccinfo.Rows[0]["rcciun"].ToString());
                                txtCardNumber.Text = objdesc.DecryptData("colintastygochengnumber", dtccinfo.Rows[0]["rccin"].ToString());
                                string[] strDate = objdesc.DecryptData("colintastygochengexpirydate", dtccinfo.Rows[0]["rccied"].ToString()).Split('-');
                                if (strDate.Length == 2)
                                {
                                    expDateMonth.SelectedValue = strDate[0].ToString();
                                    expDateYear.SelectedValue = strDate[1].ToString();
                                }
                                if (dtccinfo.Rows[0]["rccit"] != null && dtccinfo.Rows[0]["rccit"].ToString() != "")
                                {
                                    ddcType.SelectedValue = objdesc.DecryptData("colintastygochengtype", dtccinfo.Rows[0]["rccit"].ToString());
                                }
                                cvv2Number.Value = objdesc.DecryptData("colintastygochengccv", dtccinfo.Rows[0]["rcci_ccvn"].ToString());

                                objTax = new BLLTaxRate();
                                objTax.provinceId = Convert.ToInt64(dtccinfo.Rows[0]["provinceId"].ToString());
                                DataTable dtProvinceTax = objTax.getProvinceTaxRateByProvinceID();
                                double taxRate = 0;
                                double dTax = 0;
                                if (dtProvinceTax != null && dtProvinceTax.Rows.Count > 0)
                                {
                                    taxRate = Convert.ToDouble(dtProvinceTax.Rows[0]["taxRates"].ToString());
                                }
                                lblTaxText.Text = "Tax (" + taxRate + "%)";
                                if (dtccinfo.Rows[0]["package"] != null && dtccinfo.Rows[0]["package"].ToString().Trim() != ""
                                    && dtccinfo.Rows[0]["cycle"] != null && dtccinfo.Rows[0]["cycle"].ToString().Trim() != "")
                                {
                                    lblPackage.Text = dtccinfo.Rows[0]["package"].ToString().Trim();
                                    lblDuration.Text = dtccinfo.Rows[0]["cycle"].ToString().Trim();
                                    if (dtccinfo.Rows[0]["package"].ToString().Trim() == "Bronze")
                                    {
                                        if (dtccinfo.Rows[0]["cycle"].ToString().Trim() == "Monthly")
                                        {
                                            lblPackageValue.Text = "$42.95 CAD";
                                            hfPackageValue.Value = "42.95";
                                            dTax = 42.95 / 100 * taxRate;
                                            lblTax.Text = "$" + dTax.ToString("###.00") + " CAD";                                           
                                            lblTotalFee.Text = "$" + Convert.ToDouble(42.95 + dTax).ToString("###.00") + " CAD";                                           
                                            trDiscount.Visible = false;
                                        }
                                        else
                                        {
                                            lblPackageValue.Text = "$515.40 CAD";
                                            hfPackageValue.Value = "515.40";
                                            dTax = 463.86 / 100 * taxRate;
                                            lblTax.Text = "$" + dTax.ToString("###.00") + " CAD";                                            
                                            lblTotalFee.Text = "$" + Convert.ToDouble(463.86 + dTax).ToString("###.00") + " CAD";                                            
                                            lblDiscountText.Text = "Discount (10%)";
                                            lblDiscountValue.Text = "$51.54 CAD";                                            
                                            trDiscount.Visible = true;
                                        }
                                    }
                                    else if (dtccinfo.Rows[0]["package"].ToString().Trim() == "Silver")
                                    {
                                        if (dtccinfo.Rows[0]["cycle"].ToString().Trim() == "Monthly")
                                        {
                                            lblPackageValue.Text = "$52.95 CAD";
                                            hfPackageValue.Value = "52.95";
                                            dTax = 52.95 / 100 * taxRate;
                                            lblTax.Text = "$" + dTax.ToString("###.00") + " CAD";                                           
                                            lblTotalFee.Text = "$" + Convert.ToDouble(52.95 + dTax).ToString("###.00") + " CAD";                                           
                                            trDiscount.Visible = false;
                                        }
                                        else
                                        {
                                            lblPackageValue.Text = "$635.40 CAD";
                                            hfPackageValue.Value = "635.40";
                                            dTax = 540.09 / 100 * taxRate;
                                            lblTax.Text = "$" + dTax.ToString("###.00") + " CAD";                                        
                                            lblTotalFee.Text = "$" + Convert.ToDouble(540.09 + dTax).ToString("###.00") + " CAD";                                        
                                            lblDiscountText.Text = "Discount (15%)";
                                            lblDiscountValue.Text = "$95.31 CAD";                                        
                                            trDiscount.Visible = true;
                                        }
                                    }
                                    else if (dtccinfo.Rows[0]["package"].ToString().Trim() == "Gold")
                                    {
                                        if (dtccinfo.Rows[0]["cycle"].ToString().Trim() == "Monthly")
                                        {
                                            lblPackageValue.Text = "$72.95 CAD";
                                            hfPackageValue.Value = "72.95";
                                            dTax = 72.95 / 100 * taxRate;
                                            lblTax.Text = "$" + dTax.ToString("###.00") + " CAD";
                                            lblTotalFee.Text = "$" + Convert.ToDouble(72.95 + dTax).ToString("###.00") + " CAD";                                            
                                            trDiscount.Visible = false;

                                        }
                                        else
                                        {
                                            lblPackageValue.Text = "$875.40 CAD";
                                            hfPackageValue.Value = "875.40";
                                            dTax = 700.32 / 100 * taxRate;
                                            lblTax.Text = "$" + dTax.ToString("###.00") + " CAD";
                                            lblTotalFee.Text = "$" + Convert.ToDouble(700.32 + dTax).ToString("###.00") + " CAD";
                                            lblDiscountText.Text = "Discount (20%)";
                                            lblDiscountValue.Text = "$175.08 CAD";
                                            trDiscount.Visible = true;
                                        }
                                    }
                                }
                                
                            }
                            else
                            {
                                Response.Redirect("restaurantManagement.aspx?ErrorMsg=2", true);
                            }
                        }
                        else
                        {
                            pnlAskForPassword.Visible = true;
                            pnlCCinfo.Visible = false;
                        }
                    }
                }
                else
                {
                    Response.Redirect("restaurantManagement.aspx", true);
                }
            }
        }
        catch (Exception ex)
        {
            lblMessage.Text = "There is an error occur, please email us at support@tazzling.com or call 1855-295-1771.";
            lblMessage.Visible = true;
            imgGridMessage.Visible = true;
            imgGridMessage.ImageUrl = "~/admin/images/error.png"; lblMessage.ForeColor = System.Drawing.Color.Red;
        }
    }
   
    private void LoadDropDownList()
    {
        try
        {
            //Clears the Drop Down List
            this.expDateYear.Items.Clear();

            //Year
            for (int year = DateTime.Now.Year; year <= DateTime.Now.Year + 7; year++)
            {
                expDateYear.Items.Add(new ListItem(year.ToString(), year.ToString()));
            }

            this.expDateYear.SelectedValue = DateTime.Now.Year.ToString();

        }
        catch (Exception ex)
        {
            lblMessage.Text = "There is an error occur, please email us at support@tazzling.com or call 1855-295-1771.";
            lblMessage.Visible = true;
            imgGridMessage.Visible = true;
            imgGridMessage.ImageUrl = "~/admin/images/error.png"; lblMessage.ForeColor = System.Drawing.Color.Red;
        }
    }
  
    protected void btnUpdate_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            GECEncryption objEnc = new GECEncryption();
            objCreditCard = new BLLCcinfo();
            objCreditCard.modifiedBy = Convert.ToInt32(ViewState["userId"].ToString());
            objCreditCard.rcci_ccvn = objEnc.EncryptData("colintastygochengccv", cvv2Number.Value.Trim());
            objCreditCard.rccin = objEnc.EncryptData("colintastygochengnumber", txtCardNumber.Text.Trim());
            objCreditCard.rccied = objEnc.EncryptData("colintastygochengexpirydate", expDateMonth.SelectedValue.ToString() + "-" + expDateYear.SelectedValue.ToString());
            objCreditCard.rccit = objEnc.EncryptData("colintastygochengtype", ddcType.SelectedValue.ToString());
            objCreditCard.rcciun = objEnc.EncryptData("colintastygochengusername", txtCardHolderName.Text.Trim());
            objCreditCard.package = lblPackage.Text.Trim();
            objCreditCard.cycle = lblDuration.Text.Trim();
            objCreditCard.fee = hfPackageValue.Value.Trim();
            objCreditCard.userId = Convert.ToInt32(Request.QueryString["uid"].ToString());
            objCreditCard.updateRCcInfo();
            Response.Redirect("restaurantManagement.aspx?ErrorMsg=1", true);
        }
        catch (Exception ex)
        {
            lblMessage.Text = "There is an error occur, please email us at support@tazzling.com or call 1855-295-1771.";
            lblMessage.Visible = true;
            imgGridMessage.Visible = true;
            imgGridMessage.ImageUrl = "~/admin/images/error.png"; lblMessage.ForeColor = System.Drawing.Color.Red;        
        }
    }
   
    
    protected void CancelButton_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            Response.Redirect("restaurantManagement.aspx", true);
        }
        catch (Exception ex)
        {
            lblMessage.Text = "There is an error occur, please email us at support@tazzling.com or call 1855-295-1771.";
            lblMessage.Visible = true;
            imgGridMessage.Visible = true;
            imgGridMessage.ImageUrl = "~/admin/images/error.png"; lblMessage.ForeColor = System.Drawing.Color.Red;
        }
    }

    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            if (txtpassword.Text.Trim() == "superadmin123456")
            {
                Session["pass"] = "true";
                Response.Redirect("restaurantccinfo.aspx?uid=" + Request.QueryString["uid"].ToString(), true);
            }
        }
        catch (Exception ex)
        {
 
        }
    }
}
