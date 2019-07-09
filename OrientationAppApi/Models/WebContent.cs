using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrientationAppApi.Models
{
    public class WebContent
    {
        public string ContentId { get; set; }
        public string Title { get; set; }
        public string Section { get; set; }
        public string WebText { get; set; }
        public bool International { get; set; }
        public string Campus { get; set; }
        public Link BannerImage { get; set; }
        public Link OriginalTextLocation { get; set; }
    }
}
