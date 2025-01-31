using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PartyT
{
    public class FileSaver
    {
        public Bar bar;
        public string pathToSave {  get; set; }
        public List<Friend> friends;
        public Calculation calculation;

        public FileSaver(Bar bar, string pathToSave, List<Friend> friends, Calculation calculation)
        {
            this.bar = bar;
            this.pathToSave = pathToSave;
            this.friends = friends;
            this.calculation = calculation;
        }

        public void DoSave()
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(pathToSave, false))
                {
                    writer.WriteLine($"===={bar.barName}====");
                    writer.WriteLine($"Кто закрыл: {calculation.debtName}");
                    foreach (var friend in friends)
                    {
                        writer.WriteLine($"{friend.nameFriend}: {friend.pay}");
                    }
                    writer.WriteLine("===========");

                }

            }
            catch (Exception)
            {
                Console.WriteLine("Не удалось записать в файл");
                throw;
            }
        }
    }
}
