using FluentNHibernate.Automapping.Alterations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JayaHarmoni.Domain;

namespace JayaHarmoni.Infrastructure.NHibernateMaps
{
    public partial class TProjectMap : IAutoMappingOverride<TProject>
    {
        public void Override(FluentNHibernate.Automapping.AutoMapping<TProject> mapping)
        {
            #region Properties

            mapping.DynamicUpdate();
            mapping.DynamicInsert();
            mapping.SelectBeforeUpdate();

            mapping.Table("[dbo].[T_PROJECT]");
            mapping.Id(x => x.Id, "[PROJECT_ID]")
                 .GeneratedBy.Assigned();

            mapping.References<MCustomer>(x => x.CustomerId, "[CUSTOMER_ID]").ForeignKey();

            mapping.Map(x => x.ProjectName, "[PROJECT_NAME]");
            mapping.Map(x => x.ProjectSpkNo, "[PROJECT_SPK_NO]");
            mapping.Map(x => x.ProjectDate, "[PROJECT_DATE]");
            mapping.Map(x => x.ProjectPrice, "[PROJECT_PRICE]");
            mapping.Map(x => x.ProjectRetention, "[PROJECT_RETENTION]");
            mapping.Map(x => x.ProjectLocation, "[PROJECT_LOCATION]");
            mapping.Map(x => x.ProjectStartDate, "[PROJECT_START_DATE]");
            mapping.Map(x => x.ProjectEndDate, "[PROJECT_END_DATE]");
            mapping.Map(x => x.ProjectFinishDate, "[PROJECT_FINISH_DATE]");
            mapping.Map(x => x.ProjectStatus, "[PROJECT_STATUS]");
            mapping.Map(x => x.ProjectDesc, "[PROJECT_DESC]");
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
