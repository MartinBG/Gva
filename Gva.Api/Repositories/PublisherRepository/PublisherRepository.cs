﻿using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Common.Api.Models;
using Common.Data;
using Common.Linq;
using Gva.Api.Models.Views.Organization;
using Gva.Api.Models.Views.Person;
using Gva.Api.ModelsDO;

namespace Gva.Api.Repositories.PublisherRepository
{
    public class PublisherRepository : IPublisherRepository
    {
        private IUnitOfWork unitOfWork;

        public PublisherRepository(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public IEnumerable<PublisherDO> GetPublishers(
            PublisherType publisherType,
            string publisherName = null,
            string publisherCode = null,
            string publisherLin = null,
            int offset = 0,
            int? limit = null)
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
                        Lin = p.Lin,
                        Name = p.Names,
                        Code = i.ExaminerCode,
                        PublisherType = PublisherType.Inspector
                    });
            }

            if (publisherType == PublisherType.Undefined || publisherType == PublisherType.Examiner)
            {
                publishers = this.Concat(publishers,
                    this.unitOfWork.DbContext.Set<GvaViewPerson>().Include(p => p.Examiner)
                        .Where(p => p.Examiner != null && p.Examiner.Valid)
                        .Select(p => new PublisherDO
                        {
                            Lin = p.Lin,
                            Name = p.Names,
                            Code = p.Examiner.ExaminerCode,
                            PublisherType = PublisherType.Examiner
                        }));
            }

            if (publisherType == PublisherType.Undefined || publisherType == PublisherType.School)
            {
                publishers = this.Concat(publishers,
                    from nv in this.unitOfWork.DbContext.GetNomValuesByTextContentProperty("schools", "isPilotTraining", "true")
                    where nv.IsActive
                    select new PublisherDO
                    {
                        Lin = null,
                        Name = nv.Name,
                        Code = null,
                        PublisherType = PublisherType.School
                    });
            }

            if (publisherType == PublisherType.Undefined || publisherType == PublisherType.Organization)
            {
                publishers = this.Concat(publishers,
                    from o in this.unitOfWork.DbContext.Set<GvaViewOrganization>().Include(o => o.OrganizationType)
                    where o.Valid
                    select new PublisherDO
                    {
                        Lin = null,
                        Name = o.Name,
                        Code = o.OrganizationType.Name,
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
                        Lin = null,
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
                        Lin = null,
                        Name = nv.Name,
                        Code = null,
                        PublisherType = PublisherType.Other
                    });
            }

            if (!string.IsNullOrEmpty(publisherName)) 
            {
                publishers = publishers.Where(p => p.Name.Contains(publisherName));
            }

            if (!string.IsNullOrEmpty(publisherLin))
            {
                publishers = publishers.Where(p => p.Lin.ToString().Contains(publisherLin));
            }

            if (!string.IsNullOrEmpty(publisherCode))
            {
                publishers = publishers.Where(p => p.Code.Contains(publisherCode));
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
