﻿using System.Threading.Tasks;
using Catel.MVVM;

namespace RestaurantHelper.ViewModels.ClientViewModels.OrderViewModels
{
    public class MenuViewModel : ViewModelBase
    {
        private readonly IViewModel _parentViewModel;

        public MenuViewModel(IViewModel parentViewModel)
        {
            _parentViewModel = parentViewModel;
        }

        // TODO: Register models with the vmpropmodel codesnippet
        // TODO: Register view model properties with the vmprop or vmpropviewmodeltomodel codesnippets
        // TODO: Register commands with the vmcommand or vmcommandwithcanexecute codesnippets

        protected override async Task InitializeAsync()
        {
            await base.InitializeAsync();

            // TODO: subscribe to events here
        }

        protected override async Task CloseAsync()
        {
            // TODO: unsubscribe from events here

            await base.CloseAsync();
        }
    }
}
