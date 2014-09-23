using System;
using Common.Api.Models;
using Newtonsoft.Json;

namespace Regs.Api.Models
{
    public class PartVersion<T> : PartVersion where T : class
    {
        private PartVersion wrapped;

        public PartVersion()
        {
        }

        public PartVersion(PartVersion partVersion)
        {
            this.wrapped = partVersion;
        }

        public override int PartVersionId
        {
            get
            {
                return this.wrapped.PartVersionId;
            }
            set
            {
                this.wrapped.PartVersionId = value;
            }
        }

        public override int PartId
        {
            get
            {
                return this.wrapped.PartId;
            }
            set
            {
                this.wrapped.PartId = value;
            }
        }

        public override string TextContent
        {
            get
            {
                return this.wrapped.TextContent;
            }
            set
            {
                this.wrapped.TextContent = value;
            }
        }

        public override int OriginalCommitId
        {
            get
            {
                return this.wrapped.OriginalCommitId;
            }
            set
            {
                this.wrapped.OriginalCommitId = value;
            }
        }

        public override int CreatorId
        {
            get
            {
                return this.wrapped.CreatorId;
            }
            set
            {
                this.wrapped.CreatorId = value;
            }
        }

        public override DateTime CreateDate
        {
            get
            {
                return this.wrapped.CreateDate;
            }
            set
            {
                this.wrapped.CreateDate = value;
            }
        }

        public override Commit OriginalCommit
        {
            get
            {
                return this.wrapped.OriginalCommit;
            }
            set
            {
                this.wrapped.OriginalCommit = value;
            }
        }

        public override PartOperation PartOperation
        {
            get
            {
                return this.wrapped.PartOperation;
            }
            set
            {
                this.wrapped.PartOperation = value;
            }
        }

        public override Part Part
        {
            get
            {
                return this.wrapped.Part;
            }
            set
            {
                this.wrapped.Part = value;
            }
        }

        public override User Creator
        {
            get
            {
                return this.wrapped.Creator;
            }
            set
            {
                this.wrapped.Creator = value;
            }
        }

        private T content = null;

        public T Content
        {
            get
            {
                if (this.content == null)
                {
                    this.content = JsonConvert.DeserializeObject<T>(this.TextContent);
                }

                return this.content;
            }
        }
    }
}
