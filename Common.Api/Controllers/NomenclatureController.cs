using System.Collections.Generic;
using System.Web.Http;
using System.Linq;
using Common.Api.Models;
using Common.Api.Repositories.NomRepository;
using Common.Linq;
using Newtonsoft.Json.Linq;
using System;
using Common.Data;

namespace Common.Api.Controllers
{
    [Authorize]
    public class NomenclatureController : ApiController
    {
        private IUnitOfWork unitOfWork;
        private INomRepository nomRepository;

        public NomenclatureController(IUnitOfWork unitOfWork, INomRepository nomRepository)
        {
            this.unitOfWork = unitOfWork;
            this.nomRepository = nomRepository;
        }

        [Route("api/admin/nomenclatures")]
        [HttpGet]
        public IHttpActionResult GetNomenclatures()
        {
            List<Nom> noms = this.unitOfWork.DbContext.Set<Nom>().ToList();

            return Ok(noms);
        }

        [Route("api/admin/nomenclatures/{id:int}")]
        [HttpGet]
        public IHttpActionResult GetNomenclature(int id)
        {
            Nom nom = this.unitOfWork.DbContext.Set<Nom>().FirstOrDefault(e => e.NomId == id);

            return Ok(nom);
        }

        [Route("api/admin/nomenclatures/{id:int}")]
        [HttpPost]
        public IHttpActionResult UpdateNomenclature(int id, Nom data)
        {
            Nom nom = this.unitOfWork.DbContext.Set<Nom>().FirstOrDefault(e => e.NomId == id);

            nom.Name = data.Name;

            this.unitOfWork.Save();

            return Ok(nom);
        }

        [Route("api/admin/nomenclatures/{nomId:int}/values")]
        [HttpGet]
        public IHttpActionResult GetNomenclatureValues(int nomId)
        {
            List<NomValue> nomValues = this.nomRepository.GetNomValues(nomId).ToList();

            return Ok(nomValues);
        }

        [Route("api/admin/nomenclatures/{nomId:int}/values/{id:int}")]
        [HttpGet]
        public IHttpActionResult GetNomenclatureValue(int nomId, int id)
        {
            NomValue nomValue = this.nomRepository.GetNomValue(id);

            return Ok(nomValue);
        }

        [Route("api/admin/nomenclatures/{nomId:int}/values")]
        [HttpPost]
        public IHttpActionResult CreateNomenclatureValue(int nomId, NomValue data)
        {
            NomValue nomValue = new NomValue();

            nomValue.NomId = nomId;
            nomValue.Code = data.Code;
            nomValue.Name = data.Name;
            nomValue.NameAlt = data.NameAlt;
            nomValue.ParentValueId = data.ParentValueId;
            nomValue.IsActive = data.IsActive;
            nomValue.Order = data.Order;

            this.unitOfWork.DbContext.Set<NomValue>().Add(nomValue);

            this.unitOfWork.Save();

            return Ok(nomValue);
        }

        [Route("api/admin/nomenclatures/{nomId:int}/values/{id:int}")]
        [HttpPost]
        public IHttpActionResult UpdateNomenclatureValue(int nomId, int id, NomValue data)
        {
            NomValue nomValue = this.unitOfWork.DbContext.Set<NomValue>().FirstOrDefault(e => e.NomValueId == id);

            nomValue.Code = data.Code;
            nomValue.Name = data.Name;
            nomValue.NameAlt = data.NameAlt;
            nomValue.ParentValueId = data.ParentValueId;
            nomValue.IsActive = data.IsActive;
            nomValue.Order = data.Order;

            this.unitOfWork.Save();

            return Ok(nomValue);
        }

        [Route("api/admin/nomenclatures/{nomId:int}/values/{id:int}")]
        [HttpDelete]
        public IHttpActionResult DeleteNomenclatureValue(int nomId, int id)
        {
            NomValue nomValue = this.unitOfWork.DbContext.Set<NomValue>().FirstOrDefault(e => e.NomValueId == id);

            if (nomValue == null)
            {
                return NotFound();
            }

            if (!string.IsNullOrEmpty(nomValue.Alias))
            {
                throw new Exception("Can not delete nomenclature with alias.");
            }

            this.unitOfWork.DbContext.Set<NomValue>().Remove(nomValue);

            this.unitOfWork.Save();

            return Ok();
        }

        #region Nomenclature queries

        [Route("api/nomenclatures/nomList/{id:int}")]
        public IHttpActionResult GetNomItem(int id)
        {
            var result = this.unitOfWork.DbContext.Set<Nom>()
                .Where(e => e.NomId == id)
                .SingleOrDefault();

            return Ok(new
            {
                nomValueId = result.NomId,
                name = result.Name,
                alias = result.Alias,
                isActive = true
            });
        }

        [Route("api/nomenclatures/nomList")]
        public IHttpActionResult GetNomItems(string term = null, int offset = 0, int? limit = null)
        {
            var predicate =
                PredicateBuilder.True<Nom>()
                .AndStringContains(e => e.Name, term);

            var results =
                this.unitOfWork.DbContext.Set<Nom>()
                .Where(predicate)
                .OrderBy(e => e.Name)
                .WithOffsetAndLimit(offset, limit)
                .ToList()
                .Select(e => new
                {
                    nomValueId = e.NomId,
                    name = e.Name,
                    alias = e.Alias,
                    isActive = true
                });

            return Ok(results);
        }

        [Route("api/nomenclatures/nomValueList/{id:int}")]
        public IHttpActionResult GetNomValueItem(int id)
        {
            var result = this.unitOfWork.DbContext.Set<NomValue>()
                .Where(e => e.NomValueId == id)
                .SingleOrDefault();

            return Ok(new
            {
                nomValueId = result.NomValueId,
                name = result.Name,
                alias = result.Alias,
                isActive = result.IsActive
            });
        }

        [Route("api/nomenclatures/nomValueList")]
        public IHttpActionResult GetNomValueItems(string term = null, int? parentValueId = null, int offset = 0, int? limit = null)
        {
            var predicate =
                PredicateBuilder.True<NomValue>()
                .AndStringContains(e => e.Name, term)
                .AndEquals(e => e.NomId, parentValueId)
                .And(e => e.IsActive);

            var results =
                this.unitOfWork.DbContext.Set<NomValue>()
                .Where(predicate)
                .OrderBy(e => e.Order)
                .WithOffsetAndLimit(offset, limit)
                .Select(e => new
                {
                    nomValueId = e.NomValueId,
                    name = e.Name,
                    alias = e.Alias,
                    isActive = e.IsActive
                })
                .ToList();

            return Ok(results);
        }

        #endregion
    }
}