using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SharpArch.Domain.DomainModel;
using System;
using SharpArch.Domain;

namespace JayaHarmoni.Domain
{
    public class TAbsent : EntityWithTypedId<string>, IHasAssignedId<string>
    {
        [DomainSignature]
        public virtual TProject ProjectId { get; set; }
        public virtual MEquip EquipId { get; set; }
        public virtual System.DateTime? AbsentPeriod { get; set; }
        public virtual string AbsentLocation { get; set; }
        public virtual string AbsentSupervisor { get; set; }
        public virtual string AbsentAdmin { get; set; }
        public virtual decimal? AbsentTotalQty { get; set; }
        public virtual decimal? AbsentTotalResult { get; set; }
        public virtual decimal? AbsentTotalBbm { get; set; }
        public virtual string AbsentStatus { get; set; }
        public virtual string AbsentDesc { get; set; }

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
