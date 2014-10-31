using System;
using System.Web.Http;
using Gva.Api.ModelsDO;
using Gva.Api.Repositories.PublisherRepository;

namespace Gva.Api.Controllers
{
    [RoutePrefix("api/publishers")]
    public class GvaPublisherController : ApiController
    {
        private IPublisherRepository publisherRepository;

        public GvaPublisherController(
            IPublisherRepository publisherRepository)
        {
            this.publisherRepository = publisherRepository;
        }

        [Route("")]
        public IHttpActionResult GetPublishers(PublisherType publisherType = PublisherType.Undefined, string publisherName = null, string publisherCode = null, int offset = 0, int? limit = null)
        {
            return Ok(this.publisherRepository.GetPublishers(publisherType, publisherName, publisherCode, offset, limit));
        }
    }
}

