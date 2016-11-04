using FluentNHibernate.Automapping.Alterations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JayaHarmoni.Domain;

namespace JayaHarmoni.Infrastructure.NHibernateMaps
{
    public partial class TAbsentMap : IAutoMappingOverride<TAbsent>
    {
        public void Override(FluentNHibernate.Automapping.AutoMapping<TAbsent> mapping)
        {
            #region Properties
        
            mapping.DynamicUpdate();
            mapping.DynamicInsert();
            mapping.SelectBeforeUpdate();

            mapping.Table("[T_ABSENT]");
			mapping.Id(x => x.Id, "[ABSENT_ID]")
                 .GeneratedBy.Assigned();

            mapping.References<MEquip>(x => x.EquipId, "[EQUIP_ID]").ForeignKey().Fetch.Join();
            mapping.References<TProject>(x => x.ProjectId, "[PROJECT_ID]").ForeignKey().Fetch.Join();
            
            mapping.Map(x => x.AbsentPeriod, "[ABSENT_PERIOD]");
            mapping.Map(x => x.AbsentLocation, "[ABSENT_LOCATION]");
            mapping.Map(x => x.AbsentSupervisor, "[ABSENT_SUPERVISOR]");
            mapping.Map(x => x.AbsentAdmin, "[ABSENT_ADMIN]");
            mapping.Map(x => x.AbsentTotalQty, "[ABSENT_TOTAL_QTY]");
            mapping.Map(x => x.AbsentTotalResult, "[ABSENT_TOTAL_RESULT]");
            mapping.Map(x => x.AbsentTotalBbm, "[ABSENT_TOTAL_BBM]");
            mapping.Map(x => x.AbsentStatus, "[ABSENT_STATUS]");
            mapping.Map(x => x.AbsentDesc, "[ABSENT_DESC]");
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
