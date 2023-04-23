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
using Microsoft.Win32;

namespace Приложение
{
    /// <summary>
    /// Логика взаимодействия для UpdateCar.xaml
    /// </summary>
    public partial class UpdateCar : Window
    {
        DataBaseSerebryannikov1 db = new DataBaseSerebryannikov1();
        Car c;
        public bool F = false;
        public UpdateCar(Car car)
        {
            InitializeComponent();
            this.c = car;
            UpdateImage(car);
        }
        
        private void BtnUpdateCar_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtCarName.Text))
            {
                MessageBox.Show("Введите наименование!", "Изменение автомобиля", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (string.IsNullOrEmpty(txtPrice.Text))
            {
                MessageBox.Show("Введите цену!", "Изменение автомобиля", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (string.IsNullOrEmpty(txtDescription.Text))
            {
                MessageBox.Show("Введите описание!", "Изменение автомобиля", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            Car car = db.Car.Where(x => x.id == c.id).FirstOrDefault();
            car.name = txtCarName.Text;
            car.price = Convert.ToInt32(txtPrice.Text);
            car.description = txtDescription.Text;
            if (imgCar.Source == null)
            {
                if (MessageBox.Show("Вы хотите изменить изображение автомобиля?", "Изменение автомобиля", MessageBoxButton.YesNo, MessageBoxImage.Warning, MessageBoxResult.No) == MessageBoxResult.No)
                {
                    db.SaveChanges();
                }
                else
                {
                    return;
                }
            }
            else
            {
                if (F)
                {
                    car.photo = ImagingWork.ImageToByteArray(imgCar.Source as BitmapImage);                   
                }
                db.SaveChanges();
            }
            ControlCars controlCars = new ControlCars();
            controlCars.Show();
            this.Close();            
        }

        private void BtnAddCarPhoto_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.FilterIndex = 2;
            fileDialog.Filter = "jpg|*.jpg";
            if (fileDialog.ShowDialog() == true)
            {
                BitmapImage src = new BitmapImage();
                src.BeginInit();
                src.UriSource = new Uri(@"" + fileDialog.FileName, UriKind.Relative);
                src.CacheOption = BitmapCacheOption.OnLoad;
                src.EndInit();
                imgCar.Source = src;
                imgCar.Stretch = Stretch.Uniform;
                F = true;
            }
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            ControlCars controlCars = new ControlCars();
            controlCars.Show();
            this.Close();
        }

        private void TxtPrice_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text, 0)) e.Handled = true;
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
