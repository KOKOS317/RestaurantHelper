﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Catel.Collections;
using Catel.Data;
using RestaurantHelper.DAL;
using RestaurantHelper.Models;
using RestaurantHelper.Models.Reviews;
using RestaurantHelper.Services.Other;

namespace RestaurantHelper.ViewModels.ManagerViewModels
{
	using Catel.MVVM;
	using System.Threading.Tasks;

	public class ReviewsViewModel : ViewModelBase
	{
		private readonly UnitOfWork _unitOfWork = UnitOfWork.GetInstance();
		private const string NO_ANSWER = "Ответа еще нет.";
		private readonly ReviewsWithAnswersBinder _binder;

		public ReviewsViewModel()
		{
			_binder = new ReviewsWithAnswersBinder();
			RefreshReviewsCollection();

			ClearTextCommand = new Command(OnClearTextCommandExecute);
			SaveAnswerCommand = new Command(OnSaveAnswerCommandExecute, OnSaveAnswerCommandCanExecute);
			SelectionChangedCommand = new Command(OnSelectionChangedCommandExecute);
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
		public static readonly PropertyData AdminAnswerProperty = RegisterProperty("AdminAnswer", typeof(string), "Ничего не выбрано.");




		protected override async Task InitializeAsync()
		{
			await base.InitializeAsync();
		}

		protected override async Task CloseAsync()
		{
			await base.CloseAsync();
		}


		public Command ClearTextCommand { get; private set; }
		private void OnClearTextCommandExecute()
		{
			if (AdminAnswer == NO_ANSWER)
			{
				AdminAnswer = string.Empty;
			}
		}

		public Command SelectionChangedCommand { get; private set; }
		private void OnSelectionChangedCommandExecute()
		{
			RefreshAdminAnswer();
		}


		public Command SaveAnswerCommand { get; private set; }
		private bool OnSaveAnswerCommandCanExecute()
		{
			return !string.IsNullOrEmpty(AdminAnswer) && 
					AdminAnswer != NO_ANSWER && 
					SelectedClientReview != null;
		}
		private void OnSaveAnswerCommandExecute()
		{
			int id = SelectedClientReview.Id;
			_binder.SaveAnswer(AdminAnswer, id);
			RefreshReviewsCollection();
			// выбираем тот же отзыв
			SelectedClientReview = ClientReviews.First(c => c.Id == id);
		}


		private void RefreshReviewsCollection() => Filler<ClientReview>.Fill(ClientReviews, _unitOfWork.ClientReviews);

		private void RefreshAdminAnswer()
		{
			AdminAnswer = _binder.GetManagerMessage(SelectedClientReview);
		}
	}
}
