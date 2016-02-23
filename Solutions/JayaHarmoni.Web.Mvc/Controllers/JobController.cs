using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using System.Collections.Generic;
using System.Web.Mvc;
using JayaHarmoni.Domain.Contracts.Tasks;
using JayaHarmoni.Web.Mvc.Controllers.ViewModels;
using System.Linq;
using JayaHarmoni.Domain;
using System;
using System.Web;
//using JayaHarmoni.Enums;

namespace JayaHarmoni.Web.Mvc.Controllers
{
    [HandleError]
    [Authorize]
    public partial class JobController : Controller
    {
        private readonly IMJobTasks _jobTasks;
        public JobController(IMJobTasks jobTasks)
        {
            this._jobTasks = jobTasks;
        }

        [Authorize(Roles = "ADMINISTRATOR, SUPERVISOR, CS")]
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Index(bool? isModal)
        {
            if (isModal.HasValue)
                if (isModal.HasValue)
                    return View("Index", "~/Views/Shared/_NoMenuLayout.cshtml");

            return View();
        }

        public ActionResult Jobs_Read([DataSourceRequest] DataSourceRequest request)
        {
            return Json(GetJobs().ToDataSourceResult(request));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Jobs_Create([DataSourceRequest] DataSourceRequest request, JobViewModel custVM)
        {
            if (custVM != null && ModelState.IsValid)
            {
                MJob cust = new MJob();
                cust.SetAssignedIdTo(Guid.NewGuid().ToString());

                ConvertToJob(custVM, cust);

                cust.CreatedDate = DateTime.Now;
                cust.CreatedBy = User.Identity.Name;
                cust.DataStatus = "New";

                _jobTasks.Insert(cust);
            }

            return Json(new[] { custVM }.ToDataSourceResult(request, ModelState));
        }

        private void ConvertToJob(JobViewModel custVM, MJob cust)
        {
            cust.JobName = custVM.JobName;
            cust.JobUnit = custVM.JobUnit;
            cust.JobPrice = custVM.JobPrice;
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Jobs_Update([DataSourceRequest] DataSourceRequest request, JobViewModel custVM)
        {
            if (custVM != null && ModelState.IsValid)
            {
                var cust = _jobTasks.One(custVM.JobID);
                if (cust != null)
                {
                    ConvertToJob(custVM, cust);

                    cust.ModifiedDate = DateTime.Now;
                    cust.ModifiedBy = User.Identity.Name;
                    cust.DataStatus = "Updated";

                    _jobTasks.Update(cust);
                }
            }

            return Json(ModelState.ToDataSourceResult());
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Jobs_Destroy([DataSourceRequest] DataSourceRequest request, JobViewModel custVM)
        {
            if (custVM != null)
            {
                var cust = _jobTasks.One(custVM.JobID);
                if (cust != null)
                {
                    cust.ModifiedDate = DateTime.Now;
                    cust.ModifiedBy = User.Identity.Name;
                    cust.DataStatus = "Deleted";
                    _jobTasks.Update(cust);
                }
            }
            return Json(ModelState.ToDataSourceResult());
        }

        private IEnumerable<JobViewModel> GetJobs()
        {
            var jobs = this._jobTasks.GetListNotDeleted();

            return from cust in jobs
                   select new JobViewModel
        {
            JobID = cust.Id,
            JobName = cust.JobName,
            JobUnit = cust.JobUnit,
            JobPrice = cust.JobPrice
        };

        }

        public JsonResult PopulateJobs()
        {
            var list = from job in _jobTasks.GetListNotDeleted()
                       select new
                       {
                           Id = job.Id,
                           JobName = job.JobName
                       };
            ViewData["jobs"] = list;
            return Json(list, JsonRequestBehavior.AllowGet);
        }
    }
}
