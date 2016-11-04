using SharpArch.Domain.PersistenceSupport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpArch.NHibernate;
using SharpArch.NHibernate.Contracts.Repositories;

namespace JayaHarmoni.Domain.Contracts
{
    public interface ITAbsentDetRepository : INHibernateRepositoryWithTypedId<TAbsentDet, string>
    {
       IEnumerable<TAbsentDet> GetListNotDeleted();

       IEnumerable<TAbsentDet> GetListNotDeleted(string AbsentId);

       decimal? GetTotalQtyByEmp(string employeeId, DateTime? period);

       IEnumerable<TAbsentDet> GetListByProjectAndPeriod(string ProjectId, DateTime? RptPeriod);
    }
}
