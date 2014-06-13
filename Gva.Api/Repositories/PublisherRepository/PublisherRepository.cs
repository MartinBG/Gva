using System.Collections.Generic;
using System.Linq;
using Common.Api.Models;
using Common.Data;
using Common.Linq;
using Gva.Api.Models;
using System.Data.Entity;
using Gva.Api.ModelsDO;
using Regs.Api.Models;

namespace Gva.Api.Repositories.PublisherRepository
{
    public class PublisherRepository : IPublisherRepository
    {
        private IUnitOfWork unitOfWork;

        public PublisherRepository(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public IEnumerable<PublisherDO> GetPublishers(PublisherType publisherType, string name = null, int offset = 0, int? limit = null)
        {
            IQueryable<PublisherDO> publishers = null;

            if (publisherType == PublisherType.Undefined || publisherType == PublisherType.Inspector)
            {
                publishers = this.Concat(publishers,
                    from p in this.unitOfWork.DbContext.Set<GvaViewPerson>()
                    join i in this.unitOfWork.DbContext.Set<GvaViewPersonInspector>() on p.LotId equals i.LotId
                    where i.Valid
                    select new PublisherDO
                    {
                        Name = p.Names,
                        Code = i.ExaminerCode,
                        PublisherType = PublisherType.Inspector
                    });
            }

            if (publisherType == PublisherType.Undefined || publisherType == PublisherType.Examiner)
            {
                publishers = this.Concat(publishers,
                    from p in this.unitOfWork.DbContext.Set<GvaViewPerson>()
                    join i in this.unitOfWork.DbContext.Set<GvaViewOrganizationExaminer>() on p.LotId equals i.PersonLotId
                    where i.Valid && i.PermitedCheck
                    select new PublisherDO
                    {
                        Name = p.Names,
                        Code = i.ExaminerCode,
                        PublisherType = PublisherType.Examiner
                    });
            }

            if (publisherType == PublisherType.Undefined || publisherType == PublisherType.School)
            {
                publishers = this.Concat(publishers,
                    from nv in this.unitOfWork.DbContext.GetNomValuesByTextContentProperty("schools", "isPilotTraining", "true")
                    where nv.IsActive
                    select new PublisherDO
                    {
                        Name = nv.Name,
                        Code = null,
                        PublisherType = PublisherType.School
                    });
            }

            if (publisherType == PublisherType.Undefined || publisherType == PublisherType.Organization)
            {
                publishers = this.Concat(publishers,
                    from o in this.unitOfWork.DbContext.Set<GvaViewOrganization>()
                    where o.Valid
                    select new PublisherDO
                    {
                        Name = o.Name,
                        Code = o.OrganizationType,
                        PublisherType = PublisherType.Organization
                    });
            }

            if (publisherType == PublisherType.Undefined || publisherType == PublisherType.Caa)
            {
                publishers = this.Concat(publishers,
                    from nv in this.unitOfWork.DbContext.Set<NomValue>()
                    where nv.IsActive && nv.Nom.Alias == "caa"
                    select new PublisherDO
                    {
                        Name = nv.Name,
                        Code = null,
                        PublisherType = PublisherType.Caa
                    });
            }

            if (publisherType == PublisherType.Undefined || publisherType == PublisherType.Other)
            {
                publishers = this.Concat(publishers,
                    from nv in this.unitOfWork.DbContext.Set<NomValue>()
                    where nv.IsActive && nv.Nom.Alias == "otherDocPublishers"
                    select new PublisherDO
                    {
                        Name = nv.Name,
                        Code = null,
                        PublisherType = PublisherType.Other
                    });
            }

            if (!string.IsNullOrEmpty(name)) 
            {
                publishers = publishers.Where(p => p.Name.Contains(name));
            }

            return publishers
                .OrderBy(p => p.PublisherType)
                .WithOffsetAndLimit(offset, limit)
                .ToList();
        }

        private IQueryable<PublisherDO> Concat(IQueryable<PublisherDO> first, IQueryable<PublisherDO> second)
        {
            if (first == null)
            {
                return second;
            }
            else
            {
                return first.Concat(second);
            }
        }
    }
}
