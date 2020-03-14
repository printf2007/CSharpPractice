using System;
using System.Linq;

namespace testLinq
{
     class Student
      {
            public string name;
            public int id;
      }

      class Course
      {
            public int id;
            public string CourseName;
      }
      class Program
      {



            static void Main ( string [ ] args )
            {

                  Student[] student=new Student[6]
                  {
                      new Student{id=1,name="Jack"},
                      new Student{id=2,name="Lily"},
                      new Student{id=3,name="Emily"},
                      new Student{id=4,name="Tom"},
                      new Student{id=5,name="Sam"},
                      new Student{id=6,name="Jim"},
                  };

                  Course[] course=new Course[6]
                  {
                      new Course{id=1,CourseName="English"},
                      new Course{id=2,CourseName="Chinese"},
                      new Course{id=3,CourseName="nature"},
                      new Course{id=4,CourseName="English"},
                      new Course{id=5,CourseName="Physic"},
                      new Course{id=6,CourseName="chmis"},
                  };

                  var query=from Student s in student
                            join Course c in course
                                     on s.id equals c.id
                            where c.CourseName=="English"
                            select s.name;

                  foreach ( var item in query )
                  {
                        Console.WriteLine($"The student select English is {item}");
                  }
            }                         
      }
}
