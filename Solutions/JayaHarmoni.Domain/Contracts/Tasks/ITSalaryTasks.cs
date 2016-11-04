using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JayaHarmoni.Domain.Contracts.Tasks
{
    public interface ITSalaryTasks
    {
       IEnumerable<TSalary> GetAll();
        TSalary Insert(TSalary entity);
        TSalary Update(TSalary entity);
        TSalary Delete(TSalary entity);
        TSalary One(string id);
        IEnumerable<TSalary> GetListNotDeleted();
    }
}
