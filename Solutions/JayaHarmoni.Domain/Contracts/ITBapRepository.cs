using SharpArch.Domain.PersistenceSupport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpArch.NHibernate;
using SharpArch.NHibernate.Contracts.Repositories;

namespace JayaHarmoni.Domain.Contracts
{
    public interface ITBapRepository : INHibernateRepositoryWithTypedId<TBap, string>
    {
        IEnumerable<TBap> GetListNotDeleted();
        IEnumerable<TBap> GetListNotDeleted(string ParentProjectId);

        IEnumerable<object> GetListResultBapAndAbsent(string ParentProjectId);

        IEnumerable<TBap> GetListByProjectAndPeriod(string ParentProjectId, DateTime? period);
    }
}
