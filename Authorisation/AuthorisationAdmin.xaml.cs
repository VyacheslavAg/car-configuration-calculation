using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace Приложение
{
    /// <summary>
    /// Логика взаимодействия для Authorisation.xaml
    /// </summary>
    public partial class AuthorisationAdmin : Window
    {
        DataBaseSerebryannikov1 db = new DataBaseSerebryannikov1();
        public AuthorisationAdmin()
        {
            InitializeComponent();
        }

        private void BtnAuthorisation_Click(object sender, RoutedEventArgs e)
        {
            var data = db.Database.SqlQuery<Auth>("SELECT * FROM Auth WHERE login = 'admin'");
            foreach (var auth in data)
            {
                if (pbPassword.Password == auth.password.ToString() && txtLogin.Text == auth.login.ToString())
                {
                    AdminPanel adminPanel = new AdminPanel();
                    adminPanel.Show();
                }
                else
                {
                    MessageBox.Show("Вы ввели неверный логин или пароль!", "Авторизация", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }
            this.Close();
        }
    }
}
