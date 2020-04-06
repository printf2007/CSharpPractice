using System;
using System.Collections.Generic;
using System.Text;

namespace Adapter
{
      class Oracle:IHelper
      {
            public void Add ( )
            {
                  Console.WriteLine("OracleAdd");
            }
            public void Delete ( )
            {
                  Console.WriteLine("OracleDelete");
            }
            public void Modify ( )
            {
                  Console.WriteLine("OracleModify");
            }
            public void Query ( )
            {
                  Console.WriteLine("OracleQuery");
            }
      }
}
