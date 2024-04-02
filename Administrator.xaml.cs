using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
using System.Windows.Threading;

namespace Technics
{
    /// <summary>
    /// Логика взаимодействия для Adminisctrator.xaml
    /// </summary>
    public partial class Administrator : Window
    {
        public Administrator()
        {
            InitializeComponent();
            FillComboBox();
        }

        //Роли
        private void TabItem_Loaded(object sender, RoutedEventArgs e)
        {
            Role();
        }

        private void role_Loaded(object sender, RoutedEventArgs e)
        {
            Role();
        }

        private void Role()
        {
            if (role != null)
            {
                Action action = () =>
                {
                    try
                    {
                        DB db = new DB();
                        db.sqlExecute(string.Format("select [ID_Users], [Surname_User], [Name_User], [Middle_Name_User], [Email_User], [Login_User], [Password_User], [Role_ID], [ID_Role], [Name_Role] from [dbo].[Users] inner join [dbo].[Role] on [Role_ID]=[ID_Role]"), DB.act.select);
                        db.dependency.OnChange += OnChange_role;
                        role.ItemsSource = db.resultTable.DefaultView;
                        role.Columns[0].Visibility = Visibility.Hidden;
                        role.Columns[1].Header = "Фамилия";
                        role.Columns[2].Header = "Имя";
                        role.Columns[3].Header = "Отчество";
                        role.Columns[4].Visibility = Visibility.Hidden;
                        role.Columns[5].Header = "Логин";
                        role.Columns[6].Visibility = Visibility.Hidden;
                        role.Columns[7].Visibility = Visibility.Hidden;
                        role.Columns[8].Visibility = Visibility.Hidden;
                        role.Columns[9].Header = "Название роли";
                    }
                    catch
                    {
                    }
                };
                Dispatcher.Invoke(action);
            }
        }

        private void OnChange_role(object sender, SqlNotificationEventArgs e)
        {
            if (e.Info != SqlNotificationInfo.Invalid)
                Role();
        }

