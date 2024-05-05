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
    /// Логика взаимодействия для LanguagesClass.xaml
    /// </summary>
    public partial class Languages : Window
    {
        int SelectedLanguageId = 1;
        public Languages()
        {
            InitializeComponent();
            WindowState = CurrentWindow.State;
            loadAllLanguages();
            LoadLanguageWordsById(SelectedLanguageId);

        }

       
        //check
        private void loadAllLanguages()
        {

            List<LanguagesClass> languages = DatabaseManager.LoadAllLanguages();
            spAllLanguages.Children.Clear();

            foreach (LanguagesClass language in languages)
            {
                Style buttonStyle = new Style(typeof(Button));
                if (language.LanguageId == SelectedLanguageId)
                {
                    buttonStyle.Setters.Add(new Setter(Button.BackgroundProperty, new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFC97F"))));
                }
                else
                {
                    buttonStyle.Setters.Add(new Setter(Button.BackgroundProperty, new SolidColorBrush((Color)ColorConverter.ConvertFromString("#CBCBCB"))));
                }

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

                Button btnLanguage = new Button();
                btnLanguage.Content = language.LanguageName;
                btnLanguage.Style = buttonStyle;
                btnLanguage.Click += (sender, e) => LoadLanguageWordsById(language.LanguageId);
                spAllLanguages.Children.Add(btnLanguage);
            }
        }

        //check
        private void LoadLanguageWordsById(int languageId)
        {
            SelectedLanguageId = languageId;
            loadAllLanguages();
            LanguagesClass language = DatabaseManager.GetLanguageById(languageId);
            int number = DatabaseManager.GetNumberOfWordsByLanguageId(language.LanguageId);
           

            Language.Text = language.LanguageName;
            NuberOfWords.Text = number.ToString();

            List<Words> words = DatabaseManager.GetLanguageWordsByLanguageId(languageId);
            spWordsByLanguage.Children.Clear();

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
                spWordsByLanguage.Children.Add(btnWord);
            }

            int LanguageIdInUserLanguageList = DatabaseManager.isInLanguageList(SelectedLanguageId, CurrentUser.UserId);
            if (LanguageIdInUserLanguageList != -1)
            {
                btnLearnLanguage.Visibility = Visibility.Collapsed;
            }
        }

        //check
        private void OpenWordDetails(Words word)
        {
            WordDetailsWindow taskDetailsWindow = new WordDetailsWindow(word);
            taskDetailsWindow.ShowDialog(); // Отображаем окно как модальное

            loadAllLanguages();
            LoadLanguageWordsById(SelectedLanguageId);
        }



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

        //check
        private void btnlanguage_Click(object sender, RoutedEventArgs e)
        {
            Languages language = new Languages();
            language.Show();
            this.Hide();
        }


        private void btnhomeworkView_Click(object sender, RoutedEventArgs e)
        {
            HomeworksView homeworkView = new HomeworksView();
            homeworkView.Show();
            this.Hide();
        }


        private void btnProfile_Click(object sender, RoutedEventArgs e)
        {
            Profile profile = new Profile();
            profile.Show();
            this.Hide();
        }


        private void btnWords_Click(object sender, RoutedEventArgs e)
        {
            WordsWindow wordsWindow = new WordsWindow(CurrentUser.UserId);
            wordsWindow.Show();
            this.Hide();
        }



        private void btnLearnLanguage_Click(object sender, RoutedEventArgs e)
        {
            DatabaseManager.AddLanguageToUserList(SelectedLanguageId);

            DatabaseManager.AssignWordsToCurrentUser(SelectedLanguageId);

            int LanguageIdInUserLanguageList = DatabaseManager.isInLanguageList(SelectedLanguageId, CurrentUser.UserId);
            if (LanguageIdInUserLanguageList != -1)
            {
                btnLearnLanguage.Visibility = Visibility.Collapsed;
            }
        }
    }
    
    
}
