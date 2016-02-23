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
     public class TBapTasks : ITBapTasks
    {
      private readonly ITBapRepository _TBapRepository;

        public TBapTasks(ITBapRepository TBapRepository)
        {
            this._TBapRepository = TBapRepository;
        }

        public IEnumerable<Domain.TBap> GetAll()
        {
            var entitys = this._TBapRepository.GetAll(); ;
            return entitys;
        }
        
        public TBap Insert(Domain.TBap entity)
        {
            _TBapRepository.DbContext.BeginTransaction();
            _TBapRepository.Save(entity);
            _TBapRepository.DbContext.CommitTransaction();
            return entity;
        }

        public TBap Update(Domain.TBap entity)
        {
            _TBapRepository.DbContext.BeginTransaction();
            _TBapRepository.Update(entity);
            _TBapRepository.DbContext.CommitTransaction();
            return entity;
        }

        public TBap Delete(Domain.TBap entity)
        {
            _TBapRepository.DbContext.BeginTransaction();
            _TBapRepository.Delete(entity);
            _TBapRepository.DbContext.CommitTransaction();
            return entity;
        }

        public TBap One(string id)
        {
            var entitys = this._TBapRepository.Get(id); ;
            return entitys;
        }

        public IEnumerable<TBap> GetListNotDeleted()
        {
            var entitys = this._TBapRepository.GetListNotDeleted(); ;
            return entitys;
        }


        public IEnumerable<TBap> GetListNotDeleted(string ParentProjectId)
        {
            var entitys = this._TBapRepository.GetListNotDeleted(ParentProjectId);
            return entitys;
        }


        public IEnumerable<object> GetListResultBapAndAbsent(string ParentProjectId)
        {
            var entitys = this._TBapRepository.GetListResultBapAndAbsent(ParentProjectId); ;
            return entitys;
        }


        public IEnumerable<TBap> GetListByProjectAndPeriod(string ParentProjectId, DateTime? period)
        {
            var entitys = this._TBapRepository.GetListByProjectAndPeriod(ParentProjectId, period);
            return entitys;
        }
    }
}
