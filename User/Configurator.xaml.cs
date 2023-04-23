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
    /// Логика взаимодействия для Configurator.xaml
    /// </summary>
    public partial class Configurator : Window
    {
        DataBaseSerebryannikov1 db = new DataBaseSerebryannikov1();
        SelectCar selectCar = new SelectCar();
        RequestToDealer requestToDealer = new RequestToDealer();
        Car c;
        List<CarEquipment> ListForSelectEquipment = new List<CarEquipment>();
        public int TotalPrice;
        public int WorkPrice;

        public Configurator(Car car)
        {
            InitializeComponent();
            this.c = car;
            TotalPrice = WorkPrice;
        }

        private void BtnSelectConfiguration_Click(object sender, RoutedEventArgs e)
        {
            SelectConfiguration selectConfiguration = new SelectConfiguration(c);
            selectConfiguration.ShowDialog();
            this.Hide();
            this.Show();
            if (selectConfiguration.EQUIPMENT != null)
            {
                bool isElementExist = false;
                bool isTypeElementExist = false;
                for (int i = 0; i < ListForSelectEquipment.Count; i++)
                {
                    if (selectConfiguration.EQUIPMENT.id == ListForSelectEquipment[i].id)
                        isElementExist = true;
                    if (selectConfiguration.EQUIPMENT.id_TypeEquipment == ListForSelectEquipment[i].id_TypeEquipment)
                        isTypeElementExist = true;
                }
                if (isElementExist == true) return;
                if (isTypeElementExist == true) return;
                ListForSelectEquipment.Add(selectConfiguration.EQUIPMENT);
                GridSelectEquipment.ItemsSource = null;
                GridSelectEquipment.ItemsSource = ListForSelectEquipment;
                WorkPrice = Convert.ToInt32(txtSumPrice.Text);
                int EquipmentPrice;
                foreach (var k in ListForSelectEquipment)
                {
                    EquipmentPrice = Convert.ToInt32(k.price);
                    TotalPrice = WorkPrice + EquipmentPrice;
                }
                txtSumPrice.Text = Convert.ToString(TotalPrice.ToString());
            }            
        }

        private void BtnDeleteConfiguration_Click(object sender, RoutedEventArgs e)
        {            
            CarEquipment carEquipment = new CarEquipment();
            CarEquipment EQUIPMENT = GridSelectEquipment.SelectedValue as CarEquipment;
            if (EQUIPMENT == null)
            {
                MessageBox.Show("Ошибка! Вы не выбрали поле для удаления!", "Удаление выбранной конфигурации", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (MessageBox.Show("Вы действительно хотите удалить эту конфигурацию из списка выбранных?", "Удаление выбранной конфигурации", MessageBoxButton.YesNo, MessageBoxImage.Warning, MessageBoxResult.No) == MessageBoxResult.Yes)
            {
                WorkPrice = Convert.ToInt32(txtSumPrice.Text);
                int EquipmentPrice;
                EquipmentPrice = Convert.ToInt32(EQUIPMENT.price);
                TotalPrice = WorkPrice - EquipmentPrice;
                txtSumPrice.Text = Convert.ToString(TotalPrice.ToString());
                ListForSelectEquipment.Remove(EQUIPMENT);
                GridSelectEquipment.ItemsSource = null;
                GridSelectEquipment.ItemsSource = ListForSelectEquipment;
            }
            else
            {
                return;
            }
        }

        private void BtnRequestToDealer_Click(object sender, RoutedEventArgs e)
        {
            if (ListForSelectEquipment.Count == 0)
            {
                if (MessageBox.Show("Вы хотите добавить конфигурацию?", "Конфигуратор", MessageBoxButton.YesNo, MessageBoxImage.Warning, MessageBoxResult.No) == MessageBoxResult.No)
                {
                    requestToDealer.txtCarName.Text = c.name.ToString();
                    UpdateImage(c);
                    requestToDealer.txtTotalPrice.Text = Convert.ToString(c.price.ToString());
                    requestToDealer.txtAllConfiguration.Text = "Конфигурации не выбраны!";
                }
                else
                {
                    return;
                }
            }
            else
            {
                requestToDealer.txtCarName.Text = c.name.ToString();
                UpdateImage(c);
                requestToDealer.txtTotalPrice.Text = Convert.ToString(TotalPrice);
                var AllConfiguration = new System.Text.StringBuilder();
                for (int i = 0; i < ListForSelectEquipment.Count; i++) 
                {
                    AllConfiguration.AppendLine(ListForSelectEquipment[i].nameEquipment.ToString());
                }
                requestToDealer.txtAllConfiguration.Text = AllConfiguration.ToString();
            }
            requestToDealer.Show();
            this.Close();
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            selectCar.Show();
            this.Close();
        }

        private void UpdateImage(Car car)
        {
            requestToDealer.imgCar.Source = new BitmapImage(new Uri("Resources/ImageNotFound.png", UriKind.Relative));
            if (c != null)
            {
                if (c.photo != null)
                    requestToDealer.imgCar.Source = ImagingWork.ToBitmapImage(c.photo);
            }
        }
    }
}
