using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Data;
using Regs.Api.Models;

namespace Regs.Api.LotEvents
{
    public abstract class Projection<TView> : ILotEventHandler where TView : class
    {
        private static ConcurrentDictionary<Type, EntitySet> entitySets = new ConcurrentDictionary<Type,EntitySet>();

        private IUnitOfWork unitOfWork;
        private string setAlias;

        public Projection(
            IUnitOfWork unitOfWork,
            string setAlias)
        {
            this.unitOfWork = unitOfWork;
            this.setAlias = setAlias;
        }

        public abstract IEnumerable<TView> Execute(PartCollection parts);

        public void Handle(ILotEvent e)
        {
            CommitEvent commitEvent = e as CommitEvent;
            if (commitEvent == null)
            {
                return;
            }

            var commit = commitEvent.Commit;
            var lot = commitEvent.Lot;

            if (!String.IsNullOrEmpty(this.setAlias) && lot.Set.Alias != this.setAlias)
            {
                return;
            }

            var oldViews = this
                .Execute(commit.OldParts)
                .Select(view => unitOfWork.DbContext.Entry(view))
                .ToDictionary(viewEntry => CreateEntityKey(viewEntry));

            var newViews = this
                .Execute(commit.Parts)
                .Select(view => unitOfWork.DbContext.Entry(view))
                .ToDictionary(viewEntry => CreateEntityKey(viewEntry));

            // update updated views
            var updatedOldViews = oldViews.Where(oldView => newViews.ContainsKey(oldView.Key));
            foreach (var oldView in updatedOldViews)
            {
                oldView.Value.State = EntityState.Unchanged;

                oldView.Value.CurrentValues.SetValues(newViews[oldView.Key].Entity);
            }

            // remove deleted views
            var deletedOldViews = oldViews.Where(oldView => !newViews.ContainsKey(oldView.Key));
            foreach (var oldView in deletedOldViews)
            {
                oldView.Value.State = EntityState.Deleted;
            }

            // add new views
            var addedNewViews = newViews.Where(newView => !oldViews.ContainsKey(newView.Key));
            foreach (var newView in addedNewViews)
            {
                newView.Value.State = EntityState.Added;
            }
        }

        private EntityKey CreateEntityKey(DbEntityEntry<TView> viewEntry)
        {
            var entitySet = entitySets.GetOrAdd(typeof(TView), t =>
            {
                return ((System.Data.Entity.Infrastructure.IObjectContextAdapter)unitOfWork.DbContext).ObjectContext.CreateObjectSet<TView>().EntitySet;
            });

            return new EntityKey(entitySet.EntityContainer.Name + "." + entitySet.Name, entitySet.ElementType.KeyMembers.Select(key => {
                return new KeyValuePair<string, object>(key.Name, viewEntry.Property(key.Name).CurrentValue);
            }));
        }
    }
}
