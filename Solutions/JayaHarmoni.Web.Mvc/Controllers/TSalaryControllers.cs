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
    public partial class TSalaryController : Controller
    {
        private readonly ITSalaryTasks _tasks;
        private readonly IMEmployeeTasks _MEmployeeTasks;
        private readonly ITProjectTasks _TProjectTasks;
        private readonly ITAbsentDetTasks _absenDetTasks;
        public TSalaryController(ITSalaryTasks tasks, IMEmployeeTasks _MEmployeeTasks, ITProjectTasks _TProjectTasks, ITAbsentDetTasks _absenDetTasks)
        {
            this._tasks = tasks;
            this._MEmployeeTasks = _MEmployeeTasks;
            this._TProjectTasks = _TProjectTasks;
            this._absenDetTasks = _absenDetTasks;
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

        public ActionResult TSalarys_Read([DataSourceRequest] DataSourceRequest request)
        {
            return Json(GetTSalarys().ToDataSourceResult(request));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult TSalarys_Create([DataSourceRequest] DataSourceRequest request, TSalaryViewModel vm)
        {
            if (vm != null && ModelState.IsValid)
            {
                TSalary entity = new TSalary();
                entity.SetAssignedIdTo(Guid.NewGuid().ToString());

                ConvertToTSalary(vm, entity);

                entity.CreatedDate = DateTime.Now;
                entity.CreatedBy = User.Identity.Name;
                entity.DataStatus = EnumDataStatus.New.ToString();

                _tasks.Insert(entity);
            }

            return Json(new[] { vm }.ToDataSourceResult(request, ModelState));
        }

        private void ConvertToTSalary(TSalaryViewModel vm, TSalary entity)
        {
            MEmployee emp = string.IsNullOrEmpty(vm.EmployeeId) ? null : _MEmployeeTasks.One(vm.EmployeeId);
            entity.EmployeeId = emp;
            entity.ProjectId = string.IsNullOrEmpty(vm.ProjectId) ? null : _TProjectTasks.One(vm.ProjectId);

            entity.SalaryPeriod = new DateTime(vm.SalaryPeriod.Value.Year, vm.SalaryPeriod.Value.Month, 1);

            if (emp != null)
            {
                entity.SalaryGapok = emp.EmployeeBasicSalary;
                entity.SalaryWorkQty = _absenDetTasks.GetTotalQtyByEmp(emp.Id, entity.SalaryPeriod);
                entity.SalaryWorkTotal = emp.EmployeeDailyAllowance * entity.SalaryWorkQty;
                entity.SalaryTotal = entity.SalaryGapok + entity.SalaryWorkTotal;
            }
            //entity.SalaryStatus = vm.SalaryStatus;
            //entity.SalaryDesc = vm.SalaryDesc;
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult TSalarys_Update([DataSourceRequest] DataSourceRequest request, TSalaryViewModel vm)
        {
            if (vm != null && ModelState.IsValid)
            {
                var entity = _tasks.One(vm.SalaryId);
                if (entity != null)
                {
                    ConvertToTSalary(vm, entity);

                    entity.ModifiedDate = DateTime.Now;
                    entity.ModifiedBy = User.Identity.Name;
                    entity.DataStatus = EnumDataStatus.Updated.ToString();

                    _tasks.Update(entity);
                }
            }

            return Json(ModelState.ToDataSourceResult());
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult TSalarys_Destroy([DataSourceRequest] DataSourceRequest request, TSalaryViewModel vm)
        {
            if (vm != null)
            {
                var entity = _tasks.One(vm.SalaryId);
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

        private IEnumerable<TSalaryViewModel> GetTSalarys()
        {
            var entitys = this._tasks.GetListNotDeleted();

            return from entity in entitys
                   select new TSalaryViewModel
        {
            EmployeeId = entity.EmployeeId != null ? entity.EmployeeId.Id : string.Empty,
            EmployeeName = entity.EmployeeId != null ? entity.EmployeeId.EmployeeName : string.Empty,
            ProjectId = entity.ProjectId != null ? entity.ProjectId.Id : string.Empty,
            ProjectName = entity.ProjectId != null ? entity.ProjectId.ProjectName : string.Empty,

            SalaryPeriod = entity.SalaryPeriod,
            SalaryGapok = entity.SalaryGapok,
            SalaryWorkQty = entity.SalaryWorkQty,
            SalaryWorkTotal = entity.SalaryWorkTotal,
            SalaryTotal = entity.SalaryTotal,
            //SalaryStatus = entity.SalaryStatus,
            //SalaryDesc = entity.SalaryDesc,
            SalaryId = entity.Id
        };

        }

        public JsonResult PopulateTSalarys()
        {
            var list = from ent in _tasks.GetListNotDeleted()
                       select new
                               {
                                   Id = ent.Id,
                                   SalaryIdName = ent.SalaryDesc
                               };
            ViewData["TSalarys"] = list;
            return Json(list, JsonRequestBehavior.AllowGet);
        }
    }
}
