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
     public class TProjectTasks : ITProjectTasks
    {
      private readonly ITProjectRepository _TProjectRepository;

        public TProjectTasks(ITProjectRepository TProjectRepository)
        {
            this._TProjectRepository = TProjectRepository;
        }

        public IEnumerable<Domain.TProject> GetAll()
        {
            var entitys = this._TProjectRepository.GetAll(); ;
            return entitys;
        }
        
        public TProject Insert(Domain.TProject entity)
        {
            _TProjectRepository.DbContext.BeginTransaction();
            _TProjectRepository.Save(entity);
            _TProjectRepository.DbContext.CommitTransaction();
            return entity;
        }

        public TProject Update(Domain.TProject entity)
        {
            _TProjectRepository.DbContext.BeginTransaction();
            _TProjectRepository.Update(entity);
            _TProjectRepository.DbContext.CommitTransaction();
            return entity;
        }

        public TProject Delete(Domain.TProject entity)
        {
            _TProjectRepository.DbContext.BeginTransaction();
            _TProjectRepository.Delete(entity);
            _TProjectRepository.DbContext.CommitTransaction();
            return entity;
        }

        public TProject One(string id)
        {
            var entitys = this._TProjectRepository.Get(id); ;
            return entitys;
        }

        public IEnumerable<TProject> GetListNotDeleted()
        {
            var entitys = this._TProjectRepository.GetListNotDeleted(); ;
            return entitys;
        } 
    }
}
