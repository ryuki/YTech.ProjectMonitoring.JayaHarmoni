using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JayaHarmoni.Domain.Contracts.Tasks
{
    public interface ITProjectTasks
    {
       IEnumerable<TProject> GetAll();
        TProject Insert(TProject entity);
        TProject Update(TProject entity);
        TProject Delete(TProject entity);
        TProject One(string id);
        IEnumerable<TProject> GetListNotDeleted();
    }
}
