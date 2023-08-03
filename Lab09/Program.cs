using System.Text.Json;
using System.IO;
using System.Collections;

namespace Lab9Demo
{
    public class Program
    {
        static void Main(string[] args)
        {
            string json = File.ReadAllText("/Users/connerthompson/Projects/Lab09/Lab09/data.json");
            //Console.WriteLine(json);

            FeatureCollection featureCollection = JsonSerializer.Deserialize<FeatureCollection>(json);
            Console.WriteLine("Deserialized the json data");

            Location[] locations = featureCollection.features;
            //Console.WriteLine(locations);
            Part1(locations);
            Part1WithLINQ(locations);

            Console.ReadKey();
            //Part2();

        }
        public static void Part1(Location[] items)
        {
            Dictionary<string, int> locationAppearances = new Dictionary<string, int>();
            for (int i = 0; i < items.Length; i++)
            {
                Location currentLocation = items[i];
                string neighborhood = currentLocation.properties.neighborhood;
                bool neighborhoodAlreadyInDictionary = locationAppearances.ContainsKey(neighborhood);
                if (neighborhoodAlreadyInDictionary == false)
                {
                    locationAppearances.Add(neighborhood, 1);
                }
                else
                {
                    int currentValue = locationAppearances.GetValueOrDefault(neighborhood);
                    int newValue = currentValue + 1;
                    locationAppearances[neighborhood] = newValue;

                }
            }

            foreach (var location in locationAppearances)
            {
                Console.WriteLine($"{location.Key}: {location.Value}");
            }


        }
        public void Part1WithLINQ(Location[] items)
        {
            var neighborhoodQuery = from item in items
                                            group item by item.properties.neighborhood into grouped
                                            select new { Key = grouped.Key, Value = grouped.Count() };
            foreach (var location in neighborhoodQuery)
            {
                Console.WriteLine($"{location.Key}: {location.Value}");
            }
        }
       
        
    }
}

