<%@ Application Language="C#" %>

<script runat="server">

    void Application_Start(object sender, EventArgs e) 
    {
        // Code that runs on application startup

    }
    
    void Application_End(object sender, EventArgs e) 
    {
        //  Code that runs on application shutdown

    }
        
    void Application_Error(object sender, EventArgs e) 
    { 
        // Code that runs when an unhandled error occurs

    }

    void Session_Start(object sender, EventArgs e) 
    {
        // Code that runs when a new session is started

    }

    void Session_End(object sender, EventArgs e) 
    {
        // Code that runs when a session ends. 
        // Note: The Session_End event is raised only when the sessionstate mode
        // is set to InProc in the Web.config file. If session mode is set to StateServer 
        // or SQLServer, the event is not raised.

    }
    
    protected void Application_BeginRequest(Object sender, EventArgs e)
    {
        ThunderMain.URLRewriter.Rewriter.Process();

        //this is a workaround for the "POST not allowed" AJAX/asmx/aspx bug
        FixAjax(Context, "asmx");

        FixAjax(Context, "aspx");
    }

    private static void FixAjax(HttpContext context, string extension)
    {
        
        string dotExt = "." + extension;
        if (context.Request.Path.IndexOf(dotExt) != -1 && !context.Request.Path.EndsWith(dotExt))
        {
            int dotasmx = context.Request.Path.IndexOf(dotExt);
            string path = context.Request.Path.Substring(0, dotasmx + dotExt.Length);
            string pathInfo = context.Request.Path.Substring(dotasmx + dotExt.Length);
            context.RewritePath(path, pathInfo, context.Request.Url.Query);
        }
    }
    
</script>
