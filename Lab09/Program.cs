using System.Text.Json;
using System.IO;
using System.Collections;
using static System.Net.WebRequestMethods;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Lab9Demo
{
    public class Program
    {
        static void Main(string[] args)
        {
            string json = System.IO.File.ReadAllText("/Users/connerthompson/Projects/Lab09/Lab09/data.json");
            //Console.WriteLine(json);

            FeatureCollection featureCollection = JsonSerializer.Deserialize<FeatureCollection>(json);
            Console.WriteLine("Deserialized the json data");

            Location[] locations = featureCollection.features;
            //Console.WriteLine(locations);

            //Output all of the neighborhoods in this data list (Final Total: 147 neighborhoods) (without LINQ)
            Part1(locations);
            //Output all of the neighborhoods in this data list (Final Total: 147 neighborhoods)
            Part1WithLINQ(locations);
            //Filter out all the neighborhoods that do not have any names(Final Total: 143)
            Part2(locations);
            //Remove the duplicates(Final Total: 39 neighborhoods)
            Part3(locations);
            //Rewrite the queries from above and consolidate all into one single query.
            Part4(locations);
            //Rewrite at least one of these questions only using the opposing method
            Part5(locations);

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
        public static void Part1WithLINQ(Location[] items)
        {
            var neighborhoodQuery = from item in items
                                            group item by item.properties.neighborhood into grouped
                                            select new { Key = grouped.Key, Value = grouped.Count() };
            foreach (var location in neighborhoodQuery)
            {
                Console.WriteLine($"{location.Key}: {location.Value}");
            }
        }
        public static void Part2(Location[] items)
        {
            var neighborHoodQuery = from item in items
                                    where item.properties.neighborhood != ""
                                    select item;

            foreach (var location in neighborHoodQuery)
            {
                Console.WriteLine(location.properties.neighborhood);
            }
        }
        public static void Part3(Location[] items)
        {
            var distinctMethod = items
                .Where(item => !string.IsNullOrEmpty(item.properties.neighborhood))
                .Select(item => item.properties.neighborhood)
                .Distinct();

            foreach (string n in distinctMethod)
            {
                Console.WriteLine(n);
            }
        }
        public static void Part4(Location[] items)
        {
            var neighborhoodQuery = items
                .Where(item => !string.IsNullOrEmpty(item.properties.neighborhood))
                .Select(item => item.properties.neighborhood)
                .Distinct();

            foreach (var neighborhood in neighborhoodQuery)
            {
                Console.WriteLine(neighborhood);
            }
        }
        public static void Part5(Location[] items)
        {
            // Part 1 as a Method - phone a friend:
            var neighborHoodQuery1 = items
                .Where(item => !string.IsNullOrEmpty(item.properties.neighborhood))
                .GroupBy(item => item.properties.neighborhood)
                .Select(grouped => new { Key = grouped.Key, Value = grouped.Count() });
        
            // Part 2 as a Method - phone a friend:
            var neighborHoodQuery2 = items
                .Where(item => item.properties.neighborhood != "")
                .Select(item => item);
            Console.WriteLine("SYNTAX QUERY - SELECT ITEM\n");
            foreach (var neighborhood in neighborHoodQuery2)
            {
                Console.WriteLine(neighborhood.properties.neighborhood);
            }
            Console.WriteLine();

            // Part 2 as a Method - BIGGOUDAJOE - wow
            Console.WriteLine("METHOD QUERY - SELECT ITEM.PROPERTIES.NEIGHBORHOOD\n");
            var neighborhoodQueryTwo = items
                .Where(item => !string.IsNullOrEmpty(item.properties.neighborhood));

            foreach (var neighborhood in neighborhoodQueryTwo)
            {
                Console.WriteLine(neighborhood.properties.neighborhood);
            }

            // Part 4 as a Query - phone a friend:
            var neighborhoodQuery4 = items
                .Where(item => !string.IsNullOrEmpty(item.properties.neighborhood))
                .Select(item => item.properties.neighborhood)
                .Distinct();
        }






    }
}

