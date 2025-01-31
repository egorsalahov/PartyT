using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PartyT
{
    public class Output
    {
        public List<Friend> friends;

        public Output(List<Friend> friends)
        {
            this.friends = friends;
        }

        public void DoOutput()
        {
            foreach (var friend in friends)
            {
                foreach(var item in friend.debt) // item ключ-значение
                {
                    if (item.Value == 0)
                    {
                        continue;
                    }
                    Console.WriteLine($"{friend.nameFriend} => {item.Key}: {item.Value}");
                }
            }
        }
    }
}
