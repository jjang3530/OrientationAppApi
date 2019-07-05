using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using OrientationAppApi.Helpers;
using OrientationAppApi.Models;
using OrientationAppApi.Repositories.Interfaces;

namespace OrientationAppApi.Controllers
{
    [Route("api/orientation-startup")]
    [ApiController]
    public class SharepointController : ControllerBase
    {
        private const string _site = "sites/orientation-startup";
        private const string _list = "Web Content";
        private readonly IWebContentRepo _repo;
        private SharePointSettings _SharePointSettings;

        public SharepointController(IWebContentRepo repo, SharePointSettings sharepointsettings)
        {
            _repo = repo;
            _SharePointSettings = sharepointsettings;
        }

        // GET api/orientation-startup
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] {"Welcome New Student!", "Conestoga College"};
        }


        // GET api/orientation-startup/1
        [HttpGet("{id}")]
        public async Task<IActionResult> GetWebConent(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            WebContent _detailsList = null;
            string select = id.ToString(); // change
            try
            {
                _detailsList = await _repo.GetSharePointListsAsync(_SharePointSettings.UserName, _SharePointSettings.Password, _SharePointSettings.Domain, _SharePointSettings.Url, _site, _list, select);


                return Ok(_detailsList);
            }
            catch (WebException webException)
            {
                if (webException.Status == WebExceptionStatus.ProtocolError && webException.Response != null)
                {
                    var resp = (HttpWebResponse)webException.Response;

                    if (resp.StatusCode == HttpStatusCode.NotFound)
                    {
                        return NotFound();
                    }
                }

                throw webException;
            }
            catch (ArgumentNullException argumentNullException)
            {
                if (argumentNullException.ParamName == "html")
                {
                    return NotFound();
                }

                throw argumentNullException;
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

    }
}