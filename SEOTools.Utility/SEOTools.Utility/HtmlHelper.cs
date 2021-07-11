using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SEOTools.Utility
{
    public static class HtmlHelper
    {
        public static string DownloadHTMlContent(HttpWebResponse response)
        {
            string html = string.Empty;
            Stream recieveStream = response.GetResponseStream();
            StreamReader streamReader = null;
            if (String.IsNullOrWhiteSpace(response.CharacterSet))
            {
                streamReader = new StreamReader(recieveStream);
            }
            else
            {
                streamReader = new StreamReader(recieveStream, Encoding.GetEncoding(response.CharacterSet));
            }

            return streamReader.ReadToEnd();
        }

        public static List<string> FindAllAnchorTags(string html)
        {
            List<string> anchorTagCollection = new List<string>();
            var matches = Regex.Matches(html, @"<a\shref=""(?<url>.*?)"">(?<text>.*?)</a>");
            foreach (var match in matches)
            {
                anchorTagCollection.Add(match.ToString());
            }

            return anchorTagCollection;
        }
    }
}
