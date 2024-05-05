using CURSACH.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
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
    /// Логика взаимодействия для HomeworkDetailsWindow.xaml
    /// </summary>
    public partial class HomeworkDetailsWindow : Window
    {
        string action = "добавить";
        HomeworksClass HomeworkData = new HomeworksClass();

        //check
        public HomeworkDetailsWindow()
        {
            InitializeComponent();
            action = "добавить";

            List<LanguagesClass> languaes = DatabaseManager.getUserLanguages();
            btnDeleteHomework.Visibility = Visibility.Collapsed;


            foreach (LanguagesClass language in languaes)
            {
                ComboBoxItem item = new ComboBoxItem();
                item.Content = language.LanguageName.Trim();
                item.Tag = language.LanguageId; 
                cbLanguage.Items.Add(item);

               
            }
        }

        //check
        public HomeworkDetailsWindow(HomeworksClass homework)
        {
            InitializeComponent();
            action = "изменить";

            HomeworkData.HomeworkId = homework.HomeworkId;
            HomeworkData.HomeworkSolution = homework.HomeworkSolution;
            HomeworkData.HomeworkDescription = homework.HomeworkDescription;
            HomeworkData.Status = homework.Status;


            Description.Text = homework.HomeworkDescription;
            Solution.Text = homework.HomeworkSolution;

            List<LanguagesClass> languaes = DatabaseManager.getUserLanguages();

            foreach (LanguagesClass language in languaes)
            {
                ComboBoxItem item = new ComboBoxItem();
                item.Content = language.LanguageName.Trim();
                item.Tag = language.LanguageId; 
                cbLanguage.Items.Add(item);

                if (language.LanguageId == homework.LanguageId)
                {
                    item.IsSelected = true;
                }
            }

            foreach (ComboBoxItem item in cbStatus.Items)
            {
                if (item.Content.ToString().Trim() == homework.Status.Trim())
                {
                    item.IsSelected = true;
                    break;
                }
            }

        }

        //check
        private void btnDeleteHomework_Click(object sender, RoutedEventArgs e)
        {
            int homeworkId = HomeworkData.HomeworkId;
            MessageBoxResult result = MessageBox.Show("Вы уверены, что хотите удалить эту домашнюю работу?", "Удаление работы", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    DatabaseManager.DeleteHomework(homeworkId);
                    MessageBox.Show("Домашняя работа удалена");
                    Close();

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка при удалении домашней работы: " + ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        //check
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (action == "изменить")
            {
                string description = Description.Text;
                int languageId = Convert.ToInt32(((ComboBoxItem)cbLanguage.SelectedItem).Tag);
                string status = ((ComboBoxItem)cbStatus.SelectedItem).ToString().Trim();
                string solution = Solution.Text;


                HomeworkData.HomeworkDescription = description;
                HomeworkData.HomeworkSolution = solution;
                HomeworkData.LanguageId = languageId;
                HomeworkData.Status = status;



                MessageBoxResult result = MessageBox.Show("Вы уверены, что хотите изменить домашнее задание?", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    DatabaseManager.UpdateHomework(HomeworkData); 
                    MessageBox.Show("Домашнее задание успешно изменено.", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }

            if (action == "добавить")
            {
                HomeworksClass homework = new HomeworksClass();
                string description = Description.Text;
                int languageId = Convert.ToInt32(((ComboBoxItem)cbLanguage.SelectedItem).Tag);
                string solution = Solution.Text;
                string status = ((ComboBoxItem)cbStatus.SelectedItem).Content.ToString();


                homework.HomeworkDescription = description;
                homework.HomeworkSolution = solution;
                homework.LanguageId = languageId;
                homework.Status = status;
                homework.StudentId = CurrentUser.UserId;



                DatabaseManager.AddHomework(homework); 
                MessageBox.Show("Домашнее задание успешно добвлено.", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                Close();

            }


            
        }

        private void btnCloseWordDetails_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
