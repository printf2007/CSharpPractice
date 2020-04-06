using System;
using System.Collections.Generic;
using System.Text;

namespace DesignModeTest
{
      class AbstructStudentDoHomework:AbstructStudent
      {
            AbstructStudent _student=null;
            public AbstructStudentDoHomework ( AbstructStudent student )
            {
                  _student = student;
            }
            public override  void Study()
            {
                  _student.Study( );
                  Console.WriteLine("do homework");
            }
      }
}
