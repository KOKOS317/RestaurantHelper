﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace RestaurantHelper.Services.ValidationRules
{
	public class NotEmptyValidationRule : ValidationRule
	{
		public override ValidationResult Validate(object value, CultureInfo cultureInfo)
		{
			int len = (value ?? "").ToString().Length;
			return  len > 0 && len <= 3 
				? new ValidationResult(false, "Слишком короткое")
				: ValidationResult.ValidResult;
		}
	}
}
