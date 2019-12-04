using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day3
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = File.ReadLines("input.txt")
                .Take(2)
                .Select(x =>
                    x.Split(',').Select(y =>
                        (direction: y.Take(1).Single(), amount: int.Parse(string.Concat(y.Skip(1))))))
                .ToArray();

            Part1(input);
            Part2(input);
        }

        private static void Part1(IEnumerable<(char direction, int amount)>[] input)
        {
            var wirePath1 = GetPath(input.First());
            var wirePath2 = GetPath(input.Skip(1).First());

            var answer = wirePath1.Intersect(wirePath2)
                .Where(point => point != (0, 0))
                .Min(point => Math.Abs(point.x) + Math.Abs(point.y));

            Console.WriteLine(answer);
        }

        private static void Part2(IEnumerable<(char direction, int amount)>[] input)
        {
            var wirePath1 = GetPath(input.First()).ToArray();
            var wirePath2 = GetPath(input.Skip(1).First()).ToArray();
            var intersections = wirePath1.Intersect(wirePath2)
                .Where(point => point != (0, 0))
                .ToArray();

            var stepsToIntersections1 = GetStepsForIntersections(wirePath1, intersections).OrderBy(tuple => tuple.point);
            var stepsToIntersections2 = GetStepsForIntersections(wirePath2, intersections).OrderBy(tuple => tuple.point);

            var answer = stepsToIntersections1.Zip(stepsToIntersections2, (tuple1, tuple2) => tuple1.steps + tuple2.steps)
                .Min();

            Console.WriteLine(answer);
        }

        private static IEnumerable<(int x, int y)> GetPath(IEnumerable<(char direction, int amount)> wireDirections)
        {
            var finalPath = new List<(int x, int y)> {(0, 0)};
            foreach (var wireDirection in wireDirections)
            {
                switch (wireDirection.direction)
                {
                    case 'U':
                    {
                        var (x, y) = finalPath.Last();
                        for (var i = 1; i <= wireDirection.amount; i++)
                        {
                            finalPath.Add((x + i, y));
                        }

                        break;
                    }
                    case 'D':
                    {
                        var (x, y) = finalPath.Last();
                        for (var i = 1; i <= wireDirection.amount; i++)
                        {
                            finalPath.Add((x - i, y));
                        }

                        break;
                    }
                    case 'L':
                    {
                        var (x, y) = finalPath.Last();
                        for (var i = 1; i <= wireDirection.amount; i++)
                        {
                            finalPath.Add((x, y - i));
                        }

                        break;
                    }
                    case 'R':
                    {
                        var (x, y) = finalPath.Last();
                        for (var i = 1; i <= wireDirection.amount; i++)
                        {
                            finalPath.Add((x, y + i));
                        }

                        break;
                    }
                    default:
                    {
                        throw new ArgumentOutOfRangeException();
                    }
                }
            }

            return finalPath;
        }

        private static IEnumerable<(int steps, (int x, int y) point)> GetStepsForIntersections(
            (int x, int y)[] wirePath, (int x, int y)[] intersections)
        {
            var points = new List<(int steps, (int x, int y) point)>();
            for (var i = 0; i < wirePath.Length; i++)
            {
                if (intersections.Any(intersectionPoint => intersectionPoint == wirePath[i]))
                {
                    points.Add((i, wirePath[i]));
                }
            }

            return points;
        }
    }
}