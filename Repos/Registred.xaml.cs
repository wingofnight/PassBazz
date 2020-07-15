using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using MySql.Data.MySqlClient;
using PasswordCheck;
namespace PassBazz_1._2
{
    
    public partial class Registred : Window
    {
        public Registred()
        {
            InitializeComponent();
        }
        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void btn_back_Click(object sender, RoutedEventArgs e)
        {
            var main = new MainWindow();
            
            main.Show();
            Close();
        }

        private void btn_esq_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btn_confirm_reg_Click(object sender, RoutedEventArgs e)
        {          

            var userLogin = Input_login.Text;
            var userEmail = Input_mail.Text;
            var userPass = Input_password.Password;         
            var PassCheck = new Password();
            bool concurrence = false;
            bool emalaccept = false;
            bool nameaccept = false;

            if (userPass == Input_Repeat.Password)
            {   lb_er3.Visibility = Visibility.Hidden;
                concurrence = true;
            }  else  { txtblk_error_mesage_3.Foreground = Brushes.Red; txtblk_error_mesage_3.Text = "Пароли не совпадают!"; lb_er3.Visibility = Visibility.Visible; concurrence = false; }
            if (userLogin.Length >0)
            {
                nameaccept = true; lb_er0.Visibility = Visibility.Hidden;
            }
            else { lb_er0.Visibility = Visibility.Visible; nameaccept = false; txtblk_error_mesage_2.Foreground = Brushes.Red; txtblk_error_mesage_2.Text = "Где имя?!"; }
            if (userEmail.Length > 0)
            {
                emalaccept = true; lb_er2.Visibility = Visibility.Hidden;
            }
            else { lb_er2.Visibility = Visibility.Visible; emalaccept = false; txtblk_error_mesage_1.Foreground = Brushes.Red; txtblk_error_mesage_1.Text = "Где почта?!"; }

            if (concurrence == true && nameaccept == true && emalaccept == true && PassCheck.CheckPass(userPass).accept == true)
            {
                var host = "123";
                var database = "123";
                var port = "136";
                var username = "123";
                var pass = "123";
                var ConnString = "Server=" + host + ";Database=" + database + ";port=" + port + ";User Id=" + username + ";password=" + pass;
                var db = new MySqlConnection(ConnString);
                db.Open();

                var sql = $"SELECT login, pass FROM Account WHERE login = '{userLogin}'";
                
                var command = new MySqlCommand { Connection = db, CommandText = sql };
                var result = command.ExecuteReader();
                
                var main = new MainWindow();
                if (!result.Read())
                {
                    db.Close();
                    db.Open();                    
                    sql = $"INSERT INTO Account (login,pass,email) VALUES  ('{userLogin}','{userPass}','{userEmail}');";
                    var command2 = new MySqlCommand { Connection = db, CommandText = sql };

                    command2.ExecuteReader();
                    MessageBox.Show("REGISTER Complide", "Nice!", MessageBoxButton.OK, MessageBoxImage.Error);
                   
                    Close();
                    main.Show();
                }
                else
                {
                    if (result.GetString(0) == userLogin)
                    {
                        MessageBox.Show("Name is failed", "ATTENTION", MessageBoxButton.OK, MessageBoxImage.Error);
                        Close();

                        main.Show();
                    }
                }
            }
        }

        private void Input_password_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            var PassCheck = new Password();
            if (PassCheck.CheckPass(Input_password.Password).accept)
            {
                lb_er1.Visibility = Visibility.Hidden;
            }  else { lb_er1.Visibility = Visibility.Visible; }
            if (PassCheck.CheckAlphabet(Input_password.Password).acces) {  txtblk_error_mesage_3.Foreground = Brushes.GreenYellow; txtblk_error_mesage_3.Text = PassCheck.CheckAlphabet(Input_password.Password).error;  } 
            else { txtblk_error_mesage_3.Foreground = Brushes.Red; txtblk_error_mesage_3.Text = PassCheck.CheckAlphabet(Input_password.Password).error;  }

            if (PassCheck.CheckSymbols(Input_password.Password).acces) {  txtblk_error_mesage_2.Foreground = Brushes.GreenYellow; txtblk_error_mesage_2.Text = PassCheck.CheckSymbols(Input_password.Password).error; }
            else { txtblk_error_mesage_2.Foreground = Brushes.Red; txtblk_error_mesage_2.Text = PassCheck.CheckSymbols(Input_password.Password).error;  }

            if (PassCheck.MinLength(Input_password.Password).acces) {  txtblk_error_mesage_1.Foreground = Brushes.GreenYellow; txtblk_error_mesage_1.Text = PassCheck.MinLength(Input_password.Password).error; }
            else { txtblk_error_mesage_1.Foreground = Brushes.Red; txtblk_error_mesage_1.Text = PassCheck.MinLength(Input_password.Password).error;  }

            txtblk_error_mesage_3.Visibility = Visibility.Visible; txtblk_error_mesage_2.Visibility = Visibility.Visible; txtblk_error_mesage_1.Visibility = Visibility.Visible;

        }
    }
}
