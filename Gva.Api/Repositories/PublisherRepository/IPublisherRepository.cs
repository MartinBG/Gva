using System.Collections.Generic;
using Gva.Api.Models;
using Gva.Api.ModelsDO;

namespace Gva.Api.Repositories.PublisherRepository
{
    public interface IPublisherRepository
    {
        IEnumerable<PublisherDO> GetPublishers(PublisherType publisherType, int offset = 0, int? limit = null);
    }
}
