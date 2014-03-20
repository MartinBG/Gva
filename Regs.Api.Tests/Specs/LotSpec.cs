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
using Regs.Api.Models;
using Regs.Api.Repositories.LotRepositories;
using Regs.Api.Tests.Mocks;
using SubSpec;
using Xunit;
using Xunit.Extensions;
using Common.Tests;

namespace Regs.Api.Tests.Specs
{
    public class LotSpec
    {
        private IKernel kernel;

        public LotSpec()
        {
            this.kernel = new StandardKernel();

            Gva.Web.App_Start.NinjectConfig.RegisterServices(this.kernel);
            this.kernel.Unbind<IUserContextProvider>();
            this.kernel.Bind<IUserContextProvider>().To<MockUserContextProvider>();
        }

        [Specification, AutoRollback]
        public void NewLotSpec()
        {
            IUnitOfWork unitOfWork1 = null;
            ILotRepository lotRepository1 = null;
            UserContext userContext1 = null;

            IUnitOfWork unitOfWork2 = null;
            ILotRepository lotRepository2 = null;
            UserContext userContext2 = null;

            "A new lot".ContextFixture(() =>
            {
                var ctx1 = kernel.Get<DisposableTuple<IUnitOfWork, ILotRepository, IUserContextProvider>>();

                unitOfWork1 = ctx1.Item1;
                lotRepository1 = ctx1.Item2;
                userContext1 = ctx1.Item3.GetCurrentUserContext();

                var ctx2 = kernel.Get<DisposableTuple<IUnitOfWork, ILotRepository, IUserContextProvider>>();

                unitOfWork2 = ctx2.Item1;
                lotRepository2 = ctx2.Item2;
                userContext2 = ctx2.Item3.GetCurrentUserContext();

                return DisposableTuple.Create(ctx1, ctx2);
            });

            "can be saved empty".Assert(() =>
            {
                var lot1 = lotRepository1.GetSet("Person").CreateLot(userContext1);
                unitOfWork1.Save();

                var lot2 = lotRepository2.GetLotIndex(lot1.LotId);
                Assert.NotNull(lot2);
            });
            "can be saved with multiple parts in the index".Assert(() =>
            {
                var lot1 = lotRepository1.GetSet("Person").CreateLot(userContext1);
                lot1.CreatePart("/addresses/0", JObject.Parse("{ address: '0' }"), userContext1);
                lot1.UpdatePart("/addresses/0", JObject.Parse("{ address: '0-1' }"), userContext1);
                lot1.CreatePart("/addresses/1", JObject.Parse("{ address: '1' }"), userContext1);
                lot1.CreatePart("/addresses/2", JObject.Parse("{ address: '2'}"), userContext1);
                unitOfWork1.Save();

                var lot2 = lotRepository2.GetLotIndex(lot1.LotId);
                dynamic addresses = lot2.GetParts("/addresses/");
                Assert.Equal("0-1", (string)addresses[0].address);
                Assert.Equal("1", (string)addresses[1].address);
                Assert.Equal("2", (string)addresses[2].address);
            });
            "can be commited".Assert(() =>
            {
                var lot1 = lotRepository1.GetSet("Person").CreateLot(userContext1);
                lot1.CreatePart("/addresses/0", JObject.Parse("{ address: '0' }"), userContext1);
                lot1.Commit(userContext1);
                unitOfWork1.Save();

                var lot2 = lotRepository2.GetLot(lot1.LotId);
                dynamic address = lot2.GetPart("/addresses/0", lot2.LastCommit.CommitId);
                Assert.Equal("0", (string)address.address);
            });
            "can be commited partially".Assert(() =>
            {
                var lot1 = lotRepository1.GetSet("Person").CreateLot(userContext1);
                lot1.CreatePart("/addresses/0", JObject.Parse("{ address: '0' }"), userContext1);
                lot1.CreatePart("/addresses/1", JObject.Parse("{ address: '1' }"), userContext1);
                lot1.Commit(userContext1, new string[] { "/addresses/0"  });
                unitOfWork1.Save();

                var lot2 = lotRepository2.GetLot(lot1.LotId);
                dynamic address = lot2.GetPart("/addresses/1", lot2.LastCommit.CommitId);
                Assert.Null(address);
            });
            "cannot be commited without modifications".Assert(() =>
            {
                var lot1 = lotRepository1.GetSet("Person").CreateLot(userContext1);
                Assert.Throws<InvalidOperationException>(() => lot1.Commit(userContext1));
            });
            "must contain all latest versions of all parts after every commit in its index".Assert(() =>
            {
                var lot1 = lotRepository1.GetSet("Person").CreateLot(userContext1);
                lot1.CreatePart("/addresses/0", JObject.Parse("{ address: '0' }"), userContext1);
                lot1.Commit(userContext1, new string[] { "/addresses/0" });
                unitOfWork1.Save();
                lot1.CreatePart("/addresses/1", JObject.Parse("{ address: '1' }"), userContext1);
                lot1.Commit(userContext1, new string[] { "/addresses/1" });
                unitOfWork1.Save();

                var lot2 = lotRepository2.GetLotIndex(lot1.LotId);
                dynamic addresses = lot2.GetParts("/addresses/");
                Assert.Equal("0", (string)addresses[0].address);
                Assert.Equal("1", (string)addresses[1].address);
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
            UserContext userContext1 = null;

            IUnitOfWork unitOfWork2 = null;
            ILotRepository lotRepository2 = null;
            UserContext userContext2 = null;

            "An existing lot".ContextFixture(() =>
            {
                using (var tempCtx = kernel.Get<DisposableTuple<IUnitOfWork, ILotRepository, IUserContextProvider>>())
                {
                    var unitOfWork = tempCtx.Item1;
                    var lotRepository = tempCtx.Item2;
                    var userContext = tempCtx.Item3.GetCurrentUserContext();

                    var newLot = lotRepository.GetSet("Person").CreateLot(userContext);

                    newLot.CreatePart("/addresses/0", JObject.Parse("{ country: null }"), userContext);
                    newLot.CreatePart("/ratings/*", JObject.Parse("{ name: 'rating1' }"), userContext);
                    newLot.CreatePart("/ratings/*", JObject.Parse("{ name: 'rating2' }"), userContext);
                    newLot.Commit(userContext);

                    newLot.UpdatePart("/addresses/0", JObject.Parse("{ country: 'Austria' }"), userContext);
                    newLot.DeletePart("/ratings/0", userContext);
                    newLot.Commit(userContext);

                    newLot.UpdatePart("/addresses/0", JObject.Parse("{ country: 'Bulgaria' }"), userContext);
                    unitOfWork.Save();

                    var indexCommit = newLot.Commits.Where(c => c.IsIndex).Single();
                    var secondCommit = indexCommit.ParentCommit;

                    firstCommitId = secondCommit.ParentCommit.CommitId;
                    secondCommitId = secondCommit.CommitId;

                    lotId = newLot.LotId;
                }

                var ctx1 = kernel.Get<DisposableTuple<IUnitOfWork, ILotRepository, IUserContextProvider>>();

                unitOfWork1 = ctx1.Item1;
                lotRepository1 = ctx1.Item2;
                userContext1 = ctx1.Item3.GetCurrentUserContext();

                var ctx2 = kernel.Get<DisposableTuple<IUnitOfWork, ILotRepository, IUserContextProvider>>();

                unitOfWork2 = ctx2.Item1;
                lotRepository2 = ctx2.Item2;
                userContext2 = ctx2.Item3.GetCurrentUserContext();

                return DisposableTuple.Create(ctx1, ctx2);
            });

            "can have a part in its index updated".Assert(() =>
            {
                var lot1 = lotRepository1.GetLotIndex(lotId);
                dynamic address = lot1.GetPart("/addresses/0");
                address.country = "USA";
                lot1.UpdatePart("/addresses/0", address, userContext1);
                unitOfWork1.Save();

                var lot2 = lotRepository2.GetLotIndex(lotId);
                dynamic updatedAddress = lot2.GetPart("/addresses/0");
                Assert.Equal("USA", (string)updatedAddress.country);
            });
            "can have a commited part be deleted".Assert(() =>
            {
                var lot1 = lotRepository1.GetLotIndex(lotId);
                lot1.DeletePart("/ratings/1", userContext1);
                unitOfWork1.Save();

                var lot2 = lotRepository2.GetLotIndex(lotId);
                var deletedRating = lot2.GetPart("/ratings/1");
                Assert.Null(deletedRating);
            });
            "cannot have a part in its index deleted".Assert(() =>
            {
                var lot1 = lotRepository1.GetLotIndex(lotId);
                Assert.Throws<InvalidOperationException>(() => lot1.DeletePart("/addresses/0", userContext1));
            });
            "can have a part previously deleted be created again".Assert(() =>
            {
                var lot1 = lotRepository1.GetLotIndex(lotId);
                lot1.CreatePart("/ratings/0", JObject.Parse("{ name: 'rating1-1' }"), userContext1);
                unitOfWork1.Save();

                var lot2 = lotRepository2.GetLotIndex(lotId);
                dynamic createdRating = lot2.GetPart("/ratings/0");
                Assert.Equal("rating1-1", (string)createdRating.name);
            });
            "can have a part in its index reset".Assert(() =>
            {
                var lot1 = lotRepository1.GetLotIndex(lotId);

                //load the second commit
                lotRepository1.GetLot(lotId, secondCommitId);

                lot1.ResetPart("/addresses/0");
                unitOfWork1.Save();

                var lot2 = lotRepository2.GetLotIndex(lotId);
                dynamic resetAddress = lot2.GetPart("/addresses/0");
                Assert.Equal("Austria", (string)resetAddress.country);
            });
            "cannot have a part in its index reset if the last commit is not loaded".Assert(() =>
            {
                var lot1 = lotRepository1.GetLotIndex(lotId);
                Assert.Throws<InvalidOperationException>(() => lot1.ResetPart("/addresses/0"));
            });
            "cannot be modified concurrently".Assert(() =>
            {
                //get and modify the lot
                var lot1 = lotRepository1.GetLotIndex(lotId);
                dynamic address = lot1.GetPart("/addresses/0");
                address.country = "USA";
                lot1.UpdatePart("/addresses/0", address, userContext1);

                //get and modify the lot from a different context
                Lot lot2 = lotRepository2.GetLotIndex(lotId);
                lot2.CreatePart("/ratings/2", JObject.Parse(@"{}"), userContext2);
                unitOfWork2.Save();

                //saving the first retrieved lot should fail
                Assert.Throws<DbUpdateConcurrencyException>(() => unitOfWork1.Save());
            });
            "can be reset to a previous commit".Assert(() =>
            {
                var lot1 = lotRepository1.GetLotIndex(lotId);
                lot1.Commit(userContext1);

                //load the second commit
                lotRepository1.GetLot(lotId, secondCommitId);

                lot1.Reset(firstCommitId, userContext1);
                unitOfWork1.Save();

                var lot2 = lotRepository2.GetLot(lotId);
                dynamic previousAddress = lot2.GetPart("/addresses/0", lot2.LastCommit.CommitId);
                Assert.Null((string)previousAddress.country);
            });
            "cannot be reset to a previous commit if all later commits have not been loaded".Assert(() =>
            {
                var lot1 = lotRepository1.GetLotIndex(lotId);
                lot1.Commit(userContext1);
                Assert.Throws<InvalidOperationException>(() => lot1.Reset(firstCommitId, userContext1));
            });
            "must have all latest versions of all existing parts in its index after reset".Assert(() => 
            {
                var lot1 = lotRepository1.GetLotIndex(lotId);
                lot1.Commit(userContext1);

                //load the second commit
                lotRepository1.GetLot(lotId, secondCommitId);

                lot1.Reset(secondCommitId, userContext1);
                unitOfWork1.Save();

                lotRepository1.GetLot(lotId, firstCommitId);
                var lot2 = lotRepository2.GetLotIndex(lotId);
                dynamic resetAddress = lot2.GetPart("/addresses/0");
                Assert.Equal("Austria", (string)resetAddress.country);
            });
        }
    }
}
