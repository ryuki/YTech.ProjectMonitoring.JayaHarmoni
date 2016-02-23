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
    public class MOwnerViewModel
    {
        #region Properties
    
        [ScaffoldColumn(false)]
        public string OwnerId
        {
            get;
            set;
        }

        [DisplayName(" ")]
        [HiddenInput()]
        public string EquipId { get; set; }
        
        [DisplayName("Nama Pemilik")]
        public virtual string OwnerName { get; set; }
        
        [DisplayName("Persentase")]
        public virtual decimal? OwnerPercent { get; set; }
        
        [DisplayName("Kepemilikan Sejak Tanggal")]
        public virtual System.DateTime? OwnerSinceDate { get; set; }

        [DisplayName("Kepemilikan Sampai Tanggal")]
        public virtual System.DateTime? OwnerUntilDate { get; set; }        

        #endregion
    }
}
