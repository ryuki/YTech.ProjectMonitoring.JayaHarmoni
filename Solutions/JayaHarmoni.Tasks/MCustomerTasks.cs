﻿using System;
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
    public class MCustomerTasks : IMCustomerTasks
    {
        private readonly IMCustomerRepository _customerRepository;

        public MCustomerTasks(IMCustomerRepository customerRepository)
        {
            this._customerRepository = customerRepository;
        }

        public IEnumerable<Domain.MCustomer> GetAllCustomers()
        {
            var customers = this._customerRepository.GetAll(); ;
            return customers;
        }
        
        public MCustomer Insert(Domain.MCustomer cust)
        {
            _customerRepository.DbContext.BeginTransaction();
            _customerRepository.Save(cust);
            _customerRepository.DbContext.CommitTransaction();
            return cust;
        }

        public MCustomer Update(Domain.MCustomer cust)
        {
            _customerRepository.DbContext.BeginTransaction();
            _customerRepository.Update(cust);
            _customerRepository.DbContext.CommitTransaction();
            return cust;
        }

        public MCustomer Delete(Domain.MCustomer cust)
        {
            _customerRepository.DbContext.BeginTransaction();
            _customerRepository.Delete(cust);
            _customerRepository.DbContext.CommitTransaction();
            return cust;
        }

        public MCustomer One(string custId)
        {
            var customers = this._customerRepository.Get(custId); ;
            return customers;
        }

        public MCustomer GetLastCreatedCustomer()
        {
            MCustomer cust = this._customerRepository.GetLastCreatedCustomer();
            return cust;
        }


        public IEnumerable<MCustomer> GetListNotDeleted()
        {
            var customers = this._customerRepository.GetListNotDeleted(); ;
            return customers;
        }
    }
}
