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
    public class MJobViewModel
    {
        #region Properties
    
        [ScaffoldColumn(false)]
        public string JobId
        {
            get;
            set;
        }
        
        [Required]
        [DisplayName("JobName")]
        public virtual string JobName { get; set; }
        
        [Required]
        [DisplayName("JobUnit")]
        public virtual string JobUnit { get; set; }
        
        [Required]
        [DisplayName("JobPrice")]
        public virtual decimal? JobPrice { get; set; }
        
        [Required]
        [DisplayName("JobStatus")]
        public virtual string JobStatus { get; set; }
        
        [Required]
        [DisplayName("JobDesc")]
        public virtual string JobDesc { get; set; }
        

        #endregion
    }
}
