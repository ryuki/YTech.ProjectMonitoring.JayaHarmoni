using FluentNHibernate.Automapping.Alterations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JayaHarmoni.Domain;

namespace JayaHarmoni.Infrastructure.NHibernateMaps
{
    public partial class TWorkMap : IAutoMappingOverride<TWork>
    {
        public void Override(FluentNHibernate.Automapping.AutoMapping<TWork> mapping)
        {
            #region Properties

            mapping.DynamicUpdate();
            mapping.DynamicInsert();
            mapping.SelectBeforeUpdate();

            mapping.Table("[T_WORK]");
            mapping.Id(x => x.Id, "[WORK_ID]")
                 .GeneratedBy.Assigned();

            mapping.References<MJob>(x => x.JobId, "[JOB_ID]").ForeignKey();
            mapping.References<TProject>(x => x.ProjectId, "[PROJECT_ID]").ForeignKey();

            mapping.Map(x => x.WorkQty, "[WORK_QTY]");
            mapping.Map(x => x.WorkPrice, "[WORK_PRICE]");
            mapping.Map(x => x.WorkTotal, "[WORK_TOTAL]");
            mapping.Map(x => x.WorkRealQty, "[WORK_REAL_QTY]");
            mapping.Map(x => x.WorkRealPaid, "[WORK_REAL_PAID]");
            mapping.Map(x => x.WorkRetentionStatus, "[WORK_RETENTION_STATUS]");
            mapping.Map(x => x.WorkStatus, "[WORK_STATUS]");
            mapping.Map(x => x.WorkDesc, "[WORK_DESC]");
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
