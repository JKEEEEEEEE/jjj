using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
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

namespace Technics
{
    /// <summary>
    /// Логика взаимодействия для Technica.xaml
    /// </summary>
    public partial class Technica : Window
    {

        public Technica()
        {
            InitializeComponent();
            FillComboBox();
        }

        private void tech_Loaded(object sender, RoutedEventArgs e)
        {
            Tech();
        }
		string connectionString = "Data Source=SHADOURAZE\\SQLEXPRESS;Initial Catalog=Technics;Integrated Security=True;";
		private void Tech()
        {
            if (tech != null)
            {
                Action action = () =>
                {
                    try
                    {
                        DB db = new DB();
                        db.sqlExecute(string.Format("select [Id_Technic], [Name_Technic], [Inventory_Number_Technic], [Serial_Number_Technic], [Date_Of_Entry_Technic], [Name_Model], [Name_Group], [Country_Manufacturer], [Price_Technic], [Address_Territory], [Name_Room], " +
                            "[Department_Room], [Status_Condition], [Repair_Date_Condition], [Status_Write_downs], [Date_Write_downs], [Surname_User]+' '+[Name_User]+' '+[Middle_Name_User] from [dbo].[Technik] inner join [Model] on [Model_Id] = [Id_Model] inner join [Groups] " +
                            "on [Group_Id] = [Id_Group] inner join [Manufacturer] on [Manufacturer_Id] = [Id_Manufacturer] inner join [Territory] on [Territory_Id] = [Id_Territory] inner join [Room] on [Room_Id] = [Id_Room] inner join [Condition] on [Condition_Id] = [Id_Condition] " +
                            "inner join [Write_downs] on [Write_downs_Id] = [Id_Write_downs] inner join [Users] on [Users_Id] = [Id_Users]"), DB.act.select);
                        db.dependency.OnChange += OnChange_tech;
                        tech.ItemsSource = db.resultTable.DefaultView;
                        tech.Columns[0].Visibility = Visibility.Hidden;
                        tech.Columns[1].Header = "Наименование продукта";
                        tech.Columns[2].Header = "Инвентарный номер";
                        tech.Columns[3].Header = "Серийный номер";
                        tech.Columns[4].Header = "Введён в эксплуатацию";
                        tech.Columns[5].Header = "Модель";
                        tech.Columns[6].Header = "Группа";
                        tech.Columns[7].Header = "Страна производителя";
                        tech.Columns[8].Header = "Стоимость";
                        tech.Columns[9].Header = "Территория";
                        tech.Columns[10].Header = "Номер помещения";
                        tech.Columns[11].Header = "Отдел";
                        tech.Columns[12].Header = "Состояние";
                        tech.Columns[13].Header = "Дата ремонта";
                        tech.Columns[14].Header = "Списание";
                        tech.Columns[15].Header = "Дата списания";
                        tech.Columns[16].Header = "Мат. Отв. лицо";
                    }
                    catch
                    {
                    }
                };
                Dispatcher.Invoke(action);
            }
        }

		private int GetSelectedIndexPlusOne(ComboBox comboBox)
		{
			int selectedIndex = comboBox.SelectedIndex;
			return selectedIndex != -1 ? selectedIndex + 1 : 0; // Возвращаем 0, если ничего не выбрано
		}
		private void ClearComboBoxItems(ComboBox comboBox)
		{
			comboBox.Items.Clear();
		}
		private void ClearAllTextBoxesAndComboBoxes()
		{
			Name_Technic.Clear();
			Inventory_Number_Technic.Clear();
			Serial_Number_Technic.Clear();
			Date_Of_Entry_Technic.Clear();
			Price_Technic.Clear();
			Repair_Date_Condition.Clear();
			Date_Write_downs.Clear();
			ClearComboBoxItems(Name_Model);
			ClearComboBoxItems(Name_Group);
			ClearComboBoxItems(Country_Manufacturer);
			ClearComboBoxItems(Address_Territory);
			ClearComboBoxItems(Name_Room);
			ClearComboBoxItems(Department_Room);
			ClearComboBoxItems(Status_Condition);
			ClearComboBoxItems(Status_Write_downs);
			ClearComboBoxItems(FIO);
		}

