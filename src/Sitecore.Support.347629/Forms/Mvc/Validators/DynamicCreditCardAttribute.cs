using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;
using Sitecore.Forms.Mvc.Interfaces;
using Sitecore.Forms.Mvc.Validators;
using Sitecore.Forms.Mvc.ViewModels;

namespace Sitecore.Support.Forms.Mvc.Validators
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
    public class DynamicCreditCardAttribute : DynamicValidationBase
    {
        protected override ValidationResult ValidateFieldValue(IViewModel model, object value, ValidationContext validationContext)
        {
            if (!this.IsValid(value))
            {
                return new ValidationResult(this.FormatError(model, new object[0]));
            }
            return ValidationResult.Success;
        }

        public override IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            FieldViewModel model = base.GetModel<FieldViewModel>(metadata);
            yield return new ModelClientValidationRule
            {
                ErrorMessage = this.FormatError(model, new object[0]),
                ValidationType = "creditcard"
            };
        }

        public override bool IsValid(object value)
        {
            if (value == null)
                return true;
            string str = value as string;
            if (str == null)
                return false;
            string source = str.Replace("-", "").Replace(" ", "");
            int num1 = 0;
            bool flag = false;
            foreach (char ch in source.Reverse<char>())
            {
                if (ch < '0' || ch > '9')
                    return false;
                int num2 = ((int)ch - 48) * (flag ? 2 : 1);
                flag = !flag;
                for (; num2 > 0; num2 /= 10)
                    num1 += num2 % 10;
            }
            return num1 % 10 == 0;
        }
    }
}