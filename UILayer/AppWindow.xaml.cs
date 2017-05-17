using Entities;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using UILayer.ViewModels;

namespace UILayer
{
    /// <summary>
    /// Interaction logic for AppWindow.xaml
    /// </summary>
    public partial class AppWindow : Window
    {
        private AddOrderViewModel viewModel;

        public AppWindow()
        {
            InitializeComponent();
        }

        private void WorkersListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            viewModel = AddOrderGrid.DataContext as AddOrderViewModel;
            var selected = new List<Worker>();
            foreach (Worker item in WorkersListBox.SelectedItems)
            {
                selected.Add(item);
            }
           viewModel.SelectedWorkers = selected;
        }
    }
}
