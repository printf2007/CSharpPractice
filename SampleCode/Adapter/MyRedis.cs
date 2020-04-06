using System;
using System.Collections.Generic;
using System.Text;

namespace Adapter
{
     public class MyRedis
      {
            string Name { get; set; }
            public void Speak ( )
            {

            }
            public void Add ( )
            {
                  Console.WriteLine("RedisAdd");
            }

            public void Delete ( )
            {
                  Console.WriteLine("RedisDelete");
            }
      }
}