        private void FillComboBox()
        {
            try
            {
                string connectionString = "Data Source=LAPTOP-MPQU2K2E\\DB;Initial Catalog=Technics;Integrated Security=True";

                string query = "SELECT Name_Role FROM Role";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Name_Role.Items.Add(reader["Name_Role"].ToString());
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

        private void role_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (role.Items.Count != 0 & role.SelectedItems.Count != 0)
            {
                DataRowView dataRow = (DataRowView)role.SelectedItems[0];
                Name_Role.SelectedItem = dataRow[9];
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (role.Items.Count != 0 & role.SelectedItems.Count != 0)
                {
                    int qq = Name_Role.SelectedIndex + 1;
                    DataRowView dataRowView = (DataRowView)role.SelectedItems[0];
                    DB db = new DB();
                    db.sqlExecute(string.Format("update [dbo].[Users] set [Role_ID] = '{0}' where [ID_Users] = '{1}'", qq, dataRowView[0]), DB.act.manipulation);
                    Name_Role.Items.Clear();
                }
            }
            catch
            {
                MessageBox.Show("Ошибка данных!");
            }
        }

        private void Button_Click_26(object sender, RoutedEventArgs e)
        {
            Tech tech = new Tech();
            tech.Show();
            Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            Close();
        }

        //crud действия
        private void TabItem_Loaded_1(object sender, RoutedEventArgs e)
        {
            Crud();
        }

        private void TabItem_Loaded_2(object sender, RoutedEventArgs e)
        {
            Crud();
        }

        //Территории
        private void crud_Loaded(object sender, RoutedEventArgs e)
        {
            Crud();
        }

        private void Crud()
        {
            if (crud != null)
            {
                Action action = () =>
                {
                    try
                    {
                        DB db = new DB();
                        db.sqlExecute(string.Format("select [ID_Territory], [Address_Territory] from [dbo].[Territory]"), DB.act.select);
                        db.dependency.OnChange += OnChange_crud;
                        crud.ItemsSource = db.resultTable.DefaultView;
                        crud.Columns[0].Visibility = Visibility.Hidden;
                        crud.Columns[1].Header = "Адрес";
                    }
                    catch
                    { }
                };
                Dispatcher.Invoke(action);
            }
        }

        private void OnChange_crud(object sender, SqlNotificationEventArgs e)
        {
            if (e.Info != SqlNotificationInfo.Invalid)
                Crud();
        }

        private void crud_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            {
                if (crud.Items.Count != 0 & crud.SelectedItems.Count != 0)
                {
                    DataRowView dataRow = (DataRowView)crud.SelectedItems[0];
                    Address_Territory.Text = dataRow[1].ToString();
                }
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            try
            {
                DB db = new DB();
                db.sqlExecute(string.Format("insert into [dbo].[Territory]([Address_Territory])" +
                    " values ('{0}')", Address_Territory.Text), DB.act.manipulation);
                Address_Territory.Clear();
            }
            catch
            {
                MessageBox.Show("Ошибка данных!");
            }
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            try
            {
                if (crud.Items.Count != 0 & crud.SelectedItems.Count != 0)
                {
                    DataRowView dataRowView = (DataRowView)crud.SelectedItems[0];
                    DB db = new DB();
                    db.sqlExecute(string.Format("update [dbo].[Territory] set " +
                        "[Address_Territory] = '{0}' where [ID_Territory] = '{1}'", Address_Territory.Text,
                        dataRowView[0]), DB.act.manipulation);
                }
                Address_Territory.Clear();
            }
            catch
            {
                MessageBox.Show("Ошибка данных!");
            }
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            try
            {
                switch (MessageBox.Show("Удалить выбранную запись?", DB.App_Name, MessageBoxButton.YesNo, MessageBoxImage.Question))
                {
                    case MessageBoxResult.Yes:
                        if (crud.Items.Count != 0 & crud.SelectedItems.Count != 0)
                        {
                            DataRowView dataRowView = (DataRowView)crud.SelectedItems[0];
                            DB db = new DB();
                            db.sqlExecute(string.Format("delete from [dbo].[Territory] where [ID_Territory] = {0}", dataRowView[0]), DB.act.manipulation);
                        }
                        break;
                }
                Address_Territory.Clear();
            }
            catch
            {
                MessageBox.Show("Ошибка данных!");
            }
        }

        //Помещения
        private void crud_Loaded_2(object sender, RoutedEventArgs e)
        {
            Crud_2();
        }

        private void Crud_2()
        {
            if (crud_2 != null)
            {
                Action action = () =>
                {
                    try
                    {
                        DB db = new DB();
                        db.sqlExecute(string.Format("select [ID_Room], [Name_Room], [Department_Room] from [dbo].[Room]"), DB.act.select);
                        db.dependency.OnChange += OnChange_crud_2;
                        crud_2.ItemsSource = db.resultTable.DefaultView;
                        crud_2.Columns[0].Visibility = Visibility.Hidden;
                        crud_2.Columns[1].Header = "Номер";
                        crud_2.Columns[2].Header = "Отдел";
                    }
                    catch
                    { }
                };
                Dispatcher.Invoke(action);
            }
        }

        private void OnChange_crud_2(object sender, SqlNotificationEventArgs e)
        {
            if (e.Info != SqlNotificationInfo.Invalid)
                Crud_2();
        }

        private void crud_SelectionChanged_2(object sender, SelectionChangedEventArgs e)
        {
            {
                if (crud_2.Items.Count != 0 & crud_2.SelectedItems.Count != 0)
                {
                    DataRowView dataRow = (DataRowView)crud_2.SelectedItems[0];
                    Name_Room.Text = dataRow[1].ToString();
                    Department_Room.Text = dataRow[2].ToString();
                }
            }
        }

        private void Button_Click_8(object sender, RoutedEventArgs e)
        {
            try
            {
                DB db = new DB();
                db.sqlExecute(string.Format("insert into [dbo].[Room]([Name_Room], [Department_Room])" +
                    " values ('{0}', '{1}')", Name_Room.Text, Department_Room.Text), DB.act.manipulation);
                Name_Room.Clear(); Department_Room.Clear();
            }
            catch
            {
                MessageBox.Show("Ошибка данных!");
            }
        }

        private void Button_Click_9(object sender, RoutedEventArgs e)
        {
            try
            {
                if (crud_2.Items.Count != 0 & crud_2.SelectedItems.Count != 0)
                {
                    DataRowView dataRowView = (DataRowView)crud_2.SelectedItems[0];
                    DB db = new DB();
                    db.sqlExecute(string.Format("update [dbo].[Room] set " +
                        "[Name_Room] = '{0}', [Department_Room] = '{1}' where [ID_Room] = '{2}'", Name_Room.Text, Department_Room.Text,
                        dataRowView[0]), DB.act.manipulation);
                }
                Name_Room.Clear(); Department_Room.Clear();
            }
            catch
            {
                MessageBox.Show("Ошибка данных!");
            }
        }

        private void Button_Click_10(object sender, RoutedEventArgs e)
        {
            try
            {
                switch (MessageBox.Show("Удалить выбранную запись?", DB.App_Name, MessageBoxButton.YesNo, MessageBoxImage.Question))
                {
                    case MessageBoxResult.Yes:
                        if (crud_2.Items.Count != 0 & crud_2.SelectedItems.Count != 0)
                        {
                            DataRowView dataRowView = (DataRowView)crud_2.SelectedItems[0];
                            DB db = new DB();
                            db.sqlExecute(string.Format("delete from [dbo].[Room] where [ID_Room] = {0}", dataRowView[0]), DB.act.manipulation);
                        }
                        break;
                }
                Name_Room.Clear(); Department_Room.Clear();
            }
            catch
            {
                MessageBox.Show("Ошибка данных!");
            }
        }

        private void TabItem_Loaded_3(object sender, RoutedEventArgs e)
        {
            Crud();
        }

        //Производители
        private void crud_Loaded_1(object sender, RoutedEventArgs e)
        {
            Crud_1();
        }

        private void Crud_1()
        {
            if (crud_1 != null)
            {
                Action action = () =>
                {
                    try
                    {
                        DB db = new DB();
                        db.sqlExecute(string.Format("select [ID_Manufacturer], [Country_Manufacturer] from [dbo].[Manufacturer]"), DB.act.select);
                        db.dependency.OnChange += OnChange_crud_1;
                        crud_1.ItemsSource = db.resultTable.DefaultView;
                        crud_1.Columns[0].Visibility = Visibility.Hidden;
                        crud_1.Columns[1].Header = "Страна";
                    }
                    catch
                    { }
                };
                Dispatcher.Invoke(action);
            }
        }

        private void OnChange_crud_1(object sender, SqlNotificationEventArgs e)
        {
            if (e.Info != SqlNotificationInfo.Invalid)
                Crud_1();
        }

        private void crud_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            {
                if (crud_1.Items.Count != 0 & crud_1.SelectedItems.Count != 0)
                {
                    DataRowView dataRow = (DataRowView)crud_1.SelectedItems[0];
                    Country_Manufacturer.Text = dataRow[1].ToString();
                }
            }
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            try
            {
                DB db = new DB();
                db.sqlExecute(string.Format("insert into [dbo].[Manufacturer]([[Country_Manufacturer])" +
                    " values ('{0}', '{1}')", Country_Manufacturer.Text), DB.act.manipulation);
                Country_Manufacturer.Clear();
            }
            catch
            {
                MessageBox.Show("Ошибка данных!");
            }
        }

        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            try
            {
                if (crud_1.Items.Count != 0 & crud_1.SelectedItems.Count != 0)
                {
                    DataRowView dataRowView = (DataRowView)crud_1.SelectedItems[0];
                    DB db = new DB();
                    db.sqlExecute(string.Format("update [dbo].[Manufacturer] set " +
                        "[Country_Manufacturer] = '{0}' where [ID_Manufacturer] = '{1}'", Country_Manufacturer.Text,
                        dataRowView[0]), DB.act.manipulation);
                }
                Country_Manufacturer.Clear();
            }
            catch
            {
                MessageBox.Show("Ошибка данных!");
            }
        }

