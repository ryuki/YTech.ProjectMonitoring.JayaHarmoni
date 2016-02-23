using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JayaHarmoni.Domain.Contracts.Tasks
{
    public interface IMOwnerTasks
    {
       IEnumerable<MOwner> GetAll();
        MOwner Insert(MOwner entity);
        MOwner Update(MOwner entity);
        MOwner Delete(MOwner entity);
        MOwner One(string id);
        IEnumerable<MOwner> GetListNotDeleted();

        IEnumerable<MOwner> GetListNotDeleted(string ParentEquipId);
    }
}
