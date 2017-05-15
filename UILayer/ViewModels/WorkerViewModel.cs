﻿using DALayer;
using Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using UILayer.Commands;
using UILayer.Properties;

namespace UILayer.ViewModels
{
    public class WorkerViewModel : INotifyPropertyChanged
    {
        private UnitOfWork unitOfWork;

        private ICommand addWorkerCommand;
        private string newWorkerName;

        private IEnumerable<Worker> workers;
        private Worker selectedWorker;

        private DateTime fromDate;
        private DateTime toDate;
        private int doneOrders;
        private int activeOrders;
        private decimal ordersIncome;
        private ICommand updateWorkerCommand;

        public event PropertyChangedEventHandler PropertyChanged;

        public WorkerViewModel()
        {
            unitOfWork = new UnitOfWork();
            addWorkerCommand = new AddCommand(() => true, Add);
            AssignWorkers();
            SelectedWorker = workers.FirstOrDefault();
            updateWorkerCommand = new AddCommand(() => true, UpdateWorker);
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #region add worker
        private void Add()
        {
            if (string.IsNullOrWhiteSpace(newWorkerName))
            {
                MessageBox.Show(Resources.MsgWorkerEmpty, Resources.MsgError,
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            try
            {
                unitOfWork.WorkerRepository.Add(new Worker { Name = newWorkerName });
                AssignWorkers();
                MessageBox.Show(Resources.MsgAddWorkerSuccess, Resources.MsgSuccess,
                    MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(Resources.MsgAddFail + ex.Message, Resources.MsgError,
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public ICommand AddWorkerCommand
        {
            get => addWorkerCommand;
        }

        public string NewWorkerName
        {
            get => newWorkerName;
            set => newWorkerName = value;
        }
        #endregion

        #region display workers
        private void AssignWorkers()
        {
            workers = unitOfWork.WorkerRepository.GetWorkers();
            OnPropertyChanged("Workers");
        }

        public IEnumerable<Worker> Workers
        {
            get => workers;
        } 

        public Worker SelectedWorker
        {
            get => selectedWorker;
            set
            {
                selectedWorker = value;
                OnPropertyChanged("SelectedWorker");
                AssingSelectedWorkerInformation();
            }
        }
        #endregion

        #region selected worker information
        private void AssingSelectedWorkerInformation()
        {
            ToDate = DateTime.Today.Date;
            FromDate = ToDate.AddDays(-30);
            doneOrders = unitOfWork.WorkerRepository
                .DoneOrdersCount(selectedWorker, fromDate, toDate);
            activeOrders = unitOfWork.WorkerRepository
                .ActiveOrdersCount(selectedWorker, fromDate, toDate);
            ordersIncome = unitOfWork.WorkerRepository
                .GetDoneOrdersIncome(selectedWorker, fromDate, toDate);
            OnPropertyChanged("DoneOrders");
            OnPropertyChanged("ActiveOrders");
            OnPropertyChanged("OrdersIncome");
            OnPropertyChanged("SelectedWorkerNewName");
        }

        private void UpdateWorker()
        {
            try
            {
                unitOfWork.WorkerRepository.Update(SelectedWorker);
            }
            catch (Exception ex)
            {
                MessageBox.Show(Resources.MsgAddFail + ex.Message, Resources.MsgError,
                    MessageBoxButton.OK, MessageBoxImage.Error);
                AssignWorkers();
                return;
            }
            MessageBox.Show(Resources.MsgUpdateWorker, Resources.MsgSuccess,
                MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private bool IsDateValid()
        {
            return fromDate < toDate
                && toDate <= DateTime.Today.Date;
        }

        public DateTime FromDate
        {
            get => fromDate;
            set => fromDate = value;
        }

        public DateTime ToDate
        {
            get => toDate;
            set => toDate = value;
        }

        public string DoneOrders
        {
            get => doneOrders.ToString();
        }

        public string ActiveOrders
        {
            get => activeOrders.ToString();
        }

        public string OrdersIncome
        {
            get => ordersIncome.ToString();
        }

        public string SelectedWorkerNewName
        {
            get => SelectedWorker.Name;
            set => SelectedWorker.Name = value;
        }

        public ICommand UpdateWorkerCommand
        {
            get => updateWorkerCommand;
        }
        #endregion
    }
}
