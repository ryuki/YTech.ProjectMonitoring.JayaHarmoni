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
    public class TAbsentViewModel
    {
        #region Properties
    
        [ScaffoldColumn(false)]
        public string AbsentId
        {
            get;
            set;
        }
        
        [DisplayName("Alat")]
        [UIHint("Equip")]
        public string EquipId { get; set; }

        [DisplayName(" ")]
        [HiddenInput()]
        public string EquipName { get; set; }


        [DisplayName("Proyek")]
        [UIHint("Project")]
        public string ProjectId { get; set; }

        [DisplayName(" ")]
        [HiddenInput()]
        public string ProjectName { get; set; }

        [DisplayName("Periode")]
        [UIHint("Period")]
        [Required]
        public virtual System.DateTime? AbsentPeriod { get; set; }
        
        [DisplayName("Lokasi")]
        public virtual string AbsentLocation { get; set; }
        
        [DisplayName("Supervisor")]
        public virtual string AbsentSupervisor { get; set; }
        
        [DisplayName("Admin")]
        public virtual string AbsentAdmin { get; set; }
        
        [DisplayName("Total Kuantitas")]
        public virtual decimal? AbsentTotalQty { get; set; }
        
        [DisplayName("Total Hasil")]
        public virtual decimal? AbsentTotalResult { get; set; }
        
        [DisplayName("Total BBM")]
        public virtual decimal? AbsentTotalBbm { get; set; }

        #region  property for reports only
        [HiddenInput]
        [ReadOnly(true)]
        [DisplayName(" ")]
        public string EquipBrand { get; set; }
        #endregion
        #endregion
    }
}
