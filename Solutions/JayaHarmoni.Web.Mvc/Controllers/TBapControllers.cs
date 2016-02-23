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
    public partial class TBapController : Controller
    {
        private readonly ITBapTasks _tasks;
        private readonly ITProjectTasks _TProjectTasks;
        private readonly ITWorkTasks _TWorkTasks;
        public TBapController(ITBapTasks tasks, ITProjectTasks TProjectTasks, ITWorkTasks TWorkTasks)
        {
            this._tasks = tasks;
            this._TProjectTasks = TProjectTasks;
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

        public ActionResult TBaps_Read([DataSourceRequest] DataSourceRequest request, string ParentProjectId)
        {
            return Json(GetTBaps(ParentProjectId).ToDataSourceResult(request));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult TBaps_Create([DataSourceRequest] DataSourceRequest request, TBapViewModel vm, string ParentProjectId)
        {
            if (vm != null && ModelState.IsValid)
            {
                TBap entity = new TBap();
                entity.SetAssignedIdTo(Guid.NewGuid().ToString());

                ConvertToTBap(vm, entity, ParentProjectId);

                entity.CreatedDate = DateTime.Now;
                entity.CreatedBy = User.Identity.Name;
                entity.DataStatus = EnumDataStatus.New.ToString();

                _tasks.Insert(entity);
            }

            return Json(new[] { vm }.ToDataSourceResult(request, ModelState));
        }

        private void ConvertToTBap(TBapViewModel vm, TBap entity, string ParentProjectId)
        {
            entity.ProjectId = string.IsNullOrEmpty(ParentProjectId) ? null : _TProjectTasks.One(ParentProjectId);
            entity.WorkId = string.IsNullOrEmpty(vm.WorkId) ? null : _TWorkTasks.One(vm.WorkId);

            entity.BapPeriod = new DateTime(vm.BapPeriod.Value.Year, vm.BapPeriod.Value.Month, 1);
            entity.BapQty = vm.BapQty;
            entity.BapTotal = vm.BapTotal;
            entity.BapStatus = vm.BapStatus;
            entity.BapDesc = vm.BapDesc;
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult TBaps_Update([DataSourceRequest] DataSourceRequest request, TBapViewModel vm, string ParentProjectId)
        {
            if (vm != null && ModelState.IsValid)
            {
                var entity = _tasks.One(vm.BapId);
                if (entity != null)
                {
                    ConvertToTBap(vm, entity, ParentProjectId);

                    entity.ModifiedDate = DateTime.Now;
                    entity.ModifiedBy = User.Identity.Name;
                    entity.DataStatus = EnumDataStatus.Updated.ToString();

                    _tasks.Update(entity);
                }
            }

            return Json(ModelState.ToDataSourceResult());
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult TBaps_Destroy([DataSourceRequest] DataSourceRequest request, TBapViewModel vm, string ParentProjectId)
        {
            if (vm != null)
            {
                var entity = _tasks.One(vm.BapId);
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

        private IEnumerable<TBapViewModel> GetTBaps(string ParentProjectId)
        {
            var entitys = this._tasks.GetListNotDeleted(ParentProjectId);

            return from entity in entitys
                   select new TBapViewModel
        {
            //ProjectId = entity.ProjectId != null ? entity.ProjectId.Id : string.Empty,
            //ProjectName = entity.ProjectId != null ? entity.ProjectId.ProjectName : string.Empty,
            WorkId = entity.WorkId != null ? entity.WorkId.Id : string.Empty,
            WorkName = entity.WorkId != null ? entity.WorkId.JobId.JobName : string.Empty,

            BapPeriod = entity.BapPeriod,
            BapQty = entity.BapQty,
            BapTotal = entity.BapTotal,
            BapStatus = entity.BapStatus,
            BapDesc = entity.BapDesc,
            BapId = entity.Id
        };

        }

        public JsonResult PopulateTBaps()
        {
            var list = from ent in _tasks.GetListNotDeleted()
                       select new
                               {
                                   Id = ent.Id,
                                   BapDesc = ent.BapDesc
                               };
            ViewData["TBaps"] = list;
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        #region TBAP And Absent Result
        
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult ResultBapAndAbsent(bool? isModal)
        {
            if (isModal.HasValue)
                if (isModal.HasValue)
                    return View("Index", "~/Views/Shared/_NoMenuLayout.cshtml");

            return View();
        }

        public ActionResult ResultBapAndAbsent_Read([DataSourceRequest] DataSourceRequest request, string ParentProjectId)
        {
            var list = GetResultBapAndAbsent(ParentProjectId).ToList();
            return Json(list.ToDataSourceResult(request));
        }

        private IEnumerable<ResultBapAndAbsentViewModel> GetResultBapAndAbsent(string ParentProjectId)
        {
            var entitys = this._tasks.GetListResultBapAndAbsent(ParentProjectId);
            return from ent in entitys
                   select new ResultBapAndAbsentViewModel
                   {
                       //ProjectId= (string)((object[])ent)[0],
                       ProjectId = Guid.NewGuid().ToString(),
                       ProjectName = (string)((object[])ent)[1],
                       BapPeriod = (DateTime?)((object[])ent)[2],
                       WorkId = (string)((object[])ent)[3],
                       JobName = (string)((object[])ent)[4],
                       BapQty = (decimal?)((object[])ent)[5],
                       SumAbsentDetResult = (decimal?)((object[])ent)[6]
                   };
        }

        public JsonResult PopulateTBapAndAbsent()
        {
            IEnumerable<object> query = _tasks.GetListResultBapAndAbsent(string.Empty);
            var list = from ent in query
                       select new ResultBapAndAbsentViewModel
                       {
                           ProjectId = (string)((object[])ent)[0],
                           ProjectName = (string)((object[])ent)[1],
                           BapPeriod = (DateTime?)((object[])ent)[2],
                           WorkId = (string)((object[])ent)[3],
                           JobName = (string)((object[])ent)[4],
                           BapQty = (decimal?)((object[])ent)[5],
                           SumAbsentDetResult = (decimal?)((object[])ent)[6]
                       };
            ViewData["TBaps"] = list;
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        #endregion
    }
}
