using System;
using System.Collections.Generic;

namespace Enumerator
{
      internal class ColorEnumerator<T> : IEnumerator<T>
      {
            private T [ ] _colors;
            private int _position = -1;

            public ColorEnumerator (T [ ] theColors )
            {
                  _colors = new T[ theColors.Length ];
                  for ( int i = 0 ; i < theColors.Length ; i++ )
                  {
                        _colors [ i ] = theColors [ i ];
                  }
            }

            public T Current
            {
                  get
                  {
                        if ( _position == -1 )
                        {
                              throw new InvalidOperationException( );
                        }
                        if ( _position >= _colors.Length )
                        {
                              throw new InvalidOperationException( );
                        }

                        return (T)_colors [ _position ];
                  }
            }

            public bool MoveNext ( )
            {
                  if ( _position < _colors.Length - 1 )
                  {
                        _position++;
                        return true;
                  }
                  else
                        return false;
            }

            public void Reset ( )
            {
                  _position = -1;
            }

            public void Dispose ( )
            {

            }
      }
}