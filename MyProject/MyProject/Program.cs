//using MyProject.Interface;
using MyProject.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.Contracts;
using System.Diagnostics.Eventing.Reader;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace MyProject
{
    internal class Program
    {

        static void Main(string[] args)
        {
            VictorinaRegister();
        }

        private static void VictorinaRegister()
        {
            List<UserLog> logList = new List<UserLog>();
            List<UserLog> userDelete = new List<UserLog>();
            UserLog user = new UserLog();
            string FileUserLog = "Users.txt";
            int UserChek = 0;
            bool checkBool = true;
            bool isTrue = true;
            while (checkBool)/////////////////////////////////////////////////////////////
            {
                Console.Write("Ви зареєстрованi?\nЗареєстрований/на(1) не зареєстрований/на(2) -> ");
                string checkUser = Console.ReadLine();
                UserChek = Int32.Parse(checkUser);
                Console.Clear();
                if (UserChek == 1)
                {
                    
                    while (isTrue)
                    {
                        Console.Write("Введiть логiн -> ");
                        string userlog = Console.ReadLine();
                        bool result = CheckUserLogin(FileUserLog,userlog);
                        if (result == true)
                        {
                            DeleteUserLogin(FileUserLog,userDelete,user);
                            Console.Write("Введiть пароль -> ");
                            string userPass = Console.ReadLine();
                            Console.WriteLine();
                            result = CheckUserLogin(FileUserLog, userPass);
                            if (result == false)
                            {
                                int Count = 0;
                                while (Count <= 5)
                                {
                                    Console.Clear();
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine($"Пароль неправильний!\nУ вас залишилось {5 - Count} спроб!");
                                    Console.ResetColor();
                                    Console.Write("Введiть пароль -> ");
                                    userPass = Console.ReadLine();
                                    result = CheckUserLogin(FileUserLog, userPass);
                                    if (result == true)
                                    {
                                        MenuToVictorine(userlog, FileUserLog,userPass);
                                        isTrue = false;
                                        break; 
                                    }
                                    else
                                    {
                                        Count++;
                                    }
                                    if (Count == 5)
                                    {
                                        Console.WriteLine("Ваш акаунт заблоковано! Зареєструйте новий");
                                        userDelete.RemoveAll( users => users.Login.Contains(userlog));
                                        using (FileStream fileStream = new FileStream(FileUserLog, FileMode.Create, FileAccess.Write))
                                        {
                                            using (StreamWriter writer = new StreamWriter(fileStream))
                                            {
                                                foreach (var usersdel in userDelete)
                                                {
                                                    writer.WriteLine(usersdel.ToString());
                                                }
                                            }
                                        }
                                        isTrue = false;
                                        break;
                                    }
                                }
                            }
                            else
                            {
                                MenuToVictorine(userlog, FileUserLog,userPass);
                                isTrue = false;
                                break;
                            }
                        }
                        else if (result == false)
                        {
                            int Count = 0;
                            bool trueOrFalse = true;
                            while (trueOrFalse)
                            {
                                Console.Clear();
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine($"Логiн неправильний!");
                                Console.ResetColor();
                                Console.Write("Введiть логiн -> ");
                                string otherLog = Console.ReadLine();
                                result = CheckUserLogin(FileUserLog, otherLog);
                                if (result == true)
                                {
                                    DeleteUserLogin(FileUserLog, userDelete, user);
                                    Console.Write("Введiть пароль -> ");
                                    string userPass = Console.ReadLine();
                                    Console.WriteLine();
                                    result = CheckUserLogin(FileUserLog, userPass);
                                    if (result == false)
                                    {
                                        int Counts = 0;
                                        while (Counts != 5)
                                        {
                                            Console.Clear();
                                            Console.ForegroundColor = ConsoleColor.Red;
                                            Console.WriteLine($"Пароль неправильний!\nУ вас залишилось {5 - Counts} спроб!");
                                            Console.ResetColor();
                                            Console.Write("Введiть пароль -> ");
                                            userPass = Console.ReadLine();
                                            result = CheckUserLogin(FileUserLog, userPass);
                                            if (result == true)
                                            {
                                                MenuToVictorine(otherLog, FileUserLog,userPass);
                                                isTrue = false;
                                                trueOrFalse = false;
                                                break;
                                            }
                                            else
                                            {
                                                Counts++;
                                            }
                                            if (Counts == 5)
                                            {
                                                Console.WriteLine("Ваш акаунт заблоковано! Зареєструйте новий");
                                                userDelete.RemoveAll(users => users.Login.Contains(userlog));
                                                using (FileStream fileStream = new FileStream(FileUserLog, FileMode.Create, FileAccess.Write))
                                                {
                                                    using (StreamWriter writer = new StreamWriter(fileStream))
                                                    {
                                                        foreach (var usersdel in userDelete)
                                                        {
                                                            writer.WriteLine(usersdel.ToString());
                                                        }
                                                    }
                                                }
                                                isTrue = false;
                                                trueOrFalse = false;
                                            }
                                        }
                                    }
                                    else if (result == true)
                                    {
                                        MenuToVictorine(otherLog, FileUserLog, userPass);
                                        isTrue = false;
                                        trueOrFalse = false;
                                        break;
                                        
                                    }
                                }
                                else
                                {
                                    Count++;
                                }
                                if (Count == 5)
                                {
                                    Console.Write("Зареєструйте новий акаунт(1) або перезапустiть програму i попробуйте увiйти в акаунт знову(2) -> ");
                                    string regOrRestart = Console.ReadLine();
                                    int userChoice = Int32.Parse(regOrRestart);
                                    if(userChoice == 1)
                                    {
                                        Console.Clear();
                                        RegisterUser(logList,FileUserLog,user, userDelete);
                                        break;
                                    }
                                    else if(userChoice == 2)
                                    {
                                        isTrue = false;
                                        break;
                                    }
                                }
                            }
                        }
                    }
                    checkBool = false;
                }
                else if (UserChek == 2)
                {
                    RegisterUser(logList, FileUserLog, user, userDelete);
                    checkBool = false;
                }
                else
                {
                    Console.Write("Введiть (1) або (2)");
                }
            }
        }

        private static string MenuToVictorine(string user, string path,string password)
        {
            Console.Clear();
            string FileUserResultAfterTest = $"{user}.txt";
            string FileTopRatingUserGeography = "RatingUserGeography.txt";
            string FileTopRatingUserChess = "RatingUserChess.txt";
            string FileTopRatingUserComputerGames = "RatingUserComputerGames.txt";
            string FileTopRatingUserHistoryOfUkraine = "RatingUserHistoryOfUkraine.txt";
            string FileTopRatingUserShevchenko = "RatingUserShevchenko.txt";
            string FileQuestion = "Question.txt";
            List<string> questionList = new List<string>();
            List<string> variantList = new List<string>();
            List<int> respondList = new List<int>();
            List<UserLog> listUserRating = new List<UserLog>();
            List<UserRating> listTopUser = new List<UserRating>();
            UserRating userRating = new UserRating();
            UserLog userRename = new UserLog();

            bool isTrue = true;
            while (isTrue)
            {
                Console.WriteLine();
                Console.Write("Почати нову вiкторину(1)\nПереглянути минулi результати(2)\nПереглянути топ-20 з вибраної вiкторини(3)\nЗмiнити налаштування(4)\nДобавити свою вiкторину(5)\nВихiд(6) -> ");
                string userChoiceMenu = Console.ReadLine();
                int ChoiceMenu = Int32.Parse(userChoiceMenu);
                switch(ChoiceMenu)
                {
                    case 1:
                        Console.Clear();
                        Console.Write("Виберiть тему вiкторини:\nГеографiя(1)\nІсторiя України(2)\nШахмати(3)\nБiографiя Тараса Шевченка(4)\nКомп'ютернi iгри(5) -> ");
                        string choiceForVictorine = Console.ReadLine();
                        int choiceVictorine = Int32.Parse(choiceForVictorine);
                        switch (choiceVictorine)
                        {
                            case 1: 
                                int resultTestGeography = GeographyVictorine();
                                if (File.Exists(FileUserResultAfterTest))
                                {
                                    using (FileStream fileStream = new FileStream(FileUserResultAfterTest, FileMode.Append, FileAccess.Write))
                                    {
                                        using (StreamWriter writer = new StreamWriter(fileStream))
                                        {
                                            writer.WriteLine($"Результат з вiкторини по темi 'Географiя' - {resultTestGeography} правильних вiдповiдей");
                                        }
                                    }
                                    using (FileStream fileStream = new FileStream(FileTopRatingUserGeography, FileMode.Append, FileAccess.Write))
                                    {
                                        using (StreamWriter writer = new StreamWriter(fileStream))
                                        {
                                            writer.WriteLine($"{user} {resultTestGeography} правильних вiдповiдей");
                                        }
                                    }
                                }
                                else
                                {
                                    using (FileStream fileStream = new FileStream(FileUserResultAfterTest, FileMode.OpenOrCreate, FileAccess.Write))
                                    {
                                        using (StreamWriter writer = new StreamWriter(fileStream))
                                        {
                                            writer.WriteLine($"Результат з вiкторини по темi 'Географiя' - {resultTestGeography} правильних вiдповiдей");
                                        }
                                    }
                                    using (FileStream fileStream = new FileStream(FileTopRatingUserGeography, FileMode.Append, FileAccess.Write))
                                    {
                                        using (StreamWriter writer = new StreamWriter(fileStream))
                                        {
                                            writer.WriteLine($"{user} {resultTestGeography} правильних вiдповiдей");
                                        }
                                    }
                                }
                                break;
                            case 2: 
                                int resultTestHistoryOfUkrain = HistoryOfUkraineVictorine();
                                if (File.Exists(FileUserResultAfterTest))
                                {
                                    using (FileStream fileStream = new FileStream(FileUserResultAfterTest, FileMode.Append, FileAccess.Write))
                                    {
                                        using (StreamWriter writer = new StreamWriter(fileStream))
                                        {
                                            writer.WriteLine($"Результат з вiкторини по темi 'Історiя України' - {resultTestHistoryOfUkrain} правильних вiдповiдей");
                                        }
                                    }
                                    using (FileStream fileStream = new FileStream(FileTopRatingUserHistoryOfUkraine, FileMode.Append, FileAccess.Write))
                                    {
                                        using (StreamWriter writer = new StreamWriter(fileStream))
                                        {
                                            writer.WriteLine($"{user} {resultTestHistoryOfUkrain} правильних вiдповiдей");
                                        }
                                    }
                                }
                                else
                                {
                                    using (FileStream fileStream = new FileStream(FileUserResultAfterTest, FileMode.OpenOrCreate, FileAccess.Write))
                                    {
                                        using (StreamWriter writer = new StreamWriter(fileStream))
                                        {
                                            writer.WriteLine($"Результат з вiкторини по темi 'Історiя України' - {resultTestHistoryOfUkrain} правильних вiдповiдей");
                                        }
                                    }
                                }
                                break;
                            case 3:
                                int resultTestChess = ChessVictorine();
                                if (File.Exists(FileUserResultAfterTest))
                                {
                                    using (FileStream fileStream = new FileStream(FileUserResultAfterTest, FileMode.Append, FileAccess.Write))
                                    {
                                        using (StreamWriter writer = new StreamWriter(fileStream))
                                        {
                                            writer.WriteLine($"Результат з вiкторини по темi 'Шахмати' - {resultTestChess} правильних вiдповiдей");
                                        }
                                    }
                                    using (FileStream fileStream = new FileStream(FileTopRatingUserChess, FileMode.Append, FileAccess.Write))
                                    {
                                        using (StreamWriter writer = new StreamWriter(fileStream))
                                        {
                                            writer.WriteLine($"{user} {resultTestChess} правильних вiдповiдей");
                                        }
                                    }
                                }
                                else
                                {
                                    using (FileStream fileStream = new FileStream(FileUserResultAfterTest, FileMode.OpenOrCreate, FileAccess.Write))
                                    {
                                        using (StreamWriter writer = new StreamWriter(fileStream))
                                        {
                                            writer.WriteLine($"Результат з вiкторини по темi 'Шахмати' - {resultTestChess} правильних вiдповiдей");
                                        }
                                    }
                                }
                                break;
                            case 4:
                                int resultTestShevchenko = ShevchenkoVictorine();
                                if (File.Exists(FileUserResultAfterTest))
                                {
                                    using (FileStream fileStream = new FileStream(FileUserResultAfterTest, FileMode.Append, FileAccess.Write))
                                    {
                                        using (StreamWriter writer = new StreamWriter(fileStream))
                                        {
                                            writer.WriteLine($"Результат з вiкторини по темi 'Бiографiя Тараса Шевченка' - {resultTestShevchenko} правильних вiдповiдей");
                                        }
                                    }
                                    using (FileStream fileStream = new FileStream(FileTopRatingUserShevchenko, FileMode.Append, FileAccess.Write))
                                    {
                                        using (StreamWriter writer = new StreamWriter(fileStream))
                                        {
                                            writer.WriteLine($"{user} {resultTestShevchenko} правильних вiдповiдей");
                                        }
                                    }
                                }
                                else
                                {
                                    using (FileStream fileStream = new FileStream(FileUserResultAfterTest, FileMode.OpenOrCreate, FileAccess.Write))
                                    {
                                        using (StreamWriter writer = new StreamWriter(fileStream))
                                        {
                                            writer.WriteLine($"Результат з вікторини по темі 'Біографія Тараса Шевченка' - {resultTestShevchenko} правильних вiдповiдей");
                                        }
                                    }
                                }
                                break;
                            case 5:
                                int resultTestComputerGames = ComputerGamesVictorine();
                                if (File.Exists(FileUserResultAfterTest))
                                {
                                    using (FileStream fileStream = new FileStream(FileUserResultAfterTest, FileMode.Append, FileAccess.Write))
                                    {
                                        using (StreamWriter writer = new StreamWriter(fileStream))
                                        {
                                            writer.WriteLine($"Результат з вiкторини по темi 'Комп'ютернi iгри' - {resultTestComputerGames} правильних вiдповiдей");
                                        }
                                    }
                                    using (FileStream fileStream = new FileStream(FileTopRatingUserComputerGames, FileMode.Append, FileAccess.Write))
                                    {
                                        using (StreamWriter writer = new StreamWriter(fileStream))
                                        {
                                            writer.WriteLine($"{user} {resultTestComputerGames} правильних вiдповiдей");
                                        }
                                    }
                                }
                                else
                                {
                                    using (FileStream fileStream = new FileStream(FileUserResultAfterTest, FileMode.OpenOrCreate, FileAccess.Write))
                                    {
                                        using (StreamWriter writer = new StreamWriter(fileStream))
                                        {
                                            writer.WriteLine($"Результат з вiкторини по темi 'Комп'ютернi iгри' - {resultTestComputerGames} правильних вiдповiдей");
                                        }
                                    }
                                }
                                break;
                            default:Console.WriteLine("Такого варiанту немає!");break;
                        }
                        break;
                    case 2:
                        Console.Clear();
                        Console.WriteLine("Ось вашi результати з минулих вiкторин -> ");
                        string resultTestRead = ResultTestUserRead(FileUserResultAfterTest);
                        Console.WriteLine(resultTestRead);
                        break;
                    case 3:
                        Console.Clear();
                        Console.Write("Виберiть по якiй темi переглянути Топ-20\nГеографiя(1)\nІсторiя України(2)\nШахмати(3)\nБiографiя Тараса Шевченка(4)\nКомп'ютернi iгри(5) -> ");
                        string choiceForRating = Console.ReadLine();
                        int userChoiceRating = Int32.Parse(choiceForRating);
                        switch (userChoiceRating)
                        {
                            case 1:
                                string temp = "";
                                using(FileStream fileStream = new FileStream(FileTopRatingUserGeography, FileMode.Open, FileAccess.Read))
                                {
                                    using(StreamReader reader = new StreamReader(fileStream))
                                    {
                                        temp = "";
                                        while(reader.Peek() > 0)
                                        {
                                            string[] strRating = new string[] { };
                                            temp += reader.ReadLine();
                                            strRating = temp.Split(' ');
                                            userRating.Name = strRating[0];
                                            userRating.Grade = Int32.Parse(strRating[1]);
                                            listTopUser.Add(new UserRating(userRating.Name, userRating.Grade));
                                        }
                                    }
                                }
                                listTopUser.Sort((x, y) => y.Grade.CompareTo(x.Grade));
                                using (FileStream fileStream1 = new FileStream(FileTopRatingUserGeography, FileMode.OpenOrCreate, FileAccess.Write))
                                {
                                    using(StreamWriter writer = new StreamWriter(fileStream1))
                                    {
                                        int count = 1;
                                        foreach(var item in listTopUser)
                                        {
                                            writer.WriteLine($"{count}. {item.Name} - {item.Grade} правильних вiдповiдей");
                                            count += 1;
                                        }
                                    }
                                }
                                using(FileStream stream = new FileStream(FileTopRatingUserGeography, FileMode.Open, FileAccess.Read))
                                {
                                    using(StreamReader reader = new StreamReader(stream))
                                    {
                                        temp = "";
                                        while(reader.Peek() > 0)
                                        {
                                            temp += $"{reader.ReadLine()}\n";
                                        }
                                    }
                                }
                                Console.WriteLine(temp);
                                break;
                            case 2:
                                using (FileStream fileStream = new FileStream(FileTopRatingUserHistoryOfUkraine, FileMode.Open, FileAccess.Read))
                                {
                                    using (StreamReader reader = new StreamReader(fileStream))
                                    {
                                        temp = "";
                                        while (reader.Peek() > 0)
                                        {
                                            string[] strRating = new string[] { };
                                            temp += reader.ReadLine();
                                            strRating = temp.Split(' ');
                                            userRating.Name = strRating[0];
                                            //userRating.Grade = (strRating[1]);
                                            listTopUser.Add(new UserRating(userRating.Name, userRating.Grade));
                                        }
                                    }
                                }
                                listTopUser.Sort((x, y) => y.Grade.CompareTo(x.Grade));
                                using (FileStream stream = new FileStream(FileTopRatingUserHistoryOfUkraine, FileMode.Open, FileAccess.Read))
                                {
                                    using (StreamReader reader = new StreamReader(stream))
                                    {
                                        temp = "";
                                        int count = 0;
                                        foreach (var users in listTopUser)
                                        {
                                            if (count == 20)
                                            {
                                                break;
                                            }
                                            Console.WriteLine($"{count}.{users.Name} - {users.Grade} правильних вiдповiдей");
                                            count += 1;
                                        }
                                    }
                                }
                                break;
                            case 3:
                                using (FileStream fileStream = new FileStream(FileTopRatingUserChess, FileMode.Open, FileAccess.Read))
                                {
                                    using (StreamReader reader = new StreamReader(fileStream))
                                    {
                                        temp = "";
                                        while (reader.Peek() > 0)
                                        {
                                            string[] strRating = new string[] { };
                                            temp += reader.ReadLine();
                                            strRating = temp.Split(' ');
                                            userRating.Name = strRating[0];
                                            //userRating.Grade = (strRating[1]);
                                            listTopUser.Add(new UserRating(userRating.Name, userRating.Grade));
                                        }
                                    }
                                }
                                listTopUser.Sort((x, y) => y.Grade.CompareTo(x.Grade));
                                using (FileStream stream = new FileStream(FileTopRatingUserChess, FileMode.Open, FileAccess.Read))
                                {
                                    using (StreamReader reader = new StreamReader(stream))
                                    {
                                        temp = "";
                                        int count = 0;
                                        foreach (var users in listTopUser)
                                        {
                                            if (count == 20)
                                            {
                                                break;
                                            }
                                            Console.WriteLine($"{count}.{users.Name} - {users.Grade} правильних вiдповiдей");
                                            count += 1;
                                        }
                                    }
                                }
                                break;
                            case 4:
                                using (FileStream fileStream = new FileStream(FileTopRatingUserShevchenko, FileMode.Open, FileAccess.Read))
                                {
                                    using (StreamReader reader = new StreamReader(fileStream))
                                    {
                                        temp = "";
                                        while (reader.Peek() > 0)
                                        {
                                            string[] strRating = new string[] { };
                                            temp += reader.ReadLine();
                                            strRating = temp.Split(' ');
                                            userRating.Name = strRating[0];
                                            //userRating.Grade = (strRating[1]);
                                            listTopUser.Add(new UserRating(userRating.Name, userRating.Grade));
                                        }
                                    }
                                }
                                listTopUser.Sort((x, y) => y.Grade.CompareTo(x.Grade));
                                using (FileStream stream = new FileStream(FileTopRatingUserShevchenko, FileMode.Open, FileAccess.Read))
                                {
                                    using (StreamReader reader = new StreamReader(stream))
                                    {
                                        temp = "";
                                        int count = 0;
                                        foreach (var users in listTopUser)
                                        {
                                            if (count == 20)
                                            {
                                                break;
                                            }
                                            Console.WriteLine($"{count}.{users.Name} - {users.Grade} правильних вiдповiдей");
                                            count += 1;
                                        }
                                    }
                                }
                                break;
                            case 5:
                                using (FileStream fileStream = new FileStream(FileTopRatingUserComputerGames, FileMode.Open, FileAccess.Read))
                                {
                                    using (StreamReader reader = new StreamReader(fileStream))
                                    {
                                        temp = "";
                                        while (reader.Peek() > 0)
                                        {
                                            string[] strRating = new string[] { };
                                            temp += reader.ReadLine();
                                            strRating = temp.Split(' ');
                                            userRating.Name = strRating[0];
                                            //userRating.Grade = (strRating[1]);
                                            listTopUser.Add(new UserRating(userRating.Name, userRating.Grade));
                                        }
                                    }
                                }
                                listTopUser.Sort((x, y) => y.Grade.CompareTo(x.Grade));
                                using (FileStream stream = new FileStream(FileTopRatingUserComputerGames, FileMode.Open, FileAccess.Read))
                                {
                                    using (StreamReader reader = new StreamReader(stream))
                                    {
                                        temp = "";
                                        int count = 0;
                                        foreach (var users in listTopUser)
                                        {
                                            if (count == 20)
                                            {
                                                break;
                                            }
                                            Console.WriteLine($"{count}.{users.Name} - {users.Grade} правильних вiдповiдей");
                                            count += 1;
                                        }
                                    }
                                }
                                break;
                            default:Console.WriteLine("Введiть тiльки з вказаних варiантів");break;
                        }
                        break;
                    case 4:
                        Console.Write("Що потрiбно помiняти?\nПароль(1)\nДату народження(2) -> ");
                        string str = Console.ReadLine();
                        int choiceRename = Int32.Parse(str);
                        if(choiceRename == 1)
                        {
                            Console.Write("Введiть новий пароль -> ");
                            string newPassword = Console.ReadLine();
                            using (FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read))
                            {
                                using(StreamReader reader = new StreamReader(fileStream))
                                {
                                    string temp = "";
                                    while(reader.Peek() > 0)
                                    {
                                        string[] strRename = new string[] { };
                                        temp += reader.ReadLine();
                                        strRename = temp.Split(' ');
                                        userRename.Password = strRename[1];
                                        userRename.Login = strRename[0];
                                        userRename.Date = Convert.ToDateTime(strRename[3]);
                                        if(userRename.Password == password)
                                        {
                                            userRename.Password = newPassword;
                                        }
                                        listUserRating.Add(new UserLog(userRename.Login,userRename.Password,userRename.Date));
                                    }
                                }
                            }
                            using(FileStream fileStream1 = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write))
                            {
                                using(StreamWriter writer = new StreamWriter(fileStream1))
                                {
                                    foreach(var item in listUserRating)
                                    {
                                        writer.WriteLine($"{item.Login} {item.Password} {item.Date}");
                                    }
                                }
                            }
                        }
                        else if(choiceRename == 2)
                        {
                            Console.Write("Введiть стару дату нарродження (dd.mm.yyyy) -> ");
                            DateTime oldDate = Convert.ToDateTime(Console.ReadLine());
                            Console.Write("Введiть нову дату нарордження (dd.mm.yyyy) -> ");
                            DateTime newDate = Convert.ToDateTime(Console.ReadLine());
                            using (FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read))
                            {
                                using (StreamReader reader = new StreamReader(fileStream))
                                {
                                    string temp = "";
                                    while (reader.Peek() > 0)
                                    {
                                        string[] strRename = new string[] { };
                                        temp += reader.ReadLine();
                                        strRename = temp.Split(' ');
                                        userRename.Password = strRename[1];
                                        userRename.Login = strRename[0];
                                        userRename.Date = Convert.ToDateTime(strRename[3]);
                                        if (userRename.Date.Equals(oldDate))
                                        {
                                            userRename.Date = newDate;
                                        }
                                        listUserRating.Add(new UserLog(userRename.Login, userRename.Password, userRename.Date));
                                    }
                                }
                            }
                            using (FileStream fileStream1 = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write))
                            {
                                using (StreamWriter writer = new StreamWriter(fileStream1))
                                {
                                    foreach (var item in listUserRating)
                                    {
                                        writer.WriteLine($"{item.Login} {item.Password} {item.Date}");
                                    }
                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine("Такого варiанту немає");
                        }
                        break;
                    case 6: 
                        Console.Clear();
                        isTrue = false; break;
                    case 5:
                        
                        bool questionTrue = true;
                        string question = "";
                        string variant = "";
                        int[] arrRespond = new int[12];
                        while (questionTrue)
                        {
                            Console.Clear();
                            Console.Write("Напишiть питання -> ");
                            questionList.Add(Console.ReadLine());
                            Console.Clear();
                            Console.Write("Напишiть варiанти вiдповiдей через пробіл -> ");
                            variantList.Add(Console.ReadLine());
                            Console.Clear();
                            Console.Write("Напишiть правильну вiдповiдь цифрою -> ");
                            respondList.Add(Int32.Parse(Console.ReadLine()));
                        }
                        using (FileStream fileStream = new FileStream(FileQuestion, FileMode.Append, FileAccess.Write))
                        {
                            using (StreamWriter writer = new StreamWriter(fileStream))
                            {
                                writer.WriteLine($"{questionList}\n{variantList}");
                            }
                        }
                        break;
                    default : Console.WriteLine("Такої функцiї немає");break;
                }
            }
            return FileUserResultAfterTest;
        }

        private static string ReadFileUserTop(string path)
        {
            using (FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                using(StreamReader reader = new StreamReader(fileStream))
                {
                    string temp = "";
                    while(reader.Peek() > 0)
                    {
                        temp += $"{reader.ReadLine()}\n";
                    }
                    return temp;
                }
            }
        }

        private static string ResultTestUserRead(string FileUserResultAfterTest)
        {
            using (FileStream fileStream = new FileStream(FileUserResultAfterTest, FileMode.Open, FileAccess.Read))
            {
                using (StreamReader reader = new StreamReader(fileStream))
                {
                    string temp = "";
                    while (reader.Peek() > 0)
                    {
                        temp += $"{reader.ReadLine()}\n";
                    }
                    return temp;
                }
            }
        }

        private static int ComputerGamesVictorine()
        {
            Console.Clear();
            Console.WriteLine("1. Яка з цих iгор є класичним прикладом гри в жанрi \"платформер\"?\n1. Super Mario Bros.\n2. The Witcher 3\n3. Minecraft\n4. Counter-Strike");
            Console.Write("Ваша вiдповiдь -> ");
            string respondQuestion1 = Console.ReadLine();
            int Question1 = Int32.Parse(respondQuestion1);
            Console.Clear();
            Console.WriteLine("2. Яка гра є найбiльш популярною у жанрi \"королiвська битва\"?\n1. Dota 2\n2. League of Legends\n3. Fortnite\n4. Apex Legends");
            Console.Write("Ваша вiдповiдь -> ");
            string respondQuestion2 = Console.ReadLine();
            int Question2 = Int32.Parse(respondQuestion2);
            Console.Clear();
            Console.WriteLine("3. У якому роцi вийшла перша гра серiї \"The Elder Scrolls\"?\n1. 2004\n2. 1994\n3. 2000\n4. 1991");
            Console.Write("Ваша вiдповiдь -> ");
            string respondQuestion3 = Console.ReadLine();
            int Question3 = Int32.Parse(respondQuestion3);
            Console.Clear();
            Console.WriteLine("4. Яка студiя є розробником гри \"Minecraft\"?\n1. Mojang\n2. Blizzard Entertainment\n3. Valve Corporation\n4. Rockstar Games");
            Console.Write("Ваша вiдповiдь -> ");
            string respondQuestion4 = Console.ReadLine();
            int Question4 = Int32.Parse(respondQuestion4);
            Console.Clear();
            Console.WriteLine("5. Яка гра є вiдомим прикладом жанру \"ролевих ігор\" (RPG)?\n1. Call of Duty\n2. Grand Theft Auto V\n3. Skyrim\n4. FIFA 2022");
            Console.Write("Ваша вiдповiдь -> ");
            string respondQuestion5 = Console.ReadLine();
            int Question5 = Int32.Parse(respondQuestion5);
            Console.Clear();
            Console.WriteLine("6. Яка популярна стратегiчна гра в реальному часi (RTS) була розроблена Blizzard Entertainment?\n1. StarCraft\n2. Age of Empires\n3. Command & Conquer\n4. Warcraft III");
            Console.Write("Ваша вiдповiдь -> ");
            string respondQuestion6 = Console.ReadLine();
            int Question6 = Int32.Parse(respondQuestion6);
            Console.Clear();
            Console.WriteLine("7. Яка з цих iгор є прикладом \"шутера\" вiд першої особи?\n1. Tomb Raider\n2. Call of Duty\n3. The Sims\n4. League of Legends");
            Console.Write("Ваша вiдповiдь -> ");
            string respondQuestion7 = Console.ReadLine();
            int Question7 = Int32.Parse(respondQuestion7);
            Console.Clear();
            Console.WriteLine("8. У якiй грi гравцi будують мiста, займаються економiкою та ведуть дипломатiю, щоб досягти домiнування?\n1. Civilization VI\n2. The Witcher 3\n3. Minecraft\n4. Dark Souls");
            Console.Write("Ваша вiдповiдь -> ");
            string respondQuestion8 = Console.ReadLine();
            int Question8 = Int32.Parse(respondQuestion8);
            Console.Clear();
            Console.WriteLine("9. Яка серiя iгор є однiєю з найбiльш популярних в жанрi \"гоночних симуляторiв\"?\n1. Gran Turismo\n2. Need for Speed\n3. F1\n4. Forza Motorsport");
            Console.Write("Ваша вiдповiдь -> ");
            string respondQuestion9 = Console.ReadLine();
            int Question9 = Int32.Parse(respondQuestion9);
            Console.Clear();
            Console.WriteLine("10. Яка гра є культовою для своїх мультиплеєрних битв у жанрi MOBA?\n1. Dota 2\n2. Counter-Strike: Global Offensive\n3. Overwatch\n4. Hearthstone");
            Console.Write("Ваша вiдповiдь -> ");
            string respondQuestion10 = Console.ReadLine();
            int Question10 = Int32.Parse(respondQuestion10);
            Console.Clear();
            Console.WriteLine("11. Яка гра була розроблена студiєю Rockstar Games i стала популярною завдяки вiдкритому свiту та сюжетним мiсiям?\n1.Max Payne 3 \n2. Red Dead Redemption 2\n3. Grand Theft Auto V\n4. Bully");
            Console.Write("Ваша вiдповiдь -> ");
            string respondQuestion11 = Console.ReadLine();
            int Question11 = Int32.Parse(respondQuestion11);
            Console.Clear();
            Console.WriteLine("12. Як називається популярна франшиза, що має багато частин i базується на постапокалiптичному свiтi?\n1. Fallout\n2. Assassin’s Creed\n3. Half-Life\n4. BioShock");
            Console.Write("Ваша вiдповiдь -> ");
            string respondQuestion12 = Console.ReadLine();
            int Question12 = Int32.Parse(respondQuestion12);
            Console.Clear();
            int[] questions = new int[12] { Question1, Question2, Question3, Question4, Question5, Question6, Question7, Question8, Question9, Question10, Question11, Question12 };
            int[] correctRespond = new int[12] {1,3,4,1,3,1,2,1,4,1,3,1 };
            int userCorrectResponds = 0;
            for (int i = 0; i < questions.Length; i++)
            {
                if (questions[i] == correctRespond[i])
                {
                    userCorrectResponds += 1;
                }
                
            }
            Console.WriteLine($"Вiкторина завершена!\nКількість правильних вiдповiдей -> {userCorrectResponds}");
            Console.Write("Бажаєте подивитись правильнi вiдповiдi?\nТак(1) Нi(2) -> ");
            string showResponds = Console.ReadLine();
            int choiceShow = Int32.Parse(showResponds);
            if (choiceShow == 1)
            {
                Console.WriteLine("Вiдповiдi до всiх питань:");
                for (int i = 0; i < questions.Length; i++)
                {
                    Console.WriteLine($"{i + 1}. {correctRespond[i]}");
                }
            }
            else
            {
                Console.Clear();
            }
            return userCorrectResponds;
        }

        private static int ShevchenkoVictorine()
        {
            Console.Clear();
            Console.WriteLine("1. У якому роцi народився Тарас Шевченко?\n1. 1814\n2. 1812\n3. 1815\n4. 1816");
            Console.Write("Ваша вiдповiдь -> ");
            string respondQuestion1 = Console.ReadLine();
            int Question1 = Int32.Parse(respondQuestion1);
            Console.Clear();
            Console.WriteLine("2. У якому селi народився Тарас Шевченко?\n1. Моринцi\n2. Кобзарiвка\n3. Шевченкове\n4. Капустинцi");
            Console.Write("Ваша вiдповiдь -> ");
            string respondQuestion2 = Console.ReadLine();
            int Question2 = Int32.Parse(respondQuestion2);
            Console.Clear();
            Console.WriteLine("3. Як звали батька Тараса Шевченка?\n1. Іван\n2. Михайло\n3. Григорiй\n4. Андрiй");
            Console.Write("Ваша вiдповiдь -> ");
            string respondQuestion3 = Console.ReadLine();
            int Question3 = Int32.Parse(respondQuestion3);
            Console.Clear();
            Console.WriteLine("4. Ким був Тарас Шевченко за професiєю до того, як стати поетом?\n1. Селянином\n2. Маляром\n3. Вiйськовим\n4. Вчителем");
            Console.Write("Ваша вiдповiдь -> ");
            string respondQuestion4 = Console.ReadLine();
            int Question4 = Int32.Parse(respondQuestion4);
            Console.Clear();
            Console.WriteLine("5. Яке значення для Тараса Шевченка мав Петербург?\n1. Це було мiсце його заслання\n2. Тут вiн отримав свою освiту\n3. Там вiн став вiдомим поетом\n4. Там вiн народився");
            Console.Write("Ваша вiдповiдь -> ");
            string respondQuestion5 = Console.ReadLine();
            int Question5 = Int32.Parse(respondQuestion5);
            Console.Clear();
            Console.WriteLine("6. Коли Тарас Шевченко був засланий на Сибiр?\n1. 1838\n2. 1847\n3. 1851\n4. 1852");
            Console.Write("Ваша вiдповiдь -> ");
            string respondQuestion6 = Console.ReadLine();
            int Question6 = Int32.Parse(respondQuestion6);
            Console.Clear();
            Console.WriteLine("7. Хто був першим наставником Тараса Шевченка в Академiї мистецтв?\n1. Карл Брюллов\n2. Василь Штернберг\n3. iлля Рєпін\n4. Олександр Кiпренський");
            Console.Write("Ваша вiдповiдь -> ");
            string respondQuestion7 = Console.ReadLine();
            int Question7 = Int32.Parse(respondQuestion7);
            Console.Clear();
            Console.WriteLine("8. Як звали жiнку, яку Тарас Шевченко любив, але не змiг одружитися з нею?\n1. Оксана\n2. Ганна\n3. Катерина\n4. Лiза");
            Console.Write("Ваша вiдповiдь -> ");
            string respondQuestion8 = Console.ReadLine();
            int Question8 = Int32.Parse(respondQuestion8);
            Console.Clear();
            Console.WriteLine("9. Який лiтературний твiр Тараса Шевченка став його дебютом у літературi?\n1. \"Заповiт\"\n2. \"Кобзар\"\n3. \"Катерина\"\n4. \"Енеїда\"");
            Console.Write("Ваша вiдповiдь -> ");
            string respondQuestion9 = Console.ReadLine();
            int Question9 = Int32.Parse(respondQuestion9);
            Console.Clear();
            Console.WriteLine("10. Який великий твiр Тарас Шевченко написав пiд час заслання в Сибiру?\n1. \"Гайдамаки\"\n2. \"Сон\"\n3. \"І мертвим, i живим…\"\n4. \"Три лiта\"");
            Console.Write("Ваша вiдповiдь -> ");
            string respondQuestion10 = Console.ReadLine();
            int Question10 = Int32.Parse(respondQuestion10);
            Console.Clear();
            Console.WriteLine("11. У якому роцi Тарас Шевченко помер?\n1. 1860\n2. 1861\n3. 1862\n4. 1863");
            Console.Write("Ваша вiдповiдь -> ");
            string respondQuestion11 = Console.ReadLine();
            int Question11 = Int32.Parse(respondQuestion11);
            Console.Clear();
            Console.WriteLine("12. Де похований Тарас Шевченко?\n1. У Києвi на Лук'янiвському кладовищi\n2. У Санкт-Петербурзi\n3. На Чернечiй горi в Каневi\n4. У Моринцях");
            Console.Write("Ваша вiдповiдь -> ");
            string respondQuestion12 = Console.ReadLine();
            int Question12 = Int32.Parse(respondQuestion12);
            Console.Clear();
            int[] questions = new int[12] { Question1, Question2, Question3, Question4, Question5, Question6, Question7, Question8, Question9, Question10, Question11, Question12 };
            int[] correctRespond = new int[12] { 2,1,3,2,3,2,1,3,3,1,2,3 };
            int userCorrectResponds = 0;
            for (int i = 0; i < questions.Length; i++)
            {
                if (questions[i] == correctRespond[i])
                {
                    userCorrectResponds += 1;
                }
            }
            Console.WriteLine($"Вiкторина завершена!\nКiлькiсть правильних вiдповiдей -> {userCorrectResponds}");
            Console.Write("Бажаєте подивитись правильнi вiдповiдi?\nТак(1) Нi(2) -> ");
            string showResponds = Console.ReadLine();
            int choiceShow = Int32.Parse(showResponds);
            if (choiceShow == 1)
            {
                Console.WriteLine("Вiдповiдi до всiх питань:");
                for (int i = 0; i < questions.Length; i++)
                {
                    Console.WriteLine($"{i + 1}. {correctRespond[i]}");
                }
            }
            else
            {
                Console.Clear();
            }
            return userCorrectResponds;
        }

        private static int ChessVictorine()
        {
            Console.Clear();
            Console.WriteLine("1. Хто є автором класичної шахової теорiї i створив вiдому шахову школу в Іспанiї?\n1. Руй Лопес\n2. Гаррi Каспаров\n3. Михайло Ботвинник\n4. Роберт Фiшер");
            Console.Write("Ваша вiдповiдь -> ");
            string respondQuestion1 = Console.ReadLine();
            int Question1 = Int32.Parse(respondQuestion1);
            Console.Clear();
            Console.WriteLine("2. Яка фiгура в шахах є найсильнiшою?\n1. Ферзь\n2. Тура\n3. Слон\n4. Кiнь");
            Console.Write("Ваша вiдповiдь -> ");
            string respondQuestion2 = Console.ReadLine();
            int Question2 = Int32.Parse(respondQuestion2);
            Console.Clear();
            Console.WriteLine("3. Що таке \"рокiровка\" в шахах?\n1. Перемiщення ферзя\n2. Спецiальний хiд з королем i турою\n3. Перехiд до ендшпiльної фази гри\n4. Перемiщення слона по дiагоналi");
            Console.Write("Ваша вiдповiдь -> ");
            string respondQuestion3 = Console.ReadLine();
            int Question3 = Int32.Parse(respondQuestion3);
            Console.Clear();
            Console.WriteLine("4. Яка кiлькiсть клiтин на шахiвницi?\n1. 64\n2. 72\n3. 81\n4. 100");
            Console.Write("Ваша вiдповiдь -> ");
            string respondQuestion4 = Console.ReadLine();
            int Question4 = Int32.Parse(respondQuestion4);
            Console.Clear();
            Console.WriteLine("5. Який хiд робить пiшак, коли вiн досягає останнього ряду на шахiвницi?\n1. Вiдразу перетворюється на ферзя\n2. Перетворюється на будь-яку iншу фiгуру, крiм короля\n3. Повертатись назад\n4. Залишити поле порожнiм");
            Console.Write("Ваша вiдповiдь -> ");
            string respondQuestion5 = Console.ReadLine();
            int Question5 = Int32.Parse(respondQuestion5);
            Console.Clear();
            Console.WriteLine("6. Як називається ситуацiя, коли король знаходиться пiд атакою, але не може уникнути мату?\n1. Шах\n2. Мат\n3. Пат\n4. Рокiровка");
            Console.Write("Ваша вiдповiдь -> ");
            string respondQuestion6 = Console.ReadLine();
            int Question6 = Int32.Parse(respondQuestion6);
            Console.Clear();
            Console.WriteLine("7. Якi шахiсти здобули найбiльше титулiв чемпiонiв свiту в шахах?\n1. Анатолiй Карпов i Гаррi Каспаров\n2. Магнус Карлсен i Вiшванатан Ананд\n3. Роберт Фiшер i Михайло Ботвинник\n4. Петр Леко i Василь iванчук");
            Console.Write("Ваша вiдповiдь -> ");
            string respondQuestion7 = Console.ReadLine();
            int Question7 = Int32.Parse(respondQuestion7);
            Console.Clear();
            Console.WriteLine("8. Яке обмеження для фiгури \"кiнь\"?\n1. Вiн може ходити тiльки по вертикалях\n2. Вiн може тiльки рухатися на одну клiтину\n3. Він рухається на двi клiтинки по прямiй i одну по дiагоналi\n4. Вiн не може бути перемiщений через iншi фiгури");
            Console.Write("Ваша вiдповiдь -> ");
            string respondQuestion8 = Console.ReadLine();
            int Question8 = Int32.Parse(respondQuestion8);
            Console.Clear();
            Console.WriteLine("9. Як називається позицiя, коли один з гравцiв не має жодних можливостей для ходу, але його король не пiд шахом?\n1. Пат\n2. Шах\n3. Мат\n4. Рокiровка");
            Console.Write("Ваша вiдповiдь -> ");
            string respondQuestion9 = Console.ReadLine();
            int Question9 = Int32.Parse(respondQuestion9);
            Console.Clear();
            Console.WriteLine("10. Як називається стратегiя, коли один з гравцiв постiйно змушує iншого робити захиснi ходи, не даючи можливостi до розвитку гри?\n1. Тиск\n2. Блокування\n3. Контратака\n4. Затискання");
            Console.Write("Ваша вiдповiдь -> ");
            string respondQuestion10 = Console.ReadLine();
            int Question10 = Int32.Parse(respondQuestion10);
            Console.Clear();
            Console.WriteLine("11. Яка є основна мета гри в шахи?\n1. Виграти всi фiгури суперника\n2. Ставити суперника в пат\n3. Завдати мату королю суперника\n4. Захистити свого короля");
            Console.Write("Ваша вiдповiдь -> ");
            string respondQuestion11 = Console.ReadLine();
            int Question11 = Int32.Parse(respondQuestion11);
            Console.Clear();
            Console.WriteLine("12. Яким чином можна зазнати \"мату\" в шахах?\n1. Коли король потрапляє пiд шах, але може уникнути\n2. Коли король знаходиться пiд шахом i не може уникнути\n3. Коли пiшак доходить до кiнця поля\n4. Коли одна з фiгур захоплює короля");
            Console.Write("Ваша вiдповiдь -> ");
            string respondQuestion12 = Console.ReadLine();
            int Question12 = Int32.Parse(respondQuestion12);
            Console.Clear();
            int[] questions = new int[12] { Question1, Question2, Question3, Question4, Question5, Question6, Question7, Question8, Question9, Question10, Question11, Question12 };
            int[] correctRespond = new int[12] { 2,1,2,1,2,2,1,3,1,1,3,2 };
            int userCorrectResponds = 0;
            for (int i = 0; i < questions.Length; i++)
            {
                if (questions[i] == correctRespond[i])
                {
                    userCorrectResponds += 1;
                }
            }
            Console.WriteLine($"Вiкторина завершена!\nКiлькiсть правильних вiдповiдей -> {userCorrectResponds}");
            Console.Write("Бажаєте подивитись правильнi вiдповiдi?\nТак(1) Нi(2) -> ");
            string showResponds = Console.ReadLine();
            int choiceShow = Int32.Parse(showResponds);
            if (choiceShow == 1)
            {
                Console.WriteLine("Вiдповiдi до всiх питань:");
                for (int i = 0; i < questions.Length; i++)
                {
                    Console.WriteLine($"{i + 1}. {correctRespond[i]}");
                }
            }
            else
            {
                Console.Clear();
            }
            return userCorrectResponds;
        }

        private static int HistoryOfUkraineVictorine()
        {
            Console.Clear();
            Console.WriteLine("1. Хто був першим князем Київської Русi?\n1. Олег\n2. Ярослав Мудрий\n3. Володимир Великий\n4. Ігор");
            Console.Write("Ваша вiдповiдь -> ");
            string respondQuestion1 = Console.ReadLine();
            int Question1 = Int32.Parse(respondQuestion1);
            Console.Clear();
            Console.WriteLine("2. Яке значення для української iсторiї мало Хрещення Русi у 988 роцi?\n1. Покращило економiчнi зв'язки з Вiзантiєю\n2. Започаткувало боротьбу за незалежнiсть\n3. Змiцнило авторитет князя Володимира\n4. Поклало початок християнству на теренах України");
            Console.Write("Ваша вiдповiдь -> ");
            string respondQuestion2 = Console.ReadLine();
            int Question2 = Int32.Parse(respondQuestion2);
            Console.Clear();
            Console.WriteLine("3. Якi були основнi причини монгольської навали на Київську Русь у 1240 роцi?\n1. Внутрiшнi конфлiкти та ослаблення держави\n2. Боротьба за землi з Польщею\n3. Релiгiйнi розбiжностi\n4. Торговельнi суперечки з Вiзантiєю");
            Console.Write("Ваша вiдповiдь -> ");
            string respondQuestion3 = Console.ReadLine();
            int Question3 = Int32.Parse(respondQuestion3);
            Console.Clear();
            Console.WriteLine("4. Яка була роль Богдана Хмельницького в iсторiї України?\n1. Збудував першу українську армiю\n2. Очолив боротьбу за незалежнiсть у серединi XVII столiття\n3. Проголосив незалежнiсть України в 1918 роцi\n4. Пiдписав договiр з Московiєю про союз");
            Console.Write("Ваша вiдповiдь -> ");
            string respondQuestion4 = Console.ReadLine();
            int Question4 = Int32.Parse(respondQuestion4);
            Console.Clear();
            Console.WriteLine("5. Як розвивалися українськi землi пiсля подiлу Польщi в кiнцi XVIII столiття?\n1. Вони потрапили пiд владу Австрiї та Росiї\n2. Стали частиною Французької iмперiї\n3. Була утворена незалежна Українська держава\n4. Залишилися пiд контролем Польщi");
            Console.Write("Ваша вiдповiдь -> ");
            string respondQuestion5 = Console.ReadLine();
            int Question5 = Int32.Parse(respondQuestion5);
            Console.Clear();
            Console.WriteLine("6. Якi основнi подiї вiдбулись пiд час Української революцiї 1917-1921 рокiв?\n1. Проголошення незалежностi Української Народної Республiки\n2. Повна лiквiдацiя незалежностi України\n3. Пiдписання мирного договору з Росiєю\n4. Перемога бiльшовикiв i встановлення радянської влади");
            Console.Write("Ваша вiдповiдь -> ");
            string respondQuestion6 = Console.ReadLine();
            int Question6 = Int32.Parse(respondQuestion6);
            Console.Clear();
            Console.WriteLine("7. Як проходила боротьба українцiв за незалежнiсть пiд час Другої свiтової вiйни?\n1. Українцi активно пiдтримували нацистську Німеччину\n2. Українська повстанська армiя боролася проти радянської влади\n3. Всi українцi пiдтримували СРСР\n4. Україна залишалася нейтральною в цiй вiйнi");
            Console.Write("Ваша вiдповiдь -> ");
            string respondQuestion7 = Console.ReadLine();
            int Question7 = Int32.Parse(respondQuestion7);
            Console.Clear();
            Console.WriteLine("8. Що таке \"Голодомор\" 1932-1933 рокiв i як вiн вплинув на Україну?\n1. Природна катастрофа, яка призвела до голоду\n2. Полiтика колективiзацiї та примусового вивезення зерна, що викликала голод\n3. Низький рiвень врожайностi через неефективнi методи землеробства\n4. Масове вигнання селян з територiї України");
            Console.Write("Ваша вiдповiдь -> ");
            string respondQuestion8 = Console.ReadLine();
            int Question8 = Int32.Parse(respondQuestion8);
            Console.Clear();
            Console.WriteLine("9. Як Україна стала частиною СРСР, i який вплив це мало на її розвиток?\n1. Україна стала частиною СРСР пiсля Першої свiтової вiйни, що призвело до економiчного пiдйому\n2. Пiсля революцiї 1917 року Україна була змушена приєднатися до СРСР\n3. Україна була захоплена СРСР у 1922 роцi, що призвело до депресiї в країнi\n4. Україна залишалася незалежною, але мала тiснi економiчнi зв'язки з СРСР");
            Console.Write("Ваша вiдповiдь -> ");
            string respondQuestion9 = Console.ReadLine();
            int Question9 = Int32.Parse(respondQuestion9);
            Console.Clear();
            Console.WriteLine("10. Якi основнi етапи боротьби за незалежнiсть України в 1990-тi роки?\n1. Проголошення незалежностi у 1991 роцi та перехiд до ринкової економiки\n2. Боротьба за незалежнiсть продовжувалась до 1999 року\n3. Пiсля 1991 року Україна стала частиною Європейського Союзу\n4. Україна була змушена залишитись частиною СРСР");
            Console.Write("Ваша вiдповiдь -> ");
            string respondQuestion10 = Console.ReadLine();
            int Question10 = Int32.Parse(respondQuestion10);
            Console.Clear();
            Console.WriteLine("11. Якi причини Євромайдану 2013-2014 рокiв та його результати для України?\n1. Протести через вiдсутність реформ в Українi та пiдтримка iнтеграцiї з ЄС\n2. Протести через непiдтримку дiй влади в АТО\n3. Результати Євромайдану призвели до полiпшення економiчної ситуацiї\n4. Євромайдан спричинив розвиток автономiї Криму");
            Console.Write("Ваша вiдповiдь -> ");
            string respondQuestion11 = Console.ReadLine();
            int Question11 = Int32.Parse(respondQuestion11);
            Console.Clear();
            Console.WriteLine("12. Як змiнилася геополiтична ситуацiя для України пiсля анексiї Криму в 2014 роцi?\n1. Україна стала частиною Європейського Союзу\n2. Вiдносини з Росiєю стали бiльш мирними\n3. Україна пiдвищила свою вiйськову спiвпрацю з НАТО\n4. Україна уклала угоди з Росiєю про мир та спiвпрацю");
            Console.Write("Ваша вiдповiдь -> ");
            string respondQuestion12 = Console.ReadLine();
            int Question12 = Int32.Parse(respondQuestion12);
            Console.Clear();
            int[] questions = new int[12] { Question1, Question2, Question3, Question4, Question5, Question6, Question7, Question8, Question9, Question10, Question11, Question12 };
            int[] correctRespond = new int[12] { 1,3,1,2,1,1,2,2,3,1,1,3 };
            int userCorrectResponds = 0;
            for (int i = 0; i < questions.Length; i++)
            {
                if (questions[i] == correctRespond[i])
                {
                    userCorrectResponds += 1;
                }
            }
            Console.WriteLine($"Вiкторина завершена!\nКiлькiсть правильних вiдповiдей -> {userCorrectResponds}");
            Console.Write("Бажаєте подивитись правильнi вiдповiдi?\nТак(1) Нi(2) -> ");
            string showResponds = Console.ReadLine();
            int choiceShow = Int32.Parse(showResponds);
            if (choiceShow == 1)
            {
                Console.WriteLine("Вiдповiдi до всiх питань:");
                for (int i = 0; i < questions.Length; i++)
                {
                    Console.WriteLine($"{i + 1}. {correctRespond[i]}");
                }
            }
            else
            {
                Console.Clear();
            }
            return userCorrectResponds;
        }

        private static int GeographyVictorine()
        {
            Console.Clear();
            Console.WriteLine("1. Яка найвища гора в свiтi?\n1.Кiлiманджаро\n2.Монблан\n3.Еверест\n4.Мак-Кiнлi");
            Console.Write("Ваша вiдповiдь -> ");
            string respondQuestion1 = Console.ReadLine();
            int Question1 = Int32.Parse(respondQuestion1);
            Console.Clear();
            Console.WriteLine("2. Яка країна має найменшу площу на Землi?\n1.Ватикан\n2.Мальта\n3.Лiхтенштейн\n4.Молдова");
            Console.Write("Ваша вiдповiдь -> ");
            string respondQuestion2 = Console.ReadLine();
            int Question2 = Int32.Parse(respondQuestion2);
            Console.Clear();
            Console.WriteLine("3. Яка рiчка є найдовшою в свiтi?\n1.Нiл\n2.Амазонка\n3.Янцзи\n4.Мiссiсiпi");
            Console.Write("Ваша вiдповiдь -> ");
            string respondQuestion3 = Console.ReadLine();
            int Question3 = Int32.Parse(respondQuestion3);
            Console.Clear();
            Console.WriteLine("4. Яке море є найбiльшим за площею?\n1.Середземне\n2.Карибське\n3.Чорне\n4.Каспiйське");
            Console.Write("Ваша вiдповiдь -> ");
            string respondQuestion4 = Console.ReadLine();
            int Question4 = Int32.Parse(respondQuestion4);
            Console.Clear();
            Console.WriteLine("5. В якому океанi знаходиться Австралiя?\n1.Атлантичний\n2.iндiйський\n3.Тихий\n4.Пiвденний");
            Console.Write("Ваша вiдповiдь -> ");
            string respondQuestion5 = Console.ReadLine();
            int Question5 = Int32.Parse(respondQuestion5);
            Console.Clear();
            Console.WriteLine("6. Яка з наступних країн не є островною?\n1.Ірландiя\n2.Японiя\n3.Канада\n4.Шрi-Ланка");
            Console.Write("Ваша вiдповiдь -> ");
            string respondQuestion6 = Console.ReadLine();
            int Question6 = Int32.Parse(respondQuestion6);
            Console.Clear();
            Console.WriteLine("7. Яке мiсто є столицею Францiї?\n1.Лондон\n2.Рим\n3.Париж\n4.Берлiн");
            Console.Write("Ваша вiдповiдь -> ");
            string respondQuestion7 = Console.ReadLine();
            int Question7 = Int32.Parse(respondQuestion7);
            Console.Clear();
            Console.WriteLine("8. Який континент має найбільше держав?\n1.Африка\n2.Європа\n3.Азiя\n4.Пiвденна Америка");
            Console.Write("Ваша вiдповiдь -> ");
            string respondQuestion8 = Console.ReadLine();
            int Question8 = Int32.Parse(respondQuestion8);
            Console.Clear();
            Console.WriteLine("9. Яка з наступних країн знаходиться на двох континентах?\n1.Єгипет\n2.Іспанiя\n3.Туреччина\n4.Мексика");
            Console.Write("Ваша вiдповiдь -> ");
            string respondQuestion9 = Console.ReadLine();
            int Question9 = Int32.Parse(respondQuestion9);
            Console.Clear();
            Console.WriteLine("10. Яка країна є найбільш густонаселеним державою в свiтi?\n1.Індiя\n2.Китай\n3.США\n4.Індонезiя");
            Console.Write("Ваша вiдповiдь -> ");
            string respondQuestion10 = Console.ReadLine();
            int Question10 = Int32.Parse(respondQuestion10);
            Console.Clear();
            Console.WriteLine("11. Який океан омиває захiдне узбережжя США?\n1.Атлантичний\n2.Індiйський\n3.Пiвденний\n4.Тихий");
            Console.Write("Ваша вiдповiдь -> ");
            string respondQuestion11 = Console.ReadLine();
            int Question11 = Int32.Parse(respondQuestion11);
            Console.Clear();
            Console.WriteLine("12. Яка країна має найкоротшу назву серед держав світу?\n1.Канада\n2.Чилi\n3.Ємен\n4.Японiя");
            Console.Write("Ваша вiдповiдь -> ");
            string respondQuestion12 = Console.ReadLine();
            int Question12 = Int32.Parse(respondQuestion12);
            Console.Clear();
            int[] questions = new int[12] {Question1, Question2, Question3, Question4, Question5, Question6, Question7,Question8,Question9,Question10,Question11,Question12};
            int[] correctRespond = new int[12] {3,1,1,4,3,3,3,1,3,2,4,2 };
            int userCorrectResponds = 0;
            for(int i = 0; i < questions.Length; i++)
            {
                if (questions[i] == correctRespond[i])
                {
                    userCorrectResponds += 1;
                }
            }
            Console.WriteLine($"Вiкторина завершена!\nКiлькiсть правильних вiдповiдей -> {userCorrectResponds}");
            Console.Write("Бажаєте подивитись правильнi вiдповiдi?\nТак(1) Нi(2) -> ");
            string showResponds = Console.ReadLine();
            int choiceShow = Int32.Parse(showResponds);
            if(choiceShow == 1)
            {
                Console.WriteLine("Вiдповiдi до всіх питань:");
                for(int i = 0;i < questions.Length;i++)
                {
                    Console.WriteLine($"{i + 1}. {correctRespond[i]}");
                }
            }
            else
            {
                Console.Clear();
            }
            return userCorrectResponds;
        }

        private static void DeleteUserLogin(string FileUserLog, List<UserLog> list, UserLog user)
        {
            using (FileStream fileStream = new FileStream(FileUserLog, FileMode.Open, FileAccess.Read))
            {
                using (StreamReader reader = new StreamReader(FileUserLog))
                {
                    string temp = "";
                    while (reader.Peek() > 0)
                    {
                        string[] str = new string[] { };
                        temp = reader.ReadLine();
                        str = temp.Split(' ');
                        user.Login = str[0];
                        user.Password = str[1];
                        list.Add(user);
                    }
                }
            }
        }
        private static bool CheckUserLogin(string FileUserLog,string login)
        {
            using (FileStream fileStream = new FileStream(FileUserLog, FileMode.Open, FileAccess.Read))
            {
                using (StreamReader reader = new StreamReader(FileUserLog))
                {
                    string temp = "";
                    while (reader.Peek() > 0)
                    {
                        string[] str = new string[] { };
                        temp = reader.ReadLine();
                        str = temp.Split(' ');
                        if (str[0] == login || str[1] == login)
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        private static void RegisterUser(List<UserLog> logList, string path, UserLog user,List<UserLog> userDelete)
        {
            try
            {
                while(true)
                {
                    Console.Write("Введiть логiн -> ");
                    user.Login = Console.ReadLine();
                    bool check = false;
                    if(File.Exists(path))
                    {
                        check = CheckUserLogin(path, user.Login.ToString());
                    }
                    if (!string.IsNullOrEmpty(user.Login) && check == false)
                    {
                        Console.Write("Введiть пароль -> ");
                        user.Password = Console.ReadLine();
                        if (!string.IsNullOrEmpty(user.Password))
                        {
                            userDelete.Add(user);
                            Console.Write("Введiть дату народження(dd.mm.yyyy) -> ");
                            user.Date = Convert.ToDateTime(Console.ReadLine());
                            Console.WriteLine();
                            if (user.Date != DateTime.MinValue)
                            {
                                logList.Add(user);
                                
                                using (FileStream fileStream = new FileStream(path, FileMode.Append, FileAccess.Write))
                                {
                                    using (StreamWriter writer = new StreamWriter(fileStream))
                                    {
                                        writer.WriteLine($"{user.Login} {user.Password} {user.Date}");
                                    }
                                }
                                Console.WriteLine("Акаунт зареєстровано!");
                                MenuToVictorine(user.Login.ToString(),path,user.Password.ToString());
                                break;
                            }
                        }
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("Не можна зареєструвати вже iснуючий логiн!"); 
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
