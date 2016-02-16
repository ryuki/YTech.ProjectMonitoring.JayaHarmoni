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
     public class MOwnerTasks : IMOwnerTasks
    {
      private readonly IMOwnerRepository _MOwnerRepository;

        public MOwnerTasks(IMOwnerRepository MOwnerRepository)
        {
            this._MOwnerRepository = MOwnerRepository;
        }

        public IEnumerable<Domain.MOwner> GetAll()
        {
            var entitys = this._MOwnerRepository.GetAll(); ;
            return entitys;
        }
        
        public MOwner Insert(Domain.MOwner entity)
        {
            _MOwnerRepository.DbContext.BeginTransaction();
            _MOwnerRepository.Save(entity);
            _MOwnerRepository.DbContext.CommitTransaction();
            return entity;
        }

        public MOwner Update(Domain.MOwner entity)
        {
            _MOwnerRepository.DbContext.BeginTransaction();
            _MOwnerRepository.Update(entity);
            _MOwnerRepository.DbContext.CommitTransaction();
            return entity;
        }

        public MOwner Delete(Domain.MOwner entity)
        {
            _MOwnerRepository.DbContext.BeginTransaction();
            _MOwnerRepository.Delete(entity);
            _MOwnerRepository.DbContext.CommitTransaction();
            return entity;
        }

        public MOwner One(string id)
        {
            var entitys = this._MOwnerRepository.Get(id); ;
            return entitys;
        }

        public IEnumerable<MOwner> GetListNotDeleted()
        {
            var entitys = this._MOwnerRepository.GetListNotDeleted(); ;
            return entitys;
        } 
    }
}
