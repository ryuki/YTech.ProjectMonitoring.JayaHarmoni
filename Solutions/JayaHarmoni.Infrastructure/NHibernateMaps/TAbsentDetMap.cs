using FluentNHibernate.Automapping.Alterations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JayaHarmoni.Domain;

namespace JayaHarmoni.Infrastructure.NHibernateMaps
{
    public partial class TAbsentDetMap : IAutoMappingOverride<TAbsentDet>
    {
        public void Override(FluentNHibernate.Automapping.AutoMapping<TAbsentDet> mapping)
        {
            #region Properties
        
            mapping.DynamicUpdate();
            mapping.DynamicInsert();
            mapping.SelectBeforeUpdate();

            mapping.Table("[T_ABSENT_DET]");
			mapping.Id(x => x.Id, "[ABSENT_DET_ID]")
                 .GeneratedBy.Assigned();

            mapping.References<MEmployee>(x => x.AbsentDetOperator, "[ABSENT_DET_OPERATOR]").ForeignKey().Fetch.Join();
            mapping.References<MEmployee>(x => x.AbsentDetSinso, "[ABSENT_DET_SINSO]").ForeignKey().Fetch.Join();
            mapping.References<TAbsent>(x => x.AbsentId, "[ABSENT_ID]").ForeignKey().Fetch.Join();
            mapping.References<TWork>(x => x.WorkId, "[WORK_ID]").ForeignKey().Fetch.Join();
            
            mapping.Map(x => x.AbsentDetDate, "[ABSENT_DET_DATE]");
            mapping.Map(x => x.AbsentDetStart, "[ABSENT_DET_START]");
            mapping.Map(x => x.AbsentDetEnd, "[ABSENT_DET_END]");
            mapping.Map(x => x.AbsentDetQty, "[ABSENT_DET_QTY]");
            mapping.Map(x => x.AbsentDetBlock, "[ABSENT_DET_BLOCK]");
            mapping.Map(x => x.AbsentDetResult, "[ABSENT_DET_RESULT]");
            mapping.Map(x => x.AbsentDetBbm, "[ABSENT_DET_BBM]");
            mapping.Map(x => x.AbsentDetStatus, "[ABSENT_DET_STATUS]");
            mapping.Map(x => x.AbsentDetDesc, "[ABSENT_DET_DESC]");
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
