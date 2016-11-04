using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SharpArch.Domain.DomainModel;
using System;
using SharpArch.Domain;

namespace JayaHarmoni.Domain
{
    public class MEmployee : EntityWithTypedId<string>, IHasAssignedId<string>
    {
        #region Properties

        [DomainSignature]

        public virtual string EmployeeName { get; set; }
        public virtual string EmployeeAddress { get; set; }
        public virtual string EmployeePhone { get; set; }
        public virtual System.DateTime? EmployeeJoinDate { get; set; }
        public virtual decimal? EmployeeBasicSalary { get; set; }
        public virtual decimal? EmployeeDailyAllowance { get; set; }
        public virtual string EmployeeStatus { get; set; }
        public virtual string EmployeeDesc { get; set; }
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
