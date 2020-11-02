using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using UPS_APP.IService;
using UPS_APP.Model;

namespace UPS_APP
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly IUsersService _service;
        private readonly IOptions<AppSettings> _settings;

        public MainWindow() { }
        public MainWindow(IUsersService service, IOptions<AppSettings> settings)
        {
            InitializeComponent();
            _service = service;
            _settings = settings;
            usersDataGrid.ItemsSource = null;
            LoadCommand(null, null);

        }

        private void RefreshCommand()
        {
            var users = _service.LoadUsers();
            usersDataGrid.ItemsSource = users;
        }
        private void LoadCommand(object sender, RoutedEventArgs e)
        {
            var users = _service.LoadUsers();
            usersDataGrid.ItemsSource = users;
        }


        private void SearchCommand(object sender, RoutedEventArgs e)
        {
            int number = 0;
            
            if(usersDataGrid.Items.IsInUse) usersDataGrid.ItemsSource = null;

            if (String.IsNullOrEmpty(SearchTextBox.Text))
            {
                var users = _service.LoadUsers();
                usersDataGrid.ItemsSource = users;
            }

            bool isNumber = int.TryParse(SearchTextBox.Text, out number);

            if (isNumber)
            {
                var list = new List<UsersModel>();
                list.Add(_service.SearchById(number));
                usersDataGrid.ItemsSource = list;
            }

            else if (!string.IsNullOrEmpty(SearchTextBox.Text))
            {
                usersDataGrid.ItemsSource = _service.SearchByName(SearchTextBox.Text);
            }
        }

        private void AddCommand(object sender, ExecutedRoutedEventArgs e)
        {
            Registration registrationWindow = new Registration(_service, _settings);
            registrationWindow.ShowDialog();
        }

        private void UpdateCommand(object sender, ExecutedRoutedEventArgs e)
        {
            UsersModel user = (UsersModel)usersDataGrid.SelectedItem;

            var update = _service.Update(user);
            if(update != null)
            {
                RefreshCommand();
            }
        }

        private void DeleteCommand(object sender, ExecutedRoutedEventArgs e)
        {
            UsersModel user = (UsersModel)usersDataGrid.SelectedItem;

            bool delete = _service.Delete(user.id);
            if (delete)
            {
                RefreshCommand();
            }
        }
    }
}
