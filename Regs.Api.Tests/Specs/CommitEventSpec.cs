using System.Collections.Generic;
using System.Linq;
using Common.Api.UserContext;
using Common.Data;
using Newtonsoft.Json.Linq;
using Ninject;
using Ninject.Extensions.NamedScope;
using Regs.Api.LotEvents;
using Regs.Api.Models;
using Regs.Api.Repositories.LotRepositories;
using Regs.Api.Tests.Common;
using SubSpec;
using Xunit;
using Xunit.Extensions;

namespace Regs.Api.Tests.Specs
{
    public class EventsSpec
    {
        private IKernel kernel;

        public EventsSpec()
        {
            this.kernel = new StandardKernel();

            Gva.Web.App_Start.NinjectConfig.RegisterServices(this.kernel);
            this.kernel.Unbind<IUserContextProvider>();
            this.kernel.Unbind<ILotEventHandler>();
            this.kernel.Bind<IUserContextProvider>().To<MockUserContextProvider>();
            this.kernel.Bind<ILotEventHandler>().To<MockLotEventHandler>().InCallScope();
        }

        [Specification, AutoRollback]
        public void CommitEventSpec()
        {
            IUnitOfWork unitOfWork1 = null;
            ILotRepository lotRepository1 = null;
            UserContext userContext1 = null;
            MockLotEventHandler mockLotEventHandler = null;
            Lot lot = null;

            "A lot".ContextFixture(() =>
            {
                var ctx1 = kernel.Get<DisposableTuple<IUnitOfWork, ILotRepository, IUserContextProvider, ILotEventHandler>>();

                unitOfWork1 = ctx1.Item1;
                lotRepository1 = ctx1.Item2;
                userContext1 = ctx1.Item3.GetCurrentUserContext();
                mockLotEventHandler = (MockLotEventHandler)ctx1.Item4;

                lot = lotRepository1.GetSet("Person").CreateLot(userContext1);
                lot.CreatePart("/personAddresses/0", JObject.Parse("{ address: '0' }"), userContext1);
                lot.Commit(userContext1);

                return ctx1;
            });

            "fires a commit event when commited".Assert(() =>
            {
                Assert.True(mockLotEventHandler.Event is CommitEvent);
            });

            "fires a commit event when commited, containing the new index".Assert(() =>
            {
                Assert.Equal(lot.Index, ((CommitEvent)mockLotEventHandler.Event).NewIndex);
            });

            "fires a commit event when commited, containing the lot".Assert(() =>
            {
                Assert.Equal(lot, ((CommitEvent)mockLotEventHandler.Event).Lot);
            });

            "fires a commit event when commited, containing the commit that was created".Assert(() =>
            {
                Assert.Equal(lot.LastCommit, ((CommitEvent)mockLotEventHandler.Event).Commit);
            });
        }

        [Specification, AutoRollback]
        public void ResetEventSpec()
        {
            IUnitOfWork unitOfWork1 = null;
            ILotRepository lotRepository1 = null;
            UserContext userContext1 = null;
            MockLotEventHandler mockLotEventHandler = null;
            Lot lot = null;

            "A lot".ContextFixture(() =>
            {
                var ctx1 = kernel.Get<DisposableTuple<IUnitOfWork, ILotRepository, IUserContextProvider, ILotEventHandler>>();

                unitOfWork1 = ctx1.Item1;
                lotRepository1 = ctx1.Item2;
                userContext1 = ctx1.Item3.GetCurrentUserContext();
                mockLotEventHandler = (MockLotEventHandler)ctx1.Item4;

                lot = lotRepository1.GetSet("Person").CreateLot(userContext1);
                lot.CreatePart("/personAddresses/0", JObject.Parse("{ address: '0' }"), userContext1);
                lot.Commit(userContext1);

                lot.CreatePart("/personAddresses/1", JObject.Parse("{ address: '1' }"), userContext1);
                lot.Commit(userContext1, new string[] { "/personAddresses/1" });
                unitOfWork1.Save();

                var indexCommit = lot.Commits.Where(c => c.IsIndex).Single();
                var secondCommit = indexCommit.ParentCommit;

                int firstCommitId = secondCommit.ParentCommit.CommitId;
                int secondCommitId = secondCommit.CommitId;

                //load the second commit
                lotRepository1.GetLot(lot.LotId, secondCommitId);

                lot.Reset(firstCommitId, userContext1);

                return ctx1;
            });

            "fires a reset event when reset".Assert(() =>
            {
                Assert.True(mockLotEventHandler.Event is ResetEvent);
            });

            "fires a reset event when reset, containing the new index".Assert(() =>
            {
                Assert.Equal(lot.Index, ((ResetEvent)mockLotEventHandler.Event).NewIndex);
            });

            "fires a reset event when reset, containing the lot".Assert(() =>
            {
                Assert.Equal(lot, ((ResetEvent)mockLotEventHandler.Event).Lot);
            });
        }
    }
}
