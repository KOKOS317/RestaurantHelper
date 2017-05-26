﻿using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Catel.Collections;
using Catel.Data;
using Catel.MVVM;
using RestaurantHelper.DAL;
using RestaurantHelper.DAL.Repositories;
using RestaurantHelper.Models;
using RestaurantHelper.Services.Logic;

namespace RestaurantHelper.ViewModels.ClientViewModels.OrderViewModels
{
    public class ClientTotalsViewModel : ViewModelBase
    {
		private readonly UnitOfWork _unitOfWork = UnitOfWork.GetInstance();
		private readonly User _user;
	    private readonly Reservation _reservation;
	    private readonly FastObservableCollection<OrderedDish> _orderedDishes;
	    private readonly IViewModel _rootViewModel;
	    private readonly OrderedSumCalculator _sumCalculator;

		public ClientTotalsViewModel(User user, Reservation reservation, FastObservableCollection<OrderedDish> orderedDishes)
        {
			_user = user;
			_reservation = reservation;
			_orderedDishes = orderedDishes;
			_sumCalculator = new OrderedSumCalculator();

			_rootViewModel = ViewModelManager.GetFirstOrDefaultInstance<MainWindowViewModel>();

			BackCommand = new Command(OnBackCommandExecute);
			OrderCommand = new Command(OnOrderCommandExecute);

			FillViewModelProperties();
        }

	    public string UserLogin
		{
			get { return GetValue<string>(UserLoginProperty); }
			set { SetValue(UserLoginProperty, value); }
		}
		public static readonly PropertyData UserLoginProperty = RegisterProperty("UserLogin", typeof(string));

		public int TableNumber
		{
			get { return GetValue<int>(TableNumberProperty); }
			set { SetValue(TableNumberProperty, value); }
		}
		public static readonly PropertyData TableNumberProperty = RegisterProperty("TableNumber", typeof(int));

		public int TableSeatsNumber
		{
			get { return GetValue<int>(TableSeatsNumberProperty); }
			set { SetValue(TableSeatsNumberProperty, value); }
		}
		public static readonly PropertyData TableSeatsNumberProperty = RegisterProperty("TableSeatsNumber", typeof(int));

		public string VisitDate
		{
			get { return GetValue<string>(VisitDateProperty); }
			set { SetValue(VisitDateProperty, value); }
		}
		public static readonly PropertyData VisitDateProperty = RegisterProperty("VisitDate", typeof(string));

		public string FirstTime
		{
			get { return GetValue<string>(FirstTimeProperty); }
			set { SetValue(FirstTimeProperty, value); }
		}
		public static readonly PropertyData FirstTimeProperty = RegisterProperty("FirstTime", typeof(string));

		public string LastTime
		{
			get { return GetValue<string>(LastTimeProperty); }
			set { SetValue(LastTimeProperty, value); }
		}
		public static readonly PropertyData LastTimeProperty = RegisterProperty("LastTime", typeof(string));

		public FastObservableCollection<OrderedDish> OrderedDishes
		{
			get { return GetValue<FastObservableCollection<OrderedDish>>(OrderedDishesProperty); }
			set { SetValue(OrderedDishesProperty, value); }
		}
		public static readonly PropertyData OrderedDishesProperty = RegisterProperty("OrderedDishes", typeof(FastObservableCollection<OrderedDish>), 
			new FastObservableCollection<OrderedDish>());

		public int TotalSum
		{
			get { return GetValue<int>(TotalSumProperty); }
			set { SetValue(TotalSumProperty, value); }
		}
		public static readonly PropertyData TotalSumProperty = RegisterProperty("TotalSum", typeof(int));

		public string PhoneNumber
		{
			get { return GetValue<string>(PhoneNumberProperty); }
			set { SetValue(PhoneNumberProperty, value); }
		}
		public static readonly PropertyData PhoneNumberProperty = RegisterProperty("PhoneNumber", typeof(string));


	    public Command BackCommand { get; private set; }
		private void OnBackCommandExecute()
		{
			_rootViewModel.ChangePage(new ClientMenuViewModel(_user, _reservation, _orderedDishes));
		}


		public Command OrderCommand { get; private set; }
		private void OnOrderCommandExecute()
		{
			SaveReservation();
			SaveOrderWithDishes();
			_rootViewModel.ChangePage(new ClientMainViewModel(_user));
		}


	    protected override async Task InitializeAsync()
        {
            await base.InitializeAsync();
        }

        protected override async Task CloseAsync()
        {
            await base.CloseAsync();
        }

		private void FillViewModelProperties()
		{
			UserLogin = _user.Login;
			PhoneNumber = _user.Phone;

			TableNumber = _reservation.TableId;
			TableSeatsNumber = _unitOfWork.Tables.GetById(_reservation.TableId).SeatsNumber;

			VisitDate = _reservation.Day.ToLongDateString();
			FirstTime = _reservation.FirstTime.ToShortTimeString();
			LastTime = _reservation.LastTime.ToShortTimeString();

			OrderedDishes = _orderedDishes;
			TotalSum = _sumCalculator.GetCurrentOrderedSum(OrderedDishes);
		}

		private void SaveReservation()
		{
			_unitOfWork.Reservations.Insert(_reservation);
			_unitOfWork.SaveChanges();
		}

		private void SaveOrderWithDishes()
		{
			Order order = new Order
			{
				UserId = _user.Id,
				ReservationId = _reservation.Id
			};

			_unitOfWork.Orders.Insert(order);
			_unitOfWork.SaveChanges();
			// получаем индекс только что созданного заказа, который сгенерировала БД
			var orderId = _unitOfWork.Orders.GetAll().Max(o => o.Id);

			foreach (var dish in _orderedDishes)
			{
				OrderedDish orderedDish = new OrderedDish
				{
					OrderId = orderId,
					DishId = dish.Id,
					Quantity = dish.Quantity
				};
				_unitOfWork.OrderedDishes.Insert(orderedDish);
			}
			_unitOfWork.SaveChanges();
		}
	}
}