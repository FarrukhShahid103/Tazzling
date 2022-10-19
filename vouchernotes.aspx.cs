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

public partial class vouchernotes : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Title = ConfigurationManager.AppSettings["pageTitleStart"].ToString().Trim() + " | Notes";
        if (!IsPostBack)
        {
            if (Session["restaurant"] != null)
            {
                if (Request.QueryString["did"] != null
                    && Request.QueryString["did"].ToString().Trim() != "")
                {
                    BLLDealOrderDetail objDetail = new BLLDealOrderDetail();
                    try
                    {
                        objDetail.detailID = Convert.ToInt64(Request.QueryString["did"].ToString().Trim());
                        DataTable dtNote = objDetail.getVoucherNoteByDetailID();
                        if (dtNote != null && dtNote.Rows.Count > 0)
                        {
                            txtSubComment.Text = dtNote.Rows[0]["note"].ToString().Trim();
                        }
                    }
                    catch (Exception ex)
                    { }
                }
            }
            else
            {
                Response.Redirect("default.aspx", false);
            }

        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (Request.QueryString["did"] != null
                      && Request.QueryString["did"].ToString().Trim() != "")
            {
                try
                {
                    BLLDealOrderDetail objDetail = new BLLDealOrderDetail();
                    objDetail.detailID = Convert.ToInt64(Request.QueryString["did"].ToString().Trim());
                    objDetail.note = txtSubComment.Text.Trim();
                    objDetail.updateVoucherNoteByDetailID();
                    Response.Redirect("frmBusDealOrderDetailInfo.aspx");
                }
                catch (Exception ex)
                { }
            }
        }
        catch (Exception ex)
        { }
    }
}
