using System;

namespace TestFunction
{

    /*   //1.1 Simple Test
    class MyClass
    {
       public double Area(double Height,double Width)
        {
            double s;
            s = Height * Width;
            return s;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            MyClass mc = new MyClass();
            Console.WriteLine($"the area of height 15,width 20 is {mc.Area(15,20)}");
        }
    }
    */

    //1.2 test return
    class MyClass
    {
        public void TimeUpdate()
        {
            int hour = DateTime.Now.Hour;
            if (hour < 12)
                return;
            Console.WriteLine("Hello,It's afternoon");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            MyClass mc = new MyClass();
            mc.TimeUpdate();
        }
    }
}
