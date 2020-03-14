using System;

namespace TestExtend
{
      interface IPerson
      {
            string FirstName
            {
                  get;
                  set;
            }

            string LastName
            {
                  get;
                  set;
            }

            // void VerifyCredentials ( string value );
      }

      static class PersonExtend
      {
            public static void VerifyCredentials ( this IPerson p )
            {
                  System.Console.WriteLine("HelloWorld");
            }
      }

      public class Person : IPerson
      {
            // ...
            public string FirstName { get; set; }
            public string LastName { get; set; }



            public void VerifyCredentials ( string b )
            {
                  System.Console.WriteLine($"Everybody come {b}");
            }
      }

      public class Contact : PdaItem, IPerson
      {
            public Contact ( string name )
                : base(name)
            {
            }

            private Person Person
            {
                  get { return _Person; }
                  set { _Person = value; }
            }
            private Person _Person;

            public string FirstName
            {
                  get { return _Person.FirstName; }
                  set { _Person.FirstName = value; }
            }

            public string LastName
            {
                  get { return _Person.LastName; }
                  set { _Person.LastName = value; }
            }

            public void VerifyCredentials ( string a )
            { }
      }
      public abstract class PdaItem
      {
            public PdaItem ( string name )
            {
                  Name = name;
            }

            public virtual string Name { get; set; }
      }
      class Program
      {
            static void Main ( string [ ] args )
            {
                  Contact a=new Contact("Jack");
                  a.VerifyCredentials( );
            }
      }


}
