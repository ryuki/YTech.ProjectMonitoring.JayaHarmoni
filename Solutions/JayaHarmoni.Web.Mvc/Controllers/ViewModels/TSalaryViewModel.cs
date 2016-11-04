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
    public class TSalaryViewModel
    {
        #region Properties
    
        [ScaffoldColumn(false)]
        public string SalaryId
        {
            get;
            set;
        }
        
        [DisplayName("Karyawan")]
        [UIHint("Employee")]
        public string EmployeeId { get; set; }
        
        [HiddenInput]
        [ReadOnly(true)]
        [DisplayName(" ")]
        public string EmployeeName { get; set; }
        [DisplayName("Proyek")]
        [UIHint("Project")]
        public string ProjectId { get; set; }
        
        [HiddenInput]
        [ReadOnly(true)]
        [DisplayName(" ")]
        public string ProjectName { get; set; }

        [DisplayName("Periode")]
        [UIHint("Period")]
        public virtual System.DateTime? SalaryPeriod { get; set; }

        [DisplayName(" ")]
        [HiddenInput]
        public virtual decimal? SalaryGapok { get; set; }

        [DisplayName(" ")]
        [HiddenInput]
        public virtual decimal? SalaryWorkQty { get; set; }

        [DisplayName(" ")]
        [HiddenInput]
        public virtual decimal? SalaryWorkTotal { get; set; }

        [DisplayName(" ")]
        [HiddenInput]
        public virtual decimal? SalaryTotal { get; set; }
        
        //[DisplayName("SalaryStatus")]
        //public virtual string SalaryStatus { get; set; }
        
        //[DisplayName("SalaryDesc")]
        //public virtual string SalaryDesc { get; set; }
        

        #endregion
    }
}
