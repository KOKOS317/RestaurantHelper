﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Catel.Data;
using RestaurantHelper.Models.Additional;

namespace RestaurantHelper.Models
{
	public class OrderedDish : MyModelBase
	{
		public int Id
		{
			get { return GetValue<int>(IdProperty); }
			set { SetValue(IdProperty, value); }
		}
		public static readonly PropertyData IdProperty = RegisterProperty("Id", typeof(int));

		[Required]
		public int OrderId
		{
			get { return GetValue<int>(OrderIdProperty); }
			set { SetValue(OrderIdProperty, value); }
		}
		public static readonly PropertyData OrderIdProperty = RegisterProperty("OrderId", typeof(int));

		[ForeignKey("OrderId")]
		public virtual Order Order { get; set; }

		[Required]
		public int DishId
		{
			get { return GetValue<int>(DishIdProperty); }
			set { SetValue(DishIdProperty, value); }
		}
		public static readonly PropertyData DishIdProperty = RegisterProperty("DishId", typeof(int));

		[ForeignKey("DishId")]
		public virtual Dish Dish { get; set; }

		/// <summary>
		/// количество одинаковых заказанных блюд
		/// </summary>
		[Required]
		public int Quantity
		{
			get { return GetValue<int>(QuantityProperty); }
			set { SetValue(QuantityProperty, value); }
		}
		public static readonly PropertyData QuantityProperty = RegisterProperty("Quantity", typeof(int), 0);

		/// <summary>
			/// Реальная стоимость блюда, с учетом скидок
			/// </summary>
		[Required]
		public int OrderedPrice
		{
			get { return GetValue<int>(OrderedPriceProperty); }
			set { SetValue(OrderedPriceProperty, value); }
		}
		public static readonly PropertyData OrderedPriceProperty = RegisterProperty("OrderedPrice", typeof(int));
	}
}
