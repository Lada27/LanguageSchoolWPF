using CURSACH.Classes;
using CURSACH.View;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Windows;
using System.Windows.Controls;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace CURSACH
{
    internal class DatabaseManager
    {
        private static string connectionString = "data source=6.tcp.eu.ngrok.io, 16136; Database=Task_Manager; User=Lada_Rzchannikova; Password=Lada_005; TrustServerCertificate=True;";
        
        //check
        public static List<Words> GetNotStartedWordsByUserAndLanguageId(int userId, int languageId)
        {
            List<Words> words = new List<Words>();

            string query = $"SELECT * FROM Words WHERE StudentId = {userId} AND LTRIM(RTRIM(WordStatus)) = N'В ожидании' AND LanguageId = {languageId}";


            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {

                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Words word = new Words();
                        word.Word = reader.GetString(reader.GetOrdinal("Word"));
                        word.WordId = reader.GetInt32(reader.GetOrdinal("WordId"));
                        word.WordStatus = reader.GetString(reader.GetOrdinal("WordStatus"));
                        word.LanguageId = reader.GetInt32(reader.GetOrdinal("LanguageId"));
                        word.CreatedDate = reader.GetDateTime(reader.GetOrdinal("CreatedDate"));
                        word.StudentId = reader.GetInt32(reader.GetOrdinal("StudentId"));
                        word.Translation = reader.GetString(reader.GetOrdinal("Translation"));

                        
                        words.Add(word);
                    }
                    reader.Close();
                }
            }

            return words;
        }

        //check
        internal static List<Words> GetDoneWordsByUserAndLanguageId(int userId, int languageId)
        {
            List<Words> words = new List<Words>();

            string query = $"SELECT * FROM Words WHERE StudentId = {userId} AND LTRIM(RTRIM(WordStatus)) = N'Знаю' AND LanguageId = {languageId}";


            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {

                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Words word = new Words();
                        word.Word = reader.GetString(reader.GetOrdinal("Word"));
                        word.WordId = reader.GetInt32(reader.GetOrdinal("WordId"));
                        word.WordStatus = reader.GetString(reader.GetOrdinal("WordStatus"));
                        word.LanguageId = reader.GetInt32(reader.GetOrdinal("LanguageId"));
                        word.CreatedDate = reader.GetDateTime(reader.GetOrdinal("CreatedDate"));
                        word.StudentId = reader.GetInt32(reader.GetOrdinal("StudentId"));
                        word.Translation = reader.GetString(reader.GetOrdinal("Translation"));

                        words.Add(word);
                    }
                    reader.Close();
                }
            }

            return words;
        }

        //check
        public static List<Words> GetInProcessWordsByUserAndLanguageId(int userId, int languageId)
        {
            List<Words> words = new List<Words>();

            string query = $"SELECT * FROM Words WHERE StudentId = {userId} AND LTRIM(RTRIM(WordStatus)) = N'Учу' AND LanguageId = {languageId}";


            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {

                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Words word = new Words();
                        word.Word = reader.GetString(reader.GetOrdinal("Word"));
                        word.WordId = reader.GetInt32(reader.GetOrdinal("WordId"));
                        word.WordStatus = reader.GetString(reader.GetOrdinal("WordStatus"));
                        word.LanguageId = reader.GetInt32(reader.GetOrdinal("LanguageId"));
                        word.CreatedDate = reader.GetDateTime(reader.GetOrdinal("CreatedDate"));
                        word.StudentId = reader.GetInt32(reader.GetOrdinal("StudentId"));
                        word.Translation = reader.GetString(reader.GetOrdinal("Translation"));

                        words.Add(word);
                    }
                    reader.Close();
                }
            }

            return words;
        }

        //check
        internal static Users GetUserById(int studentId)
        {
            Users user = null;

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT * FROM Students WHERE StudentId = @StudentId";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@StudentId", studentId);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                user = new Users();
                                user.Id = reader.GetInt32(reader.GetOrdinal("StudentId"));
                                user.Email = reader.GetString(reader.GetOrdinal("StudentEmail"));
                                user.Name = reader.GetString(reader.GetOrdinal("StudentName"));
                                user.Password = reader.GetString(reader.GetOrdinal("StudentPassword"));
                                user.Phone = reader.IsDBNull(reader.GetOrdinal("StudentPhone")) ? null : reader.GetString(reader.GetOrdinal("StudentPhone"));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка при получении студента из базы данных: " + ex.Message);
            }

            return user;
        }

        // check
        public static int AuthenticateUser(string email, string password)
        {
            int userId = -1; 

            string query = $"SELECT StudentId FROM Students WHERE StudentEmail = N'{email}' AND StudentPassword = N'{password}'";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {                  

                    connection.Open();
                    object result = command.ExecuteScalar(); 

                    if (result != null && result != DBNull.Value)
                    {
                        userId = Convert.ToInt32(result); 
                    }
                }
            }

            return userId; 
        }
       
        //check
        public static void DeleteWord(int wordId)
        {
            string query = $"DELETE FROM Words WHERE WordId = {wordId}";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        //check
        public static void UpdateWord(Words word)
        {
            string query = "UPDATE Words SET Word=@Word, WordStatus = @WordStatus, Translation = @Translation WHERE WordId = @WordId";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@WordStatus", word.WordStatus);
                    command.Parameters.AddWithValue("@Translation", word.Translation);
                    command.Parameters.AddWithValue("@Word", word.Word);
                    command.Parameters.AddWithValue("@WordId", word.WordId);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        //check
        internal static void AddWord(Words word)
        {
            string query = $"INSERT INTO Words (LanguageId, Word, StudentId, WordStatus, Translation) VALUES (@LanguageId, @Word, @StudentId, @WordStatus, @Translation)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@LanguageId", word.LanguageId);
                    command.Parameters.AddWithValue("@StudentId", word.StudentId);
                    command.Parameters.AddWithValue("@WordStatus", word.WordStatus);
                    command.Parameters.AddWithValue("@Translation", word.Translation);
                    command.Parameters.AddWithValue("@Word", word.Word);
                    command.Parameters.AddWithValue("@WordId", word.WordId);

                    try
                    {
                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                    catch (SqlException ex)
                    {
                        Console.WriteLine("Ошибка при добавлении слова в базу данных: " + ex.Message);
                    }
                }
            }
        }

        //check
        internal static bool UpdateUserProfile(int userId, string newName, string newEmail, string newPassword, string newPhone)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "UPDATE Students SET StudentName = @NewName, StudentEmail = @NewEmail, StudentPassword = @NewPassword, StudentPhone = @NewPhone WHERE StudentId = @UserId";
                    SqlCommand command = new SqlCommand(query, connection);

                    command.Parameters.AddWithValue("@NewName", newName);
                    command.Parameters.AddWithValue("@NewEmail", newEmail);
                    command.Parameters.AddWithValue("@NewPassword", newPassword);
                    command.Parameters.AddWithValue("@NewPhone", newPhone);
                    command.Parameters.AddWithValue("@UserId", userId);

                    int rowsAffected = command.ExecuteNonQuery();

                    return rowsAffected > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка при изменении данных профиля: " + ex.Message);
                return false;
            }
        }

        //check
        public static bool AddUser(string name, string email, string phone, string password)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "INSERT INTO Students (StudentName, StudentEmail, StudentPhone, StudentPassword) VALUES (@Name, @Email, @Phone, @Password)";
                    SqlCommand command = new SqlCommand(query, connection);

                    command.Parameters.AddWithValue("@Name", name);
                    command.Parameters.AddWithValue("@Email", email);
                    command.Parameters.AddWithValue("@Phone", phone);
                    command.Parameters.AddWithValue("@Password", password);

                    int rowsAffected = command.ExecuteNonQuery();

                    return rowsAffected > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка при добавлении пользователя" + ex.Message);
                return false;
            }
        }
  
        //лишний
        public static List<Users> LoadAllUsers()
        {
            List<Users> users = new List<Users>();

            string query = "SELECT * FROM Students";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        Users user = new Users();
                        user.Id = reader.GetInt32(reader.GetOrdinal("StudentId"));
                        user.Name = reader.GetString(reader.GetOrdinal("StudentName"));
                        user.Email = reader.GetString(reader.GetOrdinal("StudentEmail"));
                        user.Password = reader.GetString(reader.GetOrdinal("StudentPassword"));
                        user.Phone = reader.GetString(reader.GetOrdinal("StudentPhone"));

                        users.Add(user);
                    }
                }
            }

            return users;
        }

        //check
        internal static List<LanguagesClass> LoadAllLanguages()
        {
            List<LanguagesClass> languages = new List<LanguagesClass>();

            string query = "SELECT * FROM Languages";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        LanguagesClass language = new LanguagesClass();
                        language.LanguageId = reader.GetInt32(reader.GetOrdinal("LanguageId"));
                        language.LanguageName = reader.GetString(reader.GetOrdinal("LanguageName"));

                        languages.Add(language);
                    }
                }
            }

            return languages;
        }

        //check
        internal static LanguagesClass GetLanguageById(int languageId)
        {
            LanguagesClass language = null;

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT * FROM Languages WHERE LanguageId = @languageId";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@LanguageId", languageId);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                language = new LanguagesClass();
                                language.LanguageId = reader.GetInt32(reader.GetOrdinal("LanguageId"));
                                language.LanguageName = reader.GetString(reader.GetOrdinal("LanguageName"));
                                
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка при получении языка из базы данных: " + ex.Message);
            }

            return language;
        }

        //check
        internal static int GetNumberOfWordsByLanguageId(int languageId)
        {
            int numberOfWords = 0;

            string query = $"SELECT COUNT(*) FROM Words WHERE LanguageId = {languageId} AND StudentId IS NULL";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    numberOfWords = (int)command.ExecuteScalar();
                }
            }

            return numberOfWords;
        }

        //check
        internal static List<Words> GetLanguageWordsByLanguageId(int languageId)
        {
            List<Words> languageWords = new List<Words>();

            string query = $"SELECT * FROM Words WHERE LanguageId = {languageId} AND StudentId IS NULL";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        Words word = new Words();
                        word.WordId = reader.GetInt32(reader.GetOrdinal("WordId"));
                        word.Word = reader.GetString(reader.GetOrdinal("Word"));
                        word.Translation = reader.GetString(reader.GetOrdinal("Translation"));
                        word.CreatedDate = reader.GetDateTime(reader.GetOrdinal("CreatedDate"));
                        word.WordStatus = reader.GetString(reader.GetOrdinal("WordStatus"));
                        word.LanguageId = reader.GetInt32(reader.GetOrdinal("LanguageId"));

                        languageWords.Add(word);
                    }

                    reader.Close();
                }
            }

            return languageWords;
        }

        //check
        internal static List<HomeworksClass> GetAllHomeworks()
        {
            List<HomeworksClass> homeworks = new List<HomeworksClass>();

            string query = $"SELECT * FROM Homeworks where StudentId = {CurrentUser.UserId}";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        HomeworksClass homework = new HomeworksClass();
                        homework.HomeworkId = reader.GetInt32(reader.GetOrdinal("HomeworkId"));
                        homework.HomeworkDescription = reader.GetString(reader.GetOrdinal("HomeworkDescription"));
                        homework.HomeworkSolution = reader.GetString(reader.GetOrdinal("HomeworkSolution"));
                        homework.CreatedDate = reader.GetDateTime(reader.GetOrdinal("CreatedDate"));
                        homework.StudentId = reader.GetInt32(reader.GetOrdinal("StudentId"));
                        homework.Status = reader.GetString(reader.GetOrdinal("Status"));
                        homework.LanguageId = reader.GetInt32(reader.GetOrdinal("LanguageId"));

                        homeworks.Add(homework);
                    }

                    reader.Close();
                }
            }

            return homeworks;
        
      }

        //check
        internal static List<LanguagesClass> getUserLanguages()
        {
            List<LanguagesClass> languages = new List<LanguagesClass>();

            string query = $"SELECT * FROM Languages WHERE LanguageId IN (SELECT LanguageId FROM StudentLanguages WHERE StudentId = {CurrentUser.UserId})";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        LanguagesClass language = new LanguagesClass();
                        language.LanguageId = reader.GetInt32(reader.GetOrdinal("LanguageId"));
                        language.LanguageName = reader.GetString(reader.GetOrdinal("LanguageName"));

                        languages.Add(language);
                    }

                    reader.Close();
                }
            }

            return languages;
        }

        //check
        internal static void DeleteHomework(object homeworkId)
        {
            string query = $"DELETE FROM Homeworks WHERE HomeworkId = {homeworkId}";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        //check
        internal static void UpdateHomework(HomeworksClass homeworkData)
        {
            string query = "UPDATE Homeworks SET HomeworkDescription=@HomeworkDescription, HomeworkSolution=@HomeworkSolution, Status = @Status, LanguageId = @LanguageId WHERE HomeworkId = @HomeworkId";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@HomeworkDescription", homeworkData.HomeworkDescription);
                    command.Parameters.AddWithValue("@HomeworkSolution", homeworkData.HomeworkSolution);
                    command.Parameters.AddWithValue("@Status", homeworkData.Status);
                    command.Parameters.AddWithValue("@LanguageId", homeworkData.LanguageId);
                    command.Parameters.AddWithValue("@HomeworkId", homeworkData.HomeworkId);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        //check
        internal static void AddHomework(HomeworksClass homework)
        {
            string query = $"INSERT INTO Homeworks (HomeworkDescription, HomeworkSolution, StudentId, Status, LanguageId) VALUES (@HomeworkDescription, @HomeworkSolution, @StudentId, @Status, @LanguageId)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@HomeworkDescription", homework.HomeworkDescription);
                    command.Parameters.AddWithValue("@HomeworkSolution", homework.HomeworkSolution);
                    command.Parameters.AddWithValue("@StudentId", homework.StudentId);
                    command.Parameters.AddWithValue("@Status", homework.Status);
                    command.Parameters.AddWithValue("@LanguageId", homework.LanguageId);


                    try
                    {
                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                    catch (SqlException ex)
                    {
                        Console.WriteLine("Ошибка при добавлении домашнего задания в базу данных: " + ex.Message);
                    }
                }
            }
        }

        //check
        internal static int isInLanguageList(int selectedLanguageId, int studentId)
        {
            int studentLanguageId = -1; 

            string query = "SELECT Id FROM StudentLanguages WHERE LanguageId = @LanguageId  AND StudentId = @StudentId";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@LanguageId", selectedLanguageId);
                    command.Parameters.AddWithValue("@StudentId", studentId);


                    try
                    {
                        connection.Open();
                        object result = command.ExecuteScalar(); 

                        if (result != null && result != DBNull.Value)
                        {
                            studentLanguageId = Convert.ToInt32(result); 
                        }
                    }
                    catch (SqlException ex)
                    {
                        MessageBox.Show("Ошибка при поиске ID языка в списке языков пользователя в базе данных: " + ex.Message);
                    }
                }
            }

            return studentLanguageId; 

        }

        //check
        internal static void AddLanguageToUserList(int selectedLanguageId)
        {
            string query = $"INSERT INTO StudentLanguages (LanguageId, StudentId) VALUES (@LanguageId, @StudentId)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@LanguageId", selectedLanguageId);
                    command.Parameters.AddWithValue("@StudentId", CurrentUser.UserId);

                    try
                    {
                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                    catch (SqlException ex)
                    {
                        Console.WriteLine("Ошибка при добавлении языка в cписок языков пользователя: " + ex.Message);
                    }
                }
            }
        }

        //check
        internal static List<HomeworksClass> GetHomeworksByStatus(string selectedStatus)
        {
            List<HomeworksClass> homeworks = new List<HomeworksClass>();

            string query = $"SELECT * FROM Homeworks where StudentId = {CurrentUser.UserId} AND  LTRIM(RTRIM(Status)) = N'{selectedStatus}'";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        HomeworksClass homework = new HomeworksClass();
                        homework.HomeworkId = reader.GetInt32(reader.GetOrdinal("HomeworkId"));
                        homework.HomeworkDescription = reader.GetString(reader.GetOrdinal("HomeworkDescription"));
                        homework.HomeworkSolution = reader.GetString(reader.GetOrdinal("HomeworkSolution"));
                        homework.CreatedDate = reader.GetDateTime(reader.GetOrdinal("CreatedDate"));
                        homework.StudentId = reader.GetInt32(reader.GetOrdinal("StudentId"));
                        homework.Status = reader.GetString(reader.GetOrdinal("Status"));
                        homework.LanguageId = reader.GetInt32(reader.GetOrdinal("LanguageId"));

                        homeworks.Add(homework);
                    }

                    reader.Close();
                }
            }

            return homeworks;
        }

        //check
        internal static List<HomeworksClass> GetHomeworksByStatusAndLanguage(string selectedStatus, int selectedLanguageId)
        {
            List<HomeworksClass> homeworks = new List<HomeworksClass>();

            string query = $"SELECT * FROM Homeworks where StudentId = {CurrentUser.UserId} AND  LTRIM(RTRIM(Status)) = N'{selectedStatus}' AND LanguageId = {selectedLanguageId}";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        HomeworksClass homework = new HomeworksClass();
                        homework.HomeworkId = reader.GetInt32(reader.GetOrdinal("HomeworkId"));
                        homework.HomeworkDescription = reader.GetString(reader.GetOrdinal("HomeworkDescription"));
                        homework.HomeworkSolution = reader.GetString(reader.GetOrdinal("HomeworkSolution"));
                        homework.CreatedDate = reader.GetDateTime(reader.GetOrdinal("CreatedDate"));
                        homework.StudentId = reader.GetInt32(reader.GetOrdinal("StudentId"));
                        homework.Status = reader.GetString(reader.GetOrdinal("Status"));
                        homework.LanguageId = reader.GetInt32(reader.GetOrdinal("LanguageId"));

                        homeworks.Add(homework);
                    }

                    reader.Close();
                }
            }

            return homeworks;
        }

        //check
        internal static List<HomeworksClass> GetHomeworksByLanguageId(int selectedLanguageId)
        {
            List<HomeworksClass> homeworks = new List<HomeworksClass>();

            string query = $"SELECT * FROM Homeworks where StudentId = {CurrentUser.UserId} AND LanguageId = {selectedLanguageId}";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        HomeworksClass homework = new HomeworksClass();
                        homework.HomeworkId = reader.GetInt32(reader.GetOrdinal("HomeworkId"));
                        homework.HomeworkDescription = reader.GetString(reader.GetOrdinal("HomeworkDescription"));
                        homework.HomeworkSolution = reader.GetString(reader.GetOrdinal("HomeworkSolution"));
                        homework.CreatedDate = reader.GetDateTime(reader.GetOrdinal("CreatedDate"));
                        homework.StudentId = reader.GetInt32(reader.GetOrdinal("StudentId"));
                        homework.Status = reader.GetString(reader.GetOrdinal("Status"));
                        homework.LanguageId = reader.GetInt32(reader.GetOrdinal("LanguageId"));

                        homeworks.Add(homework);
                    }

                    reader.Close();
                }
            }

            return homeworks;
        }

        public static void AssignWordsToCurrentUser(int languageId)
        {
            // Получаем все слова определенного языка, которые еще не принадлежат никакому пользователю
            string query = $@"
        SELECT WordId, Word, Translation, WordStatus
        FROM Words
        WHERE LanguageId = {languageId}
        AND StudentId IS NULL";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        int wordId = reader.GetInt32(0);
                        string word = reader.GetString(1);
                        string translation = reader.GetString(2);
                        string wordStatus = reader.GetString(3);

                        // Добавляем копию слова для текущего пользователя
                        AddWordForCurrentUser(word, translation, wordStatus, languageId);
                    }
                }
            }
        }

        public static void AddWordForCurrentUser(string word, string translation, string wordStatus, int languageId)
        {
            int currentUserId = CurrentUser.UserId;

            string insertQuery = $@"
        INSERT INTO Words (Word, Translation, WordStatus, StudentId, LanguageId)
        VALUES (N'{word}', N'{translation}', N'{wordStatus}', {currentUserId}, {languageId})";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(insertQuery, connection))
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

    }
}
 
    
