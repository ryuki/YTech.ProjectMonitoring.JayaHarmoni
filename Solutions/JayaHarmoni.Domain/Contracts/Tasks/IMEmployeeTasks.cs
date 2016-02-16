using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JayaHarmoni.Domain.Contracts.Tasks
{
    public interface IMEmployeeTasks
    {
        IEnumerable<MEmployee> GetAllEmps();
        MEmployee Insert(MEmployee emp);
        MEmployee Update(MEmployee emp);
        MEmployee Delete(MEmployee emp);
        MEmployee One(string empId);

        IEnumerable<MEmployee> GetListNotDeleted();
    }
}
