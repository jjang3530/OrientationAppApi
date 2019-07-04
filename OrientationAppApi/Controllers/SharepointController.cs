using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
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

        // GET api/sharepoint
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] {"Welcome New Student!", "Conestoga College"};
        }


        // GET api/sharepoint/1
        [HttpGet("{id}")]
        public async Task<IActionResult> GetWebConent(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var wikiPage = await _repo.GetWebConentAsync(_site, _list, id);
                var jObject = new JObject();

                if (wikiPage.HasHeader)
                {
                    jObject.Add("headerContent", wikiPage.HeaderContent);
                }

                if (wikiPage.ColumnCount == 1)
                {
                    jObject.Add("mainContent", wikiPage.MainContent);
                }
                else if (wikiPage.ColumnCount == 2)
                {
                    jObject.Add("leftContent", wikiPage.LeftContent);
                    jObject.Add("rightContent", wikiPage.RightContent);
                }
                else if (wikiPage.ColumnCount == 3)
                {
                    jObject.Add("leftContent", wikiPage.LeftContent);
                    jObject.Add("middleContent", wikiPage.MiddleContent);
                    jObject.Add("rightContent", wikiPage.RightContent);
                }

                if (wikiPage.HasFooter)
                {
                    jObject.Add("footerContent", wikiPage.FooterContent);
                }

                return Ok(jObject);
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