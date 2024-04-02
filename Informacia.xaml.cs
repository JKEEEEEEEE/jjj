using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
using static System.Net.Mime.MediaTypeNames;

namespace Technics
{
    /// <summary>
    /// Логика взаимодействия для Informacia.xaml
    /// </summary>
    public partial class Informacia : Window
    {
        private string Id;
        private string Name;
        private string Inventory_Number;
        private string Serial_Number;
        private string Date_Of_Entry;
        private string FIO;
        private string Territory;
        private string Room;
        private decimal Price;
        private string Write_downs;
        private string Condition;

        public Informacia(string Id, string Name, string Inventory_Number, string Serial_Number, string Date_Of_Entry, string FIO, string Territory, string Room, decimal Price, string Write_downs, string condition)
        {
            this.Id = Id;
            this.Name = Name;
            this.Inventory_Number = Inventory_Number;
            this.Serial_Number = Serial_Number;
            this.Date_Of_Entry = Date_Of_Entry;
            this.FIO = FIO;
            this.Territory = Territory;
            this.Room = Room;
            this.Price = Price;
            this.Write_downs = Write_downs;
            this.Condition = condition;

            InitializeComponent();
            Inf();
        }

        private void Inf()
        {
            Name_Tech.Text = Name;
            Inv.Text = Inventory_Number;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Tech tech = new Tech();
            tech.Show();
            Close();
        }
    }
}
