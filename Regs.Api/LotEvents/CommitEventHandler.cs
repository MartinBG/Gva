using System;
using System.Linq;
using System.Linq.Expressions;
using Common.Data;
using Regs.Api.Models;

namespace Regs.Api.LotEvents
{
    public abstract class CommitEventHandler<TView> : ILotEventHandler where TView : class, new()
    {
        private IUnitOfWork unitOfWork;
        private string setAlias;
        private string setPartAlias;
        private Func<PartVersion, bool> partMatcher;
        private Func<PartVersion, Expression<Func<TView, bool>>> viewMatcher;
        private bool isPrincipalHandler;
        private object viewMatcher1;

        public CommitEventHandler(
            IUnitOfWork unitOfWork,
            string setPartAlias,
            Func<PartVersion, Expression<Func<TView, bool>>> viewMatcher,
            Func<PartVersion, bool> partMatcher = null,
            bool isPrincipalHandler = true,
            string setAlias = null)
        {
            this.unitOfWork = unitOfWork;
            this.setAlias = setAlias;
            this.setPartAlias = setPartAlias;
            this.partMatcher = partMatcher ?? (pv => true);
            this.viewMatcher = viewMatcher;
            this.isPrincipalHandler = isPrincipalHandler;
        }

        public CommitEventHandler(IUnitOfWork unitOfWork, string setPartAlias, object viewMatcher1)
        {
            // TODO: Complete member initialization
            this.unitOfWork = unitOfWork;
            this.setPartAlias = setPartAlias;
            this.viewMatcher1 = viewMatcher1;
        }

        public abstract void Fill(TView view, PartVersion partVersion);

        public abstract void Clear(TView view);

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

            var parts = commit.ChangedPartVersions.Where(pv => pv.Part.SetPart.Alias == this.setPartAlias);

            foreach (var part in parts.Where(pv => pv.PartOperation == PartOperation.Add))
            {
                if (!this.partMatcher(part))
                {
                    continue;
                }

                if (this.isPrincipalHandler)
                {
                    TView view = new TView();
                    this.Fill(view, part);
                    this.unitOfWork.DbContext.Set<TView>().Add(view);
                }
                else
                {
                    TView view = this.unitOfWork.DbContext.Set<TView>().SingleOrDefault(viewMatcher(part));
                    this.Fill(view, part);
                }
            }

            foreach (var part in parts.Where(pv => pv.PartOperation == PartOperation.Delete))
            {
                if (!this.partMatcher(part))
                {
                    continue;
                }

                TView view = this.unitOfWork.DbContext.Set<TView>().SingleOrDefault(viewMatcher(part));
                if (this.isPrincipalHandler)
                {
                    this.unitOfWork.DbContext.Set<TView>().Remove(view);
                }
                else
                {
                    this.Clear(view);
                }
            }

            foreach (var part in parts.Where(pv => pv.PartOperation == PartOperation.Update))
            {
                TView view = this.unitOfWork.DbContext.Set<TView>().SingleOrDefault(viewMatcher(part));

                if (this.partMatcher(part))
                {
                    this.Fill(view, part);
                    continue;
                }

                var prevPartVersion = commit.CommitVersions.SingleOrDefault(cv => cv.PartVersionId == part.PartVersionId).OldPartVersion;
                if (!this.partMatcher(prevPartVersion))
                {
                    continue;
                }

                if (this.isPrincipalHandler)
                {
                    this.unitOfWork.DbContext.Set<TView>().Remove(view);
                }
                else
                {
                    this.Clear(view);
                }
            }
        }
    }
}
