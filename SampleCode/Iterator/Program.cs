using System;
using System.Collections.Generic;

namespace Iterator
{
      class MyClass
      {
            public IEnumerable<string> aaa ( )
            {
                  yield return "black";
                  yield return "red";
                  yield return "blue";
            }

            public IEnumerator<string> GetEnumerator ( )
            {
                  IEnumerable<string> myEnumerable=aaa();
                  return myEnumerable.GetEnumerator( );
            }
      }
      class Program
      {


            static void Main ( string [ ] args )
            {
                  MyClass mc=new MyClass();

                  foreach ( var item in mc )
                  {
                        Console.WriteLine("{0}",item);
                  }

                  foreach ( var item in mc.aaa() )
                  {
                        Console.WriteLine("{0}" , item);

                  }
            }
      }
}
