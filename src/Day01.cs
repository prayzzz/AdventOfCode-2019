using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualBasic.CompilerServices;

namespace AdventOfCode2019
{
    public class Day01 : IDay
    {
        public void Part1()
        {
            var input = Helper.ReadEmbeddedFile(GetType().Assembly, $"Input.{GetType().Name}.txt");
            Console.WriteLine($"{GetType().Name} Part 1: {SolvePart1(input)} (Expected: 3173518)");
        }

        public void Part2()
        {
            var input = Helper.ReadEmbeddedFile(GetType().Assembly, $"Input.{GetType().Name}.txt");
            Console.WriteLine($"{GetType().Name} Part 2: {SolvePart2(input)} (Expected: 4757427)");
        }

        private static double SolvePart1(string input)
        {
            var lines = input.Trim().Split("\n");

            var sum = 0.0;
            foreach (var line in lines)
            {
                var i = int.Parse(line);
                var requiredFuel = Math.Floor(i / 3.0) - 2;

                sum += requiredFuel;
            }

            return sum;
        }

        private static double SolvePart2(string input)
        {
            var lines = input.Trim().Split("\n");

            var sum = 0.0;
            foreach (var line in lines)
            {
                var i = double.Parse(line);
                while (true)
                {
                    var requiredFuel = Math.Floor(i / 3.0) - 2;

                    if (requiredFuel >= 0)
                    {
                        sum += requiredFuel;
                        i = requiredFuel;
                    }
                    else
                    {
                        break;
                    }
                }
            }

            return sum;
        }
    }
}
