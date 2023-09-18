using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW2
{
    public class Person
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public double Salary { get; set; }
    }

    class Program
    {
        private static List<Person> personList = new List<Person>();

        public static void Main(string[] args)
        {
            Display();
            PerformFunction();
        }

        public static void Display()
        {
            Console.WriteLine("Enter Person Information< Enter to break>:");

            while (true)
            {
                Console.Write("Name: ");
                string name = Console.ReadLine();

                if (name=="")
                    break;

                Console.Write("Address: ");
                string address = Console.ReadLine();

                double salary;
                while (true)
                {
                    Console.Write("Salary: ");
                    string salaryInput = Console.ReadLine();

                    if (double.TryParse(salaryInput, out salary))
                        break;

                    Console.WriteLine("Type Of Salary Is Double");
                }

                personList.Add(new Person { Name = name, Address = address, Salary = salary });

                // Hien thi thong tin nhan vien sau moi lan nhap
                Console.WriteLine("Information of Person you have entered:");
                Console.WriteLine("Name: " + name);
                Console.WriteLine("Address: " + address);
                Console.WriteLine("Salary: " + salary);
                Console.WriteLine();
            }
            Console.Clear();
        }

        public static void PerformFunction()
        {
            personList.Sort((p1, p2) => p1.Salary.CompareTo(p2.Salary));

            Console.WriteLine("Sorted Person List (Ascending Salary):");
            int count = Math.Min(3, personList.Count);//Hien thi nhan vien theo thu tu
            for (int i = 0; i < count; i++)
            {
                Console.WriteLine("Information of Person you have entered:");
                Console.WriteLine("Name: " + personList[i].Name);
                Console.WriteLine("Address: " + personList[i].Address);
                Console.WriteLine("Salary: " + personList[i].Salary);
                Console.WriteLine();
            }
            Console.ReadLine();
            
        }

    }
}
