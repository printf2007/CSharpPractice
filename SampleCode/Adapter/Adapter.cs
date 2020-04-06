using System;
using System.Collections.Generic;
using System.Text;

namespace Adapter
{
      class Adapter:MyRedis,IHelper
      {
            public void Modify ( )
            {
                  Console.WriteLine("MyRedisModify");
            }
            public void Query ( )
            {
                  Console.WriteLine("MyRedisQuery");
            }
      }
}
