﻿using System;
using System.Globalization;
using System.Windows.Data;
using RestaurantHelper.DAL;
using RestaurantHelper.Models;
using RestaurantHelper.Models.Actions;

namespace RestaurantHelper.Services.Converters
{
	class ActionToDishNameConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			Dish item;
			UnitOfWork unitOfWork = UnitOfWork.GetInstance();

			var discount = value as DiscountAction;
			if(discount != null)
			{
				item = unitOfWork.Dishes.GetById(discount.DishId);
				return item.Name;
			}

			var bonus = value as BonusAction;
			if (bonus != null)
			{
				item = unitOfWork.Dishes.GetById(bonus.DishId);
				return item.Name;
			}

			return string.Empty;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotSupportedException();
		}
	}
}
