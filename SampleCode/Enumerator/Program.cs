using System;

namespace Enumerator
{
      class Program
      {
            static void Main ( string [ ] args )
            {
                  Spectrum a=new Spectrum();
                  foreach ( var item in a )
                  {
                        Console.WriteLine(item);
                  }
            }
      }
}
