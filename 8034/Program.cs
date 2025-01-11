using System;

namespace _8034
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Example usage:
            Teacher[] teachers = new Teacher[]
            {
                new Educator("Educator A", 80, 85),
                new Coordinator("Coordinator B", 70, new Teacher[]
                {
                    new Educator("Educator C", 90, 80),
                    new Advisor("Advisor D", 85, 75)
                }),
                new Advisor("Advisor E", 75, 80)
            };

            OutstandingTeacher(teachers);
        }

        public static void OutstandingTeacher(Teacher[] arr)
        {
            double maxavg = 0;
            Teacher best = null;

            foreach (Teacher t in arr)
            {
                if (t.GetRating() > maxavg)
                {
                    maxavg = t.GetRating();
                    best = t;
                }
            }


            Console.WriteLine($"Highest ranked teacher: {best.GetName()}, Grade: {best.GetRating()}");
        }

        public static void OutstandingTeacherObj(object[] arr)
        {
            double maxavg = 0;
            Teacher best = null;

            foreach (object obj in arr)
            {
                if (obj is Teacher)  
                {
                    Teacher teacher = (Teacher)obj;
                    if (teacher.GetRating() > maxavg)
                    {
                        maxavg = teacher.GetRating();
                        best = teacher;
                    }
                }
            }

            //using safe casting: 
            /*

            foreach (object obj in arr)
            {
                if (obj is Teacher teacher && teacher.GetRating() > maxavg)
                {
                    maxavg = teacher.GetRating();
                    best = teacher;
                }
            }
            */


            //Console.WriteLine($"Highest ranked teacher: {best.GetName()}, Type: {best.GetType().Name}, Grade: {best.GetRating()}");
            //the below can be used if GetType cannot be used
            Console.WriteLine($"Highest ranked teacher: {best.GetName()}, Grade: {best.GetRating()}");
            if (best is Educator)
                Console.WriteLine("Educator");
            else if (best is Advisor)
                Console.WriteLine("Advisor");
            else if (best is Coordinator)
                Console.WriteLine("Coordinator");
            else
                Console.WriteLine("Teacher");


        }
    }

    public class Teacher
    {
        protected double studentAvg;
        protected string name;

        public Teacher(string name, double studentAvg)
        {
            this.name = name;
            this.studentAvg = studentAvg;
        }

        public virtual double GetRating()
        {
            return studentAvg;
        }

        public string GetName()
        {
            return name;
        }
        
    }

    public class Educator : Teacher
    {
        private double eduAvg;

        public Educator(string name, double studentAvg, double eduAvg)
            : base(name, studentAvg)
        {
            this.eduAvg = eduAvg;
        }

        public override double GetRating()
        {
            return base.GetRating() * 0.3 + eduAvg * 0.7;
        }
    }

    public class Advisor : Teacher
    {
        private double parentsAvg;

        public Advisor(string name, double studentAvg, double parentsAvg)
            : base(name, studentAvg)
        {
            this.parentsAvg = parentsAvg;
        }

        public override double GetRating()
        {
            return base.GetRating() * 0.5 + parentsAvg * 0.5;
        }
    }

    public class Coordinator : Teacher
    {
        private Teacher[] teachers;

        public Coordinator(string name, double studentAvg, Teacher[] teachers)
            : base(name, studentAvg)
        {
            this.teachers = teachers;
        }

        public override double GetRating()
        {
            double avg = 0;

            foreach (Teacher t in teachers)
            {
                avg += t.GetRating();
            }

            return base.GetRating() * 0.3 + avg / teachers.Length * 0.7;
        }
    }


}
