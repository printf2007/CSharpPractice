using System.Collections.Generic;
using OdeToFood.Core;
using System.Linq;

namespace OdeToFood.Data
{
      public class InMemoryRestaurantData : IRestaurantData
      {
            List<Restaurant> restaurants;
            public InMemoryRestaurantData ( )
            {
                  restaurants = new List<Restaurant>
                  {
                        new Restaurant{Id=1,Name="Scot Pizza",Location="MaryLand",Cuisine=CuisineType.Italian },
                        new Restaurant{Id=2,Name="Cinamon Club    ",Location="London",Cuisine=CuisineType.Indian },
                        new Restaurant{Id=3,Name="La Costa",Location="Californian",Cuisine=CuisineType.Mexican }
                  };
            }

            public Restaurant Add ( Restaurant newRestaurant )
            {
                  restaurants.Add(newRestaurant);
                  newRestaurant.Id = restaurants.Max(r => r.Id) + 1;
                  return newRestaurant;
            }

            public int Commit ( )
            {
                  return 0;
            }

            public Restaurant Delete ( int id )
            {
                  var restaurant=restaurants.FirstOrDefault(r=>r.Id==id);
                  if(restaurant != null )
                  {
                        restaurants.Remove(restaurant);
                  }
                  return restaurant;
            }


            public Restaurant GetById ( int id )
            {
                  throw new System.NotImplementedException( );
            }

            public IEnumerable<Restaurant> GetRestaurantsByName ( string name )
            {
                  return from r in restaurants
                         where string.IsNullOrEmpty(name) || r.Name.StartsWith(name)
                         orderby r.Name
                         select r;
            }

            public Restaurant Update ( Restaurant updatedRestaurant )
            {
                  throw new System.NotImplementedException( );
            }
      }


}
