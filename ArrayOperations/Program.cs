using System;
using System.IO;
using System.Linq;
using System.Text.Json;
using ArrayOperations.Properties;

namespace ArrayOperations
{
    class Program
    {
        /**
         * Array-operasjoner:
         *  - Legge til nye objekter
         *  - Slette objekter
         *  - Gå gjennom alle
         *  - Søke et spesifikke elementer
         *      - Filtrere
         *      - Sjekke om en liste inneholder en bestemt verdi
         *  - Lengde
         *  - Lage nye ting basert på hvert objekt - map()
         *  - Aggregeringer - reduce(), min, max, sum
         *  - Sortere
         *  - Reversere
         */

        static void Main(string[] args)
        {
            var dataJson = File.ReadAllText("data.txt");
            var data = JsonSerializer.Deserialize<Rootobject>(dataJson);
             var entries = data.entries;

            //var vehicles = new Vehicle[entries.Length];
            //for (var index = 0; index < entries.Length; index++)
            //{
            //    var entry = entries[index];
            //    vehicles[index] = new Vehicle {Weight = Convert.ToInt32(entry.egenvekt)};
            //}


            var vehicles = entries
                .Select(entry => new Vehicle { Weight = Convert.ToInt32(entry.egenvekt) })
                .Where(vehicle => vehicle.Weight < 1000)
                .OrderBy(vehicle => vehicle.Weight)
                .Reverse()
                .Skip(40)
                .Take(10)
                .ToArray();

            var hasVehicleWithWeight456 = vehicles.Any(vehicle=>vehicle.Weight==456);
            var hasAllVehiclesWeight456 = vehicles.All(vehicle=>vehicle.Weight==456);

            var vehicle100 = new Vehicle {Weight = 100};
            var vehicle200 = new Vehicle {Weight = 200};
            var vehicle300 = new Vehicle {Weight = 300};
            var vehicle400 = new Vehicle {Weight = 400};
            var vehicle500 = new Vehicle {Weight = 500};
            var vehiclesA = new[] {vehicle100, vehicle200, vehicle300};
            var vehiclesB = new[] { vehicle500, vehicle400, vehicle300 };



            var vehiclesUnion = vehiclesA.Union(vehiclesB).ToArray();
            var vehiclesExcept = vehiclesA.Except(vehiclesB).ToArray();
            var vehiclesIntersect = vehiclesA.Intersect(vehiclesB).ToArray();


            var myVehicle = entries
                .Select(CreateVehicleFromEntry)
                .FirstOrDefault(vehicle => vehicle.Weight < 1000);

            Console.WriteLine();
        }

        static Vehicle CreateVehicleFromEntry(Entry entry)
        {
            return new Vehicle {Weight = Convert.ToInt32(entry.egenvekt)};
        }
    }
}
