using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;
using System.Globalization;
using System.Windows.Media;
using System.Windows.Input;
using System.Xml.XPath;
using System.Net;

namespace Technics
{
    /// <summary>
    /// Логика взаимодействия для Polzovatel.xaml
    /// </summary>
    public partial class Tech : Window
    {
        public Tech()
        {
            InitializeComponent();
            FillComboBox();
            vyv();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Authorization authorization = new Authorization();
            authorization.Show();
            Close();
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            Administrator administrator = new Administrator();
            administrator.Show();
            Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Technica technica = new Technica();
            technica.Show();
            Close();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            if (poisk.Text == "")
            {
                vyv();
            }
            else
            {
                Technic[] technics = ExecuteSql("select Id_Technic, Name_Technic, Inventory_Number_Technic, Serial_Number_Technic, Date_Of_Entry_Technic, Surname_User, Name_User, Middle_Name_User, Address_Territory, Name_Room, Price_Technic, Status_Write_downs, Status_Condition from Technik inner join Users on Users_Id = Id_Users inner join Territory on Territory_Id = Id_Territory inner join Room on Room_Id = Id_Room inner join Write_downs on Write_downs_Id = Id_Write_downs inner join Condition on Condition_Id = Id_Condition WHERE Inventory_Number_Technic LIKE " + "'" + poisk.Text + "%'").ToArray();
                listviewTechnics.ItemsSource = technics;
            }
        }

        private void FillComboBox()
        {
            try
            {
                string connectionString = "Data Source=LAPTOP-MPQU2K2E\\DB;Initial Catalog=Technics;Integrated Security=True";

                string query = "SELECT DISTINCT Address_Territory FROM Territory";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                sort_ter.Items.Add(reader["Address_Territory"].ToString());
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при заполнении ComboBox: " + ex.Message);
            }
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            if (doo.Text == "" || ott.Text == "")
            {
                MessageBox.Show("Введите второй номер года");
                vyv();
            }
            else
            {
                Technic[] technics = ExecuteSql("select Id_Technic, Name_Technic, Inventory_Number_Technic, Serial_Number_Technic, Date_Of_Entry_Technic, Surname_User, Name_User, Middle_Name_User, Address_Territory, Name_Room, Price_Technic, Status_Write_downs, Status_Condition from Technik inner join Users on Users_Id = Id_Users inner join Territory on Territory_Id = Id_Territory inner join Room on Room_Id = Id_Room inner join Write_downs on Write_downs_Id = Id_Write_downs inner join Condition on Condition_Id = Id_Condition WHERE Date_Of_Entry_Technic BETWEEN" + "'" + ott.Text + "'" + "AND" + "'" + doo.Text + "'").ToArray();
                listviewTechnics.ItemsSource = technics;
            }
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sort.SelectedIndex == 0)
            {
                Technic[] technics = ExecuteSql("select Id_Technic, Name_Technic, Inventory_Number_Technic, Serial_Number_Technic, Date_Of_Entry_Technic, Surname_User, Name_User, Middle_Name_User, Address_Territory, Name_Room, Price_Technic, Status_Write_downs, Status_Condition from Technik inner join Users on Users_Id = Id_Users inner join Territory on Territory_Id = Id_Territory inner join Room on Room_Id = Id_Room inner join Write_downs on Write_downs_Id = Id_Write_downs inner join Condition on Condition_Id = Id_Condition ORDER BY Status_Condition DESC;").ToArray();
                listviewTechnics.ItemsSource = technics;
            }
            if (sort.SelectedIndex == 1)
            {
                Technic[] technics = ExecuteSql("select Id_Technic, Name_Technic, Inventory_Number_Technic, Serial_Number_Technic, Date_Of_Entry_Technic, Surname_User, Name_User, Middle_Name_User, Address_Territory, Name_Room, Price_Technic, Status_Write_downs, Status_Condition from Technik inner join Users on Users_Id = Id_Users inner join Territory on Territory_Id = Id_Territory inner join Room on Room_Id = Id_Room inner join Write_downs on Write_downs_Id = Id_Write_downs inner join Condition on Condition_Id = Id_Condition ORDER BY Status_Write_downs DESC;").ToArray();
                listviewTechnics.ItemsSource = technics;
            }
            if (sort.SelectedIndex == 2)
            {
                Technic[] technics = ExecuteSql("select Id_Technic, Name_Technic, Inventory_Number_Technic, Serial_Number_Technic, Date_Of_Entry_Technic, Surname_User, Name_User, Middle_Name_User, Address_Territory, Name_Room, Price_Technic, Status_Write_downs, Status_Condition from Technik inner join Users on Users_Id = Id_Users inner join Territory on Territory_Id = Id_Territory inner join Room on Room_Id = Id_Room inner join Write_downs on Write_downs_Id = Id_Write_downs inner join Condition on Condition_Id = Id_Condition ORDER BY Date_Of_Entry_Technic ASC;").ToArray();
                listviewTechnics.ItemsSource = technics;
            }
            if (sort.SelectedIndex == 3)
            {
                Technic[] technics = ExecuteSql("select Id_Technic, Name_Technic, Inventory_Number_Technic, Serial_Number_Technic, Date_Of_Entry_Technic, Surname_User, Name_User, Middle_Name_User, Address_Territory, Name_Room, Price_Technic, Status_Write_downs, Status_Condition from Technik inner join Users on Users_Id = Id_Users inner join Territory on Territory_Id = Id_Territory inner join Room on Room_Id = Id_Room inner join Write_downs on Write_downs_Id = Id_Write_downs inner join Condition on Condition_Id = Id_Condition ORDER BY Date_Of_Entry_Technic DESC;").ToArray();
                listviewTechnics.ItemsSource = technics;
            }
            if (sort.SelectedIndex == 4)
            {
                Technic[] technics = ExecuteSql("select Id_Technic, Name_Technic, Inventory_Number_Technic, Serial_Number_Technic, Date_Of_Entry_Technic, Surname_User, Name_User, Middle_Name_User, Address_Territory, Name_Room, Price_Technic, Status_Write_downs, Status_Condition from Technik inner join Users on Users_Id = Id_Users inner join Territory on Territory_Id = Id_Territory inner join Room on Room_Id = Id_Room inner join Write_downs on Write_downs_Id = Id_Write_downs inner join Condition on Condition_Id = Id_Condition").ToArray();
                listviewTechnics.ItemsSource = technics;
            }
        }

