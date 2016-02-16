using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SharpArch.Domain.DomainModel;
using System;
using SharpArch.Domain;

namespace JayaHarmoni.Domain
{
    public class TWork : EntityWithTypedId<string>, IHasAssignedId<string>
    {
        [DomainSignature]
        public virtual TProject ProjectId { get; set; }
        public virtual MJob JobId { get; set; }
        public virtual string WorkQty { get; set; }
        public virtual decimal? WorkPrice { get; set; }
        public virtual decimal? WorkTotal { get; set; }
        public virtual decimal? WorkRealQty { get; set; }
        public virtual decimal? WorkRealPaid { get; set; }
        public virtual string WorkStatus { get; set; }
        public virtual string WorkDesc { get; set; }

        public virtual string DataStatus { get; set; }
        public virtual string CreatedBy { get; set; }
        public virtual DateTime? CreatedDate { get; set; }
        public virtual string ModifiedBy { get; set; }
        public virtual DateTime? ModifiedDate { get; set; }
        public virtual byte[] RowVersion { get; set; }

        #region Implementation of IHasAssignedId<string>

        public virtual void SetAssignedIdTo(string assignedId)
        {
            Check.Require(!string.IsNullOrEmpty(assignedId), "Assigned Id may not be null or empty");
            Id = assignedId.Trim();
        }

        #endregion
    }
}
