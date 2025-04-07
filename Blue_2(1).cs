using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Lab_6
{
    public class Blue_2
    {
        public struct Participant
        {
            private string name;
            private string surname;
            private int[,] marks;
            private int ind;

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

                if (ind == 0)
                {
                    for (int i = 0; i < 5; i++)
                    {
                        marks[0, i] = result[i];
                    }
                    ind++;
                }
                else if (ind == 1)
                {
                    for (int i = 0; i < 5; i++)
                    {
                        marks[1, i] = result[i];
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
    }
}
