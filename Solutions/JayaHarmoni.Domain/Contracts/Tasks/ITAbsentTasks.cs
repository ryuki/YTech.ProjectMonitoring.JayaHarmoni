using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JayaHarmoni.Domain.Contracts.Tasks
{
    public interface ITAbsentTasks
    {
       IEnumerable<TAbsent> GetAll();
        TAbsent Insert(TAbsent entity);
        TAbsent Update(TAbsent entity);
        TAbsent Delete(TAbsent entity);
        TAbsent One(string id);
        IEnumerable<TAbsent> GetListNotDeleted();
    }
}
