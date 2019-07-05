using OrientationAppApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrientationAppApi.Repositories.Interfaces
{
    public interface IWebContentRepo
    {
        Task<WebContent> GetSharePointListsAsync(string username, string password, string domain, string url, string site, string list, string id);
    }
}
