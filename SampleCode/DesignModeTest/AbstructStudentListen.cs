using System;
using System.Collections.Generic;
using System.Text;

namespace DesignModeTest
{
      class AbstructStudentListen:AbstructStudent
      {
            AbstructStudent _student=null;
            public AbstructStudentListen ( AbstructStudent student )
            {
                  _student = student;
            }
            public override  void Study()
            {
                  _student.Study( );
                  Console.WriteLine("Listen");
            }
      }
}
