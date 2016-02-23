using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SharpArch.Domain.DomainModel;
using System;
using SharpArch.Domain;

namespace JayaHarmoni.Domain
{
    public class TAbsentDet : EntityWithTypedId<string>, IHasAssignedId<string>
    {
        [DomainSignature]
        public virtual TAbsent AbsentId { get; set; }
        public virtual TWork WorkId { get; set; }
        public virtual DateTime? AbsentDetDate { get; set; }
        public virtual MEmployee AbsentDetOperator { get; set; }
        public virtual MEmployee AbsentDetSinso { get; set; }
        public virtual decimal? AbsentDetStart { get; set; }
        public virtual decimal? AbsentDetEnd { get; set; }
        public virtual decimal? AbsentDetQty { get; set; }
        public virtual string AbsentDetBlock { get; set; }
        public virtual decimal? AbsentDetResult { get; set; }
        public virtual decimal? AbsentDetBbm { get; set; }
        public virtual string AbsentDetStatus { get; set; }
        public virtual string AbsentDetDesc { get; set; }

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
