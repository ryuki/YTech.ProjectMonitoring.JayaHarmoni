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
     public class TProjectCostRepository : NHibernateRepositoryWithTypedId<TProjectCost, string>, ITProjectCostRepository
    {
       public IEnumerable<TProjectCost> GetListNotDeleted()
       {
           ICriteria criteria = Session.CreateCriteria(typeof(TProjectCost));
           criteria.Add(Expression.Not(Expression.Eq("DataStatus", "Deleted")));
           return criteria.List<TProjectCost>();
       } 
    }
}
