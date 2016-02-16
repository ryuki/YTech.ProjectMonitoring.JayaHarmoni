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
    public partial class EmployeeController : Controller
    {
        private readonly IMEmployeeTasks _employeeTasks;
        public EmployeeController(IMEmployeeTasks employeeTasks)
        {
            this._employeeTasks = employeeTasks;
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

        public ActionResult Employees_Read([DataSourceRequest] DataSourceRequest request)
        {
            return Json(GetEmployees().ToDataSourceResult(request));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Employees_Create([DataSourceRequest] DataSourceRequest request, EmployeeViewModel custVM)
        {
            if (custVM != null && ModelState.IsValid)
            {
                MEmployee cust = new MEmployee();
                cust.SetAssignedIdTo(Guid.NewGuid().ToString());

                ConvertToEmployee(custVM, cust);

                cust.CreatedDate = DateTime.Now;
                cust.CreatedBy = User.Identity.Name;
                cust.DataStatus = "New";

                _employeeTasks.Insert(cust);
            }

            return Json(new[] { custVM }.ToDataSourceResult(request, ModelState));
        }

        private void ConvertToEmployee(EmployeeViewModel custVM, MEmployee cust)
        {
            cust.EmployeeName = custVM.EmployeeName;
            cust.EmployeePhone = custVM.EmployeePhone;
            cust.EmployeeAddress = custVM.EmployeeAddress;
            cust.EmployeeJoinDate = custVM.EmployeeJoinDate;
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Employees_Update([DataSourceRequest] DataSourceRequest request, EmployeeViewModel custVM)
        {
            if (custVM != null && ModelState.IsValid)
            {
                var cust = _employeeTasks.One(custVM.EmployeeID);
                if (cust != null)
                {
                    ConvertToEmployee(custVM, cust);

                    cust.ModifiedDate = DateTime.Now;
                    cust.ModifiedBy = User.Identity.Name;
                    cust.DataStatus = "Updated";

                    _employeeTasks.Update(cust);
                }
            }

            return Json(ModelState.ToDataSourceResult());
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Employees_Destroy([DataSourceRequest] DataSourceRequest request, EmployeeViewModel custVM)
        {
            if (custVM != null)
            {
                var cust = _employeeTasks.One(custVM.EmployeeID);
                if (cust != null)
                {
                    cust.ModifiedDate = DateTime.Now;
                    cust.ModifiedBy = User.Identity.Name;
                    cust.DataStatus = "Deleted";
                    _employeeTasks.Update(cust);
                }
            }
            return Json(ModelState.ToDataSourceResult());
        }

        private IEnumerable<EmployeeViewModel> GetEmployees()
        {
            var employees = this._employeeTasks.GetListNotDeleted();

            return from cust in employees
                   select new EmployeeViewModel
        {
            EmployeeID = cust.Id,
            EmployeeName = cust.EmployeeName,
            EmployeePhone = cust.EmployeePhone,
            EmployeeAddress = cust.EmployeeAddress,
            EmployeeJoinDate = cust.EmployeeJoinDate
        };

        }

    }
}
