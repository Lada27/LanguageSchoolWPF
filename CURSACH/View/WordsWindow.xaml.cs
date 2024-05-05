using CURSACH.Classes;
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

namespace CURSACH.View
{
    /// <summary>
    /// Логика взаимодействия для WordsWindow.xaml
    /// </summary>
    public partial class WordsWindow : Window
    {
        public static int languageId = 1;

        
        public WordsWindow(int Id)
        {
            InitializeComponent();
            WindowState = CurrentWindow.State;

            List<Classes.LanguagesClass> languages = DatabaseManager.getUserLanguages();

            foreach (Classes.LanguagesClass language in languages)
            {
                ComboBoxItem item = new ComboBoxItem();
                item.Content = language.LanguageName.Trim();
                item.Tag = language.LanguageId; 
                if (language.LanguageId == languageId)
                {
                    item.IsSelected = true;
                }
                cbLanguages.Items.Add(item);
            }

            LoadDoneWords(Id);
            LoadInProcessWords(Id);
            LoadNotStartedWords(Id);


        }

        // Rнопки упраавления всего окна
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


        // Кнопки управления бокового меню
        private void btnLanuages_Click(object sender, RoutedEventArgs e)
        {
            Languages language = new Languages();
            language.Show();
            this.Hide();
        }

       

        private void btnProfile_Click(object sender, RoutedEventArgs e)
        {
            Profile profile = new Profile();
            profile.Show();
            this.Hide();
        }

       


        //check
        private void LoadNotStartedWords(int userId)
        {
            List<Words> words = DatabaseManager.GetNotStartedWordsByUserAndLanguageId(userId, languageId);
            WordsNotStarted.Children.Clear();

            foreach (Words word in words)
            {
                Style buttonStyle = new Style(typeof(Button));
                buttonStyle.Setters.Add(new Setter(Button.BackgroundProperty, new SolidColorBrush((Color)ColorConverter.ConvertFromString("#CBCBCB"))));

                buttonStyle.Setters.Add(new Setter(Button.ForegroundProperty, Brushes.Black));
                buttonStyle.Setters.Add(new Setter(Button.FontSizeProperty, 14.0));
                buttonStyle.Setters.Add(new Setter(Button.MarginProperty, new Thickness(5, 10, 5, 0)));
                buttonStyle.Setters.Add(new Setter(Border.BorderThicknessProperty, new Thickness(0)));
                buttonStyle.Setters.Add(new Setter(FrameworkElement.MinWidthProperty, 90.0)); 
                buttonStyle.Setters.Add(new Setter(FrameworkElement.MinHeightProperty, 40.0)); 


                // Добавление Setter в стиль кнопок
                buttonStyle.Setters.Add(new Setter(TextBlock.TextWrappingProperty, TextWrapping.Wrap));


                // Создание объекта Border для задания радиуса скругления углов
                CornerRadius cornerRadius = new CornerRadius(30); // Устанавливаем радиус скругления углов

                // Создание объекта Setter для установки свойства CornerRadius
                Setter cornerRadiusSetter = new Setter(Border.CornerRadiusProperty, cornerRadius);

                // Добавление Setter в стиль кнопок
                buttonStyle.Setters.Add(cornerRadiusSetter);




                Button btnWord = new Button();
                btnWord.Content = word.Word;
                btnWord.Style = buttonStyle;
                btnWord.Click += (sender, e) => OpenWordDetails(word);
                WordsNotStarted.Children.Add(btnWord);
            }
        }

        //check
        private void LoadDoneWords(int userId)
        {
            List<Words> words = DatabaseManager.GetDoneWordsByUserAndLanguageId(userId, languageId);
            WordsDone.Children.Clear();

            foreach (Words word in words)
            {
                Style buttonStyle = new Style(typeof(Button));
                buttonStyle.Setters.Add(new Setter(Button.BackgroundProperty, new SolidColorBrush((Color)ColorConverter.ConvertFromString("#CBCBCB"))));

                buttonStyle.Setters.Add(new Setter(Button.ForegroundProperty, Brushes.Black));
                buttonStyle.Setters.Add(new Setter(Button.FontSizeProperty, 14.0));
                buttonStyle.Setters.Add(new Setter(Button.MarginProperty, new Thickness(5, 10, 5, 0)));
                buttonStyle.Setters.Add(new Setter(Border.BorderThicknessProperty, new Thickness(0)));
                buttonStyle.Setters.Add(new Setter(FrameworkElement.MinWidthProperty, 90.0)); 
                buttonStyle.Setters.Add(new Setter(FrameworkElement.MinHeightProperty, 40.0)); 


                // Добавление Setter в стиль кнопок
                buttonStyle.Setters.Add(new Setter(TextBlock.TextWrappingProperty, TextWrapping.Wrap));


                // Создание объекта Border для задания радиуса скругления углов
                CornerRadius cornerRadius = new CornerRadius(30); // Устанавливаем радиус скругления углов

                // Создание объекта Setter для установки свойства CornerRadius
                Setter cornerRadiusSetter = new Setter(Border.CornerRadiusProperty, cornerRadius);

                // Добавление Setter в стиль кнопок
                buttonStyle.Setters.Add(cornerRadiusSetter);




                Button btnWord = new Button();
                btnWord.Content = word.Word;
                btnWord.Style = buttonStyle;
                btnWord.Click += (sender, e) => OpenWordDetails(word);
                WordsDone.Children.Add(btnWord);
            }
        }


