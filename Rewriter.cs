using System;
using System.Web;
using System.Xml;
using System.Xml.XPath;
using System.Configuration;
using System.Collections.Specialized;
using System.Text.RegularExpressions;
using System.Xml.Xsl;
using System.Reflection;
using System.Runtime.CompilerServices;

[assembly: AssemblyDelaySign(false)]
[assembly: AssemblyKeyFile(@"keyfile.snk")]
[assembly: AssemblyKeyName("")]
[assembly: AssemblyVersion("1.0.783.30976")]


namespace ThunderMain.URLRewriter {

	public class Rewriter : IConfigurationSectionHandler {
		protected XmlNode _oRules=null;

		protected Rewriter(){}

		public string GetSubstitution(string zPath) {
			Regex oReg;

			foreach(XmlNode oNode in _oRules.SelectNodes("rule")) {
				oReg=new Regex(oNode.SelectSingleNode("url/text()").Value);
				Match oMatch=oReg.Match(zPath);

				if(oMatch.Success) {
					return oReg.Replace(zPath,oNode.SelectSingleNode("rewrite/text()").Value);
				}
			}

			return zPath;
		}

		public static void Process() {
			Rewriter oRewriter=(Rewriter)ConfigurationSettings.GetConfig("system.web/urlrewrites");


			string zSubst=oRewriter.GetSubstitution(HttpContext.Current.Request.Path);


			if(zSubst.Length>0) {
				HttpContext.Current.RewritePath(zSubst);
			}
		}

		#region Implementation of IConfigurationSectionHandler
		public object Create(object parent, object configContext, XmlNode section) {			
			_oRules=section;

			// TODO: Compile all Regular Expressions

			return this;
		}
		#endregion
	}
}
