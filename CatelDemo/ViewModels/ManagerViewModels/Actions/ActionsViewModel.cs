﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Catel.Collections;
using Catel.Data;
using Catel.MVVM;
using RestaurantHelper.Models.Actions;

namespace RestaurantHelper.ViewModels.ManagerViewModels.Actions
{
	public class ActionsViewModel : ViewModelBase
	{
		public ActionsViewModel()
		{
			SelectionChangedCommand = new Command(OnSelectionChangedCommandExecute);
			AddActionCommand = new Command(OnAddActionCommandExecute);
			ShowActionsCommand = new Command(OnShowActionsCommandExecute);
			SetShowActionProperties();
		}

		public FastObservableCollection <ActionType> ActionTypes
		{
			get { return GetValue<FastObservableCollection<ActionType>>(ActionTypesProperty); }
			set { SetValue(ActionTypesProperty, value); }
		}
		public static readonly PropertyData ActionTypesProperty = RegisterProperty("ActionTypes", typeof(FastObservableCollection<ActionType>),
			new FastObservableCollection<ActionType> { ActionType.Discount, ActionType.Bonus } );

		public  ActionType SelectedActionType
		{
			get { return GetValue<ActionType>(SelectedActionTypeProperty); }
			set { SetValue(SelectedActionTypeProperty, value); }
		}
		public static readonly PropertyData SelectedActionTypeProperty = RegisterProperty("SelectedActionType", typeof(ActionType),
			 ActionType.Discount);

		public IViewModel ActionsPage
		{
			get { return GetValue<IViewModel>(ActionsPageProperty); }
			set { SetValue(ActionsPageProperty, value); }
		}
		public static readonly PropertyData ActionsPageProperty = RegisterProperty("ActionsPage", typeof(IViewModel),
			new DiscountViewModel());


		public Command SelectionChangedCommand { get; private set; }
		private void OnSelectionChangedCommandExecute()
		{
			switch (SelectedActionType)
			{
				case ActionType.Bonus:
					SetAddBonusProperties();
					break;
				case ActionType.Discount:
					SetAddDiscountProperties();
					break;
			}
		}


		public Command AddActionCommand { get; private set; }
		private void OnAddActionCommandExecute()
		{
			SetAddDiscountProperties();
		}


		public Command ShowActionsCommand { get; private set; }
		private void OnShowActionsCommandExecute()
		{
			SetShowActionProperties();
		}

		protected override async Task InitializeAsync()
		{
			await base.InitializeAsync();
		}

		protected override async Task CloseAsync()
		{
			await base.CloseAsync();
		}

		private void SetShowActionProperties()
		{			
			ActionsPage = new DeleteActionsViewModel();
		}

		private void SetAddDiscountProperties()
		{
			ActionsPage = new DiscountViewModel();
			SelectedActionType = ActionType.Discount;
		}

		private void SetAddBonusProperties()
		{
			ActionsPage = new BonusViewModel();
			SelectedActionType = ActionType.Bonus;
		}
	}
}
