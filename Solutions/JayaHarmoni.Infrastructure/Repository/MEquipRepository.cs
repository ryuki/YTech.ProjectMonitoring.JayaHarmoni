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
   public class MEquipRepository : NHibernateRepositoryWithTypedId<MEquip, string>, IMEquipRepository
    {
       public MEquip GetLastCreatedEquip()
       {
           ICriteria criteria = Session.CreateCriteria(typeof(MEquip));
           criteria.AddOrder(new Order("CreatedDate", false));
           criteria.SetMaxResults(1);
           return criteria.UniqueResult<MEquip>();
       }


       public IEnumerable<MEquip> GetListNotDeleted()
       {
           ICriteria criteria = Session.CreateCriteria(typeof(MEquip));
           criteria.Add(Expression.Not(Expression.Eq("DataStatus", "Deleted")));
           criteria.AddOrder(new Order("EquipName", true));
           //criteria.SetFetchMode("CityId", FetchMode.Eager);
           return criteria.List<MEquip>();
       }
    }
}
