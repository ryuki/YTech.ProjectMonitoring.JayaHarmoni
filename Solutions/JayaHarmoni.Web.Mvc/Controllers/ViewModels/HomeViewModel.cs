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
    public class HomeViewModel
    {
        [ScaffoldColumn(false)]
        public string CostID
        {
            get;
            set;
        }

        [Required]
        [DisplayName("Num1")]
        public decimal? Num1
        {
            get;
            set;
        }

        [Required]
        [DisplayName("Num2")]
        public decimal? Num2
        {
            get;
            set;
        }

        [Required]
        [DisplayName("Num3")]
        public decimal? Num3
        {
            get;
            set;
        }
    }
}