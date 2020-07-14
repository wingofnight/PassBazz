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
using System.Windows.Navigation;
using System.Windows.Shapes;
using MySql.Data.MySqlClient;

namespace PassBazz_1._2
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            
        }   
       private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void btn_esq_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void HyperReg_Click(object sender, RoutedEventArgs e)
        {
            var registred = new Registred();
            registred.Show();
            Close();
        }      

        private void btn_esq_Copy_Click(object sender, RoutedEventArgs e)
        {
            var login = txtbx_login.Text;
            var Userpass = passBlock.Password;

            var host = "mysql11.hostland.ru";
            var database = "host1323541_itstep24";
            var port = "3306";
            var username = "host1323541_itstep";
            var pass = "269f43dc";
            var ConnString = "Server=" + host + ";Database=" + database + ";port=" + port + ";User Id=" + username + ";password=" + pass;
            var db = new MySqlConnection(ConnString);
            db.Open();
            var sql = $"SELECT login, pass FROM Account WHERE login = '{login}'";
            var command = new MySqlCommand { Connection = db, CommandText = sql };
            var result = command.ExecuteReader();
            if (!result.Read())
            {
                MessageBox.Show("Такого пользователя нет", "ОШИБКА", MessageBoxButton.OK, MessageBoxImage.Error);
                txtbx_login.Clear();
                passBlock.Clear();
            }
            else
            {
                if (result.GetString(0) == login && result.GetString(1) == Userpass)
                {
                    MessageBox.Show("Вход разрешён", "УСПЕХ", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }

        private void btn_esq2_Click(object sender, RoutedEventArgs e)
        {
            txtbx_login.Clear();
            passBlock.Clear();
        }
    }
}
