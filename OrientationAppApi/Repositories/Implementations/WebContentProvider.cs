using OrientationAppApi.Helpers;
using OrientationAppApi.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace OrientationAppApi.Repositories.Implementations
{
    public class WebContentProvider : IWebContentProvider
    {
        private string _baseUrl;
        private NetworkCredential _credentials;
        private string _response = null;
        private SharePointSettings _SharePointSettings;

        public WebContentProvider(SharePointSettings sharePointSettings)
        {
            _SharePointSettings = sharePointSettings;
        }

        //public async Task<string> GetSharePointListsAsync(string username, string password, string domain, string url, string site, string list, string contentId)
        public async Task<string> GetSharePointListsAsync(string site, string list, string contentId)
        {
            try
            {

                //_credentials = new NetworkCredential(username, password, domain);
                //_baseUrl = url;
                _credentials = new NetworkCredential(_SharePointSettings.UserName, _SharePointSettings.Password, _SharePointSettings.Domain);
                _baseUrl = _SharePointSettings.Url;


                string requestUri = $"{_baseUrl}/{site}/_api/web/lists/GetByTitle('{list}')/items?$filter=ContentID eq '{contentId}'";

                var handler = new HttpClientHandler { Credentials = _credentials };

                using (HttpClient httpClient = new HttpClient(handler))
                {
                    httpClient.DefaultRequestHeaders.Add("Accept", "application/json; odata=verbose");

                    _response = await httpClient.GetStringAsync(requestUri);
                }
            }
            catch (Exception e)
            {
                _response = null;
            }
            return _response;

        }
    }
}