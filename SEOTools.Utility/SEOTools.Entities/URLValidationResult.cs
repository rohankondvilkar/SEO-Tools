using System;

namespace SEOTools.Entities
{
    public class URLValidationResult
    {
        public string URL { get; set; }

        public string ErrorCode { get; set; }

        public string Status { get; set; }

        public string DetailedErrorLog { get; set; }

        public string SourceLinkText { get; set; }

        public string SourceFile { get; set; }

        public string SourceFileLocation { get; set; }

        public string LinkType { get; set; }

        public string StatusDescription { get; set; }
    }
}
