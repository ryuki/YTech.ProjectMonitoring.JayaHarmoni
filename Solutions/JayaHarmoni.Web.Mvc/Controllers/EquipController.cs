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
    public partial class EquipController : Controller
    {
        private readonly IMEquipTasks _equipTasks;
        private readonly IMOwnerTasks _MOwnerTasks;
        public EquipController(IMEquipTasks equipTasks, IMOwnerTasks MOwnerTasks)
        {
            this._equipTasks = equipTasks;
            this._MOwnerTasks = MOwnerTasks;
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

        public ActionResult Equips_Read([DataSourceRequest] DataSourceRequest request)
        {
            return Json(GetEquips().ToDataSourceResult(request));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Equips_Create([DataSourceRequest] DataSourceRequest request, EquipViewModel custVM)
        {
            if (custVM != null && ModelState.IsValid)
            {
                MEquip cust = new MEquip();
                cust.SetAssignedIdTo(custVM.EquipID);

                ConvertToEquip(custVM, cust);

                cust.CreatedDate = DateTime.Now;
                cust.CreatedBy = User.Identity.Name;
                cust.DataStatus = "New";

                _equipTasks.Insert(cust);
            }

            return Json(new[] { custVM }.ToDataSourceResult(request, ModelState));
        }

        private void ConvertToEquip(EquipViewModel custVM, MEquip cust)
        {
            cust.EquipName = custVM.EquipName;
            cust.EquipType = custVM.EquipType;
            cust.EquipBrand = custVM.EquipBrand;
            cust.EquipBuyDate = custVM.EquipBuyDate;
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Equips_Update([DataSourceRequest] DataSourceRequest request, EquipViewModel custVM)
        {
            if (custVM != null && ModelState.IsValid)
            {
                var cust = _equipTasks.One(custVM.EquipID);
                if (cust != null)
                {
                    ConvertToEquip(custVM, cust);

                    cust.ModifiedDate = DateTime.Now;
                    cust.ModifiedBy = User.Identity.Name;
                    cust.DataStatus = "Updated";

                    _equipTasks.Update(cust);
                }
            }

            return Json(ModelState.ToDataSourceResult());
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Equips_Destroy([DataSourceRequest] DataSourceRequest request, EquipViewModel custVM)
        {
            if (custVM != null)
            {
                var cust = _equipTasks.One(custVM.EquipID);
                if (cust != null)
                {
                    cust.ModifiedDate = DateTime.Now;
                    cust.ModifiedBy = User.Identity.Name;
                    cust.DataStatus = "Deleted";
                    _equipTasks.Update(cust);
                }
            }
            return Json(ModelState.ToDataSourceResult());
        }

        private IEnumerable<EquipViewModel> GetEquips()
        {
            var equips = this._equipTasks.GetListNotDeleted();

            return from cust in equips
                   select new EquipViewModel
        {
            EquipID = cust.Id,
            EquipName = cust.EquipName,
            EquipType = cust.EquipType,
            EquipBrand = cust.EquipBrand,
            EquipBuyDate = cust.EquipBuyDate
        };

        }

        public JsonResult PopulateEquips()
        {
            var list = from equip in _equipTasks.GetListNotDeleted()
                       select new
                       {
                           Id = equip.Id,
                           EquipName = equip.EquipName
                       };
            ViewData["equips"] = list;
            return Json(list, JsonRequestBehavior.AllowGet);
        }
    }
}
