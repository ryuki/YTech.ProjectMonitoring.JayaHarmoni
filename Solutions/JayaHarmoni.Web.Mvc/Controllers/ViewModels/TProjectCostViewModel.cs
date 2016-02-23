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
    public class TProjectCostViewModel
    {
        #region Properties
    
        [ScaffoldColumn(false)]
        public string ProjectCostId
        {
            get;
            set;
        }

        [DisplayName("Proyek")]
        [UIHint("Project")]
        public string ProjectId { get; set; }
        
        [HiddenInput]
        [ReadOnly(true)]
        [DisplayName(" ")]
        public string ProjectName { get; set; }

        [DisplayName("Alat")]
        [UIHint("Equip")]
        public string EquipId { get; set; }
        
        [HiddenInput]
        [ReadOnly(true)]
        [DisplayName(" ")]
        public string EquipName { get; set; }
        
        [DisplayName("Biaya")]
        [UIHint("Cost")]
        public string CostId { get; set; }
        
        [HiddenInput]
        [ReadOnly(true)]
        [DisplayName(" ")]
        public string CostName { get; set; }
        
        [DisplayName("Tanggal")]
        public virtual System.DateTime? ProjectCostDate { get; set; }
        
        [DisplayName("Kuantitas")]
        public virtual decimal? ProjectCostQty { get; set; }
        
        [DisplayName("Total")]
        public virtual decimal? ProjectCostTotal { get; set; }
        
        [DisplayName("Keterangan")]
        public virtual string ProjectCostDesc { get; set; }
        

        #endregion
    }
}
