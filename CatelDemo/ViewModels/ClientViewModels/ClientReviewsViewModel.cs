﻿using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Catel.Collections;
using Catel.Data;
using Catel.IoC;
using Catel.Services;
using RestaurantHelper.DAL;
using RestaurantHelper.Models;
using RestaurantHelper.Models.Reviews;
using RestaurantHelper.Services.Logic;

namespace RestaurantHelper.ViewModels.ClientViewModels
{
	using Catel.MVVM;
	using System;
	using System.Threading.Tasks;

	public class ClientReviewsViewModel : ViewModelBase
	{
		private readonly UnitOfWork _unitOfWork = UnitOfWork.GetInstance();
		private Action _refreshReviewsAction;
		private readonly IViewModel _rootViewModel;
		private readonly IViewModel _parentViewModel;
		private readonly ReviewsWithAnswersBinder _binder;
		private readonly User _user;
		

		public ClientReviewsViewModel(IViewModel parentViewModel, User user)
		{
			_rootViewModel = ViewModelManager.GetFirstOrDefaultInstance<MainWindowViewModel>();
			_parentViewModel = parentViewModel;
			_binder = new ReviewsWithAnswersBinder();
			_user = user;

			BackCommand = new Command(OnBackCommandExecute);
			RefreshCommand = new Command(OnRefreshCommandExecute);
			MyReviewsCommand = new Command(OnMyReviewsCommandExecute);
			AllReviewsCommand = new Command(OnAllReviewsCommandExecute);
			AddReviewCommand = new Command(OnAddReviewCommandExecute);
			EditReviewCommand = new Command(OnEditReviewCommandExecute, OnAnyReviewCommandCanExecute);
			DeleteReviewCommand = new Command(OnDeleteReviewCommandExecute, OnAnyReviewCommandCanExecute);
			SelectionChangedCommand = new Command(OnSelectionChangedCommandExecute);

			// показываем все отзывы
			OnAllReviewsCommandExecute();
		}

		public FastObservableCollection<ClientReview> ClientReviews
		{
			get { return GetValue<FastObservableCollection<ClientReview>>(ClientReviewsProperty); }
			set { SetValue(ClientReviewsProperty, value); }
		}
		public static readonly PropertyData ClientReviewsProperty = RegisterProperty("ClientReviews", typeof(FastObservableCollection<ClientReview>),
			new FastObservableCollection<ClientReview>());


		public ClientReview SelectedClientReview
		{
			get { return GetValue<ClientReview>(SelectedClientReviewProperty); }
			set { SetValue(SelectedClientReviewProperty, value); }
		}
		public static readonly PropertyData SelectedClientReviewProperty = RegisterProperty("SelectedClientReview", typeof(ClientReview));

		public string AdminAnswer
		{
			get { return GetValue<string>(AdminAnswerProperty); }
			set { SetValue(AdminAnswerProperty, value); }
		}
		public static readonly PropertyData AdminAnswerProperty = RegisterProperty("AdminAnswer", typeof(string), "Ничего не выбрано");

		public bool IsMyReviews
		{
			get { return GetValue<bool>(IsMyReviewsProperty); }
			set { SetValue(IsMyReviewsProperty, value); }
		}
		public static readonly PropertyData IsMyReviewsProperty = RegisterProperty("IsMyReviews", typeof(bool), false);

		public string ToolTipText
		{
			get { return GetValue<string>(ToolTipTextProperty); }
			set { SetValue(ToolTipTextProperty, value); }
		}
		public static readonly PropertyData ToolTipTextProperty = RegisterProperty("ToolTipText", typeof(string), string.Empty);


		protected override async Task InitializeAsync()
		{
			await base.InitializeAsync();
		}

		protected override async Task CloseAsync()
		{
			await base.CloseAsync();
		}

		public Command SelectionChangedCommand { get; private set; }
		private void OnSelectionChangedCommandExecute()
		{
			RefreshAdminAnswer();
		}

		public Command MyReviewsCommand { get; private set; }
		private void OnMyReviewsCommandExecute()
		{
			ClientReviews.Clear();
			ClientReviews.AddItems(_unitOfWork.ClientReviews.GetAll()
				.Where(cr => cr.UserId == _user.Id));
			_refreshReviewsAction = OnMyReviewsCommandExecute;
			IsMyReviews = true;
		}

		public Command AllReviewsCommand { get; private set; }
		private void OnAllReviewsCommandExecute()
		{
			ClientReviews.Clear();
			ClientReviews.AddItems(_unitOfWork.ClientReviews.GetAll());
			_refreshReviewsAction = OnAllReviewsCommandExecute;
			IsMyReviews = false;
		}

		public Command AddReviewCommand { get; private set; }
		private void OnAddReviewCommandExecute()
		{
			var visualizer = DependencyResolver.Resolve<IUIVisualizerService>();
			visualizer.ShowDialog(new AddReviewViewModel(user: _user));

			_refreshReviewsAction();
		}

		public Command EditReviewCommand { get; private set; }
		private void OnEditReviewCommandExecute()
		{
			var visualizer = DependencyResolver.Resolve<IUIVisualizerService>();
			visualizer.ShowDialog(new AddReviewViewModel(SelectedClientReview));
			int id = SelectedClientReview.Id;
			_refreshReviewsAction();
			SelectedClientReview = ClientReviews.FirstOrDefault(cr => cr.Id == id);
		}

		public Command DeleteReviewCommand { get; private set; }
		private void OnDeleteReviewCommandExecute()
		{
			_unitOfWork.ClientReviews.Delete(SelectedClientReview.Id);
			_unitOfWork.SaveChanges();

			_refreshReviewsAction();
			_rootViewModel.ChangePageWithDialog(new ShortMessageViewModel("Отзыв удален!"), 777);
		}

		private bool OnAnyReviewCommandCanExecute()
		{
			string message;
			bool res = _binder.CanPerformCommands(SelectedClientReview, IsMyReviews, out message);
			ToolTipText = message;
			return res;
		}

		public Command BackCommand { get; private set; }
		private void OnBackCommandExecute()
		{
			IViewModel root = ViewModelManager.GetFirstOrDefaultInstance<MainWindowViewModel>();
			root.ChangePage(_parentViewModel);
		}

		public Command RefreshCommand { get; private set; }
		private void OnRefreshCommandExecute()
		{
			_rootViewModel.ChangePageWithDialog(new ShortMessageViewModel("Обновление"), 999);
			_refreshReviewsAction();
		}

		private void RefreshAdminAnswer()
		{
			AdminAnswer = _binder.GetClientMessage(SelectedClientReview);
		}
	}
}
