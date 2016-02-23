using JayaHarmoni.Domain;
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
    public class TProjectViewModel
    {
        #region Properties
        
        [ScaffoldColumn(false)]
        public string ProjectId
        {
            get;
            set;
        }

        [Required]
        [DisplayName("Nama Proyek")]
        public string ProjectName { get; set; }

        [DisplayName("Nama Konsumen")]
        [UIHint("Customer")]
        public string CustomerId { get; set; }

        [DisplayName(" ")]
        [ReadOnly(true)]
        [HiddenInput()]
        public string CustomerName { get; set; }

        [DisplayName("No SPK")]
        public virtual string ProjectSpkNo { get; set; }

        [DisplayName("Tanggal Proyek")]
        public System.DateTime? ProjectDate { get; set; }

        [DisplayName("Nilai Proyek")]
        public decimal? ProjectPrice { get; set; }

        [DisplayName("Retensi")]
        public decimal? ProjectRetention { get; set; }

        [DisplayName("Lokasi")]
        public string ProjectLocation { get; set; }

        [DisplayName("Tanggal Mulai Proyek")]
        public System.DateTime? ProjectStartDate { get; set; }

        [DisplayName("Target Selesai Proyek")]
        public System.DateTime? ProjectEndDate { get; set; }

        [DisplayName("Tanggal Selesai")]
        public System.DateTime? ProjectFinishDate { get; set; }

        [DisplayName("Status")]
        public string ProjectStatus { get; set; }

        [DisplayName("Keterangan")]
        public string ProjectDesc { get; set; }


        #endregion
    }
}