        private void Button_Click_7(object sender, RoutedEventArgs e)
        {
            try
            {
                switch (MessageBox.Show("Удалить выбранную запись?", DB.App_Name, MessageBoxButton.YesNo, MessageBoxImage.Question))
                {
                    case MessageBoxResult.Yes:
                        if (crud_1.Items.Count != 0 & crud_1.SelectedItems.Count != 0)
                        {
                            DataRowView dataRowView = (DataRowView)crud_1.SelectedItems[0];
                            DB db = new DB();
                            db.sqlExecute(string.Format("delete from [dbo].[Manufacturer] where [ID_Manufacturer] = {0}", dataRowView[0]), DB.act.manipulation);
                        }
                        break;
                }
               Country_Manufacturer.Clear();
            }
            catch
            {
                MessageBox.Show("Ошибка данных!");
            }
        }

        //Модели
        private void crud_Loaded_3(object sender, RoutedEventArgs e)
        {
            Crud_3();
        }

        private void Crud_3()
        {
            if (crud_3 != null)
            {
                Action action = () =>
                {
                    try
                    {
                        DB db = new DB();
                        db.sqlExecute(string.Format("select [ID_Model], [Name_Model] from [dbo].[Model]"), DB.act.select);
                        db.dependency.OnChange += OnChange_crud_3;
                        crud_3.ItemsSource = db.resultTable.DefaultView;
                        crud_3.Columns[0].Visibility = Visibility.Hidden;
                        crud_3.Columns[1].Header = "Наименование";
                    }
                    catch
                    { }
                };
                Dispatcher.Invoke(action);
            }
        }

