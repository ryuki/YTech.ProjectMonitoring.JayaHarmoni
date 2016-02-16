using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JayaHarmoni.Domain.Contracts.Tasks
{
    public interface IMJobTasks
    {
        IEnumerable<MJob> GetAllJobs();
        MJob Insert(MJob job);
        MJob Update(MJob job);
        MJob Delete(MJob job);
        MJob One(string jobId);
        IEnumerable<MJob> GetListNotDeleted();
    }
}
