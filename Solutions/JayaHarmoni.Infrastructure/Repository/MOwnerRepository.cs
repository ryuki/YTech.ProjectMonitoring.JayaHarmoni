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
     public class MOwnerRepository : NHibernateRepositoryWithTypedId<MOwner, string>, IMOwnerRepository
    {
       public IEnumerable<MOwner> GetListNotDeleted()
       {
           ICriteria criteria = Session.CreateCriteria(typeof(MOwner));
           criteria.Add(Expression.Not(Expression.Eq("DataStatus", "Deleted")));
           return criteria.List<MOwner>();
       } 
    }
}