        private void OnChange_crud_3(object sender, SqlNotificationEventArgs e)
        {
            if (e.Info != SqlNotificationInfo.Invalid)
                Crud_3();
        }

        private void crud_SelectionChanged_3(object sender, SelectionChangedEventArgs e)
        {
            {
                if (crud_3.Items.Count != 0 & crud_3.SelectedItems.Count != 0)
                {
                    DataRowView dataRow = (DataRowView)crud_3.SelectedItems[0];
                    Name_Model.Text = dataRow[1].ToString();
                }
            }
        }

        private void Button_Click_11(object sender, RoutedEventArgs e)
        {
            try
            {
                DB db = new DB();
                db.sqlExecute(string.Format("insert into [dbo].[Model]([Name_Model])" +
                    " values ('{0}')", Name_Model.Text), DB.act.manipulation);
                Name_Model.Clear();
            }
            catch
            {
                MessageBox.Show("Ошибка данных!");
            }
        }

        private void Button_Click_12(object sender, RoutedEventArgs e)
        {
            try
            {
                if (crud_3.Items.Count != 0 & crud_3.SelectedItems.Count != 0)
                {
                    DataRowView dataRowView = (DataRowView)crud_3.SelectedItems[0];
                    DB db = new DB();
                    db.sqlExecute(string.Format("update [dbo].[Model] set " +
                        "[Name_Model] = '{0}' where [ID_Model] = '{1}'", Name_Model.Text,
                        dataRowView[0]), DB.act.manipulation);
                }
                Name_Model.Clear();
            }
            catch
            {
                MessageBox.Show("Ошибка данных!");
            }
        }

        private void Button_Click_13(object sender, RoutedEventArgs e)
        {
            try
            {
                switch (MessageBox.Show("Удалить выбранную запись?", DB.App_Name, MessageBoxButton.YesNo, MessageBoxImage.Question))
                {
                    case MessageBoxResult.Yes:
                        if (crud_3.Items.Count != 0 & crud_3.SelectedItems.Count != 0)
                        {
                            DataRowView dataRowView = (DataRowView)crud_3.SelectedItems[0];
                            DB db = new DB();
                            db.sqlExecute(string.Format("delete from [dbo].[Model] where [ID_Model] = {0}", dataRowView[0]), DB.act.manipulation);
                        }
                        break;
                }
                Name_Model.Clear();
            }
            catch
            {
                MessageBox.Show("Ошибка данных!");
            }
        }

        //Группы
        private void crud_Loaded_4(object sender, RoutedEventArgs e)
        {
            Crud_4();
        }

        private void Crud_4()
        {
            if (crud_4 != null)
            {
                Action action = () =>
                {
                    try
                    {
                        DB db = new DB();
                        db.sqlExecute(string.Format("select [ID_Group], [Name_Group] from [dbo].[Groups]"), DB.act.select);
                        db.dependency.OnChange += OnChange_crud_4;
                        crud_4.ItemsSource = db.resultTable.DefaultView;
                        crud_4.Columns[0].Visibility = Visibility.Hidden;
                        crud_4.Columns[1].Header = "Наименование";
                    }
                    catch
                    { }
                };
                Dispatcher.Invoke(action);
            }
        }

