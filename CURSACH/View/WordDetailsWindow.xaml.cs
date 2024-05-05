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
using CURSACH.Classes;
using CURSACH.View;



namespace CURSACH.View
{
    /// <summary>
    /// Логика взаимодействия для WordDetailsWindow.xaml
    /// </summary>
    public partial class WordDetailsWindow : Window
    {
        public Words WordData { get; set; }
        public int currentLanguageId { get; set; }

        string action = "изменить";

        //check
        public WordDetailsWindow(int selectedLanguageId = 1)
        {
            InitializeComponent();
            action = "добавить";
            currentLanguageId = selectedLanguageId; 

            btnDeleteWord.Visibility = Visibility.Collapsed;
        }

        //check
        public WordDetailsWindow(Words word)
        {
            InitializeComponent();
            WordData = word;
            Word.Text = word.Word;
            Translation.Text = word.Translation;


            foreach (ComboBoxItem item in cbStatus.Items)
            {
                if (item.Content.ToString().Trim() == word.WordStatus.Trim())
                {
                    item.IsSelected = true;
                    break;
                }
            }

            if (!(word.StudentId >0))
            {
                btnDeleteWord.Visibility = Visibility.Collapsed;
                btnSave.Visibility = Visibility.Collapsed;

            }
            action = "изменить";
            DataContext = this; 
        }

       

        
        //check
        private void DeleteWord_Click(object sender, RoutedEventArgs e)
        {
            int wordId = WordData.WordId;
            MessageBoxResult result = MessageBox.Show("Вы уверены, что хотите удалить это слово?", "Удаление слова", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                try
                {                   
                    DatabaseManager.DeleteWord(wordId);
                    MessageBox.Show("Слово удалено");
                    Close();
                    
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка при удалении слова: " + ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        //check
        private void btnCloseDetails_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        //check
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {

            if (action == "изменить")
            {
                string word = Word.Text;
                string status = ((ComboBoxItem)cbStatus.SelectedItem)?.Content.ToString(); 
                string translation = Translation.Text;


                WordData.Word = word;
                WordData.Translation = translation;
                WordData.Translation = translation;
                WordData.StudentId = CurrentUser.UserId;
                WordData.LanguageId = currentLanguageId;
                WordData.WordStatus = status;

                MessageBoxResult result = MessageBox.Show("Вы уверены, что хотите изменить слово?", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    DatabaseManager.UpdateWord(WordData);
                    MessageBox.Show("Слово успешно изменено.", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }

            if (action == "добавить")
            {
                Words wordToSave = new Words();
                string word = Word.Text;
                string translation = Translation.Text;
                string status = ((ComboBoxItem)cbStatus.SelectedItem)?.Content.ToString(); 
                int studentId = CurrentUser.UserId;
                int languageId = currentLanguageId;

                wordToSave.Word = word;
                wordToSave.StudentId = studentId;
                wordToSave.Translation = translation;
                wordToSave.WordStatus = status;
                wordToSave.StudentId = studentId;
                wordToSave.LanguageId = languageId;

              
                DatabaseManager.AddWord(wordToSave); 
                MessageBox.Show("Слово успешно добвлено.", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                Close();
              
            }
        }

    }
}
