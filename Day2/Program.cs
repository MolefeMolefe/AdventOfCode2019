using System;
using System.IO;
using System.Linq;

namespace Day2
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = File.ReadLines("input.txt")
                .Take(1)
                .SelectMany(x => x.Split(','))
                .Select(int.Parse)
                .ToArray();

            Part1(input.ToArray());
            Part2(input.ToArray());
        }

        private static void Part1(int[] input)
        {
            Console.WriteLine(DoTheThing(input));
        }

        private static void Part2(int[] input)
        {
            for (var noun = 0; noun <= 99; noun++)
            {
                for (var verb = 0; verb <= 99; verb++)
                {
                    var newThing = input.ToArray();
                    
                    newThing[1] = noun;
                    newThing[2] = verb;
                    
                    if (DoTheThing(newThing) == 19690720)
                    {
                        Console.WriteLine($"Noun = `{noun}` & Verb = `{verb}`");
                        return;
                    }
                }
            }
        }

        private static int DoTheThing(int[] input)
        {
            var index = 0;

            while (input[index] != 99)
            {
                var parameterIndex1 = input[index + 1];
                var parameterIndex2 = input[index + 2];
                var answerIndex = input[index + 3];

                switch (input[index])
                {
                    case 1:
                    {
                        input[answerIndex] = input[parameterIndex1] + input[parameterIndex2];
                        break;
                    }
                    case 2:
                    {
                        input[answerIndex] = input[parameterIndex1] * input[parameterIndex2];
                        break;
                    }
                    default:
                    {
                        throw new ArgumentOutOfRangeException();
                    }
                }

                index += 4;
            }

            return input[0];
        }
    }
}