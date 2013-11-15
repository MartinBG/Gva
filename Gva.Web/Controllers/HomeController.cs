using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Web;
using System.Web.Mvc;

namespace Gva.Web.Controllers
{
    public class HomeController : Controller
    {
        private Common.Data.IUnitOfWork unitOfWork;
        private Regs.Api.Managers.LotManager.ILotManager lotManager;
        private Regs.Api.Managers.LobManager.ILobManager lobManager;
        private Common.Infrastructure.UserContext userContext;

        public HomeController(Common.Data.IUnitOfWork unitOfWork, 
            Regs.Api.Managers.LotManager.ILotManager lotManager, 
            Regs.Api.Managers.LobManager.ILobManager lobManager,
            Common.Infrastructure.IUserContextProvider userContextProvider)
        {
            this.unitOfWork = unitOfWork;
            this.lotManager = lotManager;
            this.lobManager = lobManager;
            this.userContext = userContextProvider.GetCurrentUserContext();
        }

        public ActionResult Test()
        {
            using (var transaction = unitOfWork.BeginTransaction())
            {
                Regs.Api.Models.Set set = lotManager.GetSet("Person");

                Regs.Api.Models.Lot lot; //= set.AddLot(this.userContext);
                //unitOfWork.Save();

                lot = lotManager.GetLot(3);
                //Regs.Api.Models.PartVersion partVersion = lot.AddPart("/addresses/*", new Newtonsoft.Json.Linq.JObject { }, lobManager);
                //Regs.Api.Models.PartVersion partVersion2 = lot.UpdatePart("/ratings/1", Newtonsoft.Json.Linq.JObject.Parse(@"{ CPU: 'Intel', Drives: [ 'DVD read/writer', '500 gigabyte hard drive']}"), lobManager);
                //lot.Commit();
                unitOfWork.DbContext.Set<Regs.Api.Models.Commit>().Include(c => c.PartVersions).Load();
                //Regs.Api.Models.PartVersion pv = lot.ResetPart("/addresses/0");
                //Regs.Api.Models.PartVersion pv2 = lot.ResetPart("/ratings/1");
                lot.Reset(1);
                unitOfWork.Save();

                transaction.Commit();
            }

            return new EmptyResult();
        }

        public ActionResult Index()
        {
            return File(Server.MapPath("~/App/index.html"), "text/html");
        }
    }
}
