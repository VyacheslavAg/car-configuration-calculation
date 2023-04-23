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
    /// Логика взаимодействия для AddCar.xaml
    /// </summary>
    public partial class AddCar : Window
    {
        DataBaseSerebryannikov1 db = new DataBaseSerebryannikov1();
        public AddCar()
        {
            InitializeComponent();
        }

        private void BtnAddCar_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtCarName.Text))
            {
                MessageBox.Show("Введите наименование!", "Добавление автомобиля", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (string.IsNullOrEmpty(txtPrice.Text))
            {
                MessageBox.Show("Введите цену!", "Добавление автомобиля", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (string.IsNullOrEmpty(txtDescription.Text))
            {
                MessageBox.Show("Введите описание!", "Добавление автомобиля", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            Car car = new Car();
            car.name = txtCarName.Text;
            car.price = Convert.ToInt32(txtPrice.Text);
            car.description = txtDescription.Text;
            if (imgCar.Source == null)
            {
                if (MessageBox.Show("Вы хотите добавить изображение к автомобилю?", "Добавление автомобиля", MessageBoxButton.YesNo, MessageBoxImage.Warning, MessageBoxResult.No) == MessageBoxResult.No)
                {
                    db.Car.Add(car);
                    db.SaveChanges();                    
                }
                else
                {
                    return;
                }
            }
            else
            {
                car.photo = ImagingWork.ImageToByteArray(imgCar.Source as BitmapImage);
                db.Car.Add(car);
                db.SaveChanges();
            }
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
            }
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void TxtPrice_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text, 0)) e.Handled = true;
        }
    }
}
