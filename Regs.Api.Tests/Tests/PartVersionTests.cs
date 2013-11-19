using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Common.Data;
using Common.Infrastructure;
using Gva.Web.App_Start;
using Newtonsoft.Json.Linq;
using Ninject;
using Regs.Api.Managers.LobManager;
using Regs.Api.Managers.LotManager;
using Regs.Api.Models;
using Regs.Api.Tests.Common;
using Xunit;
using Xunit.Extensions;

namespace Regs.Api.Tests.Tests
{
    public class PartVersionTests
    {
        private IKernel kernel;
        private IUnitOfWork unitOfWork;
        private ILotManager lotManager;
        private ILobManager lobManager;
        private UserContext userContext;

        public PartVersionTests()
        {
            this.kernel = new StandardKernel();

            NinjectConfig.RegisterServices(this.kernel);
            this.kernel.Unbind<IUserContextProvider>();
            this.kernel.Bind<IUserContextProvider>().To<MockUserContextProvider>();

            var services = this.kernel
                .Get<Tuple<IUnitOfWork, ILotManager, ILobManager, IUserContextProvider>>();

            this.unitOfWork = services.Item1;
            this.lotManager = services.Item2;
            this.lobManager = services.Item3;
            this.userContext = services.Item4.GetCurrentUserContext();
        }

        [Fact, AutoRollback]
        public void AddPart()
        {
            Set set = this.lotManager.GetSet("Person");

            Lot lot = set.AddLot(this.userContext);
            lot.AddPart(
                "/addresses/*",
                JObject.Parse(@"{ key1: 'value1', key2: [ 'value1', 'value2']}"),
                this.lobManager,
                this.userContext);
            this.unitOfWork.Save();

            PartVersion savedPartVersion = lot.GetPart("/addresses/0");

            Part part = this.unitOfWork.DbContext.Set<Part>().SingleOrDefault(p => p.Path == "/addresses/0");
            Assert.NotNull(part);
            TextBlob textBlob = this.unitOfWork.DbContext.Set<TextBlob>().FirstOrDefault(tb => tb.Hash == "876FB827A153BB2CB30FC25B082B7CB9D31290D8");
            Assert.NotNull(textBlob);
            Commit commit = lot.Commits.First();

            Assert.Equal<int>(part.PartId, savedPartVersion.PartId);
            Assert.Equal<int>(textBlob.TextBlobId, savedPartVersion.TextBlobId);
            Assert.Equal<int>(commit.CommitId, savedPartVersion.OriginalCommitId);
            Assert.Equal<int>(this.userContext.UserId, savedPartVersion.CreatorId);
            Assert.Equal<int>((int)PartOperation.Add, (int)savedPartVersion.PartOperation);
            Assert.Contains<PartVersion>(savedPartVersion, commit.PartVersions);
        }

        [Fact, AutoRollback]
        public void AddPartDeletedInPreviousCommit()
        {
            Set set = this.lotManager.GetSet("Person");

            Lot lot = set.AddLot(this.userContext);
            lot.AddPart(
                "/generalInfo",
                new JObject (),
                this.lobManager,
                this.userContext);
            this.unitOfWork.Save();
            lot.AddPart(
                "/addresses/*",
                new JObject (),
                this.lobManager,
                this.userContext);
            this.unitOfWork.Save();

            lot.Commit(this.userContext);
            this.unitOfWork.Save();

            lot.DeletePart("/generalInfo", this.userContext);
            this.unitOfWork.Save();

            lot.Commit(this.userContext);
            this.unitOfWork.Save();

            lot.AddPart(
                "/generalInfo",
                JObject.Parse(@"{ key1: 'value1', key2: [ 'value1', 'value2']}"),
                this.lobManager,
                this.userContext);
            this.unitOfWork.Save();

            PartVersion addedPartVersion = this.unitOfWork.DbContext.Set<PartVersion>()
                .OrderByDescending(pv => pv.PartVersionId)
                .FirstOrDefault();

            Part part = this.unitOfWork.DbContext.Set<Part>().SingleOrDefault(p => p.Path == "/generalInfo");
            Assert.NotNull(part);

            TextBlob textBlob = this.unitOfWork.DbContext.Set<TextBlob>().FirstOrDefault(tb => tb.Hash == "876FB827A153BB2CB30FC25B082B7CB9D31290D8");
            Assert.NotNull(textBlob);

            Commit index = this.unitOfWork.DbContext.Set<Commit>()
                .Include(c => c.PartVersions)
                .FirstOrDefault(c => c.IsIndex);

            PartVersion addedPV = index.PartVersions.OrderByDescending(pv => pv.PartVersionId).First();
            Assert.Equal<int>(part.PartId, addedPartVersion.PartId);
            Assert.Equal<int>(textBlob.TextBlobId, addedPartVersion.TextBlobId);
            Assert.Equal<int>(index.CommitId, addedPartVersion.OriginalCommitId);
            Assert.Equal<int>(this.userContext.UserId, addedPartVersion.CreatorId);
            Assert.Equal<int>((int)PartOperation.Add, (int)addedPartVersion.PartOperation);
            Assert.Contains<PartVersion>(addedPartVersion, index.PartVersions);
        }

