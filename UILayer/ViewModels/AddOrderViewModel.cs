using DALayer;
using Entities;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Input;
using UILayer.Commands;

namespace UILayer.ViewModels
{   //TODO: add fields validation!!!
    public class AddOrderViewModel : INotifyPropertyChanged
    {
        private UnitOfWork unitOfWork;
        private Order order;
        private ICommand addOrderCommand;

        public event PropertyChangedEventHandler PropertyChanged;

        public AddOrderViewModel()
        {
            unitOfWork = new UnitOfWork();
            order = new Order()
            {
                ReceptionDate = DateTime.Now
            };
            addOrderCommand = new AddOrderCommand(() => true, AddOrder);
        }

        private void AddOrder()
        {
            try
            {
                unitOfWork.OrderRepository.AddOrder(order);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Some thing wrong:\n" + ex.Message);
            }
            MessageBox.Show("Succes");
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ICommand AddOrderCommand
        {
            get => addOrderCommand;
        }

        #region UI props
        public string CustomerName
        {
            get => order.CustomerName;
            set => order.CustomerName = value;
        }

        public string PhoneNumber
        {
            get => order.PhoneNumber;
            set => order.PhoneNumber = value;
        }

        public string Email
        {
            get => order.Email;
            set => order.Email = value;
        }

        public string Device
        {
            get => order.Device;
            set => order.Device = value;
        }

        public string Problem
        {
            get => order.Problem;
            set => order.Problem = value;
        }

        public DateTime ReceptionDate
        {
            get => order.ReceptionDate;
            set => order.ReceptionDate = value;
        }

        public decimal Cost
        {
            get => order.Cost;
            set => order.Cost = value;
        }

        public decimal Income
        {
            get => order.Income;
            set => order.Income = value;
        }

        public string Note
        {
            get => order.Note;
            set => order.Note = value;
        }
        #endregion
    }
}
