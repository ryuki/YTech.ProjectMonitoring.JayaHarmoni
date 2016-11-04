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
    public class EquipViewModel
    {
        [ScaffoldColumn(true)]
        [DisplayName("Kode Alat")]
        public string EquipID
        {
            get;
            set;
        }

        [Required]
        [DisplayName("Nama Alat")]
        public string EquipName
        {
            get;
            set;
        }

        [DisplayName("Tipe Alat")]
        public string EquipType
        {
            get;
            set;
        }

        [DisplayName("Merek Alat")]
        public string EquipBrand
        {
            get;
            set;
        }

        [DisplayName("Tgl Pembelian Alat")]
        [UIHint("Date")]
        public DateTime? EquipBuyDate
        {
            get;
            set;
        }
    }
}