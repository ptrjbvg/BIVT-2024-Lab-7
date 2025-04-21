using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication.ExtendedProtection.Configuration;
using System.Text;
using System.Threading.Tasks;

namespace Lab_7
{
    public class Blue_2
    {
        public abstract class WaterJump 
        {
            private string tournamentName;
            private int prizeFund;
            private Participant[] participants;

            public string Name => tournamentName;
            public int Bank => prizeFund;
            public Participant[] Participants => participants;

            public abstract double[] Prize { get; }

            public WaterJump(string tournamentName, int prizeFund)
            {
                this.tournamentName = tournamentName;
                this.prizeFund = prizeFund;
                this.participants = new Participant[0];
            }

            public void Add(Participant participant)
            {
                if (participants == null) return;
                Participant[] newarr = new Participant[participants.Length + 1];
                for(int i = 0; i < participants.Length; i++)
                {
                    newarr[i] = participants[i];
                }
                newarr[participants.Length] = participant;
                participants = newarr;
            }

            public void Add(Participant[] newParticipants)
            {
                if (participants == null || newParticipants == null || newParticipants.Length == 0) return;
                foreach(Participant participant in newParticipants)
                {
                    Add(participant);
                }
            }
        }

        public class WaterJump3m : WaterJump
        {
            public WaterJump3m(string tournamentName, int prizeFund) : base(tournamentName, prizeFund) { }

            public override double[] Prize
            {
                get
                {
                    if (this.Participants.Length <= 3 || this.Participants == null) return default(double[]);
                    double[] prizes = new double[3];
                    prizes[0] = 0.5 * this.Bank;
                    prizes[1] = 0.3 * this.Bank;
                    prizes[2] = 0.2 * this.Bank;
                    return prizes;
                }
            }
        }

        public class WaterJump5m : WaterJump
        {
            public WaterJump5m(string tournamentName, int prizeFund) : base(tournamentName, prizeFund) { }

            public override double[] Prize
            {
                get
                {
                    if (this.Participants.Length < 3 || this.Participants == null) return default(double[]);
                    double[] prizes = new double[Math.Min(this.Participants.Length, 10)];

                    prizes[0] = Bank * 0.40;
                    prizes[1] = Bank * 0.25;
                    prizes[2] = Bank * 0.15; 

                    int halfCount = this.Participants.Length / 2;
                    int topCount = Math.Min(Math.Max(halfCount, 3), 10); 

                    double nPercent = 20.0 / topCount;

                    for (int i = 3; i < topCount; i++)
                    {
                        prizes[i] = Bank * nPercent / 100;
                    }

                    return prizes;
                }
            }
        }

        public struct Participant
        {
            private string name;
            private string surname;
            private int[,] marks;
            private int ind;

            public string Name => name;
            public string Surname => surname;
            public int[,] Marks
            {
                get
                {
                    if (marks == null || marks.GetLength(0) == 0 || marks.GetLength(1) == 0) return null; 
                    int[,] copy = new int[marks.GetLength(0), marks.GetLength(1)];
                    for (int i = 0; i < copy.GetLength(0); i++)
                    {
                        for (int j = 0; j < copy.GetLength(1); j++)
                        {
                            copy[i, j] = marks[i, j];
                        }
                    }
                    return copy;
                }
            }

            public int TotalScore
            {
                get
                {
                    if (marks == null || marks.GetLength(0) == 0 || marks.GetLength(1) == 0) return 0;
                    int sum = 0;
                    for (int i = 0; i < 2; i++)
                    {
                        for (int j = 0; j < 5; j++)
                        {
                            sum += marks[i, j];
                        }
                    }
                    return sum;
                }
            }

            public Participant(string name, string surname)
            {
                this.name = name;
                this.surname = surname;
                this.marks = new int[2, 5];
                this.ind = -1;
            }

            public void Jump(int[] result)
            {
                if (marks == null || result == null) return;
                if (ind >= marks.GetLength(0)) return;
                for (int i = 0; i < 5; i++)
                {
                    marks[ind + 1, i] = result[i];
                }
                ind++;
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
                        if (array[j].TotalScore < array[j + 1].TotalScore)
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
                Console.WriteLine($"{name} {surname} - {TotalScore} ");
            }
        }
    }
}
