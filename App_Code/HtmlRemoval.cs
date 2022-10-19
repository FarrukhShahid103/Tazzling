using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Linq;

using System.Collections.Generic;
using System.Xml.Serialization;
using GecLibrary;
using System.IO;
using System.Text;
using System.Net;
using System.Configuration;
using System.Xml;
using System.Text.RegularExpressions;

/// <summary>
/// Summary description for HtmlRemoval
/// </summary>
public static class HtmlRemoval
{
    /// <summary>
    /// Remove HTML from string with Regex.
    /// </summary>
    public static string StripTagsRegex(string source)
    {
        return Regex.Replace(source, "<.*?>", string.Empty);
    }

    /// <summary>
    /// Compiled regular expression for performance.
    /// </summary>
    static Regex _htmlRegex = new Regex("<.*?>", RegexOptions.Compiled);

    /// <summary>
    /// Remove HTML from string with compiled Regex.
    /// </summary>
    public static string StripTagsRegexCompiled(string source)
    {
        return _htmlRegex.Replace(source, string.Empty);
    }

    /// <summary>
    /// Remove HTML tags from string using char array.
    /// </summary>
    public static string StripTagsCharArray(string source)
    {
        char[] array = new char[source.Length];
        int arrayIndex = 0;
        bool inside = false;

        for (int i = 0; i < source.Length; i++)
        {
            char let = source[i];
            if (let == '<')
            {
                inside = true;
                continue;
            }
            if (let == '>')
            {
                inside = false;
                continue;
            }
            if (!inside)
            {
                array[arrayIndex] = let;
                arrayIndex++;
            }
        }
        return new string(array, 0, arrayIndex);
    }
}
