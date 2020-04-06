using System;
using System.Reflection;
using System.Web

namespace Reflection
{
      class Program
      {
            class People
            {
                 public People(int _id,string _name )
                  {
                        id = _id;
                        name = _name;
                  }

                  public string name { get; set; }
                  public  int id { get; set; }

                  public void PrintName ( )
                  {
                        Console.WriteLine(name);
                  }
            }

            static void Main ( string [ ] args )
            {
                  People MyPeople=new People(1,"Jacky");
                  Console.WriteLine($"Name is {MyPeople.name}");
                  MyPeople.PrintName( );


                  Type t=MyPeople.GetType();
                  foreach ( System.Reflection.MethodInfo method in t.GetMethods() )
                  {
                        Console.WriteLine(method.Name);
                  }
            }
      }
}
