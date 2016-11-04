using JayaHarmoni.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace JayaHarmoni.Web.Mvc.Controllers.ViewModels
{
    public class ReportsViewModel
    {
        public ReportsViewModel()
        {
            RptDateFrom = DateTime.Today.AddDays(-7);
            RptDateTo = DateTime.Today;

            ShowDateFrom = false;
            ShowDateTo = false;
            ShowPeriod = false;
            ShowProject = false;   
        }

        public string Title
        {
            get;
            set;
        }

        [DisplayName("Dari Tanggal")]
        [UIHint("Date")]
        public DateTime? RptDateFrom
        {
            get;
            set;
        }

        public bool ShowDateFrom
        {
            get;
            set;
        }

        [DisplayName("Sampai Tanggal")]
        [UIHint("Date")]
        public DateTime? RptDateTo
        {
            get;
            set;
        }

        public bool ShowDateTo
        {
            get;
            set;
        }

        [DisplayName("Periode")]
        [UIHint("Period")]
        public DateTime? RptPeriod
        {
            get;
            set;
        }

        public bool ShowPeriod
        {
            get;
            set;
        }

        [DisplayName("Proyek")]
        [UIHint("Project")]
        public string ProjectId
        {
            get;
            set;
        }

        public bool ShowProject
        {
            get;
            set;
        }
    }
}