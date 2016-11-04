using FluentNHibernate.Automapping.Alterations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JayaHarmoni.Domain;

namespace JayaHarmoni.Infrastructure.NHibernateMaps
{
    public partial class TInvoiceMap : IAutoMappingOverride<TInvoice>
    {
        public void Override(FluentNHibernate.Automapping.AutoMapping<TInvoice> mapping)
        {
            #region Properties
        
            mapping.DynamicUpdate();
            mapping.DynamicInsert();
            mapping.SelectBeforeUpdate();

            mapping.Table("[T_INVOICE]");
			mapping.Id(x => x.Id, "[INVOICE_ID]")
                 .GeneratedBy.Assigned();

            mapping.References<TProject>(x => x.ProjectId, "[PROJECT_ID]").ForeignKey();
            
            mapping.Map(x => x.InvoicePeriod, "[INVOICE_PERIOD]");
            mapping.Map(x => x.InvoiceStartDate, "[INVOICE_START_DATE]");
            mapping.Map(x => x.InvoiceEndDate, "[INVOICE_END_DATE]");
            mapping.Map(x => x.InvoiceNo, "[INVOICE_NO]");
            mapping.Map(x => x.InvoiceDate, "[INVOICE_DATE]");
            mapping.Map(x => x.InvoiceLastStatus, "[INVOICE_LAST_STATUS]");
            mapping.Map(x => x.InvoicePaidDate, "[INVOICE_PAID_DATE]");
            mapping.Map(x => x.InvoiceValue, "[INVOICE_VALUE]");
            mapping.Map(x => x.InvoiceRetention, "[INVOICE_RETENTION]");
            mapping.Map(x => x.InvoicePpn, "[INVOICE_PPN]");
            mapping.Map(x => x.InvoiceTotal, "[INVOICE_TOTAL]");
            mapping.Map(x => x.InvoiceStatus, "[INVOICE_STATUS]");
            mapping.Map(x => x.InvoiceDesc, "[INVOICE_DESC]");
            mapping.Map(x => x.DataStatus, "[DATA_STATUS]");
            mapping.Map(x => x.CreatedBy, "[CREATED_BY]");
            mapping.Map(x => x.CreatedDate, "[CREATED_DATE]");
            mapping.Map(x => x.ModifiedBy, "[MODIFIED_BY]");
            mapping.Map(x => x.ModifiedDate, "[MODIFIED_DATE]");
            mapping.Map(x => x.RowVersion, "[ROW_VERSION]");

            #endregion
        }
    }
}
