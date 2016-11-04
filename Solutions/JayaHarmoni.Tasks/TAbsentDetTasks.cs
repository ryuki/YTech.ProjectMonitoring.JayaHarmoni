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
     public class TAbsentDetTasks : ITAbsentDetTasks
    {
      private readonly ITAbsentDetRepository _TAbsentDetRepository;

        public TAbsentDetTasks(ITAbsentDetRepository TAbsentDetRepository)
        {
            this._TAbsentDetRepository = TAbsentDetRepository;
        }

        public IEnumerable<Domain.TAbsentDet> GetAll()
        {
            var entitys = this._TAbsentDetRepository.GetAll(); ;
            return entitys;
        }
        
        public TAbsentDet Insert(Domain.TAbsentDet entity)
        {
            _TAbsentDetRepository.DbContext.BeginTransaction();
            _TAbsentDetRepository.Save(entity);
            _TAbsentDetRepository.DbContext.CommitTransaction();
            return entity;
        }

        public TAbsentDet Update(Domain.TAbsentDet entity)
        {
            _TAbsentDetRepository.DbContext.BeginTransaction();
            _TAbsentDetRepository.Update(entity);
            _TAbsentDetRepository.DbContext.CommitTransaction();
            return entity;
        }

        public TAbsentDet Delete(Domain.TAbsentDet entity)
        {
            _TAbsentDetRepository.DbContext.BeginTransaction();
            _TAbsentDetRepository.Delete(entity);
            _TAbsentDetRepository.DbContext.CommitTransaction();
            return entity;
        }

        public TAbsentDet One(string id)
        {
            var entitys = this._TAbsentDetRepository.Get(id); ;
            return entitys;
        }

        public IEnumerable<TAbsentDet> GetListNotDeleted()
        {
            var entitys = this._TAbsentDetRepository.GetListNotDeleted(); ;
            return entitys;
        }


        public IEnumerable<TAbsentDet> GetListNotDeleted(string AbsentId)
        {
            var entitys = this._TAbsentDetRepository.GetListNotDeleted(AbsentId);
            return entitys;
        }


        public decimal? GetTotalQtyByEmp(string employeeId, DateTime? period)
        {
            return this._TAbsentDetRepository.GetTotalQtyByEmp(employeeId, period); 
        }


        public IEnumerable<TAbsentDet> GetListByProjectAndPeriod(string ProjectId, DateTime? RptPeriod)
        {
            var entitys = this._TAbsentDetRepository.GetListByProjectAndPeriod(ProjectId, RptPeriod); ;
            return entitys;
        }
    }
}
