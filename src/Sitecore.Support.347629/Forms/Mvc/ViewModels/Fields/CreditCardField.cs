﻿using Sitecore.Forms.Mvc.Attributes;
using Sitecore.Forms.Mvc.ViewModels.Fields;
using Sitecore.StringExtensions;
using Sitecore.WFFM.Abstractions.Actions;
using System.ComponentModel.DataAnnotations;
using Sitecore.Support.Forms.Mvc.Validators;

namespace Sitecore.Support.Forms.Mvc.ViewModels.Fields
{
    public class CreditCardField : SingleLineTextField
    {
        [ParameterName("CardNumberHelp")]
        public override string Information
        {
            get;
            set;
        }

        [DynamicCreditCardAttribute, DataType(DataType.CreditCard)]
        public override string Value
        {
            get;
            set;
        }

        public CreditCardField()
        {
            base.Tracking = false;
        }

        public override ControlResult GetResult()
        {
            return new ControlResult(base.FieldItemId, this.Title, this.Value, "secure:<schidden>{0}</schidden>".FormatWith(new object[]
            {
                this.Value
            }), true);
        }

        public override void SetValueFromQuery(string valueFromQuery)
        {
        }
    }
}