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
    public class LotTests
    {
        private IKernel kernel;
        private IUnitOfWork unitOfWork;
        private ILotManager lotManager;
        private ILobManager lobManager;
        private UserContext userContext;

        public LotTests()
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
        public void CreateNewLot()
        {
            Set set = this.lotManager.GetSet("Person");

            Lot createdLot = set.AddLot(this.userContext);
            this.unitOfWork.Save();

            Lot savedLot = this.lotManager.GetLot(createdLot.LotId);

            Assert.Equal<int>(0, savedLot.NextIndex);
            Assert.Equal<int>(1, savedLot.Commits.Count);

            Commit savedCommit = savedLot.Commits.First();

            Assert.Equal<int>(this.userContext.UserId, savedCommit.CommiterId);
            Assert.True(savedCommit.IsIndex);
        }

        [Fact, AutoRollback]
        public void CommitAllParts()
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

            var commits = this.unitOfWork.DbContext.Set<Commit>()
                .Include(c => c.PartVersions)
                .Where(c => c.LotId == lot.LotId)
                .OrderByDescending(c => c.CommitId)
                .ToArray();
            Assert.Equal<int>(2, commits.Count());

            Commit newCommit = commits[0];
            Commit oldCommit = commits[1];

            Assert.Equal<int>(oldCommit.CommitId, newCommit.ParentCommitId.Value);
            Assert.Equal<int>(this.userContext.UserId, newCommit.CommiterId);
            Assert.True(newCommit.IsIndex);
            Assert.False(oldCommit.IsIndex);

            var indexPV = newCommit.PartVersions.ToArray();
            Assert.Equal<int>(2, indexPV.Length);
            Assert.Equal<int>(oldCommit.CommitId, indexPV[0].OriginalCommitId);
            Assert.Equal<int>(oldCommit.CommitId, indexPV[1].OriginalCommitId);
        }

        [Fact, AutoRollback]
        public void CommitOnePart()
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

            lot.Commit(this.userContext, new List<string> { "/addresses/1" });
            this.unitOfWork.Save();

            var commits = this.unitOfWork.DbContext.Set<Commit>()
                .Include(c => c.PartVersions)
                .Where(c => c.LotId == lot.LotId)
                .OrderByDescending(c => c.CommitId)
                .ToArray();
            Assert.Equal<int>(2, commits.Count());

            Commit newCommit = commits[0];
            Commit oldCommit = commits[1];

            Assert.Equal<int>(oldCommit.CommitId, newCommit.ParentCommitId.Value);
            Assert.Equal<int>(this.userContext.UserId, newCommit.CommiterId);
            Assert.True(newCommit.IsIndex);
            Assert.False(oldCommit.IsIndex);

            var indexPV = newCommit.PartVersions.OrderBy(pv => pv.PartVersionId).ToArray();
            Assert.Equal<int>(2, indexPV.Length);
            Assert.Equal<int>(newCommit.CommitId, indexPV[0].OriginalCommitId);
            Assert.Equal<int>(oldCommit.CommitId, indexPV[1].OriginalCommitId);

            var commitedPv = oldCommit.PartVersions.ToArray();
            Assert.Equal<int>(1, commitedPv.Length);
            Assert.Equal<string>("/addresses/1", commitedPv[0].Part.Path);
        }

        [Fact, AutoRollback]
        public void Reset()
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

            PartVersion deletedPv2 = lot.DeletePart("/addresses/1", this.userContext);
            this.unitOfWork.Save();

            lot.Commit(this.userContext);
            this.unitOfWork.Save();

            PartVersion pv3 = lot.AddPart(
                "/ratings/*",
                JObject.Parse(@"{ key1: 'value1', key2: [ 'value1', 'value2']}"),
                this.lobManager,
                this.userContext);
            this.unitOfWork.Save();
            PartVersion pv4 = lot.AddPart(
                "/addresses/*",
                JObject.Parse(@"{ key1: 'value1', key2: [ 'value1', 'value2']}"),
                this.lobManager,
                this.userContext);
            this.unitOfWork.Save();
            PartVersion updatedPv1 = lot.UpdatePart("/addresses/0", new JObject(), this.lobManager, this.userContext);
            this.unitOfWork.Save();

            lot.Commit(this.userContext);
            this.unitOfWork.Save();

            var parts = this.unitOfWork.DbContext.Set<Part>()
                .Where(p => p.LotId == lot.LotId)
                .OrderBy(p => p.PartId)
                .ToArray();
            Assert.Equal<int>(4, parts.Length);

            Assert.Equal<string>("/addresses/0", parts[0].Path);
            Assert.Equal<int>(2, parts[0].PartVersions.Count);
            Assert.Contains<PartVersion>(pv1, parts[0].PartVersions);
            Assert.Contains<PartVersion>(updatedPv1, parts[0].PartVersions);

            Assert.Equal<string>("/addresses/1", parts[1].Path);
            Assert.Equal<int>(2, parts[1].PartVersions.Count);
            Assert.Contains<PartVersion>(pv2, parts[1].PartVersions);
            Assert.Contains<PartVersion>(deletedPv2, parts[1].PartVersions);

            Assert.Equal<string>("/ratings/2", parts[2].Path);
            Assert.Equal<int>(1, parts[2].PartVersions.Count);
            Assert.Contains<PartVersion>(pv3, parts[2].PartVersions);

            Assert.Equal<string>("/addresses/3", parts[3].Path);
            Assert.Equal<int>(1, parts[3].PartVersions.Count);
            Assert.Contains<PartVersion>(pv4, parts[3].PartVersions);

            IEnumerable<Commit> commits = this.unitOfWork.DbContext.Set<Commit>()
                .Where(c => c.LotId == lot.LotId)
                .OrderBy(c => c.CommitId);
            Assert.Equal<int>(4, commits.Count());

            lot.Reset(commits.First().CommitId);
            this.unitOfWork.Save();

            parts = this.unitOfWork.DbContext.Set<Part>()
                .Where(p => p.LotId == lot.LotId)
                .OrderBy(p => p.PartId)
                .ToArray();
            Assert.Equal<int>(2, parts.Length);

            Assert.Equal<string>("/addresses/0", parts[0].Path);
            Assert.Equal<int>(1, parts[0].PartVersions.Count);
            Assert.Contains<PartVersion>(pv1, parts[0].PartVersions);

            Assert.Equal<string>("/addresses/1", parts[1].Path);
            Assert.Equal<int>(1, parts[1].PartVersions.Count);
            Assert.Contains<PartVersion>(pv2, parts[1].PartVersions);

            commits = this.unitOfWork.DbContext.Set<Commit>()
                .Where(c => c.LotId == lot.LotId);
            Assert.Equal<int>(1, commits.Count());
            Assert.Null(commits.First().ParentCommitId);
        }
    }
}
