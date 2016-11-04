using FluentNHibernate.Automapping.Alterations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JayaHarmoni.Domain;

namespace JayaHarmoni.Infrastructure.NHibernateMaps
{
    public class MCostMap : IAutoMappingOverride<MCost>
    {
        public void Override(FluentNHibernate.Automapping.AutoMapping<MCost> mapping)
        {
            mapping.DynamicUpdate();
            mapping.DynamicInsert();
            mapping.SelectBeforeUpdate();
            mapping.OptimisticLock.Version();

            mapping.Table("M_COST");
            mapping.Id(x => x.Id, "COST_ID")
                 .GeneratedBy.Assigned();

            mapping.Map(x => x.CostName, "COST_NAME");
            
            mapping.Map(x => x.CostStatus, "COST_STATUS");
            mapping.Map(x => x.CostDesc, "COST_DESC");

            mapping.Map(x => x.DataStatus, "DATA_STATUS");
            mapping.Map(x => x.CreatedBy, "CREATED_BY");
            mapping.Map(x => x.CreatedDate, "CREATED_DATE");
            mapping.Map(x => x.ModifiedBy, "MODIFIED_BY");
            mapping.Map(x => x.ModifiedDate, "MODIFIED_DATE");
            //mapping.Map(x => x.RowVersion, "ROW_VERSION").ReadOnly();
            mapping.Version(x => x.RowVersion)
                .Column("ROW_VERSION")
                .Not.Nullable();
        }
    }
}
