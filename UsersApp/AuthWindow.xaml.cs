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
using System.Windows.Shapes;


namespace UsersApp
{
    /// <summary>
    /// Interaction logic for AuthWindow.xaml
    /// </summary>
    public partial class AuthWindow : Window
    {
        public AuthWindow()
        {
            InitializeComponent();
        }
        private void Clear()
        {
            loginTextBox.ToolTip = null;
            loginTextBox.Background = Brushes.Transparent;
            passTextBox.ToolTip = null;
            passTextBox.Background = Brushes.Transparent;
        }
        private void Button_Auth_Click(object sender, RoutedEventArgs e)
        {
            string login = loginTextBox.Text.Trim();
            string pass = passTextBox.Password.Trim();

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
            else
            {
                Clear();

                User authUser;
                using (AppContext db = new AppContext())
                {
                    authUser = db.Users.Where(b => b.Login == login && b.Password == pass).FirstOrDefault();
                }
                if (authUser != null)
                {
                    MessageBox.Show("Успешно !", "Сообщение", MessageBoxButton.OK, MessageBoxImage.Information);
                    UserPageWindow userPageWindow = new UserPageWindow();
                    userPageWindow.Show();
                    Hide();
                }
                else
                {
                    MessageBox.Show("Вы ввели что-то не так", "Сообщение", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }
        Thread thread;
        MainWindow mainWindow;
        private void Button_Reg_Click(object sender, RoutedEventArgs e)
        {
            //thread = new Thread(OpenWindow);
            //thread.SetApartmentState(ApartmentState.STA);
            //thread.Start();
            mainWindow = new MainWindow();
            // Application.Current.Run(mainWindow);
            mainWindow.Show();
            Hide();
        }

        private void OpenWindow()
        {
            mainWindow = new MainWindow();
            // Application.Current.Run(mainWindow);
            mainWindow.Show();
            Hide();
        }
    }
}
