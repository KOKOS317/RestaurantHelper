﻿namespace RestaurantHelper.Models.Additional
{
	public class OrderWithReservationInfo
	{
		public int OrderId { get; set; }
		public int UserId { get; set; }
		public int TableId { get; set; }
		public string Day { get; set; }
		public string FirstTime { get; set; }
		public string LastTime { get; set; }
	}
}
