using FluentNHibernate.Automapping.Alterations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JayaHarmoni.Domain;

namespace JayaHarmoni.Infrastructure.NHibernateMaps
{
    public partial class TProjectCostMap : IAutoMappingOverride<TProjectCost>
    {
        public void Override(FluentNHibernate.Automapping.AutoMapping<TProjectCost> mapping)
        {
            #region Properties
        
            mapping.DynamicUpdate();
            mapping.DynamicInsert();
            mapping.SelectBeforeUpdate();

            mapping.Table("[dbo].[T_PROJECT_COST]");
			mapping.Id(x => x.Id, "[PROJECT_COST_ID]")
                 .GeneratedBy.Assigned();

            mapping.References<MCost>(x => x.CostId, "[COST_ID]").ForeignKey();
            mapping.References<MEquip>(x => x.EquipId, "[EQUIP_ID]").ForeignKey();
            mapping.References<TProject>(x => x.ProjectId, "[PROJECT_ID]").ForeignKey();
            
            mapping.Map(x => x.ProjectCostDate, "[PROJECT_COST_DATE]");
            mapping.Map(x => x.ProjectCostQty, "[PROJECT_COST_QTY]");
            mapping.Map(x => x.ProjectCostTotal, "[PROJECT_COST_TOTAL]");
            mapping.Map(x => x.ProjectCostStatus, "[PROJECT_COST_STATUS]");
            mapping.Map(x => x.ProjectCostDesc, "[PROJECT_COST_DESC]");
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
