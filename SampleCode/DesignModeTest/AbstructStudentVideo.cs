using System;
using System.Collections.Generic;
using System.Text;

namespace DesignModeTest
{
      class AbstructStudentVideo:AbstructStudent
      {
            AbstructStudent _student=null;
            public  AbstructStudentVideo(AbstructStudent student )
            {
                  _student = student;
            }

            public override  void Study()
            {
                  _student.Study( );
                  Console.WriteLine("See Video");
            }
      }
}
