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
     public class TProjectCostTasks : ITProjectCostTasks
    {
      private readonly ITProjectCostRepository _TProjectCostRepository;

        public TProjectCostTasks(ITProjectCostRepository TProjectCostRepository)
        {
            this._TProjectCostRepository = TProjectCostRepository;
        }

        public IEnumerable<Domain.TProjectCost> GetAll()
        {
            var entitys = this._TProjectCostRepository.GetAll(); ;
            return entitys;
        }
        
        public TProjectCost Insert(Domain.TProjectCost entity)
        {
            _TProjectCostRepository.DbContext.BeginTransaction();
            _TProjectCostRepository.Save(entity);
            _TProjectCostRepository.DbContext.CommitTransaction();
            return entity;
        }

        public TProjectCost Update(Domain.TProjectCost entity)
        {
            _TProjectCostRepository.DbContext.BeginTransaction();
            _TProjectCostRepository.Update(entity);
            _TProjectCostRepository.DbContext.CommitTransaction();
            return entity;
        }

        public TProjectCost Delete(Domain.TProjectCost entity)
        {
            _TProjectCostRepository.DbContext.BeginTransaction();
            _TProjectCostRepository.Delete(entity);
            _TProjectCostRepository.DbContext.CommitTransaction();
            return entity;
        }

        public TProjectCost One(string id)
        {
            var entitys = this._TProjectCostRepository.Get(id); ;
            return entitys;
        }

        public IEnumerable<TProjectCost> GetListNotDeleted()
        {
            var entitys = this._TProjectCostRepository.GetListNotDeleted(); ;
            return entitys;
        } 
    }
}
