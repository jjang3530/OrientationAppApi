using Newtonsoft.Json.Linq;
using OrientationAppApi.Models;
using OrientationAppApi.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrientationAppApi.Repositories.Implementations
{
    public class WebContentRepo : IWebContentRepo
    {
        private readonly WebContentProvider _WebContentProvider;

        public WebContentRepo()
        {
            _WebContentProvider = new WebContentProvider();
        }

        public WebContentRepo(WebContentProvider WebContentProvider)
        {
            _WebContentProvider = WebContentProvider;
        }

        public async Task<IEnumerable<WebContent>> GetSharePointListsAsync(string username, string password, string domain, string url, string site, string list, string select)
        {
            var weblists = new List<WebContent>();
            var json = await _WebContentProvider.GetSharePointListsAsync(username, password, domain, url, site, list, select);
            JArray results = (JArray)JObject.Parse(json)["d"]["results"];
            foreach (JObject result in results)
            {
                weblists.Add(new WebContent()
                {
                    ContentId = (string)result["ContentId"],
                    Title = (string)result["Title"],
                    Section = (string)result["Section"],
                    WebText = (string)result["WebText"],
                    International = (string)result["International"],
                    Campus = (string)result["Campus"],
                    BannerImage = (string)result["BannerImage"],
                    OriginalTextLocation = (string)result["OriginalTextLocation"]
            });
            }
            
            return weblists;
        }
    }
}
