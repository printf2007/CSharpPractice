
using System;

namespace DesignModeTest
{
      class Program
      {
            static void Main ( string [ ] args )
            {
                  AbstructStudent student=new AbstructStudent();
                  student=new AbstructStudentVideo(student);
                  student = new AbstructStudentListen(student);
                  student = new AbstructStudentDoHomework(student);
                  student.Study( );
            }
      }
}
