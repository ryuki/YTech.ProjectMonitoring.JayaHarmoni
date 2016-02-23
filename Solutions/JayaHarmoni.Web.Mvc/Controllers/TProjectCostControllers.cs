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
    public partial class TProjectCostController : Controller
    {
        private readonly ITProjectCostTasks _tasks;
        private readonly IMCostTasks _MCostTasks;
        private readonly IMEquipTasks _MEquipTasks;
        private readonly ITProjectTasks _TProjectTasks;
        public TProjectCostController(ITProjectCostTasks tasks, IMCostTasks MCostTasks, IMEquipTasks MEquipTasks, ITProjectTasks TProjectTasks)
        {
            this._tasks = tasks;
            this._MCostTasks = MCostTasks;
            this._MEquipTasks = MEquipTasks;
            this._TProjectTasks = TProjectTasks;
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

        public ActionResult TProjectCosts_Read([DataSourceRequest] DataSourceRequest request)
        {
            return Json(GetTProjectCosts().ToDataSourceResult(request));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult TProjectCosts_Create([DataSourceRequest] DataSourceRequest request, TProjectCostViewModel vm)
        {
            if (vm != null && ModelState.IsValid)
            {
                TProjectCost entity = new TProjectCost();
                entity.SetAssignedIdTo(Guid.NewGuid().ToString());

                ConvertToTProjectCost(vm, entity);

                entity.CreatedDate = DateTime.Now;
                entity.CreatedBy = User.Identity.Name;
                entity.DataStatus = EnumDataStatus.New.ToString();

                _tasks.Insert(entity);
            }

            return Json(new[] { vm }.ToDataSourceResult(request, ModelState));
        }

        private void ConvertToTProjectCost(TProjectCostViewModel vm, TProjectCost entity)
        {
            entity.CostId = string.IsNullOrEmpty(vm.CostId) ? null : _MCostTasks.One(vm.CostId);
            entity.EquipId = string.IsNullOrEmpty(vm.EquipId) ? null : _MEquipTasks.One(vm.EquipId);
            entity.ProjectId = string.IsNullOrEmpty(vm.ProjectId) ? null : _TProjectTasks.One(vm.ProjectId);

            entity.ProjectCostDate = vm.ProjectCostDate;
            entity.ProjectCostQty = vm.ProjectCostQty;
            entity.ProjectCostTotal = vm.ProjectCostTotal;
            entity.ProjectCostDesc = vm.ProjectCostDesc;
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult TProjectCosts_Update([DataSourceRequest] DataSourceRequest request, TProjectCostViewModel vm)
        {
            if (vm != null && ModelState.IsValid)
            {
                var entity = _tasks.One(vm.ProjectCostId);
                if (entity != null)
                {
                    ConvertToTProjectCost(vm, entity);

                    entity.ModifiedDate = DateTime.Now;
                    entity.ModifiedBy = User.Identity.Name;
                    entity.DataStatus = EnumDataStatus.Updated.ToString();

                    _tasks.Update(entity);
                }
            }

            return Json(ModelState.ToDataSourceResult());
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult TProjectCosts_Destroy([DataSourceRequest] DataSourceRequest request, TProjectCostViewModel vm)
        {
            if (vm != null)
            {
                var entity = _tasks.One(vm.ProjectCostId);
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

        private IEnumerable<TProjectCostViewModel> GetTProjectCosts()
        {
            var entitys = this._tasks.GetListNotDeleted();

            return from entity in entitys
                   select new TProjectCostViewModel
        {
            CostId = entity.CostId != null ? entity.CostId.Id : string.Empty,
            CostName = entity.CostId != null ? entity.CostId.CostName : string.Empty,
            EquipId = entity.EquipId != null ? entity.EquipId.Id : string.Empty,
            EquipName = entity.EquipId != null ? entity.EquipId.EquipName : string.Empty,
            ProjectId = entity.ProjectId != null ? entity.ProjectId.Id : string.Empty,
            ProjectName = entity.ProjectId != null ? entity.ProjectId.ProjectName : string.Empty,

            ProjectCostDate = entity.ProjectCostDate,
            ProjectCostQty = entity.ProjectCostQty,
            ProjectCostTotal = entity.ProjectCostTotal,
            ProjectCostDesc = entity.ProjectCostDesc,
            ProjectCostId = entity.Id
        };

        }

        public JsonResult PopulateTProjectCosts()
        {
            var list = from ent in _tasks.GetListNotDeleted()
                       select new
                               {
                                   Id = ent.Id,
                                   ProjectCostDesc = ent.ProjectCostDesc
                               };
            ViewData["TProjectCosts"] = list;
            return Json(list, JsonRequestBehavior.AllowGet);
        }
    }
}
