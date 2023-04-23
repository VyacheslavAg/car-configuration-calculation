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
    /// Логика взаимодействия для ControlConfigurator.xaml
    /// </summary>
    public partial class ControlConfigurator : Window
    {
        DataBaseSerebryannikov1 db = new DataBaseSerebryannikov1();
        List<CarEquipment> carEquipmentList;
        public ControlConfigurator()
        {
            InitializeComponent();
            carEquipmentList = db.CarEquipment.ToList();
            GridConfigurator.ItemsSource = null;
            GridConfigurator.ItemsSource = carEquipmentList;
        }

        private void BtnAddСonfiguration_Click(object sender, RoutedEventArgs e)
        {
            AddСonfiguration addСonfiguration = new AddСonfiguration();
            addСonfiguration.ShowDialog();
            this.Hide();
            this.Show();
            update();
        }

        private void BtnUpdateСonfiguration_Click(object sender, RoutedEventArgs e)
        {
            CarEquipment carEquipment = GridConfigurator.SelectedValue as CarEquipment;
            UpdateСonfiguration updateСonfiguration = new UpdateСonfiguration(carEquipment);
            if (carEquipment == null)
            {
                MessageBox.Show("Вы не выбрали поле для изменения!", "Изменение конфигурации", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            updateСonfiguration.txtEquipmentName.Text = carEquipment.nameEquipment;
            updateСonfiguration.txtEquipmentPrice.Text = Convert.ToString(carEquipment.price);
            updateСonfiguration.txtEquipmentDescription.Text = carEquipment.descriptionEquipment;
            updateСonfiguration.cmbSelectCar.Text = carEquipment.Car.name;
            updateСonfiguration.cmbSelectTypeEquipment.Text = carEquipment.TypeEquipment.name;
            updateСonfiguration.ShowDialog();
            this.Hide();
        }

        private void BtnDeleteСonfiguration_Click(object sender, RoutedEventArgs e)
        {
            CarEquipment carEquipment = GridConfigurator.SelectedValue as CarEquipment;
            if (carEquipment == null)
            {
                MessageBox.Show("Ошибка! Вы не выбрали поле для удаления!", "Удаление конфигурации", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (MessageBox.Show("Вы действительно хотите удалить эту конфигурацию?", "Удаление конфигурации", MessageBoxButton.YesNo, MessageBoxImage.Warning, MessageBoxResult.No) == MessageBoxResult.Yes)
            {
                db.CarEquipment.Remove(carEquipment);
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

        private void update()
        {
            GridConfigurator.ItemsSource = db.CarEquipment.ToList();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            update();
        }
    }
}
