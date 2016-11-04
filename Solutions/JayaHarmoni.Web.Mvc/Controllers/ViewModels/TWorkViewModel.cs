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
    public class TWorkViewModel
    {
        #region Properties
    
        [ScaffoldColumn(false)]
        public string WorkId
        {
            get;
            set;
        }

        [DisplayName(" ")]
        [HiddenInput()]
        public string ProjectId { get; set; }

        [DisplayName("Pekerjaan")]
        [UIHint("Job")]
        [Required]
        public string JobId { get; set; }

        [DisplayName(" ")]
        [HiddenInput()]
        public string JobName { get; set; }

        [DisplayName("Status Rentensi")]
        [UIHint("WorkRetentionStatus")]
        public virtual string WorkRetentionStatus { get; set; }
        
        [DisplayName("Kuantitas")]
        public virtual decimal? WorkQty { get; set; }
        
        [DisplayName("Harga (Rp)")]
        public virtual decimal? WorkPrice { get; set; }

        [DisplayName(" ")]
        [HiddenInput]
        public virtual decimal? WorkTotal { get; set; }
        
        [DisplayName("Realisasi Kuantitas")]
        public virtual decimal? WorkRealQty { get; set; }
        
        [DisplayName("Telah Dibayar")]
        public virtual decimal? WorkRealPaid { get; set; }
        //[DisplayName("WorkStatus")]
        //public virtual string WorkStatus { get; set; }
        
        //[DisplayName("WorkDesc")]
        //public virtual string WorkDesc { get; set; }
        

        #endregion

    }
}
