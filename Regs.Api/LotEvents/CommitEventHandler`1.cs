using System;
using System.Linq;
using System.Linq.Expressions;
using Common.Data;
using Regs.Api.Models;

namespace Regs.Api.LotEvents
{
    public abstract class CommitEventHandler<TView> : CommitEventHandler where TView : class, new()
    {
        private IUnitOfWork unitOfWork;
        private string setAlias;
        private string setPartAlias;
        private Func<PartVersion, bool> partMatcher;
        private Func<PartVersion, Expression<Func<TView, bool>>> viewMatcher;
        private object viewMatcher1;

        public CommitEventHandler(
            IUnitOfWork unitOfWork,
            string setAlias,
            string setPartAlias,
            Func<PartVersion, Expression<Func<TView, bool>>> viewMatcher,
            Func<PartVersion, bool> partMatcher = null,
            bool isPrincipal = true)
            : base(isPrincipal)
        {
            this.unitOfWork = unitOfWork;
            this.setAlias = setAlias;
            this.setPartAlias = setPartAlias;
            this.partMatcher = partMatcher ?? (pv => true);
            this.viewMatcher = viewMatcher;
        }

        public abstract void Fill(TView view, PartVersion partVersion);

        public abstract void Clear(TView view);

        public override void Handle(ILotEvent e)
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

                if (this.IsPrincipal)
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
                if (this.IsPrincipal)
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

                if (this.IsPrincipal)
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
