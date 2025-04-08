using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_7
{
    public class Blue_4
    {
        public abstract class Team
        {
            private string name;
            private int[] scores;

            public string Name
            {
                get { return name; }
            }

            public int[] Scores
            {
                get
                {
                    if (scores == null)
                        return null;
                    int[] copy = new int[scores.Length];
                    Array.Copy(scores, copy, copy.Length);
                    return copy;
                }
            }

            public int TotalScore
            {
                get
                {
                    if (scores == null)
                        return 0;

                    int sum = 0;
                    foreach (int v in scores)
                    {
                        sum += v;
                    }
                    return sum;
                }
            }

            protected Team(string name)
            {
                this.name = name;
                this.scores = new int[0];
            }

            public void PlayMatch(int result)
            {
                if (scores == null) 
                    return;

                int[] newScores = new int[scores.Length + 1];
                Array.Copy(scores, newScores, scores.Length);
                newScores[newScores.Length - 1] = result;
                scores = newScores;
            }

            public void Print()
            {
                Console.WriteLine($"{Name}: {TotalScore}");
            }
        }

        public class ManTeam : Team
        {
            public ManTeam(string name) : base(name) { }
        }

        public class WomanTeam : Team
        {
            public WomanTeam(string name) : base(name) { }
        }

        public class Group
        {
            private string name;
            private ManTeam[] manTeams;
            private WomanTeam[] womanTeams;
            private int manCount;
            private int womanCount;

            public string Name
            {
                get { return name; }
            }

            public ManTeam[] ManTeams
            {
                get { return manTeams; }
            }

            public WomanTeam[] WomanTeams
            {
                get { return womanTeams; }
            }

            public Group(string name)
            {
                this.name = name;
                this.manTeams = new ManTeam[12];
                this.womanTeams = new WomanTeam[12];
                manCount = 0;
                womanCount = 0;
            }

            public void Add(Team team)
            {
                if (team is ManTeam && manCount < 12)
                {
                    manTeams[manCount++] = (ManTeam)team;
                }
                else if (team is WomanTeam && womanCount < 12)
                {
                    womanTeams[womanCount++] = (WomanTeam)team;
                }
            }

            public void Add(params Team[] newTeams)
            {
                foreach (var team in newTeams)
                {
                    Add(team);
                }
            }

            public void Sort()
            {
                SortTeams(manTeams, manCount);
                SortTeams(womanTeams, womanCount);
            }

            private void SortTeams(Team[] teamsArray, int count)
            {
                if (teamsArray == null || count == 0)
                    return;

                for (int i = 0; i < count - 1; i++)
                {
                    for (int j = 0; j < count - i - 1; j++)
                    {
                        if (teamsArray[j].TotalScore < teamsArray[j + 1].TotalScore)
                        {
                            (teamsArray[j], teamsArray[j + 1]) = (teamsArray[j + 1], teamsArray[j]);
                        }
                    }
                }
            }

            public static Group Merge(Group group1, Group group2, int size)
            {
                Group finalists = new Group("Финалисты");
                
                MergeTeams(group1.ManTeams, group2.ManTeams, finalists.Add, size);
                MergeTeams(group1.WomanTeams, group2.WomanTeams, finalists.Add, size);

                return finalists;
            }

            private static void MergeTeams(Team[] teams1, Team[] teams2, Action<Team> addAction, int size)
            {
                int i = 0, j = 0;
                while (i < Math.Min(size / 2, teams1.Length) && j < Math.Min(size / 2, teams2.Length))
                {
                    if (teams1[i].TotalScore >= teams2[j].TotalScore)
                    {
                        addAction(teams1[i++]);
                    }
                    else
                    {
                        addAction(teams2[j++]);
                    }
                }
                while (i < Math.Min(size / 2, teams1.Length))
                {
                    addAction(teams1[i++]);
                }
                while (j < Math.Min(size / 2, teams2.Length))
                {
                    addAction(teams2[j++]);
                }
            }

            public void Print()
            {
                Console.WriteLine($"Группа: {name}");
                Console.WriteLine("Мужские команды:");
                PrintTeams(manTeams);

                Console.WriteLine("Женские команды:");
                PrintTeams(womanTeams);
            }

            private void PrintTeams(Team[] teamsArray)
            {
                if (teamsArray.Length == 0 || teamsArray == null)
                {
                    Console.WriteLine("Нет данных о командах.");
                    return;
                }

                for (int i = 0; i < teamsArray.Length; i++)
                {
                    if (teamsArray[i] != null)
                    {
                        Console.WriteLine($"Команда {i + 1}:");
                        teamsArray[i].Print();
                    }
                }
            }
        }
    }
}
