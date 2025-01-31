using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Globalization;
using System.IO;
using System.Xml.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace PartyT
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //начальный ввод
            string corpName;
            string corpDate;
            string corpPath;
            string corpPathName;

            while (228 < 1488)
            {
                Console.Write("Введите название корпоратива: ");
                corpName = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(corpName))
                {
                    Console.WriteLine("Название не может быть пустым. Введите корректное название");
                    continue;
                }
                break;
            }

            while (228 < 1488)
            {
                Console.Write("Введите дату корпоратива: ");
                corpDate = Console.ReadLine();
                if (!(DateTime.TryParseExact(corpDate, "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime parsedDate))) //thx gpt
                {
                    Console.WriteLine("Дата некорректна. Введите иную дату");
                    continue;
                }
                break;
            }

            while (228 < 1488)
            {
                Console.Write("Введите путь файла: ");
                corpPath = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(corpPath) || !Path.Exists(corpPath))
                {
                    Console.WriteLine("Путь файла не может быть пустым");
                    continue;
                }
                break;
            }

            while (228 < 1488)
            {
                Console.Write("Введите имя выходного файла: ");
                corpPathName = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(corpPathName) || corpPathName.IndexOfAny(Path.GetInvalidFileNameChars()) >= 0)
                {
                    Console.WriteLine("Некоректное имя файла. Напишите иное имя");
                    continue;
                }
                break;
            }

            //список имен друзей
            Console.WriteLine("Введите имена друзей:  (для выхода напишите 0)");
           
            List<string> namesFriends = new List<string>();
            List<Friend> friends = new List<Friend>();
            while (true)
            {
                Console.Write("Имя друга: ");
                StringBuilder stringBuilder = new StringBuilder();
                string friendName = Console.ReadLine();
                stringBuilder.Append(char.ToUpper(friendName[0]));
                stringBuilder.Append(friendName.Substring(1).ToLower());
                friendName = stringBuilder.ToString();

                if (string.IsNullOrEmpty(friendName))
                {
                    Console.WriteLine("Имя не может быть пустым");
                    continue;
                }
                if (namesFriends.Contains(friendName))
                {
                    Console.WriteLine("Ошибка: это имя уже было введено. Пожалуйста, введите другое значение.");
                    continue; 
                }
                if (friendName == "0")
                {
                    break;
                }

                Friend friend = new Friend(friendName,0);

                friends.Add(friend);
                namesFriends.Add(friendName);
            }

            //заполнить словарь у каждого друга другими друзьями
            //не гениально-ли?
            for(int i = 0; i < friends.Count; i++)
            {
                Friend toExclude = friends[i];
                foreach (Friend friend in friends)
                {
                    if (friend != toExclude)
                    {
                        toExclude.debt.Add(friend.nameFriend, 0);
                    }

                }

            }

            //кто сколько куда (тут же бары появляются)
            Console.WriteLine("===============");

            //спавним бары (только с именем)
            Console.WriteLine("Введите имена баров: (введите 0 если баров не будет)");
            
            List<Bar> bars = new List<Bar>();
            List<string> barsNames = new List<string>();
           
            while (228 < 1488)
            {
                Console.Write("Имя бара: ");
                StringBuilder stringBuilder = new StringBuilder();
                string barNameTimely = Console.ReadLine();
                stringBuilder.Append(char.ToUpper(barNameTimely[0]));
                stringBuilder.Append(barNameTimely.Substring(1).ToLower());
                barNameTimely = stringBuilder.ToString();

                if (string.IsNullOrEmpty(barNameTimely))
                {
                    Console.WriteLine("У бара должно быть имя");
                    continue;
                }

                if (barsNames.Contains(barNameTimely))
                {
                    Console.WriteLine("Такой бар уже существует");
                    continue;
                }

                
                if (barNameTimely == "0")
                {
                    break;
                }


                Bar bar = new Bar(barNameTimely);

                bars.Add(bar);
                barsNames.Add(barNameTimely);

            }

            string pathToSave = Path.Combine(corpPath, corpName);
            using (StreamWriter writer = new StreamWriter(pathToSave, false))
            {
                writer.WriteLine($"===={corpName}====");
                writer.WriteLine($"===={corpDate}==== \n");
                foreach (Bar bar in bars)
                {
                    Calculation calculation = new Calculation(bar, namesFriends, friends);
                    calculation.DoCount();
                    calculation.DoDebt();

                    
                    writer.WriteLine($"\n===={bar.barName}====");
                    writer.WriteLine($"Кто закрыл: {calculation.debtName}");
                    foreach (var friend in friends)
                    {
                       writer.WriteLine($"{friend.nameFriend}: {friend.pay}");
                    }
                    writer.WriteLine("==============\n");

                    
                }

                foreach (var friend in friends)
                {
                    foreach (var item in friend.debt) // item ключ-значение
                    {
                        if (item.Value == 0)
                        {
                            continue;
                        }
                        writer.WriteLine($"{friend.nameFriend} => {item.Key}: {item.Value}");
                    }
                }

            }


        }
    }
}
