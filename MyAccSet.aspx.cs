using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MyAccSet : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
           // Response.Write("abc");
            lblTest.Text = "None";
        }
    }

    protected void btnTest_Click(object sender, EventArgs e)
    {
        testing();
    }

    public void testing()
    {
        //  lblTest.Text = "none";
      //  lblTest.Text = DateTime.Now.ToLongTimeString();
    }

}