		private void OnChange_tech(object sender, SqlNotificationEventArgs e)
        {
            if (e.Info != SqlNotificationInfo.Invalid)
                Tech();
        }

		private void FillComboBox()
		{
			FillComboBoxFromDatabase("SELECT Id_Model, Name_Model FROM Model", Name_Model, "Name_Model");
			FillComboBoxFromDatabase("SELECT Id_Group, Name_Group FROM Groups", Name_Group, "Name_Group");
			FillComboBoxFromDatabase("SELECT Id_Manufacturer, Country_Manufacturer FROM Manufacturer", Country_Manufacturer, "Country_Manufacturer");
			FillComboBoxFromDatabase("SELECT Id_Territory, Address_Territory FROM Territory", Address_Territory, "Address_Territory");
			FillComboBoxFromDatabase("SELECT Id_Room, Name_Room FROM Room", Name_Room, "Name_Room");
			FillComboBoxFromDatabase("SELECT Id_Room, Department_Room FROM Room", Department_Room, "Department_Room");
			FillComboBoxFromDatabase("SELECT Id_Condition, Status_Condition FROM Condition", Status_Condition, "Status_Condition");
			FillComboBoxFromDatabase("SELECT Id_Write_downs, Status_Write_downs FROM Write_downs", Status_Write_downs, "Status_Write_downs");
			FillComboBoxFromDatabase("SELECT Id_Users, Surname_User, Name_User, Middle_Name_User FROM Users", FIO, "Surname_User", "Name_User", "Middle_Name_User");

		}

