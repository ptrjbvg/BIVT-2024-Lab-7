using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_7
{
    public class Blue_3
    {
        public class Participant
        {
            protected string name;
            protected string surname;
            protected int[] penaltyTimes;

            public string Name => name;

            public string Surname => surname;

            public int[] Penalties
            {
                get
                {
                    if (penaltyTimes == null)
                        return null;
                    int[] penaltyTimesCopy = new int[penaltyTimes.Length];
                    Array.Copy(penaltyTimes, penaltyTimesCopy, penaltyTimes.Length);
                    return penaltyTimesCopy;
                }
            }

            public int Total
            {
                get
                {
                    if (penaltyTimes == null)
                        return 0;
                    int totalScore = 0;
                    foreach (int penalty in penaltyTimes)
                    {
                        totalScore += penalty;
                    }
                    return totalScore;
                }
            }

            public virtual bool IsExpelled
            {
                get
                {
                    if (penaltyTimes == null)
                        return false;

                    foreach (int penalty in penaltyTimes)
                    {
                        if (penalty == 10)
                        {
                            return true;
                        }
                    }
                    return false;
                }
            }

            public virtual void PlayMatch(int value)
            {
                if (penaltyTimes == null)
                    return;

                int[] newArray = new int[penaltyTimes.Length + 1];
                Array.Copy(penaltyTimes, newArray, penaltyTimes.Length);
                newArray[newArray.Length - 1] = value;
                penaltyTimes = newArray;
            }

            public static void Sort(Participant[] array)
            {
                if (array == null) return;

                for (int i = 0; i < array.Length - 1; i++)
                {
                    for (int j = 0; j < array.Length - i - 1; j++)
                    {
                        if (array[j].Total > array[j + 1].Total)
                        {
                            (array[j], array[j + 1]) = (array[j + 1], array[j]);
                        }
                    }
                }
            }

            public void Print()
            {
                Console.WriteLine($"Name: {name}");
                Console.WriteLine($"Surname: {surname}");
                Console.Write("Penalties: ");
                foreach (var penalty in penaltyTimes)
                {
                    Console.Write(penalty + " ");
                }
                Console.WriteLine();
            }

            public Participant(string name, string surname)
            {
                this.name = name;
                this.surname = surname;
                this.penaltyTimes = new int[0];
            }
        }

        public class BasketballPlayer : Participant
        {
            private int matchesPlayed;

            public BasketballPlayer(string name, string surname) : base(name, surname) { }

            public override bool IsExpelled
            {
                get
                {
                    if (matchesPlayed == 0) return false;

                    int fiveFoulsCount = 0;
                    foreach (var penalty in penaltyTimes)
                    {
                        if (penalty == 5)
                        {
                            fiveFoulsCount++;
                        }
                    }

                    return fiveFoulsCount > matchesPlayed * 0.1 || Total > matchesPlayed * 2;
                }
            }

            public override void PlayMatch(int fouls)
            {
                if (fouls < 0 || fouls > 6)
                    return;

                base.PlayMatch(fouls);
                matchesPlayed++;
            }
        }

        public class HockeyPlayer : Participant
        {
            public HockeyPlayer(string name, string surname) : base(name, surname) { }

            public override bool IsExpelled
            {
                get
                {
                    if (Array.Exists(penaltyTimes, penalty => penalty >= 10))
                    {
                        return true;
                    }

                    int totalPenaltyTime = 0; 
                    int totalPlayers = 1;      

                    return Total > ((totalPenaltyTime / (double)totalPlayers) * 0.1);
                }
            }

            public override void PlayMatch(int penaltyMinutes)
            {
                if (penaltyMinutes < 0)
                    throw new ArgumentOutOfRangeException(nameof(penaltyMinutes), "Penalty minutes cannot be negative.");

                base.PlayMatch(penaltyMinutes);
            }
        }
    }
}
