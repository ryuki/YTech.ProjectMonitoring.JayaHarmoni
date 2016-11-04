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
     public class TInvoiceRepository : NHibernateRepositoryWithTypedId<TInvoice, string>, ITInvoiceRepository
    {
       public IEnumerable<TInvoice> GetListNotDeleted()
       {
           ICriteria criteria = Session.CreateCriteria(typeof(TInvoice));
           criteria.Add(Expression.Not(Expression.Eq("DataStatus", "Deleted")));
           return criteria.List<TInvoice>();
       }


       public IEnumerable<TInvoice> GetListNotDeleted(string ParentProjectId)
       {
           ICriteria criteria = Session.CreateCriteria(typeof(TInvoice));
           criteria.Add(Expression.Not(Expression.Eq("DataStatus", "Deleted")));
           criteria.Add(Expression.Eq("ProjectId.Id", ParentProjectId));
           return criteria.List<TInvoice>();
       }


       public IEnumerable<TInvoice> GetListByProjectAndPeriod(string ProjectId, DateTime? RptPeriod)
       {
           ICriteria criteria = Session.CreateCriteria(typeof(TInvoice));
           criteria.Add(Expression.Not(Expression.Eq("DataStatus", "Deleted")));
           criteria.Add(Expression.Eq("ProjectId.Id", ProjectId));
           criteria.Add(Expression.Eq("InvoicePeriod", RptPeriod));
           return criteria.List<TInvoice>();
       }
    }
}
