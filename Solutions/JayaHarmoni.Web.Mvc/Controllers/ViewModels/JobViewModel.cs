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
    public class JobViewModel
    {
        [ScaffoldColumn(false)]
        public string JobID
        {
            get;
            set;
        }

        [Required]
        [DisplayName("Nama Pekerjaan")]
        public string JobName
        {
            get;
            set;
        }

        [DisplayName("Satuan")]
        public string JobUnit
        {
            get;
            set;
        }

        [DisplayName("Harga")]
        [UIHint("Number")]
        public decimal? JobPrice
        {
            get;
            set;
        }
    }
}