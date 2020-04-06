using System;
using System.Collections.Generic;
using System.Text;

namespace DesignModeTest
{
      class AbstructStudent
      {
            string Name { get; set; }
            int ID { get; set; }
            public virtual  void Study()
            {
                  Console.WriteLine("Abstruct Study");
            }
      }
}
