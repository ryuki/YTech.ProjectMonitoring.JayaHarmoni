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
    public class TAbsentDetViewModel
    {
        #region Properties
    
        [ScaffoldColumn(false)]
        public string AbsentDetId
        {
            get;
            set;
        }
        [DisplayName("Tanggal")]
        [ReadOnly(true)]
        public virtual System.DateTime? AbsentDetDate { get; set; }

        [DisplayName("Operator")]
        [UIHint("Employee")]
        public string AbsentDetOperator { get; set; }

        [HiddenInput]
        public string AbsentDetOperatorName { get; set; }
       
        [DisplayName("Sinso")]
        [UIHint("Employee")]
        public string AbsentDetSinso { get; set; }

        [HiddenInput]
        [DisplayName(" ")]
        public string AbsentDetSinsoName { get; set; }

        //[DisplayName("AbsentId")]
        //[UIHint("TAbsent")]
        public string AbsentId { get; set; }

        [DisplayName("Pekerjaan")]
        [UIHint("Work")]
        public string WorkId { get; set; }

        [HiddenInput]
        [ReadOnly(true)]
        [DisplayName(" ")]
        public string WorkName { get; set; }
        
        
        [DisplayName("Jam Kerja Mulai")]
        public virtual decimal? AbsentDetStart { get; set; }

        [DisplayName("Jam Kerja Selesai")]
        public virtual decimal? AbsentDetEnd { get; set; }

        [DisplayName("Jam Kerja Jumlah")]
        public virtual decimal? AbsentDetQty { get; set; }
        
        [DisplayName("Blok")]
        public virtual string AbsentDetBlock { get; set; }
        
        [DisplayName("Hasil")]
        public virtual decimal? AbsentDetResult { get; set; }
        
        [DisplayName("BBM")]
        public virtual decimal? AbsentDetBbm { get; set; }
        
        [DisplayName("Keterangan")]
        public virtual string AbsentDetDesc { get; set; }
        

        #endregion
    }
}
