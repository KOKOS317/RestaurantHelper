﻿using RestaurantHelper.ViewModels.ManagerViewModels.AdditionalWindows;

namespace RestaurantHelper.Views.ManagerViews.AdditionalWindows
{
	using Catel.Windows;
	using ViewModels;

	public partial class AddEmployeeView
	{
		public AddEmployeeView()
			: this(null) { }

		public AddEmployeeView(AddEmployeeViewModel viewModel)
			: base(viewModel, DataWindowMode.Custom)
		{
			InitializeComponent();
		}
	}
}
