﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Gva.Web.Controllers
{
    public class HomeController : Controller
    {
        private Common.Data.IUnitOfWork unitOfWork;
        private Regs.Api.LotManager.ILotManager lotManager;

        public HomeController(Common.Data.IUnitOfWork unitOfWork, Regs.Api.LotManager.ILotManager lotManager)
        {
            this.unitOfWork = unitOfWork;
            this.lotManager = lotManager;
        }

        public ActionResult Test()
        {
            using (var transaction = unitOfWork.BeginTransaction())
            {
                Regs.Api.Models.Set set = lotManager.GetSet("testAlias");

                set.AddLot();

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