        [Fact, AutoRollback]
        public void UpdatePart()
        {
            Set set = this.lotManager.GetSet("Person");

            Lot lot = set.AddLot(this.userContext);
            lot.AddPart(
                "/addresses/*",
                JObject.Parse(@"{ key1: 'value1', key2: [ 'value1', 'value2']}"),
                this.lobManager,
                this.userContext);
            this.unitOfWork.Save();
            lot.AddPart(
                "/addresses/*",
                JObject.Parse(@"{ key1: 'value1', key2: [ 'value1', 'value2']}"),
                this.lobManager,
                this.userContext);
            this.unitOfWork.Save();

            lot = this.lotManager.GetLot(lot.LotId);
            lot.Commit(this.userContext);
            this.unitOfWork.Save();

            lot.UpdatePart("/addresses/1", new JObject(), this.lobManager, this.userContext);
            this.unitOfWork.Save();

            Commit index = this.unitOfWork.DbContext.Set<Commit>()
                .Include(c => c.PartVersions)
                .FirstOrDefault(c => c.IsIndex);
            TextBlob textBlob = this.unitOfWork.DbContext.Set<TextBlob>().FirstOrDefault(tb => tb.Hash == "BF21A9E8FBC5A3846FB05B4FA0859E0917B2202F");

            PartVersion updatedPV = index.PartVersions.FirstOrDefault(pv => pv.PartOperation == PartOperation.Update);
            Assert.NotNull(updatedPV);
            Assert.Equal<int>(index.CommitId, updatedPV.OriginalCommitId);
            Assert.Equal<int>(textBlob.TextBlobId, updatedPV.TextBlobId);

            PartVersion oldPV = index.PartVersions.FirstOrDefault(pv => pv.PartId == updatedPV.PartId && pv.PartOperation != PartOperation.Update);
            Assert.Null(oldPV);
        }

        [Fact, AutoRollback]
        public void DeletePart()
        {
            Set set = this.lotManager.GetSet("Person");

            Lot lot = set.AddLot(this.userContext);
            lot.AddPart(
                "/addresses/*",
                JObject.Parse(@"{ key1: 'value1', key2: [ 'value1', 'value2']}"),
                this.lobManager,
                this.userContext);
            this.unitOfWork.Save();
            lot.AddPart(
                "/addresses/*",
                JObject.Parse(@"{ key1: 'value1', key2: [ 'value1', 'value2']}"),
                this.lobManager,
                this.userContext);
            this.unitOfWork.Save();

            lot.Commit(this.userContext);
            this.unitOfWork.Save();

            lot.DeletePart("/addresses/1", this.userContext);
            this.unitOfWork.Save();

            Commit index = this.unitOfWork.DbContext.Set<Commit>()
                .Include(c => c.PartVersions)
                .FirstOrDefault(c => c.IsIndex);

            PartVersion deletedPV = index.PartVersions.FirstOrDefault(pv => pv.PartOperation == PartOperation.Delete);
            Assert.NotNull(deletedPV);
            Assert.Equal<int>(index.CommitId, deletedPV.OriginalCommitId);

            PartVersion oldPV = index.PartVersions.FirstOrDefault(pv => pv.PartId == deletedPV.PartId && pv.PartOperation != PartOperation.Delete);
            Assert.Null(oldPV);

            lot.Commit(this.userContext);
            this.unitOfWork.Save();

            index = this.unitOfWork.DbContext.Set<Commit>()
                .Include(c => c.PartVersions)
                .FirstOrDefault(c => c.IsIndex);
            var indexPVs = index.PartVersions.ToArray();

            Assert.Equal<int>(1, indexPVs.Length);
            Assert.Equal<string>("/addresses/0", indexPVs[0].Part.Path);
        }

