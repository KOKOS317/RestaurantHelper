﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Catel.Data;
using Catel.Runtime.Serialization;
using RestaurantHelper.Models.Additional;

namespace RestaurantHelper.Models
{
	public class Dish : MyModelBase
	{
		public int Id
		{
			get { return GetValue<int>(IdProperty); }
			set { SetValue(IdProperty, value); }
		}
		public static readonly PropertyData IdProperty = RegisterProperty("Id", typeof(int));

		public string Name
		{
			get { return GetValue<string>(NameProperty); }
			set { SetValue(NameProperty, value); }
		}
		public static readonly PropertyData NameProperty = RegisterProperty("Name", typeof(string));

		public int Price
		{
			get { return GetValue<int>(PriceProperty); }
			set { SetValue(PriceProperty, value); }
		}
		public static readonly PropertyData PriceProperty = RegisterProperty("Price", typeof(int));

		public string Info
		{
			get { return GetValue<string>(InfoProperty); }
			set { SetValue(InfoProperty, value); }
		}
		public static readonly PropertyData InfoProperty = RegisterProperty("Info", typeof(string));

		public string PicturePath
		{
			get { return GetValue<string>(PicturePathProperty); }
			set { SetValue(PicturePathProperty, value); }
		}
		public static readonly PropertyData PicturePathProperty = RegisterProperty("PicturePath", typeof(string));

		[NotMapped]
		public bool IsDiscounted
		{
			get { return GetValue<bool>(IsDiscountedProperty); }
			set { SetValue(IsDiscountedProperty, value); }
		}
		public static readonly PropertyData IsDiscountedProperty = RegisterProperty("IsDiscounted", typeof(bool), false);
	}
}
