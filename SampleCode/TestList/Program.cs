using System;
using System.Collections.Generic;
using System.Linq;

namespace TestList
{
      class Program
      {
            static void Main ( string [ ] args )
            {
                  List<string> list1=new List<string>();
                  list1.Add("sily");
                  list1.Add("Jacky");
                  list1.Add("Lucy");
                  list1.Add("Lily0");

                  list1.Sort();

                  foreach ( var item in list1 )
                  {
                        Console.WriteLine(item) ;
                  }

                  Console.WriteLine(list1[2]);

                  Dictionary<char,string> myDictionary=new Dictionary<char, string>();

                  myDictionary.Add('a' , "allen");
                  myDictionary.Add('b' , "Lily");
                  myDictionary.Add('c' , "Jacky");

                  Console.WriteLine(myDictionary['c']);

                  foreach ( var item in myDictionary )
                  {
                        Console.WriteLine("{0},{1}",item.Key,item.Value);


                  }

                  var Keys=myDictionary.Keys;

                  foreach ( var item in Keys )
                  {
                        Console.WriteLine(item);
                  }

                  ICollection<char> Keys1=myDictionary.Keys;
                  Console.WriteLine(Keys1.Count);

                  IEnumerable<string> selection1=
                        from name in myDictionary.Values
                        where name.Length==4
                        select name;

                  foreach ( var item in selection1 )
                  {
                        Console.WriteLine(item);
                  }
                        


            }
      }
}
