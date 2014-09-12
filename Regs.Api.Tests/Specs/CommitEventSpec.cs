using System.Linq;
using Autofac;
using Common.Api.UserContext;
using Common.Data;
using Newtonsoft.Json.Linq;
using Regs.Api.LotEvents;
using Regs.Api.Models;
using Regs.Api.Repositories.LotRepositories;
using Regs.Api.Tests.Mocks;
using SubSpec;
using Xunit;
using Xunit.Extensions;

namespace Regs.Api.Tests.Specs
{
    public class EventsSpec
    {
        private IContainer container;

        public EventsSpec()
        {
            ContainerBuilder builder = new ContainerBuilder();
            builder.RegisterType<RegsDbConfiguration>().As<IDbConfiguration>().SingleInstance();
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerLifetimeScope();
            builder.RegisterType<LotRepository>().As<ILotRepository>();
            builder.RegisterType<LotEventDispatcher>().As<ILotEventDispatcher>();
            builder.RegisterType<MockLotEventHandler>().As<ILotEventHandler>().InstancePerLifetimeScope();
            builder.Register(c => new UserContext(1));
            this.container = builder.Build();
        }

        [Specification, AutoRollback]
        public void CommitEventSpec()
        {
            IUnitOfWork unitOfWork = null;
            ILotRepository lotRepository = null;
            MockLotEventHandler mockLotEventHandler = null;
            ILotEventDispatcher lotEventDispatcher = null;
            UserContext userContext = null;
            Lot lot = null;

            "A lot".ContextFixture(() =>
            {
                var lf = container.BeginLifetimeScope();
                unitOfWork = lf.Resolve<IUnitOfWork>();
                lotRepository = lf.Resolve<ILotRepository>();
                mockLotEventHandler = (MockLotEventHandler)lf.Resolve<ILotEventHandler>();
                lotEventDispatcher = lf.Resolve<ILotEventDispatcher>();
                userContext = lf.Resolve<UserContext>();

                lot = lotRepository.CreateLot("Person");
                lot.CreatePart("personAddresses/0", JObject.Parse("{ address: '0' }"), userContext);
                lot.Commit(userContext, lotEventDispatcher);

                return lf;
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
            IUnitOfWork unitOfWork = null;
            ILotRepository lotRepository = null;
            MockLotEventHandler mockLotEventHandler = null;
            ILotEventDispatcher lotEventDispatcher = null;
            UserContext userContext = null;
            Lot lot = null;

            "A lot".ContextFixture(() =>
            {
                var lf = container.BeginLifetimeScope();
                unitOfWork = lf.Resolve<IUnitOfWork>();
                lotRepository = lf.Resolve<ILotRepository>();
                mockLotEventHandler = (MockLotEventHandler)lf.Resolve<ILotEventHandler>();
                lotEventDispatcher = lf.Resolve<ILotEventDispatcher>();
                userContext = lf.Resolve<UserContext>();

                lot = lotRepository.CreateLot("Person");
                lot.CreatePart("personAddresses/0", JObject.Parse("{ address: '0' }"), userContext);
                lot.Commit(userContext, lotEventDispatcher);

                lot.CreatePart("personAddresses/1", JObject.Parse("{ address: '1' }"), userContext);
                lot.Commit(userContext, lotEventDispatcher, new string[] { "personAddresses/1" });
                unitOfWork.Save();

                var indexCommit = lot.Commits.Where(c => c.IsIndex).Single();
                var secondCommit = indexCommit.ParentCommit;

                int firstCommitId = secondCommit.ParentCommit.CommitId;
                int secondCommitId = secondCommit.CommitId;

                //load the second commit
                lotRepository.GetLot(lot.LotId, secondCommitId, fullAccess: true);

                lot.Reset(firstCommitId, userContext, lotEventDispatcher);

                return lf;
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