        private void OnChange_crud_4(object sender, SqlNotificationEventArgs e)
        {
            if (e.Info != SqlNotificationInfo.Invalid)
                Crud_4();
        }

        private void crud_SelectionChanged_4(object sender, SelectionChangedEventArgs e)
        {
            {
                if (crud_4.Items.Count != 0 & crud_4.SelectedItems.Count != 0)
                {
                    DataRowView dataRow = (DataRowView)crud_4.SelectedItems[0];
                    Name_Group.Text = dataRow[1].ToString();
                }
            }
        }

        private void Button_Click_14(object sender, RoutedEventArgs e)
        {
            try
            {
                DB db = new DB();
                db.sqlExecute(string.Format("insert into [dbo].[Groups]([Name_Group])" +
                    " values ('{0}')", Name_Group.Text), DB.act.manipulation);
                Name_Group.Clear();
            }
            catch
            {
                MessageBox.Show("Ошибка данных!");
            }
        }

        private void Button_Click_15(object sender, RoutedEventArgs e)
        {
            try
            {
                if (crud_4.Items.Count != 0 & crud_4.SelectedItems.Count != 0)
                {
                    DataRowView dataRowView = (DataRowView)crud_4.SelectedItems[0];
                    DB db = new DB();
                    db.sqlExecute(string.Format("update [dbo].[Groups] set " +
                        "[Name_Group] = '{0}' where [ID_Group] = '{1}'", Name_Group.Text,
                        dataRowView[0]), DB.act.manipulation);
                }
                Name_Group.Clear();
            }
            catch
            {
                MessageBox.Show("Ошибка данных!");
            }
        }

        private void Button_Click_16(object sender, RoutedEventArgs e)
        {
            try
            {
                switch (MessageBox.Show("Удалить выбранную запись?", DB.App_Name, MessageBoxButton.YesNo, MessageBoxImage.Question))
                {
                    case MessageBoxResult.Yes:
                        if (crud_4.Items.Count != 0 & crud_4.SelectedItems.Count != 0)
                        {
                            DataRowView dataRowView = (DataRowView)crud_4.SelectedItems[0];
                            DB db = new DB();
                            db.sqlExecute(string.Format("delete from [dbo].[Groups] where [ID_Group] = {0}", dataRowView[0]), DB.act.manipulation);
                        }
                        break;
                }
                Name_Group.Clear();
            }
            catch
            {
                MessageBox.Show("Ошибка данных!");
            }
        }

        private void TabItem_Loaded_4(object sender, RoutedEventArgs e)
        {
            Crud();
        }

        //Состояния
        private void crud_Loaded_5(object sender, RoutedEventArgs e)
        {
            Crud_5();
        }

        private void Crud_5()
        {
            if (crud_5 != null)
            {
                Action action = () =>
                {
                    try
                    {
                        DB db = new DB();
                        db.sqlExecute(string.Format("select [ID_Condition], [Status_Condition] from [dbo].[Condition]"), DB.act.select);
                        db.dependency.OnChange += OnChange_crud_5;
                        crud_5.ItemsSource = db.resultTable.DefaultView;
                        crud_5.Columns[0].Visibility = Visibility.Hidden;
                        crud_5.Columns[1].Header = "Статус";
                    }
                    catch
                    { }
                };
                Dispatcher.Invoke(action);
            }
        }

        private void OnChange_crud_5(object sender, SqlNotificationEventArgs e)
        {
            if (e.Info != SqlNotificationInfo.Invalid)
                Crud_5();
        }

        private void crud_SelectionChanged_5(object sender, SelectionChangedEventArgs e)
        {
            {
                if (crud_5.Items.Count != 0 & crud_5.SelectedItems.Count != 0)
                {
                    DataRowView dataRow = (DataRowView)crud_5.SelectedItems[0];
                    Status_Condition.Text = dataRow[1].ToString();
                }
            }
        }

