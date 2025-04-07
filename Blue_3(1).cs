using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_6
{
    public class Blue_3
    {
        public struct Participant
        {
            private string name;
            private string surname;
            private int[] penaltyTimes;

            public Participant(string name, string surname)
            {
                this.name = name;
                this.surname = surname;
                this.penaltyTimes = new int[0];
            }

            public string Name
            {
                get
                {
                    if (name == null)
                        return null;
                    return name;
                }
            }

            public string Surname
            {
                get
                {
                    if (surname == null)
                        return null;
                    return surname;
                }
            }
            public int[] PenaltyTimes
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

            public int TotalTime
            {
                get
                {
                    if (penaltyTimes == null)
                        return 0;

                    int k = 0;
                    foreach (int t in penaltyTimes)
                    {
                        k += t;
                    }
                    return k;
                }
            }

            public bool IsExpelled
            {
                get
                {
                    if (penaltyTimes == null)
                        return false;

                    foreach (int t in penaltyTimes)
                    {
                        if (t == 10)
                        {
                            return true;
                        }
                    }
                    return false;
                }
            }
            public void PlayMatch(int time)
            {
                if (penaltyTimes == null)
                    return;

                int[] newArray = new int[penaltyTimes.Length + 1];

                for (int i = 0; i < penaltyTimes.Length; i++)
                {
                    newArray[i] = penaltyTimes[i];
                }
                newArray[newArray.Length - 1] = time;
                penaltyTimes = newArray;
            }

            public static void Sort(Participant[] array)
            {
                if (array == null) return;
                for (int i = 0; i < array.Length; i++)
                {
                    for (int j = 0; j < array.Length - i - 1; j++)
                    {
                        if (array[j].TotalTime > array[j + 1].TotalTime)
                        {
                            Participant temp = array[j];
                            array[j] = array[j + 1];
                            array[j + 1] = temp;
                        }
                    }
                }
            }

            public void Print()
            {
                Console.Write("Name: ");
                Console.WriteLine(name);

                Console.Write("Surname: ");
                Console.WriteLine(surname);

                Console.Write("PenaltyTimes: ");
                for (int i  = 0; i < penaltyTimes.Length; i++)
                {
                    Console.Write(penaltyTimes[i]);
                    Console.Write(" ");
                }
            }
            
        }
    }
}