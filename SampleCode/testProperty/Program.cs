using System;

namespace testProperty
{
      class Program
      {
            class Employee
            {
                  string firstName,lastName;
                  public string FirstName 
                  {
                        get
                        {
                              return firstName;
                        }

                        set
                        {
                              if ( value=="Jim" )
                              {
                                    Console.WriteLine("Oh,you entered Jim");
                              }
                        }
                  }

                  public string LastName
                  {
                        get
                        {
                              return lastName;
                        }

                        set
                        {
                              if ( value == "Green" )
                              {
                                    Console.WriteLine("Oh,you entered Green");
                              }
                        }
                  }
                 

                 public Employee(string _firstName,string _lastName )
                  {
                        FirstName = _firstName;
                        LastName = _lastName;
                  }


            }
            static void Main ( string [ ] args )
            {
                  Employee a=new Employee("Jim","Lily");
                  Console.WriteLine("Succss");
            }
      }
}
