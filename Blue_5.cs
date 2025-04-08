using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_7
{
    public class Blue_5
    {
        public class Sportsman
        {
            private string name;
            private string surname;
            private int place;
            private bool ind;

            public string Name => name;

            public string Surname => surname;

            public int Place => place;

            public Sportsman(string name, string surname)
            {
                this.name = name;
                this.surname = surname;
                this.place = 0;
                this.ind = false;
            }

            public void SetPlace(int place)
            {
                if (this.place != 0)
                {
                    Console.WriteLine("Место установлено ранее");
                    return;
                }
                this.place = place;
                this.ind = true;
            }

            public void Print()
            {
                Console.WriteLine($"Спортсмен: {name} {surname}, Место: {place}");
            }
        }

        public abstract class Team
        {
            protected string name;
            protected Sportsman[] sportsmen;
            protected int count;

            public string Name => name;

            public Sportsman[] Sportsmen => sportsmen;

            public Team(string name)
            {
                this.name = name;
                this.sportsmen = new Sportsman[6];
                this.count = 0;
            }

            public void Add(Sportsman sportsman)
            {
                if (count < 6)
                {
                    sportsmen[count] = sportsman;
                    count++;
                }
                else
                {
                    Console.WriteLine("Команда полная, не удается добавить больше спортсменов.");
                }
            }

            public void Add(params Sportsman[] newSportsmen)
            {
                foreach (var sportsman in newSportsmen)
                {
                    Add(sportsman);
                }
            }

            protected abstract double GetTeamStrength();

            public static Team GetChampion(Team[] teams)
            {
                if (teams == null || teams.Length == 0)
                    return null;

                Team champion = teams[0];
                double maxStrength = champion.GetTeamStrength();

                foreach (var team in teams)
                {
                    double strength = team.GetTeamStrength();
                    if (strength > maxStrength)
                    {
                        maxStrength = strength;
                        champion = team;
                    }
                }
                return champion;
            }

            public void Print()
            {
                Console.WriteLine($"Команда: {name}");
                Console.WriteLine("Спортсмены:");
                foreach (var sportsman in sportsmen)
                {
                    sportsman?.Print();
                }
            }
        }

        public class ManTeam : Team
        {
            public ManTeam(string name) : base(name) { }

            protected override double GetTeamStrength()
            {
                double averagePlace = 0;
                for (int i = 0; i < count; i++)
                {
                    averagePlace += sportsmen[i].Place;
                }
                averagePlace /= count;

                return averagePlace > 0 ? 100 / averagePlace : 0; 
            }
        }

        public class WomanTeam : Team
        {
            public WomanTeam(string name) : base(name) { }

            protected override double GetTeamStrength()
            {
                double sumPlaces = 0;
                double productPlaces = 1;

                for (int i = 0; i < count; i++)
                {
                    sumPlaces += sportsmen[i].Place;
                    productPlaces *= sportsmen[i].Place > 0 ? sportsmen[i].Place : 1; 
                }

                return productPlaces > 0 ? (100 * sumPlaces * count) / productPlaces : 0; 
            }
        }
    }
}