        private void Button_Click_17(object sender, RoutedEventArgs e)
        {
            try
            {
                DB db = new DB();
                db.sqlExecute(string.Format("insert into [dbo].[Condition]([Status_Condition])" +
                    " values ('{0}')", Status_Condition.Text), DB.act.manipulation);
                Status_Condition.Clear();
            }
            catch
            {
                MessageBox.Show("Ошибка данных!");
            }
        }

        private void Button_Click_18(object sender, RoutedEventArgs e)
        {
            try
            {
                if (crud_5.Items.Count != 0 & crud_5.SelectedItems.Count != 0)
                {
                    DataRowView dataRowView = (DataRowView)crud_5.SelectedItems[0];
                    DB db = new DB();
                    db.sqlExecute(string.Format("update [dbo].[Condition] set " +
                        "[Status_Condition] = '{0}' where [ID_Condition] = '{1}'", Status_Condition.Text,
                        dataRowView[0]), DB.act.manipulation);
                }
                Status_Condition.Clear();
            }
            catch
            {
                MessageBox.Show("Ошибка данных!");
            }
        }

        private void Button_Click_19(object sender, RoutedEventArgs e)
        {
            try
            {
                switch (MessageBox.Show("Удалить выбранную запись?", DB.App_Name, MessageBoxButton.YesNo, MessageBoxImage.Question))
                {
                    case MessageBoxResult.Yes:
                        if (crud_5.Items.Count != 0 & crud_5.SelectedItems.Count != 0)
                        {
                            DataRowView dataRowView = (DataRowView)crud_5.SelectedItems[0];
                            DB db = new DB();
                            db.sqlExecute(string.Format("delete from [dbo].[Condition] where [ID_Condition] = {0}", dataRowView[0]), DB.act.manipulation);
                        }
                        break;
                }
                Status_Condition.Clear();
            }
            catch
            {
                MessageBox.Show("Ошибка данных!");
            }
        }

        //Списания
        private void crud_Loaded_6(object sender, RoutedEventArgs e)
        {
            Crud_6();
        }

        private void Crud_6()
        {
            if (crud_6 != null)
            {
                Action action = () =>
                {
                    try
                    {
                        DB db = new DB();
                        db.sqlExecute(string.Format("select [ID_Write_downs], [Status_Write_downs] from [dbo].[Write_downs]"), DB.act.select);
                        db.dependency.OnChange += OnChange_crud_6;
                        crud_6.ItemsSource = db.resultTable.DefaultView;
                        crud_6.Columns[0].Visibility = Visibility.Hidden;
                        crud_6.Columns[1].Header = "Статус";
                    }
                    catch
                    { }
                };
                Dispatcher.Invoke(action);
            }
        }

        private void OnChange_crud_6(object sender, SqlNotificationEventArgs e)
        {
            if (e.Info != SqlNotificationInfo.Invalid)
                Crud_6();
        }

        private void crud_SelectionChanged_6(object sender, SelectionChangedEventArgs e)
        {
            {
                if (crud_6.Items.Count != 0 & crud_6.SelectedItems.Count != 0)
                {
                    DataRowView dataRow = (DataRowView)crud_6.SelectedItems[0];
                    Status_Write_downs.Text = dataRow[1].ToString();
                }
            }
        }

        private void Button_Click_20(object sender, RoutedEventArgs e)
        {
            try
            {
                DB db = new DB();
                db.sqlExecute(string.Format("insert into [dbo].[Write_downs]([Status_Write_downs])" +
                    " values ('{0}')", Status_Write_downs.Text), DB.act.manipulation);
                Status_Write_downs.Clear();
            }
            catch
            {
                MessageBox.Show("Ошибка данных!");
            }
        }

        private void Button_Click_21(object sender, RoutedEventArgs e)
        {
            try
            {
                if (crud_6.Items.Count != 0 & crud_6.SelectedItems.Count != 0)
                {
                    DataRowView dataRowView = (DataRowView)crud_6.SelectedItems[0];
                    DB db = new DB();
                    db.sqlExecute(string.Format("update [dbo].[Write_downs] set " +
                        "[Status_Write_downs] = '{0}' where [ID_Write_downs] = '{1}'", Status_Write_downs.Text,
                        dataRowView[0]), DB.act.manipulation);
                }
                Status_Write_downs.Clear();
            }
            catch
            {
                MessageBox.Show("Ошибка данных!");
            }
        }

