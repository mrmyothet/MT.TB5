using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TB5.ConsoleApp.AdoDotNetSample
{
    public class Student
    {
        public string Name { get; set; }

        public int Age { get; set; }

        public string StudentNo { get; set; }

        public int Marks { get; private set; }

        public Student()
        {

        }

        public void SetMark(int marks)
        {
            Marks = marks;
        }

        public string Grade()
        {
            return Marks > 80 ? "A" : Marks > 60 ? "B" : Marks > 40 ? "C" : "F";
        }

        public void RunStudentClassExample()
        {
            var student = new Student();
            Console.WriteLine("Please, input your name:");
            string name = Console.ReadLine();

            Console.WriteLine("Please, input your age:");
            int age = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Please, input student number:");
            string studentNo = Console.ReadLine();

            Console.WriteLine("Please, input your marks:");
            int marks = Convert.ToInt32(Console.ReadLine());

            student.Name = name;
            student.Age = age;
            student.StudentNo = studentNo;
            student.SetMark(marks);

            Console.WriteLine($"Student: {student.Name}, Student Number: {student.StudentNo}, Marks: {student.Marks},  Grade: {student.Grade()}");
        }
    }
}
