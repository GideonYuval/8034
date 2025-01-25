using System;

namespace _8034
{
    internal class Program
    {
        static void Main(string[] args)
        {
            RunTests();
        }

        public static void RunTests()
        {
            int testCounter = 1;

            // Test 1: Single teacher
            Teacher[] test1 = {
                new Teacher("Teacher A", 8.5)
            };
            RunTest(test1, "Teacher A", 8.5, ref testCounter);

            // Test 2: Multiple teachers with varying ratings
            Teacher[] test2 = {
    new Teacher("Teacher A", 8.0),
    new Educator("Educator B", 7.5, 9.0), // Rating: 8.55
    new Advisor("Advisor C", 8.5, 7.5),   // Rating: 8.0
    new Coordinator("Coordinator D", 6.0, new Teacher[]
    {
        new Teacher("Teacher E", 9.0),
        new Educator("Educator F", 8.0, 8.5) // Rating: 8.35
    }) // Coordinator rating: 7.675
};
            RunTest(test2, "Educator B", 8.55, ref testCounter);

            // Test 3: All teachers have the same score
            Teacher[] test3 = {
                new Teacher("Teacher A", 8.0),
                new Educator("Educator B", 8.0, 8.0),
                new Advisor("Advisor C", 8.0, 8.0)
            };
            RunTest(test3, "Teacher A", 8.0, ref testCounter);

            // Test 4: Coordinator with no teachers
            Teacher[] test4 = {
    new Coordinator("Coordinator A", 7.5, new Teacher[]
    {
        new Teacher("Teacher B", 8.0)
    })
};
            RunTest(test4, "Coordinator A", 7.85, ref testCounter);

        }

        public static void RunTest(Teacher[] teachers, string expectedName, double expectedRating, ref int testCounter)
        {
            Console.WriteLine($"Running Test {testCounter}...");
            testCounter++;

            double maxavg = 0;
            Teacher best = null;

            foreach (Teacher t in teachers)
            {
                if (t.GetRating() > maxavg)
                {
                    maxavg = t.GetRating();
                    best = t;
                }
            }

            if (best.GetName() == expectedName && Math.Abs(best.GetRating() - expectedRating) < 0.0001)
            {
                Console.WriteLine($"Test passed!\n");
            }
            else
            {
                Console.WriteLine($"Test failed!");
                Console.WriteLine($"Expected: Name = {expectedName}, Rating = {expectedRating}");
                Console.WriteLine($"Actual: Name = {best.GetName()}, Rating = {best.GetRating()}\n");
            }
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

        public override double GetRating()
        {
            return base.GetRating() * 0.3 + eduAvg * 0.7;
        }

        public Educator(string name, double studentAvg, double eduAvg)
    : base(name, studentAvg)
        {
            this.eduAvg = eduAvg;
        }
    }

    public class Advisor : Teacher
    {
        private double parentsAvg;

        public override double GetRating()
        {
            return base.GetRating() * 0.5 + parentsAvg * 0.5;
        }

        public Advisor(string name, double studentAvg, double parentsAvg)
    : base(name, studentAvg)
        {
            this.parentsAvg = parentsAvg;
        }
    }

    public class Coordinator : Teacher
    {
        private Teacher[] teachers;

        public override double GetRating()
        {
            double avg = 0;

            foreach (Teacher t in teachers)
                avg += t.GetRating();

            return base.GetRating() * 0.3 + avg / teachers.Length * 0.7;
        }

        public Coordinator(string name, double studentAvg, Teacher[] teachers)
    : base(name, studentAvg)
        {
            this.teachers = teachers;
        }

    }


}
