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
     public class TSalaryRepository : NHibernateRepositoryWithTypedId<TSalary, string>, ITSalaryRepository
    {
       public IEnumerable<TSalary> GetListNotDeleted()
       {
           ICriteria criteria = Session.CreateCriteria(typeof(TSalary));
           criteria.Add(Expression.Not(Expression.Eq("DataStatus", "Deleted")));
           return criteria.List<TSalary>();
       } 
    }
}