        [Fact, AutoRollback]
        public void ResetPart()
        {
            Set set = this.lotManager.GetSet("Person");

            Lot lot = set.AddLot(this.userContext);
            PartVersion pv1 = lot.AddPart(
                "/addresses/*",
                JObject.Parse(@"{ key1: 'value1', key2: [ 'value1', 'value2']}"),
                this.lobManager,
                this.userContext);
            this.unitOfWork.Save();
            PartVersion pv2 = lot.AddPart(
                "/addresses/*",
                JObject.Parse(@"{ key1: 'value1', key2: [ 'value1', 'value2']}"),
                this.lobManager,
                this.userContext);
            this.unitOfWork.Save();

            lot.Commit(this.userContext);
            this.unitOfWork.Save();

            PartVersion updatedPv1 = lot.UpdatePart("/addresses/0", new JObject(), this.lobManager, this.userContext);
            this.unitOfWork.Save();

            PartVersion deletedPv2 = lot.DeletePart("/addresses/1", this.userContext);
            this.unitOfWork.Save();

            PartVersion pv3 = lot.AddPart(
                "/addresses/*",
                JObject.Parse(@"{ key1: 'value1', key2: [ 'value1', 'value2']}"),
                this.lobManager,
                this.userContext);
            this.unitOfWork.Save();

            Commit index = this.unitOfWork.DbContext.Set<Commit>().FirstOrDefault(c => c.LotId == lot.LotId && c.IsIndex);
            IEnumerable<PartVersion> partVersions = this.unitOfWork.DbContext.Set<PartVersion>().Where(pv => pv.Commits.Any(c => c.CommitId == index.CommitId));
            Assert.Equal<int>(3, partVersions.Count());
            Assert.Contains<PartVersion>(updatedPv1, partVersions);
            Assert.Contains<PartVersion>(deletedPv2, partVersions);
            Assert.Contains<PartVersion>(pv3, partVersions);

            lot.ResetPart("/addresses/0");
            this.unitOfWork.Save();

            partVersions = this.unitOfWork.DbContext.Set<PartVersion>().Where(pv => pv.Commits.Any(c => c.CommitId == index.CommitId));
            Assert.Equal<int>(3, partVersions.Count());
            Assert.Contains<PartVersion>(pv1, partVersions);
            Assert.Contains<PartVersion>(deletedPv2, partVersions);
            Assert.Contains<PartVersion>(pv3, partVersions);

            lot.ResetPart("/addresses/1");
            this.unitOfWork.Save();

            partVersions = this.unitOfWork.DbContext.Set<PartVersion>().Where(pv => pv.Commits.Any(c => c.CommitId == index.CommitId));
            Assert.Equal<int>(3, partVersions.Count());
            Assert.Contains<PartVersion>(pv1, partVersions);
            Assert.Contains<PartVersion>(pv2, partVersions);
            Assert.Contains<PartVersion>(pv3, partVersions);

            lot.ResetPart("/addresses/2");
            this.unitOfWork.Save();

            partVersions = this.unitOfWork.DbContext.Set<PartVersion>().Where(pv => pv.Commits.Any(c => c.CommitId == index.CommitId));
            Assert.Equal<int>(2, partVersions.Count());
            Assert.Contains<PartVersion>(pv1, partVersions);
            Assert.Contains<PartVersion>(pv2, partVersions);
        }

        [Fact, AutoRollback]
        public void GetParts()
        {
            Set set = this.lotManager.GetSet("Person");

            Lot lot = set.AddLot(this.userContext);
            PartVersion pv1 = lot.AddPart(
                "/addresses/*",
                JObject.Parse(@"{ key1: 'value1', key2: [ 'value1', 'value2']}"),
                this.lobManager,
                this.userContext);
            this.unitOfWork.Save();
            PartVersion pv2 = lot.AddPart(
                "/addresses/*",
                JObject.Parse(@"{ key1: 'value1', key2: [ 'value1', 'value2']}"),
                this.lobManager,
                this.userContext);
            this.unitOfWork.Save();
            PartVersion pv3 = lot.AddPart(
                "/addresses/*",
                JObject.Parse(@"{ key1: 'value1', key2: [ 'value1', 'value2']}"),
                this.lobManager,
                this.userContext);
            this.unitOfWork.Save();

            lot.Commit(this.userContext);
            this.unitOfWork.Save();

            PartVersion updatedPv1 = lot.UpdatePart("/addresses/0", new JObject(), this.lobManager, this.userContext);
            this.unitOfWork.Save();

            PartVersion deletedPv2 = lot.DeletePart("/addresses/1", this.userContext);
            this.unitOfWork.Save();

            var partVersions = lot.GetParts();
            Assert.Equal<int>(2, partVersions.Count());
            Assert.Contains<PartVersion>(updatedPv1, partVersions);
            Assert.Contains<PartVersion>(pv3, partVersions);

            var addedPartVersions = lot.GetAddedParts();
            Assert.Equal<int>(1, addedPartVersions.Count());
            Assert.Contains<PartVersion>(pv3, addedPartVersions);

            var updatedPartVersions = lot.GetUpdatedParts();
            Assert.Equal<int>(1, updatedPartVersions.Count());
            Assert.Contains<PartVersion>(updatedPv1, updatedPartVersions);

            var deletedPartVersions = lot.GetDeletedParts();
            Assert.Equal<int>(1, deletedPartVersions.Count());
            Assert.Contains<PartVersion>(deletedPv2, deletedPartVersions);
        }
    }
}
