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
     public class TInvoiceTasks : ITInvoiceTasks
    {
      private readonly ITInvoiceRepository _TInvoiceRepository;

        public TInvoiceTasks(ITInvoiceRepository TInvoiceRepository)
        {
            this._TInvoiceRepository = TInvoiceRepository;
        }

        public IEnumerable<Domain.TInvoice> GetAll()
        {
            var entitys = this._TInvoiceRepository.GetAll(); ;
            return entitys;
        }
        
        public TInvoice Insert(Domain.TInvoice entity)
        {
            _TInvoiceRepository.DbContext.BeginTransaction();
            _TInvoiceRepository.Save(entity);
            _TInvoiceRepository.DbContext.CommitTransaction();
            return entity;
        }

        public TInvoice Update(Domain.TInvoice entity)
        {
            _TInvoiceRepository.DbContext.BeginTransaction();
            _TInvoiceRepository.Update(entity);
            _TInvoiceRepository.DbContext.CommitTransaction();
            return entity;
        }

        public TInvoice Delete(Domain.TInvoice entity)
        {
            _TInvoiceRepository.DbContext.BeginTransaction();
            _TInvoiceRepository.Delete(entity);
            _TInvoiceRepository.DbContext.CommitTransaction();
            return entity;
        }

        public TInvoice One(string id)
        {
            var entitys = this._TInvoiceRepository.Get(id); ;
            return entitys;
        }

        public IEnumerable<TInvoice> GetListNotDeleted()
        {
            var entitys = this._TInvoiceRepository.GetListNotDeleted(); ;
            return entitys;
        }


        public IEnumerable<TInvoice> GetListNotDeleted(string ParentProjectId)
        {
            var entitys = this._TInvoiceRepository.GetListNotDeleted(ParentProjectId); ;
            return entitys;
        }


        public IEnumerable<TInvoice> GetListByProjectAndPeriod(string ProjectId, DateTime? RptPeriod)
        {
            var entitys = this._TInvoiceRepository.GetListByProjectAndPeriod(ProjectId,  RptPeriod); ;
            return entitys;
        }
    }
}
