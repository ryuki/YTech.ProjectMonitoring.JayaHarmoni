using SharpArch.Domain.PersistenceSupport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpArch.NHibernate;
using SharpArch.NHibernate.Contracts.Repositories;

namespace JayaHarmoni.Domain.Contracts
{
    public interface ITAbsentRepository : INHibernateRepositoryWithTypedId<TAbsent, string>
    {
       IEnumerable<TAbsent> GetListNotDeleted();

       IEnumerable<TAbsent> GetListByProjectAndPeriod(string ProjectId, DateTime? RptPeriod);
    }
}
