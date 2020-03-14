using System;
using System.Dynamic;

namespace EventTest
{
      class Heater
      {
            float DeafaultTemp;

            public Heater ( float _temp )
            {
                  DeafaultTemp = _temp;
            }

            public float DeafaultTemp1 { get => DeafaultTemp; set => DeafaultTemp = value; }

          public  void TempChanged ( float temp )
            {
                  if ( temp < DeafaultTemp )
                  {
                        Console.WriteLine("Turn Heater On");
                  }
                  else
                  {
                        Console.WriteLine("Turn Heater Off");
                  }
            }
      }

      class Cooler
      {
            float DeafaultTemp;

            public Cooler ( float _temp )
            {
                  DeafaultTemp = _temp;
            }

            public float DeafaultTemp1 { get => DeafaultTemp; set => DeafaultTemp = value; }

           public void TempChanged ( float temp )
            {
                  if ( temp > DeafaultTemp )
                  {
                        Console.WriteLine("Turn Cooler On");
                  }
                  else
                  {
                        Console.WriteLine("Turn Cooler Off");
                  }
            }
      }

      class Conditioner
      {
            float _CurrentTemp;
            public Action<float>  OnTemperatureChange;

            public float CurrentTemp
            {
                  get
                  { return _CurrentTemp; }
                  set
                  {
                        if(value != CurrentTemp )
                        {
                              _CurrentTemp = value;
                              OnTemperatureChange(value);
                        }
                  }

            }


      }
      class Program
      {
            static void Main ( string [ ] args )
            {
                  Conditioner conditioner=new Conditioner();
                  Heater heater=new Heater(60);
                  Cooler cooler=new Cooler(80);
                  string temperature;

                  conditioner.OnTemperatureChange += heater.TempChanged;
                  conditioner.OnTemperatureChange += cooler.TempChanged;

                  Console.WriteLine("Please Enter an Temperature") ;
                  temperature = Console.ReadLine( );
                  conditioner.CurrentTemp = int.Parse(temperature);

            }
      }
}
