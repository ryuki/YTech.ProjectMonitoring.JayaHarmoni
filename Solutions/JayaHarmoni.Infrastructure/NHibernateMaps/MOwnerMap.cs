using FluentNHibernate.Automapping.Alterations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JayaHarmoni.Domain;

namespace JayaHarmoni.Infrastructure.NHibernateMaps
{
    public partial class MOwnerMap : IAutoMappingOverride<MOwner>
    {
        public void Override(FluentNHibernate.Automapping.AutoMapping<MOwner> mapping)
        {
            #region Properties
        
            mapping.DynamicUpdate();
            mapping.DynamicInsert();
            mapping.SelectBeforeUpdate();

            mapping.Table("[M_OWNER]");
			mapping.Id(x => x.Id, "[OWNER_ID]")
                 .GeneratedBy.Assigned();

            mapping.References<MEquip>(x => x.EquipId, "[EQUIP_ID]").ForeignKey();
            
            mapping.Map(x => x.OwnerName, "[OWNER_NAME]");
            mapping.Map(x => x.OwnerPercent, "[OWNER_PERCENT]");
            mapping.Map(x => x.OwnerSinceDate, "[OWNER_SINCE_DATE]");
            mapping.Map(x => x.OwnerUntilDate, "[OWNER_UNTIL_DATE]");
            mapping.Map(x => x.OwnerStatus, "[OWNER_STATUS]");
            mapping.Map(x => x.OwnerDesc, "[OWNER_DESC]");
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
