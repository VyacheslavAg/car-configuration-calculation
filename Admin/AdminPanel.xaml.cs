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
    /// Логика взаимодействия для AdminPanel.xaml
    /// </summary>
    public partial class AdminPanel : Window
    {
        public AdminPanel()
        {
            InitializeComponent();
        }

        private void BtnControlCars_Click(object sender, RoutedEventArgs e)
        {
            ControlCars controlCars = new ControlCars();
            controlCars.Show();
            this.Close();
        }

        private void BtnControlConfigurator_Click(object sender, RoutedEventArgs e)
        {
            ControlConfigurator controlConfigurator = new ControlConfigurator();
            controlConfigurator.Show();
            this.Close();
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }
    }
}
