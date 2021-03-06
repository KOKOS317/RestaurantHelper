﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using RestaurantHelper.DAL;
using RestaurantHelper.Models.Actions;

namespace RestaurantHelper.Services.Converters
{
	class ActionToTypeNameConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value is DiscountAction)
			{
				return "СКИДКА";
			}
			if (value is BonusAction)
			{
				return "БОНУС ";
			}
			return string.Empty;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotSupportedException();
		}
	}
}
