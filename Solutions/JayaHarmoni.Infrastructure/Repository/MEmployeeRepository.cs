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
    public class MEmployeeRepository : NHibernateRepositoryWithTypedId<MEmployee, string>, IMEmployeeRepository
    {
        public MEmployee GetLastCreatedEmployee()
       {
           ICriteria criteria = Session.CreateCriteria(typeof(MEmployee));
           criteria.AddOrder(new Order("CreatedDate", false));
           criteria.SetMaxResults(1);
           return criteria.UniqueResult<MEmployee>();
       }


        public IEnumerable<MEmployee> GetListNotDeleted()
       {
           ICriteria criteria = Session.CreateCriteria(typeof(MEmployee));
           criteria.Add(Expression.Not(Expression.Eq("DataStatus", "Deleted")));
           criteria.AddOrder(new Order("EmployeeName", true));
           //criteria.SetFetchMode("CityId", FetchMode.Eager);
           return criteria.List<MEmployee>();
       }
    }
}
