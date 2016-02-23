using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace JayaHarmoni.Web.Mvc.Controllers.ViewModels
{
    public class TInvoiceViewModel
    {
        #region Properties

        [ScaffoldColumn(false)]
        public string InvoiceId
        {
            get;
            set;
        }

        //[DisplayName("Proyek")]
        //[UIHint("Project")]
        //public string ProjectId { get; set; }

        //[HiddenInput]
        //[ReadOnly(true)]
        //[DisplayName(" ")]
        //public string ProjectName { get; set; }

        [DisplayName("Periode Invoice")]
        [UIHint("Period")]
        public virtual System.DateTime? InvoicePeriod { get; set; }

        [DisplayName("Pekerjaan Mulai Tanggal")]
        public virtual System.DateTime? InvoiceStartDate { get; set; }

        [DisplayName("Pekerjaan Sampai Tanggal")]
        public virtual System.DateTime? InvoiceEndDate { get; set; }

        [DisplayName("No Invoice")]
        public virtual string InvoiceNo { get; set; }

        [DisplayName("Tanggal Invoice")]
        public virtual System.DateTime? InvoiceDate { get; set; }

        [UIHint("InvoiceStatus")]
        [DisplayName("Status Terakhir Invoice")]
        public virtual string InvoiceLastStatus { get; set; }

        [DisplayName("Tanggal Invoice Dibayar")]
        public virtual System.DateTime? InvoicePaidDate { get; set; }

        [DisplayName("Nilai Invoice")]
        public virtual decimal? InvoiceValue { get; set; }

        [DisplayName("Retensi")]
        public virtual decimal? InvoiceRetention { get; set; }

        [DisplayName("PPN")]
        public virtual decimal? InvoicePpn { get; set; }

        [DisplayName("Total")]
        public virtual decimal? InvoiceTotal { get; set; }

        #region  property for reports only
        [HiddenInput]
        [ReadOnly(true)]
        [DisplayName(" ")]
        public string ProjectName { get; set; }

        [HiddenInput]
        [ReadOnly(true)]
        [DisplayName(" ")]
        public string ProjectSpkNo { get; set; }

        [HiddenInput]
        [ReadOnly(true)]
        [DisplayName(" ")]
        public string CustomerName { get; set; }

        [HiddenInput]
        [ReadOnly(true)]
        [DisplayName(" ")]
        public string CustomerAddress { get; set; }
        #endregion
        #endregion
    }
}
