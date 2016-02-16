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
    public class MEmployeeTasks : IMEmployeeTasks
    {
        private readonly IMEmployeeRepository _employeeRepository;

        public MEmployeeTasks(IMEmployeeRepository employeeRepository)
        {
            this._employeeRepository = employeeRepository;
        }

        public IEnumerable<Domain.MEmployee> GetAllEmps()
        {
            var employees = this._employeeRepository.GetAll(); ;
            return employees;
        }
        
        public MEmployee Insert(Domain.MEmployee cust)
        {
            _employeeRepository.DbContext.BeginTransaction();
            _employeeRepository.Save(cust);
            _employeeRepository.DbContext.CommitTransaction();
            return cust;
        }

        public MEmployee Update(Domain.MEmployee cust)
        {
            _employeeRepository.DbContext.BeginTransaction();
            _employeeRepository.Update(cust);
            _employeeRepository.DbContext.CommitTransaction();
            return cust;
        }

        public MEmployee Delete(Domain.MEmployee cust)
        {
            _employeeRepository.DbContext.BeginTransaction();
            _employeeRepository.Delete(cust);
            _employeeRepository.DbContext.CommitTransaction();
            return cust;
        }

        public MEmployee One(string custId)
        {
            var employees = this._employeeRepository.Get(custId); ;
            return employees;
        }

        public IEnumerable<MEmployee> GetListNotDeleted()
        {
            var employees = this._employeeRepository.GetListNotDeleted(); ;
            return employees;
        }
    }
}
