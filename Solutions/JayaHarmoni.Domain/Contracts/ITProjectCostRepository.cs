using SharpArch.Domain.PersistenceSupport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpArch.NHibernate;
using SharpArch.NHibernate.Contracts.Repositories;

namespace JayaHarmoni.Domain.Contracts
{
    public interface ITProjectCostRepository : INHibernateRepositoryWithTypedId<TProjectCost, string>
    {
       IEnumerable<TProjectCost> GetListNotDeleted();
    }
}
