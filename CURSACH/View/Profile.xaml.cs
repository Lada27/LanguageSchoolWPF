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
using System.Windows.Shapes;
using System.Text.RegularExpressions;
using CURSACH.Classes;


namespace CURSACH.View
{
    /// <summary>
    /// Логика взаимодействия для Profile.xaml
    /// </summary>
    public partial class Profile : Window
    {
        Users user;
        public Profile()
        {

            InitializeComponent();
            WindowState = CurrentWindow.State;

            user = DatabaseManager.GetUserById(CurrentUser.UserId);

            UserName.Text = user.Name.Trim();
            UserEmail.Text = user.Email.Trim();
            UserPassword.Text = user.Password.Trim();
            UserPhone.Text = user.Phone.Trim();
        }

        //кнопки управления окна
        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void btnMaximaze_Click(object sender, RoutedEventArgs e)
        {
            if (WindowState == WindowState.Maximized)
            {
                WindowState = WindowState.Normal;
                CurrentWindow.State = WindowState.Normal;

            }
            else
            {
                WindowState = WindowState.Maximized;
                CurrentWindow.State = WindowState.Maximized;

            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();

        }


        //кнопки управления меню

        //check
        private void btnlanguage_Click(object sender, RoutedEventArgs e)
        {
            Languages language = new Languages();
            language.Show();
            this.Hide();
        }

        //check
        private void btnhomeworkView_Click(object sender, RoutedEventArgs e)
        {
            HomeworksView homeworkView = new HomeworksView();
            homeworkView.Show();
            this.Hide();
        }

        //check
        private void btnWords_Click(object sender, RoutedEventArgs e)
        {
            WordsWindow wordsWindow = new WordsWindow(CurrentUser.UserId);
            wordsWindow.Show();
            this.Hide();
        }


        // кнопки для сохранения и выхода из профиля
        //check
        private void btnApplyUserDataChane_Click(object sender, RoutedEventArgs e)
        {
            string newName = UserName.Text.Trim();
            string newEmail = UserEmail.Text.Trim();
            string newPassword = UserPassword.Text.Trim();
            string newPhone = UserPhone.Text.Trim();

            if (!IsValidEmail(newEmail))
            {
                MessageBox.Show("Введите корректный адрес электронной почты.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (!IsValidPassword(newPassword))
            {
                MessageBox.Show("Пароль должен содержать не менее 8 символов и включать как минимум одну букву и одну цифру.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (!IsValidPhoneNumber(newPhone))
            {
                MessageBox.Show("Введите корректный номер телефона.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            string confirmationMessage = "Вы уверены, что хотите внести изменения в свой профиль?";
            MessageBoxResult result = MessageBox.Show(confirmationMessage, "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                if (DatabaseManager.UpdateUserProfile(CurrentUser.UserId, newName, newEmail, newPassword, newPhone))
                {
                    MessageBox.Show("Данные профиля успешно изменены.", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Ошибка при обновлении данных профиля.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        //check
        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            CurrentUser.UserId = 0;
            string confirmationMessage = "Вы уверены, что хотите выйти из профиля?";

            MessageBoxResult result = MessageBox.Show(confirmationMessage, "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                LoginView loginView = new LoginView();
                loginView.Show();
                this.Hide();
            }
        }



        // проверка вводимых данных
        //check
        private bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            var emailRegex = new Regex(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$");
            return emailRegex.IsMatch(email);
        }

        //check
        private bool IsValidPassword(string password)
    {
        return !string.IsNullOrEmpty(password) && password.Length >= 8 && Regex.IsMatch(password, @"\d") && Regex.IsMatch(password, "[a-zA-Z]");
    }

        //check
        private bool IsValidPhoneNumber(string phoneNumber)
        {
            string phonePattern = @"^\+?\d{0,2}?\(?\d{3}\)?[\s.-]?\d{3}[\s.-]?\d{4}$";

            return Regex.IsMatch(phoneNumber, phonePattern);
        }
    }
}
