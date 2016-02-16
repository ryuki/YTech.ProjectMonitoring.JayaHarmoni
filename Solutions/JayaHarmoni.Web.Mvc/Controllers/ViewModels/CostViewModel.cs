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
    public class CostViewModel
    {
        [ScaffoldColumn(false)]
        public string CostID
        {
            get;
            set;
        }

        [Required]
        [DisplayName("Nama Biaya")]
        public string CostName
        {
            get;
            set;
        }
    }
}