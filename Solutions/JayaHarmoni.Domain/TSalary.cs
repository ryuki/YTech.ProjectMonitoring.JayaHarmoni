using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SharpArch.Domain.DomainModel;
using System;
using SharpArch.Domain;

namespace JayaHarmoni.Domain
{
    public class TSalary : EntityWithTypedId<string>, IHasAssignedId<string>
    {
        #region Properties
    
        [DomainSignature]
        public virtual MEmployee EmployeeId { get; set; }
        public virtual TProject ProjectId { get; set; }
        
        public virtual System.DateTime? SalaryPeriod { get; set; }
        public virtual decimal? SalaryGapok { get; set; }
        public virtual decimal? SalaryWorkQty { get; set; }
        public virtual decimal? SalaryWorkTotal { get; set; }
        public virtual decimal? SalaryTotal { get; set; }
        public virtual string SalaryStatus { get; set; }
        public virtual string SalaryDesc { get; set; }
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