        //check
        private void LoadInProcessWords(int userId)
        {
            List<Words> words = DatabaseManager.GetInProcessWordsByUserAndLanguageId(userId, languageId);
            WordsInProcess.Children.Clear();

            foreach (Words word in words)
            {
                Style buttonStyle = new Style(typeof(Button));
                buttonStyle.Setters.Add(new Setter(Button.BackgroundProperty, new SolidColorBrush((Color)ColorConverter.ConvertFromString("#CBCBCB"))));

                buttonStyle.Setters.Add(new Setter(Button.ForegroundProperty, Brushes.Black));
                buttonStyle.Setters.Add(new Setter(Button.FontSizeProperty, 14.0));
                buttonStyle.Setters.Add(new Setter(Button.MarginProperty, new Thickness(5, 10, 5, 0)));
                buttonStyle.Setters.Add(new Setter(Border.BorderThicknessProperty, new Thickness(0)));
                buttonStyle.Setters.Add(new Setter(FrameworkElement.MinWidthProperty, 90.0)); // Устанавливаем минимальную ширину в 100
                buttonStyle.Setters.Add(new Setter(FrameworkElement.MinHeightProperty, 40.0)); // Устанавливаем минимальную высоту в 50


                // Добавление Setter в стиль кнопок
                buttonStyle.Setters.Add(new Setter(TextBlock.TextWrappingProperty, TextWrapping.Wrap));


                // Создание объекта Border для задания радиуса скругления углов
                CornerRadius cornerRadius = new CornerRadius(30); // Устанавливаем радиус скругления углов

                // Создание объекта Setter для установки свойства CornerRadius
                Setter cornerRadiusSetter = new Setter(Border.CornerRadiusProperty, cornerRadius);

                // Добавление Setter в стиль кнопок
                buttonStyle.Setters.Add(cornerRadiusSetter);




                Button btnWord = new Button();
                btnWord.Content = word.Word;
                btnWord.Style = buttonStyle;
                btnWord.Click += (sender, e) => OpenWordDetails(word);
                WordsInProcess.Children.Add(btnWord);
            }
        }


        //check
        private void OpenWordDetails(Words word)
        {
            WordDetailsWindow wordDetailsWindow = new WordDetailsWindow(word);
            wordDetailsWindow.ShowDialog(); 
            LoadDoneWords(CurrentUser.UserId);
            LoadInProcessWords(CurrentUser.UserId);
            LoadNotStartedWords(CurrentUser.UserId);

        }
       

        //check
        private void btnPlusWord_Click(object sender, RoutedEventArgs e)
        {
            WordDetailsWindow wordDetailsWindow = new WordDetailsWindow(languageId);
            wordDetailsWindow.ShowDialog();


            LoadDoneWords(CurrentUser.UserId);
            LoadInProcessWords(CurrentUser.UserId);
            LoadNotStartedWords(CurrentUser.UserId);

        }

        
        //check
        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = sender as ComboBox;
            ComboBoxItem selectedItem = comboBox.SelectedItem as ComboBoxItem;

            if (selectedItem != null)
            {
                languageId = (int)selectedItem.Tag;
            }


            LoadDoneWords(CurrentUser.UserId);
            LoadInProcessWords(CurrentUser.UserId);
            LoadNotStartedWords(CurrentUser.UserId);
        }

        private void btnhmeworkView_Click(object sender, RoutedEventArgs e)
        {
            HomeworksView homeworksView = new HomeworksView();
            homeworksView.Show();
            this.Hide();
        }
    }
}
