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
using JayaHarmoni.Enums;

namespace JayaHarmoni.Web.Mvc.Controllers
{
    [HandleError]
    [Authorize]
    public partial class MJobController : Controller
    {
       private readonly IMJobTasks _tasks;
        public MJobController(IMJobTasks tasks)
        {
            this._tasks = tasks;
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

        public ActionResult MJobs_Read([DataSourceRequest] DataSourceRequest request)
        {
            return Json(GetMJobs().ToDataSourceResult(request));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult MJobs_Create([DataSourceRequest] DataSourceRequest request, MJobViewModel vm)
        {
            if (vm != null && ModelState.IsValid)
            {
                MJob entity = new MJob();
                entity.SetAssignedIdTo(Guid.NewGuid().ToString());

                ConvertToMJob(vm, entity);

                entity.CreatedDate = DateTime.Now;
                entity.CreatedBy = User.Identity.Name;
                entity.DataStatus = "New";

                _tasks.Insert(entity);
            }

            return Json(new[] { vm }.ToDataSourceResult(request, ModelState));
        }

        private void ConvertToMJob(MJobViewModel vm, MJob entity)
        {
            entity.JobName = vm.JobName;
            entity.JobUnit = vm.JobUnit;
            entity.JobPrice = vm.JobPrice;
            entity.JobStatus = vm.JobStatus;
            entity.JobDesc = vm.JobDesc;
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult MJobs_Update([DataSourceRequest] DataSourceRequest request, MJobViewModel vm)
        {
            if (vm != null && ModelState.IsValid)
            {
                var entity = _tasks.One(vm.JobId);
                if (entity != null)
                {
                    ConvertToMJob(vm, entity);

                    entity.ModifiedDate = DateTime.Now;
                    entity.ModifiedBy = User.Identity.Name;
                    entity.DataStatus = "Updated";

                    _tasks.Update(entity);
                }
            }

            return Json(ModelState.ToDataSourceResult());
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult MJobs_Destroy([DataSourceRequest] DataSourceRequest request, MJobViewModel vm)
        {
            if (vm != null)
            {
                var entity = _tasks.One(vm.JobId);
                if (entity != null)
                {
                    entity.ModifiedDate = DateTime.Now;
                    entity.ModifiedBy = User.Identity.Name;
                    entity.DataStatus = "Deleted";
                    _tasks.Update(entity);
                }
            }
            return Json(ModelState.ToDataSourceResult());
        }

        private IEnumerable<MJobViewModel> GetMJobs()
        {
            var entitys = this._tasks.GetListNotDeleted();

            return from entity in entitys
                   select new MJobViewModel
        {
            JobName = entity.JobName,
            JobUnit = entity.JobUnit,
            JobPrice = entity.JobPrice,
            JobStatus = entity.JobStatus,
            JobDesc = entity.JobDesc,
            JobId = entity.Id
        };

        }
    }
}
