using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JayaHarmoni.Domain.Contracts.Tasks
{
    public interface IMEquipTasks
    {
        IEnumerable<MEquip> GetAllEquips();
        MEquip Insert(MEquip equip);
        MEquip Update(MEquip equip);
        MEquip Delete(MEquip equip);
        MEquip One(string equipId);
        IEnumerable<MEquip> GetListNotDeleted();
    }
}
