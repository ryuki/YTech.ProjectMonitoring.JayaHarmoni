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
        public TProjectController(ITProjectTasks tasks)
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
                entity.DataStatus = "New";

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
            entity.ProjectStatus = vm.ProjectStatus;
            entity.ProjectDesc = vm.ProjectDesc;
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
                    entity.DataStatus = "Updated";

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
                    entity.DataStatus = "Deleted";
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
            ProjectDate = entity.ProjectDate,
            ProjectPrice = entity.ProjectPrice,
            ProjectRetention = entity.ProjectRetention,
            ProjectLocation = entity.ProjectLocation,
            ProjectStartDate = entity.ProjectStartDate,
            ProjectEndDate = entity.ProjectEndDate,
            ProjectFinishDate = entity.ProjectFinishDate,
            ProjectStatus = entity.ProjectStatus,
            ProjectDesc = entity.ProjectDesc,
            ProjectId = entity.Id
        };

        }
    }
}
