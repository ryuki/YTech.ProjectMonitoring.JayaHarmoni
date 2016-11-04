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
     public class TSalaryTasks : ITSalaryTasks
    {
      private readonly ITSalaryRepository _TSalaryRepository;

        public TSalaryTasks(ITSalaryRepository TSalaryRepository)
        {
            this._TSalaryRepository = TSalaryRepository;
        }

        public IEnumerable<Domain.TSalary> GetAll()
        {
            var entitys = this._TSalaryRepository.GetAll(); ;
            return entitys;
        }
        
        public TSalary Insert(Domain.TSalary entity)
        {
            _TSalaryRepository.DbContext.BeginTransaction();
            _TSalaryRepository.Save(entity);
            _TSalaryRepository.DbContext.CommitTransaction();
            return entity;
        }

        public TSalary Update(Domain.TSalary entity)
        {
            _TSalaryRepository.DbContext.BeginTransaction();
            _TSalaryRepository.Update(entity);
            _TSalaryRepository.DbContext.CommitTransaction();
            return entity;
        }

        public TSalary Delete(Domain.TSalary entity)
        {
            _TSalaryRepository.DbContext.BeginTransaction();
            _TSalaryRepository.Delete(entity);
            _TSalaryRepository.DbContext.CommitTransaction();
            return entity;
        }

        public TSalary One(string id)
        {
            var entitys = this._TSalaryRepository.Get(id); ;
            return entitys;
        }

        public IEnumerable<TSalary> GetListNotDeleted()
        {
            var entitys = this._TSalaryRepository.GetListNotDeleted(); ;
            return entitys;
        } 
    }
}
