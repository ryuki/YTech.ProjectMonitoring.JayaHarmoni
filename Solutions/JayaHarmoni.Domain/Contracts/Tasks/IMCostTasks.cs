using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JayaHarmoni.Domain.Contracts.Tasks
{
    public interface IMCostTasks
    {
        IEnumerable<MCost> GetAllCosts();
        MCost Insert(MCost cost);
        MCost Update(MCost cost);
        MCost Delete(MCost cost);
        MCost One(string costId);
        IEnumerable<MCost> GetListNotDeleted();
    }
}
