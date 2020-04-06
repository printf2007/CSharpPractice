using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace EventWPF
{
      /// <summary>
      /// Shape.xaml 的交互逻辑
      /// </summary>
      public partial class Shape : Window
      {
            private bool _laugh=false;

            public Shape ( )
            {
                  InitializeComponent( );
            }

            private void OnMediaButtonClick ( object sender , RoutedEventArgs e )
            {
                  media1.Position = TimeSpan.FromSeconds(0);
                  media1.Play( );
            }

            private void SetMouth ( )
            {
                  if ( _laugh )
                  {
                        Mouth.Data = Geometry.Parse("M 40,82 Q 57,65 80,82");

                  }
                  else
                  {
                        Mouth.Data = Geometry.Parse("M 40,74 Q57,95 80,74");
                  }
                  _laugh = !_laugh;
            }

            private void Button_Click ( object sender , RoutedEventArgs e )
            {
                  var window=new Window1();
                  window.Show( );
            }
      }
}
