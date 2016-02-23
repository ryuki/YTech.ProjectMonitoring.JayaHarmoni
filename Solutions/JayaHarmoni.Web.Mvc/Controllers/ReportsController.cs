﻿using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JayaHarmoni.Domain.Contracts.Tasks;
using JayaHarmoni.Enums;
using JayaHarmoni.Web.Mvc.Controllers.ViewModels;
using Microsoft.CSharp;
using JayaHarmoni.Domain;

namespace JayaHarmoni.Web.Mvc.Controllers
{
    [HandleError]
    [Authorize]
    public class ReportsController : Controller
    {
        private readonly ITBapTasks _bapTasks;
        private readonly IMCustomerTasks _customerTasks;
        private readonly IMEmployeeTasks _empTasks;
        private readonly IMEquipTasks _equipTasks;
        private readonly ITInvoiceTasks _InvoiceTasks;
        private readonly ITProjectTasks _TProjectTasks;
        private readonly ITBapTasks _Baptasks;
        public ReportsController(IMCustomerTasks customerTasks, ITBapTasks bapTasks, IMEmployeeTasks empTasks, IMEquipTasks equipTasks, ITInvoiceTasks _InvoiceTasks, ITProjectTasks _TProjectTasks, ITBapTasks _Baptasks)
        {
            this._bapTasks = bapTasks;
            this._customerTasks = customerTasks;
            this._empTasks = empTasks;
            this._equipTasks = equipTasks;
            this._InvoiceTasks = _InvoiceTasks;
            this._TProjectTasks = _TProjectTasks;
            this._Baptasks = _Baptasks;
        }

