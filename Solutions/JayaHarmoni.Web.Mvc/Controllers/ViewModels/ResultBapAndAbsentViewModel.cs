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
    public class ResultBapAndAbsentViewModel
    {
        #region Properties

        //[ScaffoldColumn(false)]
        [DisplayName("Kode Proyek")]
        public string ProjectId { get; set; }
        
        [DisplayName("Proyek")]
        public string ProjectName { get; set; }
        
        [DisplayName("Periode")]
        public virtual System.DateTime? BapPeriod { get; set; }

        [DisplayName("Pekerjaan")]
        public string WorkId { get; set; }

        [DisplayName("Pekerjaan")]
        public string JobName { get; set; }
        
        [DisplayName("Total BAP")]
        public virtual decimal? BapQty { get; set; }
        
        [DisplayName("Total TimeSheet")]
        public virtual decimal? SumAbsentDetResult { get; set; }

        [DisplayName("Total Proyek")]
        public virtual decimal? WorkQty { get; set; }     

        #endregion
    }
}
