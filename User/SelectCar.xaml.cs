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
    /// Логика взаимодействия для SelectCar.xaml
    /// </summary>
    public partial class SelectCar : Window
    {
        DataBaseSerebryannikov1 db = new DataBaseSerebryannikov1();
        Car car;
        public SelectCar()
        {
            InitializeComponent();
            List<Car> carList = db.Car.ToList();
            GridCar.ItemsSource = null;
            GridCar.ItemsSource = carList;
        }

        private void BtnConfigurator_Click(object sender, RoutedEventArgs e)
        {            
            car = GridCar.SelectedValue as Car;
            if (car == null)
            {
                MessageBox.Show("Выберите автомобиль!", "Выбор автомобиля", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            else
            {
                Configurator configurator = new Configurator(car);
                if (car == null)
                {
                    MessageBox.Show("Выберите автомобиль!");
                    return;
                }
                var selectCar = db.Database.SqlQuery<Car>("SELECT * FROM Car WHERE id = " + car.id);
                foreach (var cars in selectCar)
                {
                    configurator.txtSumPrice.Text = Convert.ToString(cars.price.ToString());
                    configurator.txtCarName.Text = cars.name.ToString();
                }           
                configurator.Show();
                this.Close();
            }
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        private void GridCar_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            car = GridCar.SelectedValue as Car;           
            var selectCar = db.Database.SqlQuery<Car>("SELECT * FROM Car WHERE id = " + car.id);
            foreach(var cars in selectCar)
            {
                txtCarName.Text = cars.name.ToString();
                txtPrice.Text = Convert.ToString(cars.price.ToString());
                txtDescription.Text = cars.description.ToString();
                UpdateImage(cars);
            }
        }

        private void UpdateImage(Car car)
        {
            imgCar.Source = new BitmapImage(new Uri("Resources/ImageNotFound.png", UriKind.Relative));
            if (car != null)
            {
                if (car.photo != null)
                    imgCar.Source = ImagingWork.ToBitmapImage(car.photo);
            }
        }
    }
}
