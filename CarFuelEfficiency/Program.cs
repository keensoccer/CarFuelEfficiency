using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CarFuelEfficiency
{
    class Program
    {
        static void Main(string[] args)
        {
            var manufacturers = Manufacturer.ManufacturerFileToListOfCar2s("manufacturers.csv");

            var cars = Car.CarFileToListOfCars("fuel.csv");

            /*
            PrintTopNCarsByFuelEfficiences(cars, 6); //2nd parameter can be any number between 0 and 1204.
            Console.WriteLine("*****************************");
            PrintCarStatistics(cars);
            Console.WriteLine("*****************************");
            */

            //PrintTopNCarsByFuelEfficiencesByHeadquarter(cars, manufacturers, 1);

            //PrintTop2CarsByFuelEfficiencyByManufacturer(cars);

            //PrintTop2CarsByFuelEfficiencyByManufacturerUsingExpressionSyntax(cars);

            PrintTop2CarsByFuelEfficiencyByCountryUsingGroupJoin(cars, manufacturers);

            Console.ReadLine();
        }

        public static void PrintTopNCarsByFuelEfficiences(IEnumerable<Car> cars, int n)
        {
            var query =
                from car in cars
                orderby car.CombinedFE descending
                select car;

            Console.WriteLine("The top 6 most fuel efficient cars:");
            foreach (var car in query.Take(n))
            {
                Console.WriteLine($"\tThe {car.Model} has a combined fuel efficiency of {car.CombinedFE}");
            }
        }

        public static void PrintTopNCarsByFuelEfficiencesByHeadquarter(IEnumerable<Car> cars, IEnumerable<Manufacturer> manufacturers, int n)
        {
            //var query =
            //    from car in cars
            //    join manufacturer in manufacturers on car.CompanyName equals manufacturer.CompanyName
            //    group car by manufacturer.Country into newGroup
            //    orderby manufacturer.Country descending, car.CombinedFE descending
            //    select new
            //    {
            //        Headquarter = manufacturer.Country,
            //        CombFuelEfficiencies = car.CombinedFE,
            //        CarName = car.Model
            //    };


            //foreach (var car in query)
            //{
            //    Console.WriteLine($"{car.Headquarter},  {car.CombFuelEfficiencies}, {car.CarName}");
            //}


        }

        public static void PrintTop2CarsByFuelEfficiencyByManufacturer(IEnumerable<Car> cars)
        {
            var query =
                from car in cars
                group car by car.CompanyName.ToUpper() into manufacturer
                orderby manufacturer.Key
                select manufacturer;

            foreach (var group in query)
            {
                Console.WriteLine(group.Key);
                foreach (var car in group.OrderByDescending(c => c.CombinedFE).Take(2))
                {
                    Console.WriteLine($"\t{car.Model} : {car.CombinedFE}");
                }
            }
                
        }

        public static void PrintTop2CarsByFuelEfficiencyByCountryUsingGroupJoin(IEnumerable<Car> cars, IEnumerable<Manufacturer> manufacturers)
        {
            var query =
                from manufacturer in manufacturers
                join car in cars on manufacturer.CompanyName equals car.CompanyName
                    into carGroup
                orderby manufacturer.CompanyName
                select new
                {
                    CompanyName = manufacturer.CompanyName,
                    Headquarter = manufacturer.Country,
                    cars = carGroup
                };

            foreach (var group in query)
            {
                Console.WriteLine($"{group.CompanyName}:{group.Headquarter}\n");
                foreach (var car in group.cars.OrderByDescending(c => c.CombinedFE).Take(2))
                {
                    Console.WriteLine($"\t{car.Model} : {car.CombinedFE}");
                }
            }
        }



        public static void PrintTop2CarsByFuelEfficiencyByManufacturerUsingExpressionSyntax(IEnumerable<Car> cars)
        {
            var query =
                cars.GroupBy(c => c.CompanyName.ToUpper())
                    .OrderBy(g => g.Key);

            foreach (var group in query)
            {
                Console.WriteLine(group.Key);
                foreach (var car in group.OrderByDescending(c => c.CombinedFE).Take(2))
                {
                    Console.WriteLine($"\t{car.Model} : {car.CombinedFE}");
                }
            }

        }

            public static void PrintCarStatistics(IEnumerable<Car> cars)
        {
            var total = 0;
            var min = Int32.MaxValue;
            var max = Int32.MinValue;
            var count = 0;
            foreach (var car in cars)
            {
                total += car.CombinedFE;
                if (min > car.CombinedFE)
                {
                    min = car.CombinedFE;
                }
                if (max < car.CombinedFE)
                {
                    max = car.CombinedFE;
                }
                count++;
            }

            Console.WriteLine("Statistics about the cars:");

            Console.WriteLine($"\tThe total combined fuel efficiency is {total}.\n\tThe maximum combined fuel efficency is {max}. " +
                              $"\n\tThe minimum combined fuel efficincy is {min}.\n\tThe total number of cars is {count}.");
        }

        public static void PrintCarStatisticsByManufacturer(IEnumerable<Car> cars, string manufacturer)
        {
            //avg, max, min, count
        }

    }
}
