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

        public async Task<WebContent> GetSharePointListsAsync(string username, string password, string domain, string url, string site, string list, string select)
        {
            var byodlists = new WebContent();
            var json = await _WebContentProvider.GetSharePointListsAsync(username, password, domain, url, site, list, select);
            JArray result = (JArray)JObject.Parse(json)["d"]["results"];

            byodlists.ContentId = (string)result["ContentId"];
            byodlists.Title = (string)result["Title"];
            byodlists.Section = (string)result["Section"];
            byodlists.WebText = (string)result["WebText"];
            byodlists.International = (string)result["International"];
            byodlists.Campus = (string)result["Campus"];
            byodlists.BannerImage = (string)result["BannerImage"];
            byodlists.OriginalTextLocation = (string)result["OriginalTextLocation"];
            
            return byodlists;
        }
    }
}
