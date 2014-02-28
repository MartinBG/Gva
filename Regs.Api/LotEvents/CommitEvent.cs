using Common.Data;
using Regs.Api.Models;

namespace Regs.Api.LotEvents
{
    public class CommitEvent : IEvent
    {
        public CommitEvent(Lot lot, Commit newIndex, Commit commit)
        {
            this.Lot = lot;
            this.NewIndex = newIndex;
            this.Commit = commit;
        }

        public Lot Lot { get; private set; }

        public Commit NewIndex { get; private set; }

        public Commit Commit { get; private set; }
    }
}
