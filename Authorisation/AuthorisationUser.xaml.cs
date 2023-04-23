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
    /// Логика взаимодействия для AuthorisationUser.xaml
    /// </summary>
    public partial class AuthorisationUser : Window
    {
        DataBaseSerebryannikov1 db = new DataBaseSerebryannikov1();
        public AuthorisationUser()
        {
            InitializeComponent();
        }

        private void BtnAuthorisation_Click(object sender, RoutedEventArgs e)
        {
            var data = db.Database.SqlQuery<Auth>("SELECT * FROM Auth WHERE login = 'user'");
            foreach (var auth in data)
            {
                if (pbPassword.Password == auth.password.ToString() && txtLogin.Text == auth.login.ToString())
                {
                    SelectCar selectCar = new SelectCar();
                    selectCar.Show();
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
