using System.Web.Mvc;

namespace Gva.Web.Controllers
{
    public class HomeController : Controller
    {
        private Common.Data.IUnitOfWork unitOfWork;
        private Regs.Api.Repositories.LotRepositories.ILotRepository lotManager;
        private Common.Api.UserContext.UserContext userContext;

        public HomeController(
            Common.Data.IUnitOfWork unitOfWork,
            Regs.Api.Repositories.LotRepositories.ILotRepository lotManager,
            Common.Api.UserContext.IUserContextProvider userContextProvider)
        {
            this.unitOfWork = unitOfWork;
            this.lotManager = lotManager;
            this.userContext = userContextProvider.GetCurrentUserContext();
        }

        public ActionResult Index()
        {
            return this.File(Server.MapPath("~/App/build/index.html"), "text/html");
        }
    }
}
