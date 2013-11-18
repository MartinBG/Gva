﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Common.Data;
using Common.Infrastructure;
using Gva.Model.Tests.Common;
using Gva.Web.App_Start;
using Newtonsoft.Json.Linq;
using Ninject;
using Regs.Api.Managers.LobManager;
using Regs.Api.Managers.LotManager;
using Regs.Api.Models;
using Xunit;
using Xunit.Extensions;

namespace Gva.Model.Tests
{
    public class Test
    {
        private IKernel kernel;
        private IUnitOfWork unitOfWork;
        private ILotManager lotManager;
        private ILobManager lobManager;
        private UserContext userContext;

        public Test()
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
            using (var transaction = this.unitOfWork.BeginTransaction())
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
        }

        [Fact, AutoRollback]
        public void AddPart()
        {
            using (var transaction = this.unitOfWork.BeginTransaction())
            {
                Set set = this.lotManager.GetSet("Person");

                Lot lot = set.AddLot(this.userContext);
                lot.AddPart(
                    "/addresses/*",
                    JObject.Parse(@"{ key1: 'value1', key2: [ 'value1', 'value2']}"),
                    this.lobManager,
                    this.userContext);
                this.unitOfWork.Save();

                lot = this.lotManager.GetLot(lot.LotId);
                PartVersion savedPartVersion = lot.GetPart("/addresses/0");

                Part part = this.unitOfWork.DbContext.Set<Part>().FirstOrDefault(p => p.Path == "/addresses/0");
                TextBlob textBlob = this.unitOfWork.DbContext.Set<TextBlob>().FirstOrDefault(tb => tb.Hash == "876FB827A153BB2CB30FC25B082B7CB9D31290D8");
                Commit commit = lot.Commits.First();

                Assert.Equal<int>(part.PartId, savedPartVersion.PartId);
                Assert.Equal<int>(textBlob.TextBlobId, savedPartVersion.TextBlobId);
                Assert.Equal<int>(commit.CommitId, savedPartVersion.OriginalCommitId);
                Assert.Equal<int>(this.userContext.UserId, savedPartVersion.CreatorId);
                Assert.Equal<int>((int)PartOperation.Add, (int)savedPartVersion.PartOperation);
                Assert.Contains<PartVersion>(savedPartVersion, commit.PartVersions);
            }
        }

        [Fact, AutoRollback]
        public void CommitAllParts()
        {
            using (var transaction = this.unitOfWork.BeginTransaction())
            {
                Set set = this.lotManager.GetSet("Person");

                Lot lot = set.AddLot(this.userContext);
                lot.AddPart(
                    "/addresses/*",
                    JObject.Parse(@"{ key1: 'value1', key2: [ 'value1', 'value2']}"),
                    this.lobManager,
                    this.userContext);
                lot.AddPart(
                    "/addresses/*",
                    JObject.Parse(@"{ key1: 'value1', key2: [ 'value1', 'value2']}"),
                    this.lobManager,
                    this.userContext);
                this.unitOfWork.Save();

                lot = this.lotManager.GetLot(lot.LotId);
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
        }

        [Fact, AutoRollback]
        public void CommitOnePart()
        {
            using (var transaction = this.unitOfWork.BeginTransaction())
            {
                Set set = this.lotManager.GetSet("Person");

                Lot lot = set.AddLot(this.userContext);
                lot.AddPart(
                    "/addresses/*",
                    JObject.Parse(@"{ key1: 'value1', key2: [ 'value1', 'value2']}"),
                    this.lobManager,
                    this.userContext);
                lot.AddPart(
                    "/addresses/*",
                    JObject.Parse(@"{ key1: 'value1', key2: [ 'value1', 'value2']}"),
                    this.lobManager,
                    this.userContext);
                this.unitOfWork.Save();

                lot = this.lotManager.GetLot(lot.LotId);
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
        }

        [Fact, AutoRollback]
        public void UpdatePart()
        {
            using (var transaction = this.unitOfWork.BeginTransaction())
            {
                Set set = this.lotManager.GetSet("Person");

                Lot lot = set.AddLot(this.userContext);
                lot.AddPart(
                    "/addresses/*",
                    JObject.Parse(@"{ key1: 'value1', key2: [ 'value1', 'value2']}"),
                    this.lobManager,
                    this.userContext);
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

                PartVersion updatedPV = index.PartVersions.FirstOrDefault(pv => pv.PartOperation == PartOperation.Update);
                Assert.NotNull(updatedPV);
                Assert.Equal<int>(index.CommitId, updatedPV.OriginalCommitId);

                PartVersion oldPV = index.PartVersions.FirstOrDefault(pv => pv.PartId == updatedPV.PartId && pv.PartOperation != PartOperation.Update);
                Assert.Null(oldPV);
            }
        }

        [Fact, AutoRollback]
        public void DeletePart()
        {
            using (var transaction = this.unitOfWork.BeginTransaction())
            {
                Set set = this.lotManager.GetSet("Person");

                Lot lot = set.AddLot(this.userContext);
                lot.AddPart(
                    "/addresses/*",
                    JObject.Parse(@"{ key1: 'value1', key2: [ 'value1', 'value2']}"),
                    this.lobManager,
                    this.userContext);
                lot.AddPart(
                    "/addresses/*",
                    JObject.Parse(@"{ key1: 'value1', key2: [ 'value1', 'value2']}"),
                    this.lobManager,
                    this.userContext);
                this.unitOfWork.Save();

                lot = this.lotManager.GetLot(lot.LotId);
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
        }
    }
}
