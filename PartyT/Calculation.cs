using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace PartyT
{
    public class Calculation
    {
        public Bar bar;

        public List<Friend> friends;

        public List<string> namesFriends;


        public Calculation(Bar bar, List<string> namesFriends, List<Friend> friends)
        {
            this.bar = bar;
            this.namesFriends = namesFriends;
            this.friends = friends;
        }

        public void DoCount()
        {
            Console.WriteLine($"Вы тусили в: {bar.barName}");
            //кто сколько заплатил
            for(int i = 0; i < namesFriends.Count; i++)
            {
                //проверка имени
                Console.Write("Введите имя заплатившего: ");
                string payerName;
                
                while (228 < 1488)
                {
                    StringBuilder stringBuilder = new StringBuilder();
                    payerName = Console.ReadLine();
                    stringBuilder.Append(char.ToUpper(payerName[0]));
                    stringBuilder.Append(payerName.Substring(1).ToLower());
                    payerName = stringBuilder.ToString();
                    if (string.IsNullOrWhiteSpace(payerName))
                    {
                        Console.WriteLine("Имя не может быть пустым. Введите корректное название");
                        continue;
                    }
                    if (!(namesFriends.Contains(payerName)))
                    {
                        Console.WriteLine("Друг должен существовать в тусовке");
                        continue;
                    }

                    break;
                }
               
                Friend foundFriend = friends.SingleOrDefault(f => f.nameFriend == payerName);

                //сколько заплатил конкретный Friend в баре
                Console.Write("Введите, сколько друг заплатил в баре: ");
                string input;
                int number;


                while (228 < 1488)
                {
                    input = Console.ReadLine();
                    if (int.TryParse(input, out number))
                    {
                        foundFriend.pay = number;
                        bar.sum += number;
                        break;
                    }
                    else
                    {
                        Console.WriteLine($"Введите количество денег, потраченных {foundFriend.nameFriend} в {bar.barName}");
                        continue;
                    }
                }
            }         
        }
        public string debtName;
        //кто закрыл счет + равсчет долгов в конкретном баре
        public void DoDebt()
        {
            Console.WriteLine("Введите имя человека, закрывшего счет: ");
            

            while (228 < 1488)
            {
                StringBuilder stringBuilder = new StringBuilder();
                debtName = Console.ReadLine();
                stringBuilder.Append(char.ToUpper(debtName[0]));
                stringBuilder.Append(debtName.Substring(1).ToLower());
                debtName = stringBuilder.ToString();
                if (string.IsNullOrWhiteSpace(debtName))
                {
                    Console.WriteLine("Имя не может быть пустым. Введите корректное название");
                    continue;
                }
                if (!(namesFriends.Contains(debtName)))
                {
                    Console.WriteLine("Друг должен существовать в тусовке");
                    continue;
                }

                break;
            }

            Friend foundDebtPerson = friends.SingleOrDefault(f => f.nameFriend == debtName);

            //расчет в конкретном баре
            bar.sum -= foundDebtPerson.pay;
            
            foreach (Friend friend in friends)
            {
                if (friend != foundDebtPerson)
                {
                    friend.debt[foundDebtPerson.nameFriend] += friend.pay;
                }

            }
        }
    }
}
