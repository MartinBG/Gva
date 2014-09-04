using System;
using System.Data.Entity.Infrastructure;
using System.Linq;
using Autofac;
using Common.Api.Models;
using Common.Api.UserContext;
using Common.Data;
using Common.Tests;
using Newtonsoft.Json.Linq;
using Regs.Api.LotEvents;
using Regs.Api.Models;
using Regs.Api.Repositories.LotRepositories;
using SubSpec;
using Xunit;
using Xunit.Extensions;

namespace Regs.Api.Tests.Specs
{
    public class LotSpec
    {
        private IContainer container;

        public LotSpec()
        {
            ContainerBuilder builder = new ContainerBuilder();
            builder.RegisterType<RegsDbConfiguration>().As<IDbConfiguration>().SingleInstance();
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerLifetimeScope();
            builder.RegisterType<LotRepository>().As<ILotRepository>();
            builder.RegisterType<LotEventDispatcher>().As<ILotEventDispatcher>();
            builder.Register(c => new UserContext(1)).SingleInstance();
            this.container = builder.Build();
        }

        [Specification, AutoRollback]
        public void NewLotSpec()
        {
            IUnitOfWork unitOfWork1 = null;
            ILotRepository lotRepository1 = null;
            ILotEventDispatcher lotEventDispatcher1 = null;
            UserContext userContext1 = null;

            IUnitOfWork unitOfWork2 = null;
            ILotRepository lotRepository2 = null;
            ILotEventDispatcher lotEventDispatcher2 = null;
            UserContext userContext2 = null;

            "A new lot".ContextFixture(() =>
            {
                var lf1 = container.BeginLifetimeScope();
                unitOfWork1 = lf1.Resolve<IUnitOfWork>();
                lotRepository1 = lf1.Resolve<ILotRepository>();
                lotEventDispatcher1 = lf1.Resolve<ILotEventDispatcher>();
                userContext1 = lf1.Resolve<UserContext>();

                var lf2 = container.BeginLifetimeScope();
                unitOfWork2 = lf2.Resolve<IUnitOfWork>();
                lotRepository2 = lf2.Resolve<ILotRepository>();
                lotEventDispatcher2 = lf2.Resolve<ILotEventDispatcher>();
                userContext2 = lf2.Resolve<UserContext>();

                return DisposableTuple.Create(lf1, lf2);
            });

            "can be saved empty".Assert(() =>
            {
                var lot1 = lotRepository1.CreateLot("Person");
                unitOfWork1.Save();

                var lot2 = lotRepository2.GetLotIndex(lot1.LotId);
                Assert.NotNull(lot2);
            });
            "can be saved with multiple parts in the index".Assert(() =>
            {
                var lot1 = lotRepository1.CreateLot("Person");
                lot1.CreatePart("personAddresses/0", JObject.Parse("{ address: '0' }"), userContext1);
                lot1.UpdatePart("personAddresses/0", JObject.Parse("{ address: '0-1' }"), userContext1);
                lot1.CreatePart("personAddresses/1", JObject.Parse("{ address: '1' }"), userContext1);
                lot1.CreatePart("personAddresses/2", JObject.Parse("{ address: '2'}"), userContext1);
                unitOfWork1.Save();

                var lot2 = lotRepository2.GetLotIndex(lot1.LotId);
                dynamic addresses = lot2.Index.GetParts("personAddresses");
                Assert.Equal("0-1", (string)addresses[0].Content.address);
                Assert.Equal("1", (string)addresses[1].Content.address);
                Assert.Equal("2", (string)addresses[2].Content.address);
            });
            "can be commited".Assert(() =>
            {
                var lot1 = lotRepository1.CreateLot("Person");
                lot1.CreatePart("personAddresses/0", JObject.Parse("{ address: '0' }"), userContext1);
                lot1.Commit(userContext1, lotEventDispatcher1);
                unitOfWork1.Save();

                var lot2 = lotRepository2.GetLot(lot1.LotId);
                dynamic address = lot2.LastCommit.GetPart("personAddresses/0").Content;
                Assert.Equal("0", (string)address.address);
            });
            "can be commited partially".Assert(() =>
            {
                var lot1 = lotRepository1.CreateLot("Person");
                lot1.CreatePart("personAddresses/0", JObject.Parse("{ address: '0' }"), userContext1);
                lot1.CreatePart("personAddresses/1", JObject.Parse("{ address: '1' }"), userContext1);
                lot1.Commit(userContext1, lotEventDispatcher1, new string[] { "personAddresses/0" });
                unitOfWork1.Save();

                var lot2 = lotRepository2.GetLot(lot1.LotId);
                dynamic addressPart = lot2.LastCommit.GetPart("personAddresses/1");
                Assert.Null(addressPart);
            });
            "cannot be commited without modifications".Assert(() =>
            {
                var lot1 = lotRepository1.CreateLot("Person");
                Assert.Throws<InvalidOperationException>(() => lot1.Commit(userContext1, lotEventDispatcher1));
            });
            "must contain all latest versions of all parts after every commit in its index".Assert(() =>
            {
                var lot1 = lotRepository1.CreateLot("Person");
                lot1.CreatePart("personAddresses/0", JObject.Parse("{ address: '0' }"), userContext1);
                lot1.Commit(userContext1, lotEventDispatcher1, new string[] { "personAddresses/0" });
                unitOfWork1.Save();
                lot1.CreatePart("personAddresses/1", JObject.Parse("{ address: '1' }"), userContext1);
                lot1.Commit(userContext1, lotEventDispatcher1, new string[] { "personAddresses/1" });
                unitOfWork1.Save();

                var lot2 = lotRepository2.GetLotIndex(lot1.LotId);
                dynamic addresses = lot2.Index.GetParts("personAddresses");
                Assert.Equal("0", (string)addresses[0].Content.address);
                Assert.Equal("1", (string)addresses[1].Content.address);
            });
        }

