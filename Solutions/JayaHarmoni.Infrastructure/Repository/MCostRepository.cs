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
   public class MCostRepository : NHibernateRepositoryWithTypedId<MCost, string>, IMCostRepository
    {
       public MCost GetLastCreatedCost()
       {
           ICriteria criteria = Session.CreateCriteria(typeof(MCost));
           criteria.AddOrder(new Order("CreatedDate", false));
           criteria.SetMaxResults(1);
           return criteria.UniqueResult<MCost>();
       }


       public IEnumerable<MCost> GetListNotDeleted()
       {
           ICriteria criteria = Session.CreateCriteria(typeof(MCost));
           criteria.Add(Expression.Not(Expression.Eq("DataStatus", "Deleted")));
           criteria.AddOrder(new Order("CostName", true));
           //criteria.SetFetchMode("CityId", FetchMode.Eager);
           return criteria.List<MCost>();
       }
    }
}
