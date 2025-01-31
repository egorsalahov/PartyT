using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PartyT
{
    public class Bar
    {
        public string barName { get; set; }
        public int sum { get; set; } = 0;


        public Bar(string barName)
        {
           this.barName = barName;
        }
       
    }
}