		private void FillComboBoxFromDatabase(string query, ComboBox comboBox, params string[] columns)
		{
			try
			{
				comboBox.Items.Clear(); // Очищаем элементы комбобокса перед заполнением новыми данными

				using (SqlConnection connection = new SqlConnection(connectionString))
				{
					connection.Open();

					using (SqlCommand command = new SqlCommand(query, connection))
					{
						using (SqlDataReader reader = command.ExecuteReader())
						{
							while (reader.Read())
							{
								StringBuilder itemText = new StringBuilder();
								foreach (string column in columns)
								{
									itemText.Append(reader[column].ToString());
									itemText.Append(" "); // Добавляем пробел между каждым столбцом
								}
								comboBox.Items.Add(itemText.ToString().Trim()); // Удаляем лишние пробелы в конце строки
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show($"Ошибка при заполнении ComboBox '{comboBox.Name}': {ex.Message}");
			}
		}




		private void tech_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (tech.Items.Count != 0 & tech.SelectedItems.Count != 0)
			{
				DataRowView dataRow = (DataRowView)tech.SelectedItems[0];
				Name_Technic.Text = dataRow[1].ToString();
				Inventory_Number_Technic.Text = dataRow[2].ToString();
				Serial_Number_Technic.Text = dataRow[3].ToString();
				Date_Of_Entry_Technic.Text = dataRow[4].ToString();
				Price_Technic.Text = dataRow[8].ToString();
				Repair_Date_Condition.Text = dataRow[13].ToString();
				Date_Write_downs.Text = dataRow[15].ToString();

				// Вызываем методы для заполнения комбобоксов
				FillComboBoxFromDatabase("SELECT Id_Model, Name_Model FROM Model", Name_Model, dataRow[5].ToString());
				FillComboBoxFromDatabase("SELECT Id_Group, Name_Group FROM Groups", Name_Group, dataRow[6].ToString());
				FillComboBoxFromDatabase("SELECT Id_Manufacturer, Country_Manufacturer FROM Manufacturer", Country_Manufacturer, dataRow[7].ToString());
				FillComboBoxFromDatabase("SELECT Id_Territory, Address_Territory FROM Territory", Address_Territory, dataRow[9].ToString());
				FillComboBoxFromDatabase("SELECT Id_Room, Name_Room FROM Room", Name_Room, dataRow[10].ToString());
				FillComboBoxFromDatabase("SELECT Id_Room, Department_Room FROM Room", Department_Room, dataRow[11].ToString());
				FillComboBoxFromDatabase("SELECT Id_Condition, Status_Condition FROM Condition", Status_Condition, dataRow[12].ToString());
				FillComboBoxFromDatabase("SELECT Id_Write_downs, Status_Write_downs FROM Write_downs", Status_Write_downs, dataRow[14].ToString());
				FillComboBoxFromDatabaseForUsers(FIO, dataRow[16].ToString());
			}
		}

		private void FillComboBoxFromDatabase(string query, ComboBox comboBox, string selectedItem)
		{
			try
			{
				comboBox.Items.Clear();
				using (SqlConnection connection = new SqlConnection(connectionString))
				{
					connection.Open();

					using (SqlCommand command = new SqlCommand(query, connection))
					{
						using (SqlDataReader reader = command.ExecuteReader())
						{
							while (reader.Read())
							{
								comboBox.Items.Add(new ComboBoxItem { Content = reader[1].ToString(), Tag = reader[0].ToString() });
							}
						}
					}
				}

				// Выбираем соответствующий элемент в комбобоксе
				if (!string.IsNullOrEmpty(selectedItem))
				{
					foreach (ComboBoxItem item in comboBox.Items)
					{
						if (item.Content.ToString() == selectedItem)
						{
							comboBox.SelectedItem = item;
							break;
						}
					}
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show($"Ошибка при заполнении ComboBox '{comboBox.Name}': {ex.Message}");
			}
		}



		private void FillComboBoxFromDatabaseForUsers(ComboBox comboBox, string selectedUser)
		{
			try
			{
				comboBox.Items.Clear();
				string query = "SELECT Id_Users, Surname_User, Name_User, Middle_Name_User FROM Users";

				using (SqlConnection connection = new SqlConnection(connectionString))
				{
					connection.Open();

					using (SqlCommand command = new SqlCommand(query, connection))
					{
						using (SqlDataReader reader = command.ExecuteReader())
						{
							while (reader.Read())
							{
								string fullName = $"{reader["Surname_User"]} {reader["Name_User"]} {reader["Middle_Name_User"]}";
								comboBox.Items.Add(new ComboBoxItem { Content = fullName, Tag = reader["Id_Users"].ToString() });
							}
						}
					}
				}

				// Выбираем соответствующего пользователя в комбобоксе
				if (!string.IsNullOrEmpty(selectedUser))
				{
					foreach (ComboBoxItem item in comboBox.Items)
					{
						if (item.Content.ToString() == selectedUser)
						{
							comboBox.SelectedItem = item;
							break;
						}
					}
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show($"Ошибка при заполнении ComboBox '{comboBox.Name}': {ex.Message}");
			}
		}



		private void Button_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				int qq = GetSelectedIndexPlusOne(Name_Model);
				int ww = GetSelectedIndexPlusOne(Name_Group);
				int ee = GetSelectedIndexPlusOne(Country_Manufacturer);
				int rr = GetSelectedIndexPlusOne(Address_Territory);
				int tt = GetSelectedIndexPlusOne(Name_Room);
				int yy = GetSelectedIndexPlusOne(Department_Room);
				int uu = GetSelectedIndexPlusOne(Status_Condition);
				int ii = GetSelectedIndexPlusOne(Status_Write_downs);
				int oo = GetSelectedIndexPlusOne(FIO);

				DB db = new DB();
				db.sqlExecute(string.Format("insert into [dbo].[Technik]( [Name_Technic], [Inventory_Number_Technic], [Serial_Number_Technic], [Date_Of_Entry_Technic], [Price_Technic], [Repair_Date_Condition], [Date_Write_downs], [Model_Id], [Group_Id], [Manufacturer_Id], [Territory_Id], [Room_Id], [Condition_Id], [Write_downs_Id], [Users_Id])" +
					"values ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', '{10}', '{11}', '{12}', '{13}', '{14}')",
					Name_Technic.Text, Inventory_Number_Technic.Text, Serial_Number_Technic.Text, Date_Of_Entry_Technic.Text, Price_Technic.Text, Repair_Date_Condition.Text, Date_Write_downs.Text, qq, ww, ee, rr, tt, yy, uu, ii, oo), DB.act.manipulation);
				ClearAllTextBoxesAndComboBoxes();
				Tech();
				FillComboBox();
			}
			catch (Exception ex)
			{
				MessageBox.Show("Ошибка данных! " + ex.Message);
			}
		}

		private void Button_Click_1(object sender, RoutedEventArgs e)
		{
			try
			{
				if (tech.Items.Count != 0 & tech.SelectedItems.Count != 0)
				{
					int qq = GetSelectedIndexPlusOne(Name_Model);
					int ww = GetSelectedIndexPlusOne(Name_Group);
					int ee = GetSelectedIndexPlusOne(Country_Manufacturer);
					int rr = GetSelectedIndexPlusOne(Address_Territory);
					int tt = GetSelectedIndexPlusOne(Name_Room);
					int yy = GetSelectedIndexPlusOne(Department_Room);
					int uu = GetSelectedIndexPlusOne(Status_Condition);
					int ii = GetSelectedIndexPlusOne(Status_Write_downs);
					int oo = GetSelectedIndexPlusOne(FIO);
					DataRowView dataRowView = (DataRowView)tech.SelectedItems[0];
					DB db = new DB();
					db.sqlExecute(string.Format("update [dbo].[Technik] set " +
						"[Name_Technic] = '{0}'," +
						"[Inventory_Number_Technic] = '{1}'," +
						"[Serial_Number_Technic] = '{2}'," +
						"[Date_Of_Entry_Technic] = '{3}'," +
						"[Price_Technic] = '{4}', " +
						"[Repair_Date_Condition] = '{5}'," +
						"[Date_Write_downs] = '{6}'," +
						"[Model_Id] = '{7}'," +
						"[Group_Id] = '{8}'," +
						"[Manufacturer_Id] = '{9}'," +
						"[Territory_Id] = '{10}'," +
						"[Room_Id] = '{11}'," +
						"[Condition_Id] = '{12}'," +
						"[Write_downs_Id] = '{13}'," +
						"[Users_Id] = '{14}'" +
						"where [ID_Technic] = '{15}'",
					Name_Technic.Text, Inventory_Number_Technic.Text, Serial_Number_Technic.Text, Date_Of_Entry_Technic.Text, Price_Technic.Text, Repair_Date_Condition.Text, Date_Write_downs.Text, qq, ww, ee, rr, tt, yy, uu, ii, oo, dataRowView[0]), DB.act.manipulation);
				}
				ClearAllTextBoxesAndComboBoxes();
				Tech();
				FillComboBox();
			}
			catch (Exception ex)
			{
				MessageBox.Show("Ошибка данных! " + ex.Message);
			}
		}

		private void Button_Click_2(object sender, RoutedEventArgs e)
		{
			try
			{
				switch (MessageBox.Show("Удалить выбранную запись?", DB.App_Name, MessageBoxButton.YesNo, MessageBoxImage.Question))
				{
					case MessageBoxResult.Yes:
						if (tech.Items.Count != 0 & tech.SelectedItems.Count != 0)
						{
							DataRowView dataRowView = (DataRowView)tech.SelectedItems[0];
							DB db = new DB();
							db.sqlExecute(string.Format("delete from [dbo].[Technik] where [Id_Technic] = {0}", dataRowView[0]), DB.act.manipulation);
						}
						break;
				}
				ClearAllTextBoxesAndComboBoxes();
				Tech();
				FillComboBox();
			}
			catch
			{
				MessageBox.Show("Ошибка данных!");
			}
		}


		private void Button_Click_3(object sender, RoutedEventArgs e)
		{
			Tech tech = new Tech();
			tech.Show();
			Close();
		}
	}
}
