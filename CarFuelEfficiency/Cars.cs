using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CarFuelEfficiency
{
    class Car
    {
        public int Year { get; set; }
        public string Manufacturer { get; set; }
        public string Model { get; set; }
        public double EngDisplay { get; set; }
        public int Cylinders { get; set; }
        public int CityFE { get; set; }
        public int HighwayFE { get; set; }
        public int CombinedFE { get; set; }

        public static IEnumerable<Car> CarFileToListOfCars(string fileName)
        {
            var carDataLines = File.ReadAllLines(fileName);

            var cars = new List<Car>();

            for (int i = 1; i < carDataLines.Length; i++)
            {
                var carProperties = carDataLines[i].Split(',');

                var car = new Car();
                car.Year = int.Parse(carProperties[0]);
                car.Manufacturer = carProperties[1];
                car.Model = carProperties[2];
                car.EngDisplay = double.Parse(carProperties[3]);
                car.Cylinders = int.Parse(carProperties[4]);
                car.CityFE = int.Parse(carProperties[5]);
                car.HighwayFE = int.Parse(carProperties[6]);
                car.CombinedFE = int.Parse(carProperties[7]);

                cars.Add(car);

            }

            return cars;
        }

    }


}
