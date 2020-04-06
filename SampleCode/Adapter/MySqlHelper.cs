using System;
using System.Collections.Generic;
using System.Text;

namespace Adapter
{
      class MySqlHelper:IHelper
      {
            public void Add ( )
            {
                  Console.WriteLine("MySqlAdd");
            }
            public void Delete ( )
            {
                  Console.WriteLine("MySqlDelete");
            }
            public void Modify ( )
            {
                  Console.WriteLine("MySqlModify");
            }
            public void Query ( )
            {
                  Console.WriteLine("MySqlQuery");
            }
      }
}
