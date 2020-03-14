using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestArrayArguments
{
    class MyClass
    {
        public void ListInts(params int[] inVals)
        {
            if ((inVals != null) && (inVals.Length != 0))
                {
                for (int i = 0; i < inVals.Length; i++)
                {
                    inVals[i] = inVals[i] * 10;
                    Console.WriteLine("{0}", inVals[i]);
                }
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            int first = 5, second = 6, third = 7;

            MyClass mc = new MyClass();
            mc.ListInts(first, second, third);

            Console.WriteLine("{0},{1},{2}",first,second,third);
        }
    }
}
