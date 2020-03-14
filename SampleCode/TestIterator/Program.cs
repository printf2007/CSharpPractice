using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.VisualBasic;

namespace TestIterator
{
      class MyClass
      {
            public IEnumerable<string> aaa ( )
            {
                  yield return "blue";
                  yield return "blue1";
                  yield return "blue2";
            }

            public IEnumerator<string> GetEnumerator ( )
            {
                  IEnumerable<string> bbb=aaa();
                  return bbb.GetEnumerator( );
            }
      }
      class Program
      {
            static void Main ( string [ ] args )
            {
                  MyClass mc=new MyClass();
                  foreach ( var item in mc )
                  {
                        Console.WriteLine($"{item}");
                  }

            }
      }
}
