using NHibernate;
using NHibernate.Criterion;
using SharpArch.NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JayaHarmoni.Domain;
using JayaHarmoni.Domain.Contracts;

namespace JayaHarmoni.Infrastructure.Repository
{
   public class MJobRepository : NHibernateRepositoryWithTypedId<MJob, string>, IMJobRepository
    {
       public MJob GetLastCreatedJob()
       {
           ICriteria criteria = Session.CreateCriteria(typeof(MJob));
           criteria.AddOrder(new Order("CreatedDate", false));
           criteria.SetMaxResults(1);
           return criteria.UniqueResult<MJob>();
       }


       public IEnumerable<MJob> GetListNotDeleted()
       {
           ICriteria criteria = Session.CreateCriteria(typeof(MJob));
           criteria.Add(Expression.Not(Expression.Eq("DataStatus", "Deleted")));
           criteria.AddOrder(new Order("JobName", true));
           //criteria.SetFetchMode("CityId", FetchMode.Eager);
           return criteria.List<MJob>();
       }
    }
}
