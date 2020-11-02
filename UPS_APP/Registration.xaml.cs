using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using UPS_APP.IService;
using UPS_APP.Model;

namespace UPS_APP
{
    /// <summary>
    /// Interaction logic for Registration.xaml
    /// </summary>
    public partial class Registration : Window
    {
        private readonly IUsersService _service;
        private readonly AppSettings _appSettings;
        public Registration(IUsersService service, IOptions<AppSettings> settings)
        {
            InitializeComponent();
            _service = service;
            _appSettings = settings.Value;
            
        }

        private void Reset()
        {
            NameTextBox.Text ="";
            EmailTextBox.Text = "";
            GenderTextBox.Text = "";
            StatusTextBox.Text = "";
        }

        private void AddCommandHandler(object sender, RoutedEventArgs e)
        {
            var user = new UsersModel
            {
                name = NameTextBox.Text,
                email = EmailTextBox.Text,
                gender = GenderTextBox.Text,
                status = StatusTextBox.Text
            };
            var addResult = _service.AddUser(user);
            if (addResult.id <= 0)
            {
                lblMessage.Content = "Failed to register";
                lblMessage.Visibility = Visibility.Visible;
            }
            else
            {
                lblMessage.Content = "Registered Successfully";
                lblMessage.Visibility = Visibility.Visible;
                Reset();
            }
        }
    }
}
