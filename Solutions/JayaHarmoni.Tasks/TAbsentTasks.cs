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
     public class TAbsentTasks : ITAbsentTasks
    {
      private readonly ITAbsentRepository _TAbsentRepository;

        public TAbsentTasks(ITAbsentRepository TAbsentRepository)
        {
            this._TAbsentRepository = TAbsentRepository;
        }

        public IEnumerable<Domain.TAbsent> GetAll()
        {
            var entitys = this._TAbsentRepository.GetAll(); ;
            return entitys;
        }
        
        public TAbsent Insert(Domain.TAbsent entity)
        {
            _TAbsentRepository.DbContext.BeginTransaction();
            _TAbsentRepository.Save(entity);
            _TAbsentRepository.DbContext.CommitTransaction();
            return entity;
        }

        public TAbsent Update(Domain.TAbsent entity)
        {
            _TAbsentRepository.DbContext.BeginTransaction();
            _TAbsentRepository.Update(entity);
            _TAbsentRepository.DbContext.CommitTransaction();
            return entity;
        }

        public TAbsent Delete(Domain.TAbsent entity)
        {
            _TAbsentRepository.DbContext.BeginTransaction();
            _TAbsentRepository.Delete(entity);
            _TAbsentRepository.DbContext.CommitTransaction();
            return entity;
        }

        public TAbsent One(string id)
        {
            var entitys = this._TAbsentRepository.Get(id); ;
            return entitys;
        }

        public IEnumerable<TAbsent> GetListNotDeleted()
        {
            var entitys = this._TAbsentRepository.GetListNotDeleted();
            return entitys;
        }


        public IEnumerable<TAbsent> GetListByProjectAndPeriod(string ProjectId, DateTime? RptPeriod)
        {
            var entitys = this._TAbsentRepository.GetListByProjectAndPeriod(ProjectId,  RptPeriod);
            return entitys;
        }
    }
}
