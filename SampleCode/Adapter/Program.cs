using System;

namespace Adapter
{
      class Program
      {
            static void Main ( string [ ] args )
            {
                  Console.WriteLine("Hello World!");
                  IHelper helper1=new MySqlHelper();
                  IHelper helper2=new Adapter();
                  helper1.Add( );
                  helper2.Query( );
            }
      }
}
