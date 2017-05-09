﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Catel.Data;

namespace RestaurantHelper.Models
{
	[Serializable]
	public class Reservation : ModelBase
	{
		public Reservation()
		{
		}

		public Reservation(int userId, int tableNumber, DateTime firsTime, DateTime lastTime, DateTime day)
		{
			UserId = userId;
			TableNumber = tableNumber;
			FirstTime = firsTime;
			LastTime = lastTime;
			Day = day;
		}
		public int Id
		{
			get { return GetValue<int>(IdProperty); }
			set { SetValue(IdProperty, value); }
		}
		public static readonly PropertyData IdProperty = RegisterProperty("Id", typeof(int));

		public int UserId
		{
			get { return GetValue<int>(ClientIdProperty); }
			set { SetValue(ClientIdProperty, value); }
		}
		public static readonly PropertyData ClientIdProperty = RegisterProperty("UserId", typeof(int));

		public int TableNumber
		{
			get { return GetValue<int>(TableNumberProperty); }
			set { SetValue(TableNumberProperty, value); }
		}
		public static readonly PropertyData TableNumberProperty = RegisterProperty("TableNumber", typeof(int));

		public DateTime FirstTime
		{
			get { return GetValue<DateTime>(FirstTimeProperty); }
			set { SetValue(FirstTimeProperty, value); }
		}
		public static readonly PropertyData FirstTimeProperty = RegisterProperty("FirstTime", typeof(DateTime));

		public DateTime LastTime
		{
			get { return GetValue<DateTime>(LastTimeProperty); }
			set { SetValue(LastTimeProperty, value); }
		}
		public static readonly PropertyData LastTimeProperty = RegisterProperty("LastTime", typeof(DateTime));

		public DateTime Day
		{
			get { return GetValue<DateTime>(DayProperty); }
			set { SetValue(DayProperty, value); }
		}
		public static readonly PropertyData DayProperty = RegisterProperty("Day", typeof(DateTime));
	}
}