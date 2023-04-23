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
    /// Логика взаимодействия для ControlCars.xaml
    /// </summary>
    public partial class ControlCars : Window
    {
        DataBaseSerebryannikov1 db = new DataBaseSerebryannikov1();
        List<Car> carList = new List<Car>();
        public ControlCars()
        {
            InitializeComponent();
            List<Car> carList = db.Car.ToList();
            GridCar.ItemsSource = null;
            GridCar.ItemsSource = carList;
        }

        private void BtnAddCar_Click(object sender, RoutedEventArgs e)
        {
            AddCar addCar = new AddCar();
            addCar.ShowDialog();
            this.Hide();
            this.Show();
            update();
        }
                
        private void BtnUpdateCar_Click(object sender, RoutedEventArgs e)
        {
            Car car = GridCar.SelectedValue as Car;
            UpdateCar updateCar = new UpdateCar(car);
            if (car == null)
            {
                MessageBox.Show("Ошибка! Вы не выбрали поле для изменения!", "Изменение автомобиля", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            updateCar.txtCarName.Text = car.name;
            updateCar.txtPrice.Text = Convert.ToString(car.price);
            updateCar.txtDescription.Text = car.description;
            updateCar.ShowDialog();
            this.Hide();          
        }

        private void BtnDeleteCar_Click(object sender, RoutedEventArgs e)
        {
            Car car = GridCar.SelectedValue as Car;
            if (car == null)
            {
                MessageBox.Show("Ошибка! Вы не выбрали поле для удаления!", "Удаление автомобиля", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (MessageBox.Show("Вы действительно хотите удалить этот автомобиль? Удаление автомобиля может привести к удалению соответствующих ему конфигураций!", "Удаление автомобиля", MessageBoxButton.YesNo, MessageBoxImage.Warning, MessageBoxResult.No) == MessageBoxResult.Yes)
            {
                db.Car.Remove(car);
                int numberOfRowDeleted = db.Database.ExecuteSqlCommand("DELETE FROM CarEquipment WHERE id_Car=" + car.id);
                db.SaveChanges();
                update();
            }
            else
            {
                return;
            }
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            AdminPanel adminPanel = new AdminPanel();
            adminPanel.Show();            
            this.Close();
        }

        private void UpdateImage(Car car)
        {
            UpdateCar updateCar = new UpdateCar(car);
            updateCar.imgCar.Source = new BitmapImage(new Uri("Resources/ImageNotFound.png", UriKind.Relative));
            if (car != null)
            {
                if (car.photo != null)
                    updateCar.imgCar.Source = ImagingWork.ToBitmapImage(car.photo);
            }
        }

        private void update()
        {
            GridCar.ItemsSource = db.Car.ToList();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            update();
        }
    }
}
