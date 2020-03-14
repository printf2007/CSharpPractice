using System;
using System.Linq;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Xml.Linq;

namespace Linq
{

      class Program
      {
            static void Main ( string [ ] args )
            {
                  int[] array=new int[6]{3,4,5,6,7,9};

                  var countOdd=array.First(x=>x>=5);

                  Console.WriteLine($"Count of Odd number is {countOdd}");


                  XDocument employees1=
                     new XDocument(
                              new XElement("Employees",
                                  new XElement("name","Bill Wilson"),
                                  new XElement("name","Kate Li")
                              )
                        );
                  employees1.Save("EmployeesFile.xml");

                  XDocument employees2=XDocument.Load("EmployeesFile.xml");

                  Console.WriteLine(employees2);
                  
            }
      }
}
