using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
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
    /// Логика взаимодействия для Registration.xaml
    /// </summary>
    public partial class Registration : Window
    {
        public Registration()
        {
            InitializeComponent();
            DB.ConnectionStrig = string.Format(DB.ConnectionStrig, DB.Users_ID, DB.Password);
            DB dataBaseClass = new DB();
            dataBaseClass.connection.Open();
        }

        private void Button_Click_Reg(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Login.Text != Password.Password)
                {
                    if (Fam.Text.Length <= 50 && Ima.Text.Length <= 50 && Otch.Text.Length <= 50)
                    {
                        if (Password.Password.Length >= 5 && Password.Password.Length <= 20)
                        {
                            bool enUpper = false;
                            bool enLower = false;

                            for (int i = 0; i < Password.Password.Length; i++)
                            {
                                if (Password.Password[i] >= 'A' && Password.Password[i] <= 'Z') enUpper = true;
                                if (Password.Password[i] >= 'a' && Password.Password[i] <= 'z') enLower = true;
                            }

                            if (!enUpper)
                                MessageBox.Show("Введите большие символы");
                            else if (!enLower)
                                MessageBox.Show("Введите маленькие символы");
                            if (enUpper && enLower)
                            {
                                string hashedPassword = PasswordHasher.HashPassword(Password.Password);
                                DB db = new DB();
                                db.sqlExecute(string.Format("insert into [dbo].[Users] ([Surname_User], [Name_User], [Middle_Name_User], [Email_User], [Login_User], [Password_User], [Role_ID])" +
                                    " values ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}')", Fam.Text, Ima.Text, Otch.Text, Pocht.Text, Login.Text, hashedPassword, "1"), DB.act.manipulation);
                                SendEmailAsync(Login.Text, Password.Password, Pocht.Text);
                                Authorization authorization = new Authorization();
                                authorization.Show();
                                Close();
                            }
                        }
                        else MessageBox.Show("Пароль должен содержать от 5 до 20 символов");
                    }
                    else MessageBox.Show("ФИО не должно превышать 50 символов");
                }
                else MessageBox.Show("Пароль не должен содержать логин");
            }
            catch
            {
                MessageBox.Show("Ошибка данных!");
            }
        }

        private static async Task SendEmailAsync(String Login, String Password, String Pocht)
        {
            MailAddress from = new MailAddress("fikrast@mail.ru", "Логи");
            MailAddress to = new MailAddress(Pocht);
            MailMessage m = new MailMessage(from, to);
            m.Subject = "Регистрация удачна";
            m.Body = $"<h2>Здравствуйте!<br>Ваш логин: " + Login + "<br>" + "Ваш пароль: " + Password + "</h2>";
            m.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient("smtp.mail.ru");
            smtp.Credentials = new NetworkCredential("fikrast@mail.ru", "0RYDLHzKAh4cU7Namn3t");
            smtp.EnableSsl = true;
            await smtp.SendMailAsync(m);
        }

        private void Button_Click_Auth(object sender, RoutedEventArgs e)
        {
            Authorization authorization = new Authorization();
            authorization.Show();
            Close();
        }
    }
}
