using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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


        public SharepointController(IWebContentRepo repo)
        {
            _repo = repo;
        }

        // GET api/orientation-startup
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] {"Welcome New Student!", "Conestoga College"};
        }


        // GET api/orientation-startup/1
        [HttpGet("{contentId}")]
        public async Task<IActionResult> GetWebConent(string contentId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            IList<WebContent> _detailsList = null;
            try

            {
                _detailsList = await _repo.GetSharePointListsAsync(_site, _list, contentId);
                var result = _detailsList.Single();

                return Ok(result);
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