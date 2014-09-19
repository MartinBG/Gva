using System;
using System.Data.Entity.Infrastructure;
using System.Linq;
using Autofac;
using Common.Api.UserContext;
using Common.Data;
using Common.Tests;
using Gva.Api.ModelsDO.Persons;
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

                var lot2 = lotRepository2.GetLotIndex(lot1.LotId, fullAccess: true);
                Assert.NotNull(lot2);
            });
            "can be saved with multiple parts in the index".Assert(() =>
            {
                var lot1 = lotRepository1.CreateLot("Person");
                lot1.CreatePart("personAddresses/0", new PersonAddressDO { Address = "0" }, userContext1);
                lot1.UpdatePart("personAddresses/0", new PersonAddressDO { Address = "0-1" }, userContext1);
                lot1.CreatePart("personAddresses/1", new PersonAddressDO { Address = "1" }, userContext1);
                lot1.CreatePart("personAddresses/2", new PersonAddressDO { Address = "2" }, userContext1);
                unitOfWork1.Save();

                var lot2 = lotRepository2.GetLotIndex(lot1.LotId, fullAccess: true);
                var addresses = lot2.Index.GetParts<PersonAddressDO>("personAddresses");
                Assert.Equal("0-1", addresses[0].Content.Address);
                Assert.Equal("1", addresses[1].Content.Address);
                Assert.Equal("2", addresses[2].Content.Address);
            });
            "can be commited".Assert(() =>
            {
                var lot1 = lotRepository1.CreateLot("Person");
                lot1.CreatePart("personAddresses/0", new PersonAddressDO { Address = "0" }, userContext1);
                lot1.Commit(userContext1, lotEventDispatcher1);
                unitOfWork1.Save();

                var lot2 = lotRepository2.GetLot(lot1.LotId, fullAccess: true);
                var address = lot2.LastCommit.GetPart<PersonAddressDO>("personAddresses/0").Content;
                Assert.Equal("0", address.Address);
            });
            "can be commited partially".Assert(() =>
            {
                var lot1 = lotRepository1.CreateLot("Person");
                lot1.CreatePart("personAddresses/0", new PersonAddressDO { Address = "0" }, userContext1);
                lot1.CreatePart("personAddresses/1", new PersonAddressDO { Address = "1" }, userContext1);
                lot1.Commit(userContext1, lotEventDispatcher1, new string[] { "personAddresses/0" });
                unitOfWork1.Save();

                var lot2 = lotRepository2.GetLot(lot1.LotId, fullAccess: true);
                var addressPart = lot2.LastCommit.GetPart<PersonAddressDO>("personAddresses/1");
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
                lot1.CreatePart("personAddresses/0", new PersonAddressDO { Address = "0" }, userContext1);
                lot1.Commit(userContext1, lotEventDispatcher1, new string[] { "personAddresses/0" });
                unitOfWork1.Save();
                lot1.CreatePart("personAddresses/1", new PersonAddressDO { Address = "1" }, userContext1);
                lot1.Commit(userContext1, lotEventDispatcher1, new string[] { "personAddresses/1" });
                unitOfWork1.Save();

                var lot2 = lotRepository2.GetLotIndex(lot1.LotId, fullAccess: true);
                var addresses = lot2.Index.GetParts<PersonAddressDO>("personAddresses");
                Assert.Equal("0", addresses[0].Content.Address);
                Assert.Equal("1", addresses[1].Content.Address);
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

                    newLot.CreatePart("personAddresses/0", new PersonAddressDO { Address = null }, userContext);
                    newLot.CreatePart("personStatuses/*", new PersonStatusDO { Notes = "status1" }, userContext);
                    newLot.CreatePart("personStatuses/*", new PersonStatusDO { Notes = "status2" }, userContext);
                    newLot.Commit(userContext, lotEventDispatcher);

                    newLot.UpdatePart("personAddresses/0", new PersonAddressDO { Address = "Austria" }, userContext);
                    newLot.DeletePart<PersonStatusDO>("personStatuses/1", userContext);
                    newLot.Commit(userContext, lotEventDispatcher);

                    newLot.UpdatePart("personAddresses/0", new PersonAddressDO { Address = "Bulgaria" }, userContext);
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
                var lot1 = lotRepository1.GetLotIndex(lotId, fullAccess: true);
                var address = lot1.Index.GetPart<PersonAddressDO>("personAddresses/0").Content;
                address.Address = "USA";
                lot1.UpdatePart("personAddresses/0", address, userContext1);
                unitOfWork1.Save();

                var lot2 = lotRepository2.GetLotIndex(lotId, fullAccess: true);
                var updatedAddress = lot2.Index.GetPart<PersonAddressDO>("personAddresses/0").Content;
                Assert.Equal("USA", updatedAddress.Address);
            });
            "can have a commited part be deleted".Assert(() =>
            {
                var lot1 = lotRepository1.GetLotIndex(lotId, fullAccess: true);
                lot1.DeletePart<PersonStatusDO>("personStatuses/2", userContext1);
                unitOfWork1.Save();

                var lot2 = lotRepository2.GetLotIndex(lotId, fullAccess: true);
                var deletedStatus = lot2.Index.GetPart<PersonStatusDO>("personStatuses/2");
                Assert.Null(deletedStatus);
            });
            "cannot have a part in its index deleted".Assert(() =>
            {
                var lot1 = lotRepository1.GetLotIndex(lotId, fullAccess: true);
                Assert.Throws<InvalidOperationException>(() => lot1.DeletePart<PersonAddressDO>("personAddresses/0", userContext1));
            });
            "can have a part previously deleted be created again".Assert(() =>
            {
                var lot1 = lotRepository1.GetLotIndex(lotId, fullAccess: true);
                lot1.CreatePart("personStatuses/1", new PersonStatusDO { Notes = "status1-1" }, userContext1);
                unitOfWork1.Save();

                var lot2 = lotRepository2.GetLotIndex(lotId, fullAccess: true);
                var createdStatus = lot2.Index.GetPart<PersonStatusDO>("personStatuses/1").Content;
                Assert.Equal("status1-1", createdStatus.Notes);
            });
            "can have a part in its index reset".Assert(() =>
            {
                var lot1 = lotRepository1.GetLotIndex(lotId, fullAccess: true);

                //load the second commit
                lotRepository1.GetLot(lotId, secondCommitId, fullAccess: true);

                lot1.ResetPart<PersonAddressDO>("personAddresses/0");
                unitOfWork1.Save();

                var lot2 = lotRepository2.GetLotIndex(lotId, fullAccess: true);
                var resetAddress = lot2.Index.GetPart<PersonAddressDO>("personAddresses/0").Content;
                Assert.Equal("Austria", resetAddress.Address);
            });
            "cannot have a part in its index reset if the last commit is not loaded".Assert(() =>
            {
                var lot1 = lotRepository1.GetLotIndex(lotId, fullAccess: true);
                Assert.Throws<InvalidOperationException>(() => lot1.ResetPart<PersonAddressDO>("personAddresses/0"));
            });
            "cannot be modified concurrently".Assert(() =>
            {
                //get and modify the lot
                var lot1 = lotRepository1.GetLotIndex(lotId, fullAccess: true);
                var address = lot1.Index.GetPart<PersonAddressDO>("personAddresses/0").Content;
                address.Address = "USA";
                lot1.UpdatePart("personAddresses/0", address, userContext1);

                //get and modify the lot from a different context
                Lot lot2 = lotRepository2.GetLotIndex(lotId, fullAccess: true);
                lot2.CreatePart("personStatuses/3", new PersonStatusDO(), userContext2);
                unitOfWork2.Save();

                //saving the first retrieved lot should fail
                Assert.Throws<DbUpdateConcurrencyException>(() => unitOfWork1.Save());
            });
            "can be reset to a previous commit".Assert(() =>
            {
                var lot1 = lotRepository1.GetLotIndex(lotId, fullAccess: true);
                lot1.Commit(userContext1, lotEventDispatcher1);

                //load the second commit
                lotRepository1.GetLot(lotId, secondCommitId, fullAccess: true);

                lot1.Reset(firstCommitId, userContext1, lotEventDispatcher1);
                unitOfWork1.Save();

                var lot2 = lotRepository2.GetLot(lotId, fullAccess: true);
                var previousAddress = lot2.LastCommit.GetPart<PersonAddressDO>("personAddresses/0").Content;
                Assert.Null(previousAddress.Address);
            });
            "cannot be reset to a previous commit if all later commits have not been loaded".Assert(() =>
            {
                var lot1 = lotRepository1.GetLotIndex(lotId, fullAccess: true);
                lot1.Commit(userContext1, lotEventDispatcher1);
                Assert.Throws<InvalidOperationException>(() => lot1.Reset(firstCommitId, userContext1, lotEventDispatcher1));
            });
            "must have all latest versions of all existing parts in its index after reset".Assert(() => 
            {
                var lot1 = lotRepository1.GetLotIndex(lotId, fullAccess: true);
                lot1.Commit(userContext1, lotEventDispatcher1);

                //load the second commit
                lotRepository1.GetLot(lotId, secondCommitId, fullAccess: true);

                lot1.Reset(secondCommitId, userContext1, lotEventDispatcher1);
                unitOfWork1.Save();

                lotRepository1.GetLot(lotId, firstCommitId, fullAccess: true);
                var lot2 = lotRepository2.GetLotIndex(lotId, fullAccess: true);
                var resetAddress = lot2.Index.GetPart<PersonAddressDO>("personAddresses/0").Content;
                Assert.Equal("Austria", resetAddress.Address);
            });
        }
    }
}
