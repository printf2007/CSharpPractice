using System.Collections;

namespace Enumerator
{
      internal class Spectrum : IEnumerable
      {
            private string [ ] Colors = { "violet" , "red" , "cyan" , "green" , "yellow" , "orange" , "blue" };

            public IEnumerator GetEnumerator ( )
            {
                  return new ColorEnumerator(Colors);
            }
      }
}