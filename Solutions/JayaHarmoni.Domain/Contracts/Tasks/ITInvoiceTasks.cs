using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JayaHarmoni.Domain.Contracts.Tasks
{
    public interface ITInvoiceTasks
    {
       IEnumerable<TInvoice> GetAll();
        TInvoice Insert(TInvoice entity);
        TInvoice Update(TInvoice entity);
        TInvoice Delete(TInvoice entity);
        TInvoice One(string id);
        IEnumerable<TInvoice> GetListNotDeleted();

        IEnumerable<TInvoice> GetListNotDeleted(string ParentProjectId);

        IEnumerable<TInvoice> GetListByProjectAndPeriod(string ProjectId, DateTime? RptPeriod);
    }
}
