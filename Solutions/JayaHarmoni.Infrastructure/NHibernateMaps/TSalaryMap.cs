using FluentNHibernate.Automapping.Alterations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JayaHarmoni.Domain;

namespace JayaHarmoni.Infrastructure.NHibernateMaps
{
    public partial class TSalaryMap : IAutoMappingOverride<TSalary>
    {
        public void Override(FluentNHibernate.Automapping.AutoMapping<TSalary> mapping)
        {
            #region Properties
        
            mapping.DynamicUpdate();
            mapping.DynamicInsert();
            mapping.SelectBeforeUpdate();

            mapping.Table("[T_SALARY]");
			mapping.Id(x => x.Id, "[SALARY_ID]")
                 .GeneratedBy.Assigned();

            mapping.References<MEmployee>(x => x.EmployeeId, "[EMPLOYEE_ID]").ForeignKey();
            mapping.References<TProject>(x => x.ProjectId, "[PROJECT_ID]").ForeignKey();
            
            mapping.Map(x => x.SalaryPeriod, "[SALARY_PERIOD]");
            mapping.Map(x => x.SalaryGapok, "[SALARY_GAPOK]");
            mapping.Map(x => x.SalaryWorkQty, "[SALARY_WORK_QTY]");
            mapping.Map(x => x.SalaryWorkTotal, "[SALARY_WORK_TOTAL]");
            mapping.Map(x => x.SalaryTotal, "[SALARY_TOTAL]");
            mapping.Map(x => x.SalaryStatus, "[SALARY_STATUS]");
            mapping.Map(x => x.SalaryDesc, "[SALARY_DESC]");
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
