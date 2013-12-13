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
        private Common.Api.UserContext.UserContext userContext;

        public HomeController(Common.Data.IUnitOfWork unitOfWork, 
            Regs.Api.Managers.LotManager.ILotManager lotManager, 
            Regs.Api.Managers.LobManager.ILobManager lobManager,
            Common.Api.UserContext.IUserContextProvider userContextProvider)
        {
            this.unitOfWork = unitOfWork;
            this.lotManager = lotManager;
            this.lobManager = lobManager;
            this.userContext = userContextProvider.GetCurrentUserContext();
        }

        public ActionResult Index()
        {
            return File(Server.MapPath("~/App/build/index.html"), "text/html");
        }
    }
}
