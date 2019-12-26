using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2019
{
    public class Day04 : IDay
    {
        public void Part1()
        {
            var input = Helper.ReadEmbeddedFile(GetType().Assembly, $"Input.{GetType().Name}.txt");
            Console.WriteLine($"{GetType().Name} Part 1: {SolvePart1(input)} (Expected: 1033)");
        }

        public void Part2()
        {
            var input = Helper.ReadEmbeddedFile(GetType().Assembly, $"Input.{GetType().Name}.txt");
            Console.WriteLine($"{GetType().Name} Part 2: {SolvePart2(input)} (Expected: 670)");
        }

        private static int SolvePart1(string input)
        {
            var strings = input.Split("-");
            var lowerBorder = int.Parse(strings[0]);
            var upperBorder = int.Parse(strings[1]);

            var possibleValues = new List<int>();

            for (var i = lowerBorder; i < upperBorder; i++)
            {
                var str = i.ToString();

                if (TwoAdjacentDigits(str) && NeverDecrease(str))
                {
                    possibleValues.Add(i);
                }
            }

            return possibleValues.Count;
        }

        private static int SolvePart2(string input)
        {
            var strings = input.Split("-");
            var lowerBorder = int.Parse(strings[0]);
            var upperBorder = int.Parse(strings[1]);

            var possibleValues = new List<int>();

            for (var i = lowerBorder; i < upperBorder; i++)
            {
                var str = i.ToString();

                if (TwoAdjacentDigits2(str) && NeverDecrease(str))
                {
                    possibleValues.Add(i);
                    Console.WriteLine(i);
                }
            }

            return possibleValues.Count;
        }

        private static bool TwoAdjacentDigits(string str)
        {
            for (var i = 0; i < str.Length - 1; i++)
            {
                if (str[i] == str[i + 1])
                {
                    return true;
                }
            }

            return false;
        }

        private static bool TwoAdjacentDigits2(string str)
        {
            foreach (var s in str)
            {
                if (str.Count(c => c == s) == 2)
                {
                    return true;
                }
            }

            return false;
        }

        private static bool NeverDecrease(string str)
        {
            for (var i = 0; i < str.Length - 1; i++)
            {
                if (str[i] > str[i + 1])
                {
                    return false;
                }
            }

            return true;
        }
    }
}