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
    /// Логика взаимодействия для UpdateСonfiguration.xaml
    /// </summary>
    public partial class UpdateСonfiguration : Window
    {
        DataBaseSerebryannikov1 db = new DataBaseSerebryannikov1();
        CarEquipment ce;
        public UpdateСonfiguration(CarEquipment carEquipment)
        {
            InitializeComponent();
            this.ce = carEquipment;
            cmbSelectCar.ItemsSource = db.Car.ToList();
            cmbSelectTypeEquipment.ItemsSource = db.TypeEquipment.ToList();
        }

        private void BtnUpdateСonfiguration_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtEquipmentName.Text))
            {
                MessageBox.Show("Введите наименование!", "Добавление конфигурации", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (string.IsNullOrEmpty(txtEquipmentPrice.Text))
            {
                MessageBox.Show("Введите цену!", "Добавление конфигурации", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (string.IsNullOrEmpty(txtEquipmentDescription.Text))
            {
                MessageBox.Show("Введите описание!", "Добавление конфигурации", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (string.IsNullOrEmpty(cmbSelectCar.Text))
            {
                MessageBox.Show("Выберите автомобиль!", "Добавление конфигурации", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (string.IsNullOrEmpty(cmbSelectTypeEquipment.Text))
            {
                MessageBox.Show("Выберите тип конфигурации!", "Добавление конфигурации", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            CarEquipment carEquipment = db.CarEquipment.Where(x => x.id == ce.id).FirstOrDefault();
            carEquipment.nameEquipment = txtEquipmentName.Text;
            carEquipment.price = Convert.ToInt32(txtEquipmentPrice.Text);
            carEquipment.descriptionEquipment = txtEquipmentDescription.Text;
            carEquipment.id_Car = (cmbSelectCar.SelectedValue as Car).id;
            carEquipment.id_TypeEquipment = (cmbSelectTypeEquipment.SelectedValue as TypeEquipment).id;
            db.SaveChanges();
            ControlConfigurator controlConfigurator = new ControlConfigurator();
            controlConfigurator.Show();
            this.Close();
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            ControlConfigurator controlConfigurator = new ControlConfigurator();
            controlConfigurator.Show();
            this.Close();
        }

        private void TxtEquipmentPrice_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text, 0)) e.Handled = true;
        }
    }
}
