using System;

namespace testRef
{
      class Program
      {

            class Ref
            {
                public  int x;
            }

            struct Val
            {
                 public  int x;
            }
            static void Main ( string [ ] args )
            {
                  Ref r1=new Ref();
                  Val v1=new Val();

                  r1.x = 5;
                  v1.x = 5;

                  Ref r2=r1;
                  Val v2=v1;

                  r2.x = 8;
                  v2.x = 9;
                  Console.WriteLine($"r1.x is {r1.x}");
                  Console.WriteLine($"v1.x is {v1.x}");
                  Console.WriteLine($"r2.x is {r2.x}");
                  Console.WriteLine($"v2.x is {v2.x}");


            }
      }
}
