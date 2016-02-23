using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SharpArch.Domain.DomainModel;
using System;
using SharpArch.Domain;

namespace JayaHarmoni.Domain
{
    public class TInvoice : EntityWithTypedId<string>, IHasAssignedId<string>
    {
        #region Properties
    
        [DomainSignature]
        public virtual TProject ProjectId { get; set; }
        
        public virtual System.DateTime? InvoicePeriod { get; set; }
        public virtual System.DateTime? InvoiceStartDate { get; set; }
        public virtual System.DateTime? InvoiceEndDate { get; set; }
        public virtual string InvoiceNo { get; set; }
        public virtual System.DateTime? InvoiceDate { get; set; }
        public virtual string InvoiceLastStatus { get; set; }
        public virtual System.DateTime? InvoicePaidDate { get; set; }
        public virtual decimal? InvoiceValue { get; set; }
        public virtual decimal? InvoiceRetention { get; set; }
        public virtual decimal? InvoicePpn { get; set; }
        public virtual decimal? InvoiceTotal { get; set; }
        public virtual string InvoiceStatus { get; set; }
        public virtual string InvoiceDesc { get; set; }
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
