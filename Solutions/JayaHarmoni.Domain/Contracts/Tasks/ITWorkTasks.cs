using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JayaHarmoni.Domain.Contracts.Tasks
{
    public interface ITWorkTasks
    {
       IEnumerable<TWork> GetAll();
        TWork Insert(TWork entity);
        TWork Update(TWork entity);
        TWork Delete(TWork entity);
        TWork One(string id);
        IEnumerable<TWork> GetListNotDeleted();

        IEnumerable<TWork> GetListNotDeleted(string projectId);
    }
}
