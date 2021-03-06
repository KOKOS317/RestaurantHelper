﻿using Catel.Data;
using RestaurantHelper.ViewModels.ManagerViewModels.Actions;

namespace RestaurantHelper.ViewModels.ManagerViewModels
{
	using Catel.MVVM;
	using System.Threading.Tasks;

	public class ManagerMainViewModel : ViewModelBase
	{
		public ManagerMainViewModel()
		{
			EmployeesCommand = new Command(OnEmployeesCommandExecute);
			HallCommand = new Command(OnHallCommandExecute);
			MenuCommand = new Command(OnMenuCommandExecute);		
			ReportsCommand = new Command(OnReportsCommandExecute);
			ActionsCommand = new Command(OnActionsCommandExecute);
			ReviewsCommand = new Command(OnReviewsCommandExecute);
			ExitCommand = new Command(OnExitCommandExecute);

			//TODO: запускать первой страницу мониторинга
			CurrentManagerPage = new ManagerHallViewModel();
		}

		public IViewModel CurrentManagerPage
		{
			get { return GetValue<IViewModel>(CurrentManagerPageProperty); }
			set { SetValue(CurrentManagerPageProperty, value); }
		}
		public static readonly PropertyData CurrentManagerPageProperty = RegisterProperty("CurrentManagerPage", typeof(IViewModel));




		public Command EmployeesCommand { get; private set; }
		public Command HallCommand { get; private set; }
		public Command MenuCommand { get; private set; }
		public Command ReportsCommand { get; private set; }
		public Command ActionsCommand { get; private set; }
		public Command ReviewsCommand { get; private set; }
		public Command ExitCommand { get; private set; }


		private void OnEmployeesCommandExecute()
		{
			CurrentManagerPage = new ManagerEmployeesViewModel();
		}

		private void OnHallCommandExecute()
		{
			CurrentManagerPage = new ManagerHallViewModel();
		}

		private void OnMenuCommandExecute()
		{
			CurrentManagerPage = new ManagerMenuViewModel();
		}

		private void OnReportsCommandExecute()
		{
			CurrentManagerPage = new ManagerReportsViewModel();
		}

		private void OnActionsCommandExecute()
		{
			CurrentManagerPage = new ActionsViewModel(); 
		}

		private void OnReviewsCommandExecute()
		{
			CurrentManagerPage = new ManagerReviewsViewModel();
		}

		private async void OnExitCommandExecute()
		{
			var root = ViewModelManager.GetFirstOrDefaultInstance<MainWindowViewModel>();
			await root.CloseViewModelAsync(false);
		}


		protected override async Task InitializeAsync()
		{
			await base.InitializeAsync();
		}

		protected override async Task CloseAsync()
		{
			await base.CloseAsync();
		}
	}
}
