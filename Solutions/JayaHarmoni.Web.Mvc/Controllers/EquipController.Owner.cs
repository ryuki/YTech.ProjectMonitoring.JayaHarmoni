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
    public partial class EquipController : Controller
    {

        [Authorize(Roles = "ADMINISTRATOR, SUPERVISOR, CS")]
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Owner(bool? isModal)
        {
            if (isModal.HasValue)
                if (isModal.HasValue)
                    return View("Index", "~/Views/Shared/_NoMenuLayout.cshtml");

            return View();
        }

        public ActionResult MOwners_Read([DataSourceRequest] DataSourceRequest request, string ParentEquipId)
        {
            return Json(GetMOwners(ParentEquipId).ToDataSourceResult(request));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult MOwners_Create([DataSourceRequest] DataSourceRequest request, MOwnerViewModel vm, string ParentEquipId)
        {
            if (vm != null && ModelState.IsValid)
            {
                MOwner entity = new MOwner();
                entity.SetAssignedIdTo(Guid.NewGuid().ToString());

                ConvertToMOwner(vm, entity, ParentEquipId);

                entity.CreatedDate = DateTime.Now;
                entity.CreatedBy = User.Identity.Name;
                entity.DataStatus = EnumDataStatus.New.ToString();

                _MOwnerTasks.Insert(entity);
            }

            return Json(new[] { vm }.ToDataSourceResult(request, ModelState));
        }

        private void ConvertToMOwner(MOwnerViewModel vm, MOwner entity, string ParentEquipId)
        {
            entity.EquipId = string.IsNullOrEmpty(ParentEquipId) ? null : _equipTasks.One(ParentEquipId);

            entity.OwnerName = vm.OwnerName;
            entity.OwnerPercent = vm.OwnerPercent;
            entity.OwnerSinceDate = vm.OwnerSinceDate;
            entity.OwnerUntilDate = vm.OwnerUntilDate;
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult MOwners_Update([DataSourceRequest] DataSourceRequest request, MOwnerViewModel vm, string ParentEquipId)
        {
            if (vm != null && ModelState.IsValid)
            {
                var entity = _MOwnerTasks.One(vm.OwnerId);
                if (entity != null)
                {
                    ConvertToMOwner(vm, entity, ParentEquipId);

                    entity.ModifiedDate = DateTime.Now;
                    entity.ModifiedBy = User.Identity.Name;
                    entity.DataStatus = EnumDataStatus.Updated.ToString();

                    _MOwnerTasks.Update(entity);
                }
            }

            return Json(ModelState.ToDataSourceResult());
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult MOwners_Destroy([DataSourceRequest] DataSourceRequest request, MOwnerViewModel vm, string ParentEquipId)
        {
            if (vm != null)
            {
                var entity = _MOwnerTasks.One(vm.OwnerId);
                if (entity != null)
                {
                    entity.ModifiedDate = DateTime.Now;
                    entity.ModifiedBy = User.Identity.Name;
                    entity.DataStatus = EnumDataStatus.Deleted.ToString();
                    _MOwnerTasks.Update(entity);
                }
            }
            return Json(ModelState.ToDataSourceResult());
        }

        private IEnumerable<MOwnerViewModel> GetMOwners(string ParentEquipId)
        {
            var entitys = this._MOwnerTasks.GetListNotDeleted(ParentEquipId);

            return from entity in entitys
                   select new MOwnerViewModel
        {
            OwnerName = entity.OwnerName,
            OwnerPercent = entity.OwnerPercent,
            OwnerSinceDate = entity.OwnerSinceDate,
            OwnerUntilDate = entity.OwnerUntilDate,
            OwnerId = entity.Id
        };

        }

        public JsonResult PopulateMOwners()
        {
            var list = from ent in _MOwnerTasks.GetListNotDeleted()
                       select new
                               {
                                   Id = ent.Id,
                                   OwnerName = ent.OwnerName
                               };
            ViewData["MOwners"] = list;
            return Json(list, JsonRequestBehavior.AllowGet);
        }
    }
}
