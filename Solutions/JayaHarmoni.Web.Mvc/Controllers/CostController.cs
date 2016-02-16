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
    public partial class CostController : Controller
    {
        private readonly IMCostTasks _costTasks;
        public CostController(IMCostTasks costTasks)
        {
            this._costTasks = costTasks;
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

        public ActionResult Costs_Read([DataSourceRequest] DataSourceRequest request)
        {
            return Json(GetCosts().ToDataSourceResult(request));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Costs_Create([DataSourceRequest] DataSourceRequest request, CostViewModel custVM)
        {
            if (custVM != null && ModelState.IsValid)
            {
                MCost cust = new MCost();
                cust.SetAssignedIdTo(Guid.NewGuid().ToString());

                ConvertToCost(custVM, cust);

                cust.CreatedDate = DateTime.Now;
                cust.CreatedBy = User.Identity.Name;
                cust.DataStatus = "New";

                _costTasks.Insert(cust);
            }

            return Json(new[] { custVM }.ToDataSourceResult(request, ModelState));
        }

        private void ConvertToCost(CostViewModel custVM, MCost cust)
        {
            cust.CostName = custVM.CostName;
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Costs_Update([DataSourceRequest] DataSourceRequest request, CostViewModel custVM)
        {
            if (custVM != null && ModelState.IsValid)
            {
                var cust = _costTasks.One(custVM.CostID);
                if (cust != null)
                {
                    ConvertToCost(custVM, cust);

                    cust.ModifiedDate = DateTime.Now;
                    cust.ModifiedBy = User.Identity.Name;
                    cust.DataStatus = "Updated";

                    _costTasks.Update(cust);
                }
            }

            return Json(ModelState.ToDataSourceResult());
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Costs_Destroy([DataSourceRequest] DataSourceRequest request, CostViewModel custVM)
        {
            if (custVM != null)
            {
                var cust = _costTasks.One(custVM.CostID);
                if (cust != null)
                {
                    cust.ModifiedDate = DateTime.Now;
                    cust.ModifiedBy = User.Identity.Name;
                    cust.DataStatus = "Deleted";
                    _costTasks.Update(cust);
                }
            }
            return Json(ModelState.ToDataSourceResult());
        }

        private IEnumerable<CostViewModel> GetCosts()
        {
            var costs = this._costTasks.GetListNotDeleted();

            return from cust in costs
                   select new CostViewModel
        {
            CostID = cust.Id,
            CostName = cust.CostName
        };

        }
    }
}
