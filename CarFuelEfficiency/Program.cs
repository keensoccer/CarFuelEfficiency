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
            var manufacturers = File.ReadAllLines("manufacturers.csv.txt");

            var cars = Car.CarFileToListOfCars("fuel.csv");

            PrintTopNCarsByFuelEfficiences(cars, 6); //2nd parameter can be any number between 0 and 1204.
            Console.WriteLine("*****************************");
            PrintCarStatistics(cars);
            Console.WriteLine("*****************************");

            

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

        public static void PrintTopNCarsByFuelEfficiencesByManufacturer(IEnumerable<Car> cars, string manufacturer, int n)
        {

        }

        public static void PrintCarStatistics(IEnumerable<Car> cars)
        {
            var total = 0;
            var min = Int32.MaxValue;
            var max = Int32.MinValue;
            var count = 0-1;
            foreach (var car in cars)
            {
                total += car.CombinedFE;
                if(min > car.CombinedFE)
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
