using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SharpArch.Domain.DomainModel;
using System;
using SharpArch.Domain;

namespace JayaHarmoni.Domain
{
    public class TProject : EntityWithTypedId<string>, IHasAssignedId<string>
    {
        [DomainSignature]
        public virtual string ProjectName { get; set; }
        public virtual MCustomer CustomerId { get; set; }
        public virtual DateTime? ProjectDate { get; set; }
        public virtual decimal? ProjectPrice { get; set; }
        public virtual decimal? ProjectRetention { get; set; }
        public virtual string ProjectLocation { get; set; }
        public virtual DateTime? ProjectStartDate { get; set; }
        public virtual DateTime? ProjectEndDate { get; set; }
        public virtual DateTime? ProjectFinishDate { get; set; }
        public virtual string ProjectStatus { get; set; }
        public virtual string ProjectDesc { get; set; }

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
