using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JayaHarmoni.Domain.Contracts.Tasks
{
    public interface ITBapTasks
    {
       IEnumerable<TBap> GetAll();
        TBap Insert(TBap entity);
        TBap Update(TBap entity);
        TBap Delete(TBap entity);
        TBap One(string id);
        IEnumerable<TBap> GetListNotDeleted();

        IEnumerable<TBap> GetListNotDeleted(string ParentProjectId);

        IEnumerable<object> GetListResultBapAndAbsent(string ParentProjectId);

        IEnumerable<TBap> GetListByProjectAndPeriod(string ParentProjectId, DateTime? period);
    }
}
