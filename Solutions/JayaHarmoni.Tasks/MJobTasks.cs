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
    public class MJobTasks : IMJobTasks
    {
        private readonly IMJobRepository _jobRepository;

        public MJobTasks(IMJobRepository jobRepository)
        {
            this._jobRepository = jobRepository;
        }

        public IEnumerable<Domain.MJob> GetAllJobs()
        {
            var jobs = this._jobRepository.GetAll(); ;
            return jobs;
        }
        
        public MJob Insert(Domain.MJob job)
        {
            _jobRepository.DbContext.BeginTransaction();
            _jobRepository.Save(job);
            _jobRepository.DbContext.CommitTransaction();
            return job;
        }

        public MJob Update(Domain.MJob job)
        {
            _jobRepository.DbContext.BeginTransaction();
            _jobRepository.Update(job);
            _jobRepository.DbContext.CommitTransaction();
            return job;
        }

        public MJob Delete(Domain.MJob job)
        {
            _jobRepository.DbContext.BeginTransaction();
            _jobRepository.Delete(job);
            _jobRepository.DbContext.CommitTransaction();
            return job;
        }

        public MJob One(string jobId)
        {
            var jobs = this._jobRepository.Get(jobId); ;
            return jobs;
        }

        public IEnumerable<MJob> GetListNotDeleted()
        {
            var jobs = this._jobRepository.GetListNotDeleted(); ;
            return jobs;
        }
    }
}
