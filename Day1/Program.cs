using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day1
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = File.ReadLines("input.txt")
                .Select(double.Parse)
                .ToArray();
            
            Part1(input);
            Part2(input);
        }

        private static void Part1(IEnumerable<double> input)
        {
            var output = input
                .Select(x => x / 3.0)
                .Select(Math.Floor)
                .Select(x => x - 2)
                .Sum(x => x);

            Console.WriteLine($"Part 1: {output}");
        }

        private static void Part2(IEnumerable<double> input)
        {
            var output = input
                .Select(CalculateFuel)
                .Sum(x => x);

            Console.WriteLine($"Part 2: {output}");
        }

        private static double CalculateFuel(double theThing)
        {
            var result = Math.Floor(theThing / 3.0) - 2;
            if (result <= 0)
            {
                return 0;
            }

            return result + CalculateFuel(result);
        }
    }
}