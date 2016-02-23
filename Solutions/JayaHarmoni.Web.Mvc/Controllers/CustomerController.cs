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
    public partial class CustomerController : Controller
    {
        private readonly IMCustomerTasks _customerTasks;
        public CustomerController(IMCustomerTasks customerTasks)
        {
            this._customerTasks = customerTasks;
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

        public ActionResult Customers_Read([DataSourceRequest] DataSourceRequest request)
        {
            return Json(GetCustomers().ToDataSourceResult(request));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Customers_Create([DataSourceRequest] DataSourceRequest request, CustomerViewModel custVM)
        {
            if (custVM != null && ModelState.IsValid)
            {
                MCustomer cust = new MCustomer();
                cust.SetAssignedIdTo(Guid.NewGuid().ToString());

                ConvertToCustomer(custVM, cust);

                cust.CreatedDate = DateTime.Now;
                cust.CreatedBy = User.Identity.Name;
                cust.DataStatus = "New";

                _customerTasks.Insert(cust);
            }

            return Json(new[] { custVM }.ToDataSourceResult(request, ModelState));
        }

        private void ConvertToCustomer(CustomerViewModel custVM, MCustomer cust)
        {
            cust.CustomerName = custVM.CustomerName;
            cust.CustomerPhone = custVM.CustomerPhone;
            cust.CustomerAddress = custVM.CustomerAddress;
            cust.CustomerContactName = custVM.CustomerContactName;
            cust.CustomerContactPhone = custVM.CustomerContactPhone;
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Customers_Update([DataSourceRequest] DataSourceRequest request, CustomerViewModel custVM)
        {
            if (custVM != null && ModelState.IsValid)
            {
                var cust = _customerTasks.One(custVM.CustomerID);
                if (cust != null)
                {
                    ConvertToCustomer(custVM, cust);

                    cust.ModifiedDate = DateTime.Now;
                    cust.ModifiedBy = User.Identity.Name;
                    cust.DataStatus = "Updated";

                    _customerTasks.Update(cust);
                }
            }

            return Json(ModelState.ToDataSourceResult());
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Customers_Destroy([DataSourceRequest] DataSourceRequest request, CustomerViewModel custVM)
        {
            if (custVM != null)
            {
                var cust = _customerTasks.One(custVM.CustomerID);
                if (cust != null)
                {
                    cust.ModifiedDate = DateTime.Now;
                    cust.ModifiedBy = User.Identity.Name;
                    cust.DataStatus = "Deleted";
                    _customerTasks.Update(cust);
                }
            }
            return Json(ModelState.ToDataSourceResult());
        }

        private IEnumerable<CustomerViewModel> GetCustomers()
        {
            var customers = this._customerTasks.GetListNotDeleted();

            return from cust in customers
                   select new CustomerViewModel
        {
            CustomerID = cust.Id,
            CustomerName = cust.CustomerName,
            CustomerPhone = cust.CustomerPhone,
            CustomerAddress = cust.CustomerAddress,
            CustomerContactName = cust.CustomerContactName,
            CustomerContactPhone = cust.CustomerContactPhone
        };

        }

        public ActionResult GetLastCreatedCustomer(string random)
        {
            MCustomer cust = _customerTasks.GetLastCreatedCustomer();
            return Content(cust.Id);
        }

        public JsonResult PopulateCustomers()
        {
            var customers = from cust in _customerTasks.GetListNotDeleted()
                            select new
                                    {
                                        Id = cust.Id,
                                        CustomerName = cust.CustomerName + " - " + cust.CustomerPhone
                                    };
            ViewData["customers"] = customers;
            return Json(customers, JsonRequestBehavior.AllowGet);
        }
    }
}
