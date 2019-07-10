using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrientationAppApi.Repositories.Interfaces
{
    public interface IWebContentProvider
    {
        Task<string> GetSharePointListsAsync(string site, string list, string contentId);
    }
}