        [Specification, AutoRollback]
        public void SavedLotSpec()
        {
            int lotId = 0;
            int firstCommitId = 0;
            int secondCommitId = 0;

            IUnitOfWork unitOfWork1 = null;
            ILotRepository lotRepository1 = null;
            ILotEventDispatcher lotEventDispatcher1 = null;
            UserContext userContext1 = null;

            IUnitOfWork unitOfWork2 = null;
            ILotRepository lotRepository2 = null;
            ILotEventDispatcher lotEventDispatcher2 = null;
            UserContext userContext2 = null;

            "An existing lot".ContextFixture(() =>
            {
                using (var lf = container.BeginLifetimeScope())
                {
                    var unitOfWork = lf.Resolve<IUnitOfWork>();
                    var lotRepository = lf.Resolve<ILotRepository>();
                    var lotEventDispatcher = lf.Resolve<ILotEventDispatcher>();
                    var userContext = lf.Resolve<UserContext>();

                    var newLot = lotRepository.CreateLot("Person");

                    newLot.CreatePart("personAddresses/0", JObject.Parse("{ country: null }"), userContext);
                    newLot.CreatePart("personStatuses/*", JObject.Parse("{ name: 'status1' }"), userContext);
                    newLot.CreatePart("personStatuses/*", JObject.Parse("{ name: 'status2' }"), userContext);
                    newLot.Commit(userContext, lotEventDispatcher);

                    newLot.UpdatePart("personAddresses/0", JObject.Parse("{ country: 'Austria' }"), userContext);
                    newLot.DeletePart("personStatuses/0", userContext);
                    newLot.Commit(userContext, lotEventDispatcher);

                    newLot.UpdatePart("personAddresses/0", JObject.Parse("{ country: 'Bulgaria' }"), userContext);
                    unitOfWork.Save();

                    var indexCommit = newLot.Commits.Where(c => c.IsIndex).Single();
                    var secondCommit = indexCommit.ParentCommit;

                    firstCommitId = secondCommit.ParentCommit.CommitId;
                    secondCommitId = secondCommit.CommitId;

                    lotId = newLot.LotId;
                }

                var lf1 = container.BeginLifetimeScope();
                unitOfWork1 = lf1.Resolve<IUnitOfWork>();
                lotRepository1 = lf1.Resolve<ILotRepository>();
                lotEventDispatcher1 = lf1.Resolve<ILotEventDispatcher>();
                userContext1 = lf1.Resolve<UserContext>();

                var lf2 = container.BeginLifetimeScope();
                unitOfWork2 = lf2.Resolve<IUnitOfWork>();
                lotRepository2 = lf2.Resolve<ILotRepository>();
                lotEventDispatcher2 = lf2.Resolve<ILotEventDispatcher>();
                userContext2 = lf2.Resolve<UserContext>();

                return DisposableTuple.Create(lf1, lf2);
            });

            "can have a part in its index updated".Assert(() =>
            {
                var lot1 = lotRepository1.GetLotIndex(lotId);
                dynamic address = lot1.Index.GetPart("personAddresses/0").Content;
                address.country = "USA";
                lot1.UpdatePart("personAddresses/0", address, userContext1);
                unitOfWork1.Save();

                var lot2 = lotRepository2.GetLotIndex(lotId);
                dynamic updatedAddress = lot2.Index.GetPart("personAddresses/0").Content;
                Assert.Equal("USA", (string)updatedAddress.country);
            });
            "can have a commited part be deleted".Assert(() =>
            {
                var lot1 = lotRepository1.GetLotIndex(lotId);
                lot1.DeletePart("personStatuses/1", userContext1);
                unitOfWork1.Save();

                var lot2 = lotRepository2.GetLotIndex(lotId);
                var deletedRating = lot2.Index.GetPart("personStatuses/1");
                Assert.Null(deletedRating);
            });
            "cannot have a part in its index deleted".Assert(() =>
            {
                var lot1 = lotRepository1.GetLotIndex(lotId);
                Assert.Throws<InvalidOperationException>(() => lot1.DeletePart("personAddresses/0", userContext1));
            });
            "can have a part previously deleted be created again".Assert(() =>
            {
                var lot1 = lotRepository1.GetLotIndex(lotId);
                lot1.CreatePart("personStatuses/0", JObject.Parse("{ name: 'status1-1' }"), userContext1);
                unitOfWork1.Save();

                var lot2 = lotRepository2.GetLotIndex(lotId);
                dynamic createdRating = lot2.Index.GetPart("personStatuses/0").Content;
                Assert.Equal("status1-1", (string)createdRating.name);
            });
            "can have a part in its index reset".Assert(() =>
            {
                var lot1 = lotRepository1.GetLotIndex(lotId);

                //load the second commit
                lotRepository1.GetLot(lotId, secondCommitId);

                lot1.ResetPart("personAddresses/0");
                unitOfWork1.Save();

                var lot2 = lotRepository2.GetLotIndex(lotId);
                dynamic resetAddress = lot2.Index.GetPart("personAddresses/0").Content;
                Assert.Equal("Austria", (string)resetAddress.country);
            });
            "cannot have a part in its index reset if the last commit is not loaded".Assert(() =>
            {
                var lot1 = lotRepository1.GetLotIndex(lotId);
                Assert.Throws<InvalidOperationException>(() => lot1.ResetPart("personAddresses/0"));
            });
            "cannot be modified concurrently".Assert(() =>
            {
                //get and modify the lot
                var lot1 = lotRepository1.GetLotIndex(lotId);
                dynamic address = lot1.Index.GetPart("personAddresses/0").Content;
                address.country = "USA";
                lot1.UpdatePart("personAddresses/0", address, userContext1);

                //get and modify the lot from a different context
                Lot lot2 = lotRepository2.GetLotIndex(lotId);
                lot2.CreatePart("personStatuses/2", JObject.Parse(@"{}"), userContext2);
                unitOfWork2.Save();

                //saving the first retrieved lot should fail
                Assert.Throws<DbUpdateConcurrencyException>(() => unitOfWork1.Save());
            });
            "can be reset to a previous commit".Assert(() =>
            {
                var lot1 = lotRepository1.GetLotIndex(lotId);
                lot1.Commit(userContext1, lotEventDispatcher1);

                //load the second commit
                lotRepository1.GetLot(lotId, secondCommitId);

                lot1.Reset(firstCommitId, userContext1, lotEventDispatcher1);
                unitOfWork1.Save();

                var lot2 = lotRepository2.GetLot(lotId);
                dynamic previousAddress = lot2.LastCommit.GetPart("personAddresses/0").Content;
                Assert.Null((string)previousAddress.country);
            });
            "cannot be reset to a previous commit if all later commits have not been loaded".Assert(() =>
            {
                var lot1 = lotRepository1.GetLotIndex(lotId);
                lot1.Commit(userContext1, lotEventDispatcher1);
                Assert.Throws<InvalidOperationException>(() => lot1.Reset(firstCommitId, userContext1, lotEventDispatcher1));
            });
            "must have all latest versions of all existing parts in its index after reset".Assert(() => 
            {
                var lot1 = lotRepository1.GetLotIndex(lotId);
                lot1.Commit(userContext1, lotEventDispatcher1);

                //load the second commit
                lotRepository1.GetLot(lotId, secondCommitId);

                lot1.Reset(secondCommitId, userContext1, lotEventDispatcher1);
                unitOfWork1.Save();

                lotRepository1.GetLot(lotId, firstCommitId);
                var lot2 = lotRepository2.GetLotIndex(lotId);
                dynamic resetAddress = lot2.Index.GetPart("personAddresses/0").Content;
                Assert.Equal("Austria", (string)resetAddress.country);
            });
        }
    }
}
