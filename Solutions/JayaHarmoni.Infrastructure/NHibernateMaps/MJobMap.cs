using FluentNHibernate.Automapping.Alterations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JayaHarmoni.Domain;

namespace JayaHarmoni.Infrastructure.NHibernateMaps
{
    public class MJobMap : IAutoMappingOverride<MJob>
    {
        public void Override(FluentNHibernate.Automapping.AutoMapping<MJob> mapping)
        {
            mapping.DynamicUpdate();
            mapping.DynamicInsert();
            mapping.SelectBeforeUpdate();

            mapping.Table("M_JOB");
            mapping.Id(x => x.Id, "JOB_ID")
                 .GeneratedBy.Assigned();

            mapping.Map(x => x.JobName, "JOB_NAME");
            mapping.Map(x => x.JobUnit, "JOB_UNIT");
            mapping.Map(x => x.JobPrice, "JOB_PRICE");
            
            mapping.Map(x => x.JobStatus, "JOB_STATUS");
            mapping.Map(x => x.JobDesc, "JOB_DESC");

            mapping.Map(x => x.DataStatus, "DATA_STATUS");
            mapping.Map(x => x.CreatedBy, "CREATED_BY");
            mapping.Map(x => x.CreatedDate, "CREATED_DATE");
            mapping.Map(x => x.ModifiedBy, "MODIFIED_BY");
            mapping.Map(x => x.ModifiedDate, "MODIFIED_DATE");
            mapping.Map(x => x.RowVersion, "ROW_VERSION").ReadOnly();
        }
    }
}
