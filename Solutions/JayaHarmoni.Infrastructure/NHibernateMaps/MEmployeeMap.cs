using FluentNHibernate.Automapping.Alterations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JayaHarmoni.Domain;

namespace JayaHarmoni.Infrastructure.NHibernateMaps
{
    public class MEmployeeMap : IAutoMappingOverride<MEmployee>
    {
        public void Override(FluentNHibernate.Automapping.AutoMapping<MEmployee> mapping)
        {
            mapping.DynamicUpdate();
            mapping.DynamicInsert();
            mapping.SelectBeforeUpdate();

            mapping.Table("dbo.M_EMPLOYEE");
            mapping.Id(x => x.Id, "EMPLOYEE_ID")
                 .GeneratedBy.Assigned();

            mapping.Map(x => x.EmployeeName, "EMPLOYEE_NAME");

            mapping.Map(x => x.EmployeeAddress, "EMPLOYEE_ADDRESS");
            mapping.Map(x => x.EmployeePhone, "EMPLOYEE_PHONE");
            mapping.Map(x => x.EmployeeJoinDate, "EMPLOYEE_JOIN_DATE");

            mapping.Map(x => x.EmployeeStatus, "EMPLOYEE_STATUS");
            mapping.Map(x => x.EmployeeDesc, "EMPLOYEE_DESC");

            mapping.Map(x => x.DataStatus, "DATA_STATUS");
            mapping.Map(x => x.CreatedBy, "CREATED_BY");
            mapping.Map(x => x.CreatedDate, "CREATED_DATE");
            mapping.Map(x => x.ModifiedBy, "MODIFIED_BY");
            mapping.Map(x => x.ModifiedDate, "MODIFIED_DATE");
            mapping.Map(x => x.RowVersion, "ROW_VERSION").ReadOnly();
        }
    }
}
