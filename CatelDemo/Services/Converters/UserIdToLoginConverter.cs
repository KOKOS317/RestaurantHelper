﻿using System;
using System.Globalization;
using System.Windows.Data;
using RestaurantHelper.DAL;
using RestaurantHelper.Models;

namespace RestaurantHelper.Services.Converters
{
	class UserIdToLoginConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			UnitOfWork uow = UnitOfWork.GetInstance();
			if (value is int)
			{
				var user = uow.Users.GetById((int) value);

				if (user != null)
				{
					return user.Login;
				}
			}

			return string.Empty;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotSupportedException();
		}
	}
}
