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
    public class TBapRepository : NHibernateRepositoryWithTypedId<TBap, string>, ITBapRepository
    {
        public IEnumerable<TBap> GetListNotDeleted()
        {
            ICriteria criteria = Session.CreateCriteria(typeof(TBap));
            criteria.Add(Expression.Not(Expression.Eq("DataStatus", "Deleted")));
            return criteria.List<TBap>();
        }


        public IEnumerable<TBap> GetListNotDeleted(string ParentProjectId)
        {
            ICriteria criteria = Session.CreateCriteria(typeof(TBap));
            criteria.Add(Expression.Not(Expression.Eq("DataStatus", "Deleted")));
            criteria.Add(Expression.Eq("ProjectId.Id", ParentProjectId));
            return criteria.List<TBap>();
        }


        public IEnumerable<object> GetListResultBapAndAbsent(string ParentProjectId)
        {
            StringBuilder sql = new StringBuilder();

            sql.AppendLine(@"   select p.Id as ProjectId,
                    p.ProjectName,
                    b.BapPeriod,
                    w.Id as WorkId,
                    j.JobName,
                    b.BapQty,
                    sum(d.AbsentDetResult) as SumAbsentDetResult
                from TBap b, TAbsent a ,TAbsentDet d, TProject p, TWork w, MJob j
                where b.ProjectId.Id = a.ProjectId.Id and b.ProjectId.Id = p.Id and b.BapPeriod = a.AbsentPeriod
                    and a.Id = d.AbsentId and b.WorkId.Id = d.WorkId.Id and b.WorkId.Id = w.Id and w.JobId.Id = j.Id
                    and p.Id = :ParentProjectId
                group by p.Id,
                    p.ProjectName,
                    b.BapPeriod,
                    w.Id,
                    j.JobName,
                    b.BapQty");
            IQuery q = Session.CreateQuery(sql.ToString());
            q.SetString("ParentProjectId", ParentProjectId);
            return q.Enumerable<object>();
        }


        public IEnumerable<TBap> GetListByProjectAndPeriod(string ParentProjectId, DateTime? period)
        {
            ICriteria criteria = Session.CreateCriteria(typeof(TBap));
            criteria.Add(Expression.Not(Expression.Eq("DataStatus", "Deleted")));
            criteria.Add(Expression.Eq("ProjectId.Id", ParentProjectId));
            criteria.Add(Expression.Eq("BapPeriod", period));
            criteria.SetFetchMode("WorkId", FetchMode.Eager);
            criteria.SetFetchMode("WorkId.JobId", FetchMode.Eager);
            return criteria.List<TBap>();
        }
    }
}
