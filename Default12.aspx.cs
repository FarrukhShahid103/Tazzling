using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Default12 : System.Web.UI.Page
{
    public string sTest;
    protected void Page_Load(object sender, EventArgs e)
    {
        sTest = DateTime.Now.ToLongTimeString();
    }
    
    protected void btnTest_Click(object sender, EventArgs e)
    {
        testing();
    }

    public void testing()
    {
        //  lblTest.Text = "none";
          lblTest.Text = DateTime.Now.ToLongTimeString();
    }

}