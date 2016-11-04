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
    public partial class TProjectController : Controller
    {
        private readonly ITProjectTasks _tasks;
        private readonly IMCustomerTasks _custTasks;
        private readonly ITWorkTasks _workTasks;
        private readonly IMJobTasks _jobTasks;
        private readonly ITAbsentTasks _absentTasks;
        private readonly IMEquipTasks _equipTasks;
        private readonly ITAbsentDetTasks _absentDetTasks;
        private readonly IMEmployeeTasks _employeeTasks;
        private readonly ITWorkTasks _TWorkTasks;
        public TProjectController(ITProjectTasks tasks, IMCustomerTasks custTasks, ITWorkTasks workTasks, IMJobTasks jobTasks, ITAbsentTasks absentTasks, IMEquipTasks equipTasks, ITAbsentDetTasks absentDetTasks, IMEmployeeTasks employeeTasks, ITWorkTasks TWorkTasks)
        {
            this._tasks = tasks;
            this._custTasks = custTasks;
            this._workTasks = workTasks;
            this._jobTasks = jobTasks;
            this._absentTasks = absentTasks;
            this._equipTasks = equipTasks;
            this._absentDetTasks = absentDetTasks;
            this._employeeTasks = employeeTasks;
            this._TWorkTasks = TWorkTasks;
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

        #region project

        public ActionResult TProjects_Read([DataSourceRequest] DataSourceRequest request)
        {
            return Json(GetTProjects().ToDataSourceResult(request));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult TProjects_Create([DataSourceRequest] DataSourceRequest request, TProjectViewModel vm)
        {
            if (vm != null && ModelState.IsValid)
            {
                TProject entity = new TProject();
                entity.SetAssignedIdTo(Guid.NewGuid().ToString());

                ConvertToTProject(vm, entity);

                entity.CreatedDate = DateTime.Now;
                entity.CreatedBy = User.Identity.Name;
                entity.DataStatus = EnumDataStatus.New.ToString();

                _tasks.Insert(entity);
            }

            return Json(new[] { vm }.ToDataSourceResult(request, ModelState));
        }

        private void ConvertToTProject(TProjectViewModel vm, TProject entity)
        {
            entity.ProjectName = vm.ProjectName;
            entity.ProjectDate = vm.ProjectDate;
            entity.ProjectPrice = vm.ProjectPrice;
            entity.ProjectRetention = vm.ProjectRetention;
            entity.ProjectLocation = vm.ProjectLocation;
            entity.ProjectStartDate = vm.ProjectStartDate;
            entity.ProjectEndDate = vm.ProjectEndDate;
            entity.ProjectFinishDate = vm.ProjectFinishDate;
            //entity.ProjectStatus = vm.ProjectStatus;
            //entity.ProjectDesc = vm.ProjectDesc;
            entity.CustomerId = string.IsNullOrEmpty(vm.CustomerId) ? null : _custTasks.One(vm.CustomerId);
            entity.ProjectSpkNo = vm.ProjectSpkNo;
            entity.ProjectInvoiceFormat = vm.ProjectInvoiceFormat;
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult TProjects_Update([DataSourceRequest] DataSourceRequest request, TProjectViewModel vm)
        {
            if (vm != null && ModelState.IsValid)
            {
                var entity = _tasks.One(vm.ProjectId);
                if (entity != null)
                {
                    ConvertToTProject(vm, entity);

                    entity.ModifiedDate = DateTime.Now;
                    entity.ModifiedBy = User.Identity.Name;
                    entity.DataStatus = EnumDataStatus.Updated.ToString();

                    _tasks.Update(entity);
                }
            }

            return Json(ModelState.ToDataSourceResult());
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult TProjects_Destroy([DataSourceRequest] DataSourceRequest request, TProjectViewModel vm)
        {
            if (vm != null)
            {
                var entity = _tasks.One(vm.ProjectId);
                if (entity != null)
                {
                    entity.ModifiedDate = DateTime.Now;
                    entity.ModifiedBy = User.Identity.Name;
                    entity.DataStatus = EnumDataStatus.Deleted.ToString();
                    _tasks.Update(entity);
                }
            }
            return Json(ModelState.ToDataSourceResult());
        }

        private IEnumerable<TProjectViewModel> GetTProjects()
        {
            var entitys = this._tasks.GetListNotDeleted();

            return from entity in entitys
                   select new TProjectViewModel
        {
            ProjectName = entity.ProjectName,
            CustomerId = entity.CustomerId != null ? entity.CustomerId.Id : string.Empty,
            CustomerName = entity.CustomerId != null ? entity.CustomerId.CustomerName : string.Empty,
            ProjectDate = entity.ProjectDate,
            ProjectPrice = entity.ProjectPrice,
            ProjectRetention = entity.ProjectRetention,
            ProjectLocation = entity.ProjectLocation,
            ProjectStartDate = entity.ProjectStartDate,
            ProjectEndDate = entity.ProjectEndDate,
            ProjectFinishDate = entity.ProjectFinishDate,
            //ProjectStatus = entity.ProjectStatus,
            //ProjectDesc = entity.ProjectDesc,
            ProjectSpkNo = entity.ProjectSpkNo,
            ProjectInvoiceFormat = entity.ProjectInvoiceFormat,
            ProjectId = entity.Id
        };

        }

        public JsonResult PopulateProjects()
        {
            var list = from proj in _tasks.GetListNotDeleted()
                       select new
                       {
                           Id = proj.Id,
                           ProjectName = proj.ProjectName
                       };
            ViewData["projects"] = list;
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public JsonResult PopulateInvoiceFormat()
        {
            var list = from EnumInvoiceFormat ent in Enum.GetValues(typeof(EnumInvoiceFormat))
                       select new
                       {
                           Value = ent.ToString(),
                           Text = ent.ToString()
                       };
            ViewData["InvoiceFormat"] = list;
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Project Works

        public ActionResult TWorks_Read([DataSourceRequest] DataSourceRequest request, string ParentProjectId)
        {
            return Json(GetTWorks(ParentProjectId).ToDataSourceResult(request));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult TWorks_Create([DataSourceRequest] DataSourceRequest request, TWorkViewModel vm, string ParentProjectId)
        {
            if (vm != null && ModelState.IsValid)
            {
                TWork entity = new TWork();
                entity.SetAssignedIdTo(Guid.NewGuid().ToString());

                ConvertToTWork(vm, entity, ParentProjectId);

                entity.CreatedDate = DateTime.Now;
                entity.CreatedBy = User.Identity.Name;
                entity.DataStatus = "New";

                _workTasks.Insert(entity);
            }

            return Json(new[] { vm }.ToDataSourceResult(request, ModelState));
        }

        private void ConvertToTWork(TWorkViewModel vm, TWork entity, string ParentProjectId)
        {
            entity.WorkQty = vm.WorkQty;
            entity.WorkPrice = vm.WorkPrice;
            entity.WorkTotal = vm.WorkQty * vm.WorkPrice;
            entity.WorkRealQty = vm.WorkRealQty;
            entity.WorkRealPaid = vm.WorkRealPaid;
            entity.JobId = string.IsNullOrEmpty(vm.JobId) ? null : _jobTasks.One(vm.JobId);
            entity.ProjectId = string.IsNullOrEmpty(ParentProjectId) ? null : _tasks.One(ParentProjectId);
            entity.WorkRetentionStatus = vm.WorkRetentionStatus;
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult TWorks_Update([DataSourceRequest] DataSourceRequest request, TWorkViewModel vm, string ParentProjectId)
        {
            if (vm != null && ModelState.IsValid)
            {
                var entity = _workTasks.One(vm.WorkId);
                if (entity != null)
                {
                    ConvertToTWork(vm, entity, ParentProjectId);

                    entity.ModifiedDate = DateTime.Now;
                    entity.ModifiedBy = User.Identity.Name;
                    entity.DataStatus = "Updated";

                    _workTasks.Update(entity);
                }
            }

            return Json(ModelState.ToDataSourceResult());
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult TWorks_Destroy([DataSourceRequest] DataSourceRequest request, TWorkViewModel vm, string ParentProjectId)
        {
            if (vm != null)
            {
                var entity = _workTasks.One(vm.WorkId);
                if (entity != null)
                {
                    entity.ModifiedDate = DateTime.Now;
                    entity.ModifiedBy = User.Identity.Name;
                    entity.DataStatus = "Deleted";
                    _workTasks.Update(entity);
                }
            }
            return Json(ModelState.ToDataSourceResult());
        }

        private IEnumerable<TWorkViewModel> GetTWorks(string ProjectId)
        {
            var entitys = this._workTasks.GetListNotDeleted(ProjectId);

            return from entity in entitys
                   select new TWorkViewModel
                   {
                       JobId = entity.JobId != null ? entity.JobId.Id : string.Empty,
                       JobName = entity.JobId != null ? entity.JobId.JobName : string.Empty,
                       WorkQty = entity.WorkQty,
                       WorkPrice = entity.WorkPrice,
                       WorkTotal = entity.WorkTotal,
                       WorkRealQty = entity.WorkRealQty,
                       WorkRealPaid = entity.WorkRealPaid,
                       WorkRetentionStatus = entity.WorkRetentionStatus,
                       WorkId = entity.Id
                   };

        }

        public JsonResult PopulateWorks(string ParentProjectId)
        {
            var list = from ent in _workTasks.GetListNotDeleted(ParentProjectId)
                       select new
                       {
                           Id = ent.Id,
                           JobName = ent.JobId != null ? ent.JobId.JobName : string.Empty
                       };
            ViewData["works"] = list;
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public JsonResult PopulateWorkRetentionStatus()
        {
            var list = from EnumWorkRetentionStatus ent in Enum.GetValues(typeof(EnumWorkRetentionStatus))
                       select new
                       {
                           Value = ent.ToString(),
                           Text = ent.ToString()
                       };
            ViewData["WorkRetentionStatus"] = list;
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetTWork(string workId)
        {
            var work = _workTasks.One(workId);
            ViewData["GetTWork"] = work;
            return Json(work, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}
