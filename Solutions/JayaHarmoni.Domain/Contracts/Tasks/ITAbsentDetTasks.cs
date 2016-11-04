using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JayaHarmoni.Domain.Contracts.Tasks
{
    public interface ITAbsentDetTasks
    {
       IEnumerable<TAbsentDet> GetAll();
        TAbsentDet Insert(TAbsentDet entity);
        TAbsentDet Update(TAbsentDet entity);
        TAbsentDet Delete(TAbsentDet entity);
        TAbsentDet One(string id);
        IEnumerable<TAbsentDet> GetListNotDeleted();

        IEnumerable<TAbsentDet> GetListNotDeleted(string AbsentId);

        decimal? GetTotalQtyByEmp(string employeeId, DateTime? period);

        IEnumerable<TAbsentDet> GetListByProjectAndPeriod(string ProjectId, DateTime? RptPeriod);
    }
}
