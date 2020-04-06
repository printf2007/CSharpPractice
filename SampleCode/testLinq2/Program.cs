using System;
using System.Collections.Generic;
using System.IO;
using System.Transactions;
using System.Linq;

namespace testLinq2
{
      class Program
      {
            static void Main ( string [ ] args )
            {
                  List1(@"C:\MyGit\CSharpPractice" , "*.cs");
            }

            static void List1(string rootDirestory,string searchPattern )
            {
                  IEnumerable<string > fileNames=Directory.GetFiles(rootDirestory,searchPattern);
                  IEnumerable<FileInfo> fileInfos=
                        from fileName in fileNames
                        select new FileInfo(fileName);

                  foreach ( FileInfo fileInfo in fileInfos )
                  {
                        Console.WriteLine(".{0}({1})",fileInfo.Name,fileInfo.LastWriteTime);       
                  }
            }

      }
}
