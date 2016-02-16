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
    public class CustomerViewModel
    {
        [ScaffoldColumn(false)]
        public string CustomerID
        {
            get;
            set;
        }

        [Required]
        [DisplayName("Nama Konsumen")]
        public string CustomerName
        {
            get;
            set;
        }

        //[Required]
        [DisplayName("Telp / HP")]
        public string CustomerPhone
        {
            get;
            set;
        }

        //[Required]
        [DisplayName("Alamat")]
        [UIHint("TextArea")]
        public string CustomerAddress
        {
            get;
            set;
        }
        [DisplayName("Nama Kontak")]
        public string CustomerContactName
        {
            get;
            set;
        }
        [DisplayName("Nomor Kontak")]
        public string CustomerContactPhone
        {
            get;
            set;
        }
    }

    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class CustomProductNameValidationAttribute : ValidationAttribute, IClientValidatable
    {
        public override bool IsValid(object value)
        {
            var productName = (string)value;
            if (!string.IsNullOrEmpty(productName))
            {
                return Regex.IsMatch(productName, "^[A-Z]");
            }
            return true;
        }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            yield return new ModelClientValidationRule
            {
                ErrorMessage = ErrorMessage,
                ValidationType = "productnamevalidation"
            };
        }
    }
}