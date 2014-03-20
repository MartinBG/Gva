using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Api.UserContext;
using Common.Data;
using Newtonsoft.Json.Linq;
using Ninject;
using Docs.Api.Models;
using Docs.Api.Repositories.CorrespondentRepository;
using Docs.Api.Tests.Mocks;
using SubSpec;
using Xunit;
using Xunit.Extensions;
using Common.Tests;

namespace Docs.Api.Tests.Specs
{
    public class CorrespondentSpec
    {
        private IKernel kernel;

        public CorrespondentSpec()
        {
            this.kernel = new StandardKernel();

            Gva.Web.App_Start.NinjectConfig.RegisterServices(this.kernel);
            this.kernel.Unbind<IUserContextProvider>();
            this.kernel.Bind<IUserContextProvider>().To<MockUserContextProvider>();
        }

        [Specification, AutoRollback]
        public void NewCorrespondentSpec()
        {
            IUnitOfWork unitOfWork1 = null;
            ICorrespondentRepository correspondentRepository1 = null;
            UserContext userContext1 = null;

            IUnitOfWork unitOfWork2 = null;
            ICorrespondentRepository correspondentRepository2 = null;
            UserContext userContext2 = null;

            "A new correspondent setup".ContextFixture(() =>
            {
                var ctx1 = kernel.Get<DisposableTuple<IUnitOfWork, ICorrespondentRepository, IUserContextProvider>>();

                unitOfWork1 = ctx1.Item1;
                correspondentRepository1 = ctx1.Item2;
                userContext1 = ctx1.Item3.GetCurrentUserContext();

                var ctx2 = kernel.Get<DisposableTuple<IUnitOfWork, ICorrespondentRepository, IUserContextProvider>>();

                unitOfWork2 = ctx2.Item1;
                correspondentRepository2 = ctx2.Item2;
                userContext2 = ctx2.Item3.GetCurrentUserContext();

                return DisposableTuple.Create(ctx1, ctx2);
            });

            "can be created as bg citizen".Assert(() =>
            {
                var corr1 = correspondentRepository1.CreateBgCitizen(2, 1, true, "Иван", "Иванов", "1212120909", userContext1);
                unitOfWork1.Save();

                var corr2 = correspondentRepository2.GetCorrespondent(corr1.CorrespondentId);
                Assert.NotNull(corr2);
            });
            "can be created as legal entity".Assert(() =>
            {
                var corr1 = correspondentRepository1.CreateLegalEntity(2, 3, true, "Фирма Х", "10203040503", userContext1);
                unitOfWork1.Save();

                var corr2 = correspondentRepository2.GetCorrespondent(corr1.CorrespondentId);
                Assert.NotNull(corr2);
            });
            "can be created as foreigner".Assert(() =>
            {
                var corr1 = correspondentRepository1.CreateForeigner(2, 2, true, "John", "Doe", null, "Downtown", DateTime.Now, userContext1);
                unitOfWork1.Save();

                var corr2 = correspondentRepository2.GetCorrespondent(corr1.CorrespondentId);
                Assert.NotNull(corr2);
            });
            "can be created as foreign legal entity".Assert(() =>
            {
                var corr1 = correspondentRepository1.CreateFLegalEntity(2, 4, true, "Company X", null, "Ltd.", "001", "Other data", userContext1);
                unitOfWork1.Save();

                var corr2 = correspondentRepository2.GetCorrespondent(corr1.CorrespondentId);
                Assert.NotNull(corr2);
            });
        }

