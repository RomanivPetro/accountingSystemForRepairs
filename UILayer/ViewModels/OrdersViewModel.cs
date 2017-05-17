using DALayer;
using Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using UILayer.Commands;

namespace UILayer.ViewModels
{
    public class OrdersViewModel : INotifyPropertyChanged
    {
        private UnitOfWork unitOfWork;

        private ObservableCollection<Order> orders;
        private ICommand getActiveOrdersCommand;

        public event PropertyChangedEventHandler PropertyChanged;

        public OrdersViewModel()
        {
            unitOfWork = new UnitOfWork();
            getActiveOrdersCommand = new AddCommand(() => true, AssingActiveOrders);
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void AssingActiveOrders()
        {
            var ordersCollection = unitOfWork.OrderRepository.GetActiveOrders();
            orders = new ObservableCollection<Order>(ordersCollection);
            OnPropertyChanged("Orders");
        }

        public ICommand GetActiveOrdersCommand
        {
            get => getActiveOrdersCommand;
        }

        public ObservableCollection<Order> Orders
        {
            get => orders;
        }
    }
}
