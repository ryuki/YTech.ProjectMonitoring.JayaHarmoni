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
    public class TBapViewModel
    {
        #region Properties

        [ScaffoldColumn(false)]
        public string BapId
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

        [DisplayName("Pekerjaan")]
        [UIHint("Work")]
        public string WorkId { get; set; }

        [HiddenInput]
        [ReadOnly(true)]
        [DisplayName(" ")]
        public string WorkName { get; set; }

        [DisplayName("Periode")]
        [UIHint("Period")]
        [Required]
        public virtual System.DateTime? BapPeriod { get; set; }

        [DisplayName("Kuantitas")]
        public virtual decimal? BapQty { get; set; }

        [DisplayName("Total")]
        public virtual decimal? BapTotal { get; set; }

        [DisplayName("Status")]
        public virtual string BapStatus { get; set; }

        [DisplayName("Keterangan")]
        public virtual string BapDesc { get; set; }


        #endregion

        #region property for report only

        [HiddenInput]
        [ReadOnly(true)]
        [DisplayName(" ")]
        public decimal? WorkPrice { get; set; }
        #endregion
    }
}
