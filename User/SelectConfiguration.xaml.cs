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
    /// Логика взаимодействия для SelectConfiguration.xaml
    /// </summary>
    public partial class SelectConfiguration : Window
    {
        DataBaseSerebryannikov1 db = new DataBaseSerebryannikov1();
        public CarEquipment EQUIPMENT;
        public SelectConfiguration(Car car)
        {
            InitializeComponent();
            List<CarEquipment> carEquipmentList = db.CarEquipment.Where(x => x.id_Car == car.id).ToList();
            GridEquipment.ItemsSource = null;
            GridEquipment.ItemsSource = carEquipmentList;
        }

        private void GridSelectEquipment_PreviewMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            EQUIPMENT = GridEquipment.SelectedValue as CarEquipment;
            this.Close();
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            EQUIPMENT = null;
            this.Close();
        }
    }
}