        private void Button_Click_22(object sender, RoutedEventArgs e)
        {
            try
            {
                switch (MessageBox.Show("Удалить выбранную запись?", DB.App_Name, MessageBoxButton.YesNo, MessageBoxImage.Question))
                {
                    case MessageBoxResult.Yes:
                        if (crud_6.Items.Count != 0 & crud_6.SelectedItems.Count != 0)
                        {
                            DataRowView dataRowView = (DataRowView)crud_6.SelectedItems[0];
                            DB db = new DB();
                            db.sqlExecute(string.Format("delete from [dbo].[Write_downs] where [ID_Write_downs] = {0}", dataRowView[0]), DB.act.manipulation);
                        }
                        break;
                }
                Status_Write_downs.Clear();
            }
            catch
            {
                MessageBox.Show("Ошибка данных!");
            }
        }

        private void TabItem_Loaded_5(object sender, RoutedEventArgs e)
        {
            Crud();
        }

        //Роли
        private void crud_Loaded_7(object sender, RoutedEventArgs e)
        {
            Crud_7();
        }

        private void Crud_7()
        {
            if (crud_7 != null)
            {
                Action action = () =>
                {
                    try
                    {
                        DB db = new DB();
                        db.sqlExecute(string.Format("select [ID_Role], [Name_Role] from [dbo].[Role]"), DB.act.select);
                        db.dependency.OnChange += OnChange_crud_7;
                        crud_7.ItemsSource = db.resultTable.DefaultView;
                        crud_7.Columns[0].Visibility = Visibility.Hidden;
                        crud_7.Columns[1].Header = "Наименование";
                    }
                    catch
                    { }
                };
                Dispatcher.Invoke(action);
            }
        }

        private void OnChange_crud_7(object sender, SqlNotificationEventArgs e)
        {
            if (e.Info != SqlNotificationInfo.Invalid)
                Crud_7();
        }

        private void crud_SelectionChanged_7(object sender, SelectionChangedEventArgs e)
        {
            {
                if (crud_7.Items.Count != 0 & crud_7.SelectedItems.Count != 0)
                {
                    DataRowView dataRow = (DataRowView)crud_7.SelectedItems[0];
                    Name_Roles.Text = dataRow[1].ToString();
                }
            }
        }

        private void Button_Click_23(object sender, RoutedEventArgs e)
        {
            try
            {
                DB db = new DB();
                db.sqlExecute(string.Format("insert into [dbo].[Role]([Name_Role])" +
                    " values ('{0}')", Name_Roles.Text), DB.act.manipulation);
                Name_Roles.Clear();
            }
            catch
            {
                MessageBox.Show("Ошибка данных!");
            }
        }

        private void Button_Click_24(object sender, RoutedEventArgs e)
        {
            try
            {
                if (crud_7.Items.Count != 0 & crud_7.SelectedItems.Count != 0)
                {
                    DataRowView dataRowView = (DataRowView)crud_7.SelectedItems[0];
                    DB db = new DB();
                    db.sqlExecute(string.Format("update [dbo].[Role] set " +
                        "[Name_Role] = '{0}' where [ID_Role] = '{1}'", Name_Roles.Text,
                        dataRowView[0]), DB.act.manipulation);
                }
                Name_Roles.Clear();
            }
            catch
            {
                MessageBox.Show("Ошибка данных!");
            }
        }

        private void Button_Click_25(object sender, RoutedEventArgs e)
        {
            try
            {
                switch (MessageBox.Show("Удалить выбранную запись?", DB.App_Name, MessageBoxButton.YesNo, MessageBoxImage.Question))
                {
                    case MessageBoxResult.Yes:
                        if (crud_7.Items.Count != 0 & crud_7.SelectedItems.Count != 0)
                        {
                            DataRowView dataRowView = (DataRowView)crud_7.SelectedItems[0];
                            DB db = new DB();
                            db.sqlExecute(string.Format("delete from [dbo].[Role] where [ID_Role] = {0}", dataRowView[0]), DB.act.manipulation);
                        }
                        break;
                }
                Name_Roles.Clear();
            }
            catch
            {
                MessageBox.Show("Ошибка данных!");
            }
        }
    }
}
