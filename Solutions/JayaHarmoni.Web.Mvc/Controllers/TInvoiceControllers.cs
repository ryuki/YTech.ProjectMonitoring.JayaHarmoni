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
    public partial class TInvoiceController : Controller
    {
        private readonly ITInvoiceTasks _tasks;
        private readonly ITProjectTasks _TProjectTasks;
        public TInvoiceController(ITInvoiceTasks tasks, ITProjectTasks TProjectTasks)
        {
            this._tasks = tasks;
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

        public ActionResult TInvoices_Read([DataSourceRequest] DataSourceRequest request, string ParentProjectId)
        {
            return Json(GetTInvoices(ParentProjectId).ToDataSourceResult(request));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult TInvoices_Create([DataSourceRequest] DataSourceRequest request, TInvoiceViewModel vm, string ParentProjectId)
        {
            if (vm != null && ModelState.IsValid)
            {
                TInvoice entity = new TInvoice();
                entity.SetAssignedIdTo(Guid.NewGuid().ToString());

                ConvertToTInvoice(vm, entity, ParentProjectId);

                entity.CreatedDate = DateTime.Now;
                entity.CreatedBy = User.Identity.Name;
                entity.DataStatus = EnumDataStatus.New.ToString();

                _tasks.Insert(entity);
            }

            return Json(new[] { vm }.ToDataSourceResult(request, ModelState));
        }

        private void ConvertToTInvoice(TInvoiceViewModel vm, TInvoice entity, string ParentProjectId)
        {
            entity.ProjectId = string.IsNullOrEmpty(ParentProjectId) ? null : _TProjectTasks.One(ParentProjectId);

            entity.InvoicePeriod = new DateTime(vm.InvoicePeriod.Value.Year, vm.InvoicePeriod.Value.Month, 1);
            entity.InvoiceStartDate = vm.InvoiceStartDate;
            entity.InvoiceEndDate = vm.InvoiceEndDate;
            entity.InvoiceNo = vm.InvoiceNo;
            entity.InvoiceDate = vm.InvoiceDate;
            entity.InvoiceLastStatus = vm.InvoiceLastStatus;
            entity.InvoicePaidDate = vm.InvoicePaidDate;
            entity.InvoiceValue = vm.InvoiceValue;
            entity.InvoiceRetention = vm.InvoiceRetention;
            entity.InvoicePpn = vm.InvoicePpn;
            entity.InvoiceTotal = vm.InvoiceTotal;
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult TInvoices_Update([DataSourceRequest] DataSourceRequest request, TInvoiceViewModel vm, string ParentProjectId)
        {
            if (vm != null && ModelState.IsValid)
            {
                var entity = _tasks.One(vm.InvoiceId);
                if (entity != null)
                {
                    ConvertToTInvoice(vm, entity, ParentProjectId);

                    entity.ModifiedDate = DateTime.Now;
                    entity.ModifiedBy = User.Identity.Name;
                    entity.DataStatus = EnumDataStatus.Updated.ToString();

                    _tasks.Update(entity);
                }
            }

            return Json(ModelState.ToDataSourceResult());
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult TInvoices_Destroy([DataSourceRequest] DataSourceRequest request, TInvoiceViewModel vm, string ParentProjectId)
        {
            if (vm != null)
            {
                var entity = _tasks.One(vm.InvoiceId);
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

        private IEnumerable<TInvoiceViewModel> GetTInvoices(string ParentProjectId)
        {
            var entitys = this._tasks.GetListNotDeleted(ParentProjectId);

            return from entity in entitys
                   select new TInvoiceViewModel
        {
            //ProjectId = entity.ProjectId != null ? entity.ProjectId.Id : string.Empty,
            //ProjectName = entity.ProjectId != null ? entity.ProjectId.ProjectName : string.Empty,

            InvoicePeriod = entity.InvoicePeriod,
            InvoiceStartDate = entity.InvoiceStartDate,
            InvoiceEndDate = entity.InvoiceEndDate,
            InvoiceNo = entity.InvoiceNo,
            InvoiceDate = entity.InvoiceDate,
            InvoiceLastStatus = entity.InvoiceLastStatus,
            InvoicePaidDate = entity.InvoicePaidDate,
            InvoiceValue = entity.InvoiceValue,
            InvoiceRetention = entity.InvoiceRetention,
            InvoicePpn = entity.InvoicePpn,
            InvoiceTotal = entity.InvoiceTotal,
            InvoiceId = entity.Id
        };

        }

        public JsonResult PopulateTInvoices()
        {
            var list = from ent in _tasks.GetListNotDeleted()
                       select new
                               {
                                   Id = ent.Id,
                                   InvoiceNo = ent.InvoiceNo
                               };
            ViewData["TInvoices"] = list;
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public JsonResult PopulateInvoiceStatus()
        {
            var list = from EnumInvoiceStatus ent in Enum.GetValues(typeof(EnumInvoiceStatus))
                       select new
                               {
                                   Value = ent.ToString(),
                                   Text = ent.ToString()
                               };
            ViewData["InvoiceStatus"] = list;
            return Json(list, JsonRequestBehavior.AllowGet);
        }
    }
}
