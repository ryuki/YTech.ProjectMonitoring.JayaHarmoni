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
    public partial class TProjectController : Controller
    {
        [Authorize(Roles = "ADMINISTRATOR, SUPERVISOR, CS")]
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult ProjectAbsent(bool? isModal)
        {
            if (isModal.HasValue)
                if (isModal.HasValue)
                    return View("Index", "~/Views/Shared/_NoMenuLayout.cshtml");

            return View();
        }

        public ActionResult TAbsents_Read([DataSourceRequest] DataSourceRequest request)
        {
            return Json(GetTAbsents().ToDataSourceResult(request));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult TAbsents_Create([DataSourceRequest] DataSourceRequest request, TAbsentViewModel vm)
        {
            if (vm != null && ModelState.IsValid)
            {
                TAbsent entity = new TAbsent();
                entity.SetAssignedIdTo(Guid.NewGuid().ToString());

                ConvertToTAbsent(vm, entity);

                entity.CreatedDate = DateTime.Now;
                entity.CreatedBy = User.Identity.Name;
                entity.DataStatus = "New";

                _absentTasks.Insert(entity);

                int totalDays = entity.AbsentPeriod.Value.AddMonths(1).AddDays(-1).Day;
                DateTime beginDate = entity.AbsentPeriod.Value;
                TAbsentDet det;
                for (int i = 0; i < totalDays; i++)
                {
                    det = new TAbsentDet();
                    det.SetAssignedIdTo(Guid.NewGuid().ToString());

                    det.AbsentDetDate = beginDate.AddDays(i);
                    det.AbsentId = entity;

                    det.CreatedDate = DateTime.Now;
                    det.CreatedBy = User.Identity.Name;
                    det.DataStatus = "New";

                    _absentDetTasks.Insert(det);
                }
            }

            return Json(new[] { vm }.ToDataSourceResult(request, ModelState));
        }

        private void ConvertToTAbsent(TAbsentViewModel vm, TAbsent entity)
        {
            entity.AbsentPeriod = new DateTime(vm.AbsentPeriod.Value.Year, vm.AbsentPeriod.Value.Month, 1);
            entity.AbsentLocation = vm.AbsentLocation;
            entity.AbsentSupervisor = vm.AbsentSupervisor;
            entity.AbsentAdmin = vm.AbsentAdmin;
            entity.AbsentTotalQty = vm.AbsentTotalQty;
            entity.AbsentTotalResult = vm.AbsentTotalResult;
            entity.AbsentTotalBbm = vm.AbsentTotalBbm;
            entity.EquipId = string.IsNullOrEmpty(vm.EquipId) ? null : _equipTasks.One(vm.EquipId);
            entity.ProjectId = string.IsNullOrEmpty(vm.ProjectId) ? null : _tasks.One(vm.ProjectId);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult TAbsents_Update([DataSourceRequest] DataSourceRequest request, TAbsentViewModel vm)
        {
            if (vm != null && ModelState.IsValid)
            {
                var entity = _absentTasks.One(vm.AbsentId);
                if (entity != null)
                {
                    ConvertToTAbsent(vm, entity);

                    entity.ModifiedDate = DateTime.Now;
                    entity.ModifiedBy = User.Identity.Name;
                    entity.DataStatus = "Updated";

                    _absentTasks.Update(entity);
                }
            }

            return Json(ModelState.ToDataSourceResult());
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult TAbsents_Destroy([DataSourceRequest] DataSourceRequest request, TAbsentViewModel vm)
        {
            if (vm != null)
            {
                var entity = _absentTasks.One(vm.AbsentId);
                if (entity != null)
                {
                    entity.ModifiedDate = DateTime.Now;
                    entity.ModifiedBy = User.Identity.Name;
                    entity.DataStatus = "Deleted";
                    _absentTasks.Update(entity);
                }
            }
            return Json(ModelState.ToDataSourceResult());
        }

        private IEnumerable<TAbsentViewModel> GetTAbsents()
        {
            var entitys = this._absentTasks.GetListNotDeleted();

            return from entity in entitys
                   select new TAbsentViewModel
        {
            AbsentPeriod = entity.AbsentPeriod,
            AbsentLocation = entity.AbsentLocation,
            AbsentSupervisor = entity.AbsentSupervisor,
            AbsentAdmin = entity.AbsentAdmin,
            AbsentTotalQty = entity.AbsentTotalQty,
            AbsentTotalResult = entity.AbsentTotalResult,
            AbsentTotalBbm = entity.AbsentTotalBbm,
            AbsentId = entity.Id,
            EquipId = entity.EquipId != null ? entity.EquipId.Id : string.Empty,
            EquipName = entity.EquipId != null ? entity.EquipId.EquipName : string.Empty,
            ProjectId = entity.ProjectId != null ? entity.ProjectId.Id : string.Empty,
            ProjectName = entity.ProjectId != null ? entity.ProjectId.ProjectName : string.Empty
        };

        }

        #region Absent detail

        public ActionResult TAbsentDets_Read([DataSourceRequest] DataSourceRequest request,string ParentAbsentId)
        {
            return Json(GetTAbsentDets(ParentAbsentId).ToDataSourceResult(request));
        }

        //[AcceptVerbs(HttpVerbs.Post)]
        //public ActionResult TAbsentDets_Create([DataSourceRequest] DataSourceRequest request, TAbsentDetViewModel vm, string ParentAbsentId)
        //{
        //    if (vm != null && ModelState.IsValid)
        //    {
        //        TAbsentDet entity = new TAbsentDet();
        //        entity.SetAssignedIdTo(Guid.NewGuid().ToString());

        //        ConvertToTAbsentDet(vm, entity,ParentAbsentId);

        //        entity.CreatedDate = DateTime.Now;
        //        entity.CreatedBy = User.Identity.Name;
        //        entity.DataStatus = "New";

        //        _absentDetTasks.Insert(entity);
        //    }

        //    return Json(new[] { vm }.ToDataSourceResult(request, ModelState));
        //}

        private void ConvertToTAbsentDet(TAbsentDetViewModel vm, TAbsentDet entity, string ParentAbsentId)
        {
            //date is read only
            //entity.AbsentDetDate = vm.AbsentDetDate;
            entity.AbsentDetStart = vm.AbsentDetStart;
            entity.AbsentDetEnd = vm.AbsentDetEnd;
            entity.AbsentDetQty = vm.AbsentDetEnd - vm.AbsentDetStart;
            entity.AbsentDetBlock = vm.AbsentDetBlock;
            entity.AbsentDetResult = vm.AbsentDetResult;
            entity.AbsentDetBbm = vm.AbsentDetBbm;
            entity.AbsentDetDesc = vm.AbsentDetDesc;
            entity.AbsentId = string.IsNullOrEmpty(ParentAbsentId) ? null : _absentTasks.One(ParentAbsentId);
            entity.AbsentDetOperator = string.IsNullOrEmpty(vm.AbsentDetOperator) ? null : _employeeTasks.One(vm.AbsentDetOperator);
            entity.AbsentDetSinso = string.IsNullOrEmpty(vm.AbsentDetSinso) ? null : _employeeTasks.One(vm.AbsentDetSinso);
            entity.WorkId = string.IsNullOrEmpty(vm.WorkId) ? null : _TWorkTasks.One(vm.WorkId);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult TAbsentDets_Update([DataSourceRequest] DataSourceRequest request, TAbsentDetViewModel vm, string ParentAbsentId)
        {
            if (vm != null && ModelState.IsValid)
            {
                var entity = _absentDetTasks.One(vm.AbsentDetId);
                if (entity != null)
                {
                    ConvertToTAbsentDet(vm, entity, ParentAbsentId);

                    entity.ModifiedDate = DateTime.Now;
                    entity.ModifiedBy = User.Identity.Name;
                    entity.DataStatus = "Updated";

                    _absentDetTasks.Update(entity);

                    //calculate total
                    CalculateTotalAbsent(ParentAbsentId, entity.AbsentId);
                }
            }

            return Json(ModelState.ToDataSourceResult());
        }

        private void CalculateTotalAbsent(string ParentAbsentId, TAbsent absent)
        {
            var entitys = this._absentDetTasks.GetListNotDeleted(ParentAbsentId);
            absent.AbsentTotalQty = entitys.Sum(m => m.AbsentDetQty);
            absent.AbsentTotalResult = entitys.Sum(m => m.AbsentDetResult);
            absent.AbsentTotalBbm = entitys.Sum(m => m.AbsentDetBbm);

            absent.ModifiedDate = DateTime.Now;
            absent.ModifiedBy = User.Identity.Name;
            absent.DataStatus = EnumDataStatus.Updated.ToString();
            _absentTasks.Update(absent);
        }

        private IEnumerable<TAbsentDetViewModel> GetTAbsentDets(string ParentAbsentId)
        {
            var entitys = this._absentDetTasks.GetListNotDeleted(ParentAbsentId);

            return from entity in entitys
                   select new TAbsentDetViewModel
                   {
                       AbsentDetDate = entity.AbsentDetDate,
                       AbsentDetStart = entity.AbsentDetStart,
                       AbsentDetEnd = entity.AbsentDetEnd,
                       AbsentDetQty = entity.AbsentDetQty,
                       AbsentDetBlock = entity.AbsentDetBlock,
                       AbsentDetResult = entity.AbsentDetResult,
                       AbsentDetBbm = entity.AbsentDetBbm,
                       AbsentDetDesc = entity.AbsentDetDesc,
                       AbsentDetId = entity.Id,
                       AbsentDetOperator = entity.AbsentDetOperator != null ? entity.AbsentDetOperator.Id : string.Empty,
                       AbsentDetSinso = entity.AbsentDetSinso != null ? entity.AbsentDetSinso.Id : string.Empty,
                       AbsentDetOperatorName = entity.AbsentDetOperator != null ? entity.AbsentDetOperator.EmployeeName : string.Empty,
                       AbsentDetSinsoName = entity.AbsentDetSinso != null ? entity.AbsentDetSinso.EmployeeName : string.Empty,
                       WorkId = entity.WorkId != null ? entity.WorkId.Id : string.Empty,
                       WorkName = entity.WorkId != null ? entity.WorkId.JobId.JobName : string.Empty
                   };

        }

        #endregion
    }
}
