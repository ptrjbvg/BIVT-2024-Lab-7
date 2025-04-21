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
            private string name;
            private string surname;
            protected int[] penaltyTimes;

            public string Name => name;
            public string Surname => surname;
            public int[] Penalties
            {
                get
                {
                    if (penaltyTimes == null) return null;
                    int[] copy = new int[penaltyTimes.Length];
                    Array.Copy(penaltyTimes, copy, copy.Length);
                    return copy;
                }
            }

            public int Total
            {
                get
                {
                    if (penaltyTimes == null) return 0;
                    int sum = 0;
                    for (int i = 0; i < penaltyTimes.Length; i++)
                    {
                        sum += penaltyTimes[i];
                    }
                    return sum;
                }
            }

            public virtual bool IsExpelled
            {
                get
                {
                    if (penaltyTimes == null) return false;
                    bool isExpelled = false;
                    for (int i = 0; i < penaltyTimes.Length; i++)
                    {
                        if (penaltyTimes[i] == 10)
                        {
                            isExpelled = true;
                            break;
                        }
                    }
                    return isExpelled;
                }
            }

            public Participant(string name, string surname)
            {
                this.name = name;
                this.surname = surname;
                this.penaltyTimes = new int[0];
            }

            public virtual void PlayMatch(int time)
            {
                if (penaltyTimes == null) return;
                int[] arr = new int[penaltyTimes.Length + 1];
                for (int i = 0; i < penaltyTimes.Length; i++)
                {
                    arr[i] = penaltyTimes[i];
                }
                arr[arr.Length - 1] = time;
                penaltyTimes = arr;
            }

            public static void Sort(Participant[] array)
            {
                if (array == null || array.Length <= 1)
                    return;

                int n = array.Length;
                bool swapped;

                for (int i = 0; i < n - 1; i++)
                {
                    swapped = false;

                    for (int j = 0; j < n - 1 - i; j++)
                    {
                        if (array[j].Total > array[j + 1].Total)
                        {
                            Participant temp = array[j];
                            array[j] = array[j + 1];
                            array[j + 1] = temp;

                            swapped = true;
                        }
                    }
                    if (!swapped)
                        break;
                }
            }

            public void Print()
            {
                Console.WriteLine($"{name} {surname} - {Total}");
            }
        }

        public class BasketballPlayer : Participant
        {
            public override bool IsExpelled
            {
                get
                {
                    if (penaltyTimes == null) return false;
                    int count = 0;
                    for (int i = 0; i < penaltyTimes.Length; i++)
                    {
                        if (penaltyTimes[i] >= 5) count++;
                    }
                    for (int i = 0; i < penaltyTimes.Length; i++)
                    {
                        if (this.Total > 2 * penaltyTimes.Length || count > 0.1 * penaltyTimes.Length)
                        {
                            return true;
                        }
                    }
                    return false;
                }
            }

            public BasketballPlayer(string name, string surname) : base(name, surname) 
            {
                penaltyTimes = new int[0];
            }

            public override void PlayMatch(int foul)
            {
                if (penaltyTimes == null || foul < 0 || foul > 5) return;
                base.PlayMatch(foul);
            }
        }

        public class HockeyPlayer : Participant
        {
            private int allminuts;
            private int count;
            
            public HockeyPlayer(string name, string surname) : base(name, surname)
            {
                count++;
                penaltyTimes = new int[0];
            }

            public override bool IsExpelled
            {
                get
                {
                    if (penaltyTimes == null) return false;
                    for (int i = 0; i < penaltyTimes.Length; i++)
                    {
                        if (penaltyTimes[i] >= 10) return true;
                    }
                    for (int i = 0; i < penaltyTimes.Length; i++)
                    {
                        if (count == 0) return false;
                        if (penaltyTimes.Sum() > 0.1 * allminuts / count)
                        {
                            return true;
                        }
                    }
                    return false;
                }
            }

            public override void PlayMatch(int minuts)
            {
                if (minuts < 0 || minuts > 5) return;
                base.PlayMatch(minuts);
                allminuts += minuts;
            }
        }
    }
}
