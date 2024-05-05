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
    /// Логика взаимодействия для HomeworksView.xaml
    /// </summary>
    public partial class HomeworksView : Window
    {
        int selectedHomework = 1;
        int selectedLanguageId = -1;
        string selectedStatus;
        public HomeworksView()
        {
            InitializeComponent();
            WindowState = CurrentWindow.State;

            List<LanguagesClass> languaes = DatabaseManager.getUserLanguages();

            foreach (LanguagesClass language in languaes)
            {
                ComboBoxItem item = new ComboBoxItem();
                item.Content = language.LanguageName.Trim();
                item.Tag = language.LanguageId;
                cbLanguages.Items.Add(item);

            }

            List<HomeworksClass> homeworks = DatabaseManager.GetAllHomeworks();

            foreach (HomeworksClass homework in homeworks)
            {
                Style buttonStyle = new Style(typeof(Button));
                buttonStyle.Setters.Add(new Setter(Button.BackgroundProperty, new SolidColorBrush((Color)ColorConverter.ConvertFromString("#CBCBCB"))));

                buttonStyle.Setters.Add(new Setter(Button.ForegroundProperty, Brushes.Black));
                buttonStyle.Setters.Add(new Setter(Button.FontSizeProperty, 14.0));
                buttonStyle.Setters.Add(new Setter(Button.MarginProperty, new Thickness(5, 10, 5, 0)));
                buttonStyle.Setters.Add(new Setter(Border.BorderThicknessProperty, new Thickness(0)));
                buttonStyle.Setters.Add(new Setter(FrameworkElement.MinWidthProperty, 90.0));
                buttonStyle.Setters.Add(new Setter(FrameworkElement.MinHeightProperty, 40.0));
                buttonStyle.Setters.Add(new Setter(TextBlock.TextWrappingProperty, TextWrapping.Wrap));

                CornerRadius cornerRadius = new CornerRadius(30);
                Setter cornerRadiusSetter = new Setter(Border.CornerRadiusProperty, cornerRadius);
                buttonStyle.Setters.Add(cornerRadiusSetter);

                Button btnHomework = new Button();
                btnHomework.Content = homework.HomeworkDescription;
                btnHomework.Style = buttonStyle;
                btnHomework.Click += (se, ee) => openHomeworkDetails_Click(homework);
                spAllHomeworks.Children.Add(btnHomework);
            }
        }


       //check
       private void loadAllHomeworks()
        {
            List<HomeworksClass> homeworks = DatabaseManager.GetAllHomeworks();
            spAllHomeworks.Children.Clear();

            foreach (HomeworksClass homework in homeworks)
            {
                Style buttonStyle = new Style(typeof(Button));
                if (homework.HomeworkId == selectedHomework)
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
                buttonStyle.Setters.Add(new Setter(FrameworkElement.MinWidthProperty, 90.0)); 
                buttonStyle.Setters.Add(new Setter(FrameworkElement.MinHeightProperty, 60.0)); 


                buttonStyle.Setters.Add(new Setter(TextBlock.TextWrappingProperty, TextWrapping.Wrap));


                CornerRadius cornerRadius = new CornerRadius(30);

                Setter cornerRadiusSetter = new Setter(Border.CornerRadiusProperty, cornerRadius);

                buttonStyle.Setters.Add(cornerRadiusSetter);

                Button btnHomework = new Button();
                btnHomework.Content = homework.HomeworkDescription;
                btnHomework.Style = buttonStyle;
                btnHomework.Click += (sender, e) => openHomeworkDetails_Click(homework);
                spAllHomeworks.Children.Add(btnHomework);
            }
        }

        //check
        private void openHomeworkDetails_Click(HomeworksClass homework)
        {
            HomeworkDetailsWindow homeworkDetailsWindow = new HomeworkDetailsWindow(homework);
            homeworkDetailsWindow.ShowDialog(); 

            loadAllHomeworks();

        }

        //check
        private void btnAddHomework_Click(object sender, RoutedEventArgs e)
        {
            HomeworkDetailsWindow homeworkDetailsWindow = new HomeworkDetailsWindow();
            homeworkDetailsWindow.ShowDialog(); 
            loadAllHomeworks();


        }

        //check
        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            ComboBox comboBoxLanguages = cbLanguages as ComboBox;
            ComboBoxItem selectedItemCBLanguages = comboBoxLanguages.SelectedItem as ComboBoxItem;

            if (selectedItemCBLanguages != null && selectedItemCBLanguages.Tag != null)
            {
                selectedLanguageId = Convert.ToInt32(selectedItemCBLanguages.Tag);
            }

            ComboBox comboBoxStatus = cbStatus as ComboBox;
            if (comboBoxStatus != null)
            {
                ComboBoxItem selectedItemCBStatus = comboBoxStatus.SelectedItem as ComboBoxItem;

                if (selectedItemCBStatus != null)
                {
                    selectedStatus = selectedItemCBStatus.Content.ToString().Trim();
                }

                List<HomeworksClass> homeworks = new List<HomeworksClass>();

                if (selectedLanguageId == -1 && selectedStatus == "Любой статус")
                {
                    homeworks = DatabaseManager.GetAllHomeworks();
                }
                else if (selectedLanguageId == -1 && selectedStatus != "Любой статус")
                {
                    homeworks = DatabaseManager.GetHomeworksByStatus(selectedStatus);
                }
                else if (selectedLanguageId != -1 && selectedStatus != "Любой статус")
                {
                    homeworks = DatabaseManager.GetHomeworksByStatusAndLanguage(selectedStatus, selectedLanguageId);
                }
                else if (selectedLanguageId != -1 && selectedStatus == "Любой статус")
                {
                    homeworks = DatabaseManager.GetHomeworksByLanguageId(selectedLanguageId);
                }

                displayFilteredHomeworks(homeworks);
            }
            
        }

        private void displayFilteredHomeworks(List<HomeworksClass> homeworks)
        {
            if (spAllHomeworks != null)
            {
                spAllHomeworks.Children.Clear();

                foreach (HomeworksClass homework in homeworks)
                {
                    Style buttonStyle = new Style(typeof(Button));
                    buttonStyle.Setters.Add(new Setter(Button.BackgroundProperty, new SolidColorBrush((Color)ColorConverter.ConvertFromString("#CBCBCB"))));

                    buttonStyle.Setters.Add(new Setter(Button.ForegroundProperty, Brushes.Black));
                    buttonStyle.Setters.Add(new Setter(Button.FontSizeProperty, 14.0));
                    buttonStyle.Setters.Add(new Setter(Button.MarginProperty, new Thickness(5, 10, 5, 0)));
                    buttonStyle.Setters.Add(new Setter(Border.BorderThicknessProperty, new Thickness(0)));
                    buttonStyle.Setters.Add(new Setter(FrameworkElement.MinWidthProperty, 90.0));
                    buttonStyle.Setters.Add(new Setter(FrameworkElement.MinHeightProperty, 40.0));
                    buttonStyle.Setters.Add(new Setter(TextBlock.TextWrappingProperty, TextWrapping.Wrap));

                    CornerRadius cornerRadius = new CornerRadius(30);
                    Setter cornerRadiusSetter = new Setter(Border.CornerRadiusProperty, cornerRadius);
                    buttonStyle.Setters.Add(cornerRadiusSetter);

                    Button btnHomework = new Button();
                    btnHomework.Content = homework.HomeworkDescription;
                    btnHomework.Style = buttonStyle;
                    btnHomework.Click += (se, ee) => openHomeworkDetails_Click(homework);
                    spAllHomeworks.Children.Add(btnHomework);
                }
            }
        }

        // Кнопки упраавления всего окна

        //check
        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        //check
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

        //check
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();

        }



        // Кнопки управления бокового меню

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

        private void btnProfile_Click(object sender, RoutedEventArgs e)
        {
            Profile profile = new Profile();
            profile.Show();
            this.Hide();
        }

        //check

        private void btnWords_Click(object sender, RoutedEventArgs e)
        {
            WordsWindow wordsWindow = new WordsWindow(CurrentUser.UserId);
            wordsWindow.Show();
            this.Hide();
        }


    }
}
