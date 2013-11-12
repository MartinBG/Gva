using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Gva.Web.Controllers
{
    public class HomeController : Controller
    {
        private Common.Data.IUnitOfWork unitOfWork;
        private Regs.Api.Managers.LotManager.ILotManager lotManager;
        private Regs.Api.Managers.LobManager.ILobManager lobManager;

        public HomeController(Common.Data.IUnitOfWork unitOfWork, Regs.Api.Managers.LotManager.ILotManager lotManager, Regs.Api.Managers.LobManager.ILobManager lobManager)
        {
            this.unitOfWork = unitOfWork;
            this.lotManager = lotManager;
            this.lobManager = lobManager;
        }

        public ActionResult Test()
        {
            using (var transaction = unitOfWork.BeginTransaction())
            {
                Regs.Api.Models.Set set = lotManager.GetSet("Person");

                Regs.Api.Models.Lot lot = set.AddLot();
                unitOfWork.Save();

                lot = lotManager.GetLot(1);
                Regs.Api.Models.PartVersion partVersion = lot.AddPart("/addresses/*", new Newtonsoft.Json.Linq.JObject { }, lobManager);
                //Regs.Api.Models.PartVersion partVersion = lot.UpdatePart("/addresses/0", Newtonsoft.Json.Linq.JObject.Parse(@"{ CPU: 'Intel', Drives: [ 'DVD read/writer', '500 gigabyte hard drive']}"), lobManager);
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
