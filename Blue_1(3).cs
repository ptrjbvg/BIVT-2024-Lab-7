using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_7
{
    public class Blue_1
    {
        public class Response
        {
            private string name;
            protected int votes;

            public string Name
            {
                get => name;
            }

            public int Votes
            {
                get => votes;
            }

            public Response(string name)
            {
                this.name = name;
                this.votes = 0;
            }

            public virtual int CountVotes(Response[] responses)
            {
                int count = 0;
                foreach (var response in responses)
                {
                    if (response.Name == this.Name)
                    {
                        count++;
                    }
                }

                this.votes = count;
                return count;
            }

            public virtual void Print()
            {
                Console.WriteLine($"Name: {Name}");
                Console.WriteLine($"Votes: {Votes}");
            }
        }

        public class HumanResponse : Response
        {
            private string surname;

            public HumanResponse(string name, string surname) : base(name)
            {
                this.surname = surname;
            }

            public override int CountVotes(Response[] responses)
            {
                int count = 0;
                foreach (var response in responses)
                {
                    if (response is HumanResponse human && human.Name == this.Name && human.surname == this.surname)
                    {
                        count++;
                    }
                }

                this.votes = count;
                return count;
            }

            public override void Print()
            {
                Console.WriteLine($"Name: {Name}");
                Console.WriteLine($"Surname: {surname}");
                Console.WriteLine($"Votes: {Votes}");
            }
        }
    }
}
