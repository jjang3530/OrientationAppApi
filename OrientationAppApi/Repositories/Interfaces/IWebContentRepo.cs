using OrientationAppApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrientationAppApi.Repositories.Interfaces
{
    public interface IWebContentRepo
    {
        Task<IList<WebContent>> GetSharePointListsAsync(string site, string list, string contentId);
    }
}
