using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
using System.Windows.Media.Animation;

namespace UsersApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        AppContext db;
        Thread thread;
        public MainWindow()
        {
            InitializeComponent();
            db = new AppContext();
            DoubleAnimation dAnimation = new DoubleAnimation();
            dAnimation.From = 0;
            dAnimation.To = 450;
            dAnimation.Duration = TimeSpan.FromSeconds(5);
            dAnimation.AutoReverse = true;
            dAnimation.RepeatBehavior = RepeatBehavior.Forever;
            registerButton.BeginAnimation(Button.WidthProperty, dAnimation);
        }
        private void Clear()
        {
            loginTextBox.ToolTip = null;
            loginTextBox.Background = Brushes.Transparent;
            passTextBox.ToolTip = null;
            passTextBox.Background = Brushes.Transparent;
            passTextBox_Again.ToolTip = null;
            passTextBox_Again.Background = Brushes.Transparent;
            emailTextBox.ToolTip = null;
            emailTextBox.Background = Brushes.Transparent;
        }
        private void RegisterButtonClick (object sender, RoutedEventArgs e)
        {
            string login = loginTextBox.Text.Trim();
            string pass = passTextBox.Password.Trim();
            string pass_sec = passTextBox_Again.Password.Trim();
            string email = emailTextBox.Text.Trim().ToLower();       // Переводим в нижний регистр

            if (login.Length < 5)
            {
                loginTextBox.ToolTip = "Это поле введено некорректно";
                loginTextBox.Background = Brushes.DarkRed;
            }
            else if (pass.Length < 5)
            {
                passTextBox.ToolTip = "Это поле введено некорректно";
                passTextBox.Background = Brushes.DarkRed;
            }
            else if (pass != pass_sec)
            {
                passTextBox_Again.ToolTip = "Это поле введено некорректно";
                passTextBox_Again.Background = Brushes.DarkRed;
            }
            else if (email.Length < 5 || !email.Contains("@") || !email.Contains(".")) 
            {
                emailTextBox.ToolTip = "Это поле введено некорректно";
                emailTextBox.Background = Brushes.DarkRed;
            }
            else
            {
                Clear();
                MessageBox.Show("Успешно!", "Сообщение", MessageBoxButton.OK, MessageBoxImage.Information);
                User user = new User(login, pass, email);
                db.Users.Add(user);
                db.SaveChanges();
                authWindow = new AuthWindow();
                // Application.Current.Run(authWindow);
                authWindow.Show();
                Hide();
            }
        }
        AuthWindow authWindow;
        private  void ButtonEnter_Click(object sender, RoutedEventArgs e)
        {
            authWindow = new AuthWindow();
            // Application.Current.Run(authWindow);
            authWindow.Show();
            Hide();
        }

        private void OpenWindow()
        {
            authWindow = new AuthWindow();
            // Application.Current.Run(authWindow);
            authWindow.Show();
            Hide();
        }
    }
}