        [Authorize(Roles = "ADMINISTRATOR, SUPERVISOR, KASIR, TEKNISI")]
        public ActionResult Index(EnumReports rpt)
        {
            string title = string.Empty;
            ReportsViewModel rptVM = new ReportsViewModel();
            switch (rpt)
            {
                case EnumReports.RptMasterCustomer:
                    rptVM.Title = "Daftar Konsumen";
                    rptVM.ShowDateFrom = false;
                    rptVM.ShowDateTo = false;
                    break;
                case EnumReports.RptMasterEmp:
                    rptVM.Title = "Daftar Karyawan";
                    rptVM.ShowDateFrom = false;
                    rptVM.ShowDateTo = false;
                    break;
                case EnumReports.RptMasterEquip:
                    rptVM.Title = "Daftar Peralatan";
                    rptVM.ShowDateFrom = false;
                    rptVM.ShowDateTo = false;
                    break;
                case EnumReports.RptPrintInvoice:
                    rptVM.Title = "Cetak Invoice";
                    rptVM.ShowDateFrom = false;
                    rptVM.ShowDateTo = false;
                    break;
            }
            return View(rptVM);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Index(EnumReports rpt, ReportsViewModel rptVM)
        {
            ReportDataSource[] repCol = new ReportDataSource[1];
            ReportParameterCollection paramCol = null;
            switch (rpt)
            {
                case EnumReports.RptMasterCustomer:
                    repCol[0] = GetCustomers();
                    break;
                case EnumReports.RptMasterEmp:
                    repCol[0] = GetEmps();
                    break;
                case EnumReports.RptMasterEquip:
                    repCol[0] = GetEquips();
                    break;
                case EnumReports.RptPrintInvoice:
                    repCol = new ReportDataSource[2];
                    repCol[0] = GetInvoice();
                    repCol[1] = GetBap();
                    break;
            }

            //send report data source and report params to session data
            Session["ReportData"] = repCol;
            Session["ReportParams"] = paramCol;

            var e = new
            {
                Success = true,
                Message = "redirect",
                UrlReport = string.Format("{0}", rpt.ToString())
            };
            return Json(e, JsonRequestBehavior.AllowGet);
        }

        private ReportDataSource GetBap()
        {
            var baps = this._bapTasks.GetListNotDeleted();
            ReportDataSource reportDataSource = new ReportDataSource("TBapViewModel", baps);
            return reportDataSource;
        }

        private ReportDataSource GetInvoice()
        {
            var invoices = this._InvoiceTasks.GetListNotDeleted();
            ReportDataSource reportDataSource = new ReportDataSource("TInvoiceViewModel", invoices);
            return reportDataSource;
        }

        private ReportDataSource GetEquips()
        {
            var equips = this._equipTasks.GetListNotDeleted();
            ReportDataSource reportDataSource = new ReportDataSource("MEquip", equips);
            return reportDataSource;
        }

        private ReportDataSource GetEmps()
        {
            var emps = this._empTasks.GetAllEmps();
            ReportDataSource reportDataSource = new ReportDataSource("MEmp", emps);
            return reportDataSource;
        }

        private ReportDataSource GetCustomers()
        {
            var customers = this._customerTasks.GetAllCustomers();
            ReportDataSource reportDataSource = new ReportDataSource("MCustomer", customers);
            return reportDataSource;
        }

        public ActionResult PrintInvoice(string random, string invoiceId)
        {
            string msg = string.Empty;
            bool success = false;
            bool allowPrint = true;
            EnumReports rptToPrint = EnumReports.RptPrintInvoice;
            try
            {
                ////get wo by wo id
                //TWO wo = this._woTasks.One(woId);
                ////check if user have print WO, if not, allow print for role CS
                //if (User.IsInRole("CS"))
                //{
                //    allowPrint = !_woLogTasks.HaveBeenPrint(wo, User.Identity.Name);
                //}

                //if (allowPrint)
                //{
                ReportParameterCollection paramCol = null;
                ReportDataSource[] repCol = new ReportDataSource[2];
                //get data source
                var invoice = this._InvoiceTasks.One(invoiceId);
                var baps = GetTBaps(invoice.ProjectId.Id, invoice.InvoicePeriod);
                var invoices = ConvertToTInvoices(invoice);
                ReportDataSource reportDataSource = new ReportDataSource("TInvoiceViewModel", invoices);
                repCol[0] = reportDataSource;
                reportDataSource = new ReportDataSource("TBapViewModel", baps);
                repCol[1] = reportDataSource;

                ////save log
                //SaveLog(wo, EnumWOLog.Print);

                Session["ReportData"] = repCol;
                Session["ReportParams"] = paramCol;

                success = true;
                msg = "Print Invoice success";
                //}
                //else
                //{
                //    success = false;
                //    msg = "Anda sudah pernah mencetak WO";
                //}
            }
            catch (Exception ex)
            {
                success = false;
                msg = ex.GetBaseException().Message;
            }
            var e = new
            {
                Success = success,
                Message = msg,
                UrlReport = string.Format("{0}&rs%3aFormat=HTML4.0", rptToPrint.ToString())
            };
            return Json(e, JsonRequestBehavior.AllowGet);
        }

        private IEnumerable<TBapViewModel> GetTBaps(string ParentProjectId, DateTime? period)
        {
            var entitys = this._bapTasks.GetListByProjectAndPeriod(ParentProjectId, period);

            return from entity in entitys
                   select new TBapViewModel
                   {
                       //ProjectId = entity.ProjectId != null ? entity.ProjectId.Id : string.Empty,
                       //ProjectName = entity.ProjectId != null ? entity.ProjectId.ProjectName : string.Empty,
                       WorkId = entity.WorkId != null ? entity.WorkId.Id : string.Empty,
                       WorkName = entity.WorkId != null ? entity.WorkId.JobId.JobName : string.Empty,
                       WorkPrice = entity.WorkId != null ? entity.WorkId.WorkPrice : 0,
                       BapPeriod = entity.BapPeriod,
                       BapQty = entity.BapQty,
                       BapTotal = entity.BapTotal,
                       BapStatus = entity.BapStatus,
                       BapDesc = entity.BapDesc,
                       BapId = entity.Id
                   };

        }

        private IEnumerable<TInvoiceViewModel> ConvertToTInvoices(TInvoice invoice)
        {
            TInvoiceViewModel inv = new TInvoiceViewModel
                   {
                       //ProjectId = entity.ProjectId != null ? entity.ProjectId.Id : string.Empty,
                       ProjectName = invoice.ProjectId != null ? invoice.ProjectId.ProjectName : string.Empty,
                       ProjectSpkNo = invoice.ProjectId != null ? invoice.ProjectId.ProjectSpkNo : string.Empty,
                       CustomerName = invoice.ProjectId != null ? invoice.ProjectId.CustomerId.CustomerName : string.Empty,
                       CustomerAddress = invoice.ProjectId != null ? invoice.ProjectId.CustomerId.CustomerAddress : string.Empty,
                       InvoicePeriod = invoice.InvoicePeriod,
                       InvoiceStartDate = invoice.InvoiceStartDate,
                       InvoiceEndDate = invoice.InvoiceEndDate,
                       InvoiceNo = invoice.InvoiceNo,
                       InvoiceDate = invoice.InvoiceDate,
                       InvoiceLastStatus = invoice.InvoiceLastStatus,
                       InvoicePaidDate = invoice.InvoicePaidDate,
                       InvoiceValue = invoice.InvoiceValue,
                       InvoiceRetention = invoice.InvoiceRetention,
                       InvoicePpn = invoice.InvoicePpn,
                       InvoiceTotal = invoice.InvoiceTotal,
                       InvoiceId = invoice.Id
                   };
            IList<TInvoiceViewModel> list = new List<TInvoiceViewModel>();
            list.Add(inv);
            return list;
        }
    }
}