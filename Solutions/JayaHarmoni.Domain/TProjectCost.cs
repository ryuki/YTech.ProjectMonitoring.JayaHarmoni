using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SharpArch.Domain.DomainModel;
using System;
using SharpArch.Domain;

namespace JayaHarmoni.Domain
{
    public class TProjectCost : EntityWithTypedId<string>, IHasAssignedId<string>
    {
        #region Properties

        [DomainSignature]
        public virtual MCost CostId { get; set; }
        public virtual MEquip EquipId { get; set; }
        public virtual TProject ProjectId { get; set; }

        public virtual System.DateTime? ProjectCostDate { get; set; }
        public virtual decimal? ProjectCostQty { get; set; }
        public virtual decimal? ProjectCostPrice { get; set; }
        public virtual decimal? ProjectCostTotal { get; set; }
        public virtual string ProjectCostStatus { get; set; }
        public virtual string ProjectCostDesc { get; set; }
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
