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
    public class MCostTasks : IMCostTasks
    {
        private readonly IMCostRepository _costRepository;

        public MCostTasks(IMCostRepository costRepository)
        {
            this._costRepository = costRepository;
        }

        public IEnumerable<Domain.MCost> GetAllCosts()
        {
            var costs = this._costRepository.GetAll(); ;
            return costs;
        }
        
        public MCost Insert(Domain.MCost cost)
        {
            _costRepository.DbContext.BeginTransaction();
            _costRepository.Save(cost);
            _costRepository.DbContext.CommitTransaction();
            return cost;
        }

        public MCost Update(Domain.MCost cost)
        {
            _costRepository.DbContext.BeginTransaction();
            _costRepository.Update(cost);
            _costRepository.DbContext.CommitTransaction();
            return cost;
        }

        public MCost Delete(Domain.MCost cost)
        {
            _costRepository.DbContext.BeginTransaction();
            _costRepository.Delete(cost);
            _costRepository.DbContext.CommitTransaction();
            return cost;
        }

        public MCost One(string costId)
        {
            var costs = this._costRepository.Get(costId); ;
            return costs;
        }

        public IEnumerable<MCost> GetListNotDeleted()
        {
            var costs = this._costRepository.GetListNotDeleted(); ;
            return costs;
        }
    }
}
