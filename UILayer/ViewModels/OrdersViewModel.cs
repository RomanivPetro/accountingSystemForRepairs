﻿using DALayer;
using Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using UILayer.Commands;
using UILayer.Properties;

namespace UILayer.ViewModels
{
    public class OrdersViewModel : INotifyPropertyChanged
    {
        private UnitOfWork unitOfWork;

        private ObservableCollection<Order> orders;
        private Order selectedOrder;
        public string phoneNumber;
        private ICommand getActiveOrdersCommand;
        private ICommand updateOrderCommand;
        private ICommand findByPhoneCommand;

        public event PropertyChangedEventHandler PropertyChanged;

        public OrdersViewModel()
        {
            unitOfWork = new UnitOfWork();
            getActiveOrdersCommand = new AddCommand(() => true, AssingActiveOrders);
            findByPhoneCommand = new AddCommand(() => true, FindByPhone);
            updateOrderCommand = new AddCommand(() => true, UpdateSelectedOrder);
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

        private void UpdateSelectedOrder()
        {
            try
            {
                unitOfWork.OrderRepository.Update(selectedOrder);
                MessageBox.Show(Resources.MsgOrderUpdateSuccess, Resources.MsgSuccess,
                    MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception)
            {
                MessageBox.Show(Resources.MsgOrderUpdateFail, Resources.MsgError,
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void FindByPhone()
        {
            try
            {
                var newOrders = unitOfWork.OrderRepository.FindByPhone(phoneNumber);
                orders = new ObservableCollection<Order>(newOrders);
                OnPropertyChanged("Orders");
            }
            catch (Exception)
            {
                MessageBox.Show(Resources.MsgFindOrderFail, Resources.MsgError, 
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public ICommand GetActiveOrdersCommand
        {
            get { return getActiveOrdersCommand; }
        }

        public ICommand FindByPhoneCommand
        {
            get { return findByPhoneCommand; }
        }

        public ICommand UpdateOrderCommand
        {
            get { return updateOrderCommand; }
        }

        public ObservableCollection<Order> Orders
        {
            get { return orders; }
        }

        public string PhoneNumber
        {
            get { return phoneNumber; }
            set { phoneNumber = value; }
        }

        public Order SelectedOrder
        {
            get { return selectedOrder; }
            set
            {
                selectedOrder = value;
                OnPropertyChanged("SelectedOrderPhone");
                OnPropertyChanged("SelectedOrderCost");
                OnPropertyChanged("SelectedOrderIncome");
                OnPropertyChanged("SelectedOrderReceptionDate");
                OnPropertyChanged("SelectedOrderGivingDate");
                OnPropertyChanged("SelectedOrderNote");
                OnPropertyChanged("Workers");
            }
        }

        public string SelectedOrderPhone
        {
            get { return selectedOrder?.PhoneNumber; }
            set { selectedOrder.PhoneNumber = value; }
        }

        public decimal? SelectedOrderCost
        {
            get { return selectedOrder?.Cost; }
            set { selectedOrder.Cost = (decimal)value; }
        }

        public decimal? SelectedOrderIncome
        {
            get { return selectedOrder?.Income; }
            set { selectedOrder.Income = (decimal)value; }
        }

        public DateTime SelectedOrderReceptionDate
        {
            get { return selectedOrder == null ? DateTime.Now : selectedOrder.ReceptionDate; }
            set { selectedOrder.ReceptionDate = value; }
        }

        public DateTime? SelectedOrderGivingDate
        {
            get { return selectedOrder?.GivingDate; }
            set { selectedOrder.GivingDate = value; }
        }

        public string SelectedOrderNote
        {
            get { return selectedOrder?.Note; }
            set { selectedOrder.Note = value; }
        }

        public IEnumerable<string> Workers
        {
            get { return selectedOrder?.Worker.Select(w => w.Name); }
        }
    }
}
