using System;

namespace TestFunction
{
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
}
