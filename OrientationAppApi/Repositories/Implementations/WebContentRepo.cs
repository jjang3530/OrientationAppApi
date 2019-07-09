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

        public async Task<IList<WebContent>> GetSharePointListsAsync(string username, string password, string domain, string url, string site, string list, string contentId)
        {
            var weblists = new List<WebContent>();
            var json = await _WebContentProvider.GetSharePointListsAsync(username, password, domain, url, site, list, contentId);

            JArray results = (JArray)JObject.Parse(json)["d"]["results"];

            foreach (JObject result in results)
            {
                weblists.Add(new WebContent
                {
                    ContentId = (string)result["ContentId"],
                    Title = (string)result["Title"],
                    Section = (string)result["SEction"],
                    WebText = (string)result["Web_x0020_Text"],
                    International = (bool)result["International"],
                    Campus = (string)result["Campus"],
                    BannerImage = result["Banner_x0020_Image"].Type != JTokenType.Null
                        ? new Link
                        {
                            Description = (string)result["Banner_x0020_Image"]["Description"],
                            Url = (string)result["Banner_x0020_Image"]["Url"]
                        }
                        : null,
                    OriginalTextLocation = result["Original_x0020_Text_x0020_Locati"].Type != JTokenType.Null
                        ? new Link
                        {
                            Description = (string)result["Original_x0020_Text_x0020_Locati"]["Description"],
                            Url = (string)result["Original_x0020_Text_x0020_Locati"]["Url"]
                        }
                        : null
                });
            }
            
            return weblists;
        }
    }
}