        [Specification, AutoRollback]
        public void SavedCorrespondentSpec()
        {
            int crCorrespondentId = 0;
            int crCorrespondentIdWithContacts = 0;

            IUnitOfWork unitOfWork1 = null;
            ICorrespondentRepository correspondentRepository1 = null;
            UserContext userContext1 = null;

            IUnitOfWork unitOfWork2 = null;
            ICorrespondentRepository correspondentRepository2 = null;
            UserContext userContext2 = null;

            "An existing correspondent and nomenclatures".ContextFixture(() =>
            {
                using (var tempCtx = kernel.Get<DisposableTuple<IUnitOfWork, ICorrespondentRepository, IUserContextProvider>>())
                {
                    var unitOfWork = tempCtx.Item1;
                    var correspondentRepository = tempCtx.Item2;
                    var userContext = tempCtx.Item3.GetCurrentUserContext();

                    var corr1 = correspondentRepository.CreateLegalEntity(2, 3, true, "Фирма Х", "102030405", userContext);

                    var corr2 = correspondentRepository.CreateLegalEntity(2, 3, true, "Фирма Y", "201058583", userContext);
                    corr2.CreateCorrespondentContact("Vasil Petrov", "0110130441", "", true, userContext);

                    unitOfWork.Save();
                    crCorrespondentId = corr1.CorrespondentId;
                    crCorrespondentIdWithContacts = corr2.CorrespondentId;
                }

                var ctx1 = kernel.Get<DisposableTuple<IUnitOfWork, ICorrespondentRepository, IUserContextProvider>>();

                unitOfWork1 = ctx1.Item1;
                correspondentRepository1 = ctx1.Item2;
                userContext1 = ctx1.Item3.GetCurrentUserContext();

                var ctx2 = kernel.Get<DisposableTuple<IUnitOfWork, ICorrespondentRepository, IUserContextProvider>>();

                unitOfWork2 = ctx2.Item1;
                correspondentRepository2 = ctx2.Item2;
                userContext2 = ctx2.Item3.GetCurrentUserContext();

                return DisposableTuple.Create(ctx1, ctx2);
            });

            "can be found and edited".Assert(() =>
            {
                int totalCount;
                var correspondents = correspondentRepository1.GetCorrespondents(null, null, 10, 0, out totalCount);
                Assert.NotEmpty(correspondents);

                var corr1 = correspondentRepository1.GetCorrespondent(crCorrespondentId);
                Assert.NotNull(corr1);

                corr1.LegalEntityBulstat = "102034343";
                unitOfWork1.Save();

                var corr2 = correspondentRepository2.GetCorrespondent(corr1.CorrespondentId);
                Assert.Equal("102034343", corr2.LegalEntityBulstat);
            });

            "can be added correspondent contact".Assert(() =>
            {
                var corr1 = correspondentRepository1.GetCorrespondent(crCorrespondentId);
                corr1.CreateCorrespondentContact("Petar Petrov", "0110130441", "", true, userContext1);
                unitOfWork1.Save();

                var corr2 = correspondentRepository2.GetCorrespondent(crCorrespondentId);
                Assert.NotEmpty(corr2.CorrespondentContacts);
            });

            "can be removed correspondent contact".Assert(() =>
            {
                var corr1 = correspondentRepository1.GetCorrespondent(crCorrespondentIdWithContacts);

                Assert.NotEmpty(corr1.CorrespondentContacts);

                corr1.DeleteCorrespondentContact(corr1.CorrespondentContacts.First(), userContext1);
                unitOfWork1.Save();

                var corr2 = correspondentRepository2.GetCorrespondent(crCorrespondentIdWithContacts);
                Assert.Empty(corr2.CorrespondentContacts);
            });

            "cannot be modified concurrently".Assert(() =>
            {
                var corr1 = correspondentRepository1.GetCorrespondent(crCorrespondentId);
                corr1.LegalEntityBulstat = "102034343";

                var corr2 = correspondentRepository2.GetCorrespondent(crCorrespondentId);
                corr2.CreateCorrespondentContact("Petar Petrov", "0110130441", "", true, userContext2);
                unitOfWork2.Save();

                Assert.Throws<DbUpdateConcurrencyException>(() => unitOfWork1.Save());
            });

            "can be deleted".Assert(() =>
            {
                correspondentRepository1.DeteleCorrespondent(crCorrespondentId);
                unitOfWork1.Save();

                var corr2 = correspondentRepository2.GetCorrespondent(crCorrespondentId);
                Assert.Null(corr2);
            });
        }
    }
}
