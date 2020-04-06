using System;
using System.Collections.Generic;
using System.Text;

namespace Adapter
{
     public interface IHelper
      {
            public void Add ( );
            public void Delete ( );
            public void Modify ( );
            public void Query ( );
      }
}
