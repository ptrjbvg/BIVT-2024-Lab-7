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
                    {
                        return null;
                    }
                    int[,] marksCopy = new int[marks.GetLength(0), marks.GetLength(1)];
                    for (int i = 0; i < marks.GetLength(0); i++)
                    {
                        for (int j = 0; j < marks.GetLength(1); j++)
                        {
                            marksCopy[i, j] = marks[i, j];
                        }
                    }
                    return marksCopy;
                }
            }

            public int TotalScore
            {
                get
                {
                    int sum = 0;
                    if (marks == null || marks.GetLength(0) == 0 || marks.GetLength(1) == 0)
                    {
                        return 0;
                    }

                    for (int i = 0; i < marks.GetLength(0); i++)
                    {
                        for (int j = 0; j < marks.GetLength(1); j++)
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
                this.ind = 0;
            }

            public void Jump(int[] result)
            {
                if (marks == null || marks.GetLength(0) == 0 || marks.GetLength(1) == 0 || result == null || result.Length == 0 || ind > 1)
                {
                    return;
                }

                if (ind < 2) 
                {
                    for (int i = 0; i < 5 && i < result.Length; i++) 
                    {
                        marks[ind, i] = result[i];
                    }
                    ind++;
                }
            }

            public static void Sort(Participant[] array)
            {
                if (array == null || array.Length == 0)
                    return;

                for (int i = 0; i < array.Length - 1; i++)
                {
                    for (int j = 0; j < array.Length - i - 1; j++)
                    {
                        if (array[j + 1].TotalScore > array[j].TotalScore)
                        {
                            (array[j + 1], array[j]) = (array[j], array[j + 1]);
                        }
                    }
                }
            }

            public void Print()
            {
                Console.WriteLine($"Участник: {name} {surname}");
                Console.WriteLine("Оценки за прыжки:");

                if (marks != null)
                {
                    for (int i = 0; i < marks.GetLength(0); i++)
                    {
                        Console.Write($"Прыжок {i + 1}: ");
                        for (int j = 0; j < marks.GetLength(1); j++)
                        {
                            Console.Write($"{marks[i, j]} ");
                        }
                        Console.WriteLine();
                    }
                }
                else
                {
                    Console.WriteLine("Оценки отсутствуют.");
                }

                Console.WriteLine($"Общий балл: {TotalScore}");
            }
        }

        public abstract class WaterJump
        {
            private string tournamentName;
            private int prizeFund;
            private Participant[] participants; 
            private int participantCount; 

            public string Name => tournamentName;
            public int Bank => prizeFund;

            public IReadOnlyList<Participant> Participants => Array.AsReadOnly(participants); 

            public abstract double[] Prize { get; }

            protected WaterJump(string tournamentName, int prizeFund)
            {
                this.tournamentName = tournamentName;
                this.prizeFund = prizeFund;
                this.participants = new Participant[6];
                this.participantCount = 0; 
            }

            public void Add(Participant participant)
            {
                if (participantCount < participants.Length)
                {
                    participants[participantCount] = participant;
                    participantCount++;
                }
                else
                {
                    Console.WriteLine("Команда полная, не удается добавить больше участников.");
                }
            }

            public void Add(params Participant[] newParticipants)
            {
                foreach (var participant in newParticipants)
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
                    if (participantCount < 3) return new double[0]; 

                    double[] prizes = new double[3];
                    prizes[0] = Bank * 0.5; 
                    prizes[1] = Bank * 0.3; 
                    prizes[2] = Bank * 0.2; 

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
                    if (participantCount < 3) return new double[0]; 

                    double N = 20.0 / Math.Max(1, participantCount / 2); 
                    double percentageFirstPlace = 0.4;
                    double percentageSecondPlace = 0.25;
                    double percentageThirdPlace = 0.15;

                    double[] prizes = new double[participantCount > 10 ? 10 : participantCount];
                    int topCount = Math.Min(3, participantCount); 

                    for (int i = 0; i < topCount; i++)
                    {
                        if (i == 0) prizes[i] = Bank * percentageFirstPlace; 
                        else if (i == 1) prizes[i] = Bank * percentageSecondPlace; 
                        else if (i == 2) prizes[i] = Bank * percentageThirdPlace; 
                    }

                    for (int i = topCount; i < prizes.Length && i < participantCount; i++)
                    {
                        prizes[i] = N * (Bank / 100);
                    }

                    return prizes;
                }
            }
        }
    }
}
