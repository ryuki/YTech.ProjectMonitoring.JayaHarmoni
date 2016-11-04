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
    public class TAbsentDetRepository : NHibernateRepositoryWithTypedId<TAbsentDet, string>, ITAbsentDetRepository
    {
        public IEnumerable<TAbsentDet> GetListNotDeleted()
        {
            ICriteria criteria = Session.CreateCriteria(typeof(TAbsentDet));
            criteria.Add(Expression.Not(Expression.Eq("DataStatus", "Deleted")));
            return criteria.List<TAbsentDet>();
        }


        public IEnumerable<TAbsentDet> GetListNotDeleted(string AbsentId)
        {
            ICriteria criteria = Session.CreateCriteria(typeof(TAbsentDet));
            criteria.Add(Expression.Not(Expression.Eq("DataStatus", "Deleted")));
            criteria.Add(Expression.Eq("AbsentId.Id", AbsentId));

            criteria.AddOrder(new Order("AbsentDetDate", true));
            return criteria.List<TAbsentDet>();
        }


        public decimal? GetTotalQtyByEmp(string employeeId, DateTime? period)
        {
            ICriteria criteria = Session.CreateCriteria(typeof(TAbsentDet));
            criteria.Add(Expression.Not(Expression.Eq("DataStatus", "Deleted")));
            criteria.Add(Expression.Or(Expression.Eq("AbsentDetOperator.Id", employeeId), Expression.Eq("AbsentDetSinso.Id", employeeId)));
            criteria.Add(Expression.Between("AbsentDetDate", period, period.Value.AddMonths(1).AddDays(-1)));

            var list = criteria.List<TAbsentDet>();
            return list.Sum(m => m.AbsentDetQty);
        }


        public IEnumerable<TAbsentDet> GetListByProjectAndPeriod(string ProjectId, DateTime? RptPeriod)
        {
            //ICriteria criteria = Session.CreateCriteria(typeof(TAbsentDet));
            //criteria.Add(Expression.Not(Expression.Eq("DataStatus", "Deleted")));
            //criteria.Add(Expression.Eq("AbsentId.ProjectId.Id", ProjectId));
            //criteria.Add(Expression.Between("AbsentDetDate", RptPeriod, RptPeriod.Value.AddMonths(1).AddDays(-1)));
            //criteria.SetFetchMode("WorkId", FetchMode.Eager);
            //criteria.SetFetchMode("AbsentDetOperator", FetchMode.Eager);
            //criteria.SetFetchMode("AbsentDetSinso", FetchMode.Eager);
            //return criteria.List<TAbsentDet>();

            StringBuilder sql = new StringBuilder();

            sql.AppendLine(@"   select d
                from TAbsentDet d 
                    left join fetch d.AbsentDetOperator o
                    left join fetch d.AbsentDetSinso s
                    left join fetch d.WorkId w
                    left join fetch d.AbsentId a 
                    left join fetch a.ProjectId p
                    left join fetch a.EquipId e
                    left join fetch w.JobId j
                where p.Id = :ProjectId 
                order by d.AbsentDetDate");
            IQuery q = Session.CreateQuery(sql.ToString());
            q.SetString("ProjectId", ProjectId);
            return q.List<TAbsentDet>();
        }
    }
}
