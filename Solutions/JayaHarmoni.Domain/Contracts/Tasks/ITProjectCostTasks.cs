using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JayaHarmoni.Domain.Contracts.Tasks
{
    public interface ITProjectCostTasks
    {
       IEnumerable<TProjectCost> GetAll();
        TProjectCost Insert(TProjectCost entity);
        TProjectCost Update(TProjectCost entity);
        TProjectCost Delete(TProjectCost entity);
        TProjectCost One(string id);
        IEnumerable<TProjectCost> GetListNotDeleted();
    }
}