        static IEnumerable<Technic> ExecuteSql(string sql)
        {
            SqlConnection conn = new SqlConnection("Data Source=SHADOURAZE\\SQLEXPRESS;Initial Catalog=Technics;Integrated Security=True;");
            using (conn)
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader read = cmd.ExecuteReader();

                using (read)
                {
                    int i = 0;
                    while (true)
                    {
                        if (read.Read() == false) break;
                        i++;
                        var stringDate = read["Date_Of_Entry_Technic"].ToString();

                        Technic technics = new Technic()
                        {
                            Id_Technic = (int)read["Id_Technic"],
                            Name = (string)read["Name_Technic"],
                            Inventory_Number = (string)read["Inventory_Number_Technic"],
                            Serial_Number = (string)read["Serial_Number_Technic"],
                            Date_Of_Entry = stringDate,
                            FIO = (string)read["Surname_User"] + " " + (string)read["Name_User"] + " " + (string)read["Middle_Name_User"],
                            Territory = (string)read["Address_Territory"],
                            Room = (string)read["Name_Room"],
                            Price = (decimal)read["Price_Technic"],
                            Write_downs = (string)read["Status_Write_downs"],
                            Condition = (string)read["Status_Condition"]
                        };
                        yield return technics;
                    }
                }
            }
        }

        //вывод полной таблицы
        private void vyv()
        {
            Technic[] technics = ExecuteSql("select Id_Technic, Name_Technic, Inventory_Number_Technic, Serial_Number_Technic, Date_Of_Entry_Technic, Surname_User, Name_User, Middle_Name_User, Address_Territory, Name_Room, Price_Technic, Status_Write_downs, Status_Condition from Technik inner join Users on Users_Id = Id_Users inner join Territory on Territory_Id = Id_Territory inner join Room on Room_Id = Id_Room inner join Write_downs on Write_downs_Id = Id_Write_downs inner join Condition on Condition_Id = Id_Condition").ToArray();
            listviewTechnics.ItemsSource = technics;
        }

        public class Technic
        {
            public int Id_Technic { get; set; }
            public string Name { get; set; }
            public string Inventory_Number { get; set; }
            public string Serial_Number { get; set; }
            public string Date_Of_Entry { get; set; }
            public string FIO { get; set; }
            public string Territory { get; set; }
            public string Room { get; set; }
            public decimal Price { get; set; }
            public string Write_downs { get; set; }
            public string Condition { get; set; }
        }

        private void ListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            // Получаем выбранный элемент
            var selectedItem = (Technic)listviewTechnics.SelectedItem;

            if (selectedItem != null)
            {
                // Получаем значение id и другие данные из элемента
                string Id = (selectedItem.Id_Technic).ToString();
                string Name = (selectedItem.Name);
                string Inventory_Number = (selectedItem.Inventory_Number);
                string Serial_Number = (selectedItem.Serial_Number);
                string Date_Of_Entry = (selectedItem.Date_Of_Entry);
                string FIO = (selectedItem.FIO);
                string Territory = (selectedItem.Territory);
                string Room = (selectedItem.Room);
                decimal Price = (selectedItem.Price);
                string Write_downs = (selectedItem.Write_downs);
                string Condition = (selectedItem.Condition);

                Informacia informacia = new Informacia(Id, Name, Inventory_Number, Serial_Number, Date_Of_Entry, FIO, Territory, Room, Price, Write_downs, Condition);
                informacia.Show();
                Close();
            }
        }

        private void sort_ter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Technic[] technics = ExecuteSql("select Id_Technic, Name_Technic, Inventory_Number_Technic, Serial_Number_Technic, Date_Of_Entry_Technic, Surname_User, Name_User, Middle_Name_User, Address_Territory, Name_Room, Price_Technic, Status_Write_downs, Status_Condition from Technik inner join Users on Users_Id = Id_Users inner join Territory on Territory_Id = Id_Territory inner join Room on Room_Id = Id_Room inner join Write_downs on Write_downs_Id = Id_Write_downs inner join Condition on Condition_Id = Id_Condition WHERE Address_Territory LIKE " + "'" + sort_ter.SelectedValue + "%'").ToArray();
            listviewTechnics.ItemsSource = technics;
        }

        private void pom_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (pom.Text == "")
            {
                vyv();
            }
            else
            {
                Technic[] technics = ExecuteSql("select Id_Technic, Name_Technic, Inventory_Number_Technic, Serial_Number_Technic, Date_Of_Entry_Technic, Surname_User, Name_User, Middle_Name_User, Address_Territory, Name_Room, Price_Technic, Status_Write_downs, Status_Condition from Technik inner join Users on Users_Id = Id_Users inner join Territory on Territory_Id = Id_Territory inner join Room on Room_Id = Id_Room inner join Write_downs on Write_downs_Id = Id_Write_downs inner join Condition on Condition_Id = Id_Condition WHERE Name_Room LIKE " + "'" + pom.Text + "%'").ToArray();
                listviewTechnics.ItemsSource = technics;
            }
        }

    }
}
