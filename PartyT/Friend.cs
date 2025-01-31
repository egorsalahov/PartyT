using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PartyT
{
    public class Friend
    {
        public string nameFriend { get; set; }

        public int pay { get; set; } //сколько платил в баре

        public Dictionary<string, int> debt = new Dictionary<string, int>(); //кому сколько должен

        public Friend(string nameFriend, int pay)
        {
            this.nameFriend = nameFriend;
            this.pay = pay;
        }
    }
}
