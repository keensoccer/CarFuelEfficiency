using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CarFuelEfficiency
{
    class Manufacturer
    {
        public string CompanyName { get; set; }
        public string Country { get; set; }
        public int Year { get; set; }


        public static IEnumerable<Manufacturer> ManufacturerFileToListOfCar2s(string fileName)
        {
            var carDataLines = File.ReadAllLines(fileName);

            var cars = new List<Manufacturer>();

            for (int i = 0; i < carDataLines.Length; i++)
            {
                var carProperties = carDataLines[i].Split(',');

                var car = new Manufacturer();
                car.CompanyName = carProperties[0];
                car.Country = carProperties[1];
                car.Year = int.Parse(carProperties[2]);

                cars.Add(car);

            }
            return cars;
        }

    }
}
