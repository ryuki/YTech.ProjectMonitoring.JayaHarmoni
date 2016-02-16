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
        [DisplayName("ProjectName")]
        public virtual string ProjectName { get; set; }
        
        [Required]
        [DisplayName("ProjectDate")]
        public virtual System.DateTime? ProjectDate { get; set; }
        
        [Required]
        [DisplayName("ProjectPrice")]
        public virtual decimal? ProjectPrice { get; set; }
        
        [Required]
        [DisplayName("ProjectRetention")]
        public virtual decimal? ProjectRetention { get; set; }
        
        [Required]
        [DisplayName("ProjectLocation")]
        public virtual string ProjectLocation { get; set; }
        
        [Required]
        [DisplayName("ProjectStartDate")]
        public virtual System.DateTime? ProjectStartDate { get; set; }
        
        [Required]
        [DisplayName("ProjectEndDate")]
        public virtual System.DateTime? ProjectEndDate { get; set; }
        
        [Required]
        [DisplayName("ProjectFinishDate")]
        public virtual System.DateTime? ProjectFinishDate { get; set; }
        
        [Required]
        [DisplayName("ProjectStatus")]
        public virtual string ProjectStatus { get; set; }
        
        [Required]
        [DisplayName("ProjectDesc")]
        public virtual string ProjectDesc { get; set; }
        

        #endregion
    }
}
