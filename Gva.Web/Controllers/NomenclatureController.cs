using Common.Data;
using Newtonsoft.Json.Linq;
using Regs.Api.Models;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Gva.Web.Controllers
{
    public class NomenclatureController : ApiController
    {
        private IUnitOfWork unitOfWork;

        public NomenclatureController (IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public HttpResponseMessage GetAddressTypes(string type)
        {
            var nomTypeValues = this.unitOfWork.DbContext.Set<Nom>()
                .Include(n => n.NomValues)
                .SingleOrDefault(n => n.Alias == "addressTypes")
                .NomValues
                .Select(nv => new { nv.NomValueId, nv.Name, Content = JObject.Parse(nv.TextContent) })
                .Where(nv => nv.Content.GetValue("type").ToString() == type)
                .Select(nv => new { nv.NomValueId, nv.Name });

            return ControllerContext.Request.CreateResponse(HttpStatusCode.OK, nomTypeValues);
        }

        public HttpResponseMessage GetNoms(string alias, int? staffTypeId = null, string parentAlias = null)
        {
            var nomTypeValues = this.unitOfWork.DbContext.Set<Nom>()
                .Include(n => n.NomValues)
                .SingleOrDefault(n => n.Alias == alias)
                .NomValues
                .Select(nv => new { nv.NomValueId, nv.Name });

            return ControllerContext.Request.CreateResponse(HttpStatusCode.OK, nomTypeValues);
        }
    }
}