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
    public class EmployeeViewModel
    {
        [ScaffoldColumn(false)]
        public string EmployeeID
        {
            get;
            set;
        }

        [Required]
        [DisplayName("Nama Karyawan")]
        public string EmployeeName
        {
            get;
            set;
        }

        //[Required]
        [DisplayName("Telp / HP")]
        public string EmployeePhone
        {
            get;
            set;
        }

        //[Required]
        [DisplayName("Alamat")]
        [UIHint("TextArea")]
        public string EmployeeAddress
        {
            get;
            set;
        }
        [DisplayName("Tgl Mulai Bekerja")]
        [UIHint("Date")]
        public DateTime? EmployeeJoinDate
        {
            get;
            set;
        }

        [DisplayName("Gaji Pokok")]
        public virtual decimal? EmployeeBasicSalary { get; set; }

        [DisplayName("Upah Harian")]
        public virtual decimal? EmployeeDailyAllowance { get; set; }
    }
}