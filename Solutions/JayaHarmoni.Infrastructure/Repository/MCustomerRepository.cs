﻿using NHibernate;
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
   public class MCustomerRepository : NHibernateRepositoryWithTypedId<MCustomer, string>, IMCustomerRepository
    {
       public MCustomer GetLastCreatedCustomer()
       {
           ICriteria criteria = Session.CreateCriteria(typeof(MCustomer));
           criteria.AddOrder(new Order("CreatedDate", false));
           criteria.SetMaxResults(1);
           return criteria.UniqueResult<MCustomer>();
       }


       public IEnumerable<MCustomer> GetListNotDeleted()
       {
           ICriteria criteria = Session.CreateCriteria(typeof(MCustomer));
           criteria.Add(Expression.Not(Expression.Eq("DataStatus", "Deleted")));
           criteria.AddOrder(new Order("CustomerName", true));
           //criteria.SetFetchMode("CityId", FetchMode.Eager);
           return criteria.List<MCustomer>();
       }
    }
}