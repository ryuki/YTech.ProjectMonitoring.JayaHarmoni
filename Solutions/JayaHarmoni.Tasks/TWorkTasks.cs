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
     public class TWorkTasks : ITWorkTasks
    {
      private readonly ITWorkRepository _TWorkRepository;

        public TWorkTasks(ITWorkRepository TWorkRepository)
        {
            this._TWorkRepository = TWorkRepository;
        }

        public IEnumerable<Domain.TWork> GetAll()
        {
            var entitys = this._TWorkRepository.GetAll(); ;
            return entitys;
        }
        
        public TWork Insert(Domain.TWork entity)
        {
            _TWorkRepository.DbContext.BeginTransaction();
            _TWorkRepository.Save(entity);
            _TWorkRepository.DbContext.CommitTransaction();
            return entity;
        }

        public TWork Update(Domain.TWork entity)
        {
            _TWorkRepository.DbContext.BeginTransaction();
            _TWorkRepository.Update(entity);
            _TWorkRepository.DbContext.CommitTransaction();
            return entity;
        }

        public TWork Delete(Domain.TWork entity)
        {
            _TWorkRepository.DbContext.BeginTransaction();
            _TWorkRepository.Delete(entity);
            _TWorkRepository.DbContext.CommitTransaction();
            return entity;
        }

        public TWork One(string id)
        {
            var entitys = this._TWorkRepository.Get(id); ;
            return entitys;
        }

        public IEnumerable<TWork> GetListNotDeleted()
        {
            var entitys = this._TWorkRepository.GetListNotDeleted(); ;
            return entitys;
        } 
    }
}
