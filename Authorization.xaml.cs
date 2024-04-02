using System;
using System.Windows;
using System.Data.SqlClient;
using System.Windows.Controls;

namespace Technics
{
    /// <summary>
    /// Логика взаимодействия для Authorization.xaml
    /// </summary>
    public partial class Authorization : Window
    {
        public Authorization()
        {
            InitializeComponent();
            DB.ConnectionStrig = string.Format(DB.ConnectionStrig, DB.Users_ID, DB.Password);
            DB dataBaseClass = new DB();
            dataBaseClass.connection.Open();
        }

        public static string login;
        public static string password;

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            string hashedPassword = PasswordHasher.HashPassword(Passavtr.Password);
            DB dataBaseClass = new DB();
            dataBaseClass.sqlExecute(string.Format("select [Login_User], [Password_User], [Role_ID] from [dbo].[Users]" +
                "where [Login_User] = '{0}' and [Password_User] = '{1}' ", Logavtr.Text, hashedPassword), DB.act.select);

            if (dataBaseClass.resultTable.Rows.Count != 0)
            {
                string role = dataBaseClass.resultTable.Rows[0]["Role_ID"].ToString();

                login = dataBaseClass.resultTable.Rows[0]["Login_User"].ToString();
                password = dataBaseClass.resultTable.Rows[0]["Password_User"].ToString();

                App.ID = dataBaseClass.resultTable.Rows[0][0].ToString();
                switch (role)
                {
                    case "1":
                        Tech tech = new Tech();
                        tech.Show();
                        Close();
                        break;
                    case "2":
                        Polzovatel polzovatel = new Polzovatel();
                        polzovatel.Show();
                        Close();
                        break;
                }
            }
            else
            {
                MessageBox.Show("Не верный логин или пароль!", DB.App_Name);
                Logavtr.Focus();
                Logavtr.Clear();
                Passavtr.Clear();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Registration registration = new Registration();
            registration.Show();
            Close();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            Close();
        }

        //public static class UserData
        //{
        //    public static string Login { get; set; }
        //    public static string Password { get; set; }
        //}

        public void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            //if (checkBox.IsChecked == true)
            //{
            //    // Сохраняем данные полей введенные пользователем
            //    UserData.Login = Logavtr.Text;
            //    UserData.Password = Passavtr.Password;
            //}
            //else
            //{
            //    // Очищаем данные полей
            //    Logavtr.Text = "";
            //    Passavtr.Password = "";
            //}
        }
    }
}
