using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_7
{
    public class Blue_2
    {
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
                    if (marks == null || marks.GetLength(0) == 0 || marks.GetLength(1) == 0)
                        return null;

                    int[,] copy = new int[marks.GetLength(0), marks.GetLength(1)];
                    Array.Copy(marks, copy, marks.Length);
                    return copy;
                }
            }

            public int TotalScore
            {
                get
                {
                    if (marks == null || marks.GetLength(0) == 0 || marks.GetLength(1) == 0)
                        return 0;

                    int sum = 0;
                    foreach (int mark in marks)
                        sum += mark;
                    return sum;
                }
            }

            public Participant(string name, string surname)
            {
                this.name = name;
                this.surname = surname;
                this.marks = new int[2, 5];
                this.ind = 0;
            }

            public void Jump(int[] result)
            {
                if (result == null || result.Length == 0 || ind >= 2)
                    return;

                int elementsToCopy = Math.Min(result.Length, 5);
                Array.Copy(result, 0, marks, ind * 5, elementsToCopy);
                ind++;
            }

            public static void Sort(Participant[] array)
            {
                if (array == null || array.Length <= 1)
                    return;

                for (int i = 0; i < array.Length - 1; i++)
                {
                    for (int j = 0; j < array.Length - i - 1; j++)
                    {
                        if (array[j].TotalScore < array[j + 1].TotalScore)
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
                Console.WriteLine($"Участник: {name} {surname}");
                if (marks != null)
                {
                    for (int i = 0; i < marks.GetLength(0); i++)
                    {
                        Console.Write($"Прыжок {i + 1}: ");
                        for (int j = 0; j < marks.GetLength(1); j++)
                            Console.Write($"{marks[i, j]} ");
                        Console.WriteLine();
                    }
                }
                Console.WriteLine($"Общий балл: {TotalScore}");
            }
        }

        public abstract class WaterJump
        {
            private string tournamentName;
            private int prizeFund;
            private Participant[] participants;
            protected int participantCount;

            public string Name => tournamentName;
            public int Bank => prizeFund;
            public Participant[] Participants => participants;

            public abstract double[] Prize { get; }

            protected WaterJump(string tournamentName, int prizeFund)
            {
                this.tournamentName = tournamentName;
                this.prizeFund = prizeFund;
                this.participants = new Participant[0]; 
                this.participantCount = 0;
            }

            public void Add(Participant participant)
            {
                if (participants.Length == 0)
                    participants = new Participant[6]; // Ленивая инициализация

                if (participantCount < participants.Length)
                    participants[participantCount++] = participant;
            }

            public void Add(params Participant[] newParticipants)
            {
                foreach (var p in newParticipants)
                    Add(p);
            }
        }

        public class WaterJump3m : WaterJump
        {
            public WaterJump3m(string tournamentName, int prizeFund) 
                : base(tournamentName, prizeFund) { }

            public override double[] Prize
            {
                get
                {
                    if (participantCount < 3)
                        return new double[0];

                    return new double[] 
                    { 
                        Bank * 0.5, 
                        Bank * 0.3, 
                        Bank * 0.2 
                    };
                }
            }
        }

        public class WaterJump5m : WaterJump
        {
            public WaterJump5m(string tournamentName, int prizeFund) 
                : base(tournamentName, prizeFund) { }

            public override double[] Prize
            {
                get
                {
                    if (participantCount < 3)
                        return new double[0];

                    int prizeCount = Math.Min(10, participantCount);
                    double[] prizes = new double[prizeCount];
                    
                    prizes[0] = Bank * 0.4;
                    prizes[1] = Bank * 0.25;
                    prizes[2] = Bank * 0.15;

                    double N = 20.0 / Math.Max(1, participantCount / 2);
                    for (int i = 3; i < prizeCount; i++)
                        prizes[i] = N * (Bank / 100);

                    return prizes;
                }
            }
        }
    }
}
               
