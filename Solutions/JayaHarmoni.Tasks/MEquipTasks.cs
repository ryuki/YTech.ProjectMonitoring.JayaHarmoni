using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JayaHarmoni.Domain.Contracts;
using JayaHarmoni.Domain.Contracts.Tasks;
using SharpArch.Domain;
using JayaHarmoni.Infrastructure.Repository;
using JayaHarmoni.Domain;

namespace JayaHarmoni.Tasks
{
    public class MEquipTasks : IMEquipTasks
    {
        private readonly IMEquipRepository _equipRepository;

        public MEquipTasks(IMEquipRepository equipRepository)
        {
            this._equipRepository = equipRepository;
        }

        public IEnumerable<Domain.MEquip> GetAllEquips()
        {
            var equips = this._equipRepository.GetAll(); ;
            return equips;
        }
        
        public MEquip Insert(Domain.MEquip equip)
        {
            _equipRepository.DbContext.BeginTransaction();
            _equipRepository.Save(equip);
            _equipRepository.DbContext.CommitTransaction();
            return equip;
        }

        public MEquip Update(Domain.MEquip equip)
        {
            _equipRepository.DbContext.BeginTransaction();
            _equipRepository.Update(equip);
            _equipRepository.DbContext.CommitTransaction();
            return equip;
        }

        public MEquip Delete(Domain.MEquip equip)
        {
            _equipRepository.DbContext.BeginTransaction();
            _equipRepository.Delete(equip);
            _equipRepository.DbContext.CommitTransaction();
            return equip;
        }

        public MEquip One(string equipId)
        {
            var equips = this._equipRepository.Get(equipId); ;
            return equips;
        }

        public IEnumerable<MEquip> GetListNotDeleted()
        {
            var equips = this._equipRepository.GetListNotDeleted(); ;
            return equips;
        }
    }
}
