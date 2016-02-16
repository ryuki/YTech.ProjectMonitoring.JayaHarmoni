using FluentNHibernate.Automapping.Alterations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JayaHarmoni.Domain;

namespace JayaHarmoni.Infrastructure.NHibernateMaps
{
    public partial class TBapMap : IAutoMappingOverride<TBap>
    {
        public void Override(FluentNHibernate.Automapping.AutoMapping<TBap> mapping)
        {
            #region Properties
        
            mapping.DynamicUpdate();
            mapping.DynamicInsert();
            mapping.SelectBeforeUpdate();

            mapping.Table("[dbo].[T_BAP]");
			mapping.Id(x => x.Id, "[BAP_ID]")
                 .GeneratedBy.Assigned();

            mapping.References<TProject>(x => x.ProjectId, "[PROJECT_ID]").ForeignKey();
            mapping.References<TWork>(x => x.WorkId, "[WORK_ID]").ForeignKey();
            
            mapping.Map(x => x.BapPeriod, "[BAP_PERIOD]");
            mapping.Map(x => x.BapQty, "[BAP_QTY]");
            mapping.Map(x => x.BapTotal, "[BAP_TOTAL]");
            mapping.Map(x => x.BapStatus, "[BAP_STATUS]");
            mapping.Map(x => x.BapDesc, "[BAP_DESC]");
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
