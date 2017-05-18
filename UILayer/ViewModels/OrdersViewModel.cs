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
        private Order selectedOrder;
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

        public Order SelectedOrder
        {
            get => selectedOrder;
            set
            {
                selectedOrder = value;
                OnPropertyChanged("SelectedOrderPhone");
                OnPropertyChanged("SelectedOrderCost");
                OnPropertyChanged("SelectedOrderIncome");
                OnPropertyChanged("SelectedOrderReceptionDate");
                OnPropertyChanged("SelectedOrderGivingDate");
                OnPropertyChanged("SelectedOrderNote");
            }
        }

        public string SelectedOrderPhone
        {
            get => selectedOrder?.PhoneNumber;
            set => selectedOrder.PhoneNumber = value;
        }

        public decimal? SelectedOrderCost
        {
            get => selectedOrder?.Cost;
            set => selectedOrder.Cost = (decimal)value;
        }

        public decimal? SelectedOrderIncome
        {
            get => selectedOrder?.Income;
            set => selectedOrder.Income = (decimal)value;
        }

        public DateTime SelectedOrderReceptionDate
        {
            get => selectedOrder == null ? DateTime.Now : selectedOrder.ReceptionDate;
            set => selectedOrder.ReceptionDate = value;
        }

        public DateTime? SelectedOrderGivingDate
        {
            get => selectedOrder?.GivingDate;
            set => selectedOrder.GivingDate = value;
        }

        public string SelectedOrderNote
        {
            get => selectedOrder?.Note;
            set => selectedOrder.Note = value;
        }
    }
}
