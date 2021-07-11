using Microsoft.Extensions.Logging;
using SEOTools.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SEOTools.Utility
{
    public class URLValidator
    {
        public List<URLValidationResult> urlValidationResults;

        public URLValidator()
        {
            urlValidationResults = new List<URLValidationResult>();
        }

        public void CheckIfURLExists(Uri uri, string host)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    if (uri.Host == host)
                    {
                        urlValidationResults.Add(new URLValidationResult()
                        {
                            URL = uri.ToString(),
                            ErrorCode = string.Empty,
                            DetailedErrorLog = string.Empty,
                            LinkType = "Root Url",
                            SourceFile = "Root",
                            SourceFileLocation = "Root",
                            SourceLinkText = "Root Url",
                            Status = response.StatusCode.ToString(),
                            StatusDescription = response.StatusDescription
                        });

                        CheckAllUrlForRootDomain(HtmlHelper.FindAllAnchorTags(HtmlHelper.DownloadHTMlContent(response)), host);
                    }
                    else
                    {
                        urlValidationResults.Add(new URLValidationResult()
                        {
                            URL = uri.ToString(),
                            ErrorCode = string.Empty,
                            DetailedErrorLog = string.Empty,
                            LinkType = "Root Url",
                            SourceFile = "Root",
                            SourceFileLocation = "Root",
                            SourceLinkText = "Root Url",
                            Status = response.StatusCode.ToString(),
                            StatusDescription = response.StatusDescription
                        });
                    }
                }
                else
                {
                    urlValidationResults.Add(new URLValidationResult()
                    {
                        URL = uri.ToString(),
                        ErrorCode = string.Empty,
                        DetailedErrorLog = string.Empty,
                        LinkType = "Root Url",
                        SourceFile = "Root",
                        SourceFileLocation = "Root",
                        SourceLinkText = "Root Url",
                        Status = response.StatusCode.ToString(),
                        StatusDescription = response.StatusDescription
                    });
                }
            }
        }

        public void PerformURLValidation(string url)
        {
            try
            {
                Uri uriResult;
                if (GenerateUri(url, out uriResult))
                {
                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uriResult);

                    using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                    {
                        urlValidationResults.Add(new URLValidationResult()
                        {
                            URL = url,
                            ErrorCode = string.Empty,
                            DetailedErrorLog = string.Empty,
                            LinkType = "Root Url",
                            SourceFile = "Root",
                            SourceFileLocation = "Root",
                            SourceLinkText = "Root Url",
                            Status = response.StatusCode.ToString(),
                            StatusDescription = response.StatusDescription
                        });

                        CheckAllUrlForRootDomain(HtmlHelper.FindAllAnchorTags(HtmlHelper.DownloadHTMlContent(response)), uriResult.Host);
                    }
                }
                else
                {
                    urlValidationResults.Add(new URLValidationResult()
                    {
                        URL = url,
                        ErrorCode = string.Empty,
                        DetailedErrorLog = string.Empty,
                        LinkType = "Root Url",
                        SourceFile = "Root",
                        SourceFileLocation = "Root",
                        SourceLinkText = "Root Url",
                        Status = "Ïnvalid Url",
                    });
                }

            }
            catch (Exception ex)
            {

            }
        }

        private static bool GenerateUri(string url, out Uri uriResult)
        {
            return Uri.TryCreate(url, UriKind.Absolute, out uriResult);
        }

        private void CheckAllUrlForRootDomain(List<string> list, string host)
        {
            foreach (var anchorTag in list)
            {
                Uri uriResult;
                var hrefLink = XElement.Parse(anchorTag).DescendantsAndSelf().Select(x => x.Attribute("href").Value).FirstOrDefault();
                if (urlValidationResults.Where(x => x.URL.Equals(hrefLink, StringComparison.OrdinalIgnoreCase)).Count() == 0)
                {
                    if (GenerateUri(hrefLink, out uriResult))
                        CheckIfURLExists(uriResult, host);
                }
            }
        }
    }
}