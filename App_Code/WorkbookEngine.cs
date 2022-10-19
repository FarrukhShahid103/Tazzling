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
using System.Text;
using System.IO;
using System.Net;
using iTextSharp.text.pdf;
using iTextSharp.text;
using iTextSharp.text.html;
using iTextSharp.text.html.simpleparser;
using System.Threading;
using System.Text.RegularExpressions;
using System.Xml;
using SQLHelper;
using System.Xml.Xsl;
using System.Web.UI.WebControls.WebParts;
/// <summary>
/// Summary description for WorkbookEngine
/// </summary>
public class WorkbookEngine
{

    public static void CreateWorkbook(DataSet ds, String path)
    {
        XmlDataDocument xmlDataDoc = new XmlDataDocument(ds);
        XslTransform xt = new XslTransform();
        StreamReader reader = new StreamReader(typeof(WorkbookEngine).Assembly.GetManifestResourceStream(typeof(WorkbookEngine), "Excel.xsl"));
        XmlTextReader xRdr = new XmlTextReader(reader);
        xt.Load(xRdr, null, null);
        StringWriter sw = new StringWriter();
        xt.Transform(xmlDataDoc, null, sw, null);
        StreamWriter myWriter = new StreamWriter(path + "\\download.xls");
        myWriter.Write(sw.ToString());
        myWriter.Close();
    }

}
