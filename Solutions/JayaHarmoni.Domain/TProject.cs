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
        #region Properties

        [DomainSignature]
        public virtual MCustomer CustomerId { get; set; }

        public virtual string ProjectName { get; set; }
        public virtual string ProjectSpkNo { get; set; }
        public virtual System.DateTime? ProjectDate { get; set; }
        public virtual decimal? ProjectPrice { get; set; }
        public virtual decimal? ProjectRetention { get; set; }
        public virtual string ProjectLocation { get; set; }
        public virtual System.DateTime? ProjectStartDate { get; set; }
        public virtual System.DateTime? ProjectEndDate { get; set; }
        public virtual System.DateTime? ProjectFinishDate { get; set; }
        public virtual string ProjectStatus { get; set; }
        public virtual string ProjectDesc { get; set; }
        public virtual string DataStatus { get; set; }
        public virtual string CreatedBy { get; set; }
        public virtual System.DateTime? CreatedDate { get; set; }
        public virtual string ModifiedBy { get; set; }
        public virtual System.DateTime? ModifiedDate { get; set; }
        public virtual byte[] RowVersion { get; set; }

        #endregion

        #region Implementation of IHasAssignedId<string>
        public virtual void SetAssignedIdTo(string assignedId)
        {
            Check.Require(!string.IsNullOrEmpty(assignedId), "Assigned Id may not be null or empty");
            Id = assignedId.Trim();
        }
        #endregion
    }
}
