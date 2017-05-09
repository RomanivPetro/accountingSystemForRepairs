using DALayer;
using System;
using System.Windows;

namespace UILayer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private UnitOfWork unitOfWork = new UnitOfWork();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string userName = LoginText.Text;
            string password = PasswordText.Password;

            try
            {
                if (unitOfWork.AdministratorRepository.Login(userName, password))
                {
                    //TODO: run another window
                    MessageBox.Show("success");
                }
                else
                {
                    MessageBox.Show("Incorrect Login or password");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }
    }
}